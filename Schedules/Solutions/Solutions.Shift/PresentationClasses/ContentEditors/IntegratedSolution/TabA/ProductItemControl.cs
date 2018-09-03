using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
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

		private readonly Dictionary<string, List<BaseToggleTabControl>> _toggleTabSets =
			new Dictionary<string, List<BaseToggleTabControl>>();

		private LayoutTabControl LayoutTab =>
			_toggleTabSets.SelectMany(tabSet => tabSet.Value).OfType<LayoutTabControl>().FirstOrDefault();

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
				var fontSizeDelte = Container.TabInfo.CommonEditorConfiguration.FontSize.Value -
									TextEditorConfiguration.DefaultFontSize;
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
				layoutControl.Appearance.ControlDropDownHeader.ForeColor =
					Container.TabInfo.CommonEditorConfiguration.DropdownForeColor;
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

			toggleSwitchOutput.IsOn = ItemState.EnableOutput ?? true;

			buttonXToggle1.Text = String.Format("<div align=\"center\"><font size=\"12\" color=\"gray\" >{0}</font></div>",
				ItemInfo.Positioning.Title?.Replace("&", "&amp;") ?? "positioning");
			buttonXToggle1.Tag = ProductInfo.PositioningId;
			buttonXToggle2.Text = String.Format("<div align=\"center\"><font size=\"12\" color=\"gray\" >{0}</font></div>",
				ItemInfo.Research.Title?.Replace("&", "&amp;") ?? "research");
			buttonXToggle2.Tag = ProductInfo.ResearchId;
			buttonXToggle3.Text = String.Format("<div align=\"center\"><font size=\"12\" color=\"gray\" >{0}</font></div>",
				ItemInfo.Style.Title?.Replace("&", "&amp;") ?? "style");
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

			ItemState.EnableOutput = toggleSwitchOutput.IsOn;

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
						tabs.Add(new LayoutTabControl(ItemInfo.Style.Tab2, ItemState.StyleToggle.LayoutTab, this));
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

			OnEditValueChanged(sender, e);
		}

		private void OnOutputToggled(object sender, EventArgs e)
		{
			layoutControlItemHeader.Enabled =
				layoutControlItemCombo1.Enabled = layoutControlGroupToggles.Enabled = toggleSwitchOutput.IsOn;
			if (!_allowToHandleEvents) return;
			OnEditValueChanged(sender, e);
			SlideContainer.RaiseOutputStatuesChanged();
		}

		#region Output
		public bool ReadyForOutput => Initialized ? toggleSwitchOutput.IsOn : (ItemState.EnableOutput ?? true);

		public SlideType SlideType
		{
			get
			{
				var layoutItem = LayoutTab?.CurrentLayoutItem ??
								 ItemState.StyleToggle.LayoutTab.SavedLayout ?? ItemInfo.Style.Tab2.DefaultItem;

				if (layoutItem != null)
					switch (layoutItem.LayoutType)
					{
						case ProductLayoutType.Left:
							return SlideType.ShiftIntegratedSolutionA_Left;
						case ProductLayoutType.Right:
							return SlideType.ShiftIntegratedSolutionA_Right;
						default:
							return SlideType.CustomSlide;
					}
				return SlideType.CustomSlide;
			}
		}

		public OutputItem GetOutputItem()
		{
			var outputData = GetOutputData();
			return new OutputItem
			{
				Name = ItemInfo.Title,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
					Path.GetFileName(Path.GetTempFileName())),
				SlidesCount = 1,
				IsCurrent = TabControl?.SelectedTabPage == this,
				SlideGeneratingAction = (processor, destinationPresentation) =>
				{
					processor.AppendSolutionCommonSlide(outputData, destinationPresentation);
				},
				PreviewGeneratingAction = (processor, presentationSourcePath) =>
				{
					processor.PrepareSolutionCommonSlide(presentationSourcePath, outputData);
				}
			};
		}

		private OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			//outputDataPackage.Theme = SlideContainer.GetSelectedTheme(SlideType);
			
			var tab1Combo1 = (ItemState.PositionToggle.Statements.Items.ElementAtOrDefault(0)?.Combo ??
							  (ItemInfo.Positioning.Tab1.ComboCheckbox1.Value
								  ? ItemInfo.Positioning.Tab1.Combo1Items.FirstOrDefault(h => h.IsDefault)
								  : null))?.Value;
			var tab1MultiPopup1 = (ItemState.PositionToggle.Statements.Items.ElementAtOrDefault(0)?.MemoPopup ??
								   (ItemInfo.Positioning.Tab1.MemoPopupCheckbox1.Value ?
									   ItemInfo.Positioning.Tab1.MemoPopup1Items.FirstOrDefault(item =>
										   item.IsDefault && !item.IsPlaceholder) : null))?.Value;

			var tab1Combo2 = (ItemState.PositionToggle.Statements.Items.ElementAtOrDefault(1)?.Combo ??
							  (ItemInfo.Positioning.Tab1.ComboCheckbox2.Value
								  ? ItemInfo.Positioning.Tab1.Combo2Items.FirstOrDefault(h => h.IsDefault)
								  : null))?.Value;
			var tab1MultiPopup2 = (ItemState.PositionToggle.Statements.Items.ElementAtOrDefault(1)?.MemoPopup ??
								   (ItemInfo.Positioning.Tab1.MemoPopupCheckbox2.Value ?
										ItemInfo.Positioning.Tab1.MemoPopup2Items.FirstOrDefault(item =>
										item.IsDefault && !item.IsPlaceholder) : null))?.Value;

			var bullets = new[]
			{
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(0) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					ItemInfo.Positioning.Tab2.BulletCombo1Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					null))?.Value,
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(1) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					 ItemInfo.Positioning.Tab2.BulletCombo2Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					 null))?.Value,
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(2) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					 ItemInfo.Positioning.Tab2.BulletCombo3Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					 null))?.Value,
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(3) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					 ItemInfo.Positioning.Tab2.BulletCombo4Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					 null))?.Value,
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(4) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					 ItemInfo.Positioning.Tab2.BulletCombo5Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					 null))?.Value,
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(5) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					 ItemInfo.Positioning.Tab2.BulletCombo6Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					 null))?.Value,
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(6) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					 ItemInfo.Positioning.Tab2.BulletCombo7Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					 null))?.Value,
				(ItemState.PositionToggle.Bullets.Bullets.ElementAtOrDefault(7) ??
				 (ItemInfo.Positioning.Tab2.ToggleSwitch.Value?
					 ItemInfo.Positioning.Tab2.BulletCombo8Items.FirstOrDefault(item =>item.IsDefault && !item.IsPlaceholder):
					 null))?.Value
			}
			.Where(item => !String.IsNullOrWhiteSpace(item))
			.ToList();

			var bundleItem = ItemState.ResearchToggle.Data.BundleState?.ToLitsItem() ??
							 (ItemInfo.Research.Tab1.ToggleSwitch.Value ?
								ItemInfo.Research.Tab1.BundleInfo.Items.FirstOrDefault(item => item.IsDefault) :
								null);

			var layoutItem = ItemState.StyleToggle.LayoutTab.SavedLayout ?? ItemInfo.Style.Tab2.DefaultItem;

			var outputCondition = Container.CustomTabInfo.OutputConditions.FirstOrDefault(condition =>
				condition.ConditionPositioningTab1MultiPopup1 == (!String.IsNullOrWhiteSpace(tab1Combo1) || !String.IsNullOrWhiteSpace(tab1MultiPopup1)) &&
				condition.ConditionPositioningTab1MultiPopup2 == (!String.IsNullOrWhiteSpace(tab1Combo2) || !String.IsNullOrWhiteSpace(tab1MultiPopup2)) &&
				condition.ConditionPositioningTab2 == bullets.Any() &&
				condition.ConditionResearchTab1 == (bundleItem != null && !bundleItem.IsEmpty()) &&
				layoutItem != null && condition.LayoutType == layoutItem.LayoutType);


			if (outputCondition != null)
			{
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetShiftIntegratedSolutionFile(outputCondition.TemplateName);

				outputDataPackage.LayoutName = String.Format("layout_{0}", layoutItem.LayoutIndex);

				var clipart1 = ItemState.StyleToggle.ImageTab.Clipart ??
							   ImageClipartObject.FromFile(ItemInfo.Style.Tab1.DefaultImagePath);
				if (clipart1 != null)
				{
					clipart1.OutputBackground = true;
					outputDataPackage.ClipartItems.Add("SHIFT10ACLIPART1", clipart1);
				}

				outputDataPackage.TextItems.Add("SHIFT10ASOLUTIONHEADER".ToUpper(), (ItemState.Header ?? ItemInfo.HeaderItems.FirstOrDefault(h => h.IsDefault))?.Value);
				outputDataPackage.TextItems.Add("SHIFT10ASOLUTIONCOMBO1".ToUpper(), (ItemState.Combo1 ?? ItemInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);

				outputDataPackage.TextItems.Add("SHIFT10ATAB1COMBO1".ToUpper(), tab1Combo1);
				outputDataPackage.TextItems.Add("SHIFT10ATAB1MULTIBOX1".ToUpper(), tab1MultiPopup1);

				outputDataPackage.TextItems.Add("SHIFT10ATAB1COMBO2".ToUpper(), tab1Combo2);
				outputDataPackage.TextItems.Add("SHIFT10ATAB1MULTIBOX2".ToUpper(), tab1MultiPopup2);

				outputDataPackage.TextItems.Add("SHIFT10ATAB2COMBO1".ToUpper(), (ItemState.PositionToggle.Bullets.Combo1 ?? (ItemInfo.Positioning.Tab2.Checkbox1.Value ? ItemInfo.Positioning.Tab2.Combo1Items.FirstOrDefault(h => h.IsDefault) : null))?.Value);
				outputDataPackage.TextItems.Add("SHIFT10ATAB2COMBOMERGE1".ToUpper(), String.Join(((char)13).ToString(), bullets.Select(item => String.Format("- {0}", item))));

				var bundleItems = new[] { bundleItem?.Value1, bundleItem?.Value2, bundleItem?.Value3 }
					.Where(item => !String.IsNullOrWhiteSpace(item))
					.ToList();
				outputDataPackage.TextItems.Add("SHIFT10ABUNDLELINE1".ToUpper(), bundleItems.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("SHIFT10ABUNDLELINE2".ToUpper(), bundleItems.ElementAtOrDefault(1));
				outputDataPackage.TextItems.Add("SHIFT10ABUNDLELINE3".ToUpper(), bundleItems.ElementAtOrDefault(2));
			}

			return outputDataPackage;
		}
		#endregion
	}
}
