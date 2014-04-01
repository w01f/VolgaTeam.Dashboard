using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputMultiSummaryControl : UserControl, ISummaryOutputControl
	{
		private readonly List<PublicationMultiSummaryControl> _tabPages = new List<PublicationMultiSummaryControl>();
		private bool _allowToSave;

		public OutputMultiSummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			HelpToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Schedule Analysis", null, null, eTooltipColor.Gray);

			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
		}

		#region ISummaryOutputControl Members
		public Schedule LocalSchedule { get; set; }
		public bool SettingsNotSaved { get; set; }

		public SuperTooltipInfo HelpToolTip { get; private set; }

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			Controller.Instance.MultiSummaryDigitalLegend.Image = Controller.Instance.MultiSummaryDigitalLegend.Enabled && !LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.Enabled ? Resources.DigitalDisabled : Resources.Digital;
			Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.MultiSummaryDigitalLegend, new SuperTooltipInfo("Digital Products", "",
				Controller.Instance.MultiSummaryDigitalLegend.Enabled && LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.Enabled ?
				"Digital Products are Enabled for this slide" :
				"Digital Products are Disabled for this slide"
				, null, null, eTooltipColor.Gray));
			FormThemeSelector.Link(Controller.Instance.MultiSummaryTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintMultiSummary), BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintMultiSummary), (t =>
			{
				BusinessWrapper.Instance.SetSelectedTheme(SlideType.PrintMultiSummary, t.Name);
				BusinessWrapper.Instance.SaveLocalSettings();
				SettingsNotSaved = true;
			}));
			if (!quickLoad)
			{
				LoadSharedOptions();

				xtraTabControlPublications.SuspendLayout();
				Application.DoEvents();
				xtraTabControlPublications.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.PrintProducts.Select(y => y.UniqueID).Contains(x.PrintProduct.UniqueID));
				foreach (var publication in LocalSchedule.PrintProducts)
				{
					if (string.IsNullOrEmpty(publication.Name)) continue;
					var publicationTab = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID));
					if (publicationTab == null)
					{
						publicationTab = new PublicationMultiSummaryControl(this);
						_tabPages.Add(publicationTab);
						Application.DoEvents();
					}
					publicationTab.PrintProduct = publication;
					publicationTab.PageEnabled = publication.Inserts.Count > 0;
					publicationTab.LoadPublication();
					Application.DoEvents();
				}
				_tabPages.Sort((x, y) => x.PrintProduct.Index.CompareTo(y.PrintProduct.Index));
				xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());
				Application.DoEvents();
				xtraTabControlPublications.ResumeLayout();
			}
			else
			{
				foreach (var publication in LocalSchedule.PrintProducts)
				{
					if (string.IsNullOrEmpty(publication.Name)) continue;
					var publicationTab = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID));
					if (publicationTab != null)
					{
						publicationTab.PrintProduct = publication;
						publicationTab.PageEnabled = publication.Inserts.Count > 0;
					}
					Application.DoEvents();
				}
			}
			LoadProductOptions();
			SettingsNotSaved = false;
		}

		public void Save() { }

		private void LoadSharedOptions()
		{
			Controller.Instance.MultiSummaryPresentationDateText.Text = LocalSchedule.PresentationDate.HasValue ? LocalSchedule.PresentationDate.Value.ToString("MM/dd/yy") : string.Empty;
			Controller.Instance.MultiSummaryBusinessNameText.Text = LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
			Controller.Instance.MultiSummaryDecisionMakerText.Text = LocalSchedule.DecisionMaker;
			checkEditFlightDates.Text = String.Format("Campaign: {0}", LocalSchedule.FlightDates);

			_allowToSave = false;
			Controller.Instance.MultiSummaryHeaderText.Properties.Items.Clear();
			Controller.Instance.MultiSummaryHeaderText.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.OutputHeaders.ToArray());
			if (string.IsNullOrEmpty(LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader))
			{
				if (Controller.Instance.MultiSummaryHeaderText.Properties.Items.Count > 0)
					Controller.Instance.MultiSummaryHeaderText.SelectedIndex = 0;
			}
			else
			{
				var index = Controller.Instance.MultiSummaryHeaderText.Properties.Items.IndexOf(LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader);
				if (index >= 0)
					Controller.Instance.MultiSummaryHeaderText.SelectedIndex = index;
				else
					Controller.Instance.MultiSummaryHeaderText.SelectedIndex = 0;
			}
			Controller.Instance.MultiSummaryHeaderCheck.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowSlideHeader;
			Controller.Instance.MultiSummaryBusinessNameCheck.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowAdvertiser;
			Controller.Instance.MultiSummaryPresentationDateCheck.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowPresentationDate;
			Controller.Instance.MultiSummaryDecisionMakerCheck.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowDecisionMaker;
			checkEditFlightDates.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowFlightDates;
			_allowToSave = true;
		}

		private void LoadProductOptions()
		{
			var selectedProduct = xtraTabControlPublications.SelectedTabPage as PublicationMultiSummaryControl;
			if (selectedProduct == null) return;
			selectedProduct.LoadExternalOption();
		}

		public void OnOptionChanged(object sender, EventArgs e)
		{
			if (sender == Controller.Instance.MultiSummaryHeaderCheck)
				checkEditHeader_CheckedChanged();
			else if (sender == Controller.Instance.MultiSummaryHeaderText)
				comboBoxEditHeader_EditValueChanged();
			else if (sender is CheckBoxItem)
				checkEdit_CheckedChanged(sender, EventArgs.Empty);
		}

		public void ResetToDefault()
		{
			foreach (PrintProduct publication in LocalSchedule.PrintProducts)
			{
				publication.ViewSettings.MultiSummarySettings.ResetToDefault();
				if (string.IsNullOrEmpty(publication.Name)) continue;
				var publicationTab = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID));
				if (publicationTab != null)
					publicationTab.LoadPublication();
				Application.DoEvents();
			}
			SettingsNotSaved = false;
			Controller.Instance.SaveSchedule(LocalSchedule, false, true, this);
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("analysis");
		}
		#endregion

		private void checkEditHeader_CheckedChanged()
		{
			Controller.Instance.MultiSummaryHeaderText.Enabled = Controller.Instance.MultiSummaryHeaderCheck.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void comboBoxEditHeader_EditValueChanged()
		{
			if (!_allowToSave) return;
			LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader = Controller.Instance.MultiSummaryHeaderText.EditValue as String ?? String.Empty;
			SettingsNotSaved = true;
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowAdvertiser = Controller.Instance.MultiSummaryBusinessNameCheck.Checked;
			LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowDecisionMaker = Controller.Instance.MultiSummaryDecisionMakerCheck.Checked;
			LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowFlightDates = checkEditFlightDates.Checked;
			LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowPresentationDate = Controller.Instance.MultiSummaryPresentationDateCheck.Checked;
			LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowSlideHeader = Controller.Instance.MultiSummaryHeaderCheck.Checked;
			SettingsNotSaved = true;
		}

		private void checkEdit_MouseDown(object sender, MouseEventArgs e)
		{
			var cEdit = (CheckEdit)sender;
			var cInfo = (CheckEditViewInfo)cEdit.GetViewInfo();
			Rectangle r = cInfo.CheckInfo.GlyphRect;
			var editorRect = new Rectangle(new Point(0, 0), cEdit.Size);
			if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
				((DXMouseEventArgs)e).Handled = true;
		}

		private void xtraTabControlPublications_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			LoadProductOptions();
		}
		#region Output Stuff
		public int OutputFileIndex
		{
			get { return LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowOnePublicationPerSlide ? 1 : 2; }
		}

		public Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintMultiSummary).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintMultiSummary)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintMultiSummary))); }
		}

		public string Header
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.MultiSummaryHeaderText.EditValue != null && Controller.Instance.MultiSummaryHeaderCheck.Checked)
					result = Controller.Instance.MultiSummaryHeaderText.EditValue.ToString();
				return result;
			}
		}

		public string Date
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.MultiSummaryPresentationDateCheck.Checked)
					result = Controller.Instance.MultiSummaryPresentationDateText.Text;
				return result;
			}
		}

		public string BusinessName
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.MultiSummaryBusinessNameCheck.Checked)
					result = Controller.Instance.MultiSummaryBusinessNameText.Text;
				return result;
			}
		}

		public string DecisionMaker
		{
			get
			{
				string result = string.Empty;
				if (Controller.Instance.MultiSummaryDecisionMakerCheck.Checked)
					result = Controller.Instance.MultiSummaryDecisionMakerText.Text;
				return result;
			}
		}

		public string FlightDates1
		{
			get
			{
				string result = string.Empty;
				if (checkEditFlightDates.Checked)
					result = checkEditFlightDates.Text;
				return result;
			}
		}

		public string[] FlightDates2
		{
			get
			{
				var result = new List<string>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
					result.Add(publication.checkEditFlightDates.Checked ? publication.checkEditFlightDates.Text : string.Empty);
				return result.ToArray();
			}
		}

		public string[] LogoFiles
		{
			get
			{
				var result = new List<string>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
					if (publication.checkEditLogo.Checked && publication.pbLogo.Image != null)
					{
						string fileName = Path.GetTempFileName();
						publication.pbLogo.Image.Save(fileName);
						result.Add(fileName);
					}
					else
						result.Add(string.Empty);
				return result.ToArray();
			}
		}

		public string[] PublicationNames1
		{
			get
			{
				var result = new List<string>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
					result.Add(publication.PrintProduct.ViewSettings.MultiSummarySettings.ShowName && publication.checkEditLogo.Checked ? publication.PrintProduct.Name : String.Empty);
				return result.ToArray();
			}
		}

		public string[] PublicationNames2
		{
			get
			{
				var result = new List<string>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
					result.Add(publication.PrintProduct.ViewSettings.MultiSummarySettings.ShowName && !publication.checkEditLogo.Checked ? publication.PrintProduct.Name : String.Empty);
				return result.ToArray();
			}
		}

		public string[] Investments
		{
			get
			{
				var result = new List<string>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
					result.Add(publication.checkEditInvestment.Checked ? (publication.comboBoxEditInvestment.EditValue + " " + publication.laInvestment.Text) : string.Empty);
				return result.ToArray();
			}
		}

		public string[] RunDates
		{
			get
			{
				var result = new List<string>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
				{
					string runDates = publication.checkEditDates.Checked ? publication.memoEditDates.EditValue.ToString() : string.Empty;
					runDates += (publication.checkEditComments.Checked && publication.memoEditComments.EditValue != null && !string.IsNullOrEmpty(publication.memoEditComments.Text.Trim()) ? ((!string.IsNullOrEmpty(runDates) ? " - " : string.Empty) + publication.memoEditComments.Text) : string.Empty);
					result.Add(runDates);
				}

				return result.ToArray();
			}
		}

		public string[][] AdSpecs
		{
			get
			{
				var result = new List<string[]>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
				{
					var adSpecs = new List<string>();
					if (publication.checkEditTotalAds.Checked)
						adSpecs.Add(publication.checkEditTotalAds.Text);
					if (publication.checkEditTotalSquare.Checked && !string.IsNullOrEmpty(publication.checkEditTotalSquare.Text))
						adSpecs.Add(publication.checkEditTotalSquare.Text);
					if (publication.checkEditSquare.Checked && !string.IsNullOrEmpty(publication.checkEditSquare.Text))
						adSpecs.Add(publication.checkEditSquare.Text);
					if (publication.checkEditDimensions.Checked && !string.IsNullOrEmpty(publication.checkEditDimensions.Text))
						adSpecs.Add(publication.checkEditDimensions.Text);
					if (publication.checkEditPageSize.Checked && !string.IsNullOrEmpty(publication.checkEditPageSize.Text))
						adSpecs.Add(publication.checkEditPageSize.Text);
					if (publication.checkEditPercentOfPage.Checked && !string.IsNullOrEmpty(publication.checkEditPercentOfPage.Text))
						adSpecs.Add(publication.checkEditPercentOfPage.Text);
					if (publication.checkEditColor.Checked)
						adSpecs.Add(publication.checkEditColor.Text.Replace("&&", "&"));
					if (publication.checkEditAvgPCI.Checked)
						adSpecs.Add(publication.checkEditAvgPCI.Text);
					if (publication.checkEditAvgAdCost.Checked)
						adSpecs.Add(publication.checkEditAvgAdCost.Text);
					if (publication.checkEditAvgFinalCost.Checked)
						adSpecs.Add(publication.checkEditAvgFinalCost.Text);
					if (publication.checkEditDiscounts.Checked)
						adSpecs.Add(publication.checkEditDiscounts.Text);
					if (publication.checkEditMechanicals.Checked)
						adSpecs.Add(publication.checkEditMechanicals.Text);
					if (publication.checkEditSections.Checked)
						adSpecs.Add(publication.labelControlSections.Text);
					result.Add(adSpecs.ToArray());
				}
				return result.ToArray();
			}
		}

		public bool ShowDigitalLegend
		{
			get
			{
				return LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.Enabled;
			}
		}

		public bool ShowDigitalLegendOnlyFirstSlide
		{
			get
			{
				return LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.OutputOnlyOnce;
			}
		}

		public string DigitalLegend
		{
			get
			{
				if (!ShowDigitalLegend) return String.Empty;
				if (!LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.AllowEdit)
					return String.Format("Digital Product Info: {0}", LocalSchedule.GetDigitalInfo(LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.RequestOptions));
				if (!String.IsNullOrEmpty(LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.Info))
					return String.Format("Digital Product Info: {0}", LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.Info);
				return String.Empty;
			}
		}

		public void EditDigitalLegend()
		{
			var digitalLegend = LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend;
			using (var form = new FormDigital(digitalLegend))
			{
				form.ShowOutputOnce = Controller.Instance.Summaries.MultiSummary.PublicationNames1.Length - OutputFileIndex > 0;
				form.OutputOnlyFirstSlide = true;
				form.ShowLogo = false;
				form.RequestDefaultInfo += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.GetDigitalInfo(e);
				};
				if (form.ShowDialog() != DialogResult.OK) return;
				if (digitalLegend.ApplyForAll)
					LocalSchedule.ApplyDigitalLegendForAllViews(digitalLegend);
				Controller.Instance.MultiSummaryDigitalLegend.Image = !digitalLegend.Enabled ? Resources.DigitalDisabled : Resources.Digital;
				Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.MultiSummaryDigitalLegend, new SuperTooltipInfo("Digital Products", "",
					digitalLegend.Enabled ?
					"Digital Products are Enabled for this slide" :
					"Digital Products are Disabled for this slide"
					, null, null, eTooltipColor.Gray));
				SettingsNotSaved = true;
			}
		}

		private void TrackOutput()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabMultiSummary.Text, LocalSchedule.BusinessName, (decimal)LocalSchedule.PrintProducts.Sum(p => p.TotalFinalRate)));
		}

		public void PrintOutput()
		{
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					AdSchedulePowerPointHelper.Instance.AppendMultiSummary();
					formProgress.Close();
				});
			}
		}

		public void Email()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareMultiSummaryEmail(tempFileName);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
				{
					formEmail.Text = "Email this Multi-Publication Analysis";
					formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
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
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareMultiSummaryEmail(tempFileName);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
				{
					formPreview.Text = "Preview Multi-Publication Analysis";
					formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (previewResult != DialogResult.OK)
						Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				}
			}
		}
		#endregion
	}
}