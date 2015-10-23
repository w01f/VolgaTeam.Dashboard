namespace Asa.CommonGUI.ImageGallery
{
	partial class ImageGroupPage
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
			this.imageListView = new Manina.Windows.Forms.ImageListView();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemFavorites = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageListView
			// 
			this.imageListView.AllowDrag = true;
			this.imageListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.imageListView.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.imageListView.ContextMenuStrip = this.contextMenuStrip;
			this.imageListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageListView.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.imageListView.Location = new System.Drawing.Point(0, 0);
			this.imageListView.MultiSelect = false;
			this.imageListView.Name = "imageListView";
			this.imageListView.PersistentCacheDirectory = "";
			this.imageListView.PersistentCacheSize = ((long)(100));
			this.imageListView.Size = new System.Drawing.Size(521, 446);
			this.imageListView.TabIndex = 40;
			this.imageListView.ThumbnailSize = new System.Drawing.Size(120, 54);
			this.imageListView.ItemHover += new Manina.Windows.Forms.ItemHoverEventHandler(this.imageListView_ItemHover);
			this.imageListView.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView_ItemDoubleClick);
			this.imageListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageListView_MouseDown);
			this.imageListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageListView_MouseMove);
			// 
			// toolTip
			// 
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.toolTip.UseAnimation = false;
			this.toolTip.UseFading = false;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripSeparator1,
            this.toolStripMenuItemFavorites});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.ShowImageMargin = false;
			this.contextMenuStrip.Size = new System.Drawing.Size(138, 54);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// toolStripMenuItemCopy
			// 
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			this.toolStripMenuItemCopy.Size = new System.Drawing.Size(137, 22);
			this.toolStripMenuItemCopy.Text = "Copy this Image";
			this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
			// 
			// toolStripMenuItemFavorites
			// 
			this.toolStripMenuItemFavorites.Name = "toolStripMenuItemFavorites";
			this.toolStripMenuItemFavorites.Size = new System.Drawing.Size(137, 22);
			this.toolStripMenuItemFavorites.Text = "Save to Favorites";
			this.toolStripMenuItemFavorites.Click += new System.EventHandler(this.toolStripMenuItemFavorites_Click);
			// 
			// ImageGroupPage
			// 
			this.Controls.Add(this.imageListView);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(521, 446);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Manina.Windows.Forms.ImageListView imageListView;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFavorites;

	}
}
