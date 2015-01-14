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
			if (!quickLoad)
			{
				xtraTabControlOptionSets.TabPages.Clear();
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

		private void LoadActiveOptionSetData()
		{
			_allowToSave = false;
			if (ActiveOptionControl != null)
			{

				buttonXStation.Checked = ActiveOptionControl.Data.ShowStation;
				buttonXProgram.Checked = ActiveOptionControl.Data.ShowProgram;
				buttonXDay.Checked = ActiveOptionControl.Data.ShowDay;
				buttonXTime.Checked = ActiveOptionControl.Data.ShowTime;
				buttonXRate.Checked = ActiveOptionControl.Data.ShowRate;
				buttonXLength.Checked = ActiveOptionControl.Data.ShowLenght;
				buttonXLogo.Checked = ActiveOptionControl.Data.ShowLogo;
				buttonXWeeklySpots.Checked = false;
				buttonXMonthlySpots.Checked = false;
				buttonXTotalSpots.Checked = false;
				if (ActiveOptionControl.Data.ShowSpots)
				{
					switch (ActiveOptionControl.Data.SpotType)
					{
						case SpotType.Week:
							buttonXWeeklySpots.Checked = true;
							break;
						case SpotType.Month:
							buttonXMonthlySpots.Checked = true;
							break;
						case SpotType.Total:
							buttonXTotalSpots.Checked = true;
							break;
					}
				}
				buttonXLineId.Checked = ActiveOptionControl.Data.ShowLineId;
				buttonXCost.Checked = ActiveOptionControl.Data.ShowCost;
				buttonXTallySpots.Checked = ActiveOptionControl.Data.ShowTotalSpots;
				buttonXTallyCost.Checked = ActiveOptionControl.Data.ShowTotalCost;
				buttonXAvgRate.Checked = ActiveOptionControl.Data.ShowAverageRate;
				checkEditUseDecimalRate.Checked = ActiveOptionControl.Data.UseDecimalRates;
				checkEditShowSpotX.Checked = ActiveOptionControl.Data.ShowSpotsX;
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
			}
		}

		private void DeleteOptionSet(OptionsControl optionControl)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", optionControl.Data.Name) != DialogResult.Yes) return;
			_localSchedule.Options.Remove(optionControl.Data);
			_localSchedule.RebuildOptionSetIndexes();
			xtraTabControlOptionSets.TabPages.Remove(optionControl);
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
			else
			{
				pnBottom.Visible = false;
				laTotalSpotsValue.Text = laAvgRateValue.Text = String.Empty;
			}
		}

		private void UpdateTotalsVisibility()
		{
			pnTotalSpots.Visible = ActiveOptionControl != null && ActiveOptionControl.Data.ShowTotalSpots;
			pnTotalSpots.BringToFront();
			pnTotalCost.Visible = ActiveOptionControl != null && ActiveOptionControl.Data.ShowTotalCost;
			pnTotalCost.BringToFront();
			pnAvgRate.Visible = ActiveOptionControl != null && ActiveOptionControl.Data.ShowAverageRate;
			pnAvgRate.BringToFront();
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
			LoadActiveOptionSetData();
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
			buttonXWeeklySpots.Checked = false;
			buttonXMonthlySpots.Checked = false;
			buttonXTotalSpots.Checked = false;
			_allowToSave = true;
			button.Checked = true;
			UpdateTotalsValues();
		}

		private void OnInfoSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave || ActiveOptionControl == null) return;
			ActiveOptionControl.Data.ShowStation = buttonXStation.Checked;
			ActiveOptionControl.Data.ShowProgram = buttonXProgram.Checked;
			ActiveOptionControl.Data.ShowDay = buttonXDay.Checked;
			ActiveOptionControl.Data.ShowTime = buttonXTime.Checked;
			ActiveOptionControl.Data.ShowRate = buttonXRate.Checked;
			ActiveOptionControl.Data.ShowLenght = buttonXLength.Checked;
			ActiveOptionControl.Data.ShowLogo = buttonXLogo.Checked;
			ActiveOptionControl.Data.ShowSpots = buttonXWeeklySpots.Checked || buttonXMonthlySpots.Checked || buttonXTotalSpots.Checked;
			ActiveOptionControl.Data.ShowLineId = buttonXLineId.Checked;
			ActiveOptionControl.Data.ShowCost = buttonXCost.Checked;
			ActiveOptionControl.Data.ShowTotalSpots = buttonXTallySpots.Checked;
			ActiveOptionControl.Data.ShowTotalCost = buttonXTallyCost.Checked;
			ActiveOptionControl.Data.ShowAverageRate = buttonXAvgRate.Checked;
			ActiveOptionControl.Data.UseDecimalRates = checkEditUseDecimalRate.Checked;
			ActiveOptionControl.Data.ShowSpotsX = checkEditShowSpotX.Checked;
			if (buttonXWeeklySpots.Checked)
				ActiveOptionControl.Data.SpotType = SpotType.Week;
			else if (buttonXMonthlySpots.Checked)
				ActiveOptionControl.Data.SpotType = SpotType.Month;
			else if (buttonXTotalSpots.Checked)
				ActiveOptionControl.Data.SpotType = SpotType.Total;
			ActiveOptionControl.UpdateView();
			UpdateTotalsVisibility();
			UpdateTotalsValues();
			UpdateOutputStatus();
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
					Controller.Instance.OptionsEmail.Enabled = ActiveOptionControl != null && ActiveOptionControl.ReadyForOutput;
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
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as OptionsControl;
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
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as OptionsControl;
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
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as OptionsControl;
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
