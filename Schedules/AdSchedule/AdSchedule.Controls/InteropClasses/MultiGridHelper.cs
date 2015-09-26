using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendMultiGridGridBased(Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.MultiGridGridBasedTemlatesFolderPath))
			{
				try
				{
					var publicationLogos = Controller.Instance.Grids.MultiGrid.PublicationLogos;
					var publicationLogosCount = publicationLogos.Length;
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();

						var slidesCount = Controller.Instance.Grids.MultiGrid.OutputReplacementsLists.Count;
						var rowsCount = slidesCount > 0 ? Controller.Instance.Grids.MultiGrid.Grid[0].GetLength(0) : 0;
						for (var k = 0; k < slidesCount; k++)
						{
							string presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.MultiGridGridBasedTemlatesFolderPath, string.Format(OutputManager.MultiGridGridBasedSlideTemplate, new object[] { Controller.Instance.Grids.MultiGrid.SelectedColumnsCount, (Controller.Instance.Grids.MultiGrid.ShowCommentsHeader ? "adnotes" : "no_adnotes"), rowsCount }));
							int currentSlideRowsCount = Controller.Instance.Grids.MultiGrid.Grid[k].GetLength(0);
							if (File.Exists(presentationTemplatePath))
							{
								Presentation presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
								bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && Controller.Instance.Grids.MultiGrid.ShowAdSpecsOnlyOnLastSlide) || Controller.Instance.Grids.MultiGrid.DoNotShowAdSpecs;
								foreach (Slide slide in presentation.Slides)
								{
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count; i++)
										{
											switch (shape.Tags.Name(i))
											{
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

										if (shape.HasTable != MsoTriState.msoTrue) continue;
										var table = shape.Table;
										var tableRowsCount = table.Rows.Count;
										var tableColumnsCount = table.Columns.Count;

										for (var i = 1; i <= tableRowsCount; i++)
										{
											for (var j = 1; j <= tableColumnsCount; j++)
											{
												var tableShape = table.Cell(i, j).Shape;
												if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
												var cellKey = tableShape.TextFrame.TextRange.Text.Trim();
												if (!Controller.Instance.Grids.MultiGrid.OutputReplacementsLists[k].Keys.Contains(cellKey)) continue;
												var cellText = Controller.Instance.Grids.MultiGrid.OutputReplacementsLists[k][cellKey];
												if (!cellText.Equals("Merge"))
												{
													tableShape.TextFrame.TextRange.Text = cellText;
													Controller.Instance.Grids.MultiGrid.OutputReplacementsLists[k].Remove(cellKey);
												}
												else
												{
													tableShape.TextFrame.TextRange.Text = String.Empty;
													var nextColumnIndex = j - 1;
													if (nextColumnIndex >= tableColumnsCount) continue;
													table.Cell(i, j).Merge(table.Cell(i, nextColumnIndex));
													table.Cell(i, j).Shape.TextFrame.TextRange.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignLeft;
												}
											}
										}

										var deletedRows = 0;
										var dataStart = 4 + currentSlideRowsCount * (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader ? 2 : 1);
										var dataEnd = tableRowsCount - 1;
										for (var i = dataStart; i <= dataEnd; i++)
										{
											table.Rows[i - deletedRows].Delete();
											deletedRows++;
										}

										tableRowsCount = table.Rows.Count;
										deletedRows = 0;
										for (var i = 1; i <= tableRowsCount; i++)
										{
											var tableShape = table.Cell(i - deletedRows, 1).Shape;
											if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
											var cellText = tableShape.TextFrame.TextRange.Text.Trim();
											if (!cellText.Equals("MergeComment")) continue;
											table.Rows[i - deletedRows].Delete();
											deletedRows++;
										}
									}
								}
								var selectedTheme = Controller.Instance.Grids.MultiGrid.SelectedTheme;
								if (selectedTheme != null)
									presentation.ApplyTheme(selectedTheme.ThemeFilePath);
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

		public void PrepareMultiGridGridBasedEmail(string targetFileName)
		{
			PreparePresentation(targetFileName, AppendMultiGridGridBased);
		}

		public void PrepareMultiGridGridBasedPdf(string targetFileName)
		{
			var sourceFileName = Path.GetTempFileName();
			PreparePresentation(sourceFileName, AppendMultiGridGridBased);
			BuildPdf(sourceFileName, targetFileName);
		}
	}
}