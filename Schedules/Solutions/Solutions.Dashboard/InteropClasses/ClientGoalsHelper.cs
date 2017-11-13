using System;
using System.IO;
using System.Threading;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Solutions.Dashboard.InteropClasses
{
	public static partial class SolutionPowerPointHelperExtensions
	{
		public static void AppendClientGoals(this PowerPointProcessor target, IClientGoalsOutputData outputData, Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetClientGoalsFile(String.Format(MasterWizardManager.ClientGoalsSlideTemplate, outputData.GoalsCount));
			if (!File.Exists(presentationTemplatePath)) return;
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var presentation = target.PowerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					foreach (Slide slide in presentation.Slides)
					{
						foreach (Shape shape in slide.Shapes)
						{
							for (int i = 1; i <= shape.Tags.Count; i++)
							{
								switch (shape.Tags.Name(i))
								{
									case "HEADER":
										shape.TextFrame.TextRange.Text = outputData.Title;
										break;
									case "TEXTBOX0":
										shape.TextFrame.TextRange.Text = outputData.SelectedGoals[0];
										break;
									case "TEXTBOX1":
										shape.TextFrame.TextRange.Text = outputData.SelectedGoals[1];
										break;
									case "TEXTBOX2":
										shape.TextFrame.TextRange.Text = outputData.SelectedGoals[2];
										break;
									case "TEXTBOX3":
										shape.TextFrame.TextRange.Text = outputData.SelectedGoals[3];
										break;
									case "TEXTBOX4":
										shape.TextFrame.TextRange.Text = outputData.SelectedGoals[4];
										break;
								}
							}
						}
					}
					var selectedTheme = outputData.SelectedTheme;
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.GetThemePath());
					target.AppendSlide(presentation, -1, destinationPresentation);
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

		public static void PrepareClientGoals(this PowerPointProcessor target, IClientGoalsOutputData outputData, string fileName)
		{
			target.PreparePresentation(fileName, presentation => target.AppendClientGoals(outputData, presentation));
		}
	}
}