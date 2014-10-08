namespace ydnet
{
	partial class FormResult
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnLobby = new System.Windows.Forms.Button();
			this.btnReplay = new System.Windows.Forms.Button();
			this.btnQuit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label1.Location = new System.Drawing.Point(65,76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(22,17);
			this.label1.TabIndex = 0;
			this.label1.Text = "ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label2.Location = new System.Drawing.Point(163,76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43,17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Score";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label3.Location = new System.Drawing.Point(268,76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46,17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Bonus";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label4.Location = new System.Drawing.Point(387,76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32,17);
			this.label4.TabIndex = 3;
			this.label4.Text = "Win";
			// 
			// btnLobby
			// 
			this.btnLobby.Font = new System.Drawing.Font("Tahoma",10F);
			this.btnLobby.Location = new System.Drawing.Point(34,281);
			this.btnLobby.Name = "btnLobby";
			this.btnLobby.Size = new System.Drawing.Size(133,48);
			this.btnLobby.TabIndex = 4;
			this.btnLobby.Text = "Lobby";
			this.btnLobby.UseVisualStyleBackColor = true;
			this.btnLobby.Click += new System.EventHandler(this.btnLobby_Click);
			// 
			// btnReplay
			// 
			this.btnReplay.Font = new System.Drawing.Font("Tahoma",10F);
			this.btnReplay.Location = new System.Drawing.Point(203,281);
			this.btnReplay.Name = "btnReplay";
			this.btnReplay.Size = new System.Drawing.Size(133,48);
			this.btnReplay.TabIndex = 5;
			this.btnReplay.Text = "Replay";
			this.btnReplay.UseVisualStyleBackColor = true;
			this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
			// 
			// btnQuit
			// 
			this.btnQuit.Font = new System.Drawing.Font("Tahoma",10F);
			this.btnQuit.Location = new System.Drawing.Point(372,281);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(133,48);
			this.btnQuit.TabIndex = 6;
			this.btnQuit.Text = "Quit";
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// FormResult
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F,12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(615,514);
			this.ControlBox = false;
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.btnReplay);
			this.Controls.Add(this.btnLobby);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FormResult";
			this.Text = "Result";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnLobby;
		private System.Windows.Forms.Button btnReplay;
		private System.Windows.Forms.Button btnQuit;
	}
}