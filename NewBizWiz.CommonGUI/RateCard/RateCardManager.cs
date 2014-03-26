using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DevExpress.XtraTab;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.CommonGUI.RateCard
{
	public class RateCardManager
	{
		private readonly string _contentPath;

		public RateCardManager(string path)
		{
			_contentPath = path;
			RateCardFolders = new List<RateCardFolder>();
		}

		public List<RateCardFolder> RateCardFolders { get; private set; }

		public void LoadRateCards()
		{
			RateCardFolders.Clear();
			if (!Directory.Exists(_contentPath)) return;
			var rateCatdFolders = new List<string>();
			rateCatdFolders.AddRange(Directory.GetDirectories(_contentPath));
			rateCatdFolders.Sort(WinAPIHelper.StrCmpLogicalW);
			foreach (var rateCardFolder in rateCatdFolders.Select(rateCardFolderPath => new RateCardFolder(rateCardFolderPath)))
				RateCardFolders.Add(rateCardFolder);
		}

		public void ReleaseRateCards()
		{
			foreach (var rateCardFolder in RateCardFolders)
				rateCardFolder.ReleaseRateCards();
			RateCardFolders.Clear();
		}
	}

	public class RateCardFolder
	{
		public RateCardFolder(string parentFolderPath)
		{
			Folder = new DirectoryInfo(parentFolderPath);
			RateCards = new List<IRateCardViewer>();
			RateCardContainer = new RateFolderControl();
			RateCardContainer.xtraTabControlRateCards.SelectedPageChanged += SelectRateCard;
		}

		public DirectoryInfo Folder { get; private set; }
		public RateFolderControl RateCardContainer { get; private set; }
		public List<IRateCardViewer> RateCards { get; private set; }

		public string FolderName
		{
			get { return Regex.Replace(Folder.Name, @"\d+_", string.Empty); }
		}

		private void SelectRateCard(object sender, TabPageChangedEventArgs e)
		{
			var rateCard = e.Page as IRateCardViewer;
			if (rateCard != null)
			{
				rateCard.LoadViewer();
			}
		}

		public void LoadRateCards()
		{
			if (RateCards.Any()) return;
			foreach (var rateCardFile in Folder.GetFiles())
			{
				IRateCardViewer rateCard;
				switch (rateCardFile.Extension.ToLower())
				{
					case ".pdf":
						try
						{
							rateCard = new PDFViewer(rateCardFile);
						}
						catch
						{
							rateCard = new DefaultViewer(rateCardFile);
						}
						break;
					case ".xls":
					case ".xlsx":
						try
						{
							rateCard = new ExcelViewer(rateCardFile);
						}
						catch
						{
							rateCard = new DefaultViewer(rateCardFile);
						}
						break;
					case ".doc":
					case ".docx":
						try
						{
							rateCard = new WordViewer(rateCardFile);
						}
						catch
						{
							rateCard = new DefaultViewer(rateCardFile);
						}
						break;
					default:
						rateCard = new DefaultViewer(rateCardFile);
						break;
				}
				RateCards.Add(rateCard);
			}
			RateCardContainer.xtraTabControlRateCards.TabPages.AddRange(RateCards.Select(x => x as XtraTabPage).ToArray());
		}

		public void ReleaseRateCards()
		{
			foreach (var rateCard in RateCards)
				rateCard.ReleaseResources();
			RateCards.Clear();
			RateCardContainer.xtraTabControlRateCards.TabPages.Clear();
		}
	}

	public interface IRateCardViewer
	{
		bool Loaded { get; }
		FileInfo File { get; }
		void ReleaseResources();
		void LoadViewer();
		void Email();
	}
}