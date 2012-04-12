using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdScheduleBuilder.BusinessClasses
{
    class RateCardManager
    {
        private static string RateCardPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\RateCard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        private static RateCardManager _instance = new RateCardManager();

        public List<RateCardFolder> RateCardFolders { get; private set; }

        public static RateCardManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private RateCardManager()
        {
            this.RateCardFolders = new List<RateCardFolder>();
            LoadRateCards();
        }

        private void LoadRateCards()
        {
            this.RateCardFolders.Clear();
            if (Directory.Exists(RateCardPath))
            {
                List<string> rateCatdFolders = new List<string>();
                rateCatdFolders.AddRange(Directory.GetDirectories(RateCardPath));
                rateCatdFolders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                foreach (string rateCardFolderPath in rateCatdFolders)
                {
                    RateCardFolder rateCardFolder = new RateCardFolder(rateCardFolderPath);
                    this.RateCardFolders.Add(rateCardFolder);
                }
            }
        }
    }

    class RateCardFolder
    {
        public DirectoryInfo Folder { get; private set; }
        public CustomControls.RateFolderControl RateCardContainer { get; private set; }
        public List<IRateCardViewer> RateCards { get; private set; }

        public string FolderName
        {
            get
            {
                return Regex.Replace(this.Folder.Name, @"\d+_", string.Empty);
            }
        }

        public RateCardFolder(string parentFolderPath)
        {
            this.Folder = new DirectoryInfo(parentFolderPath);
            this.RateCards = new List<IRateCardViewer>();
            this.RateCardContainer = new CustomControls.RateFolderControl();
            this.RateCardContainer.xtraTabControlRateCards.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(SelectRateCard);
        }

        private void SelectRateCard(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            IRateCardViewer rateCard = e.Page as IRateCardViewer;
            if (rateCard != null)
            {
                rateCard.LoadViewer();
            }
        }

        public void LoadRateCards()
        {
            if (this.RateCards.Count == 0)
            {
                foreach (FileInfo rateCardFile in this.Folder.GetFiles())
                {
                    IRateCardViewer rateCard = null;
                    switch (rateCardFile.Extension.ToLower())
                    {
                        case ".pdf":
                            try
                            {
                                rateCard = new CustomControls.RateCardViewers.PDFViewer(rateCardFile);
                            }
                            catch
                            {
                                rateCard = new CustomControls.RateCardViewers.DefaultViewer(rateCardFile);
                            }
                            break;
                        case ".xls":
                        case ".xlsx":
                            try
                            {
                                rateCard = new CustomControls.RateCardViewers.ExcelViewer(rateCardFile);
                            }
                            catch
                            {
                                rateCard = new CustomControls.RateCardViewers.DefaultViewer(rateCardFile);
                            }
                            break;
                        case ".doc":
                        case ".docx":
                            try
                            {
                                rateCard = new CustomControls.RateCardViewers.WordViewer(rateCardFile);
                            }
                            catch
                            {
                                rateCard = new CustomControls.RateCardViewers.DefaultViewer(rateCardFile);
                            }
                            break;
                        default:
                            rateCard = new CustomControls.RateCardViewers.DefaultViewer(rateCardFile);
                            break;
                    }
                    this.RateCards.Add(rateCard);
                }
                this.RateCardContainer.xtraTabControlRateCards.TabPages.AddRange(this.RateCards.Select(x => x as DevExpress.XtraTab.XtraTabPage).ToArray());
            }
        }
    }

    interface IRateCardViewer
    {
        bool Loaded { get; }
        FileInfo File { get; }
        void ReleaseResources();
        void LoadViewer();
        void Email();
    }
}
