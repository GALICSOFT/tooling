namespace ydnet
{
	partial class FormBegin
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtUID = new System.Windows.Forms.TextBox();
			this.txtPWD = new System.Windows.Forms.TextBox();
			this.Login = new System.Windows.Forms.Button();
			this.UID = new System.Windows.Forms.Label();
			this.PWD = new System.Windows.Forms.Label();
			this.textIP = new System.Windows.Forms.TextBox();
			this.textPt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtUID
			// 
			this.txtUID.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.txtUID.Location = new System.Drawing.Point(191,202);
			this.txtUID.Margin = new System.Windows.Forms.Padding(4);
			this.txtUID.Name = "txtUID";
			this.txtUID.Size = new System.Drawing.Size(228,24);
			this.txtUID.TabIndex = 2;
			// 
			// txtPWD
			// 
			this.txtPWD.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.txtPWD.Location = new System.Drawing.Point(191,257);
			this.txtPWD.Margin = new System.Windows.Forms.Padding(4);
			this.txtPWD.Name = "txtPWD";
			this.txtPWD.Size = new System.Drawing.Size(228,24);
			this.txtPWD.TabIndex = 3;
			// 
			// Login
			// 
			this.Login.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.Login.Location = new System.Drawing.Point(191,330);
			this.Login.Margin = new System.Windows.Forms.Padding(4);
			this.Login.Name = "Login";
			this.Login.Size = new System.Drawing.Size(229,64);
			this.Login.TabIndex = 4;
			this.Login.Text = "Login";
			this.Login.UseVisualStyleBackColor = true;
			this.Login.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// UID
			// 
			this.UID.AutoSize = true;
			this.UID.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.UID.Location = new System.Drawing.Point(130,209);
			this.UID.Margin = new System.Windows.Forms.Padding(4,0,4,0);
			this.UID.Name = "UID";
			this.UID.Size = new System.Drawing.Size(53,17);
			this.UID.TabIndex = 3;
			this.UID.Text = "User ID";
			// 
			// PWD
			// 
			this.PWD.AutoSize = true;
			this.PWD.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.PWD.Location = new System.Drawing.Point(117,264);
			this.PWD.Margin = new System.Windows.Forms.Padding(4,0,4,0);
			this.PWD.Name = "PWD";
			this.PWD.Size = new System.Drawing.Size(66,17);
			this.PWD.TabIndex = 4;
			this.PWD.Text = "Password";
			// 
			// textIP
			// 
			this.textIP.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textIP.Location = new System.Drawing.Point(191,76);
			this.textIP.Margin = new System.Windows.Forms.Padding(4);
			this.textIP.Name = "textIP";
			this.textIP.Size = new System.Drawing.Size(229,24);
			this.textIP.TabIndex = 0;
			// 
			// textPt
			// 
			this.textPt.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.textPt.Location = new System.Drawing.Point(191,114);
			this.textPt.Margin = new System.Windows.Forms.Padding(4);
			this.textPt.Name = "textPt";
			this.textPt.Size = new System.Drawing.Size(64,24);
			this.textPt.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label1.Location = new System.Drawing.Point(163,79);
			this.label1.Margin = new System.Windows.Forms.Padding(4,0,4,0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(20,17);
			this.label1.TabIndex = 7;
			this.label1.Text = "IP";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.label2.Location = new System.Drawing.Point(149,117);
			this.label2.Margin = new System.Windows.Forms.Padding(4,0,4,0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34,17);
			this.label2.TabIndex = 8;
			this.label2.Text = "Port";
			// 
			// FormBegin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F,16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(607,536);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textPt);
			this.Controls.Add(this.textIP);
			this.Controls.Add(this.PWD);
			this.Controls.Add(this.UID);
			this.Controls.Add(this.Login);
			this.Controls.Add(this.txtPWD);
			this.Controls.Add(this.txtUID);
			this.Font = new System.Drawing.Font("Tahoma",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(129)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FormBegin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Begin";
			this.Load += new System.EventHandler(this.FormBegin_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textIP;
		private System.Windows.Forms.TextBox textPt;
		private System.Windows.Forms.TextBox txtUID;
		private System.Windows.Forms.TextBox txtPWD;
		private System.Windows.Forms.Button Login;
		private System.Windows.Forms.Label UID;
		private System.Windows.Forms.Label PWD;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

