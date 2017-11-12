using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Online.Controls.PresentationClasses.Products;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraTab;
using RegistryHelper = Asa.Common.Core.Helpers.RegistryHelper;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	public partial class DigitalEditorsContainer : BasePartitionEditControl<DigitalProductsContent, IDigitalSchedule<IDigitalScheduleSettings>, IDigitalScheduleSettings, MediaScheduleChangeInfo>
	//public partial class DigitalEditorsContainer :UserControl
	{
		#region Properties
		private MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;
		private MediaScheduleSettings ScheduleSettings => Schedule.Settings;

		public override string Identifier => ContentIdentifiers.DigitalProducts;

		public override RibbonTabItem TabPage => Controller.Instance.TabDigitalProduct;

		private IDigitalSection ActiveSection => xtraTabControlEditors.SelectedTabPage as IDigitalSection;
		private IDigitalEditor ActiveEditor => xtraTabControlEditors.SelectedTabPage as IDigitalEditor;
		private IDigitalItemCollectionEditor ActiveCollectionEditor => xtraTabControlEditors.SelectedTabPage as IDigitalItemCollectionEditor;
		#endregion

		public DigitalEditorsContainer()
		{
			InitializeComponent();
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			InitEditors();

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			retractableBarControl.ContentSize = retractableBarControl.Width;
			retractableBarControl.Collapse(true);

			InitCollectionButtons();

			Controller.Instance.DigitalProductLogoBar.Text =
				ListManager.Instance.DefaultControlsConfiguration.RibbonGroupDigitalLogoTitle ?? Controller.Instance.DigitalProductLogoBar.Text;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			simpleLabelItemScheduleInfo.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemScheduleInfo.MaxSize, scaleFactor);
			simpleLabelItemScheduleInfo.MinSize = RectangleHelper.ScaleSize(simpleLabelItemScheduleInfo.MinSize, scaleFactor);
			simpleLabelItemFlightDates.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDates.MaxSize, scaleFactor);
			simpleLabelItemFlightDates.MinSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDates.MinSize, scaleFactor);
		}

		protected override void UpdateEditedContet()
		{
			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged);

			EditedContent?.Dispose();
			EditedContent = Schedule.DigitalProductsContent.Clone<DigitalProductsContent, DigitalProductsContent>();

			simpleLabelItemScheduleInfo.Text = String.Format("<color=gray>{0}</color>", ScheduleSettings.BusinessName);

			simpleLabelItemFlightDates.Text = String.Format("<color=gray>{0} <i>({1})</i></color>",
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", ScheduleSettings.TotalWeeks, "week"));

			settingsContainer.LoadContent(EditedContent);

			xtraTabControlEditors.TabPages.OfType<IDigitalEditor>().ToList().ForEach(editor =>
			{
				editor.RequestReload();
			});

			xtraTabControlEditors.TabPages.OfType<DigitalListEditorControl>().Single().LoadData();
			LoadActiveEditorData();

			UpdateEditorsStatus();
			UpdateOutputStatus();
		}

		protected override void ApplyChanges()
		{
			ActiveEditor?.SaveData();
			ChangeInfo.DigitalContentChanged = ChangeInfo.DigitalContentChanged || SettingsNotSaved;
		}

		protected override void SaveData()
		{
			Schedule.DigitalProductsContent = EditedContent.Clone<DigitalProductsContent, DigitalProductsContent>();
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink(ActiveSection.HelpTag);
		}

		protected override void LoadThemes()
		{
			base.LoadThemes();

			FormThemeSelector.Link(
				Controller.Instance.DigitalProductTheme,
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
			Controller.Instance.DigitalProductThemeBar.RecalcLayout();
			Controller.Instance.DigitalProductPanel.PerformLayout();
		}

		protected override void UpdateMenuOutputButtons()
		{
			UpdateOutputStatus();
		}
		#endregion

		#region Editors Management
		private void InitEditors()
		{
			var homePage = new HomePage();
			xtraTabControlEditors.TabPages.Add(homePage);

			var editorList = new DigitalListEditorControl(this);
			xtraTabControlEditors.TabPages.Add(editorList);

			var editorProducts = new DigitalProductEditorControl(this);
			xtraTabControlEditors.TabPages.Add(editorProducts);

			var editorSummary = new DigitalSummaryEditorControl(this);
			xtraTabControlEditors.TabPages.Add(editorSummary);

			var editorProductPackage = new DigitalProductPackageEditorControl(this);
			xtraTabControlEditors.TabPages.Add(editorProductPackage);

			var editorStandalonePackage = new DigitalStandalonePackageEditorControl(this);
			xtraTabControlEditors.TabPages.Add(editorStandalonePackage);

			xtraTabControlEditors.TabPages.OfType<IDigitalEditor>().ToList().ForEach(editor =>
			{
				editor.RequestReload();
				editor.DataChanged += OnEditorDataChanged;
			});
			xtraTabControlEditors.SelectedPageChanged += OnSectionSelected;
		}

		private void LoadActiveEditorData()
		{
			OnSectionSelected(ActiveSection, new TabPageChangedEventArgs(null, (XtraTabPage)ActiveSection));
		}

		private void InitCollectionButtons()
		{
			Controller.Instance.DigitalProductAdd.SuspendLayout = true;
			Controller.Instance.DigitalProductAdd.SubItems.AddRange(ListManager.Instance.Categories
				.Select(category =>
				{
					var categoryButton = new ButtonItem
					{
						Image = category.Logo,
						Text = "<b>" + category.TooltipTitle + "</b><p>" + category.TooltipValue + "</p>",
						ImagePaddingHorizontal = 8,
						SubItemsExpandWidth = 14,
						Tag = category
					};
					categoryButton.Click += OnAddProduct;
					return categoryButton;
				})
				.ToArray());
			Controller.Instance.DigitalProductAdd.SuspendLayout = false;
			((RibbonBar)Controller.Instance.DigitalProductAdd.ContainerControl).RecalcLayout();
			Controller.Instance.DigitalProductPanel.PerformLayout();

			Controller.Instance.DigitalProductClone.Click += OnCloneProduct;
			Controller.Instance.DigitalProductDelete.Click += OnDeleteProduct;

			if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalAddTitle))
				Controller.Instance.Supertip.SetSuperTooltip(
						Controller.Instance.DigitalProductAdd,
						new SuperTooltipInfo(
							ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalAddTitle,
							"",
							ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalAddTooltip,
							null,
							null,
							eTooltipColor.Gray));
			if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalCloneTitle))
				Controller.Instance.Supertip.SetSuperTooltip(
						Controller.Instance.DigitalProductClone,
						new SuperTooltipInfo(
							ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalCloneTitle,
							"",
							ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalCloneTooltip,
							null,
							null,
							eTooltipColor.Gray));
			if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalDeleteTitle))
				Controller.Instance.Supertip.SetSuperTooltip(
						Controller.Instance.DigitalProductDelete,
						new SuperTooltipInfo(
							ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalDeleteTitle,
							"",
							ListManager.Instance.DefaultControlsConfiguration.RibbonButtonDigitalDeleteTooltip,
							null,
							null,
							eTooltipColor.Gray));
		}

		private void UpdateEditorsStatus()
		{
			xtraTabControlEditors.TabPages
				.OfType<IDigitalEditor>()
				.Where(e => e.SectionType != DigitalSectionType.List && e.SectionType != DigitalSectionType.StandalonePackage)
				.OfType<XtraTabPage>()
				.ToList()
				.ForEach(e => e.PageEnabled = EditedContent.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.DigitalProductPowerPoint.Enabled =
			Controller.Instance.MenuOutputPdfButton.Enabled =
			Controller.Instance.DigitalProductPreview.Enabled =
			Controller.Instance.MenuEmailButton.Enabled =
				EditedContent.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)) ||
				EditedContent.StandalonePackage.Items.Any();
		}

		private void UpdateCollectionChangeButtons()
		{
			Controller.Instance.DigitalProductAdd.Enabled = ActiveCollectionEditor != null;
			Controller.Instance.DigitalProductClone.Enabled =
			Controller.Instance.DigitalProductDelete.Enabled =
				ActiveCollectionEditor != null && ActiveCollectionEditor.HasItems;
		}

		private void OnSectionSelected(object sender, TabPageChangedEventArgs e)
		{
			var previousEditor = e.PrevPage as IDigitalEditor;
			previousEditor?.SaveData();
			ActiveEditor?.LoadData();
			settingsContainer.UpdateSettingsAccordingSelectedSectionEditor(ActiveSection.SectionType);
			UpdateCollectionChangeButtons();
			LoadThemes();
		}

		private void OnEditorDataChanged(object sender, DataChangedEventArgs e)
		{
			if (ActiveEditor == null) return;
			Func<IDigitalEditor, bool> predicate = null;
			switch (e.ChangedSectionType)
			{
				case DigitalSectionType.List:
					predicate = target => target.SectionType == DigitalSectionType.Products || target.SectionType == DigitalSectionType.Summary ||
										  target.SectionType == DigitalSectionType.ProductPackage;
					break;
				case DigitalSectionType.Products:
					predicate = target => target.SectionType == DigitalSectionType.Summary ||
										  target.SectionType == DigitalSectionType.ProductPackage;
					break;
				case DigitalSectionType.ProductPackage:
					predicate = target => false;
					break;
				case DigitalSectionType.Summary:
					predicate = target => false;
					break;
				case DigitalSectionType.StandalonePackage:
					predicate = target => false;
					break;
				default:
					throw new ArgumentOutOfRangeException("Undefined digital editor type");
			}
			xtraTabControlEditors.TabPages
						.OfType<IDigitalEditor>()
						.Where(predicate)
						.ToList()
						.ForEach(editor =>
						{
							editor.RequestReload();
						});
			settingsContainer.UpdateSettingsAccordingDataChanges(ActiveSection.SectionType);
			UpdateEditorsStatus();
			UpdateOutputStatus();
			SettingsNotSaved = true;
		}

		private void OnSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			if (ActiveEditor == null) return;
			ActiveEditor.UpdateAccordingSettings(e);
			SettingsNotSaved = true;
		}
		#endregion

		#region Settings management
		private void OnSettingsControlsUpdated(object sender, EventArgs e)
		{
			var buttons = settingsContainer.GetSettingsButtons().ToList();
			if (buttons.Any())
			{
				retractableBarControl.AddButtons(buttons);
				retractableBarControl.Visible = true;
			}
			else
			{
				retractableBarControl.Visible = false;
				retractableBarControl.Collapse(true);
			}
		}
		#endregion

		#region Ribbon Operations Events
		public void OnAddProduct(object sender, EventArgs e)
		{
			ActiveCollectionEditor?.AddItem(sender);
			UpdateCollectionChangeButtons();
		}

		public void OnCloneProduct(object sender, EventArgs e)
		{
			ActiveCollectionEditor?.CloneItem();
		}

		public void OnDeleteProduct(object sender, EventArgs e)
		{
			ActiveCollectionEditor?.DeleteItem();
			UpdateCollectionChangeButtons();
		}
		#endregion

		#region Output Staff
		private SlideType SlideType => ActiveSection?.SlideType ?? SlideType.DigitalProducts;

		public override void OutputPowerPoint()
		{
			var outputItems = GetOutputItems();
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				outputItems.ForEach(item => item.GenerateOutput());
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var outputItems = GetOutputItems();
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = outputItems.Select(item => item.GeneratePreview()).ToList();
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
			var outputItems = GetOutputItems();
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = outputItems.Select(item => item.GeneratePreview()).ToList();
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
		}

		public override void Email()
		{
			var outputItems = GetOutputItems();
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = outputItems.Select(item => item.GeneratePreview()).ToList();
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
		}

		private IList<IDigitalOutputItem> GetOutputItems()
		{
			var outputGroups = new List<OutputGroup>();
			var availableOutputGroups = xtraTabControlEditors.TabPages
					.OfType<IDigitalOutputContainer>()
					.Select(oc => oc.GetOutputGroup())
					.Where(g => g.OutputItems.Any())
					.ToList();
			if (availableOutputGroups.Any())
			{
				using (var form = new FormConfigureOutput(availableOutputGroups))
				{
					if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
						outputGroups.AddRange(availableOutputGroups);
				}
			}
			return outputGroups.SelectMany(g => g.OutputItems).ToList();
		}
		#endregion
	}
}