using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CNAControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppCNA;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab2Title;

		public CNAControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab2SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab2SubBTitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab2SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.CNAConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab2SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.CNAConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab2SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.CNAConfiguration.PartBClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabBClipart1,
				pictureEditTabBClipart2
			});

			memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.CNAConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
			memoEditTabASubheader2.Properties.NullText = SlideContainer.StarInfo.CNAConfiguration.PartASubHeader2Placeholder ?? memoEditTabASubheader2.Properties.NullText;

			comboBoxEditTabBCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo1.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(0)?.Value ??
				comboBoxEditTabBCombo1.Properties.NullText;
			comboBoxEditTabBCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo2.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(1)?.Value ??
				comboBoxEditTabBCombo2.Properties.NullText;
			comboBoxEditTabBCombo3.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo3.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(2)?.Value ??
				comboBoxEditTabBCombo3.Properties.NullText;
			comboBoxEditTabBCombo4.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo4.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(3)?.Value ??
				comboBoxEditTabBCombo4.Properties.NullText;
			comboBoxEditTabBCombo5.Properties.Items.AddRange(SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo5.Properties.NullText =
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(item => item.IsPlaceholder).ElementAtOrDefault(4)?.Value ??
				comboBoxEditTabBCombo5.Properties.NullText;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabAClipart1.Image = SlideContainer.EditedContent.CNAState.TabA.Clipart1 ??
											pictureEditTabAClipart1.Image;
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.CNAState.TabA.Subheader1 ??
				SlideContainer.StarInfo.CNAConfiguration.PartASubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.CNAState.TabA.Subheader2 ??
				SlideContainer.StarInfo.CNAConfiguration.PartASubHeader2DefaultValue;

			pictureEditTabBClipart1.Image = SlideContainer.EditedContent.CNAState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = SlideContainer.EditedContent.CNAState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;
			comboBoxEditTabBCombo1.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo1 ??
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabBCombo2.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo2 ??
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabBCombo3.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo3 ??
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabBCombo4.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo4 ??
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);
			comboBoxEditTabBCombo5.EditValue = SlideContainer.EditedContent.CNAState.TabB.Combo5 ??
				SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(4);

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.CNAState.TabA.SlideHeader = SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.CNAState.TabA.Clipart1 = pictureEditTabAClipart1.Image != SlideContainer.StarInfo.Tab2SubAClipart1Image ?
						pictureEditTabAClipart1.Image :
						null;

					SlideContainer.EditedContent.CNAState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.CNAConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String :
						null;
					SlideContainer.EditedContent.CNAState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.StarInfo.CNAConfiguration.PartASubHeader2DefaultValue ?
						memoEditTabASubheader2.EditValue as String :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.CNAState.TabB.SlideHeader = SlideContainer.StarInfo.CNAConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.CNAState.TabB.Clipart1 = pictureEditTabBClipart1.Image != SlideContainer.StarInfo.Tab2SubBClipart1Image ?
						pictureEditTabBClipart1.Image :
						null;
					SlideContainer.EditedContent.CNAState.TabB.Clipart2 = pictureEditTabBClipart2.Image != SlideContainer.StarInfo.Tab2SubBClipart2Image ?
						pictureEditTabBClipart2.Image :
						null;

					SlideContainer.EditedContent.CNAState.TabB.Combo1 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabBCombo1.EditValue ?
						comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
						null;
					SlideContainer.EditedContent.CNAState.TabB.Combo2 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabBCombo2.EditValue ?
						comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
						null;
					SlideContainer.EditedContent.CNAState.TabB.Combo3 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabBCombo3.EditValue ?
						comboBoxEditTabBCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo3.EditValue as String } :
						null;
					SlideContainer.EditedContent.CNAState.TabB.Combo4 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabBCombo4.EditValue ?
						comboBoxEditTabBCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo4.EditValue as String } :
						null;
					SlideContainer.EditedContent.CNAState.TabB.Combo5 = SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(4) != comboBoxEditTabBCombo5.EditValue ?
						comboBoxEditTabBCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo5.EditValue as String } :
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab2SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab2SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CNAState.TabA.SlideHeader ??
						SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab2SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab2SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CNAConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CNAState.TabB.SlideHeader ??
						SlideContainer.StarInfo.CNAConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CNAConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
			}
			_allowToSave = true;

			_dataChanged = false;
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