using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabBControl : ShareTabBaseControl
	{
		public ShareTabBControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader4.EnableSelectAll();
			textEditTabBSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader6.EnableSelectAll();
			textEditTabBSubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader8.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart1Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart1Configuration, ShareContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart2Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart2Configuration, ShareContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart3Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart3Configuration, ShareContentContainer);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabBSubheader1.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			textEditTabBSubheader2.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader2Placeholder ?? textEditTabBSubheader2.Properties.NullText;
			textEditTabBSubheader3.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader3Placeholder ?? textEditTabBSubheader3.Properties.NullText;
			textEditTabBSubheader5.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader5Placeholder ?? textEditTabBSubheader5.Properties.NullText;
			textEditTabBSubheader7.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader7Placeholder ?? textEditTabBSubheader7.Properties.NullText;
			textEditTabBSubheader8.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader8Placeholder ?? textEditTabBSubheader8.Properties.NullText;

			comboBoxEditTabBCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo1.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo1.Properties.NullText;
			comboBoxEditTabBCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo2.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo2.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart1);
			clipartEditContainer2.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart2);
			clipartEditContainer3.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart3);

			checkEditTabBGroup1.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group1Toggle;
			checkEditTabBGroup2.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group2Toggle;
			checkEditTabBGroup3.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group3Toggle;
			checkEditTabBGroup4.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group4Toggle;
			checkEditTabBGroup5.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group5Toggle;
			checkEditTabBSubheader2.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2Toggle;
			checkEditTabBSubheader7.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7Toggle;

			comboBoxEditTabBCombo1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsDefault);

			textEditTabBSubheader1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader3DefaultValue;
			spinEditTabBSubheader4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader4DefaultValue;
			textEditTabBSubheader5.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader5 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader5DefaultValue;
			spinEditTabBSubheader6.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader6 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader7DefaultValue;
			textEditTabBSubheader8.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader8 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader8DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabBFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group1Toggle = checkEditTabBGroup1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group2Toggle = checkEditTabBGroup2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group3Toggle = checkEditTabBGroup3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group4Toggle = checkEditTabBGroup4.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2Toggle = checkEditTabBSubheader2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7Toggle = checkEditTabBSubheader7.Checked;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo1 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo2 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader2DefaultValue ?
				textEditTabBSubheader2.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader3DefaultValue ?
				textEditTabBSubheader3.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader4 = (decimal?)spinEditTabBSubheader4.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader4DefaultValue ?
				(decimal?)spinEditTabBSubheader4.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader5 = textEditTabBSubheader5.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader5DefaultValue ?
				textEditTabBSubheader5.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader6 = (decimal?)spinEditTabBSubheader6.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader6DefaultValue ?
				(decimal?)spinEditTabBSubheader6.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7 = textEditTabBSubheader7.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader7DefaultValue ?
				textEditTabBSubheader7.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader8 = textEditTabBSubheader8.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader8DefaultValue ?
				textEditTabBSubheader8.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		#region Event Handlers

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ShareContentContainer.RaiseDataChanged();
		}

		private void OnTabBGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup1Inner.Enabled = checkEditTabBGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBSubheader2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader2Value.Enabled = checkEditTabBSubheader2.Checked;
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

		private void OnTabBSubheader7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader7Value.Enabled = checkEditTabBSubheader7.Checked;
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

		private void OnTabBFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var sourceValue = 0.0;
			try
			{
				sourceValue = Double.Parse((comboBoxEditTabBCombo1.EditValue as ListDataItem)?.Value ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var percent = (double)spinEditTabBSubheader4.Value;
			var costValue = (double)spinEditTabBSubheader6.Value;

			var sharepointFactor = (comboBoxEditTabBCombo2.EditValue as ListDataItem)?.Value ?? String.Empty;

			var formula1Value = (Int64)(sourceValue / 100 * percent);
			var formula2Value = formula1Value * costValue;
			var formula3Value = formula2Value / 100 *
								(sharepointFactor.StartsWith("ONE",
									StringComparison.InvariantCultureIgnoreCase)
									? 1
									: (sharepointFactor.StartsWith("TWO",
										StringComparison.InvariantCultureIgnoreCase)
										? 2
										: 3));

			simpleLabelItemTabBFormula1.CustomizationFormText = String.Format("{0:#,##0}", formula1Value);
			simpleLabelItemTabBFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabBFormula2.CustomizationFormText = String.Format("{0:$#,##0}", formula2Value);
			simpleLabelItemTabBFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabBFormula3.CustomizationFormText = String.Format("{0:$#,##0}", formula3Value);
			simpleLabelItemTabBFormula3.Text = String.Format("<b>{0:$#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ShareTabB;
		public override String OutputName => ShareContentContainer.SlideContainer.StarInfo.Titles.Tab5SubBTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05B-1.pptx");
			outputDataPackage.Theme = ShareContentContainer.SelectedTheme;

			var clipart1 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart1 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05BCLIPART1", clipart1);

			var clipart2 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart2 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05BCLIPART2", clipart2);

			var clipart3 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart3 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05BCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.SlideHeader ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05BHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader1 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader1DefaultValue);
				itemParts.Add(String.Format("= {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo1?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2Toggle)
					textDataItems.Add("CP05BFormulaPhrase2".ToUpper(), String.Format("Source: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader2DefaultValue));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader3 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader3DefaultValue);
				itemParts.Add(String.Format("= {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader4 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader4DefaultValue));
				itemParts.Add(simpleLabelItemTabBFormula1.CustomizationFormText);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader5 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader5DefaultValue);
				itemParts.Add(String.Format("= {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader6 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader6DefaultValue));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7Toggle)
					textDataItems.Add("CP05BFormulaPhrase5".ToUpper(), String.Format("Source: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader7DefaultValue));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader8 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader8DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo2?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula3.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase7".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			return textDataItems;
		}

		public override void GenerateOutput()
		{
			var outputDataPackage = GetOutputData();
			ShareContentContainer.SlideContainer.PowerPointProcessor.AppendStarCommonSlide(outputDataPackage);
		}

		public override PreviewGroup GeneratePreview()
		{
			var outputDataPackage = GetOutputData();
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			ShareContentContainer.SlideContainer.PowerPointProcessor.PrepareStarCommonSlide(outputDataPackage, tempFileName);
			return new PreviewGroup { Name = OutputName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}
