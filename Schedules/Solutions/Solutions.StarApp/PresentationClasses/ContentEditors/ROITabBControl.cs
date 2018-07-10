using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabBControl : ROITabBaseControl
	{
		public ROITabBControl(ROIControl roiContentContainer) : base(roiContentContainer)
		{
			InitializeComponent();

			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader2.EnableSelectAll();
			textEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader5.EnableSelectAll();
			textEditTabBSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader8.EnableSelectAll();
			textEditTabBSubheader9.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader10.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader11.EnableSelectAll();
			textEditTabBSubheader12.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader13.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader14.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader15.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader16.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader17.EnableSelectAll();
			textEditTabBSubheader18.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader19.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader20.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader21.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader22.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader23.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			spinEditTabBSubheader24.EnableSelectAll();
			textEditTabBSubheader25.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart1Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart1Configuration, ROIContentContainer);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart2Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart2Configuration, ROIContentContainer);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart3Image), ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBClipart3Configuration, ROIContentContainer);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;

			textEditTabBSubheader1.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			textEditTabBSubheader3.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3Placeholder ?? textEditTabBSubheader3.Properties.NullText;
			textEditTabBSubheader4.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4Placeholder ?? textEditTabBSubheader4.Properties.NullText;
			textEditTabBSubheader6.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6Placeholder ?? textEditTabBSubheader6.Properties.NullText;
			textEditTabBSubheader7.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7Placeholder ?? textEditTabBSubheader7.Properties.NullText;
			textEditTabBSubheader9.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9Placeholder ?? textEditTabBSubheader9.Properties.NullText;
			textEditTabBSubheader10.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10Placeholder ?? textEditTabBSubheader10.Properties.NullText;
			textEditTabBSubheader12.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12Placeholder ?? textEditTabBSubheader12.Properties.NullText;
			textEditTabBSubheader13.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13Placeholder ?? textEditTabBSubheader13.Properties.NullText;
			textEditTabBSubheader14.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14Placeholder ?? textEditTabBSubheader14.Properties.NullText;
			textEditTabBSubheader15.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15Placeholder ?? textEditTabBSubheader15.Properties.NullText;
			textEditTabBSubheader16.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16Placeholder ?? textEditTabBSubheader16.Properties.NullText;
			textEditTabBSubheader18.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18Placeholder ?? textEditTabBSubheader18.Properties.NullText;
			textEditTabBSubheader19.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19Placeholder ?? textEditTabBSubheader19.Properties.NullText;
			textEditTabBSubheader20.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20Placeholder ?? textEditTabBSubheader20.Properties.NullText;
			textEditTabBSubheader21.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21Placeholder ?? textEditTabBSubheader21.Properties.NullText;
			textEditTabBSubheader22.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22Placeholder ?? textEditTabBSubheader22.Properties.NullText;
			textEditTabBSubheader23.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23Placeholder ?? textEditTabBSubheader23.Properties.NullText;
			textEditTabBSubheader25.Properties.NullText = ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25Placeholder ?? textEditTabBSubheader25.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart1);
			clipartEditContainer2.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart2);
			clipartEditContainer3.LoadData(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart3);

			checkEditTabBGroup1.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group1Toggle;
			checkEditTabBGroup2.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group2Toggle;
			checkEditTabBGroup3.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group3Toggle;
			checkEditTabBGroup4.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group4Toggle;
			checkEditTabBGroup5.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group5Toggle;
			checkEditTabBGroup6.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group6Toggle;
			checkEditTabBGroup7.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group7Toggle;
			checkEditTabBGroup8.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group8Toggle;
			checkEditTabBGroup9.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group9Toggle;
			checkEditTabBGroup10.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group10Toggle;
			checkEditTabBGroup11.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group11Toggle;
			checkEditTabBSubheader24.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle;
			checkEditTabBSubheader25.Checked = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle;

			textEditTabBSubheader1.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader1 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1DefaultValue;
			spinEditTabBSubheader2.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader2 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader3 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3DefaultValue;
			textEditTabBSubheader4.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader4 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4DefaultValue;
			spinEditTabBSubheader5.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader5 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader5DefaultValue;
			textEditTabBSubheader6.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader6 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6DefaultValue;
			textEditTabBSubheader7.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader7 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7DefaultValue;
			spinEditTabBSubheader8.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader8 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader8DefaultValue;
			textEditTabBSubheader9.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader9 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9DefaultValue;
			textEditTabBSubheader10.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader10 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10DefaultValue;
			spinEditTabBSubheader11.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader11 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader11DefaultValue;
			textEditTabBSubheader12.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader12 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12DefaultValue;
			textEditTabBSubheader13.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader13 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13DefaultValue;
			textEditTabBSubheader14.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader14 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14DefaultValue;
			textEditTabBSubheader15.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader15 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15DefaultValue;
			textEditTabBSubheader16.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader16 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16DefaultValue;
			spinEditTabBSubheader17.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader17 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader17DefaultValue;
			textEditTabBSubheader18.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader18 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18DefaultValue;
			textEditTabBSubheader19.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader19 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19DefaultValue;
			textEditTabBSubheader20.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader20 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20DefaultValue;
			textEditTabBSubheader21.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader21 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21DefaultValue;
			textEditTabBSubheader22.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader22 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22DefaultValue;
			textEditTabBSubheader23.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader23 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23DefaultValue;
			spinEditTabBSubheader24.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader24DefaultValue;
			textEditTabBSubheader25.EditValue = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25 ??
				ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25DefaultValue;
			Application.DoEvents();

			_allowToSave = true;

			OnTabBFormulaSourceEditValueChanged(null, EventArgs.Empty);
			Application.DoEvents();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group1Toggle = checkEditTabBGroup1.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group2Toggle = checkEditTabBGroup2.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group3Toggle = checkEditTabBGroup3.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group4Toggle = checkEditTabBGroup4.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group5Toggle = checkEditTabBGroup5.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group6Toggle = checkEditTabBGroup6.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group7Toggle = checkEditTabBGroup7.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group8Toggle = checkEditTabBGroup8.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group9Toggle = checkEditTabBGroup9.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group10Toggle = checkEditTabBGroup10.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group11Toggle = checkEditTabBGroup11.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle = checkEditTabBSubheader24.Checked;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle = checkEditTabBSubheader25.Checked;

			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader2 = (decimal?)spinEditTabBSubheader2.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader2DefaultValue ?
				(decimal?)spinEditTabBSubheader2.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3DefaultValue ?
				textEditTabBSubheader3.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader4 = textEditTabBSubheader4.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4DefaultValue ?
				textEditTabBSubheader4.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader5 = (decimal?)spinEditTabBSubheader5.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader5DefaultValue ?
				(decimal?)spinEditTabBSubheader5.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader6 = textEditTabBSubheader6.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6DefaultValue ?
				textEditTabBSubheader6.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader7 = textEditTabBSubheader7.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7DefaultValue ?
				textEditTabBSubheader7.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader8 = (decimal?)spinEditTabBSubheader8.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader8DefaultValue ?
				(decimal?)spinEditTabBSubheader8.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader9 = textEditTabBSubheader9.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9DefaultValue ?
				textEditTabBSubheader9.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader10 = textEditTabBSubheader10.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10DefaultValue ?
				textEditTabBSubheader10.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader11 = (decimal?)spinEditTabBSubheader11.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader11DefaultValue ?
				(decimal?)spinEditTabBSubheader11.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader12 = textEditTabBSubheader12.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12DefaultValue ?
				textEditTabBSubheader12.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader13 = textEditTabBSubheader13.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13DefaultValue ?
				textEditTabBSubheader13.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader14 = textEditTabBSubheader14.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14DefaultValue ?
				textEditTabBSubheader14.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader15 = textEditTabBSubheader15.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15DefaultValue ?
				textEditTabBSubheader15.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader16 = textEditTabBSubheader16.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16DefaultValue ?
				textEditTabBSubheader16.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader17 = (decimal?)spinEditTabBSubheader17.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader17DefaultValue ?
				(decimal?)spinEditTabBSubheader17.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader18 = textEditTabBSubheader18.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18DefaultValue ?
				textEditTabBSubheader18.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader19 = textEditTabBSubheader19.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19DefaultValue ?
				textEditTabBSubheader19.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader20 = textEditTabBSubheader20.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20DefaultValue ?
				textEditTabBSubheader20.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader21 = textEditTabBSubheader21.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21DefaultValue ?
				textEditTabBSubheader21.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader22 = textEditTabBSubheader22.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22DefaultValue ?
				textEditTabBSubheader22.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader23 = textEditTabBSubheader23.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23DefaultValue ?
				textEditTabBSubheader23.EditValue as String ?? String.Empty :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24 = (decimal?)spinEditTabBSubheader24.EditValue != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader24DefaultValue ?
				(decimal?)spinEditTabBSubheader24.EditValue :
				null;
			ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25 = textEditTabBSubheader25.EditValue as String != ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25DefaultValue ?
				textEditTabBSubheader25.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		#region Event Handlers
		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			ROIContentContainer.RaiseDataChanged();
		}

		private void OnTabBGroup1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup1Inner.Enabled = checkEditTabBGroup1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup2CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup2Inner.Enabled = checkEditTabBGroup2.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup3CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup3Inner.Enabled = checkEditTabBGroup3.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup4CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup4Inner.Enabled = checkEditTabBGroup4.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup5CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup5Inner.Enabled = checkEditTabBGroup5.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup6CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup6Inner.Enabled = checkEditTabBGroup6.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup7CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup7Inner.Enabled = checkEditTabBGroup7.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup8CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup8Inner.Enabled = checkEditTabBGroup8.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup9CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup9Inner.Enabled = checkEditTabBGroup9.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup10CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup10Inner.Enabled = checkEditTabBGroup10.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBGroup11CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupTabBGroup11Inner.Enabled = checkEditTabBGroup11.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBSubheader24CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader24Value.Enabled = checkEditTabBSubheader24.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBSubheader25CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTabBSubheader25Value.Enabled = checkEditTabBSubheader25.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnTabBFormulaSourceEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var avgValue = (double)spinEditTabBSubheader2.Value;
			var highValue = (double)spinEditTabBSubheader5.Value;
			var callsCount = (double)spinEditTabBSubheader8.Value;
			var avgPercent = (double)spinEditTabBSubheader11.Value;
			var highPercent = (double)spinEditTabBSubheader17.Value;
			var investmentValue = (double)spinEditTabBSubheader24.Value;

			var avgFormula1Value = Math.Ceiling(callsCount * avgPercent / 100);
			var avgFormula2Value = avgValue * avgFormula1Value;


			var highFormula1Value = Math.Ceiling(callsCount * highPercent / 100);
			var highFormula2Value = highValue * highFormula1Value;

			var totalValue = avgFormula2Value + highFormula2Value;

			var formula3Value = investmentValue > 0 ? Math.Ceiling(totalValue / investmentValue) : 0;
			formula3Value = formula3Value < totalValue ? formula3Value : 1.0;

			simpleLabelItemTabBFormula1.CustomizationFormText = String.Format("{0:#,##0}", avgFormula1Value);
			simpleLabelItemTabBFormula1.Text = String.Format("<b>{0:#,##0}</b>", avgFormula1Value);
			simpleLabelItemTabBFormula2.CustomizationFormText = String.Format("{0:$#,##0}", avgFormula2Value);
			simpleLabelItemTabBFormula2.Text = String.Format("<b>{0:$#,##0}</b>", avgFormula2Value);

			simpleLabelItemTabBFormula3.CustomizationFormText = String.Format("{0:#,##0}", highFormula1Value);
			simpleLabelItemTabBFormula3.Text = String.Format("<b>{0:#,##0}</b>", highFormula1Value);
			simpleLabelItemTabBFormula4.CustomizationFormText = String.Format("{0:$#,##0}", highFormula2Value);
			simpleLabelItemTabBFormula4.Text = String.Format("<b>{0:$#,##0}</b>", highFormula2Value);

			simpleLabelItemTabBFormula5.CustomizationFormText = String.Format("{0:$#,##0}", totalValue);
			simpleLabelItemTabBFormula5.Text = String.Format("<b>{0:$#,##0}</b>", totalValue);

			layoutControlItemTabBSubheader25Value.CustomizationFormText = String.Format("{0:#,##0} : 1", formula3Value);
			layoutControlItemTabBSubheader25Value.Text = String.Format("= <b>{0:#,##0} : 1</b>", formula3Value);

			OnEditValueChanged(sender, e);
		}
		#endregion

		#region Output
		public override StarAppOutputType OutputType => StarAppOutputType.ROITabB;
		public override String OutputName => ROIContentContainer.SlideContainer.StarInfo.Titles.Tab6SubBTitle;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarROIFile("CP06B-1.pptx");
			outputDataPackage.Theme = ROIContentContainer.SelectedTheme;

			var clipart1 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart1 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP06BCLIPART1", clipart1);

			var clipart2 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart2 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP06BCLIPART2", clipart2);

			var clipart3 = ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Clipart3 ?? ImageClipartObject.FromImage(ROIContentContainer.SlideContainer.StarInfo.Tab6SubBClipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP06BCLIPART3", clipart3);

			outputDataPackage.TextItems = GetOutputDataTextItems();

			var slideHeader = (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.SlideHeader ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP06BHEADER", slideHeader);
			outputDataPackage.TextItems.Add("HEADER", slideHeader);

			return outputDataPackage;
		}

		protected override Dictionary<string, string> GetOutputDataTextItems()
		{
			var textDataItems = new Dictionary<string, string>();

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group1Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader1 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader1DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader2 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader2DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader3 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader3DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase1".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group2Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader4 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader4DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader5 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader5DefaultValue ?? 0).ToString("$#,##0"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader6 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader6DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase2".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group3Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader7 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader7DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader8 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader8DefaultValue ?? 0).ToString("#,##0"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader9 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader9DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase3".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group4Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader10 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader10DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader11 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader11DefaultValue ?? 0).ToString("##0'%'"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader12 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader12DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase4".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group5Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader13 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader13DefaultValue);
				itemParts.Add(simpleLabelItemTabBFormula1.CustomizationFormText);
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader14 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader14DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase5".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group6Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader15 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader15DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula2.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase6".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group7Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader16 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader16DefaultValue);
				itemParts.Add((ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader17 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader17DefaultValue ?? 0).ToString("##0'%'"));
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader18 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader18DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase7".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group8Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader19 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader19DefaultValue);
				itemParts.Add(simpleLabelItemTabBFormula3.CustomizationFormText);
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader20 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader20DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase8".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group9Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader21 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader21DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula4.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase9".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group10Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader22 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader22DefaultValue);
				itemParts.Add(String.Format("= {0}", simpleLabelItemTabBFormula5.CustomizationFormText));
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase10".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));
			}

			if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Group11Toggle)
			{
				var itemParts = new List<string>();
				itemParts.Add(ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader23 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader23DefaultValue);
				if (itemParts.Any(item => !String.IsNullOrWhiteSpace(item)))
					textDataItems.Add("CP06BFormulaPhrase11".ToUpper(), String.Join(" ", itemParts.Where(item => !String.IsNullOrWhiteSpace(item)).ToArray()));

				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24Toggle)
					textDataItems.Add("CP06BFormulaPhrase12".ToUpper(), String.Format("{0:$#,##0} Per Month", (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader24 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader24DefaultValue ?? 0)));

				if (ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25Toggle)
					textDataItems.Add("CP06BFormulaPhrase13".ToUpper(), String.Format("{0} = {1}", ROIContentContainer.SlideContainer.EditedContent.ROIState.TabB.Subheader25 ?? ROIContentContainer.SlideContainer.StarInfo.ROIConfiguration.PartBSubHeader25DefaultValue, layoutControlItemTabBSubheader25Value.CustomizationFormText));
			}

			return textDataItems;
		}

		public override void GenerateOutput()
		{
			var outputDataPackage = GetOutputData();
			ROIContentContainer.SlideContainer.PowerPointProcessor.AppendStarCommonSlide(outputDataPackage);
		}

		public override PreviewGroup GeneratePreview()
		{
			var outputDataPackage = GetOutputData();
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			ROIContentContainer.SlideContainer.PowerPointProcessor.PrepareStarCommonSlide(outputDataPackage, tempFileName);
			return new PreviewGroup { Name = OutputName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}
