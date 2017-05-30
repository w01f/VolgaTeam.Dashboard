namespace Asa.Dashboard.TabSlides
{
    partial class TabSlidesMainPage
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
			this.laSlideSize = new System.Windows.Forms.Label();
			this.pnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Controls.Add(this.laSlideSize);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(894, 560);
			this.pnMain.TabIndex = 3;
			// 
			// laSlideSize
			// 
			this.laSlideSize.Dock = System.Windows.Forms.DockStyle.Top;
			this.laSlideSize.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSlideSize.ForeColor = System.Drawing.Color.Black;
			this.laSlideSize.Location = new System.Drawing.Point(0, 0);
			this.laSlideSize.Name = "laSlideSize";
			this.laSlideSize.Size = new System.Drawing.Size(894, 31);
			this.laSlideSize.TabIndex = 1;
			this.laSlideSize.Text = "Slide Size: {0}";
			this.laSlideSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TabSlidesMainPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Name = "TabSlidesMainPage";
			this.Size = new System.Drawing.Size(894, 560);
			this.pnMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Label laSlideSize;


	}
}
