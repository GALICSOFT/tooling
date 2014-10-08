
#pragma warning(disable: 4996)

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <direct.h>
#include <io.h>
#include <errno.h>

#include "imgapp.h"


typedef set<string,less<string> >	stStr;
typedef vector<string >				lsStr;
typedef vector<ImgRec >				lsRec;
typedef vector<ImgRec* >			lpRec;


extern int      GetRectInfo (int* oImgX, int* oImgY, int* oImgW, int* oImgH, char* sFile);
extern int      GetPixelInfo(int* orgW, int* orgH, int* orgD, unsigned char**pxl, char* sFile);
extern int      Xcopy(char* dst, char* src);
extern int      LcStrSplit(vector<char* > * vStrtok, char *strToken, char* sep);
extern void     LcStrSplitClear(vector<char* > * vStrtok);

const static char	g_tagrct[] = "zzzrf2yr_";
const static char	g_dirSrc[] = "img_src";
const static char	g_dirDst[] = "img_dst";
      static char	g_file  [260] = {0};


lsStr				g_vGroup;
lsStr				g_vImgName;
lsRec				g_vImgOrg;

lpRec				g_vImgBg  ;				// <bg
lpRec				g_vImgRct ;				// <rct
lpRec				g_vImgTbar;				// <tbar
lpRec				g_vImgBtn ;				// <btn
lpRec				g_vImgSbtn;				// <sbtn
lpRec				g_vImgCbtn;				// <cbtn
lpRec				g_vImgVbtn;				// <vbtn
lpRec				g_vImgHbtn;				// <hbtn

lpRec				g_vImgMark;				// <mk marker
lpRec				g_vImgStck;				// <st sticker
lpRec				g_vImgAnim;				// <ani Animation icon
lpRec				g_vImgScl ;				// <scl will be scaling



const char* DirSrc()
{
	return g_dirSrc;
}

const char* DirDst()
{
	return g_dirDst;
}


// gather image file name
void GetherGroup(const char* folder)
{
	int			result=1;
	char		sFile[_MAX_PATH]={0};

	sprintf(sFile, "%s/000000_apg_grp.txt", folder);

	FILE* fp = fopen(sFile, "rt");
	if(NULL == fp)
		return;

	char sLine[260]={0};
	while(!feof(fp))
	{
		memset(sLine, 0, sizeof(sLine));
		fgets(sLine, 256, fp);
		if(3>strlen(sLine))
			break;

		int len = strlen(sLine);

		--len;
		for(int k=0; len>0 && k<2; --len, ++k)
		{
			if( '\r' == *(sLine +len) ||
				'\n' == *(sLine +len)  )
			*(sLine +len) =0;
		}

		g_vGroup.push_back(sLine);
	}
}



// gather image file name
void GetherImageInfo(const char* folder)
{
	intptr_t	fh;
	_finddata_t	fd;
	int			result=1;
	char		sDir [_MAX_PATH]={0};

	sprintf(sDir, "%s/*.*", folder);

	fh = _findfirst(sDir, &fd);
	if(-1L == fh )
		return;

	while (result != -1)
	{
		if( (fd.attrib & 0x20) && '.' != fd.name[0])
		{
			char* sFile = fd.name;

			strlwr(sFile);											// lower
			if(NULL != strstr(sFile, ".png"))						// find .png
			{
				if(0 != _strnicmp(sFile, g_tagrct, sizeof(g_tagrct)-1))
				{

					char fname[_MAX_PATH]={0};
					char ext  [_MAX_EXT]={0};
					char sdx  [8] = {0};
					int  idx  = 0;

					_splitpath(sFile, NULL, NULL, fname, ext );


					ImgRec rec;

					strncpy(sdx, fname, 5);
					idx = atoi(sdx);

					rec.idx   = idx;
					rec.sName = (char*)&fname[6];
					rec.sFile = sFile;
					rec.sRct  = g_tagrct;
					rec.sRct += sFile;
					rec.sOrg  = sFile;


					g_vImgOrg.push_back(rec);
				}
			}
		}


		result= _findnext(fh, &fd);
	}

	_findclose(fh);

	int		iOrgSize = g_vImgOrg.size();
	int		i  = 0, j=0, k=0;
	int		hr = 0;

	ImgRec*	rec  = NULL;

	if(0>= iOrgSize)
		return;

	rec  = &g_vImgOrg[0];

	char* name = (char*)rec->sName.c_str();
	char* str  = (char*)strchr(name, '_');

	int len = str - name;

	strncpy(g_file, name, len);

	//if(0 != _strnicmp(sFile, g_tagrct, sizeof(g_tagrct)-1))

	//g_file



	std::reverse(g_vImgOrg.begin(), g_vImgOrg.end());



	// gather image info
	for(i=0; i<iOrgSize; ++i)
	{
		int				imgX =0, imgY=0, imgW=0, imgH=0;
		int				orgW, orgH, orgD;
		unsigned char*	pxl = NULL;

		char			sOrg [_MAX_PATH]= {0};
		char			sDst [_MAX_PATH]= {0};

		rec  = &g_vImgOrg[i];

		sprintf(sOrg, "%s/%s", folder, (char*)rec->sRct.c_str());
		sprintf(sDst, "%s/%s", folder, (char*)rec->sFile.c_str());

		hr = GetRectInfo(&imgX, &imgY, &imgW, &imgH, sOrg);		// get the image information

		rec->imgX = imgX;
		rec->imgY = imgY;
		rec->imgW = imgW;
		rec->imgH = imgH;

		hr = GetPixelInfo(&orgW, &orgH, &orgD, &pxl, sDst);		// get the pixel information
		rec->orgW = orgW;
		rec->orgH = orgH;
		rec->orgD = orgD;
		rec->pxl  = pxl;
	}


	// gather file name
	ImgRec*	dst  = NULL;
	int		idx  = -1;
	int		w = 0;
	int		h = 0;
	int		d = 0;
	int		s = 0;

	for(i=0; i<iOrgSize; ++i)
	{
		rec  = &g_vImgOrg[i];
		dst  = NULL;
		idx  = -1;
		w = rec->orgW;
		h = rec->orgH;
		d = rec->orgD;
		s = w * h * d;

		for(j=0; j<i; ++j)
		{
			dst  = &g_vImgOrg[j];

			if(	w != dst->orgW ||
				h != dst->orgH ||
				d != dst->orgD )
				continue;


			for(k=0; k<s; ++k)
			{
				if( rec->pxl[k] != dst->pxl[k])
					break;
			}

			if(k!=s)
				continue;


			idx = j;
			break;
		}

		if(0 <= idx)
			rec->sFile = g_vImgOrg[idx].sName;
		else
			rec->sFile = rec->sName;


		//rec->sFile += ".png";
	}



	// gather file name to set container
	stStr	vImgName;
	for(i=0; i<iOrgSize; ++i)
	{
		rec  = &g_vImgOrg[i];
		vImgName.insert(rec->sName);
	}

	for(stStr::iterator it  = vImgName.begin();
						 it != vImgName.end();
						 ++it)
	{
		g_vImgName.push_back(*it);
	}


	// gather background
	for(i=0; i<iOrgSize; ++i)
	{
		rec  = &g_vImgOrg[i];
		string& name =  rec->sName;

		if     (string::npos != name.find("_rct" ))			g_vImgRct .push_back(rec);
		else if(string::npos != name.find("_tbar"))			g_vImgTbar.push_back(rec);
		else if(string::npos != name.find("_btn" ))			g_vImgBtn .push_back(rec);
		else if(string::npos != name.find("_sbtn"))			g_vImgSbtn.push_back(rec);
		else if(string::npos != name.find("_cbtn"))			g_vImgCbtn.push_back(rec);
		else if(string::npos != name.find("_vbtn"))			g_vImgVbtn.push_back(rec);
		else if(string::npos != name.find("_hbtn"))			g_vImgHbtn.push_back(rec);

		else if(string::npos != name.find("_mk" ))			g_vImgMark.push_back(rec);
		else if(string::npos != name.find("_st" ))			g_vImgStck.push_back(rec);
		else if(string::npos != name.find("_ani"))			g_vImgAnim.push_back(rec);
		else if(string::npos != name.find("_scl"))			g_vImgScl .push_back(rec);
		else												g_vImgBg  .push_back(rec);
	}
}




void CopyToDst(const char* dirSrc, const char* dirDst)
{
	for(int i=0; i<(int)g_vImgOrg.size(); ++i)
	{
		ImgRec*	rec = &g_vImgOrg[i];
		char*	org = (char*)rec->sOrg.c_str();
		char*	file= (char*)rec->sFile.c_str();

		char	sOrg    [_MAX_PATH] = {0};
		char	sDst    [_MAX_PATH] = {0};

		sprintf(sOrg, "%s/%s", dirSrc, org);
		sprintf(sDst, "%s/%s.png", dirDst, file);


		// copy image to dest folder
		Xcopy(sDst, sOrg);
	}
}


int SizeImageInfo()
{
	return (int)g_vImgOrg.size();
}


void ReleaseImageInfo()
{
	g_vImgName.clear();


	for(int i=0; i<(int)g_vImgOrg.size(); ++i)
	{
		ImgRec*	rec = &g_vImgOrg[i];
		free(rec->pxl);
	}

	g_vImgOrg.clear();
}


int WriteToCsv(const char* sDst)
{
	FILE*	fp = NULL;
	char	sFile[_MAX_PATH] = {0};
	sprintf(sFile, "%s/%s.csv", sDst, g_file);

	fp = fopen(sFile, "wt");
	if(!fp)
	{
		printf("Err: dest csv file for saving information of image files open failed.\n");
		return 0;
	}

	int iSize = (int)g_vImgOrg.size();
	int i=0;

	for(i=0; i<iSize; ++i)
	{
		ImgRec*	rec = &g_vImgOrg[i];
		char*	name = (char*)rec->sName.c_str();
		char*	file = (char*)rec->sFile.c_str();

		fprintf(fp, "%d;%d;%d;%d;%d;%s;%s\n"
					, i
					, rec->imgX
					, rec->imgY
					, rec->imgW
					, rec->imgH, name, file);
	}


	if(fp)
		fclose(fp);

	return 0;
}

int WriteToJsn(const char* sDst)
{
	FILE*	fp = NULL;
	char	sFile[_MAX_PATH] = {0};
	sprintf(sFile, "%s/%s.json", sDst, g_file);

	fp = fopen(sFile, "wt");
	if(!fp)
	{
		printf("Err: dest json file for saving information of image files open failed.\n");
		return 0;
	}

	fprintf(fp, "{\n");
	fprintf(fp, "	\"lst_disp\":\n\t[\n");

	int iSize = (int)g_vImgOrg.size();
	int i=0;

	for(i=0; i<iSize; ++i)
	{
		ImgRec*	rec = &g_vImgOrg[i];
		char*	name= (char*)rec->sName.c_str();
		char*	file= (char*)rec->sFile.c_str();

		fprintf(fp, "		{ \"id\": %3d, \"x\": %4d, \"y\":%4d, \"w\":%4d, \"h\":%4d, \"n\":\"%s\", \"file\":\"%s\" }"
					, i
					, rec->imgX
					, rec->imgY
					, rec->imgW
					, rec->imgH, name, file);


		if(0 < i)
			fprintf(fp, ",");

		fprintf(fp, "\n");
	}

	fprintf(fp, "\t]\n");
	fprintf(fp, "}\n");


	if(fp)
		fclose(fp);

	return 0;
}

int WriteToXml(const char* sDst)
{
	int iGroup = (int)g_vGroup.size();

	for(int k=0; k<iGroup; ++k)
	{
		int id=0;

		char sgrp[256]={0};
		int  len = 0;
		

		sprintf(sgrp, "%s", (char*)g_vGroup[k].c_str());


		FILE*	fp = NULL;
		char	sFile[_MAX_PATH] = {0};
		sprintf(sFile, "%s/%s.grp", sDst, sgrp);


		strcat(sgrp, "_");
		len = strlen(sgrp);


		fp = fopen(sFile, "wt");
		if(!fp)
		{
			printf("Err: dest xml file for saving information of image files open failed.\n");
			return 0;
		}


		fprintf(fp, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<grp>\n");

		int		iSize = 0;
		int		i=0, j=0;
		ImgRec*	rec = NULL;
		char*	name= NULL;
		char*	file= NULL;

		ImgRec*	grp_rct = NULL;

		int		pos_x = 0;
		int		pos_y = 0;


		iSize = (int)g_vImgRct.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgRct[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				grp_rct = rec;
				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgRct[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();
					

					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<rct id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<rct id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<rct id= \"%d\"", id);
					else				fprintf(fp, "	<rct id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n\n", name, file);
				}
			}
		}


		iSize = (int)g_vImgBg.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgBg[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}


			if(0<count)
			{
				fprintf(fp, "	<bg>\n");

				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgBg[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();


					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "		<rec id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "		<rec id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "		<rec id= \"%d\"", id);
					else				fprintf(fp, "		<rec id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
				fprintf(fp, "	</bg>\n");
			}
		}


		iSize = (int)g_vImgTbar.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgTbar[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				fprintf(fp, "	<tbar>\n");

				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgTbar[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();


					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "		<rec id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "		<rec id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "		<rec id= \"%d\"", id);
					else				fprintf(fp, "		<rec id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
				fprintf(fp, "	</tbar>\n\n");
			}
		}


		iSize = (int)g_vImgStck.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgStck[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgStck[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();
				
					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<st id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<st id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<st id= \"%d\"", id);
					else				fprintf(fp, "	<st id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
				fprintf(fp, "\n");
			}
		}


		iSize = (int)g_vImgAnim.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgAnim[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgAnim[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();
				
					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<ani id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<ani id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<ani id= \"%d\"", id);
					else				fprintf(fp, "	<ani id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
				fprintf(fp, "\n");
			}
		}


		iSize = (int)g_vImgScl.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgScl[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgScl[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();


					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<scl id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<scl id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<scl id= \"%d\"", id);
					else				fprintf(fp, "	<scl id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
				fprintf(fp, "\n");
			}
		}


		iSize = (int)g_vImgMark.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgMark[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgMark[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();


					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<mk id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<mk id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<mk id= \"%d\"", id);
					else				fprintf(fp, "	<mk id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
				fprintf(fp, "\n");
			}
		}

		iSize = (int)g_vImgSbtn.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgSbtn[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgSbtn[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();

					
					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<sbtn id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<sbtn id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<sbtn id= \"%d\"", id);
					else				fprintf(fp, "	<sbtn id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n\n", name, file);
				}
			}
		}
		

		iSize = (int)g_vImgBtn.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgBtn[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgBtn[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();
					

					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<btn id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<btn id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<btn id= \"%d\"", id);
					else				fprintf(fp, "	<btn id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
			}
		}
		

		iSize = (int)g_vImgCbtn.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgCbtn[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgCbtn[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();
					

					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<cbtn id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<cbtn id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<cbtn id= \"%d\"", id);
					else				fprintf(fp, "	<cbtn id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
			}
		}


		iSize = (int)g_vImgVbtn.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgVbtn[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgVbtn[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();
					

					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<vbtn id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<vbtn id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<vbtn id= \"%d\"", id);
					else				fprintf(fp, "	<vbtn id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
			}
		}


		iSize = (int)g_vImgHbtn.size();
		if(iSize)
		{
			int count = 0;
			for(i=0; i<iSize; ++i)
			{
				rec = g_vImgHbtn[i];
				name= (char*)rec->sName.c_str();
				if(0 != _strnicmp(sgrp, name, len))
					continue;

				++count;
			}

			if(0<count)
			{
				for(i=0; i<iSize; ++i)
				{
					rec = g_vImgHbtn[i];
					name= (char*)rec->sName.c_str();
					file= (char*)rec->sFile.c_str();
					

					if(0 != _strnicmp(sgrp, name, len))
						continue;


					++id;

					if(10>id)			fprintf(fp, "	<hbtn id=   \"%d\"", id);
					else if(100>id)		fprintf(fp, "	<hbtn id=  \"%d\"", id);
					else if(1000>id)	fprintf(fp, "	<hbtn id= \"%d\"", id);
					else				fprintf(fp, "	<hbtn id=\"%d\"", id);

					pos_x = rec->imgX;
					pos_y = rec->imgY;
					if(grp_rct)
					{
						pos_x -= grp_rct->imgX;
						pos_y -= grp_rct->imgY;
					}

					if(10>pos_x)		fprintf(fp, "	x=   \"%d\"", pos_x);
					else if(100>pos_x)	fprintf(fp, "	x=  \"%d\"", pos_x);
					else if(1000>pos_x)	fprintf(fp, "	x= \"%d\"", pos_x);
					else				fprintf(fp, "	x=\"%d\"", pos_x);

					if(10>pos_y)		fprintf(fp, "	y=   \"%d\"", pos_y);
					else if(100>pos_y)	fprintf(fp, "	y=  \"%d\"", pos_y);
					else if(1000>pos_y)	fprintf(fp, "	y= \"%d\"", pos_y);
					else				fprintf(fp, "	y=\"%d\"", pos_y);

					if(10>rec->imgW)		fprintf(fp, "	w=   \"%d\"", rec->imgW);
					else if(100>rec->imgW)	fprintf(fp, "	w=  \"%d\"", rec->imgW);
					else if(1000>rec->imgW)	fprintf(fp, "	w= \"%d\"", rec->imgW);
					else					fprintf(fp, "	w=\"%d\"", rec->imgW);

					if(10>rec->imgH)		fprintf(fp, "	h=   \"%d\"", rec->imgH);
					else if(100>rec->imgH)	fprintf(fp, "	h=  \"%d\"", rec->imgH);
					else if(1000>rec->imgH)	fprintf(fp, "	h= \"%d\"", rec->imgH);
					else					fprintf(fp, "	h=\"%d\"", rec->imgH);

					fprintf(fp, "	n= \"%s\" file= \"%s\" />\n", name, file);
				}
			}
		}

		fprintf(fp, "</grp>\n");


		if(fp)
			fclose(fp);
	}

	return 0;
}



int WriteToHtm(const char* sDst)
{
	int iGroup = (int)g_vGroup.size();
	for(int j=0; j<iGroup; ++j)
	{
		char sgrp[256]={0};
		int  len = 0;
		
		sprintf(sgrp, "%s", (char*)g_vGroup[j].c_str());


		FILE*	fp = NULL;
		char	sFile[_MAX_PATH] = {0};
		sprintf(sFile, "%s/%s.html", sDst, sgrp);


		strcat(sgrp, "_");
		len = strlen(sgrp);


		fp = fopen(sFile, "wt");
		if(!fp)
		{
			printf("Err: dest html file for saving information of image files open failed.\n");
			continue;
		}



		fprintf(fp, "<!DOCTYPE html><html lang=\"ko\">\n");
		fprintf(fp, "<head>\n");
		fprintf(fp, "	<meta http-equiv=\"Content-Type\" content=\"text/html; charset=euc-kr\" />\n");
		fprintf(fp, "	<style>img{position: absolute;}</style>\n");
		fprintf(fp, "	<title>exporting data viewer</title>\n");
		fprintf(fp, "</head>\n");
		fprintf(fp, "\n");
		fprintf(fp, "<body style=\"margin:0; padding:0\">\n");

		int iSize = (int)g_vImgOrg.size();
		int i=0;

		for(i=0; i<iSize; ++i)
		{
			ImgRec*	rec = &g_vImgOrg[i];
			char*   name= (char*)rec->sName.c_str();
			char*	file= (char*)rec->sFile.c_str();

			if(0 != _strnicmp(sgrp, name, len))
				continue;



			if(10>i)
				fprintf(fp, "<img id=  \"%d\"", i);

			else if(100>i)
				fprintf(fp, "<img id= \"%d\"", i);

			else
				fprintf(fp, "<img id=\"%d\"", i);



		 
			if(string::npos != rec->sName.find("rc"))
			{
				fprintf(fp, "	style=\"left:%4dpx; top:%4dpx; opacity: 0.3;\" name=\"%s\" src=\"%s.png\" />\n", rec->imgX, rec->imgY, name, file);
			}
			else
				fprintf(fp, "	style=\"left:%4dpx; top:%4dpx\" name=\"%s\" src=\"%s.png\" />\n", rec->imgX, rec->imgY, name, file);
		}

		fprintf(fp, "\n");
		fprintf(fp, "</body></html>\n");


		if(fp)
			fclose(fp);
	}

	return 0;
}

int WriteToTxt(const char* sDst)
{
	FILE*	fp = NULL;
	char	sFile[_MAX_PATH] = {0};
	sprintf(sFile, "%s/%s.txt", sDst, g_file);

	fp = fopen(sFile, "wt");
	if(!fp)
	{
		printf("Err: dest txt file for saving information of image files open failed.\n");
		return 0;
	}


	//fprintf(fp, "file name list\n");
	//for(int i=0; i<(int)g_vImgName.size(); ++i)
	//{
	//	char* name = (char*)g_vImgName[i].c_str();
	//	fprintf(fp, "%d\t%s\n", i, name);
	//}
	//fprintf(fp, "\n");


	fprintf(fp, "display list\n");

	int iSize = (int)g_vImgOrg.size();
	int i=0;

	for(i=0; i<iSize; ++i)
	{
		ImgRec*	rec = &g_vImgOrg[i];
		char*	name= (char*)rec->sName.c_str();
		char*	file= (char*)rec->sFile.c_str();

		fprintf(fp, "%3d	%3d	%3d	%3d	%3d	%s %s\n"
					, i
					, rec->imgX
					, rec->imgY
					, rec->imgW
					, rec->imgH
					, name, file);
	}


	if(fp)
		fclose(fp);

	return 0;
}



int WriteToConsole()
{
	int iGroup = (int)g_vGroup.size();
	for(int j=0; j<iGroup; ++j)
	{
		char sgrp[256]={0};
		int  len = 0;
		
		sprintf(sgrp, "%s_", (char*)g_vGroup[j].c_str());
		len = strlen(sgrp);
		printf("Group: %s\n", sgrp);


		int iSize = (int)g_vImgOrg.size();
		for(int i=0; i<iSize; ++i)
		{
			ImgRec*	rec = &g_vImgOrg[i];
			char*	name= (char*)rec->sName.c_str();
			char*	file= (char*)rec->sFile.c_str();


			if(0 != _strnicmp(sgrp, name, len))
				continue;

			printf(      "	%3d	%3d	%3d	%3d	%3d	%s %s\n"
						, i
						, rec->imgX
						, rec->imgY
						, rec->imgW
						, rec->imgH
						, name, file);
		}

	}
	return 0;
}

