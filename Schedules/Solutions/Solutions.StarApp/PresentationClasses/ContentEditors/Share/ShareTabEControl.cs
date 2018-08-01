using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Share;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Share
{
	public partial class ShareTabEControl : ChildTabBaseControl
	{
		private ShareTabEInfo CustomTabInfo => (ShareTabEInfo)TabInfo;

		public ShareTabEControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabESubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabESubheader1.Properties.NullText;
			textEditTabESubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? textEditTabESubheader2.Properties.NullText;
			textEditTabESubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabESubheader3.Properties.NullText;
			textEditTabESubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? textEditTabESubheader4.Properties.NullText;
			textEditTabESubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? textEditTabESubheader5.Properties.NullText;
			textEditTabESubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? textEditTabESubheader6.Properties.NullText;
			textEditTabESubheader8.Properties.NullText = CustomTabInfo.SubHeader8Placeholder ?? textEditTabESubheader8.Properties.NullText;
			textEditTabESubheader9.Properties.NullText = CustomTabInfo.SubHeader9Placeholder ?? textEditTabESubheader9.Properties.NullText;
			textEditTabESubheader10.Properties.NullText = CustomTabInfo.SubHeader10Placeholder ?? textEditTabESubheader10.Properties.NullText;

			comboBoxEditTabECombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo1.Properties.NullText;
			comboBoxEditTabECombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo2.Properties.NullText;
			comboBoxEditTabECombo3.Properties.Items.AddRange(CustomTabInfo.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo3.Properties.NullText =
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo3.Properties.NullText;
			comboBoxEditTabECombo4.Properties.Items.AddRange(CustomTabInfo.Combo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabECombo4.Properties.NullText =
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabECombo4.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ShareState.TabE.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ShareState.TabE.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ShareState.TabE.Clipart3);

			checkEditTabEGroup1.Checked = SlideContainer.EditedContent.ShareState.TabE.Group1Toggle;
			checkEditTabEGroup2.Checked = SlideContainer.EditedContent.ShareState.TabE.Group2Toggle;
			checkEditTabEGroup3.Checked = SlideContainer.EditedContent.ShareState.TabE.Group3Toggle;
			checkEditTabEGroup4.Checked = SlideContainer.EditedContent.ShareState.TabE.Group4Toggle;
			checkEditTabEGroup5.Checked = SlideContainer.EditedContent.ShareState.TabE.Group5Toggle;
			checkEditTabEGroup6.Checked = SlideContainer.EditedContent.ShareState.TabE.Group6Toggle;
			checkEditTabEGroup7.Checked = SlideContainer.EditedContent.ShareState.TabE.Group7Toggle;
			checkEditTabESubheader4.Checked = SlideContainer.EditedContent.ShareState.TabE.Subheader4Toggle;

			comboBoxEditTabECombo1.EditValue = SlideContainer.EditedContent.ShareState.TabE.Combo1 ??
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo2.EditValue = SlideContainer.EditedContent.ShareState.TabE.Combo2 ??
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo3.EditValue = SlideContainer.EditedContent.ShareState.TabE.Combo3 ??
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo4.EditValue = SlideContainer.EditedContent.ShareState.TabE.Combo4 ??
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabESubheader1.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			textEditTabESubheader2.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabESubheader3.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			textEditTabESubheader4.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			textEditTabESubheader5.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			textEditTabESubheader6.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			spinEditTabESubheader7.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			textEditTabESubheader8.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader8 ??
				CustomTabInfo.SubHeader8DefaultValue;
			textEditTabESubheader9.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader9 ??
				CustomTabInfo.SubHeader9DefaultValue;
			textEditTabESubheader10.EditValue = SlideContainer.EditedContent.ShareState.TabE.Subheader10 ??
				CustomTabInfo.SubHeader10DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabEFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ShareState.TabE.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabE.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabE.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ShareState.TabE.Group1Toggle = checkEditTabEGroup1.Checked;
			SlideContainer.EditedContent.ShareState.TabE.Group2Toggle = checkEditTabEGroup2.Checked;
			SlideContainer.EditedContent.ShareState.TabE.Group3Toggle = checkEditTabEGroup3.Checked;
			SlideContainer.EditedContent.ShareState.TabE.Group4Toggle = checkEditTabEGroup4.Checked;
			SlideContainer.EditedContent.ShareState.TabE.Group5Toggle = checkEditTabEGroup5.Checked;
			SlideContainer.EditedContent.ShareState.TabE.Group6Toggle = checkEditTabEGroup6.Checked;
			SlideContainer.EditedContent.ShareState.TabE.Group7Toggle = checkEditTabEGroup7.Checked;
			SlideContainer.EditedContent.ShareState.TabE.Subheader4Toggle = checkEditTabESubheader4.Checked;

			SlideContainer.EditedContent.ShareState.TabE.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo1.EditValue ?
				comboBoxEditTabECombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabECombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo2.EditValue ?
				comboBoxEditTabECombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabECombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo3.EditValue ?
				comboBoxEditTabECombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabECombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabECombo4.EditValue ?
				comboBoxEditTabECombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabECombo4.EditValue as String } :
				null;

			SlideContainer.EditedContent.ShareState.TabE.Subheader1 = textEditTabESubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabESubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader2 = textEditTabESubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				textEditTabESubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader3 = textEditTabESubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabESubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader4 = textEditTabESubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue ?
				textEditTabESubheader4.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader5 = textEditTabESubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue ?
				textEditTabESubheader5.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader6 = textEditTabESubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue ?
				textEditTabESubheader6.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader7 = (decimal?)spinEditTabESubheader7.EditValue != CustomTabInfo.SubHeader7DefaultValue ?
				(decimal?)spinEditTabESubheader7.EditValue :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader8 = textEditTabESubheader8.EditValue as String != CustomTabInfo.SubHeader8DefaultValue ?
				textEditTabESubheader8.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader9 = textEditTabESubheader9.EditValue as String != CustomTabInfo.SubHeader9DefaultValue ?
				textEditTabESubheader9.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabE.Subheader10 = textEditTabESubheader10.EditValue as String != CustomTabInfo.SubHeader10DefaultValue ?
				textEditTabESubheader10.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.ShareState.TabE.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ShareState.TabE.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.ShareState.TabE.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ShareState.TabE.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
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
		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05E-1.pptx");
			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ShareState.TabE.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05ECLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ShareState.TabE.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05ECLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ShareState.TabE.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05ECLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ShareState.TabE.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05EHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ShareState.TabE.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Combo1?.Value ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ShareState.TabE.Subheader4Toggle)
					textDataItems.Add("CP05EFormulaPhrase2".ToUpper(), SlideContainer.EditedContent.ShareState.TabE.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue);
			}

			if (SlideContainer.EditedContent.ShareState.TabE.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue);
				itemParts.Add(String.Format("is {0}", SlideContainer.EditedContent.ShareState.TabE.Combo2?.Value ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("of {0}", SlideContainer.EditedContent.ShareState.TabE.Combo3?.Value ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabE.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);
				itemParts.Add(String.Format("is {0}", SlideContainer.EditedContent.ShareState.TabE.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabE.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabEFormula1.CustomizationFormText);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue);
				itemParts.Add(String.Format("x {0}", simpleLabelItemTabEFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabE.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabEFormula3.CustomizationFormText);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Subheader9 ?? CustomTabInfo.SubHeader9DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabE.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabE.Combo4?.Value ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(simpleLabelItemTabEFormula4.CustomizationFormText);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabEFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05EFormulaPhrase7".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabE.Group7Toggle)
				textDataItems.Add("CP05EFormulaPhrase8".ToUpper(), String.Format("Source: {0}", SlideContainer.EditedContent.ShareState.TabE.Subheader10 ?? CustomTabInfo.SubHeader10DefaultValue));

			return textDataItems;
		}
		#endregion
	}
}
