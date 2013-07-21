using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using Application = System.Windows.Forms.Application;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using ShapeRange = Microsoft.Office.Interop.PowerPoint.ShapeRange;

namespace NewBizWiz.OnlineSchedule.Controls.InteropClasses
{
	public partial class OnlineScheduleExcelHelper
	{
		public bool GetProductSummaryGrid(Schedule source, int startProduct, int endProduct)
		{
			bool result = false;
			Range range = null;
			try
			{
				string tempplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.ExcelTemplatesFolderPath, string.Format(OutputManager.ExcelTemplateFileName, "Summary", OnlineSchedulePowerPointHelper.Instance.Is2003 ? "03" : "07"));
				if (File.Exists(tempplatePath))
				{
					Workbook workBook = _excelObject.Workbooks.Open(tempplatePath);
					string sheetName = ((endProduct - startProduct) + ((endProduct - startProduct) == 5 || endProduct < 5 ? 0 : 5)).ToString() + " products";
					Worksheet workSheet = workBook.Sheets[sheetName];

					for (int i = startProduct; i < endProduct; i++)
					{
						var detailsText = new List<string>();
						if (source.ProductSummarySettings.ShowDimensions && !string.IsNullOrEmpty(source.Products[i].Dimensions))
							detailsText.Add("Ad Dimensions: " + source.Products[i].Dimensions);
						if (source.ProductSummarySettings.ShowWebsites && !string.IsNullOrEmpty(source.Products[i].AllWebsites))
							detailsText.Add("Websites: " + source.Products[i].AllWebsites);
						if (source.ProductSummarySettings.ShowImpressions && source.Products[i].MonthlyImpressionsCalculated.HasValue)
							detailsText.Add("Monthly Impressions: " + source.Products[i].MonthlyImpressionsCalculated.Value.ToString("#,##0"));
						if (source.ProductSummarySettings.ShowImpressions && source.Products[i].TotalImpressionsCalculated.HasValue)
							detailsText.Add("Total Impressions: " + source.Products[i].TotalImpressionsCalculated.Value.ToString("#,##0"));
						if (source.ProductSummarySettings.ShowTotalAds && source.Products[i].TotalAds.HasValue)
							detailsText.Add("Total Ads: " + source.Products[i].TotalAds.Value.ToString("#,##0"));
						if (source.ProductSummarySettings.ShowActiveDays && source.Products[i].ActiveDays.HasValue)
							detailsText.Add("Active Days: " + source.Products[i].ActiveDays.Value.ToString("#,##0"));
						if (source.ProductSummarySettings.ShowAdRate && source.Products[i].AdRate.HasValue)
							detailsText.Add("Ad Rate: " + source.Products[i].AdRate.Value.ToString("$#,###.00"));

						if (source.ProductSummarySettings.ShowActiveDays |
							source.ProductSummarySettings.ShowAdRate |
							source.ProductSummarySettings.ShowDimensions |
							source.ProductSummarySettings.ShowImpressions |
							source.ProductSummarySettings.ShowTotalAds |
							source.ProductSummarySettings.ShowWebsites)
						{
							workSheet.Range["product" + ((i - startProduct) + 1).ToString()].Value = source.Products[i].Name;
							workSheet.Range["details" + ((i - startProduct) + 1).ToString()].Value = string.Join(";  ", detailsText.ToArray());
						}
						else
						{
							workSheet.Range["product" + ((i - startProduct) + 1).ToString()].Value = string.Empty;
							workSheet.Range["details" + ((i - startProduct) + 1).ToString()].Value = source.Products[i].Name;
						}

						string monthlyHeader = source.EnableMonthlyOnSummary ? "Monthly" : "Total";
						string totalHeader = source.EnableTotalOnSummary ? "Total" : "Monthly";
						string monthlyInvestmentValue = source.EnableMonthlyOnSummary ? (source.Products[i].MonthlyInvestmentCalculated.HasValue ? source.Products[i].MonthlyInvestmentCalculated.Value.ToString("$#,###.00") : string.Empty) : (source.Products[i].TotalInvestmentCalculated.HasValue ? source.Products[i].TotalInvestmentCalculated.Value.ToString("$#,###.00") : string.Empty);
						string monthlyCPMValue = source.EnableMonthlyOnSummary ? (source.Products[i].MonthlyCPMCalculated.HasValue ? source.Products[i].MonthlyCPMCalculated.Value.ToString("$#,###.00") : string.Empty) : (source.Products[i].TotalCPMCalculated.HasValue ? source.Products[i].TotalCPMCalculated.Value.ToString("$#,###.00") : string.Empty);
						string totalInvestmentValue = source.EnableTotalOnSummary ? (source.Products[i].TotalInvestmentCalculated.HasValue ? source.Products[i].TotalInvestmentCalculated.Value.ToString("$#,###.00") : string.Empty) : (source.Products[i].MonthlyInvestmentCalculated.HasValue ? source.Products[i].MonthlyInvestmentCalculated.Value.ToString("$#,###.00") : string.Empty);
						string totalCPMValue = source.EnableTotalOnSummary ? (source.Products[i].TotalCPMCalculated.HasValue ? source.Products[i].TotalCPMCalculated.Value.ToString("$#,###.00") : string.Empty) : (source.Products[i].MonthlyCPMCalculated.HasValue ? source.Products[i].MonthlyCPMCalculated.Value.ToString("$#,###.00") : string.Empty);

						if (source.ProductSummarySettings.ShowInvestment && source.ProductSummarySettings.ShowCPM)
						{
							workSheet.Range["mthheader" + ((i - startProduct) + 1).ToString()].Value = monthlyHeader;
							workSheet.Range["totalheader" + ((i - startProduct) + 1).ToString()].Value = monthlyHeader;
							workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString()].Value = monthlyInvestmentValue;
							workSheet.Range["totvalue" + ((i - startProduct) + 1).ToString()].Value = totalInvestmentValue;
							workSheet.Range["mthimpvalue" + ((i - startProduct) + 1).ToString()].Value = monthlyCPMValue;
							workSheet.Range["totimpvalue" + ((i - startProduct) + 1).ToString()].Value = totalCPMValue;

							if ((source.EnableMonthlyOnSummary && !source.EnableTotalOnSummary) || (!source.EnableMonthlyOnSummary && source.EnableTotalOnSummary))
							{
								range = workSheet.Range["mthheader" + ((i - startProduct) + 1).ToString(), "totalheader" + ((i - startProduct) + 1).ToString()];
								range.MergeCells = true;
								range = workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString(), "totvalue" + ((i - startProduct) + 1).ToString()];
								range.MergeCells = true;
								range = workSheet.Range["mthimpvalue" + ((i - startProduct) + 1).ToString(), "totimpvalue" + ((i - startProduct) + 1).ToString()];
								range.MergeCells = true;
							}
						}
						else if (source.ProductSummarySettings.ShowInvestment && !source.ProductSummarySettings.ShowCPM)
						{
							workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString()].Value = monthlyInvestmentValue;
							workSheet.Range["totvalue" + ((i - startProduct) + 1).ToString()].Value = totalInvestmentValue;
							workSheet.Range["mthimpvalue" + ((i - startProduct) + 1).ToString()].Value = monthlyInvestmentValue;
							workSheet.Range["totimpvalue" + ((i - startProduct) + 1).ToString()].Value = totalInvestmentValue;

							workSheet.Range["invest" + ((i - startProduct) + 1).ToString()].Value = workSheet.Range["impress" + ((i - startProduct) + 1).ToString()].Value;

							range = workSheet.Range["invest" + ((i - startProduct) + 1).ToString(), "impress" + ((i - startProduct) + 1).ToString()];
							range.MergeCells = true;
							range = workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString(), "mthimpvalue" + ((i - startProduct) + 1).ToString()];
							range.MergeCells = true;
							range = workSheet.Range["totvalue" + ((i - startProduct) + 1).ToString(), "totimpvalue" + ((i - startProduct) + 1).ToString()];
							range.MergeCells = true;

							if ((source.EnableMonthlyOnSummary && !source.EnableTotalOnSummary) || (!source.EnableMonthlyOnSummary && source.EnableTotalOnSummary))
							{
								range = workSheet.Range["mthheader" + ((i - startProduct) + 1).ToString(), "totalheader" + ((i - startProduct) + 1).ToString()];
								range.MergeCells = true;
								range = workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString(), "totvalue" + ((i - startProduct) + 1).ToString()];
								range.MergeCells = true;
							}
						}
						else if (!source.ProductSummarySettings.ShowInvestment && source.ProductSummarySettings.ShowCPM)
						{
							workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString()].Value = monthlyCPMValue;
							workSheet.Range["totvalue" + ((i - startProduct) + 1).ToString()].Value = totalCPMValue;
							workSheet.Range["mthimpvalue" + ((i - startProduct) + 1).ToString()].Value = monthlyCPMValue;
							workSheet.Range["totimpvalue" + ((i - startProduct) + 1).ToString()].Value = totalCPMValue;

							workSheet.Range["impress" + ((i - startProduct) + 1).ToString()].Value = workSheet.Range["invest" + ((i - startProduct) + 1).ToString()].Value;

							range = workSheet.Range["invest" + ((i - startProduct) + 1).ToString(), "impress" + ((i - startProduct) + 1).ToString()];
							range.MergeCells = true;
							range = workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString(), "mthimpvalue" + ((i - startProduct) + 1).ToString()];
							range.MergeCells = true;
							range = workSheet.Range["totvalue" + ((i - startProduct) + 1).ToString(), "totimpvalue" + ((i - startProduct) + 1).ToString()];
							range.MergeCells = true;

							if ((source.EnableMonthlyOnSummary && !source.EnableTotalOnSummary) || (!source.EnableMonthlyOnSummary && source.EnableTotalOnSummary))
							{
								range = workSheet.Range["mthheader" + ((i - startProduct) + 1).ToString(), "totalheader" + ((i - startProduct) + 1).ToString()];
								range.MergeCells = true;
								range = workSheet.Range["mthinvvalue" + ((i - startProduct) + 1).ToString(), "totvalue" + ((i - startProduct) + 1).ToString()];
								range.MergeCells = true;
							}
						}
						else
						{
							range = workSheet.Range["impress" + ((i - startProduct) + 1).ToString(), "totalheader" + ((i - startProduct) + 1).ToString()];
							range.Delete(XlDeleteShiftDirection.xlShiftToLeft);
						}
					}

					if ((source.ProductSummarySettings.ShowTotalsOnLastOnly && endProduct == source.Products.Count) || !source.ProductSummarySettings.ShowTotalsOnLastOnly)
					{
						workSheet.Range["combhdr1"].Value = source.ProductSummarySettings.TotalHeader1;
						workSheet.Range["combvalue1"].Value = source.ProductSummarySettings.TotalValue1;
						workSheet.Range["combhdr2"].Value = source.ProductSummarySettings.TotalHeader2;
						workSheet.Range["combvalue2"].Value = source.ProductSummarySettings.TotalValue2;
						workSheet.Range["combhdr3"].Value = source.ProductSummarySettings.TotalHeader3;
						workSheet.Range["combvalue3"].Value = source.ProductSummarySettings.TotalValue3;
						workSheet.Range["combhdr4"].Value = source.ProductSummarySettings.TotalHeader4;
						workSheet.Range["combvalue4"].Value = source.ProductSummarySettings.TotalValue4;
					}
					else
					{
						workSheet.Range["combhdr1"].Value = string.Empty;
						workSheet.Range["combvalue1"].Value = string.Empty;
						workSheet.Range["combhdr2"].Value = string.Empty;
						workSheet.Range["combvalue2"].Value = string.Empty;
						workSheet.Range["combhdr3"].Value = string.Empty;
						workSheet.Range["combvalue3"].Value = string.Empty;
						workSheet.Range["combhdr4"].Value = string.Empty;
						workSheet.Range["combvalue4"].Value = string.Empty;
					}

					workSheet.Range["areaoutput"].Copy();

					result = true;
				}
			}
			catch { }
			return result;
		}
	}

	public partial class OnlineSchedulePowerPointHelper
	{
		public void AppendProductSummary(Schedule source, bool pasteAsImage, Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.ProductSummaryTemplatesFolderPath))
			{
				string presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.ProductSummaryTemplatesFolderPath, OutputManager.ProductSummaryTemplateFileName);
				if (File.Exists(presentationTemplatePath))
				{
					try
					{
						var thread = new Thread(delegate()
						{
							MessageFilter.Register();

							var excelHelper = new OnlineScheduleExcelHelper();

							for (int k = 0; k < source.Products.Count; k += 5)
							{
								Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
								foreach (Slide slide in presentation.Slides)
								{
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "HEADER":
													shape.TextFrame.TextRange.Text = source.ProductSummarySettings.SlideHeader;
													break;
												case "DATETAG":
													shape.TextFrame.TextRange.Text = source.PresentationDate.ToString("MM/dd/yy");
													break;
												case "ADVERTISER":
													shape.TextFrame.TextRange.Text = source.BusinessName;
													break;
												case "DECMKR":
													shape.TextFrame.TextRange.Text = source.DecisionMaker;
													break;
												case "FLTDT1":
													shape.TextFrame.TextRange.Text = source.FlightDates;
													break;
												case "PRODUCTS":
													if (excelHelper.Connect())
													{
														if (!excelHelper.GetProductSummaryGrid(source, k, ((k + 5) < source.Products.Count) ? (k + 5) : source.Products.Count))
														{
															excelHelper.Disconnect();
															break;
														}

														ShapeRange shapeRange = null;
														if (pasteAsImage)
															shapeRange = slide.Shapes.PasteSpecial(DataType: PpPasteDataType.ppPasteEnhancedMetafile);
														else
															shapeRange = slide.Shapes.PasteSpecial(DataType: PpPasteDataType.ppPasteOLEObject);
														shapeRange.Top = shape.Top;
														shapeRange.Left = shape.Left;
														shapeRange.Width = shape.Width;
														shape.Visible = MsoTriState.msoFalse;
														excelHelper.Disconnect();
													}
													break;
											}
										}
									}
								}
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
		}

		public void PrepareSummaryEmail(string fileName, Schedule outputSource, bool pasteAsImage)
		{
			try
			{
				Presentations presentations = _powerPointObject.Presentations;
				Presentation presentation = presentations.Add(MsoTriState.msoFalse);
				presentation.PageSetup.SlideWidth = (float)SettingsManager.Instance.SizeWidth * 72;
				presentation.PageSetup.SlideHeight = (float)SettingsManager.Instance.SizeHeght * 72;
				switch (SettingsManager.Instance.Orientation)
				{
					case "Landscape":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
						break;
					case "Portrait":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
						break;
				}
				Utilities.Instance.ReleaseComObject(presentations);
				AppendProductSummary(outputSource, pasteAsImage, presentation);
				MessageFilter.Register();
				var thread = new Thread(delegate()
				{
					presentation.SaveAs(FileName: fileName);
					string destinationFolder = fileName.Replace(Path.GetExtension(fileName), string.Empty);
					if (!Directory.Exists(destinationFolder))
						Directory.CreateDirectory(destinationFolder);
					presentation.Export(Path: destinationFolder, FilterName: "PNG");
					presentation.Close();
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();

				Utilities.Instance.ReleaseComObject(presentation);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}
}