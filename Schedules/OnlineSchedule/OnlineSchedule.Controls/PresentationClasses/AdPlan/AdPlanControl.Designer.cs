﻿namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	abstract partial class AdPlanControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdPlanControl));
			this.pnTopHeader = new System.Windows.Forms.Panel();
			this.pnOutputOptions = new System.Windows.Forms.Panel();
			this.checkEditLessSlides = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditMoreSlides = new DevExpress.XtraEditors.CheckEdit();
			this.labelOutputOptionsTitle = new System.Windows.Forms.Label();
			this.laAdvertiser = new System.Windows.Forms.Label();
			this.xtraTabControlProducts = new DevExpress.XtraTab.XtraTabControl();
			this.retractableBar = new NewBizWiz.CommonGUI.RetractableBar.RetractableBarLeft();
			this.pnTopHeader.SuspendLayout();
			this.pnOutputOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditLessSlides.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMoreSlides.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlProducts)).BeginInit();
			this.SuspendLayout();
			// 
			// pnTopHeader
			// 
			this.pnTopHeader.BackColor = System.Drawing.Color.White;
			this.pnTopHeader.Controls.Add(this.pnOutputOptions);
			this.pnTopHeader.Controls.Add(this.laAdvertiser);
			this.pnTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTopHeader.Location = new System.Drawing.Point(300, 0);
			this.pnTopHeader.Name = "pnTopHeader";
			this.pnTopHeader.Size = new System.Drawing.Size(496, 43);
			this.pnTopHeader.TabIndex = 3;
			// 
			// pnOutputOptions
			// 
			this.pnOutputOptions.Controls.Add(this.checkEditLessSlides);
			this.pnOutputOptions.Controls.Add(this.checkEditMoreSlides);
			this.pnOutputOptions.Controls.Add(this.labelOutputOptionsTitle);
			this.pnOutputOptions.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnOutputOptions.Location = new System.Drawing.Point(224, 0);
			this.pnOutputOptions.Name = "pnOutputOptions";
			this.pnOutputOptions.Size = new System.Drawing.Size(272, 43);
			this.pnOutputOptions.TabIndex = 4;
			// 
			// checkEditLessSlides
			// 
			this.checkEditLessSlides.Location = new System.Drawing.Point(161, 12);
			this.checkEditLessSlides.Name = "checkEditLessSlides";
			this.checkEditLessSlides.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditLessSlides.Properties.Appearance.Options.UseFont = true;
			this.checkEditLessSlides.Properties.Caption = "Less Slides";
			this.checkEditLessSlides.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditLessSlides.Properties.RadioGroupIndex = 1;
			this.checkEditLessSlides.Size = new System.Drawing.Size(101, 20);
			this.checkEditLessSlides.TabIndex = 2;
			this.checkEditLessSlides.TabStop = false;
			this.checkEditLessSlides.CheckedChanged += new System.EventHandler(this.checkEditMoreSlides_CheckedChanged);
			// 
			// checkEditMoreSlides
			// 
			this.checkEditMoreSlides.Location = new System.Drawing.Point(54, 12);
			this.checkEditMoreSlides.Name = "checkEditMoreSlides";
			this.checkEditMoreSlides.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditMoreSlides.Properties.Appearance.Options.UseFont = true;
			this.checkEditMoreSlides.Properties.Caption = "More Slides";
			this.checkEditMoreSlides.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditMoreSlides.Properties.RadioGroupIndex = 1;
			this.checkEditMoreSlides.Size = new System.Drawing.Size(101, 20);
			this.checkEditMoreSlides.TabIndex = 1;
			this.checkEditMoreSlides.TabStop = false;
			this.checkEditMoreSlides.CheckedChanged += new System.EventHandler(this.checkEditMoreSlides_CheckedChanged);
			// 
			// labelOutputOptionsTitle
			// 
			this.labelOutputOptionsTitle.AutoSize = true;
			this.labelOutputOptionsTitle.Location = new System.Drawing.Point(3, 14);
			this.labelOutputOptionsTitle.Name = "labelOutputOptionsTitle";
			this.labelOutputOptionsTitle.Size = new System.Drawing.Size(45, 16);
			this.labelOutputOptionsTitle.TabIndex = 0;
			this.labelOutputOptionsTitle.Text = "Fit on:";
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
			this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
			this.laAdvertiser.Name = "laAdvertiser";
			this.laAdvertiser.Size = new System.Drawing.Size(300, 43);
			this.laAdvertiser.TabIndex = 3;
			this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// xtraTabControlProducts
			// 
			this.xtraTabControlProducts.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.Appearance.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlProducts.Location = new System.Drawing.Point(300, 43);
			this.xtraTabControlProducts.Name = "xtraTabControlProducts";
			this.xtraTabControlProducts.Size = new System.Drawing.Size(496, 387);
			this.xtraTabControlProducts.TabIndex = 4;
			this.xtraTabControlProducts.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlProducts_SelectedPageChanged);
			// 
			// retractableBar
			// 
			this.retractableBar.AnimationDelay = 0;
			this.retractableBar.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBar.Content
			// 
			this.retractableBar.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBar.Content.Name = "Content";
			this.retractableBar.Content.Size = new System.Drawing.Size(296, 386);
			this.retractableBar.Content.TabIndex = 1;
			this.retractableBar.ContentSize = 300;
			this.retractableBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.retractableBar.Location = new System.Drawing.Point(0, 0);
			this.retractableBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBar.Name = "retractableBar";
			this.retractableBar.Size = new System.Drawing.Size(300, 430);
			this.retractableBar.TabIndex = 5;
			// 
			// AdPlanControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlProducts);
			this.Controls.Add(this.pnTopHeader);
			this.Controls.Add(this.retractableBar);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "AdPlanControl";
			this.Size = new System.Drawing.Size(796, 430);
			this.pnTopHeader.ResumeLayout(false);
			this.pnOutputOptions.ResumeLayout(false);
			this.pnOutputOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditLessSlides.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMoreSlides.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlProducts)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnTopHeader;
		protected DevExpress.XtraTab.XtraTabControl xtraTabControlProducts;
		private System.Windows.Forms.Label laAdvertiser;
		private System.Windows.Forms.Panel pnOutputOptions;
		private DevExpress.XtraEditors.CheckEdit checkEditLessSlides;
		private DevExpress.XtraEditors.CheckEdit checkEditMoreSlides;
		private System.Windows.Forms.Label labelOutputOptionsTitle;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBar;

    }
}