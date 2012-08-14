namespace TVScheduleBuilder.CustomControls
{
    partial class StationsControl
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
            this.checkedListBoxControl = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.panelExTop = new DevComponents.DotNetBar.PanelEx();
            this.laTitle = new System.Windows.Forms.Label();
            this.panelExMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).BeginInit();
            this.panelExTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelExMain
            // 
            this.panelExMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelExMain.Controls.Add(this.checkedListBoxControl);
            this.panelExMain.Controls.Add(this.panelExTop);
            this.panelExMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExMain.Location = new System.Drawing.Point(3, 3);
            this.panelExMain.Name = "panelExMain";
            this.panelExMain.Size = new System.Drawing.Size(657, 310);
            this.panelExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.BorderColor.Color = System.Drawing.Color.White;
            this.panelExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExMain.Style.GradientAngle = 90;
            this.panelExMain.TabIndex = 2;
            // 
            // checkedListBoxControl
            // 
            this.checkedListBoxControl.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.checkedListBoxControl.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxControl.Appearance.ForeColor = System.Drawing.Color.White;
            this.checkedListBoxControl.Appearance.Options.UseBackColor = true;
            this.checkedListBoxControl.Appearance.Options.UseFont = true;
            this.checkedListBoxControl.Appearance.Options.UseForeColor = true;
            this.checkedListBoxControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.checkedListBoxControl.CheckOnClick = true;
            this.checkedListBoxControl.ColumnWidth = 200;
            this.checkedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxControl.ItemHeight = 35;
            this.checkedListBoxControl.Location = new System.Drawing.Point(0, 40);
            this.checkedListBoxControl.MultiColumn = true;
            this.checkedListBoxControl.Name = "checkedListBoxControl";
            this.checkedListBoxControl.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxControl.Size = new System.Drawing.Size(657, 270);
            this.checkedListBoxControl.TabIndex = 9;
            this.checkedListBoxControl.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControl_ItemCheck);
            // 
            // panelExTop
            // 
            this.panelExTop.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExTop.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelExTop.Controls.Add(this.laTitle);
            this.panelExTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExTop.Location = new System.Drawing.Point(0, 0);
            this.panelExTop.Name = "panelExTop";
            this.panelExTop.Padding = new System.Windows.Forms.Padding(10, 1, 1, 1);
            this.panelExTop.Size = new System.Drawing.Size(657, 40);
            this.panelExTop.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExTop.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExTop.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExTop.Style.BorderColor.Color = System.Drawing.Color.White;
            this.panelExTop.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExTop.Style.GradientAngle = 90;
            this.panelExTop.TabIndex = 6;
            // 
            // laTitle
            // 
            this.laTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.ForeColor = System.Drawing.Color.White;
            this.laTitle.Location = new System.Drawing.Point(10, 1);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(646, 38);
            this.laTitle.TabIndex = 2;
            this.laTitle.Text = "Stations:";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StationsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panelExMain);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "StationsControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(663, 316);
            this.panelExMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).EndInit();
            this.panelExTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelExMain;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl;
        private DevComponents.DotNetBar.PanelEx panelExTop;
        private System.Windows.Forms.Label laTitle;
    }
}
