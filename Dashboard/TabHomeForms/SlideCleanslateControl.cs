using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using Asa.Dashboard.InteropClasses;
using Asa.Dashboard.TabHomeForms.Dashboard;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class SlideCleanslateControl : SlideBaseControl
	{
		private readonly SuperTooltipInfo _toolTipHelp = new SuperTooltipInfo("HELP", "", "Help me use this Sales Dashboard", null, null, eTooltipColor.Gray);

		public override string SlideName => "Slide Header";

		public override SuperTooltipInfo TooltipHelp => _toolTipHelp;

		public override ButtonItem ThemeButton => FormMain.Instance.buttonItemHomeThemeCleanslate;

		public SlideCleanslateControl()
			: base()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			Control dashboard = new DashboardDefault();
			pnBottom.Visible = false;
			pnMain.Controls.Add(dashboard);
		}

		protected override void UpdateSavedFilesState()
		{
			SetLoadState(false);
		}

		public void UpdateOutputState()
		{
			SetOutputState(true);
		}

		public void Output()
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowProgress();
			AppManager.Instance.ShowFloater(() =>
			{
				DashboardPowerPointHelper.Instance.AppendCleanslate();
				FormProgress.CloseProgress();
			});
		}

		public void Preview()
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			string tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			DashboardPowerPointHelper.Instance.PrepareCleanslateEmail(tempFileName);
			Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Slides";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = false;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				if (previewResult != DialogResult.OK)
					AppManager.Instance.ActivateMainForm();
			}
		}
	}
}