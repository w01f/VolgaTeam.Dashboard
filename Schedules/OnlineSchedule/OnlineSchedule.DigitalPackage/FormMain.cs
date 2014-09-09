using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses;

namespace NewBizWiz.OnlineSchedule.DigitalPackage
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;
		private Control _currentControl;

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		public static void RemoveInstance()
		{
			_instance.Dispose();
			_instance = null;
		}

		public FormMain()
		{
			InitializeComponent();

			FormStateHelper.Init(this, Core.Common.SettingsManager.Instance.SettingsPath, "WebQuick", true);

			Controller.Instance.FormMain = this;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabDigitalPackage = ribbonTabItemDigitalPackage;
			Controller.Instance.TabRateCard = ribbonTabItemRateCard;

			#region Command Controls

			#region Web Package
			Controller.Instance.DigitalPackageSpecialButtons = ribbonBarDigitalPackageSpecialButtons;
			Controller.Instance.DigitalPackageAdd = buttonItemDigitalPackageProductsAdd;
			Controller.Instance.DigitalPackageDelete = buttonItemDigitalPackageProductsDelete;
			Controller.Instance.DigitalPackageHelp = buttonItemDigitalPackageHelp;
			Controller.Instance.DigitalPackageSave = buttonItemDigitalPackageSave;
			Controller.Instance.DigitalPackageSaveAs = buttonItemDigitalPackageSaveAs;
			Controller.Instance.DigitalPackagePreview = buttonItemDigitalPackagePreview;
			Controller.Instance.DigitalPackageEmail = buttonItemDigitalPackageEmail;
			Controller.Instance.DigitalPackagePowerPoint = buttonItemDigitalPackagePowerPoint;
			Controller.Instance.DigitalPackageTheme = buttonItemDigitalPackageTheme;
			Controller.Instance.DigitalPackageOptions = buttonItemDigitalPackageSettings;
			#endregion

			#region Rate Card
			Controller.Instance.RateCardSpecialButtons = ribbonBarRateCardSpecialButtons;
			Controller.Instance.RateCardHelp = buttonItemRateCardHelp;
			Controller.Instance.RateCardCombo = comboBoxEditRateCards;
			#endregion

			#endregion

			Controller.Instance.Init();

			Controller.Instance.ScheduleChanging += (o, e) => OpenSchedule(e.ScheduleFilePath);
			Controller.Instance.ScheduleChanged += (o, e) => UpdateFormTitle();
			Controller.Instance.ScheduleCreated += (o, e) => CreateSchedule();
			Controller.Instance.FloaterRequested += (o, e) =>
			{
				Resize -= FormMain_Resize;
				AppManager.Instance.ShowFloater(this, e.AfterShow);
				Resize += FormMain_Resize;
			};

			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 1, styleController.Appearance.Font.Style);
				ribbonControl.Font = font;
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				ribbonBarDigitalPackageEmail.RecalcLayout();
				ribbonBarDigitalPackageExit.RecalcLayout();
				ribbonBarDigitalPackageSettings.RecalcLayout();
				ribbonBarDigitalPackagePowerPoint.RecalcLayout();
				ribbonPanelDigitalPackage.PerformLayout();
			}
		}

		private void UpdateFormTitle()
		{
			var schedule = BusinessWrapper.Instance.ScheduleManager.GetShortSchedule();
			Text = String.Format("WebQuick - {0}", (schedule != null && !String.IsNullOrEmpty(schedule.ShortFileName) ? schedule.ShortFileName : "New Schedule"));
		}

		private void OpenSchedule(string scheduleFilePath)
		{
			BusinessWrapper.Instance.ScheduleManager.OpenSchedule(scheduleFilePath);
			LoadData();
		}

		private void CreateSchedule()
		{
			BusinessWrapper.Instance.ScheduleManager.CreateSchedule(null);
			LoadData();
		}

		private void LoadData()
		{
			UpdateFormTitle();
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Online Schedule...";
				form.TopMost = true;
				form.Show();
				var thread = new Thread(delegate() { Invoke((MethodInvoker)delegate { Controller.Instance.LoadData(); }); });
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.Enabled = true;
			ribbonBarDigitalPackagePowerPoint.RecalcLayout();
			ribbonPanelDigitalPackage.PerformLayout();
		}

		private bool AllowToLeaveCurrentControl()
		{
			bool result = false;
			if ((_currentControl == Controller.Instance.DigitalPackage))
			{
				if (Controller.Instance.DigitalPackage.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemDigitalPackage;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else
				result = true;
			return result;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			UpdateFormTitle();
			if (File.Exists(Core.OnlineSchedule.SettingsManager.Instance.IconPath))
				Icon = new Icon(Core.OnlineSchedule.SettingsManager.Instance.IconPath);

			Utilities.Instance.ActivatePowerPoint(OnlineSchedulePowerPointHelper.Instance.PowerPointObject);
			AppManager.Instance.ActivateMainForm();

			if (String.IsNullOrEmpty(BusinessClasses.SettingsManager.Instance.LastOpenSchedule))
				BusinessWrapper.Instance.ScheduleManager.CreateSchedule(null);
			else
				BusinessWrapper.Instance.ScheduleManager.OpenSchedule(BusinessClasses.SettingsManager.Instance.LastOpenSchedule, false);

			LoadData();
		}

		public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigitalPackage)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.DigitalPackage;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRateCard)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.RateCard;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
			}
			pnMain.BringToFront();
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl == Controller.Instance.DigitalPackage)
				result = Controller.Instance.DigitalPackage.AllowToLeaveControl;
			OnlineSchedulePowerPointHelper.Instance.Disconnect(false);
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized)
				Opacity = 1;
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			Resize -= FormMain_Resize;
			AppManager.Instance.ShowFloater(this, null);
			Resize += FormMain_Resize;
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
