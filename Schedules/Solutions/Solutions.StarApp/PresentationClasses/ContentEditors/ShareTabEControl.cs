using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Common;
using DevExpress.XtraEditors.ViewInfo;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabEControl : ShareTabBaseControl
	{
		public ShareTabEControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabECombo1.EnableSelectAll();
			comboBoxEditTabECombo2.EnableSelectAll();
			comboBoxEditTabECombo3.EnableSelectAll();
			comboBoxEditTabECombo4.EnableSelectAll();
			textEditTabESubheader2.EnableSelectAll();
			textEditTabESubheader3.EnableSelectAll();
			textEditTabESubheader4.EnableSelectAll();
			textEditTabESubheader5.EnableSelectAll();
			textEditTabESubheader6.EnableSelectAll();
			textEditTabESubheader7.EnableSelectAll();
			textEditTabESubheader8.EnableSelectAll();
			textEditTabESubheader9.EnableSelectAll();
			textEditTabESubheader10.EnableSelectAll();
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
			});

			Application.DoEvents();

			comboBoxEditTabECombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items);
			comboBoxEditTabECombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items);
			comboBoxEditTabECombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items);
			comboBoxEditTabECombo4.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items);
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
			textEditTabESubheader7.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader7 ??
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
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabE.Subheader7 = textEditTabESubheader7.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader7DefaultValue ?
				textEditTabESubheader7.EditValue as String :
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

			var costValue = 0.0;
			try
			{
				costValue = Double.Parse((textEditTabESubheader7.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

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

			simpleLabelItemTabEFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabEFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabEFormula2.Text = String.Format("<b>{0}</b>", (textEditTabESubheader7.EditValue as String)?.Trim());
			simpleLabelItemTabEFormula3.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabEFormula3.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabEFormula5.CustomizationFormText = formula3Value.ToString();
			simpleLabelItemTabEFormula5.Text = String.Format("<b>{0:$#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
	}
}
