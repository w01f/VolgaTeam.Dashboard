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
	public partial class SolutionDashboardPowerPointHelper
	{
		public void AppendTargetCustomers(ITargetCustomersOutputData outputData, Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetTargetCustomersFile(String.Format(MasterWizardManager.TargetCustomersSlideTemplate, 1));
			if (!File.Exists(presentationTemplatePath)) return;
			try
			{
				var thread = new Thread(delegate ()
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
										shape.TextFrame.TextRange.Text = outputData.Title;
										break;
									case "TEXTBOX0":
										shape.TextFrame.TextRange.Text = outputData.TargetDemo;
										break;
									case "TEXTBOX1":
										shape.TextFrame.TextRange.Text = outputData.HHI;
										break;
									case "TEXTBOX2":
										shape.TextFrame.TextRange.Text = outputData.Geography;
										break;
								}
							}
						}
					}
					var selectedTheme = outputData.SelectedTheme;
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

		public void PrepareTargetCustomers(ITargetCustomersOutputData outputData, string fileName)
		{
			PreparePresentation(fileName, presentation => AppendTargetCustomers(outputData, presentation));
		}
	}
}