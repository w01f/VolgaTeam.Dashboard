using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using DevExpress.XtraEditors;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	//public partial class ImageTabControl : UserControl
	public partial class ImageTabControl : BaseToggleTabControl
	{
		private ImageClipartObject _defaultClipartObject;
		private ImageClipartObject _currentClipartObject;

		public StyleInfo.ImageTabInfo TabInfo { get; }
		public IntegratedSolutionState.ImageTabState TabState { get; }

		public ImageTabControl()
		{
			InitializeComponent();
		}

		public ImageTabControl(StyleInfo.ImageTabInfo tabInfo,
			IntegratedSolutionState.ImageTabState tabState,
			ProductItemControl container) : base(container)
		{
			InitializeComponent();

			TabState = tabState;
			TabInfo = tabInfo;

			Text = TabInfo.Title;

			clipartEditContainer.MouseWheel += OnClipartMouseWheel;

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

			var savedImageClipart = TabState.Clipart is ImageClipartObject imageClipart ? imageClipart : null;
			_defaultClipartObject = ImageClipartObject.FromFile(TabInfo.DefaultImagePath);
			_currentClipartObject = TabInfo.ImageFiles.Any(item =>
										String.Equals(item, savedImageClipart?.FilePath, StringComparison.OrdinalIgnoreCase)) ?
									savedImageClipart :
									ImageClipartObject.FromFile(TabInfo.ImageFiles.FirstOrDefault());


			clipartEditContainer.Init(_defaultClipartObject, new ClipartConfiguration(), Container.Container.TabPageContainer.ParentControl);
			clipartEditContainer.EditValueChanged += OnEditValueChanged;

			clipartEditContainer.LoadData(TabState.Clipart ?? _defaultClipartObject);

			_allowHandleEvents = true;

			Initialized = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			TabState.Clipart = clipartEditContainer.GetActiveClipartObject();

			_dataChanged = false;
		}

		private void SelectNextItem()
		{
			var imageFilePath = TabInfo.ImageFiles
				.FirstOrDefault(item => _currentClipartObject == null || String.Equals(_currentClipartObject.FilePath, item, StringComparison.OrdinalIgnoreCase));
			var currentItemIndex = TabInfo.ImageFiles.IndexOf(imageFilePath);
			var nextImageFilePath = TabInfo.ImageFiles.ElementAtOrDefault(currentItemIndex + 1 < TabInfo.ImageFiles.Count ? currentItemIndex + 1 : 0);
			_currentClipartObject = ImageClipartObject.FromFile(nextImageFilePath);

			_allowHandleEvents = false;
			clipartEditContainer.LoadData(_currentClipartObject);
			_allowHandleEvents = true;
			RaiseEditValueChanged();
		}

		private void SelectPreviouseItem()
		{
			var imageFilePath = TabInfo.ImageFiles
				.FirstOrDefault(item => _currentClipartObject == null || String.Equals(_currentClipartObject.FilePath, item, StringComparison.OrdinalIgnoreCase));
			var currentItemIndex = TabInfo.ImageFiles.IndexOf(imageFilePath);
			var nextImageFilePath = TabInfo.ImageFiles.ElementAtOrDefault(currentItemIndex - 1 >= 0 ? currentItemIndex - 1 : TabInfo.ImageFiles.Count - 1);
			_currentClipartObject = ImageClipartObject.FromFile(nextImageFilePath);

			_allowHandleEvents = false;
			clipartEditContainer.LoadData(_currentClipartObject);
			_allowHandleEvents = true;
			RaiseEditValueChanged();
		}

		private void SelectItemFromList()
		{
			using (var form = new FormImageList())
			{
				form.LoadData(TabInfo.ImageFiles, (_currentClipartObject ?? _defaultClipartObject)?.FilePath);
				if (form.ShowDialog() == DialogResult.OK)
				{
					_currentClipartObject = ImageClipartObject.FromFile(form.SelectedFile);

					_allowHandleEvents = false;
					clipartEditContainer.LoadData(_currentClipartObject);
					_allowHandleEvents = true;
					RaiseEditValueChanged();
				}
			}
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
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
