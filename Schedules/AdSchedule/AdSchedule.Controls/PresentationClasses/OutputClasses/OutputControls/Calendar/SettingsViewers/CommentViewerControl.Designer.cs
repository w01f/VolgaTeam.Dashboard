namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    partial class CommentViewerControl
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
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.checkEditUseComment = new DevExpress.XtraEditors.CheckEdit();
            this.memoEditComment = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditUseComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Options.UseForeColor = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Options.UseForeColor = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // checkEditUseComment
            // 
            this.checkEditUseComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEditUseComment.Location = new System.Drawing.Point(3, 13);
            this.checkEditUseComment.Name = "checkEditUseComment";
            this.checkEditUseComment.Properties.Caption = "Custom Comment Field?";
            this.checkEditUseComment.Size = new System.Drawing.Size(339, 21);
            this.checkEditUseComment.StyleController = this.styleController;
            this.checkEditUseComment.TabIndex = 0;
            this.checkEditUseComment.CheckedChanged += new System.EventHandler(this.checkEditUseComment_CheckedChanged);
            // 
            // memoEditComment
            // 
            this.memoEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditComment.Enabled = false;
            this.memoEditComment.Location = new System.Drawing.Point(21, 40);
            this.memoEditComment.Name = "memoEditComment";
            this.memoEditComment.Size = new System.Drawing.Size(307, 259);
            this.memoEditComment.StyleController = this.styleController;
            this.memoEditComment.TabIndex = 1;
            // 
            // CommentViewerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.memoEditComment);
            this.Controls.Add(this.checkEditUseComment);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "CommentViewerControl";
            this.Size = new System.Drawing.Size(345, 313);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditUseComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.CheckEdit checkEditUseComment;
        private DevExpress.XtraEditors.MemoEdit memoEditComment;
    }
}
