using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;

namespace NewBizWiz.Dashboard.InteropClasses
{
	public partial class DashboardPowerPointHelper
	{
		public void AppendCleanslate(Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetCleanslateFile();
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					var selectedTheme = Core.Dashboard.SettingsManager.Instance.GetSelectedTheme(SlideType.Cleanslate);
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.GetThemePath());
					AppendSlide(presentation, -1, destinationPresentation);
					presentation.Close();
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void PrepareCleanslateEmail(string fileName)
		{
			PreparePresentation(fileName, AppendCleanslate);
		}
	}
}