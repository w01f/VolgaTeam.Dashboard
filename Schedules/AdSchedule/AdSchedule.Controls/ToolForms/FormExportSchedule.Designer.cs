namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
    partial class FormExportSchedule
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
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.laLogo = new System.Windows.Forms.Label();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.textEditScheduleName = new DevExpress.XtraEditors.TextEdit();
			this.labelDescription = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditScheduleName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.ExportCalendar;
			this.pbLogo.Location = new System.Drawing.Point(12, 12);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(72, 75);
			this.pbLogo.TabIndex = 0;
			this.pbLogo.TabStop = false;
			// 
			// laLogo
			// 
			this.laLogo.Location = new System.Drawing.Point(90, 12);
			this.laLogo.Name = "laLogo";
			this.laLogo.Size = new System.Drawing.Size(282, 41);
			this.laLogo.TabIndex = 1;
			this.laLogo.Text = "Save this Ninja Calendar as:";
			this.laLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(90, 164);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(98, 32);
			this.buttonXOK.TabIndex = 1;
			this.buttonXOK.Text = "Save";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(212, 164);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(98, 32);
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// textEditScheduleName
			// 
			this.textEditScheduleName.Location = new System.Drawing.Point(90, 65);
			this.textEditScheduleName.Name = "textEditScheduleName";
			this.textEditScheduleName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textEditScheduleName.Properties.Appearance.Options.UseFont = true;
			this.textEditScheduleName.Properties.NullText = "Type here";
			this.textEditScheduleName.Size = new System.Drawing.Size(282, 22);
			this.textEditScheduleName.TabIndex = 0;
			this.textEditScheduleName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditScheduleName_KeyDown);
			// 
			// labelDescription
			// 
			this.labelDescription.Location = new System.Drawing.Point(9, 90);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(363, 71);
			this.labelDescription.TabIndex = 3;
			this.labelDescription.Text = "After you save this Calendar, you can open and edit later in the Ninja Calendar A" +
    "pplication Dashboard…";
			this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormExportSchedule
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(384, 208);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.textEditScheduleName);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.laLogo);
			this.Controls.Add(this.pbLogo);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormExportSchedule";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Build a New Schedule";
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditScheduleName.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.TextEdit textEditScheduleName;
        public System.Windows.Forms.Label laLogo;
		public System.Windows.Forms.Label labelDescription;
    }
}