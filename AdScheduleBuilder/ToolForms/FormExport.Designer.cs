namespace AdScheduleBuilder.ToolForms
{
    partial class FormExport
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
            this.buttonXImport = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.laCalendarType = new System.Windows.Forms.Label();
            this.checkEditAdvanced = new DevExpress.XtraEditors.CheckEdit();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.checkEditGraphic = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditSimple = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAdvanced.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditGraphic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSimple.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXImport
            // 
            this.buttonXImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXImport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXImport.Location = new System.Drawing.Point(62, 99);
            this.buttonXImport.Name = "buttonXImport";
            this.buttonXImport.Size = new System.Drawing.Size(91, 31);
            this.buttonXImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXImport.TabIndex = 25;
            this.buttonXImport.Text = "Export";
            this.buttonXImport.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(178, 99);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(91, 31);
            this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXCancel.TabIndex = 26;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // laCalendarType
            // 
            this.laCalendarType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laCalendarType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laCalendarType.Location = new System.Drawing.Point(12, 9);
            this.laCalendarType.Name = "laCalendarType";
            this.laCalendarType.Size = new System.Drawing.Size(307, 37);
            this.laCalendarType.TabIndex = 27;
            this.laCalendarType.Text = "What calendars you want to build:";
            this.laCalendarType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.laCalendarType.UseMnemonic = false;
            // 
            // checkEditAdvanced
            // 
            this.checkEditAdvanced.EditValue = true;
            this.checkEditAdvanced.Location = new System.Drawing.Point(35, 55);
            this.checkEditAdvanced.Name = "checkEditAdvanced";
            this.checkEditAdvanced.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditAdvanced.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.checkEditAdvanced.Properties.Appearance.Options.UseFont = true;
            this.checkEditAdvanced.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditAdvanced.Properties.Caption = "NERD";
            this.checkEditAdvanced.Size = new System.Drawing.Size(75, 24);
            this.checkEditAdvanced.TabIndex = 28;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // checkEditGraphic
            // 
            this.checkEditGraphic.EditValue = true;
            this.checkEditGraphic.Location = new System.Drawing.Point(127, 55);
            this.checkEditGraphic.Name = "checkEditGraphic";
            this.checkEditGraphic.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditGraphic.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.checkEditGraphic.Properties.Appearance.Options.UseFont = true;
            this.checkEditGraphic.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditGraphic.Properties.Caption = "COOL";
            this.checkEditGraphic.Size = new System.Drawing.Size(75, 24);
            this.checkEditGraphic.TabIndex = 29;
            // 
            // checkEditSimple
            // 
            this.checkEditSimple.EditValue = true;
            this.checkEditSimple.Location = new System.Drawing.Point(221, 55);
            this.checkEditSimple.Name = "checkEditSimple";
            this.checkEditSimple.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkEditSimple.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.checkEditSimple.Properties.Appearance.Options.UseFont = true;
            this.checkEditSimple.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditSimple.Properties.Caption = "EASY";
            this.checkEditSimple.Size = new System.Drawing.Size(75, 24);
            this.checkEditSimple.TabIndex = 30;
            // 
            // FormExport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(331, 141);
            this.Controls.Add(this.checkEditSimple);
            this.Controls.Add(this.checkEditGraphic);
            this.Controls.Add(this.checkEditAdvanced);
            this.Controls.Add(this.laCalendarType);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXImport);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Schedule";
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAdvanced.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditGraphic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSimple.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXImport;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private System.Windows.Forms.Label laCalendarType;
        private DevExpress.XtraEditors.CheckEdit checkEditAdvanced;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.CheckEdit checkEditGraphic;
        private DevExpress.XtraEditors.CheckEdit checkEditSimple;
    }
}