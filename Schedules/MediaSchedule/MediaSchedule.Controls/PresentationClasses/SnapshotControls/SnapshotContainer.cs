using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab.ViewInfo;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.Properties;
using DateRange = Asa.Business.Common.Entities.NonPersistent.Common.DateRange;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls
{
	[ToolboxItem(false)]
	public partial class SnapshotContainer : BasePartitionEditControl<SnapshotContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
	//public partial class SnapshotContainer : UserControl
	{
		private bool _allowToSave;
		private XtraTabHitInfo _menuHitInfo;
		private XtraTabDragDropHelper<SnapshotControl> _tabDragDropHelper;

		private MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;

		private MediaScheduleSettings ScheduleSettings => Schedule.Settings;

		public override string Identifier => ContentIdentifiers.Snapshots;

		public override RibbonTabItem TabPage => Controller.Instance.TabSnapshot;

		public SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVSnapshot : SlideType.RadioSnapshot;

		private SnapshotControl ActiveSnapshot => xtraTabControlSnapshots.SelectedTabPage as SnapshotControl;

		private SnapshotSummaryControl ActiveSummary => xtraTabControlSnapshots.SelectedTabPage as SnapshotSummaryControl;

		private SnapshotSummaryControl Summary => xtraTabControlSnapshots.TabPages.OfType<SnapshotSummaryControl>().Single();

		public SnapshotContainer()
		{
			InitializeComponent();

			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				font = new Font(laAvgRateTitle.Font.FontFamily, laAvgRateTitle.Font.Size - 2, laAvgRateTitle.Font.Style);
				laTotalSpotsTitle.Font = font;
				laAvgRateTitle.Font = font;
				laTotalCostTitle.Font = font;
				font = new Font(laAvgRateValue.Font.FontFamily, laAvgRateValue.Font.Size - 2, laAvgRateValue.Font.Style);
				laTotalSpotsValue.Font = font;
				laAvgRateValue.Font = font;
				laTotalCostValue.Font = font;

				laColorsTitle.Font = new Font(laColorsTitle.Font.FontFamily, laColorsTitle.Font.Size - 2, laColorsTitle.Font.Style);
				labelControlScheduleInfo.Font = new Font(labelControlScheduleInfo.Font.FontFamily,
					labelControlScheduleInfo.Font.Size - 2, labelControlScheduleInfo.Font.Style);
				laActiveWeeks.Font = new Font(laActiveWeeks.Font.FontFamily, laActiveWeeks.Font.Size - 2, laActiveWeeks.Font.Style);
				laActiveWeeksWarning.Font = new Font(laActiveWeeksWarning.Font.FontFamily, laActiveWeeksWarning.Font.Size - 2,
					laActiveWeeksWarning.Font.Style);

				font = new Font(buttonXSnapshotAvgRate.Font.FontFamily, buttonXSnapshotAvgRate.Font.Size - 2,
					buttonXSnapshotAvgRate.Font.Style);
				buttonXSnapshotAvgRate.Font = font;
				buttonXSnapshotCost.Font = font;
				buttonXSnapshotDaypart.Font = font;
				buttonXSnapshotLength.Font = font;
				buttonXSnapshotLineId.Font = font;
				buttonXSnapshotLogo.Font = font;
				buttonXSnapshotProgram.Font = font;
				buttonXSnapshotRate.Font = font;
				buttonXSnapshotStation.Font = font;
				buttonXSnapshotTime.Font = font;
				buttonXSnapshotTotalRow.Font = font;
				buttonXSnapshotTotalSpots.Font = font;
				buttonXSummaryCampaign.Font = font;
				buttonXSummaryComments.Font = font;
				buttonXSummaryCost.Font = font;
				buttonXSummaryLineId.Font = font;
				buttonXSummaryLogo.Font = font;
				buttonXSummarySpots.Font = font;
				buttonXSummaryTallyCost.Font = font;
				buttonXSummaryTallySpots.Font = font;
				buttonXSummaryTotalCost.Font = font;
				buttonXSummaryTotalWeeks.Font = font;

				buttonXSelectAll.Font = font;
				buttonXClearAll.Font = font;

				hyperLinkEditInfoAdvanced.Font = new Font(hyperLinkEditInfoAdvanced.Font.FontFamily,
					hyperLinkEditInfoAdvanced.Font.Size - 2, hyperLinkEditInfoAdvanced.Font.Style);
				hyperLinkEditInfoContract.Font = new Font(hyperLinkEditInfoContract.Font.FontFamily,
					hyperLinkEditInfoContract.Font.Size - 2, hyperLinkEditInfoContract.Font.Style);
			}
		}

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			pnNoSnapshots.Dock = DockStyle.Fill;
			pnSnapshots.Dock = DockStyle.Fill;
			retractableBarControl.Collapse(true);
			_tabDragDropHelper = new XtraTabDragDropHelper<SnapshotControl>(xtraTabControlSnapshots);
			_tabDragDropHelper.TabMoved += OnTabMoved;
			InitColorsControl();
			BusinessObjects.Instance.OutputManager.ColorsChanged += (o, e) =>
			{
				InitColorsControl();
				LoadBarButtons();
			};
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();
			hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();

			Controller.Instance.SnapshotNew.Click += OnAddSnapshotClick;
			Controller.Instance.SnapshotProgramAdd.Click += OnAddProgramClick;
			Controller.Instance.SnapshotProgramDelete.Click += OnDeleteProgramClick;
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged);

			if (EditedContent != null)
				EditedContent.Dispose();
			EditedContent = Schedule.SnapshotContent.Clone<SnapshotContent, SnapshotContent>();

			labelControlScheduleInfo.Text = String.Format("{0}{3}<color=gray><i>{1} ({2})</i></color>",
				ScheduleSettings.BusinessName,
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", ScheduleSettings.TotalWeeks, "week"),
				Environment.NewLine);

			LoadSnapshots(quickLoad);

			_allowToSave = true;

			LoadActiveTabData();
		}

		protected override void ApplyChanges()
		{
			foreach (var snapshotControl in xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>())
				snapshotControl.SaveData();
			Summary.SaveData();
			SaveColors();

			ChangeInfo.SnapshotsChanged = ChangeInfo.SnapshotsChanged || SettingsNotSaved;
		}

		protected override void SaveData()
		{
			Schedule.SnapshotContent = EditedContent.Clone<SnapshotContent, SnapshotContent>();
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("snapshot");
		}

		protected override void LoadThemes()
		{
			FormThemeSelector.Link(Controller.Instance.SnapshotTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.SnapshotThemeBar.RecalcLayout();
			Controller.Instance.SnapshotPanel.PerformLayout();
		}
		#endregion

		#region Snapshots Processing
		private void LoadSnapshots(bool quickLoad)
		{
			if (quickLoad)
			{
				Summary.LoadData(EditedContent.SnapshotSummary);
			}
			else
			{
				xtraTabControlSnapshots.TabPages
					.OfType<ISnapshotSlideControl>()
					.ToList()
					.ForEach(sc => sc.Release());
				xtraTabControlSnapshots.TabPages.Clear();
				xtraTabControlSnapshots.TabPages.Add(new SnapshotSummaryControl(EditedContent.SnapshotSummary));
				Summary.DataChanged += (o, e) =>
				{
					if (!_allowToSave) return;
					UpdateTotalsValues();
					SettingsNotSaved = true;
				};
			}

			foreach (var snapshot in EditedContent.Snapshots.OrderBy(s => s.Index))
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
			UpdateSummaryState();
			UpdateProgramCollectionButtons();
		}

		private void LoadActiveTabData(bool activate = false)
		{
			_allowToSave = false;

			pnApplyForAll.Visible = EditedContent.Snapshots.Count > 1 && ActiveSnapshot != null;
			checkEditApplyForAll.Checked = EditedContent.SnapshotSummary.ApplySettingsForAll;

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
				var scheduleWeekRanges = ScheduleSettings.GetWeeks();
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
			UpdateProgramCollectionButtons();
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
				snapshotControl.Data.CloneLineToTheEnd = templateControl.Data.CloneLineToTheEnd;
			}
		}

		private void ApplySharedContractSettings(ContractSettings templateSettings)
		{
			foreach (var snapshotControl in xtraTabControlSnapshots.TabPages.OfType<ISnapshotSlideControl>())
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
				var snapshot = new Snapshot(EditedContent);
				snapshot.Name = form.SnapshotName;
				EditedContent.Snapshots.Add(snapshot);
				EditedContent.RebuildSnapshotIndexes();
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
				var snapshot = snapshotControl.Data.Clone<Snapshot, Snapshot>();
				snapshot.Name = form.SnapshotName;
				snapshot.Index += 0.5;
				EditedContent.Snapshots.Add(snapshot);
				EditedContent.RebuildSnapshotIndexes();
				var newControl = AddSnapshotControl(snapshot, (Int32)snapshot.Index);
				xtraTabControlSnapshots.SelectedTabPage = newControl;
				Summary.UpdateView();
			}
		}

		private void DeleteSnapshot(SnapshotControl snapshotControl)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", snapshotControl.Data.Name) != DialogResult.Yes) return;
			EditedContent.Snapshots.Remove(snapshotControl.Data);
			EditedContent.RebuildSnapshotIndexes();
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
				if (EditedContent.SnapshotSummary.ApplySettingsForAll)
				{
					ApplySharedSettings(sourceControl);
					xtraTabControlSnapshots.TabPages.OfType<SnapshotControl>().Where(oc => oc.Data.UniqueID != sourceControl.Data.UniqueID).ToList().ForEach(oc => oc.UpdateView());
				}
				UpdateSummaryState();
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
			if (EditedContent.Snapshots.Any())
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

		private void UpdateSummaryState()
		{
			Summary.PageEnabled = EditedContent.Snapshots.SelectMany(s => s.Programs).Any();
		}

		private void UpdateProgramCollectionButtons()
		{
			Controller.Instance.SnapshotProgramAdd.Enabled =
			Controller.Instance.SnapshotProgramDelete.Enabled = ActiveSnapshot != null;
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
		private void OnAddSnapshotClick(object sender, EventArgs e)
		{
			AddSnapshot();
			UpdateSnapshotSplash();
			SettingsNotSaved = true;
		}

		private void OnAddProgramClick(object sender, EventArgs e)
		{
			ActiveSnapshot?.AddProgram();
		}

		private void OnDeleteProgramClick(object sender, EventArgs e)
		{
			ActiveSnapshot?.DeleteProgram();
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
			UpdateSummaryState();
			SettingsNotSaved = true;
		}

		private void OnSelectedTabPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
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
			EditedContent.ChangeSnapshotPosition(
				EditedContent.Snapshots.IndexOf(((SnapshotControl)e.MovedPage).Data),
				EditedContent.Snapshots.IndexOf(((SnapshotControl)e.TargetPage).Data) + (1 * e.Offset));
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
					form.labelControlDescriptionShowSpotsPerWeek.Enabled = true;
					form.checkEditShowSpotsPerWeek.Checked = ActiveSnapshot.Data.ShowSpotsPerWeek;
					form.checkEditCloneLineToTheEnd.Enabled = true;
					form.labelControlDescriptionCloneLineToTheEnd.Enabled = true;
					form.checkEditCloneLineToTheEnd.Checked = ActiveSnapshot.Data.CloneLineToTheEnd;
				}
				else if (ActiveSummary != null)
				{
					form.checkEditShowSpotsPerWeek.Enabled = false;
					form.labelControlDescriptionShowSpotsPerWeek.Enabled = false;
					form.checkEditUseDecimalRate.Checked = ActiveSummary.Data.UseDecimalRates;
					form.checkEditShowSpotX.Checked = ActiveSummary.Data.ShowSpotsX;
					form.checkEditCloneLineToTheEnd.Enabled = false;
					form.labelControlDescriptionCloneLineToTheEnd.Enabled = false;
					form.checkEditCloneLineToTheEnd.Checked = false;
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
					ActiveSnapshot.Data.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
					if (EditedContent.SnapshotSummary.ApplySettingsForAll)
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
					if (EditedContent.SnapshotSummary.ApplySettingsForAll)
						ApplySharedContractSettings(ActiveSnapshot.ContractSettings);
				}
				else if (ActiveSummary != null)
				{
					ActiveSummary.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
					ActiveSummary.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
					ActiveSummary.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;

					if (EditedContent.SnapshotSummary.ApplySettingsForAll)
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

			EditedContent.SnapshotSummary.ApplySettingsForAll = checkEditApplyForAll.Checked;

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
					ActiveSnapshot.Data.ActiveWeeks.AddRange(checkedListBoxActiveWeeks.CheckedItems
						.OfType<CheckedListBoxItem>()
						.Select(item => item.Value).OfType<DateRange>());

				if (EditedContent.SnapshotSummary.ApplySettingsForAll)
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

		private void UpdateOutputStatus()
		{
			Controller.Instance.SnapshotPowerPoint.Enabled =
				Controller.Instance.SnapshotPdf.Enabled =
				Controller.Instance.SnapshotPreview.Enabled =
				Controller.Instance.SnapshotEmail.Enabled = xtraTabControlSnapshots.TabPages.OfType<ISnapshotSlideControl>().Any(ss => ss.ReadyForOutput);
		}

		public override void OutputPowerPoint()
		{
			var tabPages = xtraTabControlSnapshots.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<ISnapshotSlideControl>().Where(ss => ss.ReadyForOutput);
			var selectedSnapshots = new List<ISnapshotSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Snapshots";
					var currentSnapshots = xtraTabControlSnapshots.SelectedTabPage as ISnapshotSlideControl;
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
							OfType<ISnapshotSlideControl>());
				}
			else
				selectedSnapshots.AddRange(tabPages);
			if (!selectedSnapshots.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var snapshotSlide in selectedSnapshots)
					snapshotSlide.Output(SelectedTheme);
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var tabPages = xtraTabControlSnapshots.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<ISnapshotSlideControl>().Where(ss => ss.ReadyForOutput);
			var selectedSnapshots = new List<ISnapshotSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Snapshots";
					var currentSnapshots = xtraTabControlSnapshots.SelectedTabPage as ISnapshotSlideControl;
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
							OfType<ISnapshotSlideControl>());
				}
			else
				selectedSnapshots.AddRange(tabPages);
			if (!selectedSnapshots.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = new List<PreviewGroup>();
				previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf", Schedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
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

		public override void Preview()
		{
			var tabPages = xtraTabControlSnapshots.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<ISnapshotSlideControl>().Where(ss => ss.ReadyForOutput);
			var selectedSnapshots = new List<ISnapshotSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Snapshots";
					var currentSnapshots = xtraTabControlSnapshots.SelectedTabPage as ISnapshotSlideControl;
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
							OfType<ISnapshotSlideControl>());
				}
			else
				selectedSnapshots.AddRange(tabPages);
			if (!selectedSnapshots.Any()) return;
			var previewGroups = new List<PreviewGroup>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Snapshots";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			}
		}

		public override void Email()
		{
			var tabPages = xtraTabControlSnapshots.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<ISnapshotSlideControl>().Where(ss => ss.ReadyForOutput);
			var selectedSnapshots = new List<ISnapshotSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Snapshots";
					var currentSnapshots = xtraTabControlSnapshots.SelectedTabPage as ISnapshotSlideControl;
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
							OfType<ISnapshotSlideControl>());
				}
			else
				selectedSnapshots.AddRange(tabPages);
			if (!selectedSnapshots.Any()) return;
			var previewGroups = new List<PreviewGroup>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			previewGroups.AddRange(selectedSnapshots.Select(snapshotSlide => snapshotSlide.GetPreviewGroup(SelectedTheme)));
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email these Snapshots";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
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
