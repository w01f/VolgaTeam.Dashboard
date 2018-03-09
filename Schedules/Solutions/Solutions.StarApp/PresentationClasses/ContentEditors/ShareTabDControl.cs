using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabDControl : ShareTabBaseControl
	{
		public ShareTabDControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabDCombo1.EnableSelectAll();
			comboBoxEditTabDCombo2.EnableSelectAll();
			comboBoxEditTabDCombo3.EnableSelectAll();
			textEditTabDSubheader2.EnableSelectAll();
			textEditTabDSubheader3.EnableSelectAll();
			textEditTabDSubheader4.EnableSelectAll();
			textEditTabDSubheader5.EnableSelectAll();
			textEditTabDSubheader6.EnableSelectAll();
			textEditTabDSubheader7.EnableSelectAll();
			textEditTabDSubheader8.EnableSelectAll();
			textEditTabDSubheader9.EnableSelectAll();
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
				pictureEditTabDClipart2
			});

			Application.DoEvents();

			comboBoxEditTabDCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items);
			comboBoxEditTabDCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items);
			comboBoxEditTabDCombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items);
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
			textEditTabDSubheader4.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader4 ??
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
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabD.Subheader4 = textEditTabDSubheader4.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader4DefaultValue ?
				textEditTabDSubheader4.EditValue as String :
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

			var costValue = 0.0;
			try
			{
				costValue = Double.Parse((textEditTabDSubheader4.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

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

			simpleLabelItemTabDFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabDFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabDFormula2.Text = String.Format("<b>{0}</b>", (textEditTabDSubheader4.EditValue as String)?.Trim());
			simpleLabelItemTabDFormula3.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabDFormula3.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabDFormula5.CustomizationFormText = formula3Value.ToString();
			simpleLabelItemTabDFormula5.Text = String.Format("<b>{0:$#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
	}
}
