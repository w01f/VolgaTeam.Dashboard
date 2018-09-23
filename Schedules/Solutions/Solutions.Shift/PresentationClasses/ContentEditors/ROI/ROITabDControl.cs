using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.ROI;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.ROI
{
	public partial class ROITabDControl : ChildTabBaseControl
	{
		private ROITabDInfo CustomTabInfo => (ROITabDInfo)TabInfo;

		public ROITabDControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			textEditTabDSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader2.EnableSelectAll();
			textEditTabDSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader4.EnableSelectAll();
			textEditTabDSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader6.EnableSelectAll();
			textEditTabDSubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader8.EnableSelectAll();
			textEditTabDSubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader10.EnableSelectAll();
			textEditTabDSubheader11.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader12.EnableSelectAll();
			textEditTabDSubheader13.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader14.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabDSubheader15.EnableSelectAll();
			textEditTabDSubheader16.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabDSubheader17.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			textEditTabDSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabDSubheader1.Properties.NullText;
			textEditTabDSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabDSubheader3.Properties.NullText;
			textEditTabDSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? textEditTabDSubheader5.Properties.NullText;
			textEditTabDSubheader7.Properties.NullText = CustomTabInfo.SubHeader7Placeholder ?? textEditTabDSubheader7.Properties.NullText;
			textEditTabDSubheader9.Properties.NullText = CustomTabInfo.SubHeader9Placeholder ?? textEditTabDSubheader9.Properties.NullText;
			textEditTabDSubheader11.Properties.NullText = CustomTabInfo.SubHeader11Placeholder ?? textEditTabDSubheader11.Properties.NullText;
			textEditTabDSubheader13.Properties.NullText = CustomTabInfo.SubHeader13Placeholder ?? textEditTabDSubheader13.Properties.NullText;
			textEditTabDSubheader14.Properties.NullText = CustomTabInfo.SubHeader14Placeholder ?? textEditTabDSubheader14.Properties.NullText;
			textEditTabDSubheader16.Properties.NullText = CustomTabInfo.SubHeader16Placeholder ?? textEditTabDSubheader16.Properties.NullText;
			textEditTabDSubheader17.Properties.NullText = CustomTabInfo.SubHeader17Placeholder ?? textEditTabDSubheader17.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ROIState.TabD.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ROIState.TabD.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.ROIState.TabD.Clipart3);

			checkEditTabDGroup1.Checked = SlideContainer.EditedContent.ROIState.TabD.Group1Toggle;
			checkEditTabDGroup2.Checked = SlideContainer.EditedContent.ROIState.TabD.Group2Toggle;
			checkEditTabDGroup3.Checked = SlideContainer.EditedContent.ROIState.TabD.Group3Toggle;
			checkEditTabDGroup4.Checked = SlideContainer.EditedContent.ROIState.TabD.Group4Toggle;
			checkEditTabDGroup5.Checked = SlideContainer.EditedContent.ROIState.TabD.Group5Toggle;
			checkEditTabDGroup6.Checked = SlideContainer.EditedContent.ROIState.TabD.Group6Toggle;
			checkEditTabDGroup7.Checked = SlideContainer.EditedContent.ROIState.TabD.Group7Toggle;
			checkEditTabDGroup8.Checked = SlideContainer.EditedContent.ROIState.TabD.Group8Toggle;
			checkEditTabDGroup9.Checked = SlideContainer.EditedContent.ROIState.TabD.Group9Toggle;
			checkEditTabDGroup10.Checked = SlideContainer.EditedContent.ROIState.TabD.Group10Toggle;
			checkEditTabDSubheader2.Checked = SlideContainer.EditedContent.ROIState.TabD.Subheader2Toggle;
			checkEditTabDSubheader4.Checked = SlideContainer.EditedContent.ROIState.TabD.Subheader4Toggle;
			checkEditTabDSubheader6.Checked = SlideContainer.EditedContent.ROIState.TabD.Subheader6Toggle;
			checkEditTabDSubheader8.Checked = SlideContainer.EditedContent.ROIState.TabD.Subheader8Toggle;
			checkEditTabDSubheader10.Checked = SlideContainer.EditedContent.ROIState.TabD.Subheader10Toggle;
			checkEditTabDSubheader12.Checked = SlideContainer.EditedContent.ROIState.TabD.Subheader12Toggle;
			checkEditTabDSubheader15.Checked = SlideContainer.EditedContent.ROIState.TabD.Subheader15Toggle;
			checkEditTabDFormula1.Checked = SlideContainer.EditedContent.ROIState.TabD.Formula1Toggle;
			checkEditTabDFormula2.Checked = SlideContainer.EditedContent.ROIState.TabD.Formula2Toggle;
			checkEditTabDFormula3.Checked = SlideContainer.EditedContent.ROIState.TabD.Formula3Toggle;

			textEditTabDSubheader1.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			spinEditTabDSubheader2.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			textEditTabDSubheader3.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			spinEditTabDSubheader4.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader4 ??
				CustomTabInfo.SubHeader4DefaultValue;
			textEditTabDSubheader5.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader5 ??
				CustomTabInfo.SubHeader5DefaultValue;
			spinEditTabDSubheader6.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader6 ??
				CustomTabInfo.SubHeader6DefaultValue;
			textEditTabDSubheader7.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader7 ??
				CustomTabInfo.SubHeader7DefaultValue;
			spinEditTabDSubheader8.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader8 ??
				CustomTabInfo.SubHeader8DefaultValue;
			textEditTabDSubheader9.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader9 ??
				CustomTabInfo.SubHeader9DefaultValue;
			spinEditTabDSubheader10.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader10 ??
				CustomTabInfo.SubHeader10DefaultValue;
			textEditTabDSubheader11.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader11 ??
				CustomTabInfo.SubHeader11DefaultValue;
			spinEditTabDSubheader12.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader12 ??
				CustomTabInfo.SubHeader12DefaultValue;
			textEditTabDSubheader13.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader13 ??
				CustomTabInfo.SubHeader13DefaultValue;
			textEditTabDSubheader14.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader14 ??
				CustomTabInfo.SubHeader14DefaultValue;
			spinEditTabDSubheader15.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader15 ??
				CustomTabInfo.SubHeader15DefaultValue;
			textEditTabDSubheader16.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader16 ??
				CustomTabInfo.SubHeader16DefaultValue;
			textEditTabDSubheader17.EditValue = SlideContainer.EditedContent.ROIState.TabD.Subheader17 ??
				CustomTabInfo.SubHeader17DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabDFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ROIState.TabD.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabD.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.ROIState.TabD.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			SlideContainer.EditedContent.ROIState.TabD.Group1Toggle = checkEditTabDGroup1.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group2Toggle = checkEditTabDGroup2.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group3Toggle = checkEditTabDGroup3.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group4Toggle = checkEditTabDGroup4.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group5Toggle = checkEditTabDGroup5.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group6Toggle = checkEditTabDGroup6.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group7Toggle = checkEditTabDGroup7.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group8Toggle = checkEditTabDGroup8.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group9Toggle = checkEditTabDGroup9.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Group10Toggle = checkEditTabDGroup10.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Subheader2Toggle = checkEditTabDSubheader2.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Subheader4Toggle = checkEditTabDSubheader4.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Subheader6Toggle = checkEditTabDSubheader6.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Subheader8Toggle = checkEditTabDSubheader8.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Subheader10Toggle = checkEditTabDSubheader10.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Subheader12Toggle = checkEditTabDSubheader12.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Subheader15Toggle = checkEditTabDSubheader15.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Formula1Toggle = checkEditTabDFormula1.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Formula2Toggle = checkEditTabDFormula2.Checked;
			SlideContainer.EditedContent.ROIState.TabD.Formula3Toggle = checkEditTabDFormula3.Checked;

			SlideContainer.EditedContent.ROIState.TabD.Subheader1 = textEditTabDSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabDSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader2 = (decimal?)spinEditTabDSubheader2.EditValue != CustomTabInfo.SubHeader2DefaultValue ?
				(decimal?)spinEditTabDSubheader2.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader3 = textEditTabDSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabDSubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader4 = (decimal?)spinEditTabDSubheader4.EditValue != CustomTabInfo.SubHeader4DefaultValue ?
				(decimal?)spinEditTabDSubheader4.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader5 = textEditTabDSubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue ?
				textEditTabDSubheader5.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader6 = (decimal?)spinEditTabDSubheader6.EditValue != CustomTabInfo.SubHeader6DefaultValue ?
				(decimal?)spinEditTabDSubheader6.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader7 = textEditTabDSubheader7.EditValue as String != CustomTabInfo.SubHeader7DefaultValue ?
				textEditTabDSubheader7.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader8 = (decimal?)spinEditTabDSubheader8.EditValue != CustomTabInfo.SubHeader8DefaultValue ?
				(decimal?)spinEditTabDSubheader8.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader9 = textEditTabDSubheader9.EditValue as String != CustomTabInfo.SubHeader9DefaultValue ?
				textEditTabDSubheader9.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader10 = (decimal?)spinEditTabDSubheader10.EditValue != CustomTabInfo.SubHeader10DefaultValue ?
				(decimal?)spinEditTabDSubheader10.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader11 = textEditTabDSubheader11.EditValue as String != CustomTabInfo.SubHeader11DefaultValue ?
				textEditTabDSubheader11.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader12 = (decimal?)spinEditTabDSubheader12.EditValue != CustomTabInfo.SubHeader12DefaultValue ?
				(decimal?)spinEditTabDSubheader12.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader13 = textEditTabDSubheader13.EditValue as String != CustomTabInfo.SubHeader13DefaultValue ?
				textEditTabDSubheader13.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader14 = textEditTabDSubheader14.EditValue as String != CustomTabInfo.SubHeader14DefaultValue ?
				textEditTabDSubheader14.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader15 = (decimal?)spinEditTabDSubheader15.EditValue != CustomTabInfo.SubHeader15DefaultValue ?
				(decimal?)spinEditTabDSubheader15.EditValue :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader16 = textEditTabDSubheader16.EditValue as String != CustomTabInfo.SubHeader16DefaultValue ?
				textEditTabDSubheader16.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ROIState.TabD.Subheader17 = textEditTabDSubheader17.EditValue as String != CustomTabInfo.SubHeader17DefaultValue ?
				textEditTabDSubheader17.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ROIState.TabD.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ROIState.TabD.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnTabDGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup1Inner.Enabled = checkEditTabDGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader2Value.Enabled = checkEditTabDSubheader2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup2Inner.Enabled = checkEditTabDGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader4Value.Enabled = checkEditTabDSubheader4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup3Inner.Enabled = checkEditTabDGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader6Value.Enabled = checkEditTabDSubheader6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup4Inner.Enabled = checkEditTabDGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader8Value.Enabled = checkEditTabDSubheader8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup5Inner.Enabled = checkEditTabDGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader10Value.Enabled = checkEditTabDSubheader10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup6Inner.Enabled = checkEditTabDGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader12CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader12Value.Enabled = checkEditTabDSubheader12.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup7Inner.Enabled = checkEditTabDGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormula1CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula1.Enabled = checkEditTabDFormula1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup8Inner.Enabled = checkEditTabDGroup8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDSubheader15CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabDSubheader15Value.Enabled = checkEditTabDSubheader15.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup9CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup9Inner.Enabled = checkEditTabDGroup9.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormula2CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula2.Enabled = checkEditTabDFormula2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDGroup10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabDGroup10Inner.Enabled = checkEditTabDGroup10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormula3CheckedChanged(object sender, EventArgs e)
		{
			simpleLabelItemTabDFormula3.Enabled = checkEditTabDFormula3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabDFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var monthlyInvestmentValue = (double)spinEditTabDSubheader2.Value;
			var avgSaleValue = (double)spinEditTabDSubheader4.Value;
			var salePercent = (double)spinEditTabDSubheader6.Value;
			var closingPercent = (double)spinEditTabDSubheader8.Value;
			var monthlyGoal = (double)spinEditTabDSubheader15.Value;

			var formula1Value = avgSaleValue > 0 && salePercent > 0 ? Math.Ceiling(monthlyInvestmentValue / (avgSaleValue * salePercent / 100)) : 0;
			var formula2Value = closingPercent > 0 ? Math.Ceiling(formula1Value / (closingPercent / 100)) : 0;
			var formula3Value = closingPercent > 0 ? Math.Ceiling(monthlyGoal / (closingPercent / 100)) : 0;

			simpleLabelItemTabDFormula1.CustomizationFormText = String.Format("{0:#,##0}", formula1Value);
			simpleLabelItemTabDFormula1.Text = String.Format("<b>{0:#,##0}</b>", formula1Value);
			simpleLabelItemTabDFormula2.CustomizationFormText = String.Format("{0:#,##0}", formula2Value);
			simpleLabelItemTabDFormula2.Text = String.Format("<b>{0:#,##0}</b>", formula2Value);
			simpleLabelItemTabDFormula3.CustomizationFormText = String.Format("{0:#,##0}", formula3Value);
			simpleLabelItemTabDFormula3.Text = String.Format("<b>{0:#,##0}</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override SlideType SlideType => SlideType.ShiftROI;

		public override bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetShiftROIFile("roi_d.pptx");
			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.ROIState.TabD.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP06DCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ROIState.TabD.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP06DCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.ROIState.TabD.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP06DCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (SlideContainer.EditedContent.ROIState.TabD.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP06DHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		private Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (SlideContainer.EditedContent.ROIState.TabD.Group1Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase1".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Subheader2Toggle)
					textDataItems.Add("CP06DFormulaPhrase2".ToUpper(), (SlideContainer.EditedContent.ROIState.TabD.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue ?? 0).ToString("$#,##0"));
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group2Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase3".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Subheader4Toggle)
					textDataItems.Add("CP06DFormulaPhrase4".ToUpper(), (SlideContainer.EditedContent.ROIState.TabD.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue ?? 0).ToString("$#,##0"));
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group3Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase5".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Subheader6Toggle)
					textDataItems.Add("CP06DFormulaPhrase6".ToUpper(), (SlideContainer.EditedContent.ROIState.TabD.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue ?? 0).ToString("##0'%'"));
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group4Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase7".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Subheader8Toggle)
					textDataItems.Add("CP06DFormulaPhrase8".ToUpper(), (SlideContainer.EditedContent.ROIState.TabD.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue ?? 0).ToString("##0'%'"));
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group5Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase9".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader9 ?? CustomTabInfo.SubHeader9DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Subheader10Toggle)
					textDataItems.Add("CP06DFormulaPhrase10".ToUpper(), (SlideContainer.EditedContent.ROIState.TabD.Subheader10 ?? CustomTabInfo.SubHeader10DefaultValue ?? 0).ToString("#,##0"));
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group6Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase11".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader11 ?? CustomTabInfo.SubHeader11DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Subheader12Toggle)
					textDataItems.Add("CP06DFormulaPhrase12".ToUpper(), (SlideContainer.EditedContent.ROIState.TabD.Subheader12 ?? CustomTabInfo.SubHeader12DefaultValue ?? 0).ToString("#,##0"));
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group7Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase13".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader13 ?? CustomTabInfo.SubHeader13DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Formula1Toggle)
					textDataItems.Add("CP06DFormulaPhrase14".ToUpper(), simpleLabelItemTabDFormula1.CustomizationFormText);
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group8Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase15".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader14 ?? CustomTabInfo.SubHeader14DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Subheader15Toggle)
					textDataItems.Add("CP06DFormulaPhrase16".ToUpper(), (SlideContainer.EditedContent.ROIState.TabD.Subheader15 ?? CustomTabInfo.SubHeader15DefaultValue ?? 0).ToString("#,##0"));
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group9Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase17".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader16 ?? CustomTabInfo.SubHeader16DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Formula2Toggle)
					textDataItems.Add("CP06DFormulaPhrase18".ToUpper(), simpleLabelItemTabDFormula2.CustomizationFormText);
			}

			if (SlideContainer.EditedContent.ROIState.TabD.Group10Toggle)
			{
				textDataItems.Add("CP06DFormulaPhrase19".ToUpper(), SlideContainer.EditedContent.ROIState.TabD.Subheader17 ?? CustomTabInfo.SubHeader17DefaultValue);
				if (SlideContainer.EditedContent.ROIState.TabD.Formula3Toggle)
					textDataItems.Add("CP06DFormulaPhrase20".ToUpper(), simpleLabelItemTabDFormula3.CustomizationFormText);
			}

			return textDataItems;
		}
		#endregion
	}
}
