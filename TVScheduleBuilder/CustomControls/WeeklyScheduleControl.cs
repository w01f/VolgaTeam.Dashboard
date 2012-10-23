using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace TVScheduleBuilder.CustomControls
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class WeeklyScheduleControl : UserControl
	{
		private static WeeklyScheduleControl _instance = null;
		private bool _allowTosave = false;
		private BusinessClasses.Schedule _localSchedule;
		private List<BandedGridColumn> _spotColumns = new List<BandedGridColumn>();

		public bool SettingsNotSaved { get; set; }

		private WeeklyScheduleControl()
		{
			InitializeComponent();
			this.Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				Font font = new Font(laTotalPeriodsTitle.Font.FontFamily, laTotalPeriodsTitle.Font.Size - 2, laTotalPeriodsTitle.Font.Style);
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
			repositoryItemComboBoxDays.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemComboBoxDays.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemComboBoxDays.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemComboBoxDayparts.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemComboBoxDayparts.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemComboBoxDayparts.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemComboBoxLengths.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemComboBoxLengths.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemComboBoxLengths.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemPopupContainerEditProgram.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemPopupContainerEditProgram.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemPopupContainerEditProgram.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemComboBoxStations.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemComboBoxStations.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemComboBoxStations.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemComboBoxTimes.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemComboBoxTimes.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemComboBoxTimes.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemSpinEdit000s.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemSpinEdit000s.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemSpinEdit000s.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemSpinEditRate.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemSpinEditRate.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemSpinEditRate.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemSpinEditRating.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemSpinEditRating.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemSpinEditRating.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			repositoryItemSpinEditSpot.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			repositoryItemSpinEditSpot.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			repositoryItemSpinEditSpot.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public static WeeklyScheduleControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new WeeklyScheduleControl();
				return _instance;
			}
		}

		public static void RemoveInstance()
		{
			try
			{
				_instance.Dispose();
			}
			catch
			{
			}
			finally
			{
				_instance = null;
			}
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (this.SettingsNotSaved)
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
			bandedGridColumnDay.FieldName = BusinessClasses.ScheduleSection.ProgramDayDataColumnName;
			bandedGridColumnDaypart.FieldName = BusinessClasses.ScheduleSection.ProgramDaypartDataColumnName;
			bandedGridColumnCPP.FieldName = BusinessClasses.ScheduleSection.ProgramCPPDataColumnName;
			bandedGridColumnCPP.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramTotalCPPDataColumnName;
			bandedGridColumnGRP.FieldName = BusinessClasses.ScheduleSection.ProgramGRPDataColumnName;
			bandedGridColumnGRP.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramGRPDataColumnName;
			bandedGridColumnIndex.FieldName = BusinessClasses.ScheduleSection.ProgramIndexDataColumnName;
			bandedGridColumnLength.FieldName = BusinessClasses.ScheduleSection.ProgramLengthDataColumnName;
			bandedGridColumnName.FieldName = BusinessClasses.ScheduleSection.ProgramNameDataColumnName;
			bandedGridColumnRate.FieldName = BusinessClasses.ScheduleSection.ProgramRateDataColumnName;
			bandedGridColumnRating.FieldName = BusinessClasses.ScheduleSection.ProgramRatingDataColumnName;
			bandedGridColumnStation.FieldName = BusinessClasses.ScheduleSection.ProgramStationDataColumnName;
			bandedGridColumnTime.FieldName = BusinessClasses.ScheduleSection.ProgramTimeDataColumnName;
			bandedGridColumnTotalSpots.FieldName = BusinessClasses.ScheduleSection.ProgramTotalSpotDataColumnName;
			bandedGridColumnTotalSpots.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramTotalSpotDataColumnName;
			bandedGridColumnCost.FieldName = BusinessClasses.ScheduleSection.ProgramCostDataColumnName;
			bandedGridColumnCost.SummaryItem.FieldName = BusinessClasses.ScheduleSection.ProgramCostDataColumnName;
			_localSchedule.WeeklySchedule.GenerateDataSource();
			if (!quickLoad)
				BuildSpotColumns();
			if (_localSchedule.WeeklySchedule.Programs.Count > 0)
			{
				gridControlSchedule.DataSource = _localSchedule.WeeklySchedule.DataSource;
				gridControlSchedule.DataMember = BusinessClasses.ScheduleSection.ProgramDataTableName;
			}
			advBandedGridViewSchedule.EndUpdate();
			if (focussedRow >= 0 && focussedRow < advBandedGridViewSchedule.RowCount)
				advBandedGridViewSchedule.FocusedRowHandle = focussedRow;
		}

		private void UpdateTotalsVisibility()
		{
			pnTotalCPP.Visible = _localSchedule.WeeklySchedule.ShowTotalCPP;
			pnTotalCPP.SendToBack();
			pnTotalGRP.Visible = _localSchedule.WeeklySchedule.ShowTotalGRP;
			pnTotalGRP.SendToBack();
			pnTotalSpots.Visible = _localSchedule.WeeklySchedule.ShowTotalSpots;
			pnTotalSpots.SendToBack();
			pnTotalPeriods.Visible = _localSchedule.WeeklySchedule.ShowTotalPeriods;
			pnTotalPeriods.SendToBack();
			pnAvgRate.Visible = _localSchedule.WeeklySchedule.ShowAverageRate;
			pnAvgRate.BringToFront();
			pnTotalCost.Visible = _localSchedule.WeeklySchedule.ShowTotalRate;
			pnTotalCost.BringToFront();
			pnNetRate.Visible = _localSchedule.WeeklySchedule.ShowNetRate;
			pnNetRate.BringToFront();
			pnAgencyDiscount.Visible = _localSchedule.WeeklySchedule.ShowDiscount;
			pnAgencyDiscount.BringToFront();
		}

		private void UpdateTotalsValues()
		{
			laTotalPeriodsValue.Text = _localSchedule.WeeklySchedule.TotalPeriods.ToString("#,##0");
			laTotalSpotsValue.Text = _localSchedule.WeeklySchedule.TotalSpots.ToString("#,##0");
			laTotalGRPValue.Text = _localSchedule.WeeklySchedule.TotalGRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0");
			laTotalCPPValue.Text = _localSchedule.WeeklySchedule.TotalCPP.ToString("$#,###.00");
			laAvgRateValue.Text = _localSchedule.WeeklySchedule.AvgRate.ToString("$#,###.00");
			laTotalCostValue.Text = _localSchedule.WeeklySchedule.TotalCost.ToString("$#,##0");
			laNetRateValue.Text = _localSchedule.WeeklySchedule.NetRate.ToString("$#,###.00");
			laAgencyDiscountValue.Text = _localSchedule.WeeklySchedule.Discount.ToString("$#,###.00");
		}

		private void BuildSpotColumns()
		{
			foreach (BandedGridColumn column in _spotColumns)
			{
				gridBandSpots.Columns.Remove(column);
				advBandedGridViewSchedule.Columns.Remove(column);
			}

			_spotColumns.Clear();

			gridBandSpots.Visible = false;

			foreach (DataColumn column in _localSchedule.WeeklySchedule.DataSource.Tables[BusinessClasses.ScheduleSection.ProgramDataTableName].Columns)
			{
				if (column.ColumnName.Contains(BusinessClasses.ScheduleSection.ProgramSpotDataColumnNamePrefix))
				{
					BandedGridColumn bandedGridColumn = new BandedGridColumn();
					bandedGridColumn.AppearanceCell.Options.UseTextOptions = true;
					bandedGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
					bandedGridColumn.AutoFillDown = true;
					bandedGridColumn.Caption = column.Caption;
					bandedGridColumn.ColumnEdit = this.repositoryItemSpinEditSpot;
					bandedGridColumn.FieldName = column.ColumnName;
					bandedGridColumn.OptionsColumn.FixedWidth = true;
					bandedGridColumn.RowCount = 2;
					bandedGridColumn.Width = 45;
					bandedGridColumn.Visible = true;
					bandedGridColumn.SummaryItem.FieldName = column.ColumnName;
					bandedGridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
					_spotColumns.Add(bandedGridColumn);
					advBandedGridViewSchedule.Columns.Add(bandedGridColumn);
					gridBandSpots.Columns.Add(bandedGridColumn);
				}
			}
			gridBandSpots.Visible = _spotColumns.Count > 0 && _localSchedule.WeeklySchedule.ShowSpots;
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowTosave = false;

			_localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
			_localSchedule.WeeklySchedule.DataChanged += new EventHandler<EventArgs>(WeeklySchedule_DataChanged);

			laAdvertiser.Text = _localSchedule.BusinessName;
			laDemoName.Text = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);
			laScheduleWindow.Text = _localSchedule.FlightDateStart.HasValue && _localSchedule.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { _localSchedule.FlightDateStart.Value.ToString("MM/dd/yy"), _localSchedule.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
			laScheduleName.Text = _localSchedule.Name;
			laDemoName.Visible = !string.IsNullOrEmpty(_localSchedule.Demo);
			laDemoName.SendToBack();
			laAdvertiser.SendToBack();

			FormMain.Instance.buttonItemWeeklyScheduleRating.Enabled = _localSchedule.UseDemo;
			FormMain.Instance.buttonItemWeeklyScheduleCPP.Enabled = _localSchedule.UseDemo;
			FormMain.Instance.buttonItemWeeklyScheduleGRP.Enabled = _localSchedule.UseDemo;
			FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Enabled = _localSchedule.UseDemo;
			FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Enabled = _localSchedule.UseDemo;
			FormMain.Instance.buttonItemWeeklyScheduleRating.Text = _localSchedule.RatingAsCPP ? "Rtg" : "(000s)";
			bandedGridColumnRating.Caption = _localSchedule.RatingAsCPP ? "Rtg" : "(000s)";
			bandedGridColumnRating.ColumnEdit = _localSchedule.RatingAsCPP ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			FormMain.Instance.buttonItemWeeklyScheduleCPP.Text = _localSchedule.RatingAsCPP ? "CPP" : "CPM";
			FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Text = _localSchedule.RatingAsCPP ? "Overall CPP" : "Overall CPM";
			laTotalCPPTitle.Text = _localSchedule.RatingAsCPP ? "Overall CPP:" : "Overall CPM:";
			bandedGridColumnCPP.Caption = _localSchedule.RatingAsCPP ? "CPP" : "CPM";
			FormMain.Instance.buttonItemWeeklyScheduleGRP.Text = _localSchedule.RatingAsCPP ? "GRPs" : "Impr";
			FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Text = _localSchedule.RatingAsCPP ? "Total GRPs" : "Total Impr";
			laTotalGRPTitle.Text = _localSchedule.RatingAsCPP ? "Total GRPs:" : "Total Impr:";
			bandedGridColumnGRP.Caption = _localSchedule.RatingAsCPP ? "GRPs" : "Impr";
			bandedGridColumnGRP.ColumnEdit = _localSchedule.RatingAsCPP ? repositoryItemSpinEditRating : repositoryItemSpinEdit000s;
			bandedGridColumnGRP.SummaryItem.DisplayFormat = _localSchedule.RatingAsCPP ? "{0:n1}" : "{0:n0}";
			FormMain.Instance.ribbonBarWeeklyScheduleLineOptions.RecalcLayout();
			FormMain.Instance.ribbonBarWeeklyScheduleTotals.RecalcLayout();
			FormMain.Instance.ribbonPanelWeeklySchedule.PerformLayout();

			FormMain.Instance.buttonItemWeeklyScheduleRate.Checked = _localSchedule.WeeklySchedule.ShowRate;
			FormMain.Instance.buttonItemWeeklyScheduleRating.Checked = _localSchedule.WeeklySchedule.ShowRating;
			FormMain.Instance.buttonItemWeeklyScheduleCost.Checked = _localSchedule.WeeklySchedule.ShowCost;
			FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked = _localSchedule.WeeklySchedule.ShowCPP;
			FormMain.Instance.buttonItemWeeklyScheduleDay.Checked = _localSchedule.WeeklySchedule.ShowDay;
			FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked = _localSchedule.WeeklySchedule.ShowDaypart;
			FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked = _localSchedule.WeeklySchedule.ShowGRP;
			FormMain.Instance.buttonItemWeeklyScheduleLength.Checked = _localSchedule.WeeklySchedule.ShowLenght;
			FormMain.Instance.buttonItemWeeklyScheduleStation.Checked = _localSchedule.WeeklySchedule.ShowStation;
			FormMain.Instance.buttonItemWeeklyScheduleTime.Checked = _localSchedule.WeeklySchedule.ShowTime;
			FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked = _localSchedule.WeeklySchedule.ShowSpots;
			FormMain.Instance.buttonItemWeeklyScheduleEmptySpots.Enabled = _localSchedule.WeeklySchedule.ShowSpots;
			FormMain.Instance.buttonItemWeeklyScheduleEmptySpots.Checked = _localSchedule.WeeklySchedule.ShowEmptySpots;

			FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked = _localSchedule.WeeklySchedule.ShowTotalPeriods;
			FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked = _localSchedule.WeeklySchedule.ShowTotalSpots;
			FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked = _localSchedule.WeeklySchedule.ShowTotalGRP;
			FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked = _localSchedule.WeeklySchedule.ShowTotalCPP;
			FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked = _localSchedule.WeeklySchedule.ShowAverageRate;
			FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked = _localSchedule.WeeklySchedule.ShowTotalRate;
			FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked = _localSchedule.WeeklySchedule.ShowNetRate;
			FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked = _localSchedule.WeeklySchedule.ShowDiscount;

			bandedGridColumnRate.Visible = _localSchedule.WeeklySchedule.ShowRate;
			bandedGridColumnRating.Visible = _localSchedule.WeeklySchedule.ShowRating;
			gridBandRate.Visible = _localSchedule.WeeklySchedule.ShowRate | _localSchedule.WeeklySchedule.ShowRating;
			if (_localSchedule.WeeklySchedule.ShowRate)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
			if (_localSchedule.WeeklySchedule.ShowRating)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);


			bandedGridColumnCPP.Visible = _localSchedule.WeeklySchedule.ShowCPP;
			bandedGridColumnGRP.Visible = _localSchedule.WeeklySchedule.ShowGRP;
			bandedGridColumnCost.Visible = _localSchedule.WeeklySchedule.ShowCost;

			bandedGridColumnLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;
			gridBandLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;

			bandedGridColumnDay.Visible = _localSchedule.WeeklySchedule.ShowDay;
			bandedGridColumnTime.Visible = _localSchedule.WeeklySchedule.ShowTime;
			gridBandDate.Visible = _localSchedule.WeeklySchedule.ShowDay | _localSchedule.WeeklySchedule.ShowTime;
			if (_localSchedule.WeeklySchedule.ShowDay)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
			if (_localSchedule.WeeklySchedule.ShowTime)
				advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

			bandedGridColumnStation.Visible = _localSchedule.WeeklySchedule.ShowStation;
			bandedGridColumnDaypart.Visible = _localSchedule.WeeklySchedule.ShowDaypart;
			gridBandSpots.Visible = _spotColumns.Count > 0 && _localSchedule.WeeklySchedule.ShowSpots;


			repositoryItemComboBoxStations.Items.Clear();
			repositoryItemComboBoxStations.Items.AddRange(_localSchedule.Stations.Where(x => x.Available).Select(x => x.Name).ToArray());
			repositoryItemComboBoxDayparts.Items.Clear();
			repositoryItemComboBoxDayparts.Items.AddRange(_localSchedule.Dayparts.Where(x => x.Available).Select(x => x.Code).ToArray());


			if (!quickLoad)
			{
				repositoryItemComboBoxLengths.Items.Clear();
				repositoryItemComboBoxLengths.Items.AddRange(BusinessClasses.ListManager.Instance.Lengths);
				repositoryItemComboBoxDays.Items.Clear();
				repositoryItemComboBoxDays.Items.AddRange(BusinessClasses.ListManager.Instance.Days);
				repositoryItemComboBoxTimes.Items.Clear();
				repositoryItemComboBoxTimes.Items.AddRange(BusinessClasses.ListManager.Instance.Times);

				UpdateTotalsVisibility();
			}

			UpdateGrid(quickLoad);
			UpdateTotalsValues();

			_allowTosave = true;
			this.SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			advBandedGridViewSchedule.CloseEditor();
			BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, true, this);
			LoadSchedule(true);
			laScheduleName.Text = _localSchedule.Name;
			this.SettingsNotSaved = false;
			return true;
		}

		public void WeeklySchedule_DataChanged(object sender, EventArgs e)
		{
			UpdateTotalsValues();
		}

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control != FormMain.Instance.comboBoxEditBusinessName
				&& control != FormMain.Instance.comboBoxEditClientType
				&& control != FormMain.Instance.comboBoxEditDecisionMaker
				&& control != FormMain.Instance.dateEditFlightDatesEnd
				&& control != FormMain.Instance.dateEditFlightDatesStart
				&& control != FormMain.Instance.dateEditPresentationDate)
			{
				control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			this.Focus();
			advBandedGridViewSchedule.CloseEditor();
			advBandedGridViewSchedule.FocusedColumn = null;
		}
		#endregion

		private void ScheduleControl_Load(object sender, EventArgs e)
		{
			AssignCloseActiveEditorsonOutSideClick(FormMain.Instance.ribbonControl);
			AssignCloseActiveEditorsonOutSideClick(pnBottom);
			AssignCloseActiveEditorsonOutSideClick(pnTop);
		}

		#region Ribbon Operations Events
		public void buttonItemScheduleHelp_Click(object sender, EventArgs e)
		{
			BusinessClasses.HelpManager.Instance.OpenHelpLink("week");
		}

		public void buttonItemScheduleSave_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				AppManager.ShowInformation("Schedule Saved");
		}

		public void buttonItemScheduleSaveAs_Click(object sender, EventArgs e)
		{
			using (ToolForms.FormNewSchedule from = new ToolForms.FormNewSchedule())
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SaveSchedule(from.ScheduleName))
							AppManager.ShowInformation("Schedule was saved");
					}
					else
					{
						AppManager.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		public void buttonItemScheduleAdd_Click(object sender, EventArgs e)
		{
			_localSchedule.WeeklySchedule.AddProgram();
			UpdateGrid(false);
			UpdateTotalsValues();
			if (advBandedGridViewSchedule.RowCount > 0)
				advBandedGridViewSchedule.FocusedRowHandle = advBandedGridViewSchedule.RowCount - 1;
			this.SettingsNotSaved = true;
		}

		public void buttonItemScheduleDelete_Click(object sender, EventArgs e)
		{
			_localSchedule.WeeklySchedule.DeleteProgram(advBandedGridViewSchedule.GetDataSourceRowIndex(advBandedGridViewSchedule.FocusedRowHandle));
			UpdateGrid(false);
			UpdateTotalsValues();
			this.SettingsNotSaved = true;
		}
		#endregion

		#region Toggle Switch Events
		public void button_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowTosave)
			{
				_localSchedule.WeeklySchedule.ShowRate = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked;
				_localSchedule.WeeklySchedule.ShowRating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked;
				_localSchedule.WeeklySchedule.ShowCost = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked;
				_localSchedule.WeeklySchedule.ShowCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked;
				_localSchedule.WeeklySchedule.ShowDay = FormMain.Instance.buttonItemWeeklyScheduleDay.Checked;
				_localSchedule.WeeklySchedule.ShowDaypart = FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked;
				_localSchedule.WeeklySchedule.ShowGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked;
				_localSchedule.WeeklySchedule.ShowLenght = FormMain.Instance.buttonItemWeeklyScheduleLength.Checked;
				_localSchedule.WeeklySchedule.ShowStation = FormMain.Instance.buttonItemWeeklyScheduleStation.Checked;
				_localSchedule.WeeklySchedule.ShowTime = FormMain.Instance.buttonItemWeeklyScheduleTime.Checked;


				_localSchedule.WeeklySchedule.ShowSpots = FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked;
				if (!_localSchedule.WeeklySchedule.ShowSpots)
				{
					_allowTosave = false;
					FormMain.Instance.buttonItemWeeklyScheduleEmptySpots.Checked = false;
					_allowTosave = true;
				}
				FormMain.Instance.buttonItemWeeklyScheduleEmptySpots.Enabled = _localSchedule.WeeklySchedule.ShowSpots;
				_localSchedule.WeeklySchedule.ShowEmptySpots = FormMain.Instance.buttonItemWeeklyScheduleEmptySpots.Checked;

				_localSchedule.WeeklySchedule.ShowTotalPeriods = FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked;
				_localSchedule.WeeklySchedule.ShowTotalSpots = FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked;
				_localSchedule.WeeklySchedule.ShowTotalGRP = FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked;
				_localSchedule.WeeklySchedule.ShowTotalCPP = FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked;
				_localSchedule.WeeklySchedule.ShowAverageRate = FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked;
				_localSchedule.WeeklySchedule.ShowTotalRate = FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked;
				_localSchedule.WeeklySchedule.ShowNetRate = FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked;
				_localSchedule.WeeklySchedule.ShowDiscount = FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked;

				bandedGridColumnRate.Visible = _localSchedule.WeeklySchedule.ShowRate;
				bandedGridColumnRating.Visible = _localSchedule.WeeklySchedule.ShowRating;
				gridBandRate.Visible = _localSchedule.WeeklySchedule.ShowRate | _localSchedule.WeeklySchedule.ShowRating;
				if (_localSchedule.WeeklySchedule.ShowRate)
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRate, 0, 0);
				if (_localSchedule.WeeklySchedule.ShowRating)
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnRating, 1, 0);

				bandedGridColumnCPP.Visible = _localSchedule.WeeklySchedule.ShowCPP;
				bandedGridColumnGRP.Visible = _localSchedule.WeeklySchedule.ShowGRP;
				bandedGridColumnCost.Visible = _localSchedule.WeeklySchedule.ShowCost;

				bandedGridColumnLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;
				gridBandLength.Visible = _localSchedule.WeeklySchedule.ShowLenght;

				bandedGridColumnDay.Visible = _localSchedule.WeeklySchedule.ShowDay;
				bandedGridColumnTime.Visible = _localSchedule.WeeklySchedule.ShowTime;
				gridBandDate.Visible = _localSchedule.WeeklySchedule.ShowDay | _localSchedule.WeeklySchedule.ShowTime;
				if (_localSchedule.WeeklySchedule.ShowDay)
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnDay, 0, 0);
				if (_localSchedule.WeeklySchedule.ShowTime)
					advBandedGridViewSchedule.SetColumnPosition(bandedGridColumnTime, 1, 0);

				bandedGridColumnStation.Visible = _localSchedule.WeeklySchedule.ShowStation;
				bandedGridColumnDaypart.Visible = _localSchedule.WeeklySchedule.ShowDaypart;
				gridBandSpots.Visible = _spotColumns.Count > 0 && _localSchedule.WeeklySchedule.ShowSpots;

				UpdateTotalsVisibility();
				UpdateTotalsValues();

				this.SettingsNotSaved = true;
			}
		}
		#endregion

		#region Grid Events
		private void advBandedGridViewSchedule_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			advBandedGridViewSchedule.CloseEditor();
			advBandedGridViewSchedule.UpdateCurrentRow();
			this.SettingsNotSaved = true;
		}

		private void advBandedGridViewSchedule_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
		{
			if (_spotColumns.Count > 0)
			{
				e.Painter.DrawObject(e.Info);
				DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view = sender as DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView;
				DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.AdvBandedGridViewInfo viewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.AdvBandedGridViewInfo;
				BandedGridColumn column = bandedGridColumnName;
				if (_localSchedule.WeeklySchedule.ShowRate)
					column = bandedGridColumnRate;
				else if (_localSchedule.WeeklySchedule.ShowRating)
					column = bandedGridColumnRating;
				else if (_localSchedule.WeeklySchedule.ShowLenght)
					column = bandedGridColumnLength;
				else if (_localSchedule.WeeklySchedule.ShowDay)
					column = bandedGridColumnDay;
				else if (_localSchedule.WeeklySchedule.ShowTime)
					column = bandedGridColumnTime;
				int x = viewInfo.ColumnsInfo[column].Bounds.X;
				int width = viewInfo.ColumnsInfo[column].Bounds.Width;
				string spotTotalTitle = "Totals: ";
				SizeF size = e.Appearance.CalcTextSize(e.Cache, spotTotalTitle, 50);
				int textWidth = Convert.ToInt32(size.Width) + 1;
				Rectangle textRect = new Rectangle(x + width - 50, e.Bounds.Y, textWidth, e.Bounds.Height);
				e.Appearance.DrawString(e.Cache, spotTotalTitle, textRect);
				e.Handled = true;
			}
		}

		private void advBandedGridViewSchedule_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (advBandedGridViewSchedule.FocusedColumn == bandedGridColumnName && advBandedGridViewSchedule.FocusedRowHandle >= 0)
			{
				object station = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnStation);
				object daypart = advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart);
				gridColumnProgramSourceStation.Visible = station == null || string.IsNullOrEmpty(station.ToString());
				gridColumnProgramSourceDaypart.Visible = daypart == null || string.IsNullOrEmpty(daypart.ToString());
				gridColumnProgramSourceName.Caption = string.Format("Program ({0})", daypart != null && !string.IsNullOrEmpty(daypart.ToString()) ? BusinessClasses.ListManager.Instance.Dayparts.FirstOrDefault(x => x.Code.Equals(daypart.ToString())).Name : "All Programming");
				List<BusinessClasses.SourceProgram> dataSource = new List<BusinessClasses.SourceProgram>();
				if (station != null && (daypart != null || !_localSchedule.WeeklySchedule.ShowDaypart))
				{
					dataSource.AddRange(BusinessClasses.ListManager.Instance.SourcePrograms.Where(x => (x.Station.Equals(station.ToString()) || string.IsNullOrEmpty(station.ToString())) && (!_localSchedule.WeeklySchedule.ShowDaypart || (x.Daypart.Equals(daypart.ToString()) || string.IsNullOrEmpty(daypart.ToString())))));
				}
				else
				{
					dataSource.AddRange(BusinessClasses.ListManager.Instance.SourcePrograms);
					dataSource.Sort((x, y) => BusinessClasses.ListManager.Instance.Dayparts.Select((n, i) => new { daypart = n, index = i }).FirstOrDefault(l => l.daypart.Equals(x.Daypart)).index.CompareTo(BusinessClasses.ListManager.Instance.Dayparts.Select((n, i) => new { daypart = n, index = i }).FirstOrDefault(l => l.daypart.Equals(y.Daypart)).index));
				}
				gridViewProgramSource.DoubleClick -= new EventHandler(gridViewProgramSource_DoubleClick);
				gridControlProgramSource.DataSource = dataSource;
				gridViewProgramSource.DoubleClick += new EventHandler(gridViewProgramSource_DoubleClick);
			}
		}

		void gridViewProgramSource_DoubleClick(object sender, EventArgs e)
		{
			popupContainerControlProgramSource.OwnerEdit.ClosePopup();
		}

		private void repositoryItemPopupContainerEditProgram_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
		{
			if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
			{
				if (gridViewProgramSource.FocusedRowHandle >= 0)
				{
					BusinessClasses.SourceProgram program = BusinessClasses.ListManager.Instance.SourcePrograms.Where(x => x.Id.Equals(gridViewProgramSource.GetRowCellValue(gridViewProgramSource.FocusedRowHandle, gridColumnProgramSourceId).ToString())).FirstOrDefault();
					if (program != null)
					{
						advBandedGridViewSchedule.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(advBandedGridViewSchedule_CellValueChanged);
						e.Value = program.Name;
						if (advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart) == null || string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart).ToString()))
							advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDaypart, program.Daypart);
						advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnDay, program.Day);
						advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnTime, program.Time);
						if (string.IsNullOrEmpty(advBandedGridViewSchedule.GetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength).ToString()))
							advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnLength, BusinessClasses.ListManager.Instance.Lengths.FirstOrDefault());
						if (_localSchedule.ImportDemo && _localSchedule.UseDemo)
						{
							BusinessClasses.Demo demo = program.Demos.Where(x => x.Name.Equals(_localSchedule.Demo)).FirstOrDefault();
							if (demo != null)
								advBandedGridViewSchedule.SetRowCellValue(advBandedGridViewSchedule.FocusedRowHandle, bandedGridColumnRating, demo.Value);
						}
						advBandedGridViewSchedule.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(advBandedGridViewSchedule_CellValueChanged);
					}
				}
				e.AcceptValue = true;
			}
		}

		private void repositoryItemPopupContainerEditProgram_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
		{
			advBandedGridViewSchedule.CloseEditor();
		}

		private void advBandedGridViewSchedule_MouseDown(object sender, MouseEventArgs e)
		{
			AdvBandedGridView view = sender as AdvBandedGridView;
			if (view != null)
			{
				DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo hInfo = view.CalcHitInfo(e.Location);
				if (hInfo.HitTest != DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitTest.RowCell)
					CloseActiveEditorsonOutSideClick(null, null);
			}
		}
		#endregion

		#region Output Staff
		private int GetEstimatedSlideNumber()
		{
			int result = 0;
			if (_localSchedule != null)
			{
				int programsPerSlide = 10;
				programsPerSlide = _localSchedule.WeeklySchedule.Programs.Count > programsPerSlide ? programsPerSlide : _localSchedule.WeeklySchedule.Programs.Count;

				int totalSpotsCount = 0;
				if (FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked)
				{
					BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
					if (defaultProgram != null)
						totalSpotsCount = _localSchedule.WeeklySchedule.ShowEmptySpots ? defaultProgram.Spots.Count : defaultProgram.SpotsNotEmpty.Length;
				}
				for (int i = 0; i < _localSchedule.WeeklySchedule.Programs.Count; i += programsPerSlide)
					for (int k = 0, n = 0; k < (totalSpotsCount == 0 ? 1 : totalSpotsCount); k += 13, n++)
						result++;
			}
			return result;
		}

		private BusinessClasses.OutputScheduleGridBased[] PrepareOutputExcelBased()
		{
			List<BusinessClasses.OutputScheduleGridBased> outputPages = new List<BusinessClasses.OutputScheduleGridBased>();

			int programsPerSlide = 10;
			programsPerSlide = _localSchedule.WeeklySchedule.Programs.Count > programsPerSlide ? programsPerSlide : _localSchedule.WeeklySchedule.Programs.Count;

			int totalSpotsCount = 0;
			if (FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked)
			{
				BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
				if (defaultProgram != null)
					totalSpotsCount = _localSchedule.WeeklySchedule.ShowEmptySpots ? defaultProgram.Spots.Count : defaultProgram.SpotsNotEmpty.Length;
			}
			for (int i = 0; i < _localSchedule.WeeklySchedule.Programs.Count; i += programsPerSlide)
			{
				for (int k = 0; k < (totalSpotsCount == 0 ? 1 : totalSpotsCount); k += 13)
				{
					BusinessClasses.OutputScheduleGridBased outputPage = new BusinessClasses.OutputScheduleGridBased(_localSchedule.WeeklySchedule);
					outputPage.Advertiser = _localSchedule.BusinessName;
					outputPage.DecisionMaker = _localSchedule.DecisionMaker;
					outputPage.Demo = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);

					outputPage.ProgramsPerSlide = programsPerSlide;
					outputPage.SpotsPerSlide = totalSpotsCount > 0 ? (totalSpotsCount > 13 ? (((k + 1) * 13) < totalSpotsCount ? 13 : (totalSpotsCount - k + 13)) : totalSpotsCount) : 0;
					outputPage.ShowRates = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked;
					outputPage.ShowRating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked;
					outputPage.ShowCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked;
					outputPage.ShowGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked;
					outputPage.ShowCost = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked;
					outputPage.ShowSpots = FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked;
					outputPage.ShowStation = FormMain.Instance.buttonItemWeeklyScheduleStation.Checked;
					outputPage.ShowDaypart = FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked;
					outputPage.ShowDay = FormMain.Instance.buttonItemWeeklyScheduleDay.Checked;
					outputPage.ShowTime = FormMain.Instance.buttonItemWeeklyScheduleTime.Checked;
					outputPage.ShowLength = FormMain.Instance.buttonItemWeeklyScheduleLength.Checked;
					outputPage.ShowTotalSpots = FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked;
					outputPage.ShowTotalInvestment = FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked;
					outputPage.ShowDiscount = FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked;
					outputPage.ShowNetCost = FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked;

					#region Set Totals
					if (FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked)
						outputPage.Totals.Add(laTotalPeriodsTitle.Text, laTotalPeriodsValue.Text);
					if (FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked)
						outputPage.Totals.Add(laTotalSpotsTitle.Text, laTotalSpotsValue.Text);
					if (FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked)
						outputPage.Totals.Add(laTotalGRPTitle.Text, laTotalGRPValue.Text);
					if (FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked)
						outputPage.Totals.Add(laTotalCPPTitle.Text, laTotalCPPValue.Text);
					if (FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked)
						outputPage.Totals.Add(laAvgRateTitle.Text, laAvgRateValue.Text);
					if (FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked)
						outputPage.Totals.Add(laTotalCostTitle.Text, laTotalCostValue.Text);
					if (FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked)
						outputPage.Totals.Add(laNetRateTitle.Text, laNetRateValue.Text);
					if (FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked)
						outputPage.Totals.Add(laAgencyDiscountTitle.Text, laAgencyDiscountValue.Text);
					#endregion

					#region Set OutputProgram Values
					for (int j = 0; j < programsPerSlide; j++)
					{
						if ((i + j) < _localSchedule.WeeklySchedule.Programs.Count)
						{
							BusinessClasses.Program program = _localSchedule.WeeklySchedule.Programs[i + j];
							BusinessClasses.OutputProgramGridBased outputProgram = new BusinessClasses.OutputProgramGridBased(outputPage);
							outputProgram.Name = program.Name + (FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked ? ("-" + program.Daypart) : string.Empty);
							outputProgram.LineID = program.Index.ToString();
							outputProgram.Station = program.Station;
							outputProgram.Days = program.Day;
							outputProgram.Time = program.Time;
							outputProgram.Length = program.Length;
							outputProgram.Rate = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked && program.Rate.HasValue ? program.Rate.Value.ToString("$#,##0") : string.Empty;
							outputProgram.Rating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked && program.Rating.HasValue ? program.Rating.Value.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
							outputProgram.CPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked ? program.CPP.ToString("$#,###.00") : string.Empty;
							outputProgram.GRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked ? program.GRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
							outputProgram.TotalRate = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked ? program.Cost.ToString("$#,##0") : string.Empty;
							outputProgram.TotalSpots = program.TotalSpots.ToString("#,##0");

							#region Set Spots Values
							BusinessClasses.Spot[] spotsNotEmpy = program.SpotsNotEmpty;
							for (int l = 0; l < 13; l++)
							{
								if ((k + l) < totalSpotsCount)
								{
									string value = !program.Parent.ShowEmptySpots ? (spotsNotEmpy[k + l].Count > 0 ? spotsNotEmpy[k + l].Count.ToString() : "-") : (program.Spots[k + l].Count > 0 ? program.Spots[k + l].Count.ToString() : "-");
									outputProgram.Spots.Add(value);
								}
								else
									break;
								Application.DoEvents();
							}
							#endregion

							outputPage.Programs.Add(outputProgram);
						}
						else
							break;
						Application.DoEvents();
					}
					#endregion

					#region Set Total Values
					BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
					if (defaultProgram != null)
					{
						BusinessClasses.Spot[] defaultSpotsNotEmpy = defaultProgram.SpotsNotEmpty;
						for (int l = 0; l < 13; l++)
						{
							if ((k + l) < totalSpotsCount)
							{
								BusinessClasses.OutputTotalSpot outputTotalSpot = new BusinessClasses.OutputTotalSpot();
								outputTotalSpot.Day = !defaultProgram.Parent.ShowEmptySpots ? (defaultSpotsNotEmpy[k + l].Date.Day.ToString()) : (defaultProgram.Spots[k + l].Date.Day.ToString());
								outputTotalSpot.Month = BusinessClasses.Spot.GetMonthAbbreviation(!defaultProgram.Parent.ShowEmptySpots ? defaultSpotsNotEmpy[k + l].Date.Month : defaultProgram.Spots[k + l].Date.Month);

								int sum = 0;
								if (!defaultProgram.Parent.ShowEmptySpots)
									sum = defaultProgram.Parent.Programs.Select(x => x.SpotsNotEmpty.Where(y => y.Date.Equals(defaultSpotsNotEmpy[k + l].Date)).FirstOrDefault()).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
								else
									sum = defaultProgram.Parent.Programs.Select(x => x.Spots.Where(y => y.Date.Equals(defaultProgram.Spots[k + l].Date)).FirstOrDefault()).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
								outputTotalSpot.Value = sum > 0 ? sum.ToString() : "-";
								outputPage.TotalSpots.Add(outputTotalSpot);
							}
							else
								break;
							Application.DoEvents();
						}
					}
					outputPage.TotalCost = _localSchedule.WeeklySchedule.TotalCost.ToString("$#,##0");
					outputPage.TotalSpot = _localSchedule.WeeklySchedule.TotalSpots.ToString("#,##0");
					outputPage.TotalCPP = _localSchedule.WeeklySchedule.TotalCPP.ToString("$#,###.00");
					outputPage.TotalGRP = _localSchedule.WeeklySchedule.TotalGRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0");
					#endregion

					outputPages.Add(outputPage);

					Application.DoEvents();
				}
				Application.DoEvents();
			}
			return outputPages.ToArray();
		}

		private BusinessClasses.OutputScheduleGridBased PrepareOutputTableBased()
		{
			BusinessClasses.OutputScheduleGridBased outputPage = new BusinessClasses.OutputScheduleGridBased(_localSchedule.WeeklySchedule);
			BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();

			int totalSpotsCount = 0;
			if (FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked)
				if (defaultProgram != null)
					totalSpotsCount = defaultProgram.Parent.ShowEmptySpots ? defaultProgram.Spots.Count : defaultProgram.SpotsNotEmpty.Length;

			outputPage.Advertiser = _localSchedule.BusinessName;
			outputPage.DecisionMaker = _localSchedule.DecisionMaker;
			outputPage.Demo = _localSchedule.Demo + (!string.IsNullOrEmpty(_localSchedule.Source) ? (" (" + _localSchedule.Source + ")") : string.Empty);

			outputPage.ProgramsPerSlide = _localSchedule.WeeklySchedule.Programs.Count;
			outputPage.SpotsPerSlide = totalSpotsCount;
			outputPage.ShowRates = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked;
			outputPage.ShowRating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked;
			outputPage.ShowCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked;
			outputPage.ShowGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked;
			outputPage.ShowCost = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked;
			outputPage.ShowStation = FormMain.Instance.buttonItemWeeklyScheduleStation.Checked;
			outputPage.ShowDaypart = FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked;
			outputPage.ShowDay = FormMain.Instance.buttonItemWeeklyScheduleDay.Checked;
			outputPage.ShowTime = FormMain.Instance.buttonItemWeeklyScheduleTime.Checked;
			outputPage.ShowLength = FormMain.Instance.buttonItemWeeklyScheduleLength.Checked;
			outputPage.ShowTotalSpots = FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked;
			outputPage.ShowSpots = FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked;
			outputPage.ShowTotalInvestment = FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked;
			outputPage.ShowDiscount = FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked;
			outputPage.ShowNetCost = FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked;

			#region Set Totals
			if (FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked)
				outputPage.Totals.Add(laTotalPeriodsTitle.Text, laTotalPeriodsValue.Text);
			if (FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked)
				outputPage.Totals.Add(laTotalSpotsTitle.Text, laTotalSpotsValue.Text);
			if (FormMain.Instance.buttonItemWeeklyScheduleTotalGRP.Checked)
				outputPage.Totals.Add(laTotalGRPTitle.Text, laTotalGRPValue.Text);
			if (FormMain.Instance.buttonItemWeeklyScheduleTotalCPP.Checked)
				outputPage.Totals.Add(laTotalCPPTitle.Text, laTotalCPPValue.Text);
			if (FormMain.Instance.buttonItemWeeklyScheduleAvgRate.Checked)
				outputPage.Totals.Add(laAvgRateTitle.Text, laAvgRateValue.Text);
			if (FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked)
				outputPage.Totals.Add(laTotalCostTitle.Text, laTotalCostValue.Text);
			if (FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked)
				outputPage.Totals.Add(laNetRateTitle.Text, laNetRateValue.Text);
			if (FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked)
				outputPage.Totals.Add(laAgencyDiscountTitle.Text, laAgencyDiscountValue.Text);
			#endregion

			#region Set OutputProgram Values
			foreach (BusinessClasses.Program program in _localSchedule.WeeklySchedule.Programs)
			{
				BusinessClasses.OutputProgramGridBased outputProgram = new BusinessClasses.OutputProgramGridBased(outputPage);
				outputProgram.Name = program.Name + (FormMain.Instance.buttonItemWeeklyScheduleDaypart.Checked ? ("-" + program.Daypart) : string.Empty);
				outputProgram.LineID = program.Index.ToString();
				outputProgram.Station = program.Station;
				outputProgram.Days = program.Day;
				outputProgram.Time = program.Time;
				outputProgram.Length = program.Length;
				outputProgram.Rate = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked && program.Rate.HasValue ? program.Rate.Value.ToString("$#,##0") : string.Empty;
				outputProgram.Rating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked && program.Rating.HasValue ? program.Rating.Value.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
				outputProgram.CPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked ? program.CPP.ToString("$#,###.00") : string.Empty;
				outputProgram.GRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked ? program.GRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
				outputProgram.TotalRate = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked ? ((totalSpotsCount >= 8 ? "Cost: " : string.Empty) + program.Cost.ToString("$#,##0")) : string.Empty;
				outputProgram.TotalSpots = (totalSpotsCount >= 8 ? "Spots: " : string.Empty) + program.TotalSpots.ToString("#,##0");

				#region Set Spots Values
				foreach (BusinessClasses.Spot spot in program.Spots.Where(x => program.Parent.ShowEmptySpots || program.Parent.Programs.Select(z => z.Spots.Where(y => y.Date.Equals(x.Date)).FirstOrDefault()).Sum(w => w.Count) > 0))
				{
					string value = spot.Count > 0 ? spot.Count.ToString() : "-";
					outputProgram.Spots.Add(value);
					Application.DoEvents();
				}
				#endregion

				outputPage.Programs.Add(outputProgram);
				Application.DoEvents();
			}
			#endregion

			#region Set Total Values
			if (defaultProgram != null)
			{
				foreach (BusinessClasses.Spot spot in defaultProgram.Spots.Where(x => defaultProgram.Parent.ShowEmptySpots || defaultProgram.Parent.Programs.Select(z => z.Spots.Where(y => y.Date.Equals(x.Date)).FirstOrDefault()).Sum(w => w.Count) > 0))
				{
					BusinessClasses.OutputTotalSpot outputTotalSpot = new BusinessClasses.OutputTotalSpot();
					outputTotalSpot.Day = spot.Date.Day.ToString();
					outputTotalSpot.Month = BusinessClasses.Spot.GetMonthAbbreviation(spot.Date.Month);
					int sum = defaultProgram.Parent.Programs.Select(x => x.Spots.Where(y => y.Date.Equals(spot.Date)).FirstOrDefault()).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
					outputTotalSpot.Value = sum > 0 ? sum.ToString() : "-";
					outputPage.TotalSpots.Add(outputTotalSpot);
					Application.DoEvents();
				}
			}
			outputPage.TotalCost = _localSchedule.WeeklySchedule.TotalCost.ToString("$#,##0");
			outputPage.TotalSpot = _localSchedule.WeeklySchedule.TotalSpots.ToString("#,##0");
			outputPage.TotalCPP = _localSchedule.WeeklySchedule.TotalCPP.ToString("$#,###.00");
			outputPage.TotalGRP = _localSchedule.WeeklySchedule.TotalGRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0");
			#endregion

			outputPage.PopulateScheduleReplacementsList();

			return outputPage;
		}

		private BusinessClasses.OutputScheduleTagsBased[] PrepareOutputTagsBased()
		{
			List<string> temp = new List<string>();
			List<BusinessClasses.OutputScheduleTagsBased> outputPages = new List<BusinessClasses.OutputScheduleTagsBased>();

			if (_localSchedule != null)
			{
				int programsPerSlide = 10;
				programsPerSlide = _localSchedule.WeeklySchedule.Programs.Count > programsPerSlide ? programsPerSlide : _localSchedule.WeeklySchedule.Programs.Count;

				int totalSpotsCount = 0;
				if (FormMain.Instance.buttonItemWeeklyScheduleSpots.Checked)
				{
					BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
					if (defaultProgram != null)
						totalSpotsCount = _localSchedule.WeeklySchedule.ShowEmptySpots ? defaultProgram.Spots.Count : defaultProgram.SpotsNotEmpty.Length;
				}
				for (int i = 0; i < _localSchedule.WeeklySchedule.Programs.Count; i += programsPerSlide)
				{
					for (int k = 0; k < (totalSpotsCount == 0 ? 1 : totalSpotsCount); k += 13)
					{
						BusinessClasses.OutputScheduleTagsBased outputPage = new BusinessClasses.OutputScheduleTagsBased(_localSchedule.WeeklySchedule);
						outputPage.Advertiser = _localSchedule.BusinessName;
						outputPage.DecisionMaker = _localSchedule.DecisionMaker;

						outputPage.ProgramsPerSlide = programsPerSlide;
						outputPage.SpotsPerSlide = totalSpotsCount > 0 ? (totalSpotsCount > 13 ? 13 : totalSpotsCount) : 0;

						outputPage.ShowRating = FormMain.Instance.buttonItemWeeklyScheduleRating.Checked;
						outputPage.ShowRates = FormMain.Instance.buttonItemWeeklyScheduleRate.Checked;
						outputPage.ShowCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked;
						outputPage.ShowGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked;

						outputPage.ColumnsPerSlide = 1;
						if (FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked)
							outputPage.ColumnsPerSlide++;
						if (FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked)
							outputPage.ColumnsPerSlide++;
						if (FormMain.Instance.buttonItemWeeklyScheduleCost.Checked)
							outputPage.ColumnsPerSlide++;

						outputPage.ShowTotalInvestment = FormMain.Instance.buttonItemWeeklyScheduleTotalCost.Checked;
						outputPage.ShowDiscount = FormMain.Instance.buttonItemWeeklyScheduleDiscount.Checked;
						outputPage.ShowNetCost = FormMain.Instance.buttonItemWeeklyScheduleNetRate.Checked;
						outputPage.ShowTotalSpots = FormMain.Instance.buttonItemWeeklyScheduleTotalSpots.Checked;
						outputPage.ShowWeeks = FormMain.Instance.buttonItemWeeklyScheduleTotalPeriods.Checked;

						#region Set OutputProgram Values
						for (int j = 0; j < programsPerSlide; j++)
						{
							if ((i + j) < _localSchedule.WeeklySchedule.Programs.Count)
							{
								BusinessClasses.Program program = _localSchedule.WeeklySchedule.Programs[i + j];
								BusinessClasses.OutputProgramTagsBased outputProgram = new BusinessClasses.OutputProgramTagsBased();
								outputProgram.Name = program.Name + (FormMain.Instance.buttonItemWeeklyScheduleLength.Checked ? string.Format("    ({0})", program.Length) : string.Empty);
								outputProgram.LineID = program.Index.ToString();
								outputProgram.CPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked ? program.CPP.ToString("$#,###.00") : string.Empty;
								outputProgram.GRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked ? program.GRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
								outputProgram.TotalRate = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked ? program.Cost.ToString("$#,##0") : string.Empty;
								outputProgram.TotalSpots = program.TotalSpots.ToString("#,##0");

								string properties = string.Empty;

								temp.Clear();
								if (FormMain.Instance.buttonItemWeeklyScheduleStation.Checked)
									temp.Add(program.Station);
								if (FormMain.Instance.buttonItemWeeklyScheduleDay.Checked)
									temp.Add(program.Day);
								if (FormMain.Instance.buttonItemWeeklyScheduleTime.Checked)
									temp.Add(program.Time);
								if (FormMain.Instance.buttonItemWeeklyScheduleRate.Checked && program.Rate.HasValue)
									temp.Add("Rate: " + program.Rate.Value.ToString("$#,##0"));
								if (FormMain.Instance.buttonItemWeeklyScheduleRating.Checked && program.Rating.HasValue)
									temp.Add((_localSchedule.RatingAsCPP ? "Rtg: " : "000s: ") + program.Rating.Value.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0"));
								if (temp.Count > 0)
									properties = string.Join("    ", temp.ToArray());
								outputProgram.Properties = properties;


								#region Set Spots Values
								BusinessClasses.Spot[] spotsNotEmpy = program.SpotsNotEmpty;
								for (int l = 0; l < 13; l++)
								{
									if ((k + l) < totalSpotsCount)
									{
										string value = !program.Parent.ShowEmptySpots ? (spotsNotEmpy[k + l].Count > 0 ? spotsNotEmpy[k + l].Count.ToString() : "-") : (program.Spots[k + l].Count > 0 ? program.Spots[k + l].Count.ToString() : "-");
										outputProgram.Spots.Add(value);
									}
									else
										break;
									Application.DoEvents();
								}
								#endregion

								outputPage.Programs.Add(outputProgram);
							}
							else
								break;
							Application.DoEvents();
						}
						#endregion

						#region Set Total Values
						BusinessClasses.Program defaultProgram = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
						if (defaultProgram != null)
						{
							BusinessClasses.Spot[] defaultSpotsNotEmpy = defaultProgram.SpotsNotEmpty;
							for (int l = 0; l < 13; l++)
							{
								if ((k + l) < totalSpotsCount)
								{
									BusinessClasses.OutputTotalSpot outputTotalSpot = new BusinessClasses.OutputTotalSpot();
									outputTotalSpot.Day = !defaultProgram.Parent.ShowEmptySpots ? (defaultSpotsNotEmpy[k + l].Date.Day.ToString()) : (defaultProgram.Spots[k + l].Date.Day.ToString());
									outputTotalSpot.Month = BusinessClasses.Spot.GetMonthAbbreviation(!defaultProgram.Parent.ShowEmptySpots ? defaultSpotsNotEmpy[k + l].Date.Month : defaultProgram.Spots[k + l].Date.Month);

									int sum = 0;
									if (!defaultProgram.Parent.ShowEmptySpots)
										sum = defaultProgram.Parent.Programs.Select(x => x.SpotsNotEmpty.Where(y => y.Date.Equals(defaultSpotsNotEmpy[k + l].Date)).FirstOrDefault()).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
									else
										sum = defaultProgram.Parent.Programs.Select(x => x.Spots.Where(y => y.Date.Equals(defaultProgram.Spots[k + l].Date)).FirstOrDefault()).Where(z => z.Count.HasValue).Select(z => z.Count.Value).Sum();
									outputTotalSpot.Value = sum > 0 ? sum.ToString() : "-";
									outputPage.TotalSpots.Add(outputTotalSpot);
								}
								else
									break;
								Application.DoEvents();
							}
						}
						outputPage.TotalCost = FormMain.Instance.buttonItemWeeklyScheduleCost.Checked ? _localSchedule.WeeklySchedule.TotalCost.ToString("$#,##0") : string.Empty;
						outputPage.TotalSpot = _localSchedule.WeeklySchedule.TotalSpots.ToString("#,##0");
						outputPage.TotalCPP = FormMain.Instance.buttonItemWeeklyScheduleCPP.Checked ? _localSchedule.WeeklySchedule.TotalCPP.ToString("$#,###.00") : string.Empty;
						outputPage.TotalGRP = FormMain.Instance.buttonItemWeeklyScheduleGRP.Checked ? _localSchedule.WeeklySchedule.TotalGRP.ToString(_localSchedule.RatingAsCPP ? "#,###.0" : "#,##0") : string.Empty;
						#endregion

						outputPages.Add(outputPage);
						Application.DoEvents();
					}
					Application.DoEvents();
				}
			}
			return outputPages.ToArray();
		}

		public void PrintOutput()
		{
			bool showResultForm = false;
			if (_localSchedule != null)
			{
				BusinessClasses.Program program = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
				if (program != null)
				{
					using (ToolForms.FormProgress form = new ToolForms.FormProgress())
					{
						form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
						form.TopMost = true;
						this.Enabled = false;
						using (ToolForms.FormSelectOutput formSelect = new ToolForms.FormSelectOutput())
						{
							BusinessClasses.OutputScheduleGridBased outputPage = null;
							System.Threading.Thread thread = null;
							thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
							{
								this.Invoke((MethodInvoker)delegate
								{
									outputPage = PrepareOutputTableBased();
								});
							}));
							thread.Start();
							while (thread.IsAlive)
								System.Windows.Forms.Application.DoEvents();
							formSelect.ExcelBasedSlideCount = GetEstimatedSlideNumber();
							formSelect.TagsBasedSlideCount = formSelect.ExcelBasedSlideCount;
							formSelect.TableBasedSlideCount = 1;
							formSelect.IsEmailOutput = false;
							formSelect.buttonXGrid.Enabled = _localSchedule.WeeklySchedule.Programs.Count <= 4 && outputPage.SpotsPerSlide <= 13 && File.Exists(Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetTableBasedTemplatesFolderPath, string.Format(BusinessClasses.OutputManager.OneSheetTableBasedTemplateFileName, new object[] { outputPage.ProgramsPerSlide.ToString(), outputPage.SpotsPerSlide.ToString() })));
							formSelect.buttonXSlideMaster.Enabled = Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath);
							formSelect.buttonXGroupedObjects.Enabled = Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath);
							formSelect.buttonXPreview.Click += new EventHandler((sender, e) =>
							{
								using (ToolForms.FormPreview formPreview = new ToolForms.FormPreview())
								{
									using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
									{
										formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating preview slides...";
										formProgress.TopMost = true;

										string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));

										switch (formSelect.buttonXOutput.DialogResult)
										{
											case DialogResult.Yes:
												{
													formProgress.Show();
													InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailTableBased(tempFileName, outputPage, formSelect.TemplatePath);
													formProgress.Hide();
													formPreview.OutputClick += new EventHandler<EventArgs>((senderPreview, ePreview) =>
													{
														formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
														formProgress.TopMost = true;
														formProgress.Show();
														formPreview.Hide();
														formSelect.Hide();
														InteropClasses.PowerPointHelper.Instance.AppendOneSheetTableBased(outputPage, formSelect.TemplatePath);
														formProgress.Hide();
														using (ToolForms.FormSlideOutput formResult = new ToolForms.FormSlideOutput())
														{
															if (formResult.ShowDialog() != System.Windows.Forms.DialogResult.OK)
															{
																formPreview.Close();
																formSelect.Close();
																AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
															}
															else
															{
																formPreview.Close();
																formSelect.Close();
															}
														}
													});
												}
												break;
											case DialogResult.No:
											case DialogResult.Ignore:
												{
													formProgress.Show();
													BusinessClasses.OutputScheduleGridBased[] outputPages = null;
													System.Threading.Thread previewThread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
													{
														this.Invoke((MethodInvoker)delegate
														{
															outputPages = PrepareOutputExcelBased();
														});
													}));
													previewThread.Start();
													while (previewThread.IsAlive)
														System.Windows.Forms.Application.DoEvents();
													InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailExcelBased(tempFileName, outputPages, formSelect.buttonXOutput.DialogResult == DialogResult.Ignore, formSelect.TemplatePath);
													formProgress.Hide();

													formPreview.OutputClick += new EventHandler<EventArgs>((senderPreview, ePreview) =>
													{
														formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
														formProgress.TopMost = true;
														formProgress.Show();
														formPreview.Hide();
														formSelect.Hide();
														InteropClasses.PowerPointHelper.Instance.AppendOneSheetExcelBased(outputPages, formSelect.buttonXOutput.DialogResult == DialogResult.Ignore, formSelect.TemplatePath);
														formProgress.Hide();
														using (ToolForms.FormSlideOutput formResult = new ToolForms.FormSlideOutput())
														{
															if (formResult.ShowDialog() != System.Windows.Forms.DialogResult.OK)
															{
																formPreview.Close();
																formSelect.Close();
																AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
															}
															else
															{
																formPreview.Close();
																formSelect.Close();
															}
														}
													});
												}
												break;
											case DialogResult.Retry:
												{
													formProgress.Show();
													BusinessClasses.OutputScheduleTagsBased[] outputPages = null;
													System.Threading.Thread previewThread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
													{
														this.Invoke((MethodInvoker)delegate
														{
															outputPages = PrepareOutputTagsBased();
														});
													}));
													previewThread.Start();
													while (previewThread.IsAlive)
														System.Windows.Forms.Application.DoEvents();
													InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailSlideMasterBased(tempFileName, outputPages, formSelect.TemplatePath);
													formProgress.Hide();

													formPreview.OutputClick += new EventHandler<EventArgs>((senderPreview, ePreview) =>
													{
														formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
														formProgress.TopMost = true;
														formProgress.Show();
														formPreview.Hide();
														formSelect.Hide();
														InteropClasses.PowerPointHelper.Instance.AppendOneSheetSlideMasterBased(outputPages, formSelect.TemplatePath);
														formProgress.Hide();
														using (ToolForms.FormSlideOutput formResult = new ToolForms.FormSlideOutput())
														{
															if (formResult.ShowDialog() != System.Windows.Forms.DialogResult.OK)
															{
																formPreview.Close();
																formSelect.Close();
																AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
															}
															else
															{
																formPreview.Close();
																formSelect.Close();
															}
														}
													});
												}
												break;
											case DialogResult.Abort:
												{
													formProgress.Show();
													BusinessClasses.OutputScheduleTagsBased[] outputPages = null;
													System.Threading.Thread previewThread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
													{
														this.Invoke((MethodInvoker)delegate
														{
															outputPages = PrepareOutputTagsBased();
														});
													}));
													previewThread.Start();
													while (previewThread.IsAlive)
														System.Windows.Forms.Application.DoEvents();
													InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailGroupedTextBased(tempFileName, outputPages, formSelect.TemplatePath);
													formProgress.Hide();

													formPreview.OutputClick += new EventHandler<EventArgs>((senderPreview, ePreview) =>
													{
														formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
														formProgress.TopMost = true;
														formProgress.Show();
														formPreview.Hide();
														formSelect.Hide();
														InteropClasses.PowerPointHelper.Instance.AppendOneSheetGroupedTextBased(outputPages, formSelect.TemplatePath);
														formProgress.Hide();
														using (ToolForms.FormSlideOutput formResult = new ToolForms.FormSlideOutput())
														{
															if (formResult.ShowDialog() != System.Windows.Forms.DialogResult.OK)
															{
																formPreview.Close();
																formSelect.Close();
																AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
															}
															else
															{
																formPreview.Close();
																formSelect.Close();
															}
														}
													});
												}
												break;
										}
										if (File.Exists(tempFileName))
										{
											formPreview.Text = "Weekly Schedule - Quick View";
											formPreview.PresentationFile = tempFileName;
											formPreview.ShowDialog();
											ConfigurationClasses.RegistryHelper.MainFormHandle = formSelect.Handle;
											ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
										}
										formProgress.Close();
									}
								}
							});
							DialogResult result = formSelect.ShowDialog();
							ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
							ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
							if (result == DialogResult.Yes)
							{
								form.Show();
								InteropClasses.PowerPointHelper.Instance.AppendOneSheetTableBased(outputPage, formSelect.TemplatePath);
								form.Close();
								showResultForm = true;
							}
							else if (result == DialogResult.No || result == DialogResult.Ignore)
							{
								form.Show();
								BusinessClasses.OutputScheduleGridBased[] outputPages = null;
								thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
								{
									this.Invoke((MethodInvoker)delegate
									{
										outputPages = PrepareOutputExcelBased();
									});
								}));
								thread.Start();
								while (thread.IsAlive)
									System.Windows.Forms.Application.DoEvents();
								InteropClasses.PowerPointHelper.Instance.AppendOneSheetExcelBased(outputPages, result == DialogResult.Ignore, formSelect.TemplatePath);
								form.Close();
								showResultForm = true;
							}
							else if (result == DialogResult.Retry)
							{
								form.Show();
								BusinessClasses.OutputScheduleTagsBased[] outputPages = null;
								thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
								{
									this.Invoke((MethodInvoker)delegate
									{
										outputPages = PrepareOutputTagsBased();
									});
								}));
								thread.Start();
								while (thread.IsAlive)
									System.Windows.Forms.Application.DoEvents();
								InteropClasses.PowerPointHelper.Instance.AppendOneSheetSlideMasterBased(outputPages, formSelect.TemplatePath);
								form.Close();
								showResultForm = true;
							}
							else if (result == DialogResult.Abort)
							{
								form.Show();
								BusinessClasses.OutputScheduleTagsBased[] outputPages = null;
								thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
								{
									this.Invoke((MethodInvoker)delegate
									{
										outputPages = PrepareOutputTagsBased();
									});
								}));
								thread.Start();
								while (thread.IsAlive)
									System.Windows.Forms.Application.DoEvents();
								InteropClasses.PowerPointHelper.Instance.AppendOneSheetGroupedTextBased(outputPages, formSelect.TemplatePath);
								form.Close();
								showResultForm = true;
							}
						}
						this.Enabled = true;
					}
					if (showResultForm)
						using (ToolForms.FormSlideOutput form = new ToolForms.FormSlideOutput())
						{
							if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
								AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
						}
				}
			}
		}

		public void buttonItemWeeklySchedulePowerPoint_Click(object sender, EventArgs e)
		{
			PrintOutput();
		}

		public void buttonItemWeeklyScheduleEmail_Click(object sender, EventArgs e)
		{
			if (_localSchedule != null)
			{
				BusinessClasses.Program program = _localSchedule.WeeklySchedule.Programs.FirstOrDefault();
				if (program != null)
				{

					string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
					using (ToolForms.FormProgress form = new ToolForms.FormProgress())
					{
						form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
						form.TopMost = true;
						this.Enabled = false;
						using (ToolForms.FormSelectOutput formSelect = new ToolForms.FormSelectOutput())
						{
							BusinessClasses.OutputScheduleGridBased outputSchedule = null;
							System.Threading.Thread thread = null;
							thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
							{
								this.Invoke((MethodInvoker)delegate
								{
									outputSchedule = PrepareOutputTableBased();
								});
							}));
							thread.Start();
							while (thread.IsAlive)
								System.Windows.Forms.Application.DoEvents();

							formSelect.ExcelBasedSlideCount = GetEstimatedSlideNumber();
							formSelect.TagsBasedSlideCount = formSelect.ExcelBasedSlideCount;
							formSelect.TableBasedSlideCount = 1;
							formSelect.IsEmailOutput = true;
							formSelect.buttonXGrid.Enabled = _localSchedule.WeeklySchedule.Programs.Count <= 4 && outputSchedule.SpotsPerSlide <= 13 && _localSchedule.WeeklySchedule.Programs.Count <= 4 && File.Exists(Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetTableBasedTemplatesFolderPath, string.Format(BusinessClasses.OutputManager.OneSheetTableBasedTemplateFileName, new object[] { outputSchedule.ProgramsPerSlide.ToString(), outputSchedule.SpotsPerSlide.ToString() })));
							formSelect.buttonXSlideMaster.Enabled = Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath);
							formSelect.buttonXGroupedObjects.Enabled = Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath);
							DialogResult result = formSelect.ShowDialog();
							ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
							ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
							if (result == DialogResult.Yes)
							{
								form.Show();
								InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailTableBased(tempFileName, outputSchedule, formSelect.TemplatePath);
								form.Close();
							}
							else if (result == DialogResult.No || result == DialogResult.Ignore)
							{
								form.Show();
								BusinessClasses.OutputScheduleGridBased[] outputPackages = null;
								thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
								{
									this.Invoke((MethodInvoker)delegate
									{
										outputPackages = PrepareOutputExcelBased();
									});
								}));
								thread.Start();
								while (thread.IsAlive)
									System.Windows.Forms.Application.DoEvents();
								InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailExcelBased(tempFileName, outputPackages, result == DialogResult.Ignore, formSelect.TemplatePath);
								form.Close();
							}
							else if (result == DialogResult.Retry)
							{
								form.Show();
								BusinessClasses.OutputScheduleTagsBased[] outputPackages = null;
								thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
								{
									this.Invoke((MethodInvoker)delegate
									{
										outputPackages = PrepareOutputTagsBased();
									});
								}));
								thread.Start();
								while (thread.IsAlive)
									System.Windows.Forms.Application.DoEvents();
								InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailSlideMasterBased(tempFileName, outputPackages, formSelect.TemplatePath);
								form.Close();
							}
							else if (result == DialogResult.Abort)
							{
								form.Show();
								BusinessClasses.OutputScheduleTagsBased[] outputPackages = null;
								thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
								{
									this.Invoke((MethodInvoker)delegate
									{
										outputPackages = PrepareOutputTagsBased();
									});
								}));
								thread.Start();
								while (thread.IsAlive)
									System.Windows.Forms.Application.DoEvents();
								InteropClasses.PowerPointHelper.Instance.PrepareOneSheetEmailGroupedTextBased(tempFileName, outputPackages, formSelect.TemplatePath);
								form.Close();
							}
						}
						this.Enabled = true;
					}
					if (File.Exists(tempFileName))
						using (ToolForms.FormEmail formEmail = new ToolForms.FormEmail())
						{
							formEmail.Text = "Email this Weekly Schedule";
							formEmail.PresentationFile = tempFileName;
							formEmail.ShowDialog();
							ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
							ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
						}
				}
			}
		}
		#endregion
	}
}


