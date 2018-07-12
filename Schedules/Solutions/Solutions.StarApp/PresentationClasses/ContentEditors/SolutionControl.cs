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
	public sealed partial class SolutionControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppSolution;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab10Title;

		public SolutionControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabDSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab10SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab10SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab10SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab10SubDTitle;

			clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab10SubAClipart1Image), SlideContainer.StarInfo.SolutionConfiguration.PartAClipart1Configuration, this);
			clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab10SubBClipart1Image), SlideContainer.StarInfo.SolutionConfiguration.PartBClipart1Configuration, this);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab10SubBClipart2Image), SlideContainer.StarInfo.SolutionConfiguration.PartBClipart2Configuration, this);
			clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB3.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab10SubBClipart3Image), SlideContainer.StarInfo.SolutionConfiguration.PartBClipart3Configuration, this);
			clipartEditContainerTabB3.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab10SubCClipart1Image), SlideContainer.StarInfo.SolutionConfiguration.PartCClipart1Configuration, this);
			clipartEditContainerTabC1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab10SubCClipart2Image), SlideContainer.StarInfo.SolutionConfiguration.PartCClipart2Configuration, this);
			clipartEditContainerTabC2.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabD1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab10SubDClipart1Image), SlideContainer.StarInfo.SolutionConfiguration.PartDClipart1Configuration, this);
			clipartEditContainerTabD1.EditValueChanged += OnEditValueChanged;

			memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
			memoEditTabBSubheader1.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.PartBSubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;
			memoEditTabCSubheader1.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader1Placeholder ?? memoEditTabCSubheader1.Properties.NullText;
			memoEditTabCSubheader2.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader2Placeholder ?? memoEditTabCSubheader2.Properties.NullText;
			memoEditTabDSubheader1.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.PartDSubHeader1Placeholder ?? memoEditTabDSubheader1.Properties.NullText;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.SolutionState.TabA.Clipart1);
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.SolutionState.TabA.Subheader1 ??
				SlideContainer.StarInfo.SolutionConfiguration.PartASubHeader1DefaultValue;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.SolutionState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.SolutionState.TabB.Clipart2);
			clipartEditContainerTabB3.LoadData(SlideContainer.EditedContent.SolutionState.TabB.Clipart3);
			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.SolutionState.TabB.Subheader1 ??
				SlideContainer.StarInfo.SolutionConfiguration.PartBSubHeader1DefaultValue;

			clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.SolutionState.TabC.Clipart1);
			clipartEditContainerTabC2.LoadData(SlideContainer.EditedContent.SolutionState.TabC.Clipart2);
			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.SolutionState.TabC.Subheader1 ??
				SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.SolutionState.TabC.Subheader2 ??
				SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader2DefaultValue;

			clipartEditContainerTabD1.LoadData(SlideContainer.EditedContent.SolutionState.TabD.Clipart1);
			memoEditTabDSubheader1.EditValue = SlideContainer.EditedContent.SolutionState.TabD.Subheader1 ??
				SlideContainer.StarInfo.SolutionConfiguration.PartDSubHeader1DefaultValue;

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.SolutionState.TabA.SlideHeader = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.SolutionState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

					SlideContainer.EditedContent.SolutionState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.SolutionConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String ?? String.Empty :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.SolutionState.TabB.SlideHeader = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.SolutionState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
					SlideContainer.EditedContent.SolutionState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();
					SlideContainer.EditedContent.SolutionState.TabB.Clipart3 = clipartEditContainerTabB3.GetActiveClipartObject();

					SlideContainer.EditedContent.SolutionState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.SolutionConfiguration.PartBSubHeader1DefaultValue ?
						memoEditTabBSubheader1.EditValue as String ?? String.Empty :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.SolutionState.TabC.SlideHeader = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.SolutionState.TabC.Clipart1 = clipartEditContainerTabC1.GetActiveClipartObject();
					SlideContainer.EditedContent.SolutionState.TabC.Clipart2 = clipartEditContainerTabC2.GetActiveClipartObject();

					SlideContainer.EditedContent.SolutionState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader1DefaultValue ?
						memoEditTabCSubheader1.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.SolutionState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader2DefaultValue ?
						memoEditTabCSubheader2.EditValue as String ?? String.Empty :
						null;
					break;
				case 3:
					SlideContainer.EditedContent.SolutionState.TabD.SlideHeader = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.SolutionState.TabD.Clipart1 = clipartEditContainerTabD1.GetActiveClipartObject();

					SlideContainer.EditedContent.SolutionState.TabD.Subheader1 = memoEditTabDSubheader1.EditValue as String != SlideContainer.StarInfo.SolutionConfiguration.PartDSubHeader1DefaultValue ?
						memoEditTabDSubheader1.EditValue as String ?? String.Empty :
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.SolutionConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.SolutionState.TabA.SlideHeader ??
						SlideContainer.StarInfo.SolutionConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.SolutionConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.SolutionState.TabB.SlideHeader ??
						SlideContainer.StarInfo.SolutionConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.SolutionConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.SolutionState.TabC.SlideHeader ??
						SlideContainer.StarInfo.SolutionConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.SolutionConfiguration.HeadersPartDItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.SolutionState.TabD.SlideHeader ??
						SlideContainer.StarInfo.SolutionConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.SolutionConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
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