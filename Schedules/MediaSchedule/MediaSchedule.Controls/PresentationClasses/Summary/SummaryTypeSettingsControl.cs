using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Business.Media.Enums;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	//public partial class SummaryTypeSettingsControl : UserControl
	public partial class SummaryTypeSettingsControl : XtraTabPage
	{
		private bool _allowToSave;

		private SectionSummary _dataSource;

		public event EventHandler<EventArgs> SummaryTypeChanged;
		public event EventHandler<EventArgs> DataChanged;

		public SummaryTypeSettingsControl()
		{
			InitializeComponent();
			Text = "Summaries";
		}

		public void LoadData(SectionSummary dataSource)
		{
			_dataSource = dataSource;
			_allowToSave = false;

			switch (_dataSource.SummaryType)
			{
				case SectionSummaryTypeEnum.Product:
					buttonXTypeProduct.Checked = true;
					buttonXTypeCustom.Checked = false;
					buttonXTypeStrategy.Checked = false;
					checkEditTableOutput.Checked = ((BaseSummarySettings)_dataSource.Content).TableOutput;
					break;
				case SectionSummaryTypeEnum.Custom:
					buttonXTypeProduct.Checked = false;
					buttonXTypeCustom.Checked = true;
					buttonXTypeStrategy.Checked = false;

					break;
				case SectionSummaryTypeEnum.Strategy:
					buttonXTypeProduct.Checked = false;
					buttonXTypeCustom.Checked = false;
					buttonXTypeStrategy.Checked = true;
					break;

			}

			LoadTableOutput();

			UpdateControlsVisibility();

			_allowToSave = true;
		}

		private void LoadTableOutput()
		{
			switch (_dataSource.SummaryType)
			{
				case SectionSummaryTypeEnum.Product:
				case SectionSummaryTypeEnum.Custom:
					if (_dataSource.Content is BaseSummarySettings)
						checkEditTableOutput.Checked = ((BaseSummarySettings)_dataSource.Content).TableOutput;
					break;
				default:
					checkEditTableOutput.Checked = false;
					break;
			}
		}

		public void UpdateSlideCount(int slideCount)
		{
			if (slideCount > 0)
			{
				laProductSlideCount.Visible = buttonXTypeProduct.Checked;
				laCustomSlideCount.Visible = buttonXTypeCustom.Checked;
				laProductSlideCount.Text = laCustomSlideCount.Text = String.Format("Estimated Slide Count: {0}", slideCount);
			}
			else
				laProductSlideCount.Visible = laCustomSlideCount.Visible = false;
		}

		private void UpdateControlsVisibility()
		{
			laProductSlideCount.Visible = buttonXTypeProduct.Checked;
			laCustomSlideCount.Visible = buttonXTypeCustom.Checked;
			checkEditTableOutput.Visible = buttonXTypeCustom.Checked || buttonXTypeProduct.Checked;
		}

		private void SaveData()
		{
			if (buttonXTypeProduct.Checked)
				_dataSource.ChangeSummaryType(SectionSummaryTypeEnum.Product);
			else if (buttonXTypeCustom.Checked)
				_dataSource.ChangeSummaryType(SectionSummaryTypeEnum.Custom);
			else if (buttonXTypeStrategy.Checked)
				_dataSource.ChangeSummaryType(SectionSummaryTypeEnum.Strategy);
			switch (_dataSource.SummaryType)
			{
				case SectionSummaryTypeEnum.Product:
				case SectionSummaryTypeEnum.Custom:
					((BaseSummarySettings)_dataSource.Content).TableOutput = checkEditTableOutput.Checked;
					break;
			}
		}

		private void OnSettingChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			UpdateControlsVisibility();

			var button = (ButtonX)sender;
			if (!button.Checked) return;

			SaveData();

			LoadTableOutput();

			RaiseDataChanged();
		}

		private void RaiseDataChanged()
		{
			if (SummaryTypeChanged != null)
				SummaryTypeChanged(this, EventArgs.Empty);
		}

		private void OnOutputSelectorClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXTypeProduct.Checked = false;
			buttonXTypeCustom.Checked = false;
			buttonXTypeStrategy.Checked = false;
			button.Checked = true;
		}

		private void OnTableOutputCheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SaveData();
			RaiseOutputChanged();
		}

		private void RaiseOutputChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}
	}
}
