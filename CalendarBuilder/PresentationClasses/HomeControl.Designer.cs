namespace CalendarBuilder.PresentationClasses
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
            this.pbSimpleCalendar = new System.Windows.Forms.PictureBox();
            this.pbGraphicCalendar = new System.Windows.Forms.PictureBox();
            this.pbAdvancedCalendar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBetaTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSimpleCalendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraphicCalendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvancedCalendar)).BeginInit();
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
            // pbSimpleCalendar
            // 
            this.pbSimpleCalendar.Image = global::CalendarBuilder.Properties.Resources.HomeCalendar3;
            this.pbSimpleCalendar.Location = new System.Drawing.Point(16, 409);
            this.pbSimpleCalendar.Name = "pbSimpleCalendar";
            this.pbSimpleCalendar.Size = new System.Drawing.Size(726, 98);
            this.pbSimpleCalendar.TabIndex = 19;
            this.pbSimpleCalendar.TabStop = false;
            this.pbSimpleCalendar.Click += new System.EventHandler(this.pbSimpleCalendar_Click);
            this.pbSimpleCalendar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbSimpleCalendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbGraphicCalendar
            // 
            this.pbGraphicCalendar.Image = global::CalendarBuilder.Properties.Resources.HomeCalendar2;
            this.pbGraphicCalendar.Location = new System.Drawing.Point(28, 247);
            this.pbGraphicCalendar.Name = "pbGraphicCalendar";
            this.pbGraphicCalendar.Size = new System.Drawing.Size(726, 98);
            this.pbGraphicCalendar.TabIndex = 18;
            this.pbGraphicCalendar.TabStop = false;
            this.pbGraphicCalendar.Click += new System.EventHandler(this.pbGraphicCalendar_Click);
            this.pbGraphicCalendar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbGraphicCalendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // pbAdvancedCalendar
            // 
            this.pbAdvancedCalendar.Image = global::CalendarBuilder.Properties.Resources.HomeCalendar1;
            this.pbAdvancedCalendar.Location = new System.Drawing.Point(28, 85);
            this.pbAdvancedCalendar.Name = "pbAdvancedCalendar";
            this.pbAdvancedCalendar.Size = new System.Drawing.Size(726, 98);
            this.pbAdvancedCalendar.TabIndex = 17;
            this.pbAdvancedCalendar.TabStop = false;
            this.pbAdvancedCalendar.Click += new System.EventHandler(this.pbAdvancedCalendar_Click);
            this.pbAdvancedCalendar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pbAdvancedCalendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // HomeControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pbBetaTest);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbSimpleCalendar);
            this.Controls.Add(this.pbGraphicCalendar);
            this.Controls.Add(this.pbAdvancedCalendar);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(1005, 624);
            ((System.ComponentModel.ISupportInitialize)(this.pbBetaTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSimpleCalendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraphicCalendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdvancedCalendar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGraphicCalendar;
        private System.Windows.Forms.PictureBox pbAdvancedCalendar;
        private System.Windows.Forms.PictureBox pbSimpleCalendar;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbBetaTest;

    }
}
