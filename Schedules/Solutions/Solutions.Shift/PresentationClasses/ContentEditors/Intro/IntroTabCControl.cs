using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Intro;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Intro
{
	[ToolboxItem(false)]
	public sealed partial class IntroTabCControl : ChildTabBaseControl
	{
		private IntroTabCInfo CustomTabInfo => (IntroTabCInfo)TabInfo;

		public IntroTabCControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

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
			clipartEditContainer4.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image), CustomTabInfo.Clipart4Configuration, TabPageContainer.ParentControl);
			clipartEditContainer4.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.IntroState.TabC.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.IntroState.TabC.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.IntroState.TabC.Clipart3);
			clipartEditContainer4.LoadData(SlideContainer.EditedContent.IntroState.TabC.Clipart4);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.IntroState.TabC.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.IntroState.TabC.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.IntroState.TabC.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.IntroState.TabC.Clipart3 = clipartEditContainer3.GetActiveClipartObject();
			SlideContainer.EditedContent.IntroState.TabC.Clipart4 = clipartEditContainer4.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.IntroState.TabC.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.IntroState.TabC.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.IntroState.TabC.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftIntroC;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.IntroState.TabC.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT02CCLIPART1", clipart1);
			var clipart2 = SlideContainer.EditedContent.IntroState.TabC.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT02CCLIPART2", clipart2);
			var clipart3 = SlideContainer.EditedContent.IntroState.TabC.Clipart3 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("SHIFT02CCLIPART3", clipart3);
			var clipart4 = SlideContainer.EditedContent.IntroState.TabC.Clipart4 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image);
			if (clipart4 != null)
				outputDataPackage.ClipartItems.Add("SHIFT02CCLIPART4", clipart4);

			var slideHeader = (SlideContainer.EditedContent.IntroState.TabC.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftIntroFile("007_intro_boutique.pptx");

			outputDataPackage.TextItems.Add("SHIFT02CHeader".ToUpper(), slideHeader);

			return outputDataPackage;
		}
		#endregion
	}
}