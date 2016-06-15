namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
    partial class FormConfigureOutput
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
			this.checkedListBoxControlOutputOptionItems = new DevExpress.XtraEditors.CheckedListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlOutputOptionItems)).BeginInit();
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
			// checkedListBoxControlOutputOptionItems
			// 
			this.checkedListBoxControlOutputOptionItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxControlOutputOptionItems.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxControlOutputOptionItems.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxControlOutputOptionItems.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkedListBoxControlOutputOptionItems.Appearance.Options.UseBackColor = true;
			this.checkedListBoxControlOutputOptionItems.Appearance.Options.UseFont = true;
			this.checkedListBoxControlOutputOptionItems.Appearance.Options.UseForeColor = true;
			this.checkedListBoxControlOutputOptionItems.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
			this.checkedListBoxControlOutputOptionItems.CheckOnClick = true;
			this.checkedListBoxControlOutputOptionItems.ItemHeight = 25;
			this.checkedListBoxControlOutputOptionItems.Location = new System.Drawing.Point(14, 12);
			this.checkedListBoxControlOutputOptionItems.Name = "checkedListBoxControlOutputOptionItems";
			this.checkedListBoxControlOutputOptionItems.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControlOutputOptionItems.ShowFocusRect = false;
			this.checkedListBoxControlOutputOptionItems.Size = new System.Drawing.Size(322, 347);
			this.checkedListBoxControlOutputOptionItems.TabIndex = 15;
			this.checkedListBoxControlOutputOptionItems.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlProducts_ItemCheck);
			// 
			// FormConfigureOutput
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(350, 420);
			this.Controls.Add(this.checkedListBoxControlOutputOptionItems);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXContinue);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormConfigureOutput";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Schedules";
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlOutputOptionItems)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXClose;
		public DevComponents.DotNetBar.ButtonX buttonXContinue;
        public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlOutputOptionItems;
    }
}