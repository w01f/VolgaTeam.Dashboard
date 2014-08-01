namespace NewBizWiz.CommonGUI.Slides
{
	partial class SlideGroupControl
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
			this.gridControlSlides = new DevExpress.XtraGrid.GridControl();
			this.layoutViewSlides = new DevExpress.XtraGrid.Views.Layout.LayoutView();
			this.layoutViewColumnLogo = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.layoutViewFieldLogo = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewColumnTitle = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.layoutViewFieldTitle = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSlides)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewSlides)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlSlides
			// 
			this.gridControlSlides.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlSlides.Location = new System.Drawing.Point(0, 0);
			this.gridControlSlides.MainView = this.layoutViewSlides;
			this.gridControlSlides.Name = "gridControlSlides";
			this.gridControlSlides.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			this.gridControlSlides.Size = new System.Drawing.Size(728, 527);
			this.gridControlSlides.TabIndex = 0;
			this.gridControlSlides.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewSlides});
			this.gridControlSlides.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridControlSlides_MouseMove);
			// 
			// layoutViewSlides
			// 
			this.layoutViewSlides.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.layoutViewSlides.CardHorzInterval = 16;
			this.layoutViewSlides.CardMinSize = new System.Drawing.Size(255, 170);
			this.layoutViewSlides.CardVertInterval = 10;
			this.layoutViewSlides.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.layoutViewColumnLogo,
            this.layoutViewColumnTitle});
			this.layoutViewSlides.GridControl = this.gridControlSlides;
			this.layoutViewSlides.Name = "layoutViewSlides";
			this.layoutViewSlides.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewSlides.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewSlides.OptionsBehavior.AllowExpandCollapse = false;
			this.layoutViewSlides.OptionsBehavior.AllowPanCards = false;
			this.layoutViewSlides.OptionsBehavior.AllowRuntimeCustomization = false;
			this.layoutViewSlides.OptionsBehavior.Editable = false;
			this.layoutViewSlides.OptionsBehavior.ReadOnly = true;
			this.layoutViewSlides.OptionsCustomization.AllowFilter = false;
			this.layoutViewSlides.OptionsCustomization.AllowSort = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupCardCaptions = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupCardIndents = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupCards = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupFields = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupHiddenItems = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupLayout = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupLayoutTreeView = false;
			this.layoutViewSlides.OptionsCustomization.ShowGroupView = false;
			this.layoutViewSlides.OptionsCustomization.ShowResetShrinkButtons = false;
			this.layoutViewSlides.OptionsCustomization.ShowSaveLoadLayoutButtons = false;
			this.layoutViewSlides.OptionsItemText.TextToControlDistance = 0;
			this.layoutViewSlides.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewSlides.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewSlides.OptionsView.AllowHotTrackFields = false;
			this.layoutViewSlides.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutViewSlides.OptionsView.FocusRectStyle = DevExpress.XtraGrid.Views.Layout.FocusRectStyle.None;
			this.layoutViewSlides.OptionsView.ShowCardCaption = false;
			this.layoutViewSlides.OptionsView.ShowCardExpandButton = false;
			this.layoutViewSlides.OptionsView.ShowCardLines = false;
			this.layoutViewSlides.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.layoutViewSlides.OptionsView.ShowHeaderPanel = false;
			this.layoutViewSlides.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
			this.layoutViewSlides.TemplateCard = this.layoutViewCard1;
			this.layoutViewSlides.CustomFieldValueStyle += new DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventHandler(this.layoutViewSlides_CustomFieldValueStyle);
			this.layoutViewSlides.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.layoutViewSlides_FocusedRowChanged);
			this.layoutViewSlides.DoubleClick += new System.EventHandler(this.layoutViewSlides_DoubleClick);
			// 
			// layoutViewColumnLogo
			// 
			this.layoutViewColumnLogo.Caption = "Logo";
			this.layoutViewColumnLogo.ColumnEdit = this.repositoryItemPictureEdit;
			this.layoutViewColumnLogo.FieldName = "BrowseLogo";
			this.layoutViewColumnLogo.LayoutViewField = this.layoutViewFieldLogo;
			this.layoutViewColumnLogo.Name = "layoutViewColumnLogo";
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.ShowMenu = false;
			// 
			// layoutViewFieldLogo
			// 
			this.layoutViewFieldLogo.EditorPreferredWidth = 269;
			this.layoutViewFieldLogo.Location = new System.Drawing.Point(0, 0);
			this.layoutViewFieldLogo.Name = "layoutViewFieldLogo";
			this.layoutViewFieldLogo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutViewFieldLogo.Size = new System.Drawing.Size(269, 154);
			this.layoutViewFieldLogo.TextLocation = DevExpress.Utils.Locations.Default;
			this.layoutViewFieldLogo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewFieldLogo.TextToControlDistance = 0;
			this.layoutViewFieldLogo.TextVisible = false;
			// 
			// layoutViewColumnTitle
			// 
			this.layoutViewColumnTitle.AppearanceCell.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
			this.layoutViewColumnTitle.AppearanceCell.Options.UseFont = true;
			this.layoutViewColumnTitle.AppearanceCell.Options.UseTextOptions = true;
			this.layoutViewColumnTitle.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layoutViewColumnTitle.AppearanceHeader.Options.UseTextOptions = true;
			this.layoutViewColumnTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layoutViewColumnTitle.Caption = "Title";
			this.layoutViewColumnTitle.FieldName = "Name";
			this.layoutViewColumnTitle.LayoutViewField = this.layoutViewFieldTitle;
			this.layoutViewColumnTitle.Name = "layoutViewColumnTitle";
			// 
			// layoutViewFieldTitle
			// 
			this.layoutViewFieldTitle.EditorPreferredWidth = 269;
			this.layoutViewFieldTitle.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutViewFieldTitle.Location = new System.Drawing.Point(0, 154);
			this.layoutViewFieldTitle.Name = "layoutViewFieldTitle";
			this.layoutViewFieldTitle.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutViewFieldTitle.Size = new System.Drawing.Size(269, 16);
			this.layoutViewFieldTitle.TextLocation = DevExpress.Utils.Locations.Default;
			this.layoutViewFieldTitle.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewFieldTitle.TextToControlDistance = 0;
			this.layoutViewFieldTitle.TextVisible = false;
			// 
			// layoutViewCard1
			// 
			this.layoutViewCard1.CustomizationFormText = "TemplateCard";
			this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.layoutViewCard1.GroupBordersVisible = false;
			this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewFieldLogo,
            this.layoutViewFieldTitle});
			this.layoutViewCard1.Name = "layoutViewCard1";
			this.layoutViewCard1.OptionsItemText.TextToControlDistance = 0;
			this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutViewCard1.Text = "TemplateCard";
			// 
			// SlideGroupControl
			// 
			this.Controls.Add(this.gridControlSlides);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(728, 527);
			((System.ComponentModel.ISupportInitialize)(this.gridControlSlides)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewSlides)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControlSlides;
		private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewSlides;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumnLogo;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumnTitle;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewFieldLogo;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewFieldTitle;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
	}
}
