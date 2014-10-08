using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace golf_net
{
	public partial class FormMsgBox:Form
	{
		protected object				m_ob;
		protected PGN.Fnc_IntObj	m_cb;

		public FormMsgBox()
		{
			InitializeComponent();
		}

		public FormMsgBox(string body, string title, int btnType, object val, PGN.Fnc_IntObj cb)
		{
			InitializeComponent();
			m_ob = val;
			m_cb = cb;

			this.Text = title;
			this.lbl1.Text = body;

			MessageBoxButtons btnR = (MessageBoxButtons)btnType;

			if( MessageBoxButtons.OK == btnR)
			{
				this.btnOK.Visible = true;
				this.btnYES.Visible = false;
				this.btnNO.Visible = false;
			}
			else if( MessageBoxButtons.YesNo == btnR)
			{
				this.btnOK.Visible = false;
				this.btnYES.Visible = true;
				this.btnNO.Visible = true;
			}
		}

		private void btnOk_Click(object sender,EventArgs e)
		{
			if(null != m_cb)
				m_cb((int)DialogResult.OK, m_ob);

			this.Close();
		}

		private void btnYES_Click(object sender,EventArgs e)
		{
			if(null != m_cb)
				m_cb((int)DialogResult.Yes, m_ob);

			this.Close();
		}

		private void btnNO_Click(object sender,EventArgs e)
		{
			if(null != m_cb)
				m_cb((int)DialogResult.No, m_ob);

			this.Close();
		}
	}
}
