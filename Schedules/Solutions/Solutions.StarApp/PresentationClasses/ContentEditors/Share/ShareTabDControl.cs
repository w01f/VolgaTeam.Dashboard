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
	public partial class ShareTabDControl : ChildTabBaseControl
	{
		private ShareTabDInfo CustomTabInfo => (ShareTabDInfo)TabInfo;

		public ShareTabDControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabDSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabDSubheader1.Properties.NullText;
			textEditTabDSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? textEditTabDSubheader2.Properties.NullText;
			textEditTabDSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabDSubheader3.Properties.NullText;
			textEditTabDSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? textEditTabDSubheader5.Properties.NullText;
			textEditTabDSubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? textEditTabDSubheader6.Properties.NullText;
			textEditTabDSubheader7.Properties.NullText = CustomTabInfo.SubHeader7Placeholder ?? textEditTabDSubheader7.Properties.NullText;
			textEditTabDSubheader8.Properties.NullText = CustomTabInfo.SubHeader8Placeholder ?? textEditTabDSubheader8.Properties.NullText;
			textEditTabDSubheader9.Properties.NullText = CustomTabInfo.SubHeader9Placeholder ?? textEditTabDSubheader9.Properties.NullText;

			comboBoxEditTabDCombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabDCombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabDCombo1.Properties.NullText;
			comboBoxEditTabDCombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabDCombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabDCombo2.Properties.NullText;
			comboBoxEditTabDCombo3.Properties.Items.AddRange(CustomTabInfo.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabDCombo3.Properties.NullText =
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabDCombo3.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ShareState.TabD.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ShareState.TabD.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ShareState.TabD.Clipart3);

			checkEditTabDGroup1.Checked = SlideContainer.EditedContent.ShareState.TabD.Group1Toggle;
			checkEditTabDGroup2.Checked = SlideContainer.EditedContent.ShareState.TabD.Group2Toggle;
			checkEditTabDGroup3.Checked = SlideContainer.EditedContent.ShareState.TabD.Group3Toggle;
			checkEditTabDGroup4.Checked = SlideContainer.EditedContent.ShareState.TabD.Group4Toggle;
			checkEditTabDGroup5.Checked = SlideContainer.EditedContent.ShareState.TabD.Group5Toggle;
			checkEditTabDGroup6.Checked = SlideContainer.EditedContent.ShareState.TabD.Group6Toggle;
			checkEditTabDGroup7.Checked = SlideContainer.EditedContent.ShareState.TabD.Group7Toggle;

			comboBoxEditTabDCombo1.EditValue = SlideContainer.EditedContent.ShareState.TabD.Combo1 ??
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo2.EditValue = SlideContainer.EditedContent.ShareState.TabD.Combo2 ??
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo3.EditValue = SlideContainer.EditedContent.ShareState.TabD.Combo3 ??
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);

			textEditTabDSubheader1.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			textEditTabDSubheader2.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			spinEditTabDSubheader4.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			textEditTabDSubheader6.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			textEditTabDSubheader8.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader8 ??
				CustomTabInfo.SubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = SlideContainer.EditedContent.ShareState.TabD.Subheader9 ??
				CustomTabInfo.SubHeader9DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabDFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ShareState.TabD.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabD.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabD.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ShareState.TabD.Group1Toggle = checkEditTabDGroup1.Checked;
			SlideContainer.EditedContent.ShareState.TabD.Group2Toggle = checkEditTabDGroup2.Checked;
			SlideContainer.EditedContent.ShareState.TabD.Group3Toggle = checkEditTabDGroup3.Checked;
			SlideContainer.EditedContent.ShareState.TabD.Group4Toggle = checkEditTabDGroup4.Checked;
			SlideContainer.EditedContent.ShareState.TabD.Group5Toggle = checkEditTabDGroup5.Checked;
			SlideContainer.EditedContent.ShareState.TabD.Group6Toggle = checkEditTabDGroup6.Checked;
			SlideContainer.EditedContent.ShareState.TabD.Group7Toggle = checkEditTabDGroup7.Checked;

			SlideContainer.EditedContent.ShareState.TabD.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabDCombo1.EditValue ?
				comboBoxEditTabDCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabDCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabDCombo2.EditValue ?
				comboBoxEditTabDCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabDCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabDCombo3.EditValue ?
				comboBoxEditTabDCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabDCombo3.EditValue as String } :
				null;

			SlideContainer.EditedContent.ShareState.TabD.Subheader1 = textEditTabDSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabDSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader2 = textEditTabDSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				textEditTabDSubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader3 = textEditTabDSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabDSubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader4 = (decimal?)spinEditTabDSubheader4.EditValue != CustomTabInfo.SubHeader4DefaultValue ?
				(decimal?)spinEditTabDSubheader4.EditValue :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader5 = textEditTabDSubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue ?
				textEditTabDSubheader5.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader6 = textEditTabDSubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue ?
				textEditTabDSubheader6.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader7 = textEditTabDSubheader7.EditValue as String != CustomTabInfo.SubHeader7DefaultValue ?
				textEditTabDSubheader7.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader8 = textEditTabDSubheader8.EditValue as String != CustomTabInfo.SubHeader8DefaultValue ?
				textEditTabDSubheader8.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabD.Subheader9 = textEditTabDSubheader9.EditValue as String != CustomTabInfo.SubHeader9DefaultValue ?
				textEditTabDSubheader9.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.ShareState.TabD.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.ShareState.TabD.SlideHeader =
				slideHeaderValue != TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
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
		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05D-1.pptx");
			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ShareState.TabD.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05DCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ShareState.TabD.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05DCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ShareState.TabD.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05DCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ShareState.TabD.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05DHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ShareState.TabD.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabD.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ShareState.TabD.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase2".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabD.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);
				itemParts.Add(String.Format("is {0}", SlideContainer.EditedContent.ShareState.TabD.Combo1?.Value ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("of {0}", SlideContainer.EditedContent.ShareState.TabD.Combo2?.Value ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabD.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabDFormula1.CustomizationFormText);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue);
				itemParts.Add(String.Format("x {0}", simpleLabelItemTabDFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabD.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabDFormula3.CustomizationFormText);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabD.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabD.Combo3?.Value ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("Share Growth = {0}", simpleLabelItemTabDFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05DFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabD.Group7Toggle)
				textDataItems.Add("CP05DFormulaPhrase7".ToUpper(), String.Format("Source: {0}", SlideContainer.EditedContent.ShareState.TabD.Subheader9 ?? CustomTabInfo.SubHeader9DefaultValue));

			return textDataItems;
		}
		#endregion
	}
}
