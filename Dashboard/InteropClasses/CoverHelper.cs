﻿using System.Threading;
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
	public partial class DashboardPowerPointProcessor
	{
		public void AppendCover(bool firstSlide, Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetCoverFile();
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
									case "DATE_DATA0":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideCover.PresentationDate;
										break;
									case "TITLE":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideCover.Title;
										break;
									case "BUSINESS_NAME":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideCover.DecisionMaker;
										break;
									case "DECISION_MAKER":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideCover.Advertiser;
										break;
									case "QUOTE":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideCover.Quote;
										break;
									case "SALESPERSON_NAME":
										shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideCover.SalesRep;
										break;
								}
							}
						}
					}
					var selectedTheme = SettingsManager.Instance.GetSelectedTheme(SlideType.Cover);
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.GetThemePath());
					AppendSlide(presentation, -1, destinationPresentation, firstSlide);
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

		public void PrepareCover(string fileName)
		{
			PreparePresentation(fileName, presentation => AppendCover(false, presentation));
		}

		public void AppendGenericCover(bool firstSlide, Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetGenericCoverFile();
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					var selectedTheme = SettingsManager.Instance.GetSelectedTheme(SlideType.Cover);
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.GetThemePath());
					AppendSlide(presentation, -1, destinationPresentation, firstSlide);
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

		public void PrepareGenericCover(string fileName)
		{
			PreparePresentation(fileName, presentation => AppendGenericCover(false, presentation));
		}
	}
}