﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public class ScheduleSlidesControl : DigitalProductContainer
	{
		public ScheduleSlidesControl(Form formContainer)
			: base(formContainer)
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public Schedule LocalSchedule { get; set; }

		public override HelpManager HelpManager
		{
			get { return BusinessWrapper.Instance.HelpManager; }
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
				{
					if (Utilities.Instance.ShowWarningQuestion("Schedule settings have changed.\nDo you want to save changes?") == DialogResult.Yes)
					{
						if (SaveSchedule())
							result = true;
					}
				}
				else
					result = true;
				return result;
			}
		}

		public void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			FormThemeSelector.Link(Controller.Instance.DigitalSlidesTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.OnlineDigitalProduct), BusinessWrapper.Instance.GetSelectedTheme(SlideType.OnlineDigitalProduct), (t =>
			{
				BusinessWrapper.Instance.SetSelectedTheme(SlideType.OnlineWebPackage, t.Name);
				BusinessWrapper.Instance.SaveLocalSettings();
				SettingsNotSaved = true;
			}));
			if (!quickLoad)
			{
				checkEditShowFlightDates.Text = String.Format("{0}", LocalSchedule.FlightDates);

				var temp = AllowApplyValues;
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
					if (string.IsNullOrEmpty(product.Name)) continue;
					var productTab = _tabPages.FirstOrDefault(x => x.Product.UniqueID.Equals(product.UniqueID));
					if (productTab != null)
					{
						productTab.Product = product;
					}
					Application.DoEvents();
				}
			}

			SettingsNotSaved = false;
		}

		public override Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.OnlineDigitalProduct).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.OnlineDigitalProduct)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.OnlineDigitalProduct))); }
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			base.SaveSchedule(scheduleName);
			if (!string.IsNullOrEmpty(scheduleName))
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, true, this);
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
			using (var from = new FormNewSchedule())
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SaveSchedule(from.ScheduleName))
							Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
				}
			}
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("Slides");
		}

		public override void OutputSlides(IEnumerable<DigitalProductControl> tabsForOutput)
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

		public override void ShowPreview(IEnumerable<PreviewGroup> previewGroups)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, OnlineSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Digital Product";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				DialogResult previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
			}
		}
	}
}