using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class ROIControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppROI;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab6Title;

		public ROIControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			Text = SlideName;

			comboBoxEditSlideHeader.EnableSelectAll();

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

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab6SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab6SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab6SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab6SubDTitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab5SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = SlideContainer.StarInfo.Tab5SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartAClipart2Configuration.Alignment;
			pictureEditTabAClipart3.Image = SlideContainer.StarInfo.Tab5SubAClipart3Image;
			pictureEditTabAClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartAClipart3Configuration.Alignment;
			Application.DoEvents();

			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab5SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab5SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = SlideContainer.StarInfo.Tab5SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartBClipart3Configuration.Alignment;
			Application.DoEvents();

			pictureEditTabCClipart1.Image = SlideContainer.StarInfo.Tab5SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = SlideContainer.StarInfo.Tab5SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = SlideContainer.StarInfo.Tab5SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartCClipart3Configuration.Alignment;
			Application.DoEvents();

			pictureEditTabDClipart1.Image = SlideContainer.StarInfo.Tab5SubDClipart1Image;
			pictureEditTabDClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartDClipart1Configuration.Alignment;
			pictureEditTabDClipart2.Image = SlideContainer.StarInfo.Tab5SubDClipart2Image;
			pictureEditTabDClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartDClipart2Configuration.Alignment;
			pictureEditTabDClipart3.Image = SlideContainer.StarInfo.Tab5SubDClipart3Image;
			pictureEditTabDClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ROIConfiguration.PartDClipart3Configuration.Alignment;
			Application.DoEvents();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			textEditTabASubheader1.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader1DefaultValue;
			textEditTabASubheader2.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader4DefaultValue;
			textEditTabASubheader5.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader7DefaultValue;
			textEditTabASubheader8.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader8DefaultValue;
			textEditTabASubheader9.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader9DefaultValue;
			textEditTabASubheader10.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader10DefaultValue;
			textEditTabASubheader11.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader11DefaultValue;
			textEditTabASubheader12.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader12DefaultValue;
			textEditTabASubheader13.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader13DefaultValue;
			textEditTabASubheader14.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader14DefaultValue;
			textEditTabASubheader15.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartASubHeader15DefaultValue;
			Application.DoEvents();

			textEditTabBSubheader1.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3DefaultValue;
			textEditTabBSubheader4.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4DefaultValue;
			textEditTabBSubheader5.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader5DefaultValue;
			textEditTabBSubheader6.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7DefaultValue;
			textEditTabBSubheader8.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader8DefaultValue;
			textEditTabBSubheader9.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9DefaultValue;
			textEditTabBSubheader10.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10DefaultValue;
			textEditTabBSubheader11.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader11DefaultValue;
			textEditTabBSubheader12.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12DefaultValue;
			textEditTabBSubheader13.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13DefaultValue;
			textEditTabBSubheader14.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14DefaultValue;
			textEditTabBSubheader15.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15DefaultValue;
			textEditTabBSubheader16.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16DefaultValue;
			textEditTabBSubheader17.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader17DefaultValue;
			textEditTabBSubheader18.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18DefaultValue;
			textEditTabBSubheader19.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19DefaultValue;
			textEditTabBSubheader20.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20DefaultValue;
			textEditTabBSubheader21.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21DefaultValue;
			textEditTabBSubheader22.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22DefaultValue;
			textEditTabBSubheader23.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23DefaultValue;
			textEditTabBSubheader24.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader24DefaultValue;
			textEditTabBSubheader25.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25DefaultValue;
			Application.DoEvents();

			textEditTabCSubheader1.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader1DefaultValue;
			textEditTabCSubheader2.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader2DefaultValue;
			textEditTabCSubheader3.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader3DefaultValue;
			textEditTabCSubheader4.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader4DefaultValue;
			textEditTabCSubheader5.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader5DefaultValue;
			textEditTabCSubheader6.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader6DefaultValue;
			textEditTabCSubheader7.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader7DefaultValue;
			textEditTabCSubheader8.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader8DefaultValue;
			textEditTabCSubheader9.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader9DefaultValue;
			textEditTabCSubheader10.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader10DefaultValue;
			textEditTabCSubheader11.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader11DefaultValue;
			textEditTabCSubheader12.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader12DefaultValue;
			textEditTabCSubheader13.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader13DefaultValue;
			textEditTabCSubheader14.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader14DefaultValue;
			textEditTabCSubheader15.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartCSubHeader15DefaultValue;
			Application.DoEvents();

			textEditTabDSubheader1.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader1DefaultValue;
			textEditTabDSubheader2.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader3DefaultValue;
			textEditTabDSubheader4.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader5DefaultValue;
			textEditTabDSubheader6.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader7DefaultValue;
			textEditTabDSubheader8.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader9DefaultValue;
			textEditTabDSubheader10.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader10DefaultValue;
			textEditTabDSubheader11.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader11DefaultValue;
			textEditTabDSubheader12.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader12DefaultValue;
			textEditTabDSubheader13.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader13DefaultValue;
			textEditTabDSubheader14.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader14DefaultValue;
			textEditTabDSubheader15.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader15DefaultValue;
			textEditTabDSubheader16.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader16DefaultValue;
			textEditTabDSubheader17.EditValue = SlideContainer.StarInfo.ROIConfiguration.PartDSubHeader17DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabAFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
			OnTabBFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
			OnTabCFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
			OnTabDFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.ROIState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ROIState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ROIState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ROIState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ROIState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
			}
			_allowToSave = true;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
		{
			if (_allowToSave)
				ApplyChanges();
			LoadPartData();
		}

		private void OnResize(object sender, EventArgs e)
		{
			panelLogoRight.Visible = panelLogoBottom.Visible = Width > 1000;
		}

		#region Tab A Processing

		private void OnTabAGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup1Inner.Enabled = checkEditTabAGroup1.Checked;
		}

		private void OnTabAGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup2Inner.Enabled = checkEditTabAGroup2.Checked;
		}

		private void OnTabAGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup3Inner.Enabled = checkEditTabAGroup3.Checked;
		}

		private void OnTabAGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup4Inner.Enabled = checkEditTabAGroup4.Checked;
		}

		private void OnTabAGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup5Inner.Enabled = checkEditTabAGroup5.Checked;
		}

		private void OnTabAGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabAGroup6Inner.Enabled = checkEditTabAGroup6.Checked;
		}

		private void OnTabASubheader14CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader14Value.Enabled = checkEditTabASubheader14.Checked;
		}

		private void OnTabASubheader15CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabASubheader15Value.Enabled = checkEditTabASubheader15.Checked;
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

		#region Tab B Processing
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
		#endregion

		#region Tab C Processing
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
		#endregion

		#region Tab D Processing

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
		#endregion

		#region Output Staff

		public override bool ReadyForOutput => false;

		public override void GenerateOutput()
		{
			//SolutionDashboardPowerPointHelper.Instance.AppendCover(this);
		}

		public override PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//SolutionDashboardPowerPointHelper.Instance.PrepareCover(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}