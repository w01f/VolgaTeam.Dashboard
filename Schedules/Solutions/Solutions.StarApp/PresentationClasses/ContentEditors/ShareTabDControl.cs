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
	public partial class ShareTabDControl : ShareTabBaseControl
	{
		public ShareTabDControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabDCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabDCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabDCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader4.EnableSelectAll();
			textEditTabDSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader8.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			pictureEditTabDClipart1.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart1Image;
			pictureEditTabDClipart1.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDClipart1Configuration.Alignment;
			pictureEditTabDClipart2.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart2Image;
			pictureEditTabDClipart2.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDClipart2Configuration.Alignment;
			pictureEditTabDClipart3.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart3Image;
			pictureEditTabDClipart3.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDClipart3Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabDClipart1,
				pictureEditTabDClipart2,
				pictureEditTabDClipart3,
			});

			Application.DoEvents();

			textEditTabDSubheader1.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader1Placeholder ?? textEditTabDSubheader1.Properties.NullText;
			textEditTabDSubheader2.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader2Placeholder ?? textEditTabDSubheader2.Properties.NullText;
			textEditTabDSubheader3.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader3Placeholder ?? textEditTabDSubheader3.Properties.NullText;
			textEditTabDSubheader5.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader5Placeholder ?? textEditTabDSubheader5.Properties.NullText;
			textEditTabDSubheader6.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader6Placeholder ?? textEditTabDSubheader6.Properties.NullText;
			textEditTabDSubheader7.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader7Placeholder ?? textEditTabDSubheader7.Properties.NullText;
			textEditTabDSubheader8.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader8Placeholder ?? textEditTabDSubheader8.Properties.NullText;
			textEditTabDSubheader9.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader9Placeholder ?? textEditTabDSubheader9.Properties.NullText;

			comboBoxEditTabDCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabDCombo1.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabDCombo1.Properties.NullText;
			comboBoxEditTabDCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabDCombo2.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabDCombo2.Properties.NullText;
			comboBoxEditTabDCombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabDCombo3.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabDCombo3.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabDClipart1.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart1 ??
				pictureEditTabDClipart1.Image;
			pictureEditTabDClipart2.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart2 ??
				pictureEditTabDClipart2.Image;
			pictureEditTabDClipart3.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart3 ??
				pictureEditTabDClipart3.Image;

			checkEditTabDGroup1.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group1Toggle;
			checkEditTabDGroup2.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group2Toggle;
			checkEditTabDGroup3.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group3Toggle;
			checkEditTabDGroup4.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group4Toggle;
			checkEditTabDGroup5.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group5Toggle;
			checkEditTabDGroup6.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group6Toggle;
			checkEditTabDGroup7.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group7Toggle;

			comboBoxEditTabDCombo1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items.FirstOrDefault(item => item.IsDefault);

			textEditTabDSubheader1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader1DefaultValue;
			textEditTabDSubheader2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader3DefaultValue;
			spinEditTabDSubheader4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader5 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader5DefaultValue;
			textEditTabDSubheader6.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader6 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader7 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader7DefaultValue;
			textEditTabDSubheader8.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader8 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader9 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader9DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabDFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart1 = pictureEditTabDClipart1.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart1Image ?
				pictureEditTabDClipart1.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart2 = pictureEditTabDClipart2.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart2Image ?
				pictureEditTabDClipart2.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart3 = pictureEditTabDClipart3.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart3Image ?
				pictureEditTabDClipart3.Image :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group1Toggle = checkEditTabDGroup1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group2Toggle = checkEditTabDGroup2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group3Toggle = checkEditTabDGroup3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group4Toggle = checkEditTabDGroup4.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group5Toggle = checkEditTabDGroup5.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group6Toggle = checkEditTabDGroup6.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group7Toggle = checkEditTabDGroup7.Checked;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo1 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabDCombo1.EditValue ?
				comboBoxEditTabDCombo1.EditValue as ListDataItem ?? (comboBoxEditTabDCombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabDCombo1.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo2 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabDCombo2.EditValue ?
				comboBoxEditTabDCombo2.EditValue as ListDataItem ?? (comboBoxEditTabDCombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabDCombo2.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo3 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabDCombo3.EditValue ?
				comboBoxEditTabDCombo3.EditValue as ListDataItem ?? (comboBoxEditTabDCombo3.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabDCombo3.EditValue } : null) :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader1 = textEditTabDSubheader1.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader1DefaultValue ?
				textEditTabDSubheader1.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader2 = textEditTabDSubheader2.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader2DefaultValue ?
				textEditTabDSubheader2.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader3 = textEditTabDSubheader3.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader3DefaultValue ?
				textEditTabDSubheader3.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader4 = (decimal?)spinEditTabDSubheader4.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader4DefaultValue ?
				(decimal?)spinEditTabDSubheader4.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader5 = textEditTabDSubheader5.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader5DefaultValue ?
				textEditTabDSubheader5.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader6 = textEditTabDSubheader6.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader6DefaultValue ?
				textEditTabDSubheader6.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader7 = textEditTabDSubheader7.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader7DefaultValue ?
				textEditTabDSubheader7.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader8 = textEditTabDSubheader8.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader8DefaultValue ?
				textEditTabDSubheader8.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader9 = textEditTabDSubheader9.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader9DefaultValue ?
				textEditTabDSubheader9.EditValue as String :
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

		private void OnTabDGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup1Inner.Enabled = checkEditTabDGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup2Inner.Enabled = checkEditTabDGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup3Inner.Enabled = checkEditTabDGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup4Inner.Enabled = checkEditTabDGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup5Inner.Enabled = checkEditTabDGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup6Inner.Enabled = checkEditTabDGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup7Inner.Enabled = checkEditTabDGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var sourceValue = 0.0;
			try
			{
				sourceValue = Double.Parse((textEditTabDSubheader1.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var costValue = (double)spinEditTabDSubheader4.Value;

			var householdPercent = 0.0;
			try
			{
				householdPercent =
					Double.Parse((comboBoxEditTabDCombo1.EditValue as ListDataItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharePercent = 0.0;
			try
			{
				sharePercent =
					Double.Parse((comboBoxEditTabDCombo3.EditValue as ListDataItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var formula1Value = sourceValue * (householdPercent / 100);
			var formula2Value = formula1Value * costValue;
			var formula3Value = Math.Ceiling(formula2Value * (sharePercent / 100) / 100) * 100;

			simpleLabelItemTabDFormula1.CustomizationFormText = String.Format("{0:#,##0}", formula1Value);
			simpleLabelItemTabDFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabDFormula2.CustomizationFormText = String.Format("{0:$#,##0}", spinEditTabDSubheader4.Value);
			simpleLabelItemTabDFormula2.Text = String.Format("<b>{0:$#,##0}</b>", spinEditTabDSubheader4.Value);
			simpleLabelItemTabDFormula3.CustomizationFormText = String.Format("{0:$#,##0}", formula2Value);
			simpleLabelItemTabDFormula3.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabDFormula5.CustomizationFormText = String.Format("{0:$#,##0}", formula3Value);
			simpleLabelItemTabDFormula5.Text = String.Format("<b>{0:$#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ShareTabD;
		public override String OutputName => ShareContentContainer.SlideContainer.StarInfo.Titles.Tab5SubDTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05D-1.pptx");
			outputDataPackage.Theme = ShareContentContainer.SelectedTheme;

			var clipart1 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart1 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart1Image;
			if (clipart1 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart1.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05DCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
			}

			var clipart2 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart2 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart2Image;
			if (clipart2 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart2.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05DCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
			}

			var clipart3 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Clipart3 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubDClipart3Image;
			if (clipart3 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart3.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05DCLIPART3", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart3.Width, clipart3.Height) });
			}

			outputDataPackage.TextItems = GetOutputDataTextItems();

			outputDataPackage.TextItems.Add("CP05DHEADER", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.SlideHeader?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault)?.Value);
			outputDataPackage.TextItems.Add("HEADER", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.SlideHeader?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault)?.Value);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader1 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader1DefaultValue);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader2 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader2DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader3 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader3DefaultValue);
				itemParts.Add((ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader4 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader4DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader5 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader5DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase2".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader6 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader6DefaultValue);
				itemParts.Add(String.Format("is {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo1?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("of {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo2?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabDFormula1.CustomizationFormText);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader7 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader7DefaultValue);
				itemParts.Add(String.Format("x {0}", simpleLabelItemTabDFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabDFormula3.CustomizationFormText);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader8 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader8DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Combo3?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("Share Growth = {0}", simpleLabelItemTabDFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Group7Toggle)
				textDataItems.Add("CP05DFormulaPhrase7".ToUpper(), String.Format("Source: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader9 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader9DefaultValue));

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
