using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
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
	public sealed partial class ShareControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppShare;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab5Title;

		public ShareControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			Text = SlideName;

			comboBoxEditSlideHeader.EnableSelectAll();
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

			comboBoxEditTabCCombo1.EnableSelectAll();
			comboBoxEditTabCCombo2.EnableSelectAll();
			comboBoxEditTabCCombo3.EnableSelectAll();
			comboBoxEditTabCCombo4.EnableSelectAll();
			comboBoxEditTabCCombo5.EnableSelectAll();
			comboBoxEditTabCCombo6.EnableSelectAll();
			textEditTabCSubheader1.EnableSelectAll();
			textEditTabCSubheader2.EnableSelectAll();
			textEditTabCSubheader4.EnableSelectAll();
			memoEditTabCSubheader3.EnableSelectAll();
			Application.DoEvents();

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

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab5SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab5SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab5SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab5SubDTitle;
			layoutControlGroupTabE.Text = SlideContainer.StarInfo.Titles.Tab5SubETitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab5SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = SlideContainer.StarInfo.Tab5SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartAClipart2Configuration.Alignment;
			pictureEditTabAClipart3.Image = SlideContainer.StarInfo.Tab5SubAClipart3Image;
			pictureEditTabAClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartAClipart3Configuration.Alignment;
			Application.DoEvents();

			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab5SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab5SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = SlideContainer.StarInfo.Tab5SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartBClipart3Configuration.Alignment;
			Application.DoEvents();

			pictureEditTabCClipart1.Image = SlideContainer.StarInfo.Tab5SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = SlideContainer.StarInfo.Tab5SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = SlideContainer.StarInfo.Tab5SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartCClipart3Configuration.Alignment;
			Application.DoEvents();

			pictureEditTabDClipart1.Image = SlideContainer.StarInfo.Tab5SubDClipart1Image;
			pictureEditTabDClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartDClipart1Configuration.Alignment;
			pictureEditTabDClipart2.Image = SlideContainer.StarInfo.Tab5SubDClipart2Image;
			pictureEditTabDClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartDClipart2Configuration.Alignment;
			pictureEditTabDClipart3.Image = SlideContainer.StarInfo.Tab5SubDClipart3Image;
			pictureEditTabDClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartDClipart3Configuration.Alignment;
			Application.DoEvents();

			pictureEditTabEClipart1.Image = SlideContainer.StarInfo.Tab5SubEClipart1Image;
			pictureEditTabEClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartEClipart1Configuration.Alignment;
			pictureEditTabEClipart2.Image = SlideContainer.StarInfo.Tab5SubEClipart2Image;
			pictureEditTabEClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartEClipart2Configuration.Alignment;
			pictureEditTabEClipart3.Image = SlideContainer.StarInfo.Tab5SubEClipart3Image;
			pictureEditTabEClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.ShareConfiguration.PartEClipart3Configuration.Alignment;
			Application.DoEvents();

			comboBoxEditTabACombo1.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items);
			comboBoxEditTabACombo2.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items);
			comboBoxEditTabACombo3.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items);
			comboBoxEditTabACombo4.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items);
			Application.DoEvents();

			comboBoxEditTabBCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items);
			comboBoxEditTabBCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items);
			Application.DoEvents();

			comboBoxEditTabCCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items);
			comboBoxEditTabCCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items);
			comboBoxEditTabCCombo3.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items);
			comboBoxEditTabCCombo4.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items);
			comboBoxEditTabCCombo5.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items);
			comboBoxEditTabCCombo6.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items);
			Application.DoEvents();

			comboBoxEditTabDCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items);
			comboBoxEditTabDCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items);
			comboBoxEditTabDCombo3.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items);
			Application.DoEvents();

			comboBoxEditTabECombo1.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items);
			comboBoxEditTabECombo2.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items);
			comboBoxEditTabECombo3.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items);
			comboBoxEditTabECombo4.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items);
			Application.DoEvents();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditTabACombo1.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartACombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo2.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartACombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo3.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartACombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabACombo4.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartACombo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabASubheader1.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartASubHeader1DefaultValue;
			textEditTabASubheader2.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartASubHeader2DefaultValue;
			textEditTabASubheader3.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartASubHeader3DefaultValue;
			textEditTabASubheader4.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartASubHeader4DefaultValue;
			textEditTabASubheader5.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartASubHeader5DefaultValue;
			textEditTabASubheader6.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartASubHeader6DefaultValue;
			textEditTabASubheader7.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartASubHeader7DefaultValue;
			Application.DoEvents();

			comboBoxEditTabBCombo1.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsDefault);

			textEditTabBSubheader1.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader3DefaultValue;
			textEditTabBSubheader4.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader4DefaultValue;
			textEditTabBSubheader5.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader5DefaultValue;
			textEditTabBSubheader6.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader7DefaultValue;
			textEditTabBSubheader8.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartBSubHeader8DefaultValue;
			Application.DoEvents();

			comboBoxEditTabCCombo1.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo2.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartCCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo3.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartCCombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo4.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartCCombo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo5.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartCCombo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabCCombo6.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartCCombo6Items.FirstOrDefault(item => item.IsDefault);
			Application.DoEvents();

			textEditTabCSubheader1.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader1DefaultValue;
			textEditTabCSubheader2.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader3DefaultValue;
			textEditTabCSubheader4.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartCSubHeader4DefaultValue;

			comboBoxEditTabDCombo1.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartDCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo2.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartDCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabDCombo3.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartDCombo3Items.FirstOrDefault(item => item.IsDefault);

			textEditTabDSubheader1.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader1DefaultValue;
			textEditTabDSubheader2.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader3DefaultValue;
			textEditTabDSubheader4.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader5DefaultValue;
			textEditTabDSubheader6.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader7DefaultValue;
			textEditTabDSubheader8.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartDSubHeader9DefaultValue;
			Application.DoEvents();

			comboBoxEditTabECombo1.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartECombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo2.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartECombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo3.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartECombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabECombo4.EditValue =
				SlideContainer.StarInfo.ShareConfiguration.PartECombo4Items.FirstOrDefault(item => item.IsDefault);

			textEditTabESubheader1.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader1DefaultValue;
			textEditTabESubheader2.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader2DefaultValue;
			textEditTabESubheader3.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader3DefaultValue;
			textEditTabESubheader4.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader4DefaultValue;
			textEditTabESubheader5.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader5DefaultValue;
			textEditTabESubheader6.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader6DefaultValue;
			textEditTabESubheader7.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader7DefaultValue;
			textEditTabESubheader8.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader8DefaultValue;
			textEditTabESubheader9.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader9DefaultValue;
			textEditTabESubheader10.EditValue = SlideContainer.StarInfo.ShareConfiguration.PartESubHeader10DefaultValue;
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
			OnTabEFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.ShareState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems);
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 4:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubERightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubEFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems);
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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

		#endregion

		#region Tab B Processing

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

		#endregion

		#region Tab C Processing

		private void OnTabCGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup1Inner.Enabled = checkEditTabCGroup1.Checked;
		}

		private void OnTabCSubheader3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabCSubheader3Value.Enabled = checkEditTabCSubheader3.Checked;
		}

		private void OnTabCGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup2Inner.Enabled = checkEditTabCGroup2.Checked;
		}

		private void OnTabCGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup3Inner.Enabled = checkEditTabCGroup3.Checked;
		}

		private void OnTabCGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup4Inner.Enabled = checkEditTabCGroup4.Checked;
		}

		private void OnTabCGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup5Inner.Enabled = checkEditTabCGroup5.Checked;
		}

		private void OnTabCGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabCGroup6Inner.Enabled = checkEditTabCGroup6.Checked;
		}

		private void OnTabCFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var multiplierText = (comboBoxEditTabCCombo1.EditValue as ComboboxItem)?.Value ?? String.Empty;

			var sourceValue = 0.0;
			try
			{
				sourceValue = Double.Parse((textEditTabCSubheader1.EditValue as String)?.Trim() ?? "0",
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

			var householdPercent = 0.0;
			try
			{
				householdPercent =
					Double.Parse((comboBoxEditTabCCombo4.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharePercent = 0.0;
			try
			{
				sharePercent =
					Double.Parse((comboBoxEditTabCCombo6.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var formula1Value = sourceValue * (householdPercent / 100);
			var formula2Value = formula1Value * (sharePercent / 100);

			simpleLabelItemTabCFormula1.CustomizationFormText = formula1Value.ToString();
			simpleLabelItemTabCFormula1.Text = String.Format("{0} {1} x {2} = <b>{3:$#,##0}</b>",
				(textEditTabCSubheader1.EditValue as String)?.Trim(),
				(comboBoxEditTabCCombo1.EditValue as ComboboxItem)?.Value?.Trim(),
				(comboBoxEditTabCCombo4.EditValue as ComboboxItem)?.Value?.Trim(),
				formula1Value);
			simpleLabelItemTabCFormula2.Text = String.Format("<b>{0:$#,##0}</b>", formula1Value);
			simpleLabelItemTabCFormula3.Text = String.Format("Total Estimated Revenue in {0}",
				(comboBoxEditTabCCombo3.EditValue as ComboboxItem)?.Value?.Trim());
			simpleLabelItemTabCFormula4.Text = String.Format("Share Growth in {0}",
				(comboBoxEditTabCCombo3.EditValue as ComboboxItem)?.Value?.Trim());
			simpleLabelItemTabCFormula5.CustomizationFormText = formula2Value.ToString();
			simpleLabelItemTabCFormula5.Text = String.Format("<b>{0:$#,##0}</b> (annually)", formula2Value);

			OnEditValueChanged(sender, e);
		}

		#endregion

		#region Tab D Processing
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
		#endregion

		#region Tab E Processing
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

			var multiplierText = (comboBoxEditTabECombo1.EditValue as ComboboxItem)?.Value ?? String.Empty;

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
					Double.Parse((comboBoxEditTabECombo2.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
						NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
			}
			catch
			{
			}

			var sharePercent = 0.0;
			try
			{
				sharePercent =
					Double.Parse((comboBoxEditTabECombo4.EditValue as ComboboxItem)?.Value?.Trim()?.Replace("%", "") ?? "0",
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
		#endregion

		#region Output Staff

		public override bool ReadyForOutput => false;

		public override void GenerateOutput()
		{
			//SolutionDashboardPowerPointHelper.Instance.AppendCover(this);
		}

		public override PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
				Path.GetFileName(Path.GetTempFileName()));
			//SolutionDashboardPowerPointHelper.Instance.PrepareCover(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}

		#endregion
	}
}