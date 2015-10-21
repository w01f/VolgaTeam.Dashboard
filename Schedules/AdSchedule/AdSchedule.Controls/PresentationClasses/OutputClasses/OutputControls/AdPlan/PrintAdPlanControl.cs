using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.AdPlan
{
	public class PrintAdPlanControl : AdPlanControl
	{
		public PrintAdPlanControl(Form formContainer)
			: base(formContainer)
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.AdPlanThemeBar.RecalcLayout();
				Controller.Instance.AdPlanPanel.PerformLayout();
			};
		}

		public Schedule LocalSchedule { get; set; }

		public override ISchedule Schedule { get { return LocalSchedule; } }

		public override ThemeManager ThemeManager
		{
			get { return BusinessObjects.Instance.ThemeManager; }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessObjects.Instance.HelpManager; }
		}

		public override ButtonItem Theme
		{
			get { return Controller.Instance.AdPlanTheme; }
		}

		public override void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			InitThemeSelector();
			base.LoadSchedule(quickLoad);
		}

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Theme, ThemeManager.GetThemes(SlideType.PrintAdPlan), Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintAdPlan), (t =>
			{
				Core.AdSchedule.SettingsManager.Instance.SetSelectedTheme(SlideType.PrintAdPlan, t.Name);
				Core.AdSchedule.SettingsManager.Instance.SaveSettings();
				SettingsNotSaved = true;
			}));
		}

		protected override void FillProducts(bool quickLoad)
		{
			_allowToSave = false;
			if (!quickLoad)
			{
				xtraTabControlProducts.SuspendLayout();
				Application.DoEvents();
				xtraTabControlProducts.TabPages.Clear();

				ProductPages.RemoveAll(x => x is AdPlanPrintProductControl && !LocalSchedule.PrintProducts.Select(y => y.UniqueID).Contains((x as AdPlanPrintProductControl).PrintProduct.UniqueID));
				foreach (var printProduct in LocalSchedule.PrintProducts.OrderBy(pr => pr.Index))
				{
					if (!string.IsNullOrEmpty(printProduct.Name))
					{
						var printProductPage = ProductPages.OfType<AdPlanPrintProductControl>().FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(printProduct.UniqueID));
						if (printProductPage == null)
						{
							printProductPage = new AdPlanPrintProductControl();
							printProductPage.Container = this;
							ProductPages.Add(printProductPage);
							Application.DoEvents();
						}
						printProductPage.PrintProduct = printProduct;
						printProductPage.PageEnabled = printProduct.Inserts.Count > 0;
						printProductPage.LoadProduct();
						Application.DoEvents();
					}
				}

				ProductPages.RemoveAll(x => x is AdPlanDigitalProductControl && !LocalSchedule.DigitalProducts.Select(y => y.UniqueID).Contains((x as AdPlanDigitalProductControl).DigitalProduct.UniqueID));
				foreach (var digitalProduct in LocalSchedule.DigitalProducts.OrderBy(pr => pr.Index))
				{
					if (!string.IsNullOrEmpty(digitalProduct.Name))
					{
						var digitalProductPage = ProductPages.OfType<AdPlanDigitalProductControl>().FirstOrDefault(x => x.DigitalProduct.UniqueID.Equals(digitalProduct.UniqueID));
						if (digitalProductPage == null)
						{
							digitalProductPage = new AdPlanDigitalProductControl();
							digitalProductPage.Container = this;
							ProductPages.Add(digitalProductPage);
							Application.DoEvents();
						}
						digitalProductPage.DigitalProduct = digitalProduct;
						digitalProductPage.PageEnabled = true;
						digitalProductPage.LoadProduct();
						Application.DoEvents();
					}
				}

				xtraTabControlProducts.TabPages.AddRange(ProductPages.OfType<XtraTabPage>().ToArray());
				Application.DoEvents();
				xtraTabControlProducts.ResumeLayout();

				UpdateSlidesNumberSelector();
			}
			else
			{
				foreach (var printProduct in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(printProduct.Name))
					{
						var printProductPage = ProductPages.OfType<AdPlanPrintProductControl>().FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(printProduct.UniqueID));
						if (printProductPage != null)
						{
							printProductPage.PrintProduct = printProduct;
							printProductPage.PageEnabled = printProduct.Inserts.Count > 0;
						}
						Application.DoEvents();
					}
				}
				foreach (var digitalProduct in LocalSchedule.DigitalProducts)
				{
					if (!string.IsNullOrEmpty(digitalProduct.Name))
					{
						var digitalProductPage = ProductPages.OfType<AdPlanDigitalProductControl>().FirstOrDefault(x => x.DigitalProduct.UniqueID.Equals(digitalProduct.UniqueID));
						if (digitalProductPage != null)
						{
							digitalProductPage.DigitalProduct = digitalProduct;
							digitalProductPage.PageEnabled = true;
						}
						Application.DoEvents();
					}
				}
			}
			_allowToSave = true;
		}

		protected override void SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, nameChanged, false, this);
		}

		protected override IEnumerable<string> GetExistedScheduleNames()
		{
			return ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName);
		}

		public override Theme SelectedTheme
		{
			get { return ThemeManager.GetThemes(SlideType.PrintAdPlan).FirstOrDefault(t => t.Name.Equals(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintAdPlan)) || String.IsNullOrEmpty(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintAdPlan))); }
		}

		private void TrackOutput()
		{
			BusinessObjects.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabAdPlan.Text, LocalSchedule.BusinessName, ProductPages.Select(p => p.Investment).Sum()));
		}

		protected override void OutputSlides()
		{
			TrackOutput();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				OnlineSchedulePowerPointHelper.Instance.AppendAdPlan(this);
				FormProgress.CloseProgress();
			});
		}

		protected override void ShowPreview(string tempFileName)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
			{
				formPreview.Text = "Preview AdPlan";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
			}
		}

		protected override void ShowPdf()
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", LocalSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				OnlineSchedulePowerPointHelper.Instance.PrepareAdPlanPdf(pdfFileName, this);
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}
	}
}
