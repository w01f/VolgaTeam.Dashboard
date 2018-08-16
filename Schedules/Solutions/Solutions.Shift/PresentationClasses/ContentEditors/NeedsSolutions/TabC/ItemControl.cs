using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabC
{
	//public partial class ItemControl : UserControl
	public partial class ItemControl : BaseTabCSubControl
	{
		public SolutionsItemInfo ItemInfo { get; }

		public ItemControl()
		{
			InitializeComponent();
		}

		public ItemControl(SolutionsItemInfo itemInfo, NeedsSolutionsTabCControl container) : base(container)
		{
			InitializeComponent();

			ItemInfo = itemInfo;

			Text = ItemInfo.Title;

			memoEditSubheader.EnableSelectAll().RaiseNullValueIfEditorEmpty();

			if (ItemInfo.SubheaderConfiguration.FontSize.HasValue)
			{
				var fontSizeDelte = ItemInfo.SubheaderConfiguration.FontSize.Value - TextEditorConfiguration.DefaultFontSize;
				layoutControl.Appearance.Control.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlFocused.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDown.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDropDownHeader.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlDisabled.FontSizeDelta = fontSizeDelte;
				layoutControl.Appearance.ControlReadOnly.FontSizeDelta = fontSizeDelte;
			}
			if (!ItemInfo.SubheaderConfiguration.BackColor.IsEmpty)
			{
				layoutControl.Appearance.Control.BackColor = ItemInfo.SubheaderConfiguration.BackColor;
				layoutControl.Appearance.ControlFocused.BackColor = ItemInfo.SubheaderConfiguration.BackColor;
			}
			if (!ItemInfo.SubheaderConfiguration.ForeColor.IsEmpty)
			{
				layoutControl.Appearance.Control.ForeColor = ItemInfo.SubheaderConfiguration.ForeColor;
				layoutControl.Appearance.ControlFocused.ForeColor = ItemInfo.SubheaderConfiguration.ForeColor;
			}
			if (!ItemInfo.SubheaderConfiguration.DropdownForeColor.IsEmpty)
			{
				layoutControl.Appearance.ControlDropDown.ForeColor = ItemInfo.SubheaderConfiguration.DropdownForeColor;
				layoutControl.Appearance.ControlDropDownHeader.ForeColor = ItemInfo.SubheaderConfiguration.DropdownForeColor;
			}
		}


		public override void LoadData()
		{
			_allowToSave = false;

			var savedState = SlideContainer.EditedContent.NeedsSolutionsState.TabC.Items.FirstOrDefault(item =>
				String.Equals(item.Id, ItemInfo.Id, StringComparison.OrdinalIgnoreCase));

			pictureEdit.Image = ItemInfo.ClipartImage;
			memoEditSubheader.EditValue = savedState?.Subheader ?? ItemInfo.SubHeaderDefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			var savedState = SlideContainer.EditedContent.NeedsSolutionsState.TabC.Items.FirstOrDefault(item =>
				String.Equals(item.Id, ItemInfo.Id, StringComparison.OrdinalIgnoreCase));

			if (savedState == null)
			{
				savedState = new NeedsSolutionsState.SolutionsItemState();
				savedState.Id = ItemInfo.Id;
				SlideContainer.EditedContent.NeedsSolutionsState.TabC.Items.Add(savedState);
			}

			savedState.Index = TabControl.TabPages.IndexOf(this);
			savedState.Subheader =
				memoEditSubheader.EditValue as String != ItemInfo.SubHeaderDefaultValue
					? memoEditSubheader.EditValue as String ?? String.Empty
					: null;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}
	}
}
