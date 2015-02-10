using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Digital
{
	[ToolboxItem(false)]
	public class DigitalProductContainerControl : DigitalProductContainer
	{
		public DigitalProductContainerControl(Form formMain)
			: base(formMain)
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave && !e.UpdateDigital);
			});
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
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			FormThemeSelector.Link(Controller.Instance.DigitalProductTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
			}));
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

		public override Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVDigitalProduct : SlideType.RadioDigitalProduct))); }
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
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("digitalsl");
		}

		protected override IEnumerable<UserActivity> TrackOutput(IEnumerable<DigitalProductControl> tabsForOutput)
		{
			var activities = base.TrackOutput(tabsForOutput);
			foreach (var activity in activities)
				BusinessWrapper.Instance.ActivityManager.AddActivity(activity);
			return activities;
		}

		public override void OutputSlides(IEnumerable<IDigitalOutputControl> tabsForOutput)
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					foreach (var tabPage in tabsForOutput)
						tabPage.Output();
					formProgress.Close();
				});
			}
		}

		public override void ShowPreview(IEnumerable<PreviewGroup> previewGroups, Action trackOutput)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, trackOutput))
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
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					var tempFileName = Path.GetTempFileName();
					RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(tempFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
					var extension = Path.GetExtension(tempFileName);
					var pdfFileName = tempFileName.Replace(extension, ".pdf");
					if (File.Exists(pdfFileName))
						try
						{
							Process.Start(pdfFileName);
						}
						catch { }
					formProgress.Close();
				});
			}
		}

		public override HelpManager HelpManager
		{
			get { return BusinessWrapper.Instance.HelpManager; }
		}

		protected override string SlideName
		{
			get { return Controller.Instance.TabDigitalProduct.Text; }
		}
	}
}