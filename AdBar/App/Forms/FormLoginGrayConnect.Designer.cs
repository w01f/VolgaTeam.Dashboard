namespace Asa.Bar.App.Forms
{
	partial class FormLoginGrayConnect
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
            this.labelXMainTitle = new DevComponents.DotNetBar.LabelX();
            this.textBoxXPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelXUserDescription = new DevComponents.DotNetBar.LabelX();
            this.labelXUserTitle = new DevComponents.DotNetBar.LabelX();
            this.textBoxXUser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelXPasswordDescription = new DevComponents.DotNetBar.LabelX();
            this.labelXPasswordTitle = new DevComponents.DotNetBar.LabelX();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelXErrorText = new DevComponents.DotNetBar.LabelX();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.labelXLoginStepsDescription = new DevComponents.DotNetBar.LabelX();
            this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
            this.pictureBoxPasswordLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxUserLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxMainLogo = new System.Windows.Forms.PictureBox();
            this.buttonXMainSite = new DevComponents.DotNetBar.ButtonX();
            this.buttonXGrayConnect = new DevComponents.DotNetBar.ButtonX();
            this.pnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelXMainTitle
            // 
            this.labelXMainTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXMainTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.labelXMainTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXMainTitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXMainTitle.ForeColor = System.Drawing.Color.Black;
            this.labelXMainTitle.Location = new System.Drawing.Point(171, 12);
            this.labelXMainTitle.Name = "labelXMainTitle";
            this.labelXMainTitle.Size = new System.Drawing.Size(650, 70);
            this.labelXMainTitle.TabIndex = 1;
            this.labelXMainTitle.Text = "<font color=\"#ED1C24\" size=\"+4\">{0}</font>";
            // 
            // textBoxXPassword
            // 
            this.textBoxXPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXPassword.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxXPassword.Border.Class = "TextBoxBorder";
            this.textBoxXPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXPassword.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxXPassword.ForeColor = System.Drawing.Color.Black;
            this.textBoxXPassword.Location = new System.Drawing.Point(171, 201);
            this.textBoxXPassword.Name = "textBoxXPassword";
            this.textBoxXPassword.Size = new System.Drawing.Size(650, 22);
            this.textBoxXPassword.TabIndex = 2;
            this.textBoxXPassword.UseSystemPasswordChar = true;
            this.textBoxXPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnEnterKeyPress);
            // 
            // labelXUserDescription
            // 
            this.labelXUserDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXUserDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.labelXUserDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXUserDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXUserDescription.ForeColor = System.Drawing.Color.Black;
            this.labelXUserDescription.Location = new System.Drawing.Point(171, 132);
            this.labelXUserDescription.Name = "labelXUserDescription";
            this.labelXUserDescription.Size = new System.Drawing.Size(650, 19);
            this.labelXUserDescription.TabIndex = 9;
            this.labelXUserDescription.Text = "<font color=\"#8C8C8C\">(Type your email address here)</font>";
            // 
            // labelXUserTitle
            // 
            this.labelXUserTitle.AutoSize = true;
            this.labelXUserTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.labelXUserTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXUserTitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXUserTitle.ForeColor = System.Drawing.Color.Black;
            this.labelXUserTitle.Location = new System.Drawing.Point(82, 104);
            this.labelXUserTitle.Name = "labelXUserTitle";
            this.labelXUserTitle.Size = new System.Drawing.Size(48, 19);
            this.labelXUserTitle.TabIndex = 7;
            this.labelXUserTitle.Text = "Email:";
            // 
            // textBoxXUser
            // 
            this.textBoxXUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXUser.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxXUser.Border.Class = "TextBoxBorder";
            this.textBoxXUser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXUser.DisabledBackColor = System.Drawing.Color.White;
            this.textBoxXUser.ForeColor = System.Drawing.Color.Black;
            this.textBoxXUser.Location = new System.Drawing.Point(171, 104);
            this.textBoxXUser.Name = "textBoxXUser";
            this.textBoxXUser.Size = new System.Drawing.Size(650, 22);
            this.textBoxXUser.TabIndex = 1;
            this.textBoxXUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnEnterKeyPress);
            // 
            // labelXPasswordDescription
            // 
            this.labelXPasswordDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXPasswordDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.labelXPasswordDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPasswordDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXPasswordDescription.ForeColor = System.Drawing.Color.Black;
            this.labelXPasswordDescription.Location = new System.Drawing.Point(171, 229);
            this.labelXPasswordDescription.Name = "labelXPasswordDescription";
            this.labelXPasswordDescription.Size = new System.Drawing.Size(650, 19);
            this.labelXPasswordDescription.TabIndex = 13;
            this.labelXPasswordDescription.Text = "<font color=\"#8C8C8C\">(Type your account password here)</font>";
            this.labelXPasswordDescription.UseMnemonic = false;
            // 
            // labelXPasswordTitle
            // 
            this.labelXPasswordTitle.AutoSize = true;
            this.labelXPasswordTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.labelXPasswordTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXPasswordTitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXPasswordTitle.ForeColor = System.Drawing.Color.Black;
            this.labelXPasswordTitle.Location = new System.Drawing.Point(82, 201);
            this.labelXPasswordTitle.Name = "labelXPasswordTitle";
            this.labelXPasswordTitle.Size = new System.Drawing.Size(78, 19);
            this.labelXPasswordTitle.TabIndex = 11;
            this.labelXPasswordTitle.Text = "Password:";
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.Location = new System.Drawing.Point(612, 460);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(95, 33);
            this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXOK.TabIndex = 14;
            this.buttonXOK.Text = "OK";
            this.buttonXOK.Click += new System.EventHandler(this.OnOKClick);
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(726, 460);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(95, 33);
            this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXCancel.TabIndex = 15;
            this.buttonXCancel.Text = "Cancel";
            // 
            // labelXErrorText
            // 
            this.labelXErrorText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.labelXErrorText.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXErrorText.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelXErrorText.ForeColor = System.Drawing.Color.Black;
            this.labelXErrorText.Location = new System.Drawing.Point(0, 0);
            this.labelXErrorText.Name = "labelXErrorText";
            this.labelXErrorText.Size = new System.Drawing.Size(809, 36);
            this.labelXErrorText.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelXErrorText.SymbolColor = System.Drawing.Color.Red;
            this.labelXErrorText.TabIndex = 17;
            this.labelXErrorText.Text = "<span align=\"center\"><font color=\"#ED1C24\">Error</font></span> ";
            this.labelXErrorText.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelXErrorText.Visible = false;
            // 
            // pnInfo
            // 
            this.pnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.pnInfo.Controls.Add(this.circularProgress);
            this.pnInfo.Controls.Add(this.labelXErrorText);
            this.pnInfo.ForeColor = System.Drawing.Color.Black;
            this.pnInfo.Location = new System.Drawing.Point(12, 421);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(809, 36);
            this.pnInfo.TabIndex = 18;
            // 
            // circularProgress
            // 
            this.circularProgress.AnimationSpeed = 50;
            this.circularProgress.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circularProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.circularProgress.Enabled = false;
            this.circularProgress.Location = new System.Drawing.Point(0, 36);
            this.circularProgress.Name = "circularProgress";
            this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
            this.circularProgress.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.circularProgress.ProgressTextFormat = "";
            this.circularProgress.Size = new System.Drawing.Size(809, 36);
            this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circularProgress.TabIndex = 18;
            this.circularProgress.Visible = false;
            // 
            // labelXLoginStepsDescription
            // 
            this.labelXLoginStepsDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXLoginStepsDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.labelXLoginStepsDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXLoginStepsDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelXLoginStepsDescription.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXLoginStepsDescription.ForeColor = System.Drawing.Color.Black;
            this.labelXLoginStepsDescription.Location = new System.Drawing.Point(12, 270);
            this.labelXLoginStepsDescription.Name = "labelXLoginStepsDescription";
            this.labelXLoginStepsDescription.Size = new System.Drawing.Size(809, 152);
            this.labelXLoginStepsDescription.TabIndex = 19;
            this.labelXLoginStepsDescription.Text = "<div>{0} <a href=\"{1}\">{2}</a></div>\r\n<div> </div>\r\n<div>{3} <a href=\"{4}\">{5}</a" +
    "></div>\r\n<div> </div>\r\n<div>{6} <a href=\"{7}\">{8}</a></div>\r\n<div> </div>\r\n<div>" +
    "{9} <a href=\"{10}\">{11}</a></div>";
            this.labelXLoginStepsDescription.MarkupLinkClick += new DevComponents.DotNetBar.MarkupLinkClickEventHandler(this.OnUrlLabelClick);
            // 
            // styleManager
            // 
            this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // pictureBoxPasswordLogo
            // 
            this.pictureBoxPasswordLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pictureBoxPasswordLogo.ForeColor = System.Drawing.Color.Black;
            this.pictureBoxPasswordLogo.Image = global::Asa.Bar.App.Properties.Resources.PasswordLogo;
            this.pictureBoxPasswordLogo.Location = new System.Drawing.Point(12, 201);
            this.pictureBoxPasswordLogo.Name = "pictureBoxPasswordLogo";
            this.pictureBoxPasswordLogo.Size = new System.Drawing.Size(50, 47);
            this.pictureBoxPasswordLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPasswordLogo.TabIndex = 12;
            this.pictureBoxPasswordLogo.TabStop = false;
            // 
            // pictureBoxUserLogo
            // 
            this.pictureBoxUserLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pictureBoxUserLogo.ForeColor = System.Drawing.Color.Black;
            this.pictureBoxUserLogo.Image = global::Asa.Bar.App.Properties.Resources.UserLogo;
            this.pictureBoxUserLogo.Location = new System.Drawing.Point(12, 104);
            this.pictureBoxUserLogo.Name = "pictureBoxUserLogo";
            this.pictureBoxUserLogo.Size = new System.Drawing.Size(50, 47);
            this.pictureBoxUserLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxUserLogo.TabIndex = 8;
            this.pictureBoxUserLogo.TabStop = false;
            // 
            // pictureBoxMainLogo
            // 
            this.pictureBoxMainLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pictureBoxMainLogo.ForeColor = System.Drawing.Color.Black;
            this.pictureBoxMainLogo.Image = global::Asa.Bar.App.Properties.Resources.LoginLogo;
            this.pictureBoxMainLogo.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxMainLogo.Name = "pictureBoxMainLogo";
            this.pictureBoxMainLogo.Size = new System.Drawing.Size(126, 70);
            this.pictureBoxMainLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMainLogo.TabIndex = 0;
            this.pictureBoxMainLogo.TabStop = false;
            // 
            // buttonXMainSite
            // 
            this.buttonXMainSite.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXMainSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXMainSite.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXMainSite.Location = new System.Drawing.Point(12, 460);
            this.buttonXMainSite.Name = "buttonXMainSite";
            this.buttonXMainSite.Size = new System.Drawing.Size(110, 33);
            this.buttonXMainSite.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXMainSite.TabIndex = 21;
            this.buttonXMainSite.Text = "GraySales.tv";
            this.buttonXMainSite.Click += new System.EventHandler(this.OnMainSiteClick);
            // 
            // buttonXGrayConnect
            // 
            this.buttonXGrayConnect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXGrayConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXGrayConnect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXGrayConnect.Location = new System.Drawing.Point(141, 460);
            this.buttonXGrayConnect.Name = "buttonXGrayConnect";
            this.buttonXGrayConnect.Size = new System.Drawing.Size(110, 33);
            this.buttonXGrayConnect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXGrayConnect.TabIndex = 22;
            this.buttonXGrayConnect.Text = "Gray Connect";
            this.buttonXGrayConnect.Click += new System.EventHandler(this.OnGrayConnectClick);
            // 
            // FormLoginGrayConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(833, 505);
            this.Controls.Add(this.buttonXGrayConnect);
            this.Controls.Add(this.buttonXMainSite);
            this.Controls.Add(this.labelXLoginStepsDescription);
            this.Controls.Add(this.pnInfo);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.labelXPasswordDescription);
            this.Controls.Add(this.pictureBoxPasswordLogo);
            this.Controls.Add(this.labelXPasswordTitle);
            this.Controls.Add(this.labelXUserDescription);
            this.Controls.Add(this.pictureBoxUserLogo);
            this.Controls.Add(this.labelXUserTitle);
            this.Controls.Add(this.textBoxXUser);
            this.Controls.Add(this.textBoxXPassword);
            this.Controls.Add(this.labelXMainTitle);
            this.Controls.Add(this.pictureBoxMainLogo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLoginGrayConnect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud Account: {0}";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.pnInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxMainLogo;
		private DevComponents.DotNetBar.LabelX labelXMainTitle;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxXPassword;
		private DevComponents.DotNetBar.LabelX labelXUserDescription;
		private System.Windows.Forms.PictureBox pictureBoxUserLogo;
		private DevComponents.DotNetBar.LabelX labelXUserTitle;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxXUser;
		private DevComponents.DotNetBar.LabelX labelXPasswordDescription;
		private System.Windows.Forms.PictureBox pictureBoxPasswordLogo;
		private DevComponents.DotNetBar.LabelX labelXPasswordTitle;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.LabelX labelXErrorText;
		private System.Windows.Forms.Panel pnInfo;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
		private DevComponents.DotNetBar.LabelX labelXLoginStepsDescription;
		private DevComponents.DotNetBar.StyleManager styleManager;
        private DevComponents.DotNetBar.ButtonX buttonXMainSite;
        private DevComponents.DotNetBar.ButtonX buttonXGrayConnect;
    }
}