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
	public partial class ClosersTabCControl : ClosersTabBaseControl
	{
		private readonly List<User> _usersByStation = new List<User>();

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
		public override StarAppOutputType OutputType => StarAppOutputType.ClosersTabC;
		public override String OutputName => ClosersContentContainer.SlideContainer.StarInfo.Titles.Tab11SubCTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = ClosersContentContainer.SelectedTheme;

			var clipart1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart1 ?? ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart1Image;
			if (clipart1 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart1.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP11CCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
			}

			var clipart2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Clipart2 ?? ClosersContentContainer.SlideContainer.StarInfo.Tab11SubCClipart2Image;
			if (clipart2 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart2.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP11CCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
			}

			var textDataItems = new Dictionary<string, string>();

			var slideHeader = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.SlideHeader?.Value ?? ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault)?.Value;
			var subheader1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader1 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader1DefaultValue;
			var subheader2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Subheader2 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartCSubHeader2DefaultValue;

			var combo1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo1;
			var combo2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo2;
			var combo3 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo3;
			var combo4 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo4;
			var combo5 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo5;
			var combo6 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo6;
			var combo7 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo7;
			var combo8 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo8;
			var combo9 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo9;
			var combo10 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo10;
			var combo11 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabC.Combo11;

			textDataItems.Add("CP11CHEADER".ToUpper(), slideHeader);
			textDataItems.Add("HEADER".ToUpper(), slideHeader);

			if (!String.IsNullOrWhiteSpace(subheader1))
				textDataItems.Add("CP11CSubHeader1".ToUpper(), subheader1);
			if (!String.IsNullOrWhiteSpace(subheader2))
				textDataItems.Add("CP11CSubHeader2".ToUpper(), subheader1);

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
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-12.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11CCombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11CCombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11CCombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11CCombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11CCombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11CCombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));

				textDataItems.Add("CP11CCombo9".ToUpper(), combo9.ToString());
				textDataItems.Add("CP11CCombo9LinkText9".ToUpper(), String.Join("        ", combo9.Email, combo9.Phone));

				textDataItems.Add("CP11CCombo10".ToUpper(), combo10.ToString());
				textDataItems.Add("CP11CCombo10LinkText10".ToUpper(), String.Join("        ", combo10.Email, combo10.Phone));

				textDataItems.Add("CP11CCombo11".ToUpper(), combo11.ToString());
				textDataItems.Add("CP11CCombo11LinkText11".ToUpper(), String.Join("        ", combo11.Email, combo11.Phone));
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
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-13.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11CCombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11CCombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11CCombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11CCombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11CCombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11CCombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));

				textDataItems.Add("CP11CCombo9".ToUpper(), combo9.ToString());
				textDataItems.Add("CP11CCombo9LinkText9".ToUpper(), String.Join("        ", combo9.Email, combo9.Phone));

				textDataItems.Add("CP11CCombo10".ToUpper(), combo10.ToString());
				textDataItems.Add("CP11CCombo10LinkText10".ToUpper(), String.Join("        ", combo10.Email, combo10.Phone));
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
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-14.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11CCombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11CCombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11CCombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11CCombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11CCombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11CCombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));

				textDataItems.Add("CP11CCombo9".ToUpper(), combo9.ToString());
				textDataItems.Add("CP11CCombo9LinkText9".ToUpper(), String.Join("        ", combo9.Email, combo9.Phone));
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
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-15.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11CCombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11CCombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11CCombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11CCombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11CCombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11CCombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null &&
					 combo5 != null &&
					 combo6 != null &&
					 combo7 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-16.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11CCombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11CCombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11CCombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11CCombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null &&
					 combo5 != null &&
					 combo6 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-17.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11CCombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11CCombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null &&
					 combo5 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-18.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null &&
					 combo4 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-19.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null &&
					 combo3 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-20.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));
			}
			else if (combo1 != null &&
					 combo2 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-21.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));
			}
			else if (combo1 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-22.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));
			}
			else
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11C-12.pptx");

				textDataItems.Add("CP11CCombo1".ToUpper(), combo1.ToString());
				textDataItems.Add("CP11CCombo1LinkText1".ToUpper(), String.Join("        ", combo1.Email, combo1.Phone));

				textDataItems.Add("CP11CCombo2".ToUpper(), combo2.ToString());
				textDataItems.Add("CP11CCombo2LinkText2".ToUpper(), String.Join("        ", combo2.Email, combo2.Phone));

				textDataItems.Add("CP11CCombo3".ToUpper(), combo3.ToString());
				textDataItems.Add("CP11CCombo3LinkText3".ToUpper(), String.Join("        ", combo3.Email, combo3.Phone));

				textDataItems.Add("CP11CCombo4".ToUpper(), combo4.ToString());
				textDataItems.Add("CP11CCombo4LinkText4".ToUpper(), String.Join("        ", combo4.Email, combo4.Phone));

				textDataItems.Add("CP11CCombo5".ToUpper(), combo5.ToString());
				textDataItems.Add("CP11CCombo5LinkText5".ToUpper(), String.Join("        ", combo5.Email, combo5.Phone));

				textDataItems.Add("CP11CCombo6".ToUpper(), combo6.ToString());
				textDataItems.Add("CP11CCombo6LinkText6".ToUpper(), String.Join("        ", combo6.Email, combo6.Phone));

				textDataItems.Add("CP11CCombo7".ToUpper(), combo7.ToString());
				textDataItems.Add("CP11CCombo7LinkText7".ToUpper(), String.Join("        ", combo7.Email, combo7.Phone));

				textDataItems.Add("CP11CCombo8".ToUpper(), combo8.ToString());
				textDataItems.Add("CP11CCombo8LinkText8".ToUpper(), String.Join("        ", combo8.Email, combo8.Phone));

				textDataItems.Add("CP11CCombo9".ToUpper(), combo9.ToString());
				textDataItems.Add("CP11CCombo9LinkText9".ToUpper(), String.Join("        ", combo9.Email, combo9.Phone));

				textDataItems.Add("CP11CCombo10".ToUpper(), combo10.ToString());
				textDataItems.Add("CP11CCombo10LinkText10".ToUpper(), String.Join("        ", combo10.Email, combo10.Phone));

				textDataItems.Add("CP11CCombo11".ToUpper(), combo11.ToString());
				textDataItems.Add("CP11CCombo11LinkText11".ToUpper(), String.Join("        ", combo11.Email, combo11.Phone));
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
