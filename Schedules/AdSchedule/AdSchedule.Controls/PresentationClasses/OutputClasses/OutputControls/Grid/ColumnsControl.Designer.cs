namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	sealed partial class ColumnsControl
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
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.buttonXID = new DevComponents.DotNetBar.ButtonX();
			this.buttonXIndex = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXColor = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXPCI = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDimensions = new DevComponents.DotNetBar.ButtonX();
			this.buttonXPercentOfPage = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSection = new DevComponents.DotNetBar.ButtonX();
			this.buttonXPublication = new DevComponents.DotNetBar.ButtonX();
			this.buttonXMechanicals = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDelivery = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDiscounts = new DevComponents.DotNetBar.ButtonX();
			this.buttonXPageSize = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSquare = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDeadline = new DevComponents.DotNetBar.ButtonX();
			this.buttonXReadership = new DevComponents.DotNetBar.ButtonX();
			this.laTitle = new System.Windows.Forms.Label();
			this.pbHelp = new System.Windows.Forms.PictureBox();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbHelp)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Options.UseForeColor = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.ForeColor = System.Drawing.Color.Gray;
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Options.UseForeColor = true;
			// 
			// buttonXID
			// 
			this.buttonXID.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXID.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXID.Location = new System.Drawing.Point(11, 73);
			this.buttonXID.Name = "buttonXID";
			this.buttonXID.Size = new System.Drawing.Size(116, 27);
			this.buttonXID.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXID.TabIndex = 0;
			this.buttonXID.Text = "ID";
			this.buttonXID.TextColor = System.Drawing.Color.Black;
			this.buttonXID.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXID.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXIndex
			// 
			this.buttonXIndex.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXIndex.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXIndex.Location = new System.Drawing.Point(159, 73);
			this.buttonXIndex.Name = "buttonXIndex";
			this.buttonXIndex.Size = new System.Drawing.Size(116, 27);
			this.buttonXIndex.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXIndex.TabIndex = 1;
			this.buttonXIndex.Text = "INS#";
			this.buttonXIndex.TextColor = System.Drawing.Color.Black;
			this.buttonXIndex.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXIndex.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXDate
			// 
			this.buttonXDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDate.Location = new System.Drawing.Point(11, 122);
			this.buttonXDate.Name = "buttonXDate";
			this.buttonXDate.Size = new System.Drawing.Size(116, 27);
			this.buttonXDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDate.TabIndex = 2;
			this.buttonXDate.Text = "Date";
			this.buttonXDate.TextColor = System.Drawing.Color.Black;
			this.buttonXDate.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXDate.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXColor
			// 
			this.buttonXColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXColor.Location = new System.Drawing.Point(159, 122);
			this.buttonXColor.Name = "buttonXColor";
			this.buttonXColor.Size = new System.Drawing.Size(116, 27);
			this.buttonXColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXColor.TabIndex = 3;
			this.buttonXColor.Text = "Color";
			this.buttonXColor.TextColor = System.Drawing.Color.Black;
			this.buttonXColor.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXColor.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXTotalCost
			// 
			this.buttonXTotalCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalCost.Location = new System.Drawing.Point(11, 220);
			this.buttonXTotalCost.Name = "buttonXTotalCost";
			this.buttonXTotalCost.Size = new System.Drawing.Size(116, 27);
			this.buttonXTotalCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalCost.TabIndex = 4;
			this.buttonXTotalCost.Text = "Total Cost";
			this.buttonXTotalCost.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalCost.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXTotalCost.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXPCI
			// 
			this.buttonXPCI.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXPCI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXPCI.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXPCI.Location = new System.Drawing.Point(159, 171);
			this.buttonXPCI.Name = "buttonXPCI";
			this.buttonXPCI.Size = new System.Drawing.Size(116, 27);
			this.buttonXPCI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXPCI.TabIndex = 5;
			this.buttonXPCI.Text = "PCI";
			this.buttonXPCI.TextColor = System.Drawing.Color.Black;
			this.buttonXPCI.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXPCI.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXDimensions
			// 
			this.buttonXDimensions.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDimensions.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDimensions.Location = new System.Drawing.Point(11, 318);
			this.buttonXDimensions.Name = "buttonXDimensions";
			this.buttonXDimensions.Size = new System.Drawing.Size(116, 27);
			this.buttonXDimensions.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDimensions.TabIndex = 6;
			this.buttonXDimensions.Text = "Col x In";
			this.buttonXDimensions.TextColor = System.Drawing.Color.Black;
			this.buttonXDimensions.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXDimensions.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXPercentOfPage
			// 
			this.buttonXPercentOfPage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXPercentOfPage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXPercentOfPage.Location = new System.Drawing.Point(11, 269);
			this.buttonXPercentOfPage.Name = "buttonXPercentOfPage";
			this.buttonXPercentOfPage.Size = new System.Drawing.Size(116, 27);
			this.buttonXPercentOfPage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXPercentOfPage.TabIndex = 7;
			this.buttonXPercentOfPage.Text = "% of Page";
			this.buttonXPercentOfPage.TextColor = System.Drawing.Color.Black;
			this.buttonXPercentOfPage.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXPercentOfPage.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXCost
			// 
			this.buttonXCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCost.Location = new System.Drawing.Point(159, 269);
			this.buttonXCost.Name = "buttonXCost";
			this.buttonXCost.Size = new System.Drawing.Size(116, 27);
			this.buttonXCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCost.TabIndex = 8;
			this.buttonXCost.Text = "BW Ad Cost";
			this.buttonXCost.TextColor = System.Drawing.Color.Black;
			this.buttonXCost.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXCost.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXSection
			// 
			this.buttonXSection.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSection.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSection.Location = new System.Drawing.Point(11, 171);
			this.buttonXSection.Name = "buttonXSection";
			this.buttonXSection.Size = new System.Drawing.Size(116, 27);
			this.buttonXSection.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSection.TabIndex = 9;
			this.buttonXSection.Text = "Section";
			this.buttonXSection.TextColor = System.Drawing.Color.Black;
			this.buttonXSection.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXSection.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXPublication
			// 
			this.buttonXPublication.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXPublication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXPublication.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXPublication.Location = new System.Drawing.Point(159, 220);
			this.buttonXPublication.Name = "buttonXPublication";
			this.buttonXPublication.Size = new System.Drawing.Size(116, 27);
			this.buttonXPublication.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXPublication.TabIndex = 10;
			this.buttonXPublication.Text = "Publication";
			this.buttonXPublication.TextColor = System.Drawing.Color.Black;
			this.buttonXPublication.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXPublication.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXMechanicals
			// 
			this.buttonXMechanicals.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXMechanicals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXMechanicals.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXMechanicals.Location = new System.Drawing.Point(159, 318);
			this.buttonXMechanicals.Name = "buttonXMechanicals";
			this.buttonXMechanicals.Size = new System.Drawing.Size(116, 27);
			this.buttonXMechanicals.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXMechanicals.TabIndex = 11;
			this.buttonXMechanicals.Text = "Mechanicals";
			this.buttonXMechanicals.TextColor = System.Drawing.Color.Black;
			this.buttonXMechanicals.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXMechanicals.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXDelivery
			// 
			this.buttonXDelivery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDelivery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDelivery.Location = new System.Drawing.Point(11, 367);
			this.buttonXDelivery.Name = "buttonXDelivery";
			this.buttonXDelivery.Size = new System.Drawing.Size(116, 27);
			this.buttonXDelivery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDelivery.TabIndex = 12;
			this.buttonXDelivery.Text = "Delivery";
			this.buttonXDelivery.TextColor = System.Drawing.Color.Black;
			this.buttonXDelivery.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXDelivery.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXDiscounts
			// 
			this.buttonXDiscounts.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDiscounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDiscounts.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDiscounts.Location = new System.Drawing.Point(159, 367);
			this.buttonXDiscounts.Name = "buttonXDiscounts";
			this.buttonXDiscounts.Size = new System.Drawing.Size(116, 27);
			this.buttonXDiscounts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDiscounts.TabIndex = 13;
			this.buttonXDiscounts.Text = "Discounts";
			this.buttonXDiscounts.TextColor = System.Drawing.Color.Black;
			this.buttonXDiscounts.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXDiscounts.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXPageSize
			// 
			this.buttonXPageSize.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXPageSize.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXPageSize.Location = new System.Drawing.Point(11, 416);
			this.buttonXPageSize.Name = "buttonXPageSize";
			this.buttonXPageSize.Size = new System.Drawing.Size(116, 27);
			this.buttonXPageSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXPageSize.TabIndex = 14;
			this.buttonXPageSize.Text = "Page Size";
			this.buttonXPageSize.TextColor = System.Drawing.Color.Black;
			this.buttonXPageSize.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXPageSize.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXSquare
			// 
			this.buttonXSquare.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSquare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSquare.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSquare.Location = new System.Drawing.Point(159, 416);
			this.buttonXSquare.Name = "buttonXSquare";
			this.buttonXSquare.Size = new System.Drawing.Size(116, 27);
			this.buttonXSquare.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSquare.TabIndex = 15;
			this.buttonXSquare.Text = "Total Col. In.";
			this.buttonXSquare.TextColor = System.Drawing.Color.Black;
			this.buttonXSquare.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXSquare.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXDeadline
			// 
			this.buttonXDeadline.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDeadline.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDeadline.Location = new System.Drawing.Point(11, 465);
			this.buttonXDeadline.Name = "buttonXDeadline";
			this.buttonXDeadline.Size = new System.Drawing.Size(116, 27);
			this.buttonXDeadline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDeadline.TabIndex = 16;
			this.buttonXDeadline.Text = "Deadline";
			this.buttonXDeadline.TextColor = System.Drawing.Color.Black;
			this.buttonXDeadline.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXDeadline.Click += new System.EventHandler(this.button_Click);
			// 
			// buttonXReadership
			// 
			this.buttonXReadership.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReadership.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReadership.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReadership.Location = new System.Drawing.Point(159, 465);
			this.buttonXReadership.Name = "buttonXReadership";
			this.buttonXReadership.Size = new System.Drawing.Size(116, 27);
			this.buttonXReadership.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReadership.TabIndex = 17;
			this.buttonXReadership.Text = "Readership";
			this.buttonXReadership.TextColor = System.Drawing.Color.Black;
			this.buttonXReadership.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			this.buttonXReadership.Click += new System.EventHandler(this.button_Click);
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(7, 3);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(231, 40);
			this.laTitle.TabIndex = 18;
			this.laTitle.Text = "Grid Column Options:";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbHelp
			// 
			this.pbHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbHelp.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.HelpSmall;
			this.pbHelp.Location = new System.Drawing.Point(244, 5);
			this.pbHelp.Name = "pbHelp";
			this.pbHelp.Size = new System.Drawing.Size(30, 30);
			this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.superTooltip.SetSuperTooltip(this.pbHelp, new DevComponents.DotNetBar.SuperTooltipInfo("Grid Columns Help", "", "Learn more about how you can customize the columns in your Schedule grid", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.pbHelp.TabIndex = 36;
			this.pbHelp.TabStop = false;
			this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
			this.pbHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// superTooltip
			// 
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// ColumnsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pbHelp);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.buttonXReadership);
			this.Controls.Add(this.buttonXDeadline);
			this.Controls.Add(this.buttonXSquare);
			this.Controls.Add(this.buttonXPageSize);
			this.Controls.Add(this.buttonXDiscounts);
			this.Controls.Add(this.buttonXDelivery);
			this.Controls.Add(this.buttonXMechanicals);
			this.Controls.Add(this.buttonXPublication);
			this.Controls.Add(this.buttonXSection);
			this.Controls.Add(this.buttonXCost);
			this.Controls.Add(this.buttonXPercentOfPage);
			this.Controls.Add(this.buttonXDimensions);
			this.Controls.Add(this.buttonXPCI);
			this.Controls.Add(this.buttonXTotalCost);
			this.Controls.Add(this.buttonXColor);
			this.Controls.Add(this.buttonXDate);
			this.Controls.Add(this.buttonXIndex);
			this.Controls.Add(this.buttonXID);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ColumnsControl";
			this.Size = new System.Drawing.Size(282, 531);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbHelp)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevComponents.DotNetBar.ButtonX buttonXID;
        private DevComponents.DotNetBar.ButtonX buttonXIndex;
        private DevComponents.DotNetBar.ButtonX buttonXDate;
        private DevComponents.DotNetBar.ButtonX buttonXColor;
        private DevComponents.DotNetBar.ButtonX buttonXTotalCost;
        private DevComponents.DotNetBar.ButtonX buttonXPCI;
        private DevComponents.DotNetBar.ButtonX buttonXDimensions;
        private DevComponents.DotNetBar.ButtonX buttonXPercentOfPage;
        private DevComponents.DotNetBar.ButtonX buttonXCost;
        private DevComponents.DotNetBar.ButtonX buttonXSection;
        private DevComponents.DotNetBar.ButtonX buttonXPublication;
        private DevComponents.DotNetBar.ButtonX buttonXMechanicals;
        private DevComponents.DotNetBar.ButtonX buttonXDelivery;
        private DevComponents.DotNetBar.ButtonX buttonXDiscounts;
        private DevComponents.DotNetBar.ButtonX buttonXPageSize;
        private DevComponents.DotNetBar.ButtonX buttonXSquare;
        private DevComponents.DotNetBar.ButtonX buttonXDeadline;
        private DevComponents.DotNetBar.ButtonX buttonXReadership;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbHelp;
        private DevComponents.DotNetBar.SuperTooltip superTooltip;
    }
}
