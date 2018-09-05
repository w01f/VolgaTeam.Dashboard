using System;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach.TabA
{
	public partial class ContentsItemControl : BaseTabASubControl
	{
		public event EventHandler<ItemChangedEventArgs> ItemStateChanged;

		public ContentsItemControl()
		{
			InitializeComponent();
		}

		public ContentsItemControl(ApproachTabAControl container) : base(container)
		{
			InitializeComponent();

			Text = Container.CustomTabInfo.TabSelector.ContentsTabName;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(Container.CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(Container.CustomTabInfo.Combo1Configuration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(Container.CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				Container.CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			comboBoxEditCombo1.Properties.Items.Clear();
			comboBoxEditCombo1.Properties.Items.AddRange(Container.CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo1.Properties.NullText =
				Container.CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo1.Properties.NullText;

			simpleLabelItemDescription.Text = String.Format("<size=+2><color=gray>{0}</color></size>", Container.CustomTabInfo.TabSelector.ContentsTabDescription);

			itemSelectorControl.Init(Container.CustomTabInfo.ApproachItems, Container.CustomTabInfo.TabSelector.MaxSelectedTabs);
			itemSelectorControl.ItemStateChanged += OnItemStateChanged;

			if (Container.TabInfo.CommonEditorConfiguration.FontSize.HasValue)
			{
				var fontSizeDelte = Container.TabInfo.CommonEditorConfiguration.FontSize.Value - TextEditorConfiguration.DefaultFontSize;
				layoutControl.Appearance.Control.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlFocused.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDown.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDownHeader.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDisabled.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlReadOnly.FontSizeDelta = fontSizeDelte;
			}
			if (!Container.TabInfo.CommonEditorConfiguration.BackColor.IsEmpty)
			{
				layoutControl.Appearance.Control.BackColor = Container.TabInfo.CommonEditorConfiguration.BackColor;
				layoutControl.Appearance.ControlFocused.BackColor = Container.TabInfo.CommonEditorConfiguration.BackColor;
			}
			if (!Container.TabInfo.CommonEditorConfiguration.ForeColor.IsEmpty)
			{
				layoutControl.Appearance.Control.ForeColor = Container.TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlFocused.ForeColor = Container.TabInfo.CommonEditorConfiguration.ForeColor;
			}
			if (!Container.TabInfo.CommonEditorConfiguration.DropdownForeColor.IsEmpty)
			{
				layoutControl.Appearance.ControlDropDown.ForeColor = Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
				layoutControl.Appearance.ControlDropDownHeader.ForeColor = Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
			}
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ApproachState.TabA.SlideHeader ??
				Container.CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.ApproachState.TabA.Combo1 ??
				Container.CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);

			itemSelectorControl.LoadSavedState(SlideContainer.EditedContent.ApproachState.TabA.Items);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.ApproachState.TabA.SlideHeader = slideHeaderValue != Container.CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.ApproachState.TabA.Combo1 = Container.CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnItemStateChanged(object sender, ItemChangedEventArgs e)
		{
			if (!_allowToSave) return;
			ItemStateChanged?.Invoke(this, e);
		}
	}
}
