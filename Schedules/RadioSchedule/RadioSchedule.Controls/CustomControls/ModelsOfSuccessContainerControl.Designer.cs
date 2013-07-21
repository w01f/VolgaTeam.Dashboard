namespace RadioScheduleBuilder.CustomControls
{
    partial class ModelsOfSuccessContainerControl
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
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.laTitle = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(503, 270);
            this.xtraScrollableControl.TabIndex = 0;
            // 
            // laTitle
            // 
            this.laTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.laTitle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(0, 0);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(507, 54);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "Click on a link below to view the video online";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMain.Controls.Add(this.xtraScrollableControl);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 54);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(507, 274);
            this.pnMain.TabIndex = 2;
            // 
            // ModelsOfSuccessContainerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.laTitle);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ModelsOfSuccessContainerControl";
            this.Size = new System.Drawing.Size(507, 328);
            this.Load += new System.EventHandler(this.ModelsOfSuccessContainerControl_Load);
            this.Resize += new System.EventHandler(this.ModelsOfSuccessContainerControl_Resize);
            this.pnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        public DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.Panel pnMain;
    }
}
