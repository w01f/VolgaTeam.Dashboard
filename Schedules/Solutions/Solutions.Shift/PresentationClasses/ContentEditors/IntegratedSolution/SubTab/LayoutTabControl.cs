using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using DevExpress.XtraEditors;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	//public partial class LayoutTabControl : UserControl
	public partial class LayoutTabControl : BaseToggleTabControl
	{
		public LayoutItem CurrentLayoutItem { get; private set; }

		public StyleInfo.LayoutTabInfo TabInfo { get; }
		public IntegratedSolutionState.LayoutTabState TabState { get; }

		public LayoutTabControl()
		{
			InitializeComponent();
		}

		public LayoutTabControl(StyleInfo.LayoutTabInfo tabInfo,
			IntegratedSolutionState.LayoutTabState tabState,
			ProductItemControl container) : base(container)
		{
			InitializeComponent();

			TabState = tabState;
			TabInfo = tabInfo;

			Text = TabInfo.Title;

			pictureEdit.MouseWheel += OnClipartMouseWheel;

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
			}
			if (!Container.Container.TabInfo.CommonEditorConfiguration.ForeColor.IsEmpty)
			{
				layoutControl.Appearance.Control.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlFocused.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.ForeColor;
			}
			if (!Container.Container.TabInfo.CommonEditorConfiguration.DropdownForeColor.IsEmpty)
			{
				layoutControl.Appearance.ControlDropDown.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
				layoutControl.Appearance.ControlDropDownHeader.ForeColor = Container.Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
			}
		}

		public override void LoadData()
		{
			if (Initialized) return;

			_allowHandleEvents = false;

			CurrentLayoutItem = TabState.SavedLayout ?? TabInfo.DefaultItem;
			pictureEdit.Image = CurrentLayoutItem != null ? Image.FromFile(CurrentLayoutItem.FilePath) : null;

			_allowHandleEvents = true;

			Initialized = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			TabState.SavedLayout = CurrentLayoutItem;

			_dataChanged = false;
		}

		private void SelectNextItem()
		{
			var listItem = TabInfo.LayoutItems
				.FirstOrDefault(item => CurrentLayoutItem == null || String.Equals(CurrentLayoutItem.FilePath, item.FilePath, StringComparison.OrdinalIgnoreCase));
			var currentItemIndex = TabInfo.LayoutItems.IndexOf(listItem);
			var nextLayoutItem = TabInfo.LayoutItems.ElementAtOrDefault(currentItemIndex - 1 >= 0 ? currentItemIndex - 1 : TabInfo.LayoutItems.Count - 1);
			CurrentLayoutItem = nextLayoutItem;

			pictureEdit.Image = CurrentLayoutItem != null ? Image.FromFile(CurrentLayoutItem.FilePath) : null;
		}

		private void SelectPreviouseItem()
		{
			var listItem = TabInfo.LayoutItems
				.FirstOrDefault(item => CurrentLayoutItem == null || String.Equals(CurrentLayoutItem.FilePath, item.FilePath, StringComparison.OrdinalIgnoreCase));
			var currentItemIndex = TabInfo.LayoutItems.IndexOf(listItem);
			var nextLayoutItem = TabInfo.LayoutItems.ElementAtOrDefault(currentItemIndex + 1 < TabInfo.LayoutItems.Count ? currentItemIndex + 1 : 0);
			CurrentLayoutItem = nextLayoutItem;

			pictureEdit.Image = CurrentLayoutItem != null ? Image.FromFile(CurrentLayoutItem.FilePath) : null;
		}

		private void SelectItemFromList()
		{
			using (var form = new FormImageList())
			{
				form.LoadData(TabInfo.LayoutItems.Select(item => item.FilePath).ToList(), (CurrentLayoutItem ?? TabInfo.DefaultItem)?.FilePath);
				if (form.ShowDialog() == DialogResult.OK)
				{
					CurrentLayoutItem = LayoutItem.FromFile(form.SelectedFile);

					pictureEdit.Image = CurrentLayoutItem != null ? Image.FromFile(CurrentLayoutItem.FilePath) : null;
				}
			}
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
			if (!_allowHandleEvents) return;
			SlideContainer.RaiseSlideTypeChanged();
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

		private void OnClipartMouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
				OnUpButtonClick(pictureEditUp, EventArgs.Empty);
			else
				OnDownButtonClick(pictureEditDown, EventArgs.Empty);
		}

		private void OnPictureEditMouseHover(object sender, EventArgs e)
		{
			var pictureEdit = (PictureEdit)sender;
			pictureEdit.Properties.Appearance.BackColor =
				pictureEdit.Properties.AppearanceFocused.BackColor =
				SlideContainer.StyleConfiguration.ToggleHoverColor ?? pictureEdit.BackColor;
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
