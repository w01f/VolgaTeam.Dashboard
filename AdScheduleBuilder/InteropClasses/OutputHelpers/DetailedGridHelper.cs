using System.Collections.Generic;
using System.IO;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace AdScheduleBuilder.InteropClasses
{
    public partial class ExcelHelper
    {
        public bool GetDetailedGrid(OutputClasses.OutputControls.PublicationDetailedGridControl outputControl, int slideIndex)
        {
            bool result = false;
            try
            {
                Excel.Workbook workBook = _excelObject.Workbooks.Open(BusinessClasses.OutputManager.Instance.ExcelOutputTemplateFilePath);
                Excel.Worksheet workSheet = workBook.Sheets[OutputClasses.OutputControls.OutputDetailedGridControl.Instance.ShowCommentsHeader ? BusinessClasses.OutputManager.DetailedGridTemplateSheetNameWithNotes : BusinessClasses.OutputManager.DetailedGridTemplateSheetNameWithoutNotes];

                string[][] gridData = outputControl.Grid[slideIndex];
                string[] gridHeaders = outputControl.GridHeaders;
                int[] gridHeaderSizes = outputControl.GridHeaderSizes;

                int columnsCount = gridHeaders.Length;
                int columnsPerRecord = OutputClasses.OutputControls.OutputDetailedGridControl.Instance.ShowCommentsHeader ? 3 : 2;
                int rowsCount = gridData.Length;
                int excelRowsCount = 2 + rowsCount * columnsPerRecord - 1;
                double excelGridWidth = (workSheet.Range[ExcelHelper.GetColumnLetterByIndex(0) + ":" + ExcelHelper.GetColumnLetterByIndex(0)].ColumnWidth) * BusinessClasses.OutputManager.Columns;
                double outputGridWidth = gridHeaderSizes.Sum();
                double kDiff = excelGridWidth / outputGridWidth;

                for (int i = 0; i < BusinessClasses.OutputManager.Columns; i++)
                {
                    if (i < columnsCount)
                    {
                        workSheet.Range[ExcelHelper.GetColumnLetterByIndex(i) + ":" + ExcelHelper.GetColumnLetterByIndex(i)].ColumnWidth = (double)gridHeaderSizes[i] * kDiff;
                        workSheet.Range[ExcelHelper.GetColumnLetterByIndex(i) + "1"].Formula = gridHeaders[i];
                        for (int j = 0; j < rowsCount; j++)
                        {
                            int excelMainRowIndex = 2 + (j * columnsPerRecord) + 1;
                            workSheet.Range[ExcelHelper.GetColumnLetterByIndex(i) + excelMainRowIndex.ToString()].Formula = gridData[j][i + (OutputClasses.OutputControls.OutputDetailedGridControl.Instance.ShowCommentsHeader ? 1 : 0)];
                            if (OutputClasses.OutputControls.OutputDetailedGridControl.Instance.ShowCommentsHeader)
                            {
                                int excelCommentRowIndex = 2 + (j * columnsPerRecord) + 2;
                                workSheet.Range[ExcelHelper.GetColumnLetterByIndex(i) + excelCommentRowIndex.ToString()].Formula = gridData[j][0];
                            }
                        }
                    }
                    else
                        workSheet.Range[ExcelHelper.GetColumnLetterByIndex(columnsCount) + ":" + ExcelHelper.GetColumnLetterByIndex(columnsCount)].Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                }
                Excel.Range range = workSheet.Range[ExcelHelper.GetColumnLetterByIndex(0) + "1", ExcelHelper.GetColumnLetterByIndex(columnsCount - 1) + excelRowsCount.ToString()];
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThin;
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].ColorIndex = System.Drawing.Color.Black;
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThin;
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].ColorIndex = System.Drawing.Color.Black;
                range.Copy();

                result = true;
            }
            catch
            {
            }
            return result;
        }
    }

    public partial class PowerPointHelper
    {
        public void AppendDetailedGridExcelBased(OutputClasses.OutputControls.PublicationDetailedGridControl outputControl, bool pasteGridAsImage, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.DetailedGridExcelBasedTemlatesFolderPath))
            {
                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.DetailedGridExcelBasedTemlatesFolderPath, string.Format(BusinessClasses.OutputManager.DetailedGridExcelBasedSlideTemplate, outputControl.OutputFileIndex));
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();

                            ExcelHelper excelHelper = new ExcelHelper();

                            int slidesCount = outputControl.Grid.GetLength(0);
                            for (int k = 0; k < slidesCount; k++)
                            {
                                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && outputControl.ShowAdSpecsOnlyOnLastSlide) || outputControl.DoNotShowAdSpecs;
                                foreach (PowerPoint.Slide slide in presentation.Slides)
                                {
                                    foreach (PowerPoint.Shape shape in slide.Shapes)
                                    {
                                        for (int i = 1; i <= shape.Tags.Count; i++)
                                        {
                                            switch (shape.Tags.Name(i))
                                            {
                                                case "PLOGO":
                                                    if (!string.IsNullOrEmpty(outputControl.LogoFile))
                                                        slide.Shapes.AddPicture(FileName: outputControl.LogoFile, LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "PUBTAG":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationName1;
                                                    break;
                                                case "DATETAG":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationDate1;
                                                    break;
                                                case "PUBTAG2":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationName2;
                                                    break;
                                                case "DATETAG2":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationDate2;
                                                    break;
                                                case "ADVERTISER":
                                                    shape.TextFrame.TextRange.Text = outputControl.BusinessName;
                                                    break;
                                                case "DECISIONMAKER":
                                                    shape.TextFrame.TextRange.Text = outputControl.DecisionMaker;
                                                    break;
                                                case "HEADER":
                                                    shape.TextFrame.TextRange.Text = outputControl.Header;
                                                    break;
                                                case "FLTDT1":
                                                    shape.TextFrame.TextRange.Text = outputControl.FlightDates;
                                                    break;
                                                case "SIGLINE":
                                                    if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "SIGAPPROVAL":
                                                    if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "EXCELGRID":
                                                    if (excelHelper.Connect())
                                                    {
                                                        if (excelHelper.GetDetailedGrid(outputControl, k))
                                                        {
                                                            try
                                                            {
                                                                PowerPoint.ShapeRange shapeRange = null;
                                                                if (pasteGridAsImage)
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
                                                default:
                                                    for (int j = 0; j < 6; j++)
                                                    {
                                                        if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}", j + 1)))
                                                        {
                                                            if (j < outputControl.AdSpecs.Length && !hideAdSpecsOnSlide)
                                                                shape.TextFrame.TextRange.Text = outputControl.AdSpecs[j];
                                                            else
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        }
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

        public void PrepareDetailedGridExcelBasedEmail(string fileName, OutputClasses.OutputControls.PublicationDetailedGridControl[] outputControls, bool pasteAsImage)
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
                foreach (OutputClasses.OutputControls.PublicationDetailedGridControl outputControl in outputControls)
                    AppendDetailedGridExcelBased(outputControl, pasteAsImage, presentation);
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

        public void AppendDetailedGridGridBased(OutputClasses.OutputControls.PublicationDetailedGridControl outputControl,PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.DetailedGridGridBasedTemlatesFolderPath))
            {
                try
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        int slidesCount = outputControl.OutputReplacementsLists.Count;
                        int rowsCount = slidesCount > 0 ? outputControl.Grid[0].GetLength(0) : 0;
                        for (int k = 0; k < slidesCount; k++)
                        {
                            string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.DetailedGridGridBasedTemlatesFolderPath, string.Format(BusinessClasses.OutputManager.DetailedGridGridBasedSlideTemplate, new object[] { OutputClasses.OutputControls.OutputDetailedGridControl.Instance.SelectedColumnsCount, (OutputClasses.OutputControls.OutputDetailedGridControl.Instance.ShowCommentsHeader ? "adnotes" : "no_adnotes"), rowsCount }));
                            int currentSlideRowsCount = outputControl.Grid[k].GetLength(0);
                            if (File.Exists(presentationTemplatePath))
                            {
                                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && outputControl.ShowAdSpecsOnlyOnLastSlide) || outputControl.DoNotShowAdSpecs;
                                foreach (PowerPoint.Slide slide in presentation.Slides)
                                {
                                    foreach (PowerPoint.Shape shape in slide.Shapes)
                                    {
                                        for (int i = 1; i <= shape.Tags.Count; i++)
                                        {
                                            switch (shape.Tags.Name(i))
                                            {
                                                case "PLOGO":
                                                    if (!string.IsNullOrEmpty(outputControl.LogoFile))
                                                        slide.Shapes.AddPicture(FileName: outputControl.LogoFile, LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
                                                    shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "PUBTAG":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationName1;
                                                    break;
                                                case "DATETAG":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationDate1;
                                                    break;
                                                case "PUBTAG2":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationName2;
                                                    break;
                                                case "DATETAG2":
                                                    shape.TextFrame.TextRange.Text = outputControl.PresentationDate2;
                                                    break;
                                                case "ADVERTISER":
                                                    shape.TextFrame.TextRange.Text = outputControl.BusinessName;
                                                    break;
                                                case "DECISIONMAKER":
                                                    shape.TextFrame.TextRange.Text = outputControl.DecisionMaker;
                                                    break;
                                                case "HEADER":
                                                    shape.TextFrame.TextRange.Text = outputControl.Header;
                                                    break;
                                                case "FLTDT1":
                                                    shape.TextFrame.TextRange.Text = outputControl.FlightDates;
                                                    break;
                                                case "SIGLINE":
                                                    if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "SIGAPPROVAL":
                                                    if (!outputControl.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                default:
                                                    for (int j = 0; j < 6; j++)
                                                    {
                                                        if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}", j + 1)))
                                                        {
                                                            if (j < outputControl.AdSpecs.Length && !hideAdSpecsOnSlide)
                                                                shape.TextFrame.TextRange.Text = outputControl.AdSpecs[j];
                                                            else
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        if (shape.HasTable == Microsoft.Office.Core.MsoTriState.msoTrue)
                                        {
                                            PowerPoint.Table table = shape.Table;
                                            int tableRowsCount = table.Rows.Count;
                                            int tableColumnsCount = table.Columns.Count;
                                            int deletedRows = 0;
                                            for (int i = 1; i <= tableRowsCount; i++)
                                            {
                                                if (i <= (currentSlideRowsCount * (OutputClasses.OutputControls.OutputDetailedGridControl.Instance.ShowCommentsHeader ? 2 : 1)) + 1)
                                                {
                                                    for (int j = 1; j <= tableColumnsCount; j++)
                                                    {
                                                        PowerPoint.Shape tableShape = table.Cell(i, j).Shape;
                                                        if (tableShape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                                                        {
                                                            string cellText = tableShape.TextFrame.TextRange.Text.Trim();
                                                            if (outputControl.OutputReplacementsLists[k].Keys.Contains(cellText))
                                                            {
                                                                tableShape.TextFrame.TextRange.Text = outputControl.OutputReplacementsLists[k][cellText];
                                                                outputControl.OutputReplacementsLists[k].Remove(cellText);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    table.Rows[i - deletedRows].Delete();
                                                    deletedRows++;
                                                }
                                            }
                                        }
                                    }
                                }
                                AppendSlide(presentation, -1, destinationPresentation);
                                presentation.Close();
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

        public void PrepareDetailedGridGridBasedEmail(string fileName, OutputClasses.OutputControls.PublicationDetailedGridControl[] outputControls)
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
                foreach (OutputClasses.OutputControls.PublicationDetailedGridControl outputControl in outputControls)
                    AppendDetailedGridGridBased(outputControl, presentation);
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
