using System;
using System.Globalization;
using System.Windows.Forms;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabAControl : ROITabBaseControl
	{
		public ROITabAControl(ROIControl roiContentContainer) : base(roiContentContainer)
		{
			InitializeComponent();

			textEditTabASubheader1.EnableSelectAll();
			textEditTabASubheader2.EnableSelectAll();
			textEditTabASubheader3.EnableSelectAll();
			textEditTabASubheader4.EnableSelectAll();
			textEditTabASubheader5.EnableSelectAll();
			textEditTabASubheader6.EnableSelectAll();
			textEditTabASubheader7.EnableSelectAll();
			textEditTabASubheader8.EnableSelectAll();
			textEditTabASubheader9.EnableSelectAll();
			textEditTabASubheader10.EnableSelectAll();
			textEditTabASubheader11.EnableSelectAll();
			textEditTabASubheader12.EnableSelectAll();
			textEditTabASubheader13.EnableSelectAll();
			textEditTabASubheader14.EnableSelectAll();
			textEditTabASubheader15.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabAClipart1.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartAClipart2Configuration.Alignment;
			pictureEditTabAClipart3.Image = ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart3Image;
			pictureEditTabAClipart3.Properties.PictureAlignment =
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartAClipart3Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabAClipart2,
			});

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabAClipart1.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			pictureEditTabAClipart2.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart2 ??
				pictureEditTabAClipart2.Image;
			pictureEditTabAClipart3.Image = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart3 ??
				pictureEditTabAClipart3.Image;

			checkEditTabAGroup1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group1Toggle;
			checkEditTabAGroup2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group2Toggle;
			checkEditTabAGroup3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group3Toggle;
			checkEditTabAGroup4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group4Toggle;
			checkEditTabAGroup5.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group5Toggle;
			checkEditTabAGroup6.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group6Toggle;
			checkEditTabASubheader14.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle;
			checkEditTabASubheader15.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle;

			textEditTabASubheader1.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader1 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader1DefaultValue;
			textEditTabASubheader2.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader2 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader3 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader4 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader4DefaultValue;
			textEditTabASubheader5.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader5 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader6 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader7 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader7DefaultValue;
			textEditTabASubheader8.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader8 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader8DefaultValue;
			textEditTabASubheader9.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader9 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader9DefaultValue;
			textEditTabASubheader10.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader10 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader10DefaultValue;
			textEditTabASubheader11.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader11 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader11DefaultValue;
			textEditTabASubheader12.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader12 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader12DefaultValue;
			textEditTabASubheader13.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader13 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader13DefaultValue;
			textEditTabASubheader14.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader14DefaultValue;
			textEditTabASubheader15.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader15DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabAFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart1 = pictureEditTabAClipart1.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart1Image ?
				pictureEditTabAClipart1.Image :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart2 = pictureEditTabAClipart2.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart2Image ?
				pictureEditTabAClipart2.Image :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Clipart3 = pictureEditTabAClipart3.Image != ROIContentContainer.SlideContainer.StarInfo.Tab6SubAClipart3Image ?
				pictureEditTabAClipart3.Image :
				null;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group1Toggle = checkEditTabAGroup1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group2Toggle = checkEditTabAGroup2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group3Toggle = checkEditTabAGroup3.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group4Toggle = checkEditTabAGroup4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group5Toggle = checkEditTabAGroup5.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Group6Toggle = checkEditTabAGroup6.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14Toggle = checkEditTabASubheader14.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15Toggle = checkEditTabASubheader15.Checked;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader1 = textEditTabASubheader1.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader1DefaultValue ?
				textEditTabASubheader1.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader2 = textEditTabASubheader2.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader2DefaultValue ?
				textEditTabASubheader2.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader3 = textEditTabASubheader3.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader3DefaultValue ?
				textEditTabASubheader3.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader4 = textEditTabASubheader4.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader4DefaultValue ?
				textEditTabASubheader4.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader5 = textEditTabASubheader5.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader5DefaultValue ?
				textEditTabASubheader5.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader6 = textEditTabASubheader6.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader6DefaultValue ?
				textEditTabASubheader6.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader7 = textEditTabASubheader7.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader7DefaultValue ?
				textEditTabASubheader7.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader8 = textEditTabASubheader8.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader8DefaultValue ?
				textEditTabASubheader8.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader9 = textEditTabASubheader9.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader9DefaultValue ?
				textEditTabASubheader9.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader10 = textEditTabASubheader10.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader10DefaultValue ?
				textEditTabASubheader10.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader11 = textEditTabASubheader11.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader11DefaultValue ?
				textEditTabASubheader11.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader12 = textEditTabASubheader12.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader12DefaultValue ?
				textEditTabASubheader12.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader13 = textEditTabASubheader13.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader13DefaultValue ?
				textEditTabASubheader13.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader14 = textEditTabASubheader14.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader14DefaultValue ?
				textEditTabASubheader14.EditValue as String :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabA.Subheader15 = textEditTabASubheader15.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartASubHeader15DefaultValue ?
				textEditTabASubheader15.EditValue as String :
				null;

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ROIContentContainer.RaiseDataChanged();
		}

		#region Tab A Processing

		private void OnTabAGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup1Inner.Enabled = checkEditTabAGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup2Inner.Enabled = checkEditTabAGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup3Inner.Enabled = checkEditTabAGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup4Inner.Enabled = checkEditTabAGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup5Inner.Enabled = checkEditTabAGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup6Inner.Enabled = checkEditTabAGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabASubheader14CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader14Value.Enabled = checkEditTabASubheader14.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabASubheader15CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader15Value.Enabled = checkEditTabASubheader15.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabAFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var sourceValue = 0.0;
			try
			{
				sourceValue = Double.Parse((textEditTabASubheader2.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var callsCount = 0.0;
			try
			{
				callsCount = Double.Parse((textEditTabASubheader5.EditValue as String)?.Trim() ?? "0",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var percent = 0.0;
			try
			{
				percent = Double.Parse((textEditTabASubheader8.EditValue as String)?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var investmentValue = 1.0;
			try
			{
				investmentValue = Double.Parse((textEditTabASubheader14.EditValue as String)?.Trim() ?? "1",
					NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var formula1Value = Math.Ceiling(callsCount * percent / 100);
			var formula2Value = sourceValue * formula1Value;
			var formula3Value = Math.Ceiling(formula2Value / investmentValue);
			formula3Value = formula3Value < formula2Value ? formula3Value : 1.0;

			simpleLabelItemTabAFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabAFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabAFormula2.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabAFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula2Value);
			layoutControlItemTabASubheader15Value.CustomizationFormText = formula3Value.ToString();
			layoutControlItemTabASubheader15Value.Text = String.Format("= <b>{0:#,##0} : 1</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion
	}
}
