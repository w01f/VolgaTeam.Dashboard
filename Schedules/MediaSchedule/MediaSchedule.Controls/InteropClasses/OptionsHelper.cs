﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.PresentationClasses.OptionsControls;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.Core.OfficeInterops;
using Theme = Asa.Common.Core.Objects.Themes.Theme;

namespace Asa.Media.Controls.InteropClasses
{
	public partial class MediaSchedulePowerPointHelper<T> where T : class,new()
	{
		public void AppendOptions(IEnumerable<IOptionsSlideControl> pages, Theme selectedTheme, bool pasteToSlideMaster, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					foreach (var page in pages)
					{
						var slideNumber = 0;
						foreach (var pageDictionary in page.ReplacementsList)
						{
							var copyOfReplacementList = new Dictionary<string, string>(pageDictionary);
							var presentationTemplatePath = page.TemplateFilePath;
							if (!File.Exists(presentationTemplatePath)) return;
							var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
							var taggedSlide = presentation.Slides.Count > 0 ? presentation.Slides[1] : null;

							var logoShapes = new List<Shape>();
							if (page.Logos != null && slideNumber < page.Logos.Length)
							{
								var slideLogos = page.Logos[slideNumber];
								foreach (var shape in taggedSlide.Shapes.OfType<Shape>().Where(s => s.HasTable != MsoTriState.msoTrue))
								{
									for (var i = 1; i <= shape.Tags.Count; i++)
									{
										var shapeTagName = shape.Tags.Name(i);
										for (var j = 0; j < slideLogos.Length; j++)
										{
											if (!shapeTagName.Equals(string.Format("STATIONLOGO{0}", j + 1))) continue;
											string fileName = slideLogos[j];
											if (!String.IsNullOrEmpty(fileName) && File.Exists(fileName))
												logoShapes.Add(taggedSlide.Shapes.AddPicture(fileName, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top, shape.Width, shape.Height));
											shape.Visible = MsoTriState.msoFalse;
										}
									}
								}
							}

							var tableContainer = taggedSlide.Shapes.OfType<Shape>().FirstOrDefault(s => s.HasTable == MsoTriState.msoTrue);
							if (tableContainer == null) return;
							var table = tableContainer.Table;
							var tableRowsCount = table.Rows.Count;
							for (var i = 1; i <= tableRowsCount; i++)
							{
								for (var j = 1; j <= table.Columns.Count; j++)
								{
									var tableShape = table.Cell(i, j).Shape;
									if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
									var cellText = tableShape.TextFrame.TextRange.Text.Trim();
									var key = copyOfReplacementList.Keys.FirstOrDefault(k => k.Trim().ToLower().Equals(cellText.ToLower()));
									if (String.IsNullOrEmpty(key)) continue;
									tableShape.TextFrame.TextRange.Text = copyOfReplacementList[key];
									copyOfReplacementList.Remove(key);
								}
							}
							tableRowsCount = table.Rows.Count;
							for (var i = tableRowsCount; i >= 1; i--)
							{
								for (var j = 1; j < 3; j++)
								{
									var tableShape = table.Cell(i, j).Shape;
									if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
									var cellText = tableShape.TextFrame.TextRange.Text.Trim();
									if (!cellText.Equals("Delete Row")) continue;
									table.Rows[i].Delete();
									break;
								}
							}

							var tableColumnsCount = table.Columns.Count;
							if (page.ColumnWidths != null)
								for (var i = 0; i < page.ColumnWidths.Length; i++)
									if ((i + 2) <= tableColumnsCount)
										table.Columns[i + 2].Width = page.ColumnWidths[i] * 72.27f;

							tableColumnsCount = table.Columns.Count;
							for (var i = tableColumnsCount - 1; i >= 0; i--)
							{
								var tableShape = table.Cell(4, i + 1).Shape;
								if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
								var cellText = tableShape.TextFrame.TextRange.Text.Trim();
								if (cellText.Equals("Delete Column"))
									table.Columns[i + 1].Delete();
							}

							if (pasteToSlideMaster)
							{
								var newSlide = presentation.Slides.Add(1, PpSlideLayout.ppLayoutBlank);
								Design design;
								if (selectedTheme != null)
								{
									presentation.ApplyTheme(selectedTheme.GetThemePath());
									design = presentation.Designs[presentation.Designs.Count];
									design.Name = DateTime.Now.ToString("MMddyy-hhmmsstt");
								}
								else
									design = presentation.Designs.Add(DateTime.Now.ToString("MMddyy-hhmmsstt"));
								tableContainer.Copy();
								design.SlideMaster.Shapes.Paste();
								foreach (var logoShape in logoShapes)
								{
									logoShape.Copy();
									design.SlideMaster.Shapes.Paste();
								}

								if (page.ContractSettings.IsConfigured)
									FillContractInfo(design, page.ContractSettings, BusinessObjects.Instance.OutputManager.ContractTemplateFolder);

								newSlide.Design = design;
							}
							else
							{
								if (selectedTheme != null)
									presentation.ApplyTheme(selectedTheme.GetThemePath());

								if (page.ContractSettings.IsConfigured)
									FillContractInfo(taggedSlide, page.ContractSettings, BusinessObjects.Instance.OutputManager.ContractTemplateFolder);
							}
							AppendSlide(presentation, 1, destinationPresentation);
							presentation.Close();
							slideNumber++;
						}
					}
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void PrepareOptionsEmail(string fileName, IEnumerable<IOptionsSlideControl> pages, Theme selectedTheme, bool pasteToSlideMaster)
		{
			PreparePresentation(fileName, presentation => AppendOptions(pages, selectedTheme, pasteToSlideMaster, presentation));
		}
	}
}