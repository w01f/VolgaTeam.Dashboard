using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.CommonGUI.Summary;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.CommonGUI.Interop
{
	public partial class CommonPowerPointHelper<T> where T : class, new()
	{
		public void AppendSummary(ISummaryControl summary, Presentation destinationPresentation = null)
		{
			if (summary.TableOutput)
			{
				summary.PopulateReplacementsList();
				AppendTableSummary(summary, destinationPresentation);
			}
			else
				AppendSlideSummary(summary, destinationPresentation);
		}

		private void AppendSlideSummary(ISummaryControl summary, Presentation destinationPresentation = null)
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
					var presentation = PowerPointObject.Presentations.Open(mainPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
										case "SUMMARYDATA":
											shape.TextFrame.TextRange.Text = summary.SummaryData;
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
					var presentation = PowerPointObject.Presentations.Open(additionalPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
									case "SUMMARYDATA":
										shape.TextFrame.TextRange.Text = summary.SummaryData;
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

		private void AppendTableSummary(ISummaryControl summary, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryTableFolder)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slidesCount = summary.OutputReplacementsLists.Count;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryTableFolder, String.Format(MasterWizardManager.SimpleSummaryTableTemplate, summary.ItemsPerTable));
						if (!File.Exists(presentationTemplatePath)) continue;
						var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						foreach (Slide slide in presentation.Slides)
						{
							foreach (Shape shape in slide.Shapes)
							{
								for (var i = 1; i <= shape.Tags.Count; i++)
								{
									switch (shape.Tags.Name(i))
									{
										case "HEADER":
											shape.TextFrame.TextRange.Text = summary.Title;
											break;
										case "SUMMARYDATA":
											shape.TextFrame.TextRange.Text = summary.SummaryData;
											break;
										default:
											var startIndex = k * summary.ItemsPerTable;
											for (var j = 0; j < summary.ItemsPerTable; j++)
											{
												if (shape.Tags.Name(i).Equals(String.Format("ICON{0}", j + 1)))
												{
													if (summary.ShowIcons &&
														(j + startIndex) < summary.ItemsCount &&
														!String.IsNullOrEmpty(summary.TableIcons[j + startIndex]))
													{
														var filePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.SimpleSummaryTableIconFolder, summary.TableIcons[j + startIndex]);
														slide.Shapes.AddPicture(filePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top, shape.Width, shape.Height);
													}
													shape.Visible = MsoTriState.msoFalse;
												}
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
										if (j > table.Columns.Count) continue;
										var tableShape = table.Cell(i, j).Shape;
										if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
										var cellText = tableShape.TextFrame.TextRange.Text.Trim();
										if (!cellText.Contains("Product") || summary.ShowIcons) continue;
										var prevColumnIndex = j - 1;
										if (prevColumnIndex < 1) continue;
										table.Cell(i, j).Merge(table.Cell(i, prevColumnIndex));
									}
								}

								tableRowsCount = table.Rows.Count;
								tableColumnsCount = table.Columns.Count;
								for (var i = 1; i <= tableRowsCount; i++)
								{
									for (var j = 1; j <= tableColumnsCount; j++)
									{
										var tableShape = table.Cell(i, j).Shape;
										if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
										var cellText = tableShape.TextFrame.TextRange.Text.Trim();
										if (!summary.OutputReplacementsLists[k].ContainsKey(cellText)) continue;
										tableShape.TextFrame.TextRange.Text = summary.OutputReplacementsLists[k][cellText];
										summary.OutputReplacementsLists[k].Remove(cellText);
									}
								}

								for (var i = tableRowsCount; i >= 1; i--)
								{
									var tableShape = table.Cell(i, 1).Shape;
									if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
									var cellText = tableShape.TextFrame.TextRange.Text.Trim();
									if (!cellText.Equals("DeleteRow")) continue;
									try { table.Rows[i].Delete(); }
									catch { shape.Visible = MsoTriState.msoFalse; }
								}
							}
						}
						var selectedTheme = summary.SelectedTheme;
						if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.ThemeFilePath);
						AppendSlide(presentation, -1, destinationPresentation);
						presentation.Close();
					}
				});
				thread.Start();

				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void PrepareSummaryEmail(string fileName, ISummaryControl summary)
		{
			PreparePresentation(fileName, presentation => AppendSummary(summary, presentation));
		}
	}
}
