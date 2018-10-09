using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.ROI;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.ROI
{
	[ToolboxItem(false)]
	public partial class ROITabEControl : ChildTabBaseControl
	{
		private SizeF _scaleFactor;
		public ROITabEInfo CustomTabInfo => (ROITabEInfo)TabInfo;

		public ROITabEControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			xtraTabPageTab1.Text = CustomTabInfo.Tab1.Title;
			xtraTabPageTab2.Text = CustomTabInfo.Tab2.Title;
			xtraTabPageTab3.Text = CustomTabInfo.Tab3.Title;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);

			comboBoxEditTab1Combo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab1.Combo1Configuration);
			comboBoxEditTab1Combo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab1.Combo2Configuration);

			spinEditTab2Subheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab2.Subheader1Configuration);
			spinEditTab2Subheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab2.Subheader2Configuration);
			textEditTab2Subheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab2.Subheader3Configuration);

			comboBoxEditTab3Combo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Combo1Configuration);
			comboBoxEditTab3Combo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Combo2Configuration);
			comboBoxEditTab3Combo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Combo1Configuration);
			comboBoxEditTab3Combo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Combo2Configuration);
			spinEditTab3Subheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Subheader1Configuration);
			spinEditTab3Subheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Subheader2Configuration);
			spinEditTab3Subheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Subheader3Configuration);
			spinEditTab3Subheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Tab3.Subheader4Configuration);

			repositoryItemSpinEditTab1Cost.EnableSelectAll();
			repositoryItemSpinEditTab1Impressions.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			textEditTab2Subheader3.Properties.NullText = CustomTabInfo.Tab2.SubHeader3DefaultValue ?? textEditTab2Subheader3.Properties.NullText;

			comboBoxEditTab1Combo1.Properties.Items.Clear();
			comboBoxEditTab1Combo1.Properties.Items.AddRange(CustomTabInfo.Tab1.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTab1Combo1.Properties.NullText =
				CustomTabInfo.Tab1.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTab1Combo1.Properties.NullText;

			comboBoxEditTab1Combo2.Properties.Items.Clear();
			comboBoxEditTab1Combo2.Properties.Items.AddRange(CustomTabInfo.Tab1.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTab1Combo2.Properties.NullText =
				CustomTabInfo.Tab1.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTab1Combo2.Properties.NullText;

			comboBoxEditTab3Combo1.Properties.Items.Clear();
			comboBoxEditTab3Combo1.Properties.Items.AddRange(CustomTabInfo.Tab3.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTab3Combo1.Properties.NullText =
				CustomTabInfo.Tab3.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTab3Combo1.Properties.NullText;

			comboBoxEditTab3Combo2.Properties.Items.Clear();
			comboBoxEditTab3Combo2.Properties.Items.AddRange(CustomTabInfo.Tab3.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTab3Combo2.Properties.NullText =
				CustomTabInfo.Tab3.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTab3Combo2.Properties.NullText;

			comboBoxEditTab3Combo3.Properties.Items.Clear();
			comboBoxEditTab3Combo3.Properties.Items.AddRange(CustomTabInfo.Tab3.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTab3Combo3.Properties.NullText =
				CustomTabInfo.Tab3.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTab3Combo3.Properties.NullText;

			comboBoxEditTab3Combo4.Properties.Items.Clear();
			comboBoxEditTab3Combo4.Properties.Items.AddRange(CustomTabInfo.Tab3.Combo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTab3Combo4.Properties.NullText =
				CustomTabInfo.Tab3.Combo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTab3Combo4.Properties.NullText;

			_scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemTab1Combo1.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTab1Combo1.MaxSize, _scaleFactor);
			layoutControlItemTab1Combo1.MinSize = RectangleHelper.ScaleSize(layoutControlItemTab1Combo1.MinSize, _scaleFactor);
			layoutControlItemTab1Combo2.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTab1Combo2.MaxSize, _scaleFactor);
			layoutControlItemTab1Combo2.MinSize = RectangleHelper.ScaleSize(layoutControlItemTab1Combo2.MinSize, _scaleFactor);
			simpleLabelItemTab1Checkbox2.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTab1Checkbox2.MaxSize, _scaleFactor);
			simpleLabelItemTab1Checkbox2.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTab1Checkbox2.MinSize, _scaleFactor);
			layoutControlItemTab2Subheader1.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTab2Subheader1.MaxSize, _scaleFactor);
			layoutControlItemTab2Subheader1.MinSize = RectangleHelper.ScaleSize(layoutControlItemTab2Subheader1.MinSize, _scaleFactor);
			layoutControlItemTab2Subheader2.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTab2Subheader2.MaxSize, _scaleFactor);
			layoutControlItemTab2Subheader2.MinSize = RectangleHelper.ScaleSize(layoutControlItemTab2Subheader2.MinSize, _scaleFactor);
			layoutControlItemTab2Subheader3.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTab2Subheader3.MaxSize, _scaleFactor);
			layoutControlItemTab2Subheader3.MinSize = RectangleHelper.ScaleSize(layoutControlItemTab2Subheader3.MinSize, _scaleFactor);
			simpleLabelItemTab3Header.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTab3Header.MaxSize, _scaleFactor);
			simpleLabelItemTab3Header.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTab3Header.MinSize, _scaleFactor);

			gridBandRowTitles.Width = (Int32)(gridBandRowTitles.Width * _scaleFactor.Width);
			gridBandPrelaunch.Width = (Int32)(gridBandPrelaunch.Width * _scaleFactor.Width);
			gridBandTotals.Width = (Int32)(gridBandTotals.Width * _scaleFactor.Width);

			if (!CustomTabInfo.Tab1.HeaderForeColor.IsEmpty)
				bandedGridViewTab1.Appearance.HeaderPanel.ForeColor = CustomTabInfo.Tab1.HeaderForeColor;

			if (!CustomTabInfo.Tab1.ValueCellsForeColor.IsEmpty)
				bandedGridViewTab1.Appearance.Row.ForeColor = CustomTabInfo.Tab1.ValueCellsForeColor;

			if (!CustomTabInfo.Tab1.ValueCellsForeColor.IsEmpty)
				bandedGridViewTab1.Appearance.Row.ForeColor = CustomTabInfo.Tab1.ValueCellsForeColor;

			if (!CustomTabInfo.Tab1.GridLineColor.IsEmpty)
			{
				bandedGridViewTab1.Appearance.HorzLine.BackColor = CustomTabInfo.Tab1.GridLineColor;
				bandedGridViewTab1.Appearance.VertLine.BackColor = CustomTabInfo.Tab1.GridLineColor;
				bandedGridViewTab1.Appearance.FixedLine.BackColor = CustomTabInfo.Tab1.GridLineColor;
			}
		}

		public override void ApplyBackground()
		{
			if (TabInfo.BackgroundLogo == null) return;
			layoutControlGroupTab2Root.BackgroundImage = TabInfo.BackgroundLogo;
			layoutControlGroupTab2Root.BackgroundImageVisible = true;
			layoutControlGroupTab2Root.BackgroundImageLayout = ImageLayout.Stretch;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabE.SlideHeader ??
												CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			checkEditTab1Checkbox1.Checked = SlideContainer.EditedContent.ROIState.TabE.Tab1.Checkbox1 ??
										CustomTabInfo.Tab1.Checkbox1.Value;
			checkEditTab1Checkbox2.Checked = SlideContainer.EditedContent.ROIState.TabE.Tab1.Checkbox2 ??
											 CustomTabInfo.Tab1.Checkbox2.Value;

			comboBoxEditTab1Combo1.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab1.Combo1 ??
											CustomTabInfo.Tab1.Combo1Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault) ??
											CustomTabInfo.Tab1.Combo1Items.FirstOrDefault(item => !item.IsPlaceholder);
			comboBoxEditTab1Combo2.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab1.Combo2 ??
											CustomTabInfo.Tab1.Combo2Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault) ??
											CustomTabInfo.Tab1.Combo2Items.FirstOrDefault(item => !item.IsPlaceholder);

			LoadGridData(SlideContainer.EditedContent.ROIState.TabE.Tab1.Grid);

			spinEditTab2Subheader1.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab2.Subheader1 ?? CustomTabInfo.Tab2.SubHeader1DefaultValue;
			spinEditTab2Subheader2.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab2.Subheader2 ?? CustomTabInfo.Tab2.SubHeader2DefaultValue;
			textEditTab2Subheader3.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab2.Subheader3 ?? CustomTabInfo.Tab2.SubHeader3DefaultValue;

			comboBoxEditTab3Combo1.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo1 ??
											CustomTabInfo.Tab3.Combo1Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault) ??
											CustomTabInfo.Tab3.Combo1Items.FirstOrDefault(item => !item.IsPlaceholder);
			comboBoxEditTab3Combo2.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo2 ??
											CustomTabInfo.Tab3.Combo2Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault) ??
											CustomTabInfo.Tab3.Combo2Items.FirstOrDefault(item => !item.IsPlaceholder);
			comboBoxEditTab3Combo3.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo3 ??
											CustomTabInfo.Tab3.Combo3Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault) ??
											CustomTabInfo.Tab3.Combo3Items.FirstOrDefault(item => !item.IsPlaceholder);
			comboBoxEditTab3Combo4.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo4 ??
											CustomTabInfo.Tab3.Combo4Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault) ??
											CustomTabInfo.Tab3.Combo4Items.FirstOrDefault(item => !item.IsPlaceholder);

			spinEditTab3Subheader1.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader1 ?? CustomTabInfo.Tab3.SubHeader1DefaultValue;
			spinEditTab3Subheader2.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader2 ?? CustomTabInfo.Tab3.SubHeader2DefaultValue;
			spinEditTab3Subheader3.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader3 ?? CustomTabInfo.Tab3.SubHeader3DefaultValue;
			spinEditTab3Subheader4.EditValue = SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader4 ?? CustomTabInfo.Tab3.SubHeader4DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.ROIState.TabE.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.ROIState.TabE.Tab1.Checkbox1 =
				checkEditTab1Checkbox1.Checked != CustomTabInfo.Tab1.Checkbox1.Value ? checkEditTab1Checkbox1.Checked : (bool?)null;

			SlideContainer.EditedContent.ROIState.TabE.Tab1.Checkbox2 =
				checkEditTab1Checkbox2.Checked != CustomTabInfo.Tab1.Checkbox2.Value ? checkEditTab1Checkbox2.Checked : (bool?)null;

			SlideContainer.EditedContent.ROIState.TabE.Tab1.Combo1 = CustomTabInfo.Tab1.Combo1Items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != comboBoxEditTab1Combo1.EditValue ?
				comboBoxEditTab1Combo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTab1Combo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ROIState.TabE.Tab1.Combo2 = CustomTabInfo.Tab1.Combo2Items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != comboBoxEditTab1Combo2.EditValue ?
				comboBoxEditTab1Combo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTab1Combo2.EditValue as String } :
				null;

			SlideContainer.EditedContent.ROIState.TabE.Tab1.Grid = GetGridState();

			SlideContainer.EditedContent.ROIState.TabE.Tab2.Subheader1 = (decimal?)spinEditTab2Subheader1.EditValue != CustomTabInfo.Tab2.SubHeader1DefaultValue ?
				(decimal?)spinEditTab2Subheader1.EditValue :
				null; ;
			SlideContainer.EditedContent.ROIState.TabE.Tab2.Subheader2 = (decimal?)spinEditTab2Subheader2.EditValue != CustomTabInfo.Tab2.SubHeader2DefaultValue ?
				(decimal?)spinEditTab2Subheader2.EditValue :
				null; ;
			SlideContainer.EditedContent.ROIState.TabE.Tab2.Subheader3 = textEditTab2Subheader3.EditValue as String != CustomTabInfo.Tab2.SubHeader3DefaultValue ?
				textEditTab2Subheader3.EditValue as String ?? String.Empty :
				null;

			SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo1 = CustomTabInfo.Tab3.Combo1Items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != comboBoxEditTab3Combo1.EditValue ?
				comboBoxEditTab3Combo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTab3Combo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo2 = CustomTabInfo.Tab3.Combo2Items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != comboBoxEditTab3Combo2.EditValue ?
				comboBoxEditTab3Combo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTab3Combo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo3 = CustomTabInfo.Tab3.Combo3Items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != comboBoxEditTab3Combo3.EditValue ?
				comboBoxEditTab3Combo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTab3Combo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.ROIState.TabE.Tab3.Combo4 = CustomTabInfo.Tab3.Combo4Items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != comboBoxEditTab3Combo4.EditValue ?
				comboBoxEditTab3Combo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTab3Combo4.EditValue as String } :
				null;

			SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader1 = (decimal?)spinEditTab3Subheader1.EditValue != CustomTabInfo.Tab3.SubHeader1DefaultValue ?
				(decimal?)spinEditTab3Subheader1.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader2 = (decimal?)spinEditTab3Subheader2.EditValue != CustomTabInfo.Tab3.SubHeader2DefaultValue ?
				(decimal?)spinEditTab3Subheader2.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader3 = (decimal?)spinEditTab3Subheader3.EditValue != CustomTabInfo.Tab3.SubHeader3DefaultValue ?
				(decimal?)spinEditTab3Subheader3.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabE.Tab3.Subheader4 = (decimal?)spinEditTab3Subheader4.EditValue != CustomTabInfo.Tab3.SubHeader4DefaultValue ?
				(decimal?)spinEditTab3Subheader4.EditValue :
				null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ROIState.TabE.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ROIState.TabE.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void LoadGridData(ROIState.TabEState.Tab1State.GridState savedState)
		{
			var showPrelaunchColumn = checkEditTab1Checkbox2.Checked;

			var monthCount = Int32.Parse((comboBoxEditTab1Combo1.EditValue as ListDataItem)?.Value ?? "0");

			var useMonthTitles = checkEditTab1Checkbox1.Checked;
			var firstMonthItem = comboBoxEditTab1Combo2.EditValue as ListDataItem;
			var firstMonthIndex = CustomTabInfo.Tab1.Combo2Items.FindIndex(item =>
				String.Equals(item.Value, firstMonthItem?.Value, StringComparison.OrdinalIgnoreCase));

			var monthIndex = firstMonthIndex > 0 ? firstMonthIndex : 0;
			var monthNames = new List<string>();
			for (var i = 0; i < monthCount; i++)
			{
				if (useMonthTitles)
				{
					monthNames.Add(CustomTabInfo.Tab1.Combo2Items.ElementAt(monthIndex).Value);

					monthIndex++;
					if (monthIndex == CustomTabInfo.Tab1.Combo2Items.Count)
						monthIndex = 0;
				}
				else
					monthNames.Add(String.Format("{0} {1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i + 1));
			}

			var dataSource = new DataTable();
			var column = new DataColumn(ROITabEInfo.Tab1Info.GridColumnNameRowTitle, typeof(string));
			dataSource.Columns.Add(column);

			if (showPrelaunchColumn)
			{
				column = new DataColumn(ROITabEInfo.Tab1Info.GridColumnNamePrelaunch, typeof(decimal))
				{
					AllowDBNull = true
				};
				dataSource.Columns.Add(column);
			}

			var totalMonthExpression = new List<string>();
			for (var i = 0; i < monthCount; i++)
			{
				var columnName = String.Format("{0}{1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i);
				dataSource.Columns.Add(new DataColumn(columnName, typeof(decimal))
				{
					Caption = monthNames.ElementAt(i),
					AllowDBNull = true
				});
				totalMonthExpression.Add(String.Format("ISNULL({0},0)", columnName));
			}

			column = new DataColumn(ROITabEInfo.Tab1Info.GridColumnNameTotal, typeof(decimal))
			{
				AllowDBNull = true,
				Expression = String.Format("{0}{1}",
					showPrelaunchColumn ? String.Format("ISNULL({0},0) + ", ROITabEInfo.Tab1Info.GridColumnNamePrelaunch) : String.Empty,
					String.Join(" + ", totalMonthExpression))
			};
			dataSource.Columns.Add(column);

			var digitalImpressionsRow = dataSource.NewRow();
			digitalImpressionsRow.BeginEdit();
			digitalImpressionsRow[ROITabEInfo.Tab1Info.GridColumnNameRowTitle] = "Digital Impressions";
			if (showPrelaunchColumn)
				digitalImpressionsRow[ROITabEInfo.Tab1Info.GridColumnNamePrelaunch] = 0m;
			for (var i = 0; i < monthCount; i++)
				digitalImpressionsRow[String.Format("{0}{1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i)] =
					(object)savedState?.DigitalImpressionValues?.ElementAtOrDefault(i) ?? DBNull.Value;
			digitalImpressionsRow.EndEdit();
			dataSource.Rows.Add(digitalImpressionsRow);

			var mediaImpressionsRow = dataSource.NewRow();
			mediaImpressionsRow.BeginEdit();
			mediaImpressionsRow[ROITabEInfo.Tab1Info.GridColumnNameRowTitle] = "Television Impressions";
			if (showPrelaunchColumn)
				mediaImpressionsRow[ROITabEInfo.Tab1Info.GridColumnNamePrelaunch] = 0m;
			for (var i = 0; i < monthCount; i++)
				mediaImpressionsRow[String.Format("{0}{1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i)] =
					(object)savedState?.MediaImpressionValues?.ElementAtOrDefault(i) ?? DBNull.Value;
			mediaImpressionsRow.EndEdit();
			dataSource.Rows.Add(mediaImpressionsRow);

			var totalImpressionsRow = dataSource.NewRow();
			totalImpressionsRow.BeginEdit();
			totalImpressionsRow[ROITabEInfo.Tab1Info.GridColumnNameRowTitle] = "Total Impressions";
			if (showPrelaunchColumn)
				totalImpressionsRow[ROITabEInfo.Tab1Info.GridColumnNamePrelaunch] = 0m;
			for (var i = 0; i < monthCount; i++)
			{
				var digitalValue = savedState?.DigitalImpressionValues?.ElementAtOrDefault(i);
				var mediaValue = savedState?.MediaImpressionValues?.ElementAtOrDefault(i);

				totalImpressionsRow[String.Format("{0}{1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i)] =
					digitalValue != null || mediaValue != null ? (object)((digitalValue ?? 0m) + (mediaValue ?? 0m)) : DBNull.Value;
			}
			totalImpressionsRow.EndEdit();
			dataSource.Rows.Add(totalImpressionsRow);

			var investmentRow = dataSource.NewRow();
			investmentRow.BeginEdit();
			investmentRow[ROITabEInfo.Tab1Info.GridColumnNameRowTitle] = "Investment";
			if (showPrelaunchColumn)
				investmentRow[ROITabEInfo.Tab1Info.GridColumnNamePrelaunch] = (object)savedState?.Prelaunch ?? DBNull.Value;
			for (var i = 0; i < monthCount; i++)
				investmentRow[String.Format("{0}{1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i)] =
					(object)savedState?.InvestmentValues?.ElementAtOrDefault(i) ?? DBNull.Value;
			investmentRow.EndEdit();
			dataSource.Rows.Add(investmentRow);

			gridBandPrelaunch.Visible = showPrelaunchColumn;
			gridBandMonths.Visible = false;
			gridBandMonths.Columns.Clear();
			for (var i = 0; i < monthCount; i++)
			{
				var columnName = String.Format("{0}{1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i);
				var bandedGridColumn = new BandedGridColumn();
				bandedGridColumn.Caption = monthNames.ElementAtOrDefault(i);
				bandedGridColumn.ColumnEdit = repositoryItemSpinEditTab1Impressions;
				bandedGridColumn.FieldName = columnName;
				bandedGridColumn.OptionsColumn.FixedWidth = true;
				bandedGridColumn.Width = (Int32)(70 * _scaleFactor.Width);
				bandedGridColumn.Visible = true;
				bandedGridColumn.OptionsColumn.AllowSize = false;
				bandedGridColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
				bandedGridColumn.AppearanceHeader.TextOptions.Trimming = Trimming.None;
				bandedGridColumn.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
				bandedGridColumn.AppearanceHeader.Options.UseTextOptions = true;
				bandedGridColumn.OptionsColumn.AllowMerge = DefaultBoolean.False;
				bandedGridViewTab1.Columns.Add(bandedGridColumn);
				gridBandMonths.Columns.Add(bandedGridColumn);
			}
			gridBandMonths.Visible = true;

			gridControlTab1.DataSource = dataSource;
		}

		private ROIState.TabEState.Tab1State.GridState GetGridState()
		{
			var gridState = new ROIState.TabEState.Tab1State.GridState();

			var dataTable = (DataTable)gridControlTab1.DataSource;
			var monthCount = Int32.Parse((comboBoxEditTab1Combo1.EditValue as ListDataItem)?.Value ?? "0");

			for (var rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
			{
				var dataRow = dataTable.Rows[rowIndex];

				for (var i = 0; i < monthCount; i++)
				{
					var columnName = String.Format("{0}{1}", ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix, i);
					if (dataTable.Columns.Contains(columnName))
					{
						var columnValue = dataRow[columnName];
						switch (rowIndex)
						{
							case 0:
								gridState.DigitalImpressionValues.Add(columnValue != DBNull.Value ? (decimal?)columnValue : null);
								break;
							case 1:
								gridState.MediaImpressionValues.Add(columnValue != DBNull.Value ? (decimal?)columnValue : null);
								break;
							case 3:
								gridState.InvestmentValues.Add(columnValue != DBNull.Value ? (decimal?)columnValue : null);
								break;
						}
					}
				}

				if (rowIndex == 3 &&
					dataTable.Columns.Contains(ROITabEInfo.Tab1Info.GridColumnNamePrelaunch) &&
					dataRow[ROITabEInfo.Tab1Info.GridColumnNamePrelaunch] != DBNull.Value)
					gridState.Prelaunch = (decimal?)dataRow[ROITabEInfo.Tab1Info.GridColumnNamePrelaunch];
			}

			return gridState;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnFirstMonthCheckboxCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTab1Combo2.Enabled = checkEditTab1Checkbox1.Checked;
			if (_allowToSave)
				LoadGridData(GetGridState());
			OnEditValueChanged(sender, e);
		}

		private void OnPreLaunchCostCheckboxCheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				LoadGridData(GetGridState());
			OnEditValueChanged(sender, e);
		}

		private void OnMonthCountEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				LoadGridData(GetGridState());
			OnEditValueChanged(sender, e);
		}

		private void OnFirstMonthEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				LoadGridData(GetGridState());
			OnEditValueChanged(sender, e);
		}

		private void OnTab1GridCellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (_allowToSave)
			{
				bandedGridViewTab1.CloseEditor();
				bandedGridViewTab1.UpdateCurrentRow();

				if (e.Column.FieldName.StartsWith(ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix) &&
					(e.RowHandle == 0 || e.RowHandle == 1))
				{
					_allowToSave = false;

					var digitalValue = bandedGridViewTab1.GetRowCellValue(0, e.Column);
					var mediaValue = bandedGridViewTab1.GetRowCellValue(1, e.Column);
					bandedGridViewTab1.SetRowCellValue(2, e.Column,
						(digitalValue != DBNull.Value ? Decimal.Parse(digitalValue.ToString()) : 0) +
						(mediaValue != DBNull.Value ? Decimal.Parse(mediaValue.ToString()) : 0));

					_allowToSave = true;
				}
				bandedGridViewTab1.CloseEditor();
				bandedGridViewTab1.UpdateCurrentRow();
			}
			OnEditValueChanged(sender, e);
		}

		private void OnTab1GridCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column == bandedGridColumnTab1Total ||
				e.Column.FieldName.StartsWith(ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix))
			{
				if (e.RowHandle == 3)
					e.RepositoryItem = repositoryItemSpinEditTab1Cost;
				else
					e.RepositoryItem = repositoryItemSpinEditTab1Impressions;
			}
			if (e.Column == bandedGridColumnTab1Prefixed)
			{
				if (e.RowHandle == 3)
					e.RepositoryItem = repositoryItemSpinEditTab1Cost;
				else
					e.RepositoryItem = repositoryItemButtonEditEmpty;
			}
		}

		private void OnTab1GridShowingEditor(object sender, CancelEventArgs e)
		{
			var currentColumn = bandedGridViewTab1.FocusedColumn;
			var rowHandle = bandedGridViewTab1.FocusedRowHandle;
			if (currentColumn == null || rowHandle == GridControl.InvalidRowHandle)
				return;
			e.Cancel = currentColumn.FieldName.StartsWith(ROITabEInfo.Tab1Info.GridColumnNameMonthDefaultPrefix) &&
					   rowHandle == 2;
		}

		private void OnTab1GridRowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			if (e.Column == bandedGridColumnTab1RowTitle)
			{
				if (!CustomTabInfo.Tab1.RowTitlesForeColor.IsEmpty)
					e.Appearance.ForeColor = CustomTabInfo.Tab1.RowTitlesForeColor;
			}
			if (e.Column == bandedGridColumnTab1Prefixed)
			{
				if (e.RowHandle == 3)
				{
					e.Appearance.ForeColor = Color.Black;
					e.Appearance.BackColor = Color.White;
				}
				else
				{
					e.Appearance.ForeColor =
						e.Appearance.BackColor =
						!CustomTabInfo.Tab1.PrelaunchEmptySpaceBackColor.IsEmpty ?
						CustomTabInfo.Tab1.PrelaunchEmptySpaceBackColor :
						Color.DarkGray;
				}
			}
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftROI;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			//outputDataPackage.Theme = SelectedTheme;

			//outputDataPackage.TemplateName =
			//	MasterWizardManager.Instance.SelectedWizard.GetShiftROIFile("b_agreement.pptx");

			//outputDataPackage.TextItems.Add("SHIFT15BHEADER".ToUpper(), (SlideContainer.EditedContent.ROIState.TabE.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			//var investmentInfo = GetInvestmentInfo();
			//var outputInvestemntItems = new List<string>();
			//if (investmentInfo.MonthlyInvestment > 0 && (SlideContainer.EditedContent.ROIState.TabE.SummaryCheckbox1 ?? CustomTabInfo.SummaryCheckbox1.Value))
			//	outputInvestemntItems.Add(String.Format("{0:$#,##0.##} Monthly Investment", investmentInfo.MonthlyInvestment));
			//foreach (var item in investmentInfo.OneTimeInvestmentsList)
			//	outputInvestemntItems.Add(String.Format("{0:$#,##0.##} {1}", item.Item2, item.Item1));
			//if (investmentInfo.TotalInvestment > 0 && (SlideContainer.EditedContent.ROIState.TabE.SummaryCheckbox3 ?? CustomTabInfo.SummaryCheckbox3.Value))
			//	outputInvestemntItems.Add(String.Format("{0:$#,##0.##}", investmentInfo.TotalInvestment));
			//outputDataPackage.TextItems.Add("SHIFT15BSUBHEADER1".ToUpper(), String.Join("    |    ", outputInvestemntItems));

			//var outputTable1Items = new List<string>();
			//for (var i = 0; i < CustomTabInfo.Table1Column1Lists.Count; i++)
			//{
			//	var outputItem = (SlideContainer.EditedContent.ROIState.TabE.Table1Column1Values.ElementAtOrDefault(i) ??
			//					CustomTabInfo.Table1Column1Lists[i].FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault))?.Value;
			//	if (!String.IsNullOrWhiteSpace(outputItem))
			//		outputTable1Items.Add(outputItem);
			//}

			//var outputTable2Items = new List<string>();
			//for (var i = 0; i < CustomTabInfo.Table2Column1Lists.Count; i++)
			//{
			//	var outputItem = (SlideContainer.EditedContent.ROIState.TabE.Table2Column1Values.ElementAtOrDefault(i) ??
			//					  CustomTabInfo.Table2Column1Lists[i].FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault))?.Value;
			//	if (!String.IsNullOrWhiteSpace(outputItem))
			//		outputTable2Items.Add(outputItem);
			//}

			//outputDataPackage.TextItems.Add("SHIFT15BCOMBOMERGE1".ToUpper(), String.Join(String.Format("{0}", (char)13), outputTable1Items));
			//outputDataPackage.TextItems.Add("SHIFT15BCOMBOMERGE2".ToUpper(), String.Join(String.Format("{0}", (char)13), outputTable2Items));

			return outputDataPackage;
		}
		#endregion
	}
}