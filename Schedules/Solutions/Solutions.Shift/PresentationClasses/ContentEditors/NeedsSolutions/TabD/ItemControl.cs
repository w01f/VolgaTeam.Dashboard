using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabD
{
	public partial class ItemControl : UserControl
	{
		private bool _allowHandleEvents;
		private readonly List<SolutionsItemInfo> _itemInfoList = new List<SolutionsItemInfo>();
		private SolutionsItemInfo _defaultItem;
		private MainFormStyleConfiguration _styleConfiguration;

		public event EventHandler<EventArgs> EditValueChanged;

		public ItemControl()
		{
			InitializeComponent();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemWipe.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWipe.MaxSize, scaleFactor);
			layoutControlItemWipe.MinSize = RectangleHelper.ScaleSize(layoutControlItemWipe.MinSize, scaleFactor);
			emptySpaceItemButtons.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemButtons.MaxSize, scaleFactor);
			emptySpaceItemButtons.MinSize = RectangleHelper.ScaleSize(emptySpaceItemButtons.MinSize, scaleFactor);
		}

		public void Init(
			IList<SolutionsItemInfo> itemInfoList,
			SolutionsItemInfo defaultItem,
			string comboPlaceholder,
			TextEditorConfiguration comboConfiguration,
			TextEditorConfiguration subheaderConfiguration,
			MainFormStyleConfiguration styleConfiguration)
		{
			_allowHandleEvents = false;

			_itemInfoList.Clear();
			_itemInfoList.AddRange(itemInfoList);

			_defaultItem = defaultItem;

			_styleConfiguration = styleConfiguration;

			comboBoxEditCombo.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(comboConfiguration);
			memoEditSubheader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(subheaderConfiguration);

			comboBoxEditCombo.Properties.Items.Clear();
			comboBoxEditCombo.Properties.Items.AddRange(_itemInfoList);
			comboBoxEditCombo.Properties.NullText =
				comboPlaceholder ??
				comboBoxEditCombo.Properties.NullText;

			comboBoxEditCombo.EditValue = _defaultItem;
			memoEditSubheader.EditValue = _defaultItem?.SubHeaderDefaultValue;

			_allowHandleEvents = true;
		}

		public void LoadData(NeedsSolutionsState.SolutionsItemState itemState)
		{
			_allowHandleEvents = false;

			var itemInfo = _itemInfoList.FirstOrDefault(item =>
				String.Equals(item.Title, itemState?.Title, StringComparison.OrdinalIgnoreCase));

			if (itemInfo != null)
				comboBoxEditCombo.EditValue = itemInfo;
			else
				comboBoxEditCombo.EditValue = itemState?.Title;

			memoEditSubheader.EditValue = itemState?.Subheader ?? itemInfo?.SubHeaderDefaultValue;

			UpdateWipeButtons();

			_allowHandleEvents = true;
		}

		public NeedsSolutionsState.SolutionsItemState GetSavedState()
		{
			var itemState = new NeedsSolutionsState.SolutionsItemState();

			var comboValue = comboBoxEditCombo.EditValue?.ToString();
			itemState.Title = !String.Equals(comboValue, _defaultItem?.Title, StringComparison.OrdinalIgnoreCase) ? comboValue : null;

			var subheaderValue = memoEditSubheader.EditValue as String;
			itemState.Subheader = !String.Equals(subheaderValue, _defaultItem?.SubHeaderDefaultValue, StringComparison.OrdinalIgnoreCase) ? subheaderValue : null;

			if (!(String.IsNullOrWhiteSpace(itemState.Title) && String.IsNullOrWhiteSpace(itemState.Subheader)))
				return itemState;
			return null;
		}

		private void UpdateWipeButtons()
		{
			layoutControlItemWipe.Visibility = GetSavedState() != null ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowHandleEvents) return;
			UpdateWipeButtons();
			EditValueChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnComboEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowHandleEvents) return;

			var itemInfo = comboBoxEditCombo.EditValue as SolutionsItemInfo;
			memoEditSubheader.EditValue = itemInfo?.SubHeaderDefaultValue ?? memoEditSubheader.EditValue;

			OnEditValueChanged(sender, e);
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

		private void OnWipeClick(object sender, EventArgs e)
		{
			comboBoxEditCombo.EditValue = _defaultItem;
			memoEditSubheader.EditValue = _defaultItem?.SubHeaderDefaultValue;
		}
	}
}
