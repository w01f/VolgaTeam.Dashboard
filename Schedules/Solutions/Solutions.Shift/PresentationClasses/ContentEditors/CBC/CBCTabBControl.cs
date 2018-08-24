using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.CBC;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.CBC
{
	[ToolboxItem(false)]
	public sealed partial class CBCTabBControl : ChildTabBaseControl
	{
		public CBCTabBInfo CustomTabInfo => (CBCTabBInfo)TabInfo;

		public CBCTabBControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			xtraTabControl.TabPages.AddRange(new[]
			{
				new StepControl(CustomTabInfo.Tab1Info),
				new StepControl(CustomTabInfo.Tab2Info),
				new StepControl(CustomTabInfo.Tab3Info),
				new StepControl(CustomTabInfo.Tab4Info),
				new StepControl(CustomTabInfo.Tab5Info),
			});

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
				stepControl.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CBCState.TabB.SlideHeader ??
												CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
			{
				switch (stepControl.Info.StepInfo.Index)
				{
					case 1:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabB.Tab1State);
						break;
					case 2:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabB.Tab2State);
						break;
					case 3:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabB.Tab3State);
						break;
					case 4:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabB.Tab4State);
						break;
					case 5:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabB.Tab5State);
						break;
				}
			}

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.CBCState.TabB.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
				stepControl.SaveData();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CBCState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
			SlideContainer.EditedContent.CBCState.TabB.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftCBC_B;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftCBCFile("051_cbc_b1.pptx");

			outputDataPackage.TextItems.Add("SHIFT08BHEADER".ToUpper(), (SlideContainer.EditedContent.CBCState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			var tab1Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab1Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabB.Tab1State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab1Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab1Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab1Info.ComboDefaultItems[i].IsDefault ?
								  CustomTabInfo.Tab1Info.ComboDefaultItems[i] :
								  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab1Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08BCOMBOMERGE1".ToUpper(), String.Join("   |   ", tab1Values));

			var tab2Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab2Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabB.Tab2State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab2Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab2Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab2Info.ComboDefaultItems[i].IsDefault ?
									  CustomTabInfo.Tab2Info.ComboDefaultItems[i] :
									  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab2Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08BCOMBOMERGE2".ToUpper(), String.Join("   |   ", tab2Values));

			var tab3Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab3Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabB.Tab3State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab3Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab3Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab3Info.ComboDefaultItems[i].IsDefault ?
									  CustomTabInfo.Tab3Info.ComboDefaultItems[i] :
									  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab3Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08BCOMBOMERGE3".ToUpper(), String.Join("   |   ", tab3Values));

			var tab4Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab4Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabB.Tab4State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab4Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab4Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab4Info.ComboDefaultItems[i].IsDefault ?
									  CustomTabInfo.Tab4Info.ComboDefaultItems[i] :
									  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab4Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08BCOMBOMERGE4".ToUpper(), String.Join("   |   ", tab4Values));

			var tab5Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab5Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabB.Tab5State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab5Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab5Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab5Info.ComboDefaultItems[i].IsDefault ?
									  CustomTabInfo.Tab5Info.ComboDefaultItems[i] :
									  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab5Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08BCOMBOMERGE5".ToUpper(), String.Join("   |   ", tab5Values));

			return outputDataPackage;
		}
		#endregion
	}
}