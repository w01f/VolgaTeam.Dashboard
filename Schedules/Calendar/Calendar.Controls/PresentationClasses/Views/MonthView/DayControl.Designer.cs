namespace NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView
{
    partial class DayControl
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
			this.laSmallDayCaption = new System.Windows.Forms.Label();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemClone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemCopyImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPasteImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDeleteImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemAddNote = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPasteNote = new System.Windows.Forms.ToolStripMenuItem();
			this.pnData = new System.Windows.Forms.Panel();
			this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
			this.memoEditSimpleComment = new DevExpress.XtraEditors.MemoEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlData = new DevExpress.XtraEditors.LabelControl();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.pnCalendarNoteArea = new System.Windows.Forms.Panel();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.contextMenuStrip.SuspendLayout();
			this.pnData.SuspendLayout();
			this.xtraScrollableControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSimpleComment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// laSmallDayCaption
			// 
			this.laSmallDayCaption.BackColor = System.Drawing.Color.White;
			this.laSmallDayCaption.ContextMenuStrip = this.contextMenuStrip;
			this.laSmallDayCaption.Dock = System.Windows.Forms.DockStyle.Top;
			this.laSmallDayCaption.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSmallDayCaption.Location = new System.Drawing.Point(1, 1);
			this.laSmallDayCaption.Name = "laSmallDayCaption";
			this.laSmallDayCaption.Size = new System.Drawing.Size(297, 20);
			this.laSmallDayCaption.TabIndex = 0;
			this.laSmallDayCaption.Text = "label1";
			this.laSmallDayCaption.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
			this.laSmallDayCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.laSmallDayCaption.MouseHover += new System.EventHandler(this.DayControl_MouseHover);
			this.laSmallDayCaption.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseMove);
			this.laSmallDayCaption.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemPaste,
            this.toolStripMenuItemClone,
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemDelete,
            this.toolStripSeparator2,
            this.toolStripMenuItemCopyImage,
            this.toolStripMenuItemPasteImage,
            this.toolStripMenuItemDeleteImage,
            this.toolStripSeparator1,
            this.toolStripMenuItemAddNote,
            this.toolStripMenuItemPasteNote});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(152, 316);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			this.contextMenuStrip.Opened += new System.EventHandler(this.contextMenuStrip_Opened);
			// 
			// toolStripMenuItemCopy
			// 
			this.toolStripMenuItemCopy.Enabled = false;
			this.toolStripMenuItemCopy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemCopy.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.CopySmall;
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			this.toolStripMenuItemCopy.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemCopy.Text = "Copy Data";
			this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
			// 
			// toolStripMenuItemPaste
			// 
			this.toolStripMenuItemPaste.Enabled = false;
			this.toolStripMenuItemPaste.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemPaste.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.PasteSmall;
			this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
			this.toolStripMenuItemPaste.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemPaste.Text = "Paste Data";
			this.toolStripMenuItemPaste.Click += new System.EventHandler(this.toolStripMenuItemPaste_Click);
			// 
			// toolStripMenuItemClone
			// 
			this.toolStripMenuItemClone.Enabled = false;
			this.toolStripMenuItemClone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemClone.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.CloneSmall;
			this.toolStripMenuItemClone.Name = "toolStripMenuItemClone";
			this.toolStripMenuItemClone.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemClone.Text = "Clone Day...";
			this.toolStripMenuItemClone.Click += new System.EventHandler(this.toolStripMenuItemClone_Click);
			// 
			// toolStripMenuItemEdit
			// 
			this.toolStripMenuItemEdit.Enabled = false;
			this.toolStripMenuItemEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemEdit.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.EditData;
			this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
			this.toolStripMenuItemEdit.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemEdit.Text = "Edit Data...";
			this.toolStripMenuItemEdit.Click += new System.EventHandler(this.toolStripMenuItemEdit_Click);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Enabled = false;
			this.toolStripMenuItemDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemDelete.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.DeleteData;
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemDelete.Text = "Delete Data";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
			// 
			// toolStripMenuItemCopyImage
			// 
			this.toolStripMenuItemCopyImage.Enabled = false;
			this.toolStripMenuItemCopyImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemCopyImage.Name = "toolStripMenuItemCopyImage";
			this.toolStripMenuItemCopyImage.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemCopyImage.Text = "Copy Image";
			this.toolStripMenuItemCopyImage.Click += new System.EventHandler(this.toolStripMenuItemCopyImage_Click);
			// 
			// toolStripMenuItemPasteImage
			// 
			this.toolStripMenuItemPasteImage.Enabled = false;
			this.toolStripMenuItemPasteImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemPasteImage.Name = "toolStripMenuItemPasteImage";
			this.toolStripMenuItemPasteImage.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemPasteImage.Text = "Paste Image";
			this.toolStripMenuItemPasteImage.Click += new System.EventHandler(this.toolStripMenuItemPasteImage_Click);
			// 
			// toolStripMenuItemDeleteImage
			// 
			this.toolStripMenuItemDeleteImage.Enabled = false;
			this.toolStripMenuItemDeleteImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemDeleteImage.Name = "toolStripMenuItemDeleteImage";
			this.toolStripMenuItemDeleteImage.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemDeleteImage.Text = "Delete Image";
			this.toolStripMenuItemDeleteImage.Click += new System.EventHandler(this.toolStripMenuItemDeleteImage_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
			// 
			// toolStripMenuItemAddNote
			// 
			this.toolStripMenuItemAddNote.Enabled = false;
			this.toolStripMenuItemAddNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemAddNote.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.FloaterNote;
			this.toolStripMenuItemAddNote.Name = "toolStripMenuItemAddNote";
			this.toolStripMenuItemAddNote.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemAddNote.Text = "Add Note";
			this.toolStripMenuItemAddNote.Click += new System.EventHandler(this.toolStripMenuItemAddNote_Click);
			// 
			// toolStripMenuItemPasteNote
			// 
			this.toolStripMenuItemPasteNote.Enabled = false;
			this.toolStripMenuItemPasteNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemPasteNote.Image = global::NewBizWiz.Calendar.Controls.Properties.Resources.PasteSmall;
			this.toolStripMenuItemPasteNote.Name = "toolStripMenuItemPasteNote";
			this.toolStripMenuItemPasteNote.Size = new System.Drawing.Size(151, 30);
			this.toolStripMenuItemPasteNote.Text = "Paste Note";
			this.toolStripMenuItemPasteNote.Click += new System.EventHandler(this.toolStripMenuItemPasteNote_Click);
			// 
			// pnData
			// 
			this.pnData.BackColor = System.Drawing.Color.White;
			this.pnData.Controls.Add(this.xtraScrollableControl);
			this.pnData.Controls.Add(this.pnCalendarNoteArea);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(1, 21);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(297, 225);
			this.pnData.TabIndex = 1;
			// 
			// xtraScrollableControl
			// 
			this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
			this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControl.ContextMenuStrip = this.contextMenuStrip;
			this.xtraScrollableControl.Controls.Add(this.memoEditSimpleComment);
			this.xtraScrollableControl.Controls.Add(this.labelControlData);
			this.xtraScrollableControl.Controls.Add(this.pbLogo);
			this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControl.Location = new System.Drawing.Point(0, 40);
			this.xtraScrollableControl.Name = "xtraScrollableControl";
			this.xtraScrollableControl.Padding = new System.Windows.Forms.Padding(3);
			this.xtraScrollableControl.Size = new System.Drawing.Size(297, 185);
			this.xtraScrollableControl.TabIndex = 0;
			this.xtraScrollableControl.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
			this.xtraScrollableControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.xtraScrollableControl.MouseHover += new System.EventHandler(this.DayControl_MouseHover);
			this.xtraScrollableControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseMove);
			this.xtraScrollableControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
			// 
			// memoEditSimpleComment
			// 
			this.memoEditSimpleComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.memoEditSimpleComment.Location = new System.Drawing.Point(3, 60);
			this.memoEditSimpleComment.Name = "memoEditSimpleComment";
			this.memoEditSimpleComment.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditSimpleComment.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditSimpleComment.Size = new System.Drawing.Size(291, 122);
			this.memoEditSimpleComment.StyleController = this.styleController;
			this.memoEditSimpleComment.TabIndex = 2;
			this.memoEditSimpleComment.UseOptimizedRendering = true;
			this.memoEditSimpleComment.Visible = false;
			this.memoEditSimpleComment.EditValueChanged += new System.EventHandler(this.memoEditSimpleComment_EditValueChanged);
			this.memoEditSimpleComment.Leave += new System.EventHandler(this.memoEditSimpleComment_Leave);
			this.memoEditSimpleComment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.memoEditSimpleComment.MouseHover += new System.EventHandler(this.DayControl_MouseHover);
			this.memoEditSimpleComment.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
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
			// labelControlData
			// 
			this.labelControlData.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlData.ContextMenuStrip = this.contextMenuStrip;
			this.labelControlData.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlData.Location = new System.Drawing.Point(3, 44);
			this.labelControlData.Name = "labelControlData";
			this.labelControlData.Size = new System.Drawing.Size(291, 16);
			this.labelControlData.StyleController = this.styleController;
			this.labelControlData.TabIndex = 0;
			this.labelControlData.Text = "Data";
			this.labelControlData.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
			this.labelControlData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.labelControlData.MouseHover += new System.EventHandler(this.DayControl_MouseHover);
			this.labelControlData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseMove);
			this.labelControlData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
			// 
			// pbLogo
			// 
			this.pbLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pbLogo.Location = new System.Drawing.Point(3, 3);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(291, 41);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbLogo.TabIndex = 1;
			this.pbLogo.TabStop = false;
			this.pbLogo.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
			this.pbLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.pbLogo.MouseHover += new System.EventHandler(this.DayControl_MouseHover);
			this.pbLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseMove);
			this.pbLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
			// 
			// pnCalendarNoteArea
			// 
			this.pnCalendarNoteArea.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnCalendarNoteArea.Location = new System.Drawing.Point(0, 0);
			this.pnCalendarNoteArea.Name = "pnCalendarNoteArea";
			this.pnCalendarNoteArea.Size = new System.Drawing.Size(297, 40);
			this.pnCalendarNoteArea.TabIndex = 3;
			this.pnCalendarNoteArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.pnCalendarNoteArea.MouseHover += new System.EventHandler(this.DayControl_MouseHover);
			this.pnCalendarNoteArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseMove);
			this.pnCalendarNoteArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// DayControl
			// 
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.DarkGray;
			this.Controls.Add(this.pnData);
			this.Controls.Add(this.laSmallDayCaption);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Font = new System.Drawing.Font("Arial", 9.75F);
			this.Name = "DayControl";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(299, 247);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DayControl_DragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.DayControl_DragOver);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.MouseHover += new System.EventHandler(this.DayControl_MouseHover);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
			this.contextMenuStrip.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			this.xtraScrollableControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditSimpleComment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laSmallDayCaption;
        private System.Windows.Forms.Panel pnData;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private DevExpress.XtraEditors.LabelControl labelControlData;
		private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPaste;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClone;
        private System.Windows.Forms.PictureBox pbLogo;
        private DevExpress.XtraEditors.MemoEdit memoEditSimpleComment;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddNote;
        private System.Windows.Forms.Panel pnCalendarNoteArea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPasteNote;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPasteImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteImage;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
    }
}
