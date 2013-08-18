namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms
{
    partial class FormSelectOutput
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
			this.laOutputTitle = new System.Windows.Forms.Label();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.labelControlDescription = new DevExpress.XtraEditors.LabelControl();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.pbHelp = new System.Windows.Forms.PictureBox();
			this.buttonXOutput = new DevComponents.DotNetBar.ButtonX();
			this.pictureBoxTitle = new System.Windows.Forms.PictureBox();
			this.buttonXGrid = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExcel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXImage = new DevComponents.DotNetBar.ButtonX();
			this.groupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbHelp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).BeginInit();
			this.SuspendLayout();
			// 
			// laOutputTitle
			// 
			this.laOutputTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laOutputTitle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laOutputTitle.Location = new System.Drawing.Point(311, 6);
			this.laOutputTitle.Name = "laOutputTitle";
			this.laOutputTitle.Size = new System.Drawing.Size(278, 81);
			this.laOutputTitle.TabIndex = 1;
			this.laOutputTitle.Text = "PowerPoint Slide\r\nOutput Options";
			this.laOutputTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox
			// 
			this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox.Controls.Add(this.labelControlDescription);
			this.groupBox.Controls.Add(this.buttonXGrid);
			this.groupBox.Controls.Add(this.buttonXExcel);
			this.groupBox.Controls.Add(this.buttonXImage);
			this.groupBox.Location = new System.Drawing.Point(12, 88);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(613, 264);
			this.groupBox.TabIndex = 19;
			this.groupBox.TabStop = false;
			// 
			// labelControlDescription
			// 
			this.labelControlDescription.AllowHtmlString = true;
			this.labelControlDescription.Appearance.Font = new System.Drawing.Font("Arial", 14.25F);
			this.labelControlDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDescription.Location = new System.Drawing.Point(311, 21);
			this.labelControlDescription.Name = "labelControlDescription";
			this.labelControlDescription.Size = new System.Drawing.Size(281, 231);
			this.labelControlDescription.TabIndex = 21;
			this.labelControlDescription.Text = "<b>Text</b> will Go Here that explains Paste as Excel";
			// 
			// superTooltip
			// 
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXCancel.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Exit;
			this.buttonXCancel.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXCancel.Location = new System.Drawing.Point(331, 371);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(200, 60);
			this.buttonXCancel.TabIndex = 24;
			this.buttonXCancel.Text = "    Cancel";
			this.buttonXCancel.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXCancel.TextColor = System.Drawing.Color.Red;
			this.buttonXCancel.Tooltip = "Close this window";
			// 
			// pbHelp
			// 
			this.pbHelp.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.HelpSmall;
			this.pbHelp.Location = new System.Drawing.Point(595, 6);
			this.pbHelp.Name = "pbHelp";
			this.pbHelp.Size = new System.Drawing.Size(30, 30);
			this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.superTooltip.SetSuperTooltip(this.pbHelp, new DevComponents.DotNetBar.SuperTooltipInfo("Help", "", "Learn more about this slide output option", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.pbHelp.TabIndex = 23;
			this.pbHelp.TabStop = false;
			this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
			this.pbHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// buttonXOutput
			// 
			this.buttonXOutput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOutput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOutput.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOutput.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Email;
			this.buttonXOutput.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXOutput.Location = new System.Drawing.Point(101, 371);
			this.buttonXOutput.Name = "buttonXOutput";
			this.buttonXOutput.Size = new System.Drawing.Size(200, 60);
			this.buttonXOutput.TabIndex = 21;
			this.buttonXOutput.Text = "   Email";
			this.buttonXOutput.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXOutput.TextColor = System.Drawing.Color.Black;
			this.buttonXOutput.Tooltip = "Output this schedule to your PowerPoint presentation";
			// 
			// pictureBoxTitle
			// 
			this.pictureBoxTitle.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.OutputLogo;
			this.pictureBoxTitle.Location = new System.Drawing.Point(12, 6);
			this.pictureBoxTitle.Name = "pictureBoxTitle";
			this.pictureBoxTitle.Size = new System.Drawing.Size(293, 81);
			this.pictureBoxTitle.TabIndex = 20;
			this.pictureBoxTitle.TabStop = false;
			// 
			// buttonXGrid
			// 
			this.buttonXGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGrid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGrid.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXGrid.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Table;
			this.buttonXGrid.Location = new System.Drawing.Point(22, 21);
			this.buttonXGrid.Name = "buttonXGrid";
			this.buttonXGrid.Size = new System.Drawing.Size(271, 61);
			this.buttonXGrid.TabIndex = 9;
			this.buttonXGrid.Text = "  Slide Table";
			this.buttonXGrid.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXGrid.TextColor = System.Drawing.Color.Black;
			this.buttonXGrid.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
			this.buttonXGrid.Click += new System.EventHandler(this.buttonXOutputType_Click);
			// 
			// buttonXExcel
			// 
			this.buttonXExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExcel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXExcel.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Excel;
			this.buttonXExcel.Location = new System.Drawing.Point(22, 191);
			this.buttonXExcel.Name = "buttonXExcel";
			this.buttonXExcel.Size = new System.Drawing.Size(271, 61);
			this.buttonXExcel.TabIndex = 12;
			this.buttonXExcel.Text = "  Excel Grid";
			this.buttonXExcel.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXExcel.TextColor = System.Drawing.Color.Black;
			this.buttonXExcel.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
			this.buttonXExcel.Click += new System.EventHandler(this.buttonXOutputType_Click);
			// 
			// buttonXImage
			// 
			this.buttonXImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXImage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXImage.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXImage.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.Image;
			this.buttonXImage.Location = new System.Drawing.Point(22, 107);
			this.buttonXImage.Name = "buttonXImage";
			this.buttonXImage.Size = new System.Drawing.Size(271, 61);
			this.buttonXImage.TabIndex = 14;
			this.buttonXImage.Text = "  Single Image";
			this.buttonXImage.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXImage.TextColor = System.Drawing.Color.Black;
			this.buttonXImage.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
			this.buttonXImage.Click += new System.EventHandler(this.buttonXOutputType_Click);
			// 
			// FormSelectOutput
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(633, 445);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.pbHelp);
			this.Controls.Add(this.buttonXOutput);
			this.Controls.Add(this.pictureBoxTitle);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.laOutputTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectOutput";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Slide Output Options";
			this.Load += new System.EventHandler(this.FormSelectOutput_Load);
			this.groupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbHelp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXExcel;
        public DevComponents.DotNetBar.ButtonX buttonXGrid;
        private DevComponents.DotNetBar.ButtonX buttonXImage;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.PictureBox pictureBoxTitle;
        public System.Windows.Forms.Label laOutputTitle;
        public DevComponents.DotNetBar.ButtonX buttonXOutput;
        private System.Windows.Forms.PictureBox pbHelp;
        private DevComponents.DotNetBar.SuperTooltip superTooltip;
        public DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.LabelControl labelControlDescription;
    }
}