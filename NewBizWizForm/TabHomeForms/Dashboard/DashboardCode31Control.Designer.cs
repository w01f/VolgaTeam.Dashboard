namespace NewBizWizForm.TabHomeForms.Dashboard
{
    partial class DashboardCode31Control
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
            this.components = new System.ComponentModel.Container();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.pnSalesDepotBig = new System.Windows.Forms.Panel();
            this.pnClientSolutionBig = new System.Windows.Forms.Panel();
            this.pbSalesDepotBig = new System.Windows.Forms.PictureBox();
            this.pbClientSolutionBig = new System.Windows.Forms.PictureBox();
            this.pnSalesDepotBig.SuspendLayout();
            this.pnClientSolutionBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesDepotBig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientSolutionBig)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnSalesDepotBig
            // 
            this.pnSalesDepotBig.Controls.Add(this.pbSalesDepotBig);
            this.pnSalesDepotBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSalesDepotBig.Location = new System.Drawing.Point(0, 230);
            this.pnSalesDepotBig.Name = "pnSalesDepotBig";
            this.pnSalesDepotBig.Size = new System.Drawing.Size(920, 230);
            this.pnSalesDepotBig.TabIndex = 17;
            // 
            // pnClientSolutionBig
            // 
            this.pnClientSolutionBig.Controls.Add(this.pbClientSolutionBig);
            this.pnClientSolutionBig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnClientSolutionBig.Location = new System.Drawing.Point(0, 0);
            this.pnClientSolutionBig.Name = "pnClientSolutionBig";
            this.pnClientSolutionBig.Size = new System.Drawing.Size(920, 230);
            this.pnClientSolutionBig.TabIndex = 18;
            // 
            // pbSalesDepotBig
            // 
            this.pbSalesDepotBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSalesDepotBig.Image = global::NewBizWizForm.Properties.Resources.HomeSalesDepotBig;
            this.pbSalesDepotBig.Location = new System.Drawing.Point(26, 34);
            this.pbSalesDepotBig.Name = "pbSalesDepotBig";
            this.pbSalesDepotBig.Size = new System.Drawing.Size(864, 161);
            this.pbSalesDepotBig.TabIndex = 4;
            this.pbSalesDepotBig.TabStop = false;
            this.pbSalesDepotBig.Click += new System.EventHandler(this.pbSalesDepotBig_Click);
            this.pbSalesDepotBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbSalesDepotBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbClientSolutionBig
            // 
            this.pbClientSolutionBig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbClientSolutionBig.Image = global::NewBizWizForm.Properties.Resources.HomeClientSolutionBig;
            this.pbClientSolutionBig.Location = new System.Drawing.Point(26, 40);
            this.pbClientSolutionBig.Name = "pbClientSolutionBig";
            this.pbClientSolutionBig.Size = new System.Drawing.Size(864, 156);
            this.pbClientSolutionBig.TabIndex = 3;
            this.pbClientSolutionBig.TabStop = false;
            this.pbClientSolutionBig.Click += new System.EventHandler(this.pbClientSolutionBig_Click);
            this.pbClientSolutionBig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbClientSolutionBig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // DashboardCode31Control
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnSalesDepotBig);
            this.Controls.Add(this.pnClientSolutionBig);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DashboardCode31Control";
            this.Size = new System.Drawing.Size(920, 458);
            this.pnSalesDepotBig.ResumeLayout(false);
            this.pnClientSolutionBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesDepotBig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientSolutionBig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnSalesDepotBig;
        private System.Windows.Forms.PictureBox pbSalesDepotBig;
        private System.Windows.Forms.Panel pnClientSolutionBig;
        private System.Windows.Forms.PictureBox pbClientSolutionBig;
    }
}
