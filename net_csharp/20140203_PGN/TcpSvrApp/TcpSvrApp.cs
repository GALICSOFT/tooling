﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;


class TcpSvrApp
{
	static void Main(string[] args)
	{
		System.Console.WriteLine("Start TCP Server ...");

		PGN.TcpSvr	pNet = new PGN.TcpSvr();
		PGN.Packet	pPck = new PGN.Packet();

		pNet.Create("192.168.0.20", 50000);


		int c = 0;

		while(true)
		{
			++c;
			Thread.Sleep(1000);

			//float x = 100;
			//float y = 200;
			//float z = 300;

			//int op = 0x12345678;


			////string str = "ABCDEF HIJK: " + c;
			//string str = "Send Mesage: Hello world: " + c;


			//pPck.Reset();
			//pPck.PacketAdd(x);
			//pPck.PacketAdd(y);
			//pPck.PacketAdd(z);
			//pPck.PacketAdd(str);
			//pPck.EnPack(op, 0xAB, c);

			//pNet.Send(pPck);


			//PGN_TcpSvr.PGN_Packet pck = new PGN_TcpSvr.PGN_Packet();
			//pck.PacketAdd(c);
			//pck.PacketAdd(str);

			//pNet.Send(pck);
			//pNet.Send();
		}
	}
}

