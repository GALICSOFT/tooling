// DF Packet Struct
//
// type	byte
// len	2	: packet length
// opp	2	: operation protocol
//
////////////////////////////////////////////////////////////////////////////////

using System;


static class PGN
{
	// packet header
	public const int PCK_DATA				= 1360					;	// packet data size
	public const int PCK_HEAD				= 2 + 2					;	// len(2) + opp(2)
	public const int PCK_MAX				= PCK_HEAD + PCK_DATA	;	// max mtu for application

	// Network state
	public const int OK						= 0						;	// Success
	public const int EFAIL					= -1					;	// Failed
	public const int WAIT					= 1						;	// Network wait message after sending data
	public const int DEFAULT				= OK					;	// Network Default state

	// Operation Protocol
	public const int CS_REQ_LOGIN			= 103					;	// snd: character name: uchar20
	public const int SC_ANS_LOGIN			= 104					;	// rcv: UID(uint32) + character name(char20)
	public const int SC_BROADCAST_USERLIST	= 105					;	// rcv: User Number(uint8) + [UID(uint32) + charcater name(uchar20) + owner(uint8) + ready(uint8)] * N
	public const int SC_BROADCAST_LOGOUT	= 114					;	// rcv: UID

	public const int CS_REQ_READY			= 106					;	// snd:
	public const int SC_BROADCAST_READY		= 107					;	// rcv: UID, ready?(RET_READY:RET_NOTREADY)

	public const int CS_REQ_GO				= 108					;	// snd:
	public const int SC_REQ_GO				= 109					;	// rcv: uint8
	public const int SC_BROADCAST_START		= 110					;	// rcv: UID, ready?(RET_READY:RET_NOTREADY)

	public const int CS_REQ_STOP			= 111					;	// snd:
	public const int SC_BROADCAST_STOP		= 112					;	// rcv: UID
	public const int SC_BROADCAST_QUIT		= 113					;	// rcv:

	public const int CS_REQ_ECHO			= 101					;	// snd/rcv: 1:1 data max(1024byte)
	public const int CS_REQ_BROADCAST		= 102					;	// snd/rcv: 1:n data max(1024byte)

	public const int RST_OWNER_TRUE			= 1						;	// snd/rcv: is owner
	public const int RST_OWNER_FALSE		= 2						;	// snd/rcv: is not owner
	public const int RST_READY_TRUE			= 1						;	// snd/rcv: is ready
	public const int RST_READY_FALSE		= 2						;	// snd/rcv: is not ready
	public const int RST_SUCCESS			= 1						;	// snd/rcv: result success
	public const int RST_FAIL				= 2						;	// snd/rcv: result failed
}


////////////////////////////////////////////////////////////////////////////////
// Packet buffer

public class PGN_Packet
{
	private short	len = 0;
	private	byte[]	buf	= new byte[PGN.PCK_MAX];

	public	short	Len	{ get{ return len; }}
	public	byte[]	Buf { get{ return buf; }}


	public PGN_Packet()	{	this.Reset();	}
	public void Reset() {  Array.Clear(buf, 0, PGN.PCK_MAX); len = PGN.PCK_HEAD; }

	public void	PacketAdd(byte[] v, int l)
	{
		Array.Copy(v, 0, this.buf, len, l);
		this.len += (short)l;
	}

	public void	PacketAdd(float v)
	{
		byte[] b = BitConverter.GetBytes(v);
		this.PacketAdd(b, b.Length);
	}

	public void	PacketAdd(int v)
	{
		byte[] b = BitConverter.GetBytes(v);
		this.PacketAdd(b, b.Length);
	}

	public void	PacketAdd(string v)
	{
		int l = v.Length;

		for(int n=0; n< l; ++n)
			this.buf[len + n] = (byte)v[n];

		this.len += (short)l;
	}


	private	static byte[]	m_sep = new byte[2] {0xCC, 0xCC};
	private static byte[]	m_ver = new byte[2] {0x13, 0x41};

	public void EnPack(int opcode, short encryption, int sequnce)
    {
		byte[] bbEnc = BitConverter.GetBytes(encryption);

		byte[] bbLen = BitConverter.GetBytes(len);
		byte[] bbOpc = BitConverter.GetBytes(opcode);
		byte[] bbSqc = BitConverter.GetBytes(sequnce);

		// sep	2	: separater	0xcccc =110011001100100
		// ver	2	: version year, month, day/2 Exception) 0x134B =>2013/04/22
		// crp	2	: crypto method. nothing: 0, method: [1,65535]
		//
		// len	2	: packet length
		// opp	4	: operation protocol
		// sqc	4	: packet sequence(auto AddressFamily)

		Array.Copy(m_sep, 0, this.buf,  0, 2);
		Array.Copy(m_ver, 0, this.buf,  2, 2);
		Array.Copy(bbEnc, 0, this.buf, 12, 2);

		Array.Copy(bbLen, 0, this.buf,  6, 2);
		Array.Copy(bbOpc, 0, this.buf,  8, 4);
		Array.Copy(bbSqc, 0, this.buf, 16, 4);
    }


	public static int EnCode(ref byte[] dst, ref int lenD, byte[] src, int lenS)
    {
		byte[] buf = dst;

		Array.Clear(buf, 0, PGN.PCK_MAX);
		Array.Copy(src, buf, lenS);

		lenD  = lenS;
		return lenD;
    }
}


