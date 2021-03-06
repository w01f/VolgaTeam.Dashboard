﻿using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls;
using Asa.Core.Common;
using Asa.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendBasicOverview(PublicationBasicOverviewControl[] outputControls, Presentation destinationPresentation = null)
		{
			foreach (var outputControl in outputControls)
			{
				var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetBasicOverviewSlideFile(outputControl.OutputFileIndex);
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
										case "PLOGO":
											if (!string.IsNullOrEmpty(outputControl.LogoFile))
												slide.Shapes.AddPicture(FileName: outputControl.LogoFile, LinkToFile: MsoTriState.msoFalse, SaveWithDocument: MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
											shape.Visible = MsoTriState.msoFalse;
											break;
										case "PUBTAG":
											shape.TextFrame.TextRange.Text = outputControl.PresentationName1;
											break;
										case "DATETAG":
											shape.TextFrame.TextRange.Text = outputControl.PresentationDate1;
											break;
										case "PUBTAG2":
											shape.TextFrame.TextRange.Text = outputControl.PresentationName2;
											break;
										case "DATETAG2":
											shape.TextFrame.TextRange.Text = outputControl.PresentationDate2;
											break;
										case "ADVERTISER":
											shape.TextFrame.TextRange.Text = outputControl.BusinessName;
											break;
										case "DECISIONMAKER":
											shape.TextFrame.TextRange.Text = outputControl.DecisionMaker;
											break;
										case "FLTDT1":
											shape.TextFrame.TextRange.Text = outputControl.FlightDates1.Trim();
											break;
										case "FLTDT2":
											shape.TextFrame.TextRange.Text = outputControl.FlightDates2.Trim();
											break;
										case "RUNDATES":
											shape.TextFrame.TextRange.Text = outputControl.RunDates;
											break;
										case "DIGTAG":
											shape.TextFrame.TextRange.Text = outputControl.DigitalLegend;
											break;
										default:
											for (int j = 0; j < 5; j++)
											{
												if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}", j + 1)))
												{
													if (j < outputControl.AdSpecs.Length)
														shape.TextFrame.TextRange.Text = outputControl.AdSpecs[j];
													else
														shape.Visible = MsoTriState.msoFalse;
												}
											}
											for (int j = 0; j < 2; j++)
											{
												if (shape.Tags.Name(i).Equals(string.Format("TOTALADS{0}", j + 1)))
												{
													if (j < outputControl.AdSummaries.Length)
														shape.TextFrame.TextRange.Text = outputControl.AdSummaries[j];
													else
														shape.Visible = MsoTriState.msoFalse;
												}
											}
											for (int j = 0; j < 4; j++)
											{
												if (shape.Tags.Name(i).Equals(string.Format("INVTAG{0}", j + 1)))
												{
													if (j < outputControl.InvestmentDetails.Length)
														shape.TextFrame.TextRange.Text = outputControl.InvestmentDetails[j];
													else
														shape.Visible = MsoTriState.msoFalse;
												}
											}

											break;
									}
								}
							}
						}
						var selectedTheme = outputControl.SelectedTheme;
						if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.GetThemePath());
						AppendSlide(presentation, -1, destinationPresentation);
						presentation.Close();
					});
					thread.Start();

					while (thread.IsAlive)
						Application.DoEvents();
				}
				catch
				{
				}
				finally
				{
					MessageFilter.Revoke();
				}
			}
		}

		public void PrepareBasicOverviewEmail(string fileName, PublicationBasicOverviewControl[] outputControls)
		{
			PreparePresentation(fileName, presentation => AppendBasicOverview(outputControls, presentation));
		}
	}
}