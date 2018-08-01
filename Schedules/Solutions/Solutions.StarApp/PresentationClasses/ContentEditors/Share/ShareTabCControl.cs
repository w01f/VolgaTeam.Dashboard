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
	public partial class ShareTabCControl : ChildTabBaseControl
	{
		private ShareTabCInfo CustomTabInfo => (ShareTabCInfo)TabInfo;

		public ShareTabCControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabCSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? textEditTabCSubheader2.Properties.NullText;
			memoEditTabCSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? memoEditTabCSubheader3.Properties.NullText;
			textEditTabCSubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? textEditTabCSubheader4.Properties.NullText;

			comboBoxEditTabCCombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo1.Properties.NullText;
			comboBoxEditTabCCombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo2.Properties.NullText;
			comboBoxEditTabCCombo3.Properties.Items.AddRange(CustomTabInfo.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo3.Properties.NullText =
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo3.Properties.NullText;
			comboBoxEditTabCCombo4.Properties.Items.AddRange(CustomTabInfo.Combo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo4.Properties.NullText =
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo4.Properties.NullText;
			comboBoxEditTabCCombo5.Properties.Items.AddRange(CustomTabInfo.Combo5Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo5.Properties.NullText =
				CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo5.Properties.NullText;
			comboBoxEditTabCCombo6.Properties.Items.AddRange(CustomTabInfo.Combo6Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo6.Properties.NullText =
				CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo6.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ShareState.TabC.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ShareState.TabC.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ShareState.TabC.Clipart3);

			checkEditTabCGroup1.Checked = SlideContainer.EditedContent.ShareState.TabC.Group1Toggle;
			checkEditTabCGroup2.Checked = SlideContainer.EditedContent.ShareState.TabC.Group2Toggle;
			checkEditTabCGroup3.Checked = SlideContainer.EditedContent.ShareState.TabC.Group3Toggle;
			checkEditTabCGroup4.Checked = SlideContainer.EditedContent.ShareState.TabC.Group4Toggle;
			checkEditTabCGroup5.Checked = SlideContainer.EditedContent.ShareState.TabC.Group5Toggle;
			checkEditTabCGroup6.Checked = SlideContainer.EditedContent.ShareState.TabC.Group6Toggle;
			checkEditTabCSubheader3.Checked = SlideContainer.EditedContent.ShareState.TabC.Subheader3Toggle;

			comboBoxEditTabCCombo1.EditValue = SlideContainer.EditedContent.ShareState.TabC.Combo1 ??
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo2.EditValue = SlideContainer.EditedContent.ShareState.TabC.Combo2 ??
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo3.EditValue = SlideContainer.EditedContent.ShareState.TabC.Combo3 ??
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo4.EditValue = SlideContainer.EditedContent.ShareState.TabC.Combo4 ??
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo5.EditValue = SlideContainer.EditedContent.ShareState.TabC.Combo5 ??
				CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo6.EditValue = SlideContainer.EditedContent.ShareState.TabC.Combo6 ??
				CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsDefault);

			spinEditTabCSubheader1.EditValue = SlideContainer.EditedContent.ShareState.TabC.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			textEditTabCSubheader2.EditValue = SlideContainer.EditedContent.ShareState.TabC.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.EditedContent.ShareState.TabC.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			textEditTabCSubheader4.EditValue = SlideContainer.EditedContent.ShareState.TabC.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabCFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ShareState.TabC.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabC.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabC.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ShareState.TabC.Group1Toggle = checkEditTabCGroup1.Checked;
			SlideContainer.EditedContent.ShareState.TabC.Group2Toggle = checkEditTabCGroup2.Checked;
			SlideContainer.EditedContent.ShareState.TabC.Group3Toggle = checkEditTabCGroup3.Checked;
			SlideContainer.EditedContent.ShareState.TabC.Group4Toggle = checkEditTabCGroup4.Checked;
			SlideContainer.EditedContent.ShareState.TabC.Group5Toggle = checkEditTabCGroup5.Checked;
			SlideContainer.EditedContent.ShareState.TabC.Group6Toggle = checkEditTabCGroup6.Checked;
			SlideContainer.EditedContent.ShareState.TabC.Subheader3Toggle = checkEditTabCSubheader3.Checked;

			SlideContainer.EditedContent.ShareState.TabC.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
				comboBoxEditTabCCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo2.EditValue ?
				comboBoxEditTabCCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo3.EditValue ?
				comboBoxEditTabCCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo4.EditValue ?
				comboBoxEditTabCCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Combo5 = CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo5.EditValue ?
				comboBoxEditTabCCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo5.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Combo6 = CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo6.EditValue ?
				comboBoxEditTabCCombo6.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo6.EditValue as String } :
				null;

			SlideContainer.EditedContent.ShareState.TabC.Subheader1 = (decimal?)spinEditTabCSubheader1.EditValue != CustomTabInfo.SubHeader1DefaultValue ?
				(decimal?)spinEditTabCSubheader1.EditValue :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Subheader2 = textEditTabCSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				textEditTabCSubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				memoEditTabCSubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabC.Subheader4 = textEditTabCSubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue ?
				textEditTabCSubheader4.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.ShareState.TabC.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ShareState.TabC.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.ShareState.TabC.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ShareState.TabC.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
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
		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05C-1.pptx");
			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ShareState.TabC.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05CCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ShareState.TabC.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05CCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ShareState.TabC.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05CCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ShareState.TabC.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05CHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ShareState.TabC.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(String.Format("Americans SPENT: {0}", SlideContainer.EditedContent.ShareState.TabC.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue));
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabC.Combo1?.Value ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("in: {0}", SlideContainer.EditedContent.ShareState.TabC.Combo2?.Value ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("on: {0}", SlideContainer.EditedContent.ShareState.TabC.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ShareState.TabC.Subheader3Toggle)
					textDataItems.Add("CP05CFormulaPhrase2".ToUpper(), SlideContainer.EditedContent.ShareState.TabC.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
			}

			if (SlideContainer.EditedContent.ShareState.TabC.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabC.Combo3?.Value ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(String.Format("represents {0}", SlideContainer.EditedContent.ShareState.TabC.Combo4?.Value ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault)?.Value));
				itemParts.Add(String.Format("of {0}", SlideContainer.EditedContent.ShareState.TabC.Combo5?.Value ?? CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault)?.Value));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabC.Group3Toggle)
				textDataItems.Add("CP05CFormulaPhrase4".ToUpper(), simpleLabelItemTabCFormula1.CustomizationFormText);


			if (SlideContainer.EditedContent.ShareState.TabC.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(simpleLabelItemTabCFormula2.CustomizationFormText);
				itemParts.Add(simpleLabelItemTabCFormula3.CustomizationFormText);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabC.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabC.Combo6?.Value ?? CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(simpleLabelItemTabCFormula4.CustomizationFormText);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabCFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05CFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabC.Group6Toggle)
				textDataItems.Add("CP05CFormulaPhrase8".ToUpper(), String.Format("Source: {0}", SlideContainer.EditedContent.ShareState.TabC.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue));

			return textDataItems;
		}
		#endregion
	}
}
