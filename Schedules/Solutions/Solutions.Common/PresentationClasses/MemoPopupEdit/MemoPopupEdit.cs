using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Interfaces;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraEditors;

namespace Asa.Solutions.Common.PresentationClasses.MemoPopupEdit
{
	public partial class MemoPopupEdit : UserControl
	{
		private bool _allowHandleEvents;

		private readonly List<ListDataItem> _sourceList = new List<ListDataItem>();
		private ListDataItem _defaultItem;
		private ListDataItem _currentListItem;

		private MainFormStyleConfiguration _styleConfiguration;

		public event EventHandler<EventArgs> EditValueChanged;

		public MemoPopupEdit()
		{
			InitializeComponent();

			memoEdit.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemUp.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUp.MaxSize, scaleFactor);
			layoutControlItemUp.MinSize = RectangleHelper.ScaleSize(layoutControlItemUp.MinSize, scaleFactor);
			layoutControlItemList.MaxSize = RectangleHelper.ScaleSize(layoutControlItemList.MaxSize, scaleFactor);
			layoutControlItemList.MinSize = RectangleHelper.ScaleSize(layoutControlItemList.MinSize, scaleFactor);
			layoutControlItemDown.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDown.MaxSize, scaleFactor);
			layoutControlItemDown.MinSize = RectangleHelper.ScaleSize(layoutControlItemDown.MinSize, scaleFactor);
		}

		public void Init(IList<ListDataItem> sourceList,
			ListDataItem defaultItem,
			TextEditorConfiguration editorConfiguration,
			MainFormStyleConfiguration styleConfiguration,
			ISolutionsResourceManager resourceManager)
		{
			_sourceList.Clear();
			_sourceList.AddRange(sourceList);

			_defaultItem = defaultItem;

			_styleConfiguration = styleConfiguration;

			_allowHandleEvents = false;

			memoEdit.Properties.NullText = _defaultItem != null && _defaultItem.IsPlaceholder ? _defaultItem.Value : memoEdit.Properties.NullText;

			var listItem = _sourceList
				.Where(item => !item.IsPlaceholder)
				.FirstOrDefault(item => _defaultItem == null || !_defaultItem.IsPlaceholder && String.Equals(_defaultItem.Value, item.Value, StringComparison.OrdinalIgnoreCase));

			_currentListItem = listItem;
			memoEdit.EditValue = listItem ?? _defaultItem;

			_allowHandleEvents = true;

			if (editorConfiguration != null)
			{
				if (editorConfiguration.FontSize.HasValue)
				{
					var fontSizeDelte = editorConfiguration.FontSize.Value - TextEditorConfiguration.DefaultFontSize;
					layoutControl.Appearance.Control.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlFocused.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlDropDown.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlDropDownHeader.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlDisabled.FontSizeDelta = fontSizeDelte;
					layoutControl.Appearance.ControlReadOnly.FontSizeDelta = fontSizeDelte;
				}
				if (!editorConfiguration.BackColor.IsEmpty)
				{
					layoutControl.Appearance.Control.BackColor = editorConfiguration.BackColor;
					layoutControl.Appearance.ControlFocused.BackColor = editorConfiguration.BackColor;
				}
				if (!editorConfiguration.ForeColor.IsEmpty)
				{
					layoutControl.Appearance.Control.ForeColor = editorConfiguration.ForeColor;
					layoutControl.Appearance.ControlFocused.ForeColor = editorConfiguration.ForeColor;
				}
				if (!editorConfiguration.DropdownForeColor.IsEmpty)
				{
					layoutControl.Appearance.ControlDropDown.ForeColor = editorConfiguration.DropdownForeColor;
					layoutControl.Appearance.ControlDropDownHeader.ForeColor = editorConfiguration.DropdownForeColor;
				}
			}

			pictureEditUp.Image = resourceManager?.SolutionMemoPopupUp ?? pictureEditUp.Image;
			pictureEditDown.Image = resourceManager?.SolutionMemoPopupDown ?? pictureEditDown.Image;
			pictureEditList.Image = resourceManager?.SolutionMemoPopupList ?? pictureEditList.Image;
		}

		public void LoadData(ListDataItem selectedItem)
		{
			_allowHandleEvents = false;

			_currentListItem = selectedItem;

			memoEdit.EditValue = selectedItem ?? _defaultItem;

			_allowHandleEvents = true;
		}

		public ListDataItem GetSelectedItem()
		{
			return _currentListItem;
		}

		private void SelectNextItem()
		{
			var listItem = _sourceList
				.Where(item => !item.IsPlaceholder)
				.FirstOrDefault(item => _currentListItem == null || !_defaultItem.IsPlaceholder && String.Equals(_currentListItem.Value, item.Value, StringComparison.OrdinalIgnoreCase));
			var currentItemIndex = _sourceList.IndexOf(listItem);
			var nextItem = _sourceList.ElementAtOrDefault(currentItemIndex + 1 < _sourceList.Count ? currentItemIndex + 1 : 0);
			_currentListItem = String.Equals(_defaultItem?.Value, nextItem?.Value, StringComparison.OrdinalIgnoreCase) ?
				null :
				nextItem;

			_allowHandleEvents = false;
			memoEdit.EditValue = _currentListItem ?? _defaultItem;
			RaiseEditValueChanged();
			_allowHandleEvents = true;
		}

		private void SelectPreviouseItem()
		{
			var listItem = _sourceList
				.Where(item => !item.IsPlaceholder)
				.FirstOrDefault(item => _currentListItem == null || !_defaultItem.IsPlaceholder && String.Equals(_currentListItem.Value, item.Value, StringComparison.OrdinalIgnoreCase));
			var currentItemIndex = _sourceList.IndexOf(listItem);
			var nextItem = _sourceList.ElementAtOrDefault(currentItemIndex - 1 >= 0 ? currentItemIndex - 1 : _sourceList.Count - 1);
			_currentListItem = String.Equals(_defaultItem?.Value, nextItem?.Value, StringComparison.OrdinalIgnoreCase) ?
				null :
				nextItem;

			_allowHandleEvents = false;
			memoEdit.EditValue = _currentListItem ?? _defaultItem;
			RaiseEditValueChanged();
			_allowHandleEvents = true;
		}

		private void SelectItemFromList()
		{
			using (var form = new FormList())
			{
				form.LoadData(_sourceList, _currentListItem ?? _defaultItem);
				if (form.ShowDialog() == DialogResult.OK)
				{
					_currentListItem = form.SelectedItem;

					_allowHandleEvents = false;
					memoEdit.EditValue = _currentListItem ?? _defaultItem;
					RaiseEditValueChanged();
					_allowHandleEvents = true;
				}
			}
		}

		private void RaiseEditValueChanged()
		{
			EditValueChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnMemoEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowHandleEvents) return;
			_allowHandleEvents = false;

			var memoItem = memoEdit.EditValue as ListDataItem ?? new ListDataItem { Value = memoEdit.EditValue as String };
			_currentListItem = String.Equals(_defaultItem?.Value, memoItem?.Value, StringComparison.OrdinalIgnoreCase) ?
				null :
				memoItem;

			RaiseEditValueChanged();

			_allowHandleEvents = true;
		}

		private void OnUpButtonClick(object sender, EventArgs e)
		{
			SelectPreviouseItem();
		}

		private void OnDownButtonClick(object sender, EventArgs e)
		{
			SelectNextItem();
		}

		private void OnListButtonClick(object sender, EventArgs e)
		{
			SelectItemFromList();
		}

		private void OnPictureEditMouseHover(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.Properties.Appearance.BackColor =
				pictureEdit.Properties.AppearanceFocused.BackColor =
				_styleConfiguration.ToggleHoverColor ?? pictureEdit.BackColor;
		}

		private void OnPictureEditMouseMove(object sender, MouseEventArgs e)
		{
			OnPictureEditMouseHover(sender, e);
		}

		private void OnPictureEditMouseLeave(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.Properties.Appearance.BackColor = Color.Transparent;
			pictureEdit.Properties.AppearanceFocused.BackColor = Color.Transparent;
		}
	}
}
