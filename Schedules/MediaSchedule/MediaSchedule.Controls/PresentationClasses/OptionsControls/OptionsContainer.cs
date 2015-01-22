using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab.ViewInfo;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.MediaSchedule.Controls.Properties;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.OptionsControls
{
	[ToolboxItem(false)]
	public partial class OptionsContainer : UserControl
	{
		private bool _allowToSave;
		private RegularSchedule _localSchedule;
		private XtraTabHitInfo _menuHitInfo;
		private readonly XtraTabDragDropHelper<OptionsControl> _tabDragDropHelper;
		public bool SettingsNotSaved { get; set; }

		public SlideType SlideType
		{
			get { return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVOptions : SlideType.RadioOptions; }
		}

		private OptionsControl ActiveOptionControl
		{
			get { return xtraTabControlOptionSets.SelectedTabPage as OptionsControl; }
		}

		private OptionsSummaryControl ActiveSummary
		{
			get { return xtraTabControlOptionSets.SelectedTabPage as OptionsSummaryControl; }
		}

		private OptionsSummaryControl Summary
		{
			get { return xtraTabControlOptionSets.TabPages.OfType<OptionsSummaryControl>().Single(); }
		}

		public OptionsContainer()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnNoOptionSets.Dock = DockStyle.Fill;
			pnOptionSets.Dock = DockStyle.Fill;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			var buttonInfos = new List<ButtonInfo>();
			buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsInfo, Tooltip = "Open Schedule Info", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsInfo; } });
			buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsOptions, Tooltip = "Open Options", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsStyle; } });
			retractableBarControl.AddButtons(buttonInfos);
			retractableBarControl.Collapse(true);
			_tabDragDropHelper = new XtraTabDragDropHelper<OptionsControl>(xtraTabControlOptionSets);
			_tabDragDropHelper.TabMoved += OnTabMoved;
		}

		#region Methods
		public bool AllowToLeaveControl
		{
			get
			{
				if (SettingsNotSaved)
					SaveSchedule();
				return true;
			}
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;

			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			laScheduleInfo.Text = String.Format("{0}{2}{1}",
				_localSchedule.BusinessName,
				_localSchedule.FlightDates,
				Environment.NewLine);
			FormThemeSelector.Link(Controller.Instance.OptionsTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));

			LoadOptionSets(quickLoad);

			if (!quickLoad)
			{
				LoadColors();
			}

			_allowToSave = true;

			LoadActiveOptionSetData();

			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			foreach (var optionsControl in xtraTabControlOptionSets.TabPages.OfType<OptionsControl>())
				optionsControl.SaveData();
			SaveColors();
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void LoadOptionSets(bool quickLoad)
		{
			if (quickLoad)
			{
				Summary.LoadData(_localSchedule.OptionsSummary);
			}
			else
			{
				xtraTabControlOptionSets.TabPages.Clear();
				xtraTabControlOptionSets.TabPages.Add(new OptionsSummaryControl(_localSchedule.OptionsSummary));
				Summary.DataChanged += (o, e) =>
				{
					if (!_allowToSave) return;
					UpdateTotalsValues();
					SettingsNotSaved = true;
				};
			}
			foreach (var optionSet in _localSchedule.Options.OrderBy(s => s.Index))
			{
				if (quickLoad)
				{
					var optionsControl = xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().FirstOrDefault(osc => osc.Data.UniqueID.Equals(optionSet.UniqueID));
					if (optionsControl == null) continue;
					optionsControl.LoadData(optionSet);
				}
				else
					AddOptionControl(optionSet);
			}
			if (!quickLoad)
				xtraTabControlOptionSets.SelectedTabPageIndex = 0;
			UpdateOptionsSplash();
		}

		private void LoadActiveOptionSetData(bool activate = false)
		{
			_allowToSave = false;
			if (ActiveOptionControl != null)
			{
				pnSummaryInfo.Visible = false;
				pnOptionSetInfo.Visible = true;

				buttonXOptionStation.Checked = ActiveOptionControl.Data.ShowStation;
				buttonXOptionProgram.Checked = ActiveOptionControl.Data.ShowProgram;
				buttonXOptionDay.Checked = ActiveOptionControl.Data.ShowDay;
				buttonXOptionTime.Checked = ActiveOptionControl.Data.ShowTime;
				buttonXOptionRate.Checked = ActiveOptionControl.Data.ShowRate;
				buttonXOptionLength.Checked = ActiveOptionControl.Data.ShowLenght;
				buttonXOptionLogo.Checked = ActiveOptionControl.Data.ShowLogo;
				buttonXOptionWeeklySpots.Checked = false;
				buttonXOptionMonthlySpots.Checked = false;
				buttonXOptionTotalSpots.Checked = false;
				if (ActiveOptionControl.Data.ShowSpots)
				{
					switch (ActiveOptionControl.Data.SpotType)
					{
						case SpotType.Week:
							buttonXOptionWeeklySpots.Checked = true;
							break;
						case SpotType.Month:
							buttonXOptionMonthlySpots.Checked = true;
							break;
						case SpotType.Total:
							buttonXOptionTotalSpots.Checked = true;
							break;
					}
				}
				buttonXOptionLineId.Checked = ActiveOptionControl.Data.ShowLineId;
				buttonXOptionCost.Checked = ActiveOptionControl.Data.ShowCost;
				buttonXOptionTallySpots.Checked = ActiveOptionControl.Data.ShowTotalSpots;
				buttonXOptionTallyCost.Checked = ActiveOptionControl.Data.ShowTotalCost;
				buttonXOptionAvgRate.Checked = ActiveOptionControl.Data.ShowAverageRate;
				checkEditUseDecimalRate.Checked = ActiveOptionControl.Data.UseDecimalRates;
				checkEditShowSpotX.Checked = ActiveOptionControl.Data.ShowSpotsX;
			}
			else if (ActiveSummary != null)
			{
				pnSummaryInfo.Visible = true;
				pnOptionSetInfo.Visible = false;

				buttonXSummaryLineId.Checked = ActiveSummary.Data.ShowLineId;
				buttonXSummaryCampaign.Checked = ActiveSummary.Data.ShowCampaign;
				buttonXSummaryComments.Checked = ActiveSummary.Data.ShowComments;
				buttonXSummaryLogo.Checked = ActiveSummary.Data.ShowLogo;
				buttonXSummaryTotalCost.Checked = ActiveSummary.Data.ShowTotalCost;
				buttonXSummaryTallySpots.Checked = ActiveSummary.Data.ShowTallySpots;
				buttonXSummaryTallyCost.Checked = ActiveSummary.Data.ShowTallyCost;
				checkEditUseDecimalRate.Checked = ActiveSummary.Data.UseDecimalRates;
				checkEditShowSpotX.Checked = ActiveSummary.Data.ShowSpotsX;

				switch (ActiveSummary.Data.SpotType)
				{
					case SpotType.Week:
						buttonXSummaryWeeklySpots.Enabled = true;
						buttonXSummaryWeeklyCost.Enabled = true;
						buttonXSummaryTotalWeeks.Enabled = true;
						buttonXSummaryWeeklySpots.Checked = ActiveSummary.Data.ShowSpots;
						buttonXSummaryWeeklyCost.Checked = ActiveSummary.Data.ShowCost;
						buttonXSummaryTotalWeeks.Checked = ActiveSummary.Data.ShowTotalPeriods;

						buttonXSummaryMonthlySpots.Enabled = false;
						buttonXSummaryMonthlyCost.Enabled = false;
						buttonXSummaryTotalMonths.Enabled = false;
						buttonXSummaryTotalSpots.Enabled = false;

						buttonXSummaryMonthlySpots.Checked = false;
						buttonXSummaryMonthlyCost.Checked = false;
						buttonXSummaryTotalMonths.Checked = false;
						buttonXSummaryTotalSpots.Checked = false;
						break;
					case SpotType.Month:
						buttonXSummaryMonthlySpots.Enabled = true;
						buttonXSummaryMonthlyCost.Enabled = true;
						buttonXSummaryTotalMonths.Enabled = true;
						buttonXSummaryMonthlySpots.Checked = ActiveSummary.Data.ShowSpots;
						buttonXSummaryMonthlyCost.Checked = ActiveSummary.Data.ShowCost;
						buttonXSummaryTotalMonths.Checked = ActiveSummary.Data.ShowTotalPeriods;

						buttonXSummaryWeeklySpots.Enabled = false;
						buttonXSummaryWeeklyCost.Enabled = false;
						buttonXSummaryTotalWeeks.Enabled = false;
						buttonXSummaryTotalSpots.Enabled = false;

						buttonXSummaryWeeklySpots.Checked = false;
						buttonXSummaryWeeklyCost.Checked = false;
						buttonXSummaryTotalWeeks.Checked = false;
						buttonXSummaryTotalSpots.Checked = false;
						break;
					case SpotType.Total:
						buttonXSummaryTotalSpots.Enabled = true;
						buttonXSummaryTotalSpots.Checked = ActiveSummary.Data.ShowSpots;

						buttonXSummaryWeeklySpots.Enabled = false;
						buttonXSummaryWeeklyCost.Enabled = false;
						buttonXSummaryTotalWeeks.Enabled = false;
						buttonXSummaryMonthlySpots.Enabled = false;
						buttonXSummaryMonthlyCost.Enabled = false;
						buttonXSummaryTotalMonths.Enabled = false;

						buttonXSummaryWeeklySpots.Checked = false;
						buttonXSummaryWeeklyCost.Checked = false;
						buttonXSummaryTotalWeeks.Checked = false;
						buttonXSummaryMonthlySpots.Checked = false;
						buttonXSummaryMonthlyCost.Checked = false;
						buttonXSummaryTotalMonths.Checked = false;
						break;
				}

				if (activate)
					ActiveSummary.UpdateView(true);
			}
			else
			{
				pnSummaryInfo.Visible = false;
				pnOptionSetInfo.Visible = false;
			}
			UpdateTotalsValues();
			UpdateTotalsVisibility();
			UpdateOutputStatus();
			_allowToSave = true;
		}

		private void AddOptionSet()
		{
			using (var form = new FormOptionSetName())
			{
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var optionSet = new OptionSet(_localSchedule);
				optionSet.Name = form.OptionSetName;
				_localSchedule.Options.Add(optionSet);
				_localSchedule.RebuildOptionSetIndexes();
				var optionControl = AddOptionControl(optionSet);
				xtraTabControlOptionSets.SelectedTabPage = optionControl;
				Summary.UpdateView();
			}
		}

		private void DeleteOptionSet(OptionsControl optionControl)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", optionControl.Data.Name) != DialogResult.Yes) return;
			_localSchedule.Options.Remove(optionControl.Data);
			_localSchedule.RebuildOptionSetIndexes();
			xtraTabControlOptionSets.TabPages.Remove(optionControl);
			Summary.UpdateView();
		}

		private void RenameOptionSet(OptionsControl optionControl)
		{
			if (optionControl == null) return;
			using (var form = new FormOptionSetName())
			{
				form.OptionSetName = optionControl.Data.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				optionControl.Data.Name = form.OptionSetName;
				optionControl.Text = form.OptionSetName;
				Summary.UpdateView();
			}
		}

		private OptionsControl AddOptionControl(OptionSet data)
		{
			var optionControl = new OptionsControl(data);
			optionControl.DataChanged += (o, e) =>
			{
				if (!_allowToSave) return;
				UpdateTotalsValues();
				UpdateOutputStatus();
				Summary.UpdateView();
				SettingsNotSaved = true;
			};
			var position = xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().Count();
			xtraTabControlOptionSets.TabPages.Insert(position, optionControl);
			return optionControl;
		}

		private void UpdateTotalsValues()
		{
			if (ActiveOptionControl != null && ActiveOptionControl.Data.Programs.Any())
			{
				pnBottom.Visible = true;
				switch (ActiveOptionControl.Data.SpotType)
				{
					case SpotType.Week:
						laTotalSpotsTitle.Text = "Weekly Spots";
						laTotalCostTitle.Text = "Weekly Cost";
						break;
					case SpotType.Month:
						laTotalSpotsTitle.Text = "Monthly Spots";
						laTotalCostTitle.Text = "Monthly Cost";
						break;
					case SpotType.Total:
						laTotalSpotsTitle.Text = "Total Spots";
						laTotalCostTitle.Text = "Total Cost";
						break;
				}
				laTotalSpotsValue.Text = ActiveOptionControl.Data.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveOptionControl.Data.TotalCost.ToString(ActiveOptionControl.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laAvgRateValue.Text = ActiveOptionControl.Data.AvgRate.ToString(ActiveOptionControl.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
			else if (ActiveSummary != null)
			{
				pnBottom.Visible = true;
				laTotalSpotsTitle.Text = "Total Spots";
				laTotalCostTitle.Text = "Total Cost";
				laTotalSpotsValue.Text = ActiveSummary.Data.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveSummary.Data.TotalCost.ToString(ActiveSummary.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laAvgRateValue.Text = String.Empty;
			}
			else
			{
				pnBottom.Visible = false;
				laTotalSpotsValue.Text = laAvgRateValue.Text = String.Empty;
			}
		}

		private void UpdateTotalsVisibility()
		{

			if (ActiveOptionControl != null)
			{
				pnTotalSpots.Visible = ActiveOptionControl.Data.ShowTotalSpots;
				pnTotalSpots.BringToFront();
				pnTotalCost.Visible = ActiveOptionControl.Data.ShowTotalCost;
				pnTotalCost.BringToFront();
				pnAvgRate.Visible = ActiveOptionControl.Data.ShowAverageRate;
				pnAvgRate.BringToFront();
			}
			else if (ActiveSummary != null)
			{
				pnTotalSpots.Visible = ActiveSummary.Data.ShowTallySpots;
				pnTotalSpots.SendToBack();
				pnTotalCost.Visible = ActiveSummary.Data.ShowTallyCost;
				pnTotalCost.BringToFront();
				pnAvgRate.Visible = false;
			}
			else
			{
				pnTotalSpots.Visible = false;
				pnTotalCost.Visible = false;
				pnAvgRate.Visible = false;
			}
		}

		private void UpdateOptionsSplash()
		{
			if (_localSchedule.Options.Any())
				pnOptionSets.BringToFront();
			else
				pnNoOptionSets.BringToFront();
		}

		private void LoadColors()
		{
			xtraScrollableControlColors.Controls.Clear();
			var selectedColor = BusinessWrapper.Instance.OutputManager.OptionsColors.FirstOrDefault(c => c.Name.Equals(MediaMetaData.Instance.SettingsManager.SelectedColor)) ?? BusinessWrapper.Instance.OutputManager.OptionsColors.FirstOrDefault();
			var topPosition = 20;
			foreach (var color in BusinessWrapper.Instance.OutputManager.OptionsColors)
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
				button.Click += (sender, e) =>
				{
					var clickedButton = (ButtonX)sender;
					if (clickedButton.Checked) return;
					foreach (var colorButton in xtraScrollableControlColors.Controls.OfType<ButtonX>())
						colorButton.Checked = false;
					clickedButton.Checked = true;
				};
				button.CheckedChanged += (sender, e) =>
				{
					if (!_allowToSave) return;
					SettingsNotSaved = true;
				};
				xtraScrollableControlColors.Controls.Add(button);
				button.Location = new Point(20, topPosition);
				topPosition += (button.Height + 20);
			}
			checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
		}

		private void SaveColors()
		{
			var checkedColorItem = xtraScrollableControlColors.Controls.OfType<ButtonX>().FirstOrDefault(b => b.Checked);
			MediaMetaData.Instance.SettingsManager.SelectedColor = checkedColorItem != null ? ((ColorFolder)checkedColorItem.Tag).Name : String.Empty;
			MediaMetaData.Instance.SettingsManager.UseSlideMaster = checkEditLockToMaster.Checked;
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}
		#endregion

		#region Ribbon Button Clicks
		public void New_Click(object sender, EventArgs e)
		{
			AddOptionSet();
			UpdateOptionsSplash();
			SettingsNotSaved = true;
		}

		public void AddProgram_Click(object sender, EventArgs e)
		{
			if (ActiveOptionControl == null) return;
			ActiveOptionControl.AddProgram();
		}

		public void DeleteProgram_Click(object sender, EventArgs e)
		{
			if (ActiveOptionControl == null) return;
			ActiveOptionControl.DeleteProgram();
		}

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule())
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new defaultName for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
				{
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
				}
			}
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("options");
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			Preview();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			PrintOutput();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			Email();
		}
		#endregion

		#region Options Controls Event Handlers
		private void xtraTabControlOptionSets_CloseButtonClick(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			var optionControl = arg.Page as OptionsControl;
			if (optionControl == null) return;
			DeleteOptionSet(optionControl);
			UpdateOptionsSplash();
			SettingsNotSaved = true;
		}

		private void xtraTabControlOptionSets_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LoadActiveOptionSetData(true);
		}

		private void xtraTabControlOptionSets_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControlOptionSets.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			if (!(_menuHitInfo.Page is OptionsControl)) return;
			contextMenuStripOptions.Show((Control)sender, e.Location);
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			_localSchedule.ChangeOptionSetPosition(
				_localSchedule.Options.IndexOf(((OptionsControl)e.MovedPage).Data),
				_localSchedule.Options.IndexOf(((OptionsControl)e.TargetPage).Data) + (1 * e.Offset));
			Summary.UpdateView();
			SettingsNotSaved = true;
		}

		private void toolStripMenuItemOptionSetsRename_Click(object sender, EventArgs e)
		{
			RenameOptionSet(_menuHitInfo.Page as OptionsControl);
			SettingsNotSaved = true;
		}

		private void OnSpotsClick(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked)
			{
				button.Checked = false;
				return;
			}
			_allowToSave = false;
			buttonXOptionWeeklySpots.Checked = false;
			buttonXOptionMonthlySpots.Checked = false;
			buttonXOptionTotalSpots.Checked = false;
			_allowToSave = true;
			button.Checked = true;
			UpdateTotalsValues();
		}

		private void OnInfoSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (ActiveOptionControl != null)
			{
				ActiveOptionControl.Data.ShowStation = buttonXOptionStation.Checked;
				ActiveOptionControl.Data.ShowProgram = buttonXOptionProgram.Checked;
				ActiveOptionControl.Data.ShowDay = buttonXOptionDay.Checked;
				ActiveOptionControl.Data.ShowTime = buttonXOptionTime.Checked;
				ActiveOptionControl.Data.ShowRate = buttonXOptionRate.Checked;
				ActiveOptionControl.Data.ShowLenght = buttonXOptionLength.Checked;
				ActiveOptionControl.Data.ShowLogo = buttonXOptionLogo.Checked;
				ActiveOptionControl.Data.ShowSpots = buttonXOptionWeeklySpots.Checked || buttonXOptionMonthlySpots.Checked || buttonXOptionTotalSpots.Checked;
				ActiveOptionControl.Data.ShowLineId = buttonXOptionLineId.Checked;
				ActiveOptionControl.Data.ShowCost = buttonXOptionCost.Checked;
				ActiveOptionControl.Data.ShowTotalSpots = buttonXOptionTallySpots.Checked;
				ActiveOptionControl.Data.ShowTotalCost = buttonXOptionTallyCost.Checked;
				ActiveOptionControl.Data.ShowAverageRate = buttonXOptionAvgRate.Checked;
				ActiveOptionControl.Data.UseDecimalRates = checkEditUseDecimalRate.Checked;
				ActiveOptionControl.Data.ShowSpotsX = checkEditShowSpotX.Checked;
				if (buttonXOptionWeeklySpots.Checked)
					ActiveOptionControl.Data.SpotType = SpotType.Week;
				else if (buttonXOptionMonthlySpots.Checked)
					ActiveOptionControl.Data.SpotType = SpotType.Month;
				else if (buttonXOptionTotalSpots.Checked)
					ActiveOptionControl.Data.SpotType = SpotType.Total;
				ActiveOptionControl.UpdateView();
				UpdateOutputStatus();
			}
			else if (ActiveSummary != null)
			{
				ActiveSummary.Data.ShowLineId = buttonXSummaryLineId.Checked;
				ActiveSummary.Data.ShowCampaign = buttonXSummaryCampaign.Checked;
				ActiveSummary.Data.ShowComments = buttonXSummaryComments.Checked;
				ActiveSummary.Data.ShowSpots = buttonXSummaryWeeklySpots.Checked || buttonXSummaryMonthlySpots.Checked || buttonXSummaryTotalSpots.Checked;
				ActiveSummary.Data.ShowCost = buttonXSummaryWeeklyCost.Checked || buttonXSummaryMonthlyCost.Checked;
				ActiveSummary.Data.ShowLogo = buttonXSummaryLogo.Checked;
				ActiveSummary.Data.ShowTotalPeriods = buttonXSummaryTotalWeeks.Checked || buttonXSummaryTotalMonths.Checked;
				ActiveSummary.Data.ShowTotalCost = buttonXSummaryTotalCost.Checked;
				ActiveSummary.Data.ShowTallySpots = buttonXSummaryTallySpots.Checked;
				ActiveSummary.Data.ShowTallyCost = buttonXSummaryTallyCost.Checked;
				ActiveSummary.Data.UseDecimalRates = checkEditUseDecimalRate.Checked;
				ActiveSummary.Data.ShowSpotsX = checkEditShowSpotX.Checked;
			}
			Summary.UpdateView();
			UpdateTotalsVisibility();
			UpdateTotalsValues();
			SettingsNotSaved = true;
		}
		#endregion

		#region Output
		private Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType))); }
		}

		private void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabOptions.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", _localSchedule.Section.TotalSpots);
				options.Add("AverageRate", _localSchedule.Section.AvgRate);
				options.Add("GrossInvestment", _localSchedule.Section.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
		}

		private void TrackPreview()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabOptions.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", _localSchedule.Section.TotalSpots);
				options.Add("AverageRate", _localSchedule.Section.AvgRate);
				options.Add("GrossInvestment", _localSchedule.Section.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}


		private void UpdateOutputStatus()
		{
			Controller.Instance.OptionsPowerPoint.Enabled =
				Controller.Instance.OptionsPreview.Enabled =
					Controller.Instance.OptionsEmail.Enabled = xtraTabControlOptionSets.TabPages.OfType<IOptionsSlide>().Any(ss => ss.ReadyForOutput);
		}

		private void PrintOutput()
		{
			SaveSchedule();
			var tabPages = xtraTabControlOptionSets.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IOptionsSlide>().Where(ss => ss.ReadyForOutput);
			var optionsSlides = new List<IOptionsSlide>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Options";
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as IOptionsSlide;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentOptionControl)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						optionsSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IOptionsSlide>());
				}
			else
				optionsSlides.AddRange(tabPages);
			if (!optionsSlides.Any()) return;
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					foreach (var optionsSlide in optionsSlides)
						optionsSlide.Output(SelectedTheme);
					formProgress.Close();
				});
			}
		}

		private void Preview()
		{
			SaveSchedule();
			var tabPages = xtraTabControlOptionSets.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IOptionsSlide>().Where(ss => ss.ReadyForOutput);
			var optionsSlides = new List<IOptionsSlide>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Options";
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as IOptionsSlide;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentOptionControl)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						optionsSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IOptionsSlide>());
				}
			else
				optionsSlides.AddRange(tabPages);
			if (!optionsSlides.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				previewGroups.AddRange(optionsSlides.Select(optionsSlide => optionsSlide.GetPreviewGroup(SelectedTheme)));
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			var trackAction = new Action(TrackPreview);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, trackAction))
			{
				formPreview.Text = "Preview Options";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}

		private void Email()
		{
			SaveSchedule();
			var tabPages = xtraTabControlOptionSets.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IOptionsSlide>().Where(ss => ss.ReadyForOutput);
			var optionsSlides = new List<IOptionsSlide>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Options";
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as IOptionsSlide;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentOptionControl)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						optionsSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IOptionsSlide>());
				}
			else
				optionsSlides.AddRange(tabPages);
			if (!optionsSlides.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				previewGroups.AddRange(optionsSlides.Select(optionsSlide => optionsSlide.GetPreviewGroup(SelectedTheme)));
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email these Options";
				formEmail.LoadGroups(previewGroups);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}
		#endregion
	}
}
