namespace Asa.Dashboard.ToolForms
{
	partial class FormSaveTemplate
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
			this.laLogo = new System.Windows.Forms.Label();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.textEditTemplateName = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.textEditTemplateName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// laLogo
			// 
			this.laLogo.BackColor = System.Drawing.Color.White;
			this.laLogo.ForeColor = System.Drawing.Color.Black;
			this.laLogo.Location = new System.Drawing.Point(12, 12);
			this.laLogo.Name = "laLogo";
			this.laLogo.Size = new System.Drawing.Size(360, 31);
			this.laLogo.TabIndex = 1;
			this.laLogo.Text = "What do you want to name this Template?";
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(166, 83);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(92, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 1;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(278, 83);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(92, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// textEditTemplateName
			// 
			this.textEditTemplateName.Location = new System.Drawing.Point(12, 46);
			this.textEditTemplateName.Name = "textEditTemplateName";
			this.textEditTemplateName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditTemplateName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textEditTemplateName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditTemplateName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditTemplateName.Properties.Appearance.Options.UseFont = true;
			this.textEditTemplateName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditTemplateName.Properties.NullText = "Type here";
			this.textEditTemplateName.Size = new System.Drawing.Size(360, 22);
			this.textEditTemplateName.TabIndex = 0;
			this.textEditTemplateName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditTemplateName_KeyDown);
			// 
			// FormSaveTemplate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(384, 119);
			this.Controls.Add(this.textEditTemplateName);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.laLogo);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSaveTemplate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Save Template";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSaveTemplate_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.textEditTemplateName.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.TextEdit textEditTemplateName;
        public System.Windows.Forms.Label laLogo;
    }
}