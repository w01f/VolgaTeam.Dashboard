using System;
using System.Globalization;
using System.Windows.Forms;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabDControl : ROITabBaseControl
	{
		public ROITabDControl(ROIControl roiContentContainer) : base(roiContentContainer)
		{
			InitializeComponent();

			textEditTabDSubheader1.EnableSelectAll();
			textEditTabDSubheader2.EnableSelectAll();
			textEditTabDSubheader3.EnableSelectAll();
			textEditTabDSubheader4.EnableSelectAll();
			textEditTabDSubheader5.EnableSelectAll();
			textEditTabDSubheader6.EnableSelectAll();
			textEditTabDSubheader7.EnableSelectAll();
			textEditTabDSubheader8.EnableSelectAll();
			textEditTabDSubheader9.EnableSelectAll();
			textEditTabDSubheader10.EnableSelectAll();
			textEditTabDSubheader11.EnableSelectAll();
			textEditTabDSubheader12.EnableSelectAll();
			textEditTabDSubheader13.EnableSelectAll();
			textEditTabDSubheader14.EnableSelectAll();
			textEditTabDSubheader15.EnableSelectAll();
			textEditTabDSubheader16.EnableSelectAll();
			textEditTabDSubheader17.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabDClipart1.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubDClipart1Image;
			pictureEditTabDClipart1.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDClipart1Configuration.Alignment;
			pictureEditTabDClipart2.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubDClipart2Image;
			pictureEditTabDClipart2.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDClipart2Configuration.Alignment;
			pictureEditTabDClipart3.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubDClipart3Image;
			pictureEditTabDClipart3.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDClipart3Configuration.Alignment;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			textEditTabDSubheader1.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader1DefaultValue;
			textEditTabDSubheader2.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader3DefaultValue;
			textEditTabDSubheader4.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader5DefaultValue;
			textEditTabDSubheader6.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader7DefaultValue;
			textEditTabDSubheader8.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader9DefaultValue;
			textEditTabDSubheader10.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader10DefaultValue;
			textEditTabDSubheader11.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader11DefaultValue;
			textEditTabDSubheader12.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader12DefaultValue;
			textEditTabDSubheader13.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader13DefaultValue;
			textEditTabDSubheader14.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader14DefaultValue;
			textEditTabDSubheader15.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader15DefaultValue;
			textEditTabDSubheader16.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader16DefaultValue;
			textEditTabDSubheader17.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader17DefaultValue;
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
			ROIContentContainer.SlideContainer.RaiseDataChanged();
		}

		private void OnTabDGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup1Inner.Enabled = checkEditTabDGroup1.Checked;
		}

		private void OnTabDSubheader2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader2Value.Enabled = checkEditTabDSubheader2.Checked;
		}

		private void OnTabDGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup2Inner.Enabled = checkEditTabDGroup2.Checked;
		}

		private void OnTabDSubheader4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader4Value.Enabled = checkEditTabDSubheader4.Checked;
		}

		private void OnTabDGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup3Inner.Enabled = checkEditTabDGroup3.Checked;
		}

		private void OnTabDSubheader6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader6Value.Enabled = checkEditTabDSubheader6.Checked;
		}

		private void OnTabDGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup4Inner.Enabled = checkEditTabDGroup4.Checked;
		}

		private void OnTabDSubheader8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader8Value.Enabled = checkEditTabDSubheader8.Checked;
		}

		private void OnTabDGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup5Inner.Enabled = checkEditTabDGroup5.Checked;
		}

		private void OnTabDSubheader10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader10Value.Enabled = checkEditTabDSubheader10.Checked;
		}

		private void OnTabDGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup6Inner.Enabled = checkEditTabDGroup6.Checked;
		}

		private void OnTabDSubheader12CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader12Value.Enabled = checkEditTabDSubheader12.Checked;
		}

		private void OnTabDGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup7Inner.Enabled = checkEditTabDGroup7.Checked;
		}

		private void OnTabDFormula1CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula1.Enabled = checkEditTabDFormula1.Checked;
		}

		private void OnTabDGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup8Inner.Enabled = checkEditTabDGroup8.Checked;
		}

		private void OnTabDSubheader15CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader15Value.Enabled = checkEditTabDSubheader15.Checked;
		}

		private void OnTabDGroup9CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup9Inner.Enabled = checkEditTabDGroup9.Checked;
		}

		private void OnTabDFormula2CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula2.Enabled = checkEditTabDFormula2.Checked;
		}

		private void OnTabDGroup10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup10Inner.Enabled = checkEditTabDGroup10.Checked;
		}

		private void OnTabDFormula3CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula3.Enabled = checkEditTabDFormula3.Checked;
		}

		private void OnTabDFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var monthlyInvestmentValue = 0.0;
			try
			{
				monthlyInvestmentValue = Double.Parse((textEditTabDSubheader2.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var avgSaleValue = 0.0;
			try
			{
				avgSaleValue = Double.Parse((textEditTabDSubheader4.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var salePercent = 0.0;
			try
			{
				salePercent = Double.Parse((textEditTabDSubheader6.EditValue as String)?.Trim()?.Replace("%", "") ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var closingPercent = 0.0;
			try
			{
				closingPercent = Double.Parse((textEditTabDSubheader8.EditValue as String)?.Trim()?.Replace("%", "") ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var monthlyGoal = 0.0;
			try
			{
				monthlyGoal = Double.Parse((textEditTabDSubheader15.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var formula1Value = avgSaleValue > 0 && salePercent > 0 ? Math.Ceiling(monthlyInvestmentValue / (avgSaleValue * salePercent / 100)) : 0;
			var formula2Value = closingPercent > 0 ? Math.Ceiling(formula1Value / (closingPercent / 100)) : 0;
			var formula3Value = closingPercent > 0 ? Math.Ceiling(monthlyGoal / (closingPercent / 100)) : 0;

			simpleLabelItemTabDFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabDFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabDFormula2.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabDFormula2.Text = String.Format("<b>{0:#,##0}</b>", formula2Value);
			simpleLabelItemTabDFormula3.CustomizationFormText = formula3Value.ToString();
			simpleLabelItemTabDFormula3.Text = String.Format("<b>{0:#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
	}
}
