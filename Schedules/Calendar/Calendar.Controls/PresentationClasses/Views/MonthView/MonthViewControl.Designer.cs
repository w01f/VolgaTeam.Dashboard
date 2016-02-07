using Asa.Business.Common.Interfaces;

namespace Asa.Calendar.Controls.PresentationClasses.Views.MonthView
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(1000, 500);
			this.pnMain.TabIndex = 2;
			// 
			// pnEmpty
			// 
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(0, 0);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(1000, 500);
			this.pnEmpty.TabIndex = 3;
			// 
			// MonthViewControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnEmpty);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "MonthViewControl";
			this.Size = new System.Drawing.Size(1000, 500);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnEmpty;
    }
}
