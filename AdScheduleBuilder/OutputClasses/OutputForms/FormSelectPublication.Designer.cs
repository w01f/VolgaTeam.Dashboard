namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    partial class FormSelectPublication
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
            this.buttonXCurrentPublication = new DevComponents.DotNetBar.ButtonX();
            this.buttonXSelectedPublications = new DevComponents.DotNetBar.ButtonX();
            this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
            this.laTitle = new System.Windows.Forms.Label();
            this.checkedListBoxControlPublications = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlPublications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXCurrentPublication
            // 
            this.buttonXCurrentPublication.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCurrentPublication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCurrentPublication.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCurrentPublication.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonXCurrentPublication.Location = new System.Drawing.Point(334, 97);
            this.buttonXCurrentPublication.Name = "buttonXCurrentPublication";
            this.buttonXCurrentPublication.Size = new System.Drawing.Size(301, 65);
            this.buttonXCurrentPublication.TabIndex = 9;
            this.buttonXCurrentPublication.Text = "Attach just the Current Publication Overview to my Outlook Email Message";
            this.buttonXCurrentPublication.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXSelectedPublications
            // 
            this.buttonXSelectedPublications.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSelectedPublications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXSelectedPublications.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSelectedPublications.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonXSelectedPublications.Location = new System.Drawing.Point(334, 178);
            this.buttonXSelectedPublications.Name = "buttonXSelectedPublications";
            this.buttonXSelectedPublications.Size = new System.Drawing.Size(301, 65);
            this.buttonXSelectedPublications.TabIndex = 10;
            this.buttonXSelectedPublications.Text = "Attach all Selected Publications to my Outlook Email Message";
            this.buttonXSelectedPublications.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXClose
            // 
            this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXClose.Location = new System.Drawing.Point(334, 260);
            this.buttonXClose.Name = "buttonXClose";
            this.buttonXClose.Size = new System.Drawing.Size(301, 65);
            this.buttonXClose.TabIndex = 11;
            this.buttonXClose.Text = "Cancel";
            this.buttonXClose.TextColor = System.Drawing.Color.Black;
            // 
            // laTitle
            // 
            this.laTitle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.ForeColor = System.Drawing.Color.Black;
            this.laTitle.Location = new System.Drawing.Point(96, 14);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(539, 69);
            this.laTitle.TabIndex = 14;
            this.laTitle.Text = "You have Several Publications in this Basic Overview Summary…";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkedListBoxControlPublications
            // 
            this.checkedListBoxControlPublications.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.checkedListBoxControlPublications.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxControlPublications.Appearance.Options.UseBackColor = true;
            this.checkedListBoxControlPublications.Appearance.Options.UseFont = true;
            this.checkedListBoxControlPublications.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.checkedListBoxControlPublications.CheckOnClick = true;
            this.checkedListBoxControlPublications.ItemHeight = 25;
            this.checkedListBoxControlPublications.Location = new System.Drawing.Point(12, 97);
            this.checkedListBoxControlPublications.Name = "checkedListBoxControlPublications";
            this.checkedListBoxControlPublications.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxControlPublications.Size = new System.Drawing.Size(299, 229);
            this.checkedListBoxControlPublications.TabIndex = 15;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::AdScheduleBuilder.Properties.Resources.EmailBig;
            this.pbLogo.Location = new System.Drawing.Point(12, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(78, 71);
            this.pbLogo.TabIndex = 13;
            this.pbLogo.TabStop = false;
            // 
            // FormSelectPublication
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(647, 338);
            this.Controls.Add(this.checkedListBoxControlPublications);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.buttonXClose);
            this.Controls.Add(this.buttonXSelectedPublications);
            this.Controls.Add(this.buttonXCurrentPublication);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectPublication";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calendar Output Options";
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlPublications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXClose;
        public DevComponents.DotNetBar.ButtonX buttonXCurrentPublication;
        public System.Windows.Forms.PictureBox pbLogo;
        public System.Windows.Forms.Label laTitle;
        public DevComponents.DotNetBar.ButtonX buttonXSelectedPublications;
        public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlPublications;
    }
}