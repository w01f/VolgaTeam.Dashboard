using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using DevComponents.DotNetBar;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	[ToolboxItem(false)]
	public partial class SnapshotContentEditorsContainer : BasePartitionEditControl<SnapshotContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>, IMultipleSlidesOutputControl
	//public partial class SnapshotContentEditorsContainer : UserControl
	{
		private bool _allowToSave;
		private XtraTabHitInfo _menuHitInfo;
		private XtraTabDragDropHelper<SnapshotEditorsContainer> _tabDragDropHelper;

		#region Properties
		private MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;

		private MediaScheduleSettings ScheduleSettings => Schedule.Settings;

		public override string Identifier => ContentIdentifiers.Snapshots;

		public override RibbonTabItem TabPage => Controller.Instance.TabSnapshot;

		private ISnapshotContentEditorControl ActiveContentEditor => xtraTabControlContentEditors.SelectedTabPage as ISnapshotContentEditorControl;
		private SnapshotEditorsContainer ActiveSnapshotContainer => xtraTabControlContentEditors.SelectedTabPage as SnapshotEditorsContainer;
		private SnapshotSummaryEditorControl ActiveSummary => xtraTabControlContentEditors.SelectedTabPage as SnapshotSummaryEditorControl;
		private SnapshotSummaryEditorControl Summary => xtraTabControlContentEditors.TabPages.OfType<SnapshotSummaryEditorControl>().Single();
		#endregion

		public SnapshotContentEditorsContainer()
		{
			InitializeComponent();
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			retractableBarControl.simpleButtonExpand.Image = BusinessObjects.Instance.ImageResourcesManager.RetractableBarExpandImage ??
															 retractableBarControl.simpleButtonExpand.Image;
			retractableBarControl.simpleButtonCollapse.Image = BusinessObjects.Instance.ImageResourcesManager.RetractableBarCollpaseImage ??
															   retractableBarControl.simpleButtonCollapse.Image;
			retractableBarControl.ContentSize = retractableBarControl.Width;
			retractableBarControl.Collapse(true);

			_tabDragDropHelper = new XtraTabDragDropHelper<SnapshotEditorsContainer>(xtraTabControlContentEditors);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			Controller.Instance.SnapshotNew.Click += OnAddSnapshotSet;
			Controller.Instance.SnapshotProgramAdd.Click += OnAddItem;
			Controller.Instance.SnapshotProgramDelete.Click += OnDeleteItem;

			pictureEditDefaultLogo.Image = BusinessObjects.Instance.ImageResourcesManager.SnapshotsNoRecordsLogo ?? pictureEditDefaultLogo.Image;
		}

		public override void ShowControl(ContentOpenEventArgs args = null)
		{
			base.ShowControl(args);
			var snapshotOpenEventArgs = args as SnapshotOpenEventArgs;
			if (snapshotOpenEventArgs == null) return;
			var editorsContainer = xtraTabControlContentEditors.TabPages
				.OfType<SnapshotEditorsContainer>()
				.FirstOrDefault(c => c.SnapshotData.UniqueID == snapshotOpenEventArgs.SnapshotId);
			if (editorsContainer == null) return;
			xtraTabControlContentEditors.SelectedTabPage = editorsContainer;
			editorsContainer.ShowEditor(snapshotOpenEventArgs.EditorType);
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SnapshotsChanged);

			EditedContent?.Dispose();
			EditedContent = Schedule.SnapshotContent.Clone<SnapshotContent, SnapshotContent>();

			settingsContainer.LoadContent(EditedContent);

			LoadContentEditors(quickLoad);

			_allowToSave = true;

			LoadActiveEditorData();
		}

		protected override void ApplyChanges()
		{
			foreach (var editorsContainer in xtraTabControlContentEditors.TabPages.OfType<SnapshotEditorsContainer>())
				editorsContainer.SaveData();
			Summary.SaveData();

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
			base.LoadThemes();

			FormThemeSelector.Link(
				Controller.Instance.SnapshotTheme,
				Controller.Instance.FormMain,
				BusinessObjects.Instance.ThemeManager.GetThemes(SlideType),
				MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType),
				MediaMetaData.Instance.SettingsManager,
				(theme, applyForAllSlideTypes) =>
				{
					MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, theme.Name, applyForAllSlideTypes);
					MediaMetaData.Instance.SettingsManager.SaveSettings();
					if (applyForAllSlideTypes)
						Controller.Instance.ContentController.RaiseThemeChanged();
				}
			);
			Controller.Instance.SnapshotThemeBar.RecalcLayout();
			Controller.Instance.SnapshotPanel.PerformLayout();
		}

		protected override void UpdateMenuOutputButtons()
		{
			UpdateOutputStatus();
		}
		#endregion

		#region Editors Management
		private void LoadContentEditors(bool quickLoad)
		{
			if (quickLoad)
			{
				foreach (var snapshot in EditedContent.Snapshots.OrderBy(s => s.Index))
				{
					var sectionTabControl = xtraTabControlContentEditors.TabPages
						.OfType<SnapshotEditorsContainer>()
						.FirstOrDefault(sc => sc.SnapshotData.UniqueID.Equals(snapshot.UniqueID));
					sectionTabControl?.LoadData(snapshot);
				}
				Summary.LoadData(EditedContent.SnapshotSummary);
			}
			else
			{
				xtraTabControlContentEditors.TabPages
					.OfType<ISnapshotContentEditorControl>()
					.ToList()
					.ForEach(sc => sc.Release());
				xtraTabControlContentEditors.TabPages.Clear();


				foreach (var snapshot in EditedContent.Snapshots.OrderBy(s => s.Index))
					AddSnapshotSetEditorsContainerControl(snapshot);

				xtraTabControlContentEditors.TabPages.Add(new SnapshotSummaryEditorControl());
				Summary.InitControls();
				Summary.LoadData(EditedContent.SnapshotSummary);
				Summary.DataChanged += (o, e) =>
				{
					if (!_allowToSave) return;
					UpdateStatusBar();
					SettingsNotSaved = true;
				};

				xtraTabControlContentEditors.SelectedTabPageIndex = 0;
			}
			UpdateSplash();
			UpdateSummaryState();
			UpdateCollectionChangeButtons();
		}

		private void LoadActiveEditorData()
		{
			_allowToSave = false;
			if (ActiveSnapshotContainer != null)
			{
				settingsContainer.LoadSnapshot(ActiveSnapshotContainer.SnapshotData);
				UpdateStatusBar();
			}
			OnContentEditorChanged(ActiveContentEditor, EventArgs.Empty);
			UpdateOutputStatus();
			_allowToSave = true;
		}

		private SnapshotEditorsContainer AddSnapshotSetEditorsContainerControl(Snapshot data, int position = -1)
		{
			var snapshotEditorsContainer = new SnapshotEditorsContainer();
			snapshotEditorsContainer.InitControls();
			snapshotEditorsContainer.LoadData(data);
			snapshotEditorsContainer.DataChanged += OnSnapshotSetDataChanged;
			snapshotEditorsContainer.SelectedEditorChanged += OnContentEditorChanged;
			position = position == -1 ? xtraTabControlContentEditors.TabPages.OfType<SnapshotEditorsContainer>().Count() : position;
			xtraTabControlContentEditors.TabPages.Insert(position, snapshotEditorsContainer);
			return snapshotEditorsContainer;
		}

		private void AddSnapshotSet()
		{
			using (var form = new FormSnapshotName())
			{
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var snapshot = new Snapshot(EditedContent);
				snapshot.Name = form.SnapshotName;
				EditedContent.Snapshots.Add(snapshot);
				EditedContent.RebuildSnapshotIndexes();
				var snapshotEditorsContainer = AddSnapshotSetEditorsContainerControl(snapshot);
				xtraTabControlContentEditors.SelectedTabPage = snapshotEditorsContainer;
				Summary.UpdateView();
				UpdateSplash();
				SettingsNotSaved = true;
			}
		}

		private void CloneSnapshotSet(SnapshotEditorsContainer snapshotEditorsContainer)
		{
			using (var form = new FormSnapshotName())
			{
				form.SnapshotName = String.Format("{0} (Clone)", snapshotEditorsContainer.SnapshotData.Name);
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var snapshot = snapshotEditorsContainer.SnapshotData.Clone<Snapshot, Snapshot>();
				snapshot.Name = form.SnapshotName;
				snapshot.Index += 0.5;
				EditedContent.Snapshots.Add(snapshot);
				EditedContent.RebuildSnapshotIndexes();
				var newControl = AddSnapshotSetEditorsContainerControl(snapshot, (Int32)snapshot.Index);
				xtraTabControlContentEditors.SelectedTabPage = newControl;
				Summary.UpdateView();
				SettingsNotSaved = true;
			}
		}

		private void DeleteSnapshotSet(SnapshotEditorsContainer snapshotEditorsContainer)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", snapshotEditorsContainer.SnapshotData.Name) != DialogResult.Yes) return;
			EditedContent.Snapshots.Remove(snapshotEditorsContainer.SnapshotData);
			EditedContent.RebuildSnapshotIndexes();
			xtraTabControlContentEditors.TabPages.Remove(snapshotEditorsContainer);
			Summary.UpdateView();
			UpdateSplash();
			UpdateSummaryState();
			UpdateStatusBar();
			settingsContainer.UpdateSettingsAccordingDataChanges(SnapshotEditorType.Schedule);
			SettingsNotSaved = true;
		}

		private void RenameSnapshotSet(SnapshotEditorsContainer snapshotEditorsContainer)
		{
			if (snapshotEditorsContainer == null) return;
			using (var form = new FormSnapshotName())
			{
				form.SnapshotName = snapshotEditorsContainer.SnapshotData.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				snapshotEditorsContainer.SnapshotData.Name = form.SnapshotName;
				snapshotEditorsContainer.Text = form.SnapshotName;
				settingsContainer.UpdateSettingsAccordingDataChanges(SnapshotEditorType.Schedule);
				SettingsNotSaved = true;
			}
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.SnapshotPowerPoint.Enabled =
				Controller.Instance.MenuOutputPdfButton.Enabled =
				Controller.Instance.MenuEmailButton.Enabled =
				xtraTabControlContentEditors.TabPages.OfType<IOutputContainer>().Any(oc => oc.GetOutputGroup().Items.Any());
		}

		private void UpdateSummaryState()
		{
			Summary.PageEnabled = EditedContent.Snapshots.SelectMany(o => o.Programs).Any();
		}

		private void OnSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			if (ActiveContentEditor == null) return;
			if (!_allowToSave) return;
			foreach (var sectionTabControl in xtraTabControlContentEditors.TabPages.OfType<ISnapshotContentEditorControl>())
				sectionTabControl.UpdateAccordingSettings(e);
			OnSnapshotSetDataChanged(sender, e);
		}

		private void OnSnapshotSetDataChanged(object sender, EventArgs e)
		{
			if (ActiveContentEditor == null) return;
			if (!_allowToSave) return;
			settingsContainer.UpdateSettingsAccordingDataChanges(ActiveContentEditor.ActiveEditor.EditorType);
			UpdateStatusBar();
			UpdateCollectionChangeButtons();
			UpdateOutputStatus();
			UpdateSummaryState();
			Summary.UpdateView();
			SettingsNotSaved = true;
		}

		private void OnContentEditorChanged(object sender, EventArgs e)
		{
			if (ActiveContentEditor == null) return;
			settingsContainer.UpdateSettingsAccordingSelectedEditor(ActiveContentEditor.ActiveEditor.EditorType);
			UpdateCollectionChangeButtons();
			UpdateStatusBar();
			LoadThemes();
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			EditedContent.ChangeSnapshotPosition(
				EditedContent.Snapshots.IndexOf(((SnapshotEditorsContainer)e.MovedPage).SnapshotData),
				EditedContent.Snapshots.IndexOf(((SnapshotEditorsContainer)e.TargetPage).SnapshotData) + (1 * e.Offset));
			Summary.UpdateView();
			SettingsNotSaved = true;
		}

		private void UpdateSplash()
		{
			if (EditedContent.Snapshots.Any())
			{
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
				layoutControlItemData.Visibility = LayoutVisibility.Always;
				Controller.Instance.SnapshotProgramAdd.Enabled = true;
				Controller.Instance.SnapshotProgramDelete.Enabled = true;
				Controller.Instance.SnapshotSettings.Enabled = true;
			}
			else
			{
				layoutControlItemData.Visibility = LayoutVisibility.Never;
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
				Controller.Instance.SnapshotProgramAdd.Enabled = false;
				Controller.Instance.SnapshotProgramDelete.Enabled = false;
				Controller.Instance.SnapshotSettings.Enabled = false;
			}
		}

		private void UpdateCollectionChangeButtons()
		{
			var activeItemCollection = ActiveContentEditor?.ActiveItemCollection;
			if (activeItemCollection == null)
			{
				Controller.Instance.SnapshotProgramAdd.Enabled =
					Controller.Instance.SnapshotProgramDelete.Enabled = false;
				((RibbonBar)(Controller.Instance.SnapshotProgramAdd.ContainerControl)).Text = "Program";
			}
			else
			{
				Controller.Instance.SnapshotProgramAdd.Enabled = activeItemCollection.AllowToAddItem;
				Controller.Instance.SnapshotProgramDelete.Enabled = activeItemCollection.AllowToDeleteItem;
				((RibbonBar)(Controller.Instance.SnapshotProgramAdd.ContainerControl)).Text = activeItemCollection.CollectionTitle;

				var addProductTitle = String.Format("Add {0}", activeItemCollection.CollectionItemTitle);
				var addProductTooltip = String.Format("Add a {0} to your schedule", activeItemCollection.CollectionItemTitle);
				var deleteProductTitle = String.Format("Delete {0}", activeItemCollection.CollectionItemTitle);
				var deleteProductTooltip = String.Format("Delete the selected {0} from your schedule", activeItemCollection.CollectionItemTitle);

				if (ActiveContentEditor.ActiveEditor.EditorType == SnapshotEditorType.DigitalInfo)
				{
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTitle))
						addProductTitle = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTitle;
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTooltip))
						addProductTooltip = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTooltip;
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTitle))
						deleteProductTitle = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTitle;
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTooltip))
						deleteProductTooltip = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTooltip;
				}
				Controller.Instance.Supertip.SetSuperTooltip(
					Controller.Instance.SnapshotProgramAdd,
					new SuperTooltipInfo(
						addProductTitle,
						"",
						addProductTooltip,
						null,
						null,
						eTooltipColor.Gray));
				Controller.Instance.Supertip.SetSuperTooltip(
					Controller.Instance.SnapshotProgramDelete,
					new SuperTooltipInfo(
						deleteProductTitle,
						"",
						deleteProductTooltip,
						null,
						null,
						eTooltipColor.Gray));
			}
		}

		private void OnSelectedContentEditorChanged(object sender, TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LoadActiveEditorData();
			LoadThemes();
		}

		private void OnContentEditorTabCloseClick(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			var snapshotEditorsContainer = (SnapshotEditorsContainer)arg.Page;
			DeleteSnapshotSet(snapshotEditorsContainer);
		}

		private void OnContentEditorTabMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControlContentEditors.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			contextMenuStrip.Show((Control)sender, e.Location);
		}

		private void OnRenameSnapshotClick(object sender, EventArgs e)
		{
			RenameSnapshotSet((SnapshotEditorsContainer)_menuHitInfo.Page);
		}

		private void OnCloneSnapshotClick(object sender, EventArgs e)
		{
			CloneSnapshotSet((SnapshotEditorsContainer)_menuHitInfo.Page);
		}
		#endregion

		#region Settings management
		public override void EditSettings()
		{
			using (var form = new FormOutputSettings())
			{
				switch (ActiveContentEditor.ActiveEditor.EditorType)
				{
					case SnapshotEditorType.Schedule:
					case SnapshotEditorType.DigitalInfo:
						form.checkEditUseDecimalRate.Checked = ActiveSnapshotContainer.SnapshotData.UseDecimalRates;
						form.checkEditShowSpotX.Checked = ActiveSnapshotContainer.SnapshotData.ShowSpotsX;
						form.layoutControlItemShowSpotsPerWeek.Enabled = true;
						form.simpleLabelItemShowSpotsPerWeek.Enabled = true;
						form.checkEditShowSpotsPerWeek.Checked = ActiveSnapshotContainer.SnapshotData.ShowSpotsPerWeek;
						form.layoutControlItemCloneLineToTheEnd.Enabled = true;
						form.simpleLabelItemCloneLineToTheEnd.Enabled = true;
						form.checkEditCloneLineToTheEnd.Checked = ActiveSnapshotContainer.SnapshotData.CloneLineToTheEnd;

						form.checkEditShowSignatureLine.Checked = ActiveSnapshotContainer.SnapshotData.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = ActiveSnapshotContainer.SnapshotData.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = ActiveSnapshotContainer.SnapshotData.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = ActiveSnapshotContainer.SnapshotData.ContractSettings.RateExpirationDate;
						break;
					case SnapshotEditorType.Summary:
						form.layoutControlItemShowSpotsPerWeek.Enabled = false;
						form.simpleLabelItemShowSpotsPerWeek.Enabled = false;
						form.checkEditUseDecimalRate.Checked = EditedContent.SnapshotSummary.UseDecimalRates;
						form.checkEditShowSpotX.Checked = EditedContent.SnapshotSummary.ShowSpotsX;
						form.layoutControlItemCloneLineToTheEnd.Enabled = false;
						form.simpleLabelItemCloneLineToTheEnd.Enabled = false;
						form.checkEditCloneLineToTheEnd.Checked = false;

						form.checkEditShowSignatureLine.Checked = EditedContent.SnapshotSummary.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = EditedContent.SnapshotSummary.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = EditedContent.SnapshotSummary.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = EditedContent.SnapshotSummary.ContractSettings.RateExpirationDate;
						break;
					default:
						return;
				}
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
				if (form.ShowDialog() != DialogResult.OK) return;
				switch (ActiveContentEditor.ActiveEditor.EditorType)
				{
					case SnapshotEditorType.Schedule:
					case SnapshotEditorType.DigitalInfo:
						ActiveSnapshotContainer.SnapshotData.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						ActiveSnapshotContainer.SnapshotData.ShowSpotsX = form.checkEditShowSpotX.Checked;
						ActiveSnapshotContainer.SnapshotData.ShowSpotsPerWeek = form.checkEditShowSpotsPerWeek.Checked;
						ActiveSnapshotContainer.SnapshotData.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;

						ActiveSnapshotContainer.SnapshotData.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						ActiveSnapshotContainer.SnapshotData.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						ActiveSnapshotContainer.SnapshotData.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;

						if (EditedContent.SnapshotSummary.ApplySettingsForAll)
						{
							foreach (var snapshot in EditedContent.Snapshots.Where(os => os.UniqueID != ActiveSnapshotContainer.SnapshotData.UniqueID))
							{
								snapshot.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
								snapshot.ShowSpotsX = form.checkEditShowSpotX.Checked;
								snapshot.ShowSpotsPerWeek = form.checkEditShowSpotsPerWeek.Checked;
								snapshot.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;

								snapshot.ContractSettings.ShowSignatureLine = ActiveSnapshotContainer.SnapshotData.ContractSettings.ShowSignatureLine;
								snapshot.ContractSettings.ShowDisclaimer = ActiveSnapshotContainer.SnapshotData.ContractSettings.ShowDisclaimer;
								snapshot.ContractSettings.RateExpirationDate = ActiveSnapshotContainer.SnapshotData.ContractSettings.RateExpirationDate;
							}
						}
						break;
					case SnapshotEditorType.Summary:
						EditedContent.SnapshotSummary.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						EditedContent.SnapshotSummary.ShowSpotsX = form.checkEditShowSpotX.Checked;

						EditedContent.SnapshotSummary.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						EditedContent.SnapshotSummary.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						EditedContent.SnapshotSummary.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
						break;
				}
				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;
				OnSettingsChanged(this, new SettingsChangedEventArgs
				{
					ChangedSettingsType = SnapshotSettingsType.AdvancedColumns,
				});
			}
		}

		private void LoadBarButtons()
		{
			retractableBarControl.AddButtons(settingsContainer.GetSettingsButtons());
		}

		private void OnSettingsControlsUpdated(object sender, EventArgs e)
		{
			LoadBarButtons();
		}
		#endregion

		#region Ribbon Operations Events
		public void OnAddSnapshotSet(object sender, EventArgs e)
		{
			AddSnapshotSet();
		}

		public void OnAddItem(object sender, EventArgs e)
		{
			ActiveContentEditor?.ActiveItemCollection?.AddItem();
		}

		public void OnDeleteItem(object sender, EventArgs e)
		{
			ActiveContentEditor?.ActiveItemCollection?.DeleteItem();
		}
		#endregion

		#region Status Bar management

		private readonly LabelItem _statusBarActiveWeeksInfo = new LabelItem();
		private readonly LabelItem _statusBarWeeklySpotsInfo = new LabelItem();
		private readonly LabelItem _statusBarWeeklyCostInfo = new LabelItem();
		private readonly LabelItem _statusBarTotalSpotsInfo = new LabelItem();
		private readonly LabelItem _statusBarTotalCostInfo = new LabelItem();
		private readonly LabelItem _statusBarAvgRateInfo = new LabelItem();

		protected override void UpdateStatusBar()
		{
			if (ActiveSnapshotContainer != null && ActiveSnapshotContainer.SnapshotData.Programs.Any())
			{
				_statusBarActiveWeeksInfo.Text = String.Format("Active Weeks: {0}", ActiveSnapshotContainer.SnapshotData.TotalWeeks.ToString("#0"));
				_statusBarWeeklySpotsInfo.Text = String.Format("Weekly Spots: {0}", ActiveSnapshotContainer.SnapshotData.WeeklySpots.ToString("#,##0"));
				_statusBarWeeklyCostInfo.Text = String.Format("Weekly Cost: {0}", ActiveSnapshotContainer.SnapshotData.WeeklyCost.ToString(ActiveSnapshotContainer.SnapshotData.Parent.SnapshotSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0"));
				_statusBarTotalSpotsInfo.Text = String.Format("Total Spots: {0}", ActiveSnapshotContainer.SnapshotData.TotalSpots.ToString("#,##0"));
				_statusBarTotalCostInfo.Text = String.Format("Total Cost: {0}", ActiveSnapshotContainer.SnapshotData.TotalCost.ToString(ActiveSnapshotContainer.SnapshotData.Parent.SnapshotSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0"));
				_statusBarAvgRateInfo.Text = String.Format("Average Rate: {0}", ActiveSnapshotContainer.SnapshotData.AvgRate.ToString(ActiveSnapshotContainer.SnapshotData.UseDecimalRates ? "$#,##0.00" : "$#,##0"));

				var statusBarItems = new List<LabelItem>();

				statusBarItems.Add(_statusBarActiveWeeksInfo);
				if (ActiveSnapshotContainer.SnapshotData.ShowWeeklySpots)
					statusBarItems.Add(_statusBarWeeklySpotsInfo);
				if (ActiveSnapshotContainer.SnapshotData.ShowWeeklyCost)
					statusBarItems.Add(_statusBarWeeklyCostInfo);
				if (ActiveSnapshotContainer.SnapshotData.ShowTotalSpots)
					statusBarItems.Add(_statusBarTotalSpotsInfo);
				if (ActiveSnapshotContainer.SnapshotData.ShowTotalCost)
					statusBarItems.Add(_statusBarTotalCostInfo);
				if (ActiveSnapshotContainer.SnapshotData.ShowAverageRate)
					statusBarItems.Add(_statusBarAvgRateInfo);

				if (BusinessObjects.Instance.FormStyleManager.Style.StatusBarTextColor.HasValue)
					statusBarItems.ForEach(item => item.ForeColor = BusinessObjects.Instance.FormStyleManager.Style.StatusBarTextColor.Value);

				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Clear();
				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.AddRange(statusBarItems.ToArray());
				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.RecalcSize();
				ContentStatusBarManager.Instance.StatusBar.RecalcLayout();
			}
			else if (ActiveSummary != null)
			{
				_statusBarTotalSpotsInfo.Text = String.Format("Total Spots: {0}", ActiveSummary.Data.TotalSpots.ToString("#,##0"));
				_statusBarTotalCostInfo.Text = String.Format("Total Cost: {0}", ActiveSummary.Data.TotalCost.ToString(ActiveSummary.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0"));

				var statusBarItems = new List<LabelItem>();

				if (ActiveSummary.Data.ShowTallySpots)
					statusBarItems.Add(_statusBarTotalSpotsInfo);
				if (ActiveSummary.Data.ShowTallyCost)
					statusBarItems.Add(_statusBarTotalCostInfo);

				if (BusinessObjects.Instance.FormStyleManager.Style.StatusBarTextColor.HasValue)
					statusBarItems.ForEach(item => item.ForeColor = BusinessObjects.Instance.FormStyleManager.Style.StatusBarTextColor.Value);

				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Clear();
				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.AddRange(statusBarItems.ToArray());
				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.RecalcSize();
				ContentStatusBarManager.Instance.StatusBar.RecalcLayout();
			}
			else
				base.UpdateStatusBar();
		}
		#endregion

		#region Output Staff
		private SlideType SlideType => ActiveContentEditor?.ActiveEditor?.SlideType ??
				(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
					? SlideType.TVSnapshotPrograms
					: SlideType.RadioSnapshotPrograms);

		private List<OutputItem> GetOutputItems(bool onlyCurrentSlide)
		{
			var selectedOutputItems = new List<OutputItem>();

			var availableOutputGroups = new List<OutputGroup>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Slides...");
			FormProgress.ShowProgress(Controller.Instance.FormMain);
			if (onlyCurrentSlide)
			{
				if (ActiveSnapshotContainer != null)
					availableOutputGroups.Add(ActiveSnapshotContainer.GetOutputGroup());
			}
			else
			{
				var outputContainers = xtraTabControlContentEditors.TabPages
					.OfType<IOutputContainer>()
					.ToList();
				foreach (var outputContainer in outputContainers)
				{
					availableOutputGroups.Add(outputContainer.GetOutputGroup());
					Application.DoEvents();
				}
			}
			FormProgress.CloseProgress();

			if (!availableOutputGroups.Any())
				return selectedOutputItems;

			using (var form = new FormPreview(
				Controller.Instance.FormMain,
				BusinessObjects.Instance.PowerPointManager.Processor))
			{
				form.LoadGroups(availableOutputGroups);
				if (form.ShowDialog() == DialogResult.OK)
					selectedOutputItems.AddRange(form.GetSelectedItems());
			}

			return selectedOutputItems;
		}

		public override void OutputPowerPoint()
		{
			var outputItems = GetOutputItems(true);
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				outputItems.ForEach(item => item.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, null));
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPowerPointAll()
		{
			var outputItems = GetOutputItems(false);
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				outputItems.ForEach(item => item.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, null));
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var outputItems = GetOutputItems(false);
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", "star", DateTime.Now));
				BusinessObjects.Instance.PowerPointManager.Processor.BuildPdf(pdfFileName, presentation =>
				{
					foreach (var outputItem in outputItems)
						outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, presentation);
				});
				FormProgress.CloseProgress();
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
			});
		}

		public override void Email()
		{
			var outputItems = GetOutputItems(false);
			if (!outputItems.Any()) return;

			using (var form = new FormEmailFileName())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Email...");
					FormProgress.ShowProgress();
					Controller.Instance.ShowFloater(() =>
					{
						var emailFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", Schedule.Name, DateTime.Now));
						var defaultItem = outputItems.First();
						BusinessObjects.Instance.PowerPointManager.Processor.PreparePresentation(emailFileName, presentation =>
						{
							foreach (var outputItem in outputItems)
								outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, presentation);
						});

						var emailFile = Path.Combine(
							Path.GetFullPath(defaultItem.PresentationSourcePath)
								.Replace(Path.GetFileName(defaultItem.PresentationSourcePath), string.Empty),
							form.FileName + ".pptx");
						File.Copy(emailFileName, emailFile, true);

						FormProgress.CloseProgress();

						try
						{
							if (OutlookHelper.Instance.Open())
							{
								OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
								OutlookHelper.Instance.Close();
							}
							else
								PopupMessageHelper.Instance.ShowWarning("Cannot open Outlook");
							File.Delete(emailFile);
						}
						catch { }
					});
				}
			}
		}
		#endregion
	}
}