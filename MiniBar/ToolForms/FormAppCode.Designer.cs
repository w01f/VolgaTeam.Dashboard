namespace MiniBar.ToolForms
{
    partial class FormAppCode
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
            this.laTitle = new System.Windows.Forms.Label();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.laCode = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.textEditCode = new DevExpress.XtraEditors.TextEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(86, 6);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(249, 56);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "This App is LOCKED for Manager access only...";
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(168, 113);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(122, 32);
            this.buttonXCancel.TabIndex = 3;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(21, 113);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(122, 32);
            this.buttonXOK.TabIndex = 2;
            this.buttonXOK.Text = "Open";
            this.buttonXOK.TextColor = System.Drawing.Color.Black;
            // 
            // laCode
            // 
            this.laCode.AutoSize = true;
            this.laCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laCode.Location = new System.Drawing.Point(87, 62);
            this.laCode.Name = "laCode";
            this.laCode.Size = new System.Drawing.Size(89, 16);
            this.laCode.TabIndex = 11;
            this.laCode.Text = "5-Digit Code:";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::MiniBar.Properties.Resources.AppCode;
            this.pbLogo.Location = new System.Drawing.Point(3, 6);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(77, 79);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // textEditCode
            // 
            this.textEditCode.EditValue = "";
            this.textEditCode.Location = new System.Drawing.Point(195, 59);
            this.textEditCode.Name = "textEditCode";
            this.textEditCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditCode.Properties.Appearance.Options.UseFont = true;
            this.textEditCode.Properties.MaxLength = 5;
            this.textEditCode.Size = new System.Drawing.Size(95, 40);
            this.textEditCode.TabIndex = 1;
            this.textEditCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditCode_KeyDown);
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
            // FormAppCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(310, 154);
            this.Controls.Add(this.textEditCode);
            this.Controls.Add(this.laCode);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAppCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Access Code";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private System.Windows.Forms.Label laCode;
        private DevExpress.XtraEditors.TextEdit textEditCode;
        private DevExpress.XtraEditors.StyleController styleController;
    }
}