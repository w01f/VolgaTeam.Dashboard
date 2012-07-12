using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace RadioScheduleBuilder.InteropClasses
{
    public partial class ExcelHelper
    {
        public bool GetOneSheetExcel(BusinessClasses.OutputScheduleGridBased outputPage)
        {
            bool result = false;
            Excel.Range range = null;
            try
            {
                string templatePath = Path.Combine(string.Format(BusinessClasses.OutputManager.OneSheetsExcelTemplatesFolderName, new object[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), (InteropClasses.PowerPointHelper.Instance.Is2003 ? "03" : "07"), outputPage.ProgramsPerSlide, outputPage.ExcelTemplatesSubFolderName }), string.Format(BusinessClasses.OutputManager.OneSheetsExcelTemplateFileName, new object[] { outputPage.ExcelTemplateFileName }));
                if (File.Exists(templatePath))
                {
                    Excel.Workbook workBook = _excelObject.Workbooks.Open(templatePath);
                    string workSheetName = outputPage.SpotsPerSlide.ToString();
                    Excel.Worksheet workSheet = workBook.Sheets[workSheetName];
                    NamedRanges namedRanges = new NamedRanges(workSheet);
                    int programsCount = outputPage.Programs.Count;
                    int spotsCount = outputPage.TotalSpots.Count;

                    #region Fill Header and Footer
                    if (outputPage.ShowRating && outputPage.ShowRates)
                        namedRanges.SetValue("Rtgheader", outputPage.RtgHeaderTitle);
                    else if (outputPage.ShowRating)
                        namedRanges.SetValue("Rtgheader", outputPage.RtgHeaderTitle);
                    else if (outputPage.ShowRates)
                        namedRanges.SetValue("Rtgheader", "Rate");

                    if (outputPage.ShowCPP && outputPage.ShowGRP && outputPage.ShowCost)
                    {
                        namedRanges.SetValue("GRPHeader", outputPage.GRPHeaderTitle);
                        namedRanges.SetValue("CPPCPMHeader", outputPage.CPPHeaderTitle);

                        namedRanges.SetValue("Sumpts", outputPage.TotalGRP);
                        namedRanges.SetValue("SumDollars", outputPage.TotalCost);
                        namedRanges.SetValue("OverallCPPCPM", outputPage.TotalCPP);
                    }
                    else if (outputPage.ShowCPP && outputPage.ShowGRP)
                    {
                        namedRanges.SetValue("GRPHeader", outputPage.GRPHeaderTitle);
                        namedRanges.SetValue("TotalCostHeader", outputPage.CPPHeaderTitle);

                        namedRanges.SetValue("Sumpts", outputPage.TotalGRP);
                        namedRanges.SetValue("SumDollars", outputPage.TotalCPP);
                    }
                    else if (outputPage.ShowCost && outputPage.ShowCPP)
                    {
                        namedRanges.SetValue("GRPHeader", "Total");
                        namedRanges.SetValue("TotalCostHeader", outputPage.CPPHeaderTitle);

                        namedRanges.SetValue("Sumpts", outputPage.TotalCost);
                        namedRanges.SetValue("SumDollars", outputPage.TotalCPP);
                    }
                    else if (outputPage.ShowCost && outputPage.ShowGRP)
                    {
                        namedRanges.SetValue("GRPHeader", outputPage.GRPHeaderTitle);

                        namedRanges.SetValue("Sumpts", outputPage.TotalGRP);
                        namedRanges.SetValue("SumDollars", outputPage.TotalCost);
                    }
                    else if (outputPage.ShowCost)
                    {
                        namedRanges.SetValue("GRPHeader", "Total");

                        namedRanges.SetValue("Sumpts", outputPage.TotalCost);
                    }
                    else if (outputPage.ShowGRP)
                    {
                        namedRanges.SetValue("GRPHeader", outputPage.GRPHeaderTitle);

                        namedRanges.SetValue("Sumpts", outputPage.TotalGRP);
                    }
                    else if (outputPage.ShowCPP)
                    {
                        namedRanges.SetValue("GRPHeader", outputPage.CPPHeaderTitle);

                        namedRanges.SetValue("Sumpts", outputPage.TotalCPP);
                    }

                    namedRanges.SetValue("SumSpots", outputPage.TotalSpot);

                    #region Fill Spots Header and Footer
                    if (outputPage.ShowSpots)
                    {
                        if (spotsCount > 0)
                        {
                            for (int i = 0; i < spotsCount; i++)
                            {
                                namedRanges.SetValue("week" + (i + 1).ToString(), outputPage.TotalSpots[i].Month);
                                namedRanges.SetValue("date" + (i + 1).ToString(), outputPage.TotalSpots[i].Day);
                                namedRanges.SetValue("totalspts" + (i + 1).ToString(), outputPage.TotalSpots[i].Value);
                            }
                        }
                    }
                    #endregion

                    #endregion

                    #region Fill Programs
                    for (int i = 0; i < programsCount; i++)
                    {
                        namedRanges.SetValue("line" + (i + 1).ToString(), outputPage.Programs[i].LineID);
                        namedRanges.SetValue("station" + (i + 1).ToString(), outputPage.Programs[i].Station);
                        namedRanges.SetValue("pg" + (i + 1).ToString(), outputPage.Programs[i].Name);
                        namedRanges.SetValue("day" + (i + 1).ToString(), outputPage.Programs[i].Days);
                        namedRanges.SetValue("time" + (i + 1).ToString(), outputPage.Programs[i].Time);
                        namedRanges.SetValue("lgt" + (i + 1).ToString(), outputPage.Programs[i].Length);

                        if (outputPage.ShowRating && outputPage.ShowRates)
                        {
                            namedRanges.SetValue("rate" + (i + 1).ToString(), outputPage.Programs[i].Rate);
                            namedRanges.SetValue("rtgimp" + (i + 1).ToString(), outputPage.Programs[i].Rating);
                        }
                        else if (outputPage.ShowRating)
                            namedRanges.SetValue("rtgimp" + (i + 1).ToString(), outputPage.Programs[i].Rating);
                        else if (outputPage.ShowRates)
                            namedRanges.SetValue("rtgimp" + (i + 1).ToString(), outputPage.Programs[i].Rate);

                        namedRanges.SetValue("totspt" + (i + 1).ToString(), outputPage.Programs[i].TotalSpots);

                        if (outputPage.ShowCPP && outputPage.ShowGRP && outputPage.ShowCost)
                        {
                            namedRanges.SetValue("pts" + (i + 1).ToString(), outputPage.Programs[i].GRP);
                            namedRanges.SetValue("cost" + (i + 1).ToString(), outputPage.Programs[i].TotalRate);
                            namedRanges.SetValue("cppcpm" + (i + 1).ToString(), outputPage.Programs[i].CPP);
                        }
                        else if (outputPage.ShowCPP && outputPage.ShowGRP)
                        {
                            namedRanges.SetValue("pts" + (i + 1).ToString(), outputPage.Programs[i].GRP);
                            namedRanges.SetValue("cost" + (i + 1).ToString(), outputPage.Programs[i].CPP);
                        }
                        else if (outputPage.ShowCost && outputPage.ShowCPP)
                        {
                            namedRanges.SetValue("pts" + (i + 1).ToString(), outputPage.Programs[i].TotalRate);
                            namedRanges.SetValue("cost" + (i + 1).ToString(), outputPage.Programs[i].CPP);
                        }
                        else if (outputPage.ShowCost && outputPage.ShowGRP)
                        {
                            namedRanges.SetValue("pts" + (i + 1).ToString(), outputPage.Programs[i].GRP);
                            namedRanges.SetValue("cost" + (i + 1).ToString(), outputPage.Programs[i].TotalRate);
                        }
                        else if (outputPage.ShowCost)
                            namedRanges.SetValue("pts" + (i + 1).ToString(), outputPage.Programs[i].TotalRate);
                        else if (outputPage.ShowGRP)
                            namedRanges.SetValue("pts" + (i + 1).ToString(), outputPage.Programs[i].GRP);
                        else if (outputPage.ShowCPP)
                            namedRanges.SetValue("pts" + (i + 1).ToString(), outputPage.Programs[i].CPP);


                        #region Fill Program Spots
                        if (outputPage.ShowSpots)
                        {
                            for (int j = 0; j < spotsCount; j++)
                            {
                                namedRanges.SetValue("line" + (i + 1).ToString() + "spots" + (j + 1).ToString(), outputPage.Programs[i].Spots[j]);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region Delete Empty Program Positions
                    for (int i = 0; i < outputPage.ProgramsPerSlide - programsCount; i++)
                    {
                        range = namedRanges["pg" + (programsCount + i + 1).ToString()];
                        if (range != null)
                        {
                            int topProgramRow = range.Row;
                            int bottomProgramRow = range.Row + 1;
                            range = workSheet.Range["A" + topProgramRow.ToString(), "B" + bottomProgramRow.ToString()];
                            range.EntireRow.Delete();
                        }
                    }
                    #endregion

                    #region Copy Output Area
                    workSheet.Range["area" + outputPage.SpotsPerSlide.ToString()].Copy();
                    result = true;
                    #endregion
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
        public void AppendOneSheetExcelBased(BusinessClasses.OutputScheduleGridBased[] pages, bool pasteAsImage, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetExcelBasedTemplatesFolderPath))
            {
                BusinessClasses.OutputScheduleGridBased defaultPage = pages.FirstOrDefault();
                try
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        ExcelHelper excelHelper = new ExcelHelper();
                        if (defaultPage != null)
                        {
                            string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetExcelBasedTemplatesFolderPath, BusinessClasses.OutputManager.OneSheetExcelBasedTemplateFileName);
                            if (File.Exists(presentationTemplatePath))
                            {
                                int totalSlides = pages.Length;
                                int slideCounter = 1;
                                foreach (BusinessClasses.OutputScheduleGridBased page in pages)
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
                                                        //shape.TextFrame.TextRange.Text = outputPage.Title;
                                                        break;
                                                    case "ADVERTISER":
                                                        shape.TextFrame.TextRange.Text = page.Advertiser;
                                                        break;
                                                    case "DECISIONMAKER":
                                                        shape.TextFrame.TextRange.Text = page.DecisionMaker;
                                                        break;
                                                    case "FLIGHTDATES":
                                                        shape.TextFrame.TextRange.Text = page.FlightDates;
                                                        break;
                                                    case "SALESREPINFO":
                                                        shape.TextFrame.TextRange.Text = string.Empty;
                                                        break;
                                                    case "DEMO1":
                                                        shape.TextFrame.TextRange.Text = page.Demo;
                                                        break;

                                                    #region Totals
                                                    case "TOTAL1":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 0 ? page.Totals.Keys.ElementAt(0) : string.Empty;
                                                        break;
                                                    case "TTL1B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 0 ? page.Totals.Values.ElementAt(0) : string.Empty;
                                                        break;
                                                    case "TOTAL2":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 1 ? page.Totals.Keys.ElementAt(1) : string.Empty;
                                                        break;
                                                    case "TTL2B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 1 ? page.Totals.Values.ElementAt(1) : string.Empty;
                                                        break;
                                                    case "TOTAL3":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 2 ? page.Totals.Keys.ElementAt(2) : string.Empty;
                                                        break;
                                                    case "TTL3B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 2 ? page.Totals.Values.ElementAt(2) : string.Empty;
                                                        break;
                                                    case "TOTAL4":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 || page.Totals.Count == 4 ? page.Totals.Keys.ElementAt(3) : string.Empty;
                                                        break;
                                                    case "TTL4B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 || page.Totals.Count == 4 ? page.Totals.Values.ElementAt(3) : string.Empty;
                                                        break;
                                                    case "TOTAL5":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Keys.ElementAt(4) : (page.Totals.Count > 4 ? page.Totals.Keys.ElementAt(3) : string.Empty);
                                                        break;
                                                    case "TTL5B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Values.ElementAt(4) : (page.Totals.Count > 4 ? page.Totals.Values.ElementAt(3) : string.Empty);
                                                        break;
                                                    case "TOTAL6":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Keys.ElementAt(5) : (page.Totals.Count > 4 ? page.Totals.Keys.ElementAt(4) : string.Empty);
                                                        break;
                                                    case "TTL6B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Values.ElementAt(5) : (page.Totals.Count > 4 ? page.Totals.Values.ElementAt(4) : string.Empty);
                                                        break;
                                                    case "TOTAL7":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Keys.ElementAt(6) : (page.Totals.Count > 5 ? page.Totals.Keys.ElementAt(5) : string.Empty);
                                                        break;
                                                    case "TTL7B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Values.ElementAt(6) : (page.Totals.Count > 5 ? page.Totals.Values.ElementAt(5) : string.Empty);
                                                        break;
                                                    case "TOTAL8":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 7 ? page.Totals.Keys.ElementAt(7) : string.Empty;
                                                        break;
                                                    case "TTL8B":
                                                        shape.TextFrame.TextRange.Text = page.Totals.Count > 7 ? page.Totals.Values.ElementAt(7) : string.Empty;
                                                        break;
                                                    #endregion

                                                    case "EXCELGRID":
                                                        if (excelHelper.Connect())
                                                        {

                                                            if (excelHelper.GetOneSheetExcel(page))
                                                            {
                                                                try
                                                                {
                                                                    PowerPoint.ShapeRange shapeRange = null;
                                                                    if (pasteAsImage)
                                                                        shapeRange = slide.Shapes.PasteSpecial(DataType: PowerPoint.PpPasteDataType.ppPasteEnhancedMetafile);
                                                                    else
                                                                        shapeRange = slide.Shapes.PasteSpecial(DataType: PowerPoint.PpPasteDataType.ppPasteOLEObject);
                                                                    shapeRange.Top = shape.Top;
                                                                    shapeRange.Left = shape.Left;
                                                                    shapeRange.Width = shape.Width;
                                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            excelHelper.Disconnect();
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    slideCounter++;
                                    AppendSlide(presentation, -1, destinationPresentation);
                                    presentation.Close();
                                }
                            }
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

        public void PrepareOneSheetEmailExcelBased(string fileName, BusinessClasses.OutputScheduleGridBased[] pages, bool pasteAsImage)
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
                AppendOneSheetExcelBased(pages, pasteAsImage, presentation);
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

        public void AppendOneSheetTableBased(BusinessClasses.OutputScheduleGridBased page, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetTableBasedTemplatesFolderPath) && page != null)
            {
                try
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (page != null)
                        {
                            string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetTableBasedTemplatesFolderPath, string.Format(BusinessClasses.OutputManager.OneSheetTableBasedTemplateFileName, new object[] { page.ProgramsPerSlide.ToString(), page.SpotsPerSlide.ToString() }));
                            if (File.Exists(presentationTemplatePath))
                            {
                                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                int outputSlideIndex = page.GetGridBasedSlideNumber();
                                PowerPoint.Slide slide = presentation.Slides.Count > 0 && outputSlideIndex >= 0 ? presentation.Slides[outputSlideIndex] : null;
                                if (slide != null)
                                {
                                    foreach (PowerPoint.Shape shape in slide.Shapes)
                                    {
                                        for (int i = 1; i <= shape.Tags.Count; i++)
                                        {
                                            switch (shape.Tags.Name(i))
                                            {
                                                case "HEADER":
                                                    //shape.TextFrame.TextRange.Text = page.Title;
                                                    break;
                                                case "ADVERTISER":
                                                    shape.TextFrame.TextRange.Text = !string.IsNullOrEmpty(page.Advertiser) ? page.Advertiser : (!string.IsNullOrEmpty(page.DecisionMaker) ? page.FlightDates : string.Empty);
                                                    break;
                                                case "DECISIONMAKER":
                                                    shape.TextFrame.TextRange.Text = !string.IsNullOrEmpty(page.DecisionMaker) ? page.DecisionMaker : page.FlightDates;
                                                    break;
                                                case "FLIGHTDATES":
                                                    shape.TextFrame.TextRange.Text = !string.IsNullOrEmpty(page.Advertiser) && !string.IsNullOrEmpty(page.DecisionMaker) ? page.FlightDates : string.Empty;
                                                    break;
                                                case "SALESREPINFO":
                                                    shape.TextFrame.TextRange.Text = string.Empty;
                                                    break;
                                                case "DEMO1":
                                                    shape.TextFrame.TextRange.Text = page.Demo;
                                                    break;

                                                #region Totals
                                                case "TOTAL1":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 0 ? page.Totals.Keys.ElementAt(0) : string.Empty;
                                                    break;
                                                case "TTL1B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 0 ? page.Totals.Values.ElementAt(0) : string.Empty;
                                                    break;
                                                case "TOTAL2":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 1 ? page.Totals.Keys.ElementAt(1) : string.Empty;
                                                    break;
                                                case "TTL2B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 1 ? page.Totals.Values.ElementAt(1) : string.Empty;
                                                    break;
                                                case "TOTAL3":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 2 ? page.Totals.Keys.ElementAt(2) : string.Empty;
                                                    break;
                                                case "TTL3B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 2 ? page.Totals.Values.ElementAt(2) : string.Empty;
                                                    break;
                                                case "TOTAL4":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 || page.Totals.Count == 4 ? page.Totals.Keys.ElementAt(3) : string.Empty;
                                                    break;
                                                case "TTL4B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 || page.Totals.Count == 4 ? page.Totals.Values.ElementAt(3) : string.Empty;
                                                    break;
                                                case "TOTAL5":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Keys.ElementAt(4) : (page.Totals.Count > 4 ? page.Totals.Keys.ElementAt(3) : string.Empty);
                                                    break;
                                                case "TTL5B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Values.ElementAt(4) : (page.Totals.Count > 4 ? page.Totals.Values.ElementAt(3) : string.Empty);
                                                    break;
                                                case "TOTAL6":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Keys.ElementAt(5) : (page.Totals.Count > 4 ? page.Totals.Keys.ElementAt(4) : string.Empty);
                                                    break;
                                                case "TTL6B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Values.ElementAt(5) : (page.Totals.Count > 4 ? page.Totals.Values.ElementAt(4) : string.Empty);
                                                    break;
                                                case "TOTAL7":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Keys.ElementAt(6) : (page.Totals.Count > 5 ? page.Totals.Keys.ElementAt(5) : string.Empty);
                                                    break;
                                                case "TTL7B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count >= 7 ? page.Totals.Values.ElementAt(6) : (page.Totals.Count > 5 ? page.Totals.Values.ElementAt(5) : string.Empty);
                                                    break;
                                                case "TOTAL8":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 7 ? page.Totals.Keys.ElementAt(7) : string.Empty;
                                                    break;
                                                case "TTL8B":
                                                    shape.TextFrame.TextRange.Text = page.Totals.Count > 7 ? page.Totals.Values.ElementAt(7) : string.Empty;
                                                    break;
                                                #endregion
                                            }
                                        }

                                        if (shape.HasTable == Microsoft.Office.Core.MsoTriState.msoTrue)
                                        {
                                            PowerPoint.Table table = shape.Table;
                                            for (int i = 1; i <= table.Rows.Count; i++)
                                                for (int j = 1; j <= table.Columns.Count; j++)
                                                {
                                                    PowerPoint.Shape tableShape = table.Cell(i, j).Shape;
                                                    if (tableShape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                                                    {
                                                        string cellText = tableShape.TextFrame.TextRange.Text.Trim();
                                                        if (page.ReplacementsList.Keys.Contains(cellText))
                                                        {
                                                            tableShape.TextFrame.TextRange.Text = page.ReplacementsList[cellText];
                                                            page.ReplacementsList.Remove(cellText);
                                                        }
                                                        else
                                                        {
                                                            KeyValuePair<string, string>[] replacementRecords = page.ReplacementsList.Where(x => Regex.Split(cellText, @"\W").Any(w => w.Equals(x.Key))).ToArray();
                                                            foreach (var replacementRecord in replacementRecords)
                                                            {
                                                                cellText = cellText.Replace(replacementRecord.Key, replacementRecord.Value.Trim());
                                                                tableShape.TextFrame.TextRange.Text = cellText;
                                                                page.ReplacementsList.Remove(replacementRecord.Key);
                                                            }
                                                        }
                                                    }
                                                }
                                        }
                                    }
                                    AppendSlide(presentation, outputSlideIndex, destinationPresentation);
                                    presentation.Close();
                                }
                            }
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

        public void PrepareOneSheetEmailTableBased(string fileName, BusinessClasses.OutputScheduleGridBased page)
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
                AppendOneSheetTableBased(page, presentation);
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

        public void AppendOneSheetSlideMasterBased(BusinessClasses.OutputScheduleTagsBased[] pages, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath))
            {
                BusinessClasses.OutputScheduleTagsBased defaultPage = pages.FirstOrDefault();
                try
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (defaultPage != null)
                        {
                            int totalSlides = pages.Length;
                            int slideCounter = 1;
                            foreach (BusinessClasses.OutputScheduleTagsBased page in pages)
                            {
                                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath, page.GetSlideTemplateName());
                                if (File.Exists(presentationTemplatePath))
                                {
                                    PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                    PowerPoint.Slide tagedSlide = presentation.Slides.Count > 0 ? presentation.Slides[1] : null;
                                    PowerPoint.Slide newSlide = presentation.Slides.Add(1, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank);
                                    PowerPoint.Design design = presentation.Designs.Add(DateTime.Now.ToString("MMddyy-hhmmsstt"));
                                    if (tagedSlide != null)
                                    {
                                        foreach (PowerPoint.Shape shape in tagedSlide.Shapes)
                                        {
                                            if (FillQuickGridTags(page, shape))
                                            {
                                                shape.Copy();
                                                design.SlideMaster.Shapes.Paste();
                                            }
                                            else
                                            {
                                                shape.Copy();
                                                newSlide.Shapes.Paste();
                                            }
                                        }
                                    }
                                    slideCounter++;
                                    newSlide.Design = design;
                                    AppendSlide(presentation, 1, destinationPresentation);
                                    presentation.Close();
                                    AppManager.ReleaseComObject(design);
                                    AppManager.ReleaseComObject(tagedSlide);
                                    AppManager.ReleaseComObject(newSlide);
                                    AppManager.ReleaseComObject(presentation);
                                }
                            }
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

        public void PrepareOneSheetEmailSlideMasterBased(string fileName, BusinessClasses.OutputScheduleTagsBased[] pages)
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
                AppendOneSheetSlideMasterBased(pages, presentation);
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

        public void AppendOneSheetGroupedTextBased(BusinessClasses.OutputScheduleTagsBased[] pages, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath))
            {
                BusinessClasses.OutputScheduleTagsBased defaultPage = pages.FirstOrDefault();
                try
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (defaultPage != null)
                        {
                            int totalSlides = pages.Length;
                            int slideCounter = 1;
                            foreach (BusinessClasses.OutputScheduleTagsBased page in pages)
                            {
                                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetTagsBasedTemplatesFolderPath, page.GetSlideTemplateName());
                                if (File.Exists(presentationTemplatePath))
                                {
                                    PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                    PowerPoint.Slide tagedSlide = presentation.Slides.Count > 0 ? presentation.Slides[1] : null;
                                    PowerPoint.Slide newSlide = presentation.Slides.Add(1, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank);
                                    List<int> shapesIndexes = new List<int>();
                                    if (tagedSlide != null)
                                    {
                                        int shapeIndex = 1;
                                        foreach (PowerPoint.Shape shape in tagedSlide.Shapes)
                                        {
                                            if (FillQuickGridTags(page, shape))
                                            {
                                                shape.Copy();
                                                shapesIndexes.Add(shapeIndex);
                                            }
                                            else
                                                shape.Copy();
                                            newSlide.Shapes.Paste();
                                            shapeIndex++;
                                        }
                                    }
                                    newSlide.Shapes.Range(shapesIndexes.ToArray()).Group();
                                    slideCounter++;
                                    AppendSlide(presentation, 1, destinationPresentation);
                                    presentation.Close();
                                    AppManager.ReleaseComObject(tagedSlide);
                                    AppManager.ReleaseComObject(newSlide);
                                    AppManager.ReleaseComObject(presentation);
                                }
                            }
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

        public void PrepareOneSheetEmailGroupedTextBased(string fileName, BusinessClasses.OutputScheduleTagsBased[] pages)
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
                AppendOneSheetGroupedTextBased(pages, presentation);
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

        private bool FillQuickGridTags(BusinessClasses.OutputScheduleTagsBased page, PowerPoint.Shape shape)
        {
            bool allowToCopy = true;
            for (int i = 1; i <= shape.Tags.Count; i++)
            {
                switch (shape.Tags.Name(i).Trim())
                {
                    #region Basic Info
                    case "ADVERTISING PLAN":
                        allowToCopy = false; break;
                    case "ADVERTISERTAG":
                        shape.TextFrame.TextRange.Text = page.Advertiser;
                        allowToCopy = false; break;
                    case "DECISIONMAKERTAG":
                        shape.TextFrame.TextRange.Text = page.DecisionMaker;
                        allowToCopy = false; break;
                    case "FLIGHTDATESTAG":
                        shape.TextFrame.TextRange.Text = page.FlightDates;
                        allowToCopy = false; break;
                    case "SOURCEBOOKTAG":
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    case "SALESREPTAG":
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    #endregion

                    #region Programs
                    #region Program 1
                    #region Properties
                    case "BAR1":
                        if (!(page.Programs.Count > 0))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMA_(LGTHA)":
                        if (page.Programs.Count > 0 && !string.IsNullOrEmpty(page.Programs[0].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[0].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "01":
                        if (page.Programs.Count > 0 && !string.IsNullOrEmpty(page.Programs[0].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[0].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONA_DAYA_TIMEA_RATEA_RTGA":
                        if (page.Programs.Count > 0 && !string.IsNullOrEmpty(page.Programs[0].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[0].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1A":
                        if (page.Programs.Count > 0 && !string.IsNullOrEmpty(page.Programs[0].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[0].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2A":
                        if (page.Programs.Count > 0 && !string.IsNullOrEmpty(page.Programs[0].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[0].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3A":
                        if (page.Programs.Count > 0 && !string.IsNullOrEmpty(page.Programs[0].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[0].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4A":
                        if (page.Programs.Count > 0 && !string.IsNullOrEmpty(page.Programs[0].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[0].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13A":
                        if (page.Programs.Count > 0 && page.Programs[0].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[0].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 2
                    #region Properties
                    case "BAR2":
                        if (!(page.Programs.Count > 1))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMB_(LGTHB)":
                        if (page.Programs.Count > 1 && !string.IsNullOrEmpty(page.Programs[1].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[1].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "02":
                        if (page.Programs.Count > 1 && !string.IsNullOrEmpty(page.Programs[1].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[1].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONB_DAYB_TIMEB_RATEB_RTGB":
                        if (page.Programs.Count > 1 && !string.IsNullOrEmpty(page.Programs[1].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[1].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1B":
                        if (page.Programs.Count > 1 && !string.IsNullOrEmpty(page.Programs[1].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[1].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2B":
                        if (page.Programs.Count > 1 && !string.IsNullOrEmpty(page.Programs[1].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[1].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3B":
                        if (page.Programs.Count > 1 && !string.IsNullOrEmpty(page.Programs[1].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[1].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4B":
                        if (page.Programs.Count > 1 && !string.IsNullOrEmpty(page.Programs[1].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[1].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13B":
                        if (page.Programs.Count > 1 && page.Programs[1].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[1].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 3
                    #region Properties
                    case "BAR3":
                        if (!(page.Programs.Count > 2))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMC_(LGTHC)":
                        if (page.Programs.Count > 2 && !string.IsNullOrEmpty(page.Programs[2].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[2].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "03":
                        if (page.Programs.Count > 2 && !string.IsNullOrEmpty(page.Programs[2].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[2].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONC_DAYC_TIMEC_RATEC_RTGC":
                        if (page.Programs.Count > 2 && !string.IsNullOrEmpty(page.Programs[2].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[2].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1C":
                        if (page.Programs.Count > 2 && !string.IsNullOrEmpty(page.Programs[2].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[2].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2C":
                        if (page.Programs.Count > 2 && !string.IsNullOrEmpty(page.Programs[2].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[2].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3C":
                        if (page.Programs.Count > 2 && !string.IsNullOrEmpty(page.Programs[2].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[2].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4C":
                        if (page.Programs.Count > 2 && !string.IsNullOrEmpty(page.Programs[2].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[2].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13C":
                        if (page.Programs.Count > 2 && page.Programs[2].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[2].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 4
                    #region Properties
                    case "BAR4":
                        if (!(page.Programs.Count > 3))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMD_(LGTHD)":
                        if (page.Programs.Count > 3 && !string.IsNullOrEmpty(page.Programs[3].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[3].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "04":
                        if (page.Programs.Count > 3 && !string.IsNullOrEmpty(page.Programs[3].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[3].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIOND_DAYD_TIMED_RATED_RTGD":
                        if (page.Programs.Count > 3 && !string.IsNullOrEmpty(page.Programs[3].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[3].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1D":
                        if (page.Programs.Count > 3 && !string.IsNullOrEmpty(page.Programs[3].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[3].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2D":
                        if (page.Programs.Count > 3 && !string.IsNullOrEmpty(page.Programs[3].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[3].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3D":
                        if (page.Programs.Count > 3 && !string.IsNullOrEmpty(page.Programs[3].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[3].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4D":
                        if (page.Programs.Count > 3 && !string.IsNullOrEmpty(page.Programs[3].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[3].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13D":
                        if (page.Programs.Count > 3 && page.Programs[3].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[3].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 5
                    #region Properties
                    case "BAR5":
                        if (!(page.Programs.Count > 4))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAME_(LGTHE)":
                        if (page.Programs.Count > 4 && !string.IsNullOrEmpty(page.Programs[4].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[4].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "05":
                        if (page.Programs.Count > 4 && !string.IsNullOrEmpty(page.Programs[4].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[4].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONE_DAYE_TIMEE_RATEE_RTGE":
                        if (page.Programs.Count > 4 && !string.IsNullOrEmpty(page.Programs[4].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[4].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1E":
                        if (page.Programs.Count > 4 && !string.IsNullOrEmpty(page.Programs[4].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[4].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2E":
                        if (page.Programs.Count > 4 && !string.IsNullOrEmpty(page.Programs[4].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[4].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3E":
                        if (page.Programs.Count > 4 && !string.IsNullOrEmpty(page.Programs[4].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[4].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4E":
                        if (page.Programs.Count > 4 && !string.IsNullOrEmpty(page.Programs[4].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[4].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13E":
                        if (page.Programs.Count > 4 && page.Programs[4].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[4].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 6
                    #region Properties
                    case "BAR6":
                        if (!(page.Programs.Count > 5))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMF_(LGTHF)":
                        if (page.Programs.Count > 5 && !string.IsNullOrEmpty(page.Programs[5].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[5].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "06":
                        if (page.Programs.Count > 5 && !string.IsNullOrEmpty(page.Programs[5].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[5].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONF_DAYF_TIMEF_RATEF_RTGF":
                        if (page.Programs.Count > 5 && !string.IsNullOrEmpty(page.Programs[5].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[5].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1F":
                        if (page.Programs.Count > 5 && !string.IsNullOrEmpty(page.Programs[5].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[5].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2F":
                        if (page.Programs.Count > 5 && !string.IsNullOrEmpty(page.Programs[5].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[5].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3F":
                        if (page.Programs.Count > 5 && !string.IsNullOrEmpty(page.Programs[5].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[5].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4F":
                        if (page.Programs.Count > 5 && !string.IsNullOrEmpty(page.Programs[5].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[5].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13F":
                        if (page.Programs.Count > 5 && page.Programs[5].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[5].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 7
                    #region Properties
                    case "BAR7":
                        if (!(page.Programs.Count > 6))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMG_(LGTHG)":
                        if (page.Programs.Count > 6 && !string.IsNullOrEmpty(page.Programs[6].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[6].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "07":
                        if (page.Programs.Count > 6 && !string.IsNullOrEmpty(page.Programs[6].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[6].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONG_DAYG_TIMEG_RATEG_RTGG":
                        if (page.Programs.Count > 6 && !string.IsNullOrEmpty(page.Programs[6].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[6].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1G":
                        if (page.Programs.Count > 6 && !string.IsNullOrEmpty(page.Programs[6].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[6].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2G":
                        if (page.Programs.Count > 6 && !string.IsNullOrEmpty(page.Programs[6].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[6].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3G":
                        if (page.Programs.Count > 6 && !string.IsNullOrEmpty(page.Programs[6].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[6].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4G":
                        if (page.Programs.Count > 6 && !string.IsNullOrEmpty(page.Programs[6].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[6].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13G":
                        if (page.Programs.Count > 6 && page.Programs[6].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[6].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 8
                    #region Properties
                    case "BAR8":
                        if (!(page.Programs.Count > 7))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMH_(LGTHH)":
                        if (page.Programs.Count > 7 && !string.IsNullOrEmpty(page.Programs[7].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[7].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "08":
                        if (page.Programs.Count > 7 && !string.IsNullOrEmpty(page.Programs[7].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[7].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONH_DAYH_TIMEH_RATEH_RTGH":
                        if (page.Programs.Count > 7 && !string.IsNullOrEmpty(page.Programs[7].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[7].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1H":
                        if (page.Programs.Count > 7 && !string.IsNullOrEmpty(page.Programs[7].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[7].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2H":
                        if (page.Programs.Count > 7 && !string.IsNullOrEmpty(page.Programs[7].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[7].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3H":
                        if (page.Programs.Count > 7 && !string.IsNullOrEmpty(page.Programs[7].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[7].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4H":
                        if (page.Programs.Count > 7 && !string.IsNullOrEmpty(page.Programs[7].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[7].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13H":
                        if (page.Programs.Count > 7 && page.Programs[7].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[7].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 9
                    #region Properties
                    case "BAR9":
                        if (!(page.Programs.Count > 8))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMI_(LGTHI)":
                        if (page.Programs.Count > 8 && !string.IsNullOrEmpty(page.Programs[8].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[8].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "09":
                        if (page.Programs.Count > 8 && !string.IsNullOrEmpty(page.Programs[8].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[8].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONI_DAYI_TIMEI_RATEI_RTGI":
                        if (page.Programs.Count > 8 && !string.IsNullOrEmpty(page.Programs[8].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[8].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1I":
                        if (page.Programs.Count > 8 && !string.IsNullOrEmpty(page.Programs[8].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[8].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2I":
                        if (page.Programs.Count > 8 && !string.IsNullOrEmpty(page.Programs[8].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[8].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3I":
                        if (page.Programs.Count > 8 && !string.IsNullOrEmpty(page.Programs[8].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[8].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4I":
                        if (page.Programs.Count > 8 && !string.IsNullOrEmpty(page.Programs[8].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[8].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13I":
                        if (page.Programs.Count > 8 && page.Programs[8].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[8].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion

                    #region Program 10
                    #region Properties
                    case "BAR10":
                        if (!(page.Programs.Count > 9))
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = true; break;
                    case "PROGRAMJ_(LGTHJ)":
                        if (page.Programs.Count > 9 && !string.IsNullOrEmpty(page.Programs[9].Name))
                            shape.TextFrame.TextRange.Text = page.Programs[9].Name;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10":
                        if (page.Programs.Count > 9 && !string.IsNullOrEmpty(page.Programs[9].LineID))
                            shape.TextFrame.TextRange.Text = page.Programs[9].LineID;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "STATIONJ_DAYJ_TIMEJ_RATEJ_RTGJ":
                        if (page.Programs.Count > 9 && !string.IsNullOrEmpty(page.Programs[9].Properties))
                            shape.TextFrame.TextRange.Text = page.Programs[9].Properties;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM1J":
                        if (page.Programs.Count > 9 && !string.IsNullOrEmpty(page.Programs[9].Item1Value))
                            shape.TextFrame.TextRange.Text = page.Programs[9].Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2J":
                        if (page.Programs.Count > 9 && !string.IsNullOrEmpty(page.Programs[9].Item2Value))
                            shape.TextFrame.TextRange.Text = page.Programs[9].Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3J":
                        if (page.Programs.Count > 9 && !string.IsNullOrEmpty(page.Programs[9].Item3Value))
                            shape.TextFrame.TextRange.Text = page.Programs[9].Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4J":
                        if (page.Programs.Count > 9 && !string.IsNullOrEmpty(page.Programs[9].Item4Value))
                            shape.TextFrame.TextRange.Text = page.Programs[9].Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Spots
                    case "1J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[0];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[1];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[2];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[3];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[4];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[5];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[6];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[7];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[8];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[9];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[10];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[11];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13J":
                        if (page.Programs.Count > 9 && page.Programs[9].Spots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.Programs[9].Spots[12];
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion
                    #endregion
                    #endregion

                    #region  Spot Totals
                    #region Headers
                    case "ITEM1":
                        if (!string.IsNullOrEmpty(page.Item1Title))
                            shape.TextFrame.TextRange.Text = page.Item1Title;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2":
                        if (!string.IsNullOrEmpty(page.Item2Title))
                            shape.TextFrame.TextRange.Text = page.Item2Title;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3":
                        if (!string.IsNullOrEmpty(page.Item3Title))
                            shape.TextFrame.TextRange.Text = page.Item3Title;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4":
                        if (!string.IsNullOrEmpty(page.Item4Title))
                            shape.TextFrame.TextRange.Text = page.Item4Title;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM AA":
                        if (page.TotalSpots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[0].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM BB":
                        if (page.TotalSpots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[1].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM CC":
                        if (page.TotalSpots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[2].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM DD":
                        if (page.TotalSpots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[3].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM EE":
                        if (page.TotalSpots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[4].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM FF":
                        if (page.TotalSpots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[5].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM GG":
                        if (page.TotalSpots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[6].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM HH":
                        if (page.TotalSpots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[7].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM II":
                        if (page.TotalSpots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[8].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM JJ":
                        if (page.TotalSpots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[9].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM KK":
                        if (page.TotalSpots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[10].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM LL":
                        if (page.TotalSpots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[11].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "MM MM":
                        if (page.TotalSpots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[12].HeaderTagText;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    #endregion

                    #region Values
                    case "ITEM1K":
                        if (!string.IsNullOrEmpty(page.Item1Value))
                            shape.TextFrame.TextRange.Text = page.Item1Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM2K":
                        if (!string.IsNullOrEmpty(page.Item2Value))
                            shape.TextFrame.TextRange.Text = page.Item2Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM3K":
                        if (!string.IsNullOrEmpty(page.Item3Value))
                            shape.TextFrame.TextRange.Text = page.Item3Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "ITEM4K":
                        if (!string.IsNullOrEmpty(page.Item4Value))
                            shape.TextFrame.TextRange.Text = page.Item4Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "1K":
                        if (page.TotalSpots.Count > 0)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[0].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "2K":
                        if (page.TotalSpots.Count > 1)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[1].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "3K":
                        if (page.TotalSpots.Count > 2)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[2].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "4K":
                        if (page.TotalSpots.Count > 3)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[3].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "5K":
                        if (page.TotalSpots.Count > 4)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[4].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "6K":
                        if (page.TotalSpots.Count > 5)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[5].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "7K":
                        if (page.TotalSpots.Count > 6)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[6].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "8K":
                        if (page.TotalSpots.Count > 7)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[7].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "9K":
                        if (page.TotalSpots.Count > 8)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[8].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "10K":
                        if (page.TotalSpots.Count > 9)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[9].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "11K":
                        if (page.TotalSpots.Count > 10)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[10].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "12K":
                        if (page.TotalSpots.Count > 11)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[11].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;
                    case "13K":
                        if (page.TotalSpots.Count > 12)
                            shape.TextFrame.TextRange.Text = page.TotalSpots[12].Value;
                        else
                        {
                            shape.TextFrame.TextRange.Text = string.Empty;
                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        }
                        allowToCopy = true; break;

                    #endregion
                    #endregion

                    #region Totals
                    case "TOTALCLIPART":
                        allowToCopy = false; break;
                    case "TOTALTAG1":
                        shape.TextFrame.TextRange.Text = page.TotalTitleToLeft1;
                        allowToCopy = false; break;
                    case "TAG1B":
                        shape.TextFrame.TextRange.Text = page.TotalValueToLeft1;
                        allowToCopy = false; break;
                    case "TOTALTAG2":
                        shape.TextFrame.TextRange.Text = page.TotalTitleToLeft2;
                        allowToCopy = false; break;
                    case "TAG2B":
                        shape.TextFrame.TextRange.Text = page.TotalValueToLeft2;
                        allowToCopy = false; break;
                    case "TOTALTAG3":
                        shape.TextFrame.TextRange.Text = page.TotalTitleToLeft3;
                        allowToCopy = false; break;
                    case "TAG3B":
                        shape.TextFrame.TextRange.Text = page.TotalValueToLeft3;
                        allowToCopy = false; break;
                    case "TOTALTAG4":
                        shape.TextFrame.TextRange.Text = page.TotalTitleToLeft4;
                        allowToCopy = false; break;
                    case "TAG4B":
                        shape.TextFrame.TextRange.Text = page.TotalValueToLeft4;
                        allowToCopy = false; break;
                    #endregion

                    #region Demos
                    #region Demo 1
                    case "DEMOCLIPART":
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    case "DEMO1":
                        shape.TextFrame.TextRange.Text = string.Empty;
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    case "RFGC1A":
                        shape.TextFrame.TextRange.Text = string.Empty;
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    case "RFGC1B":
                        shape.TextFrame.TextRange.Text = string.Empty;
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    case "RFGC1C":
                        shape.TextFrame.TextRange.Text = string.Empty;
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    case "RFGC1D":
                        shape.TextFrame.TextRange.Text = string.Empty;
                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        allowToCopy = false; break;
                    #endregion
                    #endregion
                }
            }
            return allowToCopy;
        }
    }
}
