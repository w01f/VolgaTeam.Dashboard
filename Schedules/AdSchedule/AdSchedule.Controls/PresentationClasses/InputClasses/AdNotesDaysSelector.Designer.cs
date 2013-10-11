namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
    partial class AdNotesDaysSelector
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
			this.buttonXApplyOtherDays = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXApplyOtherDays
			// 
			this.buttonXApplyOtherDays.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXApplyOtherDays.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXApplyOtherDays.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonXApplyOtherDays.Image = global::NewBizWiz.AdSchedule.Controls.Properties.Resources.AdNotesOtherDays;
			this.buttonXApplyOtherDays.Location = new System.Drawing.Point(179, 0);
			this.buttonXApplyOtherDays.Name = "buttonXApplyOtherDays";
			this.buttonXApplyOtherDays.Size = new System.Drawing.Size(263, 61);
			this.buttonXApplyOtherDays.TabIndex = 8;
			this.buttonXApplyOtherDays.TextColor = System.Drawing.Color.Black;
			// 
			// AdNotesDaysSelector
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.buttonXApplyOtherDays);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "AdNotesDaysSelector";
			this.Size = new System.Drawing.Size(442, 61);
			this.ResumeLayout(false);

        }

        #endregion

		public DevComponents.DotNetBar.ButtonX buttonXApplyOtherDays;

	}
}
