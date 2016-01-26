using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.OnlineSchedule;
using Asa.OnlineSchedule.Controls.BusinessClasses;
using Asa.OnlineSchedule.Controls.InteropClasses;

namespace Asa.OnlineSchedule.Controls.PresentationClasses
{
	public class DigitalAdPlanControl : AdPlanControl
	{
		public DigitalAdPlanControl(Form formContainer)
			: base(formContainer)
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
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
			FormThemeSelector.Link(Theme, ThemeManager.GetThemes(SlideType.OnlineAdPlan), BusinessObjects.Instance.GetSelectedTheme(SlideType.OnlineAdPlan), (t =>
			{
				BusinessObjects.Instance.SetSelectedTheme(SlideType.OnlineAdPlan, t.Name);
				BusinessObjects.Instance.SaveLocalSettings();
			}));
			base.LoadSchedule(quickLoad);
		}

		protected override void FillProducts(bool quickLoad)
		{
			_allowToSave = false;
			if (!quickLoad)
			{
				xtraTabControlProducts.SuspendLayout();
				Application.DoEvents();
				xtraTabControlProducts.TabPages.Clear();

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
			get { return ThemeManager.GetThemes(SlideType.PrintAdPlan).FirstOrDefault(t => t.Name.Equals(BusinessObjects.Instance.GetSelectedTheme(SlideType.PrintAdPlan)) || String.IsNullOrEmpty(BusinessObjects.Instance.GetSelectedTheme(SlideType.PrintAdPlan))); }
		}

		protected override void OutputSlides()
		{
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
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, OnlineSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
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
			throw new NotImplementedException();
		}
	}
}
