using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	[ToolboxItem(false)]
	public partial class HomeControl : UserControl
	{
		private bool _allowToSave;
		private bool _digitalChanged;
		private bool _calendarTypeChanged;
		private RegularSchedule _localSchedule;
		private SpotType _loadedScheduleType;

		public HomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			LoadDigitalCategories();
			if ((base.CreateGraphics()).DpiX > 96)
			{
			}
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			stationsControl.Changed += (o, e) => { SettingsNotSaved = true; };
			daypartsControl.Changed += (o, e) => { SettingsNotSaved = true; };
		}

		public bool SettingsNotSaved { get; set; }

		public bool AllowToLeaveControl(bool quiet = false)
		{
			bool result = true;
			if (SettingsNotSaved)
				result = SaveSchedule(quiet);
			return result;
		}

		private void UpdateScheduleControls()
		{
			bool enableSchedules = Controller.Instance.HomeBusinessName.EditValue != null &
								   Controller.Instance.HomeDecisionMaker.EditValue != null &
								   Controller.Instance.HomeClientType.EditValue != null &
								   Controller.Instance.HomePresentationDate.EditValue != null &
								   Controller.Instance.HomeFlightDatesStart.EditValue != null &
								   Controller.Instance.HomeFlightDatesEnd.EditValue != null;
			Controller.Instance.UpdateScheduleTabs(enableSchedules);
			buttonXWeeklySchedule.Enabled = enableSchedules;
			buttonXMonthlySchedule.Enabled = enableSchedules;
		}

		private void UpdateScheduleType(SpotType selectedScheduleType)
		{
			switch (selectedScheduleType)
			{
				case SpotType.Week:
					Controller.Instance.TabWeeklySchedule.Visible = true;
					Controller.Instance.TabMonthlySchedule.Visible = false;
					break;
				case SpotType.Month:
					Controller.Instance.TabWeeklySchedule.Visible = false;
					Controller.Instance.TabMonthlySchedule.Visible = true;
					break;
			}
			Controller.Instance.Ribbon.Refresh();
			Controller.Instance.Ribbon.RecalcLayout();
			Controller.Instance.Ribbon.PerformLayout();
		}

		private void LoadDigitalCategories()
		{
			foreach (var category in ListManager.Instance.Categories)
			{
				var categoryButton = new ButtonItem
				{
					Image = category.Logo,
					Text = "<b>" + category.TooltipTitle + "</b><p>" + category.TooltipValue + "</p>",
					ImagePaddingHorizontal = 8,
					SubItemsExpandWidth = 14,
					Tag = category
				};
				categoryButton.Click += DigitalProductAdd;
				Controller.Instance.HomeProductAdd.SubItems.Add(categoryButton);
			}
		}

		private void UpdateProductsCount()
		{
			xtraTabPageDigital.Text = String.Format("Digital Strategy({0})", _localSchedule.DigitalProducts.Count);
		}

		private void UpdateFlexFlightDatesWarning()
		{
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				var warningText = new List<string>();
				if (Controller.Instance.HomeFlightDatesStart.EditValue != null)
				{
					var startDate = Controller.Instance.HomeFlightDatesStart.DateTime;
					if (startDate.DayOfWeek != _localSchedule.StartDayOfWeek)
					{
						var weekEnd = startDate;
						while (weekEnd.DayOfWeek != _localSchedule.EndDayOfWeek)
							weekEnd = weekEnd.AddDays(1);
						warningText.Add(String.Format("*The FIRST WEEK of your schedule STARTS on a {0}.{1}({2} - {3})", startDate.DayOfWeek, Environment.NewLine, startDate.ToString("M/d/yy"), weekEnd.ToString("M/d/yy")));
					}
				}
				if (Controller.Instance.HomeFlightDatesEnd.EditValue != null)
				{
					var endDate = Controller.Instance.HomeFlightDatesEnd.DateTime;
					if (endDate.DayOfWeek != _localSchedule.EndDayOfWeek)
					{
						var weekStart = endDate;
						while (weekStart.DayOfWeek != _localSchedule.StartDayOfWeek)
							weekStart = weekStart.AddDays(-1);
						warningText.Add(String.Format("*The LAST WEEK of your schedule ENDS on a {0}.{1}({2} - {3})", endDate.DayOfWeek, Environment.NewLine, weekStart.ToString("M/d/yy"), endDate.ToString("M/d/yy")));
					}
				}
				laFlexDateWarning.Text = String.Join(String.Format("{0}{0}", Environment.NewLine), warningText);
			}
			else
				laFlexDateWarning.Text = String.Empty;
		}
		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			_digitalChanged = false;
			_calendarTypeChanged = false;
			digitalProductListControl.UpdateData(_localSchedule,
				() =>
				{
					UpdateProductsCount();
					Controller.Instance.UpdateDigitalProductTab(_localSchedule.DigitalProducts.Any(p => !String.IsNullOrEmpty(p.Name)));
					_digitalChanged = true;
					if (_allowToSave)
						SettingsNotSaved = true;
				},
				activity =>
				{
					var propertyEditActivity = activity as PropertyEditActivity;
					if (propertyEditActivity != null)
						propertyEditActivity.Advertiser = Controller.Instance.HomeBusinessName.EditValue as String;
					BusinessWrapper.Instance.ActivityManager.AddActivity(activity);
				}
			);
			if (!quickLoad)
			{
				#region Media Tab
				Controller.Instance.HomeAccountNumberCheck.Enabled = _localSchedule.ViewSettings.SharedHomeViewSettings.EnableAccountNumber;

				xtraTabPageMedia.Text = String.Format("{0} Strategy", MediaMetaData.Instance.DataTypeString);
				pbMediaLogo.Image = MediaMetaData.Instance.DataType == MediaDataType.TV ? Properties.Resources.HomeTVLogo : Properties.Resources.HomeRadioLogo;

				Controller.Instance.HomeClientType.Properties.Items.Clear();
				Controller.Instance.HomeClientType.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.ClientTypes.ToArray());

				Controller.Instance.HomeBusinessName.EditValue = !string.IsNullOrEmpty(_localSchedule.BusinessName) ? _localSchedule.BusinessName : null;
				Controller.Instance.HomeDecisionMaker.EditValue = !string.IsNullOrEmpty(_localSchedule.DecisionMaker) ? _localSchedule.DecisionMaker : null;
				if (!string.IsNullOrEmpty(_localSchedule.ClientType))
					Controller.Instance.HomeClientType.SelectedIndex = Controller.Instance.HomeClientType.Properties.Items.IndexOf(_localSchedule.ClientType);
				Controller.Instance.HomeAccountNumberCheck.Checked = !string.IsNullOrEmpty(_localSchedule.AccountNumber);
				Controller.Instance.HomeAccountNumberText.EditValue = _localSchedule.AccountNumber;

				Controller.Instance.HomePresentationDate.EditValue = _localSchedule.PresentationDate;
				Controller.Instance.HomeFlightDatesStart.EditValue = _localSchedule.UserFlightDateStart;
				Controller.Instance.HomeFlightDatesEnd.EditValue = _localSchedule.UserFlightDateEnd;

				_loadedScheduleType = _localSchedule.SelectedSpotType;
				switch (_localSchedule.SelectedSpotType)
				{
					case SpotType.Week:
						buttonXWeeklySchedule.Checked = true;
						buttonXMonthlySchedule.Checked = false;
						break;
					case SpotType.Month:
						buttonXWeeklySchedule.Checked = false;
						buttonXMonthlySchedule.Checked = true;
						break;
				}

				var importedDemos = MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos);
				if (!importedDemos.Any())
				{
					buttonXUseDemos.Text = "Show Demo Estimates";
					pnDemosCustom.Visible = false;
					pnDemosImport.Visible = false;
					pnSelectSource.Visible = false;
				}

				comboBoxEditSource.Properties.Items.Clear();
				comboBoxEditSource.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos).Select(d => d.Source).Distinct().ToArray());

				if (_localSchedule.UseDemo)
				{
					buttonXUseDemos.Checked = true;

					buttonXDemosCustom.Enabled = true;
					buttonXDemosCustom.Checked = !_localSchedule.ImportDemo;

					buttonXDemosImport.Enabled = true;
					buttonXDemosImport.Checked = _localSchedule.ImportDemo;

					if (_localSchedule.ImportDemo)
					{
						comboBoxEditSource.Enabled = true;
						comboBoxEditSource.EditValue = _localSchedule.Source;

						buttonXDemosRtg.Enabled = false;
						buttonXDemosRtg.Checked = false;

						buttonXDemosImps.Enabled = false;
						buttonXDemosImps.Checked = true;

						comboBoxEditDemos.Properties.Items.Clear();
						var demos = MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos).Where(d => d.Source == _localSchedule.Source);
						if (demos.Any())
						{
							comboBoxEditDemos.Enabled = true;
							comboBoxEditDemos.Properties.Items.AddRange(demos.GroupBy(d => d.DisplayString).Select(g => g.First()).ToArray());
							comboBoxEditDemos.EditValue = demos.FirstOrDefault(d => d.DemoType == _localSchedule.DemoType && d.Source == _localSchedule.Source && d.Name == _localSchedule.Demo);
						}
						else
						{
							comboBoxEditDemos.Enabled = false;
							comboBoxEditDemos.EditValue = null;
						}
					}
					else
					{
						comboBoxEditDemos.Enabled = true;

						comboBoxEditSource.Enabled = false;
						comboBoxEditSource.EditValue = null;

						buttonXDemosRtg.Enabled = true;
						buttonXDemosRtg.Checked = _localSchedule.DemoType == DemoType.Rtg;

						buttonXDemosImps.Enabled = true;
						buttonXDemosImps.Checked = _localSchedule.DemoType == DemoType.Imp;

						comboBoxEditDemos.Properties.Items.Clear();
						comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
						comboBoxEditDemos.EditValue = _localSchedule.Demo;
					}
				}
				else
				{
					buttonXUseDemos.Checked = false;

					buttonXDemosCustom.Enabled = false;
					buttonXDemosCustom.Checked = true;

					buttonXDemosImport.Enabled = false;
					buttonXDemosImport.Checked = false;

					comboBoxEditDemos.Enabled = false;
					comboBoxEditDemos.Properties.Items.Clear();
					comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
					comboBoxEditDemos.EditValue = null;

					comboBoxEditSource.Enabled = false;
					comboBoxEditSource.EditValue = null;

					buttonXDemosRtg.Enabled = false;
					buttonXDemosRtg.Checked = false;
					buttonXDemosImps.Enabled = false;
					buttonXDemosImps.Checked = true;
				}

				buttonXCalendarTypeMondayBased.Checked = _localSchedule.MondayBased;
				buttonXCalendarTypeSundayBased.Checked = !_localSchedule.MondayBased;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = _localSchedule.StartDayOfWeek;

				stationsControl.LoadData(_localSchedule);
				daypartsControl.LoadData(_localSchedule);
				#endregion

				#region Digital tab
				digitalProductListControl.LoadView();
				xtraTabControlProducts_SelectedPageChanged(this, new TabPageChangedEventArgs(null, xtraTabControlMain.SelectedTabPage));
				#endregion
			}
			UpdateScheduleType(_localSchedule.SelectedSpotType);
			UpdateScheduleControls();
			UpdateFlexFlightDatesWarning();
			SettingsNotSaved = false;
			_allowToSave = true;
		}

		private bool SaveSchedule(bool quiet = false, string scheduleName = "")
		{
			bool quickSave = true;
			digitalProductListControl.CloseEditors();
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			var businessName = Controller.Instance.HomeBusinessName.EditValue as String;
			if (!String.IsNullOrEmpty(businessName))
			{
				if (_localSchedule.BusinessName != businessName)
				{
					_localSchedule.BusinessName = businessName;
					BusinessWrapper.Instance.ActivityManager.AddActivity(new PropertyEditActivity("Business Name", businessName));
				}
			}
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return false;
			}
			var decisionMaker = Controller.Instance.HomeDecisionMaker.EditValue as String;
			if (!String.IsNullOrEmpty(decisionMaker))
			{
				if (_localSchedule.DecisionMaker != decisionMaker)
				{
					_localSchedule.DecisionMaker = decisionMaker;
					BusinessWrapper.Instance.ActivityManager.AddActivity(new PropertyEditActivity("Decision Maker", decisionMaker));
				}
			}
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return false;
			}

			if (Controller.Instance.HomeClientType.EditValue != null)
				_localSchedule.ClientType = Controller.Instance.HomeClientType.EditValue.ToString();
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("You must set Client type before save");
				return false;
			}

			if (Controller.Instance.HomeAccountNumberCheck.Checked && Controller.Instance.HomeAccountNumberText.EditValue != null)
				_localSchedule.AccountNumber = Controller.Instance.HomeAccountNumberText.EditValue.ToString();
			else
				_localSchedule.AccountNumber = string.Empty;

			if (Controller.Instance.HomePresentationDate.EditValue != null)
				_localSchedule.PresentationDate = Controller.Instance.HomePresentationDate.DateTime;
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
				return false;
			}

			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && Controller.Instance.HomeFlightDatesEnd.EditValue != null)
			{
				var startDate = Controller.Instance.HomeFlightDatesStart.DateTime;
				var endDate = Controller.Instance.HomeFlightDatesEnd.DateTime;

				if (_localSchedule.FlightDateStart.HasValue && _localSchedule.FlightDateEnd.HasValue)
				{
					if (_localSchedule.FlightDateStart.Value != startDate || _localSchedule.FlightDateEnd.Value != endDate)
					{
						if (!quiet)
						{
							if (Utilities.Instance.ShowWarningQuestion("Flight Dates have been changed and all Spots will be recreated\nDo you want to proceed?") == DialogResult.Yes)
								quickSave = false;
							else
								return false;
						}
						else
							quickSave = false;
					}
				}
				else
					quickSave = false;

				_localSchedule.UserFlightDateStart = startDate;
				_localSchedule.UserFlightDateEnd = endDate;

				_localSchedule.UseDemo = buttonXUseDemos.Checked;
				_localSchedule.ImportDemo = buttonXDemosImport.Checked;
				_localSchedule.Source = comboBoxEditSource.EditValue as String;
				if (buttonXDemosImport.Checked)
				{
					var demo = comboBoxEditDemos.EditValue as Demo;
					_localSchedule.Demo = demo != null ? demo.Name : null;
					_localSchedule.DemoType = demo != null ? demo.DemoType : DemoType.Rtg;
				}
				else
				{
					if (buttonXDemosRtg.Checked)
						_localSchedule.DemoType = DemoType.Rtg;
					else if (buttonXDemosImps.Checked)
						_localSchedule.DemoType = DemoType.Imp;
					_localSchedule.Demo = comboBoxEditDemos.EditValue as String;
				}

				_localSchedule.Section.ShowRating = _localSchedule.Section.ShowRating & _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
				_localSchedule.Section.ShowCPP = _localSchedule.Section.ShowCPP & _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
				_localSchedule.Section.ShowGRP = _localSchedule.Section.ShowGRP & _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
				_localSchedule.Section.ShowTotalCPP = _localSchedule.Section.ShowTotalCPP & _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);
				_localSchedule.Section.ShowTotalGRP = _localSchedule.Section.ShowTotalGRP & _localSchedule.UseDemo & !String.IsNullOrEmpty(_localSchedule.Demo);

				if (!quickSave)
				{
					_localSchedule.Section.RebuildSpots();
				}
			}
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Flight Dates before you proceed.");
				return false;
			}

			if (_localSchedule.SelectedSpotType != _loadedScheduleType)
			{
				_localSchedule.ResetSection();
				quickSave = false;
			}

			if (stationsControl.HasChanged)
			{
				_localSchedule.Stations.Clear();
				_localSchedule.Stations.AddRange(stationsControl.GetData());
			}

			if (daypartsControl.HasChanged)
			{
				_localSchedule.Dayparts.Clear();
				_localSchedule.Dayparts.AddRange(daypartsControl.GetData());
			}

			if (_localSchedule.DigitalProducts.Any(product => String.IsNullOrEmpty(product.Name)))
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Web Product in each line before you proceed.");
				return false;
			}

			digitalProductListControl.SaveView();

			UpdateScheduleControls();
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, quickSave, _digitalChanged, _calendarTypeChanged, this);
			SettingsNotSaved = false;
			daypartsControl.HasChanged = false;
			return true;
		}

		private void xtraTabControlProducts_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			Controller.Instance.HomeProductAdd.Enabled =
			Controller.Instance.HomeProductClone.Enabled =
				e.Page == xtraTabPageDigital;
			UpdateProductsCount();
			splitContainerControl.PanelVisibility = e.Page == xtraTabPageDigital ? SplitPanelVisibility.Panel1 : SplitPanelVisibility.Both;
		}

		#region Editors Events

		public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateScheduleControls();
			SettingsNotSaved = true;
		}

		public void checkBoxItemAccountNumber_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			Controller.Instance.HomeAccountNumberText.Enabled = Controller.Instance.HomeAccountNumberCheck.Checked;
			SchedulePropertyEditValueChanged(null, null);
		}

		public void FlightDateStartEditValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && _allowToSave)
			{
				var dateStart = Controller.Instance.HomeFlightDatesStart.DateTime;
				SettingsNotSaved = true;
				if (Controller.Instance.HomeFlightDatesEnd.EditValue == null)
				{
					while (dateStart.DayOfWeek != _localSchedule.EndDayOfWeek)
						dateStart = dateStart.AddDays(1);
					_allowToSave = false;
					Controller.Instance.HomeFlightDatesEnd.EditValue = dateStart;
					_allowToSave = true;
				}
				UpdateFlexFlightDatesWarning();
			}
			SchedulePropertyEditValueChanged(null, null);
		}

		public void FlightDateEndEditValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && _allowToSave)
			{
				UpdateFlexFlightDatesWarning();
				SettingsNotSaved = true;
			}
			SchedulePropertyEditValueChanged(null, null);
		}

		public void CalcWeeksOnFlightDatesChange(object sender, EventArgs e)
		{
			Controller.Instance.HomeWeeks.Text = "";
			Controller.Instance.HomeWeeks.Visible = false;
			if (Controller.Instance.HomeFlightDatesStart.EditValue == null || Controller.Instance.HomeFlightDatesEnd.EditValue == null) return;
			var startDate = Controller.Instance.HomeFlightDatesStart.DateTime;
			while (startDate.DayOfWeek != _localSchedule.StartDayOfWeek)
				startDate = startDate.AddDays(-1);
			var endDate = Controller.Instance.HomeFlightDatesEnd.DateTime;
			while (endDate.DayOfWeek != _localSchedule.EndDayOfWeek)
				endDate = endDate.AddDays(1);
			var datesRange = endDate - startDate;
			var weeksCount = datesRange.Days / 7 + 1;
			Controller.Instance.HomeWeeks.Text = weeksCount + (weeksCount > 1 ? " Weeks" : " Week");
			Controller.Instance.HomeWeeks.Visible = true;
		}

		public void dateEditFlightDatesStart_CloseUp(object sender, CloseUpEventArgs e)
		{
			var dateEdit = sender as DateEdit;
			if (dateEdit == null) return;
			if (dateEdit.EditValue == e.Value) return;
			if (e.Value == null) return;
			DateTime temp;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			var moveDateToWeekStart = true;
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				if (temp.DayOfWeek != _localSchedule.StartDayOfWeek)
					if (Utilities.Instance.ShowWarningQuestion(String.Format("Are you sure you want to start your schedule on a {0}?{1}{1}The broadcast week normally starts on a {2}.", temp.DayOfWeek, Environment.NewLine, _localSchedule.StartDayOfWeek)) == DialogResult.Yes)
						moveDateToWeekStart = false;
			}
			if (moveDateToWeekStart)
			{
				while (temp.DayOfWeek != _localSchedule.StartDayOfWeek)
					temp = temp.AddDays(-1);
				e.Value = temp;
			}
		}

		public void dateEditFlightDatesEnd_CloseUp(object sender, CloseUpEventArgs e)
		{
			var dateEdit = sender as DateEdit;
			if (dateEdit == null) return;
			if (dateEdit.EditValue == e.Value) return;
			if (e.Value == null) return;
			DateTime temp;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			var moveDateToWeekEnd = true;
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				if (temp.DayOfWeek != _localSchedule.EndDayOfWeek)
					if (Utilities.Instance.ShowWarningQuestion(String.Format("Are you sure you want to end your schedule on a {0}?{1}{1}The broadcast week normally ends on a {2}.", temp.DayOfWeek, Environment.NewLine, _localSchedule.EndDayOfWeek)) == DialogResult.Yes)
						moveDateToWeekEnd = false;
			}
			if (moveDateToWeekEnd)
			{
				while (temp.DayOfWeek != _localSchedule.EndDayOfWeek)
					temp = temp.AddDays(1);
				e.Value = temp;
			}
		}

		public void SchedulePropertiesEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Tab) return;
			if (sender == Controller.Instance.HomeBusinessName)
				Controller.Instance.HomeDecisionMaker.Focus();
			else if (sender == Controller.Instance.HomeDecisionMaker)
				Controller.Instance.HomeClientType.Focus();
			else if (sender == Controller.Instance.HomeClientType)
				Controller.Instance.HomePresentationDate.Focus();
			else if (sender == Controller.Instance.HomePresentationDate)
				Controller.Instance.HomeFlightDatesStart.Focus();
			else if (sender == Controller.Instance.HomeFlightDatesStart)
				Controller.Instance.HomeFlightDatesEnd.Focus();
			else if (sender == Controller.Instance.HomeFlightDatesEnd)
				Controller.Instance.HomeBusinessName.Focus();
			e.Handled = true;
		}
		#endregion

		#region Ribbon Operations Events
		public void Help_Click(object sender, EventArgs e)
		{
			if (xtraTabControlMain.SelectedTabPage == xtraTabPageMedia)
				BusinessWrapper.Instance.HelpManager.OpenHelpLink(String.Format("home{0}", MediaMetaData.Instance.DataType == MediaDataType.TV ? "tv" : "rd"));
			else if (xtraTabControlMain.SelectedTabPage == xtraTabPageDigital)
				BusinessWrapper.Instance.HelpManager.OpenHelpLink(String.Format("home{0}", "dg"));
		}

		public void Save_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule())
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (@from.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(@from.ScheduleName))
				{
					if (SaveSchedule(scheduleName: @from.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
				{
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
				}
			}
		}

		#endregion

		#region Demos Processing
		private void buttonXDemosNo_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (buttonXUseDemos.Checked)
			{
				buttonXDemosCustom.Enabled = true;
				buttonXDemosImport.Enabled = true;
				buttonXDemos_CheckedChanged(sender, e);
			}
			else
			{
				buttonXDemosCustom.Enabled = false;
				buttonXDemosCustom.Checked = true;

				buttonXDemosImport.Enabled = false;
				buttonXDemosImport.Checked = false;

				comboBoxEditDemos.Enabled = false;
				comboBoxEditDemos.Properties.Items.Clear();
				comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
				comboBoxEditDemos.EditValue = null;

				comboBoxEditSource.Enabled = false;
				comboBoxEditSource.EditValue = null;

				buttonXDemosRtg.Enabled = false;
				buttonXDemosRtg.Checked = false;
				buttonXDemosImps.Enabled = false;
				buttonXDemosImps.Checked = true;
			}
			SettingsNotSaved = true;
		}

		private void buttonXDemos_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null || button.Checked) return;
			buttonXDemosCustom.Checked = false;
			buttonXDemosImport.Checked = false;
			button.Checked = true;
		}

		private void buttonXDemos_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (buttonXDemosImport.Checked)
			{
				comboBoxEditSource.Enabled = true;
				buttonXDemosRtg.Enabled = false;
				buttonXDemosRtg.Checked = false;
				buttonXDemosImps.Enabled = false;
				buttonXDemosImps.Checked = true;
				comboBoxEditSource_EditValueChanged(sender, e);
			}
			else
			{
				comboBoxEditDemos.Enabled = true;
				comboBoxEditSource.Enabled = false;
				comboBoxEditSource.EditValue = null;
				buttonXDemosRtg.Enabled = true;
				buttonXDemosImps.Enabled = true;
				comboBoxEditDemos.Properties.Items.Clear();
				comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
				comboBoxEditDemos.EditValue = null;
			}
			SettingsNotSaved = true;
		}

		private void comboBoxEditSource_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			comboBoxEditDemos.Properties.Items.Clear();
			var demos = MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos).Where(d => d.Source == comboBoxEditSource.EditValue as String);
			comboBoxEditDemos.Enabled = demos.Any();
			comboBoxEditDemos.Properties.Items.AddRange(demos.GroupBy(d => d.DisplayString).Select(g => g.First()).ToArray());
			comboBoxEditDemos.EditValue = null;
		}

		private void buttonXDemosType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null || button.Checked) return;
			buttonXDemosRtg.Checked = false;
			buttonXDemosImps.Checked = false;
			button.Checked = true;
			SettingsNotSaved = true;
		}

		private void buttonXDemosType_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}

		private void comboBoxEditDemos_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}
		#endregion

		#region Calendar processing
		private void buttonXCalendarType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked) return;
			if (Utilities.Instance.ShowWarningQuestion(String.Format("Your current schedule will be reset.{0}Do you want to continue and change calendar type?", Environment.NewLine)) != DialogResult.Yes) return;
			buttonXCalendarTypeMondayBased.Checked = false;
			buttonXCalendarTypeSundayBased.Checked = false;
			button.Checked = true;
		}

		private void buttonXCalendarType_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			_localSchedule.ResetCalendarType(buttonXCalendarTypeMondayBased.Checked);
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = _localSchedule.StartDayOfWeek;
			Controller.Instance.HomeFlightDatesStart.EditValue = null;
			Controller.Instance.HomeFlightDatesEnd.EditValue = null;
			Controller.Instance.HomeWeeks.Text = String.Empty;
			_calendarTypeChanged = true;
			SettingsNotSaved = true;
		}
		#endregion

		#region Buttons Clicks Events
		private void buttonXScheduleType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked) return;
			buttonXWeeklySchedule.Checked = false;
			buttonXMonthlySchedule.Checked = false;
			button.Checked = true;
		}

		private void buttonXScheduleType_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXWeeklySchedule.Checked)
				_localSchedule.SelectedSpotType = SpotType.Week;
			else if (buttonXMonthlySchedule.Checked)
				_localSchedule.SelectedSpotType = SpotType.Month;
			UpdateScheduleType(_localSchedule.SelectedSpotType);
			SettingsNotSaved = true;
		}
		#endregion

		#region Digital Product Events

		public void DigitalProductAdd(object sender, EventArgs e)
		{
			var category = (sender as ButtonItem).Tag as Category;
			digitalProductListControl.AddProduct(category);
		}

		public void DigitalProductClone(object sender, EventArgs e)
		{
			digitalProductListControl.CloneProduct();
		}
		#endregion

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}