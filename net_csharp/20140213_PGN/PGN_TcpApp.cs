// Tcp Application
//
//+++++++1+++++++++2+++++++++3+++++++++4+++++++++5+++++++++6+++++++++7+++++++++8

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using PGN;


public class TplayInfo
{
	public float x, y, z, d;	// position(x,y,z), direction
	public float c_x, c_y;		// contact point
	public uint  club;			// club index
	public float pow, best;		// power, best(%)
	public byte  stroke;		// stroke counting
	public uint  bonus;			// bonus
	public byte  iswin;			// is win?

	public TplayInfo()
	{
		x   = 0.0F; y   = 0.0F; z    = 0.0F; d   = 0.0F;
		c_x = 0.0F; c_y = 0.0F; club = 0   ;
		pow = 0.0F; best= 0.0F;
		stroke =0 ; bonus =0  ; iswin = NTC.RST_NO;
	}
}

public class TuserInfo
{
	public uint      id     = 0;
	public string    name   = null;
	public byte      owner  = 0;
	public byte      ready  = 0;
	public TplayInfo play   = new TplayInfo();

	public TuserInfo() : this(0, null, 0, 0){}
	public TuserInfo(uint _id, string _name, byte _owner, byte _ready)
	{
		id = _id; name = _name; owner= _owner; ready= _ready;
	}
}

public class TplayMap
{
	public uint   id  = 0;
	public string name= null;

	public TplayMap() : this(0, null){}
	public TplayMap(uint _id, string _name){ id = _id; name = _name; }
}


// Initial network data
public partial class TcpApp
{
	//public static string	net_ip		= "192.168.0.7";
	//public static string	net_ip		= "192.168.0.74";
	public static string	net_ip		= "192.168.0.20";
	public static string	net_pt		= "20001";
	public static string	net_uid		= "BBBBBBB";
	public static string	net_pwd		= "BBBBBB";
}


public partial class TcpApp
{
	// IO
	public static TcpCln			m_tcpCln    = new TcpCln();
	public static Packet			m_wrkPck	= new Packet();					// work packet
	public static byte[]			m_wrkBuf	= new byte[NTC.PCK_MAX];		// work buffer = Header + Payload
	public static TcpCln			GetMainNet () { return m_tcpCln;    }		// tcp client application

	// Game Application
	public static string			app_char_name = null;						// character name
	public static uint				app_user_id   = 0;							// user id
	public static uint				app_oppo_id   = 0;							// opponent id

	public static List<TuserInfo>	app_user_lst  = new List<TuserInfo>();		// user list
	public static List<TplayMap>	app_pmap_lst  = new List<TplayMap>();		// Play map list


	public static void Clear()
	{
		app_char_name = null;
		app_user_id   = 0;
		app_oppo_id   = 0;
		app_user_lst.Clear();
	}

	public static TuserInfo FindUser(uint id)
	{
		for(int i=0; i<app_user_lst.Count; ++i)
		{
			if(id == app_user_lst[i].id)
				return app_user_lst[i];
		}

		return null;
	}

	public static TuserInfo UserThis() { return FindUser(app_user_id); }
	public static TuserInfo UserOppo() { return FindUser(app_oppo_id); }
}


