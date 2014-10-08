namespace golf_net
{
	partial class FormMsgBox
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
			this.lbl1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnYES = new System.Windows.Forms.Button();
			this.btnNO = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbl1
			// 
			this.lbl1.AutoSize = true;
			this.lbl1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.lbl1.Location = new System.Drawing.Point(23, 40);
			this.lbl1.Name = "lbl1";
			this.lbl1.Size = new System.Drawing.Size(25, 14);
			this.lbl1.TabIndex = 1;
			this.lbl1.Text = "lbl1";
			// 
			// btnOK
			// 
			this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F);
			this.btnOK.Location = new System.Drawing.Point(64, 89);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(112, 37);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnYES
			// 
			this.btnYES.Font = new System.Drawing.Font("Tahoma", 9F);
			this.btnYES.Location = new System.Drawing.Point(26, 89);
			this.btnYES.Name = "btnYES";
			this.btnYES.Size = new System.Drawing.Size(79, 37);
			this.btnYES.TabIndex = 2;
			this.btnYES.Text = "YES";
			this.btnYES.UseVisualStyleBackColor = true;
			this.btnYES.Click += new System.EventHandler(this.btnYES_Click);
			// 
			// btnNO
			// 
			this.btnNO.Font = new System.Drawing.Font("Tahoma", 9F);
			this.btnNO.Location = new System.Drawing.Point(121, 89);
			this.btnNO.Name = "btnNO";
			this.btnNO.Size = new System.Drawing.Size(79, 37);
			this.btnNO.TabIndex = 3;
			this.btnNO.Text = "NO";
			this.btnNO.UseVisualStyleBackColor = true;
			this.btnNO.Click += new System.EventHandler(this.btnNO_Click);
			// 
			// FormMsgBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(241, 140);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnNO);
			this.Controls.Add(this.btnYES);
			this.Controls.Add(this.lbl1);
			this.Font = new System.Drawing.Font("Tahoma", 9F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMsgBox";
			this.Text = "FormMsgBox";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnYES;
		private System.Windows.Forms.Button btnNO;
	}
}