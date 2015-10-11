using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.Properties;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
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

		public abstract ISchedule Schedule { get; }
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
			laAdvertiser.Text = Schedule.BusinessName + (!string.IsNullOrEmpty(Schedule.AccountNumber) ? (" - " + Schedule.AccountNumber) : string.Empty);
			if (!quickLoad)
			{
				checkEditLessSlides.Checked = !Schedule.SharedViewSettings.AdPlanViewSettings.MoreSlides;
				checkEditMoreSlides.Checked = Schedule.SharedViewSettings.AdPlanViewSettings.MoreSlides;
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
			Schedule.SharedViewSettings.AdPlanViewSettings.MoreSlides = checkEditMoreSlides.Checked;
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

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule(GetExistedScheduleNames()))
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						SaveSchedule(from.ScheduleName);
						Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
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
						return Schedule.SharedViewSettings.AdPlanViewSettings.MoreSlides ? 4 : 10;
					case 9:
					case 10:
					case 13:
					case 14:
					case 15:
						return Schedule.SharedViewSettings.AdPlanViewSettings.MoreSlides ? 5 : 10;
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
					Schedule.SharedViewSettings.AdPlanViewSettings.MoreSlides);
			}
		}

		public abstract Theme SelectedTheme { get; }

		public string Date
		{
			get
			{
				return Schedule.PresentationDate.HasValue ? Schedule.PresentationDate.Value.ToString("MM/dd/yy") : String.Empty;
			}
		}

		public string BusinessName
		{
			get
			{
				return Schedule.BusinessName;
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
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PrepareAdPlanEmail(tempFileName, this);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formEmail = new FormEmail(OnlineSchedulePowerPointHelper.Instance, HelpManager))
				{
					formEmail.Text = "Email this AdPlan";
					formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
					RegistryHelper.MainFormHandle = formEmail.Handle;
					RegistryHelper.MaximizeMainForm = false;
					formEmail.ShowDialog();
					RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = _formContainer.Handle;
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
				string tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PrepareAdPlanEmail(tempFileName, this);
				Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					ShowPreview(tempFileName);
			}
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