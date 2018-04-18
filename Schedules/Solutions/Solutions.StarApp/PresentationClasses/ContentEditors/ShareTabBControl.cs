using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabBControl : ShareTabBaseControl
	{
		public ShareTabBControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll();
			comboBoxEditTabBCombo2.EnableSelectAll();
			textEditTabBSubheader2.EnableSelectAll();
			textEditTabBSubheader3.EnableSelectAll();
			spinEditTabBSubheader4.EnableSelectAll();
			textEditTabBSubheader5.EnableSelectAll();
			spinEditTabBSubheader6.EnableSelectAll();
			textEditTabBSubheader7.EnableSelectAll();
			textEditTabBSubheader8.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabBClipart1.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart3Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
				pictureEditTabBClipart3,
			});

			Application.DoEvents();

			comboBoxEditTabBCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items);
			comboBoxEditTabBCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabBClipart1.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;
			pictureEditTabBClipart3.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart3 ??
				pictureEditTabBClipart3.Image;

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

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart1 = pictureEditTabBClipart1.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart1Image ?
				pictureEditTabBClipart1.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart2 = pictureEditTabBClipart2.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart2Image ?
				pictureEditTabBClipart2.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart3 = pictureEditTabBClipart3.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart3Image ?
				pictureEditTabBClipart3.Image :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group1Toggle = checkEditTabBGroup1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group2Toggle = checkEditTabBGroup2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group3Toggle = checkEditTabBGroup3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Group4Toggle = checkEditTabBGroup4.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2Toggle = checkEditTabBSubheader2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7Toggle = checkEditTabBSubheader7.Checked;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo1 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? (comboBoxEditTabBCombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo1.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Combo2 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? (comboBoxEditTabBCombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo2.EditValue } : null) :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader2DefaultValue ?
				textEditTabBSubheader2.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader3DefaultValue ?
				textEditTabBSubheader3.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader4 = (decimal?)spinEditTabBSubheader4.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader4DefaultValue ?
				(decimal?)spinEditTabBSubheader4.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader5 = textEditTabBSubheader5.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader5DefaultValue ?
				textEditTabBSubheader5.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader6 = (decimal?)spinEditTabBSubheader6.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader6DefaultValue ?
				(decimal?)spinEditTabBSubheader6.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader7 = textEditTabBSubheader7.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader7DefaultValue ?
				textEditTabBSubheader7.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Subheader8 = textEditTabBSubheader8.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader8DefaultValue ?
				textEditTabBSubheader8.EditValue as String :
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

			var clipart1 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart1 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart1Image;
			if (clipart1 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart1.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05BCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
			}

			var clipart2 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart2 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart2Image;
			if (clipart2 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart2.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05BCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
			}

			var clipart3 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.Clipart3 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart3Image;
			if (clipart3 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart3.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05BCLIPART3", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart3.Width, clipart3.Height) });
			}

			outputDataPackage.TextItems = GetOutputDataTextItems();

			outputDataPackage.TextItems.Add("CP05BHEADER", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.SlideHeader?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault)?.Value);
			outputDataPackage.TextItems.Add("HEADER", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabB.SlideHeader?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault)?.Value);

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
