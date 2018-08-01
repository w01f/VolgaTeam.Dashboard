using System;
using System.Collections.Generic;
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
	public partial class ShareTabAControl : ChildTabBaseControl
	{
		private ShareTabAInfo CustomTabInfo => (ShareTabAInfo)TabInfo;

		public ShareTabAControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			textEditTabASubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabASubheader1.Properties.NullText;
			textEditTabASubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabASubheader3.Properties.NullText;
			textEditTabASubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? textEditTabASubheader4.Properties.NullText;
			textEditTabASubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? textEditTabASubheader5.Properties.NullText;
			textEditTabASubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? textEditTabASubheader6.Properties.NullText;
			textEditTabASubheader7.Properties.NullText = CustomTabInfo.SubHeader7Placeholder ?? textEditTabASubheader7.Properties.NullText;

			comboBoxEditTabACombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo1.Properties.NullText;
			comboBoxEditTabACombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo2.Properties.NullText;
			comboBoxEditTabACombo3.Properties.Items.AddRange(CustomTabInfo.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo3.Properties.NullText =
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo3.Properties.NullText;
			comboBoxEditTabACombo4.Properties.Items.AddRange(CustomTabInfo.Combo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo4.Properties.NullText =
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabACombo4.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ShareState.TabA.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ShareState.TabA.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ShareState.TabA.Clipart3);

			checkEditTabAGroup1.Checked = SlideContainer.EditedContent.ShareState.TabA.Group1Toggle;
			checkEditTabAGroup2.Checked = SlideContainer.EditedContent.ShareState.TabA.Group2Toggle;
			checkEditTabAGroup3.Checked = SlideContainer.EditedContent.ShareState.TabA.Group3Toggle;
			checkEditTabAGroup4.Checked = SlideContainer.EditedContent.ShareState.TabA.Group4Toggle;
			checkEditTabASubheader3.Checked = SlideContainer.EditedContent.ShareState.TabA.Subheader3Toggle;
			checkEditTabASubheader5.Checked = SlideContainer.EditedContent.ShareState.TabA.Subheader5Toggle;
			checkEditTabAFormula1.Checked = SlideContainer.EditedContent.ShareState.TabA.Formula1Toggle;
			checkEditTabAFormula2.Checked = SlideContainer.EditedContent.ShareState.TabA.Formula2Toggle;

			comboBoxEditTabACombo1.EditValue = SlideContainer.EditedContent.ShareState.TabA.Combo1 ??
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo2.EditValue = SlideContainer.EditedContent.ShareState.TabA.Combo2 ??
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo3.EditValue = SlideContainer.EditedContent.ShareState.TabA.Combo3 ??
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo4.EditValue = SlideContainer.EditedContent.ShareState.TabA.Combo4 ??
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabASubheader1.EditValue = SlideContainer.EditedContent.ShareState.TabA.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			spinEditTabASubheader2.EditValue = SlideContainer.EditedContent.ShareState.TabA.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = SlideContainer.EditedContent.ShareState.TabA.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = SlideContainer.EditedContent.ShareState.TabA.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			textEditTabASubheader5.EditValue = SlideContainer.EditedContent.ShareState.TabA.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = SlideContainer.EditedContent.ShareState.TabA.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = SlideContainer.EditedContent.ShareState.TabA.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabAFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ShareState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabA.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ShareState.TabA.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ShareState.TabA.Group1Toggle = checkEditTabAGroup1.Checked;
			SlideContainer.EditedContent.ShareState.TabA.Group2Toggle = checkEditTabAGroup2.Checked;
			SlideContainer.EditedContent.ShareState.TabA.Group3Toggle = checkEditTabAGroup3.Checked;
			SlideContainer.EditedContent.ShareState.TabA.Group4Toggle = checkEditTabAGroup4.Checked;
			SlideContainer.EditedContent.ShareState.TabA.Subheader3Toggle = checkEditTabASubheader3.Checked;
			SlideContainer.EditedContent.ShareState.TabA.Subheader5Toggle = checkEditTabASubheader5.Checked;
			SlideContainer.EditedContent.ShareState.TabA.Formula1Toggle = checkEditTabAFormula1.Checked;
			SlideContainer.EditedContent.ShareState.TabA.Formula2Toggle = checkEditTabAFormula2.Checked;

			SlideContainer.EditedContent.ShareState.TabA.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo1.EditValue ?
				comboBoxEditTabACombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo2.EditValue ?
				comboBoxEditTabACombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo3.EditValue ?
				comboBoxEditTabACombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabACombo4.EditValue ?
				comboBoxEditTabACombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo4.EditValue as String } :
				null;

			SlideContainer.EditedContent.ShareState.TabA.Subheader1 = textEditTabASubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabASubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Subheader2 = (decimal?)spinEditTabASubheader2.EditValue != CustomTabInfo.SubHeader2DefaultValue ?
				(decimal?)spinEditTabASubheader2.EditValue :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Subheader3 = textEditTabASubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabASubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Subheader4 = textEditTabASubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue ?
				textEditTabASubheader4.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Subheader5 = textEditTabASubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue ?
				textEditTabASubheader5.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Subheader6 = textEditTabASubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue ?
				textEditTabASubheader6.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ShareState.TabA.Subheader7 = textEditTabASubheader7.EditValue as String != CustomTabInfo.SubHeader7DefaultValue ?
				textEditTabASubheader7.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.ShareState.TabA.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ShareState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.ShareState.TabA.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ShareState.TabA.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		#region Event Handlers

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
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
		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarShareFile("CP05A-1.pptx");
			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ShareState.TabA.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP05ACLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ShareState.TabA.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP05ACLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ShareState.TabA.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP05ACLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ShareState.TabA.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP05AHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ShareState.TabA.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				itemParts.Add((SlideContainer.EditedContent.ShareState.TabA.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue)?.ToString("$#,##0"));
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Combo1?.Value ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault)?.Value);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ShareState.TabA.Subheader3Toggle)
					textDataItems.Add("CP05AFormulaPhrase2".ToUpper(), String.Format("Source: {0}", SlideContainer.EditedContent.ShareState.TabA.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue));
			}

			if (SlideContainer.EditedContent.ShareState.TabA.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Combo2?.Value ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Combo3?.Value ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault)?.Value);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ShareState.TabA.Subheader5Toggle)
					textDataItems.Add("CP05AFormulaPhrase4".ToUpper(), String.Format("Source: {0}", SlideContainer.EditedContent.ShareState.TabA.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue));
			}

			if (SlideContainer.EditedContent.ShareState.TabA.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);
				if (SlideContainer.EditedContent.ShareState.TabA.Formula1Toggle)
					itemParts.Add(simpleLabelItemTabAFormula1.CustomizationFormText);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (SlideContainer.EditedContent.ShareState.TabA.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Combo4?.Value ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault)?.Value);
				itemParts.Add(SlideContainer.EditedContent.ShareState.TabA.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP05AFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (SlideContainer.EditedContent.ShareState.TabA.Formula2Toggle)
					textDataItems.Add("CP05AFormulaPhrase7".ToUpper(), simpleLabelItemTabAFormula2.CustomizationFormText);
			}

			return textDataItems;
		}
		#endregion
	}
}
