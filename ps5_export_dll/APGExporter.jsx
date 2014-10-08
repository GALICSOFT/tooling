// Written by Heesung Oh

/*
@@@BUILDINFO@@@ APGExporter.jsx 1.0.0.1
*/

/*

// BEGIN__HARVEST_EXCEPTION_ZSTRING

<javascriptresource>
<name>$$$/JavaScripts/APGExporter/Menu=APGExporter...</name>
<category>aaaaaThisPutsMeAtTheTopOfTheMenu</category>
</javascriptresource>

// END__HARVEST_EXCEPTION_ZSTRING

*/

// enable double clicking from the Macintosh Finder or the Windows Explorer
#target photoshop

$.localize = true;


//++5++++1+++++++++2+++++++++3+++++++++4+++++++++5+++++++++6+++++++++7+++++++++8
// user defined
var libGpackPs32 = "d:/gpackps32.dll";
var libGpackPs64 = "d:/gpackps64.dll";
var exec_file    = "E:/pnginf.exe";
//
//++5++++1+++++++++2+++++++++3+++++++++4+++++++++5+++++++++6+++++++++7+++++++++8



// UI strings to be localized
var strTitle                   = localize("$$$/JavaScripts/APGExporter/Title=APGExporter");
var strTitleDestination        = localize("$$$/JavaScripts/APGExporter/SelectDestination=Select Destination");
var strMessage                 = localize("$$$/JavaScripts/APGExporter/Message=APGExporter action settings");

var strButtonRun               = localize("$$$/JavaScripts/APGExporter/Run=Run");
var strButtonCancel            = localize("$$$/JavaScripts/APGExporter/Cancel=Cancel");

var strLblWorkFolder           = localize("$$$/JavaScripts/APGExporter/Destination=Work folder:");
var strLblFileName             = localize("$$$/JavaScripts/APGExporter/FileNamePrefix=File Name:");

var salrtSpecifyDestination    = localize("$$$/JavaScripts/APGExporter/SpecifyDestination=Please specify work folder.");
var salrtDestinationNotExist   = localize("$$$/JavaScripts/APGExporter/DestionationDoesNotExist=Destination does not exist.");
var salrtDocumentMustBeOpened  = localize("$$$/JavaScripts/APGExporter/OneDocument=You must have a document open to export!");
var salrtNeedMultipleLayers    = localize("$$$/JavaScripts/APGExporter/NoLayers=You need a document with multiple layers to export!");
var salrSuccess                = localize("$$$/JavaScripts/APGExporter/Success= was successful.");



// the drop down list indexes for file type
var psdIndex = 1;

// ok and cancel button
var runButtonID = 1;
var cancelButtonID = 2;

// count
var g_cnt_src = 0;
var g_File;



main();


function main()
{
	if ( app.documents.length <= 0 )
	{
		if ( DialogModes.NO != app.playbackDisplayDialogs )
		{
			alert( salrtDocumentMustBeOpened );
		}
		return 'cancel'; // quit, returning 'cancel' (dont localize) makes the actions palette not record our script
	}

	var exportInfo = new Object();

	initExportInfo(exportInfo);

 	// look for last used params via Photoshop registry, getCustomOptions will throw if none exist
	try
	{
		//var dd = objectToDescriptor(exportInfo, strMessage, preProcessExportInfo);
		//app.putCustomOptions("1f9621ad-d479-484a-ad01-60332518c0ce", dd);

		var d = app.getCustomOptions("1f9621ad-d479-484a-ad01-60332518c0ce");
		descriptorToObject(exportInfo, d, strMessage, postProcessExportInfo);
	}
	catch(e)
	{
		// it's ok if we don't have any options, continue with defaults
	}

	// see if I am getting descriptor parameters
	descriptorToObject(exportInfo, app.playbackParameters, strMessage, postProcessExportInfo);

	if ( DialogModes.ALL == app.playbackDisplayDialogs )
	{
		if (cancelButtonID == settingDialog(exportInfo))
		{
			return 'cancel';
		}
	}


	try
	{
		var docName = app.activeDocument.name;  // save the app.activeDocument name before duplicate.

		var layerCount = app.documents[docName].layers.length;
		var layerSetsCount = app.documents[docName].layerSets.length;

		if ((layerCount <= 1)&&(layerSetsCount <= 0))
		{
			if ( DialogModes.NO != app.playbackDisplayDialogs )
			{
				alert( salrtNeedMultipleLayers );
				return 'cancel'; // quit, returning 'cancel' (dont localize) makes the actions palette not record our script
			}
		}

		else
		{
			var rememberMaximize;

			app.activeDocument = app.documents[docName];
			var duppedDocument = app.activeDocument.duplicate();
			duppedDocument.activeLayer = duppedDocument.layers[duppedDocument.layers.length-1]; // for removing
			setInvisibleAllArtLayers(duppedDocument);


			
			g_File = new File(exportInfo.workSrc + "/000000_apg_grp.txt");
			g_File.open('w');

			exportChildren(duppedDocument, app.documents[docName], exportInfo, duppedDocument, exportInfo.prefix);
			g_File.close();


			duppedDocument.close( SaveOptions.DONOTSAVECHANGES );

			var d = objectToDescriptor(exportInfo, strMessage, preProcessExportInfo);
			app.putCustomOptions("1f9621ad-d479-484a-ad01-60332518c0ce", d);


			var dd = objectToDescriptor(exportInfo, strMessage, preProcessExportInfo);


			app.playbackParameters = dd;

			if ( rememberMaximize != undefined )
			{
				app.preferences.maximizeCompatibility = rememberMaximize;
			}

			if ( DialogModes.ALL == app.playbackDisplayDialogs )
			{
				//alert(strTitle + salrSuccess);

			}

			app.playbackDisplayDialogs = DialogModes.ALL;
		}

	}
	catch (e)
	{
		if ( DialogModes.NO != app.playbackDisplayDialogs )
		{
			alert(e);
		}
		return 'cancel'; // quit, returning 'cancel' (dont localize) makes the actions palette not record our script
	}


	///////////////////////////////////////////////////////////////////////////
	//

	var libFile32 = new File(libGpackPs32);
	var libFile64 = new File(libGpackPs64);
	var objGpack;

	if(!libFile32.exists) 	{	alert( libGpackPs32 + " file is not exist");	return 'cancel';	}
	if(!libFile64.exists) 	{	alert( libGpackPs64 + " file is not exist");	return 'cancel';	}

	// try 32bit library
	try
	{
		objGpack = new ExternalObject("lib:" + libGpackPs32);
	}
	catch(e)
	{
		// try 64bit library
		try 
		{
			objGpack = new ExternalObject("lib:" + libGpackPs64);
		}
		catch(e)
		{
			alert( libGpackPs64 + " i/o error");
			return 'cancel';
		}
	}


	try
	{
		objGpack.ExecAssemble(exec_file, exportInfo.workSrc, exportInfo.workDst);
	}
	catch(e)
	{
		alert( "objGpack.ExecAssemble  error");
		return 'cancel';
	}
}


///////////////////////////////////////////////////////////////////////////////
// Usage: pop the ui and get user settings
// Input: exportInfo object containing our parameters
// Return: on ok, the dialog info is set to the exportInfo object
function settingDialog(exportInfo)
{
	var dlgRsc   = "";

	dlgRsc  += "dialog"									;
	dlgRsc  += "{"										;
	dlgRsc  +=		"orientation: 'column',"			;
	dlgRsc  +=		"alignChildren:['fill', 'top'], "	;
	dlgRsc  +=		"preferredSize:[300, 250],"			;

	dlgRsc  +=		"text: '" + strTitle +"',"			;
	dlgRsc  +=		"margins:5, "						;
	dlgRsc  +=		"Gp00: Group {margins: [0, 10, 0, 10]}, ";

	dlgRsc  +=		"st: StaticText { text:'"+ strLblWorkFolder + "',size: [300,4] }, ";

	dlgRsc  +=		"GpTpa: Group {margins: [0, 0, 0, 10], ";
	dlgRsc  +=			"etWorkSrc: EditText { text:'"+ exportInfo.workSrc + "', size: [260,24], alignment:['left', 'top'] }, ";
	dlgRsc  +=			"btWorkSrc: Button { text:'work source ...', size: [120,24], alignment:['left', 'top']}";
	dlgRsc  +=		"},";

	dlgRsc  +=		"GpTpb: Group {margins: [0, 0, 0, 10], ";
	dlgRsc  +=			"etWorkDst: EditText { text:'"+ exportInfo.workDst.toString() + "', size: [260,24], alignment:['left', 'top'] }, ";
	dlgRsc  +=			"btWorkDst: Button { text:'work dest...', size: [120,24], alignment:['left', 'top']}";

	dlgRsc  +=		"},";


	dlgRsc  +=		"GpMdd: Group {margins: [0, 0, 0, 0], ";
	dlgRsc  +=			"st2: StaticText { text:'"+ strLblFileName + "', size: [90,20], alignment:['left', 'bottom'] }, ";
	dlgRsc  +=			"etPrefix: EditText { text:'"+ exportInfo.prefix.toString() + "', size: [140,24], alignment:['left', 'bottom'] }, ";
	dlgRsc  +=		"},";


	dlgRsc  +=		"GpBtt: Group {margins: [0, 20, 0, 0], ";
	dlgRsc  +=			"btRun   : Button { text:'"+ strButtonRun + "', size: [100,44], alignment:['center', 'top']}";
	dlgRsc  +=			"btCancel: Button { text:'"+ strButtonCancel + "', size: [100,44], alignment:['center', 'top']}";
	dlgRsc  +=		"},";

	dlgRsc  +=  "}"													;


	var dlgMain = new Window(dlgRsc);


	// match our dialog background color to the host application
	var brush = dlgMain.graphics.newBrush (dlgMain.graphics.BrushType.THEME_COLOR, "appDialogBackground");
	dlgMain.graphics.backgroundColor = brush;
	dlgMain.graphics.disabledBackgroundColor = dlgMain.graphics.backgroundColor;


	
	dlgMain.GpTpa.btWorkSrc.onClick = function()
	{
		var dfWorkSrc = dlgMain.GpTpa.etWorkSrc.text;
		var testFolder = new Folder(dlgMain.GpTpa.etWorkSrc.text);
		if (!testFolder.exists)
		{
			dfWorkSrc = "~";
		}
		var selFolder = Folder.selectDialog(strTitleDestination, dfWorkSrc);
		if ( selFolder != null )
		{
			var str = StrReplace(selFolder.fsName, "\\", "/");
			dlgMain.GpTpa.etWorkSrc.text  = str;
		}

		dlgMain.defaultElement.active = true;
	}


	dlgMain.GpTpb.btWorkDst.onClick = function()
	{
		var dfWorkDst = dlgMain.GpTpb.etWorkDst.text;
		var testFolder = new Folder(dlgMain.GpTpb.etWorkDst.text);
		if (!testFolder.exists)
		{
			dfWorkDst = "~";
		}
		var selFolder = Folder.selectDialog(strTitleDestination, dfWorkDst);
		if ( selFolder != null )
		{
			var str = StrReplace(selFolder.fsName, "\\", "/");
			dlgMain.GpTpb.etWorkDst.text = str;
		}

		dlgMain.defaultElement.active = true;
	}


	dlgMain.GpBtt.btRun.onClick = function()
	{
		// check if the setting is properly
		var workSrc = dlgMain.GpTpa.etWorkSrc.text;
		if (workSrc.length == 0)
		{
			alert(salrtSpecifyDestination);
			return;
		}
		var testFolder = new Folder(workSrc);
		if (!testFolder.exists)
		{
			alert(salrtDestinationNotExist);
			return;
		}


		// check if the setting is properly
		var workDst = dlgMain.GpTpb.etWorkDst.text;
		if (workDst.length == 0)
		{
			alert(salrtSpecifyDestination);
			return;
		}

		testFolder = new Folder(workDst);
		if (!testFolder.exists)
		{
			alert(salrtDestinationNotExist);
			return;
		}


		dlgMain.close(runButtonID);
	}


	dlgMain.GpBtt.btCancel.onClick = function()
	{
		dlgMain.close(cancelButtonID);
	}

	dlgMain.defaultElement = dlgMain.GpBtt.btRun;
	dlgMain.cancelElement  = dlgMain.GpBtt.btCancel;




	// give the hosting app the focus before showing the dialog
	app.bringToFront();

	dlgMain.center();

	var result = dlgMain.show();

	if (cancelButtonID == result) {
		return result;  // close to quit
	}

	// get setting from dialog
	exportInfo.workSrc = dlgMain.GpTpa.etWorkSrc.text;
	exportInfo.workDst = dlgMain.GpTpb.etWorkDst.text;
	exportInfo.prefix  = dlgMain.GpMdd.etPrefix.text;

	return result;
}



///////////////////////////////////////////////////////////////////////////////
// Usage: create our default parameters
// Input: a new Object
// Return: a new object with params set to default
function initExportInfo(exportInfo)
{
	exportInfo.workSrc = new String("");
	exportInfo.workDst = new String("");
	exportInfo.prefix  = new String("_");

	try
	{
		exportInfo.workSrc = Folder(app.activeDocument.fullName.parent).fsName; // work folder
		exportInfo.workDst = Folder(app.activeDocument.fullName.parent).fsName; // work folder

		var tmp = app.activeDocument.fullName.name;
		exportInfo.prefix = decodeURI(tmp.substring(0, tmp.indexOf("."))); // filename body part
	}

	catch(someError)
	{
		exportInfo.workSrc = new String("");
		exportInfo.workDst = new String("");
		exportInfo.prefix = app.activeDocument.name; // filename body part
	}
}


///////////////////////////////////////////////////////////////////////////////
// Usage: the worker routine, take our params and save the file accordingly
// Input: reference to the document, the name of the output file,
//		export info object containing more information
// Return: <none>, a file on disk
function SaveToPng( docRef, dstFile)
{
	var id6  = charIDToTypeID( "Expr" );
	var id7  = charIDToTypeID( "Usng" );

	var id8  = charIDToTypeID( "Op  " );
	var id9  = charIDToTypeID( "SWOp" );
	var id10 = charIDToTypeID( "OpSa" );
	var id11 = charIDToTypeID( "Fmt " );
	var id12 = charIDToTypeID( "IRFm" );
	var id13 = charIDToTypeID( "PN24" );
	var id14 = charIDToTypeID( "Intr" );
	var id15 = charIDToTypeID( "Trns" );
	var id16 = charIDToTypeID( "Mtt " );
	var id17 = charIDToTypeID( "MttR" );
	var id18 = charIDToTypeID( "MttG" );
	var id19 = charIDToTypeID( "MttB" );
	var id20 = charIDToTypeID( "SHTM" );
	var id21 = charIDToTypeID( "SImg" );
	var id22 = charIDToTypeID( "SSSO" );
	var id23 = charIDToTypeID( "SSLt" );
	var id24 = charIDToTypeID( "DIDr" );
	var id25 = charIDToTypeID( "In  " );
	var id26 = stringIDToTypeID( "SaveForWeb" );

	var list1 = new ActionList();
	var desc3 = new ActionDescriptor();
	var desc4 = new ActionDescriptor();

	desc4.putEnumerated( id8, id9, id10 );
	desc4.putEnumerated( id11, id12, id13 );
	desc4.putBoolean( id14, true );
	desc4.putBoolean( id15, true );

	desc4.putBoolean( id16, true );
	desc4.putInteger( id17, 255 );
	desc4.putInteger( id18, 255 );
	desc4.putInteger( id19, 255 );
	desc4.putBoolean( id20, false );
	desc4.putBoolean( id21, true );
	desc4.putBoolean( id22, false );
	desc4.putList   ( id23, list1 );

	desc4.putBoolean( id24, false );
	desc4.putPath   ( id25, new File(dstFile) );

	desc3.putObject ( id7, id26, desc4 );
	executeAction   ( id6, desc3, DialogModes.NO );
}

///////////////////////////////////////////////////////////////////////////////
// Usage: return a string padded to digit(s)
// Input: num to convert, digit count needed
// Return: string padded to digit length
function zeroSuppress (num, digit) {
	var tmp = num.toString();
	while (tmp.length < digit) {
		tmp = "0" + tmp;
	}
	return tmp;
}


///////////////////////////////////////////////////////////////////////////////
// Usage: unlock and make invisible all art layers, recursively
// Input: document or layerset
// Return: all art layers are unlocked and invisible
function setInvisibleAllArtLayers(obj) {
	for( var i = 0; i < obj.artLayers.length; i++) {
		obj.artLayers[i].allLocked = false;
		obj.artLayers[i].visible = false;
	}
	for( var i = 0; i < obj.layerSets.length; i++) {
		setInvisibleAllArtLayers(obj.layerSets[i]);
	}
}


///////////////////////////////////////////////////////////////////////////////
// Usage: remove all the invisible art layers, recursively
// Input: document or layer set
// Return: <none>, all layers that were invisible are now gone
function removeAllInvisibleArtLayers(obj) {
	for( var i = obj.artLayers.length-1; 0 <= i; i--) {
		try {
			if(!obj.artLayers[i].visible) {
				obj.artLayers[i].remove();
			}
		}
		catch (e) {
		}
	}
	for( var i = obj.layerSets.length-1; 0 <= i; i--) {
		removeAllInvisibleArtLayers(obj.layerSets[i]);
	}
}


///////////////////////////////////////////////////////////////////////////////
// Usage: find all empty layer sets and remove them, recursively
// Input: document or layer set
// Return: empty layer sets are now gone
function removeAllEmptyLayerSets(obj) {
	var foundEmpty = true;
	for( var i = obj.layerSets.length-1; 0 <= i; i--) {
		if( removeAllEmptyLayerSets(obj.layerSets[i])) {
			obj.layerSets[i].remove();
		} else {
			foundEmpty = false;
		}
	}
	if (obj.artLayers.length > 0) {
		foundEmpty = false;
	}
	return foundEmpty;
}


///////////////////////////////////////////////////////////////////////////////
// Usage: return a string padded to digit(s)
// Input: num to convert, digit count needed
// Return: string padded to digit length
function removeAllInvisible(docRef) {
	removeAllInvisibleArtLayers(docRef);
	removeAllEmptyLayerSets(docRef);
}


///////////////////////////////////////////////////////////////////////////////
// Usage: find all the children in this document to save
// Input: duplicate document, original document, export info,
//		reference to document, starting file name
// Return: <none>, documents are saved accordingly
function exportChildren(dupObj, orgObj, exportInfo, dupDocRef, prefix)
{
	for( var i = 0; i < dupObj.artLayers.length; i++)
	{
		if (!orgObj.artLayers[i].visible)
			continue;


		g_cnt_src++;


		var strFile   = prefix;
		var dstRect;
		var dstFile;

		var layerName = dupObj.artLayers[i].name;  // store layer name before change doc
		var duppedTmp;


		strFile  = prefix +"_"+layerName;

		strFile  = strFile.replace(/[:\/\\*\?\"\<\>\|]/g, "_");  // '/\:*?"<>|' -> '_'
		strFile  = strFile.toLowerCase();
		strFile = StrReplace(strFile, "-", "_");
		strFile = StrReplace(strFile, " ", "_");
		strFile = IntToStr(g_cnt_src) + "_" + strFile + ".png";

		dstFile = exportInfo.workSrc + "/"               + strFile;
		dstRect = exportInfo.workSrc + "/" + "zzzrf2yr_" + strFile;



		dupObj.artLayers[i].visible = true;

		duppedTmp = dupDocRef.duplicate();
		SaveToPng(duppedTmp, dstRect);


		app.activeDocument.trim(TrimType.TRANSPARENT);
		SaveToPng(duppedTmp, dstFile);

		duppedTmp.close(SaveOptions.DONOTSAVECHANGES);
		dupObj.artLayers[i].visible = false;
	}


	var sub_idx=0;
	for( var i = 0; i < dupObj.layerSets.length; i++)
	{
		// visible layer only
		if (!orgObj.layerSets[i].visible)
			continue;

		var name = orgObj.layerSets[i].name;

		//var strFile = prefix;
		//strFile += "_" + zeroSuppress(i, 4) + "s";

		var len = name.length;

		
		if(7 < len && name.substring(0, 4) == "grp ")
		{
			name = name.substring(4, len);
			//g_cnt_src =0;

			g_File.writeln(name);
		}
		else
		{
			name = prefix;
		}


		exportChildren(dupObj.layerSets[i], orgObj.layerSets[i], exportInfo, dupDocRef, name);  // recursive call
	}
}


///////////////////////////////////////////////////////////////////////////////
// Usage: create an ActionDescriptor from a JavaScript Object
// Input: JavaScript Object (o)
//		object unique string (s)
//		Pre process converter (f)
// Return: ActionDescriptor
// NOTE: Only boolean, string, number and UnitValue are supported, use a pre processor
//	   to convert (f) other types to one of these forms.
// REUSE: This routine is used in other scripts. Please update those if you
//		modify. I am not using include or eval statements as I want these
//		scripts self contained.
function objectToDescriptor (o, s, f)
{
	if (undefined != f)
	{
		o = f(o);
	}

	var d = new ActionDescriptor;
	var l = o.reflect.properties.length;
	d.putString( app.charIDToTypeID( 'Msge' ), s );

	for (var i = 0; i < l; i++ )
	{
		var k = o.reflect.properties[i].toString();
		if (k == "__proto__" || k == "__count__" || k == "__class__" || k == "reflect")
			continue;

		var v = o[ k ];
		k = app.stringIDToTypeID(k);

		switch ( typeof(v) )
		{
			case "boolean":
				d.putBoolean(k, v);
				break;
			case "string":
				d.putString(k, v);
				break;
			case "number":
				d.putDouble(k, v);
				break;

			default:
			{
				if ( v instanceof UnitValue )
				{
					var uc = new Object;
					uc["px"] = charIDToTypeID("#Rlt"); // unitDistance
					uc["%"] = charIDToTypeID("#Prc"); // unitPercent
					d.putUnitDouble(k, uc[v.type], v.value);
				}
				else
				{
					throw( new Error("Unsupported type in objectToDescriptor " + typeof(v) ) );
				}
			}
		}
	}
	return d;
}


///////////////////////////////////////////////////////////////////////////////
// Usage: update a JavaScript Object from an ActionDescriptor
// Input: JavaScript Object (o), current object to update (output)
//		Photoshop ActionDescriptor (d), descriptor to pull new params for object from
//		object unique string (s)
//		JavaScript Function (f), post process converter utility to convert
// Return: Nothing, update is applied to passed in JavaScript Object (o)
// NOTE: Only boolean, string, number and UnitValue are supported, use a post processor
//	   to convert (f) other types to one of these forms.
// REUSE: This routine is used in other scripts. Please update those if you
//		modify. I am not using include or eval statements as I want these
//		scripts self contained.
function descriptorToObject (o, d, s, f)
{
	var l = d.count;
	if (l)
	{
		var keyMessage = app.charIDToTypeID( 'Msge' );
		if ( d.hasKey(keyMessage) && ( s != d.getString(keyMessage) ))
			return;
	}

	for (var i = 0; i < l; i++ )
	{
		var k = d.getKey(i); // i + 1 ?
		var t = d.getType(k);
		strk = app.typeIDToStringID(k);

		switch (t)
		{
			case DescValueType.BOOLEANTYPE:
				o[strk] = d.getBoolean(k);
				break;
			case DescValueType.STRINGTYPE:
				o[strk] = d.getString(k);
				break;
			case DescValueType.DOUBLETYPE:
				o[strk] = d.getDouble(k);
				break;
			case DescValueType.UNITDOUBLE:
				{
				var uc = new Object;
				uc[charIDToTypeID("#Rlt")] = "px"; // unitDistance
				uc[charIDToTypeID("#Prc")] = "%"; // unitPercent
				uc[charIDToTypeID("#Pxl")] = "px"; // unitPixels
				var ut = d.getUnitDoubleType(k);
				var uv = d.getUnitDoubleValue(k);
				o[strk] = new UnitValue( uv, uc[ut] );
				}
				break;
			case DescValueType.INTEGERTYPE:
			case DescValueType.ALIASTYPE:
			case DescValueType.CLASSTYPE:
			case DescValueType.ENUMERATEDTYPE:
			case DescValueType.LISTTYPE:
			case DescValueType.OBJECTTYPE:
			case DescValueType.RAWTYPE:
			case DescValueType.REFERENCETYPE:
			default:
				throw( new Error("Unsupported type in descriptorToObject " + t ) );
		}
	}

	if (undefined != f)
	{
		o = f(o);
	}
}


///////////////////////////////////////////////////////////////////////////////
// Usage: convert Photoshop enums to strings for storage
// Input: JavaScript Object of my params for this script
// Return: JavaScript Object with objects converted for storage
function preProcessExportInfo(o) {

	return o;
}

///////////////////////////////////////////////////////////////////////////////
// Usage: convert strings from storage to Photoshop enums
// Input: JavaScript Object of my params in string form
// Return: JavaScript Object with objects in enum form
function postProcessExportInfo(o) {

	return o;
}

///////////////////////////////////////////////////////////////////////////
// Usage: convert a string to a number, first stripping all characters
// Input: string and a default number
// Return: a number
function StrToIntWithDefault( s, n ) {
	var onlyNumbers = /[^0-9]/g;
	var t = s.replace( onlyNumbers, "" );
	t = parseInt( t );
	if ( ! isNaN( t ) ) {
		n = t;
	}
	return n;
}
// End APGExporter.jsx



function IntToStr(n) {
	var str ="";

	if     (10 >n)    str ="0000" + n;
	else if(100>n)    str ="000" + n;
	else if(1000>n)   str ="00" + n;
	else if(10000>n)  str ="0" + n;
	else if(100000>n) str ="" + n;

	return str;
}

function StrReplace(src, srcchr, dstchr)
{
	var dst = "";
	for(i=0; i < src.length; ++i)
	{
		if(src.charAt(i) == srcchr)
		{
			dst += dstchr;
		}
		else
		{
			dst += src.charAt(i);
		}
	}
	return dst;
}



