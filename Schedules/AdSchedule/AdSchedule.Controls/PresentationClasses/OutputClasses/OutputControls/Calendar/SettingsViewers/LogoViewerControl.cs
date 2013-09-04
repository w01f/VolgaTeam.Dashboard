using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
	[ToolboxItem(false)]
	public partial class LogoViewerControl : UserControl, ICalendarSettingsViewer
	{
		protected OutputCalendarControl _calendarControl = null;
		private MonthCalendarViewSettings _settings;

		public LogoViewerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ICalendarSettingsViewer Members
		public string Title
		{
			get { return "Show Logo on the slide"; }
		}

		public string FormToggleChangeCaption
		{
			get { return "Calendar Slide Logos"; }
		}

		public string EditButtonText
		{
			get { return "Change Slide Logos"; }
		}

		public string ApplyForAllText
		{
			get { return "Show this Logo on all Slides"; }
		}

		public bool ShowApplyForAll
		{
			get { return true; }
		}

		public void LoadSettings(OutputCalendarControl calendarControl, MonthCalendarViewSettings settings)
		{
			_calendarControl = calendarControl;
			_settings = settings;
			pictureEditLogo.Image = _settings.Logo;
		}

		public void SaveSettings()
		{
			if (pictureEditLogo.Image != null && _settings != null)
				_settings.Logo = pictureEditLogo.Image;
		}

		public void ApplySettingsForAll(MonthCalendarViewSettings[] allSettings)
		{
			if (_settings != null)
				foreach (MonthCalendarViewSettings settings in allSettings)
					if (_settings != settings)
					{
						if (_settings.Logo != null)
							settings.Logo = _settings.Logo.Clone() as Image;
						else
							settings.Logo = null;
					}
		}
		#endregion

		private void pictureEditLogo_Click(object sender, EventArgs e)
		{
			using (var form = new FormImageGallery(ListManager.Instance.Images))
			{
				form.SelectedImage = pictureEditLogo.Image;
				if (form.ShowDialog() == DialogResult.OK && form.SelectedImageSource != null)
				{
					pictureEditLogo.Image = new Bitmap(form.SelectedImage);
				}
			}
		}
	}
}