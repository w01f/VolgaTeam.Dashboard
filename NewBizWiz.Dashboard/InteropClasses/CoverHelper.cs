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
		public void AppendCover(bool firstSlide, Presentation destinationPresentation = null)
		{
			if (MasterWizardManager.Instance.SelectedWizard.CoverFile == null) return;
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.CoverFile;
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
					var selectedTheme = Core.Dashboard.SettingsManager.Instance.GetSelectedTheme(SlideType.Cover);
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.ThemeFilePath);
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

		public void PrepareCover(string fileName, bool firstSlide)
		{
			PreparePresentation(fileName, presentation => AppendCover(firstSlide, presentation));
		}

		public void AppendGenericCover(bool firstSlide, Presentation destinationPresentation = null)
		{
			if (!File.Exists(MasterWizardManager.Instance.SelectedWizard.GenericCoverFile)) return;
			var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GenericCoverFile;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					var selectedTheme = Core.Dashboard.SettingsManager.Instance.GetSelectedTheme(SlideType.Cover);
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.ThemeFilePath);
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

		public void PrepareGenericCover(string fileName, bool firstSlide)
		{
			PreparePresentation(fileName, presentation => AppendGenericCover(firstSlide, presentation));
		}
	}
}