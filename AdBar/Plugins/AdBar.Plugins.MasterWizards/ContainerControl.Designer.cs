namespace AdBar.Plugins.MasterWizards
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
            this.comboBoxEditStyle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.itemContainerMain = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainerStyle = new DevComponents.DotNetBar.ItemContainer();
            this.controlContainerItemStyle = new DevComponents.DotNetBar.ControlContainerItem();
            this.itemContainerSize = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainerSizeButtons1 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItemSize1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemSize4 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainerSizeButtons2 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItemSize2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemSize5 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainerSizeButtons3 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItemSize3 = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStyle.Properties)).BeginInit();
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
            this.ribbonBar.Controls.Add(this.comboBoxEditStyle);
            this.ribbonBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerMain});
            this.ribbonBar.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBar.Location = new System.Drawing.Point(0, 0);
            this.ribbonBar.Name = "ribbonBar";
            this.ribbonBar.Size = new System.Drawing.Size(213, 150);
            this.ribbonBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBar.TabIndex = 0;
            this.ribbonBar.Text = "Default Slide Template";
            // 
            // 
            // 
            this.ribbonBar.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // comboBoxEditStyle
            // 
            this.comboBoxEditStyle.Location = new System.Drawing.Point(4, 25);
            this.comboBoxEditStyle.Name = "comboBoxEditStyle";
            this.comboBoxEditStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditStyle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditStyle.Size = new System.Drawing.Size(200, 22);
            this.comboBoxEditStyle.StyleController = this.styleController;
            this.comboBoxEditStyle.TabIndex = 8;
            this.comboBoxEditStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditStyle_SelectedIndexChanged);
            this.comboBoxEditStyle.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.comboBoxEditStyle_EditValueChanging);
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
            // itemContainerMain
            // 
            // 
            // 
            // 
            this.itemContainerMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerMain.ItemSpacing = 20;
            this.itemContainerMain.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerMain.Name = "itemContainerMain";
            this.itemContainerMain.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerStyle,
            this.itemContainerSize});
            // 
            // 
            // 
            this.itemContainerMain.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // itemContainerStyle
            // 
            // 
            // 
            // 
            this.itemContainerStyle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerStyle.Name = "itemContainerStyle";
            this.itemContainerStyle.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItemStyle});
            // 
            // 
            // 
            this.itemContainerStyle.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // controlContainerItemStyle
            // 
            this.controlContainerItemStyle.AllowItemResize = false;
            this.controlContainerItemStyle.Control = this.comboBoxEditStyle;
            this.controlContainerItemStyle.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItemStyle.Name = "controlContainerItemStyle";
            // 
            // itemContainerSize
            // 
            // 
            // 
            // 
            this.itemContainerSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerSize.ItemSpacing = 50;
            this.itemContainerSize.Name = "itemContainerSize";
            this.itemContainerSize.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerSizeButtons1,
            this.itemContainerSizeButtons2,
            this.itemContainerSizeButtons3});
            // 
            // 
            // 
            this.itemContainerSize.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // itemContainerSizeButtons1
            // 
            // 
            // 
            // 
            this.itemContainerSizeButtons1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerSizeButtons1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerSizeButtons1.Name = "itemContainerSizeButtons1";
            this.itemContainerSizeButtons1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSize1,
            this.buttonItemSize4});
            // 
            // 
            // 
            this.itemContainerSizeButtons1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // buttonItemSize1
            // 
            this.buttonItemSize1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.buttonItemSize1.Enabled = false;
            this.buttonItemSize1.Name = "buttonItemSize1";
            this.buttonItemSize1.Text = " 4 x 3";
            this.buttonItemSize1.Tooltip = "Landscape";
            this.buttonItemSize1.CheckedChanged += new System.EventHandler(this.buttonItemSize_CheckedChanged);
            this.buttonItemSize1.Click += new System.EventHandler(this.buttonItemSize_Click);
            // 
            // buttonItemSize4
            // 
            this.buttonItemSize4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.buttonItemSize4.Enabled = false;
            this.buttonItemSize4.Name = "buttonItemSize4";
            this.buttonItemSize4.Text = " 3 x 4";
            this.buttonItemSize4.Tooltip = "Portrait";
            this.buttonItemSize4.CheckedChanged += new System.EventHandler(this.buttonItemSize_CheckedChanged);
            this.buttonItemSize4.Click += new System.EventHandler(this.buttonItemSize_Click);
            // 
            // itemContainerSizeButtons2
            // 
            // 
            // 
            // 
            this.itemContainerSizeButtons2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerSizeButtons2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerSizeButtons2.Name = "itemContainerSizeButtons2";
            this.itemContainerSizeButtons2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSize2,
            this.buttonItemSize5});
            // 
            // 
            // 
            this.itemContainerSizeButtons2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // buttonItemSize2
            // 
            this.buttonItemSize2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.buttonItemSize2.Enabled = false;
            this.buttonItemSize2.Name = "buttonItemSize2";
            this.buttonItemSize2.Text = " 5 x 4";
            this.buttonItemSize2.Tooltip = "Landscape Extended";
            this.buttonItemSize2.CheckedChanged += new System.EventHandler(this.buttonItemSize_CheckedChanged);
            this.buttonItemSize2.Click += new System.EventHandler(this.buttonItemSize_Click);
            // 
            // buttonItemSize5
            // 
            this.buttonItemSize5.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.buttonItemSize5.Enabled = false;
            this.buttonItemSize5.Name = "buttonItemSize5";
            this.buttonItemSize5.Text = " 4 x 5";
            this.buttonItemSize5.Tooltip = "Portrait  Extended";
            this.buttonItemSize5.CheckedChanged += new System.EventHandler(this.buttonItemSize_CheckedChanged);
            this.buttonItemSize5.Click += new System.EventHandler(this.buttonItemSize_Click);
            // 
            // itemContainerSizeButtons3
            // 
            // 
            // 
            // 
            this.itemContainerSizeButtons3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerSizeButtons3.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerSizeButtons3.Name = "itemContainerSizeButtons3";
            this.itemContainerSizeButtons3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemSize3});
            // 
            // 
            // 
            this.itemContainerSizeButtons3.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // buttonItemSize3
            // 
            this.buttonItemSize3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.buttonItemSize3.Enabled = false;
            this.buttonItemSize3.Name = "buttonItemSize3";
            this.buttonItemSize3.Text = "16 x 9";
            this.buttonItemSize3.Tooltip = "HD Wide Screen";
            this.buttonItemSize3.CheckedChanged += new System.EventHandler(this.buttonItemSize_CheckedChanged);
            this.buttonItemSize3.Click += new System.EventHandler(this.buttonItemSize_Click);
            // 
            // ContainerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ribbonBar);
            this.Name = "ContainerControl";
            this.Size = new System.Drawing.Size(217, 150);
            this.ribbonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		internal DevComponents.DotNetBar.RibbonBar ribbonBar;
		private DevComponents.DotNetBar.ItemContainer itemContainerMain;
		private DevComponents.DotNetBar.ItemContainer itemContainerStyle;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditStyle;
		private DevComponents.DotNetBar.ControlContainerItem controlContainerItemStyle;
		private DevComponents.DotNetBar.ItemContainer itemContainerSize;
		private DevComponents.DotNetBar.ItemContainer itemContainerSizeButtons1;
		private DevComponents.DotNetBar.ButtonItem buttonItemSize1;
		private DevComponents.DotNetBar.ButtonItem buttonItemSize4;
		private DevComponents.DotNetBar.ItemContainer itemContainerSizeButtons2;
		private DevComponents.DotNetBar.ButtonItem buttonItemSize2;
		private DevComponents.DotNetBar.ButtonItem buttonItemSize5;
		private DevComponents.DotNetBar.ItemContainer itemContainerSizeButtons3;
		private DevComponents.DotNetBar.ButtonItem buttonItemSize3;
	}
}
