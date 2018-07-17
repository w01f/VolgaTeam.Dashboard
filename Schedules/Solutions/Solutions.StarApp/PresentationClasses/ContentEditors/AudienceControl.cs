using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class AudienceControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppAudience;

		public AudienceControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab9SubATitle))
			{
				layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab9SubATitle;

				memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabASubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
				memoEditTabASubheader2.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2Placeholder ?? memoEditTabASubheader2.Properties.NullText;
				textEditTabBSubheader1.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;

				clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubAClipart1Image), SlideContainer.StarInfo.AudienceConfiguration.PartAClipart1Configuration, this);
				clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabA2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubAClipart2Image), SlideContainer.StarInfo.AudienceConfiguration.PartAClipart2Configuration, this);
				clipartEditContainerTabA2.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab9SubBTitle))
			{
				layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab9SubBTitle;

				textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				textEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				textEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabBSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabBSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabBSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				textEditTabBSubheader2.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2Placeholder ?? textEditTabBSubheader2.Properties.NullText;
				textEditTabBSubheader3.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3Placeholder ?? textEditTabBSubheader3.Properties.NullText;
				memoEditTabBSubheader4.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4Placeholder ?? memoEditTabBSubheader4.Properties.NullText;
				memoEditTabBSubheader5.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5Placeholder ?? memoEditTabBSubheader5.Properties.NullText;
				memoEditTabBSubheader6.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6Placeholder ?? memoEditTabBSubheader6.Properties.NullText;

				clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubBClipart1Image), SlideContainer.StarInfo.AudienceConfiguration.PartBClipart1Configuration, this);
				clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubBClipart2Image), SlideContainer.StarInfo.AudienceConfiguration.PartBClipart2Configuration, this);
				clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabB3.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubBClipart3Image), SlideContainer.StarInfo.AudienceConfiguration.PartBClipart3Configuration, this);
				clipartEditContainerTabB3.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab9SubCTitle))
			{
				layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab9SubCTitle;

				comboBoxEditTabCCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				comboBoxEditTabCCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabCCombo1.Properties.NullText =
					SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
					comboBoxEditTabCCombo1.Properties.NullText;

				clipartEditContainerTabC1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubCClipart1Image), SlideContainer.StarInfo.AudienceConfiguration.PartCClipart1Configuration, this);
				clipartEditContainerTabC1.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabC2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubCClipart2Image), SlideContainer.StarInfo.AudienceConfiguration.PartCClipart2Configuration, this);
				clipartEditContainerTabC2.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabC3.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubCClipart3Image), SlideContainer.StarInfo.AudienceConfiguration.PartCClipart3Configuration, this);
				clipartEditContainerTabC3.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabC4.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab9SubCClipart4Image), SlideContainer.StarInfo.AudienceConfiguration.PartCClipart4Configuration, this);
				clipartEditContainerTabC4.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab9SubATitle))
			{
				clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.AudienceState.TabA.Clipart1);
				clipartEditContainerTabA2.LoadData(SlideContainer.EditedContent.AudienceState.TabA.Clipart2);
				memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader1 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue;
				memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader2 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue;
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab9SubBTitle))
			{
				clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.AudienceState.TabB.Clipart1);
				clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.AudienceState.TabB.Clipart2);
				clipartEditContainerTabB3.LoadData(SlideContainer.EditedContent.AudienceState.TabB.Clipart3);
				textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader1 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue;
				textEditTabBSubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader2 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue;
				textEditTabBSubheader3.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader3 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue;
				memoEditTabBSubheader4.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader4 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue;
				memoEditTabBSubheader5.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader5 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue;
				memoEditTabBSubheader6.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader6 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue;
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab9SubCTitle))
			{
				clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart1);
				clipartEditContainerTabC2.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart2);
				clipartEditContainerTabC3.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart3);
				clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart4);
				comboBoxEditTabCCombo1.EditValue = SlideContainer.EditedContent.AudienceState.TabC.Combo1 ??
												   SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(
													   item => item.IsDefault);
			}

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA)
			{
				SlideContainer.EditedContent.AudienceState.TabA.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;

				SlideContainer.EditedContent.AudienceState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();
				SlideContainer.EditedContent.AudienceState.TabA.Clipart2 = clipartEditContainerTabA2.GetActiveClipartObject();

				SlideContainer.EditedContent.AudienceState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue ?
					memoEditTabASubheader1.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.AudienceState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue ?
					memoEditTabASubheader2.EditValue as String ?? String.Empty :
					null;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				SlideContainer.EditedContent.AudienceState.TabB.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

				SlideContainer.EditedContent.AudienceState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
				SlideContainer.EditedContent.AudienceState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();
				SlideContainer.EditedContent.AudienceState.TabB.Clipart3 = clipartEditContainerTabB3.GetActiveClipartObject();

				SlideContainer.EditedContent.AudienceState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue ?
					textEditTabBSubheader1.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.AudienceState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue ?
					textEditTabBSubheader2.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.AudienceState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue ?
					textEditTabBSubheader3.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.AudienceState.TabB.Subheader4 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue ?
					memoEditTabBSubheader4.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.AudienceState.TabB.Subheader5 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue ?
					memoEditTabBSubheader5.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.AudienceState.TabB.Subheader6 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue ?
					memoEditTabBSubheader6.EditValue as String ?? String.Empty :
					null;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				SlideContainer.EditedContent.AudienceState.TabC.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;

				SlideContainer.EditedContent.AudienceState.TabC.Clipart1 = clipartEditContainerTabC1.GetActiveClipartObject();
				SlideContainer.EditedContent.AudienceState.TabC.Clipart2 = clipartEditContainerTabC2.GetActiveClipartObject();
				SlideContainer.EditedContent.AudienceState.TabC.Clipart3 = clipartEditContainerTabC3.GetActiveClipartObject();
				SlideContainer.EditedContent.AudienceState.TabC.Clipart4 = clipartEditContainerTabC4.GetActiveClipartObject();

				SlideContainer.EditedContent.AudienceState.TabC.Combo1 = SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
					comboBoxEditTabCCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo1.EditValue as String } :
					null;
			}

			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;

			if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubAFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems
					.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabA.SlideHeader ??
													SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(
														h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems
																  .FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubBFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems
					.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabB.SlideHeader ??
													SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(
														h => h.IsDefault);

				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems
																  .FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubCFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabC.SlideHeader ??
					SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);

				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
					"Select or type";
			}

			_allowToSave = true;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSelectedPageChanging(object sender, LayoutTabPageChangingEventArgs e)
		{
			if (_allowToSave)
				ApplyChanges();
		}

		private void OnSelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
		{
			LoadPartData();
		}

		private void OnTabbedGroupClick(object sender, EventArgs e)
		{
			labelFocusFake.Focus();
		}

		private void OnResize(object sender, EventArgs e)
		{
			panelLogoRight.Visible = panelLogoBottom.Visible = Width > 1000;
		}
	}
}