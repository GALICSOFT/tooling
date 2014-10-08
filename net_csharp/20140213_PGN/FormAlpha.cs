using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
//using System.Collections.Generic;


using PGN;

namespace golf_net
{
	public partial class FormAlpha:Form
	{
		public int			m_phase   = APC.PHASE_BEGIN;
		public List<Form>	m_form    = new List<Form>();
		public FormBegin	formBegin = new FormBegin();
		public FormLobby	formLobby = new FormLobby();
		public FormPlay		formPlay  = new FormPlay();
		public FormResult	formResult= new FormResult();

		public FormAlpha()
		{
			InitializeComponent();

			m_form.Add(formBegin );
			m_form.Add(formLobby );
			m_form.Add(formPlay  );
			m_form.Add(formResult);

			for(int n=0; n<m_form.Count(); ++n)
			{
				Form form = m_form[n];

				form.ControlBox = false;
				form.TopLevel   = false;
				form.Parent     = this;
				form.Text       = "";
				form.FormBorderStyle	= FormBorderStyle.None;

				this.Controls.Add(form);
			}

			m_form[m_phase].Show();
		}

		private void FormAlpha_Load(object sender,EventArgs e)
		{
			this.Location = new System.Drawing.Point(100, 100);
		}


		public int ShowMsgBox(object v)
		{
			((FormMsgBox)(v)).Show();
			return APC.OK;
		}
	}
}
