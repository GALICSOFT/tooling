namespace ydnet
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
			this.Shot = new System.Windows.Forms.Button();
			this.textPosX = new System.Windows.Forms.TextBox();
			this.textPosY = new System.Windows.Forms.TextBox();
			this.textPosZ = new System.Windows.Forms.TextBox();
			this.textDir = new System.Windows.Forms.TextBox();
			this.textCtX = new System.Windows.Forms.TextBox();
			this.textCtY = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textClub = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textPow = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBest = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Shot
			// 
			this.Shot.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.Shot.Location = new System.Drawing.Point(152,349);
			this.Shot.Name = "Shot";
			this.Shot.Size = new System.Drawing.Size(390,54);
			this.Shot.TabIndex = 9;
			this.Shot.Text = "Shot";
			this.Shot.UseVisualStyleBackColor = true;
			this.Shot.Click += new System.EventHandler(this.Shot_Click);
			// 
			// textPosX
			// 
			this.textPosX.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textPosX.Location = new System.Drawing.Point(152,95);
			this.textPosX.Name = "textPosX";
			this.textPosX.Size = new System.Drawing.Size(127,24);
			this.textPosX.TabIndex = 1;
			// 
			// textPosY
			// 
			this.textPosY.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textPosY.Location = new System.Drawing.Point(284,95);
			this.textPosY.Name = "textPosY";
			this.textPosY.Size = new System.Drawing.Size(127,24);
			this.textPosY.TabIndex = 2;
			// 
			// textPosZ
			// 
			this.textPosZ.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textPosZ.Location = new System.Drawing.Point(415,95);
			this.textPosZ.Name = "textPosZ";
			this.textPosZ.Size = new System.Drawing.Size(127,24);
			this.textPosZ.TabIndex = 3;
			// 
			// textDir
			// 
			this.textDir.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textDir.Location = new System.Drawing.Point(152,141);
			this.textDir.Name = "textDir";
			this.textDir.Size = new System.Drawing.Size(127,24);
			this.textDir.TabIndex = 4;
			// 
			// textCtX
			// 
			this.textCtX.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textCtX.Location = new System.Drawing.Point(152,192);
			this.textCtX.Name = "textCtX";
			this.textCtX.Size = new System.Drawing.Size(127,24);
			this.textCtX.TabIndex = 5;
			// 
			// textCtY
			// 
			this.textCtY.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textCtY.Location = new System.Drawing.Point(284,192);
			this.textCtY.Name = "textCtY";
			this.textCtY.Size = new System.Drawing.Size(127,24);
			this.textCtY.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label1.Location = new System.Drawing.Point(106,98);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36,17);
			this.label1.TabIndex = 11;
			this.label1.Text = "위치";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label2.Location = new System.Drawing.Point(106,144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36,17);
			this.label2.TabIndex = 12;
			this.label2.Text = "방향";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label3.Location = new System.Drawing.Point(39,198);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92,17);
			this.label3.TabIndex = 13;
			this.label3.Text = "Contact point";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label4.Location = new System.Drawing.Point(106,16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35,17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Club";
			// 
			// textClub
			// 
			this.textClub.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textClub.Location = new System.Drawing.Point(152,12);
			this.textClub.Name = "textClub";
			this.textClub.Size = new System.Drawing.Size(127,24);
			this.textClub.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label5.Location = new System.Drawing.Point(94,256);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46,17);
			this.label5.TabIndex = 14;
			this.label5.Text = "Power";
			// 
			// textPow
			// 
			this.textPow.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textPow.Location = new System.Drawing.Point(152,251);
			this.textPow.Name = "textPow";
			this.textPow.Size = new System.Drawing.Size(127,24);
			this.textPow.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label6.Location = new System.Drawing.Point(94,304);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(34,17);
			this.label6.TabIndex = 15;
			this.label6.Text = "Best";
			// 
			// textBest
			// 
			this.textBest.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textBest.Location = new System.Drawing.Point(152,299);
			this.textBest.Name = "textBest";
			this.textBest.Size = new System.Drawing.Size(127,24);
			this.textBest.TabIndex = 8;
			// 
			// FormPlay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F,12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634,512);
			this.ControlBox = false;
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBest);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textPow);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textClub);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textCtY);
			this.Controls.Add(this.textCtX);
			this.Controls.Add(this.textDir);
			this.Controls.Add(this.textPosZ);
			this.Controls.Add(this.textPosY);
			this.Controls.Add(this.textPosX);
			this.Controls.Add(this.Shot);
			this.Name = "FormPlay";
			this.Text = "Play";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Shot;
		private System.Windows.Forms.TextBox textPosX;
		private System.Windows.Forms.TextBox textPosY;
		private System.Windows.Forms.TextBox textPosZ;
		private System.Windows.Forms.TextBox textDir;
		private System.Windows.Forms.TextBox textCtX;
		private System.Windows.Forms.TextBox textCtY;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textClub;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textPow;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBest;
	}
}