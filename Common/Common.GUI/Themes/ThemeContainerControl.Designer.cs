namespace Asa.Common.GUI.Themes
{
	partial class ThemeContainerControl
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
			this.themesListView = new Manina.Windows.Forms.ImageListView();
			this.SuspendLayout();
			// 
			// themesListView
			// 
			this.themesListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.themesListView.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.themesListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.themesListView.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.themesListView.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.themesListView.Location = new System.Drawing.Point(0, 0);
			this.themesListView.MultiSelect = false;
			this.themesListView.Name = "themesListView";
			this.themesListView.PersistentCacheDirectory = "";
			this.themesListView.PersistentCacheSize = ((long)(100));
			this.themesListView.Size = new System.Drawing.Size(728, 527);
			this.themesListView.TabIndex = 41;
			this.themesListView.ThumbnailSize = new System.Drawing.Size(255, 170);
			this.themesListView.UseWIC = true;
			this.themesListView.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.OnListViewItemDoubleClick);
			this.themesListView.SelectionChanged += new System.EventHandler(this.OnListViewSelectionChanged);
			// 
			// ThemeContainerControl
			// 
			this.Controls.Add(this.themesListView);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ThemeContainerControl";
			this.Size = new System.Drawing.Size(728, 527);
			this.ResumeLayout(false);

		}

		#endregion

		private Manina.Windows.Forms.ImageListView themesListView;
	}
}
