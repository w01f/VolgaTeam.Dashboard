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

        public PublicationControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font fontTitle = new Font(laAvgADRate.AppearanceItemCaption.Font.FontFamily, laAvgADRate.AppearanceItemCaption.Font.Size - 2, laAvgADRate.AppearanceItemCaption.Font.Style);
                Font fontValue = new Font(laAvgADRateValue.AppearanceItemCaption.Font.FontFamily, laAvgADRateValue.AppearanceItemCaption.Font.Size - 2, laAvgADRateValue.AppearanceItemCaption.Font.Style);
                laAvgADRate.AppearanceItemCaption.Font = fontTitle;
                laAvgADRateValue.AppearanceItemCaption.Font = fontValue;
                laAvgFinalRate.AppearanceItemCaption.Font = fontTitle;
                laAvgFinalRateValue.AppearanceItemCaption.Font = fontValue;
                laAvgPCIRate.AppearanceItemCaption.Font = fontTitle;
                laAvgPCIRateValue.AppearanceItemCaption.Font = fontValue;
                laTotalColorPricingCalculated.AppearanceItemCaption.Font = fontTitle;
                laTotalColorPricingCalculatedValue.AppearanceItemCaption.Font = fontValue;
                laTotalDiscountRate.AppearanceItemCaption.Font = fontTitle;
                laTotalDiscountRateValue.AppearanceItemCaption.Font = fontValue;
                laTotalFinalRate.AppearanceItemCaption.Font = fontTitle;
                laTotalFinalRateValue.AppearanceItemCaption.Font = fontValue;
                laTotalInserts.AppearanceItemCaption.Font = fontTitle;
                laTotalInsertsValue.AppearanceItemCaption.Font = fontValue;
                laTotalSquare.AppearanceItemCaption.Font = fontTitle;
                laTotalSquareValue.AppearanceItemCaption.Font = fontValue;
            }
            repositoryItemSpinEditPCIRateEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditPCIRateEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditPCIRateEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditPCIRateEditFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditPCIRateEditFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditPCIRateEditFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEditFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEditFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEditFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEditNull.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEditNull.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEditNull.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            repositoryItemSpinEditADRateEditNullFirstRow.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            repositoryItemSpinEditADRateEditNullFirstRow.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            repositoryItemSpinEditADRateEditNullFirstRow.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
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
        }

        #region Insert's Processing
        public void LoadInserts()
        {
            repositoryItemDateNull.NullDate = this.Publication.AvailableDays.FirstOrDefault();
            int currentRowHandle = advBandedGridViewPublication.FocusedRowHandle;
            gridControlPublication.DataSource = new BindingList<BusinessClasses.Insert>(this.Publication.Inserts);
            gridControlPublication.RefreshDataSource();
            advBandedGridViewPublication.FocusedRowHandle = currentRowHandle;
            UpdateTotals();
        }

        public void AddInsert()
        {
            this.Publication.AddInsert();
            LoadInserts();
            advBandedGridViewPublication.FocusedRowHandle = advBandedGridViewPublication.RowCount - 1;
            FormMain.Instance.buttonItemSchedulesDeleteInsert.Enabled = this.Publication.Inserts.Count > 0;
            FormMain.Instance.buttonItemSchedulesCloneInsert.Enabled = this.Publication.Inserts.Count > 0;
        }

        public void DeleteInsert()
        {
            if (AppManager.ShowWarningQuestion("Are you sure you want to delete selected line?") == DialogResult.Yes)
            {
                advBandedGridViewPublication.DeleteSelectedRows();
                this.Publication.RebuildInserts();
                LoadInserts();
                ScheduleBuilderControl.Instance.SettingsNotSaved = true;
                FormMain.Instance.buttonItemSchedulesDeleteInsert.Enabled = this.Publication.Inserts.Count > 0;
                FormMain.Instance.buttonItemSchedulesCloneInsert.Enabled = this.Publication.Inserts.Count > 0;
            }
        }

        public void CloneInsert()
        {
            if (this.Publication.Inserts.Count > 0 && advBandedGridViewPublication.FocusedRowHandle >= 0)
            {
                BusinessClasses.Insert originalInsert = this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(advBandedGridViewPublication.FocusedRowHandle)];
                if (originalInsert.DateObject != null)
                {
                    using (ToolForms.FormCloneInsert form = new ToolForms.FormCloneInsert(originalInsert))
                    {
                        form.checkEditPCIRate.Text = this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.StandartPCI ? gridBandPCIRate.Caption : gridBandADRate.Caption;
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            this.Publication.CloneInsert(originalInsert, form.SelectedDates, form.checkEditPCIRate.Checked, form.checkEditDiscount.Checked, this.Publication.ColorOption == BusinessClasses.ColorOptions.BlackWhite ? false : form.checkEditColorRate.Checked, form.checkEditComment.Checked, form.checkEditSections.Checked, form.checkEditDeadline.Checked, form.checkEditMechanicals.Checked);
                            LoadInserts();
                            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
                        }
                    }
                }
                else
                    AppManager.ShowWarning("You need to select Date first.");
            }
        }

        public void SortInserts()
        {
            this.Publication.SortInserts();
            LoadInserts();
        }

        #endregion

        #region Insert's Date Processing
        private void repositoryItemDateEditNull_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            string declineWarning = string.Empty;
            DateTime temp = DateTime.MinValue;
            if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out temp))
            {
                if (temp < this.Publication.Parent.FlightDateStart || temp > this.Publication.Parent.FlightDateEnd)
                {
                    e.AcceptValue = false;
                    declineWarning = "Pick a date that is in your Schedule Window…";
                }
                else if (!this.Publication.AvailableDays.Contains(temp))
                {
                    e.AcceptValue = false;
                    declineWarning = "This day is unavailable. Try another day…";
                }
                else
                    e.AcceptValue = true;
            }
            else
            {
                e.AcceptValue = false;
            }
            if (!e.AcceptValue && !string.IsNullOrEmpty(declineWarning))
                AppManager.ShowWarning(declineWarning);
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

        private void repositoryItemDateEdit_DrawItem(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (this.Publication.AvailableDays.Contains(e.Date))
            {
                e.Style.ForeColor = Color.Black;
                e.Style.Font = new System.Drawing.Font(e.Style.Font.Name, e.Style.Font.Size, FontStyle.Bold);
            }
            else
            {
                e.Style.ForeColor = Color.Gray;
                if (e.Date == DateTime.Today)
                {
                    RectangleF rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
                    Color backColor = Color.White;
                    e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
                    e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font,
                        new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region Grid Formatting
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


        private void toolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControlPublication)
                return;
            DevExpress.Utils.ToolTipControlInfo info = e.Info;
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = gridControlPublication.GetViewAt(e.ControlMousePosition) as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null)
                    return;
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    List<string> adNotes = new List<string>();

                    if (!string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].FullComment))
                        adNotes.Add(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].FullComment);
                    if (!string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Section))
                        adNotes.Add(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Section);
                    if (!string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Deadline))
                        adNotes.Add("Deadline: " + this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].DeadlineForOutput);
                    if (!string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Mechanicals))
                        adNotes.Add("Mech: " + this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Mechanicals);

                    if (hi.Column == gridColumnADRate && adNotes.Count > 0)
                    {
                        info = new DevExpress.Utils.ToolTipControlInfo(new DevExpress.XtraGrid.Views.Base.CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), string.Join(", ", adNotes.ToArray()));
                        info.ToolTipImage = Properties.Resources.AdNoteSmall;
                        return;
                    }
                    else if (e.Info != null)
                    {
                        if (hi.Column == gridColumnColorPricingPercent)
                            e.Info.Text = "Apply this Color Rate on Line 1, to all Ads in this schedule";
                        else if (hi.Column == gridColumnDiscountRate)
                            e.Info.Text = "Apply this Discount on Line 1 to all Ads in this schedule";
                        else if (hi.Column == gridColumnADRate)
                            e.Info.Text = "Add Comments, Sections and Deadlines";
                    }
                }
            }
            finally
            {
                e.Info = info;
            }
        }
        #endregion

        #region Editors Customization
        private void advBandedGridViewPublication_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnDate)
            {
                if (e.CellValue == null)
                    e.RepositoryItem = repositoryItemDateNull;
                else
                    e.RepositoryItem = repositoryItemDateEditNull;
            }
            else if (e.Column == gridColumnPCIRate && this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.StandartPCI)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditPCIRateDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditPCIRateDisplay;
            }
            else if (e.Column == gridColumnADRate)
            {
                if (e.RowHandle >= 0)
                {
                    if (string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].FullComment) &&
                        string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Section) &&
                        string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Mechanicals) &&
                        string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Deadline))
                    {
                        if (e.RowHandle == 0 && (this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.FlatModular || this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage))
                            e.RepositoryItem = repositoryItemSpinEditADRateDisplayNullFirstRow;
                        else
                            e.RepositoryItem = repositoryItemSpinEditADRateDisplayNull;
                    }
                    else
                    {
                        if (e.RowHandle == 0 && (this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.FlatModular || this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage))
                            e.RepositoryItem = repositoryItemSpinEditADRateDisplayFirstRow;
                        else
                            e.RepositoryItem = repositoryItemSpinEditADRateDisplay;
                    }
                }
            }
            else if (e.Column == gridColumnDiscounts || e.Column == gridColumnColorPricingPercent)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditDiscountsDisplayFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditDiscountsDisplay;
            }
            else if (e.Column == gridColumnColorPricing)
            {
                if (this.Publication.ColorPricing == BusinessClasses.ColorPricingType.PercentOfAdRate)
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
            if (e.Column == gridColumnPCIRate && this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.StandartPCI)
            {
                if (e.RowHandle == 0)
                    e.RepositoryItem = repositoryItemSpinEditPCIRateEditFirstRow;
                else
                    e.RepositoryItem = repositoryItemSpinEditPCIRateEdit;
            }
            if (e.Column == gridColumnADRate && (this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.FlatModular || this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage))
            {
                if (string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].FullComment) &&
                    string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Section) &&
                    string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Mechanicals) &&
                    string.IsNullOrEmpty(this.Publication.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Deadline))
                {
                    if (e.RowHandle == 0)
                        e.RepositoryItem = repositoryItemSpinEditADRateEditNullFirstRow;
                    else
                        e.RepositoryItem = repositoryItemSpinEditADRateEditNull;
                }
                else
                {
                    if (e.RowHandle == 0)
                        e.RepositoryItem = repositoryItemSpinEditADRateEditFirstRow;
                    else
                        e.RepositoryItem = repositoryItemSpinEditADRateEdit;
                }
            }
        }

        #endregion

        #region Editor's Buttons Clicks
        private void repositoryItemSpinEditColorPricingFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                advBandedGridViewPublication.CloseEditor();
                double temp = 0;
                object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
                if (value != null)
                    if (double.TryParse(value.ToString(), out temp))
                    {
                        this.Publication.CopyColorRate(temp);
                        LoadInserts();
                    }
            }
        }

        private void repositoryItemSpinEditDiscountsFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                advBandedGridViewPublication.CloseEditor();
                double temp = 0;
                object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
                if (value != null)
                    if (double.TryParse(value.ToString(), out temp))
                    {
                        if (advBandedGridViewPublication.FocusedColumn == gridColumnDiscounts)
                            this.Publication.CopyDiscounts(temp);
                        else if (advBandedGridViewPublication.FocusedColumn == gridColumnColorPricingPercent)
                            this.Publication.CopyColorRatePercent(temp);
                        LoadInserts();
                    }
            }
        }

        private void repositoryItemSpinEditPCIRateFirstRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                advBandedGridViewPublication.CloseEditor();
                double temp = 0;
                object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
                if (value != null)
                    if (double.TryParse(value.ToString(), out temp))
                    {
                        this.Publication.CopyPCIRate(temp);
                        LoadInserts();
                    }
            }
        }

        private void repositoryItemSpinEditADRate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                advBandedGridViewPublication.CloseEditor();
                double temp = 0;
                object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
                if (value != null)
                    if (double.TryParse(value.ToString(), out temp))
                    {
                        this.Publication.CopyAdRate(temp);
                        LoadInserts();
                    }
            }
            else if (e.Button.Index == 2)
            {
                using (ToolForms.FormAdNotes form = new ToolForms.FormAdNotes())
                {
                    if (advBandedGridViewPublication.GetFocusedDataSourceRowIndex() >= 0)
                    {
                        form.laID.Text = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].ID;
                        form.laDate.Text = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Date.ToString("ddd, MM/dd/yy");
                        form.Date = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Date;
                        form.laFinalRate.Text = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].FinalRate.ToString("$#,###.00");
                        form.CustomComment = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].CustomComment;
                        form.Comments = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Comments.ToArray();
                        form.Section = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Section;
                        form.Deadline = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Deadline;
                        form.Mechanicals = this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Mechanicals;
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            if (form.adNotesWeekdaysSelectorComments.SelectedDays.Length > 0)
                            {
                                DayOfWeek[] selectedDays = form.adNotesWeekdaysSelectorComments.SelectedDays;
                                foreach (var insert in this.Publication.Inserts.Where(x => selectedDays.Contains(x.Date.DayOfWeek)))
                                {
                                    insert.CustomComment = form.CustomComment;
                                    insert.Comments.Clear();
                                    insert.Comments.AddRange(form.Comments);
                                }
                            }
                            else
                            {
                                this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].CustomComment = form.CustomComment;
                                this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Comments.Clear();
                                this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Comments.AddRange(form.Comments);
                            }

                            if (form.adNotesWeekdaysSelectorSections.SelectedDays.Length > 0)
                            {
                                DayOfWeek[] selectedDays = form.adNotesWeekdaysSelectorSections.SelectedDays;
                                foreach (var insert in this.Publication.Inserts.Where(x => selectedDays.Contains(x.Date.DayOfWeek)))
                                    insert.Section = form.Section;
                            }
                            else
                                this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Section = form.Section;

                            if (form.adNotesWeekdaysSelectorDeadlines.SelectedDays.Length > 0)
                            {
                                DayOfWeek[] selectedDays = form.adNotesWeekdaysSelectorDeadlines.SelectedDays;
                                foreach (var insert in this.Publication.Inserts.Where(x => selectedDays.Contains(x.Date.DayOfWeek)))
                                    insert.Deadline = form.Deadline;
                            }
                            else
                                this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Deadline = form.Deadline;

                            if (form.adNotesWeekdaysSelectorMechanicals.SelectedDays.Length > 0)
                            {
                                DayOfWeek[] selectedDays = form.adNotesWeekdaysSelectorMechanicals.SelectedDays;
                                foreach (var insert in this.Publication.Inserts.Where(x => selectedDays.Contains(x.Date.DayOfWeek)))
                                    insert.Mechanicals = form.Mechanicals;
                            }
                            else
                                this.Publication.Inserts[advBandedGridViewPublication.GetFocusedDataSourceRowIndex()].Mechanicals = form.Mechanicals;

                            LoadInserts();

                            advBandedGridViewPublication.CloseEditor();
                            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
                        }
                    }
                }
            }

        }
        #endregion

        #region Common Methods and Event Handlers
        public void UpdateTotals()
        {
            laTotalInsertsValue.Text = this.Publication.TotalInserts.ToString("#,##0");
            laTotalSquareValue.Text = this.Publication.TotalSquare.HasValue ? this.Publication.TotalSquare.Value.ToString("#,##0.00#") : "N/A";
            laAvgADRateValue.Text = this.Publication.AvgADRate.ToString("$#,##0.00");
            laAvgPCIRateValue.Text = this.Publication.AvgPCIRate > 0 ? this.Publication.AvgPCIRate.ToString("$#,##0.00") : "N/A";
            laTotalDiscountRateValue.Text = this.Publication.TotalDiscountRate.ToString("$#,##0.00");
            laTotalColorPricingCalculatedValue.Text = this.Publication.TotalColorPricingCalculated.ToString("$#,##0.00");
            laAvgFinalRateValue.Text = this.Publication.AvgFinalRate.ToString("$#,##0.00");
            laTotalFinalRateValue.Text = this.Publication.TotalFinalRate.ToString("$#,##0.00");
        }

        private void advBandedGridViewPublication_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            advBandedGridViewPublication.UpdateCurrentRow();
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
            UpdateTotals();
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

        private void advBandedGridViewPublication_MouseMove(object sender, MouseEventArgs e)
        {
            if (advBandedGridViewPublication.ActiveEditor == null &&
                !FormMain.Instance.spinEditStandartHeight.EditorContainsFocus &&
                !FormMain.Instance.spinEditStandartWidth.EditorContainsFocus &&
                !FormMain.Instance.comboBoxEditPercentOfPage.EditorContainsFocus &&
                !FormMain.Instance.comboBoxEditRateCard.EditorContainsFocus &&
                !FormMain.Instance.comboBoxEditSharePagePageSize.EditorContainsFocus &&
                !FormMain.Instance.comboBoxEditStandartPageSize.EditorContainsFocus &&
                !FormMain.Instance.spinEditCostPerInch.EditorContainsFocus)
                advBandedGridViewPublication.Focus();
        }
        #endregion
    }
}
