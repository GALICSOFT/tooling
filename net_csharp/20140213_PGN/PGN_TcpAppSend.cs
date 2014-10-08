// Tcp Application:: sending process
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


public partial class TcpApp
{
	public static int SendConnect(string _ip, string _pt)						// Connection
	{
		TcpApp.Clear();

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;
		int     pt = Convert.ToInt32(_pt);
	
		pNet.SetIoEvent(OnIoEvent);

		pNet.CloseSocket();
		hr = pNet.Create(null, _ip, pt);
		hr = pNet.Connect();
		return hr;
	}


	public static int SendDisConnect()											// Connection
	{
		TcpCln	pNet = TcpApp.GetMainNet();
	
		pNet.SetIoEvent(OnIoEvent);
		pNet.CloseSocket();
		return NTC.OK;
	}


	public static int SendLogin(string _id, string _pwd)						// Login
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_LOGIN;
		byte[] src = Encoding.Unicode.GetBytes(_id);							// string -> byte, string str = Encoding.Default.GetString(src);	// byte -> string

		Array.Clear(m_wrkBuf, 0, m_wrkBuf.Length);								// clear the work buffer
		Array.Copy (src, m_wrkBuf, src.Length);									// copy the source data to work buffer

		m_wrkPck.Reset();														// work packet reset
		m_wrkPck.AddData(m_wrkBuf, 40);											// add the buffer
		m_wrkPck.EnCode(opp);													// finally, makeup the op code and complete the packet

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendLogout()
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		return NTC.OK;
	}

	//
	// GAME PLAY PACKET: gpp + send id + {dest id} + data
	//

	public static int SendRqInvite(uint destId, uint idx_map)
	{
		PGLog.LOGI("TcpApp::SendRqInvite");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_RQ_INVITE;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(destId);
		m_wrkPck.AddData(idx_map);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendRsInvite(uint destId, byte rq)
	{
		PGLog.LOGI("TcpApp::SendRsInvite");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_RS_INVITE;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(destId);
		m_wrkPck.AddData(rq);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGpShot(TplayInfo shot)
	{
		PGLog.LOGI("TcpApp::SendGpShot");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_PLAY_SHOT;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(TcpApp.app_oppo_id);
		m_wrkPck.AddData(shot.x);
		m_wrkPck.AddData(shot.y);
		m_wrkPck.AddData(shot.z);
		m_wrkPck.AddData(shot.d);
		m_wrkPck.AddData(shot.c_x);
		m_wrkPck.AddData(shot.c_y);
		m_wrkPck.AddData(shot.club);
		m_wrkPck.AddData(shot.pow);
		m_wrkPck.AddData(shot.best);
		m_wrkPck.AddData(shot.stroke);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGpPutt(TplayInfo putt)
	{
		PGLog.LOGI("TcpApp::SendGpPutt");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_PLAY_PUTT;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(TcpApp.app_oppo_id);
		m_wrkPck.AddData(putt.x);
		m_wrkPck.AddData(putt.y);
		m_wrkPck.AddData(putt.z);
		m_wrkPck.AddData(putt.z);
		m_wrkPck.AddData(putt.c_y);
		m_wrkPck.AddData(putt.club);
		m_wrkPck.AddData(putt.pow);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGpBallPos(float x, float y, float z)
	{
		PGLog.LOGI("TcpApp::SendGpBallPos");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_PLAY_BPOS;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(TcpApp.app_oppo_id);
		m_wrkPck.AddData(x);
		m_wrkPck.AddData(y);
		m_wrkPck.AddData(z);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}


	public static int SendGpEnd(TplayInfo end)
	{
		PGLog.LOGI("TcpApp::SendGpEnd");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_PLAY_END;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(TcpApp.app_oppo_id);
		m_wrkPck.AddData(end.x);
		m_wrkPck.AddData(end.y);
		m_wrkPck.AddData(end.z);
		m_wrkPck.AddData(end.stroke);
		m_wrkPck.AddData(end.bonus);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}


	public static int SendReady(byte bReady)
	{
		PGLog.LOGI("TcpApp::SendReady");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_READY;

		m_wrkPck.Reset();
		m_wrkPck.AddData(bReady);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGo()
	{
		int		hr = 0;

		PGLog.LOGI("TcpApp::SendGo");

		//TcpCln	pNet = TcpApp.GetMainNet();
		//ushort opp = NTC.CS_REQ_GO;

		//m_wrkPck.Reset();
		//m_wrkPck.EnCode(opp);

		//hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendStop()
	{
		PGLog.LOGI("TcpApp::SendStop");

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_STOP;

		m_wrkPck.Reset();
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}
}

