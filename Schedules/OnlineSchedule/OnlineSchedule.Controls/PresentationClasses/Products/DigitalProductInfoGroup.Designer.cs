namespace Asa.Online.Controls.PresentationClasses.Products
{
	partial class DigitalProductInfoGroup
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
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnPhrase = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 0);
			this.gridControl.MainView = this.gridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit});
			this.gridControl.Size = new System.Drawing.Size(616, 463);
			this.gridControl.TabIndex = 0;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
			// 
			// gridView
			// 
			this.gridView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridView.Appearance.Empty.Options.UseFont = true;
			this.gridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.EvenRow.Options.UseFont = true;
			this.gridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedCell.Options.UseFont = true;
			this.gridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.OddRow.Options.UseFont = true;
			this.gridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridView.Appearance.Row.Options.UseFont = true;
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSelected,
            this.gridColumnPhrase});
			this.gridView.GridControl = this.gridControl;
			this.gridView.Name = "gridView";
			this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView.OptionsCustomization.AllowColumnMoving = false;
			this.gridView.OptionsCustomization.AllowColumnResizing = false;
			this.gridView.OptionsCustomization.AllowFilter = false;
			this.gridView.OptionsCustomization.AllowGroup = false;
			this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridView.OptionsCustomization.AllowSort = false;
			this.gridView.OptionsMenu.EnableColumnMenu = false;
			this.gridView.OptionsMenu.EnableFooterMenu = false;
			this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
			this.gridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridView.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridView.OptionsView.EnableAppearanceEvenRow = true;
			this.gridView.OptionsView.ShowColumnHeaders = false;
			this.gridView.OptionsView.ShowDetailButtons = false;
			this.gridView.OptionsView.ShowGroupPanel = false;
			this.gridView.OptionsView.ShowIndicator = false;
			this.gridView.RowHeight = 25;
			this.gridView.RowSeparatorHeight = 10;
			this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
			this.gridView.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView_ShowingEditor);
			// 
			// gridColumnSelected
			// 
			this.gridColumnSelected.Caption = "Selected";
			this.gridColumnSelected.ColumnEdit = this.repositoryItemCheckEdit;
			this.gridColumnSelected.FieldName = "Selected";
			this.gridColumnSelected.Name = "gridColumnSelected";
			this.gridColumnSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnSelected.Visible = true;
			this.gridColumnSelected.VisibleIndex = 0;
			this.gridColumnSelected.Width = 30;
			// 
			// repositoryItemCheckEdit
			// 
			this.repositoryItemCheckEdit.AutoHeight = false;
			this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
			this.repositoryItemCheckEdit.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit_CheckedChanged);
			// 
			// gridColumnPhrase
			// 
			this.gridColumnPhrase.Caption = "Phrase";
			this.gridColumnPhrase.FieldName = "EditValue";
			this.gridColumnPhrase.Name = "gridColumnPhrase";
			this.gridColumnPhrase.Visible = true;
			this.gridColumnPhrase.VisibleIndex = 1;
			this.gridColumnPhrase.Width = 582;
			// 
			// DigitalProductInfoGroup
			// 
			this.Controls.Add(this.gridControl);
			this.Size = new System.Drawing.Size(616, 463);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnSelected;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPhrase;
	}
}
