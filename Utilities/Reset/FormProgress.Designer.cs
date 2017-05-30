namespace Asa.Reset
{
	partial class FormProgress
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.laTitle = new System.Windows.Forms.Label();
			this.pnBackground = new System.Windows.Forms.Panel();
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.pnBackground.SuspendLayout();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2013;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(133, 22);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(367, 62);
			this.laTitle.TabIndex = 3;
			this.laTitle.Text = "Removing adSALESapps Files…";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnBackground
			// 
			this.pnBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.pnBackground.Controls.Add(this.circularProgress);
			this.pnBackground.Controls.Add(this.laTitle);
			this.pnBackground.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBackground.ForeColor = System.Drawing.Color.Black;
			this.pnBackground.Location = new System.Drawing.Point(0, 0);
			this.pnBackground.Name = "pnBackground";
			this.pnBackground.Size = new System.Drawing.Size(512, 106);
			this.pnBackground.TabIndex = 2;
			// 
			// circularProgress
			// 
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Location = new System.Drawing.Point(30, 22);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress.ProgressColor = System.Drawing.Color.White;
			this.circularProgress.Size = new System.Drawing.Size(63, 62);
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 4;
			// 
			// FormProgress
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(512, 106);
			this.ControlBox = false;
			this.Controls.Add(this.pnBackground);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProgress";
			this.RenderFormIcon = false;
			this.RenderFormText = false;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Shown += new System.EventHandler(this.OnFormShown);
			this.pnBackground.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private System.Windows.Forms.Label laTitle;
		private System.Windows.Forms.Panel pnBackground;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
	}
}

