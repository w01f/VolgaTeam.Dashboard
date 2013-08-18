﻿using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;

namespace NewBizWiz.OnlineSchedule.Controls
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() {}
		public static Controller Instance
		{
			get { return _instance; }
		}

		public Form FormMain { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabScheduleSlides { get; set; }
		public RibbonTabItem TabDigitalPackage { get; set; }

		public void Init()
		{
			#region Schedule Settings
			ScheduleSettings = new ScheduleSettingsControl();
			HomeHelp.Click += ScheduleSettings.ScheduleSettingsHelp_Click;
			HomeSave.Click += ScheduleSettings.ScheduleSettingsSave_Click;
			HomeSaveAs.Click += ScheduleSettings.ScheduleSettingsSaveAs_Click;
			HomeBusinessName.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeClientType.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeAccountNumberText.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeAccountNumberCheck.CheckedChanged += ScheduleSettings.checkBoxItemAccountNumber_CheckedChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.FlightDateStartEditValueChanged;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesStart.CloseUp += ScheduleSettings.dateEditFlightDatesStart_CloseUp;
			HomeFlightDatesEnd.CloseUp += ScheduleSettings.dateEditFlightDatesEnd_CloseUp;
			HomeProductDelete.Click += ScheduleSettings.DigitalProductDelete;
			HomeBusinessName.Enter += Utilities.Instance.Editor_Enter;
			HomeBusinessName.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeBusinessName.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeDecisionMaker.Enter += Utilities.Instance.Editor_Enter;
			HomeDecisionMaker.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeDecisionMaker.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Schedule Slides
			ScheduleSlides = new ScheduleSlidesControl(FormMain);
			DigitalSlidesSave.Click += ScheduleSlides.Save_Click;
			DigitalSlidesSaveAs.Click += ScheduleSlides.SaveAs_Click;
			DigitalSlidesPowerPoint.Click += ScheduleSlides.PowerPoint_Click;
			DigitalSlidesPreview.Click += ScheduleSlides.Preview_Click;
			DigitalSlidesEmail.Click += ScheduleSlides.Email_Click;
			DigitalSlidesHelp.Click += ScheduleSlides.Help_Click;
			DigitalSlidesOptions.CheckedChanged += ScheduleSlides.Options_Click; 
			#endregion

			#region Web Package
			DigitalPackage = new OnlineWebPackageControl(FormMain);
			DigitalPackageSave.Click += DigitalPackage.Save_Click;
			DigitalPackageSaveAs.Click += DigitalPackage.SaveAs_Click;
			DigitalPackagePowerPoint.Click += DigitalPackage.PowerPoint_Click;
			DigitalPackagePreview.Click += DigitalPackage.Preview_Click;
			DigitalPackageEmail.Click += DigitalPackage.Email_Click;
			DigitalPackageHelp.Click += DigitalPackage.Help_Click;
			DigitalPackageOptions.CheckedChanged += DigitalPackage.TogledButton_CheckedChanged; 
			#endregion
		}

		public void RemoveInstance()
		{
			ScheduleSettings.Dispose();
			ScheduleSlides.Dispose();
			DigitalPackage.Dispose();
		}

		public void LoadData()
		{
			ScheduleSettings.LoadSchedule(false);
			ScheduleSlides.LoadSchedule(false);
			DigitalPackage.LoadSchedule(false);
		}

		public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(delegate() { BusinessWrapper.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, sender); });
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
		}

		public void UpdateSimpleOutputTabPageState(bool enable)
		{
			TabScheduleSlides.Enabled = enable;
		}

		#region Command Controls

		#region Home
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public ButtonItem HomeProductAdd { get; set; }
		public ButtonItem HomeProductClone { get; set; }
		public ButtonItem HomeProductDelete { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public ComboBoxEdit HomeClientType { get; set; }
		public TextEdit HomeAccountNumberText { get; set; }
		public CheckBoxItem HomeAccountNumberCheck { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public DateEdit HomeFlightDatesStart { get; set; }
		public DateEdit HomeFlightDatesEnd { get; set; }
		public LabelItem HomeWeeks { get; set; }
		#endregion

		#region Schedule Slides
		public ButtonItem DigitalSlidesHelp { get; set; }
		public ButtonItem DigitalSlidesSave { get; set; }
		public ButtonItem DigitalSlidesSaveAs { get; set; }
		public ButtonItem DigitalSlidesOptions { get; set; }
		public ButtonItem DigitalSlidesPreview { get; set; }
		public ButtonItem DigitalSlidesEmail { get; set; }
		public ButtonItem DigitalSlidesPowerPoint { get; set; }
		#endregion

		#region Web Package
		public ButtonItem DigitalPackageHelp { get; set; }
		public ButtonItem DigitalPackageSave { get; set; }
		public ButtonItem DigitalPackageSaveAs { get; set; }
		public ButtonItem DigitalPackagePreview { get; set; }
		public ButtonItem DigitalPackageEmail { get; set; }
		public ButtonItem DigitalPackagePowerPoint { get; set; }
		public ButtonItem DigitalPackageOptions { get; set; }
		#endregion
		#endregion

		#region Forms
		public ScheduleSettingsControl ScheduleSettings { get; private set; }
		public ScheduleSlidesControl ScheduleSlides { get; private set; }
		public OnlineWebPackageControl DigitalPackage { get; private set; }
		#endregion
	}
}