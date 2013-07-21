using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace MobileScheduleBuilder.InteropClasses
{
    public partial class PowerPointHelper
    {
        public void AppendOneSheet(BusinessClasses.Product source, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetsTemplatesFolderPath))
            {
                string presentationTemplatePath = source.GetSlideSource();
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();
                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            foreach (PowerPoint.Slide slide in presentation.Slides)
                            {
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    for (int i = 1; i <= shape.Tags.Count; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "HEADER":
                                                shape.TextFrame.TextRange.Text = source.SlideHeader;
                                                break;
                                            case "WEBSITEURL":
                                                shape.TextFrame.TextRange.Text = source.AllWebsites;
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
                                                shape.TextFrame.TextRange.Text = string.Format("{0}{1}", new object[] { !string.IsNullOrEmpty(source.SubCategory) ? (source.SubCategory + " - ") : string.Empty, source.Name });
                                                break;
                                            case "DIMVALUE":
                                                shape.TextFrame.TextRange.Text = source.Dimensions;
                                                break;
                                            case "WEBDESCRIPT":
                                                shape.TextFrame.TextRange.Text = source.Description;
                                                break;
                                            case "MONTHLYIMPVALUE":
                                                shape.TextFrame.TextRange.Text = source.MonthlyImpressionsCalculated.HasValue ? source.MonthlyImpressionsCalculated.Value.ToString("#,##0") : string.Empty; ;
                                                break;
                                            case "TOTALIMPVALUE":
                                                shape.TextFrame.TextRange.Text = source.TotalImpressionsCalculated.HasValue ? source.TotalImpressionsCalculated.Value.ToString("#,##0") : string.Empty; ;
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
                                                List<string> list = new List<string>();
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
                            AppendSlide(presentation, -1, destinationPresentation);
                            presentation.Close();
                        }));
                        thread.Start();

                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
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
        }

        public void PrepareScheduleEmail(string fileName, BusinessClasses.Product[] products)
        {
            try
            {
                PowerPoint.Presentations presentations = _powerPointObject.Presentations;
                PowerPoint.Presentation presentation = presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
                presentation.PageSetup.SlideWidth = (float)ConfigurationClasses.SettingsManager.Instance.SizeWidth * 72;
                presentation.PageSetup.SlideHeight = (float)ConfigurationClasses.SettingsManager.Instance.SizeHeght * 72;
                switch (ConfigurationClasses.SettingsManager.Instance.Orientation)
                {
                    case "Landscape":
                        presentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal;
                        break;
                    case "Portrait":
                        presentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
                        break;
                }
                AppManager.ReleaseComObject(presentations);
                foreach (BusinessClasses.Product product in products)
                    AppendOneSheet(product, presentation);
                MessageFilter.Register();
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    presentation.SaveAs(FileName: fileName);
                    string destinationFolder = fileName.Replace(Path.GetExtension(fileName), string.Empty);
                    if (!Directory.Exists(destinationFolder))
                        Directory.CreateDirectory(destinationFolder);
                    presentation.Export(Path: destinationFolder, FilterName: "PNG");
                    presentation.Close();
                }));
                thread.Start();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();

                AppManager.ReleaseComObject(presentation);
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void AppendOneSheetPackage(BusinessClasses.ProductPackage source, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetsTemplatesFolderPath))
            {
                string presentationTemplatePath = source.GetSlideSource();
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();
                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            foreach (PowerPoint.Slide slide in presentation.Slides)
                            {
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    for (int i = 1; i <= shape.Tags.Count; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "HEADER":
                                                shape.TextFrame.TextRange.Text = source.SlideHeader;
                                                break;
                                            case "WEBSITEURL":
                                                shape.TextFrame.TextRange.Text = source.AllWebsites;
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
                                                shape.TextFrame.TextRange.Text = source.Description;
                                                break;
                                            case "DIMLABEL":
                                                try
                                                {
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                }
                                                catch
                                                {
                                                    shape.TextFrame.TextRange.Text = string.Empty;
                                                }
                                                break;
                                            case "DIMVALUE":
                                                try
                                                {
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                }
                                                catch
                                                {
                                                    shape.TextFrame.TextRange.Text = string.Empty;
                                                }
                                                break;
                                            case "WEBDESCRIPT":
                                                try
                                                {
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                }
                                                catch
                                                {
                                                    shape.TextFrame.TextRange.Text = string.Empty;
                                                }
                                                break;
                                            case "MONTHLYIMPVALUE":
                                                shape.TextFrame.TextRange.Text = source.MonthlyImpressions.HasValue ? source.MonthlyImpressions.Value.ToString("#,##0") : string.Empty; ;
                                                break;
                                            case "TOTALIMPVALUE":
                                                shape.TextFrame.TextRange.Text = source.TotalImpressions.HasValue ? source.TotalImpressions.Value.ToString("#,##0") : string.Empty; ;
                                                break;
                                            case "MCPMVALUE":
                                                shape.TextFrame.TextRange.Text = source.MonthlyCPM.HasValue ? source.MonthlyCPM.Value.ToString("$#,###.00") : string.Empty;
                                                break;
                                            case "TCPMVALUE":
                                                shape.TextFrame.TextRange.Text = source.TotalCPM.HasValue ? source.TotalCPM.Value.ToString("$#,###.00") : string.Empty;
                                                break;
                                            case "ADRATEVALUE":
                                                shape.TextFrame.TextRange.Text = source.AdRate.HasValue ? source.AdRate.Value.ToString("$#,###.00") : string.Empty;
                                                break;
                                            case "MONTHINVVALUE":
                                                shape.TextFrame.TextRange.Text = source.MonthlyInvestment.HasValue ? source.MonthlyInvestment.Value.ToString("$#,###.00") : string.Empty;
                                                break;
                                            case "TOTINVVALUE":
                                                shape.TextFrame.TextRange.Text = source.TotalInvestment.HasValue ? source.TotalInvestment.Value.ToString("$#,###.00") : string.Empty;
                                                break;
                                            case "CMNTVALUE":
                                                List<string> list = new List<string>();
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
                            AppendSlide(presentation, -1, destinationPresentation);
                            presentation.Close();
                        }));
                        thread.Start();

                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
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
        }

        public void PreparePackageEmail(string fileName, BusinessClasses.ProductPackage productPackage)
        {
            try
            {
                PowerPoint.Presentations presentations = _powerPointObject.Presentations;
                PowerPoint.Presentation presentation = presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
                presentation.PageSetup.SlideWidth = (float)ConfigurationClasses.SettingsManager.Instance.SizeWidth * 72;
                presentation.PageSetup.SlideHeight = (float)ConfigurationClasses.SettingsManager.Instance.SizeHeght * 72;
                switch (ConfigurationClasses.SettingsManager.Instance.Orientation)
                {
                    case "Landscape":
                        presentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal;
                        break;
                    case "Portrait":
                        presentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
                        break;
                }
                AppManager.ReleaseComObject(presentations);
                    AppendOneSheetPackage(productPackage, presentation);
                MessageFilter.Register();
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    presentation.SaveAs(FileName: fileName);
                    string destinationFolder = fileName.Replace(Path.GetExtension(fileName), string.Empty);
                    if (!Directory.Exists(destinationFolder))
                        Directory.CreateDirectory(destinationFolder);
                    presentation.Export(Path: destinationFolder, FilterName: "PNG");
                    presentation.Close();
                }));
                thread.Start();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();

                AppManager.ReleaseComObject(presentation);
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
}
