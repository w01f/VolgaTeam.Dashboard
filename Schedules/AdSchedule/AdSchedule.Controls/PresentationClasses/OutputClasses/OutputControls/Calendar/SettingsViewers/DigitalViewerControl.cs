using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers
{
	[ToolboxItem(false)]
	public partial class DigitalViewerControl : UserControl, ICalendarSettingsViewer
	{
		private bool _loading;
		protected OutputCalendarControl _calendarControl = null;
		private MonthCalendarViewSettings _settings;

		public DigitalViewerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ICalendarSettingsViewer Members
		public string Title
		{
			get { return "Add a Digital Product Info"; }
		}

		public Image Logo
		{
			get { return Properties.Resources.Digital; }
		}

		public string FormToggleChangeCaption
		{
			get { return "Digital Info"; }
		}

		public string EditButtonText
		{
			get { return "Edit Digital Info"; }
		}

		public string ApplyForAllText
		{
			get { return "Show this Digital Info on all slides"; }
		}

		public bool ShowApplyForAll
		{
			get { return true; }
		}

		public void LoadSettings(OutputCalendarControl calendarControl, MonthCalendarViewSettings settings)
		{
			_calendarControl = calendarControl;
			_settings = settings;
			_loading = true;
			checkEditEnable.Checked = _settings.DigitalLegend.Enabled;
			checkEditAllowEdit.Checked = _settings.DigitalLegend.AllowEdit;

			buttonXShowWebsites.Checked = _settings.DigitalLegend.ShowWebsites;
			buttonXShowProduct.Checked = _settings.DigitalLegend.ShowProduct;
			buttonXShowDimensions.Checked = _settings.DigitalLegend.ShowDimensions;
			buttonXShowDates.Checked = _settings.DigitalLegend.ShowDates;
			buttonXShowImpressions.Checked = _settings.DigitalLegend.ShowImpressions;
			buttonXShowCPM.Checked = _settings.DigitalLegend.ShowCPM;
			buttonXShowInvestment.Checked = _settings.DigitalLegend.ShowInvestment;
			memoEditInfo.EditValue = null;
			if (_settings.DigitalLegend.AllowEdit && !String.IsNullOrEmpty(_settings.DigitalLegend.Info))
				memoEditInfo.EditValue = _settings.DigitalLegend.Info;
			if (checkEditEnable.Checked && memoEditInfo.EditValue == null)
				memoEditInfo.EditValue = _calendarControl.LocalSchedule.GetDigitalInfo(new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
			_loading = false;
		}

		public void SaveSettings()
		{
			if (_settings == null) return;
			_settings.DigitalLegend.Enabled = checkEditEnable.Checked;
			_settings.DigitalLegend.AllowEdit = checkEditAllowEdit.Checked;

			_settings.DigitalLegend.ShowWebsites = buttonXShowWebsites.Checked;
			_settings.DigitalLegend.ShowProduct = buttonXShowProduct.Checked;
			_settings.DigitalLegend.ShowDimensions = buttonXShowDimensions.Checked;
			_settings.DigitalLegend.ShowDates = buttonXShowDates.Checked;
			_settings.DigitalLegend.ShowImpressions = buttonXShowImpressions.Checked;
			_settings.DigitalLegend.ShowCPM = buttonXShowCPM.Checked;
			_settings.DigitalLegend.ShowInvestment = buttonXShowInvestment.Checked;
			_settings.DigitalLegend.Info = checkEditAllowEdit.Checked && memoEditInfo.EditValue != null ? memoEditInfo.EditValue.ToString() : String.Empty;
		}

		public void ApplySettingsForAll(MonthCalendarViewSettings[] allSettings)
		{
			if (_settings == null) return;
			foreach (var settings in allSettings.Where(settings => _settings != settings))
				settings.DigitalLegend = _settings.DigitalLegend.Clone();
		}

		#endregion

		private void UpdateWarning()
		{
			var togglesSelected = 0;
			if (buttonXShowWebsites.Checked)
				togglesSelected++;
			if (buttonXShowProduct.Checked)
				togglesSelected++;
			if (buttonXShowDimensions.Checked)
				togglesSelected++;
			if (buttonXShowDates.Checked)
				togglesSelected++;
			if (buttonXShowImpressions.Checked)
				togglesSelected++;
			if (buttonXShowCPM.Checked)
				togglesSelected++;
			if (buttonXShowInvestment.Checked)
				togglesSelected++;
			labelControlWarning.Visible = togglesSelected > 3;
		}

		private void checkEditEnable_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkEditEnable.Checked;
			if (!_loading)
			{
				if (checkEditEnable.Checked)
				{
					if ((checkEditAllowEdit.Checked || memoEditInfo.EditValue == null))
						memoEditInfo.EditValue = _calendarControl.LocalSchedule.GetDigitalInfo(new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
				}
				else
					memoEditInfo.EditValue = null;
			}
		}

		private void checkEditAllowEdit_CheckedChanged(object sender, EventArgs e)
		{
			buttonXShowWebsites.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowProduct.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowDimensions.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowDates.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowImpressions.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowCPM.Enabled = !checkEditAllowEdit.Checked;
			buttonXShowInvestment.Enabled = !checkEditAllowEdit.Checked;
			memoEditInfo.Properties.ReadOnly = !checkEditAllowEdit.Checked;

			if (!_loading && !checkEditAllowEdit.Checked)
				memoEditInfo.EditValue = _calendarControl.LocalSchedule.GetDigitalInfo(new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
		}

		private void buttonXShow_CheckedChanged(object sender, EventArgs e)
		{
			UpdateWarning();
			if (!_loading)
				memoEditInfo.EditValue = _calendarControl.LocalSchedule.GetDigitalInfo(new RequestDigitalInfoEventArgs(memoEditInfo, buttonXShowWebsites.Checked, buttonXShowProduct.Checked, buttonXShowDimensions.Checked, buttonXShowDates.Checked, buttonXShowImpressions.Checked, buttonXShowCPM.Checked, buttonXShowInvestment.Checked));
		}
	}
}