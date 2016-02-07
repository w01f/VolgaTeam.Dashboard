namespace Asa.Common.GUI.RateCard
{
    partial class RateFolderControl
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
			this.xtraTabControlRateCards = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlRateCards)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraTabControlRateCards
			// 
			this.xtraTabControlRateCards.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlRateCards.Appearance.Options.UseFont = true;
			this.xtraTabControlRateCards.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlRateCards.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlRateCards.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlRateCards.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlRateCards.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlRateCards.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlRateCards.Name = "xtraTabControlRateCards";
			this.xtraTabControlRateCards.Size = new System.Drawing.Size(737, 430);
			this.xtraTabControlRateCards.TabIndex = 3;
			// 
			// RateFolderControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlRateCards);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "RateFolderControl";
			this.Size = new System.Drawing.Size(737, 430);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlRateCards)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		public DevExpress.XtraTab.XtraTabControl xtraTabControlRateCards;

    }
}
