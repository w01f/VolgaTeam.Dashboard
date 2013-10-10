using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Interop;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.OnlineSchedule.Controls.InteropClasses
{
	public partial class OnlineSchedulePowerPointHelper
	{
		public void AppendAdPlan(AdPlanControl adPlanControl, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(adPlanControl.TemplatesFolderPath)) return;
			try
			{
				var thread = new Thread(delegate()
				{
					MessageFilter.Register();
					var pagesForOutput = adPlanControl.ProductPagesForOutput;
					int slidesCount = adPlanControl.OutputReplacementsLists.Count;
					var recordsPeSlide = adPlanControl.RecordsPerSlide;
					var productsCount = pagesForOutput.Count;
					for (var k = 0; k < slidesCount; k++)
					{
						var presentationTemplatePath = Path.Combine(adPlanControl.TemplatesFolderPath, adPlanControl.TemplateFileName);
						if (!File.Exists(presentationTemplatePath)) continue;
						var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						foreach (Slide slide in presentation.Slides)
						{
							foreach (Shape shape in slide.Shapes)
							{
								for (int i = 1; i <= shape.Tags.Count; i++)
								{
									switch (shape.Tags.Name(i))
									{
										case "ADVERTISER":
											shape.TextFrame.TextRange.Text = adPlanControl.BusinessName;
											break;
										case "DATETAG":
											shape.TextFrame.TextRange.Text = adPlanControl.Date;
											break;
										default:
											for (var j = 0; j < recordsPeSlide; j++)
											{
												if (shape.Tags.Name(i).Equals(string.Format("APLOGO{0}", j + 1)))
												{
													if (((k * recordsPeSlide) + j) < productsCount)
													{
														var logoFile = pagesForOutput[(k * recordsPeSlide) + j].LogoFile;
														if (!String.IsNullOrEmpty(logoFile))
															slide.Shapes.AddPicture(FileName: logoFile, LinkToFile: MsoTriState.msoFalse, SaveWithDocument: MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
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
										if (String.IsNullOrEmpty(cellText) || !adPlanControl.OutputReplacementsLists[k].ContainsKey(cellText)) continue;
										tableShape.TextFrame.TextRange.Text = adPlanControl.OutputReplacementsLists[k][cellText];
										adPlanControl.OutputReplacementsLists[k].Remove(cellText);
									}
								}
							}
						}
						var selectedTheme = adPlanControl.SelectedTheme;
						if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.ThemeFilePath);
						AppendSlide(presentation, -1, destinationPresentation);
						presentation.Close();
					}
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void PrepareAdPlanEmail(string fileName, AdPlanControl adPlanControl)
		{
			PreparePresentation(fileName, presentation => AppendAdPlan(adPlanControl, presentation));
		}
	}
}