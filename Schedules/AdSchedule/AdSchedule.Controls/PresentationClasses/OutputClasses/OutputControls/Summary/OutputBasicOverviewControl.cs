using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
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
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laFlightDates.Font = new Font(laFlightDates.Font.FontFamily, laFlightDates.Font.Size - 2, laFlightDates.Font.Style);
				laAdvertiser.Font = new Font(laAdvertiser.Font.FontFamily, laAdvertiser.Font.Size - 2, laAdvertiser.Font.Style);
			}
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
				Controller.Instance.BasicOverviewDigitalLegend.Enabled && LocalSchedule.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Enabled ?
				"Digital Products are Enabled for this slide" :
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
				LoadSharedOptions();
				Application.DoEvents();
				xtraTabControlPublications.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.PrintProducts.Select(y => y.UniqueID).Contains(x.PrintProduct.UniqueID));
				foreach (var publication in LocalSchedule.PrintProducts)
				{
					if (string.IsNullOrEmpty(publication.Name)) continue;
					var publicationTab = _tabPages.FirstOrDefault(x => x.PrintProduct.UniqueID.Equals(publication.UniqueID));
					if (publicationTab == null)
					{
						publicationTab = new PublicationBasicOverviewControl(this);
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

				var summaryControl = new BasicOverviewSummaryControl(this);
				summaryControl.UpdateControls(_tabPages.Where(tp => tp.PageEnabled).Select(tp => tp.SummaryControl));
				xtraTabControlPublications.TabPages.Add(summaryControl);

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
			SettingsNotSaved = false;
		}

		public void Save()
		{
			xtraTabControlPublications.TabPages.OfType<BasicOverviewSummaryControl>().First().Save();
		}

		private void LoadSharedOptions()
		{
			laAdvertiser.Text = LocalSchedule.BusinessName;
			laFlightDates.Text = String.Format("Campaign: {0}", LocalSchedule.FlightDates);
		}

		private void xtraTabControlPublications_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			var page = e.Page as BasicOverviewSummaryControl;
			if (page != null)
				page.SetFocus();
		}

		public void OnOptionChanged(object sender, EventArgs e)
		{
			var selectedProduct = xtraTabControlPublications.SelectedTabPage as PublicationBasicOverviewControl;
			if (selectedProduct == null) return;
			selectedProduct.OnOptionChanged(sender);
		}

		public void ResetToDefault()
		{
			foreach (var publication in LocalSchedule.PrintProducts)
			{
				publication.ViewSettings.BasicOverviewSettings.ResetToDefault();
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

		private void TrackOutput(IEnumerable<PublicationBasicOverviewControl> publications)
		{
			foreach (var publication in publications)
				BusinessWrapper.Instance.ActivityManager.AddActivity(new PublicationOutputActivity(Controller.Instance.TabBasicOverview.Text, LocalSchedule.BusinessName, publication.PrintProduct.Name, (decimal)publication.PrintProduct.TotalFinalRate));
		}

		public void PrintOutput()
		{
			Save();
			var tabPages = xtraTabControlPublications.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IBasicOverviewOutputControl>();
			var selectedProducts = new List<IBasicOverviewOutputControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Products";
					var currentProduct = xtraTabControlPublications.SelectedTabPage as IBasicOverviewOutputControl;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentProduct)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedProducts.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IBasicOverviewOutputControl>());
				}
			else
				selectedProducts.AddRange(tabPages);
			if (!selectedProducts.Any()) return;
			TrackOutput(selectedProducts.OfType<PublicationBasicOverviewControl>());
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					foreach (var product in selectedProducts)
						product.Output();
					formProgress.Close();
				});
			}
		}

		public void Email()
		{
			Save();
			var tabPages = xtraTabControlPublications.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IBasicOverviewOutputControl>();
			var selectedProducts = new List<IBasicOverviewOutputControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Products";
					var currentProduct = xtraTabControlPublications.SelectedTabPage as IBasicOverviewOutputControl;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentProduct)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedProducts.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IBasicOverviewOutputControl>());
				}
			else
				selectedProducts.AddRange(tabPages);
			if (!selectedProducts.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				foreach (var productControl in selectedProducts)
				{
					var previewGroup = productControl.GetPreviewGroup();
					if (productControl is PublicationBasicOverviewControl)
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewEmail(previewGroup.PresentationSourcePath, new[] { (PublicationBasicOverviewControl)productControl });
					else if (productControl is BasicOverviewSummaryControl)
					{
						var summaryControl = productControl as BasicOverviewSummaryControl;
						summaryControl.PopulateReplacementsList();
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
					}
					previewGroups.Add(previewGroup);
				}
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email this Basic Overview";
				formEmail.LoadGroups(previewGroups);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		public void Preview()
		{
			Save();
			var tabPages = xtraTabControlPublications.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IBasicOverviewOutputControl>();
			var selectedProducts = new List<IBasicOverviewOutputControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Products";
					var currentProduct = xtraTabControlPublications.SelectedTabPage as IBasicOverviewOutputControl;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentProduct)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedProducts.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IBasicOverviewOutputControl>());
				}
			else
				selectedProducts.AddRange(tabPages);
			if (!selectedProducts.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				foreach (var productControl in selectedProducts)
				{
					var previewGroup = productControl.GetPreviewGroup();
					if (productControl is PublicationBasicOverviewControl)
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewEmail(previewGroup.PresentationSourcePath, new[] { (PublicationBasicOverviewControl)productControl });
					else if (productControl is BasicOverviewSummaryControl)
					{
						var summaryControl = productControl as BasicOverviewSummaryControl;
						summaryControl.PopulateReplacementsList();
						AdSchedulePowerPointHelper.Instance.PrepareBasicOverviewSummaryEmail(previewGroup.PresentationSourcePath, summaryControl);
					}
					previewGroups.Add(previewGroup);
				}
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			var trackAction = new Action(() => TrackOutput(selectedProducts.OfType<PublicationBasicOverviewControl>()));
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, trackAction))
			{
				formPreview.Text = "Preview Basic Overview";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}
		#endregion
	}

	public interface IBasicOverviewOutputControl
	{
		string SlideName { get; }
		PreviewGroup GetPreviewGroup();
		void Output();
	}

}