namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
    partial class FormChangeAdStrategyWarning
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
			this.buttonXYes = new DevComponents.DotNetBar.ButtonX();
			this.buttonXNo = new DevComponents.DotNetBar.ButtonX();
			this.labelControlText = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXYes
			// 
			this.buttonXYes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXYes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.buttonXYes.Location = new System.Drawing.Point(103, 103);
			this.buttonXYes.Name = "buttonXYes";
			this.buttonXYes.Size = new System.Drawing.Size(75, 29);
			this.buttonXYes.TabIndex = 7;
			this.buttonXYes.Text = "Yes";
			this.buttonXYes.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXNo
			// 
			this.buttonXNo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXNo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.buttonXNo.Location = new System.Drawing.Point(189, 103);
			this.buttonXNo.Name = "buttonXNo";
			this.buttonXNo.Size = new System.Drawing.Size(75, 29);
			this.buttonXNo.TabIndex = 8;
			this.buttonXNo.Text = "No";
			this.buttonXNo.TextColor = System.Drawing.Color.Black;
			// 
			// labelControlText
			// 
			this.labelControlText.AllowHtmlString = true;
			this.labelControlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlText.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlText.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlText.Location = new System.Drawing.Point(65, 12);
			this.labelControlText.Name = "labelControlText";
			this.labelControlText.Size = new System.Drawing.Size(289, 82);
			this.labelControlText.TabIndex = 9;
			this.labelControlText.Text = "labelControl1";
			// 
			// pictureBoxImage
			// 
			this.pictureBoxImage.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.SharePageBig;
			this.pictureBoxImage.Location = new System.Drawing.Point(12, 12);
			this.pictureBoxImage.Name = "pictureBoxImage";
			this.pictureBoxImage.Size = new System.Drawing.Size(47, 50);
			this.pictureBoxImage.TabIndex = 10;
			this.pictureBoxImage.TabStop = false;
			// 
			// FormChangeAdStrategyWarning
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(366, 141);
			this.Controls.Add(this.pictureBoxImage);
			this.Controls.Add(this.labelControlText);
			this.Controls.Add(this.buttonXNo);
			this.Controls.Add(this.buttonXYes);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormChangeAdStrategyWarning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Change Pricing Strategy?";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXYes;
        private DevComponents.DotNetBar.ButtonX buttonXNo;
        public DevExpress.XtraEditors.LabelControl labelControlText;
        public System.Windows.Forms.PictureBox pictureBoxImage;
    }
}