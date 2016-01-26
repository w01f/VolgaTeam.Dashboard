using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Asa.AdSchedule.Controls.BusinessClasses;
using Asa.AdSchedule.Controls.InteropClasses;
using Asa.AdSchedule.Controls.Properties;
using Asa.AdSchedule.Controls.ToolForms;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.AdSchedule;
using Asa.Core.Common;

namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
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
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.BasicOverviewThemeBar.RecalcLayout();
				Controller.Instance.BasicOverviewPanel.PerformLayout();
			};
		}

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) &&
				control.GetType() != typeof(MemoEdit) &&
				control.GetType() != typeof(ComboBoxEdit) &&
				control.GetType() != typeof(LookUpEdit) &&
				control.GetType() != typeof(DateEdit) &&
				control.GetType() != typeof(CheckedListBoxControl) &&
				control.GetType() != typeof(SpinEdit) &&
				control.GetType() != typeof(CheckEdit))
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			labelControlScheduleInfo.Focus();
		}

		#region ISummaryOutputControl Members
		public Schedule LocalSchedule { get; set; }
		public bool SettingsNotSaved { get; set; }

		public SuperTooltipInfo HelpToolTip { get; private set; }

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			Controller.Instance.BasicOverviewDigitalLegend.Image = Controller.Instance.BasicOverviewDigitalLegend.Enabled && !LocalSchedule.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Enabled ? Resources.DigitalDisabled : Resources.Digital;
			Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.BasicOverviewDigitalLegend, new SuperTooltipInfo("Digital Products", "",
				Controller.Instance.BasicOverviewDigitalLegend.Enabled && LocalSchedule.ViewSettings.BasicOverviewViewSettings.DigitalLegend.Enabled ?
				"Digital Products are Enabled for this slide" :
				"Digital Products are Disabled for this slide"
				, null, null, eTooltipColor.Gray));
			InitThemeSelector();
			if (!quickLoad)
			{
				labelControlScheduleInfo.Text = String.Format("{0}   <color=gray><i>({1} {2})</i></color>",
					LocalSchedule.BusinessName,
					LocalSchedule.FlightDates,
					String.Format("{0} {1}s", LocalSchedule.TotalWeeks, "week"));
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
				AssignCloseActiveEditorsonOutSideClick(summaryControl);
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

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.BasicOverviewTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.PrintBasicOverview), Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintBasicOverview), (t =>
			{
				Core.AdSchedule.SettingsManager.Instance.SetSelectedTheme(SlideType.PrintBasicOverview, t.Name);
				Core.AdSchedule.SettingsManager.Instance.SaveSettings();
				SettingsNotSaved = true;
			}));
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

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
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
			BusinessObjects.Instance.HelpManager.OpenHelpLink("overview");
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
					((BaseEdit)e.Editor).EditValue = LocalSchedule.GetDigitalInfo(e);
					((BaseEdit)e.Editor).Tag = ((BaseEdit)e.Editor).EditValue;
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
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var product in selectedProducts)
					product.Output();
				FormProgress.CloseProgress();
			});
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
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
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
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
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
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
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
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
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

		public void PrintPdf()
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
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = new List<PreviewGroup>();
				previewGroups.AddRange(selectedProducts.Select(productControl =>
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
					return previewGroup;
				}));
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", LocalSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				AdSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
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