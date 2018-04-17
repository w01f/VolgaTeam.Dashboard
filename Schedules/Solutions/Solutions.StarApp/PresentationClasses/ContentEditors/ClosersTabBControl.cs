using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

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
			pictureEditTabBClipart1.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
			});

			Application.DoEvents();

			comboBoxEditTabBCombo1.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items);
			comboBoxEditTabBCombo2.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items);
			comboBoxEditTabBCombo3.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items);
			comboBoxEditTabBCombo4.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items);
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabBClipart1.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;

			comboBoxEditTabBCombo1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo1 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo2 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo3.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo3 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo4.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo4 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(item => item.IsDefault);

			memoEditTabBSubheader1.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader1 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader2 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue;
			memoEditTabBSubheader3.EditValue = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader3 ??
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue;
			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1 = pictureEditTabBClipart1.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart1Image ?
				pictureEditTabBClipart1.Image :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2 = pictureEditTabBClipart2.Image != ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image ?
				pictureEditTabBClipart2.Image :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo1 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? (comboBoxEditTabBCombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo1.EditValue } : null) :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo2 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? (comboBoxEditTabBCombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo2.EditValue } : null) :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo3 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo3.EditValue ?
				comboBoxEditTabBCombo3.EditValue as ListDataItem ?? (comboBoxEditTabBCombo3.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo3.EditValue } : null) :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo4 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo4.EditValue ?
				comboBoxEditTabBCombo4.EditValue as ListDataItem ?? (comboBoxEditTabBCombo4.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo4.EditValue } : null) :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue ?
				memoEditTabBSubheader1.EditValue as String :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue ?
				memoEditTabBSubheader2.EditValue as String :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader3 = memoEditTabBSubheader3.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue ?
				memoEditTabBSubheader3.EditValue as String :
				null;

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ClosersContentContainer.RaiseDataChanged();
		}

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ClosersTabB;
		public override String OutputName => ClosersContentContainer.SlideContainer.StarInfo.Titles.Tab11SubBTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = ClosersContentContainer.SelectedTheme;

			var clipart1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1 ?? ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart1Image;
			if (clipart1 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart1.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP11BCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
			}

			var clipart2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2 ?? ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image;
			if (clipart2 != null)
			{
				var fileName = Path.GetTempFileName();
				clipart2.Save(fileName);
				outputDataPackage.ClipartItems.Add("CP11BCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
			}

			var textDataItems = new Dictionary<string, string>();

			var slideHeader = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.SlideHeader?.Value ?? ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault)?.Value;

			var combo1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo1;
			var combo2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo2;
			var combo3 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo3;
			var combo4 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo4;

			var subheader1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader1 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue;
			var subheader2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader2 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue;
			var subheader3 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader3 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue;

			textDataItems.Add("CP11BHEADER".ToUpper(), slideHeader);
			textDataItems.Add("HEADER".ToUpper(), slideHeader);

			if (combo1 != null)
				textDataItems.Add("CP11BCombo1".ToUpper(), combo1.ToString());

			if (combo2 != null &&
				combo3 != null &&
				combo4 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-1.pptx");

				textDataItems.Add("CP11BSubHeader1".ToUpper(), combo2.ToString());
				if (!String.IsNullOrWhiteSpace(subheader1))
					textDataItems.Add("CP11BSubHeader2".ToUpper(), subheader1);

				textDataItems.Add("CP11BSubHeader3".ToUpper(), combo3.ToString());
				if (!String.IsNullOrWhiteSpace(subheader2))
					textDataItems.Add("CP11BSubHeader4".ToUpper(), subheader2);

				textDataItems.Add("CP11BSubHeader5".ToUpper(), combo4.ToString());
				if (!String.IsNullOrWhiteSpace(subheader3))
					textDataItems.Add("CP11BSubHeader6".ToUpper(), subheader3);
			}
			else if (combo2 != null &&
					 combo3 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-2.pptx");

				textDataItems.Add("CP11BSubHeader1".ToUpper(), combo2.ToString());
				if (!String.IsNullOrWhiteSpace(subheader1))
					textDataItems.Add("CP11BSubHeader2".ToUpper(), subheader1);

				textDataItems.Add("CP11BSubHeader3".ToUpper(), combo3.ToString());
				if (!String.IsNullOrWhiteSpace(subheader2))
					textDataItems.Add("CP11BSubHeader4".ToUpper(), subheader2);
			}
			else if (combo2 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-3.pptx");

				textDataItems.Add("CP11BSubHeader1".ToUpper(), combo2.ToString());
				if (!String.IsNullOrWhiteSpace(subheader1))
					textDataItems.Add("CP11BSubHeader2".ToUpper(), subheader1);
			}
			else
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-1.pptx");

				textDataItems.Add("CP11BSubHeader1".ToUpper(), combo2.ToString());
				if (!String.IsNullOrWhiteSpace(subheader1))
					textDataItems.Add("CP11BSubHeader2".ToUpper(), subheader1);

				textDataItems.Add("CP11BSubHeader3".ToUpper(), combo3.ToString());
				if (!String.IsNullOrWhiteSpace(subheader2))
					textDataItems.Add("CP11BSubHeader4".ToUpper(), subheader2);

				textDataItems.Add("CP11BSubHeader5".ToUpper(), combo4.ToString());
				if (!String.IsNullOrWhiteSpace(subheader3))
					textDataItems.Add("CP11BSubHeader6".ToUpper(), subheader3);
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
