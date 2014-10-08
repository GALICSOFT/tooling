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
	public partial class FormPlay:Form
	{
		public FormPlay()
		{
			InitializeComponent();
		}

		private void Shot_Click(object sender,EventArgs e)
		{
			Program.GetMainForm().ChageForm(PGC.PHASE_RESULT);
		}
	}
}
