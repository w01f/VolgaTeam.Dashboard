using System.IO;
using System.Threading;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Online.Controls.PresentationClasses.Packages;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Online.Controls.InteropClasses
{
	public partial class OnlineSchedulePowerPointHelper
	{
		public void AppendWebPackage(IWebPackageOutput digitalPackage, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var slidesCount = digitalPackage.OutputReplacementsLists.Count;
					var rowsCount = digitalPackage.RowsPerSlide;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetOnlinePackageFile(rowsCount, digitalPackage.PackageSettings.ShowScreenshot);
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

		public void PrepareWebPackageEmail(IWebPackageOutput digitalPackage, string fileName)
		{
			PreparePresentation(fileName, presentation => AppendWebPackage(digitalPackage, presentation));
		}

		public void PrepareWebPackagePdf(IWebPackageOutput digitalPackage, string targetFileName)
		{
			var sourceFileName = Path.GetTempFileName();
			PreparePresentation(sourceFileName, presentation => AppendWebPackage(digitalPackage, presentation));
			BuildPdf(sourceFileName, targetFileName);
		}
	}
}
