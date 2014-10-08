
#pragma warning(disable: 4996)

#include <vector>
using namespace std;



#include <ac/zlib.h>
#include <ac/png.h>
#include "./libpng/pngstruct.h"

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdarg.h>
#include <time.h>
#include<errno.h>

#define _WIN32_WINNT 0x0500
#include <Windows.h>

#include <sys/types.h>
#include <sys/stat.h>
#include <io.h>
#include <direct.h>


#define _GL_T_UBYTE			0x1401
#define _GL_F_RGB			0x1907
#define _GL_F_RGBA			0x1908
#define _GL_GEN_MIPMAP		0x8191


#define BOUND_WIDTH 100000


int _LcFile_inf(char *sFile, unsigned long* size, unsigned long* time_m, unsigned long* time_c)
{
	int hr = 0;

#if defined(_MSC_VER)
	struct _stat st;
	hr = _stat(sFile, &st);

#else
	struct stat st ;
	hr = stat( sFile, &st);
#endif

	if(0 != hr)
	{
		//switch(errno)
		//{
		//	case ENOENT:
		//		LOGE("LcFile_Info::Err::File Not Found:%s\n", sFile);
		//		break;
		//	default:
		//		LOGE("LcFile_Info::Err::%s:%s\n", strerror(errno), sFile);
		//}

		if(size   )	*size   = 0;
		if(time_m )	*time_m = 0;
		if(time_c )	*time_c = 0;

		return -1;
	}

	if(size   )	*size   = (unsigned long)st.st_size;
	if(time_m )	*time_m = (unsigned long)st.st_mtime;
	if(time_c )	*time_c = (unsigned long)st.st_ctime;

	return 0;
}



int Png_CheckSignature(unsigned char* sig)
{
	return !png_sig_cmp((png_bytep)sig, 0, 8);
}


int Png_FileRead( unsigned char**    poPxl		// Output Pixel
				, unsigned int*     poImgT		// Output Image Pixel Type
				, unsigned int*     poImgF		// Output Image Pixel Format
				, int*      poImgW		// Output Image Width
				, int*      poImgH		// Output Image Height
				, int*      poImgD		// Output Image Depth
				, unsigned int*     poImgC		// Output Image Color Key
				, unsigned long*    poTime		// modification:0 and creation time:1 of original file. it needs unsigned long * 2
				, char*         sFile		// Source File
				)
{
	FILE*			fp;
	unsigned char        pbSig[8];
	int             iBitDepth;
	int             iColorType;
	double          dGamma;
	png_color_16*	pBackground;
	png_uint_32		ulChannels;
	png_uint_32		ulRowBytes;
	unsigned char*		pPixel		= NULL;
	unsigned char**		ppbRowPointers = NULL;

	png_structp		png_ptr = NULL;
	png_infop		info_ptr = NULL;

	png_uint_16		oBgR	= 0;
	png_uint_16		oBgG	= 0;
	png_uint_16		oBgB	= 0;

	unsigned long		h_inf[3] = {0,};

	long	i = 0;
	int		nRead = 0;

	fp = fopen(sFile, "rb");

	if (NULL == fp)
		return -1;


	// first check the eight byte PNG signature
	nRead = fread(pbSig, 1, 8, fp);

	//if (!png_check_sig(pbSig, 8))
	if (!!png_sig_cmp(pbSig, 0, 8))
		goto LOAD_IMAGE_ERROR;

	// create the two png(-info) structures
	png_ptr = png_create_read_struct(PNG_LIBPNG_VER_STRING, NULL
							, (png_error_ptr)NULL, (png_error_ptr)NULL);

	if (!png_ptr)
		goto LOAD_IMAGE_ERROR;


	info_ptr = png_create_info_struct(png_ptr);

	if (!info_ptr)
		goto LOAD_IMAGE_ERROR;


	// initialize the png structure
	png_set_read_fn(png_ptr, (png_voidp)fp, NULL);



	png_set_sig_bytes(png_ptr, 8);


	// read all PNG info up to image data
	png_read_info(png_ptr, info_ptr);

	// get width, height, bit-depth and color-type
	png_get_IHDR(png_ptr
		, info_ptr
		, (png_uint_32 *)poImgW
		, (png_uint_32 *)poImgH
		, &iBitDepth
		, &iColorType
		, NULL, NULL, NULL);

	// expand images of all color-type and bit-depth to 3x8 bit RGB images
	// let the library process things like alpha, transparency, background

	if (iBitDepth == 16)
		png_set_strip_16(png_ptr);

	if (iColorType == PNG_COLOR_TYPE_PALETTE)
		png_set_expand(png_ptr);

	if (iBitDepth < 8)
		png_set_expand(png_ptr);

	if (png_get_valid(png_ptr, info_ptr, PNG_INFO_tRNS))
		png_set_expand(png_ptr);

	if (iColorType == PNG_COLOR_TYPE_GRAY ||
		iColorType == PNG_COLOR_TYPE_GRAY_ALPHA)
		png_set_gray_to_rgb(png_ptr);

	// set the background color to draw transparent and alpha images over.
	if (png_get_bKGD(png_ptr, info_ptr, &pBackground))
	{
		png_set_background(png_ptr, pBackground, PNG_BACKGROUND_GAMMA_FILE, 1, 1.0);

		oBgR	= pBackground->red;
		oBgG	= pBackground->green;
		oBgB	= pBackground->blue;
	}

	// if required set gamma conversion
	if (png_get_gAMA(png_ptr, info_ptr, &dGamma))
		png_set_gamma(png_ptr, (double) 2.2, dGamma);

	// after the transformations have been registered update info_ptr data
	png_read_update_info(png_ptr, info_ptr);

	// get again width, height and the new bit-depth and color-type
	png_get_IHDR(png_ptr
		, info_ptr
		, (png_uint_32 *)poImgW
		, (png_uint_32 *)poImgH
		, &iBitDepth
		, &iColorType
		, NULL, NULL, NULL);


	// row_bytes is the width x number of channels
	ulRowBytes = png_get_rowbytes(png_ptr, info_ptr);
	ulChannels = png_get_channels(png_ptr, info_ptr);


	// Setup Channel
	*poImgD= (int)ulChannels;


	// now we can allocate memory to store the image
	pPixel = (unsigned char*)malloc(ulRowBytes * (*poImgH) * sizeof(unsigned char));

	if(NULL == pPixel)
		goto LOAD_IMAGE_ERROR;


	// and allocate memory for an array of row-pointers
	ppbRowPointers = (png_bytepp)malloc( (*poImgH) * sizeof(png_bytep));

	if( NULL == ppbRowPointers)
		goto LOAD_IMAGE_ERROR;




	// set the individual row-pointers to point at the correct offsets
	for(i = 0; i < (*poImgH); i++)
		ppbRowPointers[i] = pPixel + i * ulRowBytes;


	// now we can go ahead and just read the whole image
	png_read_image(png_ptr, ppbRowPointers);

	// read the additional chunks in the PNG file (not really needed)
	png_read_end(png_ptr, NULL);

	// yepp, done
	fclose (fp);



	// and we're done
	free(	ppbRowPointers	);
	ppbRowPointers = NULL;

	// free
	png_destroy_read_struct(&png_ptr, &info_ptr, NULL);



	_LcFile_inf(sFile, &h_inf[0], &h_inf[1], &h_inf[2]);

	// copy to output data.
	if(poPxl )	*poPxl  = pPixel;
	if(poImgT)	*poImgT	= _GL_T_UBYTE;
	if(poImgF)	*poImgF = (3 == *poImgD)? _GL_F_RGB : _GL_F_RGBA;
	if(poImgC)	*poImgC	= 0x0;
	if(poTime)	memcpy(poTime, &h_inf[1], sizeof(unsigned long)* 2);

	return 0;


LOAD_IMAGE_ERROR:

	fclose(fp);

	png_destroy_read_struct(&png_ptr, &info_ptr, NULL);
	free(	ppbRowPointers	);
	ppbRowPointers = NULL;

	return -1;
}


static void png_read_data(png_structp png_ptr, png_bytep data, png_size_t length)
{
	//	png_size_t check;

	/* fread() returns 0 on error, so it is OK to store this in a png_size_t
	* instead of an int, which is what fread() actually returns.
	*/

	char* it = NULL;
	
	it = *((char**)png_ptr->io_ptr);

	memcpy(data, it, length);

	it += length;

	*((char**)png_ptr->io_ptr) = it;


	//if (check != length)
	//{
	//	png_error(png_ptr, "Read Error");
	//}
}



int Png_MemRead(  unsigned char**    poPxl		// Output Pixel
				, unsigned int*     poImgT		// Output Image Pixel Type
				, unsigned int*     poImgF		// Output Image Pixel Format
				, int*      poImgW		// Output Image Width
				, int*      poImgH		// Output Image Height
				, int*      poImgD		// Output Image Depth
				, unsigned int*     poImgC		// Output Image Color Key
				, const void*   memAddr		// Memory buffer address
				, int       memSize		// Memory Size
				)
{
	unsigned char        pbSig[8];
	int             iBitDepth;
	int             iColorType;
	double          dGamma;
	png_color_16*	pBackground;
	png_uint_32		ulChannels;
	png_uint_32		ulRowBytes;
	unsigned char*		pPixel		= NULL;
	unsigned char**		ppbRowPointers = NULL;

	png_structp		png_ptr = NULL;
	png_infop		info_ptr = NULL;

	png_uint_16		oBgR	= 0;
	png_uint_16		oBgG	= 0;
	png_uint_16		oBgB	= 0;

	long			i = 0;


	char*		pSrc = (char*)memAddr;
	char*		it	 = pSrc;
	//long		lSize= memSize;



	// first check the eight byte PNG signature
	memcpy(pbSig, it, 8);		it += 8;

	//if (!png_check_sig(pbSig, 8))
	if (!!png_sig_cmp(pbSig, 0, 8))
		goto LOAD_IMAGE_ERROR;

	// create the two png(-info) structures
	png_ptr = png_create_read_struct(PNG_LIBPNG_VER_STRING, NULL, (png_error_ptr)NULL, (png_error_ptr)NULL);

	if (!png_ptr)
		goto LOAD_IMAGE_ERROR;


	info_ptr = png_create_info_struct(png_ptr);

	if (!info_ptr)
		goto LOAD_IMAGE_ERROR;


	// initialize the png structure
	png_set_read_fn(png_ptr, (png_voidp)&it, png_read_data);



	png_set_sig_bytes(png_ptr, 8);


	// read all PNG info up to image data
	png_read_info(png_ptr, info_ptr);

	// get width, height, bit-depth and color-type
	png_get_IHDR(png_ptr
		, info_ptr
		, (png_uint_32 *)poImgW
		, (png_uint_32 *)poImgH
		, &iBitDepth
		, &iColorType
		, NULL, NULL, NULL);

	// expand images of all color-type and bit-depth to 3x8 bit RGB images
	// let the library process things like alpha, transparency, background

	if (iBitDepth == 16)
		png_set_strip_16(png_ptr);

	if (iColorType == PNG_COLOR_TYPE_PALETTE)
		png_set_expand(png_ptr);

	if (iBitDepth < 8)
		png_set_expand(png_ptr);

	if (png_get_valid(png_ptr, info_ptr, PNG_INFO_tRNS))
		png_set_expand(png_ptr);

	if (iColorType == PNG_COLOR_TYPE_GRAY ||
		iColorType == PNG_COLOR_TYPE_GRAY_ALPHA)
		png_set_gray_to_rgb(png_ptr);

	// set the background color to draw transparent and alpha images over.
	if (png_get_bKGD(png_ptr, info_ptr, &pBackground))
	{
		png_set_background(png_ptr, pBackground, PNG_BACKGROUND_GAMMA_FILE, 1, 1.0);

		oBgR	= pBackground->red;
		oBgG	= pBackground->green;
		oBgB	= pBackground->blue;
	}

	// if required set gamma conversion
	if (png_get_gAMA(png_ptr, info_ptr, &dGamma))
		png_set_gamma(png_ptr, (double) 2.2, dGamma);

	// after the transformations have been registered update info_ptr data
	png_read_update_info(png_ptr, info_ptr);

	// get again width, height and the new bit-depth and color-type
	png_get_IHDR(png_ptr
		, info_ptr
		, (png_uint_32 *)poImgW
		, (png_uint_32 *)poImgH
		, &iBitDepth
		, &iColorType
		, NULL, NULL, NULL);


	// row_bytes is the width x number of channels
	ulRowBytes = png_get_rowbytes(png_ptr, info_ptr);
	ulChannels = png_get_channels(png_ptr, info_ptr);


	// Setup Channel
	*poImgD= (int)ulChannels;


	// now we can allocate memory to store the image
	pPixel = (unsigned char*)malloc(ulRowBytes * (*poImgH) * sizeof(unsigned char));

	if(NULL == pPixel)
		goto LOAD_IMAGE_ERROR;


	// and allocate memory for an array of row-pointers
	ppbRowPointers = (png_bytepp)malloc( (*poImgH) * sizeof(png_bytep));

	if( NULL == ppbRowPointers)
		goto LOAD_IMAGE_ERROR;



	// set the individual row-pointers to point at the correct offsets
	for(i = 0; i < (*poImgH); i++)
		ppbRowPointers[i] = pPixel + i * ulRowBytes;


	// now we can go ahead and just read the whole image
	png_read_image(png_ptr, ppbRowPointers);

	// read the additional chunks in the PNG file (not really needed)
	png_read_end(png_ptr, NULL);

	// yepp, done
	//fclose (fp);



	// and we're done
	free(	ppbRowPointers	);
	ppbRowPointers = NULL;

	// free
	png_destroy_read_struct(&png_ptr, &info_ptr, NULL);


	// Setup Output Data
	*poPxl = pPixel;

	return 0;


LOAD_IMAGE_ERROR:

	//fclose(fp);

	png_destroy_read_struct(&png_ptr, &info_ptr, NULL);
	free(	ppbRowPointers	);
	ppbRowPointers = NULL;


	return -1;
}


int GetRectInfo(int* oImgX, int* oImgY, int* oImgW, int* oImgH, char* sFile)
{
	int hr =0;

	unsigned char*	pPxlS	= NULL;				// Pixel Data
	unsigned int	nKind	= 0;				// resource type
	unsigned int	nImgT	= 0;				// pixel type
	unsigned int	nImgF	= 0x0;				// pixel format
	int				nImgW	= 0;
	int				nImgH	= 0;
	int				nImgD	= 0;				// Channel(byte)
	unsigned int	nImgC	= 0x0;				// Color Key
	unsigned long	uTime[2]= {0};				// create/modified time

	int		x, y;
	int		nR, nG, nB, nA;

	int		minX =  BOUND_WIDTH;
	int		minY =  BOUND_WIDTH;
	int		maxX = -BOUND_WIDTH -1;
	int		maxY = -BOUND_WIDTH -1;


	hr = Png_FileRead(&pPxlS, &nImgT, &nImgF, &nImgW, &nImgH, &nImgD, &nImgC, uTime, sFile);
	if(0>hr)
		return hr;

	// there is no alpha channel
	if(4>nImgD)
	{
		if(oImgW) *oImgW = nImgW;
		if(oImgH) *oImgH = nImgH;

		if(oImgX) *oImgX = 0;
		if(oImgY) *oImgY = 0;

		return 0;
	}


	// there exists alpha channel
	for(y=0; y<nImgH; ++y)
	{
		for(x =0; x<nImgW; ++x)
		{
			nR = (y * nImgW + x ) * nImgD + 0;
			nG = (y * nImgW + x ) * nImgD + 1;
			nB = (y * nImgW + x ) * nImgD + 2;
			nA = (y * nImgW + x ) * nImgD + 3;

			int _R = pPxlS[nR];
			int _G = pPxlS[nG];
			int _B = pPxlS[nB];
			int _A = pPxlS[nA];

			if(0 == _A)					// transparent 100%
				continue;

			//if(_R || _G || _B)		// skip black
			{
				if(minX >x)
					minX = x;

				if(minY >y)
					minY = y;

				if(maxX <x)
					maxX = x;

				if(maxY <y)
					maxY = y;
			}
		}
	}

	maxX += 1;
	maxY += 1;


	if( BOUND_WIDTH == minX)minX =  0;
	if( BOUND_WIDTH == minY)minY =  0;
	if(-BOUND_WIDTH == maxX)maxX =  0;
	if(-BOUND_WIDTH == maxY)maxY =  0;


	if(oImgW) *oImgW = maxX - minX;
	if(oImgH) *oImgH = maxY - minY;

	if(oImgX) *oImgX = minX;
	if(oImgY) *oImgY = minY;


	free(pPxlS);
	return 0;
}



int GetPixelInfo(int* orgW, int* orgH, int* orgD, unsigned char**pxl, char* sFile)
{
	int hr =0;

	unsigned char*	pPxlS	= NULL;				// Pixel Data
	unsigned int	nKind	= 0;				// resource type
	unsigned int	nImgT	= 0;				// pixel type
	unsigned int	nImgF	= 0x0;				// pixel format
	int				nImgW	= 0;
	int				nImgH	= 0;
	int				nImgD	= 0;				// Channel(byte)
	unsigned int	nImgC	= 0x0;				// Color Key
	unsigned long	uTime[2]= {0};				// create/modified time


	hr = Png_FileRead(&pPxlS, &nImgT, &nImgF, &nImgW, &nImgH, &nImgD, &nImgC, uTime, sFile);
	if(0>hr)
		return hr;


	if(orgW) *orgW = nImgW;
	if(orgH) *orgH = nImgH;
	if(orgD) *orgD = nImgD;

	if(pxl )
	{
		*pxl   = pPxlS;
	}
	else
	{
		if(pPxlS)
			free(pPxlS);
	}

	return 0;
}


int Xcopy(char* dst, char* src)
{
	FILE*	fps = NULL;
	FILE*	fpd = NULL;

	char	sbuf[8092];
	int		r;

	fps = fopen(src, "rb");
	fpd = fopen(dst, "wb");


	if(NULL == fps || NULL == fpd)
		return -1;

	while(!feof(fps))
	{
		r = fread(sbuf, 1, 8092, fps);
		if(0 == r)
			continue;

		fwrite(sbuf, 1, r, fpd);
	}

	fclose(fps);
	fclose(fpd);

	return 0;
}


char* _LcStrSplitFindBegin(char *strToken, char* delimiter)
{
	char *s = strToken;
	char *d = delimiter;

	int bFind = 0;

	for( ;'\0'!= *s; ++s)
	{
		bFind = 0;
		for(d = delimiter; '\0'!= *d; ++d)
		{
			if( *s == *d)
			{
				bFind = 1;
				break;
			}
		}

		if(bFind)
			continue;

		return s;
	}

	return NULL;
}

char* _LcStrSplitFindEnd(char *strToken, char* delimiter)
{
	char *s = strToken;
	char *d = delimiter;

	for( ;'\0'!= *s; ++s)
	{
		for(d = delimiter; '\0'!= *d; ++d)
		{
			if( *s == *d)
			{
				return s;
			}
		}
	}

	return s;
}


int LcStrSplit(vector<char* > * vStrtok, char *strToken, char* delimiter)
{
	char *b = NULL, *e = strToken;
	int n =0;
	int s =0;

	char* str = NULL;

	while(1)
	{
		b = _LcStrSplitFindBegin(e, delimiter);

		if(NULL == b)
			break;

		e = _LcStrSplitFindEnd(b, delimiter);

		s = e - b;
		char* str = new char[ s+1];
		char* t = str;
		memset(str, 0, s+1);

		for(int k= 0; k<s; ++k)
			*(t++) = *(b++);

		vStrtok->push_back(str);

		printf("%s\n", str);
		++n;
	}

	return n;
}


void LcStrSplitClear(vector<char* > * vStrtok)
{
	for(int i=0; i<(int)vStrtok->size(); ++i)
		free( (*vStrtok)[i]);

	(*vStrtok).clear();
}

