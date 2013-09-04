using System;
using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.AdSchedule.Controls.InteropClasses
{
	public partial class AdSchedulePowerPointHelper
	{
		public void AppendAdPlan(Presentation destinationPresentation = null)
		{
			if (Directory.Exists(BusinessWrapper.Instance.OutputManager.AdPlanTemlatesFolderPath))
			{
				try
				{
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						var pagesForOutput = Controller.Instance.Summaries.AdPlan.ProductPagesForOutput;
						int slidesCount = Controller.Instance.Summaries.AdPlan.OutputReplacementsLists.Count;
						var recordsPeSlide = Controller.Instance.Summaries.AdPlan.RecordsPerSlide;
						var productsCount = pagesForOutput.Count;
						for (var k = 0; k < slidesCount; k ++)
						{
							var presentationTemplatePath = Path.Combine(BusinessWrapper.Instance.OutputManager.AdPlanTemlatesFolderPath, Controller.Instance.Summaries.AdPlan.TemplateFileName);
							if (!File.Exists(presentationTemplatePath)) continue;
							var presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
							foreach (Slide slide in presentation.Slides)
							{
								foreach (Shape shape in slide.Shapes)
								{
									for (int i = 1; i <= shape.Tags.Count; i++)
									{
										switch (shape.Tags.Name(i))
										{
											case "ADVERTISER":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.BusinessName;
												break;
											case "DATETAG":
												shape.TextFrame.TextRange.Text = Controller.Instance.Summaries.Snapshot.Date;
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
											if (String.IsNullOrEmpty(cellText) || !Controller.Instance.Summaries.AdPlan.OutputReplacementsLists[k].ContainsKey(cellText)) continue;
											tableShape.TextFrame.TextRange.Text = Controller.Instance.Summaries.AdPlan.OutputReplacementsLists[k][cellText];
											Controller.Instance.Summaries.AdPlan.OutputReplacementsLists[k].Remove(cellText);
										}
									}
								}
							}
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
		}

		public void PrepareAdPlanEmail(string fileName)
		{
			try
			{
				SavePrevSlideIndex();
				Presentations presentations = _powerPointObject.Presentations;
				Presentation presentation = presentations.Add(MsoTriState.msoFalse);
				presentation.PageSetup.SlideWidth = (float)SettingsManager.Instance.SizeWidth * 72;
				presentation.PageSetup.SlideHeight = (float)SettingsManager.Instance.SizeHeght * 72;
				switch (SettingsManager.Instance.Orientation)
				{
					case "Landscape":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
						break;
					case "Portrait":
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
						break;
				}
				Utilities.Instance.ReleaseComObject(presentations);
				AppendAdPlan(presentation);
				MessageFilter.Register();
				var thread = new Thread(delegate()
				{
					presentation.SaveAs(FileName: fileName);
					string destinationFolder = fileName.Replace(Path.GetExtension(fileName), string.Empty);
					if (!Directory.Exists(destinationFolder))
						Directory.CreateDirectory(destinationFolder);
					presentation.Export(Path: destinationFolder, FilterName: "PNG");
					presentation.Close();
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();

				Utilities.Instance.ReleaseComObject(presentation);
				RestorePrevSlideIndex();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}
}