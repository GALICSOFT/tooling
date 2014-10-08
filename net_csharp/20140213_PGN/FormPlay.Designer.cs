namespace golf_net
{
	partial class FormPlay
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnShot = new System.Windows.Forms.Button();
			this.btnPutt = new System.Windows.Forms.Button();
			this.btnResult = new System.Windows.Forms.Button();
			this.textThisPosX = new System.Windows.Forms.TextBox();
			this.textThisPosY = new System.Windows.Forms.TextBox();
			this.textThisPosZ = new System.Windows.Forms.TextBox();
			this.textThisDir = new System.Windows.Forms.TextBox();
			this.textThisCtX = new System.Windows.Forms.TextBox();
			this.textThisCtY = new System.Windows.Forms.TextBox();
			this.textThisClub = new System.Windows.Forms.TextBox();
			this.textThisPow = new System.Windows.Forms.TextBox();
			this.textThisBest = new System.Windows.Forms.TextBox();
			this.textThisStroke = new System.Windows.Forms.TextBox();
			this.textThisBonus = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.groupThis = new System.Windows.Forms.GroupBox();
			this.textOppoBonus = new System.Windows.Forms.TextBox();
			this.textOppoStroke = new System.Windows.Forms.TextBox();
			this.textOppoBest = new System.Windows.Forms.TextBox();
			this.textOppoPow = new System.Windows.Forms.TextBox();
			this.textOppoClub = new System.Windows.Forms.TextBox();
			this.textOppoCtY = new System.Windows.Forms.TextBox();
			this.textOppoCtX = new System.Windows.Forms.TextBox();
			this.textOppoDir = new System.Windows.Forms.TextBox();
			this.textOppoPosZ = new System.Windows.Forms.TextBox();
			this.textOppoPosY = new System.Windows.Forms.TextBox();
			this.textOppoPosX = new System.Windows.Forms.TextBox();
			this.groupOppo = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// btnShot
			// 
			this.btnShot.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnShot.Location = new System.Drawing.Point(29, 250);
			this.btnShot.Name = "btnShot";
			this.btnShot.Size = new System.Drawing.Size(90, 40);
			this.btnShot.TabIndex = 11;
			this.btnShot.Text = "Shot";
			this.btnShot.UseVisualStyleBackColor = true;
			this.btnShot.Click += new System.EventHandler(this.Shot_Click);
			// 
			// btnPutt
			// 
			this.btnPutt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnPutt.Location = new System.Drawing.Point(145, 250);
			this.btnPutt.Name = "btnPutt";
			this.btnPutt.Size = new System.Drawing.Size(90, 40);
			this.btnPutt.TabIndex = 12;
			this.btnPutt.Text = "Putting";
			this.btnPutt.UseVisualStyleBackColor = true;
			this.btnPutt.Click += new System.EventHandler(this.Putt_Click);
			// 
			// btnResult
			// 
			this.btnResult.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnResult.Location = new System.Drawing.Point(261, 250);
			this.btnResult.Name = "btnResult";
			this.btnResult.Size = new System.Drawing.Size(90, 40);
			this.btnResult.TabIndex = 13;
			this.btnResult.Text = "Result";
			this.btnResult.UseVisualStyleBackColor = true;
			this.btnResult.Click += new System.EventHandler(this.Result_Click);
			// 
			// textThisPosX
			// 
			this.textThisPosX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisPosX.Location = new System.Drawing.Point(69, 57);
			this.textThisPosX.Name = "textThisPosX";
			this.textThisPosX.Size = new System.Drawing.Size(50, 22);
			this.textThisPosX.TabIndex = 1;
			this.textThisPosX.Text = "0";
			// 
			// textThisPosY
			// 
			this.textThisPosY.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisPosY.Location = new System.Drawing.Point(119, 57);
			this.textThisPosY.Name = "textThisPosY";
			this.textThisPosY.Size = new System.Drawing.Size(50, 22);
			this.textThisPosY.TabIndex = 2;
			this.textThisPosY.Text = "0";
			// 
			// textThisPosZ
			// 
			this.textThisPosZ.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisPosZ.Location = new System.Drawing.Point(169, 57);
			this.textThisPosZ.Name = "textThisPosZ";
			this.textThisPosZ.Size = new System.Drawing.Size(50, 22);
			this.textThisPosZ.TabIndex = 3;
			this.textThisPosZ.Text = "0";
			// 
			// textThisDir
			// 
			this.textThisDir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisDir.Location = new System.Drawing.Point(69, 80);
			this.textThisDir.Name = "textThisDir";
			this.textThisDir.Size = new System.Drawing.Size(50, 22);
			this.textThisDir.TabIndex = 4;
			this.textThisDir.Text = "0";
			// 
			// textThisCtX
			// 
			this.textThisCtX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisCtX.Location = new System.Drawing.Point(69, 103);
			this.textThisCtX.Name = "textThisCtX";
			this.textThisCtX.Size = new System.Drawing.Size(50, 22);
			this.textThisCtX.TabIndex = 5;
			this.textThisCtX.Text = "0";
			// 
			// textThisCtY
			// 
			this.textThisCtY.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisCtY.Location = new System.Drawing.Point(119, 103);
			this.textThisCtY.Name = "textThisCtY";
			this.textThisCtY.Size = new System.Drawing.Size(50, 22);
			this.textThisCtY.TabIndex = 6;
			this.textThisCtY.Text = "0";
			// 
			// textThisClub
			// 
			this.textThisClub.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisClub.Location = new System.Drawing.Point(69, 34);
			this.textThisClub.Name = "textThisClub";
			this.textThisClub.Size = new System.Drawing.Size(50, 22);
			this.textThisClub.TabIndex = 0;
			this.textThisClub.Text = "7";
			// 
			// textThisPow
			// 
			this.textThisPow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisPow.Location = new System.Drawing.Point(69, 126);
			this.textThisPow.Name = "textThisPow";
			this.textThisPow.Size = new System.Drawing.Size(50, 22);
			this.textThisPow.TabIndex = 7;
			this.textThisPow.Text = "30";
			// 
			// textThisBest
			// 
			this.textThisBest.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisBest.Location = new System.Drawing.Point(69, 149);
			this.textThisBest.Name = "textThisBest";
			this.textThisBest.Size = new System.Drawing.Size(50, 22);
			this.textThisBest.TabIndex = 8;
			this.textThisBest.Text = "100";
			// 
			// textThisStroke
			// 
			this.textThisStroke.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisStroke.Location = new System.Drawing.Point(69, 174);
			this.textThisStroke.Name = "textThisStroke";
			this.textThisStroke.ReadOnly = true;
			this.textThisStroke.Size = new System.Drawing.Size(50, 22);
			this.textThisStroke.TabIndex = 9;
			this.textThisStroke.Text = "0";
			// 
			// textThisBonus
			// 
			this.textThisBonus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textThisBonus.Location = new System.Drawing.Point(69, 198);
			this.textThisBonus.Name = "textThisBonus";
			this.textThisBonus.Size = new System.Drawing.Size(50, 22);
			this.textThisBonus.TabIndex = 10;
			this.textThisBonus.Text = "100";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(29, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 14);
			this.label1.TabIndex = 28;
			this.label1.Text = "위치";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(29, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 14);
			this.label2.TabIndex = 29;
			this.label2.Text = "방향";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(34, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 14);
			this.label3.TabIndex = 30;
			this.label3.Text = "CtP";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(30, 38);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 14);
			this.label4.TabIndex = 27;
			this.label4.Text = "Club";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label5.Location = new System.Drawing.Point(18, 130);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(42, 14);
			this.label5.TabIndex = 31;
			this.label5.Text = "Power";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label6.Location = new System.Drawing.Point(29, 154);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(31, 14);
			this.label6.TabIndex = 32;
			this.label6.Text = "Best";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label7.Location = new System.Drawing.Point(17, 177);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(43, 14);
			this.label7.TabIndex = 33;
			this.label7.Text = "Stroke";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label8.Location = new System.Drawing.Point(20, 201);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 14);
			this.label8.TabIndex = 34;
			this.label8.Text = "Bonus";
			// 
			// groupThis
			// 
			this.groupThis.Location = new System.Drawing.Point(62, 11);
			this.groupThis.Name = "groupThis";
			this.groupThis.Size = new System.Drawing.Size(161, 214);
			this.groupThis.TabIndex = 25;
			this.groupThis.TabStop = false;
			this.groupThis.Text = "This Player";
			// 
			// textOppoBonus
			// 
			this.textOppoBonus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoBonus.Location = new System.Drawing.Point(230, 198);
			this.textOppoBonus.Name = "textOppoBonus";
			this.textOppoBonus.ReadOnly = true;
			this.textOppoBonus.Size = new System.Drawing.Size(50, 22);
			this.textOppoBonus.TabIndex = 24;
			this.textOppoBonus.Text = "100";
			// 
			// textOppoStroke
			// 
			this.textOppoStroke.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoStroke.Location = new System.Drawing.Point(230, 174);
			this.textOppoStroke.Name = "textOppoStroke";
			this.textOppoStroke.ReadOnly = true;
			this.textOppoStroke.Size = new System.Drawing.Size(50, 22);
			this.textOppoStroke.TabIndex = 23;
			this.textOppoStroke.Text = "0";
			// 
			// textOppoBest
			// 
			this.textOppoBest.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoBest.Location = new System.Drawing.Point(230, 149);
			this.textOppoBest.Name = "textOppoBest";
			this.textOppoBest.ReadOnly = true;
			this.textOppoBest.Size = new System.Drawing.Size(50, 22);
			this.textOppoBest.TabIndex = 22;
			this.textOppoBest.Text = "100";
			// 
			// textOppoPow
			// 
			this.textOppoPow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoPow.Location = new System.Drawing.Point(230, 126);
			this.textOppoPow.Name = "textOppoPow";
			this.textOppoPow.ReadOnly = true;
			this.textOppoPow.Size = new System.Drawing.Size(50, 22);
			this.textOppoPow.TabIndex = 21;
			this.textOppoPow.Text = "30";
			// 
			// textOppoClub
			// 
			this.textOppoClub.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoClub.Location = new System.Drawing.Point(230, 34);
			this.textOppoClub.Name = "textOppoClub";
			this.textOppoClub.ReadOnly = true;
			this.textOppoClub.Size = new System.Drawing.Size(50, 22);
			this.textOppoClub.TabIndex = 14;
			this.textOppoClub.Text = "6";
			// 
			// textOppoCtY
			// 
			this.textOppoCtY.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoCtY.Location = new System.Drawing.Point(280, 103);
			this.textOppoCtY.Name = "textOppoCtY";
			this.textOppoCtY.ReadOnly = true;
			this.textOppoCtY.Size = new System.Drawing.Size(50, 22);
			this.textOppoCtY.TabIndex = 20;
			this.textOppoCtY.Text = "0";
			// 
			// textOppoCtX
			// 
			this.textOppoCtX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoCtX.Location = new System.Drawing.Point(230, 103);
			this.textOppoCtX.Name = "textOppoCtX";
			this.textOppoCtX.ReadOnly = true;
			this.textOppoCtX.Size = new System.Drawing.Size(50, 22);
			this.textOppoCtX.TabIndex = 19;
			this.textOppoCtX.Text = "0";
			// 
			// textOppoDir
			// 
			this.textOppoDir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoDir.Location = new System.Drawing.Point(230, 80);
			this.textOppoDir.Name = "textOppoDir";
			this.textOppoDir.ReadOnly = true;
			this.textOppoDir.Size = new System.Drawing.Size(50, 22);
			this.textOppoDir.TabIndex = 18;
			this.textOppoDir.Text = "0";
			// 
			// textOppoPosZ
			// 
			this.textOppoPosZ.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoPosZ.Location = new System.Drawing.Point(330, 57);
			this.textOppoPosZ.Name = "textOppoPosZ";
			this.textOppoPosZ.ReadOnly = true;
			this.textOppoPosZ.Size = new System.Drawing.Size(50, 22);
			this.textOppoPosZ.TabIndex = 17;
			this.textOppoPosZ.Text = "0";
			// 
			// textOppoPosY
			// 
			this.textOppoPosY.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoPosY.Location = new System.Drawing.Point(280, 57);
			this.textOppoPosY.Name = "textOppoPosY";
			this.textOppoPosY.ReadOnly = true;
			this.textOppoPosY.Size = new System.Drawing.Size(50, 22);
			this.textOppoPosY.TabIndex = 16;
			this.textOppoPosY.Text = "0";
			// 
			// textOppoPosX
			// 
			this.textOppoPosX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textOppoPosX.Location = new System.Drawing.Point(230, 57);
			this.textOppoPosX.Name = "textOppoPosX";
			this.textOppoPosX.ReadOnly = true;
			this.textOppoPosX.Size = new System.Drawing.Size(50, 22);
			this.textOppoPosX.TabIndex = 15;
			this.textOppoPosX.Text = "0";
			// 
			// groupOppo
			// 
			this.groupOppo.Location = new System.Drawing.Point(225, 11);
			this.groupOppo.Name = "groupOppo";
			this.groupOppo.Size = new System.Drawing.Size(158, 214);
			this.groupOppo.TabIndex = 26;
			this.groupOppo.TabStop = false;
			this.groupOppo.Text = "Other Player";
			// 
			// FormPlay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 300);
			this.ControlBox = false;
			this.Controls.Add(this.textOppoBonus);
			this.Controls.Add(this.textOppoStroke);
			this.Controls.Add(this.textOppoBest);
			this.Controls.Add(this.textOppoPow);
			this.Controls.Add(this.textOppoClub);
			this.Controls.Add(this.textOppoCtY);
			this.Controls.Add(this.textOppoCtX);
			this.Controls.Add(this.textOppoDir);
			this.Controls.Add(this.textOppoPosZ);
			this.Controls.Add(this.textOppoPosY);
			this.Controls.Add(this.textOppoPosX);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textThisBonus);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textThisStroke);
			this.Controls.Add(this.btnResult);
			this.Controls.Add(this.btnPutt);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textThisBest);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textThisPow);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textThisClub);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textThisCtY);
			this.Controls.Add(this.textThisCtX);
			this.Controls.Add(this.textThisDir);
			this.Controls.Add(this.textThisPosZ);
			this.Controls.Add(this.textThisPosY);
			this.Controls.Add(this.textThisPosX);
			this.Controls.Add(this.btnShot);
			this.Controls.Add(this.groupThis);
			this.Controls.Add(this.groupOppo);
			this.Name = "FormPlay";
			this.Text = "Play";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textThisPosX;
		private System.Windows.Forms.TextBox textThisPosY;
		private System.Windows.Forms.TextBox textThisPosZ;
		private System.Windows.Forms.TextBox textThisDir;
		private System.Windows.Forms.TextBox textThisCtX;
		private System.Windows.Forms.TextBox textThisCtY;
		private System.Windows.Forms.TextBox textThisClub;
		private System.Windows.Forms.TextBox textThisPow;
		private System.Windows.Forms.TextBox textThisBest;
		private System.Windows.Forms.TextBox textThisStroke;
		private System.Windows.Forms.TextBox textThisBonus;

		private System.Windows.Forms.TextBox textOppoPosX;
		private System.Windows.Forms.TextBox textOppoPosY;
		private System.Windows.Forms.TextBox textOppoPosZ;
		private System.Windows.Forms.TextBox textOppoDir;
		private System.Windows.Forms.TextBox textOppoCtX;
		private System.Windows.Forms.TextBox textOppoCtY;
		private System.Windows.Forms.TextBox textOppoClub;
		private System.Windows.Forms.TextBox textOppoPow;
		private System.Windows.Forms.TextBox textOppoBest;
		private System.Windows.Forms.TextBox textOppoStroke;
		private System.Windows.Forms.TextBox textOppoBonus;

		private System.Windows.Forms.Button btnShot;
		private System.Windows.Forms.Button btnPutt;
		private System.Windows.Forms.Button btnResult;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;

		private System.Windows.Forms.GroupBox groupThis;
		private System.Windows.Forms.GroupBox groupOppo;
	}
}