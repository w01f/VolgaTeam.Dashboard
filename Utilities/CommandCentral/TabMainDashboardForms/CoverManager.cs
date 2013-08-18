﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMainDashboard
{
    class CoverManager
    {
        private const string CoverSourceFileName = @"Data\!Main_Dashboard\Basic Slides Source\Add Cover.xls";
        private const string CoverDestinationFileName = @"Data\!Main_Dashboard\Basic Slides XML\Add Cover.xml";

        public const string ButtonText = "Cover";

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CoverDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CoverSourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SlideHeader> titles = new List<CommonClasses.SlideHeader>();
            List<CommonClasses.Quote> quotes = new List<CommonClasses.Quote>();


            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, CoverSourceFileName));
            OleDbConnection connection = new OleDbConnection(connnectionString);
            try
            {
                connection.Open();
            }
            catch
            {
                AppManager.Instance.ShowWarning("Couldn't open source file");
                return;
            }

            if (connection.State == ConnectionState.Open)
            {
                OleDbDataAdapter dataAdapter;
                DataTable dataTable;

                //Load Titles
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Titles$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SlideHeader title = new CommonClasses.SlideHeader();
                            title.Value = row[0].ToString().Trim();
                            if (dataTable.Columns.Count > 1)
                                if (row[1] != null)
                                    title.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                            if (!string.IsNullOrEmpty(title.Value))
                                titles.Add(title);
                        }
                    titles.Sort((x, y) =>
                    {
                        int result = y.IsDefault.CompareTo(x.IsDefault);
                        if (result == 0)
                            result = InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
                        return result;
                    });
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load Quotes
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Quotes$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string value = row[0].ToString().Trim();
                            string author = row[1].ToString().Trim();
                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(author))
                                quotes.Add(new CommonClasses.Quote(value, author));
                        }
                    quotes.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Value, y.Value));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }
                connection.Close();

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<CoverSlide>");
                foreach (CommonClasses.SlideHeader title in titles)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + title.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + title.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (CommonClasses.Quote quote in quotes)
                {
                    xml.Append(@"<Quote ");
                    xml.Append("Value = \"" + quote.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Author = \"" + quote.Author.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</CoverSlide>");

                string xmlPath = Path.Combine(Application.StartupPath, CoverDestinationFileName);
                using (StreamWriter sw = new StreamWriter(xmlPath, false))
                {
                    sw.Write(xml.ToString());
                    sw.Flush();
                }

                AppManager.Instance.ShowInformation("Data was updated.");
            } 
        }
    }
}