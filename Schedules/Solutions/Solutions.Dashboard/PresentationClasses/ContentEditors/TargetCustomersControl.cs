using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class TargetCustomersControl : DashboardSlideControl, ITargetCustomersOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		public override SlideType SlideType => SlideType.TargetCustomers;
		public string SlideName => "D. Target Customer";

		public TargetCustomersControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Text = SlideName;

			comboBoxEditSlideHeader.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Headers);

			checkedListBoxControlTargetDemo.Items.Clear();
			checkedListBoxControlTargetDemo.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Demos.ToArray());

			checkedListBoxControlHouseholdIncome.Items.Clear();
			checkedListBoxControlHouseholdIncome.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.HHIs.ToArray());

			checkedListBoxControlGeographicResidence.Items.Clear();
			checkedListBoxControlGeographicResidence.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Geographies.ToArray());

			pictureEditSplash.Image = SlideContainer.DashboardInfo.TargeCustomersSplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			checkedListBoxControlTargetDemo.ItemHeight = (Int32)(checkedListBoxControlTargetDemo.ItemHeight * scaleFactor.Height);
			checkedListBoxControlHouseholdIncome.ItemHeight = (Int32)(checkedListBoxControlHouseholdIncome.ItemHeight * scaleFactor.Height);
			checkedListBoxControlGeographicResidence.ItemHeight = (Int32)(checkedListBoxControlGeographicResidence.ItemHeight * scaleFactor.Height);
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
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.TargetCustomersState.SlideHeader;

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

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void OnCheckedListBoxControlItemCheck(object sender, ItemCheckEventArgs e)
		{
			OnEditValueChanged(sender, e);
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
				var result = string.Empty;
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
				var result = string.Empty;
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

		public void GenerateOutput()
		{
			SolutionDashboardPowerPointHelper.Instance.AppendTargetCustomers(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SolutionDashboardPowerPointHelper.Instance.PrepareTargetCustomers(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}