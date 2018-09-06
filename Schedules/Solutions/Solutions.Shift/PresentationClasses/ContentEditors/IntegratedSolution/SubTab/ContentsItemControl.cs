using System;
using Asa.Business.Solutions.Common.Configuration;
using DevExpress.Utils;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	//public partial class ContentsItemControl : UserControl
	public partial class ContentsItemControl : BaseSubTabControl
	{
		public event EventHandler<ProductClickedEventArgs> ItemClicked;

		public ContentsItemControl()
		{
			InitializeComponent();
		}

		public ContentsItemControl(IntegratedSolutionSubTabControl container) : base(container)
		{
			InitializeComponent();

			Text = "Solutions";
			ShowCloseButton = DefaultBoolean.False;

			_productSelectorControl.Init(Container.CustomTabInfo.Products);
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

		public void UpdateSlideCount(int slideCount)
		{
			simpleLabelItemSlideCount.Text = String.Format("<size=+2><color=gray>Slide Count: {0}</color></size>", slideCount);
		}

		private void OnProductClicked(object sender, ProductClickedEventArgs e)
		{
			ItemClicked?.Invoke(this, e);
		}
	}
}
