using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabCControl : ShareTabBaseControl
	{
		public ShareTabCControl(IShareTabPageContainer shareTabPageContainer) : base(shareTabPageContainer)
		{
			InitializeComponent();

			comboBoxEditTabCCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabCSubheader1.EnableSelectAll();
			textEditTabCSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabCSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart1Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCClipart1Configuration, ShareContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart2Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCClipart2Configuration, ShareContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart3Image), ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCClipart3Configuration, ShareContentContainer);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabCSubheader2.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader2Placeholder ?? textEditTabCSubheader2.Properties.NullText;
			memoEditTabCSubheader3.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader3Placeholder ?? memoEditTabCSubheader3.Properties.NullText;
			textEditTabCSubheader4.Properties.NullText = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader4Placeholder ?? textEditTabCSubheader4.Properties.NullText;

			comboBoxEditTabCCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo1.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo1.Properties.NullText;
			comboBoxEditTabCCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo2.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo2.Properties.NullText;
			comboBoxEditTabCCombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo3.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo3.Properties.NullText;
			comboBoxEditTabCCombo4.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo4.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo4.Properties.NullText;
			comboBoxEditTabCCombo5.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo5.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo5.Properties.NullText;
			comboBoxEditTabCCombo6.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo6.Properties.NullText =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo6.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart1);
			clipartEditContainer2.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart2);
			clipartEditContainer3.LoadData(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart3);

			checkEditTabCGroup1.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group1Toggle;
			checkEditTabCGroup2.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group2Toggle;
			checkEditTabCGroup3.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group3Toggle;
			checkEditTabCGroup4.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group4Toggle;
			checkEditTabCGroup5.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group5Toggle;
			checkEditTabCGroup6.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group6Toggle;
			checkEditTabCSubheader3.Checked = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3Toggle;

			comboBoxEditTabCCombo1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo5.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo5 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo6.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo6 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.FirstOrDefault(item => item.IsDefault);

			spinEditTabCSubheader1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader1 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader1DefaultValue;
			textEditTabCSubheader2.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader2 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader3DefaultValue;
			textEditTabCSubheader4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader4 ??
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader4DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabCFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group1Toggle = checkEditTabCGroup1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group2Toggle = checkEditTabCGroup2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group3Toggle = checkEditTabCGroup3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group4Toggle = checkEditTabCGroup4.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group5Toggle = checkEditTabCGroup5.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group6Toggle = checkEditTabCGroup6.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3Toggle = checkEditTabCSubheader3.Checked;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo1 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
				comboBoxEditTabCCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo1.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo2 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo2.EditValue ?
				comboBoxEditTabCCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo2.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo3 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo3.EditValue ?
				comboBoxEditTabCCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo3.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo4 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo4.EditValue ?
				comboBoxEditTabCCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo4.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo5 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo5.EditValue ?
				comboBoxEditTabCCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo5.EditValue as String } :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo6 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo6.EditValue ?
				comboBoxEditTabCCombo6.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo6.EditValue as String } :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader1 = (decimal?)spinEditTabCSubheader1.EditValue != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader1DefaultValue ?
				(decimal?)spinEditTabCSubheader1.EditValue :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader2 = textEditTabCSubheader2.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader2DefaultValue ?
				textEditTabCSubheader2.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader3DefaultValue ?
				memoEditTabCSubheader3.EditValue as String ?? String.Empty :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader4 = textEditTabCSubheader4.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader4DefaultValue ?
				textEditTabCSubheader4.EditValue as String ?? String.Empty :
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

		private void OnTabCGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup1Inner.Enabled = checkEditTabCGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCSubheader3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader3Value.Enabled = checkEditTabCSubheader3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup2Inner.Enabled = checkEditTabCGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup3Inner.Enabled = checkEditTabCGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup4Inner.Enabled = checkEditTabCGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup5Inner.Enabled = checkEditTabCGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup6Inner.Enabled = checkEditTabCGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabCFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var multiplierText = (comboBoxEditTabCCombo1.EditValue as ListDataItem)?.Value ?? String.Empty;

			var sourceValue = 0.0;
			try
			{
				sourceValue = (double)spinEditTabCSubheader1.Value *
							  (multiplierText.StartsWith("mi", StringComparison.InvariantCultureIgnoreCase)
								  ? 1000000
								  : (multiplierText.StartsWith("bi", StringComparison.InvariantCultureIgnoreCase)
									  ? 1000000000
									  : 1000000000000));
			}
			catch
			{
			}

			var householdPercent = 0.0;
			try
			{
				householdPercent =
					Double.Parse((comboBoxEditTabCCombo4.EditValue as ListDataItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharePercent = 0.0;
			try
			{
				sharePercent =
					Double.Parse((comboBoxEditTabCCombo6.EditValue as ListDataItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var formula1Value = sourceValue * (householdPercent / 100);
			var formula2Value = formula1Value * (sharePercent / 100);

			simpleLabelItemTabCFormula1.CustomizationFormText = String.Format("{0:$#,##0} {1} x {2} = {3:$#,##0}",
				spinEditTabCSubheader1.Value,
				(comboBoxEditTabCCombo1.EditValue as ListDataItem)?.Value?.Trim(),
				(comboBoxEditTabCCombo4.EditValue as ListDataItem)?.Value?.Trim(),
				formula1Value);
			simpleLabelItemTabCFormula1.Text = String.Format("{0:$#,##0} {1} x {2} = <b>{3:$#,##0}</b>",
				spinEditTabCSubheader1.Value,
				(comboBoxEditTabCCombo1.EditValue as ListDataItem)?.Value?.Trim(),
				(comboBoxEditTabCCombo4.EditValue as ListDataItem)?.Value?.Trim(),
				formula1Value);
			simpleLabelItemTabCFormula2.CustomizationFormText = String.Format("{0:$#,##0}", formula1Value);
			simpleLabelItemTabCFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula1Value);
			simpleLabelItemTabCFormula3.CustomizationFormText = String.Format("Total Estimated Revenue in {0}",
				(comboBoxEditTabCCombo3.EditValue as ListDataItem)?.Value?.Trim());
			simpleLabelItemTabCFormula3.Text = String.Format("Total Estimated Revenue in {0}",
				(comboBoxEditTabCCombo3.EditValue as ListDataItem)?.Value?.Trim());
			simpleLabelItemTabCFormula4.CustomizationFormText = String.Format("Share Growth in {0}",
				(comboBoxEditTabCCombo3.EditValue as ListDataItem)?.Value?.Trim());
			simpleLabelItemTabCFormula4.Text = String.Format("Share Growth in {0}",
				(comboBoxEditTabCCombo3.EditValue as ListDataItem)?.Value?.Trim());
			simpleLabelItemTabCFormula5.CustomizationFormText = String.Format("{0:$#,##0} (annually)", formula2Value);
			simpleLabelItemTabCFormula5.Text = String.Format("<b>{0:$#,##0}</b> (annually)", formula2Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override String OutputName => ShareContentContainer.SlideContainer.StarInfo.Titles.Tab5SubCTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05C-1.pptx");
			outputDataPackage.Theme = ShareContentContainer.SelectedTheme;

			var clipart1 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart1 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05CCLIPART1", clipart1);

			var clipart2 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart2 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05CCLIPART2", clipart2);

			var clipart3 = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart3 ?? ImageClipartObject.FromImage(ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05CCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.SlideHeader ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05CHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(String.Format("Americans SPENT: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader1 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader1DefaultValue));
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo1?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("in: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo2?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("on: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader2 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader2DefaultValue));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3Toggle)
					textDataItems.Add("CP05CFormulaPhrase2".ToUpper(), ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader3DefaultValue);
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo3?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("represents {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo4?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("of {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo5?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group3Toggle)
				textDataItems.Add("CP05CFormulaPhrase4".ToUpper(), simpleLabelItemTabCFormula1.CustomizationFormText);


			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabCFormula2.CustomizationFormText);
				itemParts.Add(simpleLabelItemTabCFormula3.CustomizationFormText);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo6?.Value ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(simpleLabelItemTabCFormula4.CustomizationFormText);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabCFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group6Toggle)
				textDataItems.Add("CP05CFormulaPhrase8".ToUpper(), String.Format("Source: {0}", ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader4 ?? ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader4DefaultValue));

			return textDataItems;
		}
		#endregion
	}
}
