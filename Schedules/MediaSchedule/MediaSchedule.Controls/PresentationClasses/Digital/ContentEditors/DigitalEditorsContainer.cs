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
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using DevComponents.DotNetBar;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraTab;

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

			retractableBarControl.simpleButtonExpand.Image = BusinessObjects.Instance.ImageResourcesManager.RetractableBarExpandImage ??
															 retractableBarControl.simpleButtonExpand.Image;
			retractableBarControl.simpleButtonCollapse.Image = BusinessObjects.Instance.ImageResourcesManager.RetractableBarCollpaseImage ??
															 retractableBarControl.simpleButtonCollapse.Image;
			retractableBarControl.ContentSize = retractableBarControl.Width;
			retractableBarControl.Collapse(true);

			InitCollectionButtons();

			Controller.Instance.DigitalProductLogoBar.Text =
				ListManager.Instance.DefaultControlsConfiguration.RibbonGroupDigitalLogoTitle ?? Controller.Instance.DigitalProductLogoBar.Text;
		}

		public override void InitBusinessObjects()
		{
			BusinessObjects.Instance.AdditionalInitializator.RequestContentInitailization(Identifier);
		}

		protected override void UpdateEditedContet()
		{
			EditedContent?.Dispose();
			EditedContent = Schedule.DigitalProductsContent.Clone<DigitalProductsContent, DigitalProductsContent>();

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
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				outputItems.ForEach(item => item.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, null));
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPowerPointAll()
		{
			OutputPowerPoint();
		}

		public override void OutputPdf()
		{
			var outputItems = GetOutputItems();
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", Schedule.Name, DateTime.Now));
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
			var outputItems = GetOutputItems();
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

		private IList<OutputItem> GetOutputItems()
		{
			var selectedOutputItems = new List<OutputItem>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var availableOutputGroups = xtraTabControlEditors.TabPages
					.OfType<IDigitalOutputContainer>()
					.Select(oc => oc.GetOutputGroup())
					.ToList();
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
		#endregion
	}
}