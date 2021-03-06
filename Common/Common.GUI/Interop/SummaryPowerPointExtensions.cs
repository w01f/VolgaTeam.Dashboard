﻿using System;
using System.IO;
using System.Threading;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Summary;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Common.GUI.Interop
{
	public static class SummaryPowerPointExtensions
	{
		public static void AppendSummary(this PowerPointProcessor target, ISummaryControl summary, bool tabelOutput, Presentation destinationPresentation = null)
		{
			if (tabelOutput)
			{
				summary.PopulateReplacementsList();
				target.AppendTableSummary(summary, destinationPresentation);
			}
			else
				target.AppendSlideSummary(summary, destinationPresentation);
		}

		private static void AppendSlideSummary(this PowerPointProcessor target, ISummaryControl summary, Presentation destinationPresentation = null)
		{
			var itemsCount = summary.ItemsCount;
			var mainFileTemplateIndex = itemsCount >= SummaryConstants.MaxOneSheetItems ? SummaryConstants.MaxOneSheetItems : itemsCount;

			var additionalFileTemplateIndex = itemsCount > SummaryConstants.MaxOneSheetItems ? itemsCount % SummaryConstants.MaxOneSheetItems : 0;

			var mainFilesCount = itemsCount / SummaryConstants.MaxOneSheetItems;
			if (mainFilesCount == 0 && itemsCount > 0)
				mainFilesCount++;

			try
			{
				var thread = new Thread(delegate ()
				{
					var mainPresentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetSimpleSummaryTemlateFile(String.Format(MasterWizardManager.SimpleSummarySlideTemplate, mainFileTemplateIndex));
					MessageFilter.Register();
					var presentation = target.PowerPointObject.Presentations.Open(mainPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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

							if (summary.ContractSettings.IsConfigured &&
								summary.ContractTemplateFolder != null &&
								summary.ContractTemplateFolder.ExistsLocal())
								target.FillContractInfo(slide, summary.ContractSettings, summary.ContractTemplateFolder);
						}
						var selectedTheme = summary.SelectedTheme;
						if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.GetThemePath());
						target.AppendSlide(presentation, -1, destinationPresentation);
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

			if (additionalFileTemplateIndex == 0) return;
			try
			{
				var thread = new Thread(delegate ()
				{
					var additionalPresentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetSimpleSummaryTemlateFile(String.Format(MasterWizardManager.SimpleSummarySlideTemplate, (additionalFileTemplateIndex + mainFileTemplateIndex)));

					MessageFilter.Register();
					var presentation = target.PowerPointObject.Presentations.Open(additionalPresentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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

						if (summary.ContractSettings.IsConfigured &&
							summary.ContractTemplateFolder != null &&
							summary.ContractTemplateFolder.ExistsLocal())
							target.FillContractInfo(slide, summary.ContractSettings, summary.ContractTemplateFolder);
					}
					var selectedTheme = summary.SelectedTheme;
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.GetThemePath());
					target.AppendSlide(presentation, -1, destinationPresentation);
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

		private static void AppendTableSummary(this PowerPointProcessor target, ISummaryControl summary, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var slidesCount = summary.OutputReplacementsLists.Count;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetSimpleSummaryTableFile(String.Format(MasterWizardManager.SimpleSummaryTableTemplate, summary.ItemsPerTable));
						if (!File.Exists(presentationTemplatePath)) continue;
						var presentation = target.PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
														var filePath = MasterWizardManager.Instance.SelectedWizard.GetSimpleSummaryIconFile(summary.TableIcons[j + startIndex]);
														var newShape = slide.Shapes.AddPicture(filePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top, shape.Width, shape.Height);
														newShape.Top = shape.Top;
														newShape.Left = shape.Left;
														newShape.Width = shape.Width;
														newShape.Height = shape.Height;
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
							presentation.ApplyTheme(selectedTheme.GetThemePath());
						target.AppendSlide(presentation, -1, destinationPresentation);
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

		public static void PrepareSummaryEmail(this PowerPointProcessor target, string fileName, ISummaryControl summary, bool tabelOutput)
		{
			target.PreparePresentation(fileName, presentation => target.AppendSummary(summary, tabelOutput, presentation));
		}

		public static void PrepareSummaryPdf(this PowerPointProcessor target, string targetFileName, ISummaryControl summary, bool tabelOutput)
		{
			var sourceFileName = Path.GetTempFileName();
			target.PreparePresentation(sourceFileName, presentation => target.AppendSummary(summary, tabelOutput, presentation));
			target.BuildPdf(sourceFileName, targetFileName);
		}

		public static bool InsertVideoIntoActivePresentation(this PowerPointProcessor target, string videoFile)
		{
			bool result;
			try
			{
				var thread = new Thread(delegate ()
				{
					var newFile = Path.Combine(Path.GetDirectoryName(target.GetActivePresentation().FullName), Path.GetFileName(videoFile));
					File.Copy(videoFile, newFile, true);
					var slide = target.GetActiveSlide();
					if (slide != null)
					{
						var shape = slide.Shapes.AddMediaObject2(newFile);
						shape.AnimationSettings.PlaySettings.PlayOnEntry = MsoTriState.msoTrue;
						shape.AnimationSettings.AdvanceTime = 0;
						float maxWidth = (target.GetActivePresentation().PageSetup.SlideWidth / 10) * 9; // 5% border
						if (maxWidth < shape.Width)
						{
							shape.LockAspectRatio = MsoTriState.msoTrue;
							shape.Width = maxWidth;
						}
						shape.Left = (target.GetActivePresentation().PageSetup.SlideWidth - shape.Width) / 2;
						shape.Top = (target.GetActivePresentation().PageSetup.SlideHeight - shape.Height) / 2;
					}
				});
				thread.Start();

				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();


				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private static Slide GetActiveSlide(this PowerPointProcessor target)
		{
			if (target.PowerPointObject.Windows.Count <= 0) return null;
			target.PowerPointObject.Activate();
			if (target.PowerPointObject.ActiveWindow == null) return null;
			target.PowerPointObject.ActiveWindow.ViewType = PpViewType.ppViewNormal;
			return (Slide)target.PowerPointObject.ActiveWindow.View.Slide;
		}
	}
}
