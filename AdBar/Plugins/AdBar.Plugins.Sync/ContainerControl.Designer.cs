namespace AdBar.Plugins.Sync
{
    partial class ContainerControl
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
			this.ribbonBar = new DevComponents.DotNetBar.RibbonBar();
			this.timeEditSyncTime = new DevExpress.XtraEditors.TimeEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.itemContainerSyncSettings = new DevComponents.DotNetBar.ItemContainer();
			this.switchButtonItemSyncHourly = new DevComponents.DotNetBar.SwitchButtonItem();
			this.itemContainerSyncTime = new DevComponents.DotNetBar.ItemContainer();
			this.controlContainerItemSyncTime = new DevComponents.DotNetBar.ControlContainerItem();
			this.buttonItemSaveSyncTime = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainerSyncInfo = new DevComponents.DotNetBar.ItemContainer();
			this.itemContainerSyncStatus1 = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemLastSyncTitle = new DevComponents.DotNetBar.LabelItem();
			this.labelItemLastSyncValue = new DevComponents.DotNetBar.LabelItem();
			this.itemContainerSyncStatus2 = new DevComponents.DotNetBar.ItemContainer();
			this.labelItemNextSyncTitle = new DevComponents.DotNetBar.LabelItem();
			this.labelItemNextSyncValue = new DevComponents.DotNetBar.LabelItem();
			this.ribbonBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.timeEditSyncTime.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// ribbonBar
			// 
			this.ribbonBar.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBar.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBar.ContainerControlProcessDialogKey = true;
			this.ribbonBar.Controls.Add(this.timeEditSyncTime);
			this.ribbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerSyncSettings,
            this.itemContainerSyncInfo});
			this.ribbonBar.ItemSpacing = 10;
			this.ribbonBar.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBar.Location = new System.Drawing.Point(0, 0);
			this.ribbonBar.Name = "ribbonBar";
			this.ribbonBar.ResizeItemsToFit = false;
			this.ribbonBar.Size = new System.Drawing.Size(292, 150);
			this.ribbonBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBar.TabIndex = 0;
			this.ribbonBar.Text = "Sync Settings";
			// 
			// 
			// 
			this.ribbonBar.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.ribbonBar.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBar.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			this.ribbonBar.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// timeEditSyncTime
			// 
			this.timeEditSyncTime.EditValue = new System.DateTime(2013, 11, 5, 0, 0, 0, 0);
			this.timeEditSyncTime.Location = new System.Drawing.Point(4, 96);
			this.timeEditSyncTime.Name = "timeEditSyncTime";
			this.timeEditSyncTime.Properties.Appearance.Options.UseTextOptions = true;
			this.timeEditSyncTime.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.timeEditSyncTime.Properties.AppearanceDisabled.Options.UseTextOptions = true;
			this.timeEditSyncTime.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.timeEditSyncTime.Properties.AppearanceFocused.Options.UseTextOptions = true;
			this.timeEditSyncTime.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.timeEditSyncTime.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
			this.timeEditSyncTime.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.timeEditSyncTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.timeEditSyncTime.Properties.EditValueChangedDelay = 1000;
			this.timeEditSyncTime.Properties.Mask.EditMask = "t";
			this.timeEditSyncTime.Size = new System.Drawing.Size(120, 22);
			this.timeEditSyncTime.StyleController = this.styleController;
			this.timeEditSyncTime.TabIndex = 1;
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// itemContainerSyncSettings
			// 
			// 
			// 
			// 
			this.itemContainerSyncSettings.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncSettings.BeginGroup = true;
			this.itemContainerSyncSettings.ItemSpacing = 37;
			this.itemContainerSyncSettings.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerSyncSettings.Name = "itemContainerSyncSettings";
			this.itemContainerSyncSettings.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.switchButtonItemSyncHourly,
            this.itemContainerSyncTime});
			// 
			// 
			// 
			this.itemContainerSyncSettings.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncSettings.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// switchButtonItemSyncHourly
			// 
			this.switchButtonItemSyncHourly.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.switchButtonItemSyncHourly.ButtonHeight = 40;
			this.switchButtonItemSyncHourly.ButtonWidth = 160;
			this.switchButtonItemSyncHourly.Name = "switchButtonItemSyncHourly";
			this.switchButtonItemSyncHourly.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
			this.switchButtonItemSyncHourly.OffText = "Sync by Time";
			this.switchButtonItemSyncHourly.OnText = "Sync Hourly";
			this.switchButtonItemSyncHourly.SwitchBackColor = System.Drawing.Color.Gainsboro;
			this.switchButtonItemSyncHourly.SwitchBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.switchButtonItemSyncHourly.SwitchWidth = 80;
			this.switchButtonItemSyncHourly.Text = "Sync Hourly";
			this.switchButtonItemSyncHourly.TextVisible = false;
			this.switchButtonItemSyncHourly.ValueChanged += new System.EventHandler(this.switchButtonItemSyncHourly_ValueChanged);
			// 
			// itemContainerSyncTime
			// 
			// 
			// 
			// 
			this.itemContainerSyncTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncTime.ItemSpacing = 0;
			this.itemContainerSyncTime.Name = "itemContainerSyncTime";
			this.itemContainerSyncTime.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItemSyncTime,
            this.buttonItemSaveSyncTime});
			// 
			// 
			// 
			this.itemContainerSyncTime.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// controlContainerItemSyncTime
			// 
			this.controlContainerItemSyncTime.AllowItemResize = false;
			this.controlContainerItemSyncTime.Control = this.timeEditSyncTime;
			this.controlContainerItemSyncTime.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
			this.controlContainerItemSyncTime.Name = "controlContainerItemSyncTime";
			// 
			// buttonItemSaveSyncTime
			// 
			this.buttonItemSaveSyncTime.Image = global::AdBar.Plugins.Sync.Properties.Resources.Save;
			this.buttonItemSaveSyncTime.Name = "buttonItemSaveSyncTime";
			this.buttonItemSaveSyncTime.Text = "buttonItemSaveSyncTime";
			this.buttonItemSaveSyncTime.Click += new System.EventHandler(this.buttonItemSaveSyncTime_Click);
			// 
			// itemContainerSyncInfo
			// 
			// 
			// 
			// 
			this.itemContainerSyncInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncInfo.BeginGroup = true;
			this.itemContainerSyncInfo.ItemSpacing = 30;
			this.itemContainerSyncInfo.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerSyncInfo.Name = "itemContainerSyncInfo";
			this.itemContainerSyncInfo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerSyncStatus1,
            this.itemContainerSyncStatus2});
			// 
			// 
			// 
			this.itemContainerSyncInfo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncInfo.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// itemContainerSyncStatus1
			// 
			// 
			// 
			// 
			this.itemContainerSyncStatus1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncStatus1.ItemSpacing = 10;
			this.itemContainerSyncStatus1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerSyncStatus1.Name = "itemContainerSyncStatus1";
			this.itemContainerSyncStatus1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemLastSyncTitle,
            this.labelItemLastSyncValue});
			// 
			// 
			// 
			this.itemContainerSyncStatus1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncStatus1.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// labelItemLastSyncTitle
			// 
			this.labelItemLastSyncTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelItemLastSyncTitle.ForeColor = System.Drawing.Color.Black;
			this.labelItemLastSyncTitle.Name = "labelItemLastSyncTitle";
			this.labelItemLastSyncTitle.Text = " Last Sync:";
			this.labelItemLastSyncTitle.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// labelItemLastSyncValue
			// 
			this.labelItemLastSyncValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelItemLastSyncValue.ForeColor = System.Drawing.Color.Black;
			this.labelItemLastSyncValue.Name = "labelItemLastSyncValue";
			this.labelItemLastSyncValue.Text = " 5/21/11   8:30 AM";
			this.labelItemLastSyncValue.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// itemContainerSyncStatus2
			// 
			// 
			// 
			// 
			this.itemContainerSyncStatus2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncStatus2.ItemSpacing = 10;
			this.itemContainerSyncStatus2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerSyncStatus2.Name = "itemContainerSyncStatus2";
			this.itemContainerSyncStatus2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItemNextSyncTitle,
            this.labelItemNextSyncValue});
			// 
			// 
			// 
			this.itemContainerSyncStatus2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerSyncStatus2.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// labelItemNextSyncTitle
			// 
			this.labelItemNextSyncTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelItemNextSyncTitle.ForeColor = System.Drawing.Color.Black;
			this.labelItemNextSyncTitle.Name = "labelItemNextSyncTitle";
			this.labelItemNextSyncTitle.Text = " Next Sync:";
			this.labelItemNextSyncTitle.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// labelItemNextSyncValue
			// 
			this.labelItemNextSyncValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelItemNextSyncValue.ForeColor = System.Drawing.Color.Black;
			this.labelItemNextSyncValue.Name = "labelItemNextSyncValue";
			this.labelItemNextSyncValue.Text = " 5/22/11   8:30 AM";
			this.labelItemNextSyncValue.Click += new System.EventHandler(this.OutOfTimer_Click);
			// 
			// ContainerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ribbonBar);
			this.Name = "ContainerControl";
			this.Size = new System.Drawing.Size(302, 150);
			this.ribbonBar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.timeEditSyncTime.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		internal DevComponents.DotNetBar.RibbonBar ribbonBar;
		private DevComponents.DotNetBar.ItemContainer itemContainerSyncInfo;
		private DevComponents.DotNetBar.ItemContainer itemContainerSyncStatus1;
		private DevComponents.DotNetBar.LabelItem labelItemLastSyncTitle;
		private DevComponents.DotNetBar.LabelItem labelItemLastSyncValue;
		private DevComponents.DotNetBar.ItemContainer itemContainerSyncStatus2;
		private DevComponents.DotNetBar.LabelItem labelItemNextSyncTitle;
		private DevComponents.DotNetBar.LabelItem labelItemNextSyncValue;
		private DevComponents.DotNetBar.ItemContainer itemContainerSyncSettings;
		private DevExpress.XtraEditors.TimeEdit timeEditSyncTime;
		private DevComponents.DotNetBar.SwitchButtonItem switchButtonItemSyncHourly;
		private DevComponents.DotNetBar.ItemContainer itemContainerSyncTime;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemSyncTime;
		private DevComponents.DotNetBar.ButtonItem buttonItemSaveSyncTime;
		private DevExpress.XtraEditors.StyleController styleController;
    }
}
