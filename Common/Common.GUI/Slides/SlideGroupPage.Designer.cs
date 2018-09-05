namespace Asa.Common.GUI.Slides
{
	partial class SlideGroupPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlideGroupPage));
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
			this.toolStripMenuItemOutput = new System.Windows.Forms.ToolStripMenuItem();
			this.slidesListView = new Manina.Windows.Forms.ImageListView();
			this.toolTipController = new DevExpress.Utils.ToolTipController();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOutput});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(144, 26);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuOpening);
			// 
			// toolStripMenuItemOutput
			// 
			this.toolStripMenuItemOutput.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemOutput.Image")));
			this.toolStripMenuItemOutput.Name = "toolStripMenuItemOutput";
			this.toolStripMenuItemOutput.Size = new System.Drawing.Size(143, 22);
			this.toolStripMenuItemOutput.Text = "Insert slide(s)";
			this.toolStripMenuItemOutput.Click += new System.EventHandler(this.OnMenuItemOutputClick);
			// 
			// slidesListView
			// 
			this.slidesListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.slidesListView.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.slidesListView.ContextMenuStrip = this.contextMenuStrip;
			this.slidesListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slidesListView.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.slidesListView.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.slidesListView.Location = new System.Drawing.Point(0, 0);
			this.slidesListView.MultiSelect = false;
			this.slidesListView.Name = "slidesListView";
			this.slidesListView.PersistentCacheDirectory = "";
			this.slidesListView.PersistentCacheSize = ((long)(100));
			this.slidesListView.Size = new System.Drawing.Size(521, 446);
			this.slidesListView.TabIndex = 40;
			this.slidesListView.ThumbnailSize = new System.Drawing.Size(255, 170);
			this.slidesListView.UseWIC = true;
			this.slidesListView.ItemHover += new Manina.Windows.Forms.ItemHoverEventHandler(this.OnListViewItemHover);
			this.slidesListView.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.OnListViewItemDoubleClick);
			this.slidesListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnListViewMouseDown);
			this.slidesListView.MouseLeave += new System.EventHandler(this.OnListViewMouseLeave);
			this.slidesListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnListViewMouseMove);
			// 
			// SlideGroupPage
			// 
			this.Controls.Add(this.slidesListView);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(521, 446);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public Manina.Windows.Forms.ImageListView slidesListView;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOutput;
		private DevExpress.Utils.ToolTipController toolTipController;
	}
}
