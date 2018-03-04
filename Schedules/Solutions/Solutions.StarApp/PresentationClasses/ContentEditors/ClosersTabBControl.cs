using System;
using System.Linq;
using System.Windows.Forms;
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

			comboBoxEditTabBCombo1.EditValue =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo3.EditValue =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo4.EditValue =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(item => item.IsDefault);

			memoEditTabBSubheader1.EditValue = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue;
			memoEditTabBSubheader3.EditValue = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue;
			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			ClosersContentContainer.SlideContainer.RaiseDataChanged();
		}
	}
}
