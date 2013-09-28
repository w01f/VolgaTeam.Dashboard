using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
using FormNewSchedule = NewBizWiz.AdSchedule.Controls.ToolForms.FormNewSchedule;
using FormPreview = NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms.FormPreview;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using Schedule = NewBizWiz.Core.AdSchedule.Schedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
	[ToolboxItem(false)]
	public class DigitalProductContainerControl : DigitalProductContainer
	{
		public DigitalProductContainerControl(Form formMain)
			: base(formMain)
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public Schedule LocalSchedule { get; set; }


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
			FormThemeSelector.Link(Controller.Instance.DigitalProductTheme, BusinessWrapper.Instance.ThemeManager, LocalSchedule.ThemeName, (t =>
			{
				LocalSchedule.ThemeName = t.Name;
				SettingsNotSaved = true;
			}));
			if (!quickLoad)
			{
				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.SlideHeaders.ToArray());
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;

				bool temp = AllowApplyValues;
				AllowApplyValues = false;
				AllowApplyValues = temp;
				Application.DoEvents();

				Controller.Instance.DigitalProductOptions.Checked = LocalSchedule.ViewSettings.DigitalSchedulesViewSettings.ShowOptions;
				Options_CheckedChanged(this, EventArgs.Empty);

				xtraTabControlProducts.SuspendLayout();
				Application.DoEvents();
				xtraTabControlProducts.SelectedPageChanged -= xtraTabControlProducts_SelectedPageChanged;
				xtraTabControlProducts.TabPages.Clear();
				_tabPages.RemoveAll(x => !LocalSchedule.DigitalProducts.Select(y => y.UniqueID).Contains(x.Product.UniqueID));
				foreach (var product in LocalSchedule.DigitalProducts)
				{
					if (!string.IsNullOrEmpty(product.Name))
					{
						DigitalProductControl productTab = _tabPages.Where(x => x.Product.UniqueID.Equals(product.UniqueID)).FirstOrDefault();
						if (productTab == null)
						{
							productTab = new DigitalProductControl(this);
							_tabPages.Add(productTab);
							Application.DoEvents();
						}
						productTab.Product = product;
						productTab.LoadValues();
						Application.DoEvents();
					}
				}
				_tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
				xtraTabControlProducts.TabPages.AddRange(_tabPages.ToArray());
				Application.DoEvents();
				xtraTabControlProducts.ResumeLayout();

				LoadProduct();
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
			get { return BusinessWrapper.Instance.ThemeManager.Themes.FirstOrDefault(t => t.Name.Equals(LocalSchedule.ThemeName) || String.IsNullOrEmpty(LocalSchedule.ThemeName)); }
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
			{
				LocalSchedule.Name = scheduleName;
			}
			LocalSchedule.ViewSettings.DigitalSchedulesViewSettings.ShowOptions = Controller.Instance.DigitalProductOptions.Checked;
			foreach (DigitalProductControl product in xtraTabControlProducts.TabPages.OfType<DigitalProductControl>())
				product.SaveValues();

			Controller.Instance.SaveSchedule(LocalSchedule, false, this);
			SettingsNotSaved = false;
			return true;
		}

		public void Options_CheckedChanged(object sender, EventArgs e)
		{
			splitContainerControl.PanelVisibility = Controller.Instance.DigitalProductOptions.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			if (AllowApplyValues)
				SettingsNotSaved = true;
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
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("digitalslides");
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

		public override void ShowPreview(string tempFileName)
		{
			using (var formPreview = new FormPreview())
			{
				formPreview.Text = "Preview Digital Product";
				formPreview.PresentationFile = tempFileName;
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				else
					Utilities.Instance.ActivateMiniBar();
			}
		}

		public override ButtonItem Preview
		{
			get { return Controller.Instance.DigitalProductPreview; }
		}
		public override ButtonItem PowerPoint
		{
			get { return Controller.Instance.DigitalProductPowerPoint; }
		}
		public override ButtonItem Email
		{
			get { return Controller.Instance.DigitalProductEmail; }
		}
		public override ButtonItem Theme
		{
			get { return Controller.Instance.DigitalProductTheme; }
		}
	}
}