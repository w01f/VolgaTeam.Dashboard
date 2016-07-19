﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Output;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Settings;
using DevComponents.DotNetBar;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using RegistryHelper = Asa.Common.Core.Helpers.RegistryHelper;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	[ToolboxItem(false)]
	public partial class OptionsContentEditorsContainer : BasePartitionEditControl<OptionsContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
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
			pnData.Dock = DockStyle.Fill;
			pnNoRecords.Dock = DockStyle.Fill;

			retractableBarControl.Collapse(true);

			_tabDragDropHelper = new XtraTabDragDropHelper<OptionSetEditorsContainer>(xtraTabControlContentEditors);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			Controller.Instance.OptionsNew.Click += OnAddOptionsSet;
			Controller.Instance.OptionsProgramAdd.Click += OnAddItem;
			Controller.Instance.OptionsProgramDelete.Click += OnDeleteItem;

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
			}
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged);

			EditedContent?.Dispose();
			EditedContent = Schedule
				.GetSchedulePartitionContent<OptionsContent>(SchedulePartitionType.Options)
				.Clone<OptionsContent, OptionsContent>();

			labelControlScheduleInfo.Text = String.Format("<color=gray>{0}</color>", ScheduleSettings.BusinessName);

			labelControlFlightDates.Text = String.Format("<color=gray>{0} <i>({1})</i></color>",
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", ScheduleSettings.TotalWeeks, "week"));

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
			Schedule.ApplySchedulePartitionContent(SchedulePartitionType.Options, EditedContent.Clone<OptionsContent, OptionsContent>());
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("options");
		}

		protected override void LoadThemes()
		{
			base.LoadThemes();

			FormThemeSelector.Link(Controller.Instance.OptionsTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
			}));
			Controller.Instance.OptionsThemeBar.RecalcLayout();
			Controller.Instance.OptionsPanel.PerformLayout();
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
					UpdateTotalsValues();
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
				UpdateTotalsValues();
				UpdateTotalsVisibility();
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

		private void UpdateTotalsVisibility()
		{
			if (ActiveOptionSetContainer != null)
			{
				pnTotalSpots.Visible = ActiveOptionSetContainer.OptionSetData.ShowTotalSpots;
				pnTotalSpots.BringToFront();
				pnTotalCost.Visible = ActiveOptionSetContainer.OptionSetData.ShowTotalCost;
				pnTotalCost.BringToFront();
				pnAvgRate.Visible = ActiveOptionSetContainer.OptionSetData.ShowAverageRate;
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

		private void UpdateTotalsValues()
		{
			if (ActiveOptionSetContainer != null && ActiveOptionSetContainer.OptionSetData.Programs.Any())
			{
				pnBottom.Visible = true;
				switch (ActiveOptionSetContainer.OptionSetData.SpotType)
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
				laTotalSpotsValue.Text = ActiveOptionSetContainer.OptionSetData.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveOptionSetContainer.OptionSetData.TotalCost.ToString(ActiveOptionSetContainer.OptionSetData.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laAvgRateValue.Text = ActiveOptionSetContainer.OptionSetData.AvgRate.ToString(ActiveOptionSetContainer.OptionSetData.UseDecimalRates ? "$#,##0.00" : "$#,##0");
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

		private void UpdateOutputStatus()
		{
			Controller.Instance.OptionsPowerPoint.Enabled =
				Controller.Instance.OptionsPdf.Enabled =
				Controller.Instance.OptionsPreview.Enabled =
				Controller.Instance.OptionsEmail.Enabled =
				xtraTabControlContentEditors.TabPages.OfType<IOutputContainer>().Any(oc => oc.GetOutputGroup().Configurations.Any());
		}

		private void UpdateSummaryState()
		{
			Summary.PageEnabled = EditedContent.OptionsSummary.Enabled && EditedContent.Options.SelectMany(o => o.Programs).Any();
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
			UpdateTotalsVisibility();
			UpdateTotalsValues();
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
			UpdateTotalsValues();
			UpdateTotalsVisibility();
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
				pnData.BringToFront();
				Controller.Instance.OptionsProgramAdd.Enabled = true;
				Controller.Instance.OptionsProgramDelete.Enabled = true;
			}
			else
			{
				pnNoRecords.BringToFront();
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
				Controller.Instance.Supertip.SetSuperTooltip(
					Controller.Instance.OptionsProgramAdd,
					new SuperTooltipInfo(
						addProductTitle,
						"",
						addProductTooltip,
						null,
						null,
						eTooltipColor.Gray));
				Controller.Instance.Supertip.SetSuperTooltip(
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

		#region Output Staff
		private SlideType SlideType => ActiveContentEditor?.ActiveEditor?.SlideType ??
				(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
					? SlideType.TVOptionsPrograms
					: SlideType.RadioOptionsPrograms);

		public override void OutputPowerPoint()
		{
			var outputGroups = GetOutputConfiguration();
			if (!outputGroups.Any(g => g.Configurations.Any())) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				outputGroups.ForEach(g => g.OutputContainer.GenerateOutput(g.Configurations));
				FormProgress.CloseProgress();
			});

			outputGroups.ForEach(g => g.Dispose());
		}

		public override void OutputPdf()
		{
			var outputGroups = GetOutputConfiguration();
			if (!outputGroups.Any(g => g.Configurations.Any())) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = outputGroups.SelectMany(g => g.OutputContainer.GeneratePreview(g.Configurations)).ToList();
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf", EditedContent.Schedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});

			outputGroups.ForEach(g => g.Dispose());
		}

		public override void Preview()
		{
			var outputGroups = GetOutputConfiguration();
			if (!outputGroups.Any(g => g.Configurations.Any())) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = outputGroups.SelectMany(g => g.OutputContainer.GeneratePreview(g.Configurations)).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				RegularMediaSchedulePowerPointHelper.Instance,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Schedule";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			}

			outputGroups.ForEach(g => g.Dispose());
		}

		public override void Email()
		{
			var outputGroups = GetOutputConfiguration();
			if (!outputGroups.Any(g => g.Configurations.Any())) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = outputGroups.SelectMany(g => g.OutputContainer.GeneratePreview(g.Configurations)).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Schedule";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}

			outputGroups.ForEach(g => g.Dispose());
		}

		private IList<OutputGroup> GetOutputConfiguration()
		{
			var outputGroups = new List<OutputGroup>();
			var availableOutputGroups = xtraTabControlContentEditors.TabPages
					.OfType<IOutputContainer>()
					.Select(oc => oc.GetOutputGroup())
					.ToList();
			if (availableOutputGroups.Any())
			{
				using (var form = new FormConfigureOutput(availableOutputGroups))
				{

					if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
						outputGroups.AddRange(availableOutputGroups);
					else
						availableOutputGroups.ForEach(g => g.Dispose());
				}
			}
			return outputGroups;
		}
		#endregion
	}
}