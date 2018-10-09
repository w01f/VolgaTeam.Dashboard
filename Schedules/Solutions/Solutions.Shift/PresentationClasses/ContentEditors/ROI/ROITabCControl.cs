using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.ROI;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.ROI
{
	public partial class ROITabCControl : ChildTabBaseControl
	{
		private ROITabCInfo CustomTabInfo => (ROITabCInfo)TabInfo;

		public ROITabCControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);

			textEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabCSubheader2.EnableSelectAll();
			textEditTabCSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabCSubheader4.EnableSelectAll();
			textEditTabCSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabCSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabCSubheader7.EnableSelectAll();
			textEditTabCSubheader8.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabCSubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabCSubheader10.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabCSubheader11.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabCSubheader12.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabCSubheader13.EnableSelectAll();
			textEditTabCSubheader14.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabCSubheader15.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			textEditTabCSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabCSubheader1.Properties.NullText;
			textEditTabCSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabCSubheader3.Properties.NullText;
			textEditTabCSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? textEditTabCSubheader5.Properties.NullText;
			textEditTabCSubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? textEditTabCSubheader6.Properties.NullText;
			textEditTabCSubheader8.Properties.NullText = CustomTabInfo.SubHeader8Placeholder ?? textEditTabCSubheader8.Properties.NullText;
			textEditTabCSubheader9.Properties.NullText = CustomTabInfo.SubHeader9Placeholder ?? textEditTabCSubheader9.Properties.NullText;
			textEditTabCSubheader10.Properties.NullText = CustomTabInfo.SubHeader10Placeholder ?? textEditTabCSubheader10.Properties.NullText;
			textEditTabCSubheader11.Properties.NullText = CustomTabInfo.SubHeader11Placeholder ?? textEditTabCSubheader11.Properties.NullText;
			textEditTabCSubheader12.Properties.NullText = CustomTabInfo.SubHeader12Placeholder ?? textEditTabCSubheader12.Properties.NullText;
			textEditTabCSubheader14.Properties.NullText = CustomTabInfo.SubHeader14Placeholder ?? textEditTabCSubheader14.Properties.NullText;
			textEditTabCSubheader15.Properties.NullText = CustomTabInfo.SubHeader15Placeholder ?? textEditTabCSubheader15.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ROIState.TabC.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ROIState.TabC.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ROIState.TabC.Clipart3);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabC.SlideHeader ??
			                                    CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			checkEditTabCGroup1.Checked = SlideContainer.EditedContent.ROIState.TabC.Group1Toggle;
			checkEditTabCGroup2.Checked = SlideContainer.EditedContent.ROIState.TabC.Group2Toggle;
			checkEditTabCGroup3.Checked = SlideContainer.EditedContent.ROIState.TabC.Group3Toggle;
			checkEditTabCGroup4.Checked = SlideContainer.EditedContent.ROIState.TabC.Group4Toggle;
			checkEditTabCGroup5.Checked = SlideContainer.EditedContent.ROIState.TabC.Group5Toggle;
			checkEditTabCGroup6.Checked = SlideContainer.EditedContent.ROIState.TabC.Group6Toggle;
			checkEditTabCGroup7.Checked = SlideContainer.EditedContent.ROIState.TabC.Group7Toggle;
			checkEditTabCGroup8.Checked = SlideContainer.EditedContent.ROIState.TabC.Group8Toggle;
			checkEditTabCSubheader2.Checked = SlideContainer.EditedContent.ROIState.TabC.Subheader2Toggle;
			checkEditTabCSubheader4.Checked = SlideContainer.EditedContent.ROIState.TabC.Subheader4Toggle;
			checkEditTabCSubheader5.Checked = SlideContainer.EditedContent.ROIState.TabC.Subheader5Toggle;
			checkEditTabCSubheader7.Checked = SlideContainer.EditedContent.ROIState.TabC.Subheader7Toggle;
			checkEditTabCSubheader8.Checked = SlideContainer.EditedContent.ROIState.TabC.Subheader8Toggle;
			checkEditTabCSubheader10.Checked = SlideContainer.EditedContent.ROIState.TabC.Subheader10Toggle;
			checkEditTabCSubheader14.Checked = SlideContainer.EditedContent.ROIState.TabC.Subheader14Toggle;
			checkEditTabCFormula1.Checked = SlideContainer.EditedContent.ROIState.TabC.Formula1Toggle;
			checkEditTabCFormula2.Checked = SlideContainer.EditedContent.ROIState.TabC.Formula2Toggle;
			checkEditTabCFormula3.Checked = SlideContainer.EditedContent.ROIState.TabC.Formula3Toggle;

			textEditTabCSubheader1.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			spinEditTabCSubheader2.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabCSubheader3.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			spinEditTabCSubheader4.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			textEditTabCSubheader5.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			textEditTabCSubheader6.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			spinEditTabCSubheader7.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			textEditTabCSubheader8.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader8 ??
				CustomTabInfo.SubHeader8DefaultValue;
			textEditTabCSubheader9.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader9 ??
				CustomTabInfo.SubHeader9DefaultValue;
			textEditTabCSubheader10.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader10 ??
				CustomTabInfo.SubHeader10DefaultValue;
			textEditTabCSubheader11.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader11 ??
				CustomTabInfo.SubHeader11DefaultValue;
			textEditTabCSubheader12.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader12 ??
				CustomTabInfo.SubHeader12DefaultValue;
			spinEditTabCSubheader13.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader13 ??
				CustomTabInfo.SubHeader13DefaultValue;
			textEditTabCSubheader14.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader14 ??
				CustomTabInfo.SubHeader14DefaultValue;
			textEditTabCSubheader15.EditValue = SlideContainer.EditedContent.ROIState.TabC.Subheader15 ??
				CustomTabInfo.SubHeader15DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabCFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ROIState.TabC.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabC.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabC.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.ROIState.TabC.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.ROIState.TabC.Group1Toggle = checkEditTabCGroup1.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Group2Toggle = checkEditTabCGroup2.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Group3Toggle = checkEditTabCGroup3.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Group4Toggle = checkEditTabCGroup4.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Group5Toggle = checkEditTabCGroup5.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Group6Toggle = checkEditTabCGroup6.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Group7Toggle = checkEditTabCGroup7.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Group8Toggle = checkEditTabCGroup8.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Subheader2Toggle = checkEditTabCSubheader2.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Subheader4Toggle = checkEditTabCSubheader4.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Subheader5Toggle = checkEditTabCSubheader5.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Subheader7Toggle = checkEditTabCSubheader7.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Subheader8Toggle = checkEditTabCSubheader8.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Subheader10Toggle = checkEditTabCSubheader10.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Subheader14Toggle = checkEditTabCSubheader14.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Formula1Toggle = checkEditTabCFormula1.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Formula2Toggle = checkEditTabCFormula2.Checked;
			SlideContainer.EditedContent.ROIState.TabC.Formula3Toggle = checkEditTabCFormula3.Checked;

			SlideContainer.EditedContent.ROIState.TabC.Subheader1 = textEditTabCSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabCSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader2 = (decimal?)spinEditTabCSubheader2.EditValue != CustomTabInfo.SubHeader2DefaultValue ?
				(decimal?)spinEditTabCSubheader2.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader3 = textEditTabCSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabCSubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader4 = (decimal?)spinEditTabCSubheader4.EditValue != CustomTabInfo.SubHeader4DefaultValue ?
				(decimal?)spinEditTabCSubheader4.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader5 = textEditTabCSubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue ?
				textEditTabCSubheader5.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader6 = textEditTabCSubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue ?
				textEditTabCSubheader6.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader7 = (decimal?)spinEditTabCSubheader7.EditValue != CustomTabInfo.SubHeader7DefaultValue ?
				(decimal?)spinEditTabCSubheader7.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader8 = textEditTabCSubheader8.EditValue as String != CustomTabInfo.SubHeader8DefaultValue ?
				textEditTabCSubheader8.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader9 = textEditTabCSubheader9.EditValue as String != CustomTabInfo.SubHeader9DefaultValue ?
				textEditTabCSubheader9.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader10 = textEditTabCSubheader10.EditValue as String != CustomTabInfo.SubHeader10DefaultValue ?
				textEditTabCSubheader10.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader11 = textEditTabCSubheader11.EditValue as String != CustomTabInfo.SubHeader11DefaultValue ?
				textEditTabCSubheader11.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader12 = textEditTabCSubheader12.EditValue as String != CustomTabInfo.SubHeader12DefaultValue ?
				textEditTabCSubheader12.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader13 = (decimal?)spinEditTabCSubheader13.EditValue != CustomTabInfo.SubHeader13DefaultValue ?
				(decimal?)spinEditTabCSubheader13.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader14 = textEditTabCSubheader14.EditValue as String != CustomTabInfo.SubHeader14DefaultValue ?
				textEditTabCSubheader14.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabC.Subheader15 = textEditTabCSubheader15.EditValue as String != CustomTabInfo.SubHeader15DefaultValue ?
				textEditTabCSubheader15.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ROIState.TabC.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ROIState.TabC.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnTabCGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup1Inner.Enabled = checkEditTabCGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader2Value.Enabled = checkEditTabCSubheader2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup2Inner.Enabled = checkEditTabCGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader4Value.Enabled = checkEditTabCSubheader4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader5Value.Enabled = checkEditTabCSubheader5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup3Inner.Enabled = checkEditTabCGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader7Value.Enabled = checkEditTabCSubheader7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader8Value.Enabled = checkEditTabCSubheader8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup4Inner.Enabled = checkEditTabCGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCFormula1CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabCFormula1.Enabled = checkEditTabCFormula1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader10Value.Enabled = checkEditTabCSubheader10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup5Inner.Enabled = checkEditTabCGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCFormula2CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabCFormula2.Enabled = checkEditTabCFormula2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup6Inner.Enabled = checkEditTabCGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup7Inner.Enabled = checkEditTabCGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader14CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader14Value.Enabled = checkEditTabCSubheader14.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup8Inner.Enabled = checkEditTabCGroup8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCFormula3CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabCFormula3.Enabled = checkEditTabCFormula3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var sourceValue = (double)spinEditTabCSubheader2.Value;
			var callsCount = (double)spinEditTabCSubheader4.Value;
			var percent = (double)spinEditTabCSubheader7.Value;
			var investmentValue = (double)spinEditTabCSubheader13.Value;

			var formula1Value = Math.Ceiling(callsCount * percent / 100);
			var formula2Value = sourceValue * formula1Value;
			var formula3Value = investmentValue > 0 ? Math.Ceiling(formula2Value / investmentValue) : 0;
			formula3Value = formula3Value < formula2Value ? formula3Value : 1.0;

			simpleLabelItemTabCFormula1.CustomizationFormText = String.Format("{0:#,##0}", formula1Value);
			simpleLabelItemTabCFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabCFormula2.CustomizationFormText = String.Format("{0:$#,##0}", formula2Value);
			simpleLabelItemTabCFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabCFormula3.CustomizationFormText = String.Format("{0:#,##0} to 1", formula3Value);
			simpleLabelItemTabCFormula3.Text = String.Format("<b>{0:#,##0}   to   1</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override SlideType SlideType => SlideType.ShiftROI;

		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetShiftROIFile("roi_c.pptx");
			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ROIState.TabC.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP06CCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ROIState.TabC.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP06CCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ROIState.TabC.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP06CCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHaeder = (SlideContainer.EditedContent.ROIState.TabC.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP06CHEADER", slideHaeder);
			outputDataPackage.TextItems.Add("HEADER", slideHaeder);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ROIState.TabC.Group1Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase1".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabC.Subheader2Toggle)
					textDataItems.Add("CP06CFormulaPhrase2".ToUpper(), (SlideContainer.EditedContent.ROIState.TabC.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue ?? 0).ToString("$#,##0"));
			}

			if (SlideContainer.EditedContent.ROIState.TabC.Group2Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase3".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabC.Subheader4Toggle)
					textDataItems.Add("CP06CFormulaPhrase4".ToUpper(), (SlideContainer.EditedContent.ROIState.TabC.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue ?? 0).ToString("#,##0"));
				if (SlideContainer.EditedContent.ROIState.TabC.Subheader5Toggle)
					textDataItems.Add("CP06CFormulaPhrase5".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue);
			}

			if (SlideContainer.EditedContent.ROIState.TabC.Group3Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase6".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabC.Subheader7Toggle)
					textDataItems.Add("CP06CFormulaPhrase7".ToUpper(), (SlideContainer.EditedContent.ROIState.TabC.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue ?? 0).ToString("##0'%'"));
				if (SlideContainer.EditedContent.ROIState.TabC.Subheader8Toggle)
					textDataItems.Add("CP06CFormulaPhrase8".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue);
			}

			if (SlideContainer.EditedContent.ROIState.TabC.Group4Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase9".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader9 ?? CustomTabInfo.SubHeader9DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabC.Formula1Toggle)
					textDataItems.Add("CP06CFormulaPhrase10".ToUpper(), simpleLabelItemTabCFormula1.CustomizationFormText);
				if (SlideContainer.EditedContent.ROIState.TabC.Subheader10Toggle)
					textDataItems.Add("CP06CFormulaPhrase11".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader10 ?? CustomTabInfo.SubHeader10DefaultValue);
			}

			if (SlideContainer.EditedContent.ROIState.TabC.Group5Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase12".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader11 ?? CustomTabInfo.SubHeader11DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabC.Formula2Toggle)
					textDataItems.Add("CP06CFormulaPhrase13".ToUpper(), simpleLabelItemTabCFormula2.CustomizationFormText);
			}

			if (SlideContainer.EditedContent.ROIState.TabC.Group6Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase14".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader12 ?? CustomTabInfo.SubHeader12DefaultValue);
			}

			if (SlideContainer.EditedContent.ROIState.TabC.Group7Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase15".ToUpper(), (SlideContainer.EditedContent.ROIState.TabC.Subheader13 ?? CustomTabInfo.SubHeader13DefaultValue ?? 0).ToString("$#,##0"));
				if (SlideContainer.EditedContent.ROIState.TabC.Subheader14Toggle)
					textDataItems.Add("CP06CFormulaPhrase16".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader14 ?? CustomTabInfo.SubHeader14DefaultValue);
			}

			if (SlideContainer.EditedContent.ROIState.TabC.Group8Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase17".ToUpper(), SlideContainer.EditedContent.ROIState.TabC.Subheader15 ?? CustomTabInfo.SubHeader15DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabC.Formula3Toggle)
					textDataItems.Add("CP06CFormulaPhrase18".ToUpper(), simpleLabelItemTabCFormula3.CustomizationFormText);
			}

			return textDataItems;
		}
		#endregion
	}
}
