namespace TVScheduleBuilder.ToolForms
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
            this.laOutputDescription = new System.Windows.Forms.Label();
            this.labelControlSlidesCount = new DevExpress.XtraEditors.LabelControl();
            this.buttonXGrid = new DevComponents.DotNetBar.ButtonX();
            this.buttonXGroupedObjects = new DevComponents.DotNetBar.ButtonX();
            this.buttonXExcel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXSlideMaster = new DevComponents.DotNetBar.ButtonX();
            this.buttonXImage = new DevComponents.DotNetBar.ButtonX();
            this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
            this.pbHelp = new System.Windows.Forms.PictureBox();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXPreview = new DevComponents.DotNetBar.ButtonX();
            this.buttonXOutput = new DevComponents.DotNetBar.ButtonX();
            this.pictureBoxTitle = new System.Windows.Forms.PictureBox();
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
            this.laOutputTitle.Location = new System.Drawing.Point(282, 6);
            this.laOutputTitle.Name = "laOutputTitle";
            this.laOutputTitle.Size = new System.Drawing.Size(261, 81);
            this.laOutputTitle.TabIndex = 1;
            this.laOutputTitle.Text = "PowerPoint Slide\r\nOutput Options";
            this.laOutputTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.laOutputDescription);
            this.groupBox.Controls.Add(this.labelControlSlidesCount);
            this.groupBox.Controls.Add(this.buttonXGrid);
            this.groupBox.Controls.Add(this.buttonXGroupedObjects);
            this.groupBox.Controls.Add(this.buttonXExcel);
            this.groupBox.Controls.Add(this.buttonXSlideMaster);
            this.groupBox.Controls.Add(this.buttonXImage);
            this.groupBox.Location = new System.Drawing.Point(12, 88);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(613, 427);
            this.groupBox.TabIndex = 19;
            this.groupBox.TabStop = false;
            // 
            // laOutputDescription
            // 
            this.laOutputDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laOutputDescription.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laOutputDescription.ForeColor = System.Drawing.Color.Black;
            this.laOutputDescription.Location = new System.Drawing.Point(307, 21);
            this.laOutputDescription.Name = "laOutputDescription";
            this.laOutputDescription.Size = new System.Drawing.Size(285, 359);
            this.laOutputDescription.TabIndex = 20;
            this.laOutputDescription.Text = "Text will Go Here that explains Paste as Excel";
            // 
            // labelControlSlidesCount
            // 
            this.labelControlSlidesCount.AllowHtmlString = true;
            this.labelControlSlidesCount.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlSlidesCount.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControlSlidesCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlSlidesCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlSlidesCount.Location = new System.Drawing.Point(311, 383);
            this.labelControlSlidesCount.Name = "labelControlSlidesCount";
            this.labelControlSlidesCount.Size = new System.Drawing.Size(281, 34);
            this.labelControlSlidesCount.TabIndex = 22;
            this.labelControlSlidesCount.Text = "Estimated Slides: <b>{0}</b>";
            // 
            // buttonXGrid
            // 
            this.buttonXGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXGrid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXGrid.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXGrid.Image = global::TVScheduleBuilder.Properties.Resources.GridInactive;
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
            // buttonXGroupedObjects
            // 
            this.buttonXGroupedObjects.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXGroupedObjects.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXGroupedObjects.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXGroupedObjects.Image = global::TVScheduleBuilder.Properties.Resources.TextGroupedInactive;
            this.buttonXGroupedObjects.Location = new System.Drawing.Point(22, 188);
            this.buttonXGroupedObjects.Name = "buttonXGroupedObjects";
            this.buttonXGroupedObjects.Size = new System.Drawing.Size(271, 61);
            this.buttonXGroupedObjects.TabIndex = 18;
            this.buttonXGroupedObjects.Text = "  Grouped Objects";
            this.buttonXGroupedObjects.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXGroupedObjects.TextColor = System.Drawing.Color.Black;
            this.buttonXGroupedObjects.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
            this.buttonXGroupedObjects.Click += new System.EventHandler(this.buttonXOutputType_Click);
            // 
            // buttonXExcel
            // 
            this.buttonXExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXExcel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXExcel.Image = global::TVScheduleBuilder.Properties.Resources.ExcelInactive;
            this.buttonXExcel.Location = new System.Drawing.Point(22, 356);
            this.buttonXExcel.Name = "buttonXExcel";
            this.buttonXExcel.Size = new System.Drawing.Size(271, 61);
            this.buttonXExcel.TabIndex = 12;
            this.buttonXExcel.Text = "  Excel Grid";
            this.buttonXExcel.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXExcel.TextColor = System.Drawing.Color.Black;
            this.buttonXExcel.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
            this.buttonXExcel.Click += new System.EventHandler(this.buttonXOutputType_Click);
            // 
            // buttonXSlideMaster
            // 
            this.buttonXSlideMaster.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSlideMaster.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSlideMaster.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXSlideMaster.Image = global::TVScheduleBuilder.Properties.Resources.SlideMasterInactive;
            this.buttonXSlideMaster.Location = new System.Drawing.Point(22, 104);
            this.buttonXSlideMaster.Name = "buttonXSlideMaster";
            this.buttonXSlideMaster.Size = new System.Drawing.Size(271, 61);
            this.buttonXSlideMaster.TabIndex = 16;
            this.buttonXSlideMaster.Text = "  Slide Master";
            this.buttonXSlideMaster.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXSlideMaster.TextColor = System.Drawing.Color.Black;
            this.buttonXSlideMaster.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
            this.buttonXSlideMaster.Click += new System.EventHandler(this.buttonXOutputType_Click);
            // 
            // buttonXImage
            // 
            this.buttonXImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXImage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXImage.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXImage.Image = global::TVScheduleBuilder.Properties.Resources.ImageInactive;
            this.buttonXImage.Location = new System.Drawing.Point(22, 272);
            this.buttonXImage.Name = "buttonXImage";
            this.buttonXImage.Size = new System.Drawing.Size(271, 61);
            this.buttonXImage.TabIndex = 14;
            this.buttonXImage.Text = "  Single Image";
            this.buttonXImage.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXImage.TextColor = System.Drawing.Color.Black;
            this.buttonXImage.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
            this.buttonXImage.Click += new System.EventHandler(this.buttonXOutputType_Click);
            // 
            // superTooltip
            // 
            this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // pbHelp
            // 
            this.pbHelp.Image = global::TVScheduleBuilder.Properties.Resources.Help;
            this.pbHelp.Location = new System.Drawing.Point(549, 6);
            this.pbHelp.Name = "pbHelp";
            this.pbHelp.Size = new System.Drawing.Size(76, 81);
            this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.superTooltip.SetSuperTooltip(this.pbHelp, new DevComponents.DotNetBar.SuperTooltipInfo("Help", "", "Learn more about this slide output option", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
            this.pbHelp.TabIndex = 23;
            this.pbHelp.TabStop = false;
            this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
            this.pbHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXCancel.Location = new System.Drawing.Point(498, 528);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(127, 60);
            this.buttonXCancel.TabIndex = 24;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Red;
            this.buttonXCancel.Tooltip = "Close this window";
            // 
            // buttonXPreview
            // 
            this.buttonXPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXPreview.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXPreview.Image = global::TVScheduleBuilder.Properties.Resources.Preview;
            this.buttonXPreview.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.buttonXPreview.Location = new System.Drawing.Point(12, 528);
            this.buttonXPreview.Name = "buttonXPreview";
            this.buttonXPreview.Size = new System.Drawing.Size(169, 60);
            this.buttonXPreview.TabIndex = 25;
            this.buttonXPreview.Text = "Preview this \r\nslide first";
            this.buttonXPreview.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXPreview.TextColor = System.Drawing.Color.Black;
            this.buttonXPreview.Tooltip = "View this schedule before you send it to PowerPoint";
            // 
            // buttonXOutput
            // 
            this.buttonXOutput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOutput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOutput.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXOutput.Image = global::TVScheduleBuilder.Properties.Resources.Email;
            this.buttonXOutput.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.buttonXOutput.Location = new System.Drawing.Point(199, 528);
            this.buttonXOutput.Name = "buttonXOutput";
            this.buttonXOutput.Size = new System.Drawing.Size(169, 60);
            this.buttonXOutput.TabIndex = 21;
            this.buttonXOutput.Text = "Send to \r\nPowerPoint";
            this.buttonXOutput.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXOutput.TextColor = System.Drawing.Color.Black;
            this.buttonXOutput.Tooltip = "Output this schedule to your PowerPoint presentation";
            // 
            // pictureBoxTitle
            // 
            this.pictureBoxTitle.Image = global::TVScheduleBuilder.Properties.Resources.OutputLogo;
            this.pictureBoxTitle.Location = new System.Drawing.Point(12, 6);
            this.pictureBoxTitle.Name = "pictureBoxTitle";
            this.pictureBoxTitle.Size = new System.Drawing.Size(264, 81);
            this.pictureBoxTitle.TabIndex = 20;
            this.pictureBoxTitle.TabStop = false;
            // 
            // FormSelectOutput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(633, 600);
            this.Controls.Add(this.buttonXPreview);
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
        private DevComponents.DotNetBar.ButtonX buttonXSlideMaster;
        private DevComponents.DotNetBar.ButtonX buttonXGroupedObjects;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label laOutputDescription;
        private System.Windows.Forms.PictureBox pictureBoxTitle;
        private DevExpress.XtraEditors.LabelControl labelControlSlidesCount;
        public System.Windows.Forms.Label laOutputTitle;
        public DevComponents.DotNetBar.ButtonX buttonXOutput;
        private System.Windows.Forms.PictureBox pbHelp;
        private DevComponents.DotNetBar.SuperTooltip superTooltip;
        public DevComponents.DotNetBar.ButtonX buttonXCancel;
        public DevComponents.DotNetBar.ButtonX buttonXPreview;
    }
}