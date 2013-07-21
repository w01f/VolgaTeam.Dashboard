using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMainDashboard
{
    class MobileStrategyManager
    {
        private const string SourceFileName = @"Data\!Main_Dashboard\Mobile Source\Mobile Strategy.xls";
        private const string DestinationFileName = @"Data\!Main_Dashboard\Mobile XML\Mobile Strategy.xml";
        private const string ImageSourceFolder = @"Data\!Main_Dashboard\Mobile Source\Category Images";

        public const string ButtonText = "Mobile Strategy\nData";

        private static List<CommonClasses.Category> _categories = new List<CommonClasses.Category>();

        private static void GetCategories(OleDbConnection connection)
        {
            DataTable dataTable;
            try
            {
                _categories.Clear();
                dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in dataTable.Rows)
                {
                    CommonClasses.Category category = new CommonClasses.Category();
                    category.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "");

                    if (!category.Name.Trim().Equals("Headers") && !category.Name.Trim().Equals("Sites") && !category.Name.Trim().Equals("Strengths") && !category.Name.Trim().Equals("Categories") && !category.Name.Trim().Equals("Slides") && !category.Name.Trim().Equals("File-Status") && !category.Name.Trim().Equals("WebFormula"))
                        _categories.Add(category);
                }
            }
            catch
            {
            }
        }

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, DestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, SourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SlideHeader> slideHeaders = new List<CommonClasses.SlideHeader>();
            List<CommonClasses.SlideHeader> sites = new List<CommonClasses.SlideHeader>();
            List<CommonClasses.SlideHeader> strengths = new List<CommonClasses.SlideHeader>();
            List<CommonClasses.Product> products = new List<CommonClasses.Product>();
            List<CommonClasses.OnlineSlideSource> slideSources = new List<CommonClasses.OnlineSlideSource>();
            List<CommonClasses.SlideHeader> statuses = new List<CommonClasses.SlideHeader>();
            string defaultFormula = string.Empty;

            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
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
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers$]", connection);
                dataTable = new DataTable();
                slideHeaders.Clear();
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
                                slideHeaders.Add(title);
                        }

                    slideHeaders.Sort((x, y) =>
                    {
                        int result = y.IsDefault.CompareTo(x.IsDefault);
                        if (result == 0)
                            result = 1;
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

                //Load Statuses
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [File-Status$]", connection);
                dataTable = new DataTable();
                statuses.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SlideHeader status = new CommonClasses.SlideHeader();
                            status.Value = row[0].ToString().Trim();
                            if (dataTable.Columns.Count > 1)
                                if (row[1] != null)
                                    status.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                            if (!string.IsNullOrEmpty(status.Value))
                                statuses.Add(status);
                        }

                    statuses.Sort((x, y) =>
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

                //Load Sites
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sites$]", connection);
                dataTable = new DataTable();
                sites.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SlideHeader site = new CommonClasses.SlideHeader();
                            site.Value = row[0].ToString().Trim();
                            if (dataTable.Columns.Count > 1)
                                if (row[1] != null)
                                    site.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                            if (!string.IsNullOrEmpty(site.Value))
                                sites.Add(site);
                        }

                    sites.Sort((x, y) =>
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

                //Load Strengths
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Strengths$]", connection);
                dataTable = new DataTable();
                strengths.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SlideHeader strength = new CommonClasses.SlideHeader();
                            strength.Value = row[0].ToString().Trim();
                            if (dataTable.Columns.Count > 1)
                                if (row[1] != null)
                                    strength.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                            if (!string.IsNullOrEmpty(strength.Value))
                                strengths.Add(strength);
                        }

                    strengths.Sort((x, y) =>
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

                //Load Default Formula
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [WebFormula$]", connection);
                dataTable = new DataTable();

                bool loadDefaultFormula = false;

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 2)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Web Formula Default"))
                            {
                                loadDefaultFormula = true;
                                continue;
                            }

                            if (loadDefaultFormula)
                            {
                                if (row[0] != null && row[1] != null && row[1].ToString().Trim().ToLower().Equals("d"))
                                    defaultFormula = row[0].ToString();
                            }
                        }
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load Categories
                GetCategories(connection);
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Categories$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    int i = 0;
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 4)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string name = row[0].ToString().Trim();
                            CommonClasses.Category category = _categories.Where(x => x.Name.Equals(name)).FirstOrDefault();
                            if (category != null)
                            {
                                category.Order = i;
                                category.TooltipTitle = row[1].ToString().Trim();
                                category.TooltipValue = row[2].ToString().Trim();
                                string filePath = Path.Combine(Application.StartupPath, ImageSourceFolder, row[3].ToString().Trim());
                                if (File.Exists(filePath))
                                    category.Logo = new Bitmap(filePath);
                            }
                            i++;
                        }
                    _categories.Sort((x, y) => x.Order.CompareTo(y.Order));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load Products
                products.Clear();
                foreach (var category in _categories)
                {
                    dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", category.Name), connection);
                    dataTable = new DataTable();
                    try
                    {
                        dataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 7)
                            foreach (DataRow row in dataTable.Rows)
                            {
                                CommonClasses.Product product = new CommonClasses.Product();
                                product.SubCategory = row[0].ToString().Trim();
                                product.Name = row[1].ToString().Trim();
                                product.RateType = row[2].ToString().Trim();
                                product.Rate = row[3].ToString().Trim();
                                product.Width = row[4].ToString().Trim();
                                product.Height = row[5].ToString().Trim();
                                product.Overview = row[6].ToString().Trim();
                                product.Category = category;
                                if (!string.IsNullOrEmpty(product.Name))
                                    products.Add(product);
                            }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        dataAdapter.Dispose();
                        dataTable.Dispose();
                    }
                }
                connection.Close();
            }

            connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
            connection = new OleDbConnection(connnectionString);
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

                //Load Slides
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Slides$]", connection);
                dataTable = new DataTable();
                slideSources.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        int columnsCount = dataTable.Columns.Count;
                        if (columnsCount > 1)
                        {
                            for (int i = 1; i < columnsCount; i++)
                            {
                                CommonClasses.OnlineSlideSource slideSource = new CommonClasses.OnlineSlideSource();
                                int rowsCount = dataTable.Rows.Count;
                                for (int j = 0; j < rowsCount; j++)
                                {
                                    object name = dataTable.Rows[j][0];
                                    object value = dataTable.Rows[j][i];
                                    if (value != null && name != null)
                                    {
                                        if (j == 0)
                                            slideSource.TemplateName = value.ToString();
                                        else
                                        {
                                            bool flag = value.ToString().ToUpper().Trim().Equals("T") ? true : false;
                                            switch (name.ToString().Trim())
                                            {
                                                case "Website Checkbox":
                                                    slideSource.ShowWebsite = flag;
                                                    break;
                                                case "Presentation Date Toggle":
                                                    slideSource.ShowPresentationDate = flag;
                                                    break;
                                                case "Business Name Toggle":
                                                    slideSource.ShowBusinessName = flag;
                                                    break;
                                                case "Decision Maker Toggle":
                                                    slideSource.ShowDecisionMaker = flag;
                                                    break;
                                                case "Flight Dates Toggle":
                                                    slideSource.ShowFlightDates = flag;
                                                    break;
                                                case "Total Months Checkbox":
                                                    slideSource.ShowDuration = flag;
                                                    break;
                                                case "Active Days Toggle":
                                                    slideSource.ShowActiveDays = flag;
                                                    break;
                                                case "Total Ads Toggle":
                                                    slideSource.ShowTotalAds = flag;
                                                    break;
                                                case "Product Toggle":
                                                    slideSource.ShowProduct = flag;
                                                    break;
                                                case "Dimensions Toggle":
                                                    slideSource.ShowDimensions = flag;
                                                    break;
                                                case "Description Toggle":
                                                    slideSource.ShowDescription = flag;
                                                    break;
                                                case "Month (000) Toggle":
                                                    slideSource.ShowMonthlyImpressions = flag;
                                                    break;
                                                case "Total (000) Toggle":
                                                    slideSource.ShowTotalImpressions = flag;
                                                    break;
                                                case "CPM Toggle + Month (000) Toggle":
                                                    slideSource.ShowMonthlyCPM = flag;
                                                    break;
                                                case "CPM Toggle + Total (000) Toggle":
                                                    slideSource.ShowTotalCPM = flag;
                                                    break;
                                                case "Ad Rate Toggle":
                                                    slideSource.ShowAdRate = flag;
                                                    break;
                                                case "Monthly $ Toggle":
                                                    slideSource.ShowMonthlyInvestment = flag;
                                                    break;
                                                case "Total $ Toggle":
                                                    slideSource.ShowTotalInvestment = flag;
                                                    break;
                                                case "Comments Toggle":
                                                    slideSource.ShowComments = flag;
                                                    break;
                                                case "Signature Line Checkbox":
                                                    slideSource.ShowSignature = flag;
                                                    break;
                                                case "Screenshot Viewer Checkbox":
                                                    slideSource.ShowScreenshot = flag;
                                                    break;
                                                case "Show Image Icons":
                                                    slideSource.ShowImages = flag;
                                                    break;
                                            }
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(slideSource.TemplateName))
                                    slideSources.Add(slideSource);
                            }
                        }
                    }
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

            }
            //Save XML
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<MobileStrategy>");
            foreach (var header in slideHeaders)
            {
                xml.Append(@"<Header ");
                xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.AppendLine(@"/>");
            }
            foreach (var status in statuses)
            {
                xml.Append(@"<Status ");
                xml.Append("Value = \"" + status.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.AppendLine(@"/>");
            }
            foreach (var site in sites)
            {
                xml.Append(@"<Site ");
                xml.Append("Value = \"" + site.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.AppendLine(@"/>");
            }
            foreach (var strength in strengths)
            {
                xml.Append(@"<Strength ");
                xml.Append("Value = \"" + strength.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.AppendLine(@"/>");
            }

            xml.AppendLine("<DefaultFormula>" + defaultFormula.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</DefaultFormula>");

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            foreach (var category in _categories)
            {
                xml.Append(@"<Category ");
                xml.Append("Name = \"" + category.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("TooltipTitle = \"" + category.TooltipTitle.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("TooltipValue = \"" + category.TooltipValue.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("Logo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(category.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.AppendLine(@"/>");
            }
            foreach (var product in products)
            {
                xml.Append(@"<Product ");
                xml.Append("Category = \"" + product.Category.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("SubCategory = \"" + product.SubCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("Name = \"" + product.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("RateType = \"" + product.RateType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("Rate = \"" + product.Rate.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("Width = \"" + product.Width.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("Height = \"" + product.Height.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("Overview = \"" + product.Overview.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.AppendLine(@"/>");
            }
            foreach (var slideSource in slideSources)
            {
                xml.Append(@"<SlideSource ");
                xml.Append("Name = \"" + slideSource.TemplateName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                xml.Append("ShowActiveDays = \"" + slideSource.ShowActiveDays.ToString() + "\" ");
                xml.Append("ShowAdRate = \"" + slideSource.ShowAdRate.ToString() + "\" ");
                xml.Append("ShowBusinessName = \"" + slideSource.ShowBusinessName.ToString() + "\" ");
                xml.Append("ShowComments = \"" + slideSource.ShowComments.ToString() + "\" ");
                xml.Append("ShowDecisionMaker = \"" + slideSource.ShowDecisionMaker.ToString() + "\" ");
                xml.Append("ShowDescription = \"" + slideSource.ShowDescription.ToString() + "\" ");
                xml.Append("ShowDimensions = \"" + slideSource.ShowDimensions.ToString() + "\" ");
                xml.Append("ShowDuration = \"" + slideSource.ShowDuration.ToString() + "\" ");
                xml.Append("ShowFlightDates = \"" + slideSource.ShowFlightDates.ToString() + "\" ");
                xml.Append("ShowImages = \"" + slideSource.ShowImages.ToString() + "\" ");
                xml.Append("ShowMonthlyCPM = \"" + slideSource.ShowMonthlyCPM.ToString() + "\" ");
                xml.Append("ShowMonthlyImpressions = \"" + slideSource.ShowMonthlyImpressions.ToString() + "\" ");
                xml.Append("ShowMonthlyInvestment = \"" + slideSource.ShowMonthlyInvestment.ToString() + "\" ");
                xml.Append("ShowPresentationDate = \"" + slideSource.ShowPresentationDate.ToString() + "\" ");
                xml.Append("ShowProduct = \"" + slideSource.ShowProduct.ToString() + "\" ");
                xml.Append("ShowScreenshot = \"" + slideSource.ShowScreenshot.ToString() + "\" ");
                xml.Append("ShowSignature = \"" + slideSource.ShowSignature.ToString() + "\" ");
                xml.Append("ShowTotalAds = \"" + slideSource.ShowTotalAds.ToString() + "\" ");
                xml.Append("ShowTotalCPM = \"" + slideSource.ShowTotalCPM.ToString() + "\" ");
                xml.Append("ShowTotalImpressions = \"" + slideSource.ShowTotalImpressions.ToString() + "\" ");
                xml.Append("ShowTotalInvestment = \"" + slideSource.ShowTotalInvestment.ToString() + "\" ");
                xml.Append("ShowWebsite = \"" + slideSource.ShowWebsite.ToString() + "\" ");
                xml.AppendLine(@"/>");
            }

            xml.AppendLine(@"</MobileStrategy>");

            string xmlPath = Path.Combine(Application.StartupPath, DestinationFileName);
            using (StreamWriter sw = new StreamWriter(xmlPath, false))
            {
                sw.Write(xml.ToString());
                sw.Flush();
            }

            AppManager.Instance.ShowInformation("Data was updated.");
        }
    }
}
