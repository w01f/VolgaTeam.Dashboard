using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabAControl : ShareTabBaseControl
	{
		public ShareTabAControl(ShareControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabACombo1.EnableSelectAll();
			comboBoxEditTabACombo2.EnableSelectAll();
			comboBoxEditTabACombo3.EnableSelectAll();
			comboBoxEditTabACombo4.EnableSelectAll();
			textEditTabASubheader1.EnableSelectAll();
			textEditTabASubheader2.EnableSelectAll();
			textEditTabASubheader3.EnableSelectAll();
			textEditTabASubheader4.EnableSelectAll();
			textEditTabASubheader5.EnableSelectAll();
			textEditTabASubheader6.EnableSelectAll();
			textEditTabASubheader7.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabAClipart1.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart2Configuration.Alignment;
			pictureEditTabAClipart3.Image = ShareContentContainer.SlideContainer.StarInfo.Tab5SubAClipart3Image;
			pictureEditTabAClipart3.Properties.PictureAlignment =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart3Configuration.Alignment;
			Application.DoEvents();

			comboBoxEditTabACombo1.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items);
			comboBoxEditTabACombo2.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items);
			comboBoxEditTabACombo3.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items);
			comboBoxEditTabACombo4.Properties.Items.AddRange(ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditTabACombo1.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo2.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo3.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo4.EditValue =
				ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabASubheader1.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader1DefaultValue;
			textEditTabASubheader2.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader4DefaultValue;
			textEditTabASubheader5.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartASubHeader7DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabAFormulaSourceEditValueChanged(null, EventArgs.Empty);
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

		private void OnTabAGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup1Inner.Enabled = checkEditTabAGroup1.Checked;
		}

		private void OnTabASubheader3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader3Value.Enabled = checkEditTabASubheader3.Checked;
		}

		private void OnTabAGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup2Inner.Enabled = checkEditTabAGroup2.Checked;
		}

		private void OnTabASubheader5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader5Value.Enabled = checkEditTabASubheader5.Checked;
		}

		private void OnTabAGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup3Inner.Enabled = checkEditTabAGroup3.Checked;
		}

		private void OnTabAFormula1CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabAFormula1.Enabled = checkEditTabAFormula1.Checked;
		}

		private void OnTabAGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup4Inner.Enabled = checkEditTabAGroup4.Checked;
		}

		private void OnTabAFormula2CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabAFormula2.Enabled = checkEditTabAFormula2.Checked;
		}

		private void OnTabAFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var multiplierText = (comboBoxEditTabACombo1.EditValue as ComboboxItem)?.Value ?? String.Empty;

			var sourceValue = 0.0;
			try
			{
				sourceValue =
					Double.Parse((textEditTabASubheader2.EditValue as String)?.Trim()?.Replace("$", String.Empty) ?? "0") *
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
				percent = Double.Parse((comboBoxEditTabACombo2.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0");
			}
			catch
			{
			}

			var formula1Value = (Int64)(sourceValue / 100 * percent);
			var sharepointFactor = (comboBoxEditTabACombo4.EditValue as ComboboxItem)?.Value ?? String.Empty;
			var formula2Value = formula1Value / 100 *
								(sharepointFactor.StartsWith("ONE",
									StringComparison.InvariantCultureIgnoreCase)
									? 1
									: (sharepointFactor.StartsWith("TWO",
										StringComparison.InvariantCultureIgnoreCase)
										? 2
										: 3));

			simpleLabelItemTabAFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabAFormula1.Text = String.Format("<b>{0:$#,##0}</b>", formula1Value);

			simpleLabelItemTabAFormula2.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabAFormula2.Text = String.Format("<b>{0:$#,##0}</b>   Annually", formula2Value);

			OnEditValueChanged(sender, e);
		}
	}
}
