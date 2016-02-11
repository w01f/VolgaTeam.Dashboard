using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.ToolForms;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.Properties;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;

namespace Asa.Online.Controls.PresentationClasses.AdPlan
{
	[ToolboxItem(false)]
	public abstract partial class AdPlanControl : UserControl
	{
		protected Form _formContainer;
		protected bool _allowToSave;

		public AdPlanControl(Form formContainer)
		{
			InitializeComponent();
			_formContainer = formContainer;
			Dock = DockStyle.Fill;
			ProductPages = new List<IAdPlanItem>();
			retractableBar.AddButtons(new[] { new ButtonInfo { Logo = Resources.AdPlanSettings, Tooltip = "Expand bar" } });
		}

		public abstract DigitalProductsContent Content { get; }
		public abstract ThemeManager ThemeManager { get; }
		public abstract HelpManager HelpManager { get; }
		public abstract ButtonItem Theme { get; }

		public List<IAdPlanItem> ProductPages { get; private set; }
		public bool SettingsNotSaved { get; set; }
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public bool AllowToLeaveControl
		{
			get
			{
				bool result;
				if (SettingsNotSaved)
				{
					SaveSchedule();
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		public virtual void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;
			laAdvertiser.Text = Content.ScheduleSettings.BusinessName + (!string.IsNullOrEmpty(Content.ScheduleSettings.AccountNumber) ? (" - " + Content.ScheduleSettings.AccountNumber) : string.Empty);
			if (!quickLoad)
			{
				checkEditLessSlides.Checked = !Content.ScheduleSettings.AdPlanViewSettings.MoreSlides;
				checkEditMoreSlides.Checked = Content.ScheduleSettings.AdPlanViewSettings.MoreSlides;
			}
			_allowToSave = true;

			FillProducts(quickLoad);
			UpdateSlidesNumberSelector();
			xtraTabControlProducts_SelectedPageChanged(xtraTabControlProducts, new TabPageChangedEventArgs(null, xtraTabControlProducts.SelectedTabPage));
			SettingsNotSaved = false;
		}

		protected abstract void FillProducts(bool quickLoad);
		protected abstract void SaveSchedule(string scheduleName = "");
		protected abstract IEnumerable<string> GetExistedScheduleNames();

		private void xtraTabControlProducts_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			var currentControl = e.Page as IAdPlanItem;
			if (currentControl == null) return;
			if (!retractableBar.Content.Controls.Contains(currentControl.SettingsContainer))
			{
				currentControl.SettingsContainer.Parent = null;
				currentControl.SettingsContainer.Dock = DockStyle.Fill;
				retractableBar.Content.Controls.Add(currentControl.SettingsContainer);
			}
			currentControl.SettingsContainer.BringToFront();
		}

		private void checkEditMoreSlides_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Content.ScheduleSettings.AdPlanViewSettings.MoreSlides = checkEditMoreSlides.Checked;
			SettingsNotSaved = true;
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Preview();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			PrintOutput();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Email();
		}

		public void Pdf_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			PrintPdf();
		}

		public void Help_Click(object sender, EventArgs e)
		{
			HelpManager.OpenHelpLink("adplan");
		}

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
						return Content.ScheduleSettings.AdPlanViewSettings.MoreSlides ? 4 : 10;
					case 9:
					case 10:
					case 13:
					case 14:
					case 15:
						return Content.ScheduleSettings.AdPlanViewSettings.MoreSlides ? 5 : 10;
					default:
						if (totalRecords < 6)
							return totalRecords;
						return 5;
				}
			}
		}

		public string TemplateFilePath
		{
			get
			{
				return MasterWizardManager.Instance.SelectedWizard.GetAdPlanFile(
					ProductPagesForOutput.Count,
					Content.ScheduleSettings.AdPlanViewSettings.MoreSlides);
			}
		}

		public abstract Theme SelectedTheme { get; }

		public string Date
		{
			get
			{
				return Content.ScheduleSettings.PresentationDate.HasValue ? Content.ScheduleSettings.PresentationDate.Value.ToString("MM/dd/yy") : String.Empty;
			}
		}

		public string BusinessName
		{
			get
			{
				return Content.ScheduleSettings.BusinessName;
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
						slideRows.Add(String.Format("Investment{0}", j + 1), product.InvestmentFormatted);
						slideRows.Add(String.Format("Product{0}   Investment{0}", j + 1), String.Format("{0}   {1}", product.Product, product.InvestmentFormatted));
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
			OutputSlides();
		}

		protected abstract void OutputSlides();

		public void Email()
		{
			PopulateReplacementsList();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			OnlineSchedulePowerPointHelper.Instance.PrepareAdPlanEmail(tempFileName, this);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formEmail = new FormEmail(OnlineSchedulePowerPointHelper.Instance, HelpManager))
			{
				formEmail.Text = "Email this AdPlan";
				formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				Utilities.ActivateForm(_formContainer.Handle, _formContainer.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
			}
		}

		public void Preview()
		{
			PopulateReplacementsList();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			string tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			OnlineSchedulePowerPointHelper.Instance.PrepareAdPlanEmail(tempFileName, this);
			Utilities.ActivateForm(_formContainer.Handle, _formContainer.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();
			if (File.Exists(tempFileName))
				ShowPreview(tempFileName);
		}

		protected abstract void ShowPreview(string tempFileName);

		public void PrintPdf()
		{
			PopulateReplacementsList();
			ShowPdf();
		}

		protected abstract void ShowPdf();
		#endregion
	}
}