namespace Asa.OnlineSchedule.Controls.PresentationClasses
{
    partial class ScheduleSettingsControl
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
			this.xtraTabControlProducts = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageDigitalProducts = new DevExpress.XtraTab.XtraTabPage();
			this.digitalProductListControl = new Asa.OnlineSchedule.Controls.PresentationClasses.DigitalProductListControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlProducts)).BeginInit();
			this.xtraTabControlProducts.SuspendLayout();
			this.xtraTabPageDigitalProducts.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtraTabControlProducts
			// 
			this.xtraTabControlProducts.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.Appearance.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlProducts.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlProducts.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlProducts.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlProducts.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlProducts.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlProducts.Name = "xtraTabControlProducts";
			this.xtraTabControlProducts.SelectedTabPage = this.xtraTabPageDigitalProducts;
			this.xtraTabControlProducts.Size = new System.Drawing.Size(828, 430);
			this.xtraTabControlProducts.TabIndex = 2;
			this.xtraTabControlProducts.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageDigitalProducts});
			// 
			// xtraTabPageDigitalProducts
			// 
			this.xtraTabPageDigitalProducts.Controls.Add(this.digitalProductListControl);
			this.xtraTabPageDigitalProducts.Name = "xtraTabPageDigitalProducts";
			this.xtraTabPageDigitalProducts.Size = new System.Drawing.Size(822, 399);
			this.xtraTabPageDigitalProducts.Text = "Digital Products";
			// 
			// digitalProductListControl
			// 
			this.digitalProductListControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.digitalProductListControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.digitalProductListControl.Location = new System.Drawing.Point(0, 0);
			this.digitalProductListControl.Logo = global::Asa.OnlineSchedule.Controls.Properties.Resources.AppLogo;
			this.digitalProductListControl.Name = "digitalProductListControl";
			this.digitalProductListControl.Size = new System.Drawing.Size(822, 399);
			this.digitalProductListControl.TabIndex = 0;
			// 
			// ScheduleSettingsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlProducts);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ScheduleSettingsControl";
			this.Size = new System.Drawing.Size(828, 430);
			this.Load += new System.EventHandler(this.ScheduleSettingsControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlProducts)).EndInit();
			this.xtraTabControlProducts.ResumeLayout(false);
			this.xtraTabPageDigitalProducts.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControlProducts;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageDigitalProducts;
		private DigitalProductListControl digitalProductListControl;

    }
}
