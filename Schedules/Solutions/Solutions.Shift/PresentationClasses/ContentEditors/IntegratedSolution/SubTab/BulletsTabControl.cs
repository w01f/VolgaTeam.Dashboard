using System;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.Skins;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	//public partial class BulletsTabControl : UserControl
	public partial class BulletsTabControl : BaseToggleTabControl
	{
		public PositioningInfo.Tab2Info TabInfo { get; }
		public IntegratedSolutionState.BulletsTabState TabState { get; }

		public BulletsTabControl()
		{
			InitializeComponent();
		}

		public BulletsTabControl(PositioningInfo.Tab2Info tabInfo,
			IntegratedSolutionState.BulletsTabState tabState,
			ProductItemControl container) : base(container)
		{
			InitializeComponent();

			TabState = tabState;
			TabInfo = tabInfo;

			Text = TabInfo.Title;

			layoutControlItemCombo1.Text = String.Format("<color=gray>{0}</color>", TabInfo.Checkbox1.Title ?? "header");

			if (Container.Container.TabInfo.CommonEditorConfiguration.FontSize.HasValue)
			{
				var fontSizeDelte = Container.Container.TabInfo.CommonEditorConfiguration.FontSize.Value - TextEditorConfiguration.DefaultFontSize;
				layoutControl.Appearance.Control.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlFocused.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDown.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDownHeader.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDisabled.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlReadOnly.FontSizeDelta = fontSizeDelte;
			}
			if (!Container.Container.TabInfo.CommonEditorConfiguration.BackColor.IsEmpty)
			{
				layoutControl.Appearance.Control.BackColor = Container.Container.TabInfo.CommonEditorConfiguration.BackColor;
				layoutControl.Appearance.ControlFocused.BackColor = Container.Container.TabInfo.CommonEditorConfiguration.BackColor;
				layoutControl.Appearance.ControlDisabled.BackColor = CommonSkins.GetSkin(UserLookAndFeel.Default).Colors.GetColor(CommonColors.DisabledControl); ;
			}
			if (!Container.Container.TabInfo.CommonEditorConfiguration.ForeColor.IsEmpty)
			{
				layoutControl.Appearance.Control.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlFocused.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlDisabled.ForeColor = CommonSkins.GetSkin(UserLookAndFeel.Default).Colors.GetColor(CommonColors.DisabledText);
			}
			if (!Container.Container.TabInfo.CommonEditorConfiguration.DropdownForeColor.IsEmpty)
			{
				layoutControl.Appearance.ControlDropDown.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
				layoutControl.Appearance.ControlDropDownHeader.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
			}

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemToggleCombo1.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggleCombo1.MaxSize, scaleFactor);
			layoutControlItemToggleCombo1.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggleCombo1.MinSize, scaleFactor);
			layoutControlItemToggleSwitch.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggleSwitch.MaxSize, scaleFactor);
			layoutControlItemToggleSwitch.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggleSwitch.MinSize, scaleFactor);
		}

		public override void LoadData()
		{
			if (Initialized) return;

			_allowHandleEvents = false;

			toggleSwitch.IsOn = TabState.Toggled ?? TabInfo.ToggleSwitch.Value;

			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(TabInfo.Combo1Configuration);
			comboBoxEditCombo1.Properties.Items.Clear();
			comboBoxEditCombo1.Properties.Items.AddRange(TabInfo.Combo1Items);
			comboBoxEditCombo1.Properties.NullText =
				TabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo1.Properties.NullText;
			if (TabState.Combo1 != null)
			{
				checkEditCombo1.Checked = !String.IsNullOrEmpty(TabState.Combo1?.Value);
				comboBoxEditCombo1.EditValue = TabState.Combo1;
			}
			else
			{
				checkEditCombo1.Checked = TabInfo.Checkbox1.Value;
				comboBoxEditCombo1.EditValue = TabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault && !item.IsPlaceholder);
			}

			var bulletCombos = new[]
			{
				comboBoxEditBulletCombo1,
				comboBoxEditBulletCombo2,
				comboBoxEditBulletCombo3,
				comboBoxEditBulletCombo4,
				comboBoxEditBulletCombo5,
				comboBoxEditBulletCombo6,
				comboBoxEditBulletCombo7,
				comboBoxEditBulletCombo8,
			};

			var bulletComboLists = new[]
			{
				TabInfo.BulletCombo1Items,
				TabInfo.BulletCombo2Items,
				TabInfo.BulletCombo3Items,
				TabInfo.BulletCombo4Items,
				TabInfo.BulletCombo5Items,
				TabInfo.BulletCombo6Items,
				TabInfo.BulletCombo7Items,
				TabInfo.BulletCombo8Items
			};

			for (var i = 0; i < bulletCombos.Length; i++)
			{
				bulletCombos[i].EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(TabInfo.BulletComboConfiguration);
				bulletCombos[i].Properties.Items.Clear();
				bulletCombos[i].Properties.Items.AddRange(bulletComboLists[i]);
				bulletCombos[i].Properties.NullText =
					bulletComboLists[i].FirstOrDefault(item => item.IsPlaceholder)?.Value ??
					bulletCombos[i].Properties.NullText;
				bulletCombos[i].EditValue = TabState.Bullets.ElementAtOrDefault(i) ??
											bulletComboLists[i].FirstOrDefault(item => item.IsDefault);
			}

			_allowHandleEvents = true;

			Initialized = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			TabState.Toggled = toggleSwitch.IsOn != TabInfo.ToggleSwitch.Value ? toggleSwitch.IsOn : (bool?)null;

			if (toggleSwitch.IsOn)
			{
				if (checkEditCombo1.Checked)
				{
					TabState.Combo1 = TabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue || checkEditCombo1.Checked != TabInfo.Checkbox1.Value
						? comboBoxEditCombo1.EditValue as ListDataItem ??
						  new ListDataItem { Value = comboBoxEditCombo1.EditValue as String }
						: null;
				}
				else if (checkEditCombo1.Checked != TabInfo.Checkbox1.Value)
				{
					TabState.Combo1 = new ListDataItem();
				}
				else
					TabState.Combo1 = null;

				var bulletCombos = new[]
				{
					comboBoxEditBulletCombo1,
					comboBoxEditBulletCombo2,
					comboBoxEditBulletCombo3,
					comboBoxEditBulletCombo4,
					comboBoxEditBulletCombo5,
					comboBoxEditBulletCombo6,
					comboBoxEditBulletCombo7,
					comboBoxEditBulletCombo8,
				};

				var bulletComboLists = new[]
				{
					TabInfo.BulletCombo1Items,
					TabInfo.BulletCombo2Items,
					TabInfo.BulletCombo3Items,
					TabInfo.BulletCombo4Items,
					TabInfo.BulletCombo5Items,
					TabInfo.BulletCombo6Items,
					TabInfo.BulletCombo7Items,
					TabInfo.BulletCombo8Items
				};

				TabState.Bullets.Clear();
				for (var i = 0; i < bulletCombos.Length; i++)
				{
					TabState.Bullets.Add(bulletComboLists[i].FirstOrDefault(h => h.IsDefault) != bulletCombos[i].EditValue
						? bulletCombos[i].EditValue as ListDataItem ?? new ListDataItem { Value = bulletCombos[i].EditValue as String }
						: null);
				}
			}
			else
			{
				TabState.Combo1 = null;
				TabState.Bullets.Clear();
			}

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnToggleSwitchToggled(object sender, EventArgs e)
		{
			layoutControlItemToggleCombo1.Enabled =
				layoutControlGroupBullets.Enabled = toggleSwitch.IsOn;
			layoutControlItemCombo1.Enabled = toggleSwitch.IsOn && checkEditCombo1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnToggleCombo1CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemCombo1.Enabled = checkEditCombo1.Checked;
			OnEditValueChanged(sender, e);
		}
	}
}
