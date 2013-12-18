using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using FormNewSchedule = NewBizWiz.MediaSchedule.Controls.ToolForms.FormNewSchedule;
using Schedule = NewBizWiz.Core.MediaSchedule.Schedule;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class ScheduleSectionControl : UserControl
	{
		private readonly List<BandedGridColumn> _spotColumns = new List<BandedGridColumn>();
		private bool _allowTosave;
		protected Schedule _localSchedule;
		protected string _helpKey;

		public ScheduleSectionControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			xtraTabPageOptionsLine.Text = MediaMetaData.Instance.DataTypeString;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(laTotalPeriodsTitle.Font.FontFamily, laTotalPeriodsTitle.Font.Size - 2, laTotalPeriodsTitle.Font.Style);
				laTotalPeriodsTitle.Font = font;
				laTotalGRPTitle.Font = font;
				laTotalCPPTitle.Font = font;
				laAvgRateTitle.Font = font;
				laTotalCostTitle.Font = font;
				laNetRateTitle.Font = font;
				laAgencyDiscountTitle.Font = font;
				font = new Font(laTotalPeriodsValue.Font.FontFamily, laTotalPeriodsValue.Font.Size - 2, laTotalPeriodsValue.Font.Style);
				laTotalPeriodsValue.Font = font;
				laTotalGRPValue.Font = font;
				laTotalCPPValue.Font = font;
				laAvgRateValue.Font = font;
				laTotalCostValue.Font = font;
				laNetRateValue.Font = font;
				laAgencyDiscountValue.Font = font;
			}
			repositoryItemComboBoxDays.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxDays.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxDays.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxDayparts.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxDayparts.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxDayparts.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxLengths.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxLengths.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxLengths.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemPopupContainerEditProgram.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemPopupContainerEditProgram.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemPopupContainerEditProgram.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemTextEditProgram.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemTextEditProgram.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemTextEditProgram.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxStations.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxStations.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxStations.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemComboBoxTimes.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemComboBoxTimes.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemComboBoxTimes.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEdit000s.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEdit000s.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEdit000s.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditRate.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditRate.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditRate.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditRating.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditRating.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditRating.MouseUp += Utilities.Instance.Editor_MouseUp;
			repositoryItemSpinEditSpot.Enter += Utilities.Instance.Editor_Enter;
			repositoryItemSpinEditSpot.MouseDown += Utilities.Instance.Editor_MouseDown;
			repositoryItemSpinEditSpot.MouseUp += Utilities.Instance.Editor_MouseUp;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			digitalInfoControl.RequestDefaultInfo += (o, args) => { args.Editor.EditValue = _localSchedule.GetDigitalInfo(args); };
			digitalInfoControl.SettingsChanged += (o, args) => { SettingsNotSaved = true; };
		}

		public bool SettingsNotSaved { get; set; }

		public virtual ScheduleSection ScheduleSection
		{
			get { return _localSchedule.WeeklySchedule; }
		}

		public virtual ButtonItem OptionsButton
		{
			get { return null; }
		}
		public virtual ButtonItem ThemeButton
		{
			get { return null; }
		}
		public virtual ButtonItem PowerPointButton
		{
			get { return null; }
		}
		public virtual ButtonItem EmailButton
		{
			get { return null; }
		}
		public virtual ButtonItem PreviewButton
		{
			get { return null; }
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
					SaveSchedule();
				result = true;
				return result;
			}
		}

		#region Methods

		private void UpdateGrid(bool quickLoad)
		{
			int focussedRow = advBandedGridViewSchedule.FocusedRowHandle;
			advBandedGridViewSchedule.BeginUpdate();
			gridControlSchedule.DataSource = null;
			gridControlSchedule.DataMember = string.Empty;
			bandedGridColumnDay.FieldName = ScheduleSection.ProgramDayDataColumnName;
			bandedGridColumnDaypart.FieldName = ScheduleSection.ProgramDaypartDataColumnName;
			bandedGridColumnCPP.FieldName = ScheduleSection.ProgramCPPDataColumnName;
			bandedGridColumnCPP.SummaryItem.FieldName = ScheduleSection.ProgramTotalCPPDataColumnName;
			bandedGridColumnGRP.FieldName = ScheduleSection.ProgramGRPDataColumnName;
			bandedGridColumnGRP.SummaryItem.FieldName = ScheduleSection.ProgramGRPDataColumnName;
			bandedGridColumnIndex.FieldName = ScheduleSection.ProgramIndexDataColumnName;
			bandedGridColumnLength.FieldName = ScheduleSection.ProgramLengthDataColumnName;
			bandedGridColumnName.FieldName = ScheduleSection.ProgramNameDataColumnName;
			bandedGridColumnRate.FieldName = ScheduleSection.ProgramRateDataColumnName;
			bandedGridColumnRating.FieldName = ScheduleSection.ProgramRatingDataColumnName;
			bandedGridColumnStation.FieldName = ScheduleSection.ProgramStationDataColumnName;
			bandedGridColumnTime.FieldName = ScheduleSection.ProgramTimeDataColumnName;
			bandedGridColumnTotalSpots.FieldName = ScheduleSection.ProgramTotalSpotDataColumnName;
			bandedGridColumnTotalSpots.SummaryItem.FieldName = ScheduleSection.ProgramTotalSpotDataColumnName;
			bandedGridColumnCost.FieldName = ScheduleSection.ProgramCostDataColumnName;
			bandedGridColumnCost.SummaryItem.FieldName = ScheduleSection.ProgramCostDataColumnName;
			ScheduleSection.GenerateDataSource();
			if (!quickLoad)
				BuildSpotColumns();
			if (ScheduleSection.Programs.Count > 0)
			{
				gridControlSchedule.DataSource = ScheduleSection.DataSource;
				gridControlSchedule.DataMember = ScheduleSection.ProgramDataTableName;
			}
			advBandedGridViewSchedule.EndUpdate();
			if (focussedRow >= 0 && focussedRow < advBandedGridViewSchedule.RowCount)
				advBandedGridViewSchedule.FocusedRowHandle = focussedRow;
		}

		private void UpdateTotalsVisibility()
		{
			pnTotalCPP.Visible = ScheduleSection.ShowTotalCPP;
			pnTotalCPP.SendToBack();
			pnTotalGRP.Visible = ScheduleSection.ShowTotalGRP;
			pnTotalGRP.SendToBack();
			pnTotalSpots.Visible = ScheduleSection.ShowTotalSpots;
			pnTotalSpots.SendToBack();
			pnTotalPeriods.Visible = ScheduleSection.ShowTotalPeriods;
			pnTotalPeriods.SendToBack();
			pnAvgRate.Visible = ScheduleSection.ShowAverageRate;
			pnAvgRate.BringToFront();
			pnTotalCost.Visible = ScheduleSection.ShowTotalRate;
			pnTotalCost.BringToFront();
			pnNetRate.Visible = ScheduleSection.ShowNetRate;
			pnNetRate.BringToFront();
			pnAgencyDiscount.Visible = ScheduleSection.ShowDiscount;
			pnAgencyDiscount.BringToFront();
		}

		private void UpdateColumnsAccordingScreenSize()
		{
			if (Utilities.Instance.IsSmallScreen)
			{
				if (ScheduleSection.ShowStation && ScheduleSection.ShowDaypart)
				{
					bandedGridColumnStation.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnStation, 0, 0);
					bandedGridColumnDaypart.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDaypart, 0, 1);
				}
				else if (ScheduleSection.ShowStation)
				{
					bandedGridColumnStation.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnStation, 0, 0);
				}
				else if (ScheduleSection.ShowDaypart)
				{
					bandedGridColumnDaypart.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDaypart, 0, 0);
				}
				bandedGridColumnName.RowCount = 1;
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnName, 1, 0);
				gridBandProgram.Width = 238;

				bandedGridColumnTotalSpots.Caption = "Spots";
				gridBandTotals1.Width = 134;
				gridBandTotals1.Visible = ScheduleSection.ShowTotalSpots || ScheduleSection.ShowCost;
				gridBandTotals2.Width = 134;
				gridBandTotals2.Visible = ScheduleSection.ShowGRP || ScheduleSection.ShowCPP;
				gridBandTotals2.Columns.Add(bandedGridColumnGRP);
				gridBandTotals2.Columns.Add(bandedGridColumnCPP);
				if (ScheduleSection.ShowTotalSpots && ScheduleSection.ShowCost)
				{
					bandedGridColumnTotalSpots.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTotalSpots, 0, 0);
					bandedGridColumnCost.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCost, 1, 0);
				}
				else
				{
					bandedGridColumnTotalSpots.RowCount = 2;
					bandedGridColumnCost.RowCount = 2;
				}
				if (ScheduleSection.ShowGRP && ScheduleSection.ShowCPP)
				{
					bandedGridColumnGRP.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnGRP, 0, 1);
					bandedGridColumnCPP.RowCount = 1;
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnCPP, 1, 1);
				}
				else
				{
					bandedGridColumnGRP.RowCount = 2;
					bandedGridColumnCPP.RowCount = 2;
				}
			}
			else
				gridBandTotals2.Visible = false;
		}

		private void UpdateTotalsValues()
		{
			if (ScheduleSection.Programs.Any())
			{
				pnBottom.Enabled = true;
				laTotalPeriodsValue.Text = ScheduleSection.TotalPeriods.ToString("#,##0");
				laTotalSpotsValue.Text = ScheduleSection.TotalSpots.ToString("#,##0");
				laTotalGRPValue.Text = ScheduleSection.TotalGRP.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");
				laTotalCPPValue.Text = ScheduleSection.TotalCPP.ToString("$#,###.00");
				laAvgRateValue.Text = ScheduleSection.AvgRate.ToString("$#,###.00");
				laTotalCostValue.Text = ScheduleSection.TotalCost.ToString("$#,##0");
				laNetRateValue.Text = ScheduleSection.NetRate.ToString("$#,###.00");
				laAgencyDiscountValue.Text = ScheduleSection.Discount.ToString("$#,###.00");
			}
			else
			{
				pnBottom.Enabled = false;
				laTotalPeriodsValue.Text =
				laTotalSpotsValue.Text =
				laTotalGRPValue.Text =
				laTotalCPPValue.Text =
				laAvgRateValue.Text =
				laTotalCostValue.Text =
				laNetRateValue.Text =
				laAgencyDiscountValue.Text = String.Empty;
			}
		}

		private void UpdateOutputStatus(bool enabled)
		{
			PowerPointButton.Enabled = enabled;
			ThemeButton.Enabled = enabled;
			EmailButton.Enabled = enabled;
			PreviewButton.Enabled = enabled;
		}

		private void BuildSpotColumns()
		{
			foreach (var column in _spotColumns)
			{
				gridBandSpots.Columns.Remove(column);
				advBandedGridViewSchedule.Columns.Remove(column);
			}

			_spotColumns.Clear();

			gridBandSpots.Visible = false;

			foreach (DataColumn column in ScheduleSection.DataSource.Tables[ScheduleSection.ProgramDataTableName].Columns)
			{
				if (!column.ColumnName.Contains(ScheduleSection.ProgramSpotDataColumnNamePrefix)) continue;
				var bandedGridColumn = new BandedGridColumn();
				bandedGridColumn.AppearanceCell.Options.UseTextOptions = true;
				bandedGridColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
				bandedGridColumn.AutoFillDown = true;
				bandedGridColumn.Caption = column.Caption;
				bandedGridColumn.ColumnEdit = repositoryItemSpinEditSpot;
				bandedGridColumn.FieldName = column.ColumnName;
				bandedGridColumn.OptionsColumn.FixedWidth = true;
				bandedGridColumn.RowCount = 2;
				bandedGridColumn.Width = 45;
				bandedGridColumn.Visible = true;
				bandedGridColumn.SummaryItem.FieldName = column.ColumnName;
				bandedGridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
				_spotColumns.Add(bandedGridColumn);
				advBandedGridViewSchedule.Columns.Add(bandedGridColumn);
				gridBandSpots.Columns.Add(bandedGridColumn);
			}
			gridBandSpots.Visible = _spotColumns.Count > 0 && ScheduleSection.ShowSpots;
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowTosave = false;

			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			ScheduleSection.DataChanged += ScheduleSection_DataChanged;
			FormThemeSelector.Link(ThemeButton, BusinessWrapper.Instance.ThemeManager, _localSchedule.ThemeName, (t =>
			{
				_localSchedule.ThemeName = t.Name;
				SettingsNotSaved = true;
			}));

			laAdvertiser.Text = _localSchedule.BusinessName;
			laDemoName.Text = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);
			laScheduleWindow.Text = _localSchedule.FlightDateStart.HasValue && _localSchedule.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { _localSchedule.FlightDateStart.Value.ToString("MM/dd/yy"), _localSchedule.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
			laDemoName.Visible = !string.IsNullOrEmpty(_localSchedule.Demo);
			laDemoName.BringToFront();

			buttonXRating.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXCPP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXGRP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXTotalCPP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXTotalGRP.Enabled = _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
			buttonXRating.Text = _localSchedule.DemoType == DemoType.Rtg ? "Rtg" : "(000s)";
			bandedGridColumnRating.Caption = _localSchedule.DemoType == DemoType.Rtg ? "Rtg" : "(000s)";
			bandedGridColumnRating.ColumnEdit = _localSchedule.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			buttonXCPP.Text = _localSchedule.DemoType == DemoType.Rtg ? "CPP" : "CPM";
			buttonXTotalCPP.Text = _localSchedule.DemoType == DemoType.Rtg ? "Overall CPP" : "Overall CPM";
			laTotalCPPTitle.Text = _localSchedule.DemoType == DemoType.Rtg ? "Overall CPP:" : "Overall CPM:";
			bandedGridColumnCPP.Caption = _localSchedule.DemoType == DemoType.Rtg ? "CPP" : "CPM";
			buttonXGRP.Text = _localSchedule.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
			buttonXTotalGRP.Text = _localSchedule.DemoType == DemoType.Rtg ? "Total GRPs" : "Total Impr";
			laTotalGRPTitle.Text = _localSchedule.DemoType == DemoType.Rtg ? "Total GRPs:" : "Total Impr:";
			bandedGridColumnGRP.Caption = _localSchedule.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
			bandedGridColumnGRP.ColumnEdit = _localSchedule.DemoType == DemoType.Rtg ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			bandedGridColumnGRP.SummaryItem.DisplayFormat = _localSchedule.DemoType == DemoType.Rtg ? "{0:n1}" : "{0:n0}";
			buttonXRate.Checked = ScheduleSection.ShowRate;
			buttonXRating.Checked = ScheduleSection.ShowRating;
			buttonXCost.Checked = ScheduleSection.ShowCost;
			buttonXCPP.Checked = ScheduleSection.ShowCPP;
			buttonXDay.Checked = ScheduleSection.ShowDay;
			buttonXDaypart.Checked = ScheduleSection.ShowDaypart;
			buttonXGRP.Checked = ScheduleSection.ShowGRP;
			buttonXLength.Checked = ScheduleSection.ShowLenght;
			buttonXStation.Checked = ScheduleSection.ShowStation;
			buttonXTime.Checked = ScheduleSection.ShowTime;
			buttonXSpots.Checked = ScheduleSection.ShowSpots;
			checkEditEmptySports.Enabled = ScheduleSection.ShowSpots;
			checkEditEmptySports.Checked = !ScheduleSection.ShowEmptySpots;

			buttonXTotalPeriods.Checked = ScheduleSection.ShowTotalPeriods;
			buttonXTotalSpots.Checked = ScheduleSection.ShowTotalSpots;
			buttonXTotalGRP.Checked = ScheduleSection.ShowTotalGRP;
			buttonXTotalCPP.Checked = ScheduleSection.ShowTotalCPP;
			buttonXAvgRate.Checked = ScheduleSection.ShowAverageRate;
			buttonXTotalCost.Checked = ScheduleSection.ShowTotalRate;
			buttonXNetRate.Checked = ScheduleSection.ShowNetRate;
			buttonXDiscount.Checked = ScheduleSection.ShowDiscount;

			bandedGridColumnRate.Visible = ScheduleSection.ShowRate;
			bandedGridColumnRating.Visible = ScheduleSection.ShowRating;
			gridBandRate.Visible = ScheduleSection.ShowRate | ScheduleSection.ShowRating;
			if (ScheduleSection.ShowRate)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
			if (ScheduleSection.ShowRating)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);


			bandedGridColumnCPP.Visible = ScheduleSection.ShowCPP;
			bandedGridColumnGRP.Visible = ScheduleSection.ShowGRP;
			bandedGridColumnCost.Visible = ScheduleSection.ShowCost;

			bandedGridColumnLength.Visible = ScheduleSection.ShowLenght;
			gridBandLength.Visible = ScheduleSection.ShowLenght;

			bandedGridColumnDay.Visible = ScheduleSection.ShowDay;
			bandedGridColumnTime.Visible = ScheduleSection.ShowTime;
			gridBandDate.Visible = ScheduleSection.ShowDay | ScheduleSection.ShowTime;
			if (ScheduleSection.ShowDay)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
			if (ScheduleSection.ShowTime)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

			bandedGridColumnStation.Visible = ScheduleSection.ShowStation;
			bandedGridColumnDaypart.Visible = ScheduleSection.ShowDaypart;
			gridBandSpots.Visible = _spotColumns.Count > 0 && ScheduleSection.ShowSpots;

			UpdateColumnsAccordingScreenSize();

			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(_localSchedule.Stations.Where(x => x.Available).Select(x => x.Name).ToArray());
			repositoryItemComboBoxDayparts.Items.Clear();
			repositoryItemComboBoxDayparts.Items.AddRange(_localSchedule.Dayparts.Where(x => x.Available).Select(x => x.Code).ToArray());

			if (!quickLoad)
			{
				repositoryItemComboBoxLengths.Items.Clear();
				repositoryItemComboBoxLengths.Items.AddRange(MediaMetaData.Instance.ListManager.Lengths);
				repositoryItemComboBoxDays.Items.Clear();
				repositoryItemComboBoxDays.Items.AddRange(MediaMetaData.Instance.ListManager.Days);
				repositoryItemComboBoxTimes.Items.Clear();
				repositoryItemComboBoxTimes.Items.AddRange(MediaMetaData.Instance.ListManager.Times);

				UpdateTotalsVisibility();
			}

			UpdateGrid(quickLoad);
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());

			xtraTabPageOptionsDigital.PageEnabled = _localSchedule.DigitalProducts.Any();
			digitalInfoControl.InitData(ScheduleSection.DigitalLegend);

			xtraScrollableControlColors.Controls.Clear();
			var selectedColor = BusinessWrapper.Instance.OutputManager.AvailableColors.FirstOrDefault(c => c.Name.Equals(MediaMetaData.Instance.SettingsManager.SelectedColor)) ?? BusinessWrapper.Instance.OutputManager.AvailableColors.FirstOrDefault();
			var topPosition = 20;
			foreach (var color in BusinessWrapper.Instance.OutputManager.AvailableColors)
			{
				var button = new ButtonX();
				button.Height = 50;
				button.Width = pnColors.Width - 40;
				button.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
				button.TextAlignment = eButtonTextAlignment.Center;
				button.ColorTable = eButtonColor.OrangeWithBackground;
				button.Style = eDotNetBarStyle.StyleManagerControlled;
				button.Image = color.Logo;
				button.Tag = color;
				button.Checked = color.Name.Equals(selectedColor.Name);
				button.Click += buttonColor_Click;
				button.CheckedChanged += colorButton_CheckedChanged;
				xtraScrollableControlColors.Controls.Add(button);
				button.Location = new Point(20, topPosition);
				topPosition += (button.Height + 40);
			}
			buttonXUseSlideMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;

			_allowTosave = true;
			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			advBandedGridViewSchedule.CloseEditor();
			digitalInfoControl.SaveData();

			var checkedColorItem = xtraScrollableControlColors.Controls.OfType<ButtonX>().FirstOrDefault(b => b.Checked);
			MediaMetaData.Instance.SettingsManager.SelectedColor = checkedColorItem != null ? ((ColorFolder)checkedColorItem.Tag).Name : String.Empty;
			MediaMetaData.Instance.SettingsManager.UseSlideMaster = buttonXUseSlideMaster.Checked;
			MediaMetaData.Instance.SettingsManager.SaveSettings();

			Controller.Instance.SaveSchedule(_localSchedule, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void ScheduleSection_DataChanged(object sender, EventArgs e)
		{
			UpdateTotalsValues();
		}

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control != Controller.Instance.HomeBusinessName
				&& control != Controller.Instance.HomeClientType
				&& control != Controller.Instance.HomeDecisionMaker
				&& control != Controller.Instance.HomeFlightDatesEnd
				&& control != Controller.Instance.HomeFlightDatesStart
				&& control != Controller.Instance.HomePresentationDate)
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			Focus();
			advBandedGridViewSchedule.CloseEditor();
			advBandedGridViewSchedule.FocusedColumn = null;
		}

		#endregion

		private void ScheduleControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(Controller.Instance.Ribbon);
			AssignCloseActiveEditorsonOutSideClick(pnBottom);
			AssignCloseActiveEditorsonOutSideClick(pnTop);
		}

		#region Ribbon Operations Events

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(_helpKey);
		}

		public void Save_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule())
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
			}
		}

		public void AddProgram_Click(object sender, EventArgs e)
		{
			ScheduleSection.AddProgram();
			UpdateGrid(false);
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());
			if (advBandedGridViewSchedule.RowCount > 0)
				advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;
			SettingsNotSaved = true;
		}

		public void DeleteProgram_Click(object sender, EventArgs e)
		{
			var selectedProgramRow = advBandedGridViewSchedule.GetFocusedDataRow();
			if (selectedProgramRow == null) return;
			var selectedProgramIndex = selectedProgramRow[0].ToString();
			if (Utilities.Instance.ShowWarningQuestion(String.Format("Delete Line ID {0}?", selectedProgramIndex)) != DialogResult.Yes) return;
			ScheduleSection.DeleteProgram(advBandedGridViewSchedule.GetDataSourceRowIndex(advBandedGridViewSchedule.FocusedRowHandle));
			UpdateGrid(false);
			UpdateTotalsValues();
			UpdateOutputStatus(ScheduleSection.Programs.Any());
			SettingsNotSaved = true;
		}

		public void Options_CheckedChaged(object sender, EventArgs e)
		{
			splitContainerControl.PanelVisibility = OptionsButton.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
		}
		#endregion

		#region Options Events

		private void button_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowTosave) return;
			ScheduleSection.ShowRate = buttonXRate.Checked;
			ScheduleSection.ShowRating = buttonXRating.Checked;
			ScheduleSection.ShowCost = buttonXCost.Checked;
			ScheduleSection.ShowCPP = buttonXCPP.Checked;
			ScheduleSection.ShowDay = buttonXDay.Checked;
			ScheduleSection.ShowDaypart = buttonXDaypart.Checked;
			ScheduleSection.ShowGRP = buttonXGRP.Checked;
			ScheduleSection.ShowLenght = buttonXLength.Checked;
			ScheduleSection.ShowStation = buttonXStation.Checked;
			ScheduleSection.ShowTime = buttonXTime.Checked;


			ScheduleSection.ShowSpots = buttonXSpots.Checked;
			checkEditEmptySports.Enabled = ScheduleSection.ShowSpots;
			ScheduleSection.ShowEmptySpots = !checkEditEmptySports.Checked;

			ScheduleSection.ShowTotalPeriods = buttonXTotalPeriods.Checked;
			ScheduleSection.ShowTotalSpots = buttonXTotalSpots.Checked;
			ScheduleSection.ShowTotalGRP = buttonXTotalGRP.Checked;
			ScheduleSection.ShowTotalCPP = buttonXTotalCPP.Checked;
			ScheduleSection.ShowAverageRate = buttonXAvgRate.Checked;
			ScheduleSection.ShowTotalRate = buttonXTotalCost.Checked;
			ScheduleSection.ShowNetRate = buttonXNetRate.Checked;
			ScheduleSection.ShowDiscount = buttonXDiscount.Checked;

			bandedGridColumnRate.Visible = ScheduleSection.ShowRate;
			bandedGridColumnRating.Visible = ScheduleSection.ShowRating;
			gridBandRate.Visible = ScheduleSection.ShowRate | ScheduleSection.ShowRating;
			if (ScheduleSection.ShowRate)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
			if (ScheduleSection.ShowRating)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);

			bandedGridColumnTotalSpots.Visible = ScheduleSection.ShowTotalSpots;
			bandedGridColumnGRP.Visible = ScheduleSection.ShowGRP;
			bandedGridColumnCost.Visible = ScheduleSection.ShowCost;
			bandedGridColumnCPP.Visible = ScheduleSection.ShowCPP;

			bandedGridColumnLength.Visible = ScheduleSection.ShowLenght;
			gridBandLength.Visible = ScheduleSection.ShowLenght;

			bandedGridColumnDay.Visible = ScheduleSection.ShowDay;
			bandedGridColumnTime.Visible = ScheduleSection.ShowTime;
			gridBandDate.Visible = ScheduleSection.ShowDay | ScheduleSection.ShowTime;
			if (ScheduleSection.ShowDay)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
			if (ScheduleSection.ShowTime)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

			bandedGridColumnStation.Visible = ScheduleSection.ShowStation;
			bandedGridColumnDaypart.Visible = ScheduleSection.ShowDaypart;
			gridBandSpots.Visible = _spotColumns.Count > 0 && ScheduleSection.ShowSpots;

			UpdateTotalsVisibility();
			UpdateTotalsValues();

			UpdateColumnsAccordingScreenSize();

			SettingsNotSaved = true;
		}

		private void buttonXUseSlideMaster_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowTosave) return;
			SettingsNotSaved = true;
		}

		private void buttonColor_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button.Checked) return;
			foreach (var colorButton in xtraScrollableControlColors.Controls.OfType<ButtonX>())
				colorButton.Checked = false;
			button.Checked = true;
		}

		private void colorButton_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowTosave) return;
			SettingsNotSaved = true;
		}

		private void pbOptionsHelp_Click(object sender, EventArgs e)
		{
			if (xtraTabControlOptions.SelectedTabPage == xtraTabPageOptionsLine)
				BusinessWrapper.Instance.HelpManager.OpenHelpLink(String.Format("nav{0}", MediaMetaData.Instance.DataTypeString));
			else if (xtraTabControlOptions.SelectedTabPage == xtraTabPageOptionsSecurity)
				BusinessWrapper.Instance.HelpManager.OpenHelpLink("navsecurity");
			else if (xtraTabControlOptions.SelectedTabPage == xtraTabPageOptionsStyle)
				BusinessWrapper.Instance.HelpManager.OpenHelpLink("navstyle");
			else if (xtraTabControlOptions.SelectedTabPage == xtraTabPageOptionsTotals)
				BusinessWrapper.Instance.HelpManager.OpenHelpLink("navinfo");
		}
		#endregion

		#region Grid Events

		private void advBandedGridViewSchedule_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridViewSchedule.CloseEditor();
			advBandedGridViewSchedule.UpdateCurrentRow();
			SettingsNotSaved = true;
		}

		private void advBandedGridViewSchedule_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
		{
			if (_spotColumns.Count > 0)
			{
				e.Painter.DrawObject(e.Info);
				var view = sender as AdvBandedGridView;
				var viewInfo = view.GetViewInfo() as AdvBandedGridViewInfo;
				var column = bandedGridColumnName;
				if (ScheduleSection.ShowRate)
					column = bandedGridColumnRate;
				else if (ScheduleSection.ShowRating)
					column = bandedGridColumnRating;
				else if (ScheduleSection.ShowLenght)
					column = bandedGridColumnLength;
				else if (ScheduleSection.ShowDay)
					column = bandedGridColumnDay;
				else if (ScheduleSection.ShowTime)
					column = bandedGridColumnTime;
				var x = viewInfo.ColumnsInfo[column].Bounds.X;
				var width = viewInfo.ColumnsInfo[column].Bounds.Width;
				const string spotTotalTitle = "Totals: ";
				var size = e.Appearance.CalcTextSize(e.Cache, spotTotalTitle, 50);
				var textWidth = Convert.ToInt32(size.Width) + 1;
				var textRect = new Rectangle(x + width - 50, e.Bounds.Y, textWidth, e.Bounds.Height);
				e.Appearance.DrawString(e.Cache, spotTotalTitle, textRect);
				e.Handled = true;
			}
		}

		private void advBandedGridViewSchedule_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
		{
			if (e.Column != bandedGridColumnName || advBandedGridViewSchedule.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var station = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnStation) as String;
			var daypart = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) as String;
			gridColumnProgramSourceStation.Visible = String.IsNullOrEmpty(station);
			gridColumnProgramSourceDaypart.Visible = String.IsNullOrEmpty(daypart);
			var daypartFromList = MediaMetaData.Instance.ListManager.Dayparts.FirstOrDefault(x => x.Code.Equals(daypart));
			gridColumnProgramSourceName.Caption = String.Format("Program ({0})", daypartFromList != null ? daypartFromList.Name : "All Programming");
			var dataSource = new List<SourceProgram>();
			if (!String.IsNullOrEmpty(station) && (!String.IsNullOrEmpty(daypart) || !ScheduleSection.ShowDaypart))
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => (x.Station.Equals(station) || String.IsNullOrEmpty(station)) && (!ScheduleSection.ShowDaypart || (x.Daypart.Equals(daypart) || String.IsNullOrEmpty(daypart)))));
			else
				dataSource.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.OrderBy(sp => sp.Daypart));
			if (dataSource.Any())
			{
				gridViewProgramSource.DoubleClick -= gridViewProgramSource_DoubleClick;
				gridControlProgramSource.DataSource = dataSource;
				gridViewProgramSource.DoubleClick += gridViewProgramSource_DoubleClick;
				e.RepositoryItem = repositoryItemPopupContainerEditProgram;
			}
			else
				e.RepositoryItem = repositoryItemTextEditProgram;
		}

		private void gridViewProgramSource_DoubleClick(object sender, EventArgs e)
		{
			popupContainerControlProgramSource.OwnerEdit.ClosePopup();
		}

		private void repositoryItemPopupContainerEditProgram_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.CloseMode != PopupCloseMode.Normal) return;
			if (gridViewProgramSource.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				var program = MediaMetaData.Instance.ListManager.SourcePrograms.FirstOrDefault(x => x.Id.Equals(gridViewProgramSource.GetRowCellValue(gridViewProgramSource.FocusedRowHandle, gridColumnProgramSourceId).ToString()));
				if (program != null)
				{
					advBandedGridViewSchedule.CellValueChanged -= advBandedGridViewSchedule_CellValueChanged;
					e.Value = program.Name;
					if (advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) == null || string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart).ToString()))
						advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart, program.Daypart);
					advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDay, program.Day);
					advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnTime, program.Time);
					if (string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength).ToString()))
						advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength, MediaMetaData.Instance.ListManager.Lengths.FirstOrDefault());
					if (_localSchedule.ImportDemo && _localSchedule.UseDemo)
					{
						var demo = program.Demos.FirstOrDefault(d => d.Name.Equals(_localSchedule.Demo) && d.Source.Equals(_localSchedule.Source) && d.DemoType == _localSchedule.DemoType);
						if (demo != null)
							advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnRating, demo.Value);
					}
					advBandedGridViewSchedule.CellValueChanged += advBandedGridViewSchedule_CellValueChanged;
				}
			}
			e.AcceptValue = true;
		}

		private void repositoryItemPopupContainerEditProgram_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridViewSchedule.CloseEditor();
		}

		private void advBandedGridViewSchedule_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			var hInfo = view.CalcHitInfo(e.Location);
			if (hInfo.HitTest != BandedGridHitTest.RowCell)
				CloseActiveEditorsonOutSideClick(null, null);
		}

		#endregion

		#region Output Staff

		public bool ShowDigitalLegendOnlyFirstSlide
		{
			get { return ScheduleSection.DigitalLegend.OutputOnlyOnce; }
		}

		public string DigitalLegend
		{
			get
			{
				if (!ScheduleSection.DigitalLegend.Enabled) return String.Empty;
				var requestOptions = ScheduleSection.DigitalLegend.RequestOptions;
				if (!ScheduleSection.DigitalLegend.AllowEdit)
				{
					requestOptions.Separator = " ";
					return String.Format("Digital Product Info: {0}{1}{2}", _localSchedule.GetDigitalInfo(requestOptions), requestOptions.Separator, ScheduleSection.DigitalLegend.GetAdditionalData(" "));
				}
				if (!String.IsNullOrEmpty(ScheduleSection.DigitalLegend.Info))
					return String.Format("Digital Product Info: {0}{1}{2}", ScheduleSection.DigitalLegend.Info, requestOptions.Separator, ScheduleSection.DigitalLegend.GetAdditionalData(" "));
				return String.Empty;
			}
		}

		public Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.Themes.FirstOrDefault(t => t.Name.Equals(_localSchedule.ThemeName) || String.IsNullOrEmpty(_localSchedule.ThemeName)); }
		}

		private IEnumerable<OutputScheduleGridBased> PrepareOutputTableBased()
		{
			var outputPages = new List<OutputScheduleGridBased>();

			if (_localSchedule == null) return outputPages;
			var programsPerSlide = 12;
			programsPerSlide = ScheduleSection.Programs.Count > programsPerSlide ? programsPerSlide : ScheduleSection.Programs.Count;

			var totalSpotsCount = 0;
			if (buttonXSpots.Checked)
			{
				var defaultProgram = ScheduleSection.Programs.FirstOrDefault();
				if (defaultProgram != null)
					totalSpotsCount = ScheduleSection.ShowEmptySpots ? defaultProgram.Spots.Count : defaultProgram.SpotsNotEmpty.Length;
			}
			var spotsPerSlide = 26;
			spotsPerSlide = totalSpotsCount == 0 || totalSpotsCount > spotsPerSlide ? spotsPerSlide : totalSpotsCount;
			var spotsIteratorLimit = totalSpotsCount == 0 ? 1 : totalSpotsCount;
			for (var i = 0; i < ScheduleSection.Programs.Count; i += programsPerSlide)
			{
				for (var k = 0; k < spotsIteratorLimit; k += spotsPerSlide)
				{
					var outputPage = new OutputScheduleGridBased(ScheduleSection);

					outputPage.Advertiser = _localSchedule.BusinessName;
					outputPage.DecisionMaker = _localSchedule.DecisionMaker;
					outputPage.Demo = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);
					outputPage.DigitalInfo = !ScheduleSection.DigitalLegend.OutputOnlyOnce ||
								((i + programsPerSlide) >= ScheduleSection.Programs.Count &&
									(k + spotsPerSlide) >= spotsIteratorLimit) ?
						DigitalLegend :
						String.Empty;
					outputPage.Color = MediaMetaData.Instance.SettingsManager.SelectedColor;

					outputPage.ProgramsPerSlide = programsPerSlide;
					outputPage.SpotsPerSlide = totalSpotsCount > 0 ? spotsPerSlide : 0;
					outputPage.ShowRates = buttonXRate.Checked;
					outputPage.ShowRating = buttonXRating.Checked;
					outputPage.ShowCPP = buttonXCPP.Checked;
					outputPage.ShowGRP = buttonXGRP.Checked;
					outputPage.ShowCost = buttonXCost.Checked;
					outputPage.ShowStation = buttonXStation.Checked;
					outputPage.ShowDay = buttonXDay.Checked;
					outputPage.ShowTime = buttonXTime.Checked;
					outputPage.ShowLength = buttonXLength.Checked;
					outputPage.ShowTotalSpots = buttonXTotalSpots.Checked;
					outputPage.ShowSpots = buttonXSpots.Checked;

					#region Set Totals

					if (buttonXTotalPeriods.Checked)
						outputPage.Totals.Add(laTotalPeriodsTitle.Text, laTotalPeriodsValue.Text);
					if (buttonXTotalSpots.Checked)
						outputPage.Totals.Add(laTotalSpotsTitle.Text, laTotalSpotsValue.Text);
					if (buttonXTotalGRP.Checked)
						outputPage.Totals.Add(laTotalGRPTitle.Text, laTotalGRPValue.Text);
					if (buttonXTotalCPP.Checked)
						outputPage.Totals.Add(laTotalCPPTitle.Text, laTotalCPPValue.Text);
					if (buttonXAvgRate.Checked)
						outputPage.Totals.Add(laAvgRateTitle.Text, laAvgRateValue.Text);
					if (buttonXTotalCost.Checked)
						outputPage.Totals.Add(laTotalCostTitle.Text, laTotalCostValue.Text);
					if (buttonXNetRate.Checked)
						outputPage.Totals.Add(laNetRateTitle.Text, laNetRateValue.Text);
					if (buttonXDiscount.Checked)
						outputPage.Totals.Add(laAgencyDiscountTitle.Text, laAgencyDiscountValue.Text);

					#endregion

					#region Set OutputProgram Values

					for (var j = 0; j < programsPerSlide; j++)
					{
						if ((i + j) < ScheduleSection.Programs.Count)
						{
							var program = ScheduleSection.Programs[i + j];
							var outputProgram = new OutputProgramGridBased(outputPage);
							outputProgram.Name = program.Name + (buttonXDaypart.Checked ? ("-" + program.Daypart) : string.Empty);
							outputProgram.LineID = program.Index.ToString("00");
							outputProgram.Station = program.Station;
							outputProgram.Days = program.Day;
							outputProgram.Time = program.Time;
							outputProgram.Length = program.Length;
							outputProgram.Rate = buttonXRate.Checked && program.Rate.HasValue ? program.Rate.Value.ToString("$#,##0") : string.Empty;
							outputProgram.Rating = buttonXRating.Checked && program.Rating.HasValue ? program.Rating.Value.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : string.Empty;
							outputProgram.CPP = buttonXCPP.Checked ? program.CPP.ToString("$#,###.00") : string.Empty;
							outputProgram.GRP = buttonXGRP.Checked ? program.GRP.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0") : string.Empty;
							outputProgram.TotalRate = buttonXCost.Checked ? program.Cost.ToString("$#,##0") : String.Empty;
							outputProgram.TotalSpots = program.TotalSpots.ToString("#,##0");

							#region Set Spots Values

							var spotsNotEmpy = program.SpotsNotEmpty;
							for (var l = 0; l < spotsPerSlide; l++)
							{
								if ((k + l) < totalSpotsCount)
								{
									var value = !program.Parent.ShowEmptySpots ?
										(spotsNotEmpy[k + l].Count > 0 ? spotsNotEmpy[k + l].Count.ToString() : "-") :
										(program.Spots[k + l].Count > 0 ? program.Spots[k + l].Count.ToString() : "-");
									outputProgram.Spots.Add(value);
								}
								else
									break;
								Application.DoEvents();
							}

							#endregion

							outputPage.Programs.Add(outputProgram);
							Application.DoEvents();
						}
						else
							break;
					}

					#endregion

					#region Set Total Values

					var defaultProgram = ScheduleSection.Programs.FirstOrDefault();
					if (defaultProgram != null)
					{
						var defaultSpotsNotEmpy = defaultProgram.SpotsNotEmpty;
						for (var l = 0; l < spotsPerSlide; l++)
						{
							if ((k + l) < totalSpotsCount)
							{
								var outputTotalSpot = new OutputTotalSpot();
								outputTotalSpot.Day = !defaultProgram.Parent.ShowEmptySpots ? (defaultSpotsNotEmpy[k + l].Date.Day.ToString()) : (defaultProgram.Spots[k + l].Date.Day.ToString());
								outputTotalSpot.Month = Spot.GetMonthAbbreviation(!defaultProgram.Parent.ShowEmptySpots ? defaultSpotsNotEmpy[k + l].Date.Month : defaultProgram.Spots[k + l].Date.Month);

								int sum;
								if (!defaultProgram.Parent.ShowEmptySpots)
									sum = defaultProgram.Parent.Programs.Select(x => x.SpotsNotEmpty.FirstOrDefault(y => y.Date.Equals(defaultSpotsNotEmpy[k + l].Date))).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
								else
									sum = defaultProgram.Parent.Programs.Select(x => x.Spots.FirstOrDefault(y => y.Date.Equals(defaultProgram.Spots[k + l].Date))).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
								outputTotalSpot.Value = sum > 0 ? sum.ToString() : "-";
								outputPage.TotalSpots.Add(outputTotalSpot);
							}
							else
								break;
							Application.DoEvents();
						}
					}

					outputPage.TotalCost = ScheduleSection.TotalCost.ToString("$#,##0");
					outputPage.TotalSpot = ScheduleSection.TotalSpots.ToString("#,##0");
					outputPage.TotalCPP = ScheduleSection.TotalCPP.ToString("$#,###.00");
					outputPage.TotalGRP = ScheduleSection.TotalGRP.ToString(_localSchedule.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");

					#endregion

					outputPage.PopulateScheduleReplacementsList();

					outputPages.Add(outputPage);
				}
			}

			return outputPages;
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_localSchedule == null) return;
			if (!ScheduleSection.Programs.Any()) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					IEnumerable<OutputScheduleGridBased> outputPages = null;
					var thread = new Thread(() => Invoke((MethodInvoker)delegate { outputPages = PrepareOutputTableBased(); }));
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					MediaSchedulePowerPointHelper.Instance.AppendOneSheetTableBased(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
					formProgress.Close();
				});
			}
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_localSchedule == null) return;
			if (!ScheduleSection.Programs.Any()) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				formProgress.Show();
				formProgress.Show();
				IEnumerable<OutputScheduleGridBased> outputPages = null;
				var thread = new Thread(() => Invoke((MethodInvoker)delegate { outputPages = PrepareOutputTableBased(); }));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				MediaSchedulePowerPointHelper.Instance.PrepareOneSheetEmailTableBased(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview(Controller.Instance.FormMain, MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
				{
					formPreview.Text = "Preview Schedule";
					formPreview.PresentationFile = tempFileName;
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					var previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (previewResult != DialogResult.OK)
						Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				}
			}
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_localSchedule == null) return;
			if (!ScheduleSection.Programs.Any()) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Email...";
				formProgress.TopMost = true;
				var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				formProgress.Show();
				formProgress.Show();
				IEnumerable<OutputScheduleGridBased> outputPages = null;
				var thread = new Thread(() => Invoke((MethodInvoker)delegate { outputPages = PrepareOutputTableBased(); }));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				MediaSchedulePowerPointHelper.Instance.PrepareOneSheetEmailTableBased(tempFileName, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formEmail = new FormEmail(Controller.Instance.FormMain, MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
				{
					formEmail.Text = "Email this Schedule";
					formEmail.PresentationFile = tempFileName;
					RegistryHelper.MainFormHandle = formEmail.Handle;
					RegistryHelper.MaximizeMainForm = false;
					formEmail.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				}
			}
		}

		#endregion

		#region Picture Box Clicks Habdlers

		/// <summary>
		///     Buttonize the PictureBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}

		#endregion
	}
}