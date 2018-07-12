using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabCControl : ROITabBaseControl
	{
		public ROITabCControl(ROIControl roiContentContainer) : base(roiContentContainer)
		{
			InitializeComponent();

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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubCClipart1Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCClipart1Configuration, ROIContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubCClipart2Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCClipart2Configuration, ROIContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubCClipart3Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCClipart3Configuration, ROIContentContainer);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			textEditTabCSubheader1.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader1Placeholder ?? textEditTabCSubheader1.Properties.NullText;
			textEditTabCSubheader3.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader3Placeholder ?? textEditTabCSubheader3.Properties.NullText;
			textEditTabCSubheader5.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader5Placeholder ?? textEditTabCSubheader5.Properties.NullText;
			textEditTabCSubheader6.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader6Placeholder ?? textEditTabCSubheader6.Properties.NullText;
			textEditTabCSubheader8.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader8Placeholder ?? textEditTabCSubheader8.Properties.NullText;
			textEditTabCSubheader9.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader9Placeholder ?? textEditTabCSubheader9.Properties.NullText;
			textEditTabCSubheader10.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader10Placeholder ?? textEditTabCSubheader10.Properties.NullText;
			textEditTabCSubheader11.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader11Placeholder ?? textEditTabCSubheader11.Properties.NullText;
			textEditTabCSubheader12.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader12Placeholder ?? textEditTabCSubheader12.Properties.NullText;
			textEditTabCSubheader14.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader14Placeholder ?? textEditTabCSubheader14.Properties.NullText;
			textEditTabCSubheader15.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader15Placeholder ?? textEditTabCSubheader15.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart1);
			clipartEditContainer2.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart2);
			clipartEditContainer3.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart3);

			checkEditTabCGroup1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group1Toggle;
			checkEditTabCGroup2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group2Toggle;
			checkEditTabCGroup3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group3Toggle;
			checkEditTabCGroup4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group4Toggle;
			checkEditTabCGroup5.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group5Toggle;
			checkEditTabCGroup6.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group6Toggle;
			checkEditTabCGroup7.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group7Toggle;
			checkEditTabCGroup8.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group8Toggle;
			checkEditTabCSubheader2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader2Toggle;
			checkEditTabCSubheader4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader4Toggle;
			checkEditTabCSubheader5.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader5Toggle;
			checkEditTabCSubheader7.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader7Toggle;
			checkEditTabCSubheader8.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader8Toggle;
			checkEditTabCSubheader10.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader10Toggle;
			checkEditTabCSubheader14.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader14Toggle;
			checkEditTabCFormula1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula1Toggle;
			checkEditTabCFormula2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula2Toggle;
			checkEditTabCFormula3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula3Toggle;

			textEditTabCSubheader1.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader1 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader1DefaultValue;
			spinEditTabCSubheader2.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader2 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader2DefaultValue;
			textEditTabCSubheader3.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader3 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader3DefaultValue;
			spinEditTabCSubheader4.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader4 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader4DefaultValue;
			textEditTabCSubheader5.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader5 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader5DefaultValue;
			textEditTabCSubheader6.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader6 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader6DefaultValue;
			spinEditTabCSubheader7.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader7 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader7DefaultValue;
			textEditTabCSubheader8.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader8 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader8DefaultValue;
			textEditTabCSubheader9.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader9 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader9DefaultValue;
			textEditTabCSubheader10.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader10 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader10DefaultValue;
			textEditTabCSubheader11.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader11 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader11DefaultValue;
			textEditTabCSubheader12.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader12 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader12DefaultValue;
			spinEditTabCSubheader13.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader13 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader13DefaultValue;
			textEditTabCSubheader14.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader14 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader14DefaultValue;
			textEditTabCSubheader15.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader15 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader15DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabCFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group1Toggle = checkEditTabCGroup1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group2Toggle = checkEditTabCGroup2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group3Toggle = checkEditTabCGroup3.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group4Toggle = checkEditTabCGroup4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group5Toggle = checkEditTabCGroup5.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group6Toggle = checkEditTabCGroup6.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group7Toggle = checkEditTabCGroup7.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group8Toggle = checkEditTabCGroup8.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader2Toggle = checkEditTabCSubheader2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader4Toggle = checkEditTabCSubheader4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader5Toggle = checkEditTabCSubheader5.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader7Toggle = checkEditTabCSubheader7.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader8Toggle = checkEditTabCSubheader8.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader10Toggle = checkEditTabCSubheader10.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader14Toggle = checkEditTabCSubheader14.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula1Toggle = checkEditTabCFormula1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula2Toggle = checkEditTabCFormula2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula3Toggle = checkEditTabCFormula3.Checked;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader1 = textEditTabCSubheader1.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader1DefaultValue ?
				textEditTabCSubheader1.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader2 = (decimal?)spinEditTabCSubheader2.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader2DefaultValue ?
				(decimal?)spinEditTabCSubheader2.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader3 = textEditTabCSubheader3.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader3DefaultValue ?
				textEditTabCSubheader3.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader4 = (decimal?)spinEditTabCSubheader4.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader4DefaultValue ?
				(decimal?)spinEditTabCSubheader4.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader5 = textEditTabCSubheader5.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader5DefaultValue ?
				textEditTabCSubheader5.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader6 = textEditTabCSubheader6.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader6DefaultValue ?
				textEditTabCSubheader6.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader7 = (decimal?)spinEditTabCSubheader7.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader7DefaultValue ?
				(decimal?)spinEditTabCSubheader7.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader8 = textEditTabCSubheader8.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader8DefaultValue ?
				textEditTabCSubheader8.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader9 = textEditTabCSubheader9.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader9DefaultValue ?
				textEditTabCSubheader9.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader10 = textEditTabCSubheader10.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader10DefaultValue ?
				textEditTabCSubheader10.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader11 = textEditTabCSubheader11.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader11DefaultValue ?
				textEditTabCSubheader11.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader12 = textEditTabCSubheader12.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader12DefaultValue ?
				textEditTabCSubheader12.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader13 = (decimal?)spinEditTabCSubheader13.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader13DefaultValue ?
				(decimal?)spinEditTabCSubheader13.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader14 = textEditTabCSubheader14.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader14DefaultValue ?
				textEditTabCSubheader14.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader15 = textEditTabCSubheader15.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader15DefaultValue ?
				textEditTabCSubheader15.EditValue as String ?? String.Empty :
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
		public override StarAppOutputType OutputType => StarAppOutputType.ROITabC;
		public override String OutputName => ROIContentContainer.SlideContainer.StarInfo.Titles.Tab6SubCTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarROIFile("CP06C-1.pptx");
			outputDataPackage.Theme = ROIContentContainer.SelectedTheme;

			var clipart1 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart1 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubCClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP06CCLIPART1", clipart1);

			var clipart2 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart2 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubCClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP06CCLIPART2", clipart2);

			var clipart3 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Clipart3 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubCClipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP06CCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHaeder = (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.SlideHeader ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP06CHEADER", slideHaeder);
			outputDataPackage.TextItems.Add("HEADER", slideHaeder);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group1Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase1".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader1 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader1DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader2Toggle)
					textDataItems.Add("CP06CFormulaPhrase2".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader2 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader2DefaultValue ?? 0).ToString("$#,##0"));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group2Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase3".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader3 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader3DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader4Toggle)
					textDataItems.Add("CP06CFormulaPhrase4".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader4 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader4DefaultValue ?? 0).ToString("#,##0"));
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader5Toggle)
					textDataItems.Add("CP06CFormulaPhrase5".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader5 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader5DefaultValue);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group3Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase6".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader6 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader6DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader7Toggle)
					textDataItems.Add("CP06CFormulaPhrase7".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader7 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader7DefaultValue ?? 0).ToString("##0'%'"));
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader8Toggle)
					textDataItems.Add("CP06CFormulaPhrase8".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader8 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader8DefaultValue);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group4Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase9".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader9 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader9DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula1Toggle)
					textDataItems.Add("CP06CFormulaPhrase10".ToUpper(), simpleLabelItemTabCFormula1.CustomizationFormText);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader10Toggle)
					textDataItems.Add("CP06CFormulaPhrase11".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader10 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader10DefaultValue);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group5Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase12".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader11 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader11DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula2Toggle)
					textDataItems.Add("CP06CFormulaPhrase13".ToUpper(), simpleLabelItemTabCFormula2.CustomizationFormText);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group6Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase14".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader12 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader12DefaultValue);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group7Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase15".ToUpper(), (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader13 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader13DefaultValue ?? 0).ToString("$#,##0"));
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader14Toggle)
					textDataItems.Add("CP06CFormulaPhrase16".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader14 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader14DefaultValue);
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Group8Toggle)
			{
				textDataItems.Add("CP06CFormulaPhrase17".ToUpper(), ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Subheader15 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader15DefaultValue);
				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabC.Formula3Toggle)
					textDataItems.Add("CP06CFormulaPhrase18".ToUpper(), simpleLabelItemTabCFormula3.CustomizationFormText);
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
