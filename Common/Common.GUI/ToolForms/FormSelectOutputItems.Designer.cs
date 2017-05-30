namespace Asa.Common.GUI.ToolForms
{
    partial class FormSelectOutputItems
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
			this.buttonXContinue = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.checkedListBoxControlOutputItems = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectCurrent = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectNone = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlOutputItems)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXContinue
			// 
			this.buttonXContinue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXContinue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXContinue.Location = new System.Drawing.Point(14, 370);
			this.buttonXContinue.Name = "buttonXContinue";
			this.buttonXContinue.Size = new System.Drawing.Size(148, 43);
			this.buttonXContinue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXContinue.TabIndex = 9;
			this.buttonXContinue.Text = "Continue";
			this.buttonXContinue.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXClose.Location = new System.Drawing.Point(188, 370);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(148, 43);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 11;
			this.buttonXClose.Text = "Cancel";
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			// 
			// checkedListBoxControlOutputItems
			// 
			this.checkedListBoxControlOutputItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxControlOutputItems.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxControlOutputItems.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxControlOutputItems.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkedListBoxControlOutputItems.Appearance.Options.UseBackColor = true;
			this.checkedListBoxControlOutputItems.Appearance.Options.UseFont = true;
			this.checkedListBoxControlOutputItems.Appearance.Options.UseForeColor = true;
			this.checkedListBoxControlOutputItems.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
			this.checkedListBoxControlOutputItems.CheckOnClick = true;
			this.checkedListBoxControlOutputItems.ItemHeight = 25;
			this.checkedListBoxControlOutputItems.Location = new System.Drawing.Point(14, 53);
			this.checkedListBoxControlOutputItems.Name = "checkedListBoxControlOutputItems";
			this.checkedListBoxControlOutputItems.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControlOutputItems.Size = new System.Drawing.Size(322, 306);
			this.checkedListBoxControlOutputItems.TabIndex = 15;
			this.checkedListBoxControlOutputItems.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlProducts_ItemCheck);
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Location = new System.Drawing.Point(14, 9);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(96, 35);
			this.buttonXSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectAll.TabIndex = 16;
			this.buttonXSelectAll.Text = "All";
			this.buttonXSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectAll.Click += new System.EventHandler(this.buttonXSelectAll_Click);
			// 
			// buttonXSelectCurrent
			// 
			this.buttonXSelectCurrent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSelectCurrent.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectCurrent.Location = new System.Drawing.Point(130, 9);
			this.buttonXSelectCurrent.Name = "buttonXSelectCurrent";
			this.buttonXSelectCurrent.Size = new System.Drawing.Size(96, 35);
			this.buttonXSelectCurrent.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectCurrent.TabIndex = 17;
			this.buttonXSelectCurrent.Text = "Current";
			this.buttonXSelectCurrent.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectCurrent.Click += new System.EventHandler(this.buttonXSelectCurrent_Click);
			// 
			// buttonXSelectNone
			// 
			this.buttonXSelectNone.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSelectNone.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectNone.Location = new System.Drawing.Point(240, 9);
			this.buttonXSelectNone.Name = "buttonXSelectNone";
			this.buttonXSelectNone.Size = new System.Drawing.Size(96, 35);
			this.buttonXSelectNone.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectNone.TabIndex = 18;
			this.buttonXSelectNone.Text = "Clear";
			this.buttonXSelectNone.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectNone.Click += new System.EventHandler(this.buttonXSelectNone_Click);
			// 
			// FormSelectOutputItems
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(350, 420);
			this.Controls.Add(this.buttonXSelectNone);
			this.Controls.Add(this.buttonXSelectCurrent);
			this.Controls.Add(this.buttonXSelectAll);
			this.Controls.Add(this.checkedListBoxControlOutputItems);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXContinue);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectOutputItems";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Digital Products";
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlOutputItems)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXClose;
		public DevComponents.DotNetBar.ButtonX buttonXContinue;
        public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlOutputItems;
		public DevComponents.DotNetBar.ButtonX buttonXSelectAll;
		public DevComponents.DotNetBar.ButtonX buttonXSelectCurrent;
		public DevComponents.DotNetBar.ButtonX buttonXSelectNone;
    }
}