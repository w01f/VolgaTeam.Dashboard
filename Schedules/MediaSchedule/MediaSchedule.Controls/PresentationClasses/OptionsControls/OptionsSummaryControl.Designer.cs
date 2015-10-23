namespace Asa.MediaSchedule.Controls.PresentationClasses.OptionsControls
{
	sealed partial class OptionsSummaryControl
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandId = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandLogo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnLogo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.gridBandOtherColumns = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnComment = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.bandedGridColumnSpots = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditSpot = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.bandedGridColumnCost = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.bandedGridColumnTotalPeriods = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnPeriodCost = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSpot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 0);
			this.gridControl.MainView = this.advBandedGridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditRate,
            this.repositoryItemSpinEditSpot,
            this.repositoryItemPictureEdit,
            this.repositoryItemMemoEdit});
			this.gridControl.Size = new System.Drawing.Size(977, 563);
			this.gridControl.TabIndex = 6;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView});
			// 
			// advBandedGridView
			// 
			this.advBandedGridView.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridView.Appearance.BandPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridView.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
			this.advBandedGridView.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.FooterPanel.Options.UseForeColor = true;
			this.advBandedGridView.Appearance.FooterPanel.Options.UseTextOptions = true;
			this.advBandedGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.GroupFooter.Options.UseFont = true;
			this.advBandedGridView.Appearance.GroupFooter.Options.UseTextOptions = true;
			this.advBandedGridView.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.OddRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridView.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridView.AppearancePrint.BandPanel.Options.UseFont = true;
			this.advBandedGridView.AppearancePrint.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridView.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.advBandedGridView.AppearancePrint.EvenRow.Options.UseTextOptions = true;
			this.advBandedGridView.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.advBandedGridView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.advBandedGridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.AppearancePrint.OddRow.Options.UseFont = true;
			this.advBandedGridView.AppearancePrint.OddRow.Options.UseTextOptions = true;
			this.advBandedGridView.AppearancePrint.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.AppearancePrint.Row.Options.UseFont = true;
			this.advBandedGridView.AppearancePrint.Row.Options.UseTextOptions = true;
			this.advBandedGridView.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandId,
            this.gridBandLogo,
            this.gridBandOtherColumns});
			this.advBandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnIndex,
            this.bandedGridColumnLogo,
            this.bandedGridColumnName,
            this.bandedGridColumnComment,
            this.bandedGridColumnSpots,
            this.bandedGridColumnCost,
            this.bandedGridColumnTotalPeriods,
            this.bandedGridColumnPeriodCost});
			this.advBandedGridView.FooterPanelHeight = 0;
			this.advBandedGridView.GridControl = this.gridControl;
			this.advBandedGridView.Name = "advBandedGridView";
			this.advBandedGridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridView.OptionsCustomization.AllowBandMoving = false;
			this.advBandedGridView.OptionsCustomization.AllowBandResizing = false;
			this.advBandedGridView.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridView.OptionsCustomization.AllowColumnResizing = false;
			this.advBandedGridView.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.advBandedGridView.OptionsCustomization.AllowSort = false;
			this.advBandedGridView.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView.OptionsFilter.AllowFilterEditor = false;
			this.advBandedGridView.OptionsFind.AllowFindPanel = false;
			this.advBandedGridView.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridView.OptionsMenu.EnableFooterMenu = false;
			this.advBandedGridView.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridView.OptionsMenu.ShowAutoFilterRowItem = false;
			this.advBandedGridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.advBandedGridView.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridView.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridView.OptionsView.ShowBands = false;
			this.advBandedGridView.OptionsView.ShowDetailButtons = false;
			this.advBandedGridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.advBandedGridView.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView.OptionsView.ShowIndicator = false;
			this.advBandedGridView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.advBandedGridView_RowCellClick);
			this.advBandedGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.advBandedGridView_CellValueChanged);
			this.advBandedGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.advBandedGridView_MouseDown);
			// 
			// gridBandId
			// 
			this.gridBandId.Caption = "ID";
			this.gridBandId.Columns.Add(this.bandedGridColumnIndex);
			this.gridBandId.Name = "gridBandId";
			this.gridBandId.OptionsBand.FixedWidth = true;
			this.gridBandId.VisibleIndex = 0;
			this.gridBandId.Width = 35;
			// 
			// bandedGridColumnIndex
			// 
			this.bandedGridColumnIndex.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnIndex.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnIndex.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnIndex.AutoFillDown = true;
			this.bandedGridColumnIndex.Caption = "ID";
			this.bandedGridColumnIndex.FieldName = "DisplayIndex";
			this.bandedGridColumnIndex.Name = "bandedGridColumnIndex";
			this.bandedGridColumnIndex.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnIndex.OptionsColumn.AllowMove = false;
			this.bandedGridColumnIndex.OptionsColumn.AllowSize = false;
			this.bandedGridColumnIndex.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnIndex.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnIndex.RowCount = 3;
			this.bandedGridColumnIndex.Visible = true;
			this.bandedGridColumnIndex.Width = 35;
			// 
			// gridBandLogo
			// 
			this.gridBandLogo.Caption = "Logo";
			this.gridBandLogo.Columns.Add(this.bandedGridColumnLogo);
			this.gridBandLogo.Name = "gridBandLogo";
			this.gridBandLogo.OptionsBand.FixedWidth = true;
			this.gridBandLogo.VisibleIndex = 1;
			this.gridBandLogo.Width = 120;
			// 
			// bandedGridColumnLogo
			// 
			this.bandedGridColumnLogo.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnLogo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnLogo.AutoFillDown = true;
			this.bandedGridColumnLogo.Caption = "Logo";
			this.bandedGridColumnLogo.ColumnEdit = this.repositoryItemPictureEdit;
			this.bandedGridColumnLogo.FieldName = "SmallLogo";
			this.bandedGridColumnLogo.Name = "bandedGridColumnLogo";
			this.bandedGridColumnLogo.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnLogo.OptionsColumn.AllowMove = false;
			this.bandedGridColumnLogo.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnLogo.RowCount = 3;
			this.bandedGridColumnLogo.Visible = true;
			this.bandedGridColumnLogo.Width = 120;
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.AllowFocused = false;
			this.repositoryItemPictureEdit.Appearance.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.NullText = "No Logo";
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.ShowMenu = false;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			// 
			// gridBandOtherColumns
			// 
			this.gridBandOtherColumns.Caption = "Other";
			this.gridBandOtherColumns.Columns.Add(this.bandedGridColumnName);
			this.gridBandOtherColumns.Columns.Add(this.bandedGridColumnComment);
			this.gridBandOtherColumns.Columns.Add(this.bandedGridColumnSpots);
			this.gridBandOtherColumns.Columns.Add(this.bandedGridColumnCost);
			this.gridBandOtherColumns.Columns.Add(this.bandedGridColumnTotalPeriods);
			this.gridBandOtherColumns.Columns.Add(this.bandedGridColumnPeriodCost);
			this.gridBandOtherColumns.Name = "gridBandOtherColumns";
			this.gridBandOtherColumns.VisibleIndex = 2;
			this.gridBandOtherColumns.Width = 820;
			// 
			// bandedGridColumnName
			// 
			this.bandedGridColumnName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnName.AppearanceCell.Options.HighPriority = true;
			this.bandedGridColumnName.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnName.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnName.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnName.AutoFillDown = true;
			this.bandedGridColumnName.Caption = "Campaign";
			this.bandedGridColumnName.FieldName = "Name";
			this.bandedGridColumnName.MinWidth = 238;
			this.bandedGridColumnName.Name = "bandedGridColumnName";
			this.bandedGridColumnName.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnName.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnName.RowCount = 3;
			this.bandedGridColumnName.Visible = true;
			this.bandedGridColumnName.Width = 238;
			// 
			// bandedGridColumnComment
			// 
			this.bandedGridColumnComment.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnComment.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnComment.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnComment.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.bandedGridColumnComment.AutoFillDown = true;
			this.bandedGridColumnComment.Caption = "Comments";
			this.bandedGridColumnComment.ColumnEdit = this.repositoryItemMemoEdit;
			this.bandedGridColumnComment.FieldName = "Comment";
			this.bandedGridColumnComment.MinWidth = 238;
			this.bandedGridColumnComment.Name = "bandedGridColumnComment";
			this.bandedGridColumnComment.RowCount = 3;
			this.bandedGridColumnComment.Visible = true;
			this.bandedGridColumnComment.Width = 242;
			// 
			// repositoryItemMemoEdit
			// 
			this.repositoryItemMemoEdit.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemMemoEdit.Appearance.Options.UseFont = true;
			this.repositoryItemMemoEdit.Appearance.Options.UseTextOptions = true;
			this.repositoryItemMemoEdit.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.repositoryItemMemoEdit.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemMemoEdit.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemMemoEdit.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemMemoEdit.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.repositoryItemMemoEdit.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemMemoEdit.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemMemoEdit.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemMemoEdit.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.repositoryItemMemoEdit.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemMemoEdit.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemMemoEdit.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemMemoEdit.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.repositoryItemMemoEdit.LinesCount = 3;
			this.repositoryItemMemoEdit.Name = "repositoryItemMemoEdit";
			// 
			// bandedGridColumnSpots
			// 
			this.bandedGridColumnSpots.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bandedGridColumnSpots.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnSpots.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnSpots.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnSpots.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnSpots.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnSpots.AutoFillDown = true;
			this.bandedGridColumnSpots.Caption = "Weekly Spots";
			this.bandedGridColumnSpots.ColumnEdit = this.repositoryItemSpinEditSpot;
			this.bandedGridColumnSpots.DisplayFormat.FormatString = "#,##0";
			this.bandedGridColumnSpots.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.bandedGridColumnSpots.FieldName = "TotalSpots";
			this.bandedGridColumnSpots.MinWidth = 85;
			this.bandedGridColumnSpots.Name = "bandedGridColumnSpots";
			this.bandedGridColumnSpots.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnSpots.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnSpots.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnSpots.RowCount = 3;
			this.bandedGridColumnSpots.Visible = true;
			this.bandedGridColumnSpots.Width = 85;
			// 
			// repositoryItemSpinEditSpot
			// 
			this.repositoryItemSpinEditSpot.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.repositoryItemSpinEditSpot.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemSpinEditSpot.Appearance.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.Appearance.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditSpot.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditSpot.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AutoHeight = false;
			this.repositoryItemSpinEditSpot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.repositoryItemSpinEditSpot.DisplayFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditSpot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditSpot.EditFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditSpot.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditSpot.IsFloatValue = false;
			this.repositoryItemSpinEditSpot.Mask.EditMask = "N00";
			this.repositoryItemSpinEditSpot.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEditSpot.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.repositoryItemSpinEditSpot.Name = "repositoryItemSpinEditSpot";
			this.repositoryItemSpinEditSpot.NullText = "-";
			// 
			// bandedGridColumnCost
			// 
			this.bandedGridColumnCost.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnCost.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnCost.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnCost.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnCost.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnCost.AutoFillDown = true;
			this.bandedGridColumnCost.Caption = "Weekly Cost";
			this.bandedGridColumnCost.ColumnEdit = this.repositoryItemSpinEditRate;
			this.bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0";
			this.bandedGridColumnCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.bandedGridColumnCost.FieldName = "TotalCost";
			this.bandedGridColumnCost.MinWidth = 85;
			this.bandedGridColumnCost.Name = "bandedGridColumnCost";
			this.bandedGridColumnCost.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnCost.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnCost.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnCost.RowCount = 3;
			this.bandedGridColumnCost.Visible = true;
			this.bandedGridColumnCost.Width = 85;
			// 
			// repositoryItemSpinEditRate
			// 
			this.repositoryItemSpinEditRate.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.Appearance.Options.UseFont = true;
			this.repositoryItemSpinEditRate.Appearance.Options.UseTextOptions = true;
			this.repositoryItemSpinEditRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditRate.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemSpinEditRate.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemSpinEditRate.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemSpinEditRate.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditRate.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemSpinEditRate.AutoHeight = false;
			this.repositoryItemSpinEditRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0";
			this.repositoryItemSpinEditRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0";
			this.repositoryItemSpinEditRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditRate.IsFloatValue = false;
			this.repositoryItemSpinEditRate.Mask.EditMask = "N00";
			this.repositoryItemSpinEditRate.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEditRate.Name = "repositoryItemSpinEditRate";
			// 
			// bandedGridColumnTotalPeriods
			// 
			this.bandedGridColumnTotalPeriods.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnTotalPeriods.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnTotalPeriods.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnTotalPeriods.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnTotalPeriods.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnTotalPeriods.AutoFillDown = true;
			this.bandedGridColumnTotalPeriods.Caption = "Total Weeks";
			this.bandedGridColumnTotalPeriods.ColumnEdit = this.repositoryItemSpinEditSpot;
			this.bandedGridColumnTotalPeriods.DisplayFormat.FormatString = "#,##0";
			this.bandedGridColumnTotalPeriods.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.bandedGridColumnTotalPeriods.FieldName = "TotalPeriods";
			this.bandedGridColumnTotalPeriods.MinWidth = 85;
			this.bandedGridColumnTotalPeriods.Name = "bandedGridColumnTotalPeriods";
			this.bandedGridColumnTotalPeriods.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnTotalPeriods.RowCount = 3;
			this.bandedGridColumnTotalPeriods.Visible = true;
			this.bandedGridColumnTotalPeriods.Width = 85;
			// 
			// bandedGridColumnPeriodCost
			// 
			this.bandedGridColumnPeriodCost.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnPeriodCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnPeriodCost.AutoFillDown = true;
			this.bandedGridColumnPeriodCost.Caption = "Cost";
			this.bandedGridColumnPeriodCost.ColumnEdit = this.repositoryItemSpinEditRate;
			this.bandedGridColumnPeriodCost.DisplayFormat.FormatString = "$#,##0";
			this.bandedGridColumnPeriodCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.bandedGridColumnPeriodCost.FieldName = "TotalPeriodCost";
			this.bandedGridColumnPeriodCost.MinWidth = 85;
			this.bandedGridColumnPeriodCost.Name = "bandedGridColumnPeriodCost";
			this.bandedGridColumnPeriodCost.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnPeriodCost.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnPeriodCost.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnPeriodCost.RowCount = 3;
			this.bandedGridColumnPeriodCost.Visible = true;
			this.bandedGridColumnPeriodCost.Width = 85;
			// 
			// OptionsSummaryControl
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.gridControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Size = new System.Drawing.Size(977, 563);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSpot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		protected DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnIndex;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnLogo;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnName;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnComment;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnSpots;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditSpot;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditRate;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnCost;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandId;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLogo;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandOtherColumns;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTotalPeriods;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnPeriodCost;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit;
	}
}
