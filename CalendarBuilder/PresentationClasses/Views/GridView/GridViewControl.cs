using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Views.GridView
{
    public partial class GridViewControl : UserControl, IView
    {
        private bool _allowToSave = false;
        private List<BusinessClasses.DigitalProperties> _digitals = new List<BusinessClasses.DigitalProperties>();
        private List<BusinessClasses.NewspaperProperties> _newspapers = new List<BusinessClasses.NewspaperProperties>();

        public ICalendarControl Calendar { get; private set; }
        public SelectionManager SelectionManager { get; private set; }
        public CopyPasteManager CopyPasteManager { get; private set; }

        public bool SettingsNotSaved { get; set; }
        public event EventHandler<EventArgs> DataSaved;

        public GridViewControl(ICalendarControl calendar)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Calendar = calendar;
            this.SelectionManager = new SelectionManager(this);
            
            #region Copy-Paster Initialization
            this.CopyPasteManager = new CopyPasteManager();
            this.CopyPasteManager.OnSetCopy += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.CopyButtonItem.Enabled = true;
                CalendarVisualizer.Instance.CloneButtonItem.Enabled = true;
            });
            this.CopyPasteManager.OnSetPaste += new EventHandler<EventArgs>((sender, e) =>
            {
            });
            this.CopyPasteManager.OnResetCopy += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.CopyButtonItem.Enabled = false;
                CalendarVisualizer.Instance.CloneButtonItem.Enabled = false;
            });
            this.CopyPasteManager.OnResetPaste += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.PasteButtonItem.Enabled = false;
            });
            this.CopyPasteManager.DayCopied += new EventHandler<EventArgs>((sender, e) =>
            {
                CalendarVisualizer.Instance.PasteButtonItem.Enabled = true;
            });

            this.CopyPasteManager.DayPasted += new EventHandler<EventArgs>((sender, e) =>
            {
                this.Calendar.DayProperties.LoadData();
                this.Calendar.SlideInfo.LoadData(reload: true);
                RefreshData();
                this.SettingsNotSaved = true;
            });
            #endregion

            repositoryItemTextEditCustomComment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemTextEditCustomComment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemTextEditCustomComment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemButtonEditCustomCommentFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemButtonEditCustomCommentFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemButtonEditCustomCommentFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxDigitalProduct.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxDigitalProduct.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxDigitalProduct.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxDigitalProductFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxDigitalProductFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxDigitalProductFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxNewspaperPageSize.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxNewspaperPageSize.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxNewspaperPageSize.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxNewspaperPageSizeFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxNewspaperPageSizeFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxNewspaperPageSizeFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxNewspaperSection.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxNewspaperSection.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxNewspaperSection.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemComboBoxNewspaperSectionFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemComboBoxNewspaperSectionFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemComboBoxNewspaperSectionFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditNewspaperTotalCost.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditNewspaperTotalCost.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditNewspaperTotalCost.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditNewspaperTotalCostFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditNewspaperTotalCostFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditNewspaperTotalCostFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
        }

        #region Interface Methods
        public void LoadData(bool reload = false)
        {
            #region Digital
            repositoryItemComboBoxDigitalCategory.Items.Clear();
            repositoryItemComboBoxDigitalCategory.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineCategories.Select(x => ("Web: " + x.Name)).ToArray());
            repositoryItemComboBoxDigitalCategory.Items.AddRange(BusinessClasses.ListManager.Instance.MobileCategories.Select(x => ("Mobile: " + x.Name)).ToArray());
            repositoryItemComboBoxDigitalCategoryFirstRow.Items.Clear();
            repositoryItemComboBoxDigitalCategoryFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineCategories.Select(x => ("Web: " + x.Name)).ToArray());
            repositoryItemComboBoxDigitalCategoryFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.MobileCategories.Select(x => ("Mobile: " + x.Name)).ToArray());
            #endregion

            #region Newspaper
            repositoryItemComboBoxNewspaperPublication.Items.Clear();
            repositoryItemComboBoxNewspaperPublication.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSources.Select(x => x.Name).Distinct().ToArray());
            repositoryItemComboBoxNewspaperPublicationFirstRow.Items.Clear();
            repositoryItemComboBoxNewspaperPublicationFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSources.Select(x => x.Name).Distinct().ToArray());
            repositoryItemComboBoxNewspaperSection.Items.Clear();
            repositoryItemComboBoxNewspaperSection.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSections.Select(x => x.Name).ToArray());
            repositoryItemComboBoxNewspaperSectionFirstRow.Items.Clear();
            repositoryItemComboBoxNewspaperSectionFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSections.Select(x => x.Name).ToArray());
            repositoryItemComboBoxNewspaperPageSize.Items.Clear();
            repositoryItemComboBoxNewspaperPageSize.Items.AddRange(BusinessClasses.ListManager.Instance.PrintPageSizes);
            repositoryItemComboBoxNewspaperPageSizeFirstRow.Items.Clear();
            repositoryItemComboBoxNewspaperPageSizeFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.PrintPageSizes);
            #endregion

            this.SettingsNotSaved = false;
        }

        public void Save()
        {
            gridViewDigital.CloseEditor();
            gridViewNewspaper.CloseEditor();
            if (this.DataSaved != null)
                this.DataSaved(this, new EventArgs());
            this.SettingsNotSaved = false;
        }

        public void RefreshData()
        {
            gridViewNewspaper.RefreshData();
            gridViewDigital.RefreshData();
        }

        public void ChangeMonth(DateTime date)
        {
            BusinessClasses.CalendarMonth calendarMonth = null;
            this.SelectionManager.ClearSelection();
            this.CopyPasteManager.ResetCopy();
            calendarMonth = this.Calendar.CalendarData.Months.Where(x => x.StartDate.Equals(date)).FirstOrDefault();
            if (calendarMonth != null)
            {
                _allowToSave = false;

                #region Digital
                gridControlDigital.DataSource = null;
                _digitals.Clear();
                _digitals.AddRange(calendarMonth.Days.Where(x => x.BelongsToSchedules).Select(x => x.Digital).ToArray());
                gridControlDigital.DataSource = new BindingList<BusinessClasses.DigitalProperties>(_digitals);
                gridViewDigital.RefreshData();
                #endregion

                #region Newspaper
                gridControlNewspaper.DataSource = null;
                _newspapers.Clear();
                _newspapers.AddRange(calendarMonth.Days.Where(x => x.BelongsToSchedules).Select(x => x.Newspaper).ToArray());
                gridControlNewspaper.DataSource = new BindingList<BusinessClasses.NewspaperProperties>(_newspapers);
                gridViewNewspaper.RefreshData();
                #endregion

                _allowToSave = true;
            }
        }

        public void SelectDay(BusinessClasses.CalendarDay day, bool selected)
        {
        }

        public void Decorate(BusinessClasses.CalendarStyle style)
        {
            xtraTabPageDigital.PageVisible = style == BusinessClasses.CalendarStyle.Advanced;
            xtraTabPageNewspaper.PageVisible = style == BusinessClasses.CalendarStyle.Advanced;
        }

        #region Copy-Paste Methods and Event Handlers
        public void CopyDay()
        {
            BusinessClasses.CalendarDay selectedDay = this.SelectionManager.SelectedDays.FirstOrDefault();
            if (selectedDay != null)
                this.CopyPasteManager.Copy(selectedDay);
        }

        public void PasteDay()
        {
            BusinessClasses.CalendarDay[] selectedDays = this.SelectionManager.SelectedDays.ToArray();
            if (selectedDays != null)
                this.CopyPasteManager.Paste(selectedDays);
        }

        public void CloneDay()
        {
            BusinessClasses.CalendarDay[] clonedDays = null;
            BusinessClasses.CalendarDay selectedDay = this.SelectionManager.SelectedDays.FirstOrDefault();
            if (selectedDay != null)
            {
                using (ToolForms.FormCloneDay form = new ToolForms.FormCloneDay(selectedDay, this.Calendar.CalendarData.Schedule.FlightDateStart.Value, this.Calendar.CalendarData.Schedule.FlightDateEnd.Value))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        clonedDays = this.Calendar.CalendarData.Days.Where(x => form.SelectedDates.Contains(x.Date)).ToArray();
                }
                if (clonedDays != null)
                    this.CopyPasteManager.Clone(selectedDay, clonedDays);
            }
        }
        #endregion
        #endregion

        #region Common Event Handlers
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
        #endregion

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
                repositoryItemComboBoxDigitalSubCategoryFirstRow.Items.Clear();
                if (!string.IsNullOrEmpty(category))
                {
                    if (category.Contains("Web: "))
                    {
                        category = category.Replace("Web: ", string.Empty);
                        repositoryItemComboBoxDigitalSubCategory.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
                        repositoryItemComboBoxDigitalSubCategoryFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
                    }
                    else if (category.Contains("Mobile: "))
                    {
                        category = category.Replace("Mobile: ", string.Empty);
                        repositoryItemComboBoxDigitalSubCategory.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
                        repositoryItemComboBoxDigitalSubCategoryFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
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
                repositoryItemComboBoxDigitalProductFirstRow.Items.Clear();
                repositoryItemComboBoxDigitalSubCategory.Items.Clear();
                repositoryItemComboBoxDigitalSubCategoryFirstRow.Items.Clear();
                if (!string.IsNullOrEmpty(category))
                {
                    if (category.Contains("Web: "))
                    {

                        category = category.Replace("Web: ", string.Empty);
                        repositoryItemComboBoxDigitalProduct.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
                        repositoryItemComboBoxDigitalProductFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
                    }
                    else if (category.Contains("Mobile: "))
                    {
                        category = category.Replace("Mobile: ", string.Empty);
                        repositoryItemComboBoxDigitalProduct.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
                        repositoryItemComboBoxDigitalProductFirstRow.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
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

        private void gridViewDigital_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnDigitalCategory)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxDigitalCategoryDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxDigitalCategoryDisplay;
            }
            else if (e.Column == gridColumnDigitalSubCategory)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxDigitalSubCategoryDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxDigitalSubCategoryDisplay;
            }
            else if (e.Column == gridColumnDigitalProduct)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxDigitalProductDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxDigitalProductDisplay;
            }
            else if (e.Column == gridColumnDigitalCustomNote)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemButtonEditCustomCommentFirstRow;
                else
                    e.RepositoryItem = repositoryItemTextEditCustomComment;
            }
        }

        private void gridViewDigital_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnDigitalCategory)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxDigitalCategoryFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxDigitalCategory;
            }
            else if (e.Column == gridColumnDigitalSubCategory)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxDigitalSubCategoryFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxDigitalSubCategory;
            }
            else if (e.Column == gridColumnDigitalProduct)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxDigitalProductFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxDigitalProduct;
            }
            else if (e.Column == gridColumnDigitalCustomNote)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemButtonEditCustomCommentFirstRow;
                else
                    e.RepositoryItem = repositoryItemTextEditCustomComment;
            }
        }

        private void repositoryItemComboBoxDigitalCategoryFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewDigital.CloseEditor();
                object value = gridViewDigital.GetRowCellValue(0, gridColumnDigitalCategory);
                for (int i = 1; i < gridViewDigital.RowCount; i++)
                {
                    gridViewDigital.SetRowCellValue(i, gridColumnDigitalCategory, value);
                }
            }
        }

        private void repositoryItemComboBoxDigitalSubCategoryFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewDigital.CloseEditor();
                object value = gridViewDigital.GetRowCellValue(0, gridColumnDigitalSubCategory);
                for (int i = 1; i < gridViewDigital.RowCount; i++)
                {
                    gridViewDigital.SetRowCellValue(i, gridColumnDigitalSubCategory, value);
                }
            }
        }

        private void repositoryItemComboBoxDigitalProductFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewDigital.CloseEditor();
                object value = gridViewDigital.GetRowCellValue(0, gridColumnDigitalProduct);
                for (int i = 1; i < gridViewDigital.RowCount; i++)
                {
                    gridViewDigital.SetRowCellValue(i, gridColumnDigitalProduct, value);
                }
            }
        }

        private void repositoryItemComboBoxCustomNoteFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage == xtraTabPageDigital)
            {
                gridViewDigital.CloseEditor();
                object value = gridViewDigital.GetRowCellValue(0, gridColumnDigitalCustomNote);
                for (int i = 1; i < gridViewDigital.RowCount; i++)
                {
                    gridViewDigital.SetRowCellValue(i, gridColumnDigitalCustomNote, value);
                }
            }
            else if (xtraTabControl.SelectedTabPage == xtraTabPageNewspaper)
            {
                gridViewNewspaper.CloseEditor();
                object value = gridViewNewspaper.GetRowCellValue(0, gridColumnNewspaperCustomNote);
                for (int i = 1; i < gridViewNewspaper.RowCount; i++)
                {
                    gridViewNewspaper.SetRowCellValue(i, gridColumnNewspaperCustomNote, value);
                }
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

        private void gridViewNewspaper_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnNewspaperPublication)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPublicationDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPublicationDisplay;
            }
            else if (e.Column == gridColumnNewspaperSection)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperSectionDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperSectionDisplay;
            }
            else if (e.Column == gridColumnNewspaperPageSize)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPageSizeDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPageSizeDisplay;
            }
            else if (e.Column == gridColumnNewspaperColor)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperColorDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperColorDisplay;
            }
            else if (e.Column == gridColumnNewspaperCost)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditNewspaperTotalCostDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditNewspaperTotalCostDisplay;
            }
            else if (e.Column == gridColumnNewspaperCustomNote)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemButtonEditCustomCommentFirstRow;
                else
                    e.RepositoryItem = repositoryItemTextEditCustomComment;
            }
        }

        private void gridViewNewspaper_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnNewspaperPublication)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPublicationFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPublication;
            }
            else if (e.Column == gridColumnNewspaperSection)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperSectionFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperSection;
            }
            else if (e.Column == gridColumnNewspaperPageSize)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPageSizeFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperPageSize;
            }
            else if (e.Column == gridColumnNewspaperColor)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemComboBoxNewspaperColorFirstRow;
                else
                    e.RepositoryItem = repositoryItemComboBoxNewspaperColor;
            }
            else if (e.Column == gridColumnNewspaperCost)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditNewspaperTotalCostFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditNewspaperTotalCost;
            }
            else if (e.Column == gridColumnNewspaperCustomNote)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemButtonEditCustomCommentFirstRow;
                else
                    e.RepositoryItem = repositoryItemTextEditCustomComment;
            }
        }

        private void repositoryItemComboBoxNewspaperPublicationFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewNewspaper.CloseEditor();
                object value = gridViewNewspaper.GetRowCellValue(0, gridColumnNewspaperPublication);
                for (int i = 1; i < gridViewNewspaper.RowCount; i++)
                {
                    gridViewNewspaper.SetRowCellValue(i, gridColumnNewspaperPublication, value);
                }
            }
        }

        private void repositoryItemComboBoxNewspaperSectionFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewNewspaper.CloseEditor();
                object value = gridViewNewspaper.GetRowCellValue(0, gridColumnNewspaperSection);
                for (int i = 1; i < gridViewNewspaper.RowCount; i++)
                {
                    gridViewNewspaper.SetRowCellValue(i, gridColumnNewspaperSection, value);
                }
            }
        }

        private void repositoryItemComboBoxNewspaperPageSizeFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewNewspaper.CloseEditor();
                object value = gridViewNewspaper.GetRowCellValue(0, gridColumnNewspaperPageSize);
                for (int i = 1; i < gridViewNewspaper.RowCount; i++)
                {
                    gridViewNewspaper.SetRowCellValue(i, gridColumnNewspaperPageSize, value);
                }
            }
        }

        private void repositoryItemComboBoxNewspaperColorFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewNewspaper.CloseEditor();
                object value = gridViewNewspaper.GetRowCellValue(0, gridColumnNewspaperColor);
                for (int i = 1; i < gridViewNewspaper.RowCount; i++)
                {
                    gridViewNewspaper.SetRowCellValue(i, gridColumnNewspaperColor, value);
                }
            }
        }

        private void repositoryItemComboBoxNewspaperTotalCostFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                gridViewNewspaper.CloseEditor();
                object value = gridViewNewspaper.GetRowCellValue(0, gridColumnNewspaperCost);
                for (int i = 1; i < gridViewNewspaper.RowCount; i++)
                {
                    gridViewNewspaper.SetRowCellValue(i, gridColumnNewspaperCost, value);
                }
            }
        }
        #endregion
    }
}

