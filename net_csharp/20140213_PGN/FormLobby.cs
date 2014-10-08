using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using golf_net;
using PGN;

namespace golf_net
{
	public partial class FormLobby:Form
	{
		public FormLobby()
		{
			InitializeComponent();
		}

		private void btnStart_Click(object sender,EventArgs e)
		{
			int idx_usr = this.listUsr.SelectedIndex;
			int idx_map = this.listMap.SelectedIndex;

			TuserInfo usrInfo = TcpApp.app_user_lst[idx_usr];

			if(usrInfo.id != TcpApp.app_user_id)
				TcpApp.SendRqInvite(usrInfo.id, (uint)idx_map);
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			ChangeLobbyUserList(0);

			base.OnVisibleChanged(e);
		}

		public void ChangeLobbyUserList(int n)
		{
			int i=0;
			List<TuserInfo> usr_lst = TcpApp.app_user_lst;
			List<TplayMap> map_lst = TcpApp.app_pmap_lst;

			this.listUsr.ClearSelected();
			this.listUsr.Items.Clear();

			this.listMap.ClearSelected();
			this.listMap.Items.Clear();

			for(i=0; i<usr_lst.Count; ++i)
			{
				string	name = usr_lst[i].name;
				this.listUsr.Items.Add(name);
			}

			for(i=0; i<map_lst.Count;++i)
			{
				string	name = map_lst[i].name;
				this.listMap.Items.Add(name);
			}


			if(0< this.listUsr.Items.Count) this.listUsr.SelectedIndex = 0;
			if(0< this.listMap.Items.Count) this.listMap.SelectedIndex = 0;
		}

		private void btnDiscon_Click(object sender,EventArgs e)
		{
			TcpApp.SendDisConnect();
			Program.ChageForm(APC.PHASE_BEGIN);				
		}
	}
}
