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
		public bool GetMultiGrid(int slideIndex)
		{
			bool result = false;
			try
			{
				Workbook workBook = _excelObject.Workbooks.Open(BusinessWrapper.Instance.OutputManager.ExcelOutputTemplateFilePath);
				Worksheet workSheet = workBook.Sheets[Controller.Instance.Grids.MultiGrid.ShowCommentsHeader ? OutputManager.MultiGridTemplateSheetNameWithNotes : OutputManager.MultiGridTemplateSheetNameWithoutNotes];

				string[][] gridData = Controller.Instance.Grids.MultiGrid.Grid[slideIndex];
				string[] gridHeaders = Controller.Instance.Grids.MultiGrid.GridHeaders;
				int[] gridHeaderSizes = Controller.Instance.Grids.MultiGrid.GridHeaderSizes;

				int columnsCount = gridHeaders.Length;
				int columnsPerRecord = 3;
				int rowsCount = gridData.Length;
				int excelRowsCount = 2 + rowsCount * columnsPerRecord - (Controller.Instance.Grids.MultiGrid.ShowCommentsHeader ? 1 : 2);
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
								workSheet.Range[GetColumnLetterByIndex(i) + excelMainRowIndex.ToString()].Formula = gridData[j][i + (Controller.Instance.Grids.MultiGrid.ShowCommentsHeader && !digitalLegend ? 1 : 0)];
								if (Controller.Instance.Grids.MultiGrid.ShowCommentsHeader && !digitalLegend)
									workSheet.Range[GetColumnLetterByIndex(i) + excelCommentRowIndex.ToString()].Formula = gridData[j][0];
							}
							else
								workSheet.Range[GetColumnLetterByIndex(i) + excelMainRowIndex.ToString()].Formula = String.Empty;
							if (digitalLegend)
							{
								if (Controller.Instance.Grids.MultiGrid.ShowCommentsHeader)
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
		public void AppendMultiGridExcelBased(bool pasteGridAsImage, Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.MultiGridExcelBasedTemlatesFolderPath))
			{
				string presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.MultiGridExcelBasedTemlatesFolderPath, string.Format(OutputManager.MultiGridExcelBasedSlideTemplate, Controller.Instance.Grids.MultiGrid.OutputFileIndex, Controller.Instance.Grids.MultiGrid.GridHeaders.Length));
				if (File.Exists(presentationTemplatePath))
				{
					try
					{
						string[][] publicationLogos = Controller.Instance.Grids.MultiGrid.PublicationLogos;
						var publicationLogosCount = publicationLogos.Length;
						var thread = new Thread(delegate()
						{
							MessageFilter.Register();

							var excelHelper = new AdScheduleExcelHelper();

							int slidesCount = Controller.Instance.Grids.MultiGrid.Grid.GetLength(0);
							for (int k = 0; k < slidesCount; k++)
							{
								Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
								bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && Controller.Instance.Grids.MultiGrid.ShowAdSpecsOnlyOnLastSlide) || Controller.Instance.Grids.MultiGrid.DoNotShowAdSpecs;
								foreach (Slide slide in presentation.Slides)
								{
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "DATETAG":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.PresentationDate;
													break;
												case "ADVERTISER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.BusinessName;
													break;
												case "DECISIONMAKER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.DecisionMaker;
													break;
												case "HEADER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.Header;
													break;
												case "FLTDT1":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.FlightDates;
													break;
												case "SIGLINE":
													if (!Controller.Instance.Grids.MultiGrid.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "SIGAPPROVAL":
													if (!Controller.Instance.Grids.MultiGrid.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "EXCELGRID":
													if (excelHelper.Connect())
													{
														if (excelHelper.GetMultiGrid(k))
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
													var shapeTagName = shape.Tags.Name(i);
													for (int j = 0; j < 6; j++)
													{
														if (shapeTagName.Equals(string.Format("ADSPEC{0}", j + 1)))
														{
															if (j < Controller.Instance.Grids.MultiGrid.AdSpecs.Length && !hideAdSpecsOnSlide)
																shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.AdSpecs[j];
															else
																shape.Visible = MsoTriState.msoFalse;
														}
													}

													if (k < publicationLogosCount)
													{
														for (int j = 0; j < publicationLogos[k].Length; j++)
														{
															if (shapeTagName.Equals(string.Format("LOGO{0}", j + 1)))
															{
																string fileName = publicationLogos[k][j];
																if (!string.IsNullOrEmpty(fileName))
																	slide.Shapes.AddPicture(FileName: fileName, LinkToFile: MsoTriState.msoFalse, SaveWithDocument: MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
																shape.Visible = MsoTriState.msoFalse;
															}
														}
													}
													else
													{
														if (shapeTagName.Contains("LOGO"))
															shape.Visible = MsoTriState.msoFalse;
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

		public void PrepareMultiGridExcelBasedEmail(string fileName, bool pasteAsImage)
		{
			try
			{
				SavePrevSlideIndex();
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
				AppendMultiGridExcelBased(pasteAsImage, presentation);
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
				RestorePrevSlideIndex();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void AppendMultiGridGridBased(Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.MultiGridGridBasedTemlatesFolderPath))
			{
				try
				{
					string[][] publicationLogos = Controller.Instance.Grids.MultiGrid.PublicationLogos;
					var publicationLogosCount = publicationLogos.Length;
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();

						int slidesCount = Controller.Instance.Grids.MultiGrid.OutputReplacementsLists.Count;
						int rowsCount = slidesCount > 0 ? Controller.Instance.Grids.MultiGrid.Grid[0].GetLength(0) : 0;
						for (int k = 0; k < slidesCount; k++)
						{
							string presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.MultiGridGridBasedTemlatesFolderPath, string.Format(OutputManager.MultiGridGridBasedSlideTemplate, new object[] { Controller.Instance.Grids.MultiGrid.SelectedColumnsCount, (Controller.Instance.Grids.MultiGrid.ShowCommentsHeader ? "adnotes" : "no_adnotes"), rowsCount }));
							int currentSlideRowsCount = Controller.Instance.Grids.MultiGrid.Grid[k].GetLength(0);
							if (File.Exists(presentationTemplatePath))
							{
								Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
								bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && Controller.Instance.Grids.MultiGrid.ShowAdSpecsOnlyOnLastSlide) || Controller.Instance.Grids.MultiGrid.DoNotShowAdSpecs;
								foreach (Slide slide in presentation.Slides)
								{
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "DATETAG":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.PresentationDate;
													break;
												case "ADVERTISER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.BusinessName;
													break;
												case "DECISIONMAKER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.DecisionMaker;
													break;
												case "HEADER":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.Header;
													break;
												case "FLTDT1":
													shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.FlightDates;
													break;
												case "SIGLINE":
													if (!Controller.Instance.Grids.MultiGrid.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												case "SIGAPPROVAL":
													if (!Controller.Instance.Grids.MultiGrid.ShowSignatureLine || hideAdSpecsOnSlide)
														shape.Visible = MsoTriState.msoFalse;
													break;
												default:
													var shapeTagName = shape.Tags.Name(i);
													for (int j = 0; j < 6; j++)
													{
														if (shapeTagName.Equals(string.Format("ADSPEC{0}", j + 1)))
														{
															if (j < Controller.Instance.Grids.MultiGrid.AdSpecs.Length && !hideAdSpecsOnSlide)
																shape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.AdSpecs[j];
															else
																shape.Visible = MsoTriState.msoFalse;
														}
													}

													if (k < publicationLogosCount)
													{
														for (int j = 0; j < publicationLogos[k].Length; j++)
														{
															if (shapeTagName.Equals(string.Format("LOGO{0}", j + 1)))
															{
																string fileName = publicationLogos[k][j];
																if (!string.IsNullOrEmpty(fileName))
																	slide.Shapes.AddPicture(FileName: fileName, LinkToFile: MsoTriState.msoFalse, SaveWithDocument: MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
																shape.Visible = MsoTriState.msoFalse;
															}
														}
													}
													else
													{
														if (shapeTagName.Contains("LOGO"))
															shape.Visible = MsoTriState.msoFalse;
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
												if (i <= (currentSlideRowsCount * (Controller.Instance.Grids.MultiGrid.ShowCommentsHeader ? 2 : 1)) + 1)
												{
													for (int j = 1; j <= tableColumnsCount; j++)
													{
														Shape tableShape = table.Cell(i, j).Shape;
														if (tableShape.HasTextFrame == MsoTriState.msoTrue)
														{
															string cellText = tableShape.TextFrame.TextRange.Text.Trim();
															if (Controller.Instance.Grids.MultiGrid.OutputReplacementsLists[k].Keys.Contains(cellText))
															{
																tableShape.TextFrame.TextRange.Text = Controller.Instance.Grids.MultiGrid.OutputReplacementsLists[k][cellText];
																Controller.Instance.Grids.MultiGrid.OutputReplacementsLists[k].Remove(cellText);
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

		public void PrepareMultiGridGridBasedEmail(string fileName)
		{
			try
			{
				SavePrevSlideIndex();
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
				AppendMultiGridGridBased(presentation);
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
				RestorePrevSlideIndex();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}
}