using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendDetailedGridGridBased(PublicationDetailedGridControl[] outputControls, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(BusinessWrapper.Instance.OutputManager.DetailedGridGridBasedTemlatesFolderPath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					foreach (var outputControl in outputControls)
					{
						MessageFilter.Register();
						var slidesCount = outputControl.OutputReplacementsLists.Count;
						var rowsCount = slidesCount > 0 ? outputControl.Grid[0].GetLength(0) : 0;
						for (int k = 0; k < slidesCount; k++)
						{
							var presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.DetailedGridGridBasedTemlatesFolderPath, string.Format(OutputManager.DetailedGridGridBasedSlideTemplate, new object[] { Controller.Instance.Grids.DetailedGrid.SelectedColumnsCount, (Controller.Instance.Grids.DetailedGrid.ShowCommentsHeader ? "adnotes" : "no_adnotes"), rowsCount }));
							var currentSlideRowsCount = outputControl.Grid[k].GetLength(0);
							if (!File.Exists(presentationTemplatePath)) continue;
							var presentation = PowerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
											case "HEADER":
												shape.TextFrame.TextRange.Text = outputControl.Header;
												break;
											case "SIGLINE":
												if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
													shape.Visible = MsoTriState.msoFalse;
												break;
											case "SIGAPPROVAL":
												if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
													shape.Visible = MsoTriState.msoFalse;
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
											if (!outputControl.OutputReplacementsLists[k].Keys.Contains(cellKey)) continue;
											var cellText = outputControl.OutputReplacementsLists[k][cellKey];
											if (!cellText.Equals("Merge"))
											{
												tableShape.TextFrame.TextRange.Text = cellText;
												outputControl.OutputReplacementsLists[k].Remove(cellKey);
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
							var selectedTheme = outputControl.SelectedTheme;
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

		public void PrepareDetailedGridGridBasedEmail(string fileName, PublicationDetailedGridControl[] outputControls)
		{
			PreparePresentation(fileName, presentation => AppendDetailedGridGridBased(outputControls, presentation));
		}
	}
}