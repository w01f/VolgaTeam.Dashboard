using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabCControl : ClosersTabBaseControl
	{
		public ClosersTabCControl(ClosersControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			memoEditTabCSubheader1.EnableSelectAll();
			memoEditTabCSubheader2.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabCClipart1.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabCClipart1,
				pictureEditTabCClipart2,
			});

			Application.DoEvents();

			var users = ClosersContentContainer.SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name);
			comboBoxEditTabCCombo1.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo2.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo3.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo4.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo5.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo6.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo7.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo8.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo9.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo10.Properties.Items.AddRange(users);
			comboBoxEditTabCCombo11.Properties.Items.AddRange(users);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabCClipart1.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart1 ??
				pictureEditTabCClipart1.Image;
			pictureEditTabCClipart2.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart2 ??
				pictureEditTabCClipart2.Image;

			memoEditTabCSubheader1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader1 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader2 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader2DefaultValue;

			comboBoxEditTabCCombo1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo1;
			comboBoxEditTabCCombo2.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo2;
			comboBoxEditTabCCombo3.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo3;
			comboBoxEditTabCCombo4.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo4;
			comboBoxEditTabCCombo5.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo5;
			comboBoxEditTabCCombo6.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo6;
			comboBoxEditTabCCombo7.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo7;
			comboBoxEditTabCCombo8.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo8;
			comboBoxEditTabCCombo9.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo9;
			comboBoxEditTabCCombo10.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo10;
			comboBoxEditTabCCombo11.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo11;

			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart1 = pictureEditTabCClipart1.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart1Image ?
				pictureEditTabCClipart1.Image :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart2 = pictureEditTabCClipart2.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart2Image ?
				pictureEditTabCClipart2.Image :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader1DefaultValue ?
				memoEditTabCSubheader1.EditValue as String :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader2DefaultValue ?
				memoEditTabCSubheader2.EditValue as String :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo1 =
				comboBoxEditTabCCombo1.EditValue as User ?? (comboBoxEditTabCCombo1.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo1.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo2 =
				comboBoxEditTabCCombo2.EditValue as User ?? (comboBoxEditTabCCombo2.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo2.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo3 =
				comboBoxEditTabCCombo3.EditValue as User ?? (comboBoxEditTabCCombo3.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo3.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo4 =
				comboBoxEditTabCCombo4.EditValue as User ?? (comboBoxEditTabCCombo4.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo4.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo5 =
				comboBoxEditTabCCombo5.EditValue as User ?? (comboBoxEditTabCCombo5.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo5.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo6 =
				comboBoxEditTabCCombo6.EditValue as User ?? (comboBoxEditTabCCombo6.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo6.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo7 =
				comboBoxEditTabCCombo7.EditValue as User ?? (comboBoxEditTabCCombo7.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo7.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo8 =
				comboBoxEditTabCCombo8.EditValue as User ?? (comboBoxEditTabCCombo8.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo8.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo9 =
				comboBoxEditTabCCombo9.EditValue as User ?? (comboBoxEditTabCCombo9.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo9.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo10 =
				comboBoxEditTabCCombo10.EditValue as User ?? (comboBoxEditTabCCombo10.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo10.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo11 =
				comboBoxEditTabCCombo11.EditValue as User ?? (comboBoxEditTabCCombo11.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabCCombo11.EditValue } :
					null);

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ClosersContentContainer.RaiseDataChanged();
		}

		private void OnTabCCombo1EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo1.EditValue as User;
			simpleLabelItemTabCCombo1Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo2EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo2.EditValue as User;
			simpleLabelItemTabCCombo2Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo3EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo3.EditValue as User;
			simpleLabelItemTabCCombo3Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo4EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo4.EditValue as User;
			simpleLabelItemTabCCombo4Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo5EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo5.EditValue as User;
			simpleLabelItemTabCCombo5Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo6EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo6.EditValue as User;
			simpleLabelItemTabCCombo6Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo7EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo7.EditValue as User;
			simpleLabelItemTabCCombo7Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo8EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo8.EditValue as User;
			simpleLabelItemTabCCombo8Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo9EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo9.EditValue as User;
			simpleLabelItemTabCCombo9Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo10EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo10.EditValue as User;
			simpleLabelItemTabCCombo10Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabCCombo11EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabCCombo11.EditValue as User;
			simpleLabelItemTabCCombo11Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}
	}
}
