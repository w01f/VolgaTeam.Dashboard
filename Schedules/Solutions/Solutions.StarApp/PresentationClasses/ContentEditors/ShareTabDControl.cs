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
			Application.DoEvents();

			comboBoxEditTabDCombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items);
			comboBoxEditTabDCombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items);
			comboBoxEditTabDCombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditTabDCombo1.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo2.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo3.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items.FirstOrDefault(item => item.IsDefault);

			textEditTabDSubheader1.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader1DefaultValue;
			textEditTabDSubheader2.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader3DefaultValue;
			textEditTabDSubheader4.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader5DefaultValue;
			textEditTabDSubheader6.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader7DefaultValue;
			textEditTabDSubheader8.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader9DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabDFormulaSourceEditValueChanged(null, EventArgs.Empty);
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

		private void OnTabDGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup1Inner.Enabled = checkEditTabDGroup1.Checked;
		}

		private void OnTabDGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup2Inner.Enabled = checkEditTabDGroup2.Checked;
		}

		private void OnTabDGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup3Inner.Enabled = checkEditTabDGroup3.Checked;
		}

		private void OnTabDGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup4Inner.Enabled = checkEditTabDGroup4.Checked;
		}

		private void OnTabDGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup5Inner.Enabled = checkEditTabDGroup5.Checked;
		}

		private void OnTabDGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup6Inner.Enabled = checkEditTabDGroup6.Checked;
		}

		private void OnTabDGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup7Inner.Enabled = checkEditTabDGroup7.Checked;
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
					Double.Parse((comboBoxEditTabDCombo1.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharePercent = 0.0;
			try
			{
				sharePercent =
					Double.Parse((comboBoxEditTabDCombo3.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
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
