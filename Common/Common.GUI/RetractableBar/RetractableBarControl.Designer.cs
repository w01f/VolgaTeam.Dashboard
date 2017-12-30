namespace Asa.Common.GUI.RetractableBar
{
	partial class RetractableBarControl
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
			this.pnClosed = new DevExpress.XtraEditors.PanelControl();
			this.layoutControlClosed = new DevExpress.XtraLayout.LayoutControl();
			this.simpleButtonExpand = new DevExpress.XtraEditors.SimpleButton();
			this.pnAdditionalButtons = new System.Windows.Forms.Panel();
			this.layoutControlGroupClosedRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemAdditionalButtons = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemButtonExpand = new DevExpress.XtraLayout.LayoutControlItem();
			this.pnOpened = new DevExpress.XtraEditors.PanelControl();
			this.layoutControlOpened = new DevExpress.XtraLayout.LayoutControl();
			this.simpleButtonCollapse = new DevExpress.XtraEditors.SimpleButton();
			this.pnHeaderContent = new System.Windows.Forms.Panel();
			this.pnContent = new System.Windows.Forms.Panel();
			this.layoutControlGroupOpenedRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemContent = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemHeaderContent = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItemButtonCollpase = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).BeginInit();
			this.pnClosed.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlClosed)).BeginInit();
			this.layoutControlClosed.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupClosedRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdditionalButtons)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemButtonExpand)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).BeginInit();
			this.pnOpened.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlOpened)).BeginInit();
			this.layoutControlOpened.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupOpenedRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHeaderContent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemButtonCollpase)).BeginInit();
			this.SuspendLayout();
			// 
			// pnClosed
			// 
			this.pnClosed.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnClosed.Controls.Add(this.layoutControlClosed);
			this.pnClosed.Location = new System.Drawing.Point(6, 3);
			this.pnClosed.Name = "pnClosed";
			this.pnClosed.Size = new System.Drawing.Size(58, 151);
			this.pnClosed.TabIndex = 0;
			this.pnClosed.Visible = false;
			// 
			// layoutControlClosed
			// 
			this.layoutControlClosed.AllowCustomization = false;
			this.layoutControlClosed.Controls.Add(this.simpleButtonExpand);
			this.layoutControlClosed.Controls.Add(this.pnAdditionalButtons);
			this.layoutControlClosed.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControlClosed.Location = new System.Drawing.Point(2, 2);
			this.layoutControlClosed.Name = "layoutControlClosed";
			this.layoutControlClosed.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(372, 273, 450, 400);
			this.layoutControlClosed.OptionsView.ShareLookAndFeelWithChildren = false;
			this.layoutControlClosed.Root = this.layoutControlGroupClosedRoot;
			this.layoutControlClosed.Size = new System.Drawing.Size(54, 147);
			this.layoutControlClosed.TabIndex = 0;
			this.layoutControlClosed.Text = "layoutControl1";
			// 
			// simpleButtonExpand
			// 
			this.simpleButtonExpand.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonExpand.Image = global::Asa.Common.GUI.Properties.Resources.RetractableBarExpand;
			this.simpleButtonExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.simpleButtonExpand.Location = new System.Drawing.Point(3, 3);
			this.simpleButtonExpand.Name = "simpleButtonExpand";
			this.simpleButtonExpand.Size = new System.Drawing.Size(47, 38);
			this.simpleButtonExpand.TabIndex = 0;
			this.simpleButtonExpand.ToolTip = "Expand bar";
			this.simpleButtonExpand.Click += new System.EventHandler(this.simpleButtonExpand_Click);
			// 
			// pnAdditionalButtons
			// 
			this.pnAdditionalButtons.Location = new System.Drawing.Point(3, 42);
			this.pnAdditionalButtons.Name = "pnAdditionalButtons";
			this.pnAdditionalButtons.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.pnAdditionalButtons.Size = new System.Drawing.Size(47, 103);
			this.pnAdditionalButtons.TabIndex = 1;
			this.pnAdditionalButtons.Resize += new System.EventHandler(this.pnAdditionalButtons_Resize);
			// 
			// layoutControlGroupClosedRoot
			// 
			this.layoutControlGroupClosedRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupClosedRoot.GroupBordersVisible = false;
			this.layoutControlGroupClosedRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemAdditionalButtons,
            this.layoutControlItemButtonExpand});
			this.layoutControlGroupClosedRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupClosedRoot.Name = "Root";
			this.layoutControlGroupClosedRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.layoutControlGroupClosedRoot.Size = new System.Drawing.Size(54, 147);
			this.layoutControlGroupClosedRoot.TextVisible = false;
			// 
			// layoutControlItemAdditionalButtons
			// 
			this.layoutControlItemAdditionalButtons.Control = this.pnAdditionalButtons;
			this.layoutControlItemAdditionalButtons.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemAdditionalButtons.FillControlToClientArea = false;
			this.layoutControlItemAdditionalButtons.Location = new System.Drawing.Point(0, 40);
			this.layoutControlItemAdditionalButtons.MaxSize = new System.Drawing.Size(47, 0);
			this.layoutControlItemAdditionalButtons.MinSize = new System.Drawing.Size(47, 1);
			this.layoutControlItemAdditionalButtons.Name = "layoutControlItemAdditionalButtons";
			this.layoutControlItemAdditionalButtons.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemAdditionalButtons.Size = new System.Drawing.Size(50, 103);
			this.layoutControlItemAdditionalButtons.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemAdditionalButtons.Text = "Additional Buttons";
			this.layoutControlItemAdditionalButtons.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemAdditionalButtons.TextVisible = false;
			this.layoutControlItemAdditionalButtons.TrimClientAreaToControl = false;
			// 
			// layoutControlItemButtonExpand
			// 
			this.layoutControlItemButtonExpand.Control = this.simpleButtonExpand;
			this.layoutControlItemButtonExpand.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemButtonExpand.FillControlToClientArea = false;
			this.layoutControlItemButtonExpand.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemButtonExpand.MaxSize = new System.Drawing.Size(47, 40);
			this.layoutControlItemButtonExpand.MinSize = new System.Drawing.Size(47, 40);
			this.layoutControlItemButtonExpand.Name = "layoutControlItemButtonExpand";
			this.layoutControlItemButtonExpand.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemButtonExpand.Size = new System.Drawing.Size(50, 40);
			this.layoutControlItemButtonExpand.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemButtonExpand.Text = "Expand Button";
			this.layoutControlItemButtonExpand.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemButtonExpand.TextVisible = false;
			this.layoutControlItemButtonExpand.TrimClientAreaToControl = false;
			// 
			// pnOpened
			// 
			this.pnOpened.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnOpened.Controls.Add(this.layoutControlOpened);
			this.pnOpened.Location = new System.Drawing.Point(6, 160);
			this.pnOpened.Name = "pnOpened";
			this.pnOpened.Size = new System.Drawing.Size(219, 160);
			this.pnOpened.TabIndex = 1;
			// 
			// layoutControlOpened
			// 
			this.layoutControlOpened.AllowCustomization = false;
			this.layoutControlOpened.Controls.Add(this.simpleButtonCollapse);
			this.layoutControlOpened.Controls.Add(this.pnHeaderContent);
			this.layoutControlOpened.Controls.Add(this.pnContent);
			this.layoutControlOpened.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControlOpened.Location = new System.Drawing.Point(2, 2);
			this.layoutControlOpened.Name = "layoutControlOpened";
			this.layoutControlOpened.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(536, 39, 450, 400);
			this.layoutControlOpened.OptionsView.ShareLookAndFeelWithChildren = false;
			this.layoutControlOpened.Root = this.layoutControlGroupOpenedRoot;
			this.layoutControlOpened.Size = new System.Drawing.Size(215, 156);
			this.layoutControlOpened.TabIndex = 2;
			this.layoutControlOpened.Text = "layoutControl1";
			// 
			// simpleButtonCollapse
			// 
			this.simpleButtonCollapse.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonCollapse.Image = global::Asa.Common.GUI.Properties.Resources.RetractableBarCollapse;
			this.simpleButtonCollapse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.simpleButtonCollapse.Location = new System.Drawing.Point(2, 3);
			this.simpleButtonCollapse.Name = "simpleButtonCollapse";
			this.simpleButtonCollapse.Size = new System.Drawing.Size(47, 38);
			this.simpleButtonCollapse.TabIndex = 1;
			this.simpleButtonCollapse.ToolTip = "Collapse bar";
			this.simpleButtonCollapse.Click += new System.EventHandler(this.simpleButtonCollapse_Click);
			// 
			// pnHeaderContent
			// 
			this.pnHeaderContent.Location = new System.Drawing.Point(49, 2);
			this.pnHeaderContent.Name = "pnHeaderContent";
			this.pnHeaderContent.Size = new System.Drawing.Size(164, 40);
			this.pnHeaderContent.TabIndex = 2;
			// 
			// pnContent
			// 
			this.pnContent.Location = new System.Drawing.Point(2, 42);
			this.pnContent.Name = "pnContent";
			this.pnContent.Size = new System.Drawing.Size(211, 112);
			this.pnContent.TabIndex = 1;
			// 
			// layoutControlGroupOpenedRoot
			// 
			this.layoutControlGroupOpenedRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupOpenedRoot.GroupBordersVisible = false;
			this.layoutControlGroupOpenedRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemContent,
            this.layoutControlItemHeaderContent,
            this.layoutControlItemButtonCollpase});
			this.layoutControlGroupOpenedRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupOpenedRoot.Name = "Root";
			this.layoutControlGroupOpenedRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.layoutControlGroupOpenedRoot.Size = new System.Drawing.Size(215, 156);
			this.layoutControlGroupOpenedRoot.TextVisible = false;
			// 
			// layoutControlItemContent
			// 
			this.layoutControlItemContent.Control = this.pnContent;
			this.layoutControlItemContent.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemContent.FillControlToClientArea = false;
			this.layoutControlItemContent.Location = new System.Drawing.Point(0, 40);
			this.layoutControlItemContent.Name = "layoutControlItemContent";
			this.layoutControlItemContent.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemContent.Size = new System.Drawing.Size(211, 112);
			this.layoutControlItemContent.Text = "Content";
			this.layoutControlItemContent.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemContent.TextVisible = false;
			this.layoutControlItemContent.TrimClientAreaToControl = false;
			// 
			// layoutControlItemHeaderContent
			// 
			this.layoutControlItemHeaderContent.Control = this.pnHeaderContent;
			this.layoutControlItemHeaderContent.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemHeaderContent.FillControlToClientArea = false;
			this.layoutControlItemHeaderContent.Location = new System.Drawing.Point(47, 0);
			this.layoutControlItemHeaderContent.MaxSize = new System.Drawing.Size(0, 40);
			this.layoutControlItemHeaderContent.MinSize = new System.Drawing.Size(1, 40);
			this.layoutControlItemHeaderContent.Name = "layoutControlItemHeaderContent";
			this.layoutControlItemHeaderContent.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemHeaderContent.Size = new System.Drawing.Size(164, 40);
			this.layoutControlItemHeaderContent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemHeaderContent.Text = "Header Content";
			this.layoutControlItemHeaderContent.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemHeaderContent.TextVisible = false;
			this.layoutControlItemHeaderContent.TrimClientAreaToControl = false;
			// 
			// layoutControlItemButtonCollpase
			// 
			this.layoutControlItemButtonCollpase.Control = this.simpleButtonCollapse;
			this.layoutControlItemButtonCollpase.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.layoutControlItemButtonCollpase.FillControlToClientArea = false;
			this.layoutControlItemButtonCollpase.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemButtonCollpase.MaxSize = new System.Drawing.Size(47, 0);
			this.layoutControlItemButtonCollpase.MinSize = new System.Drawing.Size(47, 1);
			this.layoutControlItemButtonCollpase.Name = "layoutControlItemButtonCollpase";
			this.layoutControlItemButtonCollpase.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItemButtonCollpase.Size = new System.Drawing.Size(47, 40);
			this.layoutControlItemButtonCollpase.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemButtonCollpase.Text = "Collapse Button";
			this.layoutControlItemButtonCollpase.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemButtonCollpase.TextVisible = false;
			this.layoutControlItemButtonCollpase.TrimClientAreaToControl = false;
			// 
			// RetractableBarControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnOpened);
			this.Controls.Add(this.pnClosed);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "RetractableBarControl";
			this.Size = new System.Drawing.Size(359, 388);
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).EndInit();
			this.pnClosed.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlClosed)).EndInit();
			this.layoutControlClosed.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupClosedRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdditionalButtons)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemButtonExpand)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).EndInit();
			this.pnOpened.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlOpened)).EndInit();
			this.layoutControlOpened.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupOpenedRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHeaderContent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemButtonCollpase)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.PanelControl pnClosed;
		protected DevExpress.XtraEditors.PanelControl pnOpened;
		protected System.Windows.Forms.Panel pnContent;
		private System.Windows.Forms.Panel pnAdditionalButtons;
		protected System.Windows.Forms.Panel pnHeaderContent;
		public DevExpress.XtraEditors.SimpleButton simpleButtonExpand;
		public DevExpress.XtraEditors.SimpleButton simpleButtonCollapse;
		private DevExpress.XtraLayout.LayoutControl layoutControlOpened;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupOpenedRoot;
		private DevExpress.XtraLayout.LayoutControl layoutControlClosed;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupClosedRoot;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemContent;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemHeaderContent;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemButtonCollpase;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAdditionalButtons;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemButtonExpand;
	}
}
