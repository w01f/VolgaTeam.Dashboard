using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendSnapshot(Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.SnapshotTemlatesFolderPath))
			{
				try
				{
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						var recordsPeSlide = Controller.Instance.Summaries.Snapshot.RecordsPerSlide;
						var publicationsCount = Controller.Instance.Summaries.Snapshot.PublicationNames.Length;
						for (var k = 0; k < publicationsCount; k += recordsPeSlide)
						{
							string presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.SnapshotTemlatesFolderPath, Controller.Instance.Summaries.Snapshot.GetOutputTemplatePath(k));
							if (!File.Exists(presentationTemplatePath)) continue;
							var presentation = PowerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
							foreach (Slide slide in presentation.Slides)
							{
								foreach (Shape shape in slide.Shapes)
								{
									for (int i = 1; i <= shape.Tags.Count; i++)
									{
										switch (shape.Tags.Name(i))
										{
											case "ADVERTISER":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.BusinessName;
												break;
											case "DATETAG":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.Date;
												break;
											case "DECISIONMAKER":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.DecisionMaker;
												break;
											case "HEADER":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.Header;
												break;
											case "FLTDT1":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.FlightDates;
												break;
											case "DIGTAG":
												shape.TextFrame.TextRange.Text = k == 0 || !Controller.Instance.Summaries.Snapshot.ShowDigitalLegendOnlyFirstSlide ? Controller.Instance.Summaries.Snapshot.DigitalLegend : String.Empty;
												break;
											default:
												for (int j = 0; j < 6; j++)
												{
													if (shape.Tags.Name(i).Equals(string.Format("PUBLOGO{0}", Utilities.Instance.GetLetterByDigit(j + 1))))
													{
														if ((k + j) < Controller.Instance.Summaries.Snapshot.LogoFiles.Length)
															if (!string.IsNullOrEmpty(Controller.Instance.Summaries.Snapshot.LogoFiles[k + j]))
																slide.Shapes.AddPicture(FileName: Controller.Instance.Summaries.Snapshot.LogoFiles[k + j], LinkToFile: MsoTriState.msoFalse, SaveWithDocument: MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
														shape.Visible = MsoTriState.msoFalse;
													}
													else if (shape.Tags.Name(i).Equals(string.Format("PUB{0}", Utilities.Instance.GetLetterByDigit(j + 1))))
													{
														if ((k + j) < Controller.Instance.Summaries.Snapshot.PublicationNames.Length)
														{
															shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.PublicationNames[k + j];
														}
														else
															shape.Visible = MsoTriState.msoFalse;
													}
													else
													{
														for (int l = 0; l < 6; l++)
														{
															if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}{1}", new object[] { (l + 1), Utilities.Instance.GetLetterByDigit(j + 1) })))
															{
																if ((k + j) < Controller.Instance.Summaries.Snapshot.AdSpecs.Length)
																{
																	if (l < Controller.Instance.Summaries.Snapshot.AdSpecs[k + j].Length)
																		shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.AdSpecs[k + j][l];
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
							var selectedTheme = Controller.Instance.Summaries.Snapshot.SelectedTheme;
							if (selectedTheme != null)
								presentation.ApplyTheme(selectedTheme.ThemeFilePath);
							AppendSlide(presentation, -1, destinationPresentation);
							presentation.Close();
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
		}

		public void PrepareSnapshotEmail(string fileName)
		{
			PreparePresentation(fileName, AppendSnapshot);
		}

		public void PrepareSnapshotPdf(string targetFileName)
		{
			var sourceFileName = Path.GetTempFileName();
			PreparePresentation(sourceFileName, AppendSnapshot);
			BuildPdf(sourceFileName, targetFileName);
		}
	}
}