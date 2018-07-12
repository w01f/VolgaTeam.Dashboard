using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class MarketControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppMarket;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab7Title;

		public MarketControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab7SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab7SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab7SubCTitle;

			clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubAClipart1Image), SlideContainer.StarInfo.MarketConfiguration.PartAClipart1Configuration, this);
			clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubBClipart1Image), SlideContainer.StarInfo.MarketConfiguration.PartBClipart1Configuration, this);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubBClipart2Image), SlideContainer.StarInfo.MarketConfiguration.PartBClipart2Configuration, this);
			clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB3.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubBClipart3Image), SlideContainer.StarInfo.MarketConfiguration.PartBClipart3Configuration, this);
			clipartEditContainerTabB3.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB4.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubBClipart4Image), SlideContainer.StarInfo.MarketConfiguration.PartBClipart4Configuration, this);
			clipartEditContainerTabB4.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB5.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubBClipart5Image), SlideContainer.StarInfo.MarketConfiguration.PartBClipart5Configuration, this);
			clipartEditContainerTabB5.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubCClipart1Image), SlideContainer.StarInfo.MarketConfiguration.PartCClipart1Configuration, this);
			clipartEditContainerTabC1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubCClipart2Image), SlideContainer.StarInfo.MarketConfiguration.PartCClipart2Configuration, this);
			clipartEditContainerTabC2.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC3.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubCClipart3Image), SlideContainer.StarInfo.MarketConfiguration.PartCClipart3Configuration, this);
			clipartEditContainerTabC3.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC4.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab7SubCClipart4Image), SlideContainer.StarInfo.MarketConfiguration.PartCClipart4Configuration, this);
			clipartEditContainerTabC4.EditValueChanged += OnEditValueChanged;

			memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;

			textEditTabBSubheader1.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			memoEditTabBSubheader2.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader2Placeholder ?? memoEditTabBSubheader2.Properties.NullText;

			comboBoxEditTabCCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo1.Properties.NullText =
				SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo1.Properties.NullText;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;
			clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.MarketState.TabA.Clipart1);
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.MarketState.TabA.Subheader1 ??
				SlideContainer.StarInfo.MarketConfiguration.PartASubHeader1DefaultValue;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart2);
			clipartEditContainerTabB3.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart3);
			clipartEditContainerTabB4.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart4);
			clipartEditContainerTabB5.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart5);
			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.MarketState.TabB.Subheader1 ??
				SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = SlideContainer.EditedContent.MarketState.TabB.Subheader2 ??
				SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader2DefaultValue;

			clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.MarketState.TabC.Clipart1);
			clipartEditContainerTabC2.LoadData(SlideContainer.EditedContent.MarketState.TabC.Clipart2);
			clipartEditContainerTabC3.LoadData(SlideContainer.EditedContent.MarketState.TabC.Clipart3);
			clipartEditContainerTabC4.LoadData(SlideContainer.EditedContent.MarketState.TabC.Clipart4);
			comboBoxEditTabCCombo1.EditValue = SlideContainer.EditedContent.MarketState.TabC.Combo1 ??
				SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.MarketState.TabA.SlideHeader = SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.MarketState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

					SlideContainer.EditedContent.MarketState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.MarketConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String ?? String.Empty :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.MarketState.TabB.SlideHeader = SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.MarketState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
					SlideContainer.EditedContent.MarketState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();
					SlideContainer.EditedContent.MarketState.TabB.Clipart3 = clipartEditContainerTabB3.GetActiveClipartObject();
					SlideContainer.EditedContent.MarketState.TabB.Clipart4 = clipartEditContainerTabB4.GetActiveClipartObject();
					SlideContainer.EditedContent.MarketState.TabB.Clipart5 = clipartEditContainerTabB5.GetActiveClipartObject();

					SlideContainer.EditedContent.MarketState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader1DefaultValue ?
						textEditTabBSubheader1.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.MarketState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader2DefaultValue ?
						memoEditTabBSubheader2.EditValue as String ?? String.Empty :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.MarketState.TabC.SlideHeader = SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.MarketState.TabC.Clipart1 = clipartEditContainerTabC1.GetActiveClipartObject();
					SlideContainer.EditedContent.MarketState.TabC.Clipart2 = clipartEditContainerTabC2.GetActiveClipartObject();
					SlideContainer.EditedContent.MarketState.TabC.Clipart3 = clipartEditContainerTabC3.GetActiveClipartObject();
					SlideContainer.EditedContent.MarketState.TabC.Clipart4 = clipartEditContainerTabC4.GetActiveClipartObject();

					SlideContainer.EditedContent.MarketState.TabC.Combo1 = SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
						comboBoxEditTabCCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo1.EditValue as String } :
						null;
					break;
			}

			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabA.SlideHeader ??
						SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabB.SlideHeader ??
						SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabC.SlideHeader ??
						SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
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