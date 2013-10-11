using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
	[ToolboxItem(false)]
	//public partial class PrintProductControl : UserControl
	public partial class PrintProductControl : XtraTabPage
	{
		public PrintProductControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var fontTitle = new Font(laAvgADRate.AppearanceItemCaption.Font.FontFamily, laAvgADRate.AppearanceItemCaption.Font.Size - 2, laAvgADRate.AppearanceItemCaption.Font.Style);
				var fontValue = new Font(laAvgADRateValue.AppearanceItemCaption.Font.FontFamily, laAvgADRateValue.AppearanceItemCaption.Font.Size - 2, laAvgADRateValue.AppearanceItemCaption.Font.Style);
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
			repositoryItemSpinEditPCIRateEdit.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditPCIRateEdit.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditPCIRateEdit.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditPCIRateEditFirstRow.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditPCIRateEditFirstRow.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditPCIRateEditFirstRow.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditADRateEdit.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditADRateEdit.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditADRateEdit.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditADRateEditFirstRow.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditADRateEditFirstRow.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditADRateEditFirstRow.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditADRateEditNull.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditADRateEditNull.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditADRateEditNull.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditADRateEditNullFirstRow.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditADRateEditNullFirstRow.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditADRateEditNullFirstRow.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditColorPricingEdit.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditColorPricingEdit.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditColorPricingEdit.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditColorPricingEditFirstRow.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditColorPricingEditFirstRow.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditColorPricingEditFirstRow.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditDiscountsEdit.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditDiscountsEdit.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditDiscountsEdit.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditDiscountsEditFirstRow.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditDiscountsEditFirstRow.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditDiscountsEditFirstRow.MouseUp += Utilities.Instance.Editor_MouseUp;
		}

		#region Insert's Processing
		public void LoadInserts()
		{
			repositoryItemDateNull.NullDate = PrintProduct.AvailableDays.FirstOrDefault();
			int currentRowHandle = advBandedGridViewPublication.FocusedRowHandle;
			gridControlPublication.DataSource = new BindingList<Insert>(PrintProduct.Inserts);
			gridControlPublication.RefreshDataSource();
			advBandedGridViewPublication.FocusedRowHandle = currentRowHandle;
			UpdateTotals();
		}

		public void AddInsert()
		{
			PrintProduct.AddInsert();
			LoadInserts();
			advBandedGridViewPublication.FocusedRowHandle = advBandedGridViewPublication.RowCount - 1;
			Controller.Instance.PrintProductDelete.Enabled = PrintProduct.Inserts.Count > 0;
			Controller.Instance.PrintProductClone.Enabled = PrintProduct.Inserts.Count > 0;
		}

		public void DeleteInsert()
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to delete selected line?") == DialogResult.Yes)
			{
				advBandedGridViewPublication.DeleteSelectedRows();
				PrintProduct.RebuildInserts();
				LoadInserts();
				Controller.Instance.ScheduleSettings.SettingsNotSaved = true;
				Controller.Instance.PrintProductDelete.Enabled = PrintProduct.Inserts.Count > 0;
				Controller.Instance.PrintProductClone.Enabled = PrintProduct.Inserts.Count > 0;
			}
		}

		public void CloneInsert()
		{
			if (PrintProduct.Inserts.Count <= 0 || advBandedGridViewPublication.FocusedRowHandle < 0) return;
			Insert originalInsert = PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(advBandedGridViewPublication.FocusedRowHandle)];
			if (originalInsert.DateObject != null)
			{
				using (var form = new FormCloneInsert(originalInsert))
				{
					form.checkEditPCIRate.Text = PrintProduct.AdPricingStrategy == AdPricingStrategies.StandartPCI ? gridBandPCIRate.Caption : gridBandADRate.Caption;
					if (form.ShowDialog() == DialogResult.OK)
					{
						PrintProduct.CloneInsert(originalInsert, form.SelectedDates, form.checkEditPCIRate.Checked, form.checkEditDiscount.Checked, PrintProduct.ColorOption == ColorOptions.BlackWhite ? false : form.checkEditColorRate.Checked, form.checkEditComment.Checked, form.checkEditSections.Checked, form.checkEditDeadline.Checked, form.checkEditMechanicals.Checked);
						LoadInserts();
						Controller.Instance.ScheduleSettings.SettingsNotSaved = true;
					}
				}
			}
			else
				Utilities.Instance.ShowWarning("You need to select Date first.");
		}

		public void SortInserts()
		{
			PrintProduct.SortInserts();
			LoadInserts();
		}
		#endregion

		#region Insert's Date Processing
		private void repositoryItemDateEditNull_CloseUp(object sender, CloseUpEventArgs e)
		{
			string declineWarning = string.Empty;
			DateTime temp = DateTime.MinValue;
			if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out temp))
			{
				if (temp < PrintProduct.Parent.FlightDateStart || temp > PrintProduct.Parent.FlightDateEnd)
				{
					e.AcceptValue = false;
					declineWarning = "Pick a date that is in your Schedule Window…";
				}
				else if (!PrintProduct.AvailableDays.Contains(temp))
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
				Utilities.Instance.ShowWarning(declineWarning);
		}

		private void repositoryItemDateEditNull_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridViewPublication.CloseEditor();
			((BindingList<Insert>)gridControlPublication.DataSource).ResetBindings();
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

		private void repositoryItemDateEdit_DrawItem(object sender, CustomDrawDayNumberCellEventArgs e)
		{
			if (PrintProduct.AvailableDays.Contains(e.Date))
			{
				e.Style.ForeColor = Color.Black;
				e.Style.Font = new Font(e.Style.Font.Name, e.Style.Font.Size, FontStyle.Bold);
			}
			else
			{
				e.Style.ForeColor = Color.Gray;
				if (e.Date == DateTime.Today)
				{
					var rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
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
			var gridC = sender as GridControl;
			var gridView = gridC.FocusedView as AdvBandedGridView;
			var viewinfo = gridView.GetViewInfo() as BandedGridViewInfo;
			BandedGridViewRects gridViewRects = viewinfo.ViewRects;
			Rectangle r = gridViewRects.BandPanel;
			GridColumnsInfo gci = viewinfo.ColumnsInfo;
			int y = gci[gridColumnADRate].Bounds.Y - r.Height;
			int x = gci[gridColumnADRate].Bounds.Right;
			var p1 = new Point(x, y);
			int y2 = gridViewRects.Rows.Bottom;
			var p2 = new Point(x, y2);
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


		private void advBandedGridViewPublication_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
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
					img = PrintProduct.ColorOption == ColorOptions.BlackWhite ? null : imageList.Images[1];
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


		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControlPublication)
				return;
			ToolTipControlInfo info = e.Info;
			try
			{
				var view = gridControlPublication.GetViewAt(e.ControlMousePosition) as GridView;
				if (view == null)
					return;
				GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
				if (hi.InRowCell)
				{
					var adNotes = new List<string>();

					if (!string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].FullComment))
						adNotes.Add(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].FullComment);
					if (!string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].FullSection))
						adNotes.Add(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].FullSection);
					if (!string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Deadline))
						adNotes.Add("Deadline: " + PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].DeadlineForOutput);
					if (!string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Mechanicals))
						adNotes.Add("Mech: " + PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(hi.RowHandle)].Mechanicals);

					if (hi.Column == gridColumnADRate && adNotes.Count > 0)
					{
						info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), string.Join(", ", adNotes.ToArray()));
						info.ToolTipImage = Resources.AdNoteSmall;
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
		private void advBandedGridViewPublication_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column == gridColumnDate)
			{
				if (e.CellValue == null)
					e.RepositoryItem = repositoryItemDateNull;
				else
					e.RepositoryItem = repositoryItemDateEditNull;
			}
			else if (e.Column == gridColumnPCIRate && PrintProduct.AdPricingStrategy == AdPricingStrategies.StandartPCI)
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
					if (string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].FullComment) &&
						string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].FullSection) &&
						string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Mechanicals) &&
						string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Deadline))
					{
						if (e.RowHandle == 0 && (PrintProduct.AdPricingStrategy == AdPricingStrategies.FlatModular || PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage))
							e.RepositoryItem = repositoryItemSpinEditADRateDisplayNullFirstRow;
						else
							e.RepositoryItem = repositoryItemSpinEditADRateDisplayNull;
					}
					else
					{
						if (e.RowHandle == 0 && (PrintProduct.AdPricingStrategy == AdPricingStrategies.FlatModular || PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage))
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
				if (PrintProduct.ColorPricing == ColorPricingType.PercentOfAdRate)
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

		private void advBandedGridViewPublication_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
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
			if (e.Column == gridColumnPCIRate && PrintProduct.AdPricingStrategy == AdPricingStrategies.StandartPCI)
			{
				if (e.RowHandle == 0)
					e.RepositoryItem = repositoryItemSpinEditPCIRateEditFirstRow;
				else
					e.RepositoryItem = repositoryItemSpinEditPCIRateEdit;
			}
			if (e.Column == gridColumnADRate && (PrintProduct.AdPricingStrategy == AdPricingStrategies.FlatModular || PrintProduct.AdPricingStrategy == AdPricingStrategies.SharePage))
			{
				if (string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].FullComment) &&
					string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].FullSection) &&
					string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Mechanicals) &&
					string.IsNullOrEmpty(PrintProduct.Inserts[advBandedGridViewPublication.GetDataSourceRowIndex(e.RowHandle)].Deadline))
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
		private void repositoryItemSpinEditColorPricingFirstRow_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Index == 1)
			{
				advBandedGridViewPublication.CloseEditor();
				double temp = 0;
				object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
				if (value != null)
					if (double.TryParse(value.ToString(), out temp))
					{
						PrintProduct.CopyColorRate(temp);
						LoadInserts();
					}
			}
		}

		private void repositoryItemSpinEditDiscountsFirstRow_ButtonClick(object sender, ButtonPressedEventArgs e)
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
							PrintProduct.CopyDiscounts(temp);
						else if (advBandedGridViewPublication.FocusedColumn == gridColumnColorPricingPercent)
							PrintProduct.CopyColorRatePercent(temp);
						LoadInserts();
					}
			}
		}

		private void repositoryItemSpinEditPCIRateFirstRow_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Index == 1)
			{
				advBandedGridViewPublication.CloseEditor();
				double temp = 0;
				object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
				if (value != null)
					if (double.TryParse(value.ToString(), out temp))
					{
						PrintProduct.CopyPCIRate(temp);
						LoadInserts();
					}
			}
		}

		private void repositoryItemSpinEditADRate_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Index == 1)
			{
				advBandedGridViewPublication.CloseEditor();
				double temp = 0;
				object value = advBandedGridViewPublication.GetFocusedRowCellValue(advBandedGridViewPublication.FocusedColumn);
				if (value != null)
					if (double.TryParse(value.ToString(), out temp))
					{
						PrintProduct.CopyAdRate(temp);
						LoadInserts();
					}
			}
			else if (e.Button.Index == 2)
			{
				advBandedGridViewPublication.CloseEditor();
				var selectedInsert = advBandedGridViewPublication.GetFocusedRow() as Insert;
				if (selectedInsert == null) return;
				using (var form = new FormAdNotes())
				{
					form.laID.Text = selectedInsert.ID;
					form.laDate.Text = selectedInsert.Date.ToString("ddd, MM/dd/yy");
					form.Date = selectedInsert.Date;
					form.laFinalRate.Text = selectedInsert.FinalRate.ToString("$#,###.00");
					form.CustomComment = selectedInsert.CustomComment;
					form.Comments = selectedInsert.Comments.ToArray();
					form.CustomSection = selectedInsert.CustomSection;
					form.Sections = selectedInsert.Sections.ToArray();
					form.Deadline = selectedInsert.Deadline;
					form.Mechanicals = selectedInsert.Mechanicals;

					if (PrintProduct.Inserts.Count > 1)
					{
						var selectDays = new Action<AdNotesDaysSelector, string, string>((daysSelectorContainer, formCaption, header) =>
						{
							using (var dateSelector = new FormDateSelector())
							{
								dateSelector.Text = formCaption;
								dateSelector.laTitle.Text = header;
								foreach (var insert in PrintProduct.Inserts.Where(i => i != selectedInsert))
									dateSelector.checkedListBoxControlDates.Items.Add(insert.Date, String.Format("{0}   {1}", insert.Date.ToString("ddd, MM/dd/yy"), insert.FinalRate.ToString("$#,###.00")), form.adNotesDaysSelectorComments.SelectedDays.Contains(insert.Date) ? CheckState.Checked : CheckState.Unchecked, true);
								if (dateSelector.ShowDialog() != DialogResult.OK) return;
								daysSelectorContainer.SelectedDays.Clear();
								foreach (var item in dateSelector.checkedListBoxControlDates.Items.Cast<CheckedListBoxItem>().Where(item => item.CheckState == CheckState.Checked))
									daysSelectorContainer.SelectedDays.Add((DateTime) item.Value);
							}
						});
						form.adNotesDaysSelectorComments.buttonXApplyOtherDays.Click += (obj, args) => selectDays(form.adNotesDaysSelectorComments,
							"Comments & Publications",
							"Do you want to add these comments to other days in your schedule?"
							);
						form.adNotesDaysSelectorSections.buttonXApplyOtherDays.Click += (obj, args) => selectDays(form.adNotesDaysSelectorSections,
							"Sections",
							"Do you want to add these sections to other days in your schedule?"
							);
						form.adNotesDaysSelectorDeadlines.buttonXApplyOtherDays.Click += (obj, args) => selectDays(form.adNotesDaysSelectorDeadlines,
							"Deadlines",
							"Do you want to add these deadlines to other days in your schedule?"
							);
						form.adNotesDaysSelectorMechanicals.buttonXApplyOtherDays.Click += (obj, args) => selectDays(form.adNotesDaysSelectorMechanicals,
							"Mechanicals",
							"Do you want to add these mechanicals to other days in your schedule?"
							);
					}
					else
					{
						form.adNotesDaysSelectorComments.Visible = false;
						form.adNotesDaysSelectorSections.Visible = false;
						form.adNotesDaysSelectorDeadlines.Visible = false;
						form.adNotesDaysSelectorMechanicals.Visible = false;
					}

					if (form.ShowDialog() != DialogResult.OK) return;

					selectedInsert.CustomComment = form.CustomComment;
					selectedInsert.Comments.Clear();
					selectedInsert.Comments.AddRange(form.Comments);
					if (form.adNotesDaysSelectorComments.SelectedDays.Any())
					{
						var selectedDays = form.adNotesDaysSelectorComments.SelectedDays;
						foreach (var insert in PrintProduct.Inserts.Where(x => selectedDays.Contains(x.Date)))
						{
							insert.CustomComment = form.CustomComment;
							insert.Comments.Clear();
							insert.Comments.AddRange(form.Comments);
						}
					}

					selectedInsert.CustomSection = form.CustomSection;
					selectedInsert.Sections.Clear();
					selectedInsert.Sections.AddRange(form.Sections);
					if (form.adNotesDaysSelectorSections.SelectedDays.Any())
					{
						var selectedDays = form.adNotesDaysSelectorSections.SelectedDays;
						foreach (var insert in PrintProduct.Inserts.Where(x => selectedDays.Contains(x.Date)))
						{
							insert.CustomSection = form.CustomSection;
							insert.Sections.Clear();
							insert.Sections.AddRange(form.Sections);
						}
					}

					selectedInsert.Deadline = form.Deadline;
					if (form.adNotesDaysSelectorDeadlines.SelectedDays.Any())
					{
						var selectedDays = form.adNotesDaysSelectorDeadlines.SelectedDays;
						foreach (var insert in PrintProduct.Inserts.Where(x => selectedDays.Contains(x.Date)))
							insert.Deadline = form.Deadline;
					}

					selectedInsert.Mechanicals = form.Mechanicals;
					if (form.adNotesDaysSelectorMechanicals.SelectedDays.Any())
					{
						var selectedDays = form.adNotesDaysSelectorMechanicals.SelectedDays;
						foreach (var insert in PrintProduct.Inserts.Where(x => selectedDays.Contains(x.Date)))
							insert.Mechanicals = form.Mechanicals;
					}

					LoadInserts();

					Controller.Instance.ScheduleSettings.SettingsNotSaved = true;
				}
			}
		}
		#endregion

		#region Common Methods and Event Handlers
		public void UpdateTotals()
		{
			laTotalInsertsValue.Text = PrintProduct.TotalInserts.ToString("#,##0");
			laTotalSquareValue.Text = PrintProduct.TotalSquare.HasValue ? PrintProduct.TotalSquare.Value.ToString("#,##0.00#") : "N/A";
			laAvgADRateValue.Text = PrintProduct.AvgADRate.ToString("$#,##0.00");
			laAvgPCIRateValue.Text = PrintProduct.AvgPCIRate > 0 ? PrintProduct.AvgPCIRate.ToString("$#,##0.00") : "N/A";
			laTotalDiscountRateValue.Text = PrintProduct.TotalDiscountRate.ToString("$#,##0.00");
			laTotalColorPricingCalculatedValue.Text = PrintProduct.TotalColorPricingCalculated.ToString("$#,##0.00");
			laAvgFinalRateValue.Text = PrintProduct.AvgFinalRate.ToString("$#,##0.00");
			laTotalFinalRateValue.Text = PrintProduct.TotalFinalRate.ToString("$#,##0.00");
		}

		private void advBandedGridViewPublication_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridViewPublication.UpdateCurrentRow();
			Controller.Instance.ScheduleSettings.SettingsNotSaved = true;
			UpdateTotals();
		}

		private void advBandedGridViewPublication_Click(object sender, EventArgs e)
		{
			var view = (AdvBandedGridView)sender;
			BandedGridHitInfo hi = view.CalcHitInfo(view.GridControl.PointToClient(MousePosition));
			if (hi.InBandPanel && (hi.Band == gridBandDate || hi.Band == gridBandIndex))
			{
				SortInserts();
			}
		}

		private void advBandedGridViewPublication_MouseMove(object sender, MouseEventArgs e)
		{
			if (advBandedGridViewPublication.ActiveEditor == null &&
				!Controller.Instance.PrintProductStandartHeight.EditorContainsFocus &&
				!Controller.Instance.PrintProductStandartWidth.EditorContainsFocus &&
				!Controller.Instance.PrintProductPercentOfPage.EditorContainsFocus &&
				!Controller.Instance.PrintProductRateCard.EditorContainsFocus &&
				!Controller.Instance.PrintProductSharePagePageSizeCombo.EditorContainsFocus &&
				!Controller.Instance.PrintProductStandartPageSizeCombo.EditorContainsFocus &&
				!Controller.Instance.PrintProductCostPerInch.EditorContainsFocus)
				advBandedGridViewPublication.Focus();
		}
		#endregion

		public PrintProduct PrintProduct { get; set; }
	}
}