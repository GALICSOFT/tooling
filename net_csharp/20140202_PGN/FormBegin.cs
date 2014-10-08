using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ydnet
{
	public partial class FormBegin:Form
	{
		public FormBegin()
		{
			InitializeComponent();
			this.txtUID.Text = "User Id";
			this.txtPWD.Text = "User Password";

			this.textIP.Text = "127.0.0.1";
			this.textPt.Text = "50000";
		}

		private void btnLogin_Click(object sender,EventArgs e)
		{
			FormAlpha	formAlpha = Program.GetMainForm();

			formAlpha.ChageForm(PGC.PHASE_LOBBY);
		}

		private void FormBegin_Load(object sender,EventArgs e)
		{
			this.txtUID.Select();
		}
	}

}
