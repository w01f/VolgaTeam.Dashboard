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

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.SnapshotControls
{
	[ToolboxItem(false)]
	public partial class SnapshotContainer : UserControl
	{
		private bool _allowToSave;
		private RegularSchedule _localSchedule;
		private XtraTabHitInfo _menuHitInfo;
		private XtraTabDragDropHelper<SnapshotControl> _tabDragDropHelper;
		public bool SettingsNotSaved { get; set; }

		public SlideType SlideType
		{
			get { return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVSnapshot : SlideType.RadioSnapshot; }
		}

		private SnapshotControl ActiveSnapshot
		{
			get { return xtraTabControlSnapshots.SelectedTabPage as SnapshotControl; }
		}

		private SnapshotSummaryControl ActiveSummary
		{
			get { return xtraTabControlSnapshots.SelectedTabPage as SnapshotSummaryControl; }
		}

		private SnapshotSummaryControl Summary
		{
			get { return xtraTabControlSnapshots.TabPages.OfType<SnapshotSummaryControl>().Single(); }
		}

		public SnapshotContainer()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnNoSnapshots.Dock = DockStyle.Fill;
			pnSnapshots.Dock = DockStyle.Fill;
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
			_tabDragDropHelper = new XtraTabDragDropHelper<SnapshotControl>(xtraTabControlSnapshots);
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
			FormThemeSelector.Link(Controller.Instance.SnapshotTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));

			LoadSnapshots(quickLoad);

			if (!quickLoad)
			{
				LoadColors();
			}

			_allowToSave = true;

			LoadActiveTabData();

			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			foreach (var snapshotControl in xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>())
				snapshotControl.SaveData();
			Summary.SaveData();
			SaveColors();
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void LoadSnapshots(bool quickLoad)
		{
			if (quickLoad)
			{
				Summary.LoadData(_localSchedule.SnapshotSummary);
			}
			else
			{
				xtraTabControlSnapshots.TabPages.Clear();
				xtraTabControlSnapshots.TabPages.Add(new SnapshotSummaryControl(_localSchedule.SnapshotSummary));
				Summary.DataChanged += (o, e) =>
				{
					if (!_allowToSave) return;
					UpdateTotalsValues();
					SettingsNotSaved = true;
				};
			}

			foreach (var snapshot in _localSchedule.Snapshots.OrderBy(s => s.Index))
			{
				if (quickLoad)
				{
					var snapshotControl = xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().FirstOrDefault(sc => sc.Data.UniqueID.Equals(snapshot.UniqueID));
					if (snapshotControl == null) continue;
					snapshotControl.LoadData(snapshot);
				}
				else
					AddSnapshotControl(snapshot);
			}
			if (!quickLoad)
				xtraTabControlSnapshots.SelectedTabPageIndex = 0;
			UpdateSnapshotSplash();
		}

		private void LoadActiveTabData(bool activate = false)
		{
			_allowToSave = false;
			if (ActiveSnapshot != null)
			{
				pnSnapshotInfo.Visible = true;
				pnSummaryInfo.Visible = false;

				buttonXSnapshotLineId.Checked = ActiveSnapshot.Data.ShowLineId;
				buttonXSnapshotStation.Checked = ActiveSnapshot.Data.ShowStation;
				buttonXSnapshotLength.Checked = ActiveSnapshot.Data.ShowLenght;
				buttonXSnapshotProgram.Checked = ActiveSnapshot.Data.ShowProgram;
				buttonXSnapshotDaypart.Checked = ActiveSnapshot.Data.ShowDaypart;
				buttonXSnapshotTime.Checked = ActiveSnapshot.Data.ShowTime;
				buttonXSnapshotRate.Checked = ActiveSnapshot.Data.ShowRate;
				buttonXSnapshotCost.Checked = ActiveSnapshot.Data.ShowCost;
				buttonXSnapshotLogo.Checked = ActiveSnapshot.Data.ShowLogo;
				buttonXSnapshotTotalSpots.Checked = ActiveSnapshot.Data.ShowTotalSpots;
				buttonXSnapshotAvgRate.Checked = ActiveSnapshot.Data.ShowAverageRate;
				buttonXSnapshotTotalRow.Checked = ActiveSnapshot.Data.ShowTotalRow;
				checkEditUseDecimalRate.Checked = ActiveSnapshot.Data.UseDecimalRates;
				checkEditShowSpotX.Checked = ActiveSnapshot.Data.ShowSpotsX;
			}
			else if (ActiveSummary != null)
			{
				pnSummaryInfo.Visible = true;
				pnSnapshotInfo.Visible = false;

				buttonXSummaryLineId.Checked = ActiveSummary.Data.ShowLineId;
				buttonXSummaryCampaign.Checked = ActiveSummary.Data.ShowCampaign;
				buttonXSummaryComments.Checked = ActiveSummary.Data.ShowComments;
				buttonXSummarySpots.Checked = ActiveSummary.Data.ShowSpots;
				buttonXSummaryCost.Checked = ActiveSummary.Data.ShowCost;
				buttonXSummaryLogo.Checked = ActiveSummary.Data.ShowLogo;
				buttonXSummaryTotalWeeks.Checked = ActiveSummary.Data.ShowTotalWeeks;
				buttonXSummaryTotalCost.Checked = ActiveSummary.Data.ShowTotalCost;
				buttonXSummaryTallySpots.Checked = ActiveSummary.Data.ShowTallySpots;
				buttonXSummaryTallyCost.Checked = ActiveSummary.Data.ShowTallyCost;
				checkEditUseDecimalRate.Checked = ActiveSummary.Data.UseDecimalRates;
				checkEditShowSpotX.Checked = ActiveSummary.Data.ShowSpotsX;
				if(activate)
					ActiveSummary.UpdateView(true);
			}
			else
			{
				pnSummaryInfo.Visible = false;
				pnSnapshotInfo.Visible = false;
			}
			UpdateTotalsValues();
			UpdateTotalsVisibility();
			UpdateOutputStatus();
			_allowToSave = true;
		}

		private void AddSnapshot()
		{
			using (var form = new FormSnapshotName())
			{
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var snapshot = new Snapshot(_localSchedule);
				snapshot.Name = form.SnapshotName;
				_localSchedule.Snapshots.Add(snapshot);
				_localSchedule.RebuildSnapshotIndexes();
				var snapshotControl = AddSnapshotControl(snapshot);
				xtraTabControlSnapshots.SelectedTabPage = snapshotControl;
				Summary.UpdateView();
			}
		}

		private void DeleteSnapshot(SnapshotControl snapshotControl)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", snapshotControl.Data.Name) != DialogResult.Yes) return;
			_localSchedule.Snapshots.Remove(snapshotControl.Data);
			_localSchedule.RebuildSnapshotIndexes();
			xtraTabControlSnapshots.TabPages.Remove(snapshotControl);
			Summary.UpdateView();
		}

		private void RenameSnapshot(SnapshotControl snapshotControl)
		{
			if (snapshotControl == null) return;
			using (var form = new FormSnapshotName())
			{
				form.SnapshotName = snapshotControl.Data.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				snapshotControl.Data.Name = form.SnapshotName;
				snapshotControl.Text = form.SnapshotName;
				Summary.UpdateView();
			}
		}

		private SnapshotControl AddSnapshotControl(Snapshot data)
		{
			var snapshotControl = new SnapshotControl(data);
			snapshotControl.DataChanged += (o, e) =>
			{
				if (!_allowToSave) return;
				UpdateTotalsValues();
				UpdateOutputStatus();
				SettingsNotSaved = true;
			};
			var position = xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().Count();
			xtraTabControlSnapshots.TabPages.Insert(position, snapshotControl);
			return snapshotControl;
		}

		private void UpdateTotalsValues()
		{
			if (ActiveSnapshot != null && ActiveSnapshot.Data.Programs.Any())
			{
				pnBottom.Visible = true;
				laTotalSpotsValue.Text = ActiveSnapshot.Data.TotalSpots.ToString("#,##0");
				laAvgRateValue.Text = ActiveSnapshot.Data.AvgRate.ToString(ActiveSnapshot.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laTotalCostValue.Text = String.Empty;
			}
			else if (ActiveSummary != null)
			{
				pnBottom.Visible = true;
				laTotalSpotsValue.Text = ActiveSummary.Data.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveSummary.Data.TotalCost.ToString(ActiveSummary.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laAvgRateValue.Text = String.Empty;
			}
			else
			{
				pnBottom.Visible = false;
				laTotalSpotsValue.Text = laTotalCostValue.Text = laAvgRateValue.Text = String.Empty;
			}
		}

		private void UpdateTotalsVisibility()
		{
			if (ActiveSnapshot != null)
			{
				pnTotalSpots.Visible = ActiveSnapshot.Data.ShowTotalSpots;
				pnTotalSpots.SendToBack();
				pnAvgRate.Visible = ActiveSnapshot.Data.ShowAverageRate;
				pnAvgRate.BringToFront();
				pnTotalCost.Visible = false;
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

		private void UpdateSnapshotSplash()
		{
			if (_localSchedule.Snapshots.Any())
				pnSnapshots.BringToFront();
			else
				pnNoSnapshots.BringToFront();
		}

		private void LoadColors()
		{
			xtraScrollableControlColors.Controls.Clear();
			var selectedColor = BusinessWrapper.Instance.OutputManager.SnapshotColors.FirstOrDefault(c => c.Name.Equals(MediaMetaData.Instance.SettingsManager.SelectedColor)) ?? BusinessWrapper.Instance.OutputManager.SnapshotColors.FirstOrDefault();
			var topPosition = 20;
			foreach (var color in BusinessWrapper.Instance.OutputManager.SnapshotColors)
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
			AddSnapshot();
			UpdateSnapshotSplash();
			SettingsNotSaved = true;
		}

		public void AddProgram_Click(object sender, EventArgs e)
		{
			if (ActiveSnapshot == null) return;
			ActiveSnapshot.AddProgram();
		}

		public void DeleteProgram_Click(object sender, EventArgs e)
		{
			if (ActiveSnapshot == null) return;
			ActiveSnapshot.DeleteProgram();
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
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("snapshot");
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

		#region Snapshot Controls Event Handlers
		private void xtraTabControlSnapshots_CloseButtonClick(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			var snapshotControl = arg.Page as SnapshotControl;
			if (snapshotControl == null) return;
			DeleteSnapshot(snapshotControl);
			UpdateSnapshotSplash();
			SettingsNotSaved = true;
		}

		private void xtraTabControlSnapshots_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LoadActiveTabData(true);
		}

		private void xtraTabControlSnapshots_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControlSnapshots.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			if (!(_menuHitInfo.Page is SnapshotControl)) return;
			contextMenuStripSnapshot.Show((Control)sender, e.Location);
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			_localSchedule.ChangeSnapshotPosition(
				_localSchedule.Snapshots.IndexOf(((SnapshotControl)e.MovedPage).Data),
				_localSchedule.Snapshots.IndexOf(((SnapshotControl)e.TargetPage).Data) + (1 * e.Offset));
			Summary.UpdateView();
			SettingsNotSaved = true;
		}

		private void toolStripMenuItemSnapshotRename_Click(object sender, EventArgs e)
		{
			RenameSnapshot(_menuHitInfo.Page as SnapshotControl);
			SettingsNotSaved = true;
		}

		private void OnInfoSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (ActiveSnapshot != null)
			{
				ActiveSnapshot.Data.ShowLineId = buttonXSnapshotLineId.Checked;
				ActiveSnapshot.Data.ShowStation = buttonXSnapshotStation.Checked;
				ActiveSnapshot.Data.ShowLenght = buttonXSnapshotLength.Checked;
				ActiveSnapshot.Data.ShowProgram = buttonXSnapshotProgram.Checked;
				ActiveSnapshot.Data.ShowDaypart = buttonXSnapshotDaypart.Checked;
				ActiveSnapshot.Data.ShowTime = buttonXSnapshotTime.Checked;
				ActiveSnapshot.Data.ShowRate = buttonXSnapshotRate.Checked;
				ActiveSnapshot.Data.ShowCost = buttonXSnapshotCost.Checked;
				ActiveSnapshot.Data.ShowLogo = buttonXSnapshotLogo.Checked;
				ActiveSnapshot.Data.ShowTotalSpots = buttonXSnapshotTotalSpots.Checked;
				ActiveSnapshot.Data.ShowAverageRate = buttonXSnapshotAvgRate.Checked;
				ActiveSnapshot.Data.ShowTotalRow = buttonXSnapshotTotalRow.Checked;
				ActiveSnapshot.Data.UseDecimalRates = checkEditUseDecimalRate.Checked;
				ActiveSnapshot.Data.ShowSpotsX = checkEditShowSpotX.Checked;
				ActiveSnapshot.UpdateView();
				UpdateOutputStatus();
			}
			else if (ActiveSummary != null)
			{
				ActiveSummary.Data.ShowLineId = buttonXSummaryLineId.Checked;
				ActiveSummary.Data.ShowCampaign = buttonXSummaryCampaign.Checked;
				ActiveSummary.Data.ShowComments = buttonXSummaryComments.Checked;
				ActiveSummary.Data.ShowSpots = buttonXSummarySpots.Checked;
				ActiveSummary.Data.ShowCost = buttonXSummaryCost.Checked;
				ActiveSummary.Data.ShowLogo = buttonXSummaryLogo.Checked;
				ActiveSummary.Data.ShowTotalWeeks = buttonXSummaryTotalWeeks.Checked;
				ActiveSummary.Data.ShowTotalCost = buttonXSummaryTotalCost.Checked;
				ActiveSummary.Data.ShowTallySpots = buttonXSummaryTallySpots.Checked;
				ActiveSummary.Data.ShowTallyCost = buttonXSummaryTallyCost.Checked;
				ActiveSummary.Data.UseDecimalRates = checkEditUseDecimalRate.Checked;
				ActiveSummary.Data.ShowSpotsX = checkEditShowSpotX.Checked;
				ActiveSummary.UpdateView();
			}
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
			options.Add("Slide", Controller.Instance.TabSnapshot.Text);
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
			options.Add("Slide", Controller.Instance.TabSnapshot.Text);
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
			Controller.Instance.SnapshotPowerPoint.Enabled =
				Controller.Instance.SnapshotPreview.Enabled =
					Controller.Instance.SnapshotEmail.Enabled = xtraTabControlSnapshots.TabPages.OfType<ISnapshotSlide>().Any(ss => ss.ReadyForOutput);
		}

		private void PrintOutput()
		{
			SaveSchedule();
			var tabPages = xtraTabControlSnapshots.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<ISnapshotSlide>().Where(ss => ss.ReadyForOutput);
			var selectedSnapshots = new List<ISnapshotSlide>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Snapshots";
					var currentSnapshots = xtraTabControlSnapshots.SelectedTabPage as ISnapshotSlide;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentSnapshots)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedSnapshots.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<ISnapshotSlide>());
				}
			else
				selectedSnapshots.AddRange(tabPages);
			if (!selectedSnapshots.Any()) return;
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					foreach (var snapshotSlide in selectedSnapshots)
						snapshotSlide.Output(SelectedTheme);
					formProgress.Close();
				});
			}
		}

		private void Preview()
		{
			SaveSchedule();
			var tabPages = xtraTabControlSnapshots.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<ISnapshotSlide>().Where(ss => ss.ReadyForOutput);
			var selectedSnapshots = new List<ISnapshotSlide>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Snapshots";
					var currentSnapshots = xtraTabControlSnapshots.SelectedTabPage as ISnapshotSlide;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentSnapshots)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedSnapshots.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<ISnapshotSlide>());
				}
			else
				selectedSnapshots.AddRange(tabPages);
			if (!selectedSnapshots.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			var trackAction = new Action(TrackPreview);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, trackAction))
			{
				formPreview.Text = "Preview Snapshots";
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
			var tabPages = xtraTabControlSnapshots.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<ISnapshotSlide>().Where(ss => ss.ReadyForOutput);
			var selectedSnapshots = new List<ISnapshotSlide>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Snapshots";
					var currentSnapshots = xtraTabControlSnapshots.SelectedTabPage as ISnapshotSlide;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentSnapshots)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedSnapshots.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<ISnapshotSlide>());
				}
			else
				selectedSnapshots.AddRange(tabPages);
			if (!selectedSnapshots.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email these Snapshots";
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
