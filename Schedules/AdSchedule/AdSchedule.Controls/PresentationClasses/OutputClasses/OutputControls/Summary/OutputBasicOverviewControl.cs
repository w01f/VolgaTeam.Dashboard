using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputBasicOverviewControl : UserControl, ISummaryOutputControl
	{
		private readonly List<PublicationBasicOverviewControl> _tabPages = new List<PublicationBasicOverviewControl>();

		public OutputBasicOverviewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			HelpToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Basic Overview Slide", null, null, eTooltipColor.Gray);
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
			if (!quickLoad)
			{
				Application.DoEvents();
				xtraTabControlPublications.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.PrintProducts.Select(y => y.UniqueID).Contains(x.PrintProduct.UniqueID));
				foreach (PrintProduct publication in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(publication.Name))
					{
						PublicationBasicOverviewControl publicationTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
						if (publicationTab == null)
						{
							publicationTab = new PublicationBasicOverviewControl();
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
						PublicationBasicOverviewControl publicationTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
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

		private void ResetToDefault()
		{
			foreach (PrintProduct publication in LocalSchedule.PrintProducts)
			{
				publication.ViewSettings.BasicOverviewSettings.ResetToDefault();
				if (!string.IsNullOrEmpty(publication.Name))
				{
					PublicationBasicOverviewControl publicationTab = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
					if (publicationTab != null)
						publicationTab.LoadPublication();
					Application.DoEvents();
				}
			}
			SettingsNotSaved = false;
			Controller.Instance.SaveSchedule(LocalSchedule, true, this);
		}

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
		{
			ResetToDefault();
			e.Handled = true;
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("overview");
		}
		#endregion

		#region Output Stuff
		public void EditDigitalLegend()
		{
			using (var form = new FormDigital(LocalSchedule.ViewSettings.BasicOverviewViewSettings.DigitalLegend))
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
			using (var form = new FormSelectPublication())
			{
				form.Text = "Basic Overview Slide Output";
				form.pbLogo.Image = Resources.AdNoteNormal;
				form.laTitle.Text = "You have Several Publications in this Basic Overview Summary…";
				form.buttonXCurrentPublication.Text = "Send just the Current Publication Overview to my PowerPoint presentation";
				form.buttonXSelectedPublications.Text = "Send all Selected Publications to my PowerPoint presentation";
				foreach (PublicationBasicOverviewControl tabPage in _tabPages)
					if (tabPage.PageEnabled)
						form.checkedListBoxControlPublications.Items.Add(tabPage.PrintProduct.UniqueID, tabPage.PrintProduct.Name, CheckState.Checked, true);
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formProgress = new FormProgress())
				{
					formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
					formProgress.TopMost = true;
					formProgress.Show();
					if (result == DialogResult.Yes)
						(xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationBasicOverviewControl).PrintOutput();
					else if (result == DialogResult.No)
					{
						foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
						{
							if (item.CheckState == CheckState.Checked)
							{
								PublicationBasicOverviewControl tabPage = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(item.Value)).FirstOrDefault();
								if (tabPage != null)
									tabPage.PrintOutput();
							}
						}
					}
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
		}

		public void Email()
		{
			using (var form = new FormSelectPublication())
			{
				form.Text = "Basic Overview Email Output";
				form.pbLogo.Image = Resources.EmailBig;
				form.laTitle.Text = "You have Several Publications in this Basic Overview Summary…";
				form.buttonXCurrentPublication.Text = "Attach just the Current Publication Overview to my Outlook Email Message";
				form.buttonXSelectedPublications.Text = "Attach all Selected Publications to my Outlook Email Message";
				foreach (PublicationBasicOverviewControl tabPage in _tabPages)
					if (tabPage.PageEnabled)
						form.checkedListBoxControlPublications.Items.Add(tabPage.PrintProduct.UniqueID, tabPage.PrintProduct.Name, CheckState.Checked, true);
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formProgress = new FormProgress())
				{
					formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
					formProgress.TopMost = true;
					formProgress.Show();
					string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
					if (result == DialogResult.Yes)
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewEmail(tempFileName, new[] { xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationBasicOverviewControl });
					else if (result == DialogResult.No)
					{
						var emailPages = new List<PublicationBasicOverviewControl>();
						foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
						{
							if (item.CheckState == CheckState.Checked)
							{
								PublicationBasicOverviewControl tabPage = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(item.Value)).FirstOrDefault();
								if (tabPage != null)
									emailPages.Add(tabPage);
							}
						}
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewEmail(tempFileName, emailPages.ToArray());
					}
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
					formProgress.Close();
					if (File.Exists(tempFileName))
						using (var formEmail = new FormEmail())
						{
							formEmail.Text = "Email this Basic Overview";
							formEmail.PresentationFile = tempFileName;
							RegistryHelper.MainFormHandle = formEmail.Handle;
							RegistryHelper.MaximizeMainForm = false;
							formEmail.ShowDialog();
							RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
							RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
						}
				}
			}
		}

		public void Preview()
		{
			using (var form = new FormSelectPublication())
			{
				form.Text = "Basic Overview Output Preview";
				form.pbLogo.Image = Resources.Preview;
				form.laTitle.Text = "You have Several Publications in this Basic Overview Summary…";
				form.buttonXCurrentPublication.Text = "Preview just the Current Publication Overview";
				form.buttonXSelectedPublications.Text = "Preview all Selected Publications";
				foreach (PublicationBasicOverviewControl tabPage in _tabPages)
					if (tabPage.PageEnabled)
						form.checkedListBoxControlPublications.Items.Add(tabPage.PrintProduct.UniqueID, tabPage.PrintProduct.Name, CheckState.Checked, true);
				var result = DialogResult.Yes;
				if (form.checkedListBoxControlPublications.Items.Count > 1)
				{
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result == DialogResult.Cancel)
						return;
				}
				using (var formProgress = new FormProgress())
				{
					formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
					formProgress.TopMost = true;
					formProgress.Show();
					string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
					if (result == DialogResult.Yes)
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewEmail(tempFileName, new[] { xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationBasicOverviewControl });
					else if (result == DialogResult.No)
					{
						var emailPages = new List<PublicationBasicOverviewControl>();
						foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
						{
							if (item.CheckState == CheckState.Checked)
							{
								PublicationBasicOverviewControl tabPage = _tabPages.Where(x => x.PrintProduct.UniqueID.Equals(item.Value)).FirstOrDefault();
								if (tabPage != null)
									emailPages.Add(tabPage);
							}
						}
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewEmail(tempFileName, emailPages.ToArray());
					}
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
					formProgress.Close();
					if (File.Exists(tempFileName))
						using (var formPreview = new FormPreview())
						{
							formPreview.Text = "Preview Basic Overview";
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
		}
		#endregion
	}
}