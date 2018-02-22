using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabAControl : ClosersTabBaseControl
	{
		public ClosersTabAControl(ClosersControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			memoEditTabASubheader1.EnableSelectAll();
			Application.DoEvents();

			pictureEditTabAClipart1.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart1Image;
			//pictureEditTabAClipart1.Properties.PictureAlignment =
			//	ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart2Image;
			//pictureEditTabAClipart2.Properties.PictureAlignment =
			//	ShareContentContainer.SlideContainer.StarInfo.ShareConfiguration.PartAClipart2Configuration.Alignment;
			Application.DoEvents();

			var users = ClosersContentContainer.SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name);
			comboBoxEditTabACombo1.Properties.Items.AddRange(users);
			comboBoxEditTabACombo2.Properties.Items.AddRange(users);
			comboBoxEditTabACombo3.Properties.Items.AddRange(users);
			comboBoxEditTabACombo4.Properties.Items.AddRange(users);
			comboBoxEditTabACombo5.Properties.Items.AddRange(users);
			comboBoxEditTabACombo6.Properties.Items.AddRange(users);
			comboBoxEditTabACombo7.Properties.Items.AddRange(users);
			comboBoxEditTabACombo8.Properties.Items.AddRange(users);
			comboBoxEditTabACombo9.Properties.Items.AddRange(users);
			comboBoxEditTabACombo10.Properties.Items.AddRange(users);
			comboBoxEditTabACombo11.Properties.Items.AddRange(users);
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
