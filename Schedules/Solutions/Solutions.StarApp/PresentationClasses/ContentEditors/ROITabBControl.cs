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

			pictureEditTabBClipart1.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart3Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
			});

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabBClipart1.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;
			pictureEditTabBClipart3.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart3 ??
				pictureEditTabBClipart3.Image;

			checkEditTabBGroup1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group1Toggle;
			checkEditTabBGroup2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group2Toggle;
			checkEditTabBGroup3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group3Toggle;
			checkEditTabBGroup4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group4Toggle;
			checkEditTabBGroup5.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group5Toggle;
			checkEditTabBGroup6.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group6Toggle;
			checkEditTabBGroup7.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group7Toggle;
			checkEditTabBGroup8.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group8Toggle;
			checkEditTabBGroup9.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group9Toggle;
			checkEditTabBGroup10.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group10Toggle;
			checkEditTabBGroup11.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group11Toggle;
			checkEditTabBSubheader24.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle;
			checkEditTabBSubheader25.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle;

			textEditTabBSubheader1.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader1 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader2 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader3 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3DefaultValue;
			textEditTabBSubheader4.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader4 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4DefaultValue;
			textEditTabBSubheader5.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader5 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader5DefaultValue;
			textEditTabBSubheader6.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader6 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader7 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7DefaultValue;
			textEditTabBSubheader8.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader8 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader8DefaultValue;
			textEditTabBSubheader9.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader9 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9DefaultValue;
			textEditTabBSubheader10.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader10 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10DefaultValue;
			textEditTabBSubheader11.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader11 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader11DefaultValue;
			textEditTabBSubheader12.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader12 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12DefaultValue;
			textEditTabBSubheader13.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader13 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13DefaultValue;
			textEditTabBSubheader14.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader14 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14DefaultValue;
			textEditTabBSubheader15.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader15 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15DefaultValue;
			textEditTabBSubheader16.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader16 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16DefaultValue;
			textEditTabBSubheader17.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader17 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader17DefaultValue;
			textEditTabBSubheader18.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader18 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18DefaultValue;
			textEditTabBSubheader19.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader19 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19DefaultValue;
			textEditTabBSubheader20.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader20 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20DefaultValue;
			textEditTabBSubheader21.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader21 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21DefaultValue;
			textEditTabBSubheader22.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader22 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22DefaultValue;
			textEditTabBSubheader23.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader23 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23DefaultValue;
			textEditTabBSubheader24.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader24DefaultValue;
			textEditTabBSubheader25.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabBFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart1 = pictureEditTabBClipart1.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart1Image ?
				pictureEditTabBClipart1.Image :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart2 = pictureEditTabBClipart2.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart2Image ?
				pictureEditTabBClipart2.Image :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart3 = pictureEditTabBClipart3.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart3Image ?
				pictureEditTabBClipart3.Image :
				null;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group1Toggle = checkEditTabBGroup1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group2Toggle = checkEditTabBGroup2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group3Toggle = checkEditTabBGroup3.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group4Toggle = checkEditTabBGroup4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group5Toggle = checkEditTabBGroup5.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group6Toggle = checkEditTabBGroup6.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group7Toggle = checkEditTabBGroup7.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group8Toggle = checkEditTabBGroup8.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group9Toggle = checkEditTabBGroup9.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group10Toggle = checkEditTabBGroup10.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group11Toggle = checkEditTabBGroup11.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle = checkEditTabBSubheader24.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle = checkEditTabBSubheader25.Checked;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader2DefaultValue ?
				textEditTabBSubheader2.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3DefaultValue ?
				textEditTabBSubheader3.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader4 = textEditTabBSubheader4.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4DefaultValue ?
				textEditTabBSubheader4.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader5 = textEditTabBSubheader5.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader5DefaultValue ?
				textEditTabBSubheader5.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader6 = textEditTabBSubheader6.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6DefaultValue ?
				textEditTabBSubheader6.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader7 = textEditTabBSubheader7.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7DefaultValue ?
				textEditTabBSubheader7.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader8 = textEditTabBSubheader8.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader8DefaultValue ?
				textEditTabBSubheader8.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader9 = textEditTabBSubheader9.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9DefaultValue ?
				textEditTabBSubheader9.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader10 = textEditTabBSubheader10.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10DefaultValue ?
				textEditTabBSubheader10.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader11 = textEditTabBSubheader11.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader11DefaultValue ?
				textEditTabBSubheader11.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader12 = textEditTabBSubheader12.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12DefaultValue ?
				textEditTabBSubheader12.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader13 = textEditTabBSubheader13.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13DefaultValue ?
				textEditTabBSubheader13.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader14 = textEditTabBSubheader14.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14DefaultValue ?
				textEditTabBSubheader14.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader15 = textEditTabBSubheader15.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15DefaultValue ?
				textEditTabBSubheader15.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader16 = textEditTabBSubheader16.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16DefaultValue ?
				textEditTabBSubheader16.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader17 = textEditTabBSubheader17.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader17DefaultValue ?
				textEditTabBSubheader17.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader18 = textEditTabBSubheader18.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18DefaultValue ?
				textEditTabBSubheader18.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader19 = textEditTabBSubheader19.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19DefaultValue ?
				textEditTabBSubheader19.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader20 = textEditTabBSubheader20.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20DefaultValue ?
				textEditTabBSubheader20.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader21 = textEditTabBSubheader21.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21DefaultValue ?
				textEditTabBSubheader21.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader22 = textEditTabBSubheader22.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22DefaultValue ?
				textEditTabBSubheader22.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader23 = textEditTabBSubheader23.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23DefaultValue ?
				textEditTabBSubheader23.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24 = textEditTabBSubheader24.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader24DefaultValue ?
				textEditTabBSubheader24.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25 = textEditTabBSubheader25.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25DefaultValue ?
				textEditTabBSubheader25.EditValue as String :
				null;

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ROIContentContainer.RaiseDataChanged();
		}

		private void OnTabBGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup1Inner.Enabled = checkEditTabBGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup2Inner.Enabled = checkEditTabBGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup3Inner.Enabled = checkEditTabBGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup4Inner.Enabled = checkEditTabBGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup5Inner.Enabled = checkEditTabBGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup6Inner.Enabled = checkEditTabBGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup7Inner.Enabled = checkEditTabBGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup8Inner.Enabled = checkEditTabBGroup8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup9CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup9Inner.Enabled = checkEditTabBGroup9.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup10Inner.Enabled = checkEditTabBGroup10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup11CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup11Inner.Enabled = checkEditTabBGroup11.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBSubheader24CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader24Value.Enabled = checkEditTabBSubheader24.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBSubheader25CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader25Value.Enabled = checkEditTabBSubheader25.Checked;
			OnEditValueChanged(sender, e);
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
