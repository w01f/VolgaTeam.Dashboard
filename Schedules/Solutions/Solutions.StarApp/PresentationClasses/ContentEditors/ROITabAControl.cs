using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabAControl : ROITabBaseControl
	{
		public ROITabAControl(IROITabPageContainer roiTabPageContainer) : base(roiTabPageContainer)
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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart1Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartAClipart1Configuration, ROIContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart2Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartAClipart2Configuration, ROIContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart3Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartAClipart3Configuration, ROIContentContainer);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			textEditTabASubheader1.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader1Placeholder ?? textEditTabASubheader1.Properties.NullText;
			textEditTabASubheader3.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader3Placeholder ?? textEditTabASubheader3.Properties.NullText;
			textEditTabASubheader4.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader4Placeholder ?? textEditTabASubheader4.Properties.NullText;
			textEditTabASubheader6.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader6Placeholder ?? textEditTabASubheader6.Properties.NullText;
			textEditTabASubheader7.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader7Placeholder ?? textEditTabASubheader7.Properties.NullText;
			textEditTabASubheader9.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader9Placeholder ?? textEditTabASubheader9.Properties.NullText;
			textEditTabASubheader10.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader10Placeholder ?? textEditTabASubheader10.Properties.NullText;
			textEditTabASubheader11.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader11Placeholder ?? textEditTabASubheader11.Properties.NullText;
			textEditTabASubheader12.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader12Placeholder ?? textEditTabASubheader12.Properties.NullText;
			textEditTabASubheader13.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader13Placeholder ?? textEditTabASubheader13.Properties.NullText;
			textEditTabASubheader15.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader15Placeholder ?? textEditTabASubheader15.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart1);
			clipartEditContainer2.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart2);
			clipartEditContainer3.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart3);

			checkEditTabAGroup1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group1Toggle;
			checkEditTabAGroup2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group2Toggle;
			checkEditTabAGroup3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group3Toggle;
			checkEditTabAGroup4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group4Toggle;
			checkEditTabAGroup5.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group5Toggle;
			checkEditTabAGroup6.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group6Toggle;
			checkEditTabASubheader14.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle;
			checkEditTabASubheader15.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle;

			textEditTabASubheader1.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader1 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader1DefaultValue;
			spinEditTabASubheader2.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader2 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader3 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader4 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader4DefaultValue;
			spinEditTabASubheader5.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader5 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader6 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader7 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader7DefaultValue;
			spinEditTabASubheader8.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader8 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader8DefaultValue;
			textEditTabASubheader9.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader9 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader9DefaultValue;
			textEditTabASubheader10.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader10 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader10DefaultValue;
			textEditTabASubheader11.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader11 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader11DefaultValue;
			textEditTabASubheader12.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader12 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader12DefaultValue;
			textEditTabASubheader13.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader13 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader13DefaultValue;
			spinEditTabASubheader14.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader14DefaultValue;
			textEditTabASubheader15.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader15DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabAFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group1Toggle = checkEditTabAGroup1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group2Toggle = checkEditTabAGroup2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group3Toggle = checkEditTabAGroup3.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group4Toggle = checkEditTabAGroup4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group5Toggle = checkEditTabAGroup5.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group6Toggle = checkEditTabAGroup6.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle = checkEditTabASubheader14.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle = checkEditTabASubheader15.Checked;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader1 = textEditTabASubheader1.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader1DefaultValue ?
				textEditTabASubheader1.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader2 = (decimal?)spinEditTabASubheader2.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader2DefaultValue ?
				(decimal?)spinEditTabASubheader2.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader3 = textEditTabASubheader3.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader3DefaultValue ?
				textEditTabASubheader3.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader4 = textEditTabASubheader4.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader4DefaultValue ?
				textEditTabASubheader4.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader5 = (decimal?)spinEditTabASubheader5.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader5DefaultValue ?
				(decimal?)spinEditTabASubheader5.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader6 = textEditTabASubheader6.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader6DefaultValue ?
				textEditTabASubheader6.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader7 = textEditTabASubheader7.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader7DefaultValue ?
				textEditTabASubheader7.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader8 = (decimal?)spinEditTabASubheader8.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader8DefaultValue ?
				(decimal?)spinEditTabASubheader8.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader9 = textEditTabASubheader9.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader9DefaultValue ?
				textEditTabASubheader9.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader10 = textEditTabASubheader10.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader10DefaultValue ?
				textEditTabASubheader10.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader11 = textEditTabASubheader11.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader11DefaultValue ?
				textEditTabASubheader11.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader12 = textEditTabASubheader12.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader12DefaultValue ?
				textEditTabASubheader12.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader13 = textEditTabASubheader13.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader13DefaultValue ?
				textEditTabASubheader13.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14 = (decimal?)spinEditTabASubheader14.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader14DefaultValue ?
				(decimal?)spinEditTabASubheader14.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15 = textEditTabASubheader15.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader15DefaultValue ?
				textEditTabASubheader15.EditValue as String ?? String.Empty :
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
		public override String OutputName => ROIContentContainer.SlideContainer.StarInfo.Titles.Tab6SubATitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarROIFile("CP06A-1.pptx");
			outputDataPackage.Theme = ROIContentContainer.SelectedTheme;

			var clipart1 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart1 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP06ACLIPART1", clipart1);

			var clipart2 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart2 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP06ACLIPART2", clipart2);

			var clipart3 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart3 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP06ACLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.SlideHeader ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP06AHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader1 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader1DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader2 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader2DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader3 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader3DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader4 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader4DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader5 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader5DefaultValue ?? 0).ToString("#,##0"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader6 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader6DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase2".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader7 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader7DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader8 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader8DefaultValue ?? 0).ToString("##0'%'"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader9 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader9DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader10 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader10DefaultValue);
				itemParts.Add(simpleLabelItemTabAFormula1.CustomizationFormText);
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader11 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader11DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader12 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader12DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabAFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader13 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader13DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06AFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle)
					textDataItems.Add("CP06AFormulaPhrase7".ToUpper(), String.Format("{0:$#,##0} Per Month", ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader14DefaultValue));

				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle)
					textDataItems.Add("CP06AFormulaPhrase8".ToUpper(), String.Format("{0} = {1}", ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader15DefaultValue, layoutControlItemTabASubheader15Value.CustomizationFormText));
			}

			return textDataItems;
		}
		#endregion
	}
}
