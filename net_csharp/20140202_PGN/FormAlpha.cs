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

namespace ydnet
{
	public partial class FormAlpha:Form
	{
		protected int			m_phase   = PGC.PHASE_BEGIN;
		protected List<Form>	m_form    = new List<Form>();
		FormBegin				formBegin = new FormBegin();
		FormLobby				formLobby = new FormLobby();
		FormPlay				formPlay  = new FormPlay();
		FormResult				formResult= new FormResult();

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

		public int ChageForm(int phase)
		{
			if(0 > phase || phase >= PGC.PHASE_MAX)
				return PGC.EFAIL;

			if(m_phase == phase)
				return PGC.OK_AL;


			m_form[m_phase].Hide();
			m_phase = phase;
			m_form[m_phase].Show();

			return PGC.OK;
		}

		private void FormAlpha_Load(object sender,EventArgs e)
		{
			this.Location = new System.Drawing.Point(10, 10);
		}
	}
}
