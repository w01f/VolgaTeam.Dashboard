namespace AdSalesAddIn.ToolForms
{
    partial class FormFormatChangeNotification
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
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControlCurrentState = new DevExpress.XtraEditors.LabelControl();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
            this.labelControlFutureState = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDescripption = new DevExpress.XtraEditors.LabelControl();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 12F);
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
            // labelControlCurrentState
            // 
            this.labelControlCurrentState.AllowHtmlString = true;
            this.labelControlCurrentState.Location = new System.Drawing.Point(109, 12);
            this.labelControlCurrentState.Name = "labelControlCurrentState";
            this.labelControlCurrentState.Size = new System.Drawing.Size(92, 19);
            this.labelControlCurrentState.StyleController = this.styleController;
            this.labelControlCurrentState.TabIndex = 1;
            this.labelControlCurrentState.Text = "labelControl1";
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonXCancel.Location = new System.Drawing.Point(496, 176);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(122, 32);
            this.buttonXCancel.TabIndex = 14;
            this.buttonXCancel.Text = "CANCEL";
            // 
            // buttonXSave
            // 
            this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonXSave.ForeColor = System.Drawing.Color.Black;
            this.buttonXSave.Location = new System.Drawing.Point(344, 176);
            this.buttonXSave.Name = "buttonXSave";
            this.buttonXSave.Size = new System.Drawing.Size(122, 32);
            this.buttonXSave.TabIndex = 13;
            this.buttonXSave.Text = "YES!";
            // 
            // labelControlFutureState
            // 
            this.labelControlFutureState.AllowHtmlString = true;
            this.labelControlFutureState.Location = new System.Drawing.Point(109, 56);
            this.labelControlFutureState.Name = "labelControlFutureState";
            this.labelControlFutureState.Size = new System.Drawing.Size(92, 19);
            this.labelControlFutureState.StyleController = this.styleController;
            this.labelControlFutureState.TabIndex = 15;
            this.labelControlFutureState.Text = "labelControl1";
            // 
            // labelControlDescripption
            // 
            this.labelControlDescripption.AllowHtmlString = true;
            this.labelControlDescripption.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlDescripption.Location = new System.Drawing.Point(109, 103);
            this.labelControlDescripption.Name = "labelControlDescripption";
            this.labelControlDescripption.Size = new System.Drawing.Size(462, 57);
            this.labelControlDescripption.StyleController = this.styleController;
            this.labelControlDescripption.TabIndex = 16;
            this.labelControlDescripption.Text = "Changing your Slide Format size COULD distort items on the slide.\r\n\r\nDO YOU STILL" +
                " WANT TO DO THIS?";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::AdSalesAddIn.Properties.Resources.Warning;
            this.pbLogo.Location = new System.Drawing.Point(8, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(95, 94);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // FormFormatChangeNotification
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(626, 220);
            this.Controls.Add(this.labelControlDescripption);
            this.Controls.Add(this.labelControlFutureState);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXSave);
            this.Controls.Add(this.labelControlCurrentState);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFormatChangeNotification";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slide Format Change Notification!";
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        public DevExpress.XtraEditors.LabelControl labelControlCurrentState;
        public DevExpress.XtraEditors.LabelControl labelControlFutureState;
        public DevExpress.XtraEditors.LabelControl labelControlDescripption;
    }
}