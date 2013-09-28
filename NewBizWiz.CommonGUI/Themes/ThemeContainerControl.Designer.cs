namespace NewBizWiz.CommonGUI.Themes
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
			this.gridControlThemes = new DevExpress.XtraGrid.GridControl();
			this.layoutViewThemes = new DevExpress.XtraGrid.Views.Layout.LayoutView();
			this.layoutViewColumnLogo = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.layoutViewFieldLogo = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewColumnTitle = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.layoutViewFieldTitle = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
			((System.ComponentModel.ISupportInitialize)(this.gridControlThemes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewThemes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlThemes
			// 
			this.gridControlThemes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlThemes.Location = new System.Drawing.Point(0, 0);
			this.gridControlThemes.MainView = this.layoutViewThemes;
			this.gridControlThemes.Name = "gridControlThemes";
			this.gridControlThemes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			this.gridControlThemes.Size = new System.Drawing.Size(728, 527);
			this.gridControlThemes.TabIndex = 0;
			this.gridControlThemes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewThemes});
			// 
			// layoutViewThemes
			// 
			this.layoutViewThemes.CardHorzInterval = 16;
			this.layoutViewThemes.CardMinSize = new System.Drawing.Size(255, 170);
			this.layoutViewThemes.CardVertInterval = 10;
			this.layoutViewThemes.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.layoutViewColumnLogo,
            this.layoutViewColumnTitle});
			this.layoutViewThemes.GridControl = this.gridControlThemes;
			this.layoutViewThemes.Name = "layoutViewThemes";
			this.layoutViewThemes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewThemes.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewThemes.OptionsBehavior.AllowExpandCollapse = false;
			this.layoutViewThemes.OptionsBehavior.AllowPanCards = false;
			this.layoutViewThemes.OptionsBehavior.AllowRuntimeCustomization = false;
			this.layoutViewThemes.OptionsBehavior.Editable = false;
			this.layoutViewThemes.OptionsBehavior.ReadOnly = true;
			this.layoutViewThemes.OptionsCustomization.AllowFilter = false;
			this.layoutViewThemes.OptionsCustomization.AllowSort = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupCardCaptions = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupCardIndents = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupCards = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupFields = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupHiddenItems = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupLayout = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupLayoutTreeView = false;
			this.layoutViewThemes.OptionsCustomization.ShowGroupView = false;
			this.layoutViewThemes.OptionsCustomization.ShowResetShrinkButtons = false;
			this.layoutViewThemes.OptionsCustomization.ShowSaveLoadLayoutButtons = false;
			this.layoutViewThemes.OptionsItemText.TextToControlDistance = 0;
			this.layoutViewThemes.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewThemes.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewThemes.OptionsView.AllowHotTrackFields = false;
			this.layoutViewThemes.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutViewThemes.OptionsView.FocusRectStyle = DevExpress.XtraGrid.Views.Layout.FocusRectStyle.None;
			this.layoutViewThemes.OptionsView.ShowCardCaption = false;
			this.layoutViewThemes.OptionsView.ShowCardExpandButton = false;
			this.layoutViewThemes.OptionsView.ShowCardLines = false;
			this.layoutViewThemes.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.layoutViewThemes.OptionsView.ShowHeaderPanel = false;
			this.layoutViewThemes.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
			this.layoutViewThemes.TemplateCard = this.layoutViewCard1;
			this.layoutViewThemes.CustomFieldValueStyle += new DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventHandler(this.layoutViewThemes_CustomFieldValueStyle);
			this.layoutViewThemes.DoubleClick += new System.EventHandler(this.layoutViewThemes_DoubleClick);
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
			// ThemeContainerControl
			// 
			this.Controls.Add(this.gridControlThemes);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ThemeContainerControl";
			this.Size = new System.Drawing.Size(728, 527);
			((System.ComponentModel.ISupportInitialize)(this.gridControlThemes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewThemes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControlThemes;
		private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewThemes;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumnLogo;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumnTitle;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewFieldLogo;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewFieldTitle;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
	}
}
