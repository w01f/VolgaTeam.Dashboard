using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommandCentral.Entities.Common;

namespace CommandCentral.TabMarketProForms
{
    public enum MediaDataType
    {
        Cable = 0,
        DirectMail,
        Internet,
        Mobile,
        Outdoor,
        Print,
        Radio,
        TV,
        YellowPages,
        MediaStrategy
    }

    class MarketProManager
    {
        private const string CableSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Cable.xls";
        private const string CableDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Cable.xml";
        private const string DirectMailSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Direct Mail.xls";
        private const string DirectMailDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Direct Mail.xml";
        private const string InternetSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Internet.xls";
        private const string InternetDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Internet.xml";
        private const string MobileSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Mobile.xls";
        private const string MobileDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Mobile.xml";
        private const string OutdoorSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Outdoor.xls";
        private const string OutdoorDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Outdoor.xml";
        private const string PrintSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Print.xls";
        private const string PrintDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Print.xml";
        private const string RadioSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Radio.xls";
        private const string RadioDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Radio.xml";
        private const string TVSourceFileName = @"Data\!App_Data\app_market_PRO_Source\TV.xls";
        private const string TVDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\TV.xml";
        private const string YellowPagesSourceFileName = @"Data\!App_Data\app_market_PRO_Source\Yellow Pages.xls";
        private const string YellowPagesDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Yellow Pages.xml";
        private const string MediaStrategySourceFileName = @"Data\!App_Data\app_market_PRO_Source\Media Strategy.xls";
        private const string MediaStrategyDestinationFileName = @"Data\!App_Data\app_market_PRO_XML\Media Strategy.xml";

        private const string CableSlideHeader = @"*Cable Slide Headers";
        private const string CableStrengths = @"*CABLE'S STRENGTHS";
        private const string CableChallenges = @"*CABLE'S CHALLENGES";
        private const string DirectMailSlideHeader = @"*Direct Mail Slide Headers";
        private const string DirectMailStrengths = @"*DIRECT MAIL'S STRENGTHS";
        private const string DirectMailChallenges = @"*DIRECT MAIL'S CHALLENGES";
        private const string InternetSlideHeader = @"*Internet Slide Headers";
        private const string InternetStrengths = @"*INTERNET'S STRENGTHS";
        private const string InternetChallenges = @"*INTERNET'S CHALLENGES";
        private const string MobileSlideHeader = @"*Mobile Slide Headers";
        private const string MobileStrengths = @"*MOBILE'S STRENGTHS";
        private const string MobileChallenges = @"*MOBILE'S CHALLENGES";
        private const string OutdoorSlideHeader = @"*Outdoor Slide Headers";
        private const string OutdoorStrengths = @"*OUTDOOR'S STRENGTHS";
        private const string OutdoorChallenges = @"*OUTDOOR'S CHALLENGES";
        private const string PrintSlideHeader = @"*Print Slide Headers";
        private const string PrintStrengths = @"*NEWSPAPER'S STRENGTHS";
        private const string PrintChallenges = @"*NEWSPAPER'S CHALLENGES";
        private const string RadioSlideHeader = @"*Radio Slide Headers";
        private const string RadioStrengths = @"*RADIO'S STRENGTHS";
        private const string RadioChallenges = @"*RADIO'S CHALLENGES";
        private const string TVSlideHeader = @"*TV Slide Headers";
        private const string TVStrengths = @"*TV'S STRENGTHS";
        private const string TVChallenges = @"*TV'S CHALLENGES";
        private const string YellowPagesSlideHeader = @"*Yellow Pages Slide Headers";
        private const string YellowPagesStrengths = @"*YELLOW PAGES STRENGTHS";
        private const string YellowPagesChallenges = @"*YELLOW PAGES CHALLENGES";
        private const string MediaStrategySlideHeader = @"*Media Strategy Headers";

        private const string CableSheetName = @"Cable";
        private const string DirectMailSheetName = @"Dmail";
        private const string InternetSheetName = @"Internet";
        private const string MobileSheetName = @"Mobile";
        private const string OutdoorSheetName = @"Outdoor";
        private const string PrintSheetName = @"Print";
        private const string RadioSheetName = @"Radio";
        private const string TVSheetName = @"TV";
        private const string YellowPagesSheetName = @"Ypages";
        private const string MediaStrategySheetName = @"Headers";


        private static string _sourceFileName;
        private static string _destinationFileName;
        private static string _sheetName;
        private static string _slideHeader;
        private static string _strengths;
        private static string _challenges;        
        
        public static string ButtonText = string.Empty;

        public static void AssignMediaDataType(MediaDataType type)
        {
            switch (type)
            {
                case MediaDataType.Cable:
                    _sourceFileName = CableSourceFileName;
                    _destinationFileName = CableDestinationFileName;
                    _sheetName = CableSheetName;
                    _slideHeader = CableSlideHeader;
                    _strengths = CableStrengths;
                    _challenges = CableChallenges;
                    ButtonText = "Cable\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.DirectMail:
                    _sourceFileName = DirectMailSourceFileName;
                    _destinationFileName = DirectMailDestinationFileName;
                    _sheetName = DirectMailSheetName;
                    _slideHeader = DirectMailSlideHeader;
                    _strengths = DirectMailStrengths;
                    _challenges = DirectMailChallenges;
                    ButtonText = "Direct Mail\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.Internet:
                    _sourceFileName = InternetSourceFileName;
                    _destinationFileName = InternetDestinationFileName;
                    _sheetName = InternetSheetName;
                    _slideHeader = InternetSlideHeader;
                    _strengths = InternetStrengths;
                    _challenges = InternetChallenges;
                    ButtonText = "Internet\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.Mobile:
                    _sourceFileName = MobileSourceFileName;
                    _destinationFileName = MobileDestinationFileName;
                    _sheetName = MobileSheetName;
                    _slideHeader = MobileSlideHeader;
                    _strengths = MobileStrengths;
                    _challenges = MobileChallenges;
                    ButtonText = "Mobile\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.Outdoor:
                    _sourceFileName = OutdoorSourceFileName;
                    _destinationFileName = OutdoorDestinationFileName;
                    _sheetName = OutdoorSheetName;
                    _slideHeader = OutdoorSlideHeader;
                    _strengths = OutdoorStrengths;
                    _challenges = OutdoorChallenges;
                    ButtonText = "Outdoor\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.Print:
                    _sourceFileName = PrintSourceFileName;
                    _destinationFileName = PrintDestinationFileName;
                    _sheetName = PrintSheetName;
                    _slideHeader = PrintSlideHeader;
                    _strengths = PrintStrengths;
                    _challenges = PrintChallenges;
                    ButtonText = "Print\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.Radio:
                    _sourceFileName = RadioSourceFileName;
                    _destinationFileName = RadioDestinationFileName;
                    _sheetName = RadioSheetName;
                    _slideHeader = RadioSlideHeader;
                    _strengths = RadioStrengths;
                    _challenges = RadioChallenges;
                    ButtonText = "Radio\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.TV:
                    _sourceFileName = TVSourceFileName;
                    _destinationFileName = TVDestinationFileName;
                    _sheetName = TVSheetName;
                    _slideHeader = TVSlideHeader;
                    _strengths = TVStrengths;
                    _challenges = TVChallenges;
                    ButtonText = "TV\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.YellowPages:
                    _sourceFileName = YellowPagesSourceFileName;
                    _destinationFileName = YellowPagesDestinationFileName;
                    _sheetName = YellowPagesSheetName;
                    _slideHeader = YellowPagesSlideHeader;
                    _strengths = YellowPagesStrengths;
                    _challenges = YellowPagesChallenges;
                    ButtonText = "Yellow Pages\nStrengths &&\nChallenges";
                    break;
                case MediaDataType.MediaStrategy:
                    _sourceFileName = MediaStrategySourceFileName;
                    _destinationFileName = MediaStrategyDestinationFileName;
                    _sheetName = MediaStrategySheetName;
                    _slideHeader = MediaStrategySlideHeader;
                    _strengths = "*";
                    _challenges = "*";
                    ButtonText = "Media\nStrategy";
                    break;
            }
        }

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, _destinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, _sourceFileName));
        }

        public static void UpdateData()
        {
            List<SlideHeader> headers = new List<SlideHeader>();
            List<string> strengths = new List<string>();
            List<string> challenges = new List<string>();

            bool readHeaders = false;
            bool readStrengths = false;
            bool readChallenges = false;


            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";", Path.Combine(Application.StartupPath, _sourceFileName));
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
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM [" + _sheetName + "$]", connection); ;
                DataTable dataTable = new DataTable(); ;

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string rowValue1 = row[0].ToString().Trim();
                            string rowValue2 = string.Empty;
                            if (dataTable.Columns.Count > 1)
                            {
                                if (row[1] != null)
                                    rowValue2 = row[1].ToString();
                            }
                            if (rowValue1.Equals(_slideHeader))
                            {
                                readHeaders = true;
                                readStrengths = false;
                                readChallenges = false;
                                continue;
                            }
                            else if (rowValue1.Equals(_strengths))
                            {
                                readHeaders = false;
                                readStrengths = true;
                                readChallenges = false;
                                continue;
                            }
                            else if (rowValue1.Equals(_challenges))
                            {
                                readHeaders = false;
                                readStrengths = false;
                                readChallenges = true;
                                continue;
                            }
                            if (!string.IsNullOrEmpty(rowValue1))
                            {
                                if (readHeaders)
                                {
                                    SlideHeader header = new SlideHeader();
                                    header.Value = rowValue1;
                                    header.IsDefault = rowValue2.ToString().Trim().ToLower().Equals("d");
                                    headers.Add(header);
                                }
                                else if (readStrengths)
                                    strengths.Add(rowValue1);
                                else if (readChallenges)
                                    challenges.Add(rowValue1);
                            }
                        }
                    headers.Sort((x, y) =>
                    {
                        int result = y.IsDefault.CompareTo(x.IsDefault);
                        if (result == 0)
                            result = InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
                        return result;
                    });
                    strengths.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                    challenges.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
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
                xml.AppendLine("<" + _sheetName + ">");
                foreach (SlideHeader header in headers)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string strength in strengths)
                {
                    xml.Append(@"<Strength ");
                    xml.Append("Value = \"" + strength.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string challenge in challenges)
                {
                    xml.Append(@"<Challenge ");
                    xml.Append("Value = \"" + challenge.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</" + _sheetName + ">");

                string xmlPath = Path.Combine(Application.StartupPath, _destinationFileName);
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
