using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.OnlineSchedule.Controls.InteropClasses
{
	public partial class OnlineSchedulePowerPointHelper
	{
		public void AppendOneSheet(DigitalProduct[] sources, Theme theme, Presentation destinationPresentation = null)
		{
			if (!Directory.Exists(BusinessWrapper.Instance.OutputManager.OneSheetsTemplatesFolderPath)) return;
			foreach (var source in sources)
			{
				var presentationTemplatePath = source.OutputData.GetSlideSource(BusinessWrapper.Instance.OutputManager.OneSheetsTemplatesFolderPath);
				if (string.IsNullOrEmpty(presentationTemplatePath)) return;
				if (!File.Exists(presentationTemplatePath)) return;
				try
				{
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						var presentation = _powerPointObject.Presentations.Open(presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
						foreach (Slide slide in presentation.Slides)
						{
							foreach (Shape shape in slide.Shapes)
							{
								for (int i = 1; i <= shape.Tags.Count; i++)
								{
									switch (shape.Tags.Name(i))
									{
										#region Top Part
										case "WEBSITEURL":
											shape.TextFrame.TextRange.Text = source.OutputData.Websites;
											break;
										case "DATETAG":
											shape.TextFrame.TextRange.Text = source.OutputData.PresentationDate;
											break;
										case "ADVERTISER":
											shape.TextFrame.TextRange.Text = source.OutputData.BusinessName;
											break;
										case "DECMKR":
											shape.TextFrame.TextRange.Text = source.OutputData.DecisionMaker;
											break;
										case "CAMPVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.FlightDates;
											break;
										case "MWVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.DurationValue;
											break;
										case "MONTHSWEEKS":
											shape.TextFrame.TextRange.Text = source.OutputData.DurationType;
											break;
										case "WEBPRODUCT":
											shape.TextFrame.TextRange.Text = source.OutputData.ProductName;
											break;
										case "DIMVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.Dimensions;
											break;
										case "WEBDESCRIPT":
											shape.TextFrame.TextRange.Text = source.OutputData.Description;
											break;
										#endregion

										case "MTHIMPLABEL":
											if(source.OutputData.MonthlyData.Any())
												shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 0 ? source.OutputData.MonthlyData.ElementAt(0).Name : String.Empty;
											else
												shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 0 ? source.OutputData.TotalData.ElementAt(0).Name : String.Empty;	
											break;
										case "MONTHLYIMPVALUE":
											if(source.OutputData.MonthlyData.Any())
												shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 0 ? source.OutputData.MonthlyData.ElementAt(0).Code : String.Empty;
											else
												shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 0 ? source.OutputData.TotalData.ElementAt(0).Code : String.Empty;	
											break;
										case "MONTHINVLBL":
											if(source.OutputData.MonthlyData.Any())
												shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 1 ? source.OutputData.MonthlyData.ElementAt(1).Name : String.Empty;
											else
												shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 1 ? source.OutputData.TotalData.ElementAt(1).Name : String.Empty;	
											break;
										case "MONTHINVVALUE":
											if(source.OutputData.MonthlyData.Any())
												shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 1 ? source.OutputData.MonthlyData.ElementAt(1).Code : String.Empty;
											else
												shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 1 ? source.OutputData.TotalData.ElementAt(1).Code : String.Empty;	
											break;
										case "MCPM":
											if(source.OutputData.MonthlyData.Any())
												shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 2 ? source.OutputData.MonthlyData.ElementAt(2).Name : String.Empty;
											else
												shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 2 ? source.OutputData.TotalData.ElementAt(2).Name : String.Empty;	
											break;
										case "MCPMVALUE":
											if(source.OutputData.MonthlyData.Any())
												shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 2 ? source.OutputData.MonthlyData.ElementAt(2).Code : String.Empty;
											else
												shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 2 ? source.OutputData.TotalData.ElementAt(2).Code : String.Empty;	
											break;

										case "TTLIMPLABEL":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() && source.OutputData.TotalData.Count() > 0 ? source.OutputData.TotalData.ElementAt(0).Name : String.Empty;
											break;
										case "TOTALIMPVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() && source.OutputData.TotalData.Count() > 0 ? source.OutputData.TotalData.ElementAt(0).Code : String.Empty;
											break;
										case "TOTINVLBL":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() && source.OutputData.TotalData.Count() > 1 ? source.OutputData.TotalData.ElementAt(1).Name : String.Empty;
											break;
										case "TOTINVVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() && source.OutputData.TotalData.Count() > 1 ? source.OutputData.TotalData.ElementAt(1).Code : String.Empty;
											break;
										case "TCPM":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() && source.OutputData.TotalData.Count() > 2 ? source.OutputData.TotalData.ElementAt(2).Name : String.Empty;
											break;
										case "TCPMVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() && source.OutputData.TotalData.Count() > 2 ? source.OutputData.TotalData.ElementAt(2).Code : String.Empty;
											break;

										case "TOTALADS":
											shape.TextFrame.TextRange.Text = source.OutputData.DigitalData.Count() > 0 ? source.OutputData.DigitalData.ElementAt(0).Name : String.Empty;
											break;
										case "TTLADSVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.DigitalData.Count() > 0 ? source.OutputData.DigitalData.ElementAt(0).Code : String.Empty;
											break;
										case "DAYS":
											shape.TextFrame.TextRange.Text = source.OutputData.DigitalData.Count() > 1 ? source.OutputData.DigitalData.ElementAt(1).Name : String.Empty;
											break;
										case "DAYVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.DigitalData.Count() > 1 ? source.OutputData.DigitalData.ElementAt(1).Code : String.Empty;
											break;
										case "ADRATELBL":
											shape.TextFrame.TextRange.Text = source.OutputData.DigitalData.Count() > 2 ? source.OutputData.DigitalData.ElementAt(2).Name : String.Empty;
											break;
										case "ADRATEVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.DigitalData.Count() > 2 ? source.OutputData.DigitalData.ElementAt(2).Code : String.Empty;
											break;

										case "CMNTVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.Comment;;
											break;
									}
								}
							}
						}
						if (theme != null)
							presentation.ApplyTheme(theme.ThemeFilePath);
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
		}

		public void PrepareScheduleEmail(string fileName, DigitalProduct[] products, Theme theme)
		{
			PreparePresentation(fileName, presentation => AppendOneSheet(products, theme, presentation));
		}
	}
}