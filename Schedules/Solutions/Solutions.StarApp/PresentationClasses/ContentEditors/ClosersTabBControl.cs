using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabBControl : ClosersTabBaseControl
	{
		public ClosersTabBControl(ClosersControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll();
			comboBoxEditTabBCombo2.EnableSelectAll();
			comboBoxEditTabBCombo3.EnableSelectAll();
			comboBoxEditTabBCombo4.EnableSelectAll();
			memoEditTabBSubheader1.EnableSelectAll();
			memoEditTabBSubheader2.EnableSelectAll();
			memoEditTabBSubheader3.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabBClipart1.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
			});

			Application.DoEvents();

			comboBoxEditTabBCombo1.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items);
			comboBoxEditTabBCombo2.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items);
			comboBoxEditTabBCombo3.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items);
			comboBoxEditTabBCombo4.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabBClipart1.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;

			comboBoxEditTabBCombo1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo1 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo2 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo3.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo3 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo4.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo4 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(item => item.IsDefault);

			memoEditTabBSubheader1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader1 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader2 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue;
			memoEditTabBSubheader3.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader3 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue;
			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1 = pictureEditTabBClipart1.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart1Image ?
				pictureEditTabBClipart1.Image :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2 = pictureEditTabBClipart2.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image ?
				pictureEditTabBClipart2.Image :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo1 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? (comboBoxEditTabBCombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo1.EditValue } : null) :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo2 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? (comboBoxEditTabBCombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo2.EditValue } : null) :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo3 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo3.EditValue ?
				comboBoxEditTabBCombo3.EditValue as ListDataItem ?? (comboBoxEditTabBCombo3.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo3.EditValue } : null) :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo4 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo4.EditValue ?
				comboBoxEditTabBCombo4.EditValue as ListDataItem ?? (comboBoxEditTabBCombo4.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo4.EditValue } : null) :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue ?
				memoEditTabBSubheader1.EditValue as String :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue ?
				memoEditTabBSubheader2.EditValue as String :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader3 = memoEditTabBSubheader3.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue ?
				memoEditTabBSubheader3.EditValue as String :
				null;

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ClosersContentContainer.RaiseDataChanged();
		}
	}
}
