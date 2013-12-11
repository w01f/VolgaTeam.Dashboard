using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
using NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses;
using NewBizWiz.OnlineSchedule.DigitalPackage.Properties;

namespace NewBizWiz.OnlineSchedule.DigitalPackage.PresentationClasses
{
	public partial class DigitalPackageControl : WebPackageControl
	{
		public BusinessClasses.Schedule LocalSchedule { get; set; }

		public DigitalPackageControl()
		{
			InitializeComponent();
		}

		public DigitalPackageControl(Form formContainer)
			: base(formContainer, true)
		{
			InitializeComponent();
			pbDisabledOutput.SizeMode = PictureBoxSizeMode.Normal;
			pbDisabledOutput.Image = Resources.DigitalPackageDisabled;
			scheduleListControl.BringToFront();
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (!e.QuickSave)
					scheduleListControl.LoadSavedSchedules(LocalSchedule);
				UpdateLastModifiedDate(BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule());
			});
		}

		public override Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.Themes.FirstOrDefault(t => t.Name.Equals(LocalSchedule.ThemeName) || String.IsNullOrEmpty(LocalSchedule.ThemeName)); }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessWrapper.Instance.HelpManager; }
		}

		public override ISchedule Schedule { get { return LocalSchedule; } }

		public override DigitalPackageSettings Settings
		{
			get { return LocalSchedule.ViewSettings.DigitalPackageSettings; }
		}

		public override IEnumerable<ProductPackageRecord> PackageRecords
		{
			get { return LocalSchedule.DigitalProducts.Select(p => p.PackageRecord).ToList(); }
		}

		public override ButtonItem OptionsButtons
		{
			get { return Controller.Instance.DigitalPackageOptions; }
		}

		public override ButtonItem Preview
		{
			get { return Controller.Instance.DigitalPackagePreview; }
		}

		public override ButtonItem PowerPoint
		{
			get { return Controller.Instance.DigitalPackagePowerPoint; }
		}

		public override ButtonItem Email
		{
			get { return Controller.Instance.DigitalPackageEmail; }
		}

		public override ButtonItem Theme
		{
			get { return Controller.Instance.DigitalPackageTheme; }
		}

		public override void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			UpdateLastModifiedDate(LocalSchedule);
			FormThemeSelector.Link(Controller.Instance.DigitalPackageTheme, BusinessWrapper.Instance.ThemeManager, LocalSchedule.ThemeName, (t =>
			{
				LocalSchedule.ThemeName = t.Name;
				SettingsNotSaved = true;
			}));
			(Controller.Instance.DigitalPackageTheme.ContainerControl as RibbonBar).RecalcLayout();

			AllowApplyValues = false;
			comboBoxEditAdvertiser.Properties.Items.Clear();
			comboBoxEditAdvertiser.Properties.Items.AddRange(Core.Common.ListManager.Instance.Advertisers);
			comboBoxEditAdvertiser.EditValue = LocalSchedule.BusinessName;
			AllowApplyValues = true;

			scheduleListControl.LoadSavedSchedules(LocalSchedule);
			base.LoadSchedule(quickLoad);
		}

		private void UpdateLastModifiedDate(BusinessClasses.Schedule schedule)
		{
			laLastModified.Text = schedule.ScheduleFile != null ? String.Format("Last Modified: {0}", schedule.ScheduleFile.LastWriteTime.ToString("MM/dd/yy h:mm tt")) : String.Empty;
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			if (comboBoxEditAdvertiser.EditValue != null)
				LocalSchedule.BusinessName = comboBoxEditAdvertiser.EditValue.ToString();
			if (!string.IsNullOrEmpty(scheduleName))
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, String.IsNullOrEmpty(scheduleName), this);
			return base.SaveSchedule(scheduleName);
		}

		public override void OutputSlides()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					OnlineSchedulePowerPointHelper.Instance.AppendWebPackage(this);
					formProgress.Close();
				});
			}
		}

		public override void ShowPreview(string tempFileName)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, OnlineSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Digital Package";
				formPreview.PresentationFile = tempFileName;
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
			}
		}

		public override void Help_Click(object sender, EventArgs e)
		{
			HelpManager.OpenHelpLink("webquick");
		}

		public void Add_Click(object sender, EventArgs e)
		{
			LocalSchedule.AddProduct(null);
			GridControl.DataSource = PackageRecords;
			GridView.RefreshData();
			SettingsNotSaved = true;
			UpdateOutputState();
		}

		public void Delete_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Do you want to delete product?") != DialogResult.Yes) return;
			var focussedRecord = GridView.GetFocusedRow() as ProductPackageRecord;
			if (focussedRecord == null) return;
			LocalSchedule.DigitalProducts.Remove(focussedRecord.Parent);
			GridView.DeleteSelectedRows();
			LocalSchedule.RebuildDigitalProductIndexes();
			SettingsNotSaved = true;
			UpdateOutputState();
		}

		private void comboBoxEditAdvertiser_EditValueChanged(object sender, EventArgs e)
		{
			if (AllowApplyValues)
				SettingsNotSaved = true;
		}

		private void scheduleListControl_ScheduleChanged(object sender, ScheduleEventArgs e)
		{
			if (LocalSchedule.IsNameNotAssigned && SettingsNotSaved)
				SaveAs_Click(this, EventArgs.Empty);
			else if (SettingsNotSaved)
				SaveSchedule();
			Controller.Instance.OpenSchedule(e.ScheduleFilePath);
		}

		private void scheduleListControl_ScheduleCreated(object sender, EventArgs e)
		{
			SaveSchedule();
			Controller.Instance.CreateSchedule();
			SaveAs_Click(this, EventArgs.Empty);
		}

		private void scheduleListControl_ScheduleCloned(object sender, EventArgs e)
		{
			SaveSchedule();
			SaveAs_Click(this, EventArgs.Empty);
		}

		private void scheduleListControl_ScheduleDeleted(object sender, ScheduleEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Do you want to delete current schedule?") != DialogResult.Yes) return;
			var scheduleFile = e.ScheduleFilePath;
			if (File.Exists(scheduleFile))
				File.Delete(scheduleFile);
			Controller.Instance.CreateSchedule();
		}
	}
}
