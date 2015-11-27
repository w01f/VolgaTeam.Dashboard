﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab.ViewInfo;
using Asa.CommonGUI.Common;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.RetractableBar;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Asa.MediaSchedule.Controls.InteropClasses;
using Asa.MediaSchedule.Controls.Properties;

namespace Asa.MediaSchedule.Controls.PresentationClasses.SnapshotControls
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
			get { return MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVSnapshot : SlideType.RadioSnapshot; }
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
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			retractableBarControl.Collapse(true);
			_tabDragDropHelper = new XtraTabDragDropHelper<SnapshotControl>(xtraTabControlSnapshots);
			_tabDragDropHelper.TabMoved += OnTabMoved;
			BusinessObjects.Instance.OutputManager.ColorsChanged += (o, e) =>
			{
				InitColorsControl();
				LoadBarButtons();
			};
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.SnapshotThemeBar.RecalcLayout();
				Controller.Instance.SnapshotPanel.PerformLayout();
			};
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

			_localSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			labelControlScheduleInfo.Text = String.Format("{0}{3}<color=gray><i>{1} ({2})</i></color>",
				_localSchedule.BusinessName,
				_localSchedule.FlightDates,
				String.Format("{0} {1}s", _localSchedule.TotalWeeks, "week"),
				Environment.NewLine);
			InitThemeSelector();
			LoadSnapshots(quickLoad);

			if (!quickLoad)
			{
				InitColorsControl();
			}

			hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();

			_allowToSave = true;

			LoadActiveTabData();

			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			var quickLoad = !SettingsNotSaved && _localSchedule.BroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Schedule;
			foreach (var snapshotControl in xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>())
				snapshotControl.SaveData();
			Summary.SaveData();
			SaveColors();
			_localSchedule.BroadcastCalendar.UpdateDataSource();
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, quickLoad, false, false, this);
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

				checkedListBoxActiveWeeks.Items.Clear();
				var scheduleWeekRanges = _localSchedule.GetWeeks();
				var snapshotWeeks = ActiveSnapshot.Data.ActiveWeeks;
				checkedListBoxActiveWeeks.Items.AddRange(scheduleWeekRanges.Select(w => new CheckedListBoxItem(w, w.Range, !snapshotWeeks.Any() || snapshotWeeks.Any(sw => sw.StartDate == w.StartDate && sw.FinishDate == w.FinishDate) ? CheckState.Checked : CheckState.Unchecked)).ToArray());
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
				if (activate)
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
			LoadBarButtons();
			_allowToSave = true;
		}

		private void ApplySharedSettings(SnapshotControl templateControl)
		{
			foreach (var snapshotControl in xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().Where(oc => oc.Data.UniqueID != templateControl.Data.UniqueID))
			{
				snapshotControl.Data.ShowLineId = templateControl.Data.ShowLineId;
				snapshotControl.Data.ShowStation = templateControl.Data.ShowStation;
				snapshotControl.Data.ShowLenght = templateControl.Data.ShowLenght;
				snapshotControl.Data.ShowProgram = templateControl.Data.ShowProgram;
				snapshotControl.Data.ShowDaypart = templateControl.Data.ShowDaypart;
				snapshotControl.Data.ShowTime = templateControl.Data.ShowTime;
				snapshotControl.Data.ShowRate = templateControl.Data.ShowRate;
				snapshotControl.Data.ShowCost = templateControl.Data.ShowCost;
				snapshotControl.Data.ShowLogo = templateControl.Data.ShowLogo;
				snapshotControl.Data.ShowTotalSpots = templateControl.Data.ShowTotalSpots;
				snapshotControl.Data.ShowAverageRate = templateControl.Data.ShowAverageRate;
				snapshotControl.Data.ShowTotalRow = templateControl.Data.ShowTotalRow;
				snapshotControl.Data.UseDecimalRates = templateControl.Data.UseDecimalRates;
				snapshotControl.Data.ShowSpotsX = templateControl.Data.ShowSpotsX;
				snapshotControl.Data.ShowSpotsPerWeek = templateControl.Data.ShowSpotsPerWeek;
			}
		}

		private void ApplySharedContractSettings(ContractSettings templateSettings)
		{
			foreach (var snapshotControl in xtraTabControlSnapshots.TabPages.OfType<ISnapshotSlide>())
			{
				snapshotControl.ContractSettings.ShowSignatureLine = templateSettings.ShowSignatureLine;
				snapshotControl.ContractSettings.ShowDisclaimer = templateSettings.ShowDisclaimer;
				snapshotControl.ContractSettings.RateExpirationDate = templateSettings.RateExpirationDate;
			}
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

		private void CloneSnapshot(SnapshotControl snapshotControl)
		{
			using (var form = new FormSnapshotName())
			{
				form.SnapshotName = String.Format("{0} (Clone)", snapshotControl.Data.Name);
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var snapshot = snapshotControl.Data.Clone();
				snapshot.Name = form.SnapshotName;
				snapshot.Index += 0.5;
				_localSchedule.Snapshots.Add(snapshot);
				_localSchedule.RebuildSnapshotIndexes();
				var newControl = AddSnapshotControl(snapshot, (Int32)snapshot.Index);
				xtraTabControlSnapshots.SelectedTabPage = newControl;
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

		private SnapshotControl AddSnapshotControl(Snapshot data, int position = -1)
		{
			var snapshotControl = new SnapshotControl(data);
			snapshotControl.DataChanged += (o, e) =>
			{
				var sourceControl = o as SnapshotControl;
				if (sourceControl == null) return;
				if (!_allowToSave) return;
				if (_localSchedule.SnapshotSummary.ApplySettingsForAll)
				{
					ApplySharedSettings(sourceControl);
					xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().Where(oc => oc.Data.UniqueID != sourceControl.Data.UniqueID).ToList().ForEach(oc => oc.UpdateView());
				}
				UpdateTotalsValues();
				UpdateOutputStatus();
				SettingsNotSaved = true;
			};
			position = position == -1 ? xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().Count() : position;
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
			{
				pnSnapshots.BringToFront();
				Controller.Instance.SnapshotProgramAdd.Enabled = true;
				Controller.Instance.SnapshotProgramDelete.Enabled = true;
			}
			else
			{
				pnNoSnapshots.BringToFront();
				Controller.Instance.SnapshotProgramAdd.Enabled = false;
				Controller.Instance.SnapshotProgramDelete.Enabled = false;
			}
		}

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.SnapshotTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));
		}
		
		private void InitColorsControl()
		{
			xtraTabPageOptionsStyle.PageVisible = BusinessObjects.Instance.OutputManager.SnapshotColors.Items.Count > 1;
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.SnapshotColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		private void SaveColors()
		{
			MediaMetaData.Instance.SettingsManager.SelectedColor = outputColorSelector.SelectedColor ?? String.Empty;
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}

		private void LoadBarButtons()
		{
			var buttonInfos = new List<ButtonInfo>();
			buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsInfo, Tooltip = "Open Schedule Info", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsInfo; } });
			if (BusinessObjects.Instance.OutputManager.SnapshotColors.Items.Count > 1)
				buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsOptions, Tooltip = "Open Options", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsStyle; } });
			if (ActiveSnapshot != null)
			{
				xtraTabPageOptionsActiveWeeks.PageVisible = true;
				buttonInfos.Add(new ButtonInfo { Logo = Resources.SnapshotSettingsActiveWeeks, Tooltip = "Calendar", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsActiveWeeks; } });
			}
			else
				xtraTabPageOptionsActiveWeeks.PageVisible = false;
			retractableBarControl.AddButtons(buttonInfos);
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
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
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
			BusinessObjects.Instance.HelpManager.OpenHelpLink("snapshot");
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

		public void Pdf_Click(object sender, EventArgs e)
		{
			PrintPdf();
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

		private void toolStripMenuItemSnapshotClone_Click(object sender, EventArgs e)
		{
			CloneSnapshot(_menuHitInfo.Page as SnapshotControl);
			SettingsNotSaved = true;
		}

		private void hyperLinkEditInfoAdvanced_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormOutputSettings())
			{
				if (ActiveSnapshot != null)
				{
					form.checkEditUseDecimalRate.Checked = ActiveSnapshot.Data.UseDecimalRates;
					form.checkEditShowSpotX.Checked = ActiveSnapshot.Data.ShowSpotsX;
					form.checkEditShowSpotsPerWeek.Enabled = true;
					form.checkEditShowSpotsPerWeek.Checked = ActiveSnapshot.Data.ShowSpotsPerWeek;
					form.checkEditApplyForAll.Enabled = xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().Count() > 1;
					form.checkEditApplyForAll.Checked = _localSchedule.SnapshotSummary.ApplySettingsForAll;
				}
				else if (ActiveSummary != null)
				{
					form.checkEditApplyForAll.Enabled = false;
					form.checkEditShowSpotsPerWeek.Enabled = false;
					form.checkEditUseDecimalRate.Checked = ActiveSummary.Data.UseDecimalRates;
					form.checkEditShowSpotX.Checked = ActiveSummary.Data.ShowSpotsX;
				}
				else
					return;
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
				if (form.ShowDialog() != DialogResult.OK) return;
				if (ActiveSnapshot != null)
				{
					ActiveSnapshot.Data.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
					ActiveSnapshot.Data.ShowSpotsX = form.checkEditShowSpotX.Checked;
					ActiveSnapshot.Data.ShowSpotsPerWeek = form.checkEditShowSpotsPerWeek.Checked;
					_localSchedule.SnapshotSummary.ApplySettingsForAll = form.checkEditApplyForAll.Checked;
					if (_localSchedule.SnapshotSummary.ApplySettingsForAll)
					{
						ApplySharedSettings(ActiveSnapshot);
						xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().ToList().ForEach(oc => oc.UpdateView());
					}
					else
						ActiveSnapshot.UpdateView();
				}
				else if (ActiveSummary != null)
				{
					ActiveSummary.Data.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
					ActiveSummary.Data.ShowSpotsX = form.checkEditShowSpotX.Checked;
					ActiveSummary.UpdateView();
				}
				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;
				UpdateTotalsValues();
				SettingsNotSaved = true;
			}
		}

		private void hyperLinkEditInfoContract_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormContractSettings())
			{
				if (ActiveSnapshot != null)
				{
					form.checkEditShowSignatureLine.Checked = ActiveSnapshot.ContractSettings.ShowSignatureLine;
					form.checkEditShowRatesExpiration.Checked = ActiveSnapshot.ContractSettings.RateExpirationDate.HasValue;
					form.checkEditShowDisclaimer.Checked = ActiveSnapshot.ContractSettings.ShowDisclaimer;
					form.dateEditRatesExpirationDate.EditValue = ActiveSnapshot.ContractSettings.RateExpirationDate;
				}
				else if (ActiveSummary != null)
				{
					form.checkEditShowSignatureLine.Checked = ActiveSummary.ContractSettings.ShowSignatureLine;
					form.checkEditShowRatesExpiration.Checked = ActiveSummary.ContractSettings.RateExpirationDate.HasValue;
					form.checkEditShowDisclaimer.Checked = ActiveSummary.ContractSettings.ShowDisclaimer;
					form.dateEditRatesExpirationDate.EditValue = ActiveSummary.ContractSettings.RateExpirationDate;
				}
				else
					return;
				if (form.ShowDialog() != DialogResult.OK) return;
				if (ActiveSnapshot != null)
				{
					ActiveSnapshot.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
					ActiveSnapshot.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
					ActiveSnapshot.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
					if (_localSchedule.SnapshotSummary.ApplySettingsForAll)
						ApplySharedContractSettings(ActiveSnapshot.ContractSettings);
				}
				else if (ActiveSummary != null)
				{
					ActiveSummary.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
					ActiveSummary.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
					ActiveSummary.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;

					if (_localSchedule.SnapshotSummary.ApplySettingsForAll)
						ApplySharedContractSettings(ActiveSummary.ContractSettings);
				}
				SettingsNotSaved = true;
			}
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}

		private void checkedListBoxActiveWeeks_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			laActiveWeeksWarning.Visible = checkedListBoxActiveWeeks.CheckedItems.Count == 0;
			OnInfoSettingsChanged(sender, e);
		}

		private void buttonXSelectAll_Click(object sender, EventArgs e)
		{
			checkedListBoxActiveWeeks.CheckAll();
		}

		private void buttonXClearAll_Click(object sender, EventArgs e)
		{
			checkedListBoxActiveWeeks.UnCheckAll();
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

				ActiveSnapshot.Data.ActiveWeeks.Clear();
				if (checkedListBoxActiveWeeks.CheckedItems.Count != checkedListBoxActiveWeeks.ItemCount)
					ActiveSnapshot.Data.ActiveWeeks.AddRange(checkedListBoxActiveWeeks.CheckedItems.OfType<CheckedListBoxItem>().Select(item => item.Value).OfType<DateRange>());

				if (_localSchedule.SnapshotSummary.ApplySettingsForAll)
				{
					ApplySharedSettings(ActiveSnapshot);
					xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().ToList().ForEach(oc => oc.UpdateView());
				}
				else
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
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType))); }
		}

		private void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabSnapshot.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.ProgramSchedule.Sections.SelectMany(s => s.Programs).Any())
			{
				options.Add("TotalSpots", _localSchedule.ProgramSchedule.TotalSpots);
				options.Add("AverageRate", _localSchedule.ProgramSchedule.AvgRate);
				options.Add("GrossInvestment", _localSchedule.ProgramSchedule.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
		}

		private void TrackPreview()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabSnapshot.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.ProgramSchedule.Sections.SelectMany(s => s.Programs).Any())
			{
				options.Add("TotalSpots", _localSchedule.ProgramSchedule.TotalSpots);
				options.Add("AverageRate", _localSchedule.ProgramSchedule.AvgRate);
				options.Add("GrossInvestment", _localSchedule.ProgramSchedule.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}


		private void UpdateOutputStatus()
		{
			Controller.Instance.SnapshotPowerPoint.Enabled =
				Controller.Instance.SnapshotPdf.Enabled =
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

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var snapshotSlide in selectedSnapshots)
					snapshotSlide.Output(SelectedTheme);
				FormProgress.CloseProgress();
			});
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

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			var trackAction = new Action(TrackPreview);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, trackAction))
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

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
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

		private void PrintPdf()
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

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = new List<PreviewGroup>();
				previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", _localSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}
		#endregion
	}
}
