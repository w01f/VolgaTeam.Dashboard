using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Asa.MediaSchedule.Controls.InteropClasses;
using Asa.OnlineSchedule.Controls.PresentationClasses;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Digital
{
	[ToolboxItem(false)]
	public class DigitalProductContainerControl : DigitalProductContainer
	{
		public DigitalProductContainerControl(Form formMain)
			: base(formMain)
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave && !e.UpdateDigital);
			});
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.DigitalProductThemeBar.RecalcLayout();
				Controller.Instance.DigitalProductPanel.PerformLayout();
			};
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = true;
				if (SettingsNotSaved)
					SaveSchedule();
				return result;
			}
		}

		public void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			InitThemeSelector();
			if (!quickLoad || LocalSchedule.DigitalProducts.Count != _tabPages.Count)
			{
				checkEditShowFlightDates.Text = String.Format("{0}", LocalSchedule.FlightDates);

				bool temp = AllowApplyValues;
				AllowApplyValues = false;
				AllowApplyValues = temp;
				Application.DoEvents();

				xtraTabControlProducts.SuspendLayout();
				Application.DoEvents();
				xtraTabControlProducts.SelectedPageChanged -= xtraTabControlProducts_SelectedPageChanged;
				xtraTabControlProducts.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.DigitalProducts.Select(y => y.UniqueID).Contains(x.Product.UniqueID));
				foreach (var product in LocalSchedule.DigitalProducts)
				{
					if (string.IsNullOrEmpty(product.Name)) continue;
					var productTab = _tabPages.FirstOrDefault(x => x.Product.UniqueID.Equals(product.UniqueID));
					if (productTab == null)
					{
						productTab = new DigitalProductControl(this);
						AssignCloseActiveEditorsonOutSideClick(productTab);
						_tabPages.Add(productTab);
						Application.DoEvents();
					}
					productTab.Product = product;
					productTab.LoadValues();
					Application.DoEvents();
				}
				_tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
				xtraTabControlProducts.TabPages.AddRange(_tabPages.ToArray());

				var summaryControl = new DigitalSummaryControl(this);
				summaryControl.UpdateControls(_tabPages.Select(tp => tp.SummaryControl));
				xtraTabControlProducts.TabPages.Add(summaryControl);

				Application.DoEvents();
				xtraTabControlProducts.ResumeLayout();

				LoadProduct(_tabPages.FirstOrDefault());
				Application.DoEvents();
				xtraTabControlProducts.SelectedPageChanged += xtraTabControlProducts_SelectedPageChanged;

				AllowApplyValues = true;
			}
			else
			{
				foreach (var product in LocalSchedule.DigitalProducts)
				{
					if (!string.IsNullOrEmpty(product.Name))
					{
						var productTab = _tabPages.FirstOrDefault(x => x.Product.UniqueID.Equals(product.UniqueID));
						if (productTab != null)
						{
							productTab.Product = product;
						}
						Application.DoEvents();
					}
				}
			}
			SettingsNotSaved = false;
		}

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.DigitalProductTheme, BusinessObjects.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
			}));
		}

		public override Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct))); }
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			base.SaveSchedule(scheduleName);
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule((RegularSchedule)LocalSchedule, nameChanged, false, false, false, this);
			SettingsNotSaved = false;
			return true;
		}

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(form.ScheduleName))
					{
						if (SaveSchedule(form.ScheduleName))
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
			BusinessObjects.Instance.HelpManager.OpenHelpLink("digitalsl");
		}

		protected override IEnumerable<UserActivity> TrackOutput(IEnumerable<DigitalProductControl> tabsForOutput)
		{
			var activities = base.TrackOutput(tabsForOutput);
			foreach (var activity in activities)
				BusinessObjects.Instance.ActivityManager.AddActivity(activity);
			return activities;
		}

		public override void OutputSlides(IEnumerable<IDigitalOutputControl> tabsForOutput)
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var tabPage in tabsForOutput)
					tabPage.Output();
				FormProgress.CloseProgress();
			});
		}

		public override void ShowPreview(IEnumerable<PreviewGroup> previewGroups, Action trackOutput)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, trackOutput))
			{
				formPreview.Text = "Preview Digital Product";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
			}
		}

		public override void ShowPdf(IEnumerable<PreviewGroup> previewGroups, Action trackOutput)
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", LocalSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}

		public override HelpManager HelpManager
		{
			get { return BusinessObjects.Instance.HelpManager; }
		}

		protected override string SlideName
		{
			get { return Controller.Instance.TabDigitalProduct.Text; }
		}
	}
}