using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputAdPlanControl : UserControl, ISummaryOutputControl
	{
		private bool _allowToSave;

		public OutputAdPlanControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			ProductPages = new List<IAdPlanItem>();

			HelpToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the AdPlan", null, null, eTooltipColor.Gray);

			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
		}

		public List<IAdPlanItem> ProductPages { get; private set; }

		#region ISummaryOutputControl Members
		public Schedule LocalSchedule { get; set; }
		public bool SettingsNotSaved { get; set; }
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public SuperTooltipInfo HelpToolTip { get; private set; }

		public void UpdateOutput(bool quickLoad)
		{
			_allowToSave = false;
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			laAdvertiser.Text = LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
			if (!quickLoad)
			{
				checkEditLessSlides.Checked = !LocalSchedule.ViewSettings.AdPlanViewSettings.MoreSlides;
				checkEditMoreSlides.Checked = LocalSchedule.ViewSettings.AdPlanViewSettings.MoreSlides;

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
			SettingsNotSaved = false;
			_allowToSave = true;
		}

		private void checkEditMoreSlides_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.ViewSettings.AdPlanViewSettings.MoreSlides = checkEditMoreSlides.Checked;
				SettingsNotSaved = true;
			}
		}

		public void EditDigitalLegend() { }

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("adplan");
		}
		#endregion

		#region Output Stuff
		public int RecordsPerSlide
		{
			get
			{
				var totalRecords = ProductPagesForOutput.Count;
				switch (totalRecords)
				{
					case 6:
					case 7:
					case 8:
					case 11:
					case 12:
						return LocalSchedule.ViewSettings.AdPlanViewSettings.MoreSlides ? 4 : 10;
					case 9:
					case 10:
					case 13:
					case 14:
					case 15:
						return LocalSchedule.ViewSettings.AdPlanViewSettings.MoreSlides ? 5 : 10;
					default:
						if (totalRecords < 6)
							return totalRecords;
						return 5;
				}
			}
		}

		public string TemplateFileName
		{
			get
			{
				var totalRecords = ProductPagesForOutput.Count;
				switch (totalRecords)
				{
					case 6:
					case 7:
					case 8:
					case 11:
					case 12:
						return LocalSchedule.ViewSettings.AdPlanViewSettings.MoreSlides ? "adplan4.ppt" : "adplan6.ppt";
					case 9:
					case 10:
					case 13:
					case 14:
					case 15:
						return LocalSchedule.ViewSettings.AdPlanViewSettings.MoreSlides ? "adplan5.ppt" : "adplan6.ppt";
					default:
						if (totalRecords < 6)
							return String.Format("adplan{0}.ppt", totalRecords);
						return "adplan5.ppt";
				}
			}
		}

		public string Date
		{
			get
			{
				return LocalSchedule.PresentationDate.ToString("MM/dd/yy");
			}
		}

		public string BusinessName
		{
			get
			{
				return LocalSchedule.BusinessName;
			}
		}

		public List<IAdPlanItem> ProductPagesForOutput
		{
			get { return ProductPages.Where(p => !p.NotOutput).ToList(); }
		}

		public void UpdateSlidesNumberSelector()
		{
			var totalProduct = ProductPagesForOutput.Count;
			if (totalProduct < 6 || totalProduct >= 16)
			{
				pnOutputOptions.Visible = false;
			}
			else if (totalProduct >= 6 && totalProduct < 11)
			{
				pnOutputOptions.Visible = true;
				checkEditLessSlides.Text = "1 Slide";
				checkEditMoreSlides.Text = "2 Slides";
			}
			else if (totalProduct >= 11 && totalProduct < 16)
			{
				pnOutputOptions.Visible = true;
				checkEditLessSlides.Text = "2 Slides";
				checkEditMoreSlides.Text = "3 Slides";
			}
		}

		public void PopulateReplacementsList()
		{
			var pagesForOutput = ProductPagesForOutput;
			var recordsCount = pagesForOutput.Count;
			var rowsPerSlide = RecordsPerSlide;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			for (var i = 0; i < recordsCount; i += rowsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				for (var j = 0; j < rowsPerSlide; j++)
				{
					if ((i + j) < recordsCount)
					{
						var product = pagesForOutput[i + j];
						slideRows.Add(String.Format("Product{0}", j + 1), product.Product);
						slideRows.Add(String.Format("Details{0}", j + 1), product.Details);
						slideRows.Add(String.Format("Investment{0}", j + 1), product.Investment);
						slideRows.Add(String.Format("Product{0}   Investment{0}", j + 1), String.Format("{0}   {1}", product.Product, product.Investment));
					}
					else
					{
						slideRows.Add(String.Format("Product{0}", j + 1), String.Empty);
						slideRows.Add(String.Format("Details{0}", j + 1), String.Empty);
						slideRows.Add(String.Format("Investment{0}", j + 1), String.Empty);
						slideRows.Add(String.Format("Product{0}   Investment{0}", j + 1), String.Empty);
					}
				}
				OutputReplacementsLists.Add(slideRows);
			}
		}

		public void PrintOutput()
		{
			PopulateReplacementsList();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					AdSchedulePowerPointHelper.Instance.AppendAdPlan();
					formProgress.Close();
				});
			}
		}

		public void Email()
		{
			PopulateReplacementsList();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareAdPlanEmail(tempFileName);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail())
					{
						formEmail.Text = "Email this AdPlan";
						formEmail.PresentationFile = tempFileName;
						RegistryHelper.MainFormHandle = formEmail.Handle;
						RegistryHelper.MaximizeMainForm = false;
						formEmail.ShowDialog();
						RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
						RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					}
			}
		}

		public void Preview()
		{
			PopulateReplacementsList();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareAdPlanEmail(tempFileName);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formPreview = new FormPreview())
					{
						formPreview.Text = "Preview AdPlan";
						formPreview.PresentationFile = tempFileName;
						RegistryHelper.MainFormHandle = formPreview.Handle;
						RegistryHelper.MaximizeMainForm = false;
						var previewResult = formPreview.ShowDialog();
						RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
						RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
						if (previewResult != DialogResult.OK)
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
						else
							Utilities.Instance.ActivateMiniBar();
					}
			}
		}
		#endregion
	}
}