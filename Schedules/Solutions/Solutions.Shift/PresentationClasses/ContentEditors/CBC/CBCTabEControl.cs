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
	public sealed partial class CBCTabEControl : ChildTabBaseControl
	{
		public CBCTabEInfo CustomTabInfo => (CBCTabEInfo)TabInfo;

		public CBCTabEControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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
				new StepControl(CustomTabInfo.Tab5Info),
			});

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
				stepControl.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CBCState.TabE.SlideHeader ??
												CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
			{
				switch (stepControl.Info.StepInfo.Index)
				{
					case 5:
						stepControl.LoadData(SlideContainer.EditedContent.CBCState.TabE.Tab5State);
						break;
				}
			}

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.CBCState.TabE.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			foreach (var stepControl in xtraTabControl.TabPages.OfType<StepControl>().ToList())
				stepControl.SaveData();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CBCState.TabE.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
			SlideContainer.EditedContent.CBCState.TabE.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftCBC_E;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftCBCFile("054_cbc_e1.pptx");

			outputDataPackage.TextItems.Add("SHIFT0EHEADER".ToUpper(), (SlideContainer.EditedContent.CBCState.TabE.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			var tab5Values = new List<string>();
			for (var i = 0; i < CustomTabInfo.Tab5Info.ComboDefaultItems.Length; i++)
			{
				var comboState = (SlideContainer.EditedContent.CBCState.TabE.Tab5State.ComboStates.ElementAtOrDefault(i) ??
								  (CustomTabInfo.Tab5Info.ComboDefaultItems[i] != null && !CustomTabInfo.Tab5Info.ComboDefaultItems[i].IsPlaceholder && CustomTabInfo.Tab5Info.ComboDefaultItems[i].IsDefault ?
									  CustomTabInfo.Tab5Info.ComboDefaultItems[i] :
									  null))?.Value;
				if (!String.IsNullOrWhiteSpace(comboState))
					tab5Values.Add(comboState);
			}
			outputDataPackage.TextItems.Add("SHIFT08ECOMBOMERGE5".ToUpper(), String.Join("   |   ", tab5Values));

			return outputDataPackage;
		}
		#endregion
	}
}