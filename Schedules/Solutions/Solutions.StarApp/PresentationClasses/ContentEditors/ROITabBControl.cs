using System;
using System.Globalization;
using System.Windows.Forms;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabBControl : ROITabBaseControl
	{
		public ROITabBControl(ROIControl roiContentContainer) : base(roiContentContainer)
		{
			InitializeComponent();

			textEditTabBSubheader1.EnableSelectAll();
			textEditTabBSubheader2.EnableSelectAll();
			textEditTabBSubheader3.EnableSelectAll();
			textEditTabBSubheader4.EnableSelectAll();
			textEditTabBSubheader5.EnableSelectAll();
			textEditTabBSubheader6.EnableSelectAll();
			textEditTabBSubheader7.EnableSelectAll();
			textEditTabBSubheader8.EnableSelectAll();
			textEditTabBSubheader9.EnableSelectAll();
			textEditTabBSubheader10.EnableSelectAll();
			textEditTabBSubheader11.EnableSelectAll();
			textEditTabBSubheader12.EnableSelectAll();
			textEditTabBSubheader13.EnableSelectAll();
			textEditTabBSubheader14.EnableSelectAll();
			textEditTabBSubheader15.EnableSelectAll();
			textEditTabBSubheader16.EnableSelectAll();
			textEditTabBSubheader17.EnableSelectAll();
			textEditTabBSubheader18.EnableSelectAll();
			textEditTabBSubheader19.EnableSelectAll();
			textEditTabBSubheader20.EnableSelectAll();
			textEditTabBSubheader21.EnableSelectAll();
			textEditTabBSubheader22.EnableSelectAll();
			textEditTabBSubheader23.EnableSelectAll();
			textEditTabBSubheader24.EnableSelectAll();
			textEditTabBSubheader25.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabBClipart1.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = ROIContentContainer.SlideContainer.StarInfo.Tab5SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart3Configuration.Alignment;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			textEditTabBSubheader1.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3DefaultValue;
			textEditTabBSubheader4.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4DefaultValue;
			textEditTabBSubheader5.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader5DefaultValue;
			textEditTabBSubheader6.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7DefaultValue;
			textEditTabBSubheader8.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader8DefaultValue;
			textEditTabBSubheader9.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9DefaultValue;
			textEditTabBSubheader10.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10DefaultValue;
			textEditTabBSubheader11.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader11DefaultValue;
			textEditTabBSubheader12.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12DefaultValue;
			textEditTabBSubheader13.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13DefaultValue;
			textEditTabBSubheader14.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14DefaultValue;
			textEditTabBSubheader15.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15DefaultValue;
			textEditTabBSubheader16.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16DefaultValue;
			textEditTabBSubheader17.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader17DefaultValue;
			textEditTabBSubheader18.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18DefaultValue;
			textEditTabBSubheader19.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19DefaultValue;
			textEditTabBSubheader20.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20DefaultValue;
			textEditTabBSubheader21.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21DefaultValue;
			textEditTabBSubheader22.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22DefaultValue;
			textEditTabBSubheader23.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23DefaultValue;
			textEditTabBSubheader24.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader24DefaultValue;
			textEditTabBSubheader25.EditValue = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25DefaultValue;
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
			ROIContentContainer.SlideContainer.RaiseDataChanged();
		}

		private void OnTabBGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup1Inner.Enabled = checkEditTabBGroup1.Checked;
		}

		private void OnTabBGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup2Inner.Enabled = checkEditTabBGroup2.Checked;
		}

		private void OnTabBGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup3Inner.Enabled = checkEditTabBGroup3.Checked;
		}

		private void OnTabBGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup4Inner.Enabled = checkEditTabBGroup4.Checked;
		}

		private void OnTabBGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup5Inner.Enabled = checkEditTabBGroup5.Checked;
		}

		private void OnTabBGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup6Inner.Enabled = checkEditTabBGroup6.Checked;
		}

		private void OnTabBGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup7Inner.Enabled = checkEditTabBGroup7.Checked;
		}

		private void OnTabBGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup8Inner.Enabled = checkEditTabBGroup8.Checked;
		}

		private void OnTabBGroup9CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup9Inner.Enabled = checkEditTabBGroup9.Checked;
		}

		private void OnTabBGroup10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup10Inner.Enabled = checkEditTabBGroup10.Checked;
		}

		private void OnTabBGroup11CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup11Inner.Enabled = checkEditTabBGroup11.Checked;
		}

		private void OnTabBSubheader24CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader24Value.Enabled = checkEditTabBSubheader24.Checked;
		}

		private void OnTabBSubheader25CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader25Value.Enabled = checkEditTabBSubheader25.Checked;
		}

		private void OnTabBFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var avgValue = 0.0;
			try
			{
				avgValue = Double.Parse((textEditTabBSubheader2.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var highValue = 0.0;
			try
			{
				highValue = Double.Parse((textEditTabBSubheader5.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var callsCount = 0.0;
			try
			{
				callsCount = Double.Parse((textEditTabBSubheader8.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var avgPercent = 0.0;
			try
			{
				avgPercent = Double.Parse((textEditTabBSubheader11.EditValue as String)?.Trim()?.Replace("%", "") ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var highPercent = 0.0;
			try
			{
				highPercent = Double.Parse((textEditTabBSubheader17.EditValue as String)?.Trim()?.Replace("%", "") ?? "0",
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var investmentValue = 1.0;
			try
			{
				investmentValue = Double.Parse((textEditTabBSubheader24.EditValue as String)?.Trim() ?? "1",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var avgFormula1Value = Math.Ceiling(callsCount * avgPercent / 100);
			var avgFormula2Value = avgValue * avgFormula1Value;


			var highFormula1Value = Math.Ceiling(callsCount * highPercent / 100);
			var highFormula2Value = highValue * highFormula1Value;

			var totalValue = avgFormula2Value + highFormula2Value;

			var formula3Value = Math.Ceiling(totalValue / investmentValue);
			formula3Value = formula3Value < totalValue ? formula3Value : 1.0;

			simpleLabelItemTabBFormula1.CustomizationFormText = avgFormula1Value.ToString();
			simpleLabelItemTabBFormula1.Text = String.Format("<b>{0:#,##0}</b>", avgFormula1Value);
			simpleLabelItemTabBFormula2.CustomizationFormText = avgFormula2Value.ToString();
			simpleLabelItemTabBFormula2.Text = String.Format("<b>{0:$#,##0}</b>", avgFormula2Value);

			simpleLabelItemTabBFormula3.CustomizationFormText = highFormula1Value.ToString();
			simpleLabelItemTabBFormula3.Text = String.Format("<b>{0:#,##0}</b>", highFormula1Value);
			simpleLabelItemTabBFormula4.CustomizationFormText = highFormula2Value.ToString();
			simpleLabelItemTabBFormula4.Text = String.Format("<b>{0:$#,##0}</b>", highFormula2Value);

			simpleLabelItemTabBFormula5.CustomizationFormText = totalValue.ToString();
			simpleLabelItemTabBFormula5.Text = String.Format("<b>{0:$#,##0}</b>", totalValue);

			layoutControlItemTabBSubheader25Value.CustomizationFormText = formula3Value.ToString();
			layoutControlItemTabBSubheader25Value.Text = String.Format("= <b>{0:#,##0} : 1</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
	}
}
