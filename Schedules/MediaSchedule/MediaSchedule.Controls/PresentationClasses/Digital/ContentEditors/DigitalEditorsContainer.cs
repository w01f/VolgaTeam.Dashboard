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
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Online.Controls.PresentationClasses.Products;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraTab;
using RegistryHelper = Asa.Common.Core.Helpers.RegistryHelper;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	public partial class DigitalEditorsContainer : BasePartitionEditControl<DigitalProductsContent, IDigitalSchedule<IDigitalScheduleSettings>, IDigitalScheduleSettings, MediaScheduleChangeInfo>
	//public partial class DigitalEditorsContainer :System.Windows.Forms.UserControl
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

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}

			InitEditors();

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			retractableBarControl.Collapse(true);

			InitCollectionButtons();

			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();

			Controller.Instance.DigitalProductLogoBar.Text =
				ListManager.Instance.DefaultControlsConfiguration.RibbonGroupDigitalLogoTitle ?? Controller.Instance.DigitalProductLogoBar.Text;
		}

		protected override void UpdateEditedContet()
		{
			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged);

			EditedContent?.Dispose();
			EditedContent = Schedule.DigitalProductsContent.Clone<DigitalProductsContent, DigitalProductsContent>();

			labelControlScheduleInfo.Text = String.Format("{0}{3}<color=gray><i>{1} ({2})</i></color>",
				ScheduleSettings.BusinessName,
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", ScheduleSettings.TotalWeeks, "week"),
				Environment.NewLine);

			settingsContainer.LoadContent(EditedContent);

			xtraTabControlEditors.TabPages.OfType<IDigitalEditor>().ToList().ForEach(editor =>
			{
				editor.RequestReload();
			});

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
			FormThemeSelector.Link(Controller.Instance.DigitalProductTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.OnlineDigitalProduct), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.OnlineDigitalProduct), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType.OnlineDigitalProduct, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.DigitalProductThemeBar.RecalcLayout();
			Controller.Instance.DigitalProductPanel.PerformLayout();
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
			Controller.Instance.DigitalProductPdf.Enabled =
			Controller.Instance.DigitalProductPreview.Enabled =
			Controller.Instance.DigitalProductEmail.Enabled =
				EditedContent.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name));
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
		public Theme SelectedTheme
		{
			get
			{
				return BusinessObjects.Instance.ThemeManager.GetThemes(
						MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
							SlideType.TVDigitalProduct :
							SlideType.RadioDigitalProduct)
					.FirstOrDefault(t => t.Name.Equals(
						MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
							SlideType.TVDigitalProduct :
							SlideType.RadioDigitalProduct) ||
						String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
							SlideType.TVDigitalProduct :
							SlideType.RadioDigitalProduct)));
			}
		}

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
			var outputItems = new List<IDigitalOutputItem>();
			using (var form = new FormSelectOutputItems())
			{
				form.Text = "Select Digital Slides";
				form.buttonXSelectCurrent.Visible = false;
				foreach (var tabPage in xtraTabControlEditors.TabPages
					.OfType<IDigitalOutputContainer>()
					.SelectMany(container => container.GetOutputItems()))
				{
					var item = new CheckedListBoxItem(tabPage, tabPage.SlideName, CheckState.Checked);
					form.checkedListBoxControlOutputItems.Items.Add(item);
				}
				if (form.ShowDialog() == DialogResult.OK)
					outputItems.AddRange(form.checkedListBoxControlOutputItems.Items.
						OfType<CheckedListBoxItem>().
						Where(ci => ci.CheckState == CheckState.Checked).
						Select(ci => ci.Value).
						OfType<IDigitalOutputItem>());
			}
			return outputItems;
		}
		#endregion
	}
}