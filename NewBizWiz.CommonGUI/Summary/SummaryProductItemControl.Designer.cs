namespace NewBizWiz.CommonGUI.Summary
{
	partial class SummaryProductItemControl
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
			this.panelExMain = new DevComponents.DotNetBar.PanelEx();
			this.laTitle = new System.Windows.Forms.Label();
			this.ckItem = new System.Windows.Forms.CheckBox();
			this.ckDetails = new System.Windows.Forms.CheckBox();
			this.memoEditDetails = new DevExpress.XtraEditors.MemoEdit();
			this.toolTipController = new DevExpress.Utils.ToolTipController();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.panelExMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditDetails.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// panelExMain
			// 
			this.panelExMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelExMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelExMain.Controls.Add(this.laTitle);
			this.panelExMain.Controls.Add(this.ckItem);
			this.panelExMain.Controls.Add(this.ckDetails);
			this.panelExMain.Controls.Add(this.memoEditDetails);
			this.panelExMain.Location = new System.Drawing.Point(45, 21);
			this.panelExMain.Name = "panelExMain";
			this.panelExMain.Size = new System.Drawing.Size(543, 99);
			this.panelExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelExMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelExMain.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.panelExMain.Style.BorderColor.Color = System.Drawing.Color.White;
			this.panelExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelExMain.Style.GradientAngle = 90;
			this.panelExMain.TabIndex = 30;
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(30, 10);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(507, 25);
			this.laTitle.TabIndex = 38;
			this.laTitle.Text = "label1";
			// 
			// ckItem
			// 
			this.ckItem.AutoSize = true;
			this.ckItem.Checked = true;
			this.ckItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckItem.Location = new System.Drawing.Point(9, 11);
			this.ckItem.Name = "ckItem";
			this.ckItem.Size = new System.Drawing.Size(15, 14);
			this.ckItem.TabIndex = 37;
			this.ckItem.UseVisualStyleBackColor = true;
			this.ckItem.CheckedChanged += new System.EventHandler(this.ckItem_CheckedChanged);
			// 
			// ckDetails
			// 
			this.ckDetails.AutoSize = true;
			this.ckDetails.Checked = true;
			this.ckDetails.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckDetails.Location = new System.Drawing.Point(9, 40);
			this.ckDetails.Name = "ckDetails";
			this.ckDetails.Size = new System.Drawing.Size(15, 14);
			this.ckDetails.TabIndex = 36;
			this.ckDetails.UseVisualStyleBackColor = true;
			this.ckDetails.CheckedChanged += new System.EventHandler(this.ckDetails_CheckedChanged);
			// 
			// memoEditDetails
			// 
			this.memoEditDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditDetails.Location = new System.Drawing.Point(33, 38);
			this.memoEditDetails.Name = "memoEditDetails";
			this.memoEditDetails.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.memoEditDetails.Properties.Appearance.Options.UseFont = true;
			this.memoEditDetails.Properties.NullText = "Brief Overview...";
			this.memoEditDetails.Size = new System.Drawing.Size(504, 54);
			this.memoEditDetails.TabIndex = 3;
			this.memoEditDetails.EditValueChanged += new System.EventHandler(this.memoEditDetails_EditValueChanged);
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipLocation = DevExpress.Utils.ToolTipLocation.RightTop;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Location = new System.Drawing.Point(7, 21);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxLogo.TabIndex = 31;
			this.pictureBoxLogo.TabStop = false;
			// 
			// SummaryProductItemControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.panelExMain);
			this.Name = "SummaryProductItemControl";
			this.Size = new System.Drawing.Size(595, 140);
			this.panelExMain.ResumeLayout(false);
			this.panelExMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditDetails.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.PanelEx panelExMain;
        private DevExpress.XtraEditors.MemoEdit memoEditDetails;
        private System.Windows.Forms.CheckBox ckItem;
        public System.Windows.Forms.CheckBox ckDetails;
		private DevExpress.Utils.ToolTipController toolTipController;
		private System.Windows.Forms.Label laTitle;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}
