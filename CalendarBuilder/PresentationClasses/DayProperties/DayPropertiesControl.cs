using System;
using System.ComponentModel;
using System.Windows.Forms;
using CalendarBuilder.BusinessClasses;
using DevExpress.XtraBars;
using DevExpress.XtraTab;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
	public partial class DayPropertiesControl : UserControl
	{
		private string _helpKey = string.Empty;
		private CalendarStyle _style;

		public DayPropertiesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public CalendarDay Day { get; set; }

		public bool SettingsNotSaved { get; set; }

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler PropertiesSaved;

		[Browsable(true)]
		[Category("Action")]
		public event EventHandler Closed;

		[Browsable(true)]
		[Category("Action")]
		public event TabPageChangedEventHandler PropertiesGroupChanged;

		public void LoadData(CalendarDay day)
		{
			Day = day;
			LoadCurrentDayData();

			xtraTabPageDigital.PageVisible = _style == CalendarStyle.Advanced && Day.Parent.Schedule.ShowDigital;
			xtraTabPageNewspaper.PageVisible = _style == CalendarStyle.Advanced && Day.Parent.Schedule.ShowNewspaper;
			xtraTabPageTV.PageVisible = _style == CalendarStyle.Advanced;
			xtraTabPageRadio.PageVisible = _style == CalendarStyle.Advanced;
			xtraTabPageTV.PageEnabled = Day.Parent.Schedule.ShowTV;
			xtraTabPageRadio.PageEnabled = Day.Parent.Schedule.ShowRadio;
			xtraTabPageLogo.PageVisible = _style == CalendarStyle.Graphic;

			if (PropertiesGroupChanged != null)
				PropertiesGroupChanged(xtraTabControl, new TabPageChangedEventArgs(null, xtraTabControl.SelectedTabPage));
		}

		public void LoadCurrentDayData()
		{
			if (Day != null)
			{
				xtraTabPageDigital.Tooltip = "Digital Info: " + Day.Date.ToString("dddd, MMMM d, yyyy");
				xtraTabPageNewspaper.Tooltip = "Newspaper Info: " + Day.Date.ToString("dddd, MMMM d, yyyy");
				xtraTabPageTV.Tooltip = "TV Info: " + Day.Date.ToString("dddd, MMMM d, yyyy");
				xtraTabPageRadio.Tooltip = "Radio Info: " + Day.Date.ToString("dddd, MMMM d, yyyy");
				xtraTabPageComment.Tooltip = "Comment: " + Day.Date.ToString("dddd, MMMM d, yyyy");
				xtraTabPageLogo.Tooltip = "Logo: " + Day.Date.ToString("dddd, MMMM d, yyyy");
				digitalPropertiesControl.LoadData(Day);
				newspaperPropertiesControl.LoadData(Day);
				commentControl.LoadData(Day);
				logoControl.LoadData(Day);
				SettingsNotSaved = false;
			}
		}

		public void SaveData()
		{
			digitalPropertiesControl.SaveData();
			newspaperPropertiesControl.SaveData();
			tvPropertiesControl.SaveData();
			radioPropertiesControl.SaveData();
			commentControl.SaveData();
			logoControl.SaveData();
			SettingsNotSaved = false;

			if (PropertiesSaved != null)
				PropertiesSaved(this, new EventArgs());
		}

		public void Decorate(CalendarStyle style)
		{
			_style = style;
		}

		private void barLargeButtonItemApply_ItemClick(object sender, ItemClickEventArgs e)
		{
			SaveData();
		}

		private void barLargeButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			Day.ClearData();
			LoadCurrentDayData();
			SettingsNotSaved = true;
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			HelpManager.Instance.OpenHelpLink(_helpKey);
		}

		private void barLargeButtonItemClose_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (Closed != null)
				Closed(sender, e);
		}

		private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (PropertiesGroupChanged != null)
				PropertiesGroupChanged(sender, e);

			if (xtraTabControl.SelectedTabPage == xtraTabPageComment)
				_helpKey = "rightbarcomments";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageDigital)
				_helpKey = "rightbardigital";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageLogo)
				_helpKey = "rightbarlogo";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageNewspaper)
				_helpKey = "rightbarprint";
			else if (xtraTabControl.SelectedTabPage == xtraTabPageRadio)
				_helpKey = string.Empty;
			else if (xtraTabControl.SelectedTabPage == xtraTabPageTV)
				_helpKey = string.Empty;
			else
				_helpKey = string.Empty;
		}
	}
}