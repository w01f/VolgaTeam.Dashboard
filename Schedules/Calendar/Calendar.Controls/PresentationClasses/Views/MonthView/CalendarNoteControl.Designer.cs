using Asa.Business.Common.Interfaces;

namespace Asa.Calendar.Controls.PresentationClasses.Views.MonthView
{
	partial class CalendarNoteControl
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
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.memoEdit = new DevExpress.XtraEditors.MemoEdit();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemClone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemColor = new System.Windows.Forms.ToolStripMenuItem();
			this.textBox = new System.Windows.Forms.TextBox();
			this.pbClose = new System.Windows.Forms.PictureBox();
			this.labelControl = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
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
			// memoEdit
			// 
			this.memoEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.memoEdit.EditValue = "test";
			this.memoEdit.Location = new System.Drawing.Point(4, 4);
			this.memoEdit.Name = "memoEdit";
			this.memoEdit.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
			this.memoEdit.Properties.Appearance.Options.UseBackColor = true;
			this.memoEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.memoEdit.Properties.ContextMenuStrip = this.contextMenuStrip;
			this.memoEdit.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.memoEdit.Size = new System.Drawing.Size(463, 27);
			this.memoEdit.TabIndex = 3;
			this.memoEdit.UseOptimizedRendering = true;
			this.memoEdit.Visible = false;
			this.memoEdit.EditValueChanged += new System.EventHandler(this.memoEdit_EditValueChanged);
			this.memoEdit.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.memoEdit_EditValueChanging);
			this.memoEdit.Leave += new System.EventHandler(this.memoEdit_Leave);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemClone,
            this.toolStripMenuItemColor});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(170, 124);
			// 
			// toolStripMenuItemCopy
			// 
			this.toolStripMenuItemCopy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemCopy.Image = global::Asa.Calendar.Controls.Properties.Resources.CopySmall;
			this.toolStripMenuItemCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			this.toolStripMenuItemCopy.Size = new System.Drawing.Size(169, 40);
			this.toolStripMenuItemCopy.Text = "Copy Note";
			this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
			// 
			// toolStripMenuItemClone
			// 
			this.toolStripMenuItemClone.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemClone.Image = global::Asa.Calendar.Controls.Properties.Resources.CloneSmall;
			this.toolStripMenuItemClone.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemClone.Name = "toolStripMenuItemClone";
			this.toolStripMenuItemClone.Size = new System.Drawing.Size(169, 40);
			this.toolStripMenuItemClone.Text = "Clone Note...";
			this.toolStripMenuItemClone.Click += new System.EventHandler(this.toolStripMenuItemClone_Click);
			// 
			// toolStripMenuItemColor
			// 
			this.toolStripMenuItemColor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItemColor.Image = global::Asa.Calendar.Controls.Properties.Resources.Color;
			this.toolStripMenuItemColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemColor.Name = "toolStripMenuItemColor";
			this.toolStripMenuItemColor.Size = new System.Drawing.Size(169, 40);
			this.toolStripMenuItemColor.Text = "Color...";
			this.toolStripMenuItemColor.Click += new System.EventHandler(this.toolStripMenuItemColor_Click);
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox.Location = new System.Drawing.Point(4, 4);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(463, 27);
			this.textBox.TabIndex = 4;
			// 
			// pbClose
			// 
			this.pbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbClose.Image = global::Asa.Calendar.Controls.Properties.Resources.DeleteData;
			this.pbClose.Location = new System.Drawing.Point(473, 4);
			this.pbClose.Name = "pbClose";
			this.pbClose.Size = new System.Drawing.Size(24, 24);
			this.pbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbClose.TabIndex = 1;
			this.pbClose.TabStop = false;
			this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
			// 
			// labelControl
			// 
			this.labelControl.AllowHtmlString = true;
			this.labelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl.Location = new System.Drawing.Point(4, 4);
			this.labelControl.Name = "labelControl";
			this.labelControl.Size = new System.Drawing.Size(463, 27);
			this.labelControl.TabIndex = 5;
			this.labelControl.Text = "test";
			this.labelControl.Click += new System.EventHandler(this.labelControl_Click);
			// 
			// CalendarNoteControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ContextMenuStrip = this.contextMenuStrip;
			this.Controls.Add(this.pbClose);
			this.Controls.Add(this.labelControl);
			this.Controls.Add(this.memoEdit);
			this.Controls.Add(this.textBox);
			this.Name = "CalendarNoteControl";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(502, 35);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.PictureBox pbClose;
		private DevExpress.XtraEditors.StyleController styleController;
		protected DevExpress.XtraEditors.MemoEdit memoEdit;
		protected System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClone;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemColor;
		protected DevExpress.XtraEditors.LabelControl labelControl;
	}
}
