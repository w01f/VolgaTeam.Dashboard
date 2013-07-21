using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
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
				foreach (DigitalProduct product in LocalSchedule.DigitalProducts)
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
				;
				AllowApplyValues = true;
			}
			else
			{
				foreach (DigitalProduct product in LocalSchedule.DigitalProducts)
				{
					if (!string.IsNullOrEmpty(product.Name))
					{
						var productTab = _tabPages.Where(x => x.Product.UniqueID.Equals(product.UniqueID)).FirstOrDefault();
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

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
			{
				LocalSchedule.Name = scheduleName;
			}
			LocalSchedule.ViewSettings.DigitalSchedulesViewSettings.ShowOptions = Controller.Instance.DigitalProductOptions.Checked;
			foreach (DigitalProductControl product in xtraTabControlProducts.TabPages.OfType<DigitalProductControl>())
				product.SaveValues();

			Controller.Instance.SaveSchedule(LocalSchedule, true, this);
			SettingsNotSaved = false;
			return true;
		}

		public void Options_CheckedChanged(object sender, EventArgs e)
		{
			splitContainerControl.PanelVisibility = Controller.Instance.DigitalProductOptions.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			if (AllowApplyValues)
				SettingsNotSaved = true;
		}

		public void Reset_Click(object sender, EventArgs e)
		{
			var selectedProductControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl;
			if (selectedProductControl != null)
			{
				selectedProductControl.Product.ApplyDefaultView();
				LoadProduct();
				selectedProductControl.ResetProductName(this, new OpenLinkEventArgs(String.Empty));
				selectedProductControl.UpdateView();
				SaveSchedule();
			}
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

		public void Preview_Click(object sender, EventArgs e)
		{
			using (var form = new FormSelectPublication())
			{
				form.Text = "Digiotal Product Output Preview";
				form.pbLogo.Image = Resources.Preview;
				form.laTitle.Text = "You have Several Digital Slides…";
				form.buttonXCurrentPublication.Text = "Preview just the Current Digital Product";
				form.buttonXSelectedPublications.Text = "Preview all Digital Products";
				foreach (var tabPage in _tabPages)
				{
					tabPage.SaveValues();
					form.checkedListBoxControlPublications.Items.Add(tabPage.Product.UniqueID, tabPage.Product.Name, CheckState.Checked, true);
				}
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
						OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, new[] { (xtraTabControlProducts.SelectedTabPage as DigitalProductControl).Product });
					else if (result == DialogResult.No)
					{
						var outputProducts = new List<DigitalProduct>();
						foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
						{
							if (item.CheckState == CheckState.Checked)
							{
								var tabPage = _tabPages.Where(x => x.Product.UniqueID.Equals(item.Value)).FirstOrDefault();
								if (tabPage != null)
									outputProducts.Add(tabPage.Product);
							}
						}
						OnlineSchedulePowerPointHelper.Instance.PrepareScheduleEmail(tempFileName, outputProducts.ToArray());
					}
					formProgress.Close();
					if (File.Exists(tempFileName))
						using (var formPreview = new FormPreview())
						{
							formPreview.Text = "Preview Digital Product";
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
								Utilities.Instance.ActivatePowerPoint(OnlineSchedulePowerPointHelper.Instance.PowerPointObject);
								Utilities.Instance.ActivateMiniBar();
							}
						}
				}
			}
		}
	}
}