using System;
using Asa.Business.Solutions.Common.Configuration;
using DevExpress.Utils;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract.TabA
{
	//public partial class ContentsItemControl : UserControl
	public partial class ContentsItemControl : BaseSubTabControl
	{
		public event EventHandler<ProductClickingEventArgs> ItemClicking;
		public event EventHandler<ProductClickedEventArgs> ItemClicked;
		
		public ContentsItemControl()
		{
			InitializeComponent();
		}

		public ContentsItemControl(ContractTabAControl container) : base(container)
		{
			InitializeComponent();

			Text = Container.CustomTabInfo.TabSelector.ContentsTabName;
			ShowCloseButton = DefaultBoolean.False;

			simpleLabelItemDescription.Text = String.Format("<size=+2><color=gray>{0}</color></size>", Container.CustomTabInfo.TabSelector.ContentsTabDescription);

			_productSelectorControl.Init(Container.CustomTabInfo.Products);
			_productSelectorControl.ItemClicking += OnProductClicking;
			_productSelectorControl.ItemClicked += OnProductClicked;

			if (Container.TabInfo.CommonEditorConfiguration.FontSize.HasValue)
			{
				var fontSizeDelte = Container.TabInfo.CommonEditorConfiguration.FontSize.Value - TextEditorConfiguration.DefaultFontSize;
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
			}
			if (!Container.TabInfo.CommonEditorConfiguration.ForeColor.IsEmpty)
			{
				layoutControl.Appearance.Control.ForeColor = Container.TabInfo.CommonEditorConfiguration.ForeColor;
				layoutControl.Appearance.ControlFocused.ForeColor = Container.TabInfo.CommonEditorConfiguration.ForeColor;
			}
			if (!Container.TabInfo.CommonEditorConfiguration.DropdownForeColor.IsEmpty)
			{
				layoutControl.Appearance.ControlDropDown.ForeColor = Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
				layoutControl.Appearance.ControlDropDownHeader.ForeColor = Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
			}
		}

		private void OnProductClicking(object sender, ProductClickingEventArgs e)
		{
			ItemClicking?.Invoke(this, e);
		}

		private void OnProductClicked(object sender, ProductClickedEventArgs e)
		{
			ItemClicked?.Invoke(this, e);
		}
	}
}
