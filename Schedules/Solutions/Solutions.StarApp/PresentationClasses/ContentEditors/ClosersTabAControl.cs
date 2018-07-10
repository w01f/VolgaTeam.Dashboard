using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabAControl : ClosersTabBaseControl
	{
		private readonly List<User> _usersByStation = new List<User>();

		public ClosersTabAControl(ClosersControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo8.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo10.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo11.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart1Image), ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartAClipart1Configuration, ClosersContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart2Image), ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartAClipart2Configuration, ClosersContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			Application.DoEvents();

			memoEditTabASubheader1.Properties.NullText = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;

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

			clipartEditContainer1.LoadData(ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart1);
			clipartEditContainer2.LoadData(ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart2);

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

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartASubHeader1DefaultValue ?
				memoEditTabASubheader1.EditValue as String ?? String.Empty :
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

		#region Event Handlers
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
		#endregion

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ClosersTabA;
		public override String OutputName => ClosersContentContainer.SlideContainer.StarInfo.Titles.Tab11SubATitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = ClosersContentContainer.SelectedTheme;

			var clipart1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart1 ?? ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP11ACLIPART1", clipart1);

			var clipart2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart2 ?? ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP11ACLIPART2", clipart2);

			var slideHeader = (ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.SlideHeader ?? ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Subheader1 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartASubHeader1DefaultValue;

			var activeUsers = new[]
				{
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo1,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo2,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo3,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo4,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo5,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo6,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo7,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo8,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo9,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo10,
					ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo11,
				}.Where(user => !String.IsNullOrWhiteSpace(user?.ToString()))
				.ToList();

			switch (activeUsers.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-22.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-21.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-20.pptx");
					break;
				case 4:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-19.pptx");
					break;
				case 5:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-18.pptx");
					break;
				case 6:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-17.pptx");
					break;
				case 7:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-16.pptx");
					break;
				case 8:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-15.pptx");
					break;
				case 9:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-14.pptx");
					break;
				case 10:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-13.pptx");
					break;
				case 11:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-12.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-22.pptx");
					break;

			}

			outputDataPackage.TextItems.Add("CP11AHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP11ASubHeader1".ToUpper(), subHeader1);

			for (var i = 0; i < 11; i++)
			{
				var user = activeUsers.ElementAtOrDefault(i);
				if (user != null)
				{
					outputDataPackage.TextItems.Add(String.Format("CP11ACombo{0}", i + 1).ToUpper(), user.ToString());
					outputDataPackage.TextItems.Add(String.Format("CP11ACombo{0}LinkText{0}", i + 1).ToUpper(), String.Join("        ", user.Email, user.Phone));
				}
			}

			return outputDataPackage;
		}

		public override void GenerateOutput()
		{
			var outputDataPackage = GetOutputData();
			ClosersContentContainer.SlideContainer.PowerPointProcessor.AppendStarCommonSlide(outputDataPackage);
		}

		public override PreviewGroup GeneratePreview()
		{
			var outputDataPackage = GetOutputData();
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			ClosersContentContainer.SlideContainer.PowerPointProcessor.PrepareStarCommonSlide(outputDataPackage, tempFileName);
			return new PreviewGroup { Name = OutputName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}
