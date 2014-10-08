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
	public partial class FormPlay:Form
	{
		public FormPlay()
		{
			InitializeComponent();
		}

		private void Shot_Click(object sender,EventArgs e)
		{
			TplayInfo this_p = TcpApp.UserThis().play;

			++this_p.stroke;

			this_p.x     = Convert.ToSingle(textThisPosX.Text);
			this_p.y     = Convert.ToSingle(textThisPosY.Text);
			this_p.z     = Convert.ToSingle(textThisPosZ.Text);

			this_p.d     = Convert.ToSingle(textThisDir .Text);
			this_p.c_x   = Convert.ToSingle(textThisCtX .Text);
			this_p.c_y   = Convert.ToSingle(textThisCtY .Text);
			this_p.club  = Convert.ToUInt32(textThisClub.Text);
			this_p.pow   = Convert.ToSingle(textThisPow .Text);
			this_p.best  = Convert.ToSingle(textThisBest.Text);
			textThisStroke.Text = this_p.stroke.ToString();

			TcpApp.SendGpShot(this_p);
		}

		private void Putt_Click(object sender,EventArgs e)
		{
			TplayInfo this_p = TcpApp.UserThis().play;

			++this_p.stroke;

			this_p.x    = Convert.ToSingle(textThisPosX.Text);
			this_p.y    = Convert.ToSingle(textThisPosY.Text);
			this_p.z    = Convert.ToSingle(textThisPosZ.Text);

			this_p.d    = Convert.ToSingle(textThisDir .Text);
			this_p.c_y  = Convert.ToSingle(textThisCtY .Text);
			this_p.club = Convert.ToUInt32(textThisClub.Text);
			this_p.pow  = Convert.ToSingle(textThisPow .Text);
			textThisStroke.Text = this_p.stroke.ToString();

			TcpApp.SendGpPutt(this_p);
		}

		private void Result_Click(object sender,EventArgs e)
		{
			TplayInfo this_p = TcpApp.UserThis().play;

			this_p.x     = Convert.ToSingle(textThisPosX.Text);
			this_p.y     = Convert.ToSingle(textThisPosY.Text);
			this_p.z     = Convert.ToSingle(textThisPosZ.Text);

			this_p.stroke= Convert.ToByte  (textThisStroke.Text);
			this_p.bonus = Convert.ToUInt32(textThisBonus .Text);

			TcpApp.SendGpEnd(this_p);
			Program.ChageForm(APC.PHASE_RST);
		}

		public void ChangePlayPlayerInfo(int n)
		{
			TplayInfo this_p = TcpApp.UserThis().play;
			TplayInfo oppo_p = TcpApp.UserOppo().play;

			textThisPosX.Text = this_p.x   .ToString();
			textThisPosY.Text = this_p.y   .ToString();
			textThisPosZ.Text = this_p.z   .ToString();

			textThisDir .Text = this_p.d   .ToString();
			textThisCtX .Text = this_p.c_x .ToString();
			textThisCtY .Text = this_p.c_y .ToString();
			textThisClub.Text = this_p.club.ToString();
			textThisPow .Text = this_p.pow .ToString();
			textThisBest.Text = this_p.best.ToString();

			groupThis   .Text = TcpApp.UserThis().name;
		

			textOppoPosX.Text = oppo_p.x   .ToString();
			textOppoPosY.Text = oppo_p.y   .ToString();
			textOppoPosZ.Text = oppo_p.z   .ToString();

			textOppoDir .Text = oppo_p.d   .ToString();
			textOppoCtX .Text = oppo_p.c_x .ToString();
			textOppoCtY .Text = oppo_p.c_y .ToString();
			textOppoClub.Text = oppo_p.club.ToString();
			textOppoPow .Text = oppo_p.pow .ToString();
			textOppoBest.Text = oppo_p.best.ToString();

			groupOppo   .Text = TcpApp.UserOppo().name;

			this.Refresh();
		}
	}
}
