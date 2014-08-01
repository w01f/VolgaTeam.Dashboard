namespace NewBizWiz.Dashboard.TabHomeForms
{
    partial class SimpleSummaryItemContainer
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
			this.pnMain = new DevExpress.XtraEditors.XtraScrollableControl();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Appearance.Options.UseBackColor = true;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(620, 282);
			this.pnMain.TabIndex = 0;
			// 
			// SimpleSummaryItemContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Name = "SimpleSummaryItemContainer";
			this.Size = new System.Drawing.Size(620, 282);
			this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.XtraScrollableControl pnMain;

    }
}
