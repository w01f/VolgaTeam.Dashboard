using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.ROI;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.ROI
{
	public partial class ROITabAControl : ChildTabBaseControl
	{
		private ROITabAInfo CustomTabInfo => (ROITabAInfo)TabInfo;

		public ROITabAControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			textEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabASubheader2.EnableSelectAll();
			textEditTabASubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabASubheader5.EnableSelectAll();
			textEditTabASubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabASubheader8.EnableSelectAll();
			textEditTabASubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader10.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader11.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader12.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader13.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabASubheader14.EnableSelectAll();
			textEditTabASubheader15.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			textEditTabASubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabASubheader1.Properties.NullText;
			textEditTabASubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabASubheader3.Properties.NullText;
			textEditTabASubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? textEditTabASubheader4.Properties.NullText;
			textEditTabASubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? textEditTabASubheader6.Properties.NullText;
			textEditTabASubheader7.Properties.NullText = CustomTabInfo.SubHeader7Placeholder ?? textEditTabASubheader7.Properties.NullText;
			textEditTabASubheader9.Properties.NullText = CustomTabInfo.SubHeader9Placeholder ?? textEditTabASubheader9.Properties.NullText;
			textEditTabASubheader10.Properties.NullText = CustomTabInfo.SubHeader10Placeholder ?? textEditTabASubheader10.Properties.NullText;
			textEditTabASubheader11.Properties.NullText = CustomTabInfo.SubHeader11Placeholder ?? textEditTabASubheader11.Properties.NullText;
			textEditTabASubheader12.Properties.NullText = CustomTabInfo.SubHeader12Placeholder ?? textEditTabASubheader12.Properties.NullText;
			textEditTabASubheader13.Properties.NullText = CustomTabInfo.SubHeader13Placeholder ?? textEditTabASubheader13.Properties.NullText;
			textEditTabASubheader15.Properties.NullText = CustomTabInfo.SubHeader15Placeholder ?? textEditTabASubheader15.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ROIState.TabA.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ROIState.TabA.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ROIState.TabA.Clipart3);

			checkEditTabAGroup1.Checked = SlideContainer.EditedContent.ROIState.TabA.Group1Toggle;
			checkEditTabAGroup2.Checked = SlideContainer.EditedContent.ROIState.TabA.Group2Toggle;
			checkEditTabAGroup3.Checked = SlideContainer.EditedContent.ROIState.TabA.Group3Toggle;
			checkEditTabAGroup4.Checked = SlideContainer.EditedContent.ROIState.TabA.Group4Toggle;
			checkEditTabAGroup5.Checked = SlideContainer.EditedContent.ROIState.TabA.Group5Toggle;
			checkEditTabAGroup6.Checked = SlideContainer.EditedContent.ROIState.TabA.Group6Toggle;
			checkEditTabASubheader14.Checked = SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle;
			checkEditTabASubheader15.Checked = SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle;

			textEditTabASubheader1.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			spinEditTabASubheader2.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			spinEditTabASubheader5.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			spinEditTabASubheader8.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader8 ??
				CustomTabInfo.SubHeader8DefaultValue;
			textEditTabASubheader9.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader9 ??
				CustomTabInfo.SubHeader9DefaultValue;
			textEditTabASubheader10.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader10 ??
				CustomTabInfo.SubHeader10DefaultValue;
			textEditTabASubheader11.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader11 ??
				CustomTabInfo.SubHeader11DefaultValue;
			textEditTabASubheader12.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader12 ??
				CustomTabInfo.SubHeader12DefaultValue;
			textEditTabASubheader13.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader13 ??
				CustomTabInfo.SubHeader13DefaultValue;
			spinEditTabASubheader14.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader14 ??
				CustomTabInfo.SubHeader14DefaultValue;
			textEditTabASubheader15.EditValue = SlideContainer.EditedContent.ROIState.TabA.Subheader15 ??
				CustomTabInfo.SubHeader15DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabAFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ROIState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabA.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabA.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ROIState.TabA.Group1Toggle = checkEditTabAGroup1.Checked;
			SlideContainer.EditedContent.ROIState.TabA.Group2Toggle = checkEditTabAGroup2.Checked;
			SlideContainer.EditedContent.ROIState.TabA.Group3Toggle = checkEditTabAGroup3.Checked;
			SlideContainer.EditedContent.ROIState.TabA.Group4Toggle = checkEditTabAGroup4.Checked;
			SlideContainer.EditedContent.ROIState.TabA.Group5Toggle = checkEditTabAGroup5.Checked;
			SlideContainer.EditedContent.ROIState.TabA.Group6Toggle = checkEditTabAGroup6.Checked;
			SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle = checkEditTabASubheader14.Checked;
			SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle = checkEditTabASubheader15.Checked;

			SlideContainer.EditedContent.ROIState.TabA.Subheader1 = textEditTabASubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabASubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader2 = (decimal?)spinEditTabASubheader2.EditValue != CustomTabInfo.SubHeader2DefaultValue ?
				(decimal?)spinEditTabASubheader2.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader3 = textEditTabASubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabASubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader4 = textEditTabASubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue ?
				textEditTabASubheader4.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader5 = (decimal?)spinEditTabASubheader5.EditValue != CustomTabInfo.SubHeader5DefaultValue ?
				(decimal?)spinEditTabASubheader5.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader6 = textEditTabASubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue ?
				textEditTabASubheader6.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader7 = textEditTabASubheader7.EditValue as String != CustomTabInfo.SubHeader7DefaultValue ?
				textEditTabASubheader7.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader8 = (decimal?)spinEditTabASubheader8.EditValue != CustomTabInfo.SubHeader8DefaultValue ?
				(decimal?)spinEditTabASubheader8.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader9 = textEditTabASubheader9.EditValue as String != CustomTabInfo.SubHeader9DefaultValue ?
				textEditTabASubheader9.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader10 = textEditTabASubheader10.EditValue as String != CustomTabInfo.SubHeader10DefaultValue ?
				textEditTabASubheader10.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader11 = textEditTabASubheader11.EditValue as String != CustomTabInfo.SubHeader11DefaultValue ?
				textEditTabASubheader11.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader12 = textEditTabASubheader12.EditValue as String != CustomTabInfo.SubHeader12DefaultValue ?
				textEditTabASubheader12.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader13 = textEditTabASubheader13.EditValue as String != CustomTabInfo.SubHeader13DefaultValue ?
				textEditTabASubheader13.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader14 = (decimal?)spinEditTabASubheader14.EditValue != CustomTabInfo.SubHeader14DefaultValue ?
				(decimal?)spinEditTabASubheader14.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabA.Subheader15 = textEditTabASubheader15.EditValue as String != CustomTabInfo.SubHeader15DefaultValue ?
				textEditTabASubheader15.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ROIState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ROIState.TabA.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnTabAGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup1Inner.Enabled = checkEditTabAGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup2Inner.Enabled = checkEditTabAGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup3Inner.Enabled = checkEditTabAGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup4Inner.Enabled = checkEditTabAGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup5Inner.Enabled = checkEditTabAGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup6Inner.Enabled = checkEditTabAGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabASubheader14CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader14Value.Enabled = checkEditTabASubheader14.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabASubheader15CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader15Value.Enabled = checkEditTabASubheader15.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var sourceValue = (double)spinEditTabASubheader2.Value;
			var callsCount = (double)spinEditTabASubheader5.Value;
			var percent = (double)spinEditTabASubheader8.Value;
			var investmentValue = (double)spinEditTabASubheader14.Value;

			var formula1Value = Math.Ceiling(callsCount * percent / 100);
			var formula2Value = sourceValue * formula1Value;
			var formula3Value = investmentValue > 0 ? Math.Ceiling(formula2Value / investmentValue) : 0;
			formula3Value = formula3Value < formula2Value ? formula3Value : 1.0;

			simpleLabelItemTabAFormula1.CustomizationFormText = String.Format("{0:#,##0}", formula1Value);
			simpleLabelItemTabAFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabAFormula2.CustomizationFormText = String.Format("{0:$#,##0}", formula2Value);
			simpleLabelItemTabAFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			layoutControlItemTabASubheader15Value.CustomizationFormText = String.Format("{0:#,##0} : 1", formula3Value);
			layoutControlItemTabASubheader15Value.Text = String.Format("= <b>{0:#,##0} : 1</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override SlideType SlideType => SlideType.ShiftROI;

		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetShiftROIFile("roi_a.pptx");
			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ROIState.TabA.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP06ACLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ROIState.TabA.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP06ACLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ROIState.TabA.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP06ACLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ROIState.TabA.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP06AHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ROIState.TabA.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabA.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabA.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabA.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue ?? 0).ToString("#,##0"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase2".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabA.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabA.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue ?? 0).ToString("##0'%'"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader9 ?? CustomTabInfo.SubHeader9DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabA.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader10 ?? CustomTabInfo.SubHeader10DefaultValue);
				itemParts.Add(simpleLabelItemTabAFormula1.CustomizationFormText);
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader11 ?? CustomTabInfo.SubHeader11DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabA.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader12 ?? CustomTabInfo.SubHeader12DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabAFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabA.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabA.Subheader13 ?? CustomTabInfo.SubHeader13DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle)
					textDataItems.Add("CP06AFormulaPhrase7".ToUpper(), String.Format("{0:$#,##0} Per Month", SlideContainer.EditedContent.ROIState.TabA.Subheader14 ?? CustomTabInfo.SubHeader14DefaultValue));

				if (SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle)
					textDataItems.Add("CP06AFormulaPhrase8".ToUpper(), String.Format("{0} = {1}", SlideContainer.EditedContent.ROIState.TabA.Subheader15 ?? CustomTabInfo.SubHeader15DefaultValue, layoutControlItemTabASubheader15Value.CustomizationFormText));
			}

			return textDataItems;
		}
		#endregion
	}
}
