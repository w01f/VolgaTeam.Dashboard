﻿using System;
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
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
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

			HelpToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Multi-Publication Analysis", null, null, eTooltipColor.Gray);

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
			laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
			laScheduleName.Text = LocalSchedule.Name;
			laAdvertiser.Text = LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
			if (!quickLoad)
			{
				checkEditDate.Text = LocalSchedule.PresentationDateObject != null ? LocalSchedule.PresentationDate.ToString("MM/dd/yy") : string.Empty;
				checkEditBusinessName.Text = " " + LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
				checkEditDecisionMaker.Text = " " + LocalSchedule.DecisionMaker;
				checkEditFlightDates.Text = " " + LocalSchedule.FlightDateStart.ToString("MM/dd/yy") + " - " + LocalSchedule.FlightDateEnd.ToString("MM/dd/yy");

				_allowToSave = false;
				comboBoxEditSchedule.Properties.Items.Clear();
				comboBoxEditSchedule.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.OutputHeaders.ToArray());
				if (string.IsNullOrEmpty(LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader))
				{
					if (comboBoxEditSchedule.Properties.Items.Count > 0)
						comboBoxEditSchedule.SelectedIndex = 0;
				}
				else
				{
					int index = comboBoxEditSchedule.Properties.Items.IndexOf(LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader);
					if (index >= 0)
						comboBoxEditSchedule.SelectedIndex = index;
					else
						comboBoxEditSchedule.SelectedIndex = 0;
				}
				checkEditSchedule.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowSlideHeader;
				checkEditBusinessName.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowAdvertiser;
				checkEditDate.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowPresentationDate;
				checkEditDecisionMaker.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowDecisionMaker;
				checkEditFlightDates.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowFlightDates;
				rbOnePerSlide.Checked = LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowOnePublicationPerSlide;
				rbTwoPerSlide.Checked = !LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowOnePublicationPerSlide;
				_allowToSave = true;

				xtraTabControlPublications.SuspendLayout();
				Application.DoEvents();
				xtraTabControlPublications.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.PrintProducts.Select(y => y.UniqueID).Contains(x.PrintProduct.UniqueID));
				foreach (PrintProduct publication in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(publication.Name))
					{
						PublicationMultiSummaryControl publicationTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
						if (publicationTab == null)
						{
							publicationTab = new PublicationMultiSummaryControl();
							_tabPages.Add(publicationTab);
							Application.DoEvents();
						}
						publicationTab.PrintProduct = publication;
						publicationTab.PageEnabled = publication.Inserts.Count > 0;
						publicationTab.LoadPublication();
						Application.DoEvents();
					}
				}
				_tabPages.Sort((x, y) => x.PrintProduct.Index.CompareTo(y.PrintProduct.Index));
				xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());
				Application.DoEvents();
				xtraTabControlPublications.ResumeLayout();
			}
			else
			{
				foreach (PrintProduct publication in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(publication.Name))
					{
						PublicationMultiSummaryControl publicationTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
						if (publicationTab != null)
						{
							publicationTab.PrintProduct = publication;
							publicationTab.PageEnabled = publication.Inserts.Count > 0;
						}
						Application.DoEvents();
					}
				}
			}
			SettingsNotSaved = false;
		}

		public void ResetToDefault()
		{
			foreach (PrintProduct publication in LocalSchedule.PrintProducts)
			{
				publication.ViewSettings.MultiSummarySettings.ResetToDefault();
				if (!string.IsNullOrEmpty(publication.Name))
				{
					PublicationMultiSummaryControl publicationTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
					if (publicationTab != null)
						publicationTab.LoadPublication();
					Application.DoEvents();
				}
			}
		}


		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("analysis");
		}
		#endregion

		private void checkEditSchedule_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditSchedule.Enabled = checkEditSchedule.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowAdvertiser = checkEditBusinessName.Checked;
				LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
				LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowFlightDates = checkEditFlightDates.Checked;
				LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowPresentationDate = checkEditDate.Checked;
				LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowSlideHeader = checkEditSchedule.Checked;
				LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowOnePublicationPerSlide = rbOnePerSlide.Checked;
				SettingsNotSaved = true;
			}
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

		#region Output Stuff
		public int OutputFileIndex
		{
			get { return rbOnePerSlide.Checked ? 1 : 2; }
		}

		public string Header
		{
			get
			{
				string result = string.Empty;
				if (comboBoxEditSchedule.EditValue != null && checkEditSchedule.Checked)
					result = comboBoxEditSchedule.EditValue.ToString();
				return result;
			}
		}

		public string Date
		{
			get
			{
				string result = string.Empty;
				if (checkEditDate.Checked)
					result = checkEditDate.Text;
				return result;
			}
		}

		public string BusinessName
		{
			get
			{
				string result = string.Empty;
				if (checkEditBusinessName.Checked)
					result = checkEditBusinessName.Text;
				return result;
			}
		}

		public string DecisionMaker
		{
			get
			{
				string result = string.Empty;
				if (checkEditDecisionMaker.Checked)
					result = checkEditDecisionMaker.Text;
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
					result.Add(publication.checkEditName.Checked && publication.checkEditLogo.Checked ? publication.checkEditName.Text.Replace("&&", "&") : string.Empty);
				return result.ToArray();
			}
		}

		public string[] PublicationNames2
		{
			get
			{
				var result = new List<string>();
				foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
					result.Add(publication.checkEditName.Checked && !publication.checkEditLogo.Checked ? publication.checkEditName.Text.Replace("&&", "&") : string.Empty);
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

		public string DigitalLegend
		{
			get
			{
				return LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend.StringRepresentation;
			}
		}

		public void EditDigitalLegend()
		{
			using (var form = new FormDigital(LocalSchedule.ViewSettings.MultiSummaryViewSettings.DigitalLegend))
			{
				form.WebsiteRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalWebsites;
				};
				form.SimpleDigitalInfoRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalSimpleInfo;
				};
				form.DetailedDigitalInfoRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalDetailedInfo;
				};
				if (form.ShowDialog() == DialogResult.OK)
					SettingsNotSaved = true;
			}
		}

		public void PrintOutput()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				AdSchedulePowerPointHelper.Instance.AppendMultiSummary();
				formProgress.Close();
			}
			using (var formOutput = new FormSlideOutput())
			{
				if (formOutput.ShowDialog() != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				else
				{
					Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
					Utilities.Instance.ActivateMiniBar();
				}
			}
		}

		public void Email()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareMultiSummaryEmail(tempFileName);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail())
					{
						formEmail.Text = "Email this Multi-Publication Analysis";
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
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareMultiSummaryEmail(tempFileName);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formPreview = new FormPreview())
					{
						formPreview.Text = "Preview Multi-Publication Analysis";
						formPreview.PresentationFile = tempFileName;
						RegistryHelper.MainFormHandle = formPreview.Handle;
						RegistryHelper.MaximizeMainForm = false;
						DialogResult previewResult = formPreview.ShowDialog();
						RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
						RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
						if (previewResult != DialogResult.OK)
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
						else
						{
							Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
							Utilities.Instance.ActivateMiniBar();
						}
					}
			}
		}
		#endregion
	}
}