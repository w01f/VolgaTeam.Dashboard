using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Asa.Common.Core.OfficeInterops;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using Theme = Asa.Common.Core.Objects.Themes.Theme;

namespace Asa.Media.Controls.InteropClasses
{
	public partial class MediaSchedulePowerPointHelper<T> where T : class, new()
	{
		#region OneSheet
		public void AppendDigitalOneSheet(StandaloneDigitalInfoOneSheetOutputModel dataModel, Theme selectedTheme, bool pasteToSlideMaster, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var copyOfReplacementList = new Dictionary<string, string>(dataModel.ReplacementsList);
					var presentationTemplatePath = dataModel.TemplateFilePath;
					if (!File.Exists(presentationTemplatePath)) return;
					var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					var taggedSlide = presentation.Slides.Count > 0 ? presentation.Slides[1] : null;

					var logoShapes = new List<Shape>();
					foreach (var shape in taggedSlide.Shapes.OfType<Shape>().Where(s => s.HasTable != MsoTriState.msoTrue))
					{
						for (var i = 1; i <= shape.Tags.Count; i++)
						{
							var shapeTagName = shape.Tags.Name(i);
							for (var j = 0; j < BaseDigitalInfoOneSheetOutputModel.MaxRecords; j++)
							{
								if (shapeTagName.Equals(String.Format("DIGLOGO{0}", j + 1)))
								{
									var fileName = dataModel.Logos.Length > j ? dataModel.Logos[j] : String.Empty;
									if (!String.IsNullOrEmpty(fileName) && File.Exists(fileName))
									{
										var newShape = taggedSlide.Shapes.AddPicture(fileName, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left,
												shape.Top, shape.Width, shape.Height);
										newShape.Top = shape.Top;
										newShape.Left = shape.Left;
										newShape.Width = shape.Width;
										newShape.Height = shape.Height;
										logoShapes.Add(newShape);
									}
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

						if (dataModel.ContractSettings.IsConfigured)
							FillContractInfo(design, dataModel.ContractSettings, BusinessObjects.Instance.OutputManager.ContractTemplateFolder);

						newSlide.Design = design;
					}
					else
					{
						if (selectedTheme != null)
							presentation.ApplyTheme(selectedTheme.GetThemePath());

						if (dataModel.ContractSettings.IsConfigured)
							FillContractInfo(taggedSlide, dataModel.ContractSettings, BusinessObjects.Instance.OutputManager.ContractTemplateFolder);
					}
					AppendSlide(presentation, 1, destinationPresentation);
					presentation.Close();
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

		public void PrepareDigitalOneSheetEmail(string fileName, StandaloneDigitalInfoOneSheetOutputModel page, Theme selectedTheme, bool pasteToSlideMaster)
		{
			PreparePresentation(fileName, presentation => AppendDigitalOneSheet(page, selectedTheme, pasteToSlideMaster, presentation));
		}
		#endregion

		#region Strategy
		public void AppendStrategy(DigitalInfoStrategyOutputModel source, Theme theme, Presentation destinationPresentation = null)
		{
			try
			{
				var thread = new Thread(delegate ()
				{
					var presentationTemplatePath = source.TemplatePath;
					if (string.IsNullOrEmpty(presentationTemplatePath)) return;
					if (!File.Exists(presentationTemplatePath)) return;

					MessageFilter.Register();
					var presentation = PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					foreach (Slide slide in presentation.Slides)
					{
						foreach (Shape shape in slide.Shapes)
						{
							for (int i = 1; i <= shape.Tags.Count; i++)
							{
								var shapeTagName = shape.Tags.Name(i);
								switch (shapeTagName)
								{
									case "MONTHDIGINV":
										shape.TextFrame.TextRange.Text = source.Total2;
										break;
									case "TOTALDIGINV":
										shape.TextFrame.TextRange.Text = source.Total1;
										break;
									default:
										for (var j = 0; j < BaseDigitalInfoOneSheetOutputModel.MaxRecords; j++)
										{
											if (shapeTagName.Equals(String.Format("DIGITAL_GRAPHIC{0}", j + 1)))
											{
												var fileName = source.Logos.Length > j ? source.Logos[j] : String.Empty;
												if (!String.IsNullOrEmpty(fileName) && File.Exists(fileName))
												{
													var newShape = slide.Shapes.AddPicture(fileName, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top,
														shape.Width, shape.Height);
													newShape.Top = shape.Top;
													newShape.Left = shape.Left;
													newShape.Width = shape.Width;
													newShape.Height = shape.Height;
												}
												shape.Visible = MsoTriState.msoFalse;
											}
											if (shapeTagName.Equals(String.Format("DIGITAL_TAG{0}", j + 1)))
											{
												shape.TextFrame.TextRange.Text = source.Records.Count > j ? source.Records[j].Text1 : String.Empty;
											}
											if (shapeTagName.Equals(String.Format("DIGITAL_DETAILS{0}", j + 1)))
											{
												shape.TextFrame.TextRange.Text = source.Records.Count > j ? source.Records[j].Text2 : String.Empty;
											}
										}
										break;
								}
							}
						}
					}
					if (theme != null)
						presentation.ApplyTheme(theme.GetThemePath());
					AppendSlide(presentation, -1, destinationPresentation);
					presentation.Close();
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

		public void PrepareStrategyEmail(string fileName, DigitalInfoStrategyOutputModel dataModel, Theme selectedTheme)
		{
			PreparePresentation(fileName, presentation => AppendStrategy(dataModel, selectedTheme, presentation));
		}
		#endregion
	}
}
