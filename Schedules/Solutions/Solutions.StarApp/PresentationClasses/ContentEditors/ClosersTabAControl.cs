using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Dictionaries;
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

			var clipart1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart1 ?? ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart1Image;
			if (clipart1 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart1.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP11ACLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
			}

			var clipart2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Clipart2 ?? ClosersContentContainer.SlideContainer.StarInfo.Tab11SubAClipart2Image;
			if (clipart2 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart2.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP11ACLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
			}

			var textDataItems = new Dictionary<string, string>();

			var slideHeader = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.SlideHeader?.Value ?? ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault)?.Value;
			var subheader1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Subheader1 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartASubHeader1DefaultValue;

			var combo1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo1;
			var combo2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo2;
			var combo3 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo3;
			var combo4 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo4;
			var combo5 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo5;
			var combo6 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo6;
			var combo7 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo7;
			var combo8 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo8;
			var combo9 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo9;
			var combo10 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo10;
			var combo11 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabA.Combo11;

			textDataItems.Add("CP11AHEADER".ToUpper(), slideHeader);
			textDataItems.Add("HEADER".ToUpper(), slideHeader);

			if (!String.IsNullOrWhiteSpace(subheader1))
				textDataItems.Add("CP11ASubHeader1".ToUpper(), subheader1);

			if (combo1 != null &&
				combo2 != null &&
				combo3 != null &&
				combo4 != null &&
				combo5 != null &&
				combo6 != null &&
				combo7 != null &&
				combo8 != null &&
				combo9 != null &&
				combo10 != null &&
				combo11 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-12.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11ACombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11ACombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11ACombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11ACombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11ACombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11ACombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));

				textDataItems.Add("CP11ACombo9".ToUpper(), combo9.ToString());
				textDataItems.Add("CP11ACombo9LinkText9".ToUpper(), String.Join("        ", combo9.Email, combo9.Phone));

				textDataItems.Add("CP11ACombo10".ToUpper(), combo10.ToString());
				textDataItems.Add("CP11ACombo10LinkText10".ToUpper(), String.Join("        ", combo10.Email, combo10.Phone));

				textDataItems.Add("CP11ACombo11".ToUpper(), combo11.ToString());
				textDataItems.Add("CP11ACombo11LinkText11".ToUpper(), String.Join("        ", combo11.Email, combo11.Phone));
			}
			else if (combo1 != null &&
				combo2 != null &&
				combo3 != null &&
				combo4 != null &&
				combo5 != null &&
				combo6 != null &&
				combo7 != null &&
				combo8 != null &&
				combo9 != null &&
				combo10 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-13.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11ACombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11ACombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11ACombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11ACombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11ACombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11ACombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));

				textDataItems.Add("CP11ACombo9".ToUpper(), combo9.ToString());
				textDataItems.Add("CP11ACombo9LinkText9".ToUpper(), String.Join("        ", combo9.Email, combo9.Phone));

				textDataItems.Add("CP11ACombo10".ToUpper(), combo10.ToString());
				textDataItems.Add("CP11ACombo10LinkText10".ToUpper(), String.Join("        ", combo10.Email, combo10.Phone));
			}
			else if (combo1 != null &&
				combo2 != null &&
				combo3 != null &&
				combo4 != null &&
				combo5 != null &&
				combo6 != null &&
				combo7 != null &&
				combo8 != null &&
				combo9 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-14.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11ACombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11ACombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11ACombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11ACombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11ACombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11ACombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));

				textDataItems.Add("CP11ACombo9".ToUpper(), combo9.ToString());
				textDataItems.Add("CP11ACombo9LinkText9".ToUpper(), String.Join("        ", combo9.Email, combo9.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null &&
					 combo5 != null &&
					 combo6 != null &&
					 combo7 != null &&
					 combo8 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-15.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11ACombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11ACombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11ACombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11ACombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11ACombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11ACombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null &&
					 combo5 != null &&
					 combo6 != null &&
					 combo7 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-16.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11ACombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11ACombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11ACombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11ACombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null &&
					 combo5 != null &&
					 combo6 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-17.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11ACombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11ACombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null &&
					 combo5 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-18.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-19.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-20.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-21.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));
			}
			else if (combo1 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-22.pptx");

				textDataItems.Add("CP11ACombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11ACombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));
			}
			else
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11A-12.pptx");

				textDataItems.Add("CP11ACombo2".ToUpper(), combo2?.ToString());
				textDataItems.Add("CP11ACombo2LinkText2".ToUpper(), String.Join("        ", combo2?.Email, combo2?.Phone));

				textDataItems.Add("CP11ACombo3".ToUpper(), combo3?.ToString());
				textDataItems.Add("CP11ACombo3LinkText3".ToUpper(), String.Join("        ", combo3?.Email, combo3?.Phone));

				textDataItems.Add("CP11ACombo4".ToUpper(), combo4?.ToString());
				textDataItems.Add("CP11ACombo4LinkText4".ToUpper(), String.Join("        ", combo4?.Email, combo4?.Phone));

				textDataItems.Add("CP11ACombo5".ToUpper(), combo5?.ToString());
				textDataItems.Add("CP11ACombo5LinkText5".ToUpper(), String.Join("        ", combo5?.Email, combo5?.Phone));

				textDataItems.Add("CP11ACombo6".ToUpper(), combo6?.ToString());
				textDataItems.Add("CP11ACombo6LinkText6".ToUpper(), String.Join("        ", combo6?.Email, combo6?.Phone));

				textDataItems.Add("CP11ACombo7".ToUpper(), combo7?.ToString());
				textDataItems.Add("CP11ACombo7LinkText7".ToUpper(), String.Join("        ", combo7?.Email, combo7?.Phone));

				textDataItems.Add("CP11ACombo8".ToUpper(), combo8?.ToString());
				textDataItems.Add("CP11ACombo8LinkText8".ToUpper(), String.Join("        ", combo8?.Email, combo8?.Phone));

				textDataItems.Add("CP11ACombo9".ToUpper(), combo9?.ToString());
				textDataItems.Add("CP11ACombo9LinkText9".ToUpper(), String.Join("        ", combo9?.Email, combo9?.Phone));

				textDataItems.Add("CP11ACombo10".ToUpper(), combo10?.ToString());
				textDataItems.Add("CP11ACombo10LinkText10".ToUpper(), String.Join("        ", combo10?.Email, combo10?.Phone));

				textDataItems.Add("CP11ACombo11".ToUpper(), combo11?.ToString());
				textDataItems.Add("CP11ACombo11LinkText11".ToUpper(), String.Join("        ", combo11?.Email, combo11?.Phone));
			}
			outputDataPackage.TextItems = textDataItems;

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
