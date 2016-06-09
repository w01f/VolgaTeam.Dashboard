using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Media.Controls.BusinessClasses;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	[ToolboxItem(false)]
	//public partial class HomeControl : UserControl
	public partial class HomeControl : BaseScheduleSettingsEditControl<MediaScheduleSettings, MediaScheduleChangeInfo>
	{
		private bool _allowToSave;
		public DigitalProductsContent DigitalContent { get; set; }
		private MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;

		private MediaScheduleSettings OriginalSettings
		{
			get { return Schedule.Settings; }
			set { Schedule.Settings = value; }
		}

		public override string Identifier => ContentIdentifiers.ScheduleSettings;

		public override RibbonTabItem TabPage => Controller.Instance.TabHome;

		public HomeControl()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96) { }
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();


			stationsControl.Changed += (o, e) => { SettingsNotSaved = true; };
			daypartsControl.Changed += (o, e) => { SettingsNotSaved = true; };

			Controller.Instance.ContentController.RibbonTabsStateChanged += OnRibbonRibbonTabsStateChanged;

			Controller.Instance.HomeBusinessName.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeDecisionMaker.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeFlightDatesStart.Click += OnFlightDatesEditClick;
			Controller.Instance.HomeFlightDatesEnd.Click += OnFlightDatesEditClick;

			Controller.Instance.HomeBusinessName.EnableSelectAll();
			Controller.Instance.HomeDecisionMaker.EnableSelectAll();

			Controller.Instance.HomeBusinessName.TabIndex = 0;
			Controller.Instance.HomeBusinessName.KeyDown += OnSchedulePropertiesEditorKeyDown;
			Controller.Instance.HomeDecisionMaker.KeyDown += OnSchedulePropertiesEditorKeyDown;
			Controller.Instance.HomePresentationDate.KeyDown += OnSchedulePropertiesEditorKeyDown;
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;
			if (EditedSettings == null || ContentUpdateInfo.ChangeInfo.WholeScheduleChanged)
			{
				EditedSettings?.Dispose();
				EditedSettings = OriginalSettings.Clone<MediaScheduleSettings, MediaScheduleSettings>();
				SettingsNotSaved = EditedSettings.IsNew;

				#region Media Tab


				Controller.Instance.HomeBusinessName.EditValue = EditedSettings.BusinessName;
				Controller.Instance.HomeDecisionMaker.EditValue = EditedSettings.DecisionMaker;

				Controller.Instance.HomePresentationDate.EditValue = EditedSettings.PresentationDate;
				UpdateFlightDates();
				UpdateWeekCount();

				switch (EditedSettings.SelectedSpotType)
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
				comboBoxEditSource.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.SourcePrograms
					.SelectMany(sp => sp.Demos)
					.Select(d => d.Source)
					.Distinct()
					.ToArray());

				if (EditedSettings.UseDemo)
				{
					buttonXUseDemos.Checked = true;

					buttonXDemosCustom.Enabled = true;
					buttonXDemosCustom.Checked = !EditedSettings.ImportDemo;

					buttonXDemosImport.Enabled = true;
					buttonXDemosImport.Checked = EditedSettings.ImportDemo;

					if (EditedSettings.ImportDemo)
					{
						comboBoxEditSource.Enabled = true;
						comboBoxEditSource.EditValue = EditedSettings.Source;

						buttonXDemosRtg.Enabled = false;
						buttonXDemosRtg.Checked = false;

						buttonXDemosImps.Enabled = false;
						buttonXDemosImps.Checked = true;

						comboBoxEditDemos.Properties.Items.Clear();
						var demos = MediaMetaData.Instance.ListManager.SourcePrograms
							.SelectMany(sp => sp.Demos)
							.Where(d => d.Source == EditedSettings.Source);
						if (demos.Any())
						{
							comboBoxEditDemos.Enabled = true;
							comboBoxEditDemos.Properties.Items.AddRange(demos.GroupBy(d => d.DisplayString).Select(g => g.First()).ToArray());
							comboBoxEditDemos.EditValue = demos
								.FirstOrDefault(d =>
									d.DemoType == EditedSettings.DemoType &&
									d.Source == EditedSettings.Source &&
									d.Name == EditedSettings.Demo);
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
						buttonXDemosRtg.Checked = EditedSettings.DemoType == DemoType.Rtg;

						buttonXDemosImps.Enabled = true;
						buttonXDemosImps.Checked = EditedSettings.DemoType == DemoType.Imp;

						comboBoxEditDemos.Properties.Items.Clear();
						comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
						comboBoxEditDemos.EditValue = EditedSettings.Demo;
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

				buttonXCalendarTypeMondayBased.Checked = EditedSettings.MondayBased;
				buttonXCalendarTypeSundayBased.Checked = !EditedSettings.MondayBased;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = EditedSettings.StartDayOfWeek;

				stationsControl.LoadData(EditedSettings);
				daypartsControl.LoadData(EditedSettings);
				#endregion
			}

			OnProductsTabPageChanged(this, new TabPageChangedEventArgs(null, xtraTabControlMain.SelectedTabPage));

			UpdateScheduleControls();

			_allowToSave = true;
		}

		protected override void ApplyChanges()
		{
			EditedSettings.BusinessName = Controller.Instance.HomeBusinessName.EditValue as String;
			EditedSettings.DecisionMaker = Controller.Instance.HomeDecisionMaker.EditValue as String;
			EditedSettings.PresentationDate = (DateTime?)Controller.Instance.HomePresentationDate.EditValue;
			EditedSettings.UseDemo = buttonXUseDemos.Checked;
			EditedSettings.ImportDemo = buttonXDemosImport.Checked;
			EditedSettings.Source = comboBoxEditSource.EditValue as String;
			if (buttonXDemosImport.Checked)
			{
				var demo = comboBoxEditDemos.EditValue as Demo;
				EditedSettings.Demo = demo != null ? demo.Name : null;
				EditedSettings.DemoType = demo != null ? demo.DemoType : DemoType.Rtg;
			}
			else
			{
				if (buttonXDemosRtg.Checked)
					EditedSettings.DemoType = DemoType.Rtg;
				else if (buttonXDemosImps.Checked)
					EditedSettings.DemoType = DemoType.Imp;
				EditedSettings.Demo = comboBoxEditDemos.EditValue as String;
			}

			if (stationsControl.HasChanged)
			{
				EditedSettings.Stations.Clear();
				EditedSettings.Stations.AddRange(stationsControl.GetData());
				stationsControl.HasChanged = false;
			}

			if (daypartsControl.HasChanged)
			{
				EditedSettings.Dayparts.Clear();
				EditedSettings.Dayparts.AddRange(daypartsControl.GetData());
				daypartsControl.HasChanged = false;
			}

			UpdateScheduleControls();

			OriginalSettings.CompareChanges(EditedSettings, ChangeInfo);
		}

		protected override void ValidateChanges(ContentSavingEventArgs savingArgs)
		{
			if (String.IsNullOrEmpty(EditedSettings.BusinessName))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return;
			}
			if (String.IsNullOrEmpty(EditedSettings.DecisionMaker))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return;
			}
			if (!EditedSettings.PresentationDate.HasValue)
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
				return;
			}
			if (!EditedSettings.UserFlightDateStart.HasValue || !EditedSettings.UserFlightDateEnd.HasValue)
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Flight Dates before you proceed.");
				return;
			}
			if (ChangeInfo.ScheduleDatesChanged)
			{
				if (PopupMessageHelper.Instance.ShowWarningQuestion("Flight Dates have been changed and all Spots will be recreated\nDo you want to proceed?") != DialogResult.Yes)
					savingArgs.Cancel = true;
			}
		}

		protected override void SaveData()
		{
			OriginalSettings = EditedSettings.Clone<MediaScheduleSettings, MediaScheduleSettings>();
			OriginalSettings.UpdateDictionaries();

			Schedule.ApplySettingsChanges(ChangeInfo);

			if (ChangeInfo.DigitalContentChanged)
				Schedule.DigitalProductsContent = DigitalContent.Clone<DigitalProductsContent, DigitalProductsContent>();
		}

		public override void GetHelp()
		{
			if (xtraTabControlMain.SelectedTabPage == xtraTabPageSchedule)
				BusinessObjects.Instance.HelpManager.OpenHelpLink(
					String.Format("home{0}", MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? "tv" : "rd"));
		}
		#endregion

		#region Common Methods
		private void UpdateScheduleControls()
		{
			var enableSchedules = !String.IsNullOrEmpty(EditedSettings.BusinessName) &
								   !String.IsNullOrEmpty(EditedSettings.DecisionMaker) &
								   EditedSettings.PresentationDate.HasValue &
								   EditedSettings.UserFlightDateStart.HasValue &
								   EditedSettings.UserFlightDateEnd.HasValue;
			Controller.Instance.ContentController.UpdateTabsSate();
			buttonXWeeklySchedule.Enabled = enableSchedules;
			buttonXMonthlySchedule.Enabled = enableSchedules;
			if (enableSchedules)
				pnSchedule.BringToFront();
			else
				pnMediaDefault.BringToFront();
		}

		private void UpdateFlightDates()
		{
			Controller.Instance.HomeFlightDatesStart.Text = EditedSettings.UserFlightDateStart?.ToString("M/d/yy  ") ?? "Select  ";
			Controller.Instance.HomeFlightDatesEnd.Text = EditedSettings.UserFlightDateEnd?.ToString("M/d/yy  ") ?? "Select  ";
			Controller.Instance.HomeFlightDates.RecalcLayout();
			Controller.Instance.HomePanel.PerformLayout();
		}

		private void UpdateWeekCount()
		{
			var weeksCount = MediaScheduleSettings.CalcWeeksCount(
				EditedSettings.UserFlightDateStart,
				EditedSettings.UserFlightDateEnd,
				EditedSettings.StartDayOfWeek,
				EditedSettings.EndDayOfWeek
				);
			Controller.Instance.HomeFlightDates.Text = "Weeks";
			if (!weeksCount.HasValue) return;
			Controller.Instance.HomeFlightDates.Text = String.Format("Weeks ({0})", weeksCount);
			Controller.Instance.HomeFlightDates.RecalcLayout();
			Controller.Instance.HomePanel.PerformLayout();
		}

		private void OnProductsTabPageChanged(object sender, TabPageChangedEventArgs e)
		{
			xtraTabControlScheduleOptions.Visible = e.Page == xtraTabPageSchedule;
			xtraTabControlSolutionOptions.Visible = e.Page == xtraTabPageSolution;
		}

		private void OnRibbonRibbonTabsStateChanged(object sender, EventArgs e)
		{
			buttonXSnapshot.Enabled = Controller.Instance.TabSnapshot.Visible && Controller.Instance.TabSnapshot.Enabled;
			buttonXOptions.Enabled = Controller.Instance.TabOptions.Visible && Controller.Instance.TabOptions.Enabled;
			buttonXCalendar.Enabled = Controller.Instance.TabCalendar2.Visible && Controller.Instance.TabCalendar2.Enabled;
		}

		private void OnDefaultPanelResize(object sender, EventArgs e)
		{
			if (pnMediaDefault.Width > pbMediaDefault.Image.Width)
			{
				pbMediaDefault.SizeMode = PictureBoxSizeMode.Normal;
				pbMediaDefault.Dock = DockStyle.Fill;
			}
			else
			{
				pbMediaDefault.SizeMode = PictureBoxSizeMode.Zoom;
				pbMediaDefault.Dock = DockStyle.None;
				pbMediaDefault.Top = 0;
				pbMediaDefault.Left = 0;
				pbMediaDefault.Width = pnMediaDefault.Width;
				pbMediaDefault.Height = (Int32)(pbMediaDefault.Image.Height * ((decimal)pbMediaDefault.Width / pbMediaDefault.Image.Width));
			}
		}
		#endregion

		#region Editors Events

		public void OnSchedulePropertyValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			ApplyChanges();
			SettingsNotSaved = true;
		}

		private void OnFlightDatesEditClick(Object sender, EventArgs e)
		{
			using (var form = new FormFlightDatesEdit())
			{
				form.DateStart = EditedSettings.UserFlightDateStart;
				form.DateEnd = EditedSettings.UserFlightDateEnd;
				form.EndDayOfWeek = EditedSettings.EndDayOfWeek;
				form.StartDayOfWeek = EditedSettings.StartDayOfWeek;
				form.EndDayOfWeek = EditedSettings.EndDayOfWeek;
				if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
				{
					EditedSettings.UserFlightDateStart = form.DateStart;
					EditedSettings.UserFlightDateEnd = form.DateEnd;
					UpdateFlightDates();
					UpdateWeekCount();
					OnSchedulePropertyValueChanged(sender, e);
				}
			}
		}

		public void OnSchedulePropertiesEditorKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Tab) return;
			if (sender == Controller.Instance.HomeBusinessName)
				Controller.Instance.HomeDecisionMaker.Focus();
			else if (sender == Controller.Instance.HomeDecisionMaker)
				Controller.Instance.HomePresentationDate.Focus();
			else if (sender == Controller.Instance.HomePresentationDate)
				Controller.Instance.HomeBusinessName.Focus();
			e.Handled = true;
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
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Your current schedule will be reset.{0}Do you want to continue and change calendar type?", Environment.NewLine)) != DialogResult.Yes) return;
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
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = EditedSettings.StartDayOfWeek;
			EditedSettings.UserFlightDateStart = null;
			EditedSettings.UserFlightDateEnd = null;
			UpdateFlightDates();
			UpdateWeekCount();
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
				EditedSettings.SelectedSpotType = SpotType.Week;
			else if (buttonXMonthlySchedule.Checked)
				EditedSettings.SelectedSpotType = SpotType.Month;
			SettingsNotSaved = true;
		}

		private void buttonXSnapshot_Click(object sender, EventArgs e)
		{
			Controller.Instance.TabSnapshot.Select();
		}

		private void buttonXOptions_Click(object sender, EventArgs e)
		{
			Controller.Instance.TabOptions.Select();
		}

		private void buttonXCalendar_Click(object sender, EventArgs e)
		{
			Controller.Instance.TabCalendar2.Select();
		}
		#endregion

		#region Digital Product Events
		
		#endregion
	}
}