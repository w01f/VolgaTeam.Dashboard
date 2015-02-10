namespace NewBizWiz.Calendar.Controls.ToolForms
{
    partial class FormImageGallery
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageGallery));
			this.pnBottom = new System.Windows.Forms.Panel();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.imageListView = new Manina.Windows.Forms.ImageListView();
			this.pnBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnBottom
			// 
			this.pnBottom.BackColor = System.Drawing.Color.Transparent;
			this.pnBottom.Controls.Add(this.buttonXCancel);
			this.pnBottom.Controls.Add(this.buttonXOK);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.ForeColor = System.Drawing.Color.Black;
			this.pnBottom.Location = new System.Drawing.Point(0, 361);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(392, 43);
			this.pnBottom.TabIndex = 1;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(305, 6);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(75, 31);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 1;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(222, 6);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(75, 31);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 0;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// imageListView
			// 
			this.imageListView.AllowDrag = true;
			this.imageListView.BackColor = System.Drawing.Color.White;
			this.imageListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imageListView.Colors = new Manina.Windows.Forms.ImageListViewColor(resources.GetString("imageListView.Colors"));
			this.imageListView.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.imageListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageListView.ForeColor = System.Drawing.Color.Black;
			this.imageListView.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.imageListView.Location = new System.Drawing.Point(0, 0);
			this.imageListView.MultiSelect = false;
			this.imageListView.Name = "imageListView";
			this.imageListView.PersistentCacheDirectory = "";
			this.imageListView.PersistentCacheSize = ((long)(100));
			this.imageListView.Size = new System.Drawing.Size(392, 361);
			this.imageListView.TabIndex = 41;
			this.imageListView.ThumbnailSize = new System.Drawing.Size(120, 54);
			this.imageListView.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView_ItemDoubleClick);
			this.imageListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageListView_MouseMove);
			// 
			// FormImageGallery
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(392, 404);
			this.Controls.Add(this.imageListView);
			this.Controls.Add(this.pnBottom);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 430);
			this.Name = "FormImageGallery";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Logo";
			this.Load += new System.EventHandler(this.FormImageGallery_Load);
			this.pnBottom.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnBottom;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
		private Manina.Windows.Forms.ImageListView imageListView;


    }
}