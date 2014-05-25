﻿using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Interop;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.MediaSchedule.Controls.InteropClasses
{
	public partial class MediaSchedulePowerPointHelper
	{
		public void AppendStrategy(ProgramStrategyControl strategy, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(BusinessWrapper.Instance.OutputManager.StrategyTemplatesFolderPath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slidesCount = strategy.OutputReplacementsLists.Count;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.StrategyTemplatesFolderPath, String.Format(OutputManager.StrategyTemplateFileName, strategy.ItemsPerSlide));
						if (!File.Exists(presentationTemplatePath)) continue;
						var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
														!String.IsNullOrEmpty(strategy.ItemLogos[j + startIndex]))
													{
														var filePath = strategy.ItemLogos[j + startIndex];
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
						}
						var selectedTheme = strategy.SelectedTheme;
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

		public void PrepareStrategyEmail(string fileName, ProgramStrategyControl strategy)
		{
			PreparePresentation(fileName, presentation => AppendStrategy(strategy, presentation));
		}
	}
}
