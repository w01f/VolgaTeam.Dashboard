using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;

namespace AdScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    //public partial class PublicationControl : UserControl
    public partial class PublicationControl : DevExpress.XtraTab.XtraTabPage
    {
        public BusinessClasses.Publication Publication { get; set; }
        public OutputControls.PublicationBasicOverviewControl BasicOverviewOutput { get; set; }
        public OutputControls.PublicationMultiSummaryControl MultiSummaryOutput { get; set; }
        public OutputControls.PublicationDetailedGridControl DetailedGridOutput { get; set; }
        public OutputControls.PublicationMultiGridControl MultiGridOutput { get; set; }


        public PublicationControl(BusinessClasses.Publication publication)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Publication = publication;
            this.Text = publication.Name.Replace("&", "&&");

            repositoryItemDateNull.NullDate = this.Publication.Parent.FlightDateStart;

            repositoryItemSpinEditPCIRate.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditPCIRate.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditPCIRate.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEditNull.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEditNull.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEditNull.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEditNull.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEditNull.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEditNull.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditColorPricingEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditColorPricingEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditColorPricingEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditColorPricingEditFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditColorPricingEditFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditColorPricingEditFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditDiscountsEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditDiscountsEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditDiscountsEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditDiscountsEditFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditDiscountsEditFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditDiscountsEditFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);

            this.BasicOverviewOutput = new OutputControls.PublicationBasicOverviewControl();
            this.MultiSummaryOutput = new OutputControls.PublicationMultiSummaryControl();
            this.DetailedGridOutput = new OutputControls.PublicationDetailedGridControl();
            this.MultiGridOutput = new OutputControls.PublicationMultiGridControl();

            LoadInserts();
        }

        private void LoadInserts()
        {
            gridControlPublication.DataSource = new BindingList<BusinessClasses.Insert>(this.Publication.Inserts);
            UpdateTotals();
        }

        public void RefreshInserts()
        {
            int currentRowHandle = advBandedGridViewPublication.FocusedRowHandle;
            ((BindingList<BusinessClasses.Insert>)gridControlPublication.DataSource).ResetBindings();
            advBandedGridViewPublication.FocusedRowHandle = currentRowHandle;
            UpdateTotals();
        }

        public void AddInsert()
        {
            this.Publication.AddInsert();
            ((BindingList<BusinessClasses.Insert>)gridControlPublication.DataSource).ResetBindings();
            advBandedGridViewPublication.FocusedRowHandle = advBandedGridViewPublication.RowCount - 1;
            UpdateTotals();
        }

        public void DeleteInsert()
        {
            if (AppManager.ShowWarningQuestion("Are you sure you want to delete selected line?") == DialogResult.Yes)
            {
                advBandedGridViewPublication.DeleteSelectedRows();
                this.Publication.RebuildInserts();
                ScheduleBuilderControl.Instance.SettingsNotSaved = true;
                UpdateTotals();
            }
        }

        public void CloneInsert()
        {
            using (ToolForms.FormCloneInsert form = new ToolForms.FormCloneInsert())
            {
                BusinessClasses.Insert originalInsert = this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(advBandedGridViewPublication.FocusedRowHandle)];
                if (originalInsert.DateObject != null)
                {

                    DateTime[] cloneDates = this.Publication.GetCloneDates(originalInsert.Date);

                    form.laOriginalDate.Text = originalInsert.Date.ToString(@"ddd, MM/dd/yy");
                    form.laDescription.Text = string.Format(form.laDescription.Text, new object[] { cloneDates.Length.ToString(), originalInsert.Date.ToString("dddd"), cloneDates.Length == 1 ? "" : "s" });
                    form.checkEditPCIRate.Text = this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.StandartPCI ? gridBandPCIRate.Caption : gridBandADRate.Caption;
                    if (this.Publication.ColorOption == BusinessClasses.ColorOptions.BlackWhite)
                        form.checkEditColorRate.Visible = false;
                    foreach (DateTime cloneDate in cloneDates)
                        form.checkedListBoxControlCloneDates.Items.Add(cloneDate.ToString(@"MM/dd/yy    "));
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        List<DateTime> selectedDates = new List<DateTime>();
                        foreach (int index in form.checkedListBoxControlCloneDates.CheckedIndices)
                            selectedDates.Add(cloneDates[index]);
                        this.Publication.CloneInsert(originalInsert, selectedDates.ToArray(), form.checkEditPCIRate.Checked, form.checkEditDiscount.Checked, this.Publication.ColorOption == BusinessClasses.ColorOptions.BlackWhite ? false : form.checkEditColorRate.Checked, form.checkEditComment.Checked, form.checkEditSections.Checked);
                        ((BindingList<BusinessClasses.Insert>)gridControlPublication.DataSource).ResetBindings();
                        ScheduleBuilderControl.Instance.SettingsNotSaved = true;
                        UpdateTotals();
                    }
                }
                else
                    AppManager.ShowWarning("You need to select Date first.");
            }
        }

        public void SortInserts()
        {
            this.Publication.SortInserts();
            ((BindingList<BusinessClasses.Insert>)gridControlPublication.DataSource).ResetBindings();
        }

        public void UpdateTotals()
        {
            laTotalInsertsValue.Text = this.Publication.TotalInserts.ToString("#,##0");
            laTotalSquareValue.Text = this.Publication.TotalSquare.ToString("#,##0.00");
            laAvgADRateValue.Text = this.Publication.AvgADRate.ToString("$#,##0.00");
            laAvgPCIRateValue.Text = this.Publication.AvgPCIRate.ToString("$#,##0.00");
            laTotalDiscountRateValue.Text = this.Publication.TotalDiscountRate.ToString("$#,##0.00");
            laTotalColorPricingCalculatedValue.Text = this.Publication.TotalColorPricingCalculated.ToString("$#,##0.00");
            laAvgFinalRateValue.Text = this.Publication.AvgFinalRate.ToString("$#,##0.00");
            laTotalFinalRateValue.Text = this.Publication.TotalFinalRate.ToString("$#,##0.00");

            UpdateBasicOverviewOutput();
            UpdateMultiSummaryOutput();
            UpdateDetailedGridOutput();
            UpdateMultiGridOutput();
        }

        private void UpdateBasicOverviewOutput()
        {
            this.BasicOverviewOutput.Text = this.Publication.Name.Replace("&", "&&");
            this.BasicOverviewOutput.pbLogo.Image = this.Publication.BigLogo != null ? new Bitmap(this.Publication.BigLogo) : null;
            this.BasicOverviewOutput.checkEditName.Text = this.Publication.Name.Replace("&", "&&");
            this.BasicOverviewOutput.checkEditPageSize.Text = this.Publication.PageSize;
            this.BasicOverviewOutput.checkEditMechanicals.Text = "Mechanicals: " + this.Publication.Mechanicals;
            this.BasicOverviewOutput.checkEditAvgADRate.Text = "Avg Ad Rate: " + this.Publication.AvgADRate.ToString("$#,##0.00");
            this.BasicOverviewOutput.checkEditAvgPCIRate.Text = "Avg PCI: " + this.Publication.AvgPCIRate.ToString("$#,##0.00");
            this.BasicOverviewOutput.checkEditBusinessName.Text = this.Publication.Parent.BusinessName;

            switch (this.Publication.ColorOption)
            {
                case BusinessClasses.ColorOptions.BlackWhite:
                    this.BasicOverviewOutput.checkEditColor.Text = "Black && White";
                    break;
                case BusinessClasses.ColorOptions.SpotColor:
                    this.BasicOverviewOutput.checkEditColor.Text = "Spot Color";
                    break;
                case BusinessClasses.ColorOptions.FullColor:
                    this.BasicOverviewOutput.checkEditColor.Text = "Full Color";
                    break;
            }
            this.BasicOverviewOutput.checkEditColumnInchesValues.Text = this.Publication.ColumnsCount.ToString("#,##0.00") + " x " + this.Publication.InchesCount.ToString("#,##0.00");
            this.BasicOverviewOutput.checkEditTotalColumnInches.Text = this.Publication.Square.ToString("#,##0.00") + " col. in.";
            this.BasicOverviewOutput.checkEditTotalInches.Text = "Total Inches: " + this.Publication.TotalSquare.ToString("#,##0.00");
            this.BasicOverviewOutput.checkEditDate.Text = this.Publication.Parent.PresentationDateObject != null ? this.Publication.Parent.PresentationDate.ToString("MM/dd/yy") : string.Empty;

            List<string> dates = new List<string>();
            foreach (BusinessClasses.Insert insert in this.Publication.Inserts)
            {
                if (insert.DateObject != null)
                    dates.Add(insert.Date.ToString("MM/dd/yy"));
            }
            this.BasicOverviewOutput.memoEditDates.EditValue = string.Join(", ", dates.ToArray());

            this.BasicOverviewOutput.checkEditDecisionMaker.Text = this.Publication.Parent.DecisionMaker;
            this.BasicOverviewOutput.checkEditFlightDates.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            this.BasicOverviewOutput.checkEditFlightDates2.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            this.BasicOverviewOutput.checkEditTotalAds.Text = this.Publication.TotalInserts.ToString("#,##0");
            this.BasicOverviewOutput.checkEditTotalCost.Text = "Total Cost: " + this.Publication.TotalFinalRate.ToString("$#,##0.00");
            this.BasicOverviewOutput.checkEditTotalDiscounts.Text = "Total Discounts: " + this.Publication.TotalDiscountRate.ToString("$#,##0.00");
            this.BasicOverviewOutput.checkEditTotalDiscounts.Checked = this.Publication.TotalDiscountRate > 0;
        }

        private void UpdateMultiSummaryOutput()
        {
            this.MultiSummaryOutput.Text = this.Publication.Name.Replace("&", "&&");
            this.MultiSummaryOutput.pbLogo.Image = this.Publication.SmallLogo != null ? new Bitmap(this.Publication.SmallLogo) : null;
            this.MultiSummaryOutput.checkEditFlightDatesHeader.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            this.MultiSummaryOutput.checkEditFlightDates.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            this.MultiSummaryOutput.checkEditName.Text = this.Publication.Name.Replace("&", "&&");
            this.MultiSummaryOutput.checkEditTotalAds.Text = "Total Ads: " + this.Publication.TotalInserts.ToString("#,##0");
            this.MultiSummaryOutput.checkEditTotalSquare.Text = "Column Inches: " + this.Publication.TotalSquare.ToString("#,##0.00");
            this.MultiSummaryOutput.checkEditAdSize.Text = this.Publication.PageSize;
            this.MultiSummaryOutput.checkEditAvgAdCost.Text = "B&W Avg Ad Cost: " + this.Publication.AvgADRate.ToString("$#,##0.00");
            this.MultiSummaryOutput.checkEditAvgFinalCost.Text = "Final Avg Ad Cost: " + this.Publication.AvgFinalRate.ToString("$#,##0.00");
            this.MultiSummaryOutput.checkEditAvgPCI.Text = "Avg PCI: " + this.Publication.AvgPCIRate.ToString("$#,##0.00");
            this.MultiSummaryOutput.checkEditColumnInches.Text = this.Publication.Square.ToString("#,##0.00") + "col. in.";
            this.MultiSummaryOutput.checkEditDimensions.Text = this.Publication.Dimensions;
            this.MultiSummaryOutput.checkEditDiscounts.Text = "Discounts: " + this.Publication.TotalDiscountRate.ToString("$#,##0.00");
            this.MultiSummaryOutput.checkEditMechanicals.Text = "Mechanicals: " + this.Publication.Mechanicals;
            this.MultiSummaryOutput.labelControlSections.Text = "Sections: " + string.Join(", ", this.Publication.Inserts.Select(x => x.Section).Distinct());

            switch (this.Publication.ColorOption)
            {
                case BusinessClasses.ColorOptions.BlackWhite:
                    this.MultiSummaryOutput.checkEditColor.Text = "Black && White";
                    break;
                case BusinessClasses.ColorOptions.SpotColor:
                    this.MultiSummaryOutput.checkEditColor.Text = "Spot Color";
                    break;
                case BusinessClasses.ColorOptions.FullColor:
                    this.MultiSummaryOutput.checkEditColor.Text = "Full Color";
                    break;
            }
            List<string> dates = new List<string>();
            foreach (BusinessClasses.Insert insert in this.Publication.Inserts)
            {
                if (insert.DateObject != null)
                    dates.Add(insert.Date.ToString("MM/dd/yy"));
            }
            this.MultiSummaryOutput.memoEditDates.EditValue = string.Join(", ", dates.ToArray());
            this.MultiSummaryOutput.laInvestment.Text = "Investment: " + this.Publication.TotalFinalRate.ToString("$#,##0.00");
        }

        private void UpdateDetailedGridOutput()
        {
            this.DetailedGridOutput.Text = this.Publication.Name.Replace("&", "&&");
            this.DetailedGridOutput.pbLogo.Image = this.Publication.BigLogo != null ? new Bitmap(this.Publication.BigLogo) : null;
            this.DetailedGridOutput.checkEditName.Text = this.Publication.Name.Replace("&", "&&");
            this.DetailedGridOutput.checkEditDate.Text = this.Publication.Parent.PresentationDateObject != null ? this.Publication.Parent.PresentationDate.ToString("MM/dd/yy") : string.Empty;
            this.DetailedGridOutput.checkEditBusinessName.Text = this.Publication.Parent.BusinessName;
            this.DetailedGridOutput.checkEditDecisionMaker.Text = this.Publication.Parent.DecisionMaker;
            this.DetailedGridOutput.checkEditFlightDates.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            this.DetailedGridOutput.repositoryItemSpinEditColor.NullText = repositoryItemSpinEditColorPricingDisplay.NullText;
            this.DetailedGridOutput.gridControlPublication.DataSource = gridControlPublication.DataSource;
        }

        private void UpdateMultiGridOutput()
        {
            this.MultiGridOutput.Text = this.Publication.Name.Replace("&", "&&");
            this.MultiGridOutput.pbLogo.Image = this.Publication.BigLogo != null ? new Bitmap(this.Publication.BigLogo) : null;
            this.MultiGridOutput.checkEditName.Text = this.Publication.Name.Replace("&", "&&");
            this.MultiGridOutput.repositoryItemSpinEditColor.NullText = repositoryItemSpinEditColorPricingDisplay.NullText;
            this.MultiGridOutput.gridControlPublication.DataSource = gridControlPublication.DataSource;
            this.MultiGridOutput.AdjustGridMinHeight();
        }

        public void SwitchAdNotesMode(bool enable)
        {
            repositoryItemSpinEditADRateDisplay.Buttons[1].Visible = enable;
            repositoryItemSpinEditADRateEdit.Buttons[1].Visible = enable;
            repositoryItemSpinEditADRateDisplayNull.Buttons[1].Visible = enable;
            repositoryItemSpinEditADRateEditNull.Buttons[1].Visible = enable;
        }

        private void repositoryItemDateEditNull_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            e.AcceptValue = false;
            if (e.Value != null)
            {
                DateTime temp = DateTime.MinValue;
                DateTime.TryParse(e.Value.ToString(), out temp);
                if (temp >= ScheduleBuilderControl.Instance.LocalSchedule.FlightDateStart &&
                    temp <= ScheduleBuilderControl.Instance.LocalSchedule.FlightDateEnd)
                    e.AcceptValue = true;
            }
            if (!e.AcceptValue)
                AppManager.ShowWarning("Date must be between Flight Dates");
        }

        private void repositoryItemDateEditNull_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            advBandedGridViewPublication.CloseEditor();
            ((BindingList<BusinessClasses.Insert>)gridControlPublication.DataSource).ResetBindings();
        }

        private void advBandedGridViewPublication_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if (advBandedGridViewPublication.FocusedColumn != gridColumnDate)
            {
                if (advBandedGridViewPublication.GetRowCellValue(advBandedGridViewPublication.FocusedRowHandle, gridColumnDate) != null)
                    e.Cancel = false;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void advBandedGridViewPublication_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
            UpdateTotals();
        }

        private void gridControlPublication_Paint(object sender, PaintEventArgs e)
        {
            DevExpress.XtraGrid.GridControl gridC = sender as DevExpress.XtraGrid.GridControl;
            DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gridView = gridC.FocusedView as DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView;
            BandedGridViewInfo viewinfo = gridView.GetViewInfo() as BandedGridViewInfo;
            BandedGridViewRects gridViewRects = viewinfo.ViewRects;
            Rectangle r = gridViewRects.BandPanel;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridColumnsInfo gci = viewinfo.ColumnsInfo;
            int y = gci[gridColumnADRate].Bounds.Y - r.Height;
            int x = gci[gridColumnADRate].Bounds.Right;
            Point p1 = new Point(x, y);
            int y2 = gridViewRects.Rows.Bottom;
            Point p2 = new Point(x, y2);
            e.Graphics.DrawLine(Pens.LightBlue, p1, p2);

            y = gci[gridColumnColorPricing].Bounds.Y - r.Height;
            x = gci[gridColumnColorPricing].Bounds.Right;
            p1 = new Point(x, y);
            y2 = gridViewRects.Rows.Bottom;
            p2 = new Point(x, y2);
            e.Graphics.DrawLine(Pens.LightBlue, p1, p2);

            y = gci[gridColumnDate].Bounds.Y - r.Height;
            x = gci[gridColumnDate].Bounds.Right;
            p1 = new Point(x, y);
            y2 = gridViewRects.Rows.Bottom;
            p2 = new Point(x, y2);
            e.Graphics.DrawLine(Pens.LightBlue, p1, p2);

            y = gci[gridColumnID].Bounds.Y - r.Height;
            x = gci[gridColumnID].Bounds.Right;
            p1 = new Point(x, y);
            y2 = gridViewRects.Rows.Bottom;
            p2 = new Point(x, y2);
            e.Graphics.DrawLine(Pens.LightBlue, p1, p2);

            y = gci[gridColumnDiscountRate].Bounds.Y - r.Height;
            x = gci[gridColumnDiscountRate].Bounds.Right;
            p1 = new Point(x, y);
            y2 = gridViewRects.Rows.Bottom;
            p2 = new Point(x, y2);
            e.Graphics.DrawLine(Pens.LightBlue, p1, p2);

            y = gci[gridColumnFinalRate].Bounds.Y - r.Height;
            x = gci[gridColumnFinalRate].Bounds.Right;
            p1 = new Point(x, y);
            y2 = gridViewRects.Rows.Bottom;
            p2 = new Point(x, y2);
            e.Graphics.DrawLine(Pens.LightBlue, p1, p2);

            y = gci[gridColumnPCIRate].Bounds.Y - r.Height;
            x = gci[gridColumnPCIRate].Bounds.Right;
            p1 = new Point(x, y);
            y2 = gridViewRects.Rows.Bottom;
            p2 = new Point(x, y2);
            e.Graphics.DrawLine(Pens.LightBlue, p1, p2);
        }

        private void advBandedGridViewPublication_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnDate)
            {
                if (e.CellValue == null)
                    e.RepositoryItem = repositoryItemDateNull;
                else
                    e.RepositoryItem = repositoryItemDateEditNull;
            }
            if (e.Column == gridColumnADRate)
            {
                if (string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Comment) &&
                    string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Section) &&
                    string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Deadline))
                    e.RepositoryItem = repositoryItemSpinEditADRateDisplayNull;
                else
                    e.RepositoryItem = repositoryItemSpinEditADRateDisplay;
            }
            if (e.Column == gridColumnDiscounts || e.Column == gridColumnColorPricingPercent)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditDiscountsDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditDiscountsDisplay;
            }
            if (e.Column == gridColumnColorPricing)
            {
                if (FormMain.Instance.checkBoxItemPercentOfAdRate.Checked)
                {
                    e.RepositoryItem = repositoryItemSpinEditColorRate;
                }
                else
                {
                    if (e.RowHandle == 0)
                        e.RepositoryItem = repositoryItemSpinEditColorPricingDisplayFirstRow;
                    else
                        e.RepositoryItem = repositoryItemSpinEditColorPricingDisplay;
                }
            }
        }

        private void advBandedGridViewPublication_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnDiscounts || e.Column == gridColumnColorPricingPercent)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditDiscountsEditFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditDiscountsEdit;
            }
            if (e.Column == gridColumnColorPricing)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditColorPricingEditFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditColorPricingEdit;
            }
            if (e.Column == gridColumnADRate && this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.FlatModular)
            {
                if (string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Comment) &&
                    string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Section) &&
                    string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Deadline))
                    e.RepositoryItem = repositoryItemSpinEditADRateEditNull;
                else
                    e.RepositoryItem = repositoryItemSpinEditADRateEdit;
            }
        }

        private void repositoryItemSpinEditColorPricingEditFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                advBandedGridViewPublication.CloseEditor();
                double temp = 0;
                object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
                if (value != null)
                    double.TryParse(value.ToString(), out temp);
                this.Publication.CopyColorRate(temp);
                RefreshInserts();
            }
        }

        private void repositoryItemSpinEditDiscountsDisplayFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                advBandedGridViewPublication.CloseEditor();
                double temp = 0;
                object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
                if (value != null)
                    double.TryParse(value.ToString(), out temp);

                if (advBandedGridViewPublication.FocusedColumn == gridColumnDiscounts)
                    this.Publication.CopyDiscounts(temp);
                else if (advBandedGridViewPublication.FocusedColumn == gridColumnColorPricingPercent)
                    this.Publication.CopyColorRatePercent(temp);
                RefreshInserts();
            }
        }

        private void advBandedGridViewPublication_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view = (DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)sender;
            BandedGridHitInfo hi = view.CalcHitInfo(view.GridControl.PointToClient(MousePosition));
            if (hi.InBandPanel && (hi.Band == gridBandDate || hi.Band == gridBandIndex))
            {
                SortInserts();
            }
        }

        private void repositoryItemSpinEditADRate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (ToolForms.FormAdNotes form = new ToolForms.FormAdNotes())
            {
                form.laID.Text = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].ID;
                form.laDate.Text = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Date.ToString("ddd, MM/dd/yy");
                form.Date = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Date;
                form.laFinalRate.Text = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].FinalRate.ToString("$#,###.00");
                form.Comment = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Comment;
                form.Section = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Section;
                form.Deadline = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Deadline;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.ckCommentAll.Checked)
                    {
                        foreach (var insert in this.Publication.Inserts)
                            insert.Comment = form.Comment;
                    }
                    else
                        this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Comment = form.Comment;

                    if (form.ckSectionAll.Checked)
                    {
                        foreach (var insert in this.Publication.Inserts)
                            insert.Section = form.Section;
                    }
                    else
                        this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Section = form.Section;

                    if (form.ckDeadlineAll.Checked)
                    {
                        foreach (var insert in this.Publication.Inserts)
                            insert.Deadline = form.Deadline;
                    }
                    else
                        this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Deadline = form.Deadline;

                    if (form.ckCommentAll.Checked || form.ckSectionAll.Checked || form.ckDeadlineAll.Checked)
                        RefreshInserts();

                    advBandedGridViewPublication.CloseEditor();
                    ScheduleBuilderControl.Instance.SettingsNotSaved = true;
                }
            }
        }

        private void toolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControlPublication)
                return;
            DevExpress.Utils.ToolTipControlInfo info = null;
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = gridControlPublication.GetViewAt(e.ControlMousePosition) as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null)
                    return;
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    List<string> adNotes = new List<string>();

                    if (!string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Comment))
                        adNotes.Add(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Comment);
                    if (!string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Section))
                        adNotes.Add(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Section);
                    if (!string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Deadline))
                        adNotes.Add("Deadline: " + this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].DeadlineForOutput);

                    if (hi.Column == gridColumnADRate && adNotes.Count > 0)
                    {
                        info = new DevExpress.Utils.ToolTipControlInfo(new DevExpress.XtraGrid.Views.Base.CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), string.Join(", ", adNotes.ToArray()));
                        info.ToolTipImage = Properties.Resources.AdNoteSmall;
                        return;
                    }
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void advBandedGridViewPublication_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            if (e.Band == null) return;
            if (e.Band == gridBandDiscounts || e.Band == gridBandColorPricing)
            {
                Rectangle rect = e.Bounds;
                ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
                Brush brush =
                    e.Cache.GetGradientBrush(rect, e.Band.AppearanceHeader.BackColor,
                    e.Band.AppearanceHeader.BackColor2, e.Band.AppearanceHeader.GradientMode);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
                e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
                e.Painter.DrawObject(e.Info);

                Image img = null;
                if (e.Band == gridBandDiscounts)
                    img = imageList.Images[0];
                else if (e.Band == gridBandColorPricing)
                    img = this.Publication.ColorOption == BusinessClasses.ColorOptions.BlackWhite ? null : imageList.Images[1];
                if (img != null)
                {
                    Point p = Point.Empty;
                    p.X = (e.Bounds.Width - (img.Width + (int)e.Graphics.MeasureString(e.Info.Caption, advBandedGridViewPublication.Appearance.BandPanel.Font).Width + 15)) / 2 + e.Bounds.Left;
                    p.Y = (e.Bounds.Height - img.Height) / 2 + e.Bounds.Top;
                    e.Graphics.DrawImage(img, p);
                }

                e.Handled = true;
            }
        }
    }
}
