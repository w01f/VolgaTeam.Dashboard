using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.ROI;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.ROI
{
	public partial class ROITabBControl : ChildTabBaseControl
	{
		private ROITabBInfo CustomTabInfo => (ROITabBInfo)TabInfo;

		public ROITabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();
			
			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader2.EnableSelectAll();
			textEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader5.EnableSelectAll();
			textEditTabBSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader8.EnableSelectAll();
			textEditTabBSubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader10.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader11.EnableSelectAll();
			textEditTabBSubheader12.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader13.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader14.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader15.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader16.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader17.EnableSelectAll();
			textEditTabBSubheader18.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader19.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader20.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader21.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader22.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader23.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader24.EnableSelectAll();
			textEditTabBSubheader25.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			textEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			textEditTabBSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabBSubheader3.Properties.NullText;
			textEditTabBSubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? textEditTabBSubheader4.Properties.NullText;
			textEditTabBSubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? textEditTabBSubheader6.Properties.NullText;
			textEditTabBSubheader7.Properties.NullText = CustomTabInfo.SubHeader7Placeholder ?? textEditTabBSubheader7.Properties.NullText;
			textEditTabBSubheader9.Properties.NullText = CustomTabInfo.SubHeader9Placeholder ?? textEditTabBSubheader9.Properties.NullText;
			textEditTabBSubheader10.Properties.NullText = CustomTabInfo.SubHeader10Placeholder ?? textEditTabBSubheader10.Properties.NullText;
			textEditTabBSubheader12.Properties.NullText = CustomTabInfo.SubHeader12Placeholder ?? textEditTabBSubheader12.Properties.NullText;
			textEditTabBSubheader13.Properties.NullText = CustomTabInfo.SubHeader13Placeholder ?? textEditTabBSubheader13.Properties.NullText;
			textEditTabBSubheader14.Properties.NullText = CustomTabInfo.SubHeader14Placeholder ?? textEditTabBSubheader14.Properties.NullText;
			textEditTabBSubheader15.Properties.NullText = CustomTabInfo.SubHeader15Placeholder ?? textEditTabBSubheader15.Properties.NullText;
			textEditTabBSubheader16.Properties.NullText = CustomTabInfo.SubHeader16Placeholder ?? textEditTabBSubheader16.Properties.NullText;
			textEditTabBSubheader18.Properties.NullText = CustomTabInfo.SubHeader18Placeholder ?? textEditTabBSubheader18.Properties.NullText;
			textEditTabBSubheader19.Properties.NullText = CustomTabInfo.SubHeader19Placeholder ?? textEditTabBSubheader19.Properties.NullText;
			textEditTabBSubheader20.Properties.NullText = CustomTabInfo.SubHeader20Placeholder ?? textEditTabBSubheader20.Properties.NullText;
			textEditTabBSubheader21.Properties.NullText = CustomTabInfo.SubHeader21Placeholder ?? textEditTabBSubheader21.Properties.NullText;
			textEditTabBSubheader22.Properties.NullText = CustomTabInfo.SubHeader22Placeholder ?? textEditTabBSubheader22.Properties.NullText;
			textEditTabBSubheader23.Properties.NullText = CustomTabInfo.SubHeader23Placeholder ?? textEditTabBSubheader23.Properties.NullText;
			textEditTabBSubheader25.Properties.NullText = CustomTabInfo.SubHeader25Placeholder ?? textEditTabBSubheader25.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ROIState.TabB.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ROIState.TabB.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ROIState.TabB.Clipart3);

			checkEditTabBGroup1.Checked = SlideContainer.EditedContent.ROIState.TabB.Group1Toggle;
			checkEditTabBGroup2.Checked = SlideContainer.EditedContent.ROIState.TabB.Group2Toggle;
			checkEditTabBGroup3.Checked = SlideContainer.EditedContent.ROIState.TabB.Group3Toggle;
			checkEditTabBGroup4.Checked = SlideContainer.EditedContent.ROIState.TabB.Group4Toggle;
			checkEditTabBGroup5.Checked = SlideContainer.EditedContent.ROIState.TabB.Group5Toggle;
			checkEditTabBGroup6.Checked = SlideContainer.EditedContent.ROIState.TabB.Group6Toggle;
			checkEditTabBGroup7.Checked = SlideContainer.EditedContent.ROIState.TabB.Group7Toggle;
			checkEditTabBGroup8.Checked = SlideContainer.EditedContent.ROIState.TabB.Group8Toggle;
			checkEditTabBGroup9.Checked = SlideContainer.EditedContent.ROIState.TabB.Group9Toggle;
			checkEditTabBGroup10.Checked = SlideContainer.EditedContent.ROIState.TabB.Group10Toggle;
			checkEditTabBGroup11.Checked = SlideContainer.EditedContent.ROIState.TabB.Group11Toggle;
			checkEditTabBSubheader24.Checked = SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle;
			checkEditTabBSubheader25.Checked = SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle;

			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			spinEditTabBSubheader2.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			textEditTabBSubheader4.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			spinEditTabBSubheader5.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			textEditTabBSubheader6.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			spinEditTabBSubheader8.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader8 ??
				CustomTabInfo.SubHeader8DefaultValue;
			textEditTabBSubheader9.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader9 ??
				CustomTabInfo.SubHeader9DefaultValue;
			textEditTabBSubheader10.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader10 ??
				CustomTabInfo.SubHeader10DefaultValue;
			spinEditTabBSubheader11.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader11 ??
				CustomTabInfo.SubHeader11DefaultValue;
			textEditTabBSubheader12.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader12 ??
				CustomTabInfo.SubHeader12DefaultValue;
			textEditTabBSubheader13.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader13 ??
				CustomTabInfo.SubHeader13DefaultValue;
			textEditTabBSubheader14.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader14 ??
				CustomTabInfo.SubHeader14DefaultValue;
			textEditTabBSubheader15.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader15 ??
				CustomTabInfo.SubHeader15DefaultValue;
			textEditTabBSubheader16.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader16 ??
				CustomTabInfo.SubHeader16DefaultValue;
			spinEditTabBSubheader17.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader17 ??
				CustomTabInfo.SubHeader17DefaultValue;
			textEditTabBSubheader18.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader18 ??
				CustomTabInfo.SubHeader18DefaultValue;
			textEditTabBSubheader19.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader19 ??
				CustomTabInfo.SubHeader19DefaultValue;
			textEditTabBSubheader20.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader20 ??
				CustomTabInfo.SubHeader20DefaultValue;
			textEditTabBSubheader21.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader21 ??
				CustomTabInfo.SubHeader21DefaultValue;
			textEditTabBSubheader22.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader22 ??
				CustomTabInfo.SubHeader22DefaultValue;
			textEditTabBSubheader23.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader23 ??
				CustomTabInfo.SubHeader23DefaultValue;
			spinEditTabBSubheader24.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader24 ??
				CustomTabInfo.SubHeader24DefaultValue;
			textEditTabBSubheader25.EditValue = SlideContainer.EditedContent.ROIState.TabB.Subheader25 ??
				CustomTabInfo.SubHeader25DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabBFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ROIState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabB.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ROIState.TabB.Group1Toggle = checkEditTabBGroup1.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group2Toggle = checkEditTabBGroup2.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group3Toggle = checkEditTabBGroup3.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group4Toggle = checkEditTabBGroup4.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group5Toggle = checkEditTabBGroup5.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group6Toggle = checkEditTabBGroup6.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group7Toggle = checkEditTabBGroup7.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group8Toggle = checkEditTabBGroup8.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group9Toggle = checkEditTabBGroup9.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group10Toggle = checkEditTabBGroup10.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Group11Toggle = checkEditTabBGroup11.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle = checkEditTabBSubheader24.Checked;
			SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle = checkEditTabBSubheader25.Checked;

			SlideContainer.EditedContent.ROIState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader2 = (decimal?)spinEditTabBSubheader2.EditValue != CustomTabInfo.SubHeader2DefaultValue ?
				(decimal?)spinEditTabBSubheader2.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabBSubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader4 = textEditTabBSubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue ?
				textEditTabBSubheader4.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader5 = (decimal?)spinEditTabBSubheader5.EditValue != CustomTabInfo.SubHeader5DefaultValue ?
				(decimal?)spinEditTabBSubheader5.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader6 = textEditTabBSubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue ?
				textEditTabBSubheader6.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader7 = textEditTabBSubheader7.EditValue as String != CustomTabInfo.SubHeader7DefaultValue ?
				textEditTabBSubheader7.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader8 = (decimal?)spinEditTabBSubheader8.EditValue != CustomTabInfo.SubHeader8DefaultValue ?
				(decimal?)spinEditTabBSubheader8.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader9 = textEditTabBSubheader9.EditValue as String != CustomTabInfo.SubHeader9DefaultValue ?
				textEditTabBSubheader9.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader10 = textEditTabBSubheader10.EditValue as String != CustomTabInfo.SubHeader10DefaultValue ?
				textEditTabBSubheader10.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader11 = (decimal?)spinEditTabBSubheader11.EditValue != CustomTabInfo.SubHeader11DefaultValue ?
				(decimal?)spinEditTabBSubheader11.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader12 = textEditTabBSubheader12.EditValue as String != CustomTabInfo.SubHeader12DefaultValue ?
				textEditTabBSubheader12.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader13 = textEditTabBSubheader13.EditValue as String != CustomTabInfo.SubHeader13DefaultValue ?
				textEditTabBSubheader13.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader14 = textEditTabBSubheader14.EditValue as String != CustomTabInfo.SubHeader14DefaultValue ?
				textEditTabBSubheader14.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader15 = textEditTabBSubheader15.EditValue as String != CustomTabInfo.SubHeader15DefaultValue ?
				textEditTabBSubheader15.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader16 = textEditTabBSubheader16.EditValue as String != CustomTabInfo.SubHeader16DefaultValue ?
				textEditTabBSubheader16.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader17 = (decimal?)spinEditTabBSubheader17.EditValue != CustomTabInfo.SubHeader17DefaultValue ?
				(decimal?)spinEditTabBSubheader17.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader18 = textEditTabBSubheader18.EditValue as String != CustomTabInfo.SubHeader18DefaultValue ?
				textEditTabBSubheader18.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader19 = textEditTabBSubheader19.EditValue as String != CustomTabInfo.SubHeader19DefaultValue ?
				textEditTabBSubheader19.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader20 = textEditTabBSubheader20.EditValue as String != CustomTabInfo.SubHeader20DefaultValue ?
				textEditTabBSubheader20.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader21 = textEditTabBSubheader21.EditValue as String != CustomTabInfo.SubHeader21DefaultValue ?
				textEditTabBSubheader21.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader22 = textEditTabBSubheader22.EditValue as String != CustomTabInfo.SubHeader22DefaultValue ?
				textEditTabBSubheader22.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader23 = textEditTabBSubheader23.EditValue as String != CustomTabInfo.SubHeader23DefaultValue ?
				textEditTabBSubheader23.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader24 = (decimal?)spinEditTabBSubheader24.EditValue != CustomTabInfo.SubHeader24DefaultValue ?
				(decimal?)spinEditTabBSubheader24.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabB.Subheader25 = textEditTabBSubheader25.EditValue as String != CustomTabInfo.SubHeader25DefaultValue ?
				textEditTabBSubheader25.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.ROIState.TabB.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ROIState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.ROIState.TabB.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ROIState.TabB.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnTabBGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup1Inner.Enabled = checkEditTabBGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup2Inner.Enabled = checkEditTabBGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup3Inner.Enabled = checkEditTabBGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup4Inner.Enabled = checkEditTabBGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup5Inner.Enabled = checkEditTabBGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup6Inner.Enabled = checkEditTabBGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup7Inner.Enabled = checkEditTabBGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup8Inner.Enabled = checkEditTabBGroup8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup9CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup9Inner.Enabled = checkEditTabBGroup9.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup10Inner.Enabled = checkEditTabBGroup10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup11CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup11Inner.Enabled = checkEditTabBGroup11.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBSubheader24CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader24Value.Enabled = checkEditTabBSubheader24.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBSubheader25CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader25Value.Enabled = checkEditTabBSubheader25.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var avgValue = (double)spinEditTabBSubheader2.Value;
			var highValue = (double)spinEditTabBSubheader5.Value;
			var callsCount = (double)spinEditTabBSubheader8.Value;
			var avgPercent = (double)spinEditTabBSubheader11.Value;
			var highPercent = (double)spinEditTabBSubheader17.Value;
			var investmentValue = (double)spinEditTabBSubheader24.Value;

			var avgFormula1Value = Math.Ceiling(callsCount * avgPercent / 100);
			var avgFormula2Value = avgValue * avgFormula1Value;


			var highFormula1Value = Math.Ceiling(callsCount * highPercent / 100);
			var highFormula2Value = highValue * highFormula1Value;

			var totalValue = avgFormula2Value + highFormula2Value;

			var formula3Value = investmentValue > 0 ? Math.Ceiling(totalValue / investmentValue) : 0;
			formula3Value = formula3Value < totalValue ? formula3Value : 1.0;

			simpleLabelItemTabBFormula1.CustomizationFormText = String.Format("{0:#,##0}", avgFormula1Value);
			simpleLabelItemTabBFormula1.Text = String.Format("<b>{0:#,##0}</b>", avgFormula1Value);
			simpleLabelItemTabBFormula2.CustomizationFormText = String.Format("{0:$#,##0}", avgFormula2Value);
			simpleLabelItemTabBFormula2.Text = String.Format("<b>{0:$#,##0}</b>", avgFormula2Value);

			simpleLabelItemTabBFormula3.CustomizationFormText = String.Format("{0:#,##0}", highFormula1Value);
			simpleLabelItemTabBFormula3.Text = String.Format("<b>{0:#,##0}</b>", highFormula1Value);
			simpleLabelItemTabBFormula4.CustomizationFormText = String.Format("{0:$#,##0}", highFormula2Value);
			simpleLabelItemTabBFormula4.Text = String.Format("<b>{0:$#,##0}</b>", highFormula2Value);

			simpleLabelItemTabBFormula5.CustomizationFormText = String.Format("{0:$#,##0}", totalValue);
			simpleLabelItemTabBFormula5.Text = String.Format("<b>{0:$#,##0}</b>", totalValue);

			layoutControlItemTabBSubheader25Value.CustomizationFormText = String.Format("{0:#,##0} : 1", formula3Value);
			layoutControlItemTabBSubheader25Value.Text = String.Format("= <b>{0:#,##0} : 1</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarROIFile("CP06B-1.pptx");
			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ROIState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP06BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ROIState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP06BCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ROIState.TabB.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP06BCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ROIState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP06BHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ROIState.TabB.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabB.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabB.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase2".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabB.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue ?? 0).ToString("#,##0"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader9 ?? CustomTabInfo.SubHeader9DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader10 ?? CustomTabInfo.SubHeader10DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabB.Subheader11 ?? CustomTabInfo.SubHeader11DefaultValue ?? 0).ToString("##0'%'"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader12 ?? CustomTabInfo.SubHeader12DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader13 ?? CustomTabInfo.SubHeader13DefaultValue);
				itemParts.Add(simpleLabelItemTabBFormula1.CustomizationFormText);
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader14 ?? CustomTabInfo.SubHeader14DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader15 ?? CustomTabInfo.SubHeader15DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group7Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader16 ?? CustomTabInfo.SubHeader16DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ROIState.TabB.Subheader17 ?? CustomTabInfo.SubHeader17DefaultValue ?? 0).ToString("##0'%'"));
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader18 ?? CustomTabInfo.SubHeader18DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase7".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group8Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader19 ?? CustomTabInfo.SubHeader19DefaultValue);
				itemParts.Add(simpleLabelItemTabBFormula3.CustomizationFormText);
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader20 ?? CustomTabInfo.SubHeader20DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase8".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group9Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader21 ?? CustomTabInfo.SubHeader21DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula4.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase9".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group10Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader22 ?? CustomTabInfo.SubHeader22DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase10".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ROIState.TabB.Group11Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ROIState.TabB.Subheader23 ?? CustomTabInfo.SubHeader23DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase11".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle)
					textDataItems.Add("CP06BFormulaPhrase12".ToUpper(), String.Format("{0:$#,##0} Per Month", (SlideContainer.EditedContent.ROIState.TabB.Subheader24 ?? CustomTabInfo.SubHeader24DefaultValue ?? 0)));

				if (SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle)
					textDataItems.Add("CP06BFormulaPhrase13".ToUpper(), String.Format("{0} = {1}", SlideContainer.EditedContent.ROIState.TabB.Subheader25 ?? CustomTabInfo.SubHeader25DefaultValue, layoutControlItemTabBSubheader25Value.CustomizationFormText));
			}

			return textDataItems;
		}
		#endregion
	}
}
