using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace golf_net
{
	public static class Program
	{
		public static FormAlpha		m_formAlpha = null;

		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			m_formAlpha = new FormAlpha();

			Application.Run(m_formAlpha);
		}

		public static FormAlpha  GetMainForm() { return m_formAlpha; }
		
		static public int CurrentPhage()
		{
			return m_formAlpha.m_phase;
		}
		
		static public int ChageForm(int phase)
		{
			if(0 > phase || phase >= APC.PHASE_MAX)
				return APC.EFAIL;

			if(m_formAlpha.m_phase == phase)
				return APC.OK_AL;


			if(m_formAlpha.InvokeRequired)
			{
				m_formAlpha.Invoke(new PGN.Fnc_Int(ChageForm), phase);
			}
			else
			{
				m_formAlpha.m_form[m_formAlpha.m_phase].Hide();
				m_formAlpha.m_phase = phase;
				m_formAlpha.m_form[m_formAlpha.m_phase].Show();
			}

			return APC.OK;
		}


		static public int ChangeLobbyUserList(int n)
		{
			if(m_formAlpha.InvokeRequired)
			{
				m_formAlpha.Invoke(new PGN.Fnc_Int(ChangeLobbyUserList), n);
			}
			else
			{
				m_formAlpha.formLobby.ChangeLobbyUserList(n);
			}

			return APC.OK;
		}


		static public int ChangePlayPlayerInfo(int n)
		{
			if(m_formAlpha.InvokeRequired)
			{
				m_formAlpha.Invoke(new PGN.Fnc_Int(ChangePlayPlayerInfo), n);
			}
			else
			{
				m_formAlpha.formPlay.ChangePlayPlayerInfo(n);
			}

			return APC.OK;
		}
		static public int ChangeResultPlayerInfo(int n)
		{
			if(m_formAlpha.InvokeRequired)
			{
				m_formAlpha.Invoke(new PGN.Fnc_Int(ChangeResultPlayerInfo), n);
			}
			else
			{
				m_formAlpha.formResult.ChangeResultPlayerInfo(n);
			}

			return APC.OK;
		}


		static public int ShowMsgBox(object v)
		{
			if(m_formAlpha.InvokeRequired)
			{
				m_formAlpha.Invoke(new PGN.Fnc_Obj(ShowMsgBox), v);
			}
			else
			{
				m_formAlpha.ShowMsgBox(v);
			}

			return APC.OK;
		}
	}}
