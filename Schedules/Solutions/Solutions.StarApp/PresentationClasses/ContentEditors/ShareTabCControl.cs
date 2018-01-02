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

			comboBoxEditTabCCombo1.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo2.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo3.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo4.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo5.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo6.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.FirstOrDefault(item => item.IsDefault);
			Application.DoEvents();

			textEditTabCSubheader1.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader1DefaultValue;
			textEditTabCSubheader2.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader3DefaultValue;
			textEditTabCSubheader4.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader4DefaultValue;

			_allowToSave = true;

			OnTabCFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			ShareContentContainer.SlideContainer.RaiseDataChanged();
		}

		private void OnTabCGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup1Inner.Enabled = checkEditTabCGroup1.Checked;
		}

		private void OnTabCSubheader3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader3Value.Enabled = checkEditTabCSubheader3.Checked;
		}

		private void OnTabCGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup2Inner.Enabled = checkEditTabCGroup2.Checked;
		}

		private void OnTabCGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup3Inner.Enabled = checkEditTabCGroup3.Checked;
		}

		private void OnTabCGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup4Inner.Enabled = checkEditTabCGroup4.Checked;
		}

		private void OnTabCGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup5Inner.Enabled = checkEditTabCGroup5.Checked;
		}

		private void OnTabCGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup6Inner.Enabled = checkEditTabCGroup6.Checked;
		}

		private void OnTabCFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var multiplierText = (comboBoxEditTabCCombo1.EditValue as ComboboxItem)?.Value ?? String.Empty;

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
					Double.Parse((comboBoxEditTabCCombo4.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharePercent = 0.0;
			try
			{
				sharePercent =
					Double.Parse((comboBoxEditTabCCombo6.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
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
				(comboBoxEditTabCCombo1.EditValue as ComboboxItem)?.Value?.Trim(),
				(comboBoxEditTabCCombo4.EditValue as ComboboxItem)?.Value?.Trim(),
				formula1Value);
			simpleLabelItemTabCFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula1Value);
			simpleLabelItemTabCFormula3.Text = String.Format("Total Estimated Revenue in {0}",
				(comboBoxEditTabCCombo3.EditValue as ComboboxItem)?.Value?.Trim());
			simpleLabelItemTabCFormula4.Text = String.Format("Share Growth in {0}",
				(comboBoxEditTabCCombo3.EditValue as ComboboxItem)?.Value?.Trim());
			simpleLabelItemTabCFormula5.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabCFormula5.Text = String.Format("<b>{0:$#,##0}</b> (annually)", formula2Value);

			OnEditValueChanged(sender, e);
		}
	}
}
