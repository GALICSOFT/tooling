
#pragma warning(disable: 4996)

#pragma comment(lib, "libpng_.lib")


#include <vector>
#include <set>
#include <map>
#include <string>

using namespace std;

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <direct.h>
#include <io.h>
#include <errno.h>

#include "imgapp.h"


#include <Windows.h>


int main(int argc, char** argv)
{
	//for(int i=0; i<argc; ++i)
	//{
	//	MessageBox(NULL, argv[i], "World", 0);
	//}

	int		hr = 0;
	char	dir_src[280]={0};
	char	dir_dst[280]={0};


	if(3 > argc)
	{
		const char*	c_src = DirSrc();
		const char* c_dst = DirDst();
		char drive[_MAX_DRIVE];
		char dir  [_MAX_DIR];
		char fname[_MAX_FNAME];
		_splitpath( argv[0], drive, dir, fname, NULL);
		sprintf(dir_src, "%s%s%s", drive, dir, c_src);
		sprintf(dir_dst, "%s%s%s", drive, dir, c_dst);
	}
	else
	{
		strcpy(dir_src, argv[1]);
		strcpy(dir_dst, argv[2]);
	}



	// gather the group
	GetherGroup(dir_src);

	// gather image file name
	GetherImageInfo(dir_src);

	if(0 == SizeImageInfo())
	{
		printf("Err: dest file error.\n");
		return 0;
	}


	hr = _mkdir(dir_dst);
	if(-1 == hr)
	{
		if(errno != EEXIST)
		{
			printf("Err: creating the dest directory failed.\n");
			return 0;
		}
	}


	//hr = WriteToCsv(dir_dst);
	//hr = WriteToJsn(dir_dst);
	hr = WriteToXml(dir_dst);
	hr = WriteToHtm(dir_dst);
	//hr = WriteToTxt(dir_dst);
	hr = WriteToConsole();

	CopyToDst(dir_src, dir_dst);

	ReleaseImageInfo();

	return 0;
}




