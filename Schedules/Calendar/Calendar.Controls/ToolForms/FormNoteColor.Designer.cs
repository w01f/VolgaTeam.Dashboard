namespace Asa.Calendar.Controls.ToolForms
{
    partial class FormNoteColor
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
			this.buttonXShow = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.pnSelectedColor = new System.Windows.Forms.Panel();
			this.laSelectedColor = new System.Windows.Forms.Label();
			this.checkBoxApplyForAll = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// buttonXShow
			// 
			this.buttonXShow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShow.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXShow.Location = new System.Drawing.Point(12, 129);
			this.buttonXShow.Name = "buttonXShow";
			this.buttonXShow.Size = new System.Drawing.Size(131, 32);
			this.buttonXShow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShow.TabIndex = 9;
			this.buttonXShow.Text = "OK";
			this.buttonXShow.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXOK.Location = new System.Drawing.Point(171, 129);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(131, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 10;
			this.buttonXOK.Text = "Cancel";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// pnSelectedColor
			// 
			this.pnSelectedColor.BackColor = System.Drawing.Color.Transparent;
			this.pnSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnSelectedColor.ForeColor = System.Drawing.Color.Black;
			this.pnSelectedColor.Location = new System.Drawing.Point(12, 28);
			this.pnSelectedColor.Name = "pnSelectedColor";
			this.pnSelectedColor.Size = new System.Drawing.Size(290, 50);
			this.pnSelectedColor.TabIndex = 11;
			this.pnSelectedColor.DoubleClick += new System.EventHandler(this.pnSelectedColor_DoubleClick);
			// 
			// laSelectedColor
			// 
			this.laSelectedColor.AutoSize = true;
			this.laSelectedColor.BackColor = System.Drawing.Color.White;
			this.laSelectedColor.ForeColor = System.Drawing.Color.Black;
			this.laSelectedColor.Location = new System.Drawing.Point(12, 9);
			this.laSelectedColor.Name = "laSelectedColor";
			this.laSelectedColor.Size = new System.Drawing.Size(241, 16);
			this.laSelectedColor.TabIndex = 12;
			this.laSelectedColor.Text = "Selected Color (Double-click to change):";
			// 
			// checkBoxApplyForAll
			// 
			this.checkBoxApplyForAll.AutoSize = true;
			this.checkBoxApplyForAll.BackColor = System.Drawing.Color.White;
			this.checkBoxApplyForAll.ForeColor = System.Drawing.Color.Black;
			this.checkBoxApplyForAll.Location = new System.Drawing.Point(12, 93);
			this.checkBoxApplyForAll.Name = "checkBoxApplyForAll";
			this.checkBoxApplyForAll.Size = new System.Drawing.Size(131, 20);
			this.checkBoxApplyForAll.TabIndex = 13;
			this.checkBoxApplyForAll.Text = "Apply for all notes";
			this.checkBoxApplyForAll.UseVisualStyleBackColor = false;
			this.checkBoxApplyForAll.CheckedChanged += new System.EventHandler(this.checkBoxApplyForAll_CheckedChanged);
			// 
			// FormNoteColor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(319, 170);
			this.Controls.Add(this.checkBoxApplyForAll);
			this.Controls.Add(this.laSelectedColor);
			this.Controls.Add(this.pnSelectedColor);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.buttonXShow);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormNoteColor";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Color";
			this.Load += new System.EventHandler(this.FormNoteColor_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXShow;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private System.Windows.Forms.Panel pnSelectedColor;
        private System.Windows.Forms.Label laSelectedColor;
        private System.Windows.Forms.CheckBox checkBoxApplyForAll;
    }
}