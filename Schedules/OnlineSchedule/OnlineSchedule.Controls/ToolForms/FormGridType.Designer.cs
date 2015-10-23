namespace Asa.OnlineSchedule.Controls.PresentationClasses.ToolForms
{
    partial class FormGridType
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
			this.laLogo = new System.Windows.Forms.Label();
			this.buttonXImage = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExcel = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// laLogo
			// 
			this.laLogo.BackColor = System.Drawing.Color.White;
			this.laLogo.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laLogo.ForeColor = System.Drawing.Color.Black;
			this.laLogo.Location = new System.Drawing.Point(14, 9);
			this.laLogo.Name = "laLogo";
			this.laLogo.Size = new System.Drawing.Size(318, 55);
			this.laLogo.TabIndex = 1;
			this.laLogo.Text = "How do you want to Output this Schedule to the PowerPoint Slide?";
			// 
			// buttonXImage
			// 
			this.buttonXImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXImage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXImage.DialogResult = System.Windows.Forms.DialogResult.No;
			this.buttonXImage.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXImage.Image = global::Asa.OnlineSchedule.Controls.Properties.Resources.Image;
			this.buttonXImage.Location = new System.Drawing.Point(18, 146);
			this.buttonXImage.Name = "buttonXImage";
			this.buttonXImage.Size = new System.Drawing.Size(314, 55);
			this.buttonXImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXImage.TabIndex = 3;
			this.buttonXImage.Text = "        As Image";
			this.buttonXImage.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXImage.TextColor = System.Drawing.Color.Black;
			this.buttonXImage.Tooltip = "Paste the Schedule as an IMAGE\r\nto the PowerPoint slide";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXCancel.Image = global::Asa.OnlineSchedule.Controls.Properties.Resources.Exit;
			this.buttonXCancel.ImageFixedSize = new System.Drawing.Size(42, 42);
			this.buttonXCancel.Location = new System.Drawing.Point(18, 216);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(314, 55);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "           Cancel";
			this.buttonXCancel.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			this.buttonXCancel.Tooltip = "Do NOTHING and Close this Window";
			// 
			// buttonXExcel
			// 
			this.buttonXExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExcel.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.buttonXExcel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXExcel.Image = global::Asa.OnlineSchedule.Controls.Properties.Resources.Excel;
			this.buttonXExcel.Location = new System.Drawing.Point(18, 77);
			this.buttonXExcel.Name = "buttonXExcel";
			this.buttonXExcel.Size = new System.Drawing.Size(314, 55);
			this.buttonXExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExcel.TabIndex = 1;
			this.buttonXExcel.Text = "         As Excel";
			this.buttonXExcel.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXExcel.TextColor = System.Drawing.Color.Black;
			this.buttonXExcel.Tooltip = "Embed an Excel Grid \r\nto the PowerPoint slide";
			// 
			// FormGridType
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(352, 286);
			this.Controls.Add(this.buttonXImage);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXExcel);
			this.Controls.Add(this.laLogo);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormGridType";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Grid Output Format";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laLogo;
        private DevComponents.DotNetBar.ButtonX buttonXExcel;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXImage;
    }
}