﻿using System.Threading;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Solutions.Dashboard.InteropClasses
{
	public static partial class SolutionDashboardPowerPointHelperExtensions
	{
		public static void AppendDashboardCover(this PowerPointProcessor target, ICoverOutputData outputData, Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetCoverFile();
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var presentation = target.PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					foreach (Slide slide in presentation.Slides)
					{
						foreach (Shape shape in slide.Shapes)
						{
							for (int i = 1; i <= shape.Tags.Count; i++)
							{
								switch (shape.Tags.Name(i))
								{
									case "DATE_DATA0":
										shape.TextFrame.TextRange.Text = outputData.PresentationDate;
										break;
									case "TITLE":
										shape.TextFrame.TextRange.Text = outputData.Title;
										break;
									case "BUSINESS_NAME":
										shape.TextFrame.TextRange.Text = outputData.DecisionMaker;
										break;
									case "DECISION_MAKER":
										shape.TextFrame.TextRange.Text = outputData.Advertiser;
										break;
									case "QUOTE":
										shape.TextFrame.TextRange.Text = outputData.Quote;
										break;
									case "SALESPERSON_NAME":
										shape.TextFrame.TextRange.Text = outputData.SalesRep;
										break;
								}
							}
						}
					}
					var selectedTheme = outputData.SelectedTheme;
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.GetThemePath());
					target.AppendSlide(presentation, -1, destinationPresentation, outputData.AddAsPageOne);
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

		public static void PrepareDashboardCover(this PowerPointProcessor target, ICoverOutputData outputData, string fileName)
		{
			target.PreparePresentation(fileName, presentation => target.AppendDashboardCover(outputData, presentation));
		}
	}
}