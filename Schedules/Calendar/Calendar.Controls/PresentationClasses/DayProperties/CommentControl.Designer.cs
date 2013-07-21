namespace CalendarBuilder.PresentationClasses.DayProperties
{
    partial class CommentControl
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
            this.components = new System.ComponentModel.Container();
            this.memoEditComment1 = new DevExpress.XtraEditors.MemoEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.memoEditComment2 = new DevExpress.XtraEditors.MemoEdit();
            this.buttonXComment1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonXComment2 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEditComment1
            // 
            this.memoEditComment1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditComment1.Enabled = false;
            this.memoEditComment1.Location = new System.Drawing.Point(8, 34);
            this.memoEditComment1.Name = "memoEditComment1";
            this.memoEditComment1.Properties.NullText = "Type Here";
            this.memoEditComment1.Size = new System.Drawing.Size(223, 69);
            this.memoEditComment1.StyleController = this.styleController;
            this.memoEditComment1.TabIndex = 7;
            this.memoEditComment1.EditValueChanged += new System.EventHandler(this.memoEditComment_EditValueChanged);
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            // 
            // memoEditComment2
            // 
            this.memoEditComment2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditComment2.Enabled = false;
            this.memoEditComment2.Location = new System.Drawing.Point(8, 149);
            this.memoEditComment2.Name = "memoEditComment2";
            this.memoEditComment2.Properties.NullText = "Type Here";
            this.memoEditComment2.Size = new System.Drawing.Size(223, 69);
            this.memoEditComment2.StyleController = this.styleController;
            this.memoEditComment2.TabIndex = 9;
            this.memoEditComment2.EditValueChanged += new System.EventHandler(this.memoEditComment_EditValueChanged);
            // 
            // buttonXComment1
            // 
            this.buttonXComment1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXComment1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXComment1.AutoCheckOnClick = true;
            this.buttonXComment1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXComment1.Location = new System.Drawing.Point(8, 4);
            this.buttonXComment1.Name = "buttonXComment1";
            this.buttonXComment1.Size = new System.Drawing.Size(223, 24);
            this.buttonXComment1.TabIndex = 28;
            this.buttonXComment1.Text = "A. Comment #1";
            this.buttonXComment1.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXComment1.TextColor = System.Drawing.Color.Black;
            this.buttonXComment1.CheckedChanged += new System.EventHandler(this.checkEditUseComment1_CheckedChanged);
            // 
            // buttonXComment2
            // 
            this.buttonXComment2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXComment2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXComment2.AutoCheckOnClick = true;
            this.buttonXComment2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXComment2.Location = new System.Drawing.Point(8, 119);
            this.buttonXComment2.Name = "buttonXComment2";
            this.buttonXComment2.Size = new System.Drawing.Size(223, 24);
            this.buttonXComment2.TabIndex = 29;
            this.buttonXComment2.Text = "B. Comment #2";
            this.buttonXComment2.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXComment2.TextColor = System.Drawing.Color.Black;
            this.buttonXComment2.CheckedChanged += new System.EventHandler(this.checkEditUseComment2_CheckedChanged);
            // 
            // CommentControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.buttonXComment2);
            this.Controls.Add(this.buttonXComment1);
            this.Controls.Add(this.memoEditComment2);
            this.Controls.Add(this.memoEditComment1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CommentControl";
            this.Size = new System.Drawing.Size(240, 322);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.MemoEdit memoEditComment1;
        private DevExpress.XtraEditors.StyleController styleController;
        public DevExpress.XtraEditors.MemoEdit memoEditComment2;
        private DevComponents.DotNetBar.ButtonX buttonXComment1;
        private DevComponents.DotNetBar.ButtonX buttonXComment2;
    }
}
