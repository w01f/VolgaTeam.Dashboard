namespace CalendarBuilder.ToolForms
{
    partial class FormSelectOutput
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.laTitle = new System.Windows.Forms.Label();
            this.buttonXGrid = new DevComponents.DotNetBar.ButtonX();
            this.buttonXExcel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXImage = new DevComponents.DotNetBar.ButtonX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbExcel = new System.Windows.Forms.PictureBox();
            this.pbGrid = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(7, 9);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(502, 70);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "How do you want your Calendar to convert to PowerPoint?";
            // 
            // buttonXGrid
            // 
            this.buttonXGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXGrid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXGrid.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonXGrid.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXGrid.ForeColor = System.Drawing.Color.Black;
            this.buttonXGrid.Location = new System.Drawing.Point(106, 92);
            this.buttonXGrid.Name = "buttonXGrid";
            this.buttonXGrid.Size = new System.Drawing.Size(391, 72);
            this.buttonXGrid.TabIndex = 9;
            this.buttonXGrid.Text = "Output <b>TABLE</b> format";
            this.buttonXGrid.Tooltip = "Paste the Schedule as a Microsoft TABLE\r\nto the PowerPoint slide";
            // 
            // buttonXExcel
            // 
            this.buttonXExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXExcel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonXExcel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXExcel.ForeColor = System.Drawing.Color.Black;
            this.buttonXExcel.Location = new System.Drawing.Point(106, 172);
            this.buttonXExcel.Name = "buttonXExcel";
            this.buttonXExcel.Size = new System.Drawing.Size(391, 72);
            this.buttonXExcel.TabIndex = 12;
            this.buttonXExcel.Text = "Output <b>EXCEL</b> format";
            this.buttonXExcel.Tooltip = "Embed an Excel Grid\r\nto the PowerPoint slide";
            // 
            // buttonXImage
            // 
            this.buttonXImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXImage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXImage.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.buttonXImage.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXImage.ForeColor = System.Drawing.Color.Black;
            this.buttonXImage.Location = new System.Drawing.Point(106, 253);
            this.buttonXImage.Name = "buttonXImage";
            this.buttonXImage.Size = new System.Drawing.Size(391, 72);
            this.buttonXImage.TabIndex = 14;
            this.buttonXImage.Text = "Output <b>IMAGE</b> format";
            this.buttonXImage.Tooltip = "Paste the Schedule as an IMAGE\r\nto the PowerPoint slide";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CalendarBuilder.Properties.Resources.Image;
            this.pictureBox1.Location = new System.Drawing.Point(12, 253);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pbExcel
            // 
            this.pbExcel.Image = global::CalendarBuilder.Properties.Resources.Excel;
            this.pbExcel.Location = new System.Drawing.Point(12, 172);
            this.pbExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbExcel.Name = "pbExcel";
            this.pbExcel.Size = new System.Drawing.Size(72, 72);
            this.pbExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExcel.TabIndex = 11;
            this.pbExcel.TabStop = false;
            // 
            // pbGrid
            // 
            this.pbGrid.Image = global::CalendarBuilder.Properties.Resources.Grid;
            this.pbGrid.Location = new System.Drawing.Point(12, 92);
            this.pbGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbGrid.Name = "pbGrid";
            this.pbGrid.Size = new System.Drawing.Size(72, 72);
            this.pbGrid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGrid.TabIndex = 0;
            this.pbGrid.TabStop = false;
            // 
            // FormSelectOutput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(521, 341);
            this.Controls.Add(this.buttonXImage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonXExcel);
            this.Controls.Add(this.pbExcel);
            this.Controls.Add(this.buttonXGrid);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbGrid);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectOutput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slide Output Options";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGrid;
        private System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbExcel;
        private DevComponents.DotNetBar.ButtonX buttonXExcel;
        public DevComponents.DotNetBar.ButtonX buttonXGrid;
        private DevComponents.DotNetBar.ButtonX buttonXImage;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}