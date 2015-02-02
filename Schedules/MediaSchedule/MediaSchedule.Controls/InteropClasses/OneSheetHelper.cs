using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.MediaSchedule.Controls.InteropClasses
{
	public partial class MediaSchedulePowerPointHelper<T> where T : class,new()
	{
		protected abstract string OneSheetTemplatePath { get; }

		public void AppendOneSheet(IEnumerable<OutputSchedule> pages, Theme selectedTheme, bool pasteToSlideMaster, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(OneSheetTemplatePath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slideNumber = 0;
					foreach (var page in pages)
					{
						var copyOfReplacementList = new Dictionary<string, string>(page.ReplacementsList);
						var presentationTemplatePath = Path.Combine(OneSheetTemplatePath, page.TemplateFileName);
						if (!File.Exists(presentationTemplatePath)) return;
						var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						var taggedSlide = presentation.Slides.Count > 0 ? presentation.Slides[1] : null;

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
											taggedSlide.Shapes.AddPicture(fileName, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top, shape.Width, shape.Height);
										shape.Visible = MsoTriState.msoFalse;
									}
								}
							}
						}

						var tableContainer = taggedSlide.Shapes.Cast<Shape>().FirstOrDefault(s => s.HasTable == MsoTriState.msoTrue);
						if (tableContainer == null) return;
						var table = tableContainer.Table;
						var tableRowsCount = table.Rows.Count;
						var tableColumnsCount = table.Columns.Count;
						for (var i = 1; i <= tableRowsCount; i++)
							for (var j = 1; j <= tableColumnsCount; j++)
							{
								var tableShape = table.Cell(i, j).Shape;
								if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
								var cellText = tableShape.TextFrame.TextRange.Text.Replace(" ", "");
								var key = copyOfReplacementList.Keys.FirstOrDefault(k => k.Replace(" ", "").ToLower().Equals(cellText.ToLower()));
								if (String.IsNullOrEmpty(key)) continue;
								tableShape.TextFrame.TextRange.Text = copyOfReplacementList[key];
								copyOfReplacementList.Remove(cellText);
							}
						tableColumnsCount = table.Columns.Count;
						for (var i = tableColumnsCount; i >= 1; i--)
						{
							var tableShape = table.Cell(3, i).Shape;
							if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
							var cellText = tableShape.TextFrame.TextRange.Text.Trim();
							if (!cellText.Equals("Delete Column")) continue;
							table.Columns[i].Delete();
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
						if (pasteToSlideMaster)
						{
							var newSlide = presentation.Slides.Add(1, PpSlideLayout.ppLayoutBlank);
							Design design;
							if (selectedTheme != null)
							{
								presentation.ApplyTheme(selectedTheme.ThemeFilePath);
								design = presentation.Designs[presentation.Designs.Count];
								design.Name = DateTime.Now.ToString("MMddyy-hhmmsstt");
							}
							else
								design = presentation.Designs.Add(DateTime.Now.ToString("MMddyy-hhmmsstt"));
							tableContainer.Copy();
							design.SlideMaster.Shapes.Paste();
							newSlide.Design = design;
						}
						else if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.ThemeFilePath);
						AppendSlide(presentation, 1, destinationPresentation);
						presentation.Close();
						slideNumber++;
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

		public void PrepareOneSheetEmail(string fileName, IEnumerable<OutputSchedule> pages, Theme selectedTheme, bool pasteToSlideMaster)
		{
			PreparePresentation(fileName, presentation => AppendOneSheet(pages, selectedTheme, pasteToSlideMaster, presentation));
		}
	}
}