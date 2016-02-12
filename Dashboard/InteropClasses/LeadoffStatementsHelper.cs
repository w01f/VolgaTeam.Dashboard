using System;
using System.IO;
using System.Threading;
using Asa.Business.Dashboard.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Asa.Dashboard.TabHomeForms;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Dashboard.InteropClasses
{
	public partial class DashboardPowerPointHelper
	{
		public void AppendLeadoffStatements(Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetLeadoffStatementsFile(String.Format(MasterWizardManager.LeadOffSlideTemplate, TabHomeMainPage.Instance.SlideLeadoff.StatementsCount));
			if (!File.Exists(presentationTemplatePath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					foreach (Slide slide in presentation.Slides)
					{
						foreach (Shape shape in slide.Shapes)
						{
							for (int i = 1; i <= shape.Tags.Count; i++)
							{
								switch (shape.Tags.Name(i))
								{
									case "HEADER":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideLeadoff.Title;
										break;
									case "TEXTBOX1":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideLeadoff.SelectedStatements[0];
										break;
									case "TEXTBOX2":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideLeadoff.SelectedStatements[1];
										break;
									case "TEXTBOX3":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideLeadoff.SelectedStatements[2];
										break;
								}
							}
						}
					}
					var selectedTheme = SettingsManager.Instance.GetSelectedTheme(SlideType.LeadoffStatement);
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

		public void PrepareLeadoffStatements(string fileName)
		{
			PreparePresentation(fileName, AppendLeadoffStatements);
		}
	}
}