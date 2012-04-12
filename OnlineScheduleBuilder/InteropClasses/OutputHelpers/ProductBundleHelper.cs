using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Excel = Microsoft.Office.Interop.Excel;

namespace OnlineScheduleBuilder.InteropClasses
{
    public partial class ExcelHelper
    {
        public bool GetProductBundleGrid(BusinessClasses.Schedule source, int startProduct, int endProduct)
        {
            bool result = false;
            try
            {
                string tempplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.ExcelTemplatesFolderPath, string.Format(BusinessClasses.OutputManager.ExcelTemplateFileName, "Bundle", InteropClasses.PowerPointHelper.Instance.Is2003 ? "03" : "07"));
                if (File.Exists(tempplatePath))
                {

                    Excel.Workbook workBook = _excelObject.Workbooks.Open(tempplatePath);

                    string sheetName = ((endProduct - startProduct) + ((endProduct - startProduct) == 5 || endProduct < 5 ? 0 : 5)).ToString() + " products";
                    Excel.Worksheet workSheet = workBook.Sheets[sheetName];

                    for (int i = startProduct; i < endProduct; i++)
                    {

                        List<string> detailsText = new List<string>();
                        if (source.ProductBundleSettings.ShowDimensions && !string.IsNullOrEmpty(source.Products[i].Dimensions))
                            detailsText.Add("Ad Dimensions: " + source.Products[i].Dimensions);
                        if (source.ProductBundleSettings.ShowWebsites && !string.IsNullOrEmpty(source.Products[i].AllWebsites))
                            detailsText.Add("Websites: " + source.Products[i].AllWebsites);
                        if (source.ProductBundleSettings.ShowTotalAds && source.Products[i].TotalAds.HasValue)
                            detailsText.Add("Total Ads: " + source.Products[i].TotalAds.Value.ToString("#,##0"));
                        if (source.ProductBundleSettings.ShowActiveDays && source.Products[i].ActiveDays.HasValue)
                            detailsText.Add("Active Days: " + source.Products[i].ActiveDays.Value.ToString("#,##0"));
                        if (source.ProductBundleSettings.ShowAdRate && source.Products[i].AdRate.HasValue)
                            detailsText.Add("Ad Rate: " + source.Products[i].AdRate.Value.ToString("$#,###.00"));

                        if (source.ProductBundleSettings.ShowActiveDays |
                            source.ProductBundleSettings.ShowAdRate |
                            source.ProductBundleSettings.ShowDimensions |
                            source.ProductBundleSettings.ShowTotalAds |
                            source.ProductBundleSettings.ShowWebsites)
                        {
                            workSheet.Range["product" + ((i - startProduct) + 1).ToString()].Value = source.Products[i].Name;
                            workSheet.Range["details" + ((i - startProduct) + 1).ToString()].Value = string.Join(";  ", detailsText.ToArray());
                        }
                        else
                        {
                            workSheet.Range["product" + ((i - startProduct) + 1).ToString()].Value = string.Empty;
                            workSheet.Range["details" + ((i - startProduct) + 1).ToString()].Value = source.Products[i].Name;
                        }
                    }

                    if ((source.ProductBundleSettings.ShowTotalsOnLastOnly && endProduct == source.Products.Count) || !source.ProductBundleSettings.ShowTotalsOnLastOnly)
                    {
                        workSheet.Range["combhdr1"].Value = source.ProductBundleSettings.TotalHeader1;
                        workSheet.Range["combvalue1"].Value = source.ProductBundleSettings.TotalValue1;
                        workSheet.Range["cpmvalue1"].Value = source.ProductBundleSettings.TotalCPM1;
                        workSheet.Range["combhdr2"].Value = source.ProductBundleSettings.TotalHeader2;
                        workSheet.Range["combvalue2"].Value = source.ProductBundleSettings.TotalValue2;
                        workSheet.Range["cpmvalue2"].Value = source.ProductBundleSettings.TotalCPM2;
                        workSheet.Range["combhdr3"].Value = source.ProductBundleSettings.TotalHeader3;
                        workSheet.Range["combvalue3"].Value = source.ProductBundleSettings.TotalValue3;
                        workSheet.Range["cpmvalue3"].Value = source.ProductBundleSettings.TotalCPM3;
                        workSheet.Range["combhdr4"].Value = source.ProductBundleSettings.TotalHeader4;
                        workSheet.Range["combvalue4"].Value = source.ProductBundleSettings.TotalValue4;
                        workSheet.Range["cpmvalue4"].Value = source.ProductBundleSettings.TotalCPM4;
                    }
                    else
                    {
                        workSheet.Range["combhdr1"].Value = string.Empty;
                        workSheet.Range["combvalue1"].Value = string.Empty;
                        workSheet.Range["cpmvalue1"].Value = string.Empty;
                        workSheet.Range["combhdr2"].Value = string.Empty;
                        workSheet.Range["combvalue2"].Value = string.Empty;
                        workSheet.Range["cpmvalue2"].Value = string.Empty;
                        workSheet.Range["combhdr3"].Value = string.Empty;
                        workSheet.Range["combvalue3"].Value = string.Empty;
                        workSheet.Range["cpmvalue3"].Value = string.Empty;
                        workSheet.Range["combhdr4"].Value = string.Empty;
                        workSheet.Range["combvalue4"].Value = string.Empty;
                        workSheet.Range["cpmvalue4"].Value = string.Empty;
                    }

                    workSheet.Range["areaoutput"].Copy();

                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }
    }

    public partial class PowerPointHelper
    {
        public void AppendProductBundle(BusinessClasses.Schedule source, bool pasteAsImage, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.ProductSummaryTemplatesFolderPath))
            {
                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.ProductSummaryTemplatesFolderPath, BusinessClasses.OutputManager.ProductSummaryTemplateFileName);
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();

                            ExcelHelper excelHelper = new ExcelHelper();

                            for (int k = 0; k < source.Products.Count; k += 5)
                            {

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
                                                    shape.TextFrame.TextRange.Text = source.ProductBundleSettings.SlideHeader;
                                                    break;
                                                case "DATETAG":
                                                    shape.TextFrame.TextRange.Text = source.PresentationDate.ToString("MM/dd/yy");
                                                    break;
                                                case "ADVERTISER":
                                                    shape.TextFrame.TextRange.Text = source.BusinessName;
                                                    break;
                                                case "DECMKR":
                                                    shape.TextFrame.TextRange.Text = source.DecisionMaker;
                                                    break;
                                                case "FLTDT1":
                                                    shape.TextFrame.TextRange.Text = source.FlightDates;
                                                    break;
                                                case "PRODUCTS":
                                                    if (excelHelper.Connect())
                                                    {
                                                        if (!excelHelper.GetProductBundleGrid(source, k, ((k + 5) < source.Products.Count) ? (k + 5) : source.Products.Count))
                                                        {
                                                            excelHelper.Disconnect();
                                                            break;
                                                        }
                                                        PowerPoint.ShapeRange shapeRange = null;
                                                        if (pasteAsImage)
                                                            shapeRange = slide.Shapes.PasteSpecial(DataType: PowerPoint.PpPasteDataType.ppPasteEnhancedMetafile);
                                                        else
                                                            shapeRange = slide.Shapes.PasteSpecial(DataType: PowerPoint.PpPasteDataType.ppPasteOLEObject);
                                                        shapeRange.Top = shape.Top;
                                                        shapeRange.Left = shape.Left;
                                                        shapeRange.Width = shape.Width;
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        excelHelper.Disconnect();
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                }
                                AppendSlide(presentation, -1, destinationPresentation);
                                presentation.Close();
                            }
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

        public void PrepareBundleEmail(string fileName, BusinessClasses.Schedule outputSource, bool pasteAsImage)
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
                AppendProductBundle(outputSource, pasteAsImage, presentation);
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
