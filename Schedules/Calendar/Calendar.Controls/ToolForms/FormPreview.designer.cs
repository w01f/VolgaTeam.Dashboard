namespace CalendarBuilder.ToolForms
{
    partial class FormPreview
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreview));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barOperations = new DevExpress.XtraBars.Bar();
            this.barLargeButtonItemOutput = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItemHelp = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItemExit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnNavigationArea = new System.Windows.Forms.Panel();
            this.laSlideSize = new System.Windows.Forms.Label();
            this.laSlideNumber = new System.Windows.Forms.Label();
            this.comboBoxEditSlides = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.pnNavigationArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlides.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowItemAnimatedHighlighting = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barOperations});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemOutput,
            this.barLargeButtonItemHelp,
            this.barLargeButtonItemExit});
            this.barManager.MaxItemId = 15;
            // 
            // barOperations
            // 
            this.barOperations.BarName = "Tools";
            this.barOperations.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.barOperations.DockCol = 0;
            this.barOperations.DockRow = 0;
            this.barOperations.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barOperations.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemOutput, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemHelp, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemExit, DevExpress.XtraBars.BarItemPaintStyle.Standard)});
            this.barOperations.OptionsBar.AllowQuickCustomization = false;
            this.barOperations.OptionsBar.DisableClose = true;
            this.barOperations.OptionsBar.DisableCustomization = true;
            this.barOperations.OptionsBar.DrawDragBorder = false;
            this.barOperations.OptionsBar.UseWholeRow = true;
            this.barOperations.Text = "Tools";
            // 
            // barLargeButtonItemOutput
            // 
            this.barLargeButtonItemOutput.Appearance.ForeColor = System.Drawing.Color.Black;
            this.barLargeButtonItemOutput.Appearance.Options.UseForeColor = true;
            this.barLargeButtonItemOutput.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.barLargeButtonItemOutput.Caption = "Send to\r\nPowerPoint";
            this.barLargeButtonItemOutput.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barLargeButtonItemOutput.Glyph = global::CalendarBuilder.Properties.Resources.PowerPoint;
            this.barLargeButtonItemOutput.Id = 4;
            this.barLargeButtonItemOutput.Name = "barLargeButtonItemOutput";
            toolTipTitleItem1.Text = "Send to PowerPoint";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Send this Calendar to PowerPoint";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.barLargeButtonItemOutput.SuperTip = superToolTip1;
            this.barLargeButtonItemOutput.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemOutput_ItemClick);
            // 
            // barLargeButtonItemHelp
            // 
            this.barLargeButtonItemHelp.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.barLargeButtonItemHelp.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barLargeButtonItemHelp.Glyph = global::CalendarBuilder.Properties.Resources.Help;
            this.barLargeButtonItemHelp.Id = 6;
            this.barLargeButtonItemHelp.Name = "barLargeButtonItemHelp";
            toolTipTitleItem2.Text = "HELP";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Learn more about how to preview your calendar";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.barLargeButtonItemHelp.SuperTip = superToolTip2;
            this.barLargeButtonItemHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemHelp_ItemClick);
            // 
            // barLargeButtonItemExit
            // 
            this.barLargeButtonItemExit.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.barLargeButtonItemExit.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.barLargeButtonItemExit.Glyph = global::CalendarBuilder.Properties.Resources.Exit;
            this.barLargeButtonItemExit.Id = 7;
            this.barLargeButtonItemExit.Name = "barLargeButtonItemExit";
            toolTipTitleItem3.Text = "EXIT";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Close this Window";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.barLargeButtonItemExit.SuperTip = superToolTip3;
            this.barLargeButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemExit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(934, 86);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 562);
            this.barDockControlBottom.Size = new System.Drawing.Size(934, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 86);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 476);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(934, 86);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 476);
            // 
            // pnNavigationArea
            // 
            this.pnNavigationArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnNavigationArea.Controls.Add(this.laSlideSize);
            this.pnNavigationArea.Controls.Add(this.laSlideNumber);
            this.pnNavigationArea.Controls.Add(this.comboBoxEditSlides);
            this.pnNavigationArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnNavigationArea.Location = new System.Drawing.Point(0, 495);
            this.pnNavigationArea.Name = "pnNavigationArea";
            this.pnNavigationArea.Size = new System.Drawing.Size(934, 67);
            this.pnNavigationArea.TabIndex = 4;
            // 
            // laSlideSize
            // 
            this.laSlideSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laSlideSize.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSlideSize.Location = new System.Drawing.Point(687, 0);
            this.laSlideSize.Name = "laSlideSize";
            this.laSlideSize.Size = new System.Drawing.Size(240, 32);
            this.laSlideSize.TabIndex = 8;
            this.laSlideSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laSlideNumber
            // 
            this.laSlideNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.laSlideNumber.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSlideNumber.Location = new System.Drawing.Point(690, 32);
            this.laSlideNumber.Name = "laSlideNumber";
            this.laSlideNumber.Size = new System.Drawing.Size(237, 31);
            this.laSlideNumber.TabIndex = 6;
            this.laSlideNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEditSlides
            // 
            this.comboBoxEditSlides.EditValue = "";
            this.comboBoxEditSlides.Location = new System.Drawing.Point(373, 4);
            this.comboBoxEditSlides.MenuManager = this.barManager;
            this.comboBoxEditSlides.Name = "comboBoxEditSlides";
            this.comboBoxEditSlides.Properties.Appearance.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEditSlides.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditSlides.Properties.Appearance.Options.UseTextOptions = true;
            this.comboBoxEditSlides.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.comboBoxEditSlides.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEditSlides.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditSlides.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.comboBoxEditSlides.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.comboBoxEditSlides.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("comboBoxEditSlides.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("comboBoxEditSlides.Properties.Buttons1"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.comboBoxEditSlides.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditSlides.Size = new System.Drawing.Size(184, 54);
            this.comboBoxEditSlides.TabIndex = 5;
            this.comboBoxEditSlides.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditSlides_SelectedIndexChanged);
            this.comboBoxEditSlides.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.comboBoxEditSlides_ButtonClick);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(0, 86);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(934, 409);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 5;
            this.pictureBoxPreview.TabStop = false;
            // 
            // FormPreview
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.pnNavigationArea);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormQuickView_FormClosed);
            this.Shown += new System.EventHandler(this.FormQuickView_Shown);
            this.Resize += new System.EventHandler(this.FormQuickView_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.pnNavigationArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlides.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barOperations;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemOutput;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemHelp;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemExit;
        private System.Windows.Forms.Panel pnNavigationArea;
        private System.Windows.Forms.Label laSlideSize;
        private System.Windows.Forms.Label laSlideNumber;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlides;
        private System.Windows.Forms.PictureBox pictureBoxPreview;

    }
}