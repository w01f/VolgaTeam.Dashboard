using System;
using System.IO;
using System.Linq;
using System.Threading;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.Core.OfficeInterops;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Asa.Online.Controls.InteropClasses
{
	public partial class OnlineSchedulePowerPointHelper
	{
		public void AppendOneSheet(DigitalProduct[] sources, Theme theme, Presentation destinationPresentation = null)
		{
			foreach (var source in sources)
			{
				try
				{
					var thread = new Thread(delegate()
					{
						var presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GetOnlineOneSheetFile(source.OutputData.GetSlideSource());
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
									switch (shape.Tags.Name(i))
									{
										#region Top Part
										case "HEADER":
											shape.TextFrame.TextRange.Text = String.Format(source.OutputData.Header, shape.TextFrame.TextRange.Text);
											break;
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
										case "WEBPRODUCT":
											shape.TextFrame.TextRange.Text = source.OutputData.ProductName;
											break;
										case "WEBDESCRIPT":
											shape.TextFrame.TextRange.Text = source.OutputData.Description;
											break;
										#endregion

										case "MTHIMPLABEL":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() ? source.OutputData.MonthlyData.ElementAt(0).Name : String.Empty;
											break;
										case "MONTHLYIMPVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Any() ? source.OutputData.MonthlyData.ElementAt(0).Code : String.Empty;
											break;
										case "MONTHINVLBL":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 1 ? source.OutputData.MonthlyData.ElementAt(1).Name : String.Empty;
											break;
										case "MONTHINVVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 1 ? source.OutputData.MonthlyData.ElementAt(1).Code : String.Empty;
											break;
										case "MCPM":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 2 ? source.OutputData.MonthlyData.ElementAt(2).Name : String.Empty;
											break;
										case "MCPMVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.MonthlyData.Count() > 2 ? source.OutputData.MonthlyData.ElementAt(2).Code : String.Empty;
											break;

										case "TTLIMPLABEL":
											shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Any() ? source.OutputData.TotalData.ElementAt(0).Name : String.Empty;
											break;
										case "TOTALIMPVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Any() ? source.OutputData.TotalData.ElementAt(0).Code : String.Empty;
											break;
										case "TOTINVLBL":
											shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 1 ? source.OutputData.TotalData.ElementAt(1).Name : String.Empty;
											break;
										case "TOTINVVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 1 ? source.OutputData.TotalData.ElementAt(1).Code : String.Empty;
											break;
										case "TCPM":
											shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 2 ? source.OutputData.TotalData.ElementAt(2).Name : String.Empty;
											break;
										case "TCPMVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.TotalData.Count() > 2 ? source.OutputData.TotalData.ElementAt(2).Code : String.Empty;
											break;

										case "INVDET":
											shape.TextFrame.TextRange.Text = source.OutputData.InvestmentDetails; ;
											break;

										case "CMNTVALUE":
											shape.TextFrame.TextRange.Text = source.OutputData.Comment; ;
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
		}

		public void PrepareScheduleEmail(string fileName, DigitalProduct[] products, Theme theme)
		{
			PreparePresentation(fileName, presentation => AppendOneSheet(products, theme, presentation));
		}
	}
}