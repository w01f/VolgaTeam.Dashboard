namespace CommandCentral
{
    partial class DataControl
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
			this.buttonXSourceFile = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXSourceFile
			// 
			this.buttonXSourceFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSourceFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSourceFile.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXSourceFile.Location = new System.Drawing.Point(15, 16);
			this.buttonXSourceFile.Name = "buttonXSourceFile";
			this.buttonXSourceFile.Size = new System.Drawing.Size(159, 77);
			this.buttonXSourceFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSourceFile.TabIndex = 0;
			this.buttonXSourceFile.Text = "Closing\r\nSummary";
			this.buttonXSourceFile.TextColor = System.Drawing.Color.Black;
			this.buttonXSourceFile.Click += new System.EventHandler(this.buttonXSourceFile_Click);
			// 
			// DataControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.buttonXSourceFile);
			this.Name = "DataControl";
			this.Size = new System.Drawing.Size(669, 463);
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXSourceFile;
    }
}
