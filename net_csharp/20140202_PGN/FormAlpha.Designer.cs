namespace ydnet
{
	partial class FormAlpha
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
			this.SuspendLayout();
			// 
			// FormAlpha
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F,12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634,506);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAlpha";
			this.Text = "Alpha";
			this.Load += new System.EventHandler(this.FormAlpha_Load);
			this.ResumeLayout(false);

		}

		#endregion
	}
}