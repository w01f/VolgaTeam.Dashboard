namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	public partial class DashboardSlideControl
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pnSplash = new System.Windows.Forms.Panel();
			this.pbSplash = new System.Windows.Forms.PictureBox();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnSplash.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbSplash)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnMain.Location = new System.Drawing.Point(0, 51);
			this.pnMain.MaximumSize = new System.Drawing.Size(700, 0);
			this.pnMain.MinimumSize = new System.Drawing.Size(700, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(700, 461);
			this.pnMain.TabIndex = 0;
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.comboBoxEditSlideHeader);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(997, 51);
			this.pnTop.TabIndex = 29;
			// 
			// comboBoxEditSlideHeader
			// 
			this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(11, 14);
			this.comboBoxEditSlideHeader.Name = "comboBoxEditSlideHeader";
			this.comboBoxEditSlideHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditSlideHeader.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(311, 22);
			this.comboBoxEditSlideHeader.TabIndex = 28;
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// pnSplash
			// 
			this.pnSplash.Controls.Add(this.pbSplash);
			this.pnSplash.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnSplash.Location = new System.Drawing.Point(700, 51);
			this.pnSplash.Name = "pnSplash";
			this.pnSplash.Size = new System.Drawing.Size(297, 461);
			this.pnSplash.TabIndex = 30;
			this.pnSplash.Resize += new System.EventHandler(this.OnSplashResize);
			// 
			// pbSplash
			// 
			this.pbSplash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbSplash.Location = new System.Drawing.Point(-114, 151);
			this.pbSplash.Name = "pbSplash";
			this.pbSplash.Size = new System.Drawing.Size(411, 310);
			this.pbSplash.TabIndex = 0;
			this.pbSplash.TabStop = false;
			// 
			// DashboardSlideControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnSplash);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnTop);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DashboardSlideControl";
			this.Size = new System.Drawing.Size(997, 512);
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnSplash.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbSplash)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		protected System.Windows.Forms.Panel pnMain;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
		protected System.Windows.Forms.Panel pnTop;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		protected System.Windows.Forms.PictureBox pbSplash;
		protected System.Windows.Forms.Panel pnSplash;
	}
}
