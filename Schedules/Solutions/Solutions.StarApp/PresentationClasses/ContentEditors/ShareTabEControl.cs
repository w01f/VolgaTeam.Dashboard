using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Common;

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

			comboBoxEditTabECombo1.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo2.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo3.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo4.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabESubheader1.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader1DefaultValue;
			textEditTabESubheader2.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader2DefaultValue;
			textEditTabESubheader3.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader3DefaultValue;
			textEditTabESubheader4.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader4DefaultValue;
			textEditTabESubheader5.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader5DefaultValue;
			textEditTabESubheader6.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader6DefaultValue;
			textEditTabESubheader7.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader7DefaultValue;
			textEditTabESubheader8.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader8DefaultValue;
			textEditTabESubheader9.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader9DefaultValue;
			textEditTabESubheader10.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartESubHeader10DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabEFormulaSourceEditValueChanged(null, EventArgs.Empty);
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

		private void OnTabEGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup1Inner.Enabled = checkEditTabEGroup1.Checked;
		}

		private void OnTabESubheader4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabESubheader4Value.Enabled = checkEditTabESubheader4.Checked;
		}

		private void OnTabEGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup2Inner.Enabled = checkEditTabEGroup2.Checked;
		}

		private void OnTabEGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup3Inner.Enabled = checkEditTabEGroup3.Checked;
		}

		private void OnTabEGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup4Inner.Enabled = checkEditTabEGroup4.Checked;
		}

		private void OnTabEGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup5Inner.Enabled = checkEditTabEGroup5.Checked;
		}

		private void OnTabEGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup6Inner.Enabled = checkEditTabEGroup6.Checked;
		}

		private void OnTabEGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabEGroup7Inner.Enabled = checkEditTabEGroup7.Checked;
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
