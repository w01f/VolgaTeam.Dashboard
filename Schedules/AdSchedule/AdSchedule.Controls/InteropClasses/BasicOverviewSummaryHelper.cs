using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls;
using NewBizWiz.Core.Interop;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendBasicOverviewSummary(BasicOverviewSummaryControl basicOverviewSummary, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(BusinessWrapper.Instance.OutputManager.BasicOverviewSummaryTemplatesFolderPath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slidesCount = basicOverviewSummary.OutputReplacementsLists.Count;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.BasicOverviewSummaryTemplatesFolderPath, OutputManager.BasicOverviewSummaryTemplateFileName);
						if (!File.Exists(presentationTemplatePath)) continue;
						var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						foreach (Slide slide in presentation.Slides)
						{
							foreach (Shape shape in slide.Shapes)
							{
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
										if (!basicOverviewSummary.OutputReplacementsLists[k].ContainsKey(cellText)) continue;
										tableShape.TextFrame.TextRange.Text = basicOverviewSummary.OutputReplacementsLists[k][cellText];
										basicOverviewSummary.OutputReplacementsLists[k].Remove(cellText);
									}
								}

								var deletedRows = 0;
								for (var i = 1; i <= tableRowsCount; i++)
								{
									var tableShape = table.Cell(i - deletedRows, 1).Shape;
									if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
									var cellText = tableShape.TextFrame.TextRange.Text.Trim();
									if (!cellText.Equals("DeleteRow")) continue;
									table.Rows[i - deletedRows].Delete();
									deletedRows++;
								}
							}
						}
						var selectedTheme = basicOverviewSummary.SelectedTheme;
						if (selectedTheme != null)
							presentation.ApplyTheme(AsyncHelper.RunSync(selectedTheme.GetThemePath));
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

		public void PrepareBasicOverviewSummaryEmail(string fileName, BasicOverviewSummaryControl basicOverviewSummary)
		{
			PreparePresentation(fileName, presentation => AppendBasicOverviewSummary(basicOverviewSummary, presentation));
		}
	}
}
