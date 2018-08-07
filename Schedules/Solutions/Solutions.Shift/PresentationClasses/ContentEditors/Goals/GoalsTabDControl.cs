﻿using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Goals;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Goals
{
	[ToolboxItem(false)]
	public sealed partial class GoalsTabDControl : ChildTabBaseControl
	{
		private GoalsTabDInfo CustomTabInfo => (GoalsTabDInfo)TabInfo;

		public GoalsTabDControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			memoPopupEdit1.Init(SlideContainer.ShiftInfo.ClientGoalsLists.Goals, CustomTabInfo.Combo1DefaultItem, CustomTabInfo.Combo1Configuration, SlideContainer.StyleConfiguration, SlideContainer.ResourceManager);
			memoPopupEdit1.EditValueChanged += OnEditValueChanged;

			memoPopupEdit2.Init(SlideContainer.ShiftInfo.ClientGoalsLists.Goals, CustomTabInfo.Combo2DefaultItem, CustomTabInfo.Combo2Configuration, SlideContainer.StyleConfiguration, SlideContainer.ResourceManager);
			memoPopupEdit2.EditValueChanged += OnEditValueChanged;

			memoPopupEdit3.Init(SlideContainer.ShiftInfo.ClientGoalsLists.Goals, CustomTabInfo.Combo3DefaultItem, CustomTabInfo.Combo3Configuration, SlideContainer.StyleConfiguration, SlideContainer.ResourceManager);
			memoPopupEdit3.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.GoalsState.TabD.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.GoalsState.TabD.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.GoalsState.TabD.Clipart3);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.GoalsState.TabD.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			memoPopupEdit1.LoadData(SlideContainer.EditedContent.GoalsState.TabD.Combo1);
			memoPopupEdit2.LoadData(SlideContainer.EditedContent.GoalsState.TabD.Combo2);
			memoPopupEdit3.LoadData(SlideContainer.EditedContent.GoalsState.TabD.Combo3);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.GoalsState.TabD.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.GoalsState.TabD.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.GoalsState.TabD.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.GoalsState.TabD.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.GoalsState.TabD.Combo1 = memoPopupEdit1.GetSelectedItem();
			SlideContainer.EditedContent.GoalsState.TabD.Combo2 = memoPopupEdit2.GetSelectedItem();
			SlideContainer.EditedContent.GoalsState.TabD.Combo3 = memoPopupEdit3.GetSelectedItem();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.GoalsState.TabD.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.GoalsState.TabD.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftGoalsD;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.GoalsState.TabD.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT04DCLIPART1", clipart1);
			var clipart2 = SlideContainer.EditedContent.GoalsState.TabD.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT04DCLIPART2", clipart2);
			var clipart3 = SlideContainer.EditedContent.GoalsState.TabD.Clipart3 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("SHIFT04DCLIPART3", clipart3);

			var slideHeader = (SlideContainer.EditedContent.GoalsState.TabD.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var combos = new[]
				{
					(SlideContainer.EditedContent.GoalsState.TabD.Combo1??CustomTabInfo.Combo1DefaultItem)?.Value,
					(SlideContainer.EditedContent.GoalsState.TabD.Combo2 ?? CustomTabInfo.Combo2DefaultItem)?.Value,
					(SlideContainer.EditedContent.GoalsState.TabD.Combo3 ?? CustomTabInfo.Combo3DefaultItem)?.Value,
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftGoalsFile("016_goals_regional.pptx");

			outputDataPackage.TextItems.Add("SHIFT04DHeader".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("SHIFT04DMULTIBOX1".ToUpper(), combos.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("SHIFT04DMULTIBOX2".ToUpper(), combos.ElementAtOrDefault(1));
			outputDataPackage.TextItems.Add("SHIFT04DMULTIBOX3".ToUpper(), combos.ElementAtOrDefault(2));

			return outputDataPackage;
		}
		#endregion
	}
}