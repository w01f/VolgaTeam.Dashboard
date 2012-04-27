namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class DayControl
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
            this.laSmallDayCaption = new System.Windows.Forms.Label();
            this.pnData = new System.Windows.Forms.Panel();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pnData.SuspendLayout();
            this.SuspendLayout();
            // 
            // laSmallDayCaption
            // 
            this.laSmallDayCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.laSmallDayCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.laSmallDayCaption.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSmallDayCaption.Location = new System.Drawing.Point(1, 1);
            this.laSmallDayCaption.Name = "laSmallDayCaption";
            this.laSmallDayCaption.Size = new System.Drawing.Size(274, 21);
            this.laSmallDayCaption.TabIndex = 0;
            this.laSmallDayCaption.Text = "label1";
            // 
            // pnData
            // 
            this.pnData.Controls.Add(this.xtraScrollableControl);
            this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData.Location = new System.Drawing.Point(1, 22);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(274, 224);
            this.pnData.TabIndex = 1;
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
            this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(274, 224);
            this.xtraScrollableControl.TabIndex = 0;
            this.xtraScrollableControl.DoubleClick += new System.EventHandler(this.xtraScrollableControl_DoubleClick);
            // 
            // DayControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnData);
            this.Controls.Add(this.laSmallDayCaption);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Name = "DayControl";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(276, 247);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DayControl_Paint);
            this.pnData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laSmallDayCaption;
        private System.Windows.Forms.Panel pnData;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;

    }
}
