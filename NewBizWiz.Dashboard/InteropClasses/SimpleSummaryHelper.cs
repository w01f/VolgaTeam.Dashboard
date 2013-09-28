using System.IO;
using System.Threading;
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
		public void AppendSimpleSummary()
		{
			if (Directory.Exists(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder))
			{
				int itemsCount = SimpleSummaryControl.Instance.ItemsCount;
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
						using (var form = new FormProgress())
						{
							form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
							form.TopMost = true;
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
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.Title;
														break;
													case "CAMPAIGN":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.CampaignDates))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "STARTENDDATE":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.CampaignDates;
														break;
													case "PREPAREDFOR":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.Advertiser))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "ADVERTISER":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.Advertiser;
														break;
													case "LINECLIENT":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.DecisionMaker))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "DECISIONMAKER":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.DecisionMaker;
														break;
													case "DATE_FORMAT":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.PresentationDate;
														break;
													case "MNTHLY1":
														shape.Visible = SimpleSummaryControl.Instance.ShowMonhlyHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
														break;
													case "TOTAL2":
														shape.Visible = SimpleSummaryControl.Instance.ShowTotalHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
														break;
													case "MWH":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.TotalMonthlyValue))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "TOTALMW":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.TotalMonthlyValue;
														break;
													case "MWT":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.TotalTotalValue))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "TOTALINVEST":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.TotalTotalValue;
														break;
													default:
														for (int k = 0; k < mainFileTemplateIndex; k++)
														{
															if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.ItemTitles != null)
																	if ((j + k) < itemsCount)
																		if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.ItemTitles[j + k]))
																			shape.Visible = MsoTriState.msoTrue;
															}
															else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.ItemTitles != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.ItemTitles[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
															else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.ItemDetails != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.ItemDetails[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
															else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.MonthlyValues != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.MonthlyValues[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
															else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.TotalValues != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.TotalValues[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
														}
														break;
												}
											}
										}
									}
									var selectedTheme = Core.Dashboard.SettingsManager.Instance.SelectedTheme;
									if (selectedTheme != null)
										presentation.ApplyTheme(selectedTheme.ThemeFilePath);
									AppendSlide(presentation, -1);
								}
								presentation.Close();
							});
							thread.Start();

							form.Show();

							while (thread.IsAlive)
								Application.DoEvents();
							form.Close();
						}
					}
					catch { }
					finally
					{
						MessageFilter.Revoke();
					}

					if (additionalFileTemplateIndex != 0 && File.Exists(additionalPresentationTemplatePath))
					{
						try
						{
							using (var form = new FormProgress())
							{
								form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
								form.TopMost = true;
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
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.Title;
														break;
													case "CAMPAIGN":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.CampaignDates))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "STARTENDDATE":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.CampaignDates;
														break;
													case "PREPAREDFOR":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.Advertiser))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "ADVERTISER":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.Advertiser;
														break;
													case "LINECLIENT":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.DecisionMaker))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "DECISIONMAKER":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.DecisionMaker;
														break;
													case "MNTHLY1":
														shape.Visible = SimpleSummaryControl.Instance.ShowMonhlyHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
														break;
													case "DATE_FORMAT":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.PresentationDate;
														break;
													case "MWH":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.TotalMonthlyValue))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "TOTALMW":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.TotalMonthlyValue;
														break;
													case "MWT":
														if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.TotalTotalValue))
															shape.Visible = MsoTriState.msoTrue;
														else
															shape.Visible = MsoTriState.msoFalse;
														break;
													case "TOTALINVEST":
														shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.TotalTotalValue;
														break;
													default:
														int j = mainFileTemplateIndex * mainFilesCount;
														for (int k = 0; k < additionalFileTemplateIndex; k++)
														{
															if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.ItemTitles != null)
																	if ((j + k) < itemsCount)
																		if (!string.IsNullOrEmpty(SimpleSummaryControl.Instance.ItemTitles[j + k]))
																			shape.Visible = MsoTriState.msoTrue;
															}
															else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.ItemTitles != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.ItemTitles[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
															else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.ItemDetails != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.ItemDetails[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
															else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.MonthlyValues != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.MonthlyValues[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
															else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
															{
																shape.Visible = MsoTriState.msoFalse;
																if (SimpleSummaryControl.Instance.TotalValues != null)
																	if ((j + k) < itemsCount)
																	{
																		shape.TextFrame.TextRange.Text = SimpleSummaryControl.Instance.TotalValues[j + k];
																		shape.Visible = MsoTriState.msoTrue;
																	}
															}
														}
														break;
												}
											}
										}
									}
									var selectedTheme = Core.Dashboard.SettingsManager.Instance.SelectedTheme;
									if (selectedTheme != null)
										presentation.ApplyTheme(selectedTheme.ThemeFilePath);
									AppendSlide(presentation, -1);
									presentation.Close();
								});
								thread.Start();

								form.Show();

								while (thread.IsAlive)
									Application.DoEvents();
								form.Close();
							}
						}
						catch { }
						finally
						{
							MessageFilter.Revoke();
						}
					}
				}
			}
		}
	}
}