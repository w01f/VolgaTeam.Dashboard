namespace NewBizWiz.CommonGUI.FavoriteImages
{
	partial class FavoriteImagesControl
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
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListView = new Manina.Windows.Forms.ImageListView();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemRename,
            this.toolStripSeparator1,
            this.toolStripMenuItemDelete});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.ShowImageMargin = false;
			this.contextMenuStrip.Size = new System.Drawing.Size(102, 76);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// toolStripMenuItemCopy
			// 
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			this.toolStripMenuItemCopy.Size = new System.Drawing.Size(101, 22);
			this.toolStripMenuItemCopy.Text = "Copy";
			this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
			// 
			// toolStripMenuItemRename
			// 
			this.toolStripMenuItemRename.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemRename.Name = "toolStripMenuItemRename";
			this.toolStripMenuItemRename.Size = new System.Drawing.Size(101, 22);
			this.toolStripMenuItemRename.Text = "Rename...";
			this.toolStripMenuItemRename.Click += new System.EventHandler(this.toolStripMenuItemRename_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(98, 6);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(101, 22);
			this.toolStripMenuItemDelete.Text = "Delete";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// imageListView
			// 
			this.imageListView.AllowDrag = true;
			this.imageListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imageListView.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.imageListView.ContextMenuStrip = this.contextMenuStrip;
			this.imageListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageListView.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.imageListView.IconAlignment = System.Drawing.ContentAlignment.TopCenter;
			this.imageListView.Location = new System.Drawing.Point(0, 0);
			this.imageListView.MultiSelect = false;
			this.imageListView.Name = "imageListView";
			this.imageListView.PersistentCacheDirectory = "";
			this.imageListView.PersistentCacheSize = ((long)(100));
			this.imageListView.Size = new System.Drawing.Size(251, 391);
			this.imageListView.TabIndex = 39;
			this.imageListView.ThumbnailSize = new System.Drawing.Size(120, 54);
			this.imageListView.ItemHover += new Manina.Windows.Forms.ItemHoverEventHandler(this.OnGalleryItemHover);
			this.imageListView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.OnGalleryGiveFeedback);
			this.imageListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGalleryMouseDown);
			this.imageListView.MouseLeave += new System.EventHandler(this.OnGalleryMouseLeave);
			this.imageListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnGalleryMouseMove);
			this.imageListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnGalleryMouseUp);
			// 
			// toolTip
			// 
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.toolTip.UseAnimation = false;
			this.toolTip.UseFading = false;
			// 
			// FavoriteImagesControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.imageListView);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FavoriteImagesControl";
			this.Size = new System.Drawing.Size(251, 391);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRename;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
		private Manina.Windows.Forms.ImageListView imageListView;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
