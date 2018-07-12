using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabBControl : ClosersTabBaseControl
	{
		public ClosersTabBControl(ClosersControl shareContentContainer) : base(shareContentContainer)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart1Image), ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBClipart1Configuration, ClosersContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image), ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBClipart2Configuration, ClosersContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			Application.DoEvents();
			memoEditTabBSubheader1.Properties.NullText = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;
			memoEditTabBSubheader2.Properties.NullText = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2Placeholder ?? memoEditTabBSubheader2.Properties.NullText;
			memoEditTabBSubheader3.Properties.NullText = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3Placeholder ?? memoEditTabBSubheader3.Properties.NullText;

			comboBoxEditTabBCombo1.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo1.Properties.NullText =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo1.Properties.NullText;
			comboBoxEditTabBCombo2.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo2.Properties.NullText =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo2.Properties.NullText;
			comboBoxEditTabBCombo3.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo3.Properties.NullText =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo3.Properties.NullText;
			comboBoxEditTabBCombo4.Properties.Items.AddRange(ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo4.Properties.NullText =
				ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo4.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1);
			clipartEditContainer2.LoadData(ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2);

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

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo1 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo2 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo3 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo3.EditValue ?
				comboBoxEditTabBCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo3.EditValue as String } :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo4 = ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo4.EditValue ?
				comboBoxEditTabBCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo4.EditValue as String } :
				null;

			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue ?
				memoEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue ?
				memoEditTabBSubheader2.EditValue as String ?? String.Empty :
				null;
			ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader3 = memoEditTabBSubheader3.EditValue as String != ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue ?
				memoEditTabBSubheader3.EditValue as String ?? String.Empty :
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

			var clipart1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart1 ?? ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP11BCLIPART1", clipart1);

			var clipart2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Clipart2 ?? ImageClipartObject.FromImage(ClosersContentContainer.SlideContainer.StarInfo.Tab11SubBClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP11BCLIPART2", clipart2);

			var slideHeader = (ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.SlideHeader ?? ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;

			var optionalComboItemKeys = new[]
			{
				"CP11BSubHeader1",
				"CP11BSubHeader3",
				"CP11BSubHeader5"
			};
			var optionalComboItemValues = new List<string>();
			var combo1 = (ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo1 ??
						 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo1Items.FirstOrDefault(item => item.IsDefault))?.Value;
			var combo2 = (ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo2 ??
						 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo2Items.FirstOrDefault(item => item.IsDefault))?.Value;
			if (!String.IsNullOrWhiteSpace(combo2))
				optionalComboItemValues.Add(combo2);
			var combo3 = (ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo3 ??
						 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo3Items.FirstOrDefault(item => item.IsDefault))?.Value;
			if (!String.IsNullOrWhiteSpace(combo3))
				optionalComboItemValues.Add(combo3);
			var combo4 = (ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Combo4 ??
						 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBCombo4Items.FirstOrDefault(item => item.IsDefault))?.Value;
			if (!String.IsNullOrWhiteSpace(combo4))
				optionalComboItemValues.Add(combo4);

			var subheader1 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader1 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader1DefaultValue;
			var subheader2 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader2 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader2DefaultValue;
			var subheader3 = ClosersContentContainer.SlideContainer.EditedContent.ClosersState.TabB.Subheader3 ??
							 ClosersContentContainer.SlideContainer.StarInfo.ClosersConfiguration.PartBSubHeader3DefaultValue;

			outputDataPackage.TextItems.Add("CP11BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

			outputDataPackage.TextItems.Add("CP11BCombo1".ToUpper(), combo1);
			outputDataPackage.TextItems.Add("CP11BSubHeader2".ToUpper(), subheader1);
			outputDataPackage.TextItems.Add("CP11BSubHeader4".ToUpper(), subheader2);
			outputDataPackage.TextItems.Add("CP11BSubHeader6".ToUpper(), subheader3);

			switch (optionalComboItemValues.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-3.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-2.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-1.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-3.pptx");
					break;
			}

			for (var i = 0; i < optionalComboItemKeys.Length; i++)
				outputDataPackage.TextItems.Add(optionalComboItemKeys[i].ToUpper(), optionalComboItemValues.ElementAtOrDefault(i) ?? String.Empty);

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
