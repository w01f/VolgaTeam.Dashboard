using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.PresentationClasses.Products;
using Asa.Online.Controls.PresentationClasses.Summary;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital
{
	[ToolboxItem(false)]
	public class DigitalProductContainerControl : DigitalProductContainer<DigitalProductsContent, IDigitalSchedule<IDigitalScheduleSettings>, IDigitalScheduleSettings, MediaScheduleChangeInfo>
	{
		protected override IDigitalSchedule<IDigitalScheduleSettings> Schedule =>
			BusinessObjects.Instance.ScheduleManager.ActiveSchedule;

		public override string Identifier => ContentIdentifiers.DigitalProducts;

		public override RibbonTabItem TabPage => Controller.Instance.TabDigitalProduct;

		public override Form MainForm => Controller.Instance.FormMain;

		#region BaseParttionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();
			OptionButtonsGroup = new DigitalProductListOptionButtonsGroup
			{
				ToggleDimensions = Controller.Instance.DigitalProductToggleDimensions,
				ToggleLocation = Controller.Instance.DigitalProductToggleLocation,
				ToggleStrategy = Controller.Instance.DigitalProductToggleStrategy,
				ToggleRichMedia = Controller.Instance.DigitalProductToggleRichMedia,
				ToggleTargeting = Controller.Instance.DigitalProductToggleTargeting
			};
		}

		protected override void UpdateEditedContet()
		{

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.DigitalContentChanged);

			if (quickLoad) return;

			EditedContent?.Dispose();
			EditedContent = Schedule.DigitalProductsContent.Clone<DigitalProductsContent, DigitalProductsContent>();

			base.UpdateEditedContet();

			OnProductsTabControlSelectedPageChanged(xtraTabControlProducts, new TabPageChangedEventArgs(null, xtraTabPageList));
		}

		protected override void ApplyChanges()
		{
			base.ApplyChanges();
			ChangeInfo.DigitalContentChanged = ChangeInfo.DigitalContentChanged || SettingsNotSaved;
		}

		protected override void ValidateChanges(ContentSavingEventArgs savingArgs)
		{
			if (!EditedContent.DigitalProducts.Any(product => String.IsNullOrEmpty(product.Name))) return;
			savingArgs.Cancel = true;
			savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
		}

		protected override void SaveData()
		{
			Schedule.DigitalProductsContent = EditedContent.Clone<DigitalProductsContent, DigitalProductsContent>();
			base.SaveData();
		}

		public override void GetHelp()
		{
			if (xtraTabControlProducts.SelectedTabPage == xtraTabPageList)
				BusinessObjects.Instance.HelpManager.OpenHelpLink(String.Format("home{0}", "dg"));
			else
				BusinessObjects.Instance.HelpManager.OpenHelpLink("digitalsl");
		}

		protected override void LoadThemes()
		{
			base.LoadThemes();
			FormThemeSelector.Link(Controller.Instance.DigitalProductTheme, BusinessObjects.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.DigitalProductThemeBar.RecalcLayout();
			Controller.Instance.DigitalProductPanel.PerformLayout();
		}

		protected override void LoadProductControls(bool fullReload = false)
		{
			var temp = AllowApplyValues;
			AllowApplyValues = false;

			xtraTabControlProducts.SelectedPageChanged -= OnProductsTabControlSelectedPageChanged;

			xtraTabControlProducts.SuspendLayout();
			Application.DoEvents();

			if (fullReload)
			{
				_tabPages.Clear();
				xtraTabControlProducts.TabPages.OfType<IDigitalSlideControl>().ToList().ForEach(c => c.Release());
			}
			else
			{
				var pagesToDelete = _tabPages.Where(tp => !EditedContent.DigitalProducts.Contains(tp.Product)).ToList();
				pagesToDelete.ForEach(p => p.Release());
				_tabPages.RemoveAll(tp => pagesToDelete.Contains(tp));
			}

			foreach (var productPage in xtraTabControlProducts.TabPages.OfType<IDigitalProductControl>().ToList())
				xtraTabControlProducts.TabPages.Remove((XtraTabPage)productPage);

			foreach (var product in EditedContent.DigitalProducts
				.Where(product => !String.IsNullOrEmpty(product.Name))
				.ToList())
			{
				var productTab = _tabPages.FirstOrDefault(tp => tp.Product == product);
				if (productTab == null)
				{
					productTab =
						new DigitalProductControl
							<DigitalProductsContent, IDigitalSchedule<IDigitalScheduleSettings>, IDigitalScheduleSettings,
								MediaScheduleChangeInfo>(this);
					AssignCloseActiveEditorsOnOutsideClick(productTab);
					_tabPages.Add(productTab);
					Application.DoEvents();
					productTab.Product = product;
				}

				productTab.LoadValues();
				Application.DoEvents();
			}

			_tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
			var tabIndex = 1;
			foreach (var tabPage in _tabPages)
			{
				xtraTabControlProducts.TabPages.Insert(tabIndex, tabPage);
				tabIndex++;
			}

			var summaryControl = xtraTabControlProducts.TabPages.OfType<DigitalSummaryControl>().FirstOrDefault();
			if (fullReload || summaryControl == null)
			{
				if (summaryControl != null)
				{
					summaryControl.Release();
					xtraTabControlProducts.TabPages.Remove(summaryControl);
				}
				summaryControl = new DigitalSummaryControl(this);
				xtraTabControlProducts.TabPages.Add(summaryControl);
			}
			summaryControl.UpdateControls(_tabPages.Select(tp => tp.SummaryControl).ToList());
			summaryControl.PageVisible = _tabPages.Any();

			Application.DoEvents();
			xtraTabControlProducts.ResumeLayout();

			xtraTabControlProducts.SelectedPageChanged += OnProductsTabControlSelectedPageChanged;

			AllowApplyValues = temp;
		}
		#endregion

		#region Output Stuff
		public override Theme SelectedTheme
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

		protected override string SlideName => Controller.Instance.TabDigitalProduct.Text;

		public override ButtonItem ProductAddButton => Controller.Instance.DigitalProductAdd;
		public override ButtonItem ProductCloneButton => Controller.Instance.DigitalProductClone;
		public override RibbonPanel RibbonPanel => Controller.Instance.DigitalProductPanel;

		protected override void GeneratePowerPointSlides(IEnumerable<IDigitalSlideControl> tabsForOutput)
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var tabPage in tabsForOutput)
					tabPage.Output();
				FormProgress.CloseProgress();
			});
		}

		protected override void GeneratePdfSlides(IEnumerable<PreviewGroup> previewGroups)
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
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

		protected override void PreviewSlides(IEnumerable<PreviewGroup> previewGroups)
		{
			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				RegularMediaSchedulePowerPointHelper.Instance,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Digital Product";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = MainForm.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = MainForm.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(MainForm.Handle, MainForm.WindowState == FormWindowState.Maximized, false);
			}
		}

		protected override void EmailSlides(IEnumerable<PreviewGroup> previewGroups)
		{
			using (var formEmail = new FormEmail(OnlineSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Online Schedule";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(MainForm.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = MainForm.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = MainForm.Handle;
			}
		}
		#endregion
	}
}