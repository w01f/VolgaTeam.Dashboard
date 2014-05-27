namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	sealed partial class ProgramStrategyControl
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
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.gridControlItems = new DevExpress.XtraGrid.GridControl();
			this.persistentRepository = new DevExpress.XtraEditors.Repository.PersistentRepository();
			this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.advBandedGridViewItems = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandItemsEnabled = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnItemsEnabled = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandItemsLogo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnItemsLogo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandItemsInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnItemsName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnItemsDescription = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.favoriteImagesControl = new NewBizWiz.CommonGUI.FavoriteImages.FavoriteImagesControl();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.gridControlItems);
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.favoriteImagesControl);
			this.splitContainerControl.Panel2.MinSize = 300;
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(835, 623);
			this.splitContainerControl.SplitterPosition = 300;
			this.splitContainerControl.TabIndex = 0;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// gridControlItems
			// 
			this.gridControlItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlItems.ExternalRepository = this.persistentRepository;
			this.gridControlItems.Location = new System.Drawing.Point(0, 0);
			this.gridControlItems.MainView = this.advBandedGridViewItems;
			this.gridControlItems.Name = "gridControlItems";
			this.gridControlItems.Size = new System.Drawing.Size(529, 623);
			this.gridControlItems.TabIndex = 0;
			this.gridControlItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridViewItems,
            this.gridView1});
			// 
			// persistentRepository
			// 
			this.persistentRepository.Items.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit,
            this.repositoryItemPictureEdit});
			// 
			// repositoryItemCheckEdit
			// 
			this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
			this.repositoryItemCheckEdit.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit_CheckedChanged);
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.Padding = new System.Windows.Forms.Padding(15);
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.ShowMenu = false;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			// 
			// advBandedGridViewItems
			// 
			this.advBandedGridViewItems.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridViewItems.Appearance.BandPanel.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridViewItems.Appearance.Empty.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewItems.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewItems.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewItems.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewItems.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewItems.Appearance.OddRow.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.Row.BackColor = System.Drawing.Color.AliceBlue;
			this.advBandedGridViewItems.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewItems.Appearance.Row.Options.UseBackColor = true;
			this.advBandedGridViewItems.Appearance.Row.Options.UseFont = true;
			this.advBandedGridViewItems.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewItems.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridViewItems.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandItemsEnabled,
            this.gridBandItemsLogo,
            this.gridBandItemsInfo});
			this.advBandedGridViewItems.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnItemsEnabled,
            this.bandedGridColumnItemsLogo,
            this.bandedGridColumnItemsName,
            this.bandedGridColumnItemsDescription});
			this.advBandedGridViewItems.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
			this.advBandedGridViewItems.GridControl = this.gridControlItems;
			this.advBandedGridViewItems.Name = "advBandedGridViewItems";
			this.advBandedGridViewItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridViewItems.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridViewItems.OptionsBehavior.AutoPopulateColumns = false;
			this.advBandedGridViewItems.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridViewItems.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.advBandedGridViewItems.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridViewItems.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridViewItems.OptionsView.ShowBands = false;
			this.advBandedGridViewItems.OptionsView.ShowColumnHeaders = false;
			this.advBandedGridViewItems.OptionsView.ShowGroupPanel = false;
			this.advBandedGridViewItems.OptionsView.ShowIndicator = false;
			this.advBandedGridViewItems.RowHeight = 40;
			this.advBandedGridViewItems.RowSeparatorHeight = 15;
			this.advBandedGridViewItems.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.advBandedGridViewItems_RowCellStyle);
			this.advBandedGridViewItems.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.advBandedGridViewItems_PopupMenuShowing);
			this.advBandedGridViewItems.ShownEditor += new System.EventHandler(this.advBandedGridViewItems_ShownEditor);
			this.advBandedGridViewItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.advBandedGridViewItems_CellValueChanged);
			// 
			// gridBandItemsEnabled
			// 
			this.gridBandItemsEnabled.Caption = "Enabled";
			this.gridBandItemsEnabled.Columns.Add(this.bandedGridColumnItemsEnabled);
			this.gridBandItemsEnabled.Name = "gridBandItemsEnabled";
			this.gridBandItemsEnabled.OptionsBand.FixedWidth = true;
			this.gridBandItemsEnabled.RowCount = 3;
			this.gridBandItemsEnabled.Width = 40;
			// 
			// bandedGridColumnItemsEnabled
			// 
			this.bandedGridColumnItemsEnabled.Caption = "Enabled";
			this.bandedGridColumnItemsEnabled.ColumnEdit = this.repositoryItemCheckEdit;
			this.bandedGridColumnItemsEnabled.FieldName = "Enabled";
			this.bandedGridColumnItemsEnabled.Name = "bandedGridColumnItemsEnabled";
			this.bandedGridColumnItemsEnabled.RowCount = 4;
			this.bandedGridColumnItemsEnabled.Visible = true;
			this.bandedGridColumnItemsEnabled.Width = 40;
			// 
			// gridBandItemsLogo
			// 
			this.gridBandItemsLogo.Caption = "Logo";
			this.gridBandItemsLogo.Columns.Add(this.bandedGridColumnItemsLogo);
			this.gridBandItemsLogo.Name = "gridBandItemsLogo";
			this.gridBandItemsLogo.OptionsBand.FixedWidth = true;
			this.gridBandItemsLogo.RowCount = 3;
			this.gridBandItemsLogo.Width = 200;
			// 
			// bandedGridColumnItemsLogo
			// 
			this.bandedGridColumnItemsLogo.Caption = "Logo";
			this.bandedGridColumnItemsLogo.ColumnEdit = this.repositoryItemPictureEdit;
			this.bandedGridColumnItemsLogo.FieldName = "Logo";
			this.bandedGridColumnItemsLogo.Name = "bandedGridColumnItemsLogo";
			this.bandedGridColumnItemsLogo.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnItemsLogo.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnItemsLogo.RowCount = 4;
			this.bandedGridColumnItemsLogo.Visible = true;
			this.bandedGridColumnItemsLogo.Width = 200;
			// 
			// gridBandItemsInfo
			// 
			this.gridBandItemsInfo.Caption = "Info";
			this.gridBandItemsInfo.Columns.Add(this.bandedGridColumnItemsName);
			this.gridBandItemsInfo.Columns.Add(this.bandedGridColumnItemsDescription);
			this.gridBandItemsInfo.Name = "gridBandItemsInfo";
			this.gridBandItemsInfo.Width = 210;
			// 
			// bandedGridColumnItemsName
			// 
			this.bandedGridColumnItemsName.AppearanceCell.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bandedGridColumnItemsName.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnItemsName.Caption = "Name";
			this.bandedGridColumnItemsName.FieldName = "CompiledName";
			this.bandedGridColumnItemsName.Name = "bandedGridColumnItemsName";
			this.bandedGridColumnItemsName.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnItemsName.OptionsColumn.AllowFocus = false;
			this.bandedGridColumnItemsName.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnItemsName.RowCount = 2;
			this.bandedGridColumnItemsName.Visible = true;
			this.bandedGridColumnItemsName.Width = 210;
			// 
			// bandedGridColumnItemsDescription
			// 
			this.bandedGridColumnItemsDescription.AppearanceCell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bandedGridColumnItemsDescription.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnItemsDescription.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnItemsDescription.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.bandedGridColumnItemsDescription.Caption = "Description";
			this.bandedGridColumnItemsDescription.FieldName = "Description";
			this.bandedGridColumnItemsDescription.Name = "bandedGridColumnItemsDescription";
			this.bandedGridColumnItemsDescription.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnItemsDescription.OptionsColumn.AllowFocus = false;
			this.bandedGridColumnItemsDescription.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnItemsDescription.RowCount = 2;
			this.bandedGridColumnItemsDescription.RowIndex = 1;
			this.bandedGridColumnItemsDescription.Visible = true;
			this.bandedGridColumnItemsDescription.Width = 210;
			// 
			// gridView1
			// 
			this.gridView1.GridControl = this.gridControlItems;
			this.gridView1.Name = "gridView1";
			// 
			// favoriteImagesControl
			// 
			this.favoriteImagesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.favoriteImagesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.favoriteImagesControl.Location = new System.Drawing.Point(0, 0);
			this.favoriteImagesControl.Name = "favoriteImagesControl";
			this.favoriteImagesControl.Size = new System.Drawing.Size(300, 623);
			this.favoriteImagesControl.TabIndex = 0;
			// 
			// ProgramStrategyControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ProgramStrategyControl";
			this.Size = new System.Drawing.Size(835, 623);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private DevExpress.XtraGrid.GridControl gridControlItems;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewItems;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnItemsEnabled;
		private DevExpress.XtraEditors.Repository.PersistentRepository persistentRepository;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnItemsLogo;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnItemsName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnItemsDescription;
		private CommonGUI.FavoriteImages.FavoriteImagesControl favoriteImagesControl;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandItemsEnabled;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandItemsLogo;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandItemsInfo;
	}
}
