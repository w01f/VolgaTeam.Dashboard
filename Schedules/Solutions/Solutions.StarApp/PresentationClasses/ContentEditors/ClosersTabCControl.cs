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
			//pictureEditTabCClipart1.Properties.PictureAlignment =
			//	ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart2Image;
			//pictureEditTabCClipart2.Properties.PictureAlignment =
			//	ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart2Configuration.Alignment;
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
