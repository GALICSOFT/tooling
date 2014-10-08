namespace golf_net
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
			this.textBonus0 = new System.Windows.Forms.TextBox();
			this.textScore0 = new System.Windows.Forms.TextBox();
			this.textId0 = new System.Windows.Forms.TextBox();
			this.textWin0 = new System.Windows.Forms.TextBox();
			this.textWin1 = new System.Windows.Forms.TextBox();
			this.textBonus1 = new System.Windows.Forms.TextBox();
			this.textScore1 = new System.Windows.Forms.TextBox();
			this.textId1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(10, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(110, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 14);
			this.label2.TabIndex = 12;
			this.label2.Text = "Score";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(216, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 14);
			this.label3.TabIndex = 13;
			this.label3.Text = "Bonus";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(319, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 14);
			this.label4.TabIndex = 14;
			this.label4.Text = "Win";
			// 
			// btnLobby
			// 
			this.btnLobby.Font = new System.Drawing.Font("Tahoma", 9F);
			this.btnLobby.Location = new System.Drawing.Point(13, 212);
			this.btnLobby.Name = "btnLobby";
			this.btnLobby.Size = new System.Drawing.Size(75, 48);
			this.btnLobby.TabIndex = 8;
			this.btnLobby.Text = "Lobby";
			this.btnLobby.UseVisualStyleBackColor = true;
			this.btnLobby.Click += new System.EventHandler(this.btnLobby_Click);
			// 
			// btnReplay
			// 
			this.btnReplay.Font = new System.Drawing.Font("Tahoma", 9F);
			this.btnReplay.Location = new System.Drawing.Point(141, 212);
			this.btnReplay.Name = "btnReplay";
			this.btnReplay.Size = new System.Drawing.Size(75, 48);
			this.btnReplay.TabIndex = 9;
			this.btnReplay.Text = "Replay";
			this.btnReplay.UseVisualStyleBackColor = true;
			this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
			// 
			// btnQuit
			// 
			this.btnQuit.Font = new System.Drawing.Font("Tahoma", 9F);
			this.btnQuit.Location = new System.Drawing.Point(276, 212);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(75, 48);
			this.btnQuit.TabIndex = 10;
			this.btnQuit.Text = "Quit";
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// textBonus0
			// 
			this.textBonus0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textBonus0.Location = new System.Drawing.Point(204, 60);
			this.textBonus0.Name = "textBonus0";
			this.textBonus0.ReadOnly = true;
			this.textBonus0.Size = new System.Drawing.Size(71, 22);
			this.textBonus0.TabIndex = 2;
			this.textBonus0.Text = "0";
			// 
			// textScore0
			// 
			this.textScore0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textScore0.Location = new System.Drawing.Point(102, 60);
			this.textScore0.Name = "textScore0";
			this.textScore0.ReadOnly = true;
			this.textScore0.Size = new System.Drawing.Size(71, 22);
			this.textScore0.TabIndex = 1;
			this.textScore0.Text = "0";
			// 
			// textId0
			// 
			this.textId0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textId0.Location = new System.Drawing.Point(13, 60);
			this.textId0.Name = "textId0";
			this.textId0.ReadOnly = true;
			this.textId0.Size = new System.Drawing.Size(83, 22);
			this.textId0.TabIndex = 0;
			this.textId0.Text = "0";
			// 
			// textWin0
			// 
			this.textWin0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textWin0.Location = new System.Drawing.Point(303, 60);
			this.textWin0.Name = "textWin0";
			this.textWin0.ReadOnly = true;
			this.textWin0.Size = new System.Drawing.Size(67, 22);
			this.textWin0.TabIndex = 3;
			this.textWin0.Text = "0";
			// 
			// textWin1
			// 
			this.textWin1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textWin1.Location = new System.Drawing.Point(303, 88);
			this.textWin1.Name = "textWin1";
			this.textWin1.ReadOnly = true;
			this.textWin1.Size = new System.Drawing.Size(67, 22);
			this.textWin1.TabIndex = 7;
			this.textWin1.Text = "0";
			// 
			// textBonus1
			// 
			this.textBonus1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textBonus1.Location = new System.Drawing.Point(204, 88);
			this.textBonus1.Name = "textBonus1";
			this.textBonus1.ReadOnly = true;
			this.textBonus1.Size = new System.Drawing.Size(71, 22);
			this.textBonus1.TabIndex = 6;
			this.textBonus1.Text = "0";
			// 
			// textScore1
			// 
			this.textScore1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textScore1.Location = new System.Drawing.Point(102, 88);
			this.textScore1.Name = "textScore1";
			this.textScore1.ReadOnly = true;
			this.textScore1.Size = new System.Drawing.Size(71, 22);
			this.textScore1.TabIndex = 5;
			this.textScore1.Text = "0";
			// 
			// textId1
			// 
			this.textId1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.textId1.Location = new System.Drawing.Point(13, 88);
			this.textId1.Name = "textId1";
			this.textId1.ReadOnly = true;
			this.textId1.Size = new System.Drawing.Size(83, 22);
			this.textId1.TabIndex = 4;
			this.textId1.Text = "0";
			// 
			// FormResult
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 300);
			this.ControlBox = false;
			this.Controls.Add(this.textWin1);
			this.Controls.Add(this.textBonus1);
			this.Controls.Add(this.textScore1);
			this.Controls.Add(this.textId1);
			this.Controls.Add(this.textWin0);
			this.Controls.Add(this.textBonus0);
			this.Controls.Add(this.textScore0);
			this.Controls.Add(this.textId0);
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


		private System.Windows.Forms.TextBox textId0;
		private System.Windows.Forms.TextBox textScore0;
		private System.Windows.Forms.TextBox textBonus0;
		private System.Windows.Forms.TextBox textWin0;

		private System.Windows.Forms.TextBox textId1;
		private System.Windows.Forms.TextBox textScore1;
		private System.Windows.Forms.TextBox textBonus1;
		private System.Windows.Forms.TextBox textWin1;

		private System.Windows.Forms.Button btnLobby;
		private System.Windows.Forms.Button btnReplay;
		private System.Windows.Forms.Button btnQuit;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}