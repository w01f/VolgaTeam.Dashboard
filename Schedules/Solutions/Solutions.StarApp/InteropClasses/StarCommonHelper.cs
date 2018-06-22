using System;
using System.IO;
using System.Threading;
using Asa.Common.Core.OfficeInterops;
using Asa.Solutions.StarApp.PresentationClasses.Output;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Solutions.StarApp.InteropClasses
{
	public static partial class SolutionStarPowerPointHelperExtensions
	{
		public static void AppendStarCommonSlide(this PowerPointProcessor target, OutputDataPackage dataPackage, Presentation destinationPresentation = null)
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
						foreach (Shape shape in slide.Shapes)
						{
							for (var i = 1; i <= shape.Tags.Count; i++)
							{
								var tagName = shape.Tags.Name(i);
								if (tagName.ToUpper().Contains("CLIPART"))
								{
									if (dataPackage.ClipartItems.ContainsKey(tagName.ToUpper()))
									{
										var imageInfo = dataPackage.ClipartItems[tagName.ToUpper()];
										var fileName = imageInfo.FilePath;
										if (!String.IsNullOrEmpty(fileName) && File.Exists(fileName))
										{
											var newShape = slide.Shapes.AddPicture(fileName, MsoTriState.msoFalse, MsoTriState.msoCTrue, shape.Left, shape.Top);

											var originalWidth = imageInfo.Size.Width;
											var originalHeight = imageInfo.Size.Height;
											var percentWidth = shape.Width / originalWidth;
											var percentHeight = shape.Height / originalHeight;
											var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
											var newWidth = percent < 1 ? (int)(originalWidth * percent) : originalWidth;
											var newHeight = percent < 1 ? (int)(originalHeight * percent) : originalHeight;

											newShape.Top = shape.Top + (shape.Height - newHeight) / 2;
											newShape.Left = shape.Left + (shape.Width - newWidth) / 2;
											newShape.Width = newWidth;
											newShape.Height = newHeight;
										}
									}
									shape.Visible = MsoTriState.msoFalse;
								}
								else
								{
									if (dataPackage.TextItems.ContainsKey(tagName.ToUpper()))
										shape.TextFrame.TextRange.Text = dataPackage.TextItems[tagName.ToUpper()] ?? String.Empty;
									else
										shape.Visible = MsoTriState.msoFalse;
								}
							}
						}
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

		public static void PrepareStarCommonSlide(this PowerPointProcessor target, OutputDataPackage dataPackage, string fileName)
		{
			target.PreparePresentation(fileName, presentation => target.AppendStarCommonSlide(dataPackage, presentation));
		}
	}
}