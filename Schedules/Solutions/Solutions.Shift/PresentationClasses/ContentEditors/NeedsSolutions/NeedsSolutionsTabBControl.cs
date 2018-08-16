using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions
{
	[ToolboxItem(false)]
	public sealed partial class NeedsSolutionsTabBControl : ChildTabBaseControl
	{
		private NeedsSolutionsTabBInfo CustomTabInfo => (NeedsSolutionsTabBInfo)TabInfo;

		public NeedsSolutionsTabBControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			itemControl1.Init(CustomTabInfo.NeedsList,
				null,//CustomTabInfo.ItemInfoList.ElementAtOrDefault(0),
				CustomTabInfo.Combo1Placeholder,
				CustomTabInfo.Combo1Configuration,
				CustomTabInfo.SubHeader1Configuration,
				SlideContainer.StyleConfiguration);
			itemControl1.EditValueChanged += OnEditValueChanged;

			itemControl2.Init(CustomTabInfo.NeedsList,
				null,//CustomTabInfo.ItemInfoList.ElementAtOrDefault(1),
				CustomTabInfo.Combo2Placeholder,
				CustomTabInfo.Combo2Configuration,
				CustomTabInfo.SubHeader2Configuration,
				SlideContainer.StyleConfiguration);
			itemControl2.EditValueChanged += OnEditValueChanged;

			itemControl3.Init(CustomTabInfo.NeedsList,
				null,//CustomTabInfo.ItemInfoList.ElementAtOrDefault(2),
				CustomTabInfo.Combo3Placeholder,
				CustomTabInfo.Combo3Configuration,
				CustomTabInfo.SubHeader3Configuration,
				SlideContainer.StyleConfiguration);
			itemControl3.EditValueChanged += OnEditValueChanged;

			itemControl4.Init(CustomTabInfo.NeedsList,
				null,//CustomTabInfo.ItemInfoList.ElementAtOrDefault(0),
				CustomTabInfo.Combo4Placeholder,
				CustomTabInfo.Combo1Configuration,
				CustomTabInfo.SubHeader1Configuration,
				SlideContainer.StyleConfiguration);
			itemControl4.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart3);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.NeedsSolutionsState.TabB.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			itemControl1.LoadData(SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState1);
			itemControl2.LoadData(SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState2);
			itemControl3.LoadData(SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState3);
			itemControl4.LoadData(SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState4);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.NeedsSolutionsState.TabB.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState1 = itemControl1.GetSavedState();
			SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState2 = itemControl2.GetSavedState();
			SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState3 = itemControl3.GetSavedState();
			SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState4 = itemControl4.GetSavedState();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.NeedsSolutionsState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.NeedsSolutionsState.TabB.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
			SlideContainer.RaiseSlideTypeChanged();
		}

		#region Output
		public override bool ReadyForOutput => new[]
			{
				itemControl1.GetSavedState(),
				itemControl2.GetSavedState(),
				itemControl3.GetSavedState(),
				itemControl4.GetSavedState()
			}
			.Any(item => item != null);

		public override SlideType SlideType
		{
			get
			{
				var activeItems = new[]
					{
						itemControl1.GetSavedState(),
						itemControl2.GetSavedState(),
						itemControl3.GetSavedState(),
						itemControl4.GetSavedState()
					}.Where(item => item != null)
					.ToList();

				switch (activeItems.Count)
				{
					case 1:
						return SlideType.ShiftNeedsSolutionsB4;
					case 2:
						return SlideType.ShiftNeedsSolutionsB3;
					case 3:
						return SlideType.ShiftNeedsSolutionsB2;
					case 4:
						return SlideType.ShiftNeedsSolutionsB1;
					default:
						return SlideType.ShiftNeedsSolutionsB4;
				}
			}
		}

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart1 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
			{
				clipart1.OutputBackground = true;
				outputDataPackage.ClipartItems.Add("SHIFT07BCLIPART1", clipart1);
			}
			var clipart2 = SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart2 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT07BCLIPART2", clipart2);
			var clipart3 = SlideContainer.EditedContent.NeedsSolutionsState.TabB.Clipart3 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("SHIFT07BCLIPART3", clipart3);

			var activeItems = new[]
				{
					SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState1,
					SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState2,
					SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState3,
					SlideContainer.EditedContent.NeedsSolutionsState.TabB.ItemState4,
				}.Where(item => item != null)
				.ToList();

			switch (activeItems.Count)
			{
				case 1:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("033_marketing_needs_b4.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("032_marketing_needs_b3.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("031_marketing_needs_b2.pptx");
					break;
				case 4:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("030_marketing_needs_b1.pptx");
					break;
				default:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("033_marketing_needs_b4.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("SHIFT07BHEADER".ToUpper(), (SlideContainer.EditedContent.NeedsSolutionsState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			for (var i = 0; i < activeItems.Count; i++)
			{
				outputDataPackage.TextItems.Add(String.Format("SHIFT07BCOMBO{0}", i + 1).ToUpper(), activeItems[i].Title);
				outputDataPackage.TextItems.Add(String.Format("SHIFT07BLINKTEXT{0}", i + 1).ToUpper(), activeItems[i].Subheader);
			}

			return outputDataPackage;
		}
		#endregion
	}
}