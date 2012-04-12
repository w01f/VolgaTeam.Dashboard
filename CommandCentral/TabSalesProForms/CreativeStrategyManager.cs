using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabSalesProForms
{
    class CreativeStrategyManager
    {
        private const string CreativeStrategySourceFileName = @"Data\!App_Data\app_sales_PRO_Source\Creative Strategy.xls";
        private const string CreativeStrategyDestinationFileName = @"Data\!App_Data\app_sales_PRO_XML\Creative Strategy.xml";

        public const string ButtonText = "Creative\nStrategy";

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CreativeStrategyDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CreativeStrategySourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SlideHeader> headers = new List<CommonClasses.SlideHeader>();
            List<string> imageBrandings = new List<string>();
            List<string> callToActionPositionings = new List<string>();
            List<string> marketingWarfareStrategies = new List<string>();



            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, CreativeStrategySourceFileName));
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

                //Load Headers
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Slide Headers$]", connection);
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
                                headers.Add(title);
                        }
                    headers.Sort((x, y) =>
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

                //Load Image and Branding
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Image and Branding$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string imageAndBranding = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(imageAndBranding))
                                imageBrandings.Add(imageAndBranding);
                        }
                    imageBrandings.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Call to Action-Positioning
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Call to Action-Positioning$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string callToActionPositioning = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(callToActionPositioning))
                                callToActionPositionings.Add(callToActionPositioning);
                        }
                    callToActionPositionings.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Marketing Warfare Strategies
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Marketing Warfare Strategies$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string marketingWarfareStrategy = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(marketingWarfareStrategy))
                                marketingWarfareStrategies.Add(marketingWarfareStrategy);
                        }
                    marketingWarfareStrategies.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
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
                xml.AppendLine("<CreativeStrategy>");
                foreach (CommonClasses.SlideHeader header in headers)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string imageBranding in imageBrandings)
                {
                    xml.Append(@"<ImageAndBranding ");
                    xml.Append("Value = \"" + imageBranding.Replace(@"&", "&#38;").Replace("\"", "&quot;").Replace("\"", "&quot") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string callToActionPositioning in callToActionPositionings)
                {
                    xml.Append(@"<CallToActionPositioning ");
                    xml.Append("Value = \"" + callToActionPositioning.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string marketingWarfareStrategy in marketingWarfareStrategies)
                {
                    xml.Append(@"<MarketingWarfareStrategies ");
                    xml.Append("Value = \"" + marketingWarfareStrategy.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</CreativeStrategy>");

                string xmlPath = Path.Combine(Application.StartupPath, CreativeStrategyDestinationFileName);
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
