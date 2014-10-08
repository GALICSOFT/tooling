//
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;


// tcp client
class PGN_Tcln
{
	// for network socket
	protected	Socket					m_scH   = null;
	protected	SocketAsyncEventArgs	m_arCon = null;
	protected	SocketAsyncEventArgs	m_arRcv = null;
	protected	SocketAsyncEventArgs	m_arSnd = null;

	// for connection
	protected	string					m_sIp	= "";							// default ip
	protected	int						m_sPt	= 0;							// default port

	// for controll
	protected	List<byte[]>			m_buf	= new List<byte[]>();			// queue for send
	protected	int						m_stV	= 0;
	protected	int						m_bSnd	= 0;
	protected	int						m_nSnd	= -1;							// current send index

	protected	object					m_lock	= new object();

	protected	PGN_Packet				m_strPck= new PGN_Packet();				// for string message

	protected	byte[]					m_autKey= null;
	protected	int						m_autId	= -1;

	

	public PGN_Tcln()
	{
		for(int n=0; n<10; ++n)
		{
			byte[]	buf = new byte[PGN.PCK_MAX];
			m_buf.Add(buf);
		}
	}


	public int Connect(string ip, int pt)
	{
		m_sIp = ip;
		m_sPt = pt;

		//IPHostEntry	host	= Dns.GetHostEntry(m_sIp);
		//IPAddress[]	vAddr	= host.AddressList;
		//IPEndPoint	sdH		= new IPEndPoint(vAddr[vAddr.Length - 1], m_sPt);

		IPAddress	vAddr		= IPAddress.Parse(m_sIp);
		IPEndPoint	sdH			= new IPEndPoint(vAddr, m_sPt);

		m_scH					= new Socket(AddressFamily.InterNetwork
											,SocketType.Stream,ProtocolType.Tcp);

		m_arCon					= new SocketAsyncEventArgs();
		m_arRcv					= new SocketAsyncEventArgs();
		m_arSnd					= new SocketAsyncEventArgs();

		m_arCon.UserToken		= m_scH;
		m_arCon.RemoteEndPoint	= sdH;
		m_arCon.Completed		+= new EventHandler<SocketAsyncEventArgs>(IoComplete);

		m_arRcv.UserToken		= m_scH;
		m_arRcv.RemoteEndPoint	= sdH;
		m_arRcv.Completed		+= new EventHandler<SocketAsyncEventArgs>(IoComplete);
		m_arRcv.SetBuffer(new byte[PGN.PCK_MAX], 0, PGN.PCK_MAX);

		m_arSnd.UserToken		= m_scH;
		m_arSnd.RemoteEndPoint	= sdH;
		m_arSnd.Completed		+= new EventHandler<SocketAsyncEventArgs>(IoComplete);


		m_scH.ConnectAsync(m_arCon);		// try to asynchronous connect server

		return 0;
	}


	public void CloseSocket()
    {
        if(null == m_scH)
			return;


		lock(m_lock)
		{
			m_arCon	= null;
			m_arRcv	= null;
			m_arSnd	= null;

			m_scH.Shutdown(SocketShutdown.Both);
			m_scH.Close();
			m_scH = null;

			m_sIp = "";
			m_sPt = 0;
		}
    }

	////////////////////////////////////////////////////////////////////////////
	// Inner Process...

	protected void IoComplete(object sender, SocketAsyncEventArgs args)
	{
		try
		{
			SocketError				err = args.SocketError;
			int						rcn = args.BytesTransferred;
			byte[]					rcb = args.Buffer;
			SocketAsyncOperation	opc = args.LastOperation;

			if(SocketAsyncOperation.Receive == opc)
			{
				// close message from server
				if(err != SocketError.Success || 0 == rcn)
				{
					Console.WriteLine("IoComplete::Disconnect by server");
					CloseSocket();
					return;
				}

				lock(m_lock)
				{
					// read the key and id from string
					if(-1 == m_autId)
					{
						int n = 0;
						string rcv_s = Encoding.Default.GetString(rcb).Trim();

						m_autKey	= new byte[PGN.PCK_KEY+4];
						Array.Clear(m_autKey, 0, PGN.PCK_KEY+4);
						m_autId	= 0;

						for(n=0; n<PGN.PCK_KEY; ++n)
							m_autKey[n] = (byte)rcv_s[n];

						string skey = Encoding.Default.GetString(m_autKey).Trim();
						string sid  = rcv_s.Substring(32).Trim();
						m_autId = Convert.ToInt32(sid);

						string ss = Encoding.Default.GetString(m_autKey).Trim();
						Console.WriteLine("Auth Key::" + skey + ", Id:" + m_autId);
					}

					// the transformed stream exists
					else
						ReadBuf(args.Buffer, rcn);
				}

				m_scH.ReceiveAsync(m_arRcv);

				return;
			}

			else if(SocketAsyncOperation.Connect == opc)
			{
				if(err != SocketError.Success)
				{
					Console.WriteLine("IoComplete::Cann't Connect to Server");
					CloseSocket();
				}
				else
				{
					Console.WriteLine("IoComplete::Connection Success");

					m_scH.ReceiveAsync(m_arRcv);	// enable the receive event
				}

				m_arCon = null;						// release the connection event instance
				return;
			}

			else if(SocketAsyncOperation.Send == opc)
			{
				if(err != SocketError.Success)
				{
					Console.WriteLine("IoComplete::Cann't Send to Server");
					CloseSocket();
					return; 
				}

				lock (m_lock)
				{
					m_bSnd = 0;
				}

				Console.WriteLine("IoComplete::Send compelte");
				return;
			}
		}
		catch(SocketException)
		{
		}
		catch (Exception)
		{
		}
	}


	protected void ReadBuf(byte[] buf, int len)
    {
		int i =0;
		byte[] buff = new byte[len+2];

        for(i = 0; i < len; i++)
        {
			if('\n' == buf[i])
				break;

			buff[i] = buf[i];
        }

		len = i+1;
		buff[len] = 0;

		string stttr = Encoding.Default.GetString(buff,0, len);
		Console.WriteLine(stttr);
    }


	protected void DeCode(ref byte[] buf, int len)
    {
    }


	protected void EnCryption(ref byte[] buf, int len)
    {
    }

	protected void DeCryption(ref byte[] buf,int len)
    {
    }



	////////////////////////////////////////////////////////////////////////////
	// Interface ...

	public int Send(string str)
	{
		if( 0 > IsConnected() || 0 < m_bSnd)
			return -1;

		lock (m_lock)
		{
			m_strPck.Reset();
			m_strPck.PacketAdd(str);


			byte[]	src = m_strPck.Buf;
			int		len = m_strPck.Len;
			int		nSnd = 0;


			++m_nSnd;
			m_nSnd %= m_buf.Count;


			byte[] buf = m_buf[m_nSnd];
			nSnd = PGN_Packet.EnCode(ref buf, ref nSnd, src, len);


			m_arSnd.SetBuffer(m_buf[m_nSnd], 0, nSnd);
			m_bSnd = 1;

			m_scH.SendAsync(m_arSnd);
		}

		return 0;
	}


	public int Send(PGN_Packet pck)
	{
		if( 0 > IsConnected() || 0 < m_bSnd)
			return -1;

		lock (m_lock)
		{
			byte[]	src = pck.Buf;
			int		len = pck.Len;
			int		nSnd = 0;


			++m_nSnd;
			m_nSnd %= m_buf.Count;


			byte[] buf = m_buf[m_nSnd];
			nSnd = PGN_Packet.EnCode(ref buf, ref nSnd, src, len);


			m_arSnd.SetBuffer(m_buf[m_nSnd], 0, nSnd);
			m_bSnd = 1;

			m_scH.SendAsync(m_arSnd);
		}

		return 0;
	}

	public int IsConnected() { return (null==m_scH||false==m_scH.Connected)? -1: 0;	}

}

