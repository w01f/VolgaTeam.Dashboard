using System;
using System.ComponentModel;
using System.Drawing;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Interfaces;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	[ToolboxItem(false)]
	//public abstract partial class BaseSummaryInfoControl : UserControl, ISectionSettingsControl
	public abstract partial class BaseSummaryInfoControl : XtraTabPage, ISectionSettingsControl
	{
		protected bool _allowToSave;

		protected ISectionSummaryContent _sectionSummaryContent;

		protected BaseSummarySettings _baseSummarySettings;
		public Int32 Order => 1;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public abstract ScheduleSettingsType SettingsType { get; }

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		protected BaseSummaryInfoControl()
		{
			InitializeComponent();
			Text = "Slide Info";
			BarButton = new ButtonInfo
			{
				Tooltip = "Edit Summary Settings",
				Logo = Resources.SummaryOptionsInfo,
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

				laInvestment.Font = new Font(laInvestment.Font.FontFamily, laInvestment.Font.Size - 2, laInvestment.Font.Style);
			}
		}

		public virtual void LoadSectionData(ScheduleSection sectionData)
		{
			_sectionSummaryContent = (ISectionSummaryContent)_baseSummarySettings;
			_allowToSave = false;

			checkEditBusinessName.Text = String.Format("{0}", _sectionSummaryContent.Parent.Parent.ParentScheduleSettings.BusinessName);
			checkEditDecisionMaker.Text = String.Format("{0}", _sectionSummaryContent.Parent.Parent.ParentScheduleSettings.DecisionMaker);
			checkEditPresentationDate.Text = String.Format("Presentation Date: {0}",
				_sectionSummaryContent.Parent.Parent.ParentScheduleSettings.PresentationDate.HasValue ?
				_sectionSummaryContent.Parent.Parent.ParentScheduleSettings.PresentationDate.Value.ToString("MM/dd/yyyy") :
				String.Empty);
			checkEditFlightDates.Text = String.Format("Campaign Dates: {0}", _sectionSummaryContent.Parent.Parent.ParentScheduleSettings.FlightDates);

			checkEditBusinessName.Checked = _baseSummarySettings.ShowAdvertiser;
			checkEditDecisionMaker.Checked = _baseSummarySettings.ShowDecisionMaker;
			checkEditPresentationDate.Checked = _baseSummarySettings.ShowPresentationDate;
			checkEditFlightDates.Checked = _baseSummarySettings.ShowFlightDates;
			checkEditMonthlyInvestment.Checked = _baseSummarySettings.ShowMonthly;
			checkEditTotalInvestment.Checked = _baseSummarySettings.ShowTotal;
			checkEditTableOutput.Checked = _baseSummarySettings.TableOutput;

			UpdateTotals();

			_allowToSave = true;
		}


		protected virtual void SaveData()
		{
			_baseSummarySettings.ShowAdvertiser = checkEditBusinessName.Checked;
			_baseSummarySettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
			_baseSummarySettings.ShowPresentationDate = checkEditPresentationDate.Checked;
			_baseSummarySettings.ShowFlightDates = checkEditFlightDates.Checked;
			_baseSummarySettings.ShowMonthly = checkEditMonthlyInvestment.Checked;
			_baseSummarySettings.ShowTotal = checkEditTotalInvestment.Checked;
			_baseSummarySettings.TableOutput = checkEditTableOutput.Checked;
		}

		public abstract void UpdateTotals();

		private void OnSettingChanged(object sender, EventArgs e)
		{
			checkEditMonthlyInvestment.ForeColor =
				checkEditMonthlyInvestment.Properties.Appearance.ForeColor =
				checkEditMonthlyInvestment.Properties.AppearanceFocused.ForeColor =
				 checkEditMonthlyInvestment.Checked ? Color.Black : Color.Gray;
			checkEditMonthlyInvestment.Refresh();
			checkEditTotalInvestment.ForeColor =
				checkEditTotalInvestment.Properties.Appearance.ForeColor =
				checkEditTotalInvestment.Properties.AppearanceFocused.ForeColor =
				 checkEditTotalInvestment.Checked ? Color.Black : Color.Gray;
			checkEditTotalInvestment.Refresh();
			spinEditMonthly.Enabled = checkEditMonthlyInvestment.Checked;
			spinEditTotal.Enabled = checkEditTotalInvestment.Checked;

			if (!_allowToSave) return;
			SaveData();
			RaiseDataChanged();
		}

		protected void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
