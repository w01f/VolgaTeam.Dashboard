namespace RadioScheduleBuilder.ToolForms
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
            this.buttonXOutput = new DevComponents.DotNetBar.ButtonX();
            this.labelControlSlidesCount = new DevExpress.XtraEditors.LabelControl();
            this.pbOutputLogo = new System.Windows.Forms.PictureBox();
            this.buttonXGrid = new DevComponents.DotNetBar.ButtonX();
            this.buttonXGroupedObjects = new DevComponents.DotNetBar.ButtonX();
            this.buttonXExcel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXSlideMaster = new DevComponents.DotNetBar.ButtonX();
            this.buttonXImage = new DevComponents.DotNetBar.ButtonX();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutputLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // laOutputTitle
            // 
            this.laOutputTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laOutputTitle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laOutputTitle.Location = new System.Drawing.Point(323, 6);
            this.laOutputTitle.Name = "laOutputTitle";
            this.laOutputTitle.Size = new System.Drawing.Size(259, 81);
            this.laOutputTitle.TabIndex = 1;
            this.laOutputTitle.Text = "PowerPoint Slide\r\nOutput Options";
            this.laOutputTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.laOutputDescription);
            this.groupBox.Controls.Add(this.buttonXGrid);
            this.groupBox.Controls.Add(this.buttonXGroupedObjects);
            this.groupBox.Controls.Add(this.buttonXExcel);
            this.groupBox.Controls.Add(this.buttonXSlideMaster);
            this.groupBox.Controls.Add(this.buttonXImage);
            this.groupBox.Location = new System.Drawing.Point(12, 88);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(574, 427);
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
            this.laOutputDescription.Size = new System.Drawing.Size(246, 393);
            this.laOutputDescription.TabIndex = 20;
            this.laOutputDescription.Text = "Text will Go Here that explains Paste as Excel";
            // 
            // buttonXOutput
            // 
            this.buttonXOutput.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOutput.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOutput.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXOutput.Location = new System.Drawing.Point(319, 527);
            this.buttonXOutput.Name = "buttonXOutput";
            this.buttonXOutput.Size = new System.Drawing.Size(267, 61);
            this.buttonXOutput.TabIndex = 21;
            this.buttonXOutput.Text = "Send to PowerPoint";
            this.buttonXOutput.TextColor = System.Drawing.Color.Black;
            this.buttonXOutput.Tooltip = "Embed an Excel Grid\r\nto the PowerPoint slide";
            // 
            // labelControlSlidesCount
            // 
            this.labelControlSlidesCount.AllowHtmlString = true;
            this.labelControlSlidesCount.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControlSlidesCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlSlidesCount.Location = new System.Drawing.Point(12, 527);
            this.labelControlSlidesCount.Name = "labelControlSlidesCount";
            this.labelControlSlidesCount.Size = new System.Drawing.Size(293, 61);
            this.labelControlSlidesCount.TabIndex = 22;
            this.labelControlSlidesCount.Text = "Estimated Slides: <b>{0}</b>";
            // 
            // pbOutputLogo
            // 
            this.pbOutputLogo.Image = global::RadioScheduleBuilder.Properties.Resources.OutputLogo;
            this.pbOutputLogo.Location = new System.Drawing.Point(12, 6);
            this.pbOutputLogo.Name = "pbOutputLogo";
            this.pbOutputLogo.Size = new System.Drawing.Size(293, 81);
            this.pbOutputLogo.TabIndex = 23;
            this.pbOutputLogo.TabStop = false;
            // 
            // buttonXGrid
            // 
            this.buttonXGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXGrid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXGrid.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXGrid.Image = global::RadioScheduleBuilder.Properties.Resources.Grid;
            this.buttonXGrid.Location = new System.Drawing.Point(22, 21);
            this.buttonXGrid.Name = "buttonXGrid";
            this.buttonXGrid.Size = new System.Drawing.Size(271, 61);
            this.buttonXGrid.TabIndex = 9;
            this.buttonXGrid.Text = "  Slide Table";
            this.buttonXGrid.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXGrid.TextColor = System.Drawing.Color.Black;
            this.buttonXGrid.Tooltip = "Paste the Schedule as a Microsoft TABLE\r\nto the PowerPoint slide";
            this.buttonXGrid.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
            this.buttonXGrid.Click += new System.EventHandler(this.buttonXOutputType_Click);
            // 
            // buttonXGroupedObjects
            // 
            this.buttonXGroupedObjects.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXGroupedObjects.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXGroupedObjects.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXGroupedObjects.Image = global::RadioScheduleBuilder.Properties.Resources.TextGrouped;
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
            this.buttonXExcel.Image = global::RadioScheduleBuilder.Properties.Resources.Excel;
            this.buttonXExcel.Location = new System.Drawing.Point(22, 356);
            this.buttonXExcel.Name = "buttonXExcel";
            this.buttonXExcel.Size = new System.Drawing.Size(271, 61);
            this.buttonXExcel.TabIndex = 12;
            this.buttonXExcel.Text = "  Excel Grid";
            this.buttonXExcel.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXExcel.TextColor = System.Drawing.Color.Black;
            this.buttonXExcel.Tooltip = "Embed an Excel Grid\r\nto the PowerPoint slide";
            this.buttonXExcel.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
            this.buttonXExcel.Click += new System.EventHandler(this.buttonXOutputType_Click);
            // 
            // buttonXSlideMaster
            // 
            this.buttonXSlideMaster.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSlideMaster.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSlideMaster.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXSlideMaster.Image = global::RadioScheduleBuilder.Properties.Resources.SlideMaster;
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
            this.buttonXImage.Image = global::RadioScheduleBuilder.Properties.Resources.Image;
            this.buttonXImage.Location = new System.Drawing.Point(22, 272);
            this.buttonXImage.Name = "buttonXImage";
            this.buttonXImage.Size = new System.Drawing.Size(271, 61);
            this.buttonXImage.TabIndex = 14;
            this.buttonXImage.Text = "  Single Image";
            this.buttonXImage.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXImage.TextColor = System.Drawing.Color.Black;
            this.buttonXImage.Tooltip = "Paste the Schedule as an IMAGE\r\nto the PowerPoint slide";
            this.buttonXImage.CheckedChanged += new System.EventHandler(this.buttonXOutputType_CheckedChanged);
            this.buttonXImage.Click += new System.EventHandler(this.buttonXOutputType_Click);
            // 
            // FormSelectOutput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(594, 601);
            this.Controls.Add(this.pbOutputLogo);
            this.Controls.Add(this.labelControlSlidesCount);
            this.Controls.Add(this.buttonXOutput);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbOutputLogo)).EndInit();
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
        private DevExpress.XtraEditors.LabelControl labelControlSlidesCount;
        public System.Windows.Forms.Label laOutputTitle;
        public DevComponents.DotNetBar.ButtonX buttonXOutput;
        private System.Windows.Forms.PictureBox pbOutputLogo;
    }
}