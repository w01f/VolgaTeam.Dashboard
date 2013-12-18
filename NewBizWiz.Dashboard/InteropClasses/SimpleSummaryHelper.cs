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
		public void AppendSimpleSummary(Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder)) return;
			var itemsCount = TabHomeMainPage.Instance.SlideSimpleSummary.ItemsCount;
			var mainFileTemplateIndex = itemsCount >= 5 ? 5 : itemsCount;

			var additionalFileTemplateIndex = itemsCount > 5 ? itemsCount % 5 : 0;

			var mainFilesCount = itemsCount / 5;
			if (mainFilesCount == 0 && itemsCount > 0)
				mainFilesCount++;


			var mainPresentationTemplatePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder, string.Format(MasterWizardManager.SimpleSummarySlideTemplate, mainFileTemplateIndex));
			var additionalPresentationTemplatePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder, string.Format(MasterWizardManager.SimpleSummarySlideTemplate, (additionalFileTemplateIndex + mainFileTemplateIndex)));

			if (!File.Exists(mainPresentationTemplatePath)) return;
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
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.Title;
											break;
										case "CAMPAIGN":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.CampaignDates))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "STARTENDDATE":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.CampaignDates;
											break;
										case "PREPAREDFOR":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.Advertiser))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "ADVERTISER":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.Advertiser;
											break;
										case "LINECLIENT":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.DecisionMaker))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "DECISIONMAKER":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.DecisionMaker;
											break;
										case "DATE_FORMAT":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.PresentationDate;
											break;
										case "MNTHLY1":
											shape.Visible = TabHomeMainPage.Instance.SlideSimpleSummary.ShowMonthlyHeader && TabHomeMainPage.Instance.SlideSimpleSummary.ShowTotalHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
											break;
										case "TOTAL2":
											if ((TabHomeMainPage.Instance.SlideSimpleSummary.ShowMonthlyHeader && TabHomeMainPage.Instance.SlideSimpleSummary.ShowTotalHeader) || TabHomeMainPage.Instance.SlideSimpleSummary.ShowTotalHeader)
												shape.TextFrame.TextRange.Text = "Total Investment";
											else if (TabHomeMainPage.Instance.SlideSimpleSummary.ShowMonthlyHeader)
												shape.TextFrame.TextRange.Text = "Monthly Investment";
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "MWH":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.TotalMonthlyValue))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "TOTALMW":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.TotalMonthlyValue;
											break;
										case "MWT":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.TotalTotalValue))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "TOTALINVEST":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.TotalTotalValue;
											break;
										default:
											for (int k = 0; k < mainFileTemplateIndex; k++)
											{
												if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles != null)
														if ((j + k) < itemsCount)
															if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles[j + k]))
																shape.Visible = MsoTriState.msoTrue;
												}
												else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles != null)
														if ((j + k) < itemsCount)
														{
															shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles[j + k];
															shape.Visible = MsoTriState.msoTrue;
														}
												}
												else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.ItemDetails != null)
														if ((j + k) < itemsCount)
														{
															shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.ItemDetails[j + k];
															shape.Visible = MsoTriState.msoTrue;
														}
												}
												else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.MonthlyValues == null) continue;
													if ((j + k) < itemsCount)
													{
														shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.MonthlyValues[j + k];
														shape.Visible = MsoTriState.msoTrue;
													}
												}
												else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.TotalValues != null)
														if ((j + k) < itemsCount)
														{
															shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.TotalValues[j + k];
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
						AppendSlide(presentation, -1, destinationPresentation);
					}
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
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.Title;
											break;
										case "CAMPAIGN":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.CampaignDates))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "STARTENDDATE":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.CampaignDates;
											break;
										case "PREPAREDFOR":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.Advertiser))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "ADVERTISER":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.Advertiser;
											break;
										case "LINECLIENT":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.DecisionMaker))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "DECISIONMAKER":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.DecisionMaker;
											break;
										case "MNTHLY1":
											shape.Visible = TabHomeMainPage.Instance.SlideSimpleSummary.ShowMonthlyHeader && TabHomeMainPage.Instance.SlideSimpleSummary.ShowTotalHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
											break;
										case "TOTAL2":
											if ((TabHomeMainPage.Instance.SlideSimpleSummary.ShowMonthlyHeader && TabHomeMainPage.Instance.SlideSimpleSummary.ShowTotalHeader) || TabHomeMainPage.Instance.SlideSimpleSummary.ShowTotalHeader)
												shape.TextFrame.TextRange.Text = "Total Investment";
											else if (TabHomeMainPage.Instance.SlideSimpleSummary.ShowMonthlyHeader)
												shape.TextFrame.TextRange.Text = "Monthly Investment";
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "DATE_FORMAT":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.PresentationDate;
											break;
										case "MWH":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.TotalMonthlyValue))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "TOTALMW":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.TotalMonthlyValue;
											break;
										case "MWT":
											if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.TotalTotalValue))
												shape.Visible = MsoTriState.msoTrue;
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "TOTALINVEST":
											shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.TotalTotalValue;
											break;
										default:
											int j = mainFileTemplateIndex * mainFilesCount;
											for (int k = 0; k < additionalFileTemplateIndex; k++)
											{
												if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles != null)
														if ((j + k) < itemsCount)
															if (!string.IsNullOrEmpty(TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles[j + k]))
																shape.Visible = MsoTriState.msoTrue;
												}
												else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles != null)
														if ((j + k) < itemsCount)
														{
															shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.ItemTitles[j + k];
															shape.Visible = MsoTriState.msoTrue;
														}
												}
												else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.ItemDetails != null)
														if ((j + k) < itemsCount)
														{
															shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.ItemDetails[j + k];
															shape.Visible = MsoTriState.msoTrue;
														}
												}
												else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.MonthlyValues != null)
														if ((j + k) < itemsCount)
														{
															shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.MonthlyValues[j + k];
															shape.Visible = MsoTriState.msoTrue;
														}
												}
												else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (TabHomeMainPage.Instance.SlideSimpleSummary.TotalValues != null)
														if ((j + k) < itemsCount)
														{
															shape.TextFrame.TextRange.Text = TabHomeMainPage.Instance.SlideSimpleSummary.TotalValues[j + k];
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
						AppendSlide(presentation, -1, destinationPresentation);
						presentation.Close();
					});
					thread.Start();
				}
				catch { }
				finally
				{
					MessageFilter.Revoke();
				}
			}
		}

		public void PrepareSimpleSummary(string fileName)
		{
			PreparePresentation(fileName, AppendSimpleSummary);
		}
	}
}