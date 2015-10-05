using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Interop;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.OnlineSchedule.Controls.InteropClasses
{
	public partial class OnlineSchedulePowerPointHelper
	{
		public void AppendWebPackage(WebPackageControl digitalPackage, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(BusinessWrapper.Instance.OutputManager.DigitalPackageTemplatesFolderPath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slidesCount = digitalPackage.OutputReplacementsLists.Count;
					var rowsCount = digitalPackage.RowsPerSlide;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.DigitalPackageTemplatesFolderPath, String.Format(OutputManager.DigitalPackageTemplateFileName, rowsCount, (digitalPackage.Settings.ShowScreenshot ? "p" : String.Empty)));
						if (!File.Exists(presentationTemplatePath)) continue;
						var presentation = PowerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
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
										if (!digitalPackage.OutputReplacementsLists[k].ContainsKey(cellText)) continue;
										tableShape.TextFrame.TextRange.Text = digitalPackage.OutputReplacementsLists[k][cellText];
										digitalPackage.OutputReplacementsLists[k].Remove(cellText);
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

								var deletedColumns = 0;
								tableRowsCount = table.Rows.Count;
								tableColumnsCount = table.Columns.Count;
								for (var i = 1; i <= tableColumnsCount; i++)
								{
									for (var j = 1; j <= tableRowsCount; j++)
									{
										var tableShape = table.Cell(j, i - deletedColumns).Shape;
										if (tableShape.HasTextFrame != MsoTriState.msoTrue) continue;
										var cellText = tableShape.TextFrame.TextRange.Text.Trim();
										if (!cellText.Equals("DeleteColumn")) continue;
										table.Columns[i - deletedColumns].Delete();
										deletedColumns++;
										break;
									}
								}
							}
						}
						var selectedTheme = digitalPackage.SelectedTheme;
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

		public void PrepareWebPackageEmail(WebPackageControl digitalPackage, string fileName)
		{
			PreparePresentation(fileName, presentation => AppendWebPackage(digitalPackage, presentation));
		}

		public void PrepareWebPackagePdf(WebPackageControl digitalPackage, string targetFileName)
		{
			var sourceFileName = Path.GetTempFileName();
			PreparePresentation(sourceFileName, presentation => AppendWebPackage(digitalPackage, presentation));
			BuildPdf(sourceFileName, targetFileName);
		}
	}
}
