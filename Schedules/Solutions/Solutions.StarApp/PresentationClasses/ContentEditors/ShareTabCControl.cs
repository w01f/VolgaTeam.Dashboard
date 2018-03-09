using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabCControl : ShareTabBaseControl
	{
		public ShareTabCControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabCCombo1.EnableSelectAll();
			comboBoxEditTabCCombo2.EnableSelectAll();
			comboBoxEditTabCCombo3.EnableSelectAll();
			comboBoxEditTabCCombo4.EnableSelectAll();
			comboBoxEditTabCCombo5.EnableSelectAll();
			comboBoxEditTabCCombo6.EnableSelectAll();
			textEditTabCSubheader1.EnableSelectAll();
			textEditTabCSubheader2.EnableSelectAll();
			textEditTabCSubheader4.EnableSelectAll();
			memoEditTabCSubheader3.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabCClipart1.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCClipart3Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabCClipart1,
				pictureEditTabCClipart2
			});

			Application.DoEvents();

			comboBoxEditTabCCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items);
			comboBoxEditTabCCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items);
			comboBoxEditTabCCombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items);
			comboBoxEditTabCCombo4.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items);
			comboBoxEditTabCCombo5.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items);
			comboBoxEditTabCCombo6.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabCClipart1.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart1 ??
				pictureEditTabCClipart1.Image;
			pictureEditTabCClipart2.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart2 ??
				pictureEditTabCClipart2.Image;
			pictureEditTabCClipart3.Image = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart3 ??
				pictureEditTabCClipart3.Image;

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

			textEditTabCSubheader1.EditValue = ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader1 ??
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

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart1 = pictureEditTabCClipart1.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart1Image ?
				pictureEditTabCClipart1.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart2 = pictureEditTabCClipart2.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart2Image ?
				pictureEditTabCClipart2.Image :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Clipart3 = pictureEditTabCClipart3.Image != ShareContentContainer.SlideContainer.StarInfo.Tab5SubCClipart3Image ?
				pictureEditTabCClipart3.Image :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group1Toggle = checkEditTabCGroup1.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group2Toggle = checkEditTabCGroup2.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group3Toggle = checkEditTabCGroup3.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group4Toggle = checkEditTabCGroup4.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group5Toggle = checkEditTabCGroup5.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Group6Toggle = checkEditTabCGroup6.Checked;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3Toggle = checkEditTabCSubheader3.Checked;
			
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo1 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
				comboBoxEditTabCCombo1.EditValue as ListDataItem ?? (comboBoxEditTabCCombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabCCombo1.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo2 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo2.EditValue ?
				comboBoxEditTabCCombo2.EditValue as ListDataItem ?? (comboBoxEditTabCCombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabCCombo2.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo3 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo3.EditValue ?
				comboBoxEditTabCCombo3.EditValue as ListDataItem ?? (comboBoxEditTabCCombo3.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabCCombo3.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo4 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo4.EditValue ?
				comboBoxEditTabCCombo4.EditValue as ListDataItem ?? (comboBoxEditTabCCombo4.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabCCombo4.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo5 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo5.EditValue ?
				comboBoxEditTabCCombo5.EditValue as ListDataItem ?? (comboBoxEditTabCCombo5.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabCCombo5.EditValue } : null) :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Combo6 = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo6.EditValue ?
				comboBoxEditTabCCombo6.EditValue as ListDataItem ?? (comboBoxEditTabCCombo6.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabCCombo6.EditValue } : null) :
				null;

			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader1 = textEditTabCSubheader1.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader1DefaultValue ?
				textEditTabCSubheader1.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader2 = textEditTabCSubheader2.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader2DefaultValue ?
				textEditTabCSubheader2.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader3DefaultValue ?
				memoEditTabCSubheader3.EditValue as String :
				null;
			ShareContentContainer.SlideContainer.EditedContent.ShareState.TabC.Subheader4 = textEditTabCSubheader4.EditValue as String != ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader4DefaultValue ?
				textEditTabCSubheader4.EditValue as String :
				null;

			_dataChanged = false;
		}

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
				sourceValue = Double.Parse((textEditTabCSubheader1.EditValue as String)?.Trim() ?? "0",
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

			simpleLabelItemTabCFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabCFormula1.Text = String.Format("{0} {1} x {2} = <b>{3:$#,##0}</b>",
				(textEditTabCSubheader1.EditValue as String)?.Trim(),
				(comboBoxEditTabCCombo1.EditValue as ListDataItem)?.Value?.Trim(),
				(comboBoxEditTabCCombo4.EditValue as ListDataItem)?.Value?.Trim(),
				formula1Value);
			simpleLabelItemTabCFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula1Value);
			simpleLabelItemTabCFormula3.Text = String.Format("Total Estimated Revenue in {0}",
				(comboBoxEditTabCCombo3.EditValue as ListDataItem)?.Value?.Trim());
			simpleLabelItemTabCFormula4.Text = String.Format("Share Growth in {0}",
				(comboBoxEditTabCCombo3.EditValue as ListDataItem)?.Value?.Trim());
			simpleLabelItemTabCFormula5.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabCFormula5.Text = String.Format("<b>{0:$#,##0}</b> (annually)", formula2Value);

			OnEditValueChanged(sender, e);
		}
	}
}
