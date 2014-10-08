// Tcp Application:: receiving process
//
//+++++++1+++++++++2+++++++++3+++++++++4+++++++++5+++++++++6+++++++++7+++++++++8

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

using PGN;

public partial class TcpApp
{
	public static int invited_rep(int btnRet, object v)
	{
		DialogResult ret= (DialogResult)btnRet;
		uint[]	arr     = (uint[])v;
		uint	sendId	= arr[0];
		uint	destId	= arr[1];
		uint	idx_map = arr[2];

		int inviteRps = NTC.RST_OK;


		if(DialogResult.No == ret)
			inviteRps = NTC.RST_NO;


		TcpApp.SendRsInvite(sendId, (byte)inviteRps);


		if(NTC.RST_OK == inviteRps)
		{
			TcpApp.app_oppo_id = sendId;

			TcpApp.FindUser(app_user_id).ready = NTC.RST_READY_TRUE;
			TcpApp.SendReady(NTC.RST_READY_TRUE);
		}

		return 0;
	}

	public static int invited_rqs(int btnRet, object v)
	{
		TcpApp.SendReady(NTC.RST_READY_TRUE);
		return 0;
	}


	public static int OnIoEvent(int ev, int hr, object socket, int rcn, object data)
	{
		if     (NTC.EV_CLOSE == ev)
		{
			PGLog.LOGI("OnIoEvent: gracefully closed");
			golf_net.Program.ChageForm(golf_net.APC.PHASE_BEGIN);
		}

		else if(NTC.EV_ACCEPT == ev)
		{
			PGLog.LOGI("OnIoEvent: Accept");
		}

		else if(NTC.EV_CONNECT == ev)
		{
			PGLog.LOGI("OnIoEvent: Connect");

			if(NTC.OK != hr)
			{
				PGLog.LOGW("OnIoEvent: Connection Failed");
			}
		}

		else if(NTC.EV_SEND == ev)
		{
			PGLog.LOGI("OnIoEvent: Send::type::" + rcn);
		}

		else if(NTC.EV_RECV == ev)
		{
			PGLog.LOGI("OnIoEvent: Receive");

			byte[] rcv = null;
			ushort len = 0;
			ushort opp = 0;
			int    idx_bgn= NTC.PCK_HEAD;
			int    i   = 0;

			if(0>hr)
			{
			    PGLog.LOGI("OnIoEvent: disconnected");
			    TcpCln	pCln = (TcpCln)data;
			    golf_net.Program.ChageForm(golf_net.APC.PHASE_BEGIN);

			    return NTC.OK;
			}
			
			rcv = (byte[])data;
			len = Packet.LenFromBuf(rcv);
			opp = Packet.OppFromBuf(rcv);

			if(NTC.SC_ANS_LOGIN == opp)
			{
				byte  user_count = 0;

				app_user_lst.Clear();
				app_pmap_lst.Clear();

				// setup the user id, and character name
				Packet.ValFromBuf(ref app_user_id  , rcv, idx_bgn + 0);
				Packet.ValFromBuf(ref app_char_name, rcv, idx_bgn + 4, 40);


				PGLog.LOGI("OnIoEvent:user id/name::" + app_user_id + ", " + app_char_name);


				Packet.ValFromBuf(ref user_count   , rcv, idx_bgn + 48);


				idx_bgn = NTC.PCK_HEAD + 49;
				for(i=0; i<user_count; ++i)
				{
					uint	id   = 0;
					string	name = null;
					byte	owner= 0;
					byte	ready= 0;

					Packet.ValFromBuf(ref id   , rcv, idx_bgn + 46 *i +  0    );
					Packet.ValFromBuf(ref name , rcv, idx_bgn + 46 *i +  4, 40);
					Packet.ValFromBuf(ref owner, rcv, idx_bgn + 46 *i + 44    );
					Packet.ValFromBuf(ref ready, rcv, idx_bgn + 46 *i + 45    );

					TuserInfo usr = new TuserInfo(id, name, owner, ready);
					app_user_lst.Add(usr);
					PGLog.LOGI("OnIoEvent:NTC.SC_ANS_LOGIN:: id/name::" + id + ", " + name);
				}


				for(i=0; i<3; ++i)
				{
					uint	id   = (uint)i;
					string	name = "map " + i;

					TplayMap map = new TplayMap(id, name);
					app_pmap_lst.Add(map);
				}


				// change the phase of the application
				golf_net.Program.ChageForm(golf_net.APC.PHASE_LOBBY);
			}

			else if(NTC.SC_BROADCAST_USERLIST == opp)
			{
				byte  user_count = 0;
				idx_bgn = NTC.PCK_HEAD;

				app_user_lst.Clear();

				Packet.ValFromBuf(ref user_count   , rcv, idx_bgn + 0);


				idx_bgn = NTC.PCK_HEAD + 1;
				for(i=0; i<user_count; ++i)
				{
					uint	id   = 0;
					string	name = null;
					byte	owner= 0;
					byte	ready= 0;

					Packet.ValFromBuf(ref id   , rcv, idx_bgn + 46 *i +  0    );
					Packet.ValFromBuf(ref name , rcv, idx_bgn + 46 *i +  4, 40);
					Packet.ValFromBuf(ref owner, rcv, idx_bgn + 46 *i + 44    );
					Packet.ValFromBuf(ref ready, rcv, idx_bgn + 46 *i + 45    );

					TuserInfo usr = new TuserInfo(id, name, owner, ready);
					app_user_lst.Add(usr);
					PGLog.LOGI("OnIoEvent:NTC.SC_ANS_LOGIN:: id/name::" + id + ", " + name);
				}


				golf_net.Program.ChangeLobbyUserList(0);
			}

			else if(NTC.SC_BROADCAST_LOGOUT == opp)
			{
				uint	id   = 0;
				string	name = null;

				idx_bgn = NTC.PCK_HEAD;

				Packet.ValFromBuf(ref id, rcv, idx_bgn + 0);
				name = TcpApp.FindUser(id).name;

				for(i=0; i<app_user_lst.Count; ++i)
				{
					if(app_user_lst[i].id == id)
					{
						app_user_lst.RemoveAt(i);
						break;
					}
				}

				golf_net.Program.ChangeLobbyUserList(0);
				PGLog.LOGI("OnIoEvent:NTC.SC_BROADCAST_LOGOUT::" + name);
			}
			
			else if(NTC.CS_REQ_BROADCAST == opp)
			{
				//
				// GAME PLAY PACKET: gpp + send id + {dest id} + data
				//


				idx_bgn= NTC.PCK_HEAD;
				ushort	gpp     = 0;
				uint	sendId	= 0;
				uint	destId	= 0;

				Packet.ValFromBuf(ref gpp, rcv, idx_bgn + 0);


				// 초대 받음
				if     (NTC.GP_RQ_INVITE   == gpp)
				{
					uint idx_map = 0;

					Packet.ValFromBuf(ref sendId, rcv, idx_bgn + 2);
					Packet.ValFromBuf(ref destId, rcv, idx_bgn + 2 + 4);
					Packet.ValFromBuf(ref idx_map, rcv, idx_bgn + 2 + 4 + 4);

					if(TcpApp.app_user_id == destId)
					{
						uint[]	arr = new uint[4];
						arr[0] = sendId	;
						arr[1] = destId	;
						arr[2] = idx_map ;

						//MessageBox.Show("From " + name + " 한판 뜰래? ", "Inviting", MessageBoxButtons.YesNo);
						string name = TcpApp.FindUser(sendId).name;
						string log  = "OnIoEvent:NTC.GP_INVITE:: sender:" + name + ", dest id::" + destId + ", map id::" + idx_map ;
						PGLog.LOGI(log);

						golf_net.Program.ShowMsgBox(new golf_net.FormMsgBox("From " + name + " 한판 뜰래? ", "Inviting", (int)MessageBoxButtons.YesNo, arr, invited_rep));
					}
				}

				// 초대 응답
				else if(NTC.GP_RS_INVITE   == gpp)
				{
					byte rq = 0;
					Packet.ValFromBuf(ref sendId, rcv, idx_bgn + 2);
					Packet.ValFromBuf(ref destId, rcv, idx_bgn + 2 + 4);
					Packet.ValFromBuf(ref     rq, rcv, idx_bgn + 2 + 4 + 4);

					if(TcpApp.app_user_id == destId)
					{
						string name = TcpApp.FindUser(sendId).name;
						string log = "OnIoEvent:NTC.GP_RS_INVITE::" + name + "::"+rq;
						PGLog.LOGI(log);

						// Send Ready
						if(rq == NTC.RST_OK)
						{
							app_oppo_id = sendId;
							TcpApp.FindUser(app_oppo_id).ready = NTC.RST_READY_TRUE;

							golf_net.Program.ShowMsgBox(new golf_net.FormMsgBox("Inviting success","Msg", (int)MessageBoxButtons.OK, null, invited_rqs));

							return NTC.OK;
						}

						golf_net.Program.ShowMsgBox(new golf_net.FormMsgBox("Inviting failed","Msg", (int)MessageBoxButtons.OK, null, null));
					}
				}

				else if(NTC.GP_PLAY_SHOT == gpp)
				{
					idx_bgn= NTC.PCK_HEAD + 2;
					Packet.ValFromBuf(ref sendId, rcv, idx_bgn + 0 * 4);
					Packet.ValFromBuf(ref destId, rcv, idx_bgn + 1 * 4);

					// dest id is me...
					if( TcpApp.app_oppo_id == sendId &&
						TcpApp.app_user_id == destId)
					{
						TplayInfo oppo_p = TcpApp.UserOppo().play;
						string    name   = TcpApp.FindUser(sendId).name;

						float shot_x=0.0F, shot_y=0.0F, shot_z=0.0F;
						float shot_d=0.0F;
						float shot_c_x=0.0F, shot_c_y=0.0F;
						uint  shot_club=0;
						float shot_pow=0.0F, shot_best=0.0F;
						byte  shot_stroke = 0;

						Packet.ValFromBuf(ref shot_x     , rcv, idx_bgn + 2 * 4);
						Packet.ValFromBuf(ref shot_y     , rcv, idx_bgn + 3 * 4);
						Packet.ValFromBuf(ref shot_z     , rcv, idx_bgn + 4 * 4);
						Packet.ValFromBuf(ref shot_d     , rcv, idx_bgn + 5 * 4);
						Packet.ValFromBuf(ref shot_c_x   , rcv, idx_bgn + 6 * 4);
						Packet.ValFromBuf(ref shot_c_y   , rcv, idx_bgn + 7 * 4);
						Packet.ValFromBuf(ref shot_club  , rcv, idx_bgn + 8 * 4);
						Packet.ValFromBuf(ref shot_pow   , rcv, idx_bgn + 9 * 4);
						Packet.ValFromBuf(ref shot_best  , rcv, idx_bgn +10 * 4);
						Packet.ValFromBuf(ref shot_stroke, rcv, idx_bgn +11 * 4);

						oppo_p.x     = shot_x   ;
						oppo_p.y     = shot_y   ;
						oppo_p.z     = shot_z   ;
						oppo_p.d     = shot_d   ;
						oppo_p.c_x   = shot_c_x;
						oppo_p.c_y   = shot_c_y;
						oppo_p.club  = shot_club;
						oppo_p.pow   = shot_pow ;
						oppo_p.best  = shot_best;
						oppo_p.best  = shot_stroke;

						golf_net.Program.ChangePlayPlayerInfo(0);
						PGLog.LOGI("OnIoEvent:NTC.NTC.GP_PLAY_SHOT::" + name);
					}
				}
				else if(NTC.GP_PLAY_PUTT == gpp)
				{
					idx_bgn= NTC.PCK_HEAD + 2;
					Packet.ValFromBuf(ref sendId, rcv, idx_bgn + 0 * 4);
					Packet.ValFromBuf(ref destId, rcv, idx_bgn + 1 * 4);

					// dest id is me...
					if( TcpApp.app_oppo_id == sendId &&
						TcpApp.app_user_id == destId)
					{
						TplayInfo oppo_p = TcpApp.UserOppo().play;
						string    name   = TcpApp.FindUser(sendId).name;

						float putt_x=0.0F, putt_y=0.0F, putt_z=0.0F;
						float putt_d=0.0F;
						float putt_c_y=0.0F;
						uint  putt_club=0;
						float putt_pow=0.0F;

						Packet.ValFromBuf(ref putt_x   , rcv, idx_bgn + 2 * 4);
						Packet.ValFromBuf(ref putt_y   , rcv, idx_bgn + 3 * 4);
						Packet.ValFromBuf(ref putt_z   , rcv, idx_bgn + 4 * 4);
						Packet.ValFromBuf(ref putt_d   , rcv, idx_bgn + 5 * 4);
						Packet.ValFromBuf(ref putt_c_y , rcv, idx_bgn + 6 * 4);
						Packet.ValFromBuf(ref putt_club, rcv, idx_bgn + 7 * 4);
						Packet.ValFromBuf(ref putt_pow , rcv, idx_bgn + 8 * 4);

						oppo_p.x     = putt_x   ;
						oppo_p.y     = putt_y   ;
						oppo_p.z     = putt_z   ;
						oppo_p.d     = putt_d   ;
						oppo_p.c_y  = putt_c_y;
						oppo_p.club  = putt_club;
						oppo_p.pow   = putt_pow ;

						golf_net.Program.ChangePlayPlayerInfo(0);
						PGLog.LOGI("OnIoEvent:NTC.NTC.GP_PLAY_PUTT::" + name);
					}
				}
				else if(NTC.GP_PLAY_BPOS == gpp)
				{
					idx_bgn= NTC.PCK_HEAD + 2;
					Packet.ValFromBuf(ref sendId, rcv, idx_bgn + 0 * 4);
					Packet.ValFromBuf(ref destId, rcv, idx_bgn + 1 * 4);

					// dest id is me...
					if( TcpApp.app_oppo_id == sendId &&
						TcpApp.app_user_id == destId)
					{
						TplayInfo oppo_p = TcpApp.UserOppo().play;
						string    name   = TcpApp.FindUser(sendId).name;

						float bpos_x=0.0F, bpos_y=0.0F, bpos_z=0.0F;

						Packet.ValFromBuf(ref bpos_x   , rcv, idx_bgn + 2 * 4);
						Packet.ValFromBuf(ref bpos_y   , rcv, idx_bgn + 3 * 4);
						Packet.ValFromBuf(ref bpos_z   , rcv, idx_bgn + 4 * 4);

						oppo_p.x     = bpos_x   ;
						oppo_p.y     = bpos_y   ;
						oppo_p.z     = bpos_z   ;

						golf_net.Program.ChangePlayPlayerInfo(0);
						PGLog.LOGI("OnIoEvent:NTC.NTC.GP_PLAY_BPOS::" + name);
					}
				}
				else if(NTC.GP_PLAY_END  == gpp)
				{
					idx_bgn= NTC.PCK_HEAD + 2;
					Packet.ValFromBuf(ref sendId, rcv, idx_bgn + 0 * 4);
					Packet.ValFromBuf(ref destId, rcv, idx_bgn + 1 * 4);

					// dest id is me...
					if( TcpApp.app_oppo_id == sendId &&
						TcpApp.app_user_id == destId)
					{
						TplayInfo oppo_p = TcpApp.UserOppo().play;
						string    name   = TcpApp.FindUser(sendId).name;

						float end_x=0.0F, end_y=0.0F, end_z=0.0F;
						byte  end_stroke=0;
						uint  end_bonus =0;

						Packet.ValFromBuf(ref end_x     , rcv, idx_bgn + 2 * 4);
						Packet.ValFromBuf(ref end_y     , rcv, idx_bgn + 3 * 4);
						Packet.ValFromBuf(ref end_z     , rcv, idx_bgn + 4 * 4);
						Packet.ValFromBuf(ref end_stroke, rcv, idx_bgn + 5 * 4);
						Packet.ValFromBuf(ref end_bonus , rcv, idx_bgn + 6 * 4);

						oppo_p.x     = end_x   ;
						oppo_p.y     = end_y   ;
						oppo_p.z     = end_z   ;
						oppo_p.stroke= end_stroke;
						oppo_p.bonus = end_bonus;

						PGLog.LOGI("OnIoEvent:NTC.NTC.GP_PLAY_END::" + name);						
					}

					golf_net.Program.ChangePlayPlayerInfo(0);
					golf_net.Program.ChangeResultPlayerInfo(0);
				}
			}

			else if(NTC.SC_BROADCAST_READY == opp)
			{
				TuserInfo usr = null;
				uint	id    = 0;
				byte	ready = 0;
	
				idx_bgn= NTC.PCK_HEAD;

				Packet.ValFromBuf(ref id  , rcv, idx_bgn + 0);
				Packet.ValFromBuf(ref ready, rcv, idx_bgn + 4);


				usr = TcpApp.FindUser(id);
				usr.ready = ready;

				PGLog.LOGI("OnIoEvent:NTC.SC_BROADCAST_READY::" + usr.name + ", " + ready);

				TuserInfo usrThis  = TcpApp.FindUser(app_user_id);
				TuserInfo usrOther = TcpApp.FindUser(app_oppo_id);

				if( NTC.RST_READY_TRUE == usrThis .ready &&
					NTC.RST_READY_TRUE == usrOther.ready)
				{
					TcpApp.SendGo();
					golf_net.Program.ChageForm(golf_net.APC.PHASE_PLAY);
					golf_net.Program.ChangePlayPlayerInfo(0);
				}
			}

			else if(NTC.SC_BROADCAST_START == opp)
			{
				golf_net.Program.ChageForm(golf_net.APC.PHASE_PLAY);
			}
		}
		else
		{
			PGLog.LOGI("OnIoEvent: Not defined");
		}


		return NTC.OK;
	}
}

