using System;
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
			//pictureEditTabBClipart1.Properties.PictureAlignment =
			//	ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image;
			//pictureEditTabBClipart2.Properties.PictureAlignment =
			//	ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
			});

			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

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
