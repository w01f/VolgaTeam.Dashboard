using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabAControl : ClosersTabBaseControl
	{
		private readonly List<User> _usersByStation = new List<User>();

		public ClosersTabAControl(ClosersControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			memoEditTabASubheader1.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabAClipart1.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartAClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabAClipart2,
			});

			Application.DoEvents();

			_usersByStation.AddRange(ClosersContentContainer.SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditTabACombo1.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo2.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo3.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo4.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo5.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo6.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo7.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo8.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo9.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo10.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabACombo11.Properties.Items.AddRange(_usersByStation);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabAClipart1.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			pictureEditTabAClipart2.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart2 ??
				pictureEditTabAClipart2.Image;

			memoEditTabASubheader1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Subheader1 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartASubHeader1DefaultValue;

			comboBoxEditTabACombo1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo1;
			comboBoxEditTabACombo2.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo2;
			comboBoxEditTabACombo3.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo3;
			comboBoxEditTabACombo4.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo4;
			comboBoxEditTabACombo5.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo5;
			comboBoxEditTabACombo6.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo6;
			comboBoxEditTabACombo7.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo7;
			comboBoxEditTabACombo8.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo8;
			comboBoxEditTabACombo9.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo9;
			comboBoxEditTabACombo10.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo10;
			comboBoxEditTabACombo11.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo11;

			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart1 = pictureEditTabAClipart1.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart1Image ?
				pictureEditTabAClipart1.Image :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart2 = pictureEditTabAClipart2.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart2Image ?
				pictureEditTabAClipart2.Image :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartASubHeader1DefaultValue ?
				memoEditTabASubheader1.EditValue as String :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo1 =
				comboBoxEditTabACombo1.EditValue as User ?? (comboBoxEditTabACombo1.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo1.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo2 =
				comboBoxEditTabACombo2.EditValue as User ?? (comboBoxEditTabACombo2.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo2.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo3 =
				comboBoxEditTabACombo3.EditValue as User ?? (comboBoxEditTabACombo3.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo3.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo4 =
				comboBoxEditTabACombo4.EditValue as User ?? (comboBoxEditTabACombo4.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo4.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo5 =
				comboBoxEditTabACombo5.EditValue as User ?? (comboBoxEditTabACombo5.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo5.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo6 =
				comboBoxEditTabACombo6.EditValue as User ?? (comboBoxEditTabACombo6.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo6.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo7 =
				comboBoxEditTabACombo7.EditValue as User ?? (comboBoxEditTabACombo7.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo7.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo8 =
				comboBoxEditTabACombo8.EditValue as User ?? (comboBoxEditTabACombo8.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo8.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo9 =
				comboBoxEditTabACombo9.EditValue as User ?? (comboBoxEditTabACombo9.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo9.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo10 =
				comboBoxEditTabACombo10.EditValue as User ?? (comboBoxEditTabACombo10.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo10.EditValue } :
					null);
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo11 =
				comboBoxEditTabACombo11.EditValue as User ?? (comboBoxEditTabACombo11.EditValue is String ?
					new User { FirstName = (String)comboBoxEditTabACombo11.EditValue } :
					null);

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ClosersContentContainer.RaiseDataChanged();
		}

		private void OnTabACombo1EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo1.EditValue as User;
			simpleLabelItemTabACombo1Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo2EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo2.EditValue as User;
			simpleLabelItemTabACombo2Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo3EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo3.EditValue as User;
			simpleLabelItemTabACombo3Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo4EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo4.EditValue as User;
			simpleLabelItemTabACombo4Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo5EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo5.EditValue as User;
			simpleLabelItemTabACombo5Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo6EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo6.EditValue as User;
			simpleLabelItemTabACombo6Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo7EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo7.EditValue as User;
			simpleLabelItemTabACombo7Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo8EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo8.EditValue as User;
			simpleLabelItemTabACombo8Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo9EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo9.EditValue as User;
			simpleLabelItemTabACombo9Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo10EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo10.EditValue as User;
			simpleLabelItemTabACombo10Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}

		private void OnTabACombo11EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditTabACombo11.EditValue as User;
			simpleLabelItemTabACombo11Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
		}
	}
}
