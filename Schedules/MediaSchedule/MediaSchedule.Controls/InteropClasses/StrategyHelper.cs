﻿using System;
using System.IO;
using System.Threading;
using Asa.MediaSchedule.Controls.PresentationClasses.Summary;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Asa.Core.Interop;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.MediaSchedule.Controls.InteropClasses
{
	public partial class MediaSchedulePowerPointHelper<T> where T : class,new()
	{
		public void AppendStrategy(StrategySummaryControl strategy, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slidesCount = strategy.OutputReplacementsLists.Count;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = BusinessObjects.Instance.OutputManager.GetStartegyFile(strategy.ItemsPerSlide);
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
											//shape.TextFrame.TextRange.Text = strategy.Title;
											break;
										default:
											var startIndex = k * strategy.ItemsPerSlide;
											for (var j = 0; j < strategy.ItemsPerSlide; j++)
											{
												if (shape.Tags.Name(i).Equals(String.Format("STRATLOGO{0}", j + 1)))
												{
													if ((j + startIndex) < strategy.ItemsCount &&
														strategy.ItemLogos[j + startIndex].ContainsData)
													{
														var itemLogo = strategy.ItemLogos[j + startIndex];

														var originalWidth = itemLogo.BigImage.Width;
														var originalHeight = itemLogo.BigImage.Height;
														var percentWidth = shape.Width / originalWidth;
														var percentHeight = shape.Height / originalHeight;
														var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
														var shapeWidth = originalWidth * percent;
														var shapeHeight = originalHeight * percent;
														slide.Shapes.AddPicture(itemLogo.OutputFilePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top, shapeWidth, shapeHeight);
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
										var tableShape = table.Cell(i, j).Shape;
										if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
										var cellText = tableShape.TextFrame.TextRange.Text.Trim();
										if (!strategy.OutputReplacementsLists[k].ContainsKey(cellText)) continue;
										tableShape.TextFrame.TextRange.Text = strategy.OutputReplacementsLists[k][cellText];
										strategy.OutputReplacementsLists[k].Remove(cellText);
									}
								}
								for (var i = 1; i <= tableRowsCount; i++)
								{
									var tableShape = table.Cell(i, 1).Shape;
									if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
									var cellText = tableShape.TextFrame.TextRange.Text.Trim();
									if (!cellText.Equals("DeleteRow")) continue;
									shape.Visible = MsoTriState.msoFalse;
									break;
								}
							}

							if (strategy.ContractSettings.IsConfigured)
								FillContractInfo(slide, strategy.ContractSettings, BusinessObjects.Instance.OutputManager.ContractTemplateFolder);
						}

						var selectedTheme = strategy.SelectedTheme;
						if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.GetThemePath());

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

		public void PrepareStrategyEmail(string fileName, StrategySummaryControl strategy)
		{
			PreparePresentation(fileName, presentation => AppendStrategy(strategy, presentation));
		}

		public void PrepareStrategyPdf(string targetFileName, StrategySummaryControl strategy)
		{
			var sourceFileName = Path.GetTempFileName();
			PreparePresentation(sourceFileName, presentation => AppendStrategy(strategy, presentation));
			BuildPdf(sourceFileName, targetFileName);
		}
	}
}
