namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
    partial class OutputSnapshotContainer
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
			this.pnLeftColumn = new System.Windows.Forms.Panel();
			this.pnRightColumn = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnLeftColumn
			// 
			this.pnLeftColumn.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnLeftColumn.Location = new System.Drawing.Point(0, 0);
			this.pnLeftColumn.Name = "pnLeftColumn";
			this.pnLeftColumn.Size = new System.Drawing.Size(268, 524);
			this.pnLeftColumn.TabIndex = 0;
			// 
			// pnRightColumn
			// 
			this.pnRightColumn.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnRightColumn.Location = new System.Drawing.Point(268, 0);
			this.pnRightColumn.Name = "pnRightColumn";
			this.pnRightColumn.Size = new System.Drawing.Size(286, 524);
			this.pnRightColumn.TabIndex = 1;
			// 
			// OutputSnapshotContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnRightColumn);
			this.Controls.Add(this.pnLeftColumn);
			this.Name = "OutputSnapshotContainer";
			this.Size = new System.Drawing.Size(557, 524);
			this.Load += new System.EventHandler(this.OutputSnapshotContainer_Load);
			this.SizeChanged += new System.EventHandler(this.OutputSnapshotContainer_SizeChanged);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnLeftColumn;
        private System.Windows.Forms.Panel pnRightColumn;
    }
}
