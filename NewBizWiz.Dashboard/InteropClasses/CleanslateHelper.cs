using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Dashboard.ToolForms;
using Application = System.Windows.Forms.Application;

namespace NewBizWiz.Dashboard.InteropClasses
{
	public partial class DashboardPowerPointHelper
	{
		public void AppendCleanslate()
		{
			if (File.Exists(MasterWizardManager.Instance.SelectedWizard.CleanslateFile))
			{
				string presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
				try
				{
					using (var form = new FormProgress())
					{
						form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
						form.TopMost = true;
						var thread = new Thread(delegate()
						{
							MessageFilter.Register();
							Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
							AppendSlide(presentation, -1);
							presentation.Close();
						});
						thread.Start();

						form.Show();

						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
					using (var form = new FormSlideOutput())
					{
						if (form.ShowDialog() != DialogResult.OK)
							AppManager.Instance.ActivateMainForm();
					}
				}
				catch {}
				finally
				{
					MessageFilter.Revoke();
				}
			}
		}
	}
}