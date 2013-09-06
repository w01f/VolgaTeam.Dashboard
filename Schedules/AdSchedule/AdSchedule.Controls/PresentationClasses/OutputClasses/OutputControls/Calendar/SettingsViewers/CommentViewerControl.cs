using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
	[ToolboxItem(false)]
	public partial class CommentViewerControl : UserControl, ICalendarSettingsViewer
	{
		protected OutputCalendarControl _calendarControl = null;
		private MonthCalendarViewSettings _settings;

		public CommentViewerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ICalendarSettingsViewer Members
		public string Title
		{
			get { return "Add a Custom Comment"; }
		}

		public Image Logo
		{
			get { return Properties.Resources.GridDetails; }
		}

		public string FormToggleChangeCaption
		{
			get { return "Custom Comments"; }
		}

		public string EditButtonText
		{
			get { return "Edit Comments"; }
		}

		public string ApplyForAllText
		{
			get { return "Show this Exact Same Notes on all slides"; }
		}

		public bool ShowApplyForAll
		{
			get { return true; }
		}

		public void LoadSettings(OutputCalendarControl calendarControl, MonthCalendarViewSettings settings)
		{
			_calendarControl = calendarControl;
			_settings = settings;
			if (!string.IsNullOrEmpty(_settings.Comments))
			{
				checkEditUseComment.Checked = true;
				memoEditComment.EditValue = _settings.Comments;
			}
			else
				checkEditUseComment.Checked = false;
		}

		public void SaveSettings()
		{
			if (_settings != null)
			{
				if (checkEditUseComment.Checked && memoEditComment.EditValue != null && !string.IsNullOrEmpty(memoEditComment.EditValue.ToString().Trim()))
					_settings.Comments = memoEditComment.EditValue.ToString().Trim();
				else
					_settings.Comments = string.Empty;
			}
		}

		public void ApplySettingsForAll(MonthCalendarViewSettings[] allSettings)
		{
			if (_settings != null)
				foreach (MonthCalendarViewSettings settings in allSettings)
					if (_settings != settings)
						settings.Comments = _settings.Comments;
		}
		#endregion

		private void checkEditUseComment_CheckedChanged(object sender, EventArgs e)
		{
			memoEditComment.Enabled = checkEditUseComment.Checked;
			if (!checkEditUseComment.Checked)
				memoEditComment.EditValue = null;
		}
	}
}