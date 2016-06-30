namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	partial class HomePage
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.pnLogo = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.pnLogo.SuspendLayout();
			this.SuspendLayout();
			// 
			// pbLogo
			// 
			this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbLogo.Location = new System.Drawing.Point(20, 20);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(110, 110);
			this.pbLogo.TabIndex = 0;
			this.pbLogo.TabStop = false;
			// 
			// pnLogo
			// 
			this.pnLogo.BackColor = System.Drawing.Color.Transparent;
			this.pnLogo.Controls.Add(this.pbLogo);
			this.pnLogo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnLogo.Location = new System.Drawing.Point(0, 0);
			this.pnLogo.Name = "pnLogo";
			this.pnLogo.Padding = new System.Windows.Forms.Padding(20);
			this.pnLogo.Size = new System.Drawing.Size(150, 150);
			this.pnLogo.TabIndex = 1;
			// 
			// HomePage
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnLogo);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "HomePage";
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.pnLogo.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Panel pnLogo;
	}
}
