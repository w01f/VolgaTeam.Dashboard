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
	public sealed partial class CBCTabDControl : ChildTabBaseControl
	{
		public CBCTabDInfo CustomTabInfo => (CBCTabDInfo)TabInfo;

		public CBCTabDControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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
				new StepControl(CustomTabInfo.Tab3Info),
				new StepControl(CustomTabInfo.Tab4Info),
			});

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
				stepControl.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CBCState.TabD.SlideHeader ??
												CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
			{
				switch (stepControl.Info.StepInfo.Index)
				{
					case 3:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabD.Tab3State);
						break;
					case 4:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabD.Tab4State);
						break;
				}
			}

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.CBCState.TabD.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
				stepControl.SaveData();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CBCState.TabD.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
			SlideContainer.EditedContent.CBCState.TabD.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftCBC_D;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftCBCFile("053_cbc_d1.pptx");

			outputDataPackage.TextItems.Add("SHIFT08DHEADER".ToUpper(), (SlideContainer.EditedContent.CBCState.TabD.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			var tab3Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab3Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabD.Tab3State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab3Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab3Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab3Info.ComboDefaultItems[i].IsDefault ?
									  CustomTabInfo.Tab3Info.ComboDefaultItems[i] :
									  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab3Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08DCOMBOMERGE3".ToUpper(), String.Join("   |   ", tab3Values));

			var tab4Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab4Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabD.Tab4State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab4Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab4Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab4Info.ComboDefaultItems[i].IsDefault ?
									  CustomTabInfo.Tab4Info.ComboDefaultItems[i] :
									  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab4Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08DCOMBOMERGE4".ToUpper(), String.Join("   |   ", tab4Values));

			return outputDataPackage;
		}
		#endregion
	}
}