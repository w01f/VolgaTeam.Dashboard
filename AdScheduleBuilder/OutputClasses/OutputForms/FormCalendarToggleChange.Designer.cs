namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    partial class FormCalendarToggleChange
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
            this.buttonXDisable = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEdit = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // buttonXDisable
            // 
            this.buttonXDisable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDisable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDisable.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonXDisable.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXDisable.ForeColor = System.Drawing.Color.Black;
            this.buttonXDisable.Location = new System.Drawing.Point(14, 12);
            this.buttonXDisable.Name = "buttonXDisable";
            this.buttonXDisable.Size = new System.Drawing.Size(211, 39);
            this.buttonXDisable.TabIndex = 1;
            this.buttonXDisable.Text = "Disable this item";
            // 
            // buttonXEdit
            // 
            this.buttonXEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEdit.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonXEdit.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXEdit.ForeColor = System.Drawing.Color.Black;
            this.buttonXEdit.Location = new System.Drawing.Point(14, 67);
            this.buttonXEdit.Name = "buttonXEdit";
            this.buttonXEdit.Size = new System.Drawing.Size(211, 39);
            this.buttonXEdit.TabIndex = 2;
            this.buttonXEdit.Text = "Edit this item";
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXCancel.ForeColor = System.Drawing.Color.Red;
            this.buttonXCancel.Location = new System.Drawing.Point(14, 122);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(211, 39);
            this.buttonXCancel.TabIndex = 3;
            this.buttonXCancel.Text = "CANCEL";
            // 
            // FormCalendarToggleChange
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(238, 173);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXEdit);
            this.Controls.Add(this.buttonXDisable);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCalendarToggleChange";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calendar";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXDisable;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        public DevComponents.DotNetBar.ButtonX buttonXEdit;
    }
}