namespace CommandCentral.TabSalesDepotForms
{
    partial class SalesDepotMainPage
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.laTitle = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 72);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(665, 389);
			this.pnMain.TabIndex = 0;
			// 
			// laTitle
			// 
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(0, 0);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(665, 72);
			this.laTitle.TabIndex = 2;
			this.laTitle.Text = "Sales Depot Files";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SalesDepotMainPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.laTitle);
			this.Name = "SalesDepotMainPage";
			this.Size = new System.Drawing.Size(665, 461);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Label laTitle;
    }
}
