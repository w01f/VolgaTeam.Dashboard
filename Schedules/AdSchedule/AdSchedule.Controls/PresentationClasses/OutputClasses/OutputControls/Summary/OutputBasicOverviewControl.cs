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
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
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
			Controller.Instance.BasicOverviewDigitalLegend.Image = Controller.Instance.BasicOverviewDigitalLegend.Enabled && !LocalSchedule.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Enabled ? Resources.DigitalDisabled : Resources.Digital;
			Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.BasicOverviewDigitalLegend, new SuperTooltipInfo("Digital Products", "", 
				Controller.Instance.BasicOverviewDigitalLegend.Enabled && LocalSchedule.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Enabled?
				"Digital Products are Enabled for this slide":
				"Digital Products are Disabled for this slide"
				, null, null, eTooltipColor.Gray));
			FormThemeSelector.Link(Controller.Instance.BasicOverviewTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintBasicOverview), BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintBasicOverview), (t =>
			{
				BusinessWrapper.Instance.SetSelectedTheme(SlideType.PrintBasicOverview, t.Name);
				BusinessWrapper.Instance.SaveLocalSettings();
				SettingsNotSaved = true;
			}));
			if (!quickLoad)
			{
				Application.DoEvents();
				xtraTabControlPublications.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.PrintProducts.Select(y => y.UniqueID).Contains(x.PrintProduct.UniqueID));
				foreach (var publication in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(publication.Name))
					{
						var publicationTab = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID));
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
				foreach (var publication in LocalSchedule.PrintProducts)
				{
					if (!string.IsNullOrEmpty(publication.Name))
					{
						var publicationTab = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID));
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
			foreach (var publication in LocalSchedule.PrintProducts)
			{
				publication.ViewSettings.BasicOverviewSettings.ResetToDefault();
				if (!string.IsNullOrEmpty(publication.Name))
				{
					var publicationTab = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID));
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
			var digitalLegend = LocalSchedule.ViewSettings.BasicOverviewViewSettings.DigitalLegend;
			using (var form = new FormDigital(digitalLegend))
			{
				form.ShowOutputOnce = LocalSchedule.PrintProducts.Count(p => p.Inserts.Any()) > 1;
				form.OutputOnlyFirstSlide = true;
				form.ShowLogo = false;
				form.RequestDefaultInfo += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.GetDigitalInfo(e);
				};
				if (form.ShowDialog() != DialogResult.OK) return;
				if (digitalLegend.ApplyForAll)
					LocalSchedule.ApplyDigitalLegendForAllViews(digitalLegend);
				Controller.Instance.BasicOverviewDigitalLegend.Image = !digitalLegend.Enabled ? Resources.DigitalDisabled : Resources.Digital;
				Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.BasicOverviewDigitalLegend, new SuperTooltipInfo("Digital Products", "",
					digitalLegend.Enabled ?
					"Digital Products are Enabled for this slide" :
					"Digital Products are Disabled for this slide"
					, null, null, eTooltipColor.Gray));
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
				foreach (var tabPage in _tabPages)
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
					Controller.Instance.ShowFloater(() =>
					{
						formProgress.Show();
						if (result == DialogResult.Yes)
							(xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationBasicOverviewControl).PrintOutput();
						else if (result == DialogResult.No)
						{
							foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
							{
								if (item.CheckState != CheckState.Checked) continue;
								var tabPage = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(item.Value));
								if (tabPage != null)
									tabPage.PrintOutput();
							}
						}
						formProgress.Close();
					});
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
						using (var formEmail = new FormEmail(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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
						using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
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
						}
				}
			}
		}
		#endregion
	}
}