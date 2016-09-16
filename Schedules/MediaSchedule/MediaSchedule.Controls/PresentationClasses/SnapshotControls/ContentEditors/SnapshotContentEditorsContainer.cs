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
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings;
using DevComponents.DotNetBar;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using RegistryHelper = Asa.Common.Core.Helpers.RegistryHelper;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	[ToolboxItem(false)]
	public partial class SnapshotContentEditorsContainer : BasePartitionEditControl<SnapshotContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
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
			pnData.Dock = DockStyle.Fill;
			pnNoRecords.Dock = DockStyle.Fill;

			retractableBarControl.Collapse(true);

			_tabDragDropHelper = new XtraTabDragDropHelper<SnapshotEditorsContainer>(xtraTabControlContentEditors);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			Controller.Instance.SnapshotNew.Click += OnAddSnapshotSet;
			Controller.Instance.SnapshotProgramAdd.Click += OnAddItem;
			Controller.Instance.SnapshotProgramDelete.Click += OnDeleteItem;

			pbNoRecords.Image = BusinessObjects.Instance.ImageResourcesManager.SnapshotsNoRecordsLogo ?? pbNoRecords.Image;

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
				laActiveWeeksTitle.Font = font;
				laWeeklySpotsTitle.Font = font;
				laWeeklyCostTitle.Font = font;
				laTotalSpotsTitle.Font = font;
				laTotalCostTitle.Font = font;
				laAvgRateTitle.Font = font;
				font = new Font(laAvgRateValue.Font.FontFamily, laAvgRateValue.Font.Size - 2, laAvgRateValue.Font.Style);
				laWeeklySpotsValue.Font = font;
				laWeeklyCostValue.Font = font;
				laTotalSpotsValue.Font = font;
				laTotalCostValue.Font = font;
				laAvgRateValue.Font = font;
			}
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
			foreach (var editorsContainer in xtraTabControlContentEditors.TabPages.OfType<SnapshotEditorsContainer>())
				editorsContainer.SaveData();
			Summary.SaveData();
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
			if (ActiveSnapshotContainer != null)
			{
				settingsContainer.LoadSnapshot(ActiveSnapshotContainer.SnapshotData);
				UpdateTotalsValues();
				UpdateTotalsVisibility();
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

		private void UpdateTotalsValues()
		{
			if (ActiveSnapshotContainer != null && ActiveSnapshotContainer.SnapshotData.Programs.Any())
			{
				pnBottom.Visible = true;
				laActiveWeeksValue.Text = ActiveSnapshotContainer.SnapshotData.TotalWeeks.ToString("#0");
				laWeeklySpotsValue.Text = ActiveSnapshotContainer.SnapshotData.WeeklySpots.ToString("#,##0");
				laWeeklyCostValue.Text = ActiveSnapshotContainer.SnapshotData.WeeklyCost.ToString(ActiveSnapshotContainer.SnapshotData.Parent.SnapshotSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laTotalSpotsValue.Text = ActiveSnapshotContainer.SnapshotData.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveSnapshotContainer.SnapshotData.TotalCost.ToString(ActiveSnapshotContainer.SnapshotData.Parent.SnapshotSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laAvgRateValue.Text = ActiveSnapshotContainer.SnapshotData.AvgRate.ToString(ActiveSnapshotContainer.SnapshotData.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
			else if (ActiveSummary != null)
			{
				pnBottom.Visible = true;
				laTotalSpotsValue.Text = ActiveSummary.Data.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveSummary.Data.TotalCost.ToString(ActiveSummary.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
			else
			{
				pnBottom.Visible = false;
			}
		}

		private void UpdateTotalsVisibility()
		{
			if (ActiveSnapshotContainer != null)
			{
				pnTotalSpots.Visible = ActiveSnapshotContainer.SnapshotData.ShowTotalSpots;

				pnWeeklyCost.Visible = ActiveSnapshotContainer.SnapshotData.ShowWeeklyCost;
				pnWeeklyCost.SendToBack();

				pnWeeklySpots.Visible = ActiveSnapshotContainer.SnapshotData.ShowWeeklySpots;
				pnWeeklySpots.SendToBack();

				pnActiveWeeks.Visible = true;
				pnActiveWeeks.SendToBack();

				pnTotalCost.Visible = ActiveSnapshotContainer.SnapshotData.ShowTotalCost;
				pnTotalCost.BringToFront();

				pnAvgRate.Visible = ActiveSnapshotContainer.SnapshotData.ShowAverageRate;
				pnAvgRate.BringToFront();
			}
			else if (ActiveSummary != null)
			{
				pnActiveWeeks.Visible = false;
				pnWeeklySpots.Visible = false;
				pnWeeklyCost.Visible = false;
				pnAvgRate.Visible = false;

				pnTotalSpots.Visible = ActiveSummary.Data.ShowTallySpots;
				pnTotalSpots.SendToBack();
				pnTotalCost.Visible = ActiveSummary.Data.ShowTallyCost;
				pnTotalCost.BringToFront();
			}
			else
			{
				pnActiveWeeks.Visible = false;
				pnWeeklySpots.Visible = false;
				pnWeeklyCost.Visible = false;
				pnTotalSpots.Visible = false;
				pnTotalCost.Visible = false;
				pnAvgRate.Visible = false;
			}
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.SnapshotPowerPoint.Enabled =
				Controller.Instance.SnapshotPdf.Enabled =
				Controller.Instance.SnapshotPreview.Enabled =
				Controller.Instance.SnapshotEmail.Enabled =
				xtraTabControlContentEditors.TabPages.OfType<IOutputContainer>().Any(oc => oc.GetOutputGroup().Configurations.Any());
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
				pnData.BringToFront();
				Controller.Instance.SnapshotProgramAdd.Enabled = true;
				Controller.Instance.SnapshotProgramDelete.Enabled = true;
			}
			else
			{
				pnNoRecords.BringToFront();
				Controller.Instance.SnapshotProgramAdd.Enabled = false;
				Controller.Instance.SnapshotProgramDelete.Enabled = false;
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

		#region Output Staff
		private SlideType SlideType => ActiveContentEditor?.ActiveEditor?.SlideType ??
				(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
					? SlideType.TVSnapshotPrograms
					: SlideType.RadioSnapshotPrograms);

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