﻿namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class MonthViewControl
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
            this.pnHeader = new System.Windows.Forms.Panel();
            this.laSaturday = new System.Windows.Forms.Label();
            this.laFriday = new System.Windows.Forms.Label();
            this.laThursday = new System.Windows.Forms.Label();
            this.laWednesday = new System.Windows.Forms.Label();
            this.laTuesday = new System.Windows.Forms.Label();
            this.laMonday = new System.Windows.Forms.Label();
            this.laSunday = new System.Windows.Forms.Label();
            this.pnData = new System.Windows.Forms.Panel();
            this.pnHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnHeader.Controls.Add(this.laSaturday);
            this.pnHeader.Controls.Add(this.laFriday);
            this.pnHeader.Controls.Add(this.laThursday);
            this.pnHeader.Controls.Add(this.laWednesday);
            this.pnHeader.Controls.Add(this.laTuesday);
            this.pnHeader.Controls.Add(this.laMonday);
            this.pnHeader.Controls.Add(this.laSunday);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(1, 1);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pnHeader.Size = new System.Drawing.Size(554, 26);
            this.pnHeader.TabIndex = 0;
            this.pnHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // laSaturday
            // 
            this.laSaturday.Dock = System.Windows.Forms.DockStyle.Left;
            this.laSaturday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSaturday.Location = new System.Drawing.Point(416, 0);
            this.laSaturday.Name = "laSaturday";
            this.laSaturday.Size = new System.Drawing.Size(72, 25);
            this.laSaturday.TabIndex = 5;
            this.laSaturday.Text = "Saturday";
            this.laSaturday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laSaturday.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // laFriday
            // 
            this.laFriday.Dock = System.Windows.Forms.DockStyle.Left;
            this.laFriday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laFriday.Location = new System.Drawing.Point(344, 0);
            this.laFriday.Name = "laFriday";
            this.laFriday.Size = new System.Drawing.Size(72, 25);
            this.laFriday.TabIndex = 4;
            this.laFriday.Text = "Friday";
            this.laFriday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laFriday.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // laThursday
            // 
            this.laThursday.Dock = System.Windows.Forms.DockStyle.Left;
            this.laThursday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laThursday.Location = new System.Drawing.Point(272, 0);
            this.laThursday.Name = "laThursday";
            this.laThursday.Size = new System.Drawing.Size(72, 25);
            this.laThursday.TabIndex = 1;
            this.laThursday.Text = "Thursday";
            this.laThursday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laThursday.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // laWednesday
            // 
            this.laWednesday.Dock = System.Windows.Forms.DockStyle.Left;
            this.laWednesday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laWednesday.Location = new System.Drawing.Point(202, 0);
            this.laWednesday.Name = "laWednesday";
            this.laWednesday.Size = new System.Drawing.Size(70, 25);
            this.laWednesday.TabIndex = 2;
            this.laWednesday.Text = "Wednesday";
            this.laWednesday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laWednesday.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // laTuesday
            // 
            this.laTuesday.Dock = System.Windows.Forms.DockStyle.Left;
            this.laTuesday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTuesday.Location = new System.Drawing.Point(137, 0);
            this.laTuesday.Name = "laTuesday";
            this.laTuesday.Size = new System.Drawing.Size(65, 25);
            this.laTuesday.TabIndex = 3;
            this.laTuesday.Text = "Tuesday";
            this.laTuesday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laTuesday.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // laMonday
            // 
            this.laMonday.Dock = System.Windows.Forms.DockStyle.Left;
            this.laMonday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laMonday.Location = new System.Drawing.Point(72, 0);
            this.laMonday.Name = "laMonday";
            this.laMonday.Size = new System.Drawing.Size(65, 25);
            this.laMonday.TabIndex = 0;
            this.laMonday.Text = "Monday";
            this.laMonday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laMonday.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // laSunday
            // 
            this.laSunday.Dock = System.Windows.Forms.DockStyle.Left;
            this.laSunday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSunday.Location = new System.Drawing.Point(0, 0);
            this.laSunday.Name = "laSunday";
            this.laSunday.Size = new System.Drawing.Size(72, 25);
            this.laSunday.TabIndex = 6;
            this.laSunday.Text = "Sunday";
            this.laSunday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laSunday.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            // 
            // pnData
            // 
            this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData.Location = new System.Drawing.Point(1, 27);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(554, 281);
            this.pnData.TabIndex = 1;
            // 
            // MonthViewControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnData);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MonthViewControl";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(556, 309);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Weekdays_Paint);
            this.pnHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label laSunday;
        private System.Windows.Forms.Label laSaturday;
        private System.Windows.Forms.Label laFriday;
        private System.Windows.Forms.Label laThursday;
        private System.Windows.Forms.Label laWednesday;
        private System.Windows.Forms.Label laTuesday;
        private System.Windows.Forms.Label laMonday;
        private System.Windows.Forms.Panel pnData;
    }
}
