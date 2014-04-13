using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Dashboard;
using NewBizWiz.Dashboard.InteropClasses;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.Dashboard.ListManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class SlideTargetCustomersControl : SlideBaseControl
	{
		private bool _allowToSave;
		private readonly SuperTooltipInfo _toolTip = new SuperTooltipInfo("HELP", "", "Help me with the Target Customer Slide", null, null, eTooltipColor.Gray);

		public SlideTargetCustomersControl()
			: base()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.TargetCustomersLists.Headers);

			checkedListBoxControlTargetDemo.Items.Clear();
			checkedListBoxControlTargetDemo.Items.AddRange(ListManager.Instance.TargetCustomersLists.Demos.ToArray());

			checkedListBoxControlHouseholdIncome.Items.Clear();
			checkedListBoxControlHouseholdIncome.Items.AddRange(ListManager.Instance.TargetCustomersLists.HHIs.ToArray());

			checkedListBoxControlGeographicResidence.Items.Clear();
			checkedListBoxControlGeographicResidence.Items.AddRange(ListManager.Instance.TargetCustomersLists.Geographies.ToArray());

			checkEditSolutionNew.EditValueChanged += EditValueChanged;
			checkEditSolutionOld.EditValueChanged += EditValueChanged;

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (!SettingsNotSaved) return;
				SaveState();
				ViewSettingsManager.Instance.TargetCustomersState.Save();
			};

			LoadSavedState();
		}

		public override string SlideName
		{
			get { return "Target Audience"; }
		}

		public override SuperTooltipInfo Tooltip
		{
			get { return _toolTip; }
		}

		public override ButtonItem ThemeButton
		{
			get { return FormMain.Instance.buttonItemHomeThemeTargetCustomers; }
		}

		public bool SettingsNotSaved { get; set; }

		private void LoadSavedState()
		{
			_allowToSave = false;
			checkEditSolutionNew.Checked = ViewSettingsManager.Instance.TargetCustomersState.IsNewSolution;
			checkEditSolutionOld.Checked = !ViewSettingsManager.Instance.TargetCustomersState.IsNewSolution;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.TargetCustomersState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.TargetCustomersState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}

			foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Demo.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Income.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Geographic.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;

			_allowToSave = true;
			SettingsNotSaved = false;

			UpdateSavedFilesState();
			UpdateOutputState();
		}

		private void SaveState()
		{
			ViewSettingsManager.Instance.TargetCustomersState.IsNewSolution = checkEditSolutionNew.Checked;

			ViewSettingsManager.Instance.TargetCustomersState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;

			ViewSettingsManager.Instance.TargetCustomersState.Demo.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
				if (item.CheckState == CheckState.Checked)
					ViewSettingsManager.Instance.TargetCustomersState.Demo.Add(item.Value.ToString());
			ViewSettingsManager.Instance.TargetCustomersState.Income.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
				if (item.CheckState == CheckState.Checked)
					ViewSettingsManager.Instance.TargetCustomersState.Income.Add(item.Value.ToString());
			ViewSettingsManager.Instance.TargetCustomersState.Geographic.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
				if (item.CheckState == CheckState.Checked)
					ViewSettingsManager.Instance.TargetCustomersState.Geographic.Add(item.Value.ToString());
			SettingsNotSaved = false;
		}

		public override void LoadClick()
		{
			using (var form = new FormSavedTargetCustomers())
			{
				if (form.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(form.SelectedFile)) return;
				ViewSettingsManager.Instance.TargetCustomersState.Load(form.SelectedFile);
				LoadSavedState();
			}
		}

		private void checkedListBoxControl_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (!_allowToSave) return;
			UpdateOutputState();
			SettingsNotSaved = true;
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SettingsNotSaved = true;
		}

		#region Output Staff
		public string Title
		{
			get { return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString(); }
		}

		public string TargetDemo
		{
			get
			{
				string result = string.Empty;
				foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.CheckedItems)
					result += ", " + item.Value;
				if (!string.IsNullOrEmpty(result))
					result = result.Substring(2);
				return result;
			}
		}

		public string HHI
		{
			get
			{
				string result = string.Empty;
				foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.CheckedItems)
					result += ", " + item.Value;
				if (!string.IsNullOrEmpty(result))
					result = result.Substring(2);
				return result;
			}
		}

		public string Geography
		{
			get
			{
				string result = string.Empty;
				foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.CheckedItems)
					result += ", " + item.Value;
				if (!string.IsNullOrEmpty(result))
					result = result.Substring(2);
				return result;
			}
		}

		public void UpdateOutputState()
		{
			SetOutputState(checkedListBoxControlGeographicResidence.CheckedItems.Count > 0 && checkedListBoxControlHouseholdIncome.CheckedItems.Count > 0 && checkedListBoxControlTargetDemo.CheckedItems.Count > 0);
		}

		public void UpdateSavedFilesState()
		{
			SetLoadState(ViewSettingsManager.Instance.TargetCustomersState.AllowToLoad());
		}

		private void SaveChanges()
		{
			if (!SettingsNotSaved) return;
			SaveState();
			ViewSettingsManager.Instance.TargetCustomersState.Save();
			UpdateSavedFilesState();
		}

		public void TrackOutput()
		{
			var otherOptions = new Dictionary<string, object>();
			otherOptions.Add("IsNewSolution", ViewSettingsManager.Instance.TargetCustomersState.IsNewSolution);
			AppManager.Instance.ActivityManager.AddActivity(new OutputActivity(SlideName, null, null, otherOptions));
		}

		public void Output()
		{
			SaveChanges();
			TrackOutput();
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				form.TopMost = true;
				form.Show();
				AppManager.Instance.ShowFloater(null, () =>
				{
					DashboardPowerPointHelper.Instance.AppendTargetCustomers();
					form.Close();
				});
			}
		}

		public void Preview()
		{
			SaveChanges();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				DashboardPowerPointHelper.Instance.PrepareTargetCustomers(tempFileName);
				Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater, TrackOutput))
				{
					formPreview.Text = "Preview Slides";
					formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					var previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = false;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (previewResult != DialogResult.OK)
						AppManager.Instance.ActivateMainForm();
				}
			}
		}
		#endregion
	}
}