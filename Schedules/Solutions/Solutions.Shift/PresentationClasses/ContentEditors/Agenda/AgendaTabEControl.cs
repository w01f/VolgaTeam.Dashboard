using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Agenda;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Agenda
{
	[ToolboxItem(false)]
	public sealed partial class AgendaTabEControl : ChildTabBaseControl
	{
		private AgendaTabEInfo CustomTabInfo => (AgendaTabEInfo)TabInfo;

		public AgendaTabEControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			layoutControlGroupTab1.Text = CustomTabInfo.Tab1Title;
			layoutControlGroupTab2.Text = CustomTabInfo.Tab2Title;
			layoutControlGroupTab3.Text = CustomTabInfo.Tab3Title;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.ComboConfiguration);
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.ComboConfiguration);
			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.ComboConfiguration);
			comboBoxEditCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.ComboConfiguration);
			comboBoxEditCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.ComboConfiguration);
			comboBoxEditCombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.ComboConfiguration);
			comboBoxEditCombo7.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.ComboConfiguration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			memoPopupEdit1.Init(
				CustomTabInfo.MemoPopup1Items.Where(item => !item.IsPlaceholder).ToList(),
				CustomTabInfo.MemoPopup1Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault),
				CustomTabInfo.MemoPopup1Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			memoPopupEdit1.EditValueChanged += OnEditValueChanged;

			memoPopupEdit2.Init(
				CustomTabInfo.MemoPopup2Items.Where(item => !item.IsPlaceholder).ToList(),
				CustomTabInfo.MemoPopup2Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault),
				CustomTabInfo.MemoPopup2Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			memoPopupEdit2.EditValueChanged += OnEditValueChanged;

			memoPopupEdit3.Init(
				CustomTabInfo.MemoPopup3Items.Where(item => !item.IsPlaceholder).ToList(),
				CustomTabInfo.MemoPopup3Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault),
				CustomTabInfo.MemoPopup3Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			memoPopupEdit3.EditValueChanged += OnEditValueChanged;

			memoPopupEdit4.Init(
				CustomTabInfo.MemoPopup4Items.Where(item => !item.IsPlaceholder).ToList(),
				CustomTabInfo.MemoPopup4Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault),
				CustomTabInfo.MemoPopup4Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			memoPopupEdit4.EditValueChanged += OnEditValueChanged;

			memoPopupEdit5.Init(
				CustomTabInfo.MemoPopup5Items.Where(item => !item.IsPlaceholder).ToList(),
				CustomTabInfo.MemoPopup5Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault),
				CustomTabInfo.MemoPopup5Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			memoPopupEdit5.EditValueChanged += OnEditValueChanged;

			memoPopupEdit6.Init(
				CustomTabInfo.MemoPopup6Items.Where(item => !item.IsPlaceholder).ToList(),
				CustomTabInfo.MemoPopup6Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault),
				CustomTabInfo.MemoPopup6Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			memoPopupEdit6.EditValueChanged += OnEditValueChanged;

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

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;

			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.AgendaState.TabE.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.AgendaState.TabE.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.AgendaState.TabE.Clipart3);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AgendaState.TabE.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			memoPopupEdit1.LoadData(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup1);
			memoPopupEdit2.LoadData(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup2);
			memoPopupEdit3.LoadData(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup3);
			memoPopupEdit4.LoadData(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup4);
			memoPopupEdit5.LoadData(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup5);
			memoPopupEdit6.LoadData(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup6);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.AgendaState.TabE.Combo1 ??
											   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.AgendaState.TabE.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo3.EditValue = SlideContainer.EditedContent.AgendaState.TabE.Combo3 ??
										   CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo4.EditValue = SlideContainer.EditedContent.AgendaState.TabE.Combo4 ??
										   CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo5.EditValue = SlideContainer.EditedContent.AgendaState.TabE.Combo5 ??
										   CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo6.EditValue = SlideContainer.EditedContent.AgendaState.TabE.Combo6 ??
										   CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo7.EditValue = SlideContainer.EditedContent.AgendaState.TabE.Combo7 ??
										   CustomTabInfo.Combo7Items.FirstOrDefault(item => item.IsDefault);
			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.AgendaState.TabE.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.AgendaState.TabE.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.AgendaState.TabE.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.AgendaState.TabE.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.AgendaState.TabE.MemoPopup1 = memoPopupEdit1.GetSelectedItem();
			SlideContainer.EditedContent.AgendaState.TabE.MemoPopup2 = memoPopupEdit2.GetSelectedItem();
			SlideContainer.EditedContent.AgendaState.TabE.MemoPopup3 = memoPopupEdit3.GetSelectedItem();
			SlideContainer.EditedContent.AgendaState.TabE.MemoPopup4 = memoPopupEdit4.GetSelectedItem();
			SlideContainer.EditedContent.AgendaState.TabE.MemoPopup5 = memoPopupEdit5.GetSelectedItem();
			SlideContainer.EditedContent.AgendaState.TabE.MemoPopup6 = memoPopupEdit6.GetSelectedItem();

			SlideContainer.EditedContent.AgendaState.TabE.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabE.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabE.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue ?
				comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabE.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo4.EditValue ?
				comboBoxEditCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabE.Combo5 = CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo5.EditValue ?
				comboBoxEditCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo5.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabE.Combo6 = CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo6.EditValue ?
				comboBoxEditCombo6.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo6.EditValue as String } :
				null;
			SlideContainer.EditedContent.AgendaState.TabE.Combo7 = CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo7.EditValue ?
				comboBoxEditCombo7.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo7.EditValue as String } :
				null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.AgendaState.TabE.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
			SlideContainer.EditedContent.AgendaState.TabE.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftAgendaE;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.AgendaState.TabE.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
			{
				clipart1.OutputBackground = true;
				outputDataPackage.ClipartItems.Add("SHIFT03ECLIPART1", clipart1);
			}

			var clipart2 = SlideContainer.EditedContent.AgendaState.TabE.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT03ECLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.AgendaState.TabE.Clipart3 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("SHIFT03ECLIPART3", clipart3);

			var slideHeader = (SlideContainer.EditedContent.AgendaState.TabE.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;

			var memopPopups1 = new[]
				{
					(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup1 ?? CustomTabInfo.MemoPopup1Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup2 ?? CustomTabInfo.MemoPopup2Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup3 ?? CustomTabInfo.MemoPopup3Items.FirstOrDefault(h => h.IsDefault))?.Value,
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			var memopPopups2 = new[]
				{
					(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup4 ?? CustomTabInfo.MemoPopup4Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup5 ?? CustomTabInfo.MemoPopup5Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.MemoPopup6 ?? CustomTabInfo.MemoPopup6Items.FirstOrDefault(h => h.IsDefault))?.Value,
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			var combos = new[]
				{
					(SlideContainer.EditedContent.AgendaState.TabE.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.Combo3 ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.Combo4 ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.Combo5 ?? CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.Combo6 ?? CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.AgendaState.TabE.Combo7 ?? CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault))?.Value,
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftAgendaFile("012a_agenda_west_palm.pptx");

			outputDataPackage.TextItems.Add("SHIFT03EHeader".ToUpper(), slideHeader);

			outputDataPackage.TextItems.Add("SHIFT03ETAB1NAME".ToUpper(), CustomTabInfo.Tab1Title);
			outputDataPackage.TextItems.Add("SHIFT03ETAB2NAME".ToUpper(), CustomTabInfo.Tab2Title);
			outputDataPackage.TextItems.Add("SHIFT03ETAB3NAME".ToUpper(), CustomTabInfo.Tab3Title);

			outputDataPackage.TextItems.Add("SHIFT03EMULTIBOXMERGE1".ToUpper(), String.Join(String.Format("{0}", (char)13), memopPopups1));
			outputDataPackage.TextItems.Add("SHIFT03EMULTIBOXMERGE2".ToUpper(), String.Join(String.Format("{0}", (char)13), memopPopups2));
			outputDataPackage.TextItems.Add("SHIFT03ECOMBOMERGE3".ToUpper(), String.Join(String.Format("{0}", (char)13), combos));

			return outputDataPackage;
		}
		#endregion
	}
}