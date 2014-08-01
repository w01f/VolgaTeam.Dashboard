namespace NewBizWiz.OnlineSchedule.Controls.ToolForms
{
	partial class FormProductInfo
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
			this.pnHeader = new System.Windows.Forms.Panel();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxInfoType = new System.Windows.Forms.PictureBox();
			this.pnFooter = new System.Windows.Forms.Panel();
			this.xtraTabControlGroups = new DevExpress.XtraTab.XtraTabControl();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoType)).BeginInit();
			this.pnFooter.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGroups)).BeginInit();
			this.SuspendLayout();
			// 
			// pnHeader
			// 
			this.pnHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnHeader.Controls.Add(this.labelControlTitle);
			this.pnHeader.Controls.Add(this.pictureBoxInfoType);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.ForeColor = System.Drawing.Color.Black;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(624, 42);
			this.pnHeader.TabIndex = 0;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTitle.Location = new System.Drawing.Point(41, 12);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(31, 19);
			this.labelControlTitle.TabIndex = 1;
			this.labelControlTitle.Text = "Title";
			// 
			// pictureBoxInfoType
			// 
			this.pictureBoxInfoType.BackColor = System.Drawing.Color.White;
			this.pictureBoxInfoType.ForeColor = System.Drawing.Color.Black;
			this.pictureBoxInfoType.Image = global::NewBizWiz.OnlineSchedule.Controls.Properties.Resources.TargetButton;
			this.pictureBoxInfoType.Location = new System.Drawing.Point(3, 5);
			this.pictureBoxInfoType.Name = "pictureBoxInfoType";
			this.pictureBoxInfoType.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxInfoType.TabIndex = 0;
			this.pictureBoxInfoType.TabStop = false;
			// 
			// pnFooter
			// 
			this.pnFooter.BackColor = System.Drawing.Color.Transparent;
			this.pnFooter.Controls.Add(this.buttonXCancel);
			this.pnFooter.Controls.Add(this.buttonXOK);
			this.pnFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnFooter.ForeColor = System.Drawing.Color.Black;
			this.pnFooter.Location = new System.Drawing.Point(0, 393);
			this.pnFooter.Name = "pnFooter";
			this.pnFooter.Size = new System.Drawing.Size(624, 49);
			this.pnFooter.TabIndex = 1;
			// 
			// xtraTabControlGroups
			// 
			this.xtraTabControlGroups.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControlGroups.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGroups.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlGroups.Appearance.Options.UseBackColor = true;
			this.xtraTabControlGroups.Appearance.Options.UseFont = true;
			this.xtraTabControlGroups.Appearance.Options.UseForeColor = true;
			this.xtraTabControlGroups.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGroups.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlGroups.Location = new System.Drawing.Point(0, 42);
			this.xtraTabControlGroups.Name = "xtraTabControlGroups";
			this.xtraTabControlGroups.Size = new System.Drawing.Size(624, 351);
			this.xtraTabControlGroups.TabIndex = 2;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(378, 7);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(105, 34);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 2;
			this.buttonXOK.Text = "OK";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(507, 7);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(105, 34);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 3;
			this.buttonXCancel.Text = "Cancel";
			// 
			// FormProductInfo
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(624, 442);
			this.Controls.Add(this.xtraTabControlGroups);
			this.Controls.Add(this.pnFooter);
			this.Controls.Add(this.pnHeader);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "FormProductInfo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormProductInfo";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormProductInfo_FormClosed);
			this.pnHeader.ResumeLayout(false);
			this.pnHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoType)).EndInit();
			this.pnFooter.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGroups)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnHeader;
		public DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.PictureBox pictureBoxInfoType;
		private System.Windows.Forms.Panel pnFooter;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlGroups;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
	}
}