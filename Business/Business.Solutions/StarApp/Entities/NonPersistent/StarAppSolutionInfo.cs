using System.Drawing;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Dictionaries;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Dictionaries;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;
using CoverLists = Asa.Business.Solutions.StarApp.Dictionaries.CoverLists;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class StarAppSolutionInfo : BaseSolutionInfo
	{
		public Image RibbonLogo { get; private set; }
		public TitlesConfiguration Titles { get; } = new TitlesConfiguration();

		public CoverLists CoverLists { get; }
		public CNALists CNALists { get; }
		public FishingLists FishingLists { get; }
		public CustomerLists CustomerLists { get; }
		public ShareLists ShareLists { get; }
		public ROILists ROILists { get; }
		public MarketLists MarketLists { get; }
		public VideoLists VideoLists { get; }
		public AudienceLists AudienceLists { get; }
		public SolutionLists SolutionLists { get; }
		public ClosersLists ClosersLists { get; }

		public Image Tab1SubARightLogo { get; private set; }
		public Image Tab1SubAFooterLogo { get; private set; }
		public Image Tab1SubBRightLogo { get; private set; }
		public Image Tab1SubBFooterLogo { get; private set; }

		public Image Tab2SubARightLogo { get; private set; }
		public Image Tab2SubAFooterLogo { get; private set; }
		public Image Tab2SubBRightLogo { get; private set; }
		public Image Tab2SubBFooterLogo { get; private set; }

		public Image Tab3SubARightLogo { get; private set; }
		public Image Tab3SubAFooterLogo { get; private set; }
		public Image Tab3SubBRightLogo { get; private set; }
		public Image Tab3SubBFooterLogo { get; private set; }
		public Image Tab3SubCRightLogo { get; private set; }
		public Image Tab3SubCFooterLogo { get; private set; }

		public Image Tab4SubARightLogo { get; private set; }
		public Image Tab4SubAFooterLogo { get; private set; }
		public Image Tab4SubBRightLogo { get; private set; }
		public Image Tab4SubBFooterLogo { get; private set; }
		public Image Tab4SubCRightLogo { get; private set; }
		public Image Tab4SubCFooterLogo { get; private set; }

		public Image Tab5SubARightLogo { get; private set; }
		public Image Tab5SubAFooterLogo { get; private set; }
		public Image Tab5SubBRightLogo { get; private set; }
		public Image Tab5SubBFooterLogo { get; private set; }
		public Image Tab5SubCRightLogo { get; private set; }
		public Image Tab5SubCFooterLogo { get; private set; }
		public Image Tab5SubDRightLogo { get; private set; }
		public Image Tab5SubDFooterLogo { get; private set; }
		public Image Tab5SubERightLogo { get; private set; }
		public Image Tab5SubEFooterLogo { get; private set; }

		public Image Tab6SubARightLogo { get; private set; }
		public Image Tab6SubAFooterLogo { get; private set; }
		public Image Tab6SubBRightLogo { get; private set; }
		public Image Tab6SubBFooterLogo { get; private set; }
		public Image Tab6SubCRightLogo { get; private set; }
		public Image Tab6SubCFooterLogo { get; private set; }
		public Image Tab6SubDRightLogo { get; private set; }
		public Image Tab6SubDFooterLogo { get; private set; }

		public Image Tab7SubARightLogo { get; private set; }
		public Image Tab7SubAFooterLogo { get; private set; }
		public Image Tab7SubBRightLogo { get; private set; }
		public Image Tab7SubBFooterLogo { get; private set; }
		public Image Tab7SubCRightLogo { get; private set; }
		public Image Tab7SubCFooterLogo { get; private set; }

		public Image Tab8SubARightLogo { get; private set; }
		public Image Tab8SubAFooterLogo { get; private set; }
		public Image Tab8SubBRightLogo { get; private set; }
		public Image Tab8SubBFooterLogo { get; private set; }
		public Image Tab8SubCRightLogo { get; private set; }
		public Image Tab8SubCFooterLogo { get; private set; }
		public Image Tab8SubDRightLogo { get; private set; }
		public Image Tab8SubDFooterLogo { get; private set; }

		public Image Tab9SubARightLogo { get; private set; }
		public Image Tab9SubAFooterLogo { get; private set; }
		public Image Tab9SubBRightLogo { get; private set; }
		public Image Tab9SubBFooterLogo { get; private set; }
		public Image Tab9SubCRightLogo { get; private set; }
		public Image Tab9SubCFooterLogo { get; private set; }

		public Image Tab10SubARightLogo { get; private set; }
		public Image Tab10SubAFooterLogo { get; private set; }
		public Image Tab10SubBRightLogo { get; private set; }
		public Image Tab10SubBFooterLogo { get; private set; }
		public Image Tab10SubCRightLogo { get; private set; }
		public Image Tab10SubCFooterLogo { get; private set; }
		public Image Tab10SubDRightLogo { get; private set; }
		public Image Tab10SubDFooterLogo { get; private set; }

		public Image Tab11SubARightLogo { get; private set; }
		public Image Tab11SubAFooterLogo { get; private set; }
		public Image Tab11SubBRightLogo { get; private set; }
		public Image Tab11SubBFooterLogo { get; private set; }
		public Image Tab11SubCRightLogo { get; private set; }
		public Image Tab11SubCFooterLogo { get; private set; }

		public StarAppSolutionInfo()
		{
			Type = SolutionType.StarApp;

			CoverLists = new CoverLists();
			CNALists = new CNALists();
			FishingLists = new FishingLists();
			CustomerLists = new CustomerLists();
			ShareLists = new ShareLists();
			ROILists = new ROILists();
			MarketLists = new MarketLists();
			VideoLists = new VideoLists();
			AudienceLists = new AudienceLists();
			SolutionLists = new SolutionLists();
			ClosersLists = new ClosersLists();
		}

		public override void LoadData(StorageDirectory holderAppDataFolder)
		{
			base.LoadData(holderAppDataFolder);

			var resourceManager = new ResourceManager();

			AsyncHelper.RunSync(() => resourceManager.Load(DataFolder));

			Titles.Load(resourceManager.TitlesConfigFile);

			RibbonLogo = resourceManager.LogoRibbonFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoRibbonFile.LocalPath)
				: null;

			CoverLists.Load(resourceManager.DataCoverFile);
			CNALists.Load(resourceManager.DataCNAFile);
			FishingLists.Load(resourceManager.DataFishingFile);
			CustomerLists.Load(resourceManager.DataCustomerFile);
			ShareLists.Load(resourceManager.DataShareFile);
			ROILists.Load(resourceManager.DataROIFile);
			MarketLists.Load(resourceManager.DataMarketFile);
			VideoLists.Load(resourceManager.DataVideoFile);
			AudienceLists.Load(resourceManager.DataAudienceFile);
			SolutionLists.Load(resourceManager.DataSolutionFile);
			ClosersLists.Load(resourceManager.DataClosersFile);

			Tab1SubARightLogo = resourceManager.LogoTab1SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubARightFile.LocalPath)
				: null;
			Tab1SubAFooterLogo = resourceManager.LogoTab1SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubAFooterFile.LocalPath)
				: null;
			Tab1SubBRightLogo = resourceManager.LogoTab1SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubBRightFile.LocalPath)
				: null;
			Tab1SubBFooterLogo = resourceManager.LogoTab1SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubBFooterFile.LocalPath)
				: null;

			Tab2SubARightLogo = resourceManager.LogoTab2SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubARightFile.LocalPath)
				: null;
			Tab2SubAFooterLogo = resourceManager.LogoTab2SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubAFooterFile.LocalPath)
				: null;
			Tab2SubBRightLogo = resourceManager.LogoTab2SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubBRightFile.LocalPath)
				: null;
			Tab2SubBFooterLogo = resourceManager.LogoTab2SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubBFooterFile.LocalPath)
				: null;

			Tab3SubARightLogo = resourceManager.LogoTab3SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubARightFile.LocalPath)
				: null;
			Tab3SubAFooterLogo = resourceManager.LogoTab3SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubAFooterFile.LocalPath)
				: null;
			Tab3SubBRightLogo = resourceManager.LogoTab3SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubBRightFile.LocalPath)
				: null;
			Tab3SubBFooterLogo = resourceManager.LogoTab3SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubBFooterFile.LocalPath)
				: null;
			Tab3SubCRightLogo = resourceManager.LogoTab3SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCRightFile.LocalPath)
				: null;
			Tab3SubCFooterLogo = resourceManager.LogoTab3SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCFooterFile.LocalPath)
				: null;

			Tab4SubARightLogo = resourceManager.LogoTab4SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubARightFile.LocalPath)
				: null;
			Tab4SubAFooterLogo = resourceManager.LogoTab4SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubAFooterFile.LocalPath)
				: null;
			Tab4SubBRightLogo = resourceManager.LogoTab4SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBRightFile.LocalPath)
				: null;
			Tab4SubBFooterLogo = resourceManager.LogoTab4SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBFooterFile.LocalPath)
				: null;
			Tab4SubCRightLogo = resourceManager.LogoTab4SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubCRightFile.LocalPath)
				: null;
			Tab4SubCFooterLogo = resourceManager.LogoTab4SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubCFooterFile.LocalPath)
				: null;

			Tab5SubARightLogo = resourceManager.LogoTab5SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubARightFile.LocalPath)
				: null;
			Tab5SubAFooterLogo = resourceManager.LogoTab5SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubAFooterFile.LocalPath)
				: null;
			Tab5SubBRightLogo = resourceManager.LogoTab5SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubBRightFile.LocalPath)
				: null;
			Tab5SubBFooterLogo = resourceManager.LogoTab5SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubBFooterFile.LocalPath)
				: null;
			Tab5SubCRightLogo = resourceManager.LogoTab5SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubCRightFile.LocalPath)
				: null;
			Tab5SubCFooterLogo = resourceManager.LogoTab5SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubCFooterFile.LocalPath)
				: null;
			Tab5SubDRightLogo = resourceManager.LogoTab5SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubDRightFile.LocalPath)
				: null;
			Tab5SubDFooterLogo = resourceManager.LogoTab5SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubDFooterFile.LocalPath)
				: null;
			Tab5SubERightLogo = resourceManager.LogoTab5SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubERightFile.LocalPath)
				: null;
			Tab5SubEFooterLogo = resourceManager.LogoTab5SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubEFooterFile.LocalPath)
				: null;

			Tab6SubARightLogo = resourceManager.LogoTab6SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubARightFile.LocalPath)
				: null;
			Tab6SubAFooterLogo = resourceManager.LogoTab6SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubAFooterFile.LocalPath)
				: null;
			Tab6SubBRightLogo = resourceManager.LogoTab6SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubBRightFile.LocalPath)
				: null;
			Tab6SubBFooterLogo = resourceManager.LogoTab6SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubBFooterFile.LocalPath)
				: null;
			Tab6SubCRightLogo = resourceManager.LogoTab6SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubCRightFile.LocalPath)
				: null;
			Tab6SubCFooterLogo = resourceManager.LogoTab6SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubCFooterFile.LocalPath)
				: null;
			Tab6SubDRightLogo = resourceManager.LogoTab6SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubDRightFile.LocalPath)
				: null;
			Tab6SubDFooterLogo = resourceManager.LogoTab6SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubDFooterFile.LocalPath)
				: null;

			Tab7SubARightLogo = resourceManager.LogoTab7SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubARightFile.LocalPath)
				: null;
			Tab7SubAFooterLogo = resourceManager.LogoTab7SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubAFooterFile.LocalPath)
				: null;
			Tab7SubBRightLogo = resourceManager.LogoTab7SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBRightFile.LocalPath)
				: null;
			Tab7SubBFooterLogo = resourceManager.LogoTab7SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBFooterFile.LocalPath)
				: null;
			Tab7SubCRightLogo = resourceManager.LogoTab7SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubCRightFile.LocalPath)
				: null;
			Tab7SubCFooterLogo = resourceManager.LogoTab7SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubCFooterFile.LocalPath)
				: null;

			Tab8SubARightLogo = resourceManager.LogoTab8SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubARightFile.LocalPath)
				: null;
			Tab8SubAFooterLogo = resourceManager.LogoTab8SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubAFooterFile.LocalPath)
				: null;
			Tab8SubBRightLogo = resourceManager.LogoTab8SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubBRightFile.LocalPath)
				: null;
			Tab8SubBFooterLogo = resourceManager.LogoTab8SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubBFooterFile.LocalPath)
				: null;
			Tab8SubCRightLogo = resourceManager.LogoTab8SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubCRightFile.LocalPath)
				: null;
			Tab8SubCFooterLogo = resourceManager.LogoTab8SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubCFooterFile.LocalPath)
				: null;
			Tab8SubDRightLogo = resourceManager.LogoTab8SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubDRightFile.LocalPath)
				: null;
			Tab8SubDFooterLogo = resourceManager.LogoTab8SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubDFooterFile.LocalPath)
				: null;

			Tab9SubARightLogo = resourceManager.LogoTab9SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubARightFile.LocalPath)
				: null;
			Tab9SubAFooterLogo = resourceManager.LogoTab9SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubAFooterFile.LocalPath)
				: null;
			Tab9SubBRightLogo = resourceManager.LogoTab9SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubBRightFile.LocalPath)
				: null;
			Tab9SubBFooterLogo = resourceManager.LogoTab9SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubBFooterFile.LocalPath)
				: null;
			Tab9SubCRightLogo = resourceManager.LogoTab9SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCRightFile.LocalPath)
				: null;
			Tab9SubCFooterLogo = resourceManager.LogoTab9SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCFooterFile.LocalPath)
				: null;

			Tab10SubARightLogo = resourceManager.LogoTab10SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubARightFile.LocalPath)
				: null;
			Tab10SubAFooterLogo = resourceManager.LogoTab10SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubAFooterFile.LocalPath)
				: null;
			Tab10SubBRightLogo = resourceManager.LogoTab10SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubBRightFile.LocalPath)
				: null;
			Tab10SubBFooterLogo = resourceManager.LogoTab10SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubBFooterFile.LocalPath)
				: null;
			Tab10SubCRightLogo = resourceManager.LogoTab10SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubCRightFile.LocalPath)
				: null;
			Tab10SubCFooterLogo = resourceManager.LogoTab10SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubCFooterFile.LocalPath)
				: null;
			Tab10SubDRightLogo = resourceManager.LogoTab10SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubDRightFile.LocalPath)
				: null;
			Tab10SubDFooterLogo = resourceManager.LogoTab10SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubDFooterFile.LocalPath)
				: null;

			Tab11SubARightLogo = resourceManager.LogoTab11SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubARightFile.LocalPath)
				: null;
			Tab11SubAFooterLogo = resourceManager.LogoTab11SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubAFooterFile.LocalPath)
				: null;
			Tab11SubBRightLogo = resourceManager.LogoTab11SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubBRightFile.LocalPath)
				: null;
			Tab11SubBFooterLogo = resourceManager.LogoTab11SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubBFooterFile.LocalPath)
				: null;
			Tab11SubCRightLogo = resourceManager.LogoTab11SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubCRightFile.LocalPath)
				: null;
			Tab11SubCFooterLogo = resourceManager.LogoTab11SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubCFooterFile.LocalPath)
				: null;
		}
	}
}
