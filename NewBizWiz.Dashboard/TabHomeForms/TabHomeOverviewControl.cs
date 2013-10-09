using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.TabHomeForms.Dashboard;
using NewBizWiz.Dashboard.ToolForms;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class TabHomeOverviewControl : UserControl
	{
		private static TabHomeOverviewControl _instance;

		public AppManager.SingleParameterDelegate EnableOutput { get; set; }

		private TabHomeOverviewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);

			Control dashboard = null;
			switch (SettingsManager.Instance.DashboardCode)
			{
				case "newspaper":
					dashboard = new DashboardPrint();
					break;
				case "tv":
					dashboard = new DashboardTV();
					break;
				case "radio":
					dashboard = new DashboardRadio();
					break;
				case "cable":
					dashboard = new DashboardCable();
					break;
			}
			if (dashboard != null)
				pnMain.Controls.Add(dashboard);

			pbWatermark.Image = MasterWizardManager.Instance.Watermark;
			pbWatermark.BringToFront();
			laUserName.Text = Environment.UserName;
			pbVersion.Image = MasterWizardManager.Instance.Version;
		}

		public static TabHomeOverviewControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TabHomeOverviewControl();
				return _instance;
			}
		}

		public void UpdateOutputState()
		{
			bool result = true;
			if (EnableOutput != null)
				EnableOutput(result);
		}

		public void Output()
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				form.TopMost = true;
				form.Show();
				AppManager.Instance.ShowFloater(null, () =>
				{
					DashboardPowerPointHelper.Instance.AppendCleanslate();
					form.Close();
				});
			}
		}

		public void Preview()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				DashboardPowerPointHelper.Instance.PrepareCleanslateEmail(tempFileName);
				Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview())
				{
					formPreview.Text = "Preview Slides";
					formPreview.PresentationFile = tempFileName;
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					var previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = false;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (previewResult != DialogResult.OK)
						Utilities.Instance.ActivateForm(FormMain.Instance.Handle, true, false);
					else
						Utilities.Instance.ActivateMiniBar();
				}
			}
		}
	}
}