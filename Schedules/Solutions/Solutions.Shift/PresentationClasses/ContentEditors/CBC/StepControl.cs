using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.CBC;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.CBC
{
	//public partial class StepControl : UserControl
	public partial class StepControl : XtraTabPage
	{
		private bool _allowHandleEvents;

		private CBCState.SubTabState _savedState;

		private readonly List<ComboBoxEdit> _editors = new List<ComboBoxEdit>();

		public TabInfo Info { get; }
		public event EventHandler<EventArgs> EditValueChanged;

		public StepControl(TabInfo tabInfo)
		{
			InitializeComponent();

			Info = tabInfo;

			_editors.AddRange(new[]
			{
				comboBoxEditCombo1,
				comboBoxEditCombo2,
				comboBoxEditCombo3,
				comboBoxEditCombo4,
				comboBoxEditCombo5,
				comboBoxEditCombo6,
				comboBoxEditCombo7,
				comboBoxEditCombo8,
				comboBoxEditCombo9,
				comboBoxEditCombo10,
				comboBoxEditCombo11,
				comboBoxEditCombo12
			});

			Text = Info.Title;

			if (tabInfo.ComboConfiguration != null)
			{
				if (Info.ComboConfiguration.FontSize.HasValue)
				{
					var fontSizeDelte = Info.ComboConfiguration.FontSize.Value - TextEditorConfiguration.DefaultFontSize;
					layoutControl.Appearance.Control.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlFocused.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlDropDown.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlDropDownHeader.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlDisabled.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlReadOnly.FontSizeDelta = fontSizeDelte;
				}
				if (!Info.ComboConfiguration.BackColor.IsEmpty)
				{
					layoutControl.Appearance.Control.BackColor = Info.ComboConfiguration.BackColor;
					layoutControl.Appearance.ControlFocused.BackColor = Info.ComboConfiguration.BackColor;
				}
				if (!Info.ComboConfiguration.ForeColor.IsEmpty)
				{
					layoutControl.Appearance.Control.ForeColor = Info.ComboConfiguration.ForeColor;
					layoutControl.Appearance.ControlFocused.ForeColor = Info.ComboConfiguration.ForeColor;
				}
				if (!Info.ComboConfiguration.DropdownForeColor.IsEmpty)
				{
					layoutControl.Appearance.ControlDropDown.ForeColor = Info.ComboConfiguration.DropdownForeColor;
					layoutControl.Appearance.ControlDropDownHeader.ForeColor = Info.ComboConfiguration.DropdownForeColor;
				}
			}

			for (var i = 0; i < _editors.Count; i++)
			{
				_editors[i].EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(Info.ComboConfiguration);
				_editors[i].Properties.Items.Clear();
				_editors[i].Properties.Items.AddRange(Info.StepInfo.Products);
				_editors[i].Properties.NullText = Info.ComboDefaultItems[i] != null && Info.ComboDefaultItems[i].IsPlaceholder ?
					Info.ComboDefaultItems[i].Value :
					_editors[i].Properties.NullText;
			}
		}

		public void LoadData(CBCState.SubTabState savedState)
		{
			_savedState = savedState;

			_allowHandleEvents = false;

			for (var i = 0; i < _editors.Count; i++)
			{
				_editors[i].EditValue = _savedState.ComboStates.ElementAtOrDefault(i) ??
					(Info.ComboDefaultItems[i] != null && !Info.ComboDefaultItems[i].IsPlaceholder && Info.ComboDefaultItems[i].IsDefault ?
						Info.ComboDefaultItems[i] :
						null);
			}

			_allowHandleEvents = true;
		}

		public void SaveData()
		{
			_savedState.ComboStates.Clear();
			for (var i = 0; i < _editors.Count; i++)
			{
				_savedState.ComboStates.Add(Info.ComboDefaultItems[i] == null ||
						Info.ComboDefaultItems[i].IsPlaceholder ||
						!Info.ComboDefaultItems[i].IsDefault ||
						Info.ComboDefaultItems[i] != _editors[i].EditValue ?
					(_editors[i].EditValue as ListDataItem ?? new ListDataItem { Value = _editors[i].EditValue as String }) :
					null);
			}
		}

		private void RaiseEditValueChanged()
		{
			if (!_allowHandleEvents) return;
			EditValueChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}
	}
}
