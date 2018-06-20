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
	public partial class ShareTabEControl : ShareTabBaseControl
	{
		public ShareTabEControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabECombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabECombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabECombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabECombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabESubheader7.EnableSelectAll();
			textEditTabESubheader8.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabESubheader10.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			pictureEditTabEClipart1.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart1Image;
			pictureEditTabEClipart1.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartEClipart1Configuration.Alignment;
			pictureEditTabEClipart2.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart2Image;
			pictureEditTabEClipart2.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartEClipart2Configuration.Alignment;
			pictureEditTabEClipart3.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart3Image;
			pictureEditTabEClipart3.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartEClipart3Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabEClipart1,
				pictureEditTabEClipart2,
				pictureEditTabEClipart3,
			});

			Application.DoEvents();

			textEditTabESubheader1.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader1Placeholder ?? textEditTabESubheader1.Properties.NullText;
			textEditTabESubheader2.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader2Placeholder ?? textEditTabESubheader2.Properties.NullText;
			textEditTabESubheader3.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader3Placeholder ?? textEditTabESubheader3.Properties.NullText;
			textEditTabESubheader4.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader4Placeholder ?? textEditTabESubheader4.Properties.NullText;
			textEditTabESubheader5.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader5Placeholder ?? textEditTabESubheader5.Properties.NullText;
			textEditTabESubheader6.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader6Placeholder ?? textEditTabESubheader6.Properties.NullText;
			textEditTabESubheader8.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader8Placeholder ?? textEditTabESubheader8.Properties.NullText;
			textEditTabESubheader9.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader9Placeholder ?? textEditTabESubheader9.Properties.NullText;
			textEditTabESubheader10.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader10Placeholder ?? textEditTabESubheader10.Properties.NullText;

			comboBoxEditTabECombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo1.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo1.Properties.NullText;
			comboBoxEditTabECombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo2.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo2.Properties.NullText;
			comboBoxEditTabECombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo3.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo3.Properties.NullText;
			comboBoxEditTabECombo4.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo4.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo4.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabEClipart1.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart1 ??
				pictureEditTabEClipart1.Image;
			pictureEditTabEClipart2.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart2 ??
				pictureEditTabEClipart2.Image;
			pictureEditTabEClipart3.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart3 ??
				pictureEditTabEClipart3.Image;

			checkEditTabEGroup1.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group1Toggle;
			checkEditTabEGroup2.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group2Toggle;
			checkEditTabEGroup3.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group3Toggle;
			checkEditTabEGroup4.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group4Toggle;
			checkEditTabEGroup5.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group5Toggle;
			checkEditTabEGroup6.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group6Toggle;
			checkEditTabEGroup7.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group7Toggle;
			checkEditTabESubheader4.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader4Toggle;

			comboBoxEditTabECombo1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabESubheader1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader1DefaultValue;
			textEditTabESubheader2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader2DefaultValue;
			textEditTabESubheader3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader3DefaultValue;
			textEditTabESubheader4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader4DefaultValue;
			textEditTabESubheader5.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader5 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader5DefaultValue;
			textEditTabESubheader6.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader6 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader6DefaultValue;
			spinEditTabESubheader7.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader7 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader7DefaultValue;
			textEditTabESubheader8.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader8 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader8DefaultValue;
			textEditTabESubheader9.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader9 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader9DefaultValue;
			textEditTabESubheader10.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader10 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader10DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabEFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart1 = pictureEditTabEClipart1.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart1Image ?
				pictureEditTabEClipart1.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart2 = pictureEditTabEClipart2.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart2Image ?
				pictureEditTabEClipart2.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart3 = pictureEditTabEClipart3.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart3Image ?
				pictureEditTabEClipart3.Image :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group1Toggle = checkEditTabEGroup1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group2Toggle = checkEditTabEGroup2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group3Toggle = checkEditTabEGroup3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group4Toggle = checkEditTabEGroup4.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group5Toggle = checkEditTabEGroup5.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group6Toggle = checkEditTabEGroup6.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group7Toggle = checkEditTabEGroup7.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader4Toggle = checkEditTabESubheader4.Checked;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo1 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo1.EditValue ?
				comboBoxEditTabECombo1.EditValue as ListDataItem ?? (comboBoxEditTabECombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabECombo1.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo2 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo2.EditValue ?
				comboBoxEditTabECombo2.EditValue as ListDataItem ?? (comboBoxEditTabECombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabECombo2.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo3 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo3.EditValue ?
				comboBoxEditTabECombo3.EditValue as ListDataItem ?? (comboBoxEditTabECombo3.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabECombo3.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo4 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo4.EditValue ?
				comboBoxEditTabECombo4.EditValue as ListDataItem ?? (comboBoxEditTabECombo4.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabECombo4.EditValue } : null) :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader1 = textEditTabESubheader1.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader1DefaultValue ?
				textEditTabESubheader1.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader2 = textEditTabESubheader2.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader2DefaultValue ?
				textEditTabESubheader2.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader3 = textEditTabESubheader3.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader3DefaultValue ?
				textEditTabESubheader3.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader4 = textEditTabESubheader4.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader4DefaultValue ?
				textEditTabESubheader4.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader5 = textEditTabESubheader5.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader5DefaultValue ?
				textEditTabESubheader5.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader6 = textEditTabESubheader6.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader6DefaultValue ?
				textEditTabESubheader6.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader7 = (decimal?)spinEditTabESubheader7.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader7DefaultValue ?
				(decimal?)spinEditTabESubheader7.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader8 = textEditTabESubheader8.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader8DefaultValue ?
				textEditTabESubheader8.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader9 = textEditTabESubheader9.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader9DefaultValue ?
				textEditTabESubheader9.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader10 = textEditTabESubheader10.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader10DefaultValue ?
				textEditTabESubheader10.EditValue as String :
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

		private void OnTabEGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup1Inner.Enabled = checkEditTabEGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabESubheader4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabESubheader4Value.Enabled = checkEditTabESubheader4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabEGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup2Inner.Enabled = checkEditTabEGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabEGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup3Inner.Enabled = checkEditTabEGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabEGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup4Inner.Enabled = checkEditTabEGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabEGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup5Inner.Enabled = checkEditTabEGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabEGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup6Inner.Enabled = checkEditTabEGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabEGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup7Inner.Enabled = checkEditTabEGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabEFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var multiplierText = (comboBoxEditTabECombo1.EditValue as ListDataItem)?.Value ?? String.Empty;

			var sourceValue = 0.0;
			try
			{
				sourceValue = Double.Parse((textEditTabESubheader2.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) *
							  (multiplierText.StartsWith("mi", StringComparison.InvariantCultureIgnoreCase)
								  ? 1000000
								  : (multiplierText.StartsWith("bi", StringComparison.InvariantCultureIgnoreCase)
									  ? 1000000000
									  : 1000000000000));
			}
			catch
			{
			}

			var costValue = (double)spinEditTabESubheader7.Value;

			var householdPercent = 0.0;
			try
			{
				householdPercent =
					Double.Parse((comboBoxEditTabECombo2.EditValue as ListDataItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharePercent = 0.0;
			try
			{
				sharePercent =
					Double.Parse((comboBoxEditTabECombo4.EditValue as ListDataItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var formula1Value = sourceValue * (householdPercent / 100);
			var formula2Value = formula1Value * costValue;
			var formula3Value = Math.Ceiling(formula2Value * (sharePercent / 100) / 100) * 100;

			simpleLabelItemTabEFormula1.CustomizationFormText = String.Format("{0:#,##0}", formula1Value);
			simpleLabelItemTabEFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabEFormula2.CustomizationFormText = String.Format("{0:$#,##0}", spinEditTabESubheader7.Value);
			simpleLabelItemTabEFormula2.Text = String.Format("<b>{0:$#,##0}</b>", spinEditTabESubheader7.Value);
			simpleLabelItemTabEFormula3.CustomizationFormText = String.Format("{0:$#,##0}", formula2Value);
			simpleLabelItemTabEFormula3.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabEFormula5.CustomizationFormText = String.Format("{0:$#,##0}", formula3Value);
			simpleLabelItemTabEFormula5.Text = String.Format("<b>{0:$#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ShareTabE;
		public override String OutputName => ShareContentContainer.SlideContainer.StarInfo.Titles.Tab5SubETitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05E-1.pptx");
			outputDataPackage.Theme = ShareContentContainer.SelectedTheme;

			var clipart1 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart1 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart1Image;
			if (clipart1 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart1.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05ECLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
			}

			var clipart2 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart2 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart2Image;
			if (clipart2 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart2.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05ECLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
			}

			var clipart3 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Clipart3 ?? ShareContentContainer.SlideContainer.StarInfo.Tab5SubEClipart3Image;
			if (clipart3 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart3.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP05ECLIPART3", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart3.Width, clipart3.Height) });
			}

			outputDataPackage.TextItems = GetOutputDataTextItems();

			outputDataPackage.TextItems.Add("CP05EHEADER", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.SlideHeader?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.FirstOrDefault(h => h.IsDefault)?.Value);
			outputDataPackage.TextItems.Add("HEADER", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.SlideHeader?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.FirstOrDefault(h => h.IsDefault)?.Value);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader1 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader1DefaultValue);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader2 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader2DefaultValue);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo1?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader3 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader3DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader4Toggle)
					textDataItems.Add("CP05EFormulaPhrase2".ToUpper(), ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader4 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader4DefaultValue);
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader5 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader5DefaultValue);
				itemParts.Add(String.Format("is {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo2?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("of {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo3?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader6 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader6DefaultValue);
				itemParts.Add(String.Format("is {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader7 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader7DefaultValue));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabEFormula1.CustomizationFormText);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader8 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader8DefaultValue);
				itemParts.Add(String.Format("x {0}", simpleLabelItemTabEFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabEFormula3.CustomizationFormText);
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader9 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader9DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Combo4?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(simpleLabelItemTabEFormula4.CustomizationFormText);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabEFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase7".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Group7Toggle)
				textDataItems.Add("CP05EFormulaPhrase8".ToUpper(), String.Format("Source: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader10 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader10DefaultValue));

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
