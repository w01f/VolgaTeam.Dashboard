using System.IO;
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
			if (File.Exists(MasterWizardManager.Instance.SelectedWizard.CleanslateFile))
			{
				string presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
				try
				{
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						var selectedTheme = Core.Dashboard.SettingsManager.Instance.SelectedTheme;
						if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.ThemeFilePath);
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
		}

		public void PrepareCleanslateEmail(string fileName)
		{
			PreparePresentation(fileName, AppendCleanslate);
		}
	}
}