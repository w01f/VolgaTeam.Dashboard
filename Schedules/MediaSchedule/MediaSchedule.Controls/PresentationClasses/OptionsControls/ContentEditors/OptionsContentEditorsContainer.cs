using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Option;
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
using Asa.Media.Controls.PresentationClasses.OptionsControls.Output;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Settings;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using DevComponents.DotNetBar;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	[ToolboxItem(false)]
	public partial class OptionsContentEditorsContainer : BasePartitionEditControl<OptionsContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>, IMultipleSlidesOutputControl
	//public partial class OptionsContentEditorsContainer : UserControl
	{
		private bool _allowToSave;
		private XtraTabHitInfo _menuHitInfo;
		private XtraTabDragDropHelper<OptionSetEditorsContainer> _tabDragDropHelper;

		#region Properties
		private MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;

		private MediaScheduleSettings ScheduleSettings => Schedule.Settings;

		public override string Identifier => ContentIdentifiers.Options;

		public override RibbonTabItem TabPage => Controller.Instance.TabOptions;

		private IOptionContentEditorControl ActiveContentEditor => xtraTabControlContentEditors.SelectedTabPage as IOptionContentEditorControl;
		private OptionSetEditorsContainer ActiveOptionSetContainer => xtraTabControlContentEditors.SelectedTabPage as OptionSetEditorsContainer;
		private OptionsSummaryEditorControl ActiveSummary => xtraTabControlContentEditors.SelectedTabPage as OptionsSummaryEditorControl;
		private OptionsSummaryEditorControl Summary => xtraTabControlContentEditors.TabPages.OfType<OptionsSummaryEditorControl>().Single();
		#endregion

		public OptionsContentEditorsContainer()
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

			_tabDragDropHelper = new XtraTabDragDropHelper<OptionSetEditorsContainer>(xtraTabControlContentEditors);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			Controller.Instance.OptionsNew.Click += OnAddOptionsSet;
			Controller.Instance.OptionsProgramAdd.Click += OnAddItem;
			Controller.Instance.OptionsProgramDelete.Click += OnDeleteItem;

			pictureEditDefaultLogo.Image = BusinessObjects.Instance.ImageResourcesManager.OptionsNoRecordsLogo ?? pictureEditDefaultLogo.Image;
		}

		public override void ShowControl(ContentOpenEventArgs args = null)
		{
			base.ShowControl(args);
			var optionsSetOpenEventArgs = args as OptionsSetOpenEventArgs;
			if (optionsSetOpenEventArgs == null) return;
			var editorsContainer = xtraTabControlContentEditors.TabPages
				.OfType<OptionSetEditorsContainer>()
				.FirstOrDefault(c => c.OptionSetData.UniqueID == optionsSetOpenEventArgs.OptionsSetId);
			if (editorsContainer == null) return;
			xtraTabControlContentEditors.SelectedTabPage = editorsContainer;
			editorsContainer.ShowEditor(optionsSetOpenEventArgs.EditorType);
		}

		public override void InitBusinessObjects()
		{
			BusinessObjects.Instance.AdditionalInitializator.RequestContentInitailization(Identifier);
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged ||
				ContentUpdateInfo.ChangeInfo.OptionsChanged);

			EditedContent?.Dispose();
			EditedContent = Schedule.OptionsContent.Clone<OptionsContent, OptionsContent>();

			settingsContainer.LoadContent(EditedContent);

			LoadContentEditors(quickLoad);

			_allowToSave = true;

			LoadActiveEditorData();
		}

		protected override void ApplyChanges()
		{
			foreach (var editorsContainer in xtraTabControlContentEditors.TabPages.OfType<OptionSetEditorsContainer>())
				editorsContainer.SaveData();
			Summary.SaveData();
		}

		protected override void SaveData()
		{
			Schedule.OptionsContent = EditedContent.Clone<OptionsContent, OptionsContent>();
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("options");
		}

		protected override void LoadThemes()
		{
			base.LoadThemes();

			FormThemeSelector.Link(
				Controller.Instance.OptionsTheme,
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
			Controller.Instance.OptionsThemeBar.RecalcLayout();
			Controller.Instance.OptionsPanel.PerformLayout();
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
				foreach (var optionSet in EditedContent.Options.OrderBy(s => s.Index))
				{
					var sectionTabControl = xtraTabControlContentEditors.TabPages
						.OfType<OptionSetEditorsContainer>()
						.FirstOrDefault(sc => sc.OptionSetData.UniqueID.Equals(optionSet.UniqueID));
					sectionTabControl?.LoadData(optionSet);
				}
				Summary.LoadData(EditedContent.OptionsSummary);
			}
			else
			{
				xtraTabControlContentEditors.TabPages
					.OfType<IOptionContentEditorControl>()
					.ToList()
					.ForEach(sc => sc.Release());
				xtraTabControlContentEditors.TabPages.Clear();


				foreach (var optionSet in EditedContent.Options.OrderBy(s => s.Index))
					AddOptionSetEditorsContainerControl(optionSet);

				xtraTabControlContentEditors.TabPages.Add(new OptionsSummaryEditorControl());
				Summary.InitControls();
				Summary.LoadData(EditedContent.OptionsSummary);
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
			if (ActiveOptionSetContainer != null)
			{
				settingsContainer.LoadOptionSet(ActiveOptionSetContainer.OptionSetData);
				UpdateStatusBar();
			}
			OnContentEditorChanged(ActiveContentEditor, EventArgs.Empty);
			UpdateOutputStatus();
			_allowToSave = true;
		}

		private OptionSetEditorsContainer AddOptionSetEditorsContainerControl(OptionSet data, int position = -1)
		{
			var optionSetEditorsContainer = new OptionSetEditorsContainer();
			optionSetEditorsContainer.InitControls();
			optionSetEditorsContainer.LoadData(data);
			optionSetEditorsContainer.DataChanged += OnOptionSetDataChanged;
			optionSetEditorsContainer.SelectedEditorChanged += OnContentEditorChanged;
			position = position == -1 ? xtraTabControlContentEditors.TabPages.OfType<OptionSetEditorsContainer>().Count() : position;
			xtraTabControlContentEditors.TabPages.Insert(position, optionSetEditorsContainer);
			return optionSetEditorsContainer;
		}

		private void AddOptionSet()
		{
			using (var form = new FormOptionSetName())
			{
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var optionSet = new OptionSet(EditedContent);
				optionSet.Name = form.OptionSetName;
				EditedContent.Options.Add(optionSet);
				EditedContent.RebuildOptionSetIndexes();
				var optionControl = AddOptionSetEditorsContainerControl(optionSet);
				xtraTabControlContentEditors.SelectedTabPage = optionControl;
				Summary.UpdateView();
				UpdateSplash();
				SettingsNotSaved = true;
			}
		}

		private void CloneOptionSet(OptionSetEditorsContainer optionControl)
		{
			using (var form = new FormOptionSetName())
			{
				form.OptionSetName = String.Format("{0} (Clone)", optionControl.OptionSetData.Name);
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var optionSet = optionControl.OptionSetData.Clone<OptionSet, OptionSet>();
				optionSet.Name = form.OptionSetName;
				optionSet.Index += 0.5;
				EditedContent.Options.Add(optionSet);
				EditedContent.RebuildOptionSetIndexes();
				var newControl = AddOptionSetEditorsContainerControl(optionSet, (Int32)optionSet.Index);
				xtraTabControlContentEditors.SelectedTabPage = newControl;
				Summary.UpdateView();
				SettingsNotSaved = true;
			}
		}

		private void DeleteOptionSet(OptionSetEditorsContainer optionSetEditorsContainer)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", optionSetEditorsContainer.OptionSetData.Name) != DialogResult.Yes) return;
			EditedContent.Options.Remove(optionSetEditorsContainer.OptionSetData);
			EditedContent.RebuildOptionSetIndexes();
			xtraTabControlContentEditors.TabPages.Remove(optionSetEditorsContainer);
			Summary.UpdateView();
			UpdateSplash();
			UpdateSummaryState();
			UpdateStatusBar();
			settingsContainer.UpdateSettingsAccordingDataChanges(OptionEditorType.Schedule);
			SettingsNotSaved = true;
		}

		private void RenameOptionSet(OptionSetEditorsContainer optionSetEditorsContainer)
		{
			if (optionSetEditorsContainer == null) return;
			using (var form = new FormOptionSetName())
			{
				form.OptionSetName = optionSetEditorsContainer.OptionSetData.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				optionSetEditorsContainer.OptionSetData.Name = form.OptionSetName;
				optionSetEditorsContainer.Text = form.OptionSetName;
				settingsContainer.UpdateSettingsAccordingDataChanges(OptionEditorType.Schedule);
				SettingsNotSaved = true;
			}
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.OptionsPowerPoint.Enabled =
				Controller.Instance.MenuOutputPdfButton.Enabled =
				Controller.Instance.MenuEmailButton.Enabled =
				xtraTabControlContentEditors.TabPages.OfType<IOutputContainer>().Any(oc => oc.GetOutputGroup().Items.Any());
		}

		private void UpdateSummaryState()
		{
			Summary.PageEnabled = EditedContent.OptionsSummary.Enabled && EditedContent.Options.Count > 1 && EditedContent.Options.SelectMany(o => o.Programs).Any();
		}

		private void OnSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			if (ActiveContentEditor == null) return;
			if (!_allowToSave) return;
			foreach (var sectionTabControl in xtraTabControlContentEditors.TabPages.OfType<IOptionContentEditorControl>())
				sectionTabControl.UpdateAccordingSettings(e);
			OnOptionSetDataChanged(sender, e);
		}

		private void OnOptionSetDataChanged(object sender, EventArgs e)
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
			EditedContent.ChangeOptionSetPosition(
				EditedContent.Options.IndexOf(((OptionSetEditorsContainer)e.MovedPage).OptionSetData),
				EditedContent.Options.IndexOf(((OptionSetEditorsContainer)e.TargetPage).OptionSetData) + (1 * e.Offset));
			Summary.UpdateView();
			SettingsNotSaved = true;
		}

		private void UpdateSplash()
		{
			if (EditedContent.Options.Any())
			{
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
				layoutControlItemData.Visibility = LayoutVisibility.Always;
				Controller.Instance.OptionsProgramAdd.Enabled = true;
				Controller.Instance.OptionsProgramDelete.Enabled = true;
			}
			else
			{
				layoutControlItemData.Visibility = LayoutVisibility.Never;
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
				Controller.Instance.OptionsProgramAdd.Enabled = false;
				Controller.Instance.OptionsProgramDelete.Enabled = false;
			}
		}

		private void UpdateCollectionChangeButtons()
		{
			var activeItemCollection = ActiveContentEditor?.ActiveItemCollection;
			if (activeItemCollection == null)
			{
				Controller.Instance.OptionsProgramAdd.Enabled =
					Controller.Instance.OptionsProgramDelete.Enabled = false;
				((RibbonBar)(Controller.Instance.OptionsProgramAdd.ContainerControl)).Text = "Program";
			}
			else
			{
				Controller.Instance.OptionsProgramAdd.Enabled = activeItemCollection.AllowToAddItem;
				Controller.Instance.OptionsProgramDelete.Enabled = activeItemCollection.AllowToDeleteItem;
				((RibbonBar)(Controller.Instance.OptionsProgramAdd.ContainerControl)).Text = activeItemCollection.CollectionTitle;

				var addProductTitle = String.Format("Add {0}", activeItemCollection.CollectionItemTitle);
				var addProductTooltip = String.Format("Add a {0} to your schedule", activeItemCollection.CollectionItemTitle);
				var deleteProductTitle = String.Format("Delete {0}", activeItemCollection.CollectionItemTitle);
				var deleteProductTooltip = String.Format("Delete the selected {0} from your schedule", activeItemCollection.CollectionItemTitle);

				if (ActiveContentEditor.ActiveEditor.EditorType == OptionEditorType.DigitalInfo)
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
				Controller.Instance.SuperTip.SetSuperTooltip(
					Controller.Instance.OptionsProgramAdd,
					new SuperTooltipInfo(
						addProductTitle,
						"",
						addProductTooltip,
						null,
						null,
						eTooltipColor.Gray));
				Controller.Instance.SuperTip.SetSuperTooltip(
					Controller.Instance.OptionsProgramDelete,
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
			var optionSetEditorsContainer = (OptionSetEditorsContainer)arg.Page;
			DeleteOptionSet(optionSetEditorsContainer);
		}

		private void OnContentEditorTabMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControlContentEditors.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			contextMenuStrip.Show((Control)sender, e.Location);
		}

		private void OnRenameOptionSetClick(object sender, EventArgs e)
		{
			RenameOptionSet((OptionSetEditorsContainer)_menuHitInfo.Page);
		}

		private void OnCloneOptionSetClick(object sender, EventArgs e)
		{
			CloneOptionSet((OptionSetEditorsContainer)_menuHitInfo.Page);
		}
		#endregion

		#region Settings management
		private void LoadBarButtons()
		{
			retractableBarControl.AddButtons(settingsContainer.GetSettingsButtons());
		}

		private void OnSettingsControlsUpdated(object sender, EventArgs e)
		{
			LoadBarButtons();
		}

		public override void EditSettings()
		{
			using (var form = new FormOutputSettings())
			{
				switch (ActiveOptionSetContainer.ActiveEditor.EditorType)
				{
					case OptionEditorType.Schedule:
					case OptionEditorType.DigitalInfo:
						form.checkEditUseDecimalRate.Checked = ActiveOptionSetContainer.OptionSetData.UseDecimalRates;
						form.checkEditShowSpotX.Checked = ActiveOptionSetContainer.OptionSetData.ShowSpotsX;

						form.layoutControlItemCloneLineToTheEnd.Enabled = true;
						form.simpleLabelItemCloneLineToTheEnd.Enabled = true;
						form.checkEditCloneLineToTheEnd.Checked = ActiveOptionSetContainer.OptionSetData.CloneLineToTheEnd;

						form.checkEditShowSignatureLine.Checked = ActiveOptionSetContainer.OptionSetData.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = ActiveOptionSetContainer.OptionSetData.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = ActiveOptionSetContainer.OptionSetData.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = ActiveOptionSetContainer.OptionSetData.ContractSettings.RateExpirationDate;
						break;
					case OptionEditorType.Summary:
						form.checkEditUseDecimalRate.Checked = EditedContent.OptionsSummary.UseDecimalRates;
						form.checkEditShowSpotX.Checked = EditedContent.OptionsSummary.ShowSpotsX;

						form.layoutControlItemCloneLineToTheEnd.Enabled = false;
						form.simpleLabelItemCloneLineToTheEnd.Enabled = false;
						form.checkEditCloneLineToTheEnd.Checked = false;

						form.checkEditShowSignatureLine.Checked = EditedContent.OptionsSummary.ContractSettings.ShowSignatureLine;
						form.checkEditShowRatesExpiration.Checked = EditedContent.OptionsSummary.ContractSettings.RateExpirationDate.HasValue;
						form.checkEditShowDisclaimer.Checked = EditedContent.OptionsSummary.ContractSettings.ShowDisclaimer;
						form.dateEditRatesExpirationDate.EditValue = EditedContent.OptionsSummary.ContractSettings.RateExpirationDate;
						break;
					default:
						return;
				}
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
				if (form.ShowDialog() != DialogResult.OK) return;
				switch (ActiveOptionSetContainer.ActiveEditor.EditorType)
				{
					case OptionEditorType.Schedule:
					case OptionEditorType.DigitalInfo:
						ActiveOptionSetContainer.OptionSetData.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						ActiveOptionSetContainer.OptionSetData.ShowSpotsX = form.checkEditShowSpotX.Checked;
						ActiveOptionSetContainer.OptionSetData.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
						ActiveOptionSetContainer.OptionSetData.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						ActiveOptionSetContainer.OptionSetData.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						ActiveOptionSetContainer.OptionSetData.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
						if (EditedContent.OptionsSummary.ApplySettingsForAll)
						{
							foreach (var optionSet in EditedContent.Options.Where(os => os.UniqueID != ActiveOptionSetContainer.OptionSetData.UniqueID))
							{
								optionSet.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
								optionSet.ShowSpotsX = form.checkEditShowSpotX.Checked;
								optionSet.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;

								optionSet.ContractSettings.ShowSignatureLine = ActiveOptionSetContainer.OptionSetData.ContractSettings.ShowSignatureLine;
								optionSet.ContractSettings.ShowDisclaimer = ActiveOptionSetContainer.OptionSetData.ContractSettings.ShowDisclaimer;
								optionSet.ContractSettings.RateExpirationDate = ActiveOptionSetContainer.OptionSetData.ContractSettings.RateExpirationDate;
							}
						}
						break;
					case OptionEditorType.Summary:
						EditedContent.OptionsSummary.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
						EditedContent.OptionsSummary.ShowSpotsX = form.checkEditShowSpotX.Checked;

						EditedContent.OptionsSummary.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
						EditedContent.OptionsSummary.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
						EditedContent.OptionsSummary.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
						break;
				}
				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;
				OnSettingsChanged(this, new SettingsChangedEventArgs
				{
					ChangedSettingsType = OptionSettingsType.AdvancedColumns
				});
			}
		}
		#endregion

		#region Ribbon Operations Events
		public void OnAddOptionsSet(object sender, EventArgs e)
		{
			AddOptionSet();
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

		private readonly LabelItem _statusBarTotalSpotsInfo = new LabelItem();
		private readonly LabelItem _statusBarTotalCostInfo = new LabelItem();
		private readonly LabelItem _statusBarAvgRateInfo = new LabelItem();

		protected override void UpdateStatusBar()
		{
			if (ActiveOptionSetContainer != null && ActiveOptionSetContainer.OptionSetData.Programs.Any())
			{
				switch (ActiveOptionSetContainer.OptionSetData.SpotType)
				{
					case SpotType.Week:
						_statusBarTotalSpotsInfo.Text = String.Format("Weekly Spots: {0}", ActiveOptionSetContainer.OptionSetData.TotalSpots.ToString("#,##0"));
						_statusBarTotalCostInfo.Text = String.Format("Weekly Cost: {0}", ActiveOptionSetContainer.OptionSetData.TotalCost.ToString(ActiveOptionSetContainer.OptionSetData.Parent.OptionsSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0"));
						break;
					case SpotType.Month:
						_statusBarTotalSpotsInfo.Text = String.Format("Monthly Spots: {0}", ActiveOptionSetContainer.OptionSetData.TotalSpots.ToString("#,##0"));
						_statusBarTotalCostInfo.Text = String.Format("Monthly Cost: {0}", ActiveOptionSetContainer.OptionSetData.TotalCost.ToString(ActiveOptionSetContainer.OptionSetData.Parent.OptionsSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0"));
						break;
					case SpotType.Total:
						_statusBarTotalSpotsInfo.Text = String.Format("Total Spots: {0}", ActiveOptionSetContainer.OptionSetData.TotalSpots.ToString("#,##0"));
						_statusBarTotalCostInfo.Text = String.Format("Total Cost: {0}", ActiveOptionSetContainer.OptionSetData.TotalCost.ToString(ActiveOptionSetContainer.OptionSetData.Parent.OptionsSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0"));
						break;
				}
				_statusBarAvgRateInfo.Text = String.Format("Average Rate: {0}", ActiveOptionSetContainer.OptionSetData.AvgRate.ToString(ActiveOptionSetContainer.OptionSetData.UseDecimalRates ? "$#,##0.00" : "$#,##0"));

				var statusBarItems = new List<LabelItem>();

				if (ActiveOptionSetContainer.OptionSetData.ShowTotalSpots)
					statusBarItems.Add(_statusBarTotalSpotsInfo);
				if (ActiveOptionSetContainer.OptionSetData.ShowTotalCost)
					statusBarItems.Add(_statusBarTotalCostInfo);
				if (ActiveOptionSetContainer.OptionSetData.ShowAverageRate)
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
					? SlideType.TVOptionsPrograms
					: SlideType.RadioOptionsPrograms);

		private List<OutputItem> GetOutputItems(bool onlyCurrentSlide)
		{
			var selectedOutputItems = new List<OutputItem>();

			var availableOutputGroups = new List<OutputGroup>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Slides...");
			FormProgress.ShowProgress(Controller.Instance.FormMain);
			if (onlyCurrentSlide)
			{
				if (ActiveOptionSetContainer != null)
					availableOutputGroups.Add(ActiveOptionSetContainer.GetOutputGroup());
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