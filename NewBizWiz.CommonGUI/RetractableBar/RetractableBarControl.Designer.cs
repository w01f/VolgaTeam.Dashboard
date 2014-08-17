namespace NewBizWiz.CommonGUI.RetractableBar
{
	partial class RetractableBarControl
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
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			this.pnClosed = new DevExpress.XtraEditors.PanelControl();
			this.pictureBoxImage = new System.Windows.Forms.PictureBox();
			this.simpleButtonExpand = new DevExpress.XtraEditors.SimpleButton();
			this.pnOpened = new DevExpress.XtraEditors.PanelControl();
			this.pnContent = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			this.simpleButtonCollapse = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).BeginInit();
			this.pnClosed.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).BeginInit();
			this.pnOpened.SuspendLayout();
			this.pnTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnClosed
			// 
			this.pnClosed.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnClosed.Appearance.Options.UseBackColor = true;
			this.pnClosed.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnClosed.Controls.Add(this.pictureBoxImage);
			this.pnClosed.Controls.Add(this.simpleButtonExpand);
			this.pnClosed.Location = new System.Drawing.Point(0, 149);
			this.pnClosed.Name = "pnClosed";
			this.pnClosed.Size = new System.Drawing.Size(55, 485);
			this.pnClosed.TabIndex = 0;
			this.pnClosed.Visible = false;
			// 
			// pictureBoxImage
			// 
			this.pictureBoxImage.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxImage.Image = global::NewBizWiz.CommonGUI.Properties.Resources.RetractableBarLogo;
			this.pictureBoxImage.Location = new System.Drawing.Point(4, 42);
			this.pictureBoxImage.Name = "pictureBoxImage";
			this.pictureBoxImage.Size = new System.Drawing.Size(47, 420);
			this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxImage.TabIndex = 1;
			this.pictureBoxImage.TabStop = false;
			this.pictureBoxImage.Click += new System.EventHandler(this.simpleButtonExpand_Click);
			// 
			// simpleButtonExpand
			// 
			this.simpleButtonExpand.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonExpand.Image = global::NewBizWiz.CommonGUI.Properties.Resources.RetractableBarExpand;
			this.simpleButtonExpand.Location = new System.Drawing.Point(4, 5);
			this.simpleButtonExpand.Name = "simpleButtonExpand";
			this.simpleButtonExpand.Size = new System.Drawing.Size(47, 32);
			toolTipTitleItem1.Text = "Expand";
			toolTipItem1.LeftIndent = 6;
			toolTipItem1.Text = "Expand bar";
			superToolTip1.Items.Add(toolTipTitleItem1);
			superToolTip1.Items.Add(toolTipItem1);
			this.simpleButtonExpand.SuperTip = superToolTip1;
			this.simpleButtonExpand.TabIndex = 0;
			this.simpleButtonExpand.Click += new System.EventHandler(this.simpleButtonExpand_Click);
			// 
			// pnOpened
			// 
			this.pnOpened.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnOpened.Appearance.Options.UseBackColor = true;
			this.pnOpened.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnOpened.Controls.Add(this.pnContent);
			this.pnOpened.Controls.Add(this.pnTop);
			this.pnOpened.Location = new System.Drawing.Point(140, 84);
			this.pnOpened.Name = "pnOpened";
			this.pnOpened.Size = new System.Drawing.Size(219, 347);
			this.pnOpened.TabIndex = 1;
			// 
			// pnContent
			// 
			this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContent.Location = new System.Drawing.Point(2, 42);
			this.pnContent.Name = "pnContent";
			this.pnContent.Size = new System.Drawing.Size(215, 303);
			this.pnContent.TabIndex = 1;
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.simpleButtonCollapse);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(2, 2);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(215, 40);
			this.pnTop.TabIndex = 0;
			// 
			// simpleButtonCollapse
			// 
			this.simpleButtonCollapse.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonCollapse.Image = global::NewBizWiz.CommonGUI.Properties.Resources.RetractableBarCollapse;
			this.simpleButtonCollapse.Location = new System.Drawing.Point(4, 3);
			this.simpleButtonCollapse.Name = "simpleButtonCollapse";
			this.simpleButtonCollapse.Size = new System.Drawing.Size(45, 32);
			toolTipTitleItem2.Text = "Collapse";
			toolTipItem2.LeftIndent = 6;
			toolTipItem2.Text = "Collapse bar";
			superToolTip2.Items.Add(toolTipTitleItem2);
			superToolTip2.Items.Add(toolTipItem2);
			this.simpleButtonCollapse.SuperTip = superToolTip2;
			this.simpleButtonCollapse.TabIndex = 1;
			this.simpleButtonCollapse.Click += new System.EventHandler(this.simpleButtonCollapse_Click);
			// 
			// RetractableBarControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pnOpened);
			this.Controls.Add(this.pnClosed);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "RetractableBarControl";
			this.Size = new System.Drawing.Size(359, 655);
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).EndInit();
			this.pnClosed.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).EndInit();
			this.pnOpened.ResumeLayout(false);
			this.pnTop.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.PanelControl pnClosed;
		protected DevExpress.XtraEditors.PanelControl pnOpened;
		protected System.Windows.Forms.PictureBox pictureBoxImage;
		protected DevExpress.XtraEditors.SimpleButton simpleButtonExpand;
		protected System.Windows.Forms.Panel pnTop;
		protected System.Windows.Forms.Panel pnContent;
		protected DevExpress.XtraEditors.SimpleButton simpleButtonCollapse;

	}
}
