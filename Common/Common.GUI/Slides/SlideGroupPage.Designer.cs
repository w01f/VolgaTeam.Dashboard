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
			this.slidesListView = new Manina.Windows.Forms.ImageListView();
			this.SuspendLayout();
			// 
			// slidesListView
			// 
			this.slidesListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.slidesListView.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.slidesListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slidesListView.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.slidesListView.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.slidesListView.Location = new System.Drawing.Point(0, 0);
			this.slidesListView.MultiSelect = false;
			this.slidesListView.Name = "slidesListView";
			this.slidesListView.PersistentCacheDirectory = "";
			this.slidesListView.PersistentCacheSize = ((long)(100));
			this.slidesListView.Size = new System.Drawing.Size(521, 446);
			this.slidesListView.TabIndex = 40;
			this.slidesListView.ThumbnailSize = new System.Drawing.Size(255, 170);
			this.slidesListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageListView_MouseMove);
			// 
			// SlideGroupPage
			// 
			this.Controls.Add(this.slidesListView);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ImageGroupPage";
			this.Size = new System.Drawing.Size(521, 446);
			this.ResumeLayout(false);

		}

		#endregion

		private Manina.Windows.Forms.ImageListView slidesListView;

	}
}
