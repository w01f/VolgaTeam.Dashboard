using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Market;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Market
{
	[ToolboxItem(false)]
	public sealed partial class MarketTabEControl : ChildTabBaseControl
	{
		private MarketTabEInfo CustomTabInfo => (MarketTabEInfo)TabInfo;

		public MarketTabEControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo2Configuration);
			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo3Configuration);
			comboBoxEditCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo4Configuration);
			comboBoxEditCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo5Configuration);
			comboBoxEditCombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo6Configuration);
			comboBoxEditCombo7.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo7Configuration);
			comboBoxEditCombo8.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo8Configuration);
			comboBoxEditCombo9.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo9Configuration);
			comboBoxEditCombo10.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo10Configuration);
			comboBoxEditCombo11.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo11Configuration);
			comboBoxEditCombo12.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo12Configuration);
			comboBoxEditCombo13.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo13Configuration);
			spinEditSubheader1.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			memoEditSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader2Configuration);
			spinEditSubheader3.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader3Configuration);
			spinEditSubheader4.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader4Configuration);
			spinEditSubheader5.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader5Configuration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			comboBoxEditCombo1.Properties.Items.Clear();
			comboBoxEditCombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo1.Properties.NullText;

			comboBoxEditCombo2.Properties.Items.Clear();
			comboBoxEditCombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo2.Properties.NullText;

			comboBoxEditCombo3.Properties.Items.Clear();
			comboBoxEditCombo3.Properties.Items.AddRange(CustomTabInfo.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo3.Properties.NullText =
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo3.Properties.NullText;

			comboBoxEditCombo4.Properties.Items.Clear();
			comboBoxEditCombo4.Properties.Items.AddRange(CustomTabInfo.Combo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo4.Properties.NullText =
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo4.Properties.NullText;

			comboBoxEditCombo5.Properties.Items.Clear();
			comboBoxEditCombo5.Properties.Items.AddRange(CustomTabInfo.Combo5Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo5.Properties.NullText =
				CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo5.Properties.NullText;

			comboBoxEditCombo6.Properties.Items.Clear();
			comboBoxEditCombo6.Properties.Items.AddRange(CustomTabInfo.Combo6Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo6.Properties.NullText =
				CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo6.Properties.NullText;

			comboBoxEditCombo7.Properties.Items.Clear();
			comboBoxEditCombo7.Properties.Items.AddRange(CustomTabInfo.Combo7Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo7.Properties.NullText =
				CustomTabInfo.Combo7Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo7.Properties.NullText;

			comboBoxEditCombo8.Properties.Items.Clear();
			comboBoxEditCombo8.Properties.Items.AddRange(CustomTabInfo.Combo8Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo8.Properties.NullText =
				CustomTabInfo.Combo8Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo8.Properties.NullText;

			comboBoxEditCombo9.Properties.Items.Clear();
			comboBoxEditCombo9.Properties.Items.AddRange(CustomTabInfo.Combo9Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo9.Properties.NullText =
				CustomTabInfo.Combo9Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo9.Properties.NullText;

			comboBoxEditCombo10.Properties.Items.Clear();
			comboBoxEditCombo10.Properties.Items.AddRange(CustomTabInfo.Combo10Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo10.Properties.NullText =
				CustomTabInfo.Combo10Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo10.Properties.NullText;

			comboBoxEditCombo11.Properties.Items.Clear();
			comboBoxEditCombo11.Properties.Items.AddRange(CustomTabInfo.Combo11Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo11.Properties.NullText =
				CustomTabInfo.Combo11Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo11.Properties.NullText;

			comboBoxEditCombo12.Properties.Items.Clear();
			comboBoxEditCombo12.Properties.Items.AddRange(CustomTabInfo.Combo12Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo12.Properties.NullText =
				CustomTabInfo.Combo12Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo12.Properties.NullText;

			comboBoxEditCombo13.Properties.Items.Clear();
			comboBoxEditCombo13.Properties.Items.AddRange(CustomTabInfo.Combo13Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo13.Properties.NullText =
				CustomTabInfo.Combo13Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo13.Properties.NullText;

			memoEditSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditSubheader2.Properties.NullText;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.MarketState.TabE.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.MarketState.TabE.Clipart2);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabE.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo1 ??
										   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo3.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo3 ??
										   CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo4.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo4 ??
										   CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo5.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo5 ??
										   CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo6.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo6 ??
										   CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo7.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo7 ??
										   CustomTabInfo.Combo7Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo8.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo8 ??
										   CustomTabInfo.Combo8Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo9.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo9 ??
										   CustomTabInfo.Combo9Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo10.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo10 ??
										   CustomTabInfo.Combo10Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo11.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo11 ??
										   CustomTabInfo.Combo11Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo12.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo12 ??
										   CustomTabInfo.Combo12Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo13.EditValue = SlideContainer.EditedContent.MarketState.TabE.Combo13 ??
										   CustomTabInfo.Combo13Items.FirstOrDefault(item => item.IsDefault);

			spinEditSubheader1.EditValue = SlideContainer.EditedContent.MarketState.TabE.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditSubheader2.EditValue = SlideContainer.EditedContent.MarketState.TabE.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;
			spinEditSubheader3.EditValue = SlideContainer.EditedContent.MarketState.TabE.Subheader3 ??
										   CustomTabInfo.SubHeader3DefaultValue;
			spinEditSubheader4.EditValue = SlideContainer.EditedContent.MarketState.TabE.Subheader4 ??
										   CustomTabInfo.SubHeader4DefaultValue;
			spinEditSubheader5.EditValue = SlideContainer.EditedContent.MarketState.TabE.Subheader5 ??
										   CustomTabInfo.SubHeader5DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.MarketState.TabE.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabE.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.MarketState.TabE.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.MarketState.TabE.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue ?
				comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo4.EditValue ?
				comboBoxEditCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo5 = CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo5.EditValue ?
				comboBoxEditCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo5.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo6 = CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo6.EditValue ?
				comboBoxEditCombo6.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo6.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo7 = CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo7.EditValue ?
				comboBoxEditCombo7.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo7.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo8 = CustomTabInfo.Combo8Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo8.EditValue ?
				comboBoxEditCombo8.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo8.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo9 = CustomTabInfo.Combo9Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo9.EditValue ?
				comboBoxEditCombo9.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo9.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo10 = CustomTabInfo.Combo10Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo10.EditValue ?
				comboBoxEditCombo10.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo10.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo11 = CustomTabInfo.Combo11Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo11.EditValue ?
				comboBoxEditCombo11.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo11.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo12 = CustomTabInfo.Combo12Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo12.EditValue ?
				comboBoxEditCombo12.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo12.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Combo13 = CustomTabInfo.Combo13Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo13.EditValue ?
				comboBoxEditCombo13.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo13.EditValue as String } :
				null;

			SlideContainer.EditedContent.MarketState.TabE.Subheader1 = (decimal?)spinEditSubheader1.EditValue != CustomTabInfo.SubHeader1DefaultValue ?
				(decimal?)spinEditSubheader1.EditValue :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Subheader2 =
				memoEditSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue
					? memoEditSubheader2.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.MarketState.TabE.Subheader3 = (decimal?)spinEditSubheader3.EditValue != CustomTabInfo.SubHeader3DefaultValue ?
				(decimal?)spinEditSubheader3.EditValue :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Subheader4 = (decimal?)spinEditSubheader4.EditValue != CustomTabInfo.SubHeader4DefaultValue ?
				(decimal?)spinEditSubheader4.EditValue :
				null;
			SlideContainer.EditedContent.MarketState.TabE.Subheader5 = (decimal?)spinEditSubheader5.EditValue != CustomTabInfo.SubHeader5DefaultValue ?
				(decimal?)spinEditSubheader5.EditValue :
				null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.MarketState.TabE.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.MarketState.TabE.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftMarketA;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.MarketState.TabE.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT05ECLIPART1", clipart1);
			var clipart2 = SlideContainer.EditedContent.MarketState.TabE.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT05ECLIPART2", clipart2);

			var subheader3 = SlideContainer.EditedContent.MarketState.TabE.Subheader3 ??
							  CustomTabInfo.SubHeader3DefaultValue ?? 0;
			var subheader4 = SlideContainer.EditedContent.MarketState.TabE.Subheader4 ??
							 CustomTabInfo.SubHeader4DefaultValue ?? 0;
			var subheader5 = SlideContainer.EditedContent.MarketState.TabE.Subheader5 ??
							 CustomTabInfo.SubHeader5DefaultValue ?? 0;

			var chart1Data = new Dictionary<string, decimal>();
			chart1Data.Add("B2", subheader3 / 100m);
			chart1Data.Add("B3", (100 - subheader3) / 100m);
			outputDataPackage.ChartItems.Add("SHIFT05ECHART1", chart1Data);

			var chart2Data = new Dictionary<string, decimal>();
			chart2Data.Add("B2", subheader4 / 100m);
			chart2Data.Add("B3", (100 - subheader4) / 100m);
			outputDataPackage.ChartItems.Add("SHIFT05ECHART2", chart2Data);

			var chart3Data = new Dictionary<string, decimal>();
			chart3Data.Add("B2", subheader5 / 100m);
			chart3Data.Add("B3", (100 - subheader5) / 100m);
			outputDataPackage.ChartItems.Add("SHIFT05ECHART3", chart3Data);

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftMarketFile("021_market_opp_e.pptx");

			outputDataPackage.TextItems.Add("SHIFT05EHEADER".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT05ECOMBO1".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO2".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO3".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo3 ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO4".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo4 ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO5".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo5 ?? CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO6".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo6 ?? CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO7".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo7 ?? CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO8".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo8 ?? CustomTabInfo.Combo8Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMBO9".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo9 ?? CustomTabInfo.Combo9Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMB10".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo10 ?? CustomTabInfo.Combo10Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMB11".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo11 ?? CustomTabInfo.Combo11Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMB12".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo12 ?? CustomTabInfo.Combo12Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05ECOMB13".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Combo13 ?? CustomTabInfo.Combo13Items.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT05ESUBHEADER1".ToUpper(), (SlideContainer.EditedContent.MarketState.TabE.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue ?? 0).ToString("##0'%'"));
			outputDataPackage.TextItems.Add("SHIFT05ESUBHEADER2".ToUpper(), SlideContainer.EditedContent.MarketState.TabE.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT05ESUBHEADER3".ToUpper(), subheader3.ToString("##0'%'"));
			outputDataPackage.TextItems.Add("SHIFT05ESUBHEADER4".ToUpper(), subheader4.ToString("##0'%'"));
			outputDataPackage.TextItems.Add("SHIFT05ESUBHEADER5".ToUpper(), subheader5.ToString("##0'%'"));

			return outputDataPackage;
		}
		#endregion
	}
}