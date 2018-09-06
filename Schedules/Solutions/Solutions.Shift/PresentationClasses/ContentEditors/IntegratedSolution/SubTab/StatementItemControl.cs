using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Interfaces;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using DevExpress.Skins;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	public partial class StatementItemControl : UserControl
	{
		private bool _allowHandleEvents;
		private bool _dataChanged;

		private readonly List<ListDataItem> _comboList = new List<ListDataItem>();
		private readonly List<ListDataItem> _memoPopupList = new List<ListDataItem>();
		private IntegratedSolutionState.StatementItemState _itemState;
		private MainFormStyleConfiguration _styleConfiguration;
		private CheckboxInfo _comboToggleInfo;

		public event EventHandler<EventArgs> EditValueChanged;

		public StatementItemControl()
		{
			InitializeComponent();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemCombo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, RectangleHelper.ScaleHorizontal(layoutControlItemCombo.Padding.Right, scaleFactor.Width), 0, 0);
		}

		public void Init(
			CheckboxInfo comboToggleInfo,
			IList<ListDataItem> comboList,
			CheckboxInfo memoPopupToggleInfo,
			IList<ListDataItem> memoPopupList,
			IntegratedSolutionState.StatementItemState itemState,
			TextEditorConfiguration comboConfiguration,
			TextEditorConfiguration memoPopupConfiguration,
			MainFormStyleConfiguration styleConfiguration,
			ISolutionsResourceManager resourceManager)
		{
			_allowHandleEvents = false;

			_comboToggleInfo = comboToggleInfo;

			_comboList.Clear();
			_comboList.AddRange(comboList);

			_memoPopupList.Clear();
			_memoPopupList.AddRange(memoPopupList);

			_itemState = itemState;

			_styleConfiguration = styleConfiguration;

			comboBoxEditCombo.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(comboConfiguration);

			checkEditCombo.Text = String.Format("<color=gray>{0}</color>", comboToggleInfo.Title ?? "header");
			layoutControlItemToggleCombo.MinSize = new Size(checkEditCombo.Width, checkEditCombo.Height);
			layoutControlItemToggleCombo.MaxSize = new Size(checkEditCombo.Width, checkEditCombo.Height);

			comboBoxEditCombo.Properties.Items.Clear();
			comboBoxEditCombo.Properties.Items.AddRange(_comboList);
			comboBoxEditCombo.Properties.NullText =
				_comboList.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo.Properties.NullText;

			memoPopupEdit.Init(_memoPopupList,
				_memoPopupList.FirstOrDefault(item => item.IsDefault || item.IsPlaceholder),
				memoPopupConfiguration,
				_styleConfiguration,
				resourceManager);
			memoPopupEdit.EditValueChanged += OnEditValueChanged;

			if (_itemState.Combo != null)
			{
				checkEditCombo.Checked = !String.IsNullOrEmpty(_itemState.Combo?.Value);
				comboBoxEditCombo.EditValue = _itemState.Combo;
			}
			else
			{
				checkEditCombo.Checked = comboToggleInfo.Value;
				comboBoxEditCombo.EditValue = _comboList.FirstOrDefault(item => item.IsDefault && !item.IsPlaceholder);
			}

			if (_itemState.MemoPopup != null)
			{
				checkEditMemoPopup.Checked = !String.IsNullOrEmpty(_itemState.MemoPopup?.Value);
				memoPopupEdit.LoadData(_itemState.MemoPopup);
			}
			else
			{
				checkEditMemoPopup.Checked = memoPopupToggleInfo.Value;
			}

			_allowHandleEvents = true;
		}

		public void ApplyChanges()
		{
			if (!_dataChanged) return;

			if (checkEditCombo.Checked)
			{
				_itemState.Combo = _comboList.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo.EditValue
					? comboBoxEditCombo.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo.EditValue as String }
					: null;
			}
			else if (checkEditCombo.Checked != _comboToggleInfo.Value)
			{
				_itemState.Combo = new ListDataItem();
			}
			else
				_itemState.Combo = null;

			if (checkEditMemoPopup.Checked)
				_itemState.MemoPopup = memoPopupEdit.GetSelectedItem();
			else
				_itemState.MemoPopup = new ListDataItem();

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowHandleEvents) return;
			_dataChanged = true;
			EditValueChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnToggleComboCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemCombo.Enabled = checkEditCombo.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnToggleMemoPopupCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemMemoPopup.Enabled = checkEditMemoPopup.Checked;
			OnEditValueChanged(sender, e);
		}
	}
}
