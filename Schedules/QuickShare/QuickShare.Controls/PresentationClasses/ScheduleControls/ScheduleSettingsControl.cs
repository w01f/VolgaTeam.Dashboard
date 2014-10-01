using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.Core.QuickShare;

namespace NewBizWiz.QuickShare.Controls.PresentationClasses.ScheduleControls
{
	[ToolboxItem(false)]
	public partial class ScheduleSettingsControl : XtraTabPage
	//public partial class ScheduleSettingsControl : UserControl
	{
		private bool _allowToSave;
		public PackageSchedule Schedule { get; private set; }

		public EventHandler<EventArgs> Changed;

		public ScheduleSettingsControl(PackageSchedule schedule)
		{
			InitializeComponent();
			Schedule = schedule;

			comboBoxEditSource.Properties.Items.Clear();
			comboBoxEditSource.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos).Select(d => d.Source).Distinct().ToArray());

			stationsControl.Changed += (o, e) => { if (Changed != null) Changed(o, e); };
			daypartsControl.Changed += (o, e) => { if (Changed != null) Changed(o, e); };
		}

		public void UpdateData(bool fullUpdate = false)
		{
			_allowToSave = false;
			Text = String.Format("Spec {0}", Schedule.DisplayedIndex);
			PageEnabled = Schedule.IsConfigured;
			UpdateFlexFlightDatesWarning();

			if (fullUpdate)
			{
				#region Demo settings
				var importedDemos = MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos);
				if (!importedDemos.Any())
				{
					buttonXUseDemos.Text = "Show Demo Estimates";
					pnDemosCustom.Visible = false;
					pnDemosImport.Visible = false;
					pnSelectSource.Visible = false;
				}

				if (Schedule.UseDemo)
				{
					buttonXUseDemos.Checked = true;

					buttonXDemosCustom.Enabled = true;
					buttonXDemosCustom.Checked = !Schedule.ImportDemo;

					buttonXDemosImport.Enabled = true;
					buttonXDemosImport.Checked = Schedule.ImportDemo;

					if (Schedule.ImportDemo)
					{
						comboBoxEditSource.Enabled = true;
						comboBoxEditSource.EditValue = Schedule.Source;

						buttonXDemosRtg.Enabled = false;
						buttonXDemosRtg.Checked = false;

						buttonXDemosImps.Enabled = false;
						buttonXDemosImps.Checked = true;

						comboBoxEditDemos.Properties.Items.Clear();
						var demos = MediaMetaData.Instance.ListManager.SourcePrograms.SelectMany(sp => sp.Demos).Where(d => d.Source == Schedule.Source);
						if (demos.Any())
						{
							comboBoxEditDemos.Enabled = true;
							comboBoxEditDemos.Properties.Items.AddRange(demos.GroupBy(d => d.DisplayString).Select(g => g.First()).ToArray());
							comboBoxEditDemos.EditValue = demos.FirstOrDefault(d => d.DemoType == Schedule.DemoType && d.Source == Schedule.Source && d.Name == Schedule.Demo);
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
						buttonXDemosRtg.Checked = Schedule.DemoType == DemoType.Rtg;

						buttonXDemosImps.Enabled = true;
						buttonXDemosImps.Checked = Schedule.DemoType == DemoType.Imp;

						comboBoxEditDemos.Properties.Items.Clear();
						comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
						comboBoxEditDemos.EditValue = Schedule.Demo;
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
				#endregion

				stationsControl.LoadData(Schedule);
				daypartsControl.LoadData(Schedule);
			}
			_allowToSave = true;
		}

		public void SaveData()
		{
			Schedule.UseDemo = buttonXUseDemos.Checked;
			Schedule.ImportDemo = buttonXDemosImport.Checked;
			Schedule.Source = comboBoxEditSource.EditValue as String;
			if (buttonXDemosImport.Checked)
			{
				var demo = comboBoxEditDemos.EditValue as Demo;
				Schedule.Demo = demo != null ? demo.Name : null;
				Schedule.DemoType = demo != null ? demo.DemoType : DemoType.Rtg;
			}
			else
			{
				if (buttonXDemosRtg.Checked)
					Schedule.DemoType = DemoType.Rtg;
				else if (buttonXDemosImps.Checked)
					Schedule.DemoType = DemoType.Imp;
				Schedule.Demo = comboBoxEditDemos.EditValue as String;
			}

			Schedule.Section.ShowRating = Schedule.Section.ShowRating & Schedule.UseDemo & !String.IsNullOrEmpty(Schedule.Demo);
			Schedule.Section.ShowCPP = Schedule.Section.ShowCPP & Schedule.UseDemo & !String.IsNullOrEmpty(Schedule.Demo);
			Schedule.Section.ShowGRP = Schedule.Section.ShowGRP & Schedule.UseDemo & !String.IsNullOrEmpty(Schedule.Demo);
			Schedule.Section.ShowTotalCPP = Schedule.Section.ShowTotalCPP & Schedule.UseDemo & !String.IsNullOrEmpty(Schedule.Demo);
			Schedule.Section.ShowTotalGRP = Schedule.Section.ShowTotalGRP & Schedule.UseDemo & !String.IsNullOrEmpty(Schedule.Demo);

			if (stationsControl.HasChanged)
			{
				Schedule.Stations.Clear();
				Schedule.Stations.AddRange(stationsControl.GetData());
			}

			if (daypartsControl.HasChanged)
			{
				Schedule.Dayparts.Clear();
				Schedule.Dayparts.AddRange(daypartsControl.GetData());
			}
		}

		private void UpdateFlexFlightDatesWarning()
		{
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				var warningText = new List<string>();
				if (Schedule.UserFlightDateStart.HasValue)
				{
					var startDate = Schedule.UserFlightDateStart.Value;
					if (startDate.DayOfWeek != Schedule.StartDayOfWeek)
					{
						var weekEnd = startDate;
						while (weekEnd.DayOfWeek != Schedule.EndDayOfWeek)
							weekEnd = weekEnd.AddDays(1);
						warningText.Add(String.Format("*The FIRST WEEK of your schedule STARTS on a {0}.{1}({2} - {3})", startDate.DayOfWeek, Environment.NewLine, startDate.ToString("M/d/yy"), weekEnd.ToString("M/d/yy")));
					}
				}
				if (Schedule.UserFlightDateEnd.HasValue)
				{
					var endDate = Schedule.UserFlightDateEnd.Value;
					if (endDate.DayOfWeek != Schedule.EndDayOfWeek)
					{
						var weekStart = endDate;
						while (weekStart.DayOfWeek != Schedule.StartDayOfWeek)
							weekStart = weekStart.AddDays(-1);
						warningText.Add(String.Format("*The LAST WEEK of your schedule ENDS on a {0}.{1}({2} - {3})", endDate.DayOfWeek, Environment.NewLine, weekStart.ToString("M/d/yy"), endDate.ToString("M/d/yy")));
					}
				}
				laFlexDateWarning.Text = String.Join(String.Format("{0}{0}", Environment.NewLine), warningText);
			}
			else
				laFlexDateWarning.Text = String.Empty;
		}

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
			if (Changed != null) Changed(this, EventArgs.Empty);
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
			if (Changed != null) Changed(this, EventArgs.Empty);
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
			if (Changed != null) Changed(this, EventArgs.Empty);
		}

		private void buttonXDemosType_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (Changed != null) Changed(this, EventArgs.Empty);
		}

		private void comboBoxEditDemos_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			if (Changed != null) Changed(this, EventArgs.Empty);
		}
		#endregion
	}
}
