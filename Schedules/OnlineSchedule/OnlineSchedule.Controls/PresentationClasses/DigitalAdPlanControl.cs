using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	public class DigitalAdPlanControl : AdPlanControl
	{
		public DigitalAdPlanControl(Form formContainer)
			: base(formContainer)
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public Schedule LocalSchedule { get; set; }

		public override ISchedule Schedule { get { return LocalSchedule; } }
		public override string TemplatesFolderPath
		{
			get { return BusinessWrapper.Instance.OutputManager.AdPlanTemlatesFolderPath; }
		}

		public override ThemeManager ThemeManager
		{
			get { return BusinessWrapper.Instance.ThemeManager; }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessWrapper.Instance.HelpManager; }
		}

		public override ButtonItem Theme
		{
			get { return Controller.Instance.AdPlanTheme; }
		}

		public override void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
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
			if (!string.IsNullOrEmpty(scheduleName))
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, false, this);
		}

		protected override void OutputSlides()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					OnlineSchedulePowerPointHelper.Instance.AppendAdPlan(this);
					formProgress.Close();
				});
			}
		}

		protected override void ShowPreview(string tempFileName)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, OnlineSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview AdPlan";
				formPreview.PresentationFile = tempFileName;
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				else
					Utilities.Instance.ActivateMiniBar();
			}
		}
	}
}
