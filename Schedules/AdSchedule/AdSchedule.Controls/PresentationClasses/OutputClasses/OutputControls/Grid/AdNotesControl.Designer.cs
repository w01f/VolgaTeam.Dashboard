namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
    partial class AdNotesControl
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
			this.checkEditShowAdNotes = new DevExpress.XtraEditors.CheckEdit();
			this.checkedListBoxAdNotes = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonXDown = new DevComponents.DotNetBar.ButtonX();
			this.buttonXUp = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowAdNotes.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxAdNotes)).BeginInit();
			this.SuspendLayout();
			// 
			// checkEditShowAdNotes
			// 
			this.checkEditShowAdNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditShowAdNotes.EditValue = true;
			this.checkEditShowAdNotes.Location = new System.Drawing.Point(9, 3);
			this.checkEditShowAdNotes.Name = "checkEditShowAdNotes";
			this.checkEditShowAdNotes.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.checkEditShowAdNotes.Properties.Appearance.Options.UseFont = true;
			this.checkEditShowAdNotes.Properties.AutoHeight = false;
			this.checkEditShowAdNotes.Properties.Caption = "Select up to 4 Ad-Notes";
			this.checkEditShowAdNotes.Size = new System.Drawing.Size(217, 40);
			this.checkEditShowAdNotes.TabIndex = 21;
			this.checkEditShowAdNotes.CheckedChanged += new System.EventHandler(this.checkEditShowAdNotes_CheckedChanged);
			// 
			// checkedListBoxAdNotes
			// 
			this.checkedListBoxAdNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxAdNotes.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxAdNotes.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxAdNotes.Appearance.Options.UseBackColor = true;
			this.checkedListBoxAdNotes.Appearance.Options.UseFont = true;
			this.checkedListBoxAdNotes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.checkedListBoxAdNotes.ItemHeight = 45;
			this.checkedListBoxAdNotes.Location = new System.Drawing.Point(9, 62);
			this.checkedListBoxAdNotes.Name = "checkedListBoxAdNotes";
			this.checkedListBoxAdNotes.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxAdNotes.Size = new System.Drawing.Size(217, 512);
			this.checkedListBoxAdNotes.TabIndex = 18;
			this.checkedListBoxAdNotes.ItemChecking += new DevExpress.XtraEditors.Controls.ItemCheckingEventHandler(this.checkedListBoxAdNotes_ItemChecking);
			this.checkedListBoxAdNotes.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxAdNotes_ItemCheck);
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonXDown
			// 
			this.buttonXDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDown.Image = global::Asa.AdSchedule.Controls.Properties.Resources.NudgeDown;
			this.buttonXDown.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonXDown.Location = new System.Drawing.Point(232, 110);
			this.buttonXDown.Name = "buttonXDown";
			this.buttonXDown.Size = new System.Drawing.Size(42, 42);
			this.buttonXDown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDown.TabIndex = 20;
			this.buttonXDown.Click += new System.EventHandler(this.buttonXDown_Click);
			// 
			// buttonXUp
			// 
			this.buttonXUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUp.Image = global::Asa.AdSchedule.Controls.Properties.Resources.NudgeUp;
			this.buttonXUp.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonXUp.Location = new System.Drawing.Point(232, 62);
			this.buttonXUp.Name = "buttonXUp";
			this.buttonXUp.Size = new System.Drawing.Size(42, 42);
			this.buttonXUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUp.TabIndex = 19;
			this.buttonXUp.Click += new System.EventHandler(this.buttonXUp_Click);
			// 
			// AdNotesControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.checkEditShowAdNotes);
			this.Controls.Add(this.buttonXDown);
			this.Controls.Add(this.buttonXUp);
			this.Controls.Add(this.checkedListBoxAdNotes);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "AdNotesControl";
			this.Size = new System.Drawing.Size(282, 591);
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowAdNotes.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxAdNotes)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit checkEditShowAdNotes;
        private DevComponents.DotNetBar.ButtonX buttonXDown;
        private DevComponents.DotNetBar.ButtonX buttonXUp;
		public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxAdNotes;
        private DevComponents.DotNetBar.SuperTooltip superTooltip;
    }
}
