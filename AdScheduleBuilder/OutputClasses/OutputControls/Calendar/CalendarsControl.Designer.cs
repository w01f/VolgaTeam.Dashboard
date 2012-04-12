namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class CalendarsControl
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
            this.pnEmpty = new System.Windows.Forms.Panel();
            this.pnMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnEmpty
            // 
            this.pnEmpty.BackColor = System.Drawing.Color.AliceBlue;
            this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmpty.Location = new System.Drawing.Point(0, 0);
            this.pnEmpty.Name = "pnEmpty";
            this.pnEmpty.Size = new System.Drawing.Size(453, 359);
            this.pnEmpty.TabIndex = 0;
            // 
            // pnMain
            // 
            this.pnMain.BackColor = System.Drawing.Color.AliceBlue;
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(453, 359);
            this.pnMain.TabIndex = 1;
            // 
            // CalendarsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnEmpty);
            this.Controls.Add(this.pnMain);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CalendarsControl";
            this.Size = new System.Drawing.Size(453, 359);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnEmpty;
        private System.Windows.Forms.Panel pnMain;
    }
}
