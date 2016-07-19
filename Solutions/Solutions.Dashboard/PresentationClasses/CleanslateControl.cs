using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class CleanslateControl : DashboardSlideControl
	{
		public override SlideType SlideType => SlideType.Cleanslate;
		public override string SlideName => "Slide Header";

		public CleanslateControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			pnBottom.Visible = false;
		}

		public override void LoadData() { }

		public override void ApplyChanges() { }

		public override Boolean ReadyForOutput => true;

		public override void GenerateOutput()
		{
			//DashboardPowerPointHelper.Instance.AppendCleanslate();
		}

		public override PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			//FormProgress.ShowProgress();
			//string tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//DashboardPowerPointHelper.Instance.PrepareCleanslateEmail(tempFileName);
			//Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			//FormProgress.CloseProgress();
			//if (!File.Exists(tempFileName)) return;
			//using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			//{
			//	formPreview.Text = "Preview Slides";
			//	formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
			//	RegistryHelper.MainFormHandle = formPreview.Handle;
			//	RegistryHelper.MaximizeMainForm = false;
			//	var previewResult = formPreview.ShowDialog();
			//	RegistryHelper.MaximizeMainForm = false;
			//	RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			//	if (previewResult != DialogResult.OK)
			//		AppManager.Instance.ActivateMainForm();
			//}
		}
	}
}