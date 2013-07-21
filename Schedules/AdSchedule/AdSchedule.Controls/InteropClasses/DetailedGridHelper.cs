using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using ShapeRange = Microsoft.Office.Interop.PowerPoint.ShapeRange;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;

namespace NewBizWiz.AdSchedule.Controls.InteropClasses
{
	public partial class AdScheduleExcelHelper
	{
		public bool GetDetailedGrid(PublicationDetailedGridControl outputControl, int slideIndex)
		{
			bool result = false;
			try
			{
				Workbook workBook = _excelObject.Workbooks.Open(BusinessWrapper.Instance.OutputManager.ExcelOutputTemplateFilePath);
				Worksheet workSheet = workBook.Sheets[Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader ? OutputManager.DetailedGridTemplateSheetNameWithNotes : OutputManager.DetailedGridTemplateSheetNameWithoutNotes];

				string[][] gridData = outputControl.Grid[slideIndex];
				string[] gridHeaders = outputControl.GridHeaders;
				int[] gridHeaderSizes = outputControl.GridHeaderSizes;

				int columnsCount = gridHeaders.Length;
				int columnsPerRecord = Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader ? 3 : 2;
				int rowsCount = gridData.Length;
				int excelRowsCount = 2 + rowsCount * columnsPerRecord - 1;
				double excelGridWidth = (workSheet.Range[GetColumnLetterByIndex(0) + ":" + GetColumnLetterByIndex(0)].ColumnWidth) * OutputManager.Columns;
				double outputGridWidth = gridHeaderSizes.Sum();
				double kDiff = excelGridWidth / outputGridWidth;

				var mergeRanges = new List<string>();
				for (int i = 0; i < OutputManager.Columns; i++)
				{
					if (i < columnsCount)
					{
						workSheet.Range[GetColumnLetterByIndex(i) + ":" + GetColumnLetterByIndex(i)].ColumnWidth = gridHeaderSizes[i] * kDiff;
						workSheet.Range[GetColumnLetterByIndex(i) + "1"].Formula = gridHeaders[i];
						for (int j = 0; j < rowsCount; j++)
						{
							int excelMainRowIndex = 2 + (j * columnsPerRecord) + 1;
							int excelCommentRowIndex = 2 + (j * columnsPerRecord) + 2;
							var digitalLegend = gridData[j].Length == 1;
							if (i < gridData[j].Length)
							{
								workSheet.Range[GetColumnLetterByIndex(i) + excelMainRowIndex.ToString()].Formula = gridData[j][i + (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader && !digitalLegend ? 1 : 0)];
								if (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader && !digitalLegend)
									workSheet.Range[GetColumnLetterByIndex(i) + excelCommentRowIndex.ToString()].Formula = gridData[j][0];
							}
							else
								workSheet.Range[GetColumnLetterByIndex(i) + excelMainRowIndex.ToString()].Formula = String.Empty;
							if (digitalLegend)
							{
								if (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader)
									mergeRanges.Add(String.Format("{0}{1}:{2}{3}", GetColumnLetterByIndex(0), excelMainRowIndex, GetColumnLetterByIndex(columnsCount - 1), excelCommentRowIndex));
								else
									mergeRanges.Add(String.Format("{0}{1}:{2}{1}", GetColumnLetterByIndex(0), excelMainRowIndex, GetColumnLetterByIndex(columnsCount - 1)));
							}
						}
					}
					else
						workSheet.Range[GetColumnLetterByIndex(columnsCount) + ":" + GetColumnLetterByIndex(columnsCount)].Delete(XlDeleteShiftDirection.xlShiftToLeft);
				}
				foreach (var mergeRangeName in mergeRanges)
				{
					var mergeRange = workSheet.Range[mergeRangeName];
					mergeRange.Merge();
					mergeRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
					var borders = mergeRange.Borders;
					borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
					borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
					borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
					borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
					borders.Color = Color.Black;
					borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
					borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;
					borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
					borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
					Utilities.Instance.ReleaseComObject(borders);
				}

				Range range = workSheet.Range[GetColumnLetterByIndex(0) + "1", GetColumnLetterByIndex(columnsCount - 1) + excelRowsCount.ToString()];
				range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				range.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				range.Borders[XlBordersIndex.xlEdgeRight].ColorIndex = Color.Black;
				range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				range.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				range.Borders[XlBordersIndex.xlEdgeLeft].ColorIndex = Color.Black;
				range.Copy();

				result = true;
			}
			catch { }
			return result;
		}
	}

	public partial class AdSchedulePowerPointHelper
	{
		public void AppendDetailedGridExcelBased(PublicationDetailedGridControl outputControl, bool pasteGridAsImage, Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.DetailedGridExcelBasedTemlatesFolderPath))
			{
				string presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.DetailedGridExcelBasedTemlatesFolderPath, string.Format(OutputManager.DetailedGridExcelBasedSlideTemplate, outputControl.OutputFileIndex));
				if (File.Exists(presentationTemplatePath))
				{
					try
					{
						var thread = new Thread(delegate()
						{
							MessageFilter.Register();

							var excelHelper = new AdScheduleExcelHelper();

							int slidesCount = outputControl.Grid.GetLength(0);
							for (int k = 0; k < slidesCount; k++)
							{
								Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
								bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && outputControl.ShowAdSpecsOnlyOnLastSlide) || outputControl.DoNotShowAdSpecs;
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
												case "HEADER":
													shape.TextFrame.TextRange.Text = outputControl.Header;
													break;
												case "FLTDT1":
													shape.TextFrame.TextRange.Text = outputControl.FlightDates;
													break;
												case "SIGLINE":
													if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "SIGAPPROVAL":
													if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "EXCELGRID":
													if (excelHelper.Connect())
													{
														if (excelHelper.GetDetailedGrid(outputControl, k))
														{
															try
															{
																ShapeRange shapeRange = null;
																if (pasteGridAsImage)
																	shapeRange = slide.Shapes.PasteSpecial(DataType: PpPasteDataType.ppPasteEnhancedMetafile);
																else
																	shapeRange = slide.Shapes.PasteSpecial(DataType: PpPasteDataType.ppPasteOLEObject);
																shapeRange.Top = shape.Top;
																shapeRange.Left = shape.Left;
																shapeRange.Width = shape.Width;
																shape.Visible = MsoTriState.msoFalse;
															}
															catch { }
														}
														excelHelper.Disconnect();
													}
													break;
												default:
													for (int j = 0; j < 6; j++)
													{
														if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}", j + 1)))
														{
															if (j < outputControl.AdSpecs.Length && !hideAdSpecsOnSlide)
																shape.TextFrame.TextRange.Text = outputControl.AdSpecs[j];
															else
																shape.Visible = MsoTriState.msoFalse;
														}
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

		public void PrepareDetailedGridExcelBasedEmail(string fileName, PublicationDetailedGridControl[] outputControls, bool pasteAsImage)
		{
			try
			{
				Presentations presentations = _powerPointObject.Presentations;
				Presentation presentation = presentations.Add(MsoTriState.msoFalse);
				presentation.PageSetup.SlideWidth = (float)NewBizWiz.Core.Common.SettingsManager.Instance.SizeWidth * 72;
				presentation.PageSetup.SlideHeight = (float)NewBizWiz.Core.Common.SettingsManager.Instance.SizeHeght * 72;
				switch (NewBizWiz.Core.Common.SettingsManager.Instance.Orientation)
				{
					case "Landscape":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
						break;
					case "Portrait":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
						break;
				}
				NewBizWiz.Core.Common.Utilities.Instance.ReleaseComObject(presentations);
				foreach (PublicationDetailedGridControl outputControl in outputControls)
					AppendDetailedGridExcelBased(outputControl, pasteAsImage, presentation);
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

				NewBizWiz.Core.Common.Utilities.Instance.ReleaseComObject(presentation);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void AppendDetailedGridGridBased(PublicationDetailedGridControl outputControl, Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.DetailedGridGridBasedTemlatesFolderPath))
			{
				try
				{
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						int slidesCount = outputControl.OutputReplacementsLists.Count;
						int rowsCount = slidesCount > 0 ? outputControl.Grid[0].GetLength(0) : 0;
						for (int k = 0; k < slidesCount; k++)
						{
							string presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.DetailedGridGridBasedTemlatesFolderPath, string.Format(OutputManager.DetailedGridGridBasedSlideTemplate, new object[] { Controller.Instance.Grids.DetailedGrid.SelectedColumnsCount, (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader ? "adnotes" : "no_adnotes"), rowsCount }));
							int currentSlideRowsCount = outputControl.Grid[k].GetLength(0);
							if (File.Exists(presentationTemplatePath))
							{
								Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
								bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && outputControl.ShowAdSpecsOnlyOnLastSlide) || outputControl.DoNotShowAdSpecs;
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
												case "HEADER":
													shape.TextFrame.TextRange.Text = outputControl.Header;
													break;
												case "FLTDT1":
													shape.TextFrame.TextRange.Text = outputControl.FlightDates;
													break;
												case "SIGLINE":
													if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "SIGAPPROVAL":
													if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												default:
													for (int j = 0; j < 6; j++)
													{
														if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}", j + 1)))
														{
															if (j < outputControl.AdSpecs.Length && !hideAdSpecsOnSlide)
																shape.TextFrame.TextRange.Text = outputControl.AdSpecs[j];
															else
																shape.Visible = MsoTriState.msoFalse;
														}
													}
													break;
											}
										}
										if (shape.HasTable == MsoTriState.msoTrue)
										{
											Table table = shape.Table;
											int tableRowsCount = table.Rows.Count;
											int tableColumnsCount = table.Columns.Count;
											int deletedRows = 0;
											for (int i = 1; i <= tableRowsCount; i++)
											{
												if (i <= (currentSlideRowsCount * (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader ? 2 : 1)) + 1)
												{
													for (int j = 1; j <= tableColumnsCount; j++)
													{
														var tableShape = table.Cell(i, j).Shape;
														if (tableShape.HasTextFrame == MsoTriState.msoTrue)
														{
															string cellText = tableShape.TextFrame.TextRange.Text.Trim();
															if (outputControl.OutputReplacementsLists[k].Keys.Contains(cellText))
															{
																tableShape.TextFrame.TextRange.Text = outputControl.OutputReplacementsLists[k][cellText];
																outputControl.OutputReplacementsLists[k].Remove(cellText);
															}
														}
													}
												}
												else
												{
													table.Rows[i - deletedRows].Delete();
													deletedRows++;
												}
											}

											deletedRows = 0;
											tableRowsCount = table.Rows.Count;
											tableColumnsCount = table.Columns.Count;
											for (int i = 1; i <= tableRowsCount; i++)
											{
												for (int j = tableColumnsCount; j >= 1; j--)
												{
													var tableShape = table.Cell(i, j).Shape;
													if (tableShape.HasTextFrame == MsoTriState.msoTrue)
													{
														var cellText = tableShape.TextFrame.TextRange.Text.Trim();
														if (cellText.Equals("Merge"))
														{
															tableShape.TextFrame.TextRange.Text = string.Empty;
															var prevRowIndex = j - 1;
															if (prevRowIndex >= 1)
															{
																table.Cell(i, prevRowIndex).Merge(table.Cell(i, j));
																table.Cell(i, prevRowIndex).Shape.TextFrame.TextRange.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignLeft;
															}
														}
													}
												}
											}
											for (int i = 1; i <= tableRowsCount; i++)
											{
												var tableShape = table.Cell(i - deletedRows, 1).Shape;
												if (tableShape.HasTextFrame == MsoTriState.msoTrue)
												{
													var cellText = tableShape.TextFrame.TextRange.Text.Trim();
													if (cellText.Equals("MergeComment"))
													{
														table.Rows[i - deletedRows].Delete();
														deletedRows++;
													}
												}
											}
										}
									}
								}
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
		}

		public void PrepareDetailedGridGridBasedEmail(string fileName, PublicationDetailedGridControl[] outputControls)
		{
			try
			{
				Presentations presentations = _powerPointObject.Presentations;
				Presentation presentation = presentations.Add(MsoTriState.msoFalse);
				presentation.PageSetup.SlideWidth = (float)NewBizWiz.Core.Common.SettingsManager.Instance.SizeWidth * 72;
				presentation.PageSetup.SlideHeight = (float)NewBizWiz.Core.Common.SettingsManager.Instance.SizeHeght * 72;
				switch (NewBizWiz.Core.Common.SettingsManager.Instance.Orientation)
				{
					case "Landscape":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
						break;
					case "Portrait":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
						break;
				}
				NewBizWiz.Core.Common.Utilities.Instance.ReleaseComObject(presentations);
				foreach (PublicationDetailedGridControl outputControl in outputControls)
					AppendDetailedGridGridBased(outputControl, presentation);
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

				NewBizWiz.Core.Common.Utilities.Instance.ReleaseComObject(presentation);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}
}