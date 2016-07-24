using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Activities;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.SlideSettingsEditors;
using Asa.SlideTemplateViewer.Properties;
using DevComponents.DotNetBar;

namespace Asa.SlideTemplateViewer
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		private FormMain()
		{
			_instance = this;
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
			}
			PowerPointManager.Instance.SettingsChanged += (o, e) =>
			{
				Text = AppManager.Instance.FormCaption;
			};
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

		#region Configuration Methods
		private void ApplyMasterWizard()
		{
			Text = AppManager.Instance.FormCaption;

			var userName = Environment.UserName;
			ribbonBarSlidesLogo.Text = userName;
		}
		#endregion

		#region GUI Event Handlers
		public void Init()
		{
			FormStateHelper.Init(this, ResourceManager.Instance.AppSettingsFolder, "add slides", false).LoadState();

			ApplyMasterWizard();

			buttonItemSlidesPowerPoint.Click += TabSlidesMainPage.Instance.buttonItemSlidesPowerPoint_Click;
			buttonItemSlidesPreview.Click += TabSlidesMainPage.Instance.buttonItemSlidesPreview_Click;

			Controls.Add(TabSlidesMainPage.Instance);
			TabSlidesMainPage.Instance.BringToFront();

			buttonItemSlideSettings.Visible =
				MasterWizardManager.Instance.MasterWizards.Count > 1 ||
				(MasterWizardManager.Instance.MasterWizards.Count == 1 && SlideSettings.GetAvailableConfigurations().Count(MasterWizardManager.Instance.MasterWizards.First().Value.HasSlideConfiguration) > 1);
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			Utilities.ActivatePowerPoint(SlideTemplateViewerPowerPointHelper.Instance.PowerPointObject);
			AppManager.Instance.ActivateMainForm();
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			AppManager.Instance.ActivityManager.AddActivity(new UserActivity("Application Closed"));
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}
		#endregion

		#region Ribbon Buttons's Clicks Event Handlers
		public void buttonItemFloater_Click(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			if (formSender != null && formSender.IsDisposed) return;
			AppManager.Instance.ShowFloater(
				formSender,
				new FloaterRequestedEventArgs
				{
					Logo = Resources.AddSlidesLogo
				});
		}

		public void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonItemHelp_Click(object sender, EventArgs e)
		{
			AppManager.Instance.HelpManager.OpenHelpLink("Slides");
		}

		private void buttonItemSlideSettings_Click(object sender, EventArgs e)
		{
			using (var form = new FormEditSlideSettings())
			{
				form.ShowDialog(this);
			}
		}
		#endregion
	}
}