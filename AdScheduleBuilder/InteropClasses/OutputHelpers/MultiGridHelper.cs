﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Excel = Microsoft.Office.Interop.Excel;

namespace AdScheduleBuilder.InteropClasses
{
    public partial class ExcelHelper
    {
        public bool GetMultiGrid(int slideIndex)
        {
            bool result = false;
            try
            {
                Excel.Workbook workBook = _excelObject.Workbooks.Open(BusinessClasses.OutputManager.Instance.ExcelOutputTemplateFilePath);
                Excel.Worksheet workSheet = workBook.Sheets[OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowCommentsHeader ? BusinessClasses.OutputManager.MultiGridTemplateSheetNameWithNotes : BusinessClasses.OutputManager.MultiGridTemplateSheetNameWithoutNotes];

                string[][] gridData = OutputClasses.OutputControls.OutputMultiGridControl.Instance.Grid[slideIndex];
                string[] gridHeaders = OutputClasses.OutputControls.OutputMultiGridControl.Instance.GridHeaders;
                int[] gridHeaderSizes = OutputClasses.OutputControls.OutputMultiGridControl.Instance.GridHeaderSizes;

                int columnsCount = gridHeaders.Length;
                int columnsPerRecord = 3;
                int rowsCount = gridData.Length;
                int excelRowsCount = 2 + rowsCount * columnsPerRecord - (OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowCommentsHeader ? 1 : 2);
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
                            workSheet.Range[ExcelHelper.GetColumnLetterByIndex(i) + excelMainRowIndex.ToString()].Formula = gridData[j][i + (OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowCommentsHeader ? 1 : 0)];
                            if (OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowCommentsHeader)
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
        public void AppendMultiGridExcelBased(bool pasteGridAsImage, PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.MultiGridExcelBasedTemlatesFolderPath))
            {
                string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.MultiGridExcelBasedTemlatesFolderPath, string.Format(BusinessClasses.OutputManager.MultiGridExcelBasedSlideTemplate, OutputClasses.OutputControls.OutputMultiGridControl.Instance.OutputFileIndex, OutputClasses.OutputControls.OutputMultiGridControl.Instance.GridHeaders.Length));
                if (File.Exists(presentationTemplatePath))
                {
                    try
                    {
                        string[][] publicationLogos = OutputClasses.OutputControls.OutputMultiGridControl.Instance.PublicationLogos;
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();

                            ExcelHelper excelHelper = new ExcelHelper();

                            int slidesCount = OutputClasses.OutputControls.OutputMultiGridControl.Instance.Grid.GetLength(0);
                            for (int k = 0; k < slidesCount; k++)
                            {
                                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowAdSpecsOnlyOnLastSlide) || OutputClasses.OutputControls.OutputMultiGridControl.Instance.DoNotShowAdSpecs;
                                foreach (PowerPoint.Slide slide in presentation.Slides)
                                {
                                    foreach (PowerPoint.Shape shape in slide.Shapes)
                                    {
                                        for (int i = 1; i <= shape.Tags.Count; i++)
                                        {
                                            switch (shape.Tags.Name(i))
                                            {
                                                case "DATETAG":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.PresentationDate;
                                                    break;
                                                case "ADVERTISER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.BusinessName;
                                                    break;
                                                case "DECISIONMAKER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.DecisionMaker;
                                                    break;
                                                case "HEADER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.Header;
                                                    break;
                                                case "FLTDT1":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.FlightDates;
                                                    break;
                                                case "SIGLINE":
                                                    if (!OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "SIGAPPROVAL":
                                                    if (!OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "EXCELGRID":
                                                    if (excelHelper.Connect())
                                                    {
                                                        if (excelHelper.GetMultiGrid(k))
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
                                                            if (j < OutputClasses.OutputControls.OutputMultiGridControl.Instance.AdSpecs.Length && !hideAdSpecsOnSlide)
                                                                shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.AdSpecs[j];
                                                            else
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        }
                                                    }

                                                    for (int j = 0; j < 8; j++)
                                                    {
                                                        if (shape.Tags.Name(i).Equals(string.Format("LOGO{0}", j + 1)))
                                                        {
                                                            string fileName = publicationLogos[k][j];
                                                            if (!string.IsNullOrEmpty(fileName))
                                                                slide.Shapes.AddPicture(FileName: fileName, LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
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

        public void PrepareMultiGridExcelBasedEmail(string fileName, bool pasteAsImage)
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
                AppendMultiGridExcelBased(pasteAsImage, presentation);
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

        public void AppendMultiGridGridBased(PowerPoint.Presentation destinationPresentation = null)
        {
            if (Directory.Exists(BusinessClasses.OutputManager.Instance.MultiGridGridBasedTemlatesFolderPath))
            {
                try
                {
                    string[][] publicationLogos = OutputClasses.OutputControls.OutputMultiGridControl.Instance.PublicationLogos;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();

                        int slidesCount = OutputClasses.OutputControls.OutputMultiGridControl.Instance.OutputReplacementsLists.Count;
                        int rowsCount = slidesCount > 0 ? OutputClasses.OutputControls.OutputMultiGridControl.Instance.Grid[0].GetLength(0) : 0;
                        for (int k = 0; k < slidesCount; k++)
                        {
                            string presentationTemplatePath = Path.Combine(BusinessClasses.OutputManager.Instance.MultiGridGridBasedTemlatesFolderPath, string.Format(BusinessClasses.OutputManager.MultiGridGridBasedSlideTemplate, new object[] { OutputClasses.OutputControls.OutputMultiGridControl.Instance.SelectedColumnsCount, (OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowCommentsHeader ? "adnotes" : "no_adnotes"), rowsCount }));
                            int currentSlideRowsCount = OutputClasses.OutputControls.OutputMultiGridControl.Instance.Grid[k].GetLength(0);
                            if (File.Exists(presentationTemplatePath))
                            {
                                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                bool hideAdSpecsOnSlide = ((k + 1) < slidesCount && OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowAdSpecsOnlyOnLastSlide) || OutputClasses.OutputControls.OutputMultiGridControl.Instance.DoNotShowAdSpecs;
                                foreach (PowerPoint.Slide slide in presentation.Slides)
                                {
                                    foreach (PowerPoint.Shape shape in slide.Shapes)
                                    {
                                        for (int i = 1; i <= shape.Tags.Count; i++)
                                        {
                                            switch (shape.Tags.Name(i))
                                            {
                                                case "DATETAG":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.PresentationDate;
                                                    break;
                                                case "ADVERTISER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.BusinessName;
                                                    break;
                                                case "DECISIONMAKER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.DecisionMaker;
                                                    break;
                                                case "HEADER":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.Header;
                                                    break;
                                                case "FLTDT1":
                                                    shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.FlightDates;
                                                    break;
                                                case "SIGLINE":
                                                    if (!OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                case "SIGAPPROVAL":
                                                    if (!OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowSignatureLine || hideAdSpecsOnSlide)
                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                    break;
                                                default:
                                                    for (int j = 0; j < 6; j++)
                                                    {
                                                        if (shape.Tags.Name(i).Equals(string.Format("ADSPEC{0}", j + 1)))
                                                        {
                                                            if (j < OutputClasses.OutputControls.OutputMultiGridControl.Instance.AdSpecs.Length && !hideAdSpecsOnSlide)
                                                                shape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.AdSpecs[j];
                                                            else
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        }
                                                    }

                                                    for (int j = 0; j < publicationLogos[k].Length; j++)
                                                    {
                                                        if (shape.Tags.Name(i).Equals(string.Format("LOGO{0}", j + 1)))
                                                        {
                                                            string fileName = publicationLogos[k][j];
                                                            if (!string.IsNullOrEmpty(fileName))
                                                                slide.Shapes.AddPicture(FileName: fileName, LinkToFile: Microsoft.Office.Core.MsoTriState.msoFalse, SaveWithDocument: Microsoft.Office.Core.MsoTriState.msoCTrue, Left: shape.Left, Top: shape.Top, Width: shape.Width, Height: shape.Height);
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
                                                if (i <= (currentSlideRowsCount * (OutputClasses.OutputControls.OutputMultiGridControl.Instance.ShowCommentsHeader ? 2 : 1)) + 1)
                                                {
                                                    for (int j = 1; j <= tableColumnsCount; j++)
                                                    {
                                                        PowerPoint.Shape tableShape = table.Cell(i, j).Shape;
                                                        if (tableShape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                                                        {
                                                            string cellText = tableShape.TextFrame.TextRange.Text.Trim();
                                                            if (OutputClasses.OutputControls.OutputMultiGridControl.Instance.OutputReplacementsLists[k].Keys.Contains(cellText))
                                                            {
                                                                tableShape.TextFrame.TextRange.Text = OutputClasses.OutputControls.OutputMultiGridControl.Instance.OutputReplacementsLists[k][cellText];
                                                                OutputClasses.OutputControls.OutputMultiGridControl.Instance.OutputReplacementsLists[k].Remove(cellText);
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

        public void PrepareMultiGridGridBasedEmail(string fileName)
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
                AppendMultiGridGridBased(presentation);
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
