namespace CalendarBuilder.CustomControls
{
    partial class HomeControl
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
            this.laTitle = new System.Windows.Forms.Label();
            this.pbBetaTest = new System.Windows.Forms.PictureBox();
            this.pbCalendar3 = new System.Windows.Forms.PictureBox();
            this.pbCalendar2 = new System.Windows.Forms.PictureBox();
            this.pbCalendar1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBetaTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar1)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.ForeColor = System.Drawing.Color.White;
            this.laTitle.Location = new System.Drawing.Point(28, 12);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(959, 70);
            this.laTitle.TabIndex = 23;
            this.laTitle.Text = "What kind of Ad Calendar do you want to BUILD?";
            this.laTitle.UseMnemonic = false;
            // 
            // pbBetaTest
            // 
            this.pbBetaTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBetaTest.Image = global::CalendarBuilder.Properties.Resources.BetaTest;
            this.pbBetaTest.Location = new System.Drawing.Point(779, 538);
            this.pbBetaTest.Name = "pbBetaTest";
            this.pbBetaTest.Size = new System.Drawing.Size(223, 72);
            this.pbBetaTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBetaTest.TabIndex = 24;
            this.pbBetaTest.TabStop = false;
            // 
            // pbCalendar3
            // 
            this.pbCalendar3.Image = global::CalendarBuilder.Properties.Resources.HomeCalendar3;
            this.pbCalendar3.Location = new System.Drawing.Point(28, 361);
            this.pbCalendar3.Name = "pbCalendar3";
            this.pbCalendar3.Size = new System.Drawing.Size(726, 123);
            this.pbCalendar3.TabIndex = 19;
            this.pbCalendar3.TabStop = false;
            this.pbCalendar3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbCalendar3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbCalendar2
            // 
            this.pbCalendar2.Image = global::CalendarBuilder.Properties.Resources.HomeCalendar2;
            this.pbCalendar2.Location = new System.Drawing.Point(28, 223);
            this.pbCalendar2.Name = "pbCalendar2";
            this.pbCalendar2.Size = new System.Drawing.Size(726, 123);
            this.pbCalendar2.TabIndex = 18;
            this.pbCalendar2.TabStop = false;
            this.pbCalendar2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbCalendar2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbCalendar1
            // 
            this.pbCalendar1.Image = global::CalendarBuilder.Properties.Resources.HomeCalendar1;
            this.pbCalendar1.Location = new System.Drawing.Point(28, 85);
            this.pbCalendar1.Name = "pbCalendar1";
            this.pbCalendar1.Size = new System.Drawing.Size(726, 123);
            this.pbCalendar1.TabIndex = 17;
            this.pbCalendar1.TabStop = false;
            this.pbCalendar1.Click += new System.EventHandler(this.pbCalendar1_Click);
            this.pbCalendar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbCalendar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // HomeControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pbBetaTest);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbCalendar3);
            this.Controls.Add(this.pbCalendar2);
            this.Controls.Add(this.pbCalendar1);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(1005, 624);
            ((System.ComponentModel.ISupportInitialize)(this.pbBetaTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalendar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCalendar2;
        private System.Windows.Forms.PictureBox pbCalendar1;
        private System.Windows.Forms.PictureBox pbCalendar3;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbBetaTest;

    }
}
