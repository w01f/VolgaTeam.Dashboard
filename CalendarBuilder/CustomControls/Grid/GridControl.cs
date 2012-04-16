using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.Grid
{
    public partial class GridControl : UserControl
    {
        private BusinessClasses.CalendarMonth _month = null;
        private bool _allowToSave = false;
        private List<BusinessClasses.DigitalProperties> _digitals = new List<BusinessClasses.DigitalProperties>();
        private List<BusinessClasses.NewspaperProperties> _newspapers = new List<BusinessClasses.NewspaperProperties>();
        public bool SettingsNotSaved { get; set; }

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesApplied;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesClosed;

        public GridControl()
        {
            InitializeComponent();

            repositoryItemTextEditCustomComment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemTextEditCustomComment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemTextEditCustomComment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxDigitalProduct.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxDigitalProduct.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxDigitalProduct.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxNewspaperPageSize.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxNewspaperPageSize.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxNewspaperPageSize.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxNewspaperSection.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxNewspaperSection.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxNewspaperSection.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
        }

        public void LoadMonth(BusinessClasses.CalendarMonth month)
        {
            _month = month;
            LoadCurrentMonthData();
        }

        public void LoadCurrentMonthData()
        {
            if (_month != null)
            {
                _allowToSave = false;

                #region Digital
                repositoryItemComboBoxDigitalCategory.Items.Clear();
                repositoryItemComboBoxDigitalCategory.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineCategories.Select(x => ("Web: " + x.Name)).ToArray());
                repositoryItemComboBoxDigitalCategory.Items.AddRange(BusinessClasses.ListManager.Instance.MobileCategories.Select(x => ("Mobile: " + x.Name)).ToArray());
                gridControlDigital.DataSource = null;
                _digitals.Clear();
                _digitals.AddRange(_month.Days.Where(x => x.BelongsToSchedules).Select(x => x.Digital).ToArray());
                gridControlDigital.DataSource = new BindingList<BusinessClasses.DigitalProperties>(_digitals);
                gridViewDigital.RefreshData();
                #endregion

                #region Newspaper
                repositoryItemComboBoxNewspaperPublication.Items.Clear();
                repositoryItemComboBoxNewspaperPublication.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSources.Select(x => x.Name).Distinct().ToArray());
                repositoryItemComboBoxNewspaperSection.Items.Clear();
                repositoryItemComboBoxNewspaperSection.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSections.Select(x => x.Name).ToArray());
                repositoryItemComboBoxNewspaperPageSize.Items.Clear();
                repositoryItemComboBoxNewspaperPageSize.Items.AddRange(BusinessClasses.ListManager.Instance.PrintPageSizes);
                gridControlNewspaper.DataSource = null;
                _newspapers.Clear();
                _newspapers.AddRange(_month.Days.Where(x => x.BelongsToSchedules).Select(x => x.Newspaper).ToArray());
                gridControlNewspaper.DataSource = new BindingList<BusinessClasses.NewspaperProperties>(_newspapers);
                gridViewNewspaper.RefreshData();
                #endregion

                _allowToSave = true;
                this.SettingsNotSaved = false;
            }
        }

        public void SaveData()
        {
            if (_allowToSave)
            {
                gridViewDigital.CloseEditor();
                gridViewNewspaper.CloseEditor();
                this.SettingsNotSaved = false;
            }
        }

        public BusinessClasses.CalendarDay GetSelectedDay()
        {
            BusinessClasses.CalendarDay day = null;
            if (gridViewDigital.FocusedRowHandle >= 0 && _month != null)
            {
                day = _month.Days[gridViewDigital.FocusedRowHandle];
            }
            return day;
        }

        private void barLargeButtonItemApply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            if (this.PropertiesApplied != null)
                this.PropertiesApplied(sender, e);
        }

        private void barLargeButtonItemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.PropertiesClosed != null)
                this.PropertiesClosed(sender, e);
        }

        private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                this.SettingsNotSaved = true;
        }

        private void repositoryItemComboBox_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            gridViewDigital.CloseEditor();
            gridViewNewspaper.CloseEditor();
        }

        private void gridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null && e.CellValue == null && e.RowHandle != view.FocusedRowHandle)
                e.Appearance.ForeColor = Color.Gray;
        }

        #region Digital Event Handlers
        private void gridViewDigital_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gridColumnDigitalCategory)
            {
                gridViewDigital.SetRowCellValue(e.RowHandle, gridColumnDigitalSubCategory, null);
                gridViewDigital.SetRowCellValue(e.RowHandle, gridColumnDigitalProduct, null);
            }
            else if (e.Column == gridColumnDigitalSubCategory)
                gridViewDigital.SetRowCellValue(e.RowHandle, gridColumnDigitalProduct, null);
            propertiesControl_PropertiesChanged(null, null);
        }

        private void gridViewDigital_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            if (gridViewDigital.FocusedColumn == gridColumnDigitalSubCategory)
            {
                string category = _digitals[gridViewDigital.GetFocusedDataSourceRowIndex()].Category;
                string message = string.Empty;
                repositoryItemComboBoxDigitalSubCategory.Items.Clear();
                if (!string.IsNullOrEmpty(category))
                {
                    if (category.Contains("Web: "))
                    {
                        category = category.Replace("Web: ", string.Empty);
                        repositoryItemComboBoxDigitalSubCategory.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
                    }
                    else if (category.Contains("Mobile: "))
                    {
                        category = category.Replace("Mobile: ", string.Empty);
                        repositoryItemComboBoxDigitalSubCategory.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
                    }
                    if (repositoryItemComboBoxDigitalSubCategory.Items.Count == 0)
                        message = "Sub-Group is not available for Selected Category";
                }
                else
                    message = "Select Category First";
                if (!string.IsNullOrEmpty(message))
                {
                    AppManager.ShowWarning(message);
                    e.Cancel = true;
                }

            }
            else if (gridViewDigital.FocusedColumn == gridColumnDigitalProduct)
            {
                string category = _digitals[gridViewDigital.GetFocusedDataSourceRowIndex()].Category;
                string subCategory = _digitals[gridViewDigital.GetFocusedDataSourceRowIndex()].SubCategory;
                repositoryItemComboBoxDigitalProduct.Items.Clear();
                repositoryItemComboBoxDigitalSubCategory.Items.Clear();
                if (!string.IsNullOrEmpty(category))
                {
                    if (category.Contains("Web: "))
                    {

                        category = category.Replace("Web: ", string.Empty);
                        repositoryItemComboBoxDigitalProduct.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
                    }
                    else if (category.Contains("Mobile: "))
                    {
                        category = category.Replace("Mobile: ", string.Empty);
                        repositoryItemComboBoxDigitalProduct.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
                    }
                }
            }
        }

        private void gridViewDigital_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage == xtraTabPageDigital)
            {
                gridViewNewspaper.FocusedRowHandle = gridViewDigital.FocusedRowHandle;
            }
        }
        #endregion

        #region Newspaper Event Handlers
        private void gridViewNewspaper_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            propertiesControl_PropertiesChanged(null, null);
        }

        private void gridViewNewspaper_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage == xtraTabPageNewspaper)
            {
                gridViewDigital.FocusedRowHandle = gridViewNewspaper.FocusedRowHandle;
            }
        }
        #endregion
       
    }
}
