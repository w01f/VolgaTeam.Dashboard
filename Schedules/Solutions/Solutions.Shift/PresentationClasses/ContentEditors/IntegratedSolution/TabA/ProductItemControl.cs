using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	//public partial class ProductItemControl : UserControl
	public partial class ProductItemControl : BaseTabASubControl
	{
		private bool _allowToHandleEvents;
		private bool _dataChanged;

		private readonly Dictionary<string, List<BaseToggleTabControl>> _toggleTabSets = new Dictionary<string, List<BaseToggleTabControl>>();

		public bool Initialized { get; private set; }

		public ProductInfo ItemInfo { get; }
		public IntegratedSolutionState.ProductItemState ItemState { get; }

		public ProductItemControl()
		{
			InitializeComponent();
		}

		public ProductItemControl(ProductInfo itemInfo,
			IntegratedSolutionState.ProductItemState itemState,
			IntegratedSolutionTabAControl container) : base(container)
		{
			InitializeComponent();

			ItemState = itemState;
			ItemInfo = itemInfo;

			Text = ItemInfo.Title;
			ShowCloseButton = DefaultBoolean.True;

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

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemToggle1.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggle1.MaxSize, scaleFactor);
			layoutControlItemToggle1.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggle1.MinSize, scaleFactor);
			layoutControlItemToggle2.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggle2.MaxSize, scaleFactor);
			layoutControlItemToggle2.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggle2.MinSize, scaleFactor);
			layoutControlItemToggle3.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggle3.MaxSize, scaleFactor);
			layoutControlItemToggle3.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggle3.MinSize, scaleFactor);
		}

		public void InitControl()
		{
			if (Initialized) return;

			ItemInfo.LoadData();

			_allowToHandleEvents = false;

			comboBoxEditHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(ItemInfo.HeaderConfiguration);
			comboBoxEditHeader.Properties.Items.Clear();
			comboBoxEditHeader.Properties.Items.AddRange(ItemInfo.HeaderItems);
			comboBoxEditHeader.Properties.NullText =
				ItemInfo.HeaderItems.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditHeader.Properties.NullText;
			comboBoxEditHeader.EditValue = ItemState.Header ?? ItemInfo.HeaderItems.FirstOrDefault(h => h.IsDefault);
			
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(ItemInfo.Combo1Configuration);
			comboBoxEditCombo1.Properties.Items.Clear();
			comboBoxEditCombo1.Properties.Items.AddRange(ItemInfo.Combo1Items);
			comboBoxEditCombo1.Properties.NullText =
				ItemInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo1.Properties.NullText;
			comboBoxEditCombo1.EditValue = ItemState.Combo1 ?? ItemInfo.Combo1Items.FirstOrDefault(h => h.IsDefault);

			buttonXToggle1.Text = String.Format("<div align=\"center\"><font size=\"12\" color=\"gray\" >{0}</font></div>", ItemInfo.Positioning.Title?.Replace("&", "&amp;") ?? "positioning");
			buttonXToggle1.Tag = ProductInfo.PositioningId;
			buttonXToggle2.Text = String.Format("<div align=\"center\"><font size=\"12\" color=\"gray\" >{0}</font></div>", ItemInfo.Research.Title?.Replace("&", "&amp;") ?? "research");
			buttonXToggle2.Tag = ProductInfo.ResearchId;
			buttonXToggle3.Text = String.Format("<div align=\"center\"><font size=\"12\" color=\"gray\" >{0}</font></div>", ItemInfo.Style.Title?.Replace("&", "&amp;") ?? "style");
			buttonXToggle3.Tag = ProductInfo.StyleId;

			buttonXToggle1.Checked = true;
			LoadToggleTabs(ProductInfo.PositioningId);

			_allowToHandleEvents = true;

			Initialized = true;
		}

		public void ApplyChanges()
		{
			if (!_dataChanged) return;

			ItemState.Header = ItemInfo.HeaderItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditHeader.EditValue
				? comboBoxEditHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditHeader.EditValue as String }
				: null;

			ItemState.Combo1 = ItemInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue
				? comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String }
				: null;

			foreach (var toggleTabControl in _toggleTabSets.SelectMany(tabSet => tabSet.Value))
				toggleTabControl.ApplyChanges();

			_dataChanged = false;
		}

		private void LoadToggleTabs(string toggleId)
		{
			xtraTabControl.SuspendLayout();
			foreach (XtraTabPage tabPage in xtraTabControl.TabPages)
			{
				tabPage.SuspendLayout();
				tabPage.PageVisible = false;
				tabPage.ResumeLayout(true);
			}

			if (_toggleTabSets.ContainsKey(toggleId))
			{
				foreach (var tabPage in _toggleTabSets[toggleId])
				{
					tabPage.SuspendLayout();
					tabPage.PageVisible = true;
					tabPage.ResumeLayout(true);
				}
			}
			else
			{
				var tabs = new List<BaseToggleTabControl>();
				switch (toggleId)
				{
					case ProductInfo.PositioningId:
						tabs.Add(new StatementsTabControl(ItemInfo.Positioning.Tab1, ItemState.PositionToggle.Statements, this));
						tabs.Add(new BulletsTabControl(ItemInfo.Positioning.Tab2, ItemState.PositionToggle.Bullets, this));
						break;
					case ProductInfo.ResearchId:
						tabs.Add(new ResearchDataTabControl(ItemInfo.Research.Tab1, ItemState.ResearchToggle.Data, this));
						break;
					case ProductInfo.StyleId:
						tabs.Add(new ImageTabControl(ItemInfo.Style.Tab1, ItemState.StyleToggle.ImageTab, this));
						tabs.Add(new ImageTabControl(ItemInfo.Style.Tab2, ItemState.StyleToggle.LayoutTab, this));
						break;
				}
				_toggleTabSets.Add(toggleId, tabs);

				xtraTabControl.TabPages.AddRange(tabs.ToArray());
				foreach (var tabControl in tabs)
					tabControl.LoadData();
			}
			xtraTabControl.ResumeLayout(true);
		}

		public void RaiseEditValueChanged()
		{
			if (!_allowToHandleEvents) return;
			_dataChanged = true;
			Container.RaiseEditValueChanged();
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnToggleButtonClick(object sender, EventArgs e)
		{
			var toggleButton = (ButtonX)sender;
			if (toggleButton.Checked) return;

			buttonXToggle1.Checked = false;
			buttonXToggle2.Checked = false;
			buttonXToggle3.Checked = false;

			toggleButton.Checked = true;
		}

		private void OnToggleButtonCheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToHandleEvents) return;

			var toggleButton = (ButtonX)sender;
			if (!toggleButton.Checked) return;

			LoadToggleTabs((String)toggleButton.Tag);
		}
	}
}
