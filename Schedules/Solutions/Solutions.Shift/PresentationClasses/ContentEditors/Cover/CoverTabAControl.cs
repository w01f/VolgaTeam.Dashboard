using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Cover;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Cover
{
	[ToolboxItem(false)]
	public sealed partial class CoverTabAControl : ChildTabBaseControl
	{
		private readonly DateTime _defaultDate = DateTime.Today;
		private CoverTabAInfo CustomTabInfo => (CoverTabAInfo)TabInfo;

		public CoverTabAControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(TabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				TabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";
			memoEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.CoverState.TabA.Clipart1);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CoverState.TabA.SlideHeader ??
				TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
			memoEditSubheader1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;

			dateEditCalendar1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue
				? SlideContainer.EditedContent.CoverState.TabA.Calendar1 ?? _defaultDate
				: _defaultDate;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CoverState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.CoverState.TabA.SlideHeader = slideHeaderValue != TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;
			SlideContainer.EditedContent.CoverState.TabA.Subheader1 =
				memoEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? memoEditSubheader1.EditValue as String ?? String.Empty
					: null;

			SlideContainer.EditedContent.CoverState.TabA.Calendar1 = (DateTime?)dateEditCalendar1.EditValue == _defaultDate ? null : (DateTime?)dateEditCalendar1.EditValue;

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftCoverA;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart = SlideContainer.EditedContent.CoverState.TabA.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart != null)
				outputDataPackage.ClipartItems.Add("SHIFT01ACLIPART1", clipart);

			var slideHeader = (SlideContainer.EditedContent.CoverState.TabA.SlideHeader ??
							   TabInfo.HeadersItems.FirstOrDefault(h =>
								   h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.CoverState.TabA.Subheader1 ??
							 CustomTabInfo.SubHeader1DefaultValue;
			var calendar1 = SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue
				? SlideContainer.EditedContent.CoverState.TabA.Calendar1 ?? _defaultDate
				: (DateTime?)null;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftCoverFile("001_cover_direct.pptx");

			outputDataPackage.TextItems.Add("SHIFT01AHeader".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("SHIFT01ACalendar1".ToUpper(), calendar1?.ToString("MMMM d, yyyy") ?? String.Empty);
			outputDataPackage.TextItems.Add("SHIFT01ASubheader1".ToUpper(), subHeader1);

			return outputDataPackage;
		}
		#endregion
	}
}