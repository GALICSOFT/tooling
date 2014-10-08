//
//
////////////////////////////////////////////////////////////////////////////////

#ifndef _IMGAPP_H_
#define _IMGAPP_H_

#pragma warning(disable: 4996)

#include <algorithm>
#include <vector>
#include <set>
#include <map>
#include <string>
using namespace std;

struct ImgRec
{
	int		idx;
	string	sName;
	string	sFile;

	int		imgX, imgY, imgW, imgH;
	int		orgW, orgH, orgD;
	unsigned char* pxl;

	string	sOrg;
	string	sRct;

	ImgRec()
	{
		idx = -1; sName=""; sFile=""; sOrg=""; sRct=""; imgX = imgY = imgW = imgH =0;
		orgW = orgH = orgD = 0; pxl = NULL;
	}

	ImgRec(const string _sOrg)
	{
		idx = -1; sName=""; sFile=""; sOrg=_sOrg; sRct=""; imgX = imgY = imgW = imgH =0;
		orgW = orgH = orgD = 0; pxl = NULL;
	
	}
};


const char* DirSrc();
const char* DirDst();

void    GetherImageInfo(const char* sSrc);
void    GetherGroup(const char* folder);
void    ReleaseImageInfo();
void    CopyToDst(const char* sSrc, const char* sDst);
int		SizeImageInfo();

int     WriteToCsv(const char* sDst);
int     WriteToJsn(const char* sDst);
int     WriteToXml(const char* sDst);
int     WriteToHtm(const char* sDst);
int     WriteToTxt(const char* sDst);
int     WriteToConsole();


#endif

