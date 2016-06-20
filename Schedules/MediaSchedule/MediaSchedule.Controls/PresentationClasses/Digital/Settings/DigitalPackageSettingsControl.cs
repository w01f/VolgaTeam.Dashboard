using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	//public partial class DigitalPackageSettingsControl : UserControl, ISettingsControl
	public partial class DigitalPackageSettingsControl : XtraTabPage, ISettingsControl
	{
		private bool _allowToSave;
		private DigitalProductsContent _content;
		public int Order => 1;
		public ButtonInfo BarButton { get; }
		public DigitalSettingsType SettingsType => DigitalSettingsType.ProductPackage;

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public DigitalPackageSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.DigitalSettingsPackage,
				Tooltip = String.Format("Open {0} Schedule Settings",
					MediaMetaData.Instance.DataTypeString),
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				font = new Font(buttonXCategory.Font.FontFamily, buttonXCategory.Font.Size - 2, buttonXCategory.Font.Style);
				buttonXGroup.Font = font;
				buttonXProduct.Font = font;
				buttonXImpressions.Font = font;
				buttonXCPM.Font = font;
				buttonXRate.Font = font;
				buttonXInvestment.Font = font;
				buttonXInfo.Font = font;
				buttonXComments.Font = font;
				buttonXScreenshot.Font = font;
			}
		}

		public void LoadContentData(DigitalProductsContent content)
		{
			_content = content;

			_allowToSave = false;
			buttonXCategory.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowCategory;
			buttonXGroup.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowGroup;
			buttonXProduct.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowProduct;
			buttonXImpressions.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowImpressions;
			buttonXCPM.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowCPM;
			buttonXRate.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowRate;
			buttonXInvestment.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowInvestment;
			buttonXInfo.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowInfo;
			buttonXComments.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowComments;
			buttonXScreenshot.Checked = _content.ScheduleSettings.DigitalPackageSettings.ShowScreenshot;
			switch (_content.ScheduleSettings.DigitalPackageSettings.Formula)
			{
				case FormulaType.CPM:
					checkEditFormulaCPM.Checked = true;
					checkEditFormulaInvestment.Checked = false;
					checkEditFormulaImpressions.Checked = false;
					break;
				case FormulaType.Investment:
					checkEditFormulaCPM.Checked = false;
					checkEditFormulaInvestment.Checked = true;
					checkEditFormulaImpressions.Checked = false;
					break;
				case FormulaType.Impressions:
					checkEditFormulaCPM.Checked = false;
					checkEditFormulaInvestment.Checked = false;
					checkEditFormulaImpressions.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_content.ScheduleSettings.DigitalPackageSettings.ShowCategory = buttonXCategory.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowGroup = buttonXGroup.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowProduct = buttonXProduct.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowImpressions = buttonXImpressions.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowCPM = buttonXCPM.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowRate = buttonXRate.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowInvestment = buttonXInvestment.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowInfo = buttonXInfo.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowComments = buttonXComments.Checked;
			_content.ScheduleSettings.DigitalPackageSettings.ShowScreenshot = buttonXScreenshot.Checked;
			if (checkEditFormulaCPM.Checked)
				_content.ScheduleSettings.DigitalPackageSettings.Formula = FormulaType.CPM;
			else if (checkEditFormulaInvestment.Checked)
				_content.ScheduleSettings.DigitalPackageSettings.Formula = FormulaType.Investment;
			else if (checkEditFormulaImpressions.Checked)
				_content.ScheduleSettings.DigitalPackageSettings.Formula = FormulaType.Impressions;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
