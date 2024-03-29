﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.Approach;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach.TabC
{
	//public partial class ItemControl : UserControl
	public partial class ItemControl : XtraTabPage
	{
		private readonly ApproachTabCControl _container;
		private const string EmptyTabNameFormat = "Approach #{0}";

		private bool _allowHandleEvents;

		private readonly List<ApproachItemInfo> _sourceList = new List<ApproachItemInfo>();
		private ApproachItemInfo _currentItemInfo;
		private string _defaultTabName;

		protected virtual int DefaultItemIndex { get; }

		public ApproachState.ApproachItemState ItemState { get; private set; }

		public event EventHandler<EventArgs> EditValueChanged;

		public ItemControl(ApproachTabCControl container)
		{
			_container = container;
			InitializeComponent();

			memoEditSubheader.EnableSelectAll().RaiseNullValueIfEditorEmpty();

			ItemState = ApproachState.ApproachItemState.Empty();
			ItemState.Index = DefaultItemIndex;

			pictureEditClipart.MouseWheel += OnClipartMouseWheel;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemUp.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUp.MaxSize, scaleFactor);
			layoutControlItemUp.MinSize = RectangleHelper.ScaleSize(layoutControlItemUp.MinSize, scaleFactor);
			layoutControlItemList.MaxSize = RectangleHelper.ScaleSize(layoutControlItemList.MaxSize, scaleFactor);
			layoutControlItemList.MinSize = RectangleHelper.ScaleSize(layoutControlItemList.MinSize, scaleFactor);
			layoutControlItemDown.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDown.MaxSize, scaleFactor);
			layoutControlItemDown.MinSize = RectangleHelper.ScaleSize(layoutControlItemDown.MinSize, scaleFactor);
			layoutControlItemWipe.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWipe.MaxSize, scaleFactor);
			layoutControlItemWipe.MinSize = RectangleHelper.ScaleSize(layoutControlItemWipe.MinSize, scaleFactor);
			emptySpaceItemWipe.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemWipe.MaxSize, scaleFactor);
			emptySpaceItemWipe.MinSize = RectangleHelper.ScaleSize(emptySpaceItemWipe.MinSize, scaleFactor);
		}

		public void Init(string defaultTabName,
			TextEditorConfiguration editorConfiguration)
		{
			_sourceList.Clear();
			_sourceList.AddRange(_container.CustomTabInfo.ApproachItems);

			_defaultTabName = defaultTabName;

			UpdateDataControl();

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
		}

		public void LoadData(ApproachState.ApproachItemState selectedItem)
		{
			ItemState = selectedItem;

			_currentItemInfo = _sourceList.FirstOrDefault(itemInfo =>
				String.Equals(itemInfo.Title, ItemState.Title, StringComparison.OrdinalIgnoreCase)) ??
				_sourceList.FirstOrDefault();

			UpdateDataControl();
		}

		private void UpdateDataControl()
		{
			_allowHandleEvents = false;

			Text = ItemState.Title ?? _defaultTabName ?? String.Format(EmptyTabNameFormat, DefaultItemIndex + 1);

			layoutControlItemWipe.Visibility =
				!ItemState.IsEmpty() ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlItemClipart.Visibility =
				!ItemState.IsEmpty() ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlItemClipartPlaceholder.Visibility =
				ItemState.IsEmpty() ? LayoutVisibility.Always : LayoutVisibility.Never;
			memoEditSubheader.Enabled = !ItemState.IsEmpty();

			pictureEditClipart.Image = (ItemState.Clipart as ImageClipartObject)?.Image;
			memoEditSubheader.EditValue = ItemState?.Subheader;

			_allowHandleEvents = true;
		}

		private void RaiseEditValueChanged()
		{
			if (!_allowHandleEvents) return;
			EditValueChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnSubheaderEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowHandleEvents) return;

			ItemState.Subheader = memoEditSubheader.EditValue as String;

			RaiseEditValueChanged();
		}

		private void OnUpButtonClick(object sender, EventArgs e)
		{
			if (!_sourceList.Any()) return;

			var currentItemIndex = _sourceList.IndexOf(_currentItemInfo);
			var nextIndex = currentItemIndex > 0 ? currentItemIndex - 1 : _sourceList.Count - 1;
			_currentItemInfo = _sourceList[nextIndex];
			ItemState = ApproachState.ApproachItemState.FromItemInfo(_currentItemInfo);

			UpdateDataControl();
			RaiseEditValueChanged();
		}

		private void OnDownButtonClick(object sender, EventArgs e)
		{
			if (!_sourceList.Any()) return;

			var currentItemIndex = _sourceList.IndexOf(_currentItemInfo);
			var nextIndex = currentItemIndex < _sourceList.Count - 1 ? currentItemIndex + 1 : 0;
			_currentItemInfo = _sourceList[nextIndex];
			ItemState = ApproachState.ApproachItemState.FromItemInfo(_currentItemInfo);

			UpdateDataControl();
			RaiseEditValueChanged();
		}

		private void OnClipartMouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
				OnUpButtonClick(pictureEditUp, EventArgs.Empty);
			else
				OnDownButtonClick(pictureEditDown, EventArgs.Empty);
		}

		private void OnListButtonClick(object sender, EventArgs e)
		{
			if (!_sourceList.Any()) return;

			using (var form = new FormList())
			{
				form.LoadData(_sourceList, _currentItemInfo, _container.CustomTabInfo.FormListConfiguration);
				if (form.ShowDialog() == DialogResult.OK)
				{
					_currentItemInfo = form.SelectedItem;
					ItemState = ApproachState.ApproachItemState.FromItemInfo(_currentItemInfo);

					UpdateDataControl();
					RaiseEditValueChanged();
				}
			}
		}

		private void OnWipeButtonClick(object sender, EventArgs e)
		{
			ItemState = ApproachState.ApproachItemState.Empty();
			_currentItemInfo = _sourceList.FirstOrDefault();

			UpdateDataControl();
			RaiseEditValueChanged();
		}

		private void OnPictureEditMouseHover(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.Properties.Appearance.BackColor =
				pictureEdit.Properties.AppearanceFocused.BackColor =
					_container.SlideContainer.StyleConfiguration.ToggleHoverColor ?? pictureEdit.BackColor;
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

	public class Item1Control : ItemControl
	{
		protected override int DefaultItemIndex => 0;

		public Item1Control(ApproachTabCControl container) : base(container) { }
	}

	public class Item2Control : ItemControl
	{
		protected override int DefaultItemIndex => 1;

		public Item2Control(ApproachTabCControl container) : base(container) { }
	}

	public class Item3Control : ItemControl
	{
		protected override int DefaultItemIndex => 2;

		public Item3Control(ApproachTabCControl container) : base(container) { }
	}

	public class Item4Control : ItemControl
	{
		protected override int DefaultItemIndex => 3;

		public Item4Control(ApproachTabCControl container) : base(container) { }
	}
}
