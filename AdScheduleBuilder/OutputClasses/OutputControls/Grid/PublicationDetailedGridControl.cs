using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    public partial class PublicationDetailedGridControl : DevExpress.XtraTab.XtraTabPage
    //public partial class PublicationDetailedGridControl : System.Windows.Forms.UserControl
    {
        private DevExpress.XtraGrid.Columns.GridColumn _activeCol = null;
        private bool _allowToSave = false;

        public BusinessClasses.Publication Publication { get; set; }

        public bool SettingsNotSaved
        {
            get
            {
                return OutputDetailedGridControl.Instance.SettingsNotSaved;
            }
            set
            {
                OutputDetailedGridControl.Instance.SettingsNotSaved = value;
            }
        }

        public PublicationDetailedGridControl()
        {
            InitializeComponent();
            comboBoxEditSchedule.Properties.Items.Clear();
            comboBoxEditSchedule.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders.ToArray());
            if (comboBoxEditSchedule.Properties.Items.Count > 0)
                comboBoxEditSchedule.SelectedIndex = 0;

            textEditHeader.Hide();
            textEditHeader.Parent = this.gridControlPublication;
        }

        public void LoadPublication()
        {
            this.Text = this.Publication.Name.Replace("&", "&&");
            pbLogo.Image = this.Publication.SmallLogo != null ? new Bitmap(this.Publication.SmallLogo) : null;
            laPublicationName.Text = this.Publication.Name.Replace("&", "&&");
            laDate.Text = this.Publication.Parent.PresentationDateObject != null ? this.Publication.Parent.PresentationDate.ToString("MM/dd/yy") : string.Empty;
            laBusinessName.Text = this.Publication.Parent.BusinessName + (!string.IsNullOrEmpty(this.Publication.Parent.AccountNumber) ? (" - " + this.Publication.Parent.AccountNumber) : string.Empty);
            laDecisionMaker.Text = this.Publication.Parent.DecisionMaker;
            laFlightDates.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            gridControlPublication.DataSource = new BindingList<BusinessClasses.Insert>(this.Publication.Inserts);

            _allowToSave = false;
            comboBoxEditSchedule.Properties.Items.Clear();
            comboBoxEditSchedule.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders.ToArray());
            if (string.IsNullOrEmpty(this.Publication.ViewSettings.DetailedGridSettings.SlideHeader))
            {
                if (comboBoxEditSchedule.Properties.Items.Count > 0)
                    comboBoxEditSchedule.SelectedIndex = 0;
            }
            else
            {
                int index = comboBoxEditSchedule.Properties.Items.IndexOf(this.Publication.ViewSettings.DetailedGridSettings.SlideHeader);
                if (index >= 0)
                    comboBoxEditSchedule.SelectedIndex = index;
                else
                    comboBoxEditSchedule.SelectedIndex = 0;
            }
            _allowToSave = true;
        }

        public void SetSlideHeader()
        {
            comboBoxEditSchedule.Enabled = OutputDetailedGridControl.Instance.SlideHeaderState.ShowSlideHeader;
            laBusinessName.Enabled = OutputDetailedGridControl.Instance.SlideHeaderState.ShowAdvertiser;
            laDate.Enabled = OutputDetailedGridControl.Instance.SlideHeaderState.ShowPresentationDate;
            laDecisionMaker.Enabled = OutputDetailedGridControl.Instance.SlideHeaderState.ShowDecisionMaker;
            laFlightDates.Enabled = OutputDetailedGridControl.Instance.SlideHeaderState.ShowFlightDates;
            laPublicationName.Enabled = OutputDetailedGridControl.Instance.SlideHeaderState.ShowName;
        }

        #region Editor's Events
        private void textEditHeader_Leave(object sender, EventArgs e)
        {
            _activeCol.Caption = textEditHeader.Text;

            if (_activeCol == gridColumnADRate)
                OutputDetailedGridControl.Instance.CaptionCost = _activeCol.Caption;
            else if (_activeCol == gridColumnColorPricing)
                OutputDetailedGridControl.Instance.CaptionColor = _activeCol.Caption;
            else if (_activeCol == gridColumnColumnInches)
                OutputDetailedGridControl.Instance.CaptionSquare = _activeCol.Caption;
            else if (_activeCol == gridColumnDate)
                OutputDetailedGridControl.Instance.CaptionDate = _activeCol.Caption;
            else if (_activeCol == gridColumnDeadline)
                OutputDetailedGridControl.Instance.CaptionDeadline = _activeCol.Caption;
            else if (_activeCol == gridColumnDelivery)
                OutputDetailedGridControl.Instance.CaptionDelivery = _activeCol.Caption;
            else if (_activeCol == gridColumnDimensions)
                OutputDetailedGridControl.Instance.CaptionDimensions = _activeCol.Caption;
            else if (_activeCol == gridColumnDiscountRate)
                OutputDetailedGridControl.Instance.CaptionDiscount = _activeCol.Caption;
            else if (_activeCol == gridColumnFinalRate)
                OutputDetailedGridControl.Instance.CaptionFinalCost = _activeCol.Caption;
            else if (_activeCol == gridColumnID)
                OutputDetailedGridControl.Instance.CaptionID = _activeCol.Caption;
            else if (_activeCol == gridColumnIndex)
                OutputDetailedGridControl.Instance.CaptionIndex = _activeCol.Caption;
            else if (_activeCol == gridColumnMechanicals)
                OutputDetailedGridControl.Instance.CaptionMechanicals = _activeCol.Caption;
            else if (_activeCol == gridColumnPageSize)
                OutputDetailedGridControl.Instance.CaptionPageSize = _activeCol.Caption;
            else if (_activeCol == gridColumnPercentOfPage)
                OutputDetailedGridControl.Instance.CaptionPercentOfPage = _activeCol.Caption;
            else if (_activeCol == gridColumnPCIRate)
                OutputDetailedGridControl.Instance.CaptionPCI = _activeCol.Caption;
            else if (_activeCol == gridColumnPublication)
                OutputDetailedGridControl.Instance.CaptionPublication = _activeCol.Caption;
            else if (_activeCol == gridColumnReadership)
                OutputDetailedGridControl.Instance.CaptionReadership = _activeCol.Caption;
            else if (_activeCol == gridColumnSection)
                OutputDetailedGridControl.Instance.CaptionSection = _activeCol.Caption;

            OutputDetailedGridControl.Instance.SaveView();

            textEditHeader.Hide();
        }

        private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Publication.ViewSettings.DetailedGridSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }


        private void checkEdit_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit cEdit = (DevExpress.XtraEditors.CheckEdit)sender;
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo cInfo = (DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo)cEdit.GetViewInfo();
            System.Drawing.Rectangle r = cInfo.CheckInfo.GlyphRect;
            System.Drawing.Rectangle editorRect = new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), cEdit.Size);
            if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
                ((DevExpress.Utils.DXMouseEventArgs)e).Handled = true;
        }
        #endregion

        #region Grid Events
        private void gridViewPublication_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo vi = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
                System.Drawing.Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
                bounds.Width -= 10;
                bounds.Height -= 3;
                bounds.Y += 3;
                textEditHeader.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                textEditHeader.EditValue = hi.Column.Caption;
                textEditHeader.Show();
                textEditHeader.Focus();
                _activeCol = hi.Column;
            }
            else
                AppManager.ShowWarning(" If you want to modify this Schedule Data,\ngo to the Schedules Tab and save changes...");
        }

        private void gridViewPublications_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn && e.Clicks == 2)
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo ViewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
                DevExpress.XtraGrid.Views.Grid.GridState prevState = view.State;
                if ((e.Button & System.Windows.Forms.MouseButtons.Left) != 0)
                {
                    if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new System.Drawing.Point(e.X, e.Y)))
                    {
                        ViewInfo.SelectionInfo.ClearPressedInfo();
                        args.Handled = true;
                    }
                }
            }
        }

        private void gridViewPublications_CalcPreviewText(object sender, DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs e)
        {
            SortedDictionary<int, string> previewText = new SortedDictionary<int, string>();
            int maxNumber = 12;
            if (OutputDetailedGridControl.Instance.ShowCommentsInPreview && !string.IsNullOrEmpty(e.PreviewText))
                previewText.Add(OutputDetailedGridControl.Instance.PositionCommentsInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionCommentsInPreview) ? OutputDetailedGridControl.Instance.PositionCommentsInPreview : ++maxNumber, e.PreviewText);
            if (OutputDetailedGridControl.Instance.ShowSectionInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnSection).ToString()))
                previewText.Add(OutputDetailedGridControl.Instance.PositionSectionInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionSectionInPreview) ? OutputDetailedGridControl.Instance.PositionSectionInPreview : ++maxNumber, "Section: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnSection).ToString());
            if (OutputDetailedGridControl.Instance.ShowMechanicalsInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnMechanicals)))
                previewText.Add(OutputDetailedGridControl.Instance.PositionMechanicalsInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionMechanicalsInPreview) ? OutputDetailedGridControl.Instance.PositionMechanicalsInPreview : ++maxNumber, "Mech: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnMechanicals).ToString());
            if (OutputDetailedGridControl.Instance.ShowDeliveryInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDelivery).ToString()))
                previewText.Add(OutputDetailedGridControl.Instance.PositionDeliveryInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionDeliveryInPreview) ? OutputDetailedGridControl.Instance.PositionDeliveryInPreview : ++maxNumber, "Delivery: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDelivery).ToString());
            if (OutputDetailedGridControl.Instance.ShowPublicationInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPublication).ToString()))
                previewText.Add(OutputDetailedGridControl.Instance.PositionPublicationInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionPublicationInPreview) ? OutputDetailedGridControl.Instance.PositionPublicationInPreview : ++maxNumber, "Publication: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPublication).ToString());
            if (OutputDetailedGridControl.Instance.ShowPageSizeInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPageSize)))
                previewText.Add(OutputDetailedGridControl.Instance.PositionPageSizeInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionPageSizeInPreview) ? OutputDetailedGridControl.Instance.PositionPageSizeInPreview : ++maxNumber, "Page Size: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPageSize).ToString());
            if (OutputDetailedGridControl.Instance.ShowPercentOfPageInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPercentOfPage)))
                previewText.Add(OutputDetailedGridControl.Instance.PositionPercentOfPageInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionPercentOfPageInPreview) ? OutputDetailedGridControl.Instance.PositionPercentOfPageInPreview : ++maxNumber, "% of Page: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPercentOfPage).ToString());
            if (OutputDetailedGridControl.Instance.ShowDimensionsInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDimensions).ToString()))
                previewText.Add(OutputDetailedGridControl.Instance.PositionDimensionsInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionDimensionsInPreview) ? OutputDetailedGridControl.Instance.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDimensions).ToString());
            if (OutputDetailedGridControl.Instance.ShowColumnInchesInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnColumnInches).ToString()))
                previewText.Add(OutputDetailedGridControl.Instance.PositionColumnInchesInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionColumnInchesInPreview) ? OutputDetailedGridControl.Instance.PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnColumnInches).ToString() + " col. in.");
            if (OutputDetailedGridControl.Instance.ShowReadershipInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnReadership).ToString()))
                previewText.Add(OutputDetailedGridControl.Instance.PositionReadershipInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionReadershipInPreview) ? OutputDetailedGridControl.Instance.PositionReadershipInPreview : ++maxNumber, "Readership: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnReadership).ToString());
            if (OutputDetailedGridControl.Instance.ShowDeadlineInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDeadline).ToString()))
                previewText.Add(OutputDetailedGridControl.Instance.PositionDeadlineInPreview > 0 && !previewText.Keys.Contains(OutputDetailedGridControl.Instance.PositionDeadlineInPreview) ? OutputDetailedGridControl.Instance.PositionDeadlineInPreview : ++maxNumber, "Deadline: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDeadline).ToString());
            e.PreviewText = string.Join(",   ", previewText.Values.ToArray());
            if (string.IsNullOrEmpty(e.PreviewText))
                e.PreviewText = "            ";
        }

        private void gridViewPublications_MouseMove(object sender, MouseEventArgs e)
        {
            gridViewPublications.Focus();
        }

        private void gridViewPublications_ColumnPositionChanged(object sender, EventArgs e)
        {
            if (OutputDetailedGridControl.Instance.AllowToSave)
            {
                OutputDetailedGridControl.Instance.PositionCost = gridColumnADRate.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionColor = gridColumnColorPricing.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionSquare = gridColumnColumnInches.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionDate = gridColumnDate.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionDeadline = gridColumnDeadline.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionDelivery = gridColumnDelivery.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionDimensions = gridColumnDimensions.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionDiscount = gridColumnDiscountRate.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionFinalCost = gridColumnFinalRate.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionID = gridColumnID.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionIndex = gridColumnIndex.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionMechanicals = gridColumnMechanicals.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionPageSize = gridColumnPageSize.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionPercentOfPage = gridColumnPercentOfPage.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionPCI = gridColumnPCIRate.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionPublication = gridColumnPublication.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionReadership = gridColumnReadership.VisibleIndex;
                OutputDetailedGridControl.Instance.PositionSection = gridColumnSection.VisibleIndex;
                OutputDetailedGridControl.Instance.SaveView();
            }
        }

        private void gridViewPublications_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            if (OutputDetailedGridControl.Instance.AllowToSave)
            {
                OutputDetailedGridControl.Instance.WidthCost = gridColumnADRate.Width;
                OutputDetailedGridControl.Instance.WidthColor = gridColumnColorPricing.Width;
                OutputDetailedGridControl.Instance.WidthSquare = gridColumnColumnInches.Width;
                OutputDetailedGridControl.Instance.WidthDate = gridColumnDate.Width;
                OutputDetailedGridControl.Instance.WidthDeadline = gridColumnDeadline.Width;
                OutputDetailedGridControl.Instance.WidthDelivery = gridColumnDelivery.Width;
                OutputDetailedGridControl.Instance.WidthDimensions = gridColumnDimensions.Width;
                OutputDetailedGridControl.Instance.WidthDiscount = gridColumnDiscountRate.Width;
                OutputDetailedGridControl.Instance.WidthFinalCost = gridColumnFinalRate.Width;
                OutputDetailedGridControl.Instance.WidthID = gridColumnID.Width;
                OutputDetailedGridControl.Instance.WidthIndex = gridColumnIndex.Width;
                OutputDetailedGridControl.Instance.WidthMechanicals = gridColumnMechanicals.Width;
                OutputDetailedGridControl.Instance.WidthPageSize = gridColumnPageSize.Width;
                OutputDetailedGridControl.Instance.WidthPercentOfPage = gridColumnPercentOfPage.Width;
                OutputDetailedGridControl.Instance.WidthPCI = gridColumnPCIRate.Width;
                OutputDetailedGridControl.Instance.WidthPublication = gridColumnPublication.Width;
                OutputDetailedGridControl.Instance.WidthReadership = gridColumnReadership.Width;
                OutputDetailedGridControl.Instance.WidthSection = gridColumnSection.Width;
                OutputDetailedGridControl.Instance.SaveView();
            }
        }
        #endregion

        #region Output Staff
        public void PrepareOutput(bool excelOutput)
        {
            this.Grid = GetGrid(excelOutput);
            if (!excelOutput)
                PopulateWeeklyScheduleReplacementsList();
        }

        public void PrintOutput(bool excelOutput, bool pasteAsImage)
        {
            PrepareOutput(excelOutput);
            if (excelOutput)
                InteropClasses.PowerPointHelper.Instance.AppendDetailedGridExcelBased(this, pasteAsImage);
            else
                InteropClasses.PowerPointHelper.Instance.AppendDetailedGridGridBased(this);
        }

        public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

        public void PopulateWeeklyScheduleReplacementsList()
        {
            string key = string.Empty;
            string value = string.Empty;
            List<string> temp = new List<string>();
            this.OutputReplacementsLists = new List<Dictionary<string, string>>();
            foreach (string[][] slideGrid in this.Grid)
            {
                Dictionary<string, string> slideReplacementList = new Dictionary<string, string>();

                string[] gridHeaders = this.GridHeaders;
                for (int i = 0; i < gridHeaders.Length; i++)
                {
                    key = string.Format("Header{0}", (i + 1).ToString());
                    value = gridHeaders[i];
                    if (!slideReplacementList.Keys.Contains(key))
                        slideReplacementList.Add(key, value);
                }

                for (int i = 0; i < slideGrid.Length; i++)
                {
                    for (int j = 0; j < slideGrid[i].Length; j++)
                    {
                        string columnPrefix = "a";
                        if (OutputDetailedGridControl.Instance.ShowCommentsHeader)
                        {
                            switch (j)
                            {
                                case 0:
                                    columnPrefix = "g";
                                    break;
                                case 1:
                                    columnPrefix = "a";
                                    break;
                                case 2:
                                    columnPrefix = "b";
                                    break;
                                case 3:
                                    columnPrefix = "c";
                                    break;
                                case 4:
                                    columnPrefix = "d";
                                    break;
                                case 5:
                                    columnPrefix = "e";
                                    break;
                                case 6:
                                    columnPrefix = "f";
                                    break;
                            }
                        }
                        else
                        {
                            switch (j)
                            {
                                case 0:
                                    columnPrefix = "a";
                                    break;
                                case 1:
                                    columnPrefix = "b";
                                    break;
                                case 2:
                                    columnPrefix = "c";
                                    break;
                                case 3:
                                    columnPrefix = "d";
                                    break;
                                case 4:
                                    columnPrefix = "e";
                                    break;
                                case 5:
                                    columnPrefix = "f";
                                    break;
                            }
                        }
                        key = string.Format("{0}{1}", new object[] { (i + 1).ToString(), columnPrefix });
                        value = slideGrid[i][j];
                        if (!slideReplacementList.Keys.Contains(key))
                            slideReplacementList.Add(key, value);
                    }
                }
                this.OutputReplacementsLists.Add(slideReplacementList);
            }
        }

        public int OutputFileIndex
        {
            get
            {
                if (InteropClasses.PowerPointHelper.Instance.Is2003)
                {
                    return OutputDetailedGridControl.Instance.ShowCommentsHeader ? 1 : 2;
                }
                else
                {
                    return OutputDetailedGridControl.Instance.ShowCommentsHeader ? 3 : 4;
                }
            }
        }

        public string Header
        {
            get
            {
                string result = string.Empty;
                if (comboBoxEditSchedule.EditValue != null && OutputDetailedGridControl.Instance.SlideHeaderState.ShowSlideHeader)
                    result = comboBoxEditSchedule.EditValue.ToString();
                return result;
            }
        }

        public string LogoFile
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowLogo1)
                {
                    result = System.IO.Path.GetTempFileName();
                    pbLogo.Image.Save(result);
                }
                return result;
            }
        }

        public string PresentationName1
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowName && OutputDetailedGridControl.Instance.SlideHeaderState.ShowLogo1)
                    result = this.Text;
                return result.Replace("&&", "&");
            }
        }

        public string PresentationDate1
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowPresentationDate && OutputDetailedGridControl.Instance.SlideHeaderState.ShowLogo1)
                    result = laDate.Text;
                return result;
            }
        }

        public string PresentationName2
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowName && !OutputDetailedGridControl.Instance.SlideHeaderState.ShowLogo1)
                    result = this.Text;
                return result.Replace("&&", "&");
            }
        }

        public string PresentationDate2
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowPresentationDate && !OutputDetailedGridControl.Instance.SlideHeaderState.ShowLogo1)
                    result = laDate.Text;
                return result;
            }
        }

        public string BusinessName
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowAdvertiser)
                    result = laBusinessName.Text;
                return result;
            }
        }

        public string DecisionMaker
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowDecisionMaker)
                    result = laDecisionMaker.Text;
                return result;
            }
        }

        public string FlightDates
        {
            get
            {
                string result = string.Empty;
                if (OutputDetailedGridControl.Instance.SlideHeaderState.ShowFlightDates)
                    result = laFlightDates.Text;
                return result;
            }
        }

        public bool ShowSignatureLine
        {
            get
            {
                return OutputDetailedGridControl.Instance.SlideBulletsState.ShowSignature;
            }
        }

        public bool ShowAdSpecsOnlyOnLastSlide
        {
            get
            {
                bool result = true;
                result = OutputDetailedGridControl.Instance.SlideBulletsState.ShowOnlyOnLastSlide;
                return result;
            }
        }

        public bool DoNotShowAdSpecs
        {
            get
            {
                return !OutputDetailedGridControl.Instance.SlideBulletsState.ShowSlideBullets;
            }
        }

        public string[] AdSpecs
        {
            get
            {
                List<string> values = new List<string>();
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowTotalInserts)
                    values.Add("Total Ads: " + this.Publication.TotalInserts.ToString("#,##0"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowPageSize)
                    values.Add("Page Size: " + this.Publication.SizeOptions.PageSize);
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowPercentOfPage)
                    values.Add("% of Page: " + this.Publication.SizeOptions.PercentOfPage);
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowDimensions)
                    values.Add("Col. x Inches: " + this.Publication.SizeOptions.Dimensions);
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowColumnInches && this.Publication.SizeOptions.Square.HasValue)
                    values.Add("Total Col. In.: " + this.Publication.SizeOptions.Square.Value.ToString("#,###.00#"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowAvgAdCost)
                    values.Add("BW Ad Cost: " + this.Publication.AvgADRate.ToString("$#,###.00"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowAvgFinalCost)
                    values.Add("Final Ad Cost: " + this.Publication.AvgFinalRate.ToString("$#,###.00"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowAvgPCI)
                    values.Add("Avg PCI: " + this.Publication.AvgPCIRate.ToString("$#,###.00"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowDelivery && this.Publication.DailyDelivery != null)
                    values.Add("Delivery: " + this.Publication.DailyDelivery.Value.ToString("#,##0"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowReadership && this.Publication.DailyReadership != null)
                    values.Add("Readership: " + this.Publication.DailyReadership.Value.ToString("#,##0"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowTotalColor)
                    values.Add("Total Color: " + this.Publication.TotalColorPricingCalculated.ToString("$#,###.00"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowDiscounts)
                    values.Add("Discounts: " + this.Publication.TotalDiscountRate.ToString("$#,###.00"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowTotalFinalCost)
                    values.Add("Investment: " + this.Publication.TotalFinalRate.ToString("$#,###.00"));
                if (OutputDetailedGridControl.Instance.SlideBulletsState.ShowTotalSquare && this.Publication.TotalSquare.HasValue)
                    values.Add("Total Inches: " + this.Publication.TotalSquare.Value.ToString("#,###.00#"));
                return values.ToArray();
            }
        }

        public string[] GridHeaders
        {
            get
            {
                SortedDictionary<int, string> headers = new SortedDictionary<int, string>();
                if (OutputDetailedGridControl.Instance.PositionID != -1 && OutputDetailedGridControl.Instance.ShowIDHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionID, OutputDetailedGridControl.Instance.CaptionID);
                if (OutputDetailedGridControl.Instance.PositionDate != -1 && OutputDetailedGridControl.Instance.ShowDateHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionDate, OutputDetailedGridControl.Instance.CaptionDate);
                if (OutputDetailedGridControl.Instance.PositionPCI != -1 && OutputDetailedGridControl.Instance.ShowPCIHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionPCI, OutputDetailedGridControl.Instance.CaptionPCI);
                if (OutputDetailedGridControl.Instance.PositionCost != -1 && OutputDetailedGridControl.Instance.ShowCostHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionCost, OutputDetailedGridControl.Instance.CaptionCost.Replace("&&", "&"));
                if (OutputDetailedGridControl.Instance.PositionDiscount != -1 && OutputDetailedGridControl.Instance.ShowDiscountHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionDiscount, OutputDetailedGridControl.Instance.CaptionDiscount);
                if (OutputDetailedGridControl.Instance.PositionColor != -1 && OutputDetailedGridControl.Instance.ShowColorHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionColor, OutputDetailedGridControl.Instance.CaptionColor);
                if (OutputDetailedGridControl.Instance.PositionFinalCost != -1 && OutputDetailedGridControl.Instance.ShowFinalCostHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionFinalCost, OutputDetailedGridControl.Instance.CaptionFinalCost);
                if (OutputDetailedGridControl.Instance.PositionIndex != -1 && OutputDetailedGridControl.Instance.ShowIndexHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionIndex, OutputDetailedGridControl.Instance.CaptionIndex);
                if (OutputDetailedGridControl.Instance.PositionSquare != -1 && OutputDetailedGridControl.Instance.ShowSquareHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionSquare, OutputDetailedGridControl.Instance.CaptionSquare);
                if (OutputDetailedGridControl.Instance.PositionPageSize != -1 && OutputDetailedGridControl.Instance.ShowPageSizeHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionPageSize, OutputDetailedGridControl.Instance.CaptionPageSize);
                if (OutputDetailedGridControl.Instance.PositionPercentOfPage != -1 && OutputDetailedGridControl.Instance.ShowPercentOfPageHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionPercentOfPage, OutputDetailedGridControl.Instance.CaptionPercentOfPage);
                if (OutputDetailedGridControl.Instance.PositionMechanicals != -1 && OutputDetailedGridControl.Instance.ShowMechanicalsHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionMechanicals, OutputDetailedGridControl.Instance.CaptionMechanicals);
                if (OutputDetailedGridControl.Instance.PositionPublication != -1 && OutputDetailedGridControl.Instance.ShowPublicationHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionPublication, OutputDetailedGridControl.Instance.CaptionPublication);
                if (OutputDetailedGridControl.Instance.PositionDimensions != -1 && OutputDetailedGridControl.Instance.ShowDimensionsHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionDimensions, OutputDetailedGridControl.Instance.CaptionDimensions);
                if (OutputDetailedGridControl.Instance.PositionSection != -1 && OutputDetailedGridControl.Instance.ShowSectionHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionSection, OutputDetailedGridControl.Instance.CaptionSection);
                if (OutputDetailedGridControl.Instance.PositionReadership != -1 && OutputDetailedGridControl.Instance.ShowReadershipHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionReadership, OutputDetailedGridControl.Instance.CaptionReadership);
                if (OutputDetailedGridControl.Instance.PositionDelivery != -1 && OutputDetailedGridControl.Instance.ShowDeliveryHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionDelivery, OutputDetailedGridControl.Instance.CaptionDelivery);
                if (OutputDetailedGridControl.Instance.PositionDeadline != -1 && OutputDetailedGridControl.Instance.ShowDeadlineHeader)
                    headers.Add(OutputDetailedGridControl.Instance.PositionDeadline, OutputDetailedGridControl.Instance.CaptionDeadline);
                return headers.Values.ToArray();
            }
        }

        public int[] GridHeaderSizes
        {
            get
            {
                SortedDictionary<int, int> sizes = new SortedDictionary<int, int>();
                if (OutputDetailedGridControl.Instance.PositionID != -1 && OutputDetailedGridControl.Instance.ShowIDHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionID, OutputDetailedGridControl.Instance.WidthID);
                if (OutputDetailedGridControl.Instance.PositionDate != -1 && OutputDetailedGridControl.Instance.ShowDateHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionDate, OutputDetailedGridControl.Instance.WidthDate);
                if (OutputDetailedGridControl.Instance.PositionPCI != -1 && OutputDetailedGridControl.Instance.ShowPCIHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionPCI, OutputDetailedGridControl.Instance.WidthPCI);
                if (OutputDetailedGridControl.Instance.PositionCost != -1 && OutputDetailedGridControl.Instance.ShowCostHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionCost, OutputDetailedGridControl.Instance.WidthCost);
                if (OutputDetailedGridControl.Instance.PositionDiscount != -1 && OutputDetailedGridControl.Instance.ShowDiscountHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionDiscount, OutputDetailedGridControl.Instance.WidthDiscount);
                if (OutputDetailedGridControl.Instance.PositionColor != -1 && OutputDetailedGridControl.Instance.ShowColorHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionColor, OutputDetailedGridControl.Instance.WidthColor);
                if (OutputDetailedGridControl.Instance.PositionFinalCost != -1 && OutputDetailedGridControl.Instance.ShowFinalCostHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionFinalCost, OutputDetailedGridControl.Instance.WidthFinalCost);
                if (OutputDetailedGridControl.Instance.PositionIndex != -1 && OutputDetailedGridControl.Instance.ShowIndexHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionIndex, OutputDetailedGridControl.Instance.WidthIndex);
                if (OutputDetailedGridControl.Instance.PositionSquare != -1 && OutputDetailedGridControl.Instance.ShowSquareHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionSquare, OutputDetailedGridControl.Instance.WidthSquare);
                if (OutputDetailedGridControl.Instance.PositionPageSize != -1 && OutputDetailedGridControl.Instance.ShowPageSizeHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionPageSize, OutputDetailedGridControl.Instance.WidthPageSize);
                if (OutputDetailedGridControl.Instance.PositionPercentOfPage != -1 && OutputDetailedGridControl.Instance.ShowPercentOfPageHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionPercentOfPage, OutputDetailedGridControl.Instance.WidthPercentOfPage);
                if (OutputDetailedGridControl.Instance.PositionMechanicals != -1 && OutputDetailedGridControl.Instance.ShowMechanicalsHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionMechanicals, OutputDetailedGridControl.Instance.WidthMechanicals);
                if (OutputDetailedGridControl.Instance.PositionPublication != -1 && OutputDetailedGridControl.Instance.ShowPublicationHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionPublication, OutputDetailedGridControl.Instance.WidthPublication);
                if (OutputDetailedGridControl.Instance.PositionDimensions != -1 && OutputDetailedGridControl.Instance.ShowDimensionsHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionDimensions, OutputDetailedGridControl.Instance.WidthDimensions);
                if (OutputDetailedGridControl.Instance.PositionSection != -1 && OutputDetailedGridControl.Instance.ShowSectionHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionSection, OutputDetailedGridControl.Instance.WidthSection);
                if (OutputDetailedGridControl.Instance.PositionReadership != -1 && OutputDetailedGridControl.Instance.ShowReadershipHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionReadership, OutputDetailedGridControl.Instance.WidthReadership);
                if (OutputDetailedGridControl.Instance.PositionDelivery != -1 && OutputDetailedGridControl.Instance.ShowDeliveryHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionDelivery, OutputDetailedGridControl.Instance.WidthDelivery);
                if (OutputDetailedGridControl.Instance.PositionDeadline != -1 && OutputDetailedGridControl.Instance.ShowDeadlineHeader)
                    sizes.Add(OutputDetailedGridControl.Instance.PositionDeadline, OutputDetailedGridControl.Instance.WidthDeadline);
                return sizes.Values.ToArray();
            }
        }

        public string[][][] Grid { get; private set; }

        private string[][][] GetGrid(bool excelOutput)
        {
            List<string[][]> result = new List<string[][]>();
            List<string[]> slide = new List<string[]>();
            SortedDictionary<int, string> row = new SortedDictionary<int, string>();
            SortedDictionary<int, string> adNotes = new SortedDictionary<int, string>();
            BusinessClasses.Publication publication = this.Publication;
            int slidesCount = OutputDetailedGridControl.Instance.ShowCommentsHeader ? (excelOutput ? BusinessClasses.OutputManager.DetailedGridExcelBasedRowsCountWithNotes : BusinessClasses.OutputManager.DetailedGridGridBasedRowsCountWithNotes) : (excelOutput ? BusinessClasses.OutputManager.DetailedGridExcelBasedRowsCountWithoutNotes : BusinessClasses.OutputManager.DetailedGridGridBasedRowsCountWithoutNotes);
            int insertsCount = publication.Inserts.Count;
            for (int i = 0; i < insertsCount; i += slidesCount)
            {
                slide.Clear();
                for (int j = 0; j < slidesCount; j++)
                {
                    row.Clear();
                    if ((i + j) < insertsCount)
                    {
                        if (OutputDetailedGridControl.Instance.ShowCommentsHeader)
                        {
                            adNotes.Clear();
                            int maxNumber = 12;
                            if (OutputDetailedGridControl.Instance.ShowCommentsInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].FullComment))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionCommentsInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionCommentsInPreview) ? OutputDetailedGridControl.Instance.PositionCommentsInPreview : ++maxNumber, publication.Inserts[i + j].FullComment);
                            if (OutputDetailedGridControl.Instance.ShowSectionInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].FullSection))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionSectionInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionSectionInPreview) ? OutputDetailedGridControl.Instance.PositionSectionInPreview : ++maxNumber, "Section: " + publication.Inserts[i + j].FullSection);
                            if (OutputDetailedGridControl.Instance.ShowMechanicalsInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].Mechanicals))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionMechanicalsInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionMechanicalsInPreview) ? OutputDetailedGridControl.Instance.PositionMechanicalsInPreview : ++maxNumber, "Mech: " + publication.Inserts[i + j].MechanicalsOutput);
                            if (OutputDetailedGridControl.Instance.ShowDeliveryInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].Delivery))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionDeliveryInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionDeliveryInPreview) ? OutputDetailedGridControl.Instance.PositionDeliveryInPreview : ++maxNumber, "Delivery: " + publication.Inserts[i + j].Delivery);
                            if (OutputDetailedGridControl.Instance.ShowPublicationInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].Publication))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionPublicationInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionPublicationInPreview) ? OutputDetailedGridControl.Instance.PositionPublicationInPreview : ++maxNumber, "Publication: " + publication.Inserts[i + j].Publication);
                            if (OutputDetailedGridControl.Instance.ShowPageSizeInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].PageSize))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionPageSizeInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionPageSizeInPreview) ? OutputDetailedGridControl.Instance.PositionPageSizeInPreview : ++maxNumber, "Page Size: " + publication.Inserts[i + j].PageSizeOutput);
                            if (OutputDetailedGridControl.Instance.ShowPercentOfPageInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].PercentOfPage))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionPercentOfPageInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionPercentOfPageInPreview) ? OutputDetailedGridControl.Instance.PositionPercentOfPageInPreview : ++maxNumber, "% of Page: " + publication.Inserts[i + j].PercentOfPageOutput);
                            if (OutputDetailedGridControl.Instance.ShowDimensionsInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].Dimensions))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionDimensionsInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionDimensionsInPreview) ? OutputDetailedGridControl.Instance.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + publication.Inserts[i + j].Dimensions);
                            if (OutputDetailedGridControl.Instance.ShowColumnInchesInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].SquareStringFormatted))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionColumnInchesInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionColumnInchesInPreview) ? OutputDetailedGridControl.Instance.PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + publication.Inserts[i + j].SquareStringFormatted + " col. in.");
                            if (OutputDetailedGridControl.Instance.ShowReadershipInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].Readership))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionReadershipInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionReadershipInPreview) ? OutputDetailedGridControl.Instance.PositionReadershipInPreview : ++maxNumber, "Readership: " + publication.Inserts[i + j].Readership);
                            if (OutputDetailedGridControl.Instance.ShowDeadlineInPreview && !string.IsNullOrEmpty(publication.Inserts[i + j].Deadline))
                                adNotes.Add(OutputDetailedGridControl.Instance.PositionDeadlineInPreview > 0 && !adNotes.Keys.Contains(OutputDetailedGridControl.Instance.PositionDeadlineInPreview) ? OutputDetailedGridControl.Instance.PositionDeadlineInPreview : ++maxNumber, "Deadline: " + publication.Inserts[i + j].DeadlineForOutput);
                            if (adNotes.Count > 0)
                                row.Add(-1, string.Join(",   ", adNotes.Values.ToArray()));
                            else
                                row.Add(-1, "            ");
                        }
                        if (OutputDetailedGridControl.Instance.PositionID != -1 && OutputDetailedGridControl.Instance.ShowIDHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionID, publication.Inserts[i + j].ID);
                        if (OutputDetailedGridControl.Instance.PositionDate != -1 && OutputDetailedGridControl.Instance.ShowDateHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionDate, publication.Inserts[i + j].Date.ToString("MM/dd/yy"));
                        if (OutputDetailedGridControl.Instance.PositionPCI != -1 && OutputDetailedGridControl.Instance.ShowPCIHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionPCI, publication.Inserts[i + j].PCIRate.HasValue ? (publication.Inserts[i + j].PCIRate.Value.ToString("$#,###.00")) : "N/A");
                        if (OutputDetailedGridControl.Instance.PositionCost != -1 && OutputDetailedGridControl.Instance.ShowCostHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionCost, publication.Inserts[i + j].ADRate.ToString("$#,###.00"));
                        if (OutputDetailedGridControl.Instance.PositionDiscount != -1 && OutputDetailedGridControl.Instance.ShowDiscountHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionDiscount, publication.Inserts[i + j].DiscountRate.ToString("$#,###.00"));
                        if (OutputDetailedGridControl.Instance.PositionColor != -1 && OutputDetailedGridControl.Instance.ShowColorHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionColor, publication.ColorOption != BusinessClasses.ColorOptions.BlackWhite && publication.ColorPricing != BusinessClasses.ColorPricingType.ColorIncluded ? publication.Inserts[i + j].ColorPricingCalculated.ToString("$#,###.00") : publication.Inserts[i + j].ColorPricingObject.ToString());
                        if (OutputDetailedGridControl.Instance.PositionFinalCost != -1 && OutputDetailedGridControl.Instance.ShowFinalCostHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionFinalCost, publication.Inserts[i + j].FinalRate.ToString("$#,###.00"));
                        if (OutputDetailedGridControl.Instance.PositionIndex != -1 && OutputDetailedGridControl.Instance.ShowIndexHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionIndex, publication.Inserts[i + j].Index.ToString("#,##0"));
                        if (OutputDetailedGridControl.Instance.PositionSquare != -1 && OutputDetailedGridControl.Instance.ShowSquareHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionSquare, "'" + publication.Inserts[i + j].SquareStringFormatted);
                        if (OutputDetailedGridControl.Instance.PositionPageSize != -1 && OutputDetailedGridControl.Instance.ShowPageSizeHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionPageSize, publication.Inserts[i + j].PageSizeOutput);
                        if (OutputDetailedGridControl.Instance.PositionPercentOfPage != -1 && OutputDetailedGridControl.Instance.ShowPercentOfPageHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionPercentOfPage, publication.Inserts[i + j].PercentOfPageOutput);
                        if (OutputDetailedGridControl.Instance.PositionMechanicals != -1 && OutputDetailedGridControl.Instance.ShowMechanicalsHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionMechanicals, publication.Inserts[i + j].MechanicalsOutput);
                        if (OutputDetailedGridControl.Instance.PositionPublication != -1 && OutputDetailedGridControl.Instance.ShowPublicationHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionPublication, publication.Name);
                        if (OutputDetailedGridControl.Instance.PositionDimensions != -1 && OutputDetailedGridControl.Instance.ShowDimensionsHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionDimensions, publication.Inserts[i + j].DimensionsOutput);
                        if (OutputDetailedGridControl.Instance.PositionSection != -1 && OutputDetailedGridControl.Instance.ShowSectionHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionSection, publication.Inserts[i + j].FullSection);
                        if (OutputDetailedGridControl.Instance.PositionReadership != -1 && OutputDetailedGridControl.Instance.ShowReadershipHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionReadership, publication.Inserts[i + j].Readership);
                        if (OutputDetailedGridControl.Instance.PositionDelivery != -1 && OutputDetailedGridControl.Instance.ShowDeliveryHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionDelivery, publication.Inserts[i + j].Delivery);
                        if (OutputDetailedGridControl.Instance.PositionDeadline != -1 && OutputDetailedGridControl.Instance.ShowDeadlineHeader)
                            row.Add(OutputDetailedGridControl.Instance.PositionDeadline, publication.Inserts[i + j].DeadlineForOutput);
                    }
                    if (row.Values.Count > 0)
                        slide.Add(row.Values.ToArray());
                }
                result.Add(slide.ToArray());
            }
            return result.ToArray();
        }
        #endregion
    }
}
