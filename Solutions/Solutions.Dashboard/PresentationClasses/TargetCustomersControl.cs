using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	public sealed partial class TargetCustomersControl : DashboardSlideControl
	{
		private bool _allowToSave;
		public override SlideType SlideType => SlideType.TargetCustomers;
		public override string SlideName => "D. Target Customer";

		public TargetCustomersControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Text = SlideName;

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Headers);

			checkedListBoxControlTargetDemo.Items.Clear();
			checkedListBoxControlTargetDemo.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Demos.ToArray());

			checkedListBoxControlHouseholdIncome.Items.Clear();
			checkedListBoxControlHouseholdIncome.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.HHIs.ToArray());

			checkedListBoxControlGeographicResidence.Items.Clear();
			checkedListBoxControlGeographicResidence.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Geographies.ToArray());

			pbSplash.Image = SlideContainer.DashboardInfo.TargeCustomersSplashLogo;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			if (string.IsNullOrEmpty(SlideContainer.EditedContent.TargetCustomersState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(SlideContainer.EditedContent.TargetCustomersState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}

			checkedListBoxControlTargetDemo.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
				if (SlideContainer.EditedContent.TargetCustomersState.Demo.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			checkedListBoxControlHouseholdIncome.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
				if (SlideContainer.EditedContent.TargetCustomersState.Income.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
			checkedListBoxControlGeographicResidence.UnCheckAll();
			foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
				if (SlideContainer.EditedContent.TargetCustomersState.Geographic.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.TargetCustomersState.SlideHeader = comboBoxEditSlideHeader.EditValue?.ToString() ?? string.Empty;

			SlideContainer.EditedContent.TargetCustomersState.Demo.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
				if (item.CheckState == CheckState.Checked)
					SlideContainer.EditedContent.TargetCustomersState.Demo.Add(item.Value.ToString());
			SlideContainer.EditedContent.TargetCustomersState.Income.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
				if (item.CheckState == CheckState.Checked)
					SlideContainer.EditedContent.TargetCustomersState.Income.Add(item.Value.ToString());
			SlideContainer.EditedContent.TargetCustomersState.Geographic.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
				if (item.CheckState == CheckState.Checked)
					SlideContainer.EditedContent.TargetCustomersState.Geographic.Add(item.Value.ToString());
		}

		private void checkedListBoxControl_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SlideContainer.RaiseDataChanged();
		}

		#region Output Staff
		public override bool ReadyForOutput =>
				checkedListBoxControlGeographicResidence.CheckedItems.Count > 0 &&
				checkedListBoxControlHouseholdIncome.CheckedItems.Count > 0 &&
				checkedListBoxControlTargetDemo.CheckedItems.Count > 0;

		public string Title => comboBoxEditSlideHeader.EditValue?.ToString() ?? string.Empty;

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

		public override void GenerateOutput()
		{
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			//FormProgress.ShowProgress();
			//AppManager.Instance.ShowFloater(() =>
			//{
			//	DashboardPowerPointHelper.Instance.AppendTargetCustomers();
			//	FormProgress.CloseProgress();
			//});
		}

		public override PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			//FormProgress.ShowProgress();
			//var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//DashboardPowerPointHelper.Instance.PrepareTargetCustomers(tempFileName);
			//Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			//FormProgress.CloseProgress();
			//if (!File.Exists(tempFileName)) return;
			//using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
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