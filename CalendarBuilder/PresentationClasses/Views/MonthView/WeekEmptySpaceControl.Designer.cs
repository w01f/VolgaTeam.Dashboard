namespace CalendarBuilder.PresentationClasses.Views.MonthView
{
    partial class WeekEmptySpaceControl
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
            this.SuspendLayout();
            // 
            // WeekEmptySpaceControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Name = "WeekEmptySpaceControl";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(312, 154);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WeekEmptySpaceControl_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
