using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.OnlineSchedule.Controls.InteropClasses
{
	public partial class OnlineSchedulePowerPointHelper
	{
		public void AppendDigitalSummary(DigitalSummaryControl digitalSummary, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slidesCount = digitalSummary.OutputReplacementsLists.Count;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetOnlineSummaryFile();
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
										if (!digitalSummary.OutputReplacementsLists[k].ContainsKey(cellText)) continue;
										tableShape.TextFrame.TextRange.Text = digitalSummary.OutputReplacementsLists[k][cellText];
										digitalSummary.OutputReplacementsLists[k].Remove(cellText);
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
						var selectedTheme = digitalSummary.Parent.SelectedTheme;
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

		public void PrepareDigitalSummaryEmail(string fileName, DigitalSummaryControl digitalSummary)
		{
			PreparePresentation(fileName, presentation => AppendDigitalSummary(digitalSummary, presentation));
		}
	}
}
