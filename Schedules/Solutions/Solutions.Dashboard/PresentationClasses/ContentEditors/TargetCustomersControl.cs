﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Configuration;
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
		public override string ControlName => SlideContainer.DashboardInfo.TargeCustomersTitle;

		public TargetCustomersControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Text = ControlName;

			comboBoxEditSlideHeader.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Headers.Where(item => !item.IsPlaceholder).Select(item => item.Value).ToArray());

			checkedListBoxControlTargetDemo.Items.Clear();
			checkedListBoxControlTargetDemo.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Demos.Where(item => !item.IsPlaceholder).ToArray());

			checkedListBoxControlHouseholdIncome.Items.Clear();
			checkedListBoxControlHouseholdIncome.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.HHIs.Where(item => !item.IsPlaceholder).ToArray());

			checkedListBoxControlGeographicResidence.Items.Clear();
			checkedListBoxControlGeographicResidence.Items.AddRange(SlideContainer.DashboardInfo.TargetCustomersLists.Geographies.Where(item => !item.IsPlaceholder).ToArray());

			pictureEditSplash.Image = SlideContainer.DashboardInfo.GraphicResources?.TargeCustomersSplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControl.MaximumSize = RectangleHelper.ScaleSize(layoutControl.MaximumSize, scaleFactor);
			layoutControl.MinimumSize = RectangleHelper.ScaleSize(layoutControl.MinimumSize, scaleFactor);
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
			if (SlideContainer.EditedContent.TargetCustomersState.Demo.Any())
			{
				foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
					if (SlideContainer.EditedContent.TargetCustomersState.Demo.Contains(item.Value.ToString()))
						item.CheckState = CheckState.Checked;
			}
			else
			{
				foreach (CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
					item.CheckState = ((ListDataItem)item.Value).IsDefault ? CheckState.Checked : CheckState.Unchecked;
			}

			checkedListBoxControlHouseholdIncome.UnCheckAll();
			if (SlideContainer.EditedContent.TargetCustomersState.Income.Any())
			{
				foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
					if (SlideContainer.EditedContent.TargetCustomersState.Income.Contains(item.Value.ToString()))
						item.CheckState = CheckState.Checked;
			}
			else
			{
				foreach (CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
					item.CheckState = ((ListDataItem)item.Value).IsDefault ? CheckState.Checked : CheckState.Unchecked;
			}

			checkedListBoxControlGeographicResidence.UnCheckAll();
			if (SlideContainer.EditedContent.TargetCustomersState.Geographic.Any())
			{
				foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
					if (SlideContainer.EditedContent.TargetCustomersState.Geographic.Contains(item.Value.ToString()))
						item.CheckState = CheckState.Checked;
			}
			else
			{
				foreach (CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
					item.CheckState = ((ListDataItem)item.Value).IsDefault ? CheckState.Checked : CheckState.Unchecked;
			}

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

		public OutputGroup GetOutputData()
		{
			return new OutputGroup
			{
				Name = ControlName,
				IsCurrent = SlideContainer.SelectedSlideType == SlideType,
				Items = new List<OutputItem>(new[]
				{
					new OutputItem
					{
						Name = ControlName,
						PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = 1,
						IsCurrent = true,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							processor.AppendDashboardTargetCustomers(this,destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							processor.PrepareDashboardTargetCustomers(this, presentationSourcePath);
						}
					}
				})
			};
		}
		#endregion
	}
}