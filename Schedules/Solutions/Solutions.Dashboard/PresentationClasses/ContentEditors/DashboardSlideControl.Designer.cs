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
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pictureEditSplash = new DevExpress.XtraEditors.PictureEdit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditSplash.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// pictureEditSplash
			// 
			this.pictureEditSplash.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditSplash.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureEditSplash.Location = new System.Drawing.Point(0, 0);
			this.pictureEditSplash.Name = "pictureEditSplash";
			this.pictureEditSplash.Properties.AllowFocused = false;
			this.pictureEditSplash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditSplash.Properties.NullText = " ";
			this.pictureEditSplash.Properties.PictureAlignment = System.Drawing.ContentAlignment.BottomRight;
			this.pictureEditSplash.Properties.ReadOnly = true;
			this.pictureEditSplash.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditSplash.Properties.ShowMenu = false;
			this.pictureEditSplash.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			this.pictureEditSplash.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditSplash.Size = new System.Drawing.Size(997, 512);
			this.pictureEditSplash.TabIndex = 2;
			// 
			// DashboardSlideControl
			// 
			this.Controls.Add(this.pictureEditSplash);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(997, 512);
			((System.ComponentModel.ISupportInitialize)(this.pictureEditSplash.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevComponents.DotNetBar.SuperTooltip superTooltip;
		protected DevExpress.XtraEditors.PictureEdit pictureEditSplash;
	}
}
