using System;
using System.Globalization;
using System.Windows.Forms;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabCControl : ROITabBaseControl
	{
		public ROITabCControl(ROIControl roiContentContainer) : base(roiContentContainer)
		{
			InitializeComponent();

			textEditTabCSubheader1.EnableSelectAll();
			textEditTabCSubheader2.EnableSelectAll();
			textEditTabCSubheader3.EnableSelectAll();
			textEditTabCSubheader4.EnableSelectAll();
			textEditTabCSubheader5.EnableSelectAll();
			textEditTabCSubheader6.EnableSelectAll();
			textEditTabCSubheader7.EnableSelectAll();
			textEditTabCSubheader8.EnableSelectAll();
			textEditTabCSubheader9.EnableSelectAll();
			textEditTabCSubheader10.EnableSelectAll();
			textEditTabCSubheader11.EnableSelectAll();
			textEditTabCSubheader12.EnableSelectAll();
			textEditTabCSubheader13.EnableSelectAll();
			textEditTabCSubheader14.EnableSelectAll();
			textEditTabCSubheader15.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabCClipart1.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCClipart3Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabCClipart1,
				pictureEditTabCClipart2,
			});

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			textEditTabCSubheader1.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader1DefaultValue;
			textEditTabCSubheader2.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader2DefaultValue;
			textEditTabCSubheader3.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader3DefaultValue;
			textEditTabCSubheader4.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader4DefaultValue;
			textEditTabCSubheader5.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader5DefaultValue;
			textEditTabCSubheader6.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader6DefaultValue;
			textEditTabCSubheader7.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader7DefaultValue;
			textEditTabCSubheader8.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader8DefaultValue;
			textEditTabCSubheader9.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader9DefaultValue;
			textEditTabCSubheader10.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader10DefaultValue;
			textEditTabCSubheader11.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader11DefaultValue;
			textEditTabCSubheader12.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader12DefaultValue;
			textEditTabCSubheader13.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader13DefaultValue;
			textEditTabCSubheader14.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader14DefaultValue;
			textEditTabCSubheader15.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader15DefaultValue;
			Application.DoEvents();

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
			ROIContentContainer.SlideContainer.RaiseDataChanged();
		}

		private void OnTabCGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup1Inner.Enabled = checkEditTabCGroup1.Checked;
		}

		private void OnTabCSubheader2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader2Value.Enabled = checkEditTabCSubheader2.Checked;
		}

		private void OnTabCGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup2Inner.Enabled = checkEditTabCGroup2.Checked;
		}

		private void OnTabCSubheader4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader4Value.Enabled = checkEditTabCSubheader4.Checked;
		}

		private void OnTabCSubheader5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader5Value.Enabled = checkEditTabCSubheader5.Checked;
		}

		private void OnTabCGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup3Inner.Enabled = checkEditTabCGroup3.Checked;
		}

		private void OnTabCSubheader7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader7Value.Enabled = checkEditTabCSubheader7.Checked;
		}

		private void OnTabCSubheader8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader8Value.Enabled = checkEditTabCSubheader8.Checked;
		}

		private void OnTabCGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup4Inner.Enabled = checkEditTabCGroup4.Checked;
		}

		private void OnTabCFormula1CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabCFormula1.Enabled = checkEditTabCFormula1.Checked;
		}

		private void OnTabCSubheader10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader10Value.Enabled = checkEditTabCSubheader10.Checked;
		}

		private void OnTabCGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup5Inner.Enabled = checkEditTabCGroup5.Checked;
		}

		private void OnTabCFormula2CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabCFormula2.Enabled = checkEditTabCFormula2.Checked;
		}

		private void OnTabCGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup6Inner.Enabled = checkEditTabCGroup6.Checked;
		}

		private void OnTabCGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup7Inner.Enabled = checkEditTabCGroup7.Checked;
		}

		private void OnTabCSubheader14CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader14Value.Enabled = checkEditTabCSubheader14.Checked;
		}

		private void OnTabCGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup8Inner.Enabled = checkEditTabCGroup8.Checked;
		}

		private void OnTabCFormula3CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabCFormula3.Enabled = checkEditTabCFormula3.Checked;
		}

		private void OnTabCFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var sourceValue = 0.0;
			try
			{
				sourceValue = Double.Parse((textEditTabCSubheader2.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var callsCount = 0.0;
			try
			{
				callsCount = Double.Parse((textEditTabCSubheader4.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var percent = 0.0;
			try
			{
				percent = Double.Parse((textEditTabCSubheader7.EditValue as String)?.Trim()?.Replace("%", "") ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var investmentValue = 1.0;
			try
			{
				investmentValue = Double.Parse((textEditTabCSubheader13.EditValue as String)?.Trim() ?? "1",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var formula1Value = Math.Ceiling(callsCount * percent / 100);
			var formula2Value = sourceValue * formula1Value;
			var formula3Value = Math.Ceiling(formula2Value / investmentValue);
			formula3Value = formula3Value < formula2Value ? formula3Value : 1.0;

			simpleLabelItemTabCFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabCFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabCFormula2.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabCFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			simpleLabelItemTabCFormula3.CustomizationFormText = formula3Value.ToString();
			simpleLabelItemTabCFormula3.Text = String.Format("<b>{0:#,##0}   to   1</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
	}
}
