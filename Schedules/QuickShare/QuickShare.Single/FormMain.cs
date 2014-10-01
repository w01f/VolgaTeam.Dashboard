using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.QuickShare.Controls;
using NewBizWiz.QuickShare.Controls.BusinessClasses;
using NewBizWiz.QuickShare.Controls.InteropClasses;

namespace NewBizWiz.QuickShare.Single
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;
		private Control _currentControl;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		private FormMain()
		{
			InitializeComponent();

			FormStateHelper.Init(this, SettingsManager.Instance.SettingsPath, MediaMetaData.Instance.DataTypeString, true);

			Controller.Instance.FormMain = this;
			Controller.Instance.PagesContainer = pnMain;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabHome = ribbonTabItemHome;

			#region Command Controls

			#region Home
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeSave = buttonItemHomeSave;
			Controller.Instance.HomeSaveAs = buttonItemHomeSaveAs;
			Controller.Instance.HomeHelp = buttonItemHomeHelp;
			Controller.Instance.HomeScheduleAdd = buttonItemHomeScheduleAdd;
			Controller.Instance.HomeScheduleClone = buttonItemHomeScheduleClone;
			Controller.Instance.HomeClientType = comboBoxEditClientType;
			Controller.Instance.HomeAccountNumberText = textEditAccountNumber;
			Controller.Instance.HomeAccountNumberCheck = checkBoxItemHomeAccountNumber;
			#endregion

			#endregion

			Controller.Instance.Init();

			Controller.Instance.PackageChanged += (o, e) => UpdateFormTitle();
			Controller.Instance.FloaterRequested += (o, e) => AppManager.Instance.ShowFloater(this, e);
			Controller.Instance.CloseRequested += (o, e) => Close();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 1, styleController.Appearance.Font.Style);
				ribbonControl.Font = font;
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				comboBoxEditBusinessName.Font = font;
				comboBoxEditDecisionMaker.Font = font;
				comboBoxEditClientType.Font = font;
				dateEditPresentationDate.Font = font;
				ribbonBarHomeBasicInfo.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonPanelHome.PerformLayout();
			}
		}

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

		private void UpdateFormTitle()
		{
			if (string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard)) return;
			var shortSchedule = BusinessWrapper.Instance.PackageManager.GetShortPackage();
			Text = String.Format("QuickSHARE - {0} - {1} {2}", SettingsManager.Instance.SelectedWizard, SettingsManager.Instance.Size, String.Format("({0})", shortSchedule != null ? shortSchedule.ShortFileName : String.Empty));
		}

		private void LoadData()
		{
			UpdateFormTitle();
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Schedule...";
				form.TopMost = true;
				form.Show();
				var thread = new Thread(() => Invoke((MethodInvoker)(() => Controller.Instance.LoadData())));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.Enabled = true;
		}

		private bool AllowToLeaveCurrentControl()
		{
			var result = false;
			if ((_currentControl == Controller.Instance.HomeControl))
			{
				if (Controller.Instance.HomeControl.AllowToLeaveControl())
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;
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
			if (File.Exists(MediaMetaData.Instance.SettingsManager.IconPath))
				Icon = new Icon(Core.OnlineSchedule.SettingsManager.Instance.IconPath);

			Utilities.Instance.ActivatePowerPoint(QuickSharePowerPointHelper.Instance.PowerPointObject);
			AppManager.Instance.ActivateMainForm();

			using (var formStart = new FormStart())
			{
				formStart.buttonXOpen.Enabled = ScheduleManager.GetShortScheduleList().Length > 0;
				var result = formStart.ShowDialog();
				if (result == DialogResult.Yes || result == DialogResult.No)
				{
					if (result == DialogResult.Yes)
						buttonItemHomeNewPackage_Click(null, null);
					else
						buttonItemHomeOpenPackage_Click(null, null);
				}
				else
					Application.Exit();
			}
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}

		public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.HomeControl;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(_currentControl);
				}
				_currentControl.BringToFront();
				pnMain.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem.Tag == null)
			{
				_currentControl = null;
				pnMain.BringToFront();
			}
			else
			{
				pnEmpty.Visible = true;
				_currentControl = null;
				pnMain.BringToFront();
			}
			if (WindowState == FormWindowState.Normal)
			{
				Width++;
				Width--;
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Controller.Instance.HomeControl.AllowToLeaveControl();
			QuickSharePowerPointHelper.Instance.Disconnect(false);
		}

		private void buttonItemHomeNewPackage_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule())
			{
				from.Text = "Build a New Package";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						var fileName = BusinessWrapper.Instance.PackageManager.GetPackageFileName(from.ScheduleName.Trim());
						BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("New Created", from.ScheduleName.Trim()));
						BusinessWrapper.Instance.PackageManager.OpenPackage(fileName);
						LoadData();
					}
					else
					{
						Utilities.Instance.ShowWarning("Package Name can't be empty");
					}
				}
				else if (!BusinessWrapper.Instance.PackageManager.PackageLoaded)
					Close();
			}
		}

		private void buttonItemHomeOpenPackage_Click(object sender, EventArgs e)
		{
			using (var from = new FormOpenPackage())
			{
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.PackageName))
					{
						var fileName = from.PackageName.Trim();
						BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Previous Opened", Path.GetFileNameWithoutExtension(fileName)));
						BusinessWrapper.Instance.PackageManager.OpenPackage(fileName, false);
						LoadData();
					}
					else
					{
						Utilities.Instance.ShowWarning("Package Name can't be empty");
					}
				}
				else if (!BusinessWrapper.Instance.PackageManager.PackageLoaded)
					Close();
			}
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			AppManager.Instance.ShowFloater(formSender ?? this, new FloaterRequestedEventArgs { Logo = QuickShare.Controls.Properties.Resources.RibbonLogo });
		}
	}
}