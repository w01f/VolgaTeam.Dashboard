using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Dashboard.TabHomeForms;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.Dashboard.InteropClasses
{
	public partial class DashboardPowerPointHelper
	{
		public void AppendClientGoals(Presentation destinationPresentation = null)
		{
			if (Directory.Exists(MasterWizardManager.Instance.SelectedWizard.ClientGoalsFolder))
			{
				var presentationTemplatePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.ClientGoalsFolder, string.Format(MasterWizardManager.ClientGoalsSlideTemplate, TabHomeMainPage.Instance.SlideClientGoals.GoalsCount));
				if (!File.Exists(presentationTemplatePath)) return;
				try
				{
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						var presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						foreach (Slide slide in presentation.Slides)
						{
							foreach (Shape shape in slide.Shapes)
							{
								for (int i = 1; i <= shape.Tags.Count; i++)
								{
									switch (shape.Tags.Name(i))
									{
										case "HEADER":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideClientGoals.Title;
											break;
										case "TEXTBOX0":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideClientGoals.SelectedGoals[0];
											break;
										case "TEXTBOX1":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideClientGoals.SelectedGoals[1];
											break;
										case "TEXTBOX2":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideClientGoals.SelectedGoals[2];
											break;
										case "TEXTBOX3":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideClientGoals.SelectedGoals[3];
											break;
										case "TEXTBOX4":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideClientGoals.SelectedGoals[4];
											break;
									}
								}
							}
						}
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

		public void PrepareClientGoals(string fileName)
		{
			PreparePresentation(fileName, AppendClientGoals);
		}
	}
}