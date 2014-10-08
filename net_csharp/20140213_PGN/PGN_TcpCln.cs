// Tcp Client
//
//+++++++1+++++++++2+++++++++3+++++++++4+++++++++5+++++++++6+++++++++7+++++++++8

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace PGN
{
	public class TcpCln : PGN.TcpBase
	{
		protected	byte[]			m_rcvS	= new byte[NTC.PCK_MAX];			// receive buffer for server
		protected	int				m_rcvN	= 0;								// current received count

		protected	List<byte[]>	m_sndB	= new List<byte[]>();				// send queue buffer
		protected	int				m_sndC	= NTC.OK;							// sending condition complete or op?
		protected	int				m_sndN	= -1;								// current send index

		protected	uint			m_nSqc	= 0;								// packet sequence
		protected	PGN.Packet		m_sPck	= new PGN.Packet();					// for string message


		public TcpCln() : this(NTC.OK){}
		public TcpCln(uint nId)
		{
			m_aId = nId;

			for(int n=0; n<NTC.PCK_LIST; ++n)
			{
				byte[]	buf = new byte[NTC.PCK_MAX];

				Array.Clear(buf, 0, buf.Length);
				m_sndB.Add(buf);
			}

			Array.Clear(m_rcvS, 0, m_rcvS.Length);
			m_sPck.Reset();
		}

		override public void Destroy()
		{
			CloseSocket();

			m_sndC	= NTC.OK;
			m_sndN	= -1;

			for(int n=0; n<m_sndB.Count; ++n)
				Array.Clear(m_sndB[n], 0, m_sndB[n].Length);

			m_sndB.Clear();
		}

		override public int Query(string msg, object v)
		{
			return NTC.EFAIL;
		}


		public int Create(TcpBase parent, string ip, int pt)
		{
			IPAddress ipAdd = null;
			
			m_pPrn	= parent;
			m_sIp	= ip;
			m_sPt	= pt;

			if(null == m_sIp)
			{
				IPAddress[]	vAdd
				= Dns.GetHostEntry(Environment.MachineName).AddressList;

				ipAdd = vAdd[vAdd.Length - 1];
			}
			else
			{
				ipAdd = IPAddress.Parse(m_sIp);
			}

			m_sdH	= new IPEndPoint(ipAdd, m_sPt);
			m_scH	= new Socket( AddressFamily.InterNetwork
								, SocketType.Stream
								, ProtocolType.Tcp);

			return NTC.OK;
		}

		public int Create(TcpBase parent, Socket scH, EndPoint sdH)
		{
			m_pPrn	= parent;
			m_scH	= scH;
			m_sdH	= sdH;

			return NTC.OK;
		}

		public void CloseSocket()
		{
			if(null == m_scH)
				return;


			lock(m_oLock)
			{
				m_scH.Shutdown(SocketShutdown.Both);
				m_scH.Close();
				m_scH = null;
				m_sdH = null;
				m_sIp = "";
				m_sPt = 0;

				m_sndC	= NTC.OK;
				m_sndN	= -1;

				for(int n=0; n<m_sndB.Count; ++n) { Array.Clear(m_sndB[n], 0, m_sndB[n].Length); }

			}

			if(null != m_pPrn)
			{
				TcpBase p = (TcpBase)m_pPrn;
				p.Query("Remove Client", this);
			}
		}

		public int Connect()
		{
			m_scH.BeginConnect(m_sdH, IoConnect, this);
			return NTC.OK;
		}


		////////////////////////////////////////////////////////////////////////
		// Inner Process...
		protected void IoConnect(IAsyncResult iar)
		{
			int		hr   = NTC.OK;
			TcpCln	pCln = (TcpCln)iar.AsyncState;

			try
			{
				pCln.m_scH.EndConnect(iar);
			}
			catch(SocketException e0)
			{
				PGLog.LOGW("IoConnect::SocketException::" + e0.ToString() );
				hr = NTC.EFAIL_SOCK;
				goto END;
			}
			catch(Exception e1)
			{
				PGLog.LOGW("IoConnect::Exception::" + e1.ToString() );
				hr = NTC.EFAIL;
				goto END;
			}

			if(pCln != this)
			{
				PGLog.LOGW("IoConnect::Different object");
				hr = NTC.EFAIL;
				goto END;
			}


			lock(m_oLock)
			{
				PGLog.LOGW("IoConnect::Success");
				IoEvnt(NTC.EV_CONNECT, NTC.OK, pCln.m_scH, 0, this);
			}

			pCln.Recv();
			return;

			END:
			lock(m_oLock)
			{
				IoEvnt(NTC.EV_CONNECT, hr, pCln.m_scH, 0, this);
			}
		}

		protected void IoSend(IAsyncResult iar)
		{
			int		hr   = NTC.OK;
			TcpCln	pCln = (TcpCln)iar.AsyncState;
			int		sent = -1;
			int		sentO= m_sndC;

			try
			{
				sent = pCln.m_scH.EndSend(iar);
			}
			catch(SocketException e0)
			{
				PGLog.LOGW("IoSend::SocketException::" + e0.ToString() );
				hr = NTC.EFAIL_SOCK;
				goto END;
			}
			catch(Exception e1)
			{
				PGLog.LOGW("IoSend::Exception::" + e1.ToString() );
				hr = NTC.EFAIL;
				goto END;
			}

			if(pCln != this)
			{
				PGLog.LOGW("IoSend::Different object");
				hr = NTC.EFAIL;
				goto END;
			}

			PGLog.LOGI("IoSend::Success::Sending byte::" + sent);


			END:
			lock(m_oLock)
			{
				m_sndC = NTC.OK;
				IoEvnt(NTC.EV_SEND, hr, pCln.m_scH, sentO, this);
			}
		}


		protected void IoRecv(IAsyncResult iar)
		{
			int		hr   = NTC.OK;
			int		io   = NTC.EV_RECV;
			TcpCln	pCln = (TcpCln)iar.AsyncState;
			int		rcn = -1;

			try
			{
				rcn = pCln.m_scH.EndReceive(iar);
			}
			catch(SocketException e0)
			{
				PGLog.LOGW("IoRecv::SocketException::" + e0.ToString() );
				hr = NTC.EFAIL_SOCK;
				goto END;
			}
			catch(Exception e1)
			{
				PGLog.LOGW("IoRecv::Exception::" + e1.ToString() );
				hr = NTC.EFAIL;
				goto END;
			}

			if(pCln != this)
			{
				PGLog.LOGW("IoRecv::Different object");
				hr = NTC.EFAIL;
				goto END;
			}

			// closed socket
			if(0 >= rcn)
			{
				PGLog.LOGW("IoRecv::force disconnected from server ----------");

				io   = NTC.EV_CLOSE;
				goto END;
			}

			PGLog.LOGW("IoRecv::Received size:" + rcn);
			lock(m_oLock) { IoEvnt(NTC.EV_RECV, NTC.OK, pCln.m_scH, rcn, m_rcvS); }

			pCln.Recv();
			return;

			END:
			lock(m_oLock) { IoEvnt(io, hr, pCln.m_scH, 0, this); }
			CloseSocket();
		}


		////////////////////////////////////////////////////////////////////////////
		// Interface ...

		public int Recv()
		{
			Array.Clear(m_rcvS, 0, m_rcvS.Length);
			m_scH.BeginReceive(m_rcvS, 0, m_rcvS.Length, SocketFlags.None, IoRecv, this);
			return NTC.OK;
		}

		public int Send(string str, ushort op)
		{
			if( 0 > this.IsConnected || NTC.OK != m_sndC)
				return NTC.EFAIL;

			lock (m_oLock)
			{
				++m_nSqc;
				m_sPck.Reset();
				m_sPck.AddData(str);
				m_sPck.EnCode(op);

				byte[]	s = m_sPck.Buf;
				int		l = m_sPck.Len + NTC.PCK_HEAD;
				int		nSnd = 0;

				++m_sndN;
				m_sndN %= m_sndB.Count;

				byte[] buf = m_sndB[m_sndN];
				nSnd = PGN.Packet.EnCrypt(ref buf, ref nSnd, s, l);

				m_sndC = op;//NTC.WAIT;

				m_scH.BeginSend(buf, 0, nSnd, SocketFlags.None, IoSend, this);
			}

			return NTC.OK;
		}


		public int Send(PGN.Packet pck)
		{
			if( 0 > this.IsConnected || NTC.OK != m_sndC)
				return NTC.EFAIL;

			lock (m_oLock)
			{
				int		snd = 0;
				byte[]	buf = null;

				++m_nSqc;					// increase packet sequence
				++m_sndN;					// increase buffer index
				m_sndN %= m_sndB.Count;

				buf = m_sndB[m_sndN];
				snd = pck.EnCrypt(ref buf);

				m_sndC = pck.Opp;//NTC.WAIT;

				m_scH.BeginSend(buf, 0, snd, SocketFlags.None, IoSend, this);
			}

			return NTC.OK;
		}

		public int	IsConnected { get { return (null==m_scH||false==m_scH.Connected)? NTC.EFAIL: NTC.OK; } }
		public uint	Sqence		{ get { return m_nSqc;  }	set { m_nSqc = value; }			   }		
	}

}// namespace PGN

