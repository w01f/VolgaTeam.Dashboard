using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabAControl : ShareTabBaseControl
	{
		public ShareTabAControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabACombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabASubheader2.EnableSelectAll();
			textEditTabASubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabASubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart1Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart1Configuration, ShareContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart2Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart2Configuration, ShareContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart3Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart3Configuration, ShareContentContainer);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabASubheader1.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader1Placeholder ?? textEditTabASubheader1.Properties.NullText;
			textEditTabASubheader3.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader3Placeholder ?? textEditTabASubheader3.Properties.NullText;
			textEditTabASubheader4.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader4Placeholder ?? textEditTabASubheader4.Properties.NullText;
			textEditTabASubheader5.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader5Placeholder ?? textEditTabASubheader5.Properties.NullText;
			textEditTabASubheader6.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader6Placeholder ?? textEditTabASubheader6.Properties.NullText;
			textEditTabASubheader7.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader7Placeholder ?? textEditTabASubheader7.Properties.NullText;

			comboBoxEditTabACombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo1.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo1.Properties.NullText;
			comboBoxEditTabACombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo2.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo2.Properties.NullText;
			comboBoxEditTabACombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo3.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo3.Properties.NullText;
			comboBoxEditTabACombo4.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo4.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo4.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart1);
			clipartEditContainer2.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart2);
			clipartEditContainer3.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart3);

			checkEditTabAGroup1.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group1Toggle;
			checkEditTabAGroup2.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group2Toggle;
			checkEditTabAGroup3.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group3Toggle;
			checkEditTabAGroup4.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group4Toggle;
			checkEditTabASubheader3.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader3Toggle;
			checkEditTabASubheader5.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader5Toggle;
			checkEditTabAFormula1.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Formula1Toggle;
			checkEditTabAFormula2.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Formula2Toggle;

			comboBoxEditTabACombo1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabASubheader1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader1DefaultValue;
			spinEditTabASubheader2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader4DefaultValue;
			textEditTabASubheader5.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader5 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader6 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader7 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader7DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabAFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group1Toggle = checkEditTabAGroup1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group2Toggle = checkEditTabAGroup2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group3Toggle = checkEditTabAGroup3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group4Toggle = checkEditTabAGroup4.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader3Toggle = checkEditTabASubheader3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader5Toggle = checkEditTabASubheader5.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Formula1Toggle = checkEditTabAFormula1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Formula2Toggle = checkEditTabAFormula2.Checked;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo1 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo1.EditValue ?
				comboBoxEditTabACombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo1.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo2 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo2.EditValue ?
				comboBoxEditTabACombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo2.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo3 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo3.EditValue ?
				comboBoxEditTabACombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo3.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo4 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo4.EditValue ?
				comboBoxEditTabACombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo4.EditValue as String } :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader1 = textEditTabASubheader1.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader1DefaultValue ?
				textEditTabASubheader1.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader2 = (decimal?)spinEditTabASubheader2.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader2DefaultValue ?
				(decimal?)spinEditTabASubheader2.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader3 = textEditTabASubheader3.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader3DefaultValue ?
				textEditTabASubheader3.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader4 = textEditTabASubheader4.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader4DefaultValue ?
				textEditTabASubheader4.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader5 = textEditTabASubheader5.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader5DefaultValue ?
				textEditTabASubheader5.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader6 = textEditTabASubheader6.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader6DefaultValue ?
				textEditTabASubheader6.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader7 = textEditTabASubheader7.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader7DefaultValue ?
				textEditTabASubheader7.EditValue as String ?? String.Empty :
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

		private void OnTabAGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup1Inner.Enabled = checkEditTabAGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabASubheader3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader3Value.Enabled = checkEditTabASubheader3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup2Inner.Enabled = checkEditTabAGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabASubheader5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader5Value.Enabled = checkEditTabASubheader5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup3Inner.Enabled = checkEditTabAGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAFormula1CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabAFormula1.Enabled = checkEditTabAFormula1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup4Inner.Enabled = checkEditTabAGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAFormula2CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabAFormula2.Enabled = checkEditTabAFormula2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var multiplierText = (comboBoxEditTabACombo1.EditValue as ListDataItem)?.Value ?? String.Empty;

			var sourceValue = 0.0;
			try
			{
				sourceValue =
					(double)spinEditTabASubheader2.Value *
					(multiplierText.StartsWith("mi", StringComparison.InvariantCultureIgnoreCase)
						? 1000000
						: (multiplierText.StartsWith("bi", StringComparison.InvariantCultureIgnoreCase)
							? 1000000000
							: 1000000000000));
			}
			catch
			{
			}

			var percent = 0.0;
			try
			{
				percent = Double.Parse((comboBoxEditTabACombo2.EditValue as ListDataItem)?.Value?.Trim()?.Replace("%", "") ?? "0");
			}
			catch
			{
			}

			var formula1Value = (Int64)(sourceValue / 100 * percent);
			var sharepointFactor = (comboBoxEditTabACombo4.EditValue as ListDataItem)?.Value ?? String.Empty;
			var formula2Value = formula1Value / 100 *
								(sharepointFactor.StartsWith("ONE",
									StringComparison.InvariantCultureIgnoreCase)
									? 1
									: (sharepointFactor.StartsWith("TWO",
										StringComparison.InvariantCultureIgnoreCase)
										? 2
										: 3));

			simpleLabelItemTabAFormula1.CustomizationFormText = String.Format("{0:$#,##0}", formula1Value);
			simpleLabelItemTabAFormula1.Text = String.Format("<b>{0:$#,##0}</b>", formula1Value);

			simpleLabelItemTabAFormula2.CustomizationFormText = String.Format("{0:$#,##0} Annually", formula2Value);
			simpleLabelItemTabAFormula2.Text = String.Format("<b>{0:$#,##0}</b>   Annually", formula2Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ShareTabA;
		public override String OutputName => ShareContentContainer.SlideContainer.StarInfo.Titles.Tab5SubATitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05A-1.pptx");
			outputDataPackage.Theme = ShareContentContainer.SelectedTheme;

			var clipart1 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart1 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05ACLIPART1", clipart1);

			var clipart2 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart2 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05ACLIPART2", clipart2);

			var clipart3 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Clipart3 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05ACLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.SlideHeader ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05AHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader1 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader1DefaultValue);
				itemParts.Add((ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader2 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader2DefaultValue)?.ToString("$#,##0"));
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo1?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items.FirstOrDefault(h => h.IsDefault)?.Value);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader3Toggle)
					textDataItems.Add("CP05AFormulaPhrase2".ToUpper(), String.Format("Source: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader3 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader3DefaultValue));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader4 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader4DefaultValue);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo2?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo3?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items.FirstOrDefault(h => h.IsDefault)?.Value);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader5Toggle)
					textDataItems.Add("CP05AFormulaPhrase4".ToUpper(), String.Format("Source: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader5 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader5DefaultValue));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader6 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader6DefaultValue);
				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Formula1Toggle)
					itemParts.Add(simpleLabelItemTabAFormula1.CustomizationFormText);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Combo4?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Subheader7 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader7DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabA.Formula2Toggle)
					textDataItems.Add("CP05AFormulaPhrase7".ToUpper(), simpleLabelItemTabAFormula2.CustomizationFormText);
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
