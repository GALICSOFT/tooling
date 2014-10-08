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
	public partial class FormLobby:Form
	{
		public FormLobby()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender,EventArgs e)
		{
			MessageBox.Show("Hello world");
			Program.GetMainForm().ChageForm(PGC.PHASE_PLAY);
		}

		private void usrLst_SelectedIndexChanged(object sender,EventArgs e)
		{

		}
	}
}
