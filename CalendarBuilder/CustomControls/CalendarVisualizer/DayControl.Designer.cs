namespace CalendarBuilder.CustomControls.CalendarVisualizer
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
            this.pnData = new System.Windows.Forms.Panel();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.labelControlData = new DevExpress.XtraEditors.LabelControl();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pnData.SuspendLayout();
            this.xtraScrollableControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // laSmallDayCaption
            // 
            this.laSmallDayCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.laSmallDayCaption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSmallDayCaption.ContextMenuStrip = this.contextMenuStrip;
            this.laSmallDayCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.laSmallDayCaption.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSmallDayCaption.Location = new System.Drawing.Point(0, 0);
            this.laSmallDayCaption.Name = "laSmallDayCaption";
            this.laSmallDayCaption.Size = new System.Drawing.Size(276, 21);
            this.laSmallDayCaption.TabIndex = 0;
            this.laSmallDayCaption.Text = "label1";
            this.laSmallDayCaption.Click += new System.EventHandler(this.Control_Click);
            this.laSmallDayCaption.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
            // 
            // pnData
            // 
            this.pnData.BackColor = System.Drawing.Color.AliceBlue;
            this.pnData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnData.Controls.Add(this.xtraScrollableControl);
            this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData.Location = new System.Drawing.Point(0, 21);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(276, 226);
            this.pnData.TabIndex = 1;
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.AlwaysScrollActiveControlIntoView = false;
            this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl.ContextMenuStrip = this.contextMenuStrip;
            this.xtraScrollableControl.Controls.Add(this.labelControlData);
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Padding = new System.Windows.Forms.Padding(3);
            this.xtraScrollableControl.Size = new System.Drawing.Size(272, 222);
            this.xtraScrollableControl.TabIndex = 0;
            this.xtraScrollableControl.Click += new System.EventHandler(this.Control_Click);
            this.xtraScrollableControl.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
            // 
            // labelControlData
            // 
            this.labelControlData.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControlData.ContextMenuStrip = this.contextMenuStrip;
            this.labelControlData.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlData.Location = new System.Drawing.Point(3, 3);
            this.labelControlData.Name = "labelControlData";
            this.labelControlData.Size = new System.Drawing.Size(266, 16);
            this.labelControlData.StyleController = this.styleController;
            this.labelControlData.TabIndex = 0;
            this.labelControlData.Text = "Data";
            this.labelControlData.Click += new System.EventHandler(this.Control_Click);
            this.labelControlData.DoubleClick += new System.EventHandler(this.Control_DoubleClick);
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
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // popupMenu
            // 
            this.popupMenu.Name = "popupMenu";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemPaste,
            this.toolStripMenuItemDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(182, 146);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            this.contextMenuStrip.Opened += new System.EventHandler(this.contextMenuStrip_Opened);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuItemCopy.Image = global::CalendarBuilder.Properties.Resources.CopySmall;
            this.toolStripMenuItemCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(181, 40);
            this.toolStripMenuItemCopy.Text = "Copy Data";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // toolStripMenuItemPaste
            // 
            this.toolStripMenuItemPaste.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuItemPaste.Image = global::CalendarBuilder.Properties.Resources.PasteSmall;
            this.toolStripMenuItemPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
            this.toolStripMenuItemPaste.Size = new System.Drawing.Size(181, 40);
            this.toolStripMenuItemPaste.Text = "Paste Data";
            this.toolStripMenuItemPaste.Click += new System.EventHandler(this.toolStripMenuItemPaste_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuItemDelete.Image = global::CalendarBuilder.Properties.Resources.DeleteData;
            this.toolStripMenuItemDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(181, 40);
            this.toolStripMenuItemDelete.Text = "Delete Data";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // DayControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnData);
            this.Controls.Add(this.laSmallDayCaption);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Name = "DayControl";
            this.Size = new System.Drawing.Size(276, 247);
            this.pnData.ResumeLayout(false);
            this.xtraScrollableControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laSmallDayCaption;
        private System.Windows.Forms.Panel pnData;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private DevExpress.XtraEditors.LabelControl labelControlData;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPaste;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;

    }
}
