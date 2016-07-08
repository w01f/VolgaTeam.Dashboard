namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	partial class ActiveWeeksSettingsControl
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.checkedListBoxActiveWeeks = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.pnActiveweeksButtons = new System.Windows.Forms.Panel();
			this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.laActiveWeeksWarning = new System.Windows.Forms.Label();
			this.laActiveWeeks = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxActiveWeeks)).BeginInit();
			this.pnActiveweeksButtons.SuspendLayout();
			this.SuspendLayout();
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
			// checkedListBoxActiveWeeks
			// 
			this.checkedListBoxActiveWeeks.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxActiveWeeks.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxActiveWeeks.Appearance.Options.UseBackColor = true;
			this.checkedListBoxActiveWeeks.Appearance.Options.UseFont = true;
			this.checkedListBoxActiveWeeks.CheckOnClick = true;
			this.checkedListBoxActiveWeeks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxActiveWeeks.ItemHeight = 40;
			this.checkedListBoxActiveWeeks.Location = new System.Drawing.Point(0, 90);
			this.checkedListBoxActiveWeeks.Name = "checkedListBoxActiveWeeks";
			this.checkedListBoxActiveWeeks.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxActiveWeeks.Size = new System.Drawing.Size(279, 264);
			this.checkedListBoxActiveWeeks.StyleController = this.styleController;
			this.checkedListBoxActiveWeeks.TabIndex = 51;
			this.checkedListBoxActiveWeeks.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.OnItemCheck);
			// 
			// pnActiveweeksButtons
			// 
			this.pnActiveweeksButtons.Controls.Add(this.buttonXClearAll);
			this.pnActiveweeksButtons.Controls.Add(this.buttonXSelectAll);
			this.pnActiveweeksButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnActiveweeksButtons.Location = new System.Drawing.Point(0, 42);
			this.pnActiveweeksButtons.Name = "pnActiveweeksButtons";
			this.pnActiveweeksButtons.Size = new System.Drawing.Size(279, 48);
			this.pnActiveweeksButtons.TabIndex = 54;
			// 
			// buttonXClearAll
			// 
			this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClearAll.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXClearAll.Location = new System.Drawing.Point(165, 8);
			this.buttonXClearAll.Name = "buttonXClearAll";
			this.buttonXClearAll.Size = new System.Drawing.Size(107, 33);
			this.buttonXClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClearAll.TabIndex = 11;
			this.buttonXClearAll.Text = "Clear All";
			this.buttonXClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXClearAll.Click += new System.EventHandler(this.OnClearAll_Click);
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXSelectAll.Location = new System.Drawing.Point(8, 8);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(107, 33);
			this.buttonXSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectAll.TabIndex = 10;
			this.buttonXSelectAll.Text = "Select All";
			this.buttonXSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectAll.Click += new System.EventHandler(this.OnSelectAll_Click);
			// 
			// laActiveWeeksWarning
			// 
			this.laActiveWeeksWarning.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.laActiveWeeksWarning.Font = new System.Drawing.Font("Arial", 9.75F);
			this.laActiveWeeksWarning.ForeColor = System.Drawing.Color.Red;
			this.laActiveWeeksWarning.Location = new System.Drawing.Point(0, 354);
			this.laActiveWeeksWarning.Name = "laActiveWeeksWarning";
			this.laActiveWeeksWarning.Size = new System.Drawing.Size(279, 40);
			this.laActiveWeeksWarning.TabIndex = 53;
			this.laActiveWeeksWarning.Text = "You should select at least 1 week";
			this.laActiveWeeksWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.laActiveWeeksWarning.Visible = false;
			// 
			// laActiveWeeks
			// 
			this.laActiveWeeks.Dock = System.Windows.Forms.DockStyle.Top;
			this.laActiveWeeks.Font = new System.Drawing.Font("Arial", 9.75F);
			this.laActiveWeeks.Location = new System.Drawing.Point(0, 0);
			this.laActiveWeeks.Name = "laActiveWeeks";
			this.laActiveWeeks.Padding = new System.Windows.Forms.Padding(5);
			this.laActiveWeeks.Size = new System.Drawing.Size(279, 42);
			this.laActiveWeeks.TabIndex = 52;
			this.laActiveWeeks.Text = "Do you want to apply this schedule to specific weeks on the calendar?";
			// 
			// ActiveWeeksSettingsControl
			// 
			this.Controls.Add(this.checkedListBoxActiveWeeks);
			this.Controls.Add(this.pnActiveweeksButtons);
			this.Controls.Add(this.laActiveWeeksWarning);
			this.Controls.Add(this.laActiveWeeks);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ActiveWeeksSettingsControl";
			this.Size = new System.Drawing.Size(279, 394);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxActiveWeeks)).EndInit();
			this.pnActiveweeksButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxActiveWeeks;
		private System.Windows.Forms.Panel pnActiveweeksButtons;
		private DevComponents.DotNetBar.ButtonX buttonXClearAll;
		private DevComponents.DotNetBar.ButtonX buttonXSelectAll;
		private System.Windows.Forms.Label laActiveWeeksWarning;
		private System.Windows.Forms.Label laActiveWeeks;
	}
}
