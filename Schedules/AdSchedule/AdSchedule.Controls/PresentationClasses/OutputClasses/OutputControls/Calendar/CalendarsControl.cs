using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class CalendarsControl : UserControl
	{
		private ICalendarControl _selectedOutput;

		public OutputCalendarControl Calendar { get; private set; }

		public CalendarsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			pnMain.Dock = DockStyle.Fill;
			Calendar = new OutputCalendarControl();
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (_selectedOutput != null && _selectedOutput.SettingsNotSaved)
				{
					SaveSchedule();
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		private void SaveSchedule(string newName = "")
		{
			if (_selectedOutput != null)
			{
				var nameChanged = !string.IsNullOrEmpty(newName);
				if (nameChanged)
					_selectedOutput.LocalSchedule.Name = newName;
				Controller.Instance.SaveSchedule(_selectedOutput.LocalSchedule, nameChanged, false, this);
				_selectedOutput.SettingsNotSaved = false;
				_selectedOutput.UpdateOutput(true);
			}
		}

		private ICalendarSettingsViewer GetSettingsViwerAccordingToggledButton(ButtonX toggledButton)
		{
			ICalendarSettingsViewer result = null;
			if (toggledButton == buttonXMonthShowSlideTitle)
				result = new TitleViewerControl();
			else if (toggledButton == buttonXMonthShowComment)
				result = new CommentViewerControl();
			else if (toggledButton == buttonXMonthShowLegend)
				result = new LegendViewerControl();
			else if (toggledButton == buttonXMonthShowLogo)
				result = new LogoViewerControl();
			else if (toggledButton == buttonXMonthShowBusinessName)
				result = new BusinessNameViewerControl();
			else if (toggledButton == buttonXMonthShowActiveDays)
				result = new TotalDaysViewerControl();
			else if (toggledButton == buttonXMonthShowAvgCost)
				result = new AvgCostViewerControl();
			else if (toggledButton == buttonXMonthShowDecisionMaker)
				result = new DecisionMakerViewerControl();
			else if (toggledButton == buttonXMonthShowTotalAds)
				result = new TotalAdsViewerControl();
			else if (toggledButton == buttonXMonthShowTotalCost)
				result = new TotalCostViewerControl();
			else if (toggledButton == buttonXMonthShowDigital)
				result = new DigitalViewerControl();
			return result;
		}

		public void UpdatePageAccordingToggledButton()
		{
			_selectedOutput = Calendar;
			if (_selectedOutput != null)
			{
				if (!pnMain.Controls.Contains(_selectedOutput as Control))
				{
					Application.DoEvents();
					pnEmpty.BringToFront();
					Application.DoEvents();
					pnMain.Controls.Add(_selectedOutput as Control);
					Application.DoEvents();
					pnMain.BringToFront();
					Application.DoEvents();
				}
				Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.CalendarHelp, _selectedOutput.HelpToolTip);
				Controller.Instance.CalendarOptions.Checked = _selectedOutput.LocalSchedule.ViewSettings.CalendarViewSettings.ShowOptions;
				splitContainerControl.PanelVisibility = _selectedOutput.LocalSchedule.ViewSettings.CalendarViewSettings.ShowOptions ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
				_selectedOutput.ApplySettings();
				_selectedOutput.UpdateMonthView();
				(_selectedOutput as Control).BringToFront();
				pnMain.BringToFront();
			}
			else
			{
				pnEmpty.BringToFront();
				Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.CalendarHelp, null);
			}
		}

		public void comboBoxEditCalendar_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_selectedOutput != null)
			{
				_selectedOutput.UpdateMonthView();
			}
		}

		public void buttonItemCalendarsToggledAdditional_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null || _selectedOutput == null) return;
			var settingsViewer = GetSettingsViwerAccordingToggledButton(button);
			if (button.Checked)
			{
				using (var form = new FormCalendarToggleChange())
				{
					form.Text = settingsViewer.FormToggleChangeCaption;
					form.buttonXEdit.Text = settingsViewer.EditButtonText;
					DialogResult formResult = form.ShowDialog();
					if (formResult == DialogResult.Yes)
						button.Checked = false;
					else if (formResult == DialogResult.No)
					{
						_selectedOutput.ShowOutputOptions(settingsViewer);
					}
				}
			}
			else
			{
				button.Checked = true;
				_selectedOutput.ShowOutputOptions(settingsViewer);
			}
		}

		public void buttonItemCalendarsToggled_CheckedChanged(object sender, EventArgs e)
		{
			if (_selectedOutput != null)
				_selectedOutput.UpdateToggledOptions();
		}

		public void buttonItemCalendarsToggledSize_CheckedChanged(object sender, EventArgs e)
		{
			if (_selectedOutput != null)
			{
				if (_selectedOutput.AllowToSave)
				{
					_selectedOutput.AllowToSave = false;
					if (sender == buttonXDayShowAdSize)
					{
						if (buttonXDayShowAdSize.Checked && (buttonXDayShowPageSize.Checked || buttonXDayShowPercentOfPage.Checked))
						{
							buttonXDayShowPageSize.Checked = false;
							buttonXDayShowPercentOfPage.Checked = false;
						}
					}
					else if (sender == buttonXDayShowPageSize)
					{
						if (buttonXDayShowPageSize.Checked && (buttonXDayShowAdSize.Checked || buttonXDayShowPercentOfPage.Checked))
						{
							buttonXDayShowAdSize.Checked = false;
							buttonXDayShowPercentOfPage.Checked = false;
						}
					}
					else if (sender == buttonXDayShowPercentOfPage)
					{
						if (buttonXDayShowPercentOfPage.Checked && (buttonXDayShowAdSize.Checked || buttonXDayShowPageSize.Checked))
						{
							buttonXDayShowAdSize.Checked = false;
							buttonXDayShowPageSize.Checked = false;
						}
					}
					_selectedOutput.AllowToSave = true;
				}
			}
		}

		public void buttonItemCalendarsThemeColor_Click(object sender, EventArgs e)
		{
			if (_selectedOutput != null)
			{
				_selectedOutput.AllowToSave = false;
				buttonXColorBlack.Checked = false;
				buttonXColorBlue.Checked = false;
				buttonXColorGray.Checked = false;
				buttonXColorGreen.Checked = false;
				buttonXColorOrange.Checked = false;
				buttonXColorTeal.Checked = false;
				_selectedOutput.AllowToSave = true;
				(sender as ButtonX).Checked = true;
			}
		}

		public void MonthList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_selectedOutput != null && _selectedOutput.AllowToSave)
			{
				_selectedOutput.UpdateMonthView();
			}
		}

		public void buttonItemCalendarsOptions_CheckedChanged(object sender, EventArgs e)
		{
			if (_selectedOutput != null && _selectedOutput.AllowToSave)
			{
				_selectedOutput.LocalSchedule.ViewSettings.CalendarViewSettings.ShowOptions = Controller.Instance.CalendarOptions.Checked;
				splitContainerControl.PanelVisibility = _selectedOutput.LocalSchedule.ViewSettings.CalendarViewSettings.ShowOptions ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
				_selectedOutput.SettingsNotSaved = true;
			}
		}

		public void buttonItemCalendarsExport_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_selectedOutput != null)
				_selectedOutput.Export();
		}

		public void buttonItemCalendarsPreview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_selectedOutput != null)
				_selectedOutput.Preview();
		}

		public void buttonItemCalendarsPowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_selectedOutput != null)
				_selectedOutput.PrintOutput();
		}

		public void buttonItemCalendarsEmail_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			if (_selectedOutput != null)
				_selectedOutput.Email();
		}

		public void buttonItemCalendarSave_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void buttonItemCalendarSaveAs_Click(object sender, EventArgs e)
		{
			if (_selectedOutput != null)
				using (var from = new FormNewSchedule())
				{
					from.Text = "Save Schedule";
					from.laLogo.Text = "Please set a new name for your Schedule:";
					if (from.ShowDialog() == DialogResult.OK)
					{
						if (!string.IsNullOrEmpty(from.ScheduleName))
						{
							SaveSchedule(from.ScheduleName);
							Utilities.Instance.ShowInformation("Schedule was saved");
						}
						else
						{
							Utilities.Instance.ShowWarning("Schedule Name can't be empty");
						}
					}
				}
		}

		public void buttonItemCalendarsHelp_Click(object sender, EventArgs e)
		{
			if (_selectedOutput != null)
				_selectedOutput.OpenHelp();
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			PictureBox pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			PictureBox pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion

		private void pbSettingsDayHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("calnavbar1");
		}

		private void pbSettingsMonthHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("calnavbar2");
		}

		private void pbSettingsThemeHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("calnavbar3");
		}
	}

	public interface ICalendarControl
	{
		Schedule LocalSchedule { get; set; }
		bool SettingsNotSaved { get; set; }
		SuperTooltipInfo HelpToolTip { get; }
		bool AllowToSave { get; set; }
		List<Insert> Inserts { get; }

		void ApplySettings();
		void UpdateMonthView();
		void UpdateToggledOptions();
		void ShowOutputOptions(ICalendarSettingsViewer settingsViewer);
		void UpdateOutput(bool quickLoad);
		void Export();
		void PrintOutput();
		void Email();
		void Preview();
		void OpenHelp();
	}
}