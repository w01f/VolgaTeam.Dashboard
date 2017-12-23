namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	partial class CleanslateControl
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
			this.components = new System.ComponentModel.Container();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.pictureEditHeader = new DevExpress.XtraEditors.PictureEdit();
			this.pictureEditSplash = new DevExpress.XtraEditors.PictureEdit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditHeader.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditSplash.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureEditHeader
			// 
			this.pictureEditHeader.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureEditHeader.Location = new System.Drawing.Point(40, 40);
			this.pictureEditHeader.Name = "pictureEditHeader";
			this.pictureEditHeader.Properties.AllowFocused = false;
			this.pictureEditHeader.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditHeader.Properties.NullText = " ";
			this.pictureEditHeader.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.pictureEditHeader.Properties.ReadOnly = true;
			this.pictureEditHeader.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditHeader.Properties.ShowMenu = false;
			this.pictureEditHeader.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditHeader.Size = new System.Drawing.Size(925, 284);
			this.pictureEditHeader.TabIndex = 29;
			// 
			// pictureEditSplash
			// 
			this.pictureEditSplash.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureEditSplash.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pictureEditSplash.Location = new System.Drawing.Point(40, 324);
			this.pictureEditSplash.MaximumSize = new System.Drawing.Size(0, 320);
			this.pictureEditSplash.MinimumSize = new System.Drawing.Size(0, 320);
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
			this.pictureEditSplash.Size = new System.Drawing.Size(925, 320);
			this.pictureEditSplash.TabIndex = 67;
			// 
			// CleanslateControl
			// 
			this.Controls.Add(this.pictureEditHeader);
			this.Controls.Add(this.pictureEditSplash);
			this.Padding = new System.Windows.Forms.Padding(40, 40, 0, 0);
			this.Size = new System.Drawing.Size(965, 644);
			((System.ComponentModel.ISupportInitialize)(this.pictureEditHeader.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditSplash.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip;
		private DevExpress.XtraEditors.PictureEdit pictureEditHeader;
		private DevExpress.XtraEditors.PictureEdit pictureEditSplash;
	}
}
