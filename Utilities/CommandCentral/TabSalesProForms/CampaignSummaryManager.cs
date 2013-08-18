﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabSalesProForms
{
    class CampaignSummaryManager
    {
        private const string CampaignSummarySourceFileName = @"Data\!App_Data\app_sales_PRO_Source\Campaign Summary.xls";
        private const string CampaignSummaryDestinationFileName = @"Data\!App_Data\app_sales_PRO_XML\Campaign Summary.xml";

        public const string ButtonText = "Campaign\nSummary";

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CampaignSummaryDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CampaignSummarySourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SlideHeader> titles = new List<CommonClasses.SlideHeader>();


            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, CampaignSummarySourceFileName));
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
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers$]", connection);
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
                connection.Close();

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<CampaignSummary>");
                foreach (CommonClasses.SlideHeader title in titles)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + title.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + title.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</CampaignSummary>");

                string xmlPath = Path.Combine(Application.StartupPath, CampaignSummaryDestinationFileName);
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