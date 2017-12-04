using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

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

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			layoutControl.BeginInit();
			layoutControl.SuspendLayout();

			layoutControlItemMainScheduleTypeWeekly.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleTypeWeekly.MaxSize, scaleFactor);
			layoutControlItemMainScheduleTypeWeekly.MinSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleTypeWeekly.MinSize, scaleFactor);
			layoutControlItemMainScheduleTypeMonthly.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleTypeMonthly.MaxSize, scaleFactor);
			layoutControlItemMainScheduleTypeMonthly.MinSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleTypeMonthly.MinSize, scaleFactor);
			layoutControlItemMainScheduleSnapshot.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleSnapshot.MaxSize, scaleFactor);
			layoutControlItemMainScheduleSnapshot.MinSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleSnapshot.MinSize, scaleFactor);
			layoutControlItemMainScheduleOptions.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleOptions.MaxSize, scaleFactor);
			layoutControlItemMainScheduleOptions.MinSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleOptions.MinSize, scaleFactor);
			layoutControlItemMainScheduleCalendar.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleCalendar.MaxSize, scaleFactor);
			layoutControlItemMainScheduleCalendar.MinSize = RectangleHelper.ScaleSize(layoutControlItemMainScheduleCalendar.MinSize, scaleFactor);
			emptySpaceItemDaypartsSeparator.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDaypartsSeparator.MaxSize, scaleFactor);
			emptySpaceItemDaypartsSeparator.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDaypartsSeparator.MinSize, scaleFactor);

			layoutControlItemDemosDisabled.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDemosDisabled.MaxSize, scaleFactor);
			layoutControlItemDemosDisabled.MinSize = RectangleHelper.ScaleSize(layoutControlItemDemosDisabled.MinSize, scaleFactor);
			layoutControlItemDemosRtg.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDemosRtg.MaxSize, scaleFactor);
			layoutControlItemDemosRtg.MinSize = RectangleHelper.ScaleSize(layoutControlItemDemosRtg.MinSize, scaleFactor);
			layoutControlItemDemosImps.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDemosImps.MaxSize, scaleFactor);
			layoutControlItemDemosImps.MinSize = RectangleHelper.ScaleSize(layoutControlItemDemosImps.MinSize, scaleFactor);
			layoutControlItemDemosItems.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDemosItems.MaxSize, scaleFactor);
			layoutControlItemDemosItems.MinSize = RectangleHelper.ScaleSize(layoutControlItemDemosItems.MinSize, scaleFactor);
			simpleLabelItemDemosDescription.Size = RectangleHelper.ScaleSize(simpleLabelItemDemosDescription.Size, scaleFactor);
			emptySpaceItemDemosSeparator.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemDemosSeparator.MaxSize, scaleFactor);
			emptySpaceItemDemosSeparator.MinSize = RectangleHelper.ScaleSize(emptySpaceItemDemosSeparator.MinSize, scaleFactor);

			layoutControlItemCalendarTypeMonday.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCalendarTypeMonday.MaxSize, scaleFactor);
			layoutControlItemCalendarTypeMonday.MinSize = RectangleHelper.ScaleSize(layoutControlItemCalendarTypeMonday.MinSize, scaleFactor);
			layoutControlItemCalendarTypeSunday.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCalendarTypeSunday.MaxSize, scaleFactor);
			layoutControlItemCalendarTypeSunday.MinSize = RectangleHelper.ScaleSize(layoutControlItemCalendarTypeSunday.MinSize, scaleFactor);

			layoutControlItemScheduleOptionsStations.MinSize = RectangleHelper.ScaleSize(layoutControlItemScheduleOptionsStations.MinSize, scaleFactor);

			layoutControl.ResumeLayout(true);
			layoutControl.EndInit();
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			pictureEditDefaultTitle.Image = BusinessObjects.Instance.ImageResourcesManager.HomeDefaultLogo ?? pictureEditDefaultTitle.Image;
			buttonXWeeklySchedule.Image = BusinessObjects.Instance.ImageResourcesManager.HomeWeeklyScheduleImage ?? buttonXWeeklySchedule.Image;
			buttonXMonthlySchedule.Image = BusinessObjects.Instance.ImageResourcesManager.HomeMonthlyScheduleImage ?? buttonXMonthlySchedule.Image;
			buttonXSnapshot.Image = BusinessObjects.Instance.ImageResourcesManager.HomeSnaphotShortcutImage ?? buttonXSnapshot.Image;
			buttonXOptions.Image = BusinessObjects.Instance.ImageResourcesManager.HomeOptionsShortcutImage ?? buttonXOptions.Image;
			buttonXCalendar.Image = BusinessObjects.Instance.ImageResourcesManager.HomeCalendarShortcutImage ?? buttonXCalendar.Image;

			stationsControl.Changed += (o, e) => { SettingsNotSaved = true; };

			Controller.Instance.ContentController.RibbonTabsStateChanged += OnRibbonRibbonTabsStateChanged;

			Controller.Instance.HomeBusinessName.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeDecisionMaker.EditValueChanged += OnSchedulePropertyValueChanged;
			Controller.Instance.HomeFlightDatesStartTitle.Click += OnFlightDatesEditClick;
			Controller.Instance.HomeFlightDatesStartValue.Click += OnFlightDatesEditClick;
			Controller.Instance.HomeFlightDatesEndTitle.Click += OnFlightDatesEditClick;
			Controller.Instance.HomeFlightDatesEndValue.Click += OnFlightDatesEditClick;

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

				LoadDayparts();
				LoadDemos();
				LoadCalendarType();

				stationsControl.LoadData(EditedSettings);
				#endregion
			}

			UpdateScheduleControls();

			_allowToSave = true;
		}

		public override void ShowControl(ContentOpenEventArgs args = null)
		{
			Controller.Instance.MenuOutputPdfButton.Enabled = Controller.Instance.MenuEmailButton.Enabled = false;
			base.ShowControl(args);
			layoutControlGroupDemosValues.Invalidate();
			layoutControl.Refresh();
			layoutControl.Update();
		}

		protected override void ApplyChanges()
		{
			EditedSettings.BusinessName = Controller.Instance.HomeBusinessName.EditValue as String;
			EditedSettings.DecisionMaker = Controller.Instance.HomeDecisionMaker.EditValue as String;
			EditedSettings.PresentationDate = (DateTime?)Controller.Instance.HomePresentationDate.EditValue;
			if (checkEditDemosDisabled.Checked)
			{
				EditedSettings.UseDemo = false;
				EditedSettings.Demo = null;
			}
			else
			{
				EditedSettings.UseDemo = true;
				if (checkEditDemosRtg.Checked)
					EditedSettings.DemoType = DemoType.Rtg;
				else if (checkEditDemosImps.Checked)
					EditedSettings.DemoType = DemoType.Imp;
				EditedSettings.Demo = comboBoxEditDemos.EditValue as String;
			}

			if (stationsControl.HasChanged)
			{
				EditedSettings.Stations.Clear();
				EditedSettings.Stations.AddRange(stationsControl.GetData());
				stationsControl.HasChanged = false;
			}

			UpdateScheduleControls();

			ChangeInfo.Merge(OriginalSettings.GetChangeInfo(EditedSettings));
		}

		protected override void ValidateChanges(ContentSavingEventArgs savingArgs)
		{
			if (EditedSettings.EditMode != ScheduleEditMode.Regular) return;

			if (String.IsNullOrEmpty(EditedSettings.BusinessName))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add(
					"Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return;
			}
			if (String.IsNullOrEmpty(EditedSettings.DecisionMaker))
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add(
					"Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return;
			}
			if (!EditedSettings.PresentationDate.HasValue)
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add(
					"Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
				return;
			}
			if (!EditedSettings.UserFlightDateStart.HasValue || !EditedSettings.UserFlightDateEnd.HasValue)
			{
				savingArgs.Cancel = true;
				savingArgs.ErrorMessages.Add("Your schedule is missing important information!\nPlease make sure you have a Flight Dates before you proceed.");
				return;
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
			if (tabbedControlGroupMain.SelectedTabPage == layoutControlGroupMainSchedule)
				BusinessObjects.Instance.HelpManager.OpenHelpLink(
					String.Format("home{0}", MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? "tv" : "rd"));
		}
		#endregion

		#region Common Methods
		private void UpdateScheduleControls()
		{
			var enableSchedules = EditedSettings.EditMode == ScheduleEditMode.Quick || (!String.IsNullOrEmpty(EditedSettings.BusinessName) &&
				!String.IsNullOrEmpty(EditedSettings.DecisionMaker) &&
				EditedSettings.PresentationDate.HasValue &&
				EditedSettings.UserFlightDateStart.HasValue &&
				EditedSettings.UserFlightDateEnd.HasValue);
			Controller.Instance.ContentController.UpdateTabsSate();
			buttonXWeeklySchedule.Enabled = enableSchedules;
			buttonXMonthlySchedule.Enabled = enableSchedules;
			if (enableSchedules)
			{
				layoutControlItemMainScheduleDefaultTitle.Visibility = LayoutVisibility.Never;
				tabbedControlGroupMain.Visibility = LayoutVisibility.Always;
				splitterItemMain.Visibility = LayoutVisibility.Always;
				tabbedControlGroupScheduleOptions.Visibility = LayoutVisibility.Always;
			}
			else
			{
				tabbedControlGroupMain.Visibility = LayoutVisibility.Never;
				splitterItemMain.Visibility = LayoutVisibility.Never;
				tabbedControlGroupScheduleOptions.Visibility = LayoutVisibility.Never;
				layoutControlItemMainScheduleDefaultTitle.Visibility = LayoutVisibility.Always;
			}
		}

		private void UpdateFlightDates()
		{
			Controller.Instance.HomeFlightDatesStartValue.Text = EditedSettings.UserFlightDateStart?.ToString("M/d/yy  ") ?? "Select  ";
			Controller.Instance.HomeFlightDatesEndValue.Text = EditedSettings.UserFlightDateEnd?.ToString("M/d/yy  ") ?? "Select  ";
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

		private void OnRibbonRibbonTabsStateChanged(object sender, EventArgs e)
		{
			buttonXSnapshot.Enabled = Controller.Instance.TabSnapshot.Visible && Controller.Instance.TabSnapshot.Enabled;
			buttonXOptions.Enabled = Controller.Instance.TabOptions.Visible && Controller.Instance.TabOptions.Enabled;
			buttonXCalendar.Enabled = Controller.Instance.TabCalendar2.Visible && Controller.Instance.TabCalendar2.Enabled;
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
				var currentDateStart = EditedSettings.UserFlightDateStart;
				var currentDateEnd = EditedSettings.UserFlightDateEnd;
				form.DateStart = currentDateStart;
				form.DateEnd = currentDateEnd;
				form.EndDayOfWeek = EditedSettings.EndDayOfWeek;
				form.StartDayOfWeek = EditedSettings.StartDayOfWeek;
				form.EndDayOfWeek = EditedSettings.EndDayOfWeek;
				if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
				{
					var comparableSettings = EditedSettings.Clone<MediaScheduleSettings, MediaScheduleSettings>();
					comparableSettings.UserFlightDateStart = form.DateStart;
					comparableSettings.UserFlightDateEnd = form.DateEnd;
					var changeInfo = EditedSettings.GetChangeInfo(comparableSettings);
					if (changeInfo.ScheduleDatesChanged)
					{
						if (Schedule.ProgramSchedule.Sections.Any(s => s.Programs.Any(p => p.TotalSpots > 0)))
						{
							using (var formWarning = new FormFlightDatesChangeWarning(EditedSettings.SelectedSpotType))
							{
								if (formWarning.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK)
								{
									comparableSettings.Dispose();
									return;
								}
								ChangeInfo.KeepSpotsWhenDatesChanged = formWarning.KeepSpots;
							}
						}
						EditedSettings.UserFlightDateStart = form.DateStart;
						EditedSettings.UserFlightDateEnd = form.DateEnd;
						OnSchedulePropertyValueChanged(sender, e);
						UpdateFlightDates();
						UpdateWeekCount();
					}
					comparableSettings.Dispose();
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

		#region Dayparts processing
		private void LoadDayparts()
		{
			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			var layoutItems = new List<BaseLayoutItem>();
			foreach (var daypart in EditedSettings.Dayparts)
			{
				var control = new CheckEdit();
				control.Properties.AllowFocused = false;
				control.Properties.AutoWidth = true;
				control.Properties.Caption = daypart.Name;
				control.StyleController = this.layoutControl;
				control.Tag = daypart;
				control.Checked = daypart.Available;
				control.CheckedChanged += OnDaypartStateChanged;
				layoutControl.Controls.Add(control);

				var layoutItem = new LayoutControlItem();
				layoutItem.Control = control;
				layoutItem.FillControlToClientArea = false;
				layoutItem.TextVisible = false;
				layoutItem.TrimClientAreaToControl = false;
				layoutItem.ControlAlignment = ContentAlignment.MiddleLeft;
				layoutItem.SizeConstraintsType = SizeConstraintsType.Custom;
				layoutItem.MinSize = new Size(control.Width + (Int32)(30 * scaleFactor.Width), (Int32)(30 * scaleFactor.Width));
				layoutItems.Add(layoutItem);
			}
			layoutControlGroupDaypartValues.Items.AddRange(layoutItems.ToArray());
		}

		private void OnDaypartStateChanged(Object sender, EventArgs e)
		{
			var control = (CheckEdit)sender;
			var daypart = (Daypart)control.Tag;
			daypart.Available = control.Checked;
			SettingsNotSaved = true;
		}
		#endregion

		#region Demos Processing
		private void LoadDemos()
		{
			comboBoxEditDemos.Properties.Items.Clear();
			comboBoxEditDemos.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.CustomDemos);
			comboBoxEditDemos.EditValue = MediaMetaData.Instance.ListManager.CustomDemos.FirstOrDefault();

			if (EditedSettings.UseDemo)
			{
				if (EditedSettings.DemoType == DemoType.Rtg)
				{
					checkEditDemosDisabled.Checked = false;
					checkEditDemosRtg.Checked = true;
					checkEditDemosImps.Checked = false;
				}
				else if (EditedSettings.DemoType == DemoType.Imp)
				{
					checkEditDemosDisabled.Checked = false;
					checkEditDemosRtg.Checked = false;
					checkEditDemosImps.Checked = true;
				}
				comboBoxEditDemos.EditValue = EditedSettings.Demo ?? MediaMetaData.Instance.ListManager.CustomDemos.FirstOrDefault();
			}
			else
			{
				checkEditDemosDisabled.Checked = true;
				checkEditDemosRtg.Checked = false;
				checkEditDemosImps.Checked = false;
			}
		}

		private void OnDemoTypeCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemDemosItems.Visibility = !checkEditDemosDisabled.Checked
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}

		private void OnDemoValueEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
		}
		#endregion

		#region Calendar processing
		private void LoadCalendarType()
		{
			checkEditCalendarTypeMonday.Checked = EditedSettings.MondayBased;
			checkEditCalendarTypeSunday.Checked = !EditedSettings.MondayBased;
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = EditedSettings.StartDayOfWeek;
		}

		private void OnCalendarTypeEditValueChanging(object sender, ChangingEventArgs e)
		{
			if (!_allowToSave) return;
			if ((bool)e.NewValue != true) return;
			e.Cancel = PopupMessageHelper.Instance.ShowWarningQuestion(
						   String.Format("Your current schedule will be reset.{0}Do you want to continue and change calendar type?",
							   Environment.NewLine)) != DialogResult.Yes;
		}

		private void OnCalendarTypeCheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			EditedSettings.MondayBased = checkEditCalendarTypeMonday.Checked;
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