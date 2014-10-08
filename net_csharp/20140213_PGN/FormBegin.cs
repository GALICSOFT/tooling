using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PGN;

namespace golf_net
{
	public partial class FormBegin:Form
	{
		public FormBegin()
		{
			InitializeComponent();
			txtIp.Text = TcpApp.net_ip;
			txtPt.Text = TcpApp.net_pt;

			txtUID.Text = TcpApp.net_uid;
			txtPWD.Text = TcpApp.net_pwd;
			txtUID.Focus();
		}
		
		protected override void OnShown(EventArgs e)
		{
			txtUID.Focus();
			base.OnShown(e);
		}

		private void btnConnect_Click(object sender,EventArgs e)
		{
			TcpApp.SendConnect(this.txtIp.Text, this.txtPt.Text);
		}

		private void btnDiscon_Click(object sender,EventArgs e)
		{
			TcpApp.SendDisConnect();
		}

		private void btnLogin_Click(object sender,EventArgs e)
		{
			TcpApp.SendLogin(this.txtUID.Text, this.txtPWD.Text );
		}

		private void btnLogout_Click(object sender,EventArgs e)
		{
			TcpApp.SendLogout();
		}
	}

}
