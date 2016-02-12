using System.Threading;
using Asa.Business.Dashboard.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = System.Windows.Forms.Application;

namespace Asa.Dashboard.InteropClasses
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
					var selectedTheme = SettingsManager.Instance.GetSelectedTheme(SlideType.Cleanslate);
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