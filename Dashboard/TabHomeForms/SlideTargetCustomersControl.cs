using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Dashboard.Dictionaries;
using Asa.Business.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class SlideTargetCustomersControl : SlideBaseControl
	{
		private bool _allowToSave;
		private readonly SuperTooltipInfo _toolTipLoad = new SuperTooltipInfo("Target Customer Slides", "", "Open previously-saved Target Customer slide data files", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _toolTipHelp = new SuperTooltipInfo("HELP", "", "Help me with the Target Customer Slide", null, null, eTooltipColor.Gray);

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

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (!SettingsNotSaved) return;
				SaveState();
				ViewSettingsManager.Instance.TargetCustomersState.Save();
			};

			LoadSavedState();
		}

		public override string SlideName => "Target Audience";

		public override SuperTooltipInfo TooltipLoad => _toolTipLoad;

		public override SuperTooltipInfo TooltipHelp => _toolTipHelp;

		public override ButtonItem ThemeButton => FormMain.Instance.buttonItemHomeThemeTargetCustomers;

		private void LoadSavedState()
		{
			_allowToSave = false;
			checkEditSolutionNew.Checked = ViewSettingsManager.Instance.TargetCustomersState.IsNewSolution;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.TargetCustomersState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
				comboBoxEditSlideHeader.EditValue = ViewSettingsManager.Instance.TargetCustomersState.SlideHeader;

			checkedListBoxControlTargetDemo.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Demo.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			checkedListBoxControlHouseholdIncome.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
				if (ViewSettingsManager.Instance.TargetCustomersState.Income.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			checkedListBoxControlGeographicResidence.UnCheckAll();
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
				if (form.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.TargetCustomersState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
			base.LoadClick();
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
		public string Title => comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString();

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

		protected override void UpdateSavedFilesState()
		{
			SetLoadState(ViewSettingsManager.Instance.TargetCustomersState.AllowToLoad());
		}

		protected override void SaveChanges(string fileName = "")
		{
			if (!SettingsNotSaved) return;
			SaveState();
			ViewSettingsManager.Instance.TargetCustomersState.Save(fileName);
			UpdateSavedFilesState();
		}

		public void Output()
		{
			SaveChanges();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowProgress();
			AppManager.Instance.ShowFloater(() =>
			{
				AppManager.Instance.PowerPointManager.Processor.AppendTargetCustomers();
				FormProgress.CloseProgress();
			});
		}

		public void Preview()
		{
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			//FormProgress.ShowProgress();
			//var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//AppManager.Instance.PowerPointManager.Processor.PrepareTargetCustomers(tempFileName);
			//Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			//FormProgress.CloseProgress();
			//if (!File.Exists(tempFileName)) return;
			//using (var formPreview = new FormPreview(FormMain.Instance, AppManager.Instance.PowerPointManager.Processor, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater, AppManager.Instance.CheckPowerPointRunning))
			//{
			//	formPreview.Text = "Preview Slides";
			//	formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
			//	RegistryHelper.MainFormHandle = formPreview.Handle;
			//	RegistryHelper.MaximizeMainForm = false;
			//	var previewResult = formPreview.ShowDialog();
			//	RegistryHelper.MaximizeMainForm = false;
			//	RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			//	if (previewResult != DialogResult.OK)
			//		AppManager.Instance.ActivateMainForm();
			//}
		}
		#endregion
	}
}