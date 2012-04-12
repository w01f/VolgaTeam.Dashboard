using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace TVScheduleBuilder.InteropClasses
{
    public partial class ExcelHelper
    {
        public bool GetOneSheetExcel(BusinessClasses.OutputSchedule outputPage)
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
        public void AppendOneSheetExcelBased(BusinessClasses.OutputSchedule[] pages, bool pasteAsImage, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetExcelBasedTemplatesFolderPath))
            {
                BusinessClasses.OutputSchedule defaultPage = pages.FirstOrDefault();
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
                                foreach (BusinessClasses.OutputSchedule page in pages)
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
                                                                    if(pasteAsImage)
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

        public void PrepareOneSheetEmailExcelBased(string fileName, BusinessClasses.OutputSchedule[] pages, bool pasteAsImage)
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
                //AppendOneSheetExcelBased(pages, presentation);
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

        public void AppendOneSheetGridBased(BusinessClasses.OutputSchedule page, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.OneSheetGridBasedTemplatesFolderPath) && page != null)
            {
                try
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (page != null)
                        {
                            string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetGridBasedTemplatesFolderPath, string.Format(BusinessClasses.OutputManager.OneSheetGridBasedTemplateFileName, new object[] { page.ProgramsPerSlide.ToString(), page.SpotsPerSlide.ToString() }));
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

        public void PrepareOneSheetEmailGridBased(string fileName, BusinessClasses.OutputSchedule page)
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
                AppendOneSheetGridBased(page, presentation);
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
