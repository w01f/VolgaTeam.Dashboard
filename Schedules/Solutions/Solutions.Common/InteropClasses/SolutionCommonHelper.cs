using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Common.Core.OfficeInterops;
using Asa.Solutions.Common.PresentationClasses.Output;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using Shapes = Microsoft.Office.Interop.PowerPoint.Shapes;

namespace Asa.Solutions.Common.InteropClasses
{
	public static class SolutionPowerPointExtensions
	{
		public static void AppendSolutionCommonSlide(this PowerPointProcessor target, OutputDataPackage dataPackage, Presentation destinationPresentation = null)
		{
			var presentationTemplatePath = dataPackage.TemplateName;
			try
			{
				var thread = new Thread(delegate ()
				{
					MessageFilter.Register();
					var presentation = target.PowerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
					foreach (Slide slide in presentation.Slides)
					{
						var backgroundClipartShapes = new List<Shape>();
						foreach (Shape shape in slide.Shapes)
						{
							for (var i = 1; i <= shape.Tags.Count; i++)
							{
								var tagName = shape.Tags.Name(i);
								if (tagName.ToUpper().Contains("CLIPART"))
								{
									if (dataPackage.ClipartItems.ContainsKey(tagName.ToUpper()))
									{
										var clipartObject = dataPackage.ClipartItems[tagName.ToUpper()];
										var newShape = slide.Shapes.AddClipartObject(clipartObject, shape);
										if (newShape != null && clipartObject.OutputBackground)
											backgroundClipartShapes.Add(newShape);
									}
									shape.Visible = MsoTriState.msoFalse;
								}
								else if (tagName.ToUpper().Contains("CHART"))
								{
									if (dataPackage.ChartItems.ContainsKey(tagName.ToUpper()))
										shape.UpdateChartData(dataPackage.ChartItems[tagName.ToUpper()]);
								}
								else if (shape.TextFrame.HasText != MsoTriState.msoFalse)
								{
									if (dataPackage.TextItems.ContainsKey(tagName.ToUpper()))
										shape.TextFrame.TextRange.Text = dataPackage.TextItems[tagName.ToUpper()] ?? String.Empty;
									else
										shape.Visible = MsoTriState.msoFalse;
								}
							}
						}
						if (!String.IsNullOrEmpty(dataPackage.LayoutName))
							foreach (CustomLayout customLayout in slide.Design.SlideMaster.CustomLayouts)
							{
								if (!String.Equals(dataPackage.LayoutName, customLayout.Name, StringComparison.OrdinalIgnoreCase))
									continue;

								slide.CustomLayout = customLayout;
								break;
							}

						foreach (var clipartShape in backgroundClipartShapes)
							clipartShape.ZOrder(MsoZOrderCmd.msoSendToBack);
					}

					var selectedTheme = dataPackage.Theme;
					if (selectedTheme != null)
						presentation.ApplyTheme(selectedTheme.GetThemePath());
					target.AppendSlide(presentation, -1, destinationPresentation, dataPackage.AddAsFirtsPage);
					presentation.Close();
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

		public static void PrepareSolutionCommonSlide(this PowerPointProcessor target, string fileName, OutputDataPackage dataPackage)
		{
			target.PreparePresentation(fileName, presentation => target.AppendSolutionCommonSlide(dataPackage, presentation));
		}

		private static Shape AddClipartObject(this Shapes shapes, ClipartObject clipartObject, Shape shapeTemplate)
		{
			Shape shape = null;
			switch (clipartObject.Type)
			{
				case ClipartObjectType.Image:
					{
						var imageObject = (ImageClipartObject)clipartObject;
						var fileName = Path.GetTempFileName();
						if (imageObject.Image != null)
						{
							imageObject.Image.Save(fileName);

							var originalWidth = imageObject.Image.Width;
							var originalHeight = imageObject.Image.Height;
							var percentWidth = shapeTemplate.Width / originalWidth;
							var percentHeight = shapeTemplate.Height / originalHeight;
							var percent = new[] { 1, percentWidth, percentHeight }.Min();
							var width = (Int32)(originalWidth * percent);
							var height = (Int32)(originalHeight * percent);

							shape = shapes.AddPicture(fileName,
								MsoTriState.msoFalse,
								MsoTriState.msoCTrue,
								shapeTemplate.Left + (shapeTemplate.Width - width) / 2,
								shapeTemplate.Top + (shapeTemplate.Height - height) / 2,
								width,
								height);
						}
					}
					break;

				case ClipartObjectType.Video:
					{
						var videoClipartObject = (VideoClipartObject)clipartObject;

						var originalWidth = videoClipartObject.Thumbnail.Width;
						var originalHeight = videoClipartObject.Thumbnail.Height;
						var percentWidth = shapeTemplate.Width / originalWidth;
						var percentHeight = shapeTemplate.Height / originalHeight;
						var percent = new[] { 1, percentWidth, percentHeight }.Min();
						var width = (Int32)(originalWidth * percent);
						var height = (Int32)(originalHeight * percent);

						shape = shapes.AddMediaObject2(videoClipartObject.SourceFilePath, MsoTriState.msoFalse, MsoTriState.msoCTrue,
							shapeTemplate.Left + (shapeTemplate.Width - width) / 2,
							shapeTemplate.Top + (shapeTemplate.Height - height) / 2,
							width,
							height);

						var fileName = Path.GetTempFileName();
						videoClipartObject.Thumbnail.Save(fileName);
						shape.MediaFormat.SetDisplayPictureFromFile(fileName);
					}
					break;

				case ClipartObjectType.YouTube:
					{
						var youTubeObject = (YouTubeClipartObject)clipartObject;

						var width = (Int32)shapeTemplate.Width;
						var height = (Int32)(shapeTemplate.Width * 9 / 16);

						var youTubeTag = String.Format("<object><embed src='{0}' type='application/x-shockwave-flash'></embed></object>", youTubeObject.EmbeddedUrl);

						shape = shapes.AddMediaObjectFromEmbedTag(youTubeTag,
							shapeTemplate.Left + (shapeTemplate.Width - width) / 2,
							shapeTemplate.Top + (shapeTemplate.Height - height) / 2,
							width,
							height);
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Undefined clipart type found");
			}
			return shape;
		}

		private static void UpdateChartData(this Shape shape, Dictionary<string, decimal> chartData)
		{
			if (shape.HasChart == MsoTriState.msoFalse) return;
			var dataWorksheet = (Worksheet)((Workbook)shape.Chart.ChartData.Workbook).Worksheets[1];
			foreach (var chartDataItem in chartData)
				dataWorksheet.Range[chartDataItem.Key].Value = chartDataItem.Value;
		}
	}
}