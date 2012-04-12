using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMainDashboard
{
    class NewspaperStrategyManager
    {
        private const string SourceFileName = @"Data\!Main_Dashboard\Newspaper Source\Print Strategy.xls";
        private const string DestinationFileName = @"Data\!Main_Dashboard\Newspaper XML\Print Strategy.xml";

        public const string ButtonText = "Print Strategy\nData";

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
            List<CommonClasses.Publication> publications = new List<CommonClasses.Publication>();
            List<CommonClasses.AdSize> adSizes = new List<CommonClasses.AdSize>();
            List<CommonClasses.NameCodePair> notes = new List<CommonClasses.NameCodePair>();
            List<string> clientTypes = new List<string>();
            List<CommonClasses.Section> sections = new List<CommonClasses.Section>();
            List<CommonClasses.MechanicalType> mechanicals = new List<CommonClasses.MechanicalType>();
            List<string> deadlines = new List<string>();
            List<CommonClasses.SlideHeader> statuses = new List<CommonClasses.SlideHeader>();
            List<CommonClasses.ShareUnit> shareUnits = new List<CommonClasses.ShareUnit>();
            string defaultPricingStrategy = string.Empty;
            string defaultColorPricing = string.Empty;
            string selectedNotesBorderValue = string.Empty;

            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
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

                bool loadHeaders = false;
                slideHeaders.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Ad Schedule Slide Headers"))
                            {
                                loadHeaders = true;
                                continue;
                            }

                            if (loadHeaders)
                            {
                                CommonClasses.SlideHeader title = new CommonClasses.SlideHeader();
                                title.Value = row[0].ToString().Trim();
                                if (dataTable.Columns.Count > 1)
                                    if (row[1] != null)
                                        title.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                                if (!string.IsNullOrEmpty(title.Value))
                                    slideHeaders.Add(title);
                            }
                        }

                    slideHeaders.Sort((x, y) =>
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

                //Load Statuses
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [File-Status$]", connection);
                dataTable = new DataTable();

                loadHeaders = false;
                statuses.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*File-Status"))
                            {
                                loadHeaders = true;
                                continue;
                            }

                            if (loadHeaders)
                            {
                                CommonClasses.SlideHeader status = new CommonClasses.SlideHeader();
                                status.Value = row[0].ToString().Trim();
                                if (dataTable.Columns.Count > 1)
                                    if (row[1] != null)
                                        status.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                                if (!string.IsNullOrEmpty(status.Value))
                                    statuses.Add(status);
                            }
                        }

                    statuses.Sort((x, y) =>
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

                //Load Publications
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Publications$]", connection);
                dataTable = new DataTable();

                bool loadPublications = false;
                publications.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 16)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("User-Entry"))
                            {
                                loadPublications = true;
                                continue;
                            }

                            if (loadPublications)
                            {
                                CommonClasses.Publication publication = new CommonClasses.Publication();
                                publication.Name = row[0].ToString().Trim();
                                if (row[1] != null)
                                    publication.SortOrder = row[1].ToString().Trim();
                                if (row[2] != null)
                                    publication.Abbreviation = row[2].ToString().Trim();
                                if (row[3] != null)
                                    publication.BigLogo = row[3].ToString().Trim();
                                if (row[4] != null)
                                    publication.LittleLogo = row[4].ToString().Trim();
                                if (row[5] != null)
                                    publication.TinyLogo = row[5].ToString().Trim();
                                if (row[6] != null)
                                    publication.DailyCirculation = row[6].ToString().Trim();
                                if (row[7] != null)
                                    publication.DailyReadership = row[7].ToString().Trim();
                                if (row[8] != null)
                                    publication.SundayCirculation = row[8].ToString().Trim();
                                if (row[9] != null)
                                    publication.SundayReadership = row[9].ToString().Trim();
                                if (row[10] != null)
                                    publication.AllowSundaySelect = row[10].ToString().Trim().ToLower().Equals("y");
                                if (row[11] != null)
                                    publication.AllowMondaySelect = row[11].ToString().Trim().ToLower().Equals("y");
                                if (row[12] != null)
                                    publication.AllowTuesdaySelect = row[12].ToString().Trim().ToLower().Equals("y");
                                if (row[13] != null)
                                    publication.AllowWednesdaySelect = row[13].ToString().Trim().ToLower().Equals("y");
                                if (row[14] != null)
                                    publication.AllowThursdaySelect = row[14].ToString().Trim().ToLower().Equals("y");
                                if (row[15] != null)
                                    publication.AllowFridaySelect = row[15].ToString().Trim().ToLower().Equals("y");
                                if (row[16] != null)
                                    publication.AllowSaturdaySelect = row[16].ToString().Trim().ToLower().Equals("y");
                                if (!string.IsNullOrEmpty(publication.Name))
                                    publications.Add(publication);
                            }
                        }
                    publications.Sort((x, y) => x.SortOrder.CompareTo(y.SortOrder) == 0 ? InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name) : (string.IsNullOrEmpty(x.SortOrder) ? "z" : x.SortOrder).CompareTo((string.IsNullOrEmpty(y.SortOrder) ? "z" : y.SortOrder)));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load Default Pricing Strategy
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Default Toggle$]", connection);
                dataTable = new DataTable();

                bool loadDefaultPricingStrategy = false;
                bool loadDefaultColorPricing = false;

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 2)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Default Toggle"))
                            {
                                loadDefaultPricingStrategy = true;
                                loadDefaultColorPricing = false;
                                continue;
                            }
                            else if (row[0].ToString().Trim().Equals("*Default Color"))
                            {
                                loadDefaultPricingStrategy = false;
                                loadDefaultColorPricing = true;
                                continue;
                            }

                            if (loadDefaultPricingStrategy)
                            {
                                if (row[0] != null && row[1] != null && row[1].ToString().Trim().ToLower().Equals("d"))
                                    defaultPricingStrategy = row[0].ToString();
                            }
                            else if (loadDefaultColorPricing)
                            {
                                if (row[0] != null && row[1] != null && row[1].ToString().Trim().ToLower().Equals("d"))
                                    defaultColorPricing = row[0].ToString();
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

                //Load AdSizes
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Ad-Sizes$]", connection);
                dataTable = new DataTable();

                bool loadAdSizes = false;
                adSizes.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Ad-Sizes"))
                            {
                                loadAdSizes = true;
                                continue;
                            }

                            if (loadAdSizes)
                            {
                                CommonClasses.AdSize adSize = new CommonClasses.AdSize();
                                adSize.Name = row[0].ToString().Trim();
                                if (!string.IsNullOrEmpty(adSize.Name))
                                    adSizes.Add(adSize);
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

                //Load ShareUnits
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Share-Units$]", connection);
                dataTable = new DataTable();

                bool loadShareUnits = false;
                shareUnits.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 6)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Rate Card"))
                            {
                                loadShareUnits = true;
                                continue;
                            }

                            if (loadShareUnits)
                            {
                                CommonClasses.ShareUnit shareUnit = new CommonClasses.ShareUnit();
                                if (row[0] != null)
                                    shareUnit.RateCard = row[0].ToString().Trim();
                                if (row[1] != null)
                                    shareUnit.PercentOfPage = row[1].ToString().Trim();
                                if (row[2] != null)
                                    shareUnit.Width = row[2].ToString().Trim();
                                if (row[3] != null)
                                    shareUnit.WidthMeasureUnit = row[3].ToString().Trim();
                                if (row[4] != null)
                                    shareUnit.Height = row[4].ToString().Trim();
                                if (row[5] != null)
                                    shareUnit.HeightMeasureUnit = row[5].ToString().Trim();
                                if (!string.IsNullOrEmpty(shareUnit.RateCard))
                                    shareUnits.Add(shareUnit);
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

                //Load Notes
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Notes$]", connection);
                dataTable = new DataTable();

                bool loadNotes = false;
                notes.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Notes Library"))
                            {
                                loadNotes = true;
                                if (dataTable.Columns.Count >= 2)
                                    selectedNotesBorderValue = row[1].ToString().Trim();
                                continue;
                            }

                            if (loadNotes)
                            {
                                CommonClasses.NameCodePair note = new CommonClasses.NameCodePair();
                                note.Name = row[0].ToString().Trim();
                                if (dataTable.Columns.Count >= 2)
                                    note.Code = row[1].ToString().Trim();
                                if (!string.IsNullOrEmpty(note.Name))
                                    notes.Add(note);
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

                //Load Client Types
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Client Type$]", connection);
                dataTable = new DataTable();

                bool loadClientTypes = false;
                clientTypes.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Client Type"))
                            {
                                loadClientTypes = true;
                                continue;
                            }

                            if (loadClientTypes)
                            {
                                string clientType = row[0].ToString().Trim();
                                if (!string.IsNullOrEmpty(clientType))
                                    clientTypes.Add(clientType);
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

                //Load Sections
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sections$]", connection);
                dataTable = new DataTable();

                bool loadSections = false;
                sections.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Sections"))
                            {
                                loadSections = true;
                                continue;
                            }

                            if (loadSections)
                            {
                                CommonClasses.Section section = new CommonClasses.Section();
                                section.Name = row[0].ToString().Trim();
                                if (dataTable.Columns.Count > 1)
                                    if (row[1] != null)
                                        section.Abbreviation = row[1].ToString().Trim();
                                if (!string.IsNullOrEmpty(section.Name))
                                    sections.Add(section);
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

                //Load Mechanicals
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Mechanicals$]", connection);
                dataTable = new DataTable();

                mechanicals.Clear();
                CommonClasses.MechanicalType mechanicalType = null;

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Contains("*") && !row[0].ToString().Trim().Contains("*Comment:"))
                            {
                                if (mechanicalType != null)
                                    mechanicals.Add(mechanicalType);
                                mechanicalType = new CommonClasses.MechanicalType();
                                mechanicalType.Name = row[0].ToString().Replace("*", "");
                                continue;
                            }

                            if (mechanicalType != null)
                            {
                                CommonClasses.MechanicalItem mechanicalItem = new CommonClasses.MechanicalItem();
                                mechanicalItem.Name = row[0].ToString().Trim();
                                if (dataTable.Columns.Count > 1)
                                    if (row[1] != null)
                                        mechanicalItem.Value = row[1].ToString().Trim();
                                if (!string.IsNullOrEmpty(mechanicalItem.Name))
                                    mechanicalType.Items.Add(mechanicalItem);
                            }
                        }
                    if (mechanicalType != null)
                        mechanicals.Add(mechanicalType);
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load Deadlines
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Deadlines$]", connection);
                dataTable = new DataTable();

                bool loadDeadlines = false;
                deadlines.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Deadlines"))
                            {
                                loadDeadlines = true;
                                continue;
                            }

                            if (loadDeadlines)
                            {
                                string deadline = row[0].ToString().Trim();
                                if (!string.IsNullOrEmpty(deadline))
                                    deadlines.Add(deadline);
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

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<PrintStrategy>");
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
                foreach (var publication in publications)
                {
                    xml.Append(@"<Publication ");
                    xml.Append("Name = \"" + publication.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Abbreviation = \"" + publication.Abbreviation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("BigLogo = \"" + publication.BigLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("LittleLogo = \"" + publication.LittleLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("TinyLogo = \"" + publication.TinyLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("DailyCirculation = \"" + publication.DailyCirculation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("DailyReadership = \"" + publication.DailyReadership.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("SundayCirculation = \"" + publication.SundayCirculation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("SundayReadership = \"" + publication.SundayReadership.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("AllowSundaySelect = \"" + publication.AllowSundaySelect.ToString() + "\" ");
                    xml.Append("AllowMondaySelect = \"" + publication.AllowMondaySelect.ToString() + "\" ");
                    xml.Append("AllowTuesdaySelect = \"" + publication.AllowTuesdaySelect.ToString() + "\" ");
                    xml.Append("AllowWednesdaySelect = \"" + publication.AllowWednesdaySelect.ToString() + "\" ");
                    xml.Append("AllowThursdaySelect = \"" + publication.AllowThursdaySelect.ToString() + "\" ");
                    xml.Append("AllowFridaySelect = \"" + publication.AllowFridaySelect.ToString() + "\" ");
                    xml.Append("AllowSaturdaySelect = \"" + publication.AllowSaturdaySelect.ToString() + "\" ");
                    xml.AppendLine(@"/>");
                }

                xml.AppendLine(@"<DefaultPricingStrategy>" + defaultPricingStrategy.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DefaultPricingStrategy>");
                xml.AppendLine(@"<DefaultColorPricing>" + defaultColorPricing.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DefaultColorPricing>");

                foreach (var adSize in adSizes)
                {
                    xml.Append(@"<AdSize ");
                    xml.Append("Name = \"" + adSize.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var shareUnit in shareUnits)
                {
                    xml.Append(@"<ShareUnit ");
                    xml.Append("RateCard = \"" + shareUnit.RateCard.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("PercentOfPage = \"" + shareUnit.PercentOfPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Width = \"" + shareUnit.Width.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("WidthMeasureUnit = \"" + shareUnit.WidthMeasureUnit.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Height = \"" + shareUnit.Height.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("HeightMeasureUnit = \"" + shareUnit.HeightMeasureUnit.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var note in notes)
                {
                    xml.Append(@"<Note ");
                    xml.Append("Value = \"" + note.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Code = \"" + note.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"<SelectedNotesBorderValue>" + selectedNotesBorderValue.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedNotesBorderValue>");
                
                foreach (var clientType in clientTypes)
                {
                    xml.Append(@"<ClientType ");
                    xml.Append("Value = \"" + clientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var section in sections)
                {
                    xml.Append(@"<Section ");
                    xml.Append("Name = \"" + section.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Abbreviation = \"" + section.Abbreviation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (CommonClasses.MechanicalType mechanical in mechanicals)
                {
                    xml.Append(@"<Mechanicals ");
                    xml.Append("Name = \"" + mechanical.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@">");
                    foreach (var mechanicalItem in mechanical.Items)
                    {
                        xml.Append(@"<Mechanical ");
                        xml.Append("Name = \"" + mechanicalItem.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.Append("Value = \"" + mechanicalItem.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.AppendLine(@"/>");
                    }
                    xml.AppendLine(@"</Mechanicals>");
                }
                foreach (var deadline in deadlines)
                {
                    xml.Append(@"<Deadline ");
                    xml.Append("Value = \"" + deadline.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</PrintStrategy>");

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
}
