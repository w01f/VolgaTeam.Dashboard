using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExpress.XtraTab;
using Asa.Core.Common;
using Asa.Core.Interop;

namespace Asa.CommonGUI.RateCard
{
	public class RateCardManager
	{
		private readonly StorageDirectory _rootFolder;
		public RateCardManager(StorageDirectory rootFolder)
		{
			_rootFolder = rootFolder;
			RateCardFolders = new List<RateCardFolder>();
		}

		public List<RateCardFolder> RateCardFolders { get; private set; }

		public void LoadRateCards()
		{
			RateCardFolders.Clear();
			if (!_rootFolder.ExistsLocal()) return;
			var rateCatdFolders = _rootFolder.GetLocalFolders().ToList();
			rateCatdFolders.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
			foreach (var rateCardFolder in rateCatdFolders.Select(rateCardFolder => new RateCardFolder(rateCardFolder)))
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
		private readonly StorageDirectory _rootFolder;
		public RateCardFolder(StorageDirectory rootFolder)
		{
			_rootFolder = rootFolder;
			RateCards = new List<IRateCardViewer>();
			RateCardContainer = new RateFolderControl();
			RateCardContainer.xtraTabControlRateCards.SelectedPageChanged += SelectRateCard;
		}

		public RateFolderControl RateCardContainer { get; private set; }
		public List<IRateCardViewer> RateCards { get; private set; }

		public string FolderName
		{
			get { return Regex.Replace(_rootFolder.Name, @"\d+_", string.Empty); }
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

			var files = _rootFolder.GetLocalFiles().ToList();
			foreach (var rateCardFile in files.Select(f => new FileInfo(f.LocalPath)))
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