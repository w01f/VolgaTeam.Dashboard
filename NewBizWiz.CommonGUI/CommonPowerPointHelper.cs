using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.CommonGUI.Summary;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.CommonGUI
{
	public class CommonPowerPointHelper<T> : PowerPointHelper<T> where T : class,new()
	{
		public void AppendSummary(SummaryControl summary, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder)) return;
			var itemsCount = summary.ItemsCount;
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
					var presentation = _powerPointObject.Presentations.Open(mainPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
											shape.TextFrame.TextRange.Text = summary.Title;
											break;
										case "CAMPAIGN":
											shape.Visible = !string.IsNullOrEmpty(summary.CampaignDates) ? 
												MsoTriState.msoTrue : 
												MsoTriState.msoFalse;
											break;
										case "STARTENDDATE":
											shape.TextFrame.TextRange.Text = summary.CampaignDates;
											break;
										case "PREPAREDFOR":
											shape.Visible = !string.IsNullOrEmpty(summary.Advertiser) ? 
												MsoTriState.msoTrue : 
												MsoTriState.msoFalse;
											break;
										case "ADVERTISER":
											shape.TextFrame.TextRange.Text = summary.Advertiser;
											break;
										case "LINECLIENT":
											shape.Visible = !string.IsNullOrEmpty(summary.DecisionMaker) ? 
												MsoTriState.msoTrue : 
												MsoTriState.msoFalse;
											break;
										case "DECISIONMAKER":
											shape.TextFrame.TextRange.Text = summary.DecisionMaker;
											break;
										case "DATE_FORMAT":
											shape.TextFrame.TextRange.Text = summary.PresentationDate;
											break;
										case "MNTHLY1":
											shape.Visible = summary.ShowMonthlyHeader && summary.ShowTotalHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
											shape.TextFrame.TextRange.Text = String.Format("Monthly{0}Investment", (char)13);
											break;
										case "TOTAL2":
											if ((summary.ShowMonthlyHeader && summary.ShowTotalHeader) || summary.ShowTotalHeader)
												shape.TextFrame.TextRange.Text = "Total Investment";
											else if (summary.ShowMonthlyHeader)
												shape.TextFrame.TextRange.Text = "Monthly Investment";
											else
												shape.Visible = MsoTriState.msoFalse;
											break;
										case "MWH":
											shape.Visible = !string.IsNullOrEmpty(summary.TotalMonthlyValue) ? 
												MsoTriState.msoTrue : 
												MsoTriState.msoFalse;
											break;
										case "TOTALMW":
											shape.TextFrame.TextRange.Text = summary.TotalMonthlyValue;
											break;
										case "MWT":
											shape.Visible = !string.IsNullOrEmpty(summary.TotalTotalValue) ? 
												MsoTriState.msoTrue : 
												MsoTriState.msoFalse;
											break;
										case "TOTALINVEST":
											shape.TextFrame.TextRange.Text = summary.TotalTotalValue;
											break;
										default:
											for (int k = 0; k < mainFileTemplateIndex; k++)
											{
												if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (summary.ItemTitles == null) continue;
													if ((j + k) < itemsCount)
														if (!string.IsNullOrEmpty(summary.ItemTitles[j + k]))
															shape.Visible = MsoTriState.msoTrue;
												}
												else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (summary.ItemTitles == null) continue;
													if ((j + k) >= itemsCount) continue;
													shape.TextFrame.TextRange.Text = summary.ItemTitles[j + k];
													shape.Visible = MsoTriState.msoTrue;
												}
												else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (summary.ItemDetails == null) continue;
													if ((j + k) >= itemsCount) continue;
													shape.TextFrame.TextRange.Text = summary.ItemDetails[j + k];
													shape.Visible = MsoTriState.msoTrue;
												}
												else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (summary.MonthlyValues == null) continue;
													if ((j + k) >= itemsCount) continue;
													shape.TextFrame.TextRange.Text = summary.MonthlyValues[j + k];
													shape.Visible = MsoTriState.msoTrue;
												}
												else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
												{
													shape.Visible = MsoTriState.msoFalse;
													if (summary.TotalValues == null) continue;
													if ((j + k) >= itemsCount) continue;
													shape.TextFrame.TextRange.Text = summary.TotalValues[j + k];
													shape.Visible = MsoTriState.msoTrue;
												}
											}
											break;
									}
								}
							}
						}
						var selectedTheme = summary.SelectedTheme;
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

			if (additionalFileTemplateIndex == 0 || !File.Exists(additionalPresentationTemplatePath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var presentation = _powerPointObject.Presentations.Open(additionalPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					foreach (Slide slide in presentation.Slides)
					{
						foreach (Shape shape in slide.Shapes)
						{
							for (int i = 1; i <= shape.Tags.Count; i++)
							{
								switch (shape.Tags.Name(i))
								{
									case "HEADER":
										shape.TextFrame.TextRange.Text = summary.Title;
										break;
									case "CAMPAIGN":
										shape.Visible = !string.IsNullOrEmpty(summary.CampaignDates) ? 
											MsoTriState.msoTrue : 
											MsoTriState.msoFalse;
										break;
									case "STARTENDDATE":
										shape.TextFrame.TextRange.Text = summary.CampaignDates;
										break;
									case "PREPAREDFOR":
										shape.Visible = !string.IsNullOrEmpty(summary.Advertiser) ? 
											MsoTriState.msoTrue : 
											MsoTriState.msoFalse;
										break;
									case "ADVERTISER":
										shape.TextFrame.TextRange.Text = summary.Advertiser;
										break;
									case "LINECLIENT":
										shape.Visible = !string.IsNullOrEmpty(summary.DecisionMaker) ? 
											MsoTriState.msoTrue : 
											MsoTriState.msoFalse;
										break;
									case "DECISIONMAKER":
										shape.TextFrame.TextRange.Text = summary.DecisionMaker;
										break;
									case "MNTHLY1":
										shape.Visible = summary.ShowMonthlyHeader && summary.ShowTotalHeader ? MsoTriState.msoTrue : MsoTriState.msoFalse;
										shape.TextFrame.TextRange.Text = String.Format("Monthly{0}Investment", (char)13);
										break;
									case "TOTAL2":
										if ((summary.ShowMonthlyHeader && summary.ShowTotalHeader) || summary.ShowTotalHeader)
											shape.TextFrame.TextRange.Text = "Total Investment";
										else if (summary.ShowMonthlyHeader)
											shape.TextFrame.TextRange.Text = "Monthly Investment";
										else
											shape.Visible = MsoTriState.msoFalse;
										break;
									case "DATE_FORMAT":
										shape.TextFrame.TextRange.Text = summary.PresentationDate;
										break;
									case "MWH":
										shape.Visible = !string.IsNullOrEmpty(summary.TotalMonthlyValue) ? 
											MsoTriState.msoTrue : 
											MsoTriState.msoFalse;
										break;
									case "TOTALMW":
										shape.TextFrame.TextRange.Text = summary.TotalMonthlyValue;
										break;
									case "MWT":
										shape.Visible = !string.IsNullOrEmpty(summary.TotalTotalValue) ?
											MsoTriState.msoTrue : 
											MsoTriState.msoFalse;
										break;
									case "TOTALINVEST":
										shape.TextFrame.TextRange.Text = summary.TotalTotalValue;
										break;
									default:
										int j = mainFileTemplateIndex * mainFilesCount;
										for (int k = 0; k < additionalFileTemplateIndex; k++)
										{
											if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
											{
												shape.Visible = MsoTriState.msoFalse;
												if (summary.ItemTitles == null) continue;
												if ((j + k) < itemsCount)
													if (!string.IsNullOrEmpty(summary.ItemTitles[j + k]))
														shape.Visible = MsoTriState.msoTrue;
											}
											else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
											{
												shape.Visible = MsoTriState.msoFalse;
												if (summary.ItemTitles == null) continue;
												if ((j + k) >= itemsCount) continue;
												shape.TextFrame.TextRange.Text = summary.ItemTitles[j + k];
												shape.Visible = MsoTriState.msoTrue;
											}
											else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
											{
												shape.Visible = MsoTriState.msoFalse;
												if (summary.ItemDetails == null) continue;
												if ((j + k) >= itemsCount) continue;
												shape.TextFrame.TextRange.Text = summary.ItemDetails[j + k];
												shape.Visible = MsoTriState.msoTrue;
											}
											else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
											{
												shape.Visible = MsoTriState.msoFalse;
												if (summary.MonthlyValues == null) continue;
												if ((j + k) >= itemsCount) continue;
												shape.TextFrame.TextRange.Text = summary.MonthlyValues[j + k];
												shape.Visible = MsoTriState.msoTrue;
											}
											else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
											{
												shape.Visible = MsoTriState.msoFalse;
												if (summary.TotalValues == null) continue;
												if ((j + k) >= itemsCount) continue;
												shape.TextFrame.TextRange.Text = summary.TotalValues[j + k];
												shape.Visible = MsoTriState.msoTrue;
											}
										}
										break;
								}
							}
						}
					}
					var selectedTheme = summary.SelectedTheme;
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

		public void PrepareSummaryEmail(string fileName, SummaryControl summary)
		{
			PreparePresentation(fileName, presentation => AppendSummary(summary, presentation));
		}
	}
}
