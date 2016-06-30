namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	partial class SectionSummaryControl
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
			this.pnInputBorder = new System.Windows.Forms.Panel();
			this.xtraScrollableControlInput = new DevExpress.XtraEditors.XtraScrollableControl();
			this.buttonXAddItem = new DevComponents.DotNetBar.ButtonX();
			this.pnInputHeader = new System.Windows.Forms.Panel();
			this.laHeaderTitle = new System.Windows.Forms.Label();
			this.comboBoxEditHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.pnInputBorder.SuspendLayout();
			this.pnInputHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditHeader.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnInputBorder
			// 
			this.pnInputBorder.BackColor = System.Drawing.Color.LightGray;
			this.pnInputBorder.Controls.Add(this.xtraScrollableControlInput);
			this.pnInputBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnInputBorder.Location = new System.Drawing.Point(0, 59);
			this.pnInputBorder.Name = "pnInputBorder";
			this.pnInputBorder.Padding = new System.Windows.Forms.Padding(2);
			this.pnInputBorder.Size = new System.Drawing.Size(737, 498);
			this.pnInputBorder.TabIndex = 2;
			// 
			// xtraScrollableControlInput
			// 
			this.xtraScrollableControlInput.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraScrollableControlInput.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlInput.Location = new System.Drawing.Point(2, 2);
			this.xtraScrollableControlInput.Name = "xtraScrollableControlInput";
			this.xtraScrollableControlInput.Size = new System.Drawing.Size(733, 494);
			this.xtraScrollableControlInput.TabIndex = 0;
			// 
			// buttonXAddItem
			// 
			this.buttonXAddItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXAddItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAddItem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXAddItem.Location = new System.Drawing.Point(600, 9);
			this.buttonXAddItem.Name = "buttonXAddItem";
			this.buttonXAddItem.Size = new System.Drawing.Size(127, 40);
			this.buttonXAddItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXAddItem, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Add another Media Sales\r\nItem to your Summary Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXAddItem.TabIndex = 120;
			this.buttonXAddItem.TabStop = false;
			this.buttonXAddItem.Text = "Add an Item";
			this.buttonXAddItem.TextColor = System.Drawing.Color.Black;
			// 
			// pnInputHeader
			// 
			this.pnInputHeader.Controls.Add(this.buttonXAddItem);
			this.pnInputHeader.Controls.Add(this.laHeaderTitle);
			this.pnInputHeader.Controls.Add(this.comboBoxEditHeader);
			this.pnInputHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnInputHeader.Location = new System.Drawing.Point(0, 0);
			this.pnInputHeader.Name = "pnInputHeader";
			this.pnInputHeader.Size = new System.Drawing.Size(737, 59);
			this.pnInputHeader.TabIndex = 0;
			// 
			// laHeaderTitle
			// 
			this.laHeaderTitle.AutoSize = true;
			this.laHeaderTitle.ForeColor = System.Drawing.Color.Black;
			this.laHeaderTitle.Location = new System.Drawing.Point(315, 21);
			this.laHeaderTitle.Name = "laHeaderTitle";
			this.laHeaderTitle.Size = new System.Drawing.Size(73, 16);
			this.laHeaderTitle.TabIndex = 72;
			this.laHeaderTitle.Text = "(Slide Title)";
			// 
			// comboBoxEditHeader
			// 
			this.comboBoxEditHeader.Location = new System.Drawing.Point(8, 18);
			this.comboBoxEditHeader.Name = "comboBoxEditHeader";
			this.comboBoxEditHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditHeader.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditHeader.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditHeader.Size = new System.Drawing.Size(301, 22);
			this.comboBoxEditHeader.TabIndex = 71;
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// SectionSummaryControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnInputBorder);
			this.Controls.Add(this.pnInputHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SectionSummaryControl";
			this.Size = new System.Drawing.Size(737, 557);
			this.pnInputBorder.ResumeLayout(false);
			this.pnInputHeader.ResumeLayout(false);
			this.pnInputHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditHeader.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnInputHeader;
		private System.Windows.Forms.Panel pnInputBorder;
		protected DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlInput;
		protected DevComponents.DotNetBar.ButtonX buttonXAddItem;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditHeader;
		private System.Windows.Forms.Label laHeaderTitle;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;

	}
}
