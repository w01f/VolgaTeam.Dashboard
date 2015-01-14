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

		public void AppendOneSheetTableBased(IEnumerable<OutputScheduleGridBased> pages, Theme selectedTheme, bool pasteToSlideMaster, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(OneSheetTemplatePath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					foreach (var page in pages)
					{
						var copyOfReplacementList = new Dictionary<string, string>(page.ReplacementsList);
						var presentationTemplatePath = Path.Combine(OneSheetTemplatePath, string.Format(OutputManager.OneSheetTableBasedTemplateFileName, page.Color, page.ProgramsPerSlide, page.SpotsPerSlide));
						if (!File.Exists(presentationTemplatePath)) return;
						var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						var tagedSlide = presentation.Slides.Count > 0 ? presentation.Slides[1] : null;
						var shape = tagedSlide.Shapes.Cast<Shape>().FirstOrDefault(s => s.HasTable == MsoTriState.msoTrue);
						if (shape == null) return;
						var table = shape.Table;
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
							shape.Copy();
							design.SlideMaster.Shapes.Paste();
							newSlide.Design = design;
						}
						else if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.ThemeFilePath);
						AppendSlide(presentation, 1, destinationPresentation);
						presentation.Close();
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

		public void PrepareOneSheetEmailTableBased(string fileName, IEnumerable<OutputScheduleGridBased> pages, Theme selectedTheme, bool pasteToSlideMaster)
		{
			PreparePresentation(fileName, presentation => AppendOneSheetTableBased(pages, selectedTheme, pasteToSlideMaster, presentation));
		}
	}
}