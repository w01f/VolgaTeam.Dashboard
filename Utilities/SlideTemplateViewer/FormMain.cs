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

		#region GUI Event Handlers
		public void Init()
		{
			FormStateHelper.Init(this, ResourceManager.Instance.AppSettingsFolder, "add slides", false).LoadState();

			Text = AppManager.Instance.FormCaption;
			Icon = AppManager.Instance.SlideManager.FormIcon ?? Icon;

			ribbonTabItemSlides.Text = AppManager.Instance.SlideManager.TabTitle ?? ribbonTabItemSlides.Text;
			ribbonBarSlidesLogo.Text = Environment.UserName;
			labelItemSlidesLogo.Image = AppManager.Instance.SlideManager.RibbonBarLogo ?? Resources.AddSlidesLogo;

			buttonItemSlidesPowerPoint.Click += TabSlidesMainPage.Instance.buttonItemSlidesPowerPoint_Click;
			buttonItemSlidesPreview.Click += TabSlidesMainPage.Instance.buttonItemSlidesPreview_Click;

			buttonItemSlideSettings.Visible =
				MasterWizardManager.Instance.MasterWizards.Count > 1 ||
				(MasterWizardManager.Instance.MasterWizards.Count == 1 && SlideSettings.GetAvailableConfigurations().Count(MasterWizardManager.Instance.MasterWizards.First().Value.HasSlideConfiguration) > 1);
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			Utilities.ActivatePowerPoint(SlideTemplateViewerPowerPointHelper.Instance.PowerPointObject);
			Controls.Add(TabSlidesMainPage.Instance);
			TabSlidesMainPage.Instance.BringToFront();
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
				new FloaterRequestedEventArgs());
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