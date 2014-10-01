using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.Core.QuickShare;
using NewBizWiz.QuickShare.Controls.BusinessClasses;
using NewBizWiz.QuickShare.Controls.PresentationClasses.ScheduleControls;
using NewBizWiz.QuickShare.Controls.Properties;

namespace NewBizWiz.QuickShare.Controls
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() { }
		public static Controller Instance
		{
			get { return _instance; }
		}

		public event EventHandler<EventArgs> PackageChanged;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;
		public event EventHandler<EventArgs> CloseRequested;

		public Form FormMain { get; set; }
		public Control PagesContainer { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabHome { get; set; }

		public void Init()
		{
			SetDefaultCulture();

			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Application Started"));

			#region Schedule Settings
			HomeControl = new HomeControl();
			HomeScheduleAdd.Click += HomeControl.Add_Click;
			HomeScheduleClone.Click += HomeControl.Clone_Click;
			HomeHelp.Click += HomeControl.Help_Click;
			HomeSave.Click += HomeControl.Save_Click;
			HomeSaveAs.Click += HomeControl.SaveAs_Click;
			HomeBusinessName.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeClientType.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberText.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberCheck.CheckedChanged += HomeControl.checkBoxItemAccountNumber_CheckedChanged;
			HomeBusinessName.Enter += Utilities.Instance.Editor_Enter;
			HomeBusinessName.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeBusinessName.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeDecisionMaker.Enter += Utilities.Instance.Editor_Enter;
			HomeDecisionMaker.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeDecisionMaker.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeBusinessName.TabIndex = 0;
			HomeBusinessName.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeDecisionMaker.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeClientType.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomePresentationDate.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			#endregion

			Ribbon_SelectedRibbonTabChanged(Ribbon, EventArgs.Empty);
			Ribbon.SelectedRibbonTabChanged -= Ribbon_SelectedRibbonTabChanged;
			Ribbon.SelectedRibbonTabChanged += Ribbon_SelectedRibbonTabChanged;
			Ribbon.SelectedRibbonTabChanged -= HomeControl.Ribbon_ScheduleTabChanged;
			Ribbon.SelectedRibbonTabChanged += HomeControl.Ribbon_ScheduleTabChanged;
		}

		public void RemoveInstance()
		{
			HomeControl.Dispose();
			FloaterRequested = null;
			SetDefaultCulture();
		}

		public void LoadData()
		{
			MediaMetaData.Instance.SettingsManager.InitThemeHelper(BusinessWrapper.Instance.ThemeManager);
			HomeControl.LoadPackage();
		}

		public void SavePackage(Package localPackage, bool nameChanged, bool quickSave, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessWrapper.Instance.PackageManager.SavePackage(localPackage, quickSave, sender));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			if (nameChanged)
			{
				var options = new Dictionary<string, object>();
				options.Add("Advertiser", localPackage.BusinessName);
				BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Saved As", localPackage.Name));
			}
			if (PackageChanged != null)
				PackageChanged(this, EventArgs.Empty);
		}

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow, Logo = Resources.RibbonLogo };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		public void CloseApp()
		{
			if (CloseRequested != null)
				CloseRequested(null, EventArgs.Empty);
		}

		private void Ribbon_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new TabActivity(Ribbon.SelectedRibbonTabItem.Text, BusinessWrapper.Instance.PackageManager.CurrentAdvertiser));
		}

		public void SetDefaultCulture()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
		}
		#region Command Controls

		#region Home
		public ButtonItem HomeScheduleAdd { get; set; }
		public ButtonItem HomeScheduleClone { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public ComboBoxEdit HomeClientType { get; set; }
		public TextEdit HomeAccountNumberText { get; set; }
		public CheckBoxItem HomeAccountNumberCheck { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		#endregion

		#endregion

		#region Forms
		public HomeControl HomeControl { get; private set; }
		#endregion
	}
}
