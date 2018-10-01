using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Contract;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabD;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract
{
	[ToolboxItem(false)]
	public sealed partial class ContractTabDControl : ChildTabBaseControl
	{
		private ContractTabDInfo CustomTabInfo => (ContractTabDInfo)TabInfo;

		public ContractTabDControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditUserName.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.EditorNameConfiguration);
			textEditUserDescription.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.EditorDescriptionConfiguration);
			textEditUserEmail.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.EditorEmailConfiguration);
			textEditUserPhone.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.EditorPhoneConfiguration);

			comboBoxEditUserName.Properties.Items.Clear();
			comboBoxEditUserName.Properties.Items.AddRange(CustomTabInfo.UserList);

			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ContractState.TabD.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ContractState.TabD.Clipart3);

			comboBoxEditUserName.EditValue = SlideContainer.EditedContent.ContractState.TabD.User ?? CustomTabInfo.UserList.FirstOrDefault();
			FillUserData();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ContractState.TabD.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ContractState.TabD.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ContractState.TabD.User = new UserData
			{
				Name = comboBoxEditUserName.EditValue?.ToString(),
				Description = textEditUserDescription.EditValue as String,
				Email = textEditUserEmail.EditValue as String,
				Phone = textEditUserPhone.EditValue as String,
				ImageFilePath = pictureEditClipart1.Tag as String
			};

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ContractState.TabD.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ContractState.TabD.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void FillUserData()
		{
			var userData = comboBoxEditUserName.EditValue as UserData;
			if (userData != null)
			{
				if (File.Exists(userData.ImageFilePath))
				{
					pictureEditClipart1.Image = Image.FromFile(userData.ImageFilePath);
					pictureEditClipart1.Tag = userData.ImageFilePath;
				}
				else
				{
					pictureEditClipart1.Image = null;
					pictureEditClipart1.Tag = null;
				}

				textEditUserDescription.EditValue = userData.Description;
				textEditUserEmail.EditValue = userData.Email;
				textEditUserPhone.EditValue = userData.Phone;
			}
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnUserNameEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				_allowToSave = false;
				FillUserData();
				_allowToSave = true;
			}
			OnEditValueChanged(sender, e);
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftContract;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftContractFile("d_agreement_closing.pptx");

			var clipart2 = SlideContainer.EditedContent.ContractState.TabD.Clipart2 ??
			               ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT15DCLIPART2", clipart2);
			var clipart3 = SlideContainer.EditedContent.ContractState.TabD.Clipart3 ??
			               ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("SHIFT15DCLIPART3", clipart3);

			var userData = SlideContainer.EditedContent.ContractState.TabD.User ?? CustomTabInfo.UserList.FirstOrDefault();
			if (userData != null)
			{
				var clipart1 = ImageClipartObject.FromFile(userData?.ImageFilePath);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("SHIFT15DCLIPART1", clipart1);

				outputDataPackage.TextItems.Add("SHIFT15DHeader".ToUpper(), userData.Name);
				outputDataPackage.TextItems.Add("SHIFT15DLinkText1".ToUpper(), userData.Description);
				outputDataPackage.TextItems.Add("SHIFT15DLinkText2".ToUpper(), userData.Email);
				outputDataPackage.TextItems.Add("SHIFT15DLinkText3".ToUpper(), userData.Phone);
			}

			return outputDataPackage;
		}
		#endregion
	}
}