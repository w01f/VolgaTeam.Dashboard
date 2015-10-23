namespace Asa.Calendar.Controls.ToolForms
{
	partial class FormDayProperties
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
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.checkEditComment = new DevExpress.XtraEditors.CheckEdit();
			this.memoEditComment = new DevExpress.XtraEditors.MemoEdit();
			this.labelControlCommentDisclaimer = new DevExpress.XtraEditors.LabelControl();
			this.pnComment = new System.Windows.Forms.Panel();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).BeginInit();
			this.pnComment.SuspendLayout();
			this.SuspendLayout();
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
			// checkEditComment
			// 
			this.checkEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditComment.Location = new System.Drawing.Point(4, 9);
			this.checkEditComment.Name = "checkEditComment";
			this.checkEditComment.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditComment.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditComment.Properties.Caption = "Show Comment";
			this.checkEditComment.Size = new System.Drawing.Size(328, 19);
			this.checkEditComment.StyleController = this.styleController;
			this.checkEditComment.TabIndex = 36;
			this.checkEditComment.CheckedChanged += new System.EventHandler(this.checkEditComment_CheckedChanged);
			// 
			// memoEditComment
			// 
			this.memoEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditComment.Enabled = false;
			this.memoEditComment.Location = new System.Drawing.Point(5, 36);
			this.memoEditComment.Name = "memoEditComment";
			this.memoEditComment.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditComment.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.memoEditComment.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditComment.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditComment.Properties.NullText = "Type Here";
			this.memoEditComment.Size = new System.Drawing.Size(340, 70);
			this.memoEditComment.StyleController = this.styleController;
			this.memoEditComment.TabIndex = 37;
			this.memoEditComment.UseOptimizedRendering = true;
			// 
			// labelControlCommentDisclaimer
			// 
			this.labelControlCommentDisclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlCommentDisclaimer.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlCommentDisclaimer.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlCommentDisclaimer.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlCommentDisclaimer.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlCommentDisclaimer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlCommentDisclaimer.Location = new System.Drawing.Point(6, 112);
			this.labelControlCommentDisclaimer.Name = "labelControlCommentDisclaimer";
			this.labelControlCommentDisclaimer.Size = new System.Drawing.Size(326, 17);
			this.labelControlCommentDisclaimer.StyleController = this.styleController;
			this.labelControlCommentDisclaimer.TabIndex = 38;
			this.labelControlCommentDisclaimer.Text = "*Keep Comments Short & Sweet";
			this.labelControlCommentDisclaimer.UseMnemonic = false;
			// 
			// pnComment
			// 
			this.pnComment.BackColor = System.Drawing.Color.Transparent;
			this.pnComment.Controls.Add(this.buttonXOK);
			this.pnComment.Controls.Add(this.buttonXCancel);
			this.pnComment.Controls.Add(this.checkEditComment);
			this.pnComment.Controls.Add(this.labelControlCommentDisclaimer);
			this.pnComment.Controls.Add(this.memoEditComment);
			this.pnComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnComment.ForeColor = System.Drawing.Color.Black;
			this.pnComment.Location = new System.Drawing.Point(0, 0);
			this.pnComment.Name = "pnComment";
			this.pnComment.Size = new System.Drawing.Size(479, 139);
			this.pnComment.TabIndex = 40;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(373, 11);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(98, 35);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 39;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(373, 94);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(98, 35);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 40;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// FormDayProperties
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(479, 139);
			this.Controls.Add(this.pnComment);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDayProperties";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Day Properties:";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDayProperties_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).EndInit();
			this.pnComment.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditComment;
		public DevExpress.XtraEditors.MemoEdit memoEditComment;
		private DevExpress.XtraEditors.LabelControl labelControlCommentDisclaimer;
		private System.Windows.Forms.Panel pnComment;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
	}
}