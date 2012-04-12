namespace NewBizWizForm.TabHomeForms
{
    partial class SimpleSummaryOutputControl
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
            this.ckDetails = new System.Windows.Forms.CheckBox();
            this.ckItem = new System.Windows.Forms.CheckBox();
            this.ckMonthly = new System.Windows.Forms.CheckBox();
            this.ckTotal = new System.Windows.Forms.CheckBox();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.pnMonthly = new System.Windows.Forms.Panel();
            this.pnTotal = new System.Windows.Forms.Panel();
            this.pnDetails = new System.Windows.Forms.Panel();
            this.panelExMain.SuspendLayout();
            this.pnHeader.SuspendLayout();
            this.pnMonthly.SuspendLayout();
            this.pnTotal.SuspendLayout();
            this.pnDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelExMain
            // 
            this.panelExMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelExMain.Controls.Add(this.ckDetails);
            this.panelExMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExMain.Location = new System.Drawing.Point(5, 0);
            this.panelExMain.Name = "panelExMain";
            this.panelExMain.Size = new System.Drawing.Size(621, 49);
            this.panelExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.panelExMain.Style.BorderColor.Color = System.Drawing.Color.White;
            this.panelExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExMain.Style.GradientAngle = 90;
            this.panelExMain.TabIndex = 30;
            // 
            // ckDetails
            // 
            this.ckDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ckDetails.Checked = true;
            this.ckDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckDetails.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckDetails.Location = new System.Drawing.Point(3, 3);
            this.ckDetails.Name = "ckDetails";
            this.ckDetails.Size = new System.Drawing.Size(615, 43);
            this.ckDetails.TabIndex = 33;
            this.ckDetails.UseVisualStyleBackColor = true;
            this.ckDetails.CheckedChanged += new System.EventHandler(this.ckItem_CheckedChanged);
            // 
            // ckItem
            // 
            this.ckItem.Checked = true;
            this.ckItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.ckItem.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckItem.Location = new System.Drawing.Point(0, 0);
            this.ckItem.Name = "ckItem";
            this.ckItem.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ckItem.Size = new System.Drawing.Size(243, 51);
            this.ckItem.TabIndex = 31;
            this.ckItem.UseVisualStyleBackColor = true;
            this.ckItem.CheckedChanged += new System.EventHandler(this.ckItem_CheckedChanged);
            // 
            // ckMonthly
            // 
            this.ckMonthly.Checked = true;
            this.ckMonthly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckMonthly.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckMonthly.Location = new System.Drawing.Point(0, 0);
            this.ckMonthly.Name = "ckMonthly";
            this.ckMonthly.Size = new System.Drawing.Size(137, 51);
            this.ckMonthly.TabIndex = 32;
            this.ckMonthly.Text = "$0.00";
            this.ckMonthly.UseVisualStyleBackColor = true;
            this.ckMonthly.CheckedChanged += new System.EventHandler(this.ckItem_CheckedChanged);
            // 
            // ckTotal
            // 
            this.ckTotal.Checked = true;
            this.ckTotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ckTotal.Location = new System.Drawing.Point(0, 0);
            this.ckTotal.Name = "ckTotal";
            this.ckTotal.Size = new System.Drawing.Size(144, 51);
            this.ckTotal.TabIndex = 33;
            this.ckTotal.Text = "$0.00";
            this.ckTotal.UseVisualStyleBackColor = true;
            this.ckTotal.CheckedChanged += new System.EventHandler(this.ckItem_CheckedChanged);
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnHeader.Controls.Add(this.pnMonthly);
            this.pnHeader.Controls.Add(this.pnTotal);
            this.pnHeader.Controls.Add(this.ckItem);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(631, 51);
            this.pnHeader.TabIndex = 43;
            // 
            // pnMonthly
            // 
            this.pnMonthly.Controls.Add(this.ckMonthly);
            this.pnMonthly.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnMonthly.Location = new System.Drawing.Point(350, 0);
            this.pnMonthly.Name = "pnMonthly";
            this.pnMonthly.Size = new System.Drawing.Size(137, 51);
            this.pnMonthly.TabIndex = 35;
            // 
            // pnTotal
            // 
            this.pnTotal.Controls.Add(this.ckTotal);
            this.pnTotal.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnTotal.Location = new System.Drawing.Point(487, 0);
            this.pnTotal.Name = "pnTotal";
            this.pnTotal.Size = new System.Drawing.Size(144, 51);
            this.pnTotal.TabIndex = 34;
            // 
            // pnDetails
            // 
            this.pnDetails.Controls.Add(this.panelExMain);
            this.pnDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDetails.Location = new System.Drawing.Point(0, 51);
            this.pnDetails.Name = "pnDetails";
            this.pnDetails.Padding = new System.Windows.Forms.Padding(5, 0, 5, 10);
            this.pnDetails.Size = new System.Drawing.Size(631, 59);
            this.pnDetails.TabIndex = 44;
            // 
            // SimpleSummaryOutputControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnDetails);
            this.Controls.Add(this.pnHeader);
            this.Name = "SimpleSummaryOutputControl";
            this.Size = new System.Drawing.Size(631, 110);
            this.panelExMain.ResumeLayout(false);
            this.pnHeader.ResumeLayout(false);
            this.pnMonthly.ResumeLayout(false);
            this.pnTotal.ResumeLayout(false);
            this.pnDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelExMain;
        private System.Windows.Forms.CheckBox ckItem;
        private System.Windows.Forms.CheckBox ckDetails;
        public System.Windows.Forms.Panel pnMonthly;
        public System.Windows.Forms.Panel pnTotal;
        private System.Windows.Forms.CheckBox ckMonthly;
        private System.Windows.Forms.CheckBox ckTotal;
        public System.Windows.Forms.Panel pnDetails;
        public System.Windows.Forms.Panel pnHeader;
    }
}
