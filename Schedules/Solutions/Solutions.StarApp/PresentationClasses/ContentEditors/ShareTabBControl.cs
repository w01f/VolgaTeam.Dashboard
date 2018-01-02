using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabBControl : ShareTabBaseControl
	{
		public ShareTabBControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll();
			comboBoxEditTabBCombo2.EnableSelectAll();
			textEditTabBSubheader2.EnableSelectAll();
			textEditTabBSubheader3.EnableSelectAll();
			textEditTabBSubheader4.EnableSelectAll();
			textEditTabBSubheader5.EnableSelectAll();
			textEditTabBSubheader6.EnableSelectAll();
			textEditTabBSubheader7.EnableSelectAll();
			textEditTabBSubheader8.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabBClipart1.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBClipart3Configuration.Alignment;
			Application.DoEvents();

			comboBoxEditTabBCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items);
			comboBoxEditTabBCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditTabBCombo1.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsDefault);

			textEditTabBSubheader1.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader3DefaultValue;
			textEditTabBSubheader4.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader4DefaultValue;
			textEditTabBSubheader5.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader5DefaultValue;
			textEditTabBSubheader6.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader7DefaultValue;
			textEditTabBSubheader8.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader8DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabBFormulaSourceEditValueChanged(null, EventArgs.Empty);
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

		private void OnTabBGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup1Inner.Enabled = checkEditTabBGroup1.Checked;
		}

		private void OnTabBSubheader2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader2Value.Enabled = checkEditTabBSubheader2.Checked;
		}

		private void OnTabBGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup2Inner.Enabled = checkEditTabBGroup2.Checked;
		}

		private void OnTabBGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup3Inner.Enabled = checkEditTabBGroup3.Checked;
		}

		private void OnTabBSubheader7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader7Value.Enabled = checkEditTabBSubheader7.Checked;
		}

		private void OnTabBGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup4Inner.Enabled = checkEditTabBGroup4.Checked;
		}

		private void OnTabBGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup5Inner.Enabled = checkEditTabBGroup5.Checked;
		}

		private void OnTabBFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var sourceValue = 0.0;
			try
			{
				sourceValue = Double.Parse((comboBoxEditTabBCombo1.EditValue as ComboboxItem)?.Value ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var percent = 0.0;
			try
			{
				percent = Double.Parse((textEditTabBSubheader4.EditValue as String)?.Trim()?.Replace("%", "") ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var costValue = 0.0;
			try
			{
				costValue = Double.Parse((textEditTabBSubheader6.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharepointFactor = (comboBoxEditTabBCombo2.EditValue as ComboboxItem)?.Value ?? String.Empty;

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

			simpleLabelItemTabBFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabBFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabBFormula2.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabBFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabBFormula3.CustomizationFormText = formula3Value.ToString();
			simpleLabelItemTabBFormula3.Text = String.Format("<b>{0:$#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
	}
}
