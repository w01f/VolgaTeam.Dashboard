﻿using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Asa.AdSchedule.Controls.BusinessClasses;
using Asa.Core.Common;
using Asa.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendMultiSummary(Presentation destinationPresentation = null)
		{

			int fileIndex = Controller.Instance.Summaries.MultiSummary.OutputFileIndex;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();

					int publicationsCount = Controller.Instance.Summaries.MultiSummary.PublicationNames1.Length;
					for (int k = 0; k < publicationsCount; k += fileIndex)
					{
						var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetMultiSummaryFile(String.Format("{0}{1}", fileIndex, Controller.Instance.Summaries.MultiSummary.ShowDigitalLegend && (k == 0 || !Controller.Instance.Summaries.MultiSummary.ShowDigitalLegendOnlyFirstSlide) ? "d" : String.Empty));
						if (File.Exists(presentationTemplatePath))
						{

							Presentation presentation = PowerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
							foreach (Slide slide in presentation.Slides)
							{
								foreach (Shape shape in slide.Shapes)
								{
									for (int i = 1; i <= shape.Tags.Count; i++)
									{
										switch (shape.Tags.Name(i))
										{
											case "ADVERTISER":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.BusinessName;
												break;
											case "DATETAG":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.Date;
												break;
											case "DECISIONMAKER":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.DecisionMaker;
												break;
											case "HEADER":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.Header;
												break;
											case "FLTDT1":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.FlightDates1;
												break;
											case "MLIN2":
												if (!((k + 1) < publicationsCount))
													shape.Visible = MsoTriState.msoFalse;
												break;
											case "CAL2":
												if (!((k + 1) < publicationsCount))
													shape.Visible = MsoTriState.msoFalse;
												break;
											case "RD2":
												if (!((k + 1) < publicationsCount))
													shape.Visible = MsoTriState.msoFalse;
												break;
											case "DIGTAG":
												shape.TextFrame.TextRange.Text = k == 0 || !Controller.Instance.Summaries.MultiSummary.ShowDigitalLegendOnlyFirstSlide ? Controller.Instance.Summaries.MultiSummary.DigitalLegend : String.Empty;
												break;
											default:
												for (int j = 0; j < fileIndex; j++)
												{
													if (shape.Tags.Name(i).Equals(string.Format("MLTLOGO{0}", j + 1)))
													{
														if ((k + j) < Controller.Instance.Summaries.MultiSummary.LogoFiles.Length)
															if (!string.IsNullOrEmpty(Controller.Instance.Summaries.MultiSummary.LogoFiles[k + j]))
																slide.Shapes.AddPicture(FileName: Controller.Instance.Summaries.MultiSummary.LogoFiles[k + j], LinkToFile: MsoTriState.msoFalse, SaveWithDocument: MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
														shape.Visible = MsoTriState.msoFalse;
													}
													else if (shape.Tags.Name(i).Equals(string.Format("PUBTAG{0}", j + 1)))
													{
														if ((k + j) < Controller.Instance.Summaries.MultiSummary.PublicationNames1.Length)
														{
															shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.PublicationNames1[k + j];
															shape.Width = shape.TextFrame.TextRange.BoundWidth + 10;
														}
														else
															shape.Visible = MsoTriState.msoFalse;
													}
													else if (shape.Tags.Name(i).Equals(string.Format("PUBTAG{0}B", j + 1)))
													{
														if ((k + j) < Controller.Instance.Summaries.MultiSummary.PublicationNames2.Length)
														{
															shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.PublicationNames2[k + j];
															shape.Width = shape.TextFrame.TextRange.BoundWidth + 10;
														}
														else
															shape.Visible = MsoTriState.msoFalse;
													}
													else if (shape.Tags.Name(i).Equals(string.Format("INVTAG{0}", j + 1)))
													{
														if ((k + j) < Controller.Instance.Summaries.MultiSummary.Investments.Length && !String.IsNullOrEmpty(Controller.Instance.Summaries.MultiSummary.Investments[k + j]))
														{
															shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.Investments[k + j];
															shape.Width = shape.TextFrame.TextRange.BoundWidth + 10;
															shape.Left = presentation.SlideMaster.Width - shape.Width - 20;
														}
														else
															shape.Visible = MsoTriState.msoFalse;
													}
													else if (shape.Tags.Name(i).Equals(string.Format("FLTDT2{0}", (j > 0 ? "B" : string.Empty))))
													{
														if ((k + j) < Controller.Instance.Summaries.MultiSummary.FlightDates2.Length)
														{
															shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.FlightDates2[k + j];
														}
														else
															shape.Visible = MsoTriState.msoFalse;
													}
													else if (shape.Tags.Name(i).Equals(string.Format("DATELIST{0}", j + 1)))
													{
														if ((k + j) < Controller.Instance.Summaries.MultiSummary.RunDates.Length)
														{
															shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.RunDates[k + j];
														}
														else
															shape.Visible = MsoTriState.msoFalse;
													}
													else
													{
														for (int l = 0; l < 6; l++)
														{
															if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}{1}", new object[] { (l + 1), (j > 0 ? "B" : string.Empty) })))
															{
																if ((k + j) < Controller.Instance.Summaries.MultiSummary.AdSpecs.Length)
																{
																	if (l < Controller.Instance.Summaries.MultiSummary.AdSpecs[k + j].Length)
																		shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.MultiSummary.AdSpecs[k + j][l];
																	else
																		shape.Visible = MsoTriState.msoFalse;
																}
																else
																	shape.Visible = MsoTriState.msoFalse;
															}
														}
													}
												}
												break;
										}
									}
								}
							}
							var selectedTheme = Controller.Instance.Summaries.MultiSummary.SelectedTheme;
							if (selectedTheme != null)
								presentation.ApplyTheme(selectedTheme.GetThemePath());
							AppendSlide(presentation, -1, destinationPresentation);
							presentation.Close();
						}
					}
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

		public void PrepareMultiSummaryEmail(string fileName)
		{
			PreparePresentation(fileName, AppendMultiSummary);
		}

		public void PrepareMultiSummaryPdf(string targetFileName)
		{
			var sourceFileName = Path.GetTempFileName();
			PreparePresentation(sourceFileName, AppendMultiSummary);
			BuildPdf(sourceFileName, targetFileName);
		}
	}
}