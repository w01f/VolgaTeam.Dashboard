using System;
using System.Collections.Generic;
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
				var presentationTemplatePath = source.GetSlideSource(BusinessWrapper.Instance.OutputManager.OneSheetsTemplatesFolderPath);
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
										case "HEADER":
											shape.TextFrame.TextRange.Text = source.SlideHeader;
											break;
										case "WEBSITEURL":
											shape.TextFrame.TextRange.Text = String.Join(", ", source.AllWebsites.ToArray());
											break;
										case "DATETAG":
											shape.TextFrame.TextRange.Text = source.Parent.PresentationDate.ToString("MM/dd/yy");
											break;
										case "ADVERTISER":
											shape.TextFrame.TextRange.Text = source.Parent.BusinessName;
											break;
										case "DECMKR":
											shape.TextFrame.TextRange.Text = source.Parent.DecisionMaker;
											break;
										case "CAMPVALUE":
											shape.TextFrame.TextRange.Text = source.Parent.FlightDates;
											break;
										case "MWVALUE":
											shape.TextFrame.TextRange.Text = source.DurationValue.HasValue ? source.DurationValue.Value.ToString("#,##0") : string.Empty;
											break;
										case "MONTHSWEEKS":
											shape.TextFrame.TextRange.Text = source.DurationType;
											break;
										case "DAYVALUE":
											shape.TextFrame.TextRange.Text = source.ActiveDays.HasValue ? source.ActiveDays.Value.ToString("#,##0") : string.Empty;
											break;
										case "TTLADSVALUE":
											shape.TextFrame.TextRange.Text = source.TotalAds.HasValue ? source.TotalAds.Value.ToString("#,##0") : string.Empty;
											break;
										case "WEBPRODUCT":
											shape.TextFrame.TextRange.Text = source.UserDefinedName;
											break;
										case "DIMVALUE":
											shape.TextFrame.TextRange.Text = source.Dimensions;
											break;
										case "WEBDESCRIPT":
											shape.TextFrame.TextRange.Text = source.Description;
											break;
										case "MONTHLYIMPVALUE":
											shape.TextFrame.TextRange.Text = source.MonthlyImpressionsCalculated.HasValue ? source.MonthlyImpressionsCalculated.Value.ToString("#,##0") : string.Empty;
											;
											break;
										case "TOTALIMPVALUE":
											shape.TextFrame.TextRange.Text = source.TotalImpressionsCalculated.HasValue ? source.TotalImpressionsCalculated.Value.ToString("#,##0") : string.Empty;
											;
											break;
										case "MCPMVALUE":
											shape.TextFrame.TextRange.Text = source.MonthlyCPMCalculated.HasValue ? source.MonthlyCPMCalculated.Value.ToString("$#,###.00") : string.Empty;
											break;
										case "TCPMVALUE":
											shape.TextFrame.TextRange.Text = source.TotalCPMCalculated.HasValue ? source.TotalCPMCalculated.Value.ToString("$#,###.00") : string.Empty;
											break;
										case "ADRATEVALUE":
											shape.TextFrame.TextRange.Text = source.AdRate.HasValue ? source.AdRate.Value.ToString("$#,###.00") : string.Empty;
											break;
										case "MONTHINVVALUE":
											shape.TextFrame.TextRange.Text = source.MonthlyInvestmentCalculated.HasValue ? source.MonthlyInvestmentCalculated.Value.ToString("$#,###.00") : string.Empty;
											break;
										case "TOTINVVALUE":
											shape.TextFrame.TextRange.Text = source.TotalInvestmentCalculated.HasValue ? source.TotalInvestmentCalculated.Value.ToString("$#,###.00") : string.Empty;
											break;
										case "CMNTVALUE":
											var list = new List<string>();
											if (source.ShowStrength1 && !string.IsNullOrEmpty(source.Strength1))
												list.Add(source.Strength1);
											if (source.ShowStrength2 && !string.IsNullOrEmpty(source.Strength2))
												list.Add(source.Strength2);
											if (source.ShowCommentText && !string.IsNullOrEmpty(source.Comment))
												list.Add(source.Comment);
											shape.TextFrame.TextRange.Text = string.Join(", ", list.ToArray());
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