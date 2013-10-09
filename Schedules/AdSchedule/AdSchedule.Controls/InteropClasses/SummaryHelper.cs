using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendSummary(Presentation destinationPresentation = null)
		{
			if (Directory.Exists(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder))
			{
				int itemsCount = Controller.Instance.Summary.ItemsCount;
				int mainFileTemplateIndex = itemsCount >= 5 ? 5 : itemsCount;

				int additionalFileTemplateIndex = itemsCount > 5 ? itemsCount % 5 : 0;

				int mainFilesCount = itemsCount / 5;
				if (mainFilesCount == 0 && itemsCount > 0)
					mainFilesCount++;


				string mainPresentationTemplatePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder, string.Format(MasterWizardManager.SimpleSummarySlideTemplate, mainFileTemplateIndex));
				string additionalPresentationTemplatePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder, string.Format(MasterWizardManager.SimpleSummarySlideTemplate, (additionalFileTemplateIndex + mainFileTemplateIndex)));

				if (File.Exists(mainPresentationTemplatePath))
				{
					try
					{
						var thread = new Thread(delegate()
						{
							MessageFilter.Register();
							Presentation presentation = _powerPointObject.Presentations.Open(FileName: mainPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
							for (int j = 0; j < (itemsCount - additionalFileTemplateIndex); j += mainFileTemplateIndex)
							{
								foreach (Slide slide in presentation.Slides)
								{
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "HEADER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.Title;
													break;
												case "CAMPAIGN":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.CampaignDates))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "STARTENDDATE":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.CampaignDates;
													break;
												case "PREPAREDFOR":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.Advertiser))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "ADVERTISER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.Advertiser;
													break;
												case "LINECLIENT":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.DecisionMaker))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "DECISIONMAKER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.DecisionMaker;
													break;
												case "DATE_FORMAT":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.PresentationDate;
													break;
												case "MNTHLY1":
													shape.Visible = Controller.Instance.Summary.ShowMonthlyHeader && Controller.Instance.Summary.ShowTotalHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.ShowMonthlyHeader ? String.Format("Monthly{0}Investment", (char)13) : String.Format("Total{0}Investment", (char)13);
													break;
												case "TOTAL2":
													shape.Visible = Controller.Instance.Summary.ShowMonthlyHeader || Controller.Instance.Summary.ShowTotalHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.ShowMonthlyHeader && Controller.Instance.Summary.ShowTotalHeader ? String.Format("Total{0}Investment", (char)13) : String.Format("Monthly{0}Investment", (char)13);
													break;
												case "MWH":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.TotalMonthlyValue))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "TOTALMW":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.TotalMonthlyValue;
													break;
												case "MWT":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.TotalTotalValue))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "TOTALINVEST":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.TotalTotalValue;
													break;
												default:
													for (int k = 0; k < mainFileTemplateIndex; k++)
													{
														if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ItemTitles != null)
																if ((j + k) < itemsCount)
																	if (!string.IsNullOrEmpty(Controller.Instance.Summary.ItemTitles[j + k]))
																		shape.Visible = MsoTriState.msoTrue;
														}
														else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ItemTitles != null)
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.ItemTitles[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
														}
														else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ItemDetails != null)
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.ItemDetails[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
														}
														else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ShowMonthlyHeader && Controller.Instance.Summary.ShowTotalHeader && Controller.Instance.Summary.MonthlyValues.Any())
															{
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.MonthlyValues[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
															}
														}
														else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ShowMonthlyHeader && Controller.Instance.Summary.ShowTotalHeader && Controller.Instance.Summary.TotalValues.Any())
															{
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.TotalValues[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
															}
															else if (Controller.Instance.Summary.ShowMonthlyHeader && Controller.Instance.Summary.MonthlyValues.Any())
															{
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.MonthlyValues[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
															}
														}
													}
													break;
											}
										}
									}
								}
								var selectedTheme = Controller.Instance.Summary.SelectedTheme;
								if (selectedTheme != null)
									presentation.ApplyTheme(selectedTheme.ThemeFilePath);
								AppendSlide(presentation, -1, destinationPresentation);
							}
							presentation.Close();
						});
						thread.Start();

						while (thread.IsAlive)
							System.Windows.Forms.Application.DoEvents();
					}
					finally
					{
						MessageFilter.Revoke();
					}

					if (additionalFileTemplateIndex != 0 && File.Exists(additionalPresentationTemplatePath))
					{
						try
						{
							var thread = new Thread(delegate()
							{
								MessageFilter.Register();
								Presentation presentation = _powerPointObject.Presentations.Open(FileName: additionalPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
								foreach (Slide slide in presentation.Slides)
								{
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "HEADER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.Title;
													break;
												case "CAMPAIGN":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.CampaignDates))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "STARTENDDATE":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.CampaignDates;
													break;
												case "PREPAREDFOR":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.Advertiser))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "ADVERTISER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.Advertiser;
													break;
												case "LINECLIENT":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.DecisionMaker))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "DECISIONMAKER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.DecisionMaker;
													break;
												case "MNTHLY1":
													shape.Visible = Controller.Instance.Summary.ShowMonthlyHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
													break;
												case "DATE_FORMAT":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.PresentationDate;
													break;
												case "MWH":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.TotalMonthlyValue))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "TOTALMW":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.TotalMonthlyValue;
													break;
												case "MWT":
													if (!string.IsNullOrEmpty(Controller.Instance.Summary.TotalTotalValue))
														shape.Visible = MsoTriState.msoTrue;
													else
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "TOTALINVEST":
													shape.TextFrame.TextRange.Text = Controller.Instance.Summary.TotalTotalValue;
													break;
												default:
													int j = mainFileTemplateIndex * mainFilesCount;
													for (int k = 0; k < additionalFileTemplateIndex; k++)
													{
														if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ItemTitles != null)
																if ((j + k) < itemsCount)
																	if (!string.IsNullOrEmpty(Controller.Instance.Summary.ItemTitles[j + k]))
																		shape.Visible = MsoTriState.msoTrue;
														}
														else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ItemTitles != null)
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.ItemTitles[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
														}
														else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.ItemDetails != null)
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.ItemDetails[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
														}
														else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.MonthlyValues != null)
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.MonthlyValues[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
														}
														else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
														{
															shape.Visible = MsoTriState.msoFalse;
															if (Controller.Instance.Summary.TotalValues != null)
																if ((j + k) < itemsCount)
																{
																	shape.TextFrame.TextRange.Text = Controller.Instance.Summary.TotalValues[j + k];
																	shape.Visible = MsoTriState.msoTrue;
																}
														}
													}
													break;
											}
										}
									}
								}
								var selectedTheme = Controller.Instance.Summary.SelectedTheme;
								if (selectedTheme != null)
									presentation.ApplyTheme(selectedTheme.ThemeFilePath);
								AppendSlide(presentation, -1, destinationPresentation);
								presentation.Close();
							});
							thread.Start();

							while (thread.IsAlive)
								System.Windows.Forms.Application.DoEvents();
						}
						finally
						{
							MessageFilter.Revoke();
						}
					}
				}
			}
		}

		public void PrepareSummaryEmail(string fileName)
		{
			PreparePresentation(fileName, AppendSummary);
		}
	}
}