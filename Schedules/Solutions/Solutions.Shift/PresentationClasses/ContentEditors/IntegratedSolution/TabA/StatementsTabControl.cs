using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	//public partial class StatementsTabControl : UserControl
	public partial class StatementsTabControl : BaseToggleTabControl
	{
		public PositioningInfo.Tab1Info TabInfo { get; }
		public IntegratedSolutionState.StatementsTabState TabState { get; }

		public StatementsTabControl()
		{
			InitializeComponent();
		}

		public StatementsTabControl(PositioningInfo.Tab1Info tabInfo,
			IntegratedSolutionState.StatementsTabState tabState,
			ProductItemControl container) : base(container)
		{
			InitializeComponent();

			TabState = tabState;
			TabInfo = tabInfo;

			Text = TabInfo.Title;

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

			var item1State = TabState.Items.ElementAtOrDefault(0);
			if (item1State == null)
			{
				item1State = new IntegratedSolutionState.StatementItemState();
				TabState.Items.Add(item1State);
			}

			var item2State = TabState.Items.ElementAtOrDefault(1);
			if (item2State == null)
			{
				item2State = new IntegratedSolutionState.StatementItemState();
				TabState.Items.Add(item2State);
			}

			statementItemControl1.Init(TabInfo.ComboCheckbox1,
				TabInfo.Combo1Items,
				TabInfo.MemoPopupCheckbox1,
				TabInfo.MemoPopup1Items,
				item1State,
				TabInfo.Combo1Configuration,
				TabInfo.MemoPopup1Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			statementItemControl1.EditValueChanged += OnEditValueChanged;

			statementItemControl2.Init(TabInfo.ComboCheckbox2,
				TabInfo.Combo2Items,
				TabInfo.MemoPopupCheckbox2,
				TabInfo.MemoPopup2Items,
				item2State,
				TabInfo.Combo2Configuration,
				TabInfo.MemoPopup2Configuration,
				SlideContainer.StyleConfiguration,
				SlideContainer.ResourceManager);
			statementItemControl2.EditValueChanged += OnEditValueChanged;

			_allowHandleEvents = true;

			Initialized = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			statementItemControl1.ApplyChanges();
			statementItemControl2.ApplyChanges();

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}
	}
}
