using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabCControl : ClosersTabBaseControl
	{
		private readonly List<User> _usersByStation = new List<User>();

		public ClosersTabCControl(IClosersTabPageContainer shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			memoEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo8.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo10.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo11.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart1Image), ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCClipart1Configuration, ClosersContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart2Image), ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCClipart2Configuration, ClosersContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			memoEditTabCSubheader1.Properties.NullText = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader1Placeholder ?? memoEditTabCSubheader1.Properties.NullText;
			memoEditTabCSubheader2.Properties.NullText = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader2Placeholder ?? memoEditTabCSubheader2.Properties.NullText;

			_usersByStation.AddRange(ClosersContentContainer.SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditTabCCombo1.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo2.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo3.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo4.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo5.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo6.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo7.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo8.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo9.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo10.Properties.Items.AddRange(_usersByStation);
			comboBoxEditTabCCombo11.Properties.Items.AddRange(_usersByStation);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart1);
			clipartEditContainer2.LoadData(ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart2);

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

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader1DefaultValue ?
				memoEditTabCSubheader1.EditValue as String ?? String.Empty :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader2DefaultValue ?
				memoEditTabCSubheader2.EditValue as String ?? String.Empty :
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

		#region Event Handlers
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
		#endregion

		#region Output
		public override String OutputName => ClosersContentContainer.SlideContainer.StarInfo.Titles.Tab11SubCTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = ClosersContentContainer.SelectedTheme;

			var clipart1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart1 ?? ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP11CCLIPART1", clipart1);

			var clipart2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart2 ?? ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP11CCLIPART2", clipart2);

			var slideHeader = (ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.SlideHeader ?? ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader1 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader1DefaultValue;
			var subHeader2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader2 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader2DefaultValue;

			var activeUsers = new[]
				{
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo1,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo2,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo3,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo4,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo5,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo6,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo7,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo8,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo9,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo10,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo11,
				}.Where(user => !String.IsNullOrWhiteSpace(user?.ToString()))
				.ToList();

			switch (activeUsers.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-22.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-21.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-20.pptx");
					break;
				case 4:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-19.pptx");
					break;
				case 5:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-18.pptx");
					break;
				case 6:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-17.pptx");
					break;
				case 7:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-16.pptx");
					break;
				case 8:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-15.pptx");
					break;
				case 9:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-14.pptx");
					break;
				case 10:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-13.pptx");
					break;
				case 11:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-12.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-22.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("CP11CHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP11CSubHeader1".ToUpper(), subHeader1);
			outputDataPackage.TextItems.Add("CP11CSubHeader2".ToUpper(), subHeader2);

			for (var i = 0; i < 11; i++)
			{
				var user = activeUsers.ElementAtOrDefault(i);
				if (user != null)
				{
					outputDataPackage.TextItems.Add(String.Format("CP11CCombo{0}", i + 1).ToUpper(), user.ToString());
					outputDataPackage.TextItems.Add(String.Format("CP11CCombo{0}LinkText{0}", i + 1).ToUpper(), String.Join("        ", user.Email, user.Phone));
				}
			}

			return outputDataPackage;
		}
		#endregion
	}
}
