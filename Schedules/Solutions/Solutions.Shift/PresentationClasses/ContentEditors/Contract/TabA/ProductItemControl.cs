using System;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Contract;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabA;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract.TabA
{
	//public partial class ProductItemControl : UserControl
	public partial class ProductItemControl : BaseSubTabControl
	{
		private bool _allowToHandleEvents;
		private bool _dataChanged;

		public bool Initialized { get; private set; }

		public ProductInfo ItemInfo { get; }
		public ContractState.ProductItemState ItemState { get; }

		public bool ReadyForOutput => memoPopupEdit1.GetSelectedItem() != null ||
			comboBoxEditCombo1.EditValue != null ||
			comboBoxEditCombo2.EditValue != null ||
			comboBoxEditCombo3.EditValue != null;

		public ProductItemControl()
		{
			InitializeComponent();
		}

		public ProductItemControl(ProductInfo itemInfo,
			ContractState.ProductItemState itemState,
			ContractTabAControl container) : base(container)
		{
			InitializeComponent();

			ItemState = itemState;
			ItemInfo = itemInfo;

			Text = ItemInfo.Title;
			ShowCloseButton = DefaultBoolean.True;

			if (Container.TabInfo.CommonEditorConfiguration.FontSize.HasValue)
			{
				var fontSizeDelte = Container.TabInfo.CommonEditorConfiguration.FontSize.Value -
									TextEditorConfiguration.DefaultFontSize;
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
				layoutControl.Appearance.ControlDisabled.BackColor = CommonSkins.GetSkin(UserLookAndFeel.Default).Colors.GetColor(CommonColors.DisabledControl); ;
			}
			if (!Container.TabInfo.CommonEditorConfiguration.ForeColor.IsEmpty)
			{
				layoutControl.Appearance.Control.ForeColor = Container.TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlFocused.ForeColor = Container.TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlDisabled.ForeColor = CommonSkins.GetSkin(UserLookAndFeel.Default).Colors.GetColor(CommonColors.DisabledText);
			}
			if (!Container.TabInfo.CommonEditorConfiguration.DropdownForeColor.IsEmpty)
			{
				layoutControl.Appearance.ControlDropDown.ForeColor = Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
				layoutControl.Appearance.ControlDropDownHeader.ForeColor =
					Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
			}
		}

		public void InitControl()
		{
			if (Initialized) return;

			ItemInfo.LoadData();

			_allowToHandleEvents = false;

			memoPopupEdit1.Init(
				ItemInfo.MemoPopup1Items,
				ItemInfo.MemoPopup1Items.FirstOrDefault(h => h.IsDefault),
				ItemInfo.MemoPopup1Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			memoPopupEdit1.EditValueChanged -= OnEditValueChanged;
			memoPopupEdit1.EditValueChanged += OnEditValueChanged;
			memoPopupEdit1.LoadData(ItemState.MemoPopup1);

			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(ItemInfo.Combo1Configuration);
			comboBoxEditCombo1.Properties.Items.Clear();
			comboBoxEditCombo1.Properties.Items.AddRange(ItemInfo.Combo1Items);
			comboBoxEditCombo1.Properties.NullText =
				ItemInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo1.Properties.NullText;
			comboBoxEditCombo1.EditValue = ItemState.Combo1 ?? ItemInfo.Combo1Items.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(ItemInfo.Combo2Configuration);
			comboBoxEditCombo2.Properties.Items.Clear();
			comboBoxEditCombo2.Properties.Items.AddRange(ItemInfo.Combo2Items);
			comboBoxEditCombo2.Properties.NullText =
				ItemInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo2.Properties.NullText;
			comboBoxEditCombo2.EditValue = ItemState.Combo2 ?? ItemInfo.Combo2Items.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(ItemInfo.Combo3Configuration);
			comboBoxEditCombo3.Properties.Items.Clear();
			comboBoxEditCombo3.Properties.Items.AddRange(ItemInfo.Combo3Items);
			comboBoxEditCombo3.Properties.NullText =
				ItemInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo3.Properties.NullText;
			comboBoxEditCombo3.EditValue = ItemState.Combo3 ?? ItemInfo.Combo3Items.FirstOrDefault(h => h.IsDefault);

			_allowToHandleEvents = true;

			Initialized = true;
		}

		public void ApplyChanges()
		{
			if (!_dataChanged) return;

			ItemState.MemoPopup1 = memoPopupEdit1.GetSelectedItem();

			ItemState.Combo1 = ItemInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue
				? comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String }
				: null;

			ItemState.Combo2 = ItemInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue
				? comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String }
				: null;

			ItemState.Combo3 = ItemInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue
				? comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String }
				: null;

			_dataChanged = false;
		}

		public void RaiseEditValueChanged()
		{
			if (!_allowToHandleEvents) return;
			_dataChanged = true;
			Container.RaiseEditValueChanged();
			SlideContainer.RaiseOutputStatuesChanged();
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}
	}
}
