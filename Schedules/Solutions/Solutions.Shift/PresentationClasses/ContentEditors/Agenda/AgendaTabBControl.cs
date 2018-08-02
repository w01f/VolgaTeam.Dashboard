﻿using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Agenda;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Agenda
{
	[ToolboxItem(false)]
	public sealed partial class AgendaTabBControl : ChildTabBaseControl
	{
		private AgendaTabBInfo CustomTabInfo => (AgendaTabBInfo)TabInfo;

		public AgendaTabBControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo7.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo8.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.AgendaState.TabB.Clipart1);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AgendaState.TabB.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo1 ??
											   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo3.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo3 ??
										   CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo4.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo4 ??
										   CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo5.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo5 ??
										   CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo6.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo6 ??
										   CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo7.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo7 ??
										   CustomTabInfo.Combo7Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo8.EditValue = SlideContainer.EditedContent.AgendaState.TabB.Combo8 ??
										   CustomTabInfo.Combo8Items.FirstOrDefault(item => item.IsDefault);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.AgendaState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.AgendaState.TabB.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.AgendaState.TabB.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabB.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabB.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue ?
				comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabB.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo4.EditValue ?
				comboBoxEditCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabB.Combo5 = CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo5.EditValue ?
				comboBoxEditCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo5.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabB.Combo6 = CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo6.EditValue ?
				comboBoxEditCombo6.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo6.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabB.Combo7 = CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo7.EditValue ?
				comboBoxEditCombo7.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo7.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabB.Combo8 = CustomTabInfo.Combo8Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo8.EditValue ?
				comboBoxEditCombo8.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo8.EditValue as String } :
				null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.AgendaState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.AgendaState.TabB.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftAgendaB;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.AgendaState.TabB.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT03BCLIPART1", clipart1);

			var slideHeader = (SlideContainer.EditedContent.AgendaState.TabB.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var combos = new[]
				{
					(SlideContainer.EditedContent.AgendaState.TabB.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabB.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabB.Combo3 ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabB.Combo4 ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabB.Combo5 ?? CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabB.Combo6 ?? CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabB.Combo7 ?? CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabB.Combo8 ?? CustomTabInfo.Combo8Items.FirstOrDefault(h => h.IsDefault))?.Value,
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftAgendaFile("010_agenda_ihm.pptx");

			outputDataPackage.TextItems.Add("SHIFT03BHeader".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO1".ToUpper(), combos.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO2".ToUpper(), combos.ElementAtOrDefault(1));
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO3".ToUpper(), combos.ElementAtOrDefault(2));
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO4".ToUpper(), combos.ElementAtOrDefault(3));
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO5".ToUpper(), combos.ElementAtOrDefault(4));
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO6".ToUpper(), combos.ElementAtOrDefault(5));
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO7".ToUpper(), combos.ElementAtOrDefault(6));
			outputDataPackage.TextItems.Add("SHIFT03BCOMBO8".ToUpper(), combos.ElementAtOrDefault(7));

			return outputDataPackage;
		}
		#endregion
	}
}