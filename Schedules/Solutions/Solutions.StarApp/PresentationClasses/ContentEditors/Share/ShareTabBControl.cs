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
	public partial class ShareTabBControl : ChildTabBaseControl
	{
		private ShareTabBInfo CustomTabInfo => (ShareTabBInfo)TabInfo;

		public ShareTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			textEditTabBSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? textEditTabBSubheader2.Properties.NullText;
			textEditTabBSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabBSubheader3.Properties.NullText;
			textEditTabBSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? textEditTabBSubheader5.Properties.NullText;
			textEditTabBSubheader7.Properties.NullText = CustomTabInfo.SubHeader7Placeholder ?? textEditTabBSubheader7.Properties.NullText;
			textEditTabBSubheader8.Properties.NullText = CustomTabInfo.SubHeader8Placeholder ?? textEditTabBSubheader8.Properties.NullText;

			comboBoxEditTabBCombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo1.Properties.NullText;
			comboBoxEditTabBCombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo2.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ShareState.TabB.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ShareState.TabB.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ShareState.TabB.Clipart3);

			checkEditTabBGroup1.Checked = SlideContainer.EditedContent.ShareState.TabB.Group1Toggle;
			checkEditTabBGroup2.Checked = SlideContainer.EditedContent.ShareState.TabB.Group2Toggle;
			checkEditTabBGroup3.Checked = SlideContainer.EditedContent.ShareState.TabB.Group3Toggle;
			checkEditTabBGroup4.Checked = SlideContainer.EditedContent.ShareState.TabB.Group4Toggle;
			checkEditTabBGroup5.Checked = SlideContainer.EditedContent.ShareState.TabB.Group5Toggle;
			checkEditTabBSubheader2.Checked = SlideContainer.EditedContent.ShareState.TabB.Subheader2Toggle;
			checkEditTabBSubheader7.Checked = SlideContainer.EditedContent.ShareState.TabB.Subheader7Toggle;

			comboBoxEditTabBCombo1.EditValue = SlideContainer.EditedContent.ShareState.TabB.Combo1 ??
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue = SlideContainer.EditedContent.ShareState.TabB.Combo2 ??
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);

			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			spinEditTabBSubheader4.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			textEditTabBSubheader5.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			spinEditTabBSubheader6.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			textEditTabBSubheader8.EditValue = SlideContainer.EditedContent.ShareState.TabB.Subheader8 ??
				CustomTabInfo.SubHeader8DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabBFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ShareState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabB.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ShareState.TabB.Group1Toggle = checkEditTabBGroup1.Checked;
			SlideContainer.EditedContent.ShareState.TabB.Group2Toggle = checkEditTabBGroup2.Checked;
			SlideContainer.EditedContent.ShareState.TabB.Group3Toggle = checkEditTabBGroup3.Checked;
			SlideContainer.EditedContent.ShareState.TabB.Group4Toggle = checkEditTabBGroup4.Checked;
			SlideContainer.EditedContent.ShareState.TabB.Subheader2Toggle = checkEditTabBSubheader2.Checked;
			SlideContainer.EditedContent.ShareState.TabB.Subheader7Toggle = checkEditTabBSubheader7.Checked;

			SlideContainer.EditedContent.ShareState.TabB.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
				null;

			SlideContainer.EditedContent.ShareState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				textEditTabBSubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabBSubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Subheader4 = (decimal?)spinEditTabBSubheader4.EditValue != CustomTabInfo.SubHeader4DefaultValue ?
				(decimal?)spinEditTabBSubheader4.EditValue :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Subheader5 = textEditTabBSubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue ?
				textEditTabBSubheader5.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Subheader6 = (decimal?)spinEditTabBSubheader6.EditValue != CustomTabInfo.SubHeader6DefaultValue ?
				(decimal?)spinEditTabBSubheader6.EditValue :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Subheader7 = textEditTabBSubheader7.EditValue as String != CustomTabInfo.SubHeader7DefaultValue ?
				textEditTabBSubheader7.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabB.Subheader8 = textEditTabBSubheader8.EditValue as String != CustomTabInfo.SubHeader8DefaultValue ?
				textEditTabBSubheader8.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.ShareState.TabB.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.ShareState.TabB.SlideHeader =
				slideHeaderValue != TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
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
		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05B-1.pptx");
			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ShareState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ShareState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05BCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ShareState.TabB.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05BCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ShareState.TabB.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05BHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ShareState.TabB.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				itemParts.Add(String.Format("= {0}", SlideContainer.EditedContent.ShareState.TabB.Combo1?.Value ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ShareState.TabB.Subheader2Toggle)
					textDataItems.Add("CP05BFormulaPhrase2".ToUpper(), String.Format("Source: {0}", SlideContainer.EditedContent.ShareState.TabB.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue));
			}

			if (SlideContainer.EditedContent.ShareState.TabB.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabB.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
				itemParts.Add(String.Format("= {0}", SlideContainer.EditedContent.ShareState.TabB.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue));
				itemParts.Add(simpleLabelItemTabBFormula1.CustomizationFormText);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabB.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabB.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue);
				itemParts.Add(String.Format("= {0}", SlideContainer.EditedContent.ShareState.TabB.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ShareState.TabB.Subheader7Toggle)
					textDataItems.Add("CP05BFormulaPhrase5".ToUpper(), String.Format("Source: {0}", SlideContainer.EditedContent.ShareState.TabB.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue));
			}

			if (SlideContainer.EditedContent.ShareState.TabB.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabB.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabB.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabB.Combo2?.Value ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula3.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05BFormulaPhrase7".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			return textDataItems;
		}
		#endregion
	}
}
