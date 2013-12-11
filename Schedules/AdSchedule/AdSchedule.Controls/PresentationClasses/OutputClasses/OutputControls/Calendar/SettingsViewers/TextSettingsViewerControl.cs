using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
	[ToolboxItem(false)]
	public partial class TextSettingsViewerControl : UserControl, ICalendarSettingsViewer
	{
		protected MonthCalendarViewSettings _settings = null;
		protected OutputCalendarControl _calendarControl = null;

		public TextSettingsViewerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ICalendarSettingsViewer Members
		public virtual string Title
		{
			get { return string.Empty; }
		}

		public Image Logo
		{
			get { return Properties.Resources.GridDetails; }
		}

		public virtual string FormToggleChangeCaption
		{
			get { return string.Empty; }
		}

		public string EditButtonText
		{
			get { return "View this Item"; }
		}

		public string ApplyForAllText
		{
			get { return string.Empty; }
		}

		public bool ShowApplyForAll
		{
			get { return false; }
		}

		public void SaveSettings() { }
		#endregion

		public void LoadSettings(OutputCalendarControl calendarControl, MonthCalendarViewSettings settings)
		{
			_calendarControl = calendarControl;
			_settings = settings;
			LoadValue();
		}

		public void ApplySettingsForAll(MonthCalendarViewSettings[] allSettings) { }

		public virtual void LoadValue() { }
	}

	[ToolboxItem(false)]
	public class BusinessNameViewerControl : TextSettingsViewerControl
	{
		public override string Title
		{
			get { return "Advertiser Name"; }
		}

		public override string FormToggleChangeCaption
		{
			get { return "Advertiser Name"; }
		}

		public override void LoadValue()
		{
			laValue.Text = "Business Name: " + (_calendarControl.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(_calendarControl.LocalSchedule.AccountNumber) ? (" - " + _calendarControl.LocalSchedule.AccountNumber) : string.Empty));
		}
	}

	[ToolboxItem(false)]
	public class DecisionMakerViewerControl : TextSettingsViewerControl
	{
		public override string Title
		{
			get { return "Decisionmaker Name"; }
		}

		public override string FormToggleChangeCaption
		{
			get { return "Decision Maker"; }
		}

		public override void LoadValue()
		{
			laValue.Text = "Decisionmaker Name: " + _calendarControl.LocalSchedule.DecisionMaker;
		}
	}

	[ToolboxItem(false)]
	public class TotalCostViewerControl : TextSettingsViewerControl
	{
		public override string Title
		{
			get { return "Monthly Investment"; }
		}

		public override string FormToggleChangeCaption
		{
			get { return "Monthly Investment"; }
		}

		public override void LoadValue()
		{
			laValue.Text = "Monthly Investment: " + _calendarControl.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).Select(x => x.FinalRate).Sum().ToString("$#,###.00");
		}
	}

	[ToolboxItem(false)]
	public class AvgCostViewerControl : TextSettingsViewerControl
	{
		public override string Title
		{
			get { return "Monthly Average Ad Rate"; }
		}

		public override string FormToggleChangeCaption
		{
			get { return "Average Rate"; }
		}

		public override void LoadValue()
		{
			laValue.Text = "Average Ad Rate: " + (_calendarControl.Inserts.Any(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year) ? _calendarControl.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).Select(x => x.FinalRate).Average().ToString("$#,###.00") : "$.00");
		}
	}

	[ToolboxItem(false)]
	public class TotalAdsViewerControl : TextSettingsViewerControl
	{
		public override string Title
		{
			get { return "Monthly Ads Scheduled"; }
		}

		public override string FormToggleChangeCaption
		{
			get { return "Monthly Ads Scheduled"; }
		}

		public override void LoadValue()
		{
			laValue.Text = "Total Ads: " + _calendarControl.Inserts.Count(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year);
		}
	}

	[ToolboxItem(false)]
	public class TotalDaysViewerControl : TextSettingsViewerControl
	{
		public override string Title
		{
			get { return "Active Days"; }
		}

		public override string FormToggleChangeCaption
		{
			get { return "Active Days"; }
		}

		public override void LoadValue()
		{
			laValue.Text = "Active Days: " + _calendarControl.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Month == _settings.Month.Month && x.Date.Value.Year == _settings.Month.Year).GroupBy(x => x.Date).Count();
		}
	}
}