// 2D Game App
//
////////////////////////////////////////////////////////////////////////////////

#pragma warning(disable:4996)

#pragma comment(lib, "winmm.lib")
#pragma comment(lib, "d3d9.lib")
#pragma comment(lib, "d3dx9.lib")


#include <windows.h>
#include <d3d9.h>
#include <d3dx9.h>

#include <io.h>

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <malloc.h>
#include <memory.h>
#include <tchar.h>
#include <time.h>
#include <Mmsystem.h>


#define SAFE_DELETE(p)       { if(p) { delete (p);     (p)=NULL; } }
#define SAFE_DELETE_ARRAY(p) { if(p) { delete[] (p);   (p)=NULL; } }
#define SAFE_RELEASE(p)      { if(p) { (p)->Release(); (p)=NULL; } }



TCHAR					m_sCls[128]	= "2D Game App";
HINSTANCE				m_hInst		= NULL;
HWND					m_hWnd		= NULL;
DWORD					m_dWinStyle	= WS_OVERLAPPED| WS_CAPTION| WS_SYSMENU| WS_VISIBLE;
DWORD					m_dScnX		= 400;
DWORD					m_dScnY		= 300;
BOOL					m_bShowCusor= TRUE;
BOOL					m_bWindow	= TRUE;			// Window mode

D3DPRESENT_PARAMETERS	m_d3dpp		;
LPDIRECT3D9				m_pD3D		= NULL;			// D3D
LPDIRECT3DDEVICE9		m_pDev		= NULL;			// Device


LRESULT WINAPI WndProc( HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam )
{
	if(WM_DESTROY == msg)
		PostQuitMessage( 0 );

	return DefWindowProc( hWnd, msg, wParam, lParam );
}


void FileConvert(char* root);
void FileConvertToTga(char* file);


//INT WINAPI WinMain( HINSTANCE hInst, HINSTANCE, LPSTR, INT )
int main(int argc, char** argv)
{
	char sDir[MAX_PATH*2] = {0};

	if(2 <= argc)
		sprintf(sDir, argv[1]);
	else
		GetCurrentDirectory(MAX_PATH, sDir);

	int len =strlen(sDir);
	if('\\' == sDir[len-1] || '/' == sDir[len-1])
		sDir[len-1] = 0;



	HINSTANCE hInst = (HINSTANCE)GetModuleHandle(NULL);
	m_hInst	= hInst;

	WNDCLASS wc =								// Register the window class
	{
		CS_CLASSDC
		, WndProc
		, 0L
		, 0L
		, m_hInst
		, NULL
		, LoadCursor(NULL,IDC_ARROW)
		, (HBRUSH)GetStockObject(WHITE_BRUSH)
		, NULL
		, m_sCls
	};

	RegisterClass( &wc );

	RECT rc;									//Create the application's window

	SetRect( &rc, 0, 0, m_dScnX, m_dScnY);
	AdjustWindowRect( &rc, m_dWinStyle, FALSE );

	int iScnSysW = ::GetSystemMetrics(SM_CXSCREEN);
	int iScnSysH = ::GetSystemMetrics(SM_CYSCREEN);

	m_hWnd = CreateWindow( m_sCls
		, m_sCls
		, m_dWinStyle
		, (iScnSysW - (rc.right-rc.left))/2
		, (iScnSysH - (rc.bottom-rc.top))/2
		, (rc.right  - rc.left)
		, (rc.bottom - rc.top)
		, NULL
		, NULL
		, m_hInst
		, NULL );


	if( NULL == ( m_pD3D = Direct3DCreate9( D3D_SDK_VERSION ) ) )
		goto END;

	D3DDISPLAYMODE d3ddm;
    if( FAILED( m_pD3D->GetAdapterDisplayMode( D3DADAPTER_DEFAULT, &d3ddm ) ) )
        goto END;


	memset(&m_d3dpp, 0, sizeof(m_d3dpp));
	m_d3dpp.Windowed				= m_bWindow;
	m_d3dpp.SwapEffect				= D3DSWAPEFFECT_DISCARD;
	m_d3dpp.BackBufferFormat		= d3ddm.Format;
	m_d3dpp.BackBufferCount			= 2;
	m_d3dpp.BackBufferWidth			= m_dScnX;
	m_d3dpp.BackBufferHeight		= m_dScnY;
	m_d3dpp.EnableAutoDepthStencil	= TRUE;
	m_d3dpp.AutoDepthStencilFormat	= D3DFMT_D16;


	// D3DADAPTER_DEFAULT: 대부분의 그래픽카드는 모노 듀얼일 경우 이부분을 수정
	// D3DDEVTYPE_HAL : 하드웨어 가속(가장 큰 속도)을 받을 것인가.. 하드웨어 지
	// 원이 없을 경우 D3D는 소프트웨어로 이를 대체 할 수 있다.

	if( FAILED( m_pD3D->CreateDevice( D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, m_hWnd,
		D3DCREATE_MIXED_VERTEXPROCESSING, &m_d3dpp, &m_pDev ) ) )
	{
		if( FAILED( m_pD3D->CreateDevice( D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, m_hWnd,
		D3DCREATE_SOFTWARE_VERTEXPROCESSING, &m_d3dpp, &m_pDev ) ) )
		{
			SAFE_RELEASE(	m_pDev	);
			SAFE_RELEASE(	m_pD3D	);
			goto END;
		}
	}

	ShowWindow( m_hWnd, SW_HIDE );
	UpdateWindow( m_hWnd );
	::ShowCursor(m_bShowCusor);


	FileConvert(sDir);

END:

	SAFE_RELEASE(	m_pDev	);
	SAFE_RELEASE(	m_pD3D	);

	DestroyWindow(m_hWnd);

	return 0;
}


void FileConvert(char* root)
{
	char			sSearchPath[MAX_PATH*2]={0};
	_finddata_t		fd;
	INT_PTR			handle;

	INT				result=1;

	char			ext[32]={0};

	sprintf(sSearchPath,"%s/*.*", root);

	handle = (INT_PTR)_findfirst(sSearchPath, &fd);
	if(-1 != handle)
	{
		while (result != -1)
		{
			// Folder
			if( (fd.attrib & 0x10) && '.' != fd.name[0])
			{
				char	tNode[MAX_PATH*2]={0};
				sprintf(tNode, "%s/%s", root, fd.name);

				printf("Sub folder: %s\n", tNode);

				FileConvert(tNode);
			}


			// File
			if( (fd.attrib & 0x20) && '.' != fd.name[0])
			{
				char	tNode[MAX_PATH*2]={0};

				_splitpath(fd.name, NULL, NULL, NULL, ext);

				if(0 == stricmp(".dds", ext))
				{
					sprintf(tNode, "%s/%s", root, fd.name);
					//printf("File: %s\n", tNode);
					FileConvertToTga(tNode);
				}
				else if(0 == stricmp(".bmp", ext))
				{
					sprintf(tNode, "%s/%s", root, fd.name);
					//printf("File: %s\n", tNode);
					FileConvertToTga(tNode);
				}
			}

			result =_findnext((long)handle, &fd);
		}

		_findclose(handle);
	}
}


void FileConvertToTga(char* file)
{
	HRESULT hr = 0;

	char					dstFile[MAX_PATH*2] ={0};
	LPDIRECT3DTEXTURE9		pTex = NULL;
	D3DXIMAGE_INFO			img_inf={0};
	D3DSURFACE_DESC			srf_dsc={(D3DFORMAT)0,};
	IDirect3DSurface9*		pSrcSurface = NULL;


	char drive[_MAX_DRIVE]={0};
	char dir  [_MAX_DIR  ]={0};
	char fname[_MAX_FNAME]={0};
	char flwrs[_MAX_FNAME]={0};

	strcpy(flwrs, file);

	char* lwr = strlwr(flwrs);
	_splitpath( lwr, drive, dir, fname, NULL );

	D3DXGetImageInfoFromFile(file, &img_inf);

	//	Load Ui texture
	hr = D3DXCreateTextureFromFileEx(
		m_pDev, file
		, D3DX_DEFAULT
		, D3DX_DEFAULT
		, D3DX_DEFAULT
		, 0
		, D3DFMT_UNKNOWN
		, D3DPOOL_MANAGED
		, 0x0000001, 0x0000001
		, 0
		, NULL	//, &img_inf
		, 0
		, &pTex);


	if( FAILED(hr))
		return;


	hr = pTex->GetLevelDesc(0, &srf_dsc);
	hr = pTex->GetSurfaceLevel(0, &pSrcSurface);
	if( FAILED(hr))
	{
		pTex->Release();
		return;
	}


	//sprintf(dstFile, "%s%s%s.png", drive, dir, fname);
	//D3DXSaveSurfaceToFile(dstFile, D3DXIFF_PNG, pSrcSurface, NULL, NULL);

	sprintf(dstFile, "%s%s%s.tga", drive, dir, fname);
	D3DXSaveSurfaceToFile(dstFile, D3DXIFF_TGA, pSrcSurface, NULL, NULL);

	pSrcSurface->Release();
	pSrcSurface = NULL;

	return;

	////////////////////////////////////////////////////////////////////////////
	//
	// need not inversion
	//
	////////////////////////////////////////////////////////////////////////////

	//	Load Ui texture
	hr = D3DXCreateTextureFromFileEx(
		m_pDev, dstFile
		, D3DX_DEFAULT
		, D3DX_DEFAULT
		, D3DX_DEFAULT
		, 0
		, D3DFMT_UNKNOWN
		, D3DPOOL_MANAGED
		, 0x0000001, 0x0000001
		, 0
		, NULL	//, &img_inf
		, 0
		, &pTex);


	if( FAILED(hr))
		return;




	hr = pTex->GetLevelDesc(0, &srf_dsc);
	hr = pTex->GetSurfaceLevel(0, &pSrcSurface);
	if( FAILED(hr))
	{
		pTex->Release();
		return;
	}


	// dds format is a zip so, when directly call this method will be error.
	// first save tga and re open, inversion and re save.
	// it'a hardly method and i do not recommend because i have no idea.

	D3DLOCKED_RECT	d3d_lock;
	INT colByte  = 0;
	INT src_w;
	INT src_h;

	pSrcSurface->LockRect(&d3d_lock, NULL, 0);

	src_w = srf_dsc.Width;
	src_h = srf_dsc.Height;

	colByte = d3d_lock.Pitch / src_w;

	if(1 == colByte)
	{
		BYTE* col = (BYTE*)d3d_lock.pBits;
		BYTE  tmp = 0;
		BYTE* s0  = NULL;
		BYTE* s1  = NULL;

		for(int y=0; y< src_h/2; ++y)
		{
			s0 = &col[ y * srf_dsc.Width];
			s1 = &col[ (src_h-1-y) * src_w];

			for(int x=0; x<src_w; ++x)
			{
				tmp    = s1[x];
				s1[x]  = s0[x];
				s0[x]  = tmp;
			}
		}
	}
	else if(2 == colByte)
	{
		WORD* col = (WORD*)d3d_lock.pBits;
		WORD  tmp = 0;
		WORD* s0  = NULL;
		WORD* s1  = NULL;

		for(int y=0; y< src_h/2; ++y)
		{
			s0 = &col[ y * src_w];
			s1 = &col[ (src_h-1-y) * src_w];

			for(int x=0; x<src_w; ++x)
			{
				tmp    = s1[x];
				s1[x]  = s0[x];
				s0[x]  = tmp;
			}
		}
	}
	else
	{
		DWORD* col = (DWORD*)d3d_lock.pBits;
		DWORD  tmp = 0;
		DWORD* s0  = NULL;
		DWORD* s1  = NULL;

		for(int y=0; y< src_h/2; ++y)
		{
			s0 = &col[ y * src_w];
			s1 = &col[ (src_h-1-y) * src_w];

			for(int x=0; x<src_w; ++x)
			{
				tmp    = s1[x];
				s1[x]  = s0[x];
				s0[x]  = tmp;
			}
		}
	}

	pSrcSurface->UnlockRect();

	//sprintf(dstFile, "%s%s%s.png", drive, dir, fname);
	//D3DXSaveSurfaceToFile(dstFile, D3DXIFF_PNG, pSrcSurface, NULL, NULL);

	sprintf(dstFile, "%s%s%s.tga", drive, dir, fname);
	D3DXSaveSurfaceToFile(dstFile, D3DXIFF_TGA, pSrcSurface, NULL, NULL);

	pSrcSurface->Release();
	pSrcSurface = NULL;	
}

