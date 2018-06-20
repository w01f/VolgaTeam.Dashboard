using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabDControl : ROITabBaseControl
	{
		public ROITabDControl(ROIControl roiContentContainer) : base(roiContentContainer)
		{
			InitializeComponent();

			textEditTabDSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader2.EnableSelectAll();
			textEditTabDSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader4.EnableSelectAll();
			textEditTabDSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader6.EnableSelectAll();
			textEditTabDSubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader8.EnableSelectAll();
			textEditTabDSubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader10.EnableSelectAll();
			textEditTabDSubheader11.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader12.EnableSelectAll();
			textEditTabDSubheader13.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader14.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader15.EnableSelectAll();
			textEditTabDSubheader16.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader17.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			pictureEditTabDClipart1.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart1Image;
			pictureEditTabDClipart1.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDClipart1Configuration.Alignment;
			pictureEditTabDClipart2.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart2Image;
			pictureEditTabDClipart2.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDClipart2Configuration.Alignment;
			pictureEditTabDClipart3.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart3Image;
			pictureEditTabDClipart3.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDClipart3Configuration.Alignment;

			textEditTabDSubheader1.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader1Placeholder ?? textEditTabDSubheader1.Properties.NullText;
			textEditTabDSubheader3.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader3Placeholder ?? textEditTabDSubheader3.Properties.NullText;
			textEditTabDSubheader5.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader5Placeholder ?? textEditTabDSubheader5.Properties.NullText;
			textEditTabDSubheader7.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader7Placeholder ?? textEditTabDSubheader7.Properties.NullText;
			textEditTabDSubheader9.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader9Placeholder ?? textEditTabDSubheader9.Properties.NullText;
			textEditTabDSubheader11.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader11Placeholder ?? textEditTabDSubheader11.Properties.NullText;
			textEditTabDSubheader13.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader13Placeholder ?? textEditTabDSubheader13.Properties.NullText;
			textEditTabDSubheader14.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader14Placeholder ?? textEditTabDSubheader14.Properties.NullText;
			textEditTabDSubheader16.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader16Placeholder ?? textEditTabDSubheader16.Properties.NullText;
			textEditTabDSubheader17.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader17Placeholder ?? textEditTabDSubheader17.Properties.NullText;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabDClipart1,
				pictureEditTabDClipart2,
				pictureEditTabDClipart3,
			});

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabDClipart1.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart1 ??
				pictureEditTabDClipart1.Image;
			pictureEditTabDClipart2.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart2 ??
				pictureEditTabDClipart2.Image;
			pictureEditTabDClipart3.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart3 ??
				pictureEditTabDClipart3.Image;

			checkEditTabDGroup1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group1Toggle;
			checkEditTabDGroup2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group2Toggle;
			checkEditTabDGroup3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group3Toggle;
			checkEditTabDGroup4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group4Toggle;
			checkEditTabDGroup5.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group5Toggle;
			checkEditTabDGroup6.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group6Toggle;
			checkEditTabDGroup7.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group7Toggle;
			checkEditTabDGroup8.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group8Toggle;
			checkEditTabDGroup9.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group9Toggle;
			checkEditTabDGroup10.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group10Toggle;
			checkEditTabDSubheader2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader2Toggle;
			checkEditTabDSubheader4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader4Toggle;
			checkEditTabDSubheader6.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader6Toggle;
			checkEditTabDSubheader8.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader8Toggle;
			checkEditTabDSubheader10.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader10Toggle;
			checkEditTabDSubheader12.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader12Toggle;
			checkEditTabDSubheader15.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader15Toggle;
			checkEditTabDFormula1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula1Toggle;
			checkEditTabDFormula2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula2Toggle;
			checkEditTabDFormula3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula3Toggle;

			textEditTabDSubheader1.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader1 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader1DefaultValue;
			spinEditTabDSubheader2.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader2 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader3 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader3DefaultValue;
			spinEditTabDSubheader4.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader4 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader5 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader5DefaultValue;
			spinEditTabDSubheader6.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader6 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader7 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader7DefaultValue;
			spinEditTabDSubheader8.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader8 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader9 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader9DefaultValue;
			spinEditTabDSubheader10.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader10 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader10DefaultValue;
			textEditTabDSubheader11.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader11 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader11DefaultValue;
			spinEditTabDSubheader12.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader12 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader12DefaultValue;
			textEditTabDSubheader13.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader13 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader13DefaultValue;
			textEditTabDSubheader14.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader14 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader14DefaultValue;
			spinEditTabDSubheader15.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader15 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader15DefaultValue;
			textEditTabDSubheader16.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader16 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader16DefaultValue;
			textEditTabDSubheader17.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader17 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader17DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabDFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart1 = pictureEditTabDClipart1.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart1Image ?
				pictureEditTabDClipart1.Image :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart2 = pictureEditTabDClipart2.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart2Image ?
				pictureEditTabDClipart2.Image :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart3 = pictureEditTabDClipart3.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart3Image ?
				pictureEditTabDClipart3.Image :
				null;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group1Toggle = checkEditTabDGroup1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group2Toggle = checkEditTabDGroup2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group3Toggle = checkEditTabDGroup3.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group4Toggle = checkEditTabDGroup4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group5Toggle = checkEditTabDGroup5.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group6Toggle = checkEditTabDGroup6.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group7Toggle = checkEditTabDGroup7.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group8Toggle = checkEditTabDGroup8.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group9Toggle = checkEditTabDGroup9.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group10Toggle = checkEditTabDGroup10.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader2Toggle = checkEditTabDSubheader2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader4Toggle = checkEditTabDSubheader4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader6Toggle = checkEditTabDSubheader6.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader8Toggle = checkEditTabDSubheader8.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader10Toggle = checkEditTabDSubheader10.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader12Toggle = checkEditTabDSubheader12.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader15Toggle = checkEditTabDSubheader15.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula1Toggle = checkEditTabDFormula1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula2Toggle = checkEditTabDFormula2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula3Toggle = checkEditTabDFormula3.Checked;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader1 = textEditTabDSubheader1.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader1DefaultValue ?
				textEditTabDSubheader1.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader2 = (decimal?)spinEditTabDSubheader2.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader2DefaultValue ?
				(decimal?)spinEditTabDSubheader2.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader3 = textEditTabDSubheader3.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader3DefaultValue ?
				textEditTabDSubheader3.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader4 = (decimal?)spinEditTabDSubheader4.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader4DefaultValue ?
				(decimal?)spinEditTabDSubheader4.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader5 = textEditTabDSubheader5.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader5DefaultValue ?
				textEditTabDSubheader5.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader6 = (decimal?)spinEditTabDSubheader6.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader6DefaultValue ?
				(decimal?)spinEditTabDSubheader6.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader7 = textEditTabDSubheader7.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader7DefaultValue ?
				textEditTabDSubheader7.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader8 = (decimal?)spinEditTabDSubheader8.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader8DefaultValue ?
				(decimal?)spinEditTabDSubheader8.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader9 = textEditTabDSubheader9.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader9DefaultValue ?
				textEditTabDSubheader9.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader10 = (decimal?)spinEditTabDSubheader10.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader10DefaultValue ?
				(decimal?)spinEditTabDSubheader10.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader11 = textEditTabDSubheader11.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader11DefaultValue ?
				textEditTabDSubheader11.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader12 = (decimal?)spinEditTabDSubheader12.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader12DefaultValue ?
				(decimal?)spinEditTabDSubheader12.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader13 = textEditTabDSubheader13.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader13DefaultValue ?
				textEditTabDSubheader13.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader14 = textEditTabDSubheader14.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader14DefaultValue ?
				textEditTabDSubheader14.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader15 = (decimal?)spinEditTabDSubheader15.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader15DefaultValue ?
				(decimal?)spinEditTabDSubheader15.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader16 = textEditTabDSubheader16.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader16DefaultValue ?
				textEditTabDSubheader16.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader17 = textEditTabDSubheader17.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader17DefaultValue ?
				textEditTabDSubheader17.EditValue as String :
				null;

			_dataChanged = false;
		}

		#region Event Handlers

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ROIContentContainer.RaiseDataChanged();
		}

		private void OnTabDGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup1Inner.Enabled = checkEditTabDGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader2Value.Enabled = checkEditTabDSubheader2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup2Inner.Enabled = checkEditTabDGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader4Value.Enabled = checkEditTabDSubheader4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup3Inner.Enabled = checkEditTabDGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader6Value.Enabled = checkEditTabDSubheader6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup4Inner.Enabled = checkEditTabDGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader8Value.Enabled = checkEditTabDSubheader8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup5Inner.Enabled = checkEditTabDGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader10Value.Enabled = checkEditTabDSubheader10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup6Inner.Enabled = checkEditTabDGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader12CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader12Value.Enabled = checkEditTabDSubheader12.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup7Inner.Enabled = checkEditTabDGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormula1CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula1.Enabled = checkEditTabDFormula1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup8Inner.Enabled = checkEditTabDGroup8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader15CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader15Value.Enabled = checkEditTabDSubheader15.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup9CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup9Inner.Enabled = checkEditTabDGroup9.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormula2CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula2.Enabled = checkEditTabDFormula2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup10Inner.Enabled = checkEditTabDGroup10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormula3CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula3.Enabled = checkEditTabDFormula3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var monthlyInvestmentValue = (double)spinEditTabDSubheader2.Value;
			var avgSaleValue = (double)spinEditTabDSubheader4.Value;
			var salePercent = (double)spinEditTabDSubheader6.Value;
			var closingPercent = (double)spinEditTabDSubheader8.Value;
			var monthlyGoal = (double)spinEditTabDSubheader15.Value;

			var formula1Value = avgSaleValue > 0 && salePercent > 0 ? Math.Ceiling(monthlyInvestmentValue / (avgSaleValue * salePercent / 100)) : 0;
			var formula2Value = closingPercent > 0 ? Math.Ceiling(formula1Value / (closingPercent / 100)) : 0;
			var formula3Value = closingPercent > 0 ? Math.Ceiling(monthlyGoal / (closingPercent / 100)) : 0;

			simpleLabelItemTabDFormula1.CustomizationFormText = String.Format("{0:#,##0}", formula1Value);
			simpleLabelItemTabDFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabDFormula2.CustomizationFormText = String.Format("{0:#,##0}", formula2Value);
			simpleLabelItemTabDFormula2.Text = String.Format("<b>{0:#,##0}</b>", formula2Value);
			simpleLabelItemTabDFormula3.CustomizationFormText = String.Format("{0:#,##0}", formula3Value);
			simpleLabelItemTabDFormula3.Text = String.Format("<b>{0:#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ROITabD;
		public override String OutputName => ROIContentContainer.SlideContainer.StarInfo.Titles.Tab6SubDTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarROIFile("CP06D-1.pptx");
			outputDataPackage.Theme = ROIContentContainer.SelectedTheme;

			var clipart1 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart1 ?? ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart1Image;
			if (clipart1 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart1.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP06DCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
			}

			var clipart2 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart2 ?? ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart2Image;
			if (clipart2 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart2.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP06DCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
			}

			var clipart3 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Clipart3 ?? ROIContentContainer.SlideContainer.StarInfo.Tab6SubDClipart3Image;
			if (clipart3 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart3.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP06DCLIPART3", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart3.Width, clipart3.Height) });
			}

			outputDataPackage.TextItems = GetOutputDataTextItems();

			outputDataPackage.TextItems.Add("CP06DHEADER", ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.SlideHeader?.Value ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault)?.Value);
			outputDataPackage.TextItems.Add("HEADER", ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.SlideHeader?.Value ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault)?.Value);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group1Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase1".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader1 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader1DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader2Toggle)
					textDataItems.Add("CP06DFormulaPhrase2".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader2 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader2DefaultValue ?? 0).ToString("$#,##0"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group2Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase3".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader3 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader3DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader4Toggle)
					textDataItems.Add("CP06DFormulaPhrase4".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader4 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader4DefaultValue ?? 0).ToString("$#,##0"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group3Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase5".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader5 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader5DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader6Toggle)
					textDataItems.Add("CP06DFormulaPhrase6".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader6 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader6DefaultValue ?? 0).ToString("##0'%'"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group4Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase7".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader7 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader7DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader8Toggle)
					textDataItems.Add("CP06DFormulaPhrase8".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader8 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader8DefaultValue ?? 0).ToString("##0'%'"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group5Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase9".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader9 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader9DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader10Toggle)
					textDataItems.Add("CP06DFormulaPhrase10".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader10 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader10DefaultValue ?? 0).ToString("#,##0"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group6Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase11".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader11 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader11DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader12Toggle)
					textDataItems.Add("CP06DFormulaPhrase12".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader12 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader12DefaultValue ?? 0).ToString("#,##0"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group7Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase13".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader13 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader13DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula1Toggle)
					textDataItems.Add("CP06DFormulaPhrase14".ToUpper(), simpleLabelItemTabDFormula1.CustomizationFormText);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group8Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase15".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader14 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader14DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader15Toggle)
					textDataItems.Add("CP06DFormulaPhrase16".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader15 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader15DefaultValue ?? 0).ToString("#,##0"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group9Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase17".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader16 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader16DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula2Toggle)
					textDataItems.Add("CP06DFormulaPhrase18".ToUpper(), simpleLabelItemTabDFormula2.CustomizationFormText);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Group10Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase19".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Subheader17 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader17DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabD.Formula3Toggle)
					textDataItems.Add("CP06DFormulaPhrase20".ToUpper(), simpleLabelItemTabDFormula3.CustomizationFormText);
			}

			return textDataItems;
		}

		public override void GenerateOutput()
		{
			var outputDataPackage = GetOutputData();
			ROIContentContainer.SlideContainer.PowerPointProcessor.AppendStarCommonSlide(outputDataPackage);
		}

		public override PreviewGroup GeneratePreview()
		{
			var outputDataPackage = GetOutputData();
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			ROIContentContainer.SlideContainer.PowerPointProcessor.PrepareStarCommonSlide(outputDataPackage, tempFileName);
			return new PreviewGroup { Name = OutputName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}
