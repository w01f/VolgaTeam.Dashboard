namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class StarAppControl
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
			this.pnTop = new System.Windows.Forms.Panel();
			this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pnLogoRight = new System.Windows.Forms.Panel();
			this.pbLogoRight = new System.Windows.Forms.PictureBox();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnLogoFooter = new System.Windows.Forms.Panel();
			this.pbLogoFooter = new System.Windows.Forms.PictureBox();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnLogoRight.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogoRight)).BeginInit();
			this.pnLogoFooter.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogoFooter)).BeginInit();
			this.SuspendLayout();
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.comboBoxEditSlideHeader);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(677, 51);
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
			// pnLogoRight
			// 
			this.pnLogoRight.Controls.Add(this.pbLogoRight);
			this.pnLogoRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnLogoRight.Location = new System.Drawing.Point(677, 0);
			this.pnLogoRight.Name = "pnLogoRight";
			this.pnLogoRight.Padding = new System.Windows.Forms.Padding(10);
			this.pnLogoRight.Size = new System.Drawing.Size(320, 512);
			this.pnLogoRight.TabIndex = 30;
			// 
			// pbLogoRight
			// 
			this.pbLogoRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbLogoRight.Location = new System.Drawing.Point(10, 10);
			this.pbLogoRight.Name = "pbLogoRight";
			this.pbLogoRight.Size = new System.Drawing.Size(300, 492);
			this.pbLogoRight.TabIndex = 0;
			this.pbLogoRight.TabStop = false;
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 51);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(677, 359);
			this.pnMain.TabIndex = 0;
			// 
			// pnLogoFooter
			// 
			this.pnLogoFooter.BackColor = System.Drawing.Color.Transparent;
			this.pnLogoFooter.Controls.Add(this.pbLogoFooter);
			this.pnLogoFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnLogoFooter.Location = new System.Drawing.Point(0, 410);
			this.pnLogoFooter.Name = "pnLogoFooter";
			this.pnLogoFooter.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
			this.pnLogoFooter.Size = new System.Drawing.Size(677, 102);
			this.pnLogoFooter.TabIndex = 31;
			// 
			// pbLogoFooter
			// 
			this.pbLogoFooter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbLogoFooter.Location = new System.Drawing.Point(10, 10);
			this.pbLogoFooter.Name = "pbLogoFooter";
			this.pbLogoFooter.Size = new System.Drawing.Size(667, 82);
			this.pbLogoFooter.TabIndex = 1;
			this.pbLogoFooter.TabStop = false;
			// 
			// StarAppSlideControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnLogoFooter);
			this.Controls.Add(this.pnTop);
			this.Controls.Add(this.pnLogoRight);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "StarAppControl";
			this.Size = new System.Drawing.Size(997, 512);
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnLogoRight.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLogoRight)).EndInit();
			this.pnLogoFooter.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbLogoFooter)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
		protected System.Windows.Forms.Panel pnTop;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		protected System.Windows.Forms.PictureBox pbLogoRight;
		protected System.Windows.Forms.Panel pnLogoRight;
		protected System.Windows.Forms.Panel pnMain;
		protected System.Windows.Forms.Panel pnLogoFooter;
		protected System.Windows.Forms.PictureBox pbLogoFooter;
	}
}
