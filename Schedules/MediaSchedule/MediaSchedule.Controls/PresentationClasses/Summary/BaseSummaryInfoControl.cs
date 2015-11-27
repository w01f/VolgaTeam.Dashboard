using System;
using System.ComponentModel;
using System.Drawing;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using DevExpress.XtraTab;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	//public abstract partial class BaseSummaryInfoControl : UserControl, ISummarySettingsEditorControl
	public abstract partial class BaseSummaryInfoControl : XtraTabPage, ISummarySettingsEditorControl
	{
		protected bool _allowToSave;

		protected ISectionSummaryContent _sectionSummaryContent;

		protected BaseSummarySettings _baseSummarySettings;

		public event EventHandler<EventArgs> DataChanged;

		protected BaseSummaryInfoControl()
		{
			InitializeComponent();
			Text = "Slide Info";
		}

		public virtual void LoadData(BaseSummarySettings dataSource)
		{
			_baseSummarySettings = dataSource;
			_sectionSummaryContent = (ISectionSummaryContent)dataSource;
			_allowToSave = false;

			checkEditBusinessName.Text = String.Format("{0}", _sectionSummaryContent.Parent.Parent.ParentSchedule.BusinessName);
			checkEditDecisionMaker.Text = String.Format("{0}", _sectionSummaryContent.Parent.Parent.ParentSchedule.DecisionMaker);
			laPresentationDate.Text = String.Format("{0}", 
				_sectionSummaryContent.Parent.Parent.ParentSchedule.PresentationDate.HasValue ? 
				_sectionSummaryContent.Parent.Parent.ParentSchedule.PresentationDate.Value.ToString("MM/dd/yyyy") : 
				String.Empty);
			laFlightDates.Text = String.Format("{0}", _sectionSummaryContent.Parent.Parent.ParentSchedule.FlightDates);

			checkEditBusinessName.Checked = _baseSummarySettings.ShowAdvertiser;
			checkEditDecisionMaker.Checked = _baseSummarySettings.ShowDecisionMaker;
			checkEditPresentationDate.Checked = _baseSummarySettings.ShowPresentationDate;
			checkEditFlightDates.Checked = _baseSummarySettings.ShowFlightDates;
			checkEditMonthlyInvestment.Checked = _baseSummarySettings.ShowMonthly;
			checkEditTotalInvestment.Checked = _baseSummarySettings.ShowTotal;

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
			
			if(!_allowToSave) return;
			SaveData();
			RaiseDataChanged();
		}

		protected void RaiseDataChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}
	}
}
