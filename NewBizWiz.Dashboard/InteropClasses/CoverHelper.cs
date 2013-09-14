using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Dashboard.TabHomeForms;
using NewBizWiz.Dashboard.ToolForms;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.Dashboard.InteropClasses
{
	public partial class DashboardPowerPointHelper
	{
		public void AppendCover(bool firstSlide)
		{
			if (MasterWizardManager.Instance.SelectedWizard.CoverFile != null)
			{
				string presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.CoverFile;
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
							foreach (Slide slide in presentation.Slides)
							{
								foreach (Shape shape in slide.Shapes)
								{
									for (int i = 1; i <= shape.Tags.Count; i++)
									{
										switch (shape.Tags.Name(i))
										{
											case "DATE_DATA0":
												shape.TextFrame.TextRange.Text = CoverControl.Instance.PresentationDate;
												break;
											case "TITLE":
												shape.TextFrame.TextRange.Text = CoverControl.Instance.Title;
												break;
											case "BUSINESS_NAME":
												shape.TextFrame.TextRange.Text = CoverControl.Instance.DecisionMaker;
												break;
											case "DECISION_MAKER":
												shape.TextFrame.TextRange.Text = CoverControl.Instance.Advertiser;
												break;
											case "QUOTE":
												shape.TextFrame.TextRange.Text = CoverControl.Instance.Quote;
												break;
											case "SALESPERSON_NAME":
												shape.TextFrame.TextRange.Text = CoverControl.Instance.SalesRep;
												break;
										}
									}
								}
							}
							AppendSlide(presentation, -1, null, firstSlide);
							presentation.Close();
						});
						thread.Start();

						form.Show();

						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
				}
				catch {}
				finally
				{
					MessageFilter.Revoke();
				}
			}
		}

		public void AppendGenericCover(bool firstSlide)
		{
			if (File.Exists(MasterWizardManager.Instance.SelectedWizard.GenericCoverFile))
			{
				string presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GenericCoverFile;
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
							AppendSlide(presentation, -1, null, firstSlide);
							presentation.Close();
						});
						thread.Start();

						form.Show();

						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
				}
				catch {}
				finally
				{
					MessageFilter.Revoke();
				}
			}
			else
				Utilities.Instance.ShowWarning("No Cover Available");
		}
	}
}