using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using DevExpress.XtraEditors;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	//public partial class ResearchDataTabControl : UserControl
	public partial class ResearchDataTabControl : BaseToggleTabControl
	{
		private ResearchInfo.BundleListItem _defaultItem;
		private ResearchInfo.BundleListItem _currentListItem;

		public ResearchInfo.Tab1Info TabInfo { get; }
		public IntegratedSolutionState.ResearchDataTabState TabState { get; }

		public ResearchDataTabControl()
		{
			InitializeComponent();
		}

		public ResearchDataTabControl(ResearchInfo.Tab1Info tabInfo,
			IntegratedSolutionState.ResearchDataTabState tabState,
			ProductItemControl container) : base(container)
		{
			InitializeComponent();

			TabState = tabState;
			TabInfo = tabInfo;

			Text = TabInfo.Title;

			memoEditBundleLine1.MaskBox.MouseWheel += OnBundleMouseWheel;
			memoEditBundleLine2.MaskBox.MouseWheel += OnBundleMouseWheel;
			memoEditBundleLine3.MaskBox.MouseWheel += OnBundleMouseWheel;

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

			memoEditBundleLine1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(TabInfo.Item1Configuration);
			memoEditBundleLine2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(TabInfo.Item2Configuration);
			memoEditBundleLine3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(TabInfo.Item3Configuration);

			memoEditBundleLine1.Properties.NullText = TabInfo.Placeholder1 ?? memoEditBundleLine1.Properties.NullText;
			memoEditBundleLine2.Properties.NullText = TabInfo.Placeholder2 ?? memoEditBundleLine2.Properties.NullText;
			memoEditBundleLine3.Properties.NullText = TabInfo.Placeholder3 ?? memoEditBundleLine3.Properties.NullText;

			if (TabState.BundleState != null)
				toggleSwitch.IsOn = !TabState.BundleState.IsEmpty();
			else
				toggleSwitch.IsOn = TabInfo.ToggleSwitch.Value;

			var savedListItem = TabState.BundleState?.ToLitsItem();
			_defaultItem = TabInfo.BundleInfo.Items.FirstOrDefault(item => item.IsDefault);
			_currentListItem = TabInfo.BundleInfo.Items.FirstOrDefault(bundleListItem =>
								   ResearchInfo.BundleListItem.Equals(bundleListItem, savedListItem)) ??
							   TabInfo.BundleInfo.Items.FirstOrDefault();

			memoEditBundleLine1.EditValue = (savedListItem ?? _defaultItem)?.Value1;
			memoEditBundleLine2.EditValue = (savedListItem ?? _defaultItem)?.Value2;
			memoEditBundleLine3.EditValue = (savedListItem ?? _defaultItem)?.Value3;

			_allowHandleEvents = true;

			Initialized = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			TabState.Toggled = toggleSwitch.IsOn;

			if (toggleSwitch.IsOn)
			{
				var savedBundleState = new IntegratedSolutionState.ResearchBundleState
				{
					Item1 = memoEditBundleLine1.EditValue as String,
					Item2 = memoEditBundleLine2.EditValue as String,
					Item3 = memoEditBundleLine3.EditValue as String
				};

				TabState.BundleState = !ResearchInfo.BundleListItem.Equals(savedBundleState.ToLitsItem(), _defaultItem)
					? savedBundleState
					: null;
			}
			else if (toggleSwitch.IsOn != TabInfo.ToggleSwitch.Value)
			{
				TabState.BundleState = new IntegratedSolutionState.ResearchBundleState();
			}
			else
				TabState.BundleState = null;

			_dataChanged = false;
		}

		private void SelectNextItem()
		{
			var listItem = TabInfo.BundleInfo.Items
				.FirstOrDefault(item => _currentListItem == null || ResearchInfo.BundleListItem.Equals(_currentListItem, item));
			var currentItemIndex = TabInfo.BundleInfo.Items.IndexOf(listItem);
			var nextItem = TabInfo.BundleInfo.Items.ElementAtOrDefault(currentItemIndex + 1 < TabInfo.BundleInfo.Items.Count ? currentItemIndex + 1 : 0);
			_currentListItem = ResearchInfo.BundleListItem.Equals(_defaultItem, nextItem) ?
				null :
				nextItem;

			_allowHandleEvents = false;
			memoEditBundleLine1.EditValue = (_currentListItem ?? _defaultItem)?.Value1;
			memoEditBundleLine2.EditValue = (_currentListItem ?? _defaultItem)?.Value2;
			memoEditBundleLine3.EditValue = (_currentListItem ?? _defaultItem)?.Value3;
			RaiseEditValueChanged();
			_allowHandleEvents = true;
		}

		private void SelectPreviouseItem()
		{
			var listItem = TabInfo.BundleInfo.Items
				.FirstOrDefault(item => _currentListItem == null || ResearchInfo.BundleListItem.Equals(_currentListItem, item));
			var currentItemIndex = TabInfo.BundleInfo.Items.IndexOf(listItem);
			var nextItem = TabInfo.BundleInfo.Items.ElementAtOrDefault(currentItemIndex - 1 >= 0 ? currentItemIndex - 1 : TabInfo.BundleInfo.Items.Count - 1);
			_currentListItem = ResearchInfo.BundleListItem.Equals(_defaultItem, nextItem) ?
				null :
				nextItem;

			_allowHandleEvents = false;
			memoEditBundleLine1.EditValue = (_currentListItem ?? _defaultItem)?.Value1;
			memoEditBundleLine2.EditValue = (_currentListItem ?? _defaultItem)?.Value2;
			memoEditBundleLine3.EditValue = (_currentListItem ?? _defaultItem)?.Value3;
			RaiseEditValueChanged();
			_allowHandleEvents = true;
		}

		private void SelectItemFromList()
		{
			using (var form = new FormResearchBundleList())
			{
				form.LoadData(TabInfo.BundleInfo.Items, _currentListItem ?? _defaultItem);
				if (form.ShowDialog() == DialogResult.OK)
				{
					_currentListItem = form.SelectedItem;

					_allowHandleEvents = false;
					memoEditBundleLine1.EditValue = (_currentListItem ?? _defaultItem)?.Value1;
					memoEditBundleLine2.EditValue = (_currentListItem ?? _defaultItem)?.Value2;
					memoEditBundleLine3.EditValue = (_currentListItem ?? _defaultItem)?.Value3;
					RaiseEditValueChanged();
					_allowHandleEvents = true;
				}
			}
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnToggledSwitch(object sender, EventArgs e)
		{
			layoutControlGroupBundle.Enabled = toggleSwitch.IsOn;
			OnEditValueChanged(sender, e);
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

		private void OnBundleMouseWheel(object sender, MouseEventArgs e)
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
