namespace golf_net
{
	partial class FormLobby
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
			this.btnStart = new System.Windows.Forms.Button();
			this.listUsr = new System.Windows.Forms.ListBox();
			this.listMap = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblPlayer = new System.Windows.Forms.Label();
			this.lblMap = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnDiscon = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnStart
			// 
			this.btnStart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnStart.Location = new System.Drawing.Point(212, 249);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(148, 39);
			this.btnStart.TabIndex = 3;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// listUsr
			// 
			this.listUsr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.listUsr.FormattingEnabled = true;
			this.listUsr.ItemHeight = 14;
			this.listUsr.Location = new System.Drawing.Point(21, 6);
			this.listUsr.Name = "listUsr";
			this.listUsr.Size = new System.Drawing.Size(169, 228);
			this.listUsr.TabIndex = 0;
			// 
			// listMap
			// 
			this.listMap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.listMap.FormattingEnabled = true;
			this.listMap.ItemHeight = 14;
			this.listMap.Location = new System.Drawing.Point(209, 6);
			this.listMap.Name = "listMap";
			this.listMap.Size = new System.Drawing.Size(148, 144);
			this.listMap.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(209, 196);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 14);
			this.label1.TabIndex = 4;
			this.label1.Text = "Player:";
			// 
			// lblPlayer
			// 
			this.lblPlayer.AutoSize = true;
			this.lblPlayer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblPlayer.Location = new System.Drawing.Point(255, 198);
			this.lblPlayer.Name = "lblPlayer";
			this.lblPlayer.Size = new System.Drawing.Size(28, 14);
			this.lblPlayer.TabIndex = 5;
			this.lblPlayer.Text = "***";
			// 
			// lblMap
			// 
			this.lblMap.AutoSize = true;
			this.lblMap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblMap.Location = new System.Drawing.Point(255, 219);
			this.lblMap.Name = "lblMap";
			this.lblMap.Size = new System.Drawing.Size(28, 14);
			this.lblMap.TabIndex = 7;
			this.lblMap.Text = "***";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(209, 214);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(33, 14);
			this.label3.TabIndex = 6;
			this.label3.Text = "Map:";
			// 
			// btnDiscon
			// 
			this.btnDiscon.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.btnDiscon.Location = new System.Drawing.Point(21, 249);
			this.btnDiscon.Name = "btnDiscon";
			this.btnDiscon.Size = new System.Drawing.Size(148, 39);
			this.btnDiscon.TabIndex = 2;
			this.btnDiscon.Text = "Disconnect";
			this.btnDiscon.UseVisualStyleBackColor = true;
			this.btnDiscon.Click += new System.EventHandler(this.btnDiscon_Click);
			// 
			// FormLobby
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 300);
			this.ControlBox = false;
			this.Controls.Add(this.btnDiscon);
			this.Controls.Add(this.lblMap);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblPlayer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listMap);
			this.Controls.Add(this.listUsr);
			this.Controls.Add(this.btnStart);
			this.Name = "FormLobby";
			this.Text = "Lobby";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ListBox listUsr;
		private System.Windows.Forms.ListBox listMap;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblPlayer;
		private System.Windows.Forms.Label lblMap;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnDiscon;
	}
}