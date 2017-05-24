using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	class ResourceManager
	{
		public StorageFile LogoRibbonFile { get; private set; }
		public StorageFile TitlesConfigFile { get; private set; }

		public StorageFile DataCoverFile { get; private set; }
		public StorageFile DataCNAFile { get; private set; }
		public StorageFile DataFishingFile { get; private set; }
		public StorageFile DataCustomerFile { get; private set; }
		public StorageFile DataShareFile { get; private set; }
		public StorageFile DataROIFile { get; private set; }
		public StorageFile DataMarketFile { get; private set; }
		public StorageFile DataVideoFile { get; private set; }
		public StorageFile DataAudienceFile { get; private set; }
		public StorageFile DataSolutionFile { get; private set; }
		public StorageFile DataClosersFile { get; private set; }

		public StorageFile LogoTab1SubARightFile { get; private set; }
		public StorageFile LogoTab1SubAFooterFile { get; private set; }
		public StorageFile LogoTab1SubBRightFile { get; private set; }
		public StorageFile LogoTab1SubBFooterFile { get; private set; }

		public StorageFile LogoTab2SubARightFile { get; private set; }
		public StorageFile LogoTab2SubAFooterFile { get; private set; }
		public StorageFile LogoTab2SubBRightFile { get; private set; }
		public StorageFile LogoTab2SubBFooterFile { get; private set; }

		public StorageFile LogoTab3SubARightFile { get; private set; }
		public StorageFile LogoTab3SubAFooterFile { get; private set; }
		public StorageFile LogoTab3SubBRightFile { get; private set; }
		public StorageFile LogoTab3SubBFooterFile { get; private set; }
		public StorageFile LogoTab3SubCRightFile { get; private set; }
		public StorageFile LogoTab3SubCFooterFile { get; private set; }

		public StorageFile LogoTab4SubARightFile { get; private set; }
		public StorageFile LogoTab4SubAFooterFile { get; private set; }
		public StorageFile LogoTab4SubBRightFile { get; private set; }
		public StorageFile LogoTab4SubBFooterFile { get; private set; }
		public StorageFile LogoTab4SubCRightFile { get; private set; }
		public StorageFile LogoTab4SubCFooterFile { get; private set; }

		public StorageFile LogoTab5SubARightFile { get; private set; }
		public StorageFile LogoTab5SubAFooterFile { get; private set; }
		public StorageFile LogoTab5SubBRightFile { get; private set; }
		public StorageFile LogoTab5SubBFooterFile { get; private set; }
		public StorageFile LogoTab5SubCRightFile { get; private set; }
		public StorageFile LogoTab5SubCFooterFile { get; private set; }
		public StorageFile LogoTab5SubDRightFile { get; private set; }
		public StorageFile LogoTab5SubDFooterFile { get; private set; }
		public StorageFile LogoTab5SubERightFile { get; private set; }
		public StorageFile LogoTab5SubEFooterFile { get; private set; }

		public StorageFile LogoTab6SubARightFile { get; private set; }
		public StorageFile LogoTab6SubAFooterFile { get; private set; }
		public StorageFile LogoTab6SubBRightFile { get; private set; }
		public StorageFile LogoTab6SubBFooterFile { get; private set; }
		public StorageFile LogoTab6SubCRightFile { get; private set; }
		public StorageFile LogoTab6SubCFooterFile { get; private set; }
		public StorageFile LogoTab6SubDRightFile { get; private set; }
		public StorageFile LogoTab6SubDFooterFile { get; private set; }

		public StorageFile LogoTab7SubARightFile { get; private set; }
		public StorageFile LogoTab7SubAFooterFile { get; private set; }
		public StorageFile LogoTab7SubBRightFile { get; private set; }
		public StorageFile LogoTab7SubBFooterFile { get; private set; }
		public StorageFile LogoTab7SubCRightFile { get; private set; }
		public StorageFile LogoTab7SubCFooterFile { get; private set; }

		public StorageFile LogoTab8SubARightFile { get; private set; }
		public StorageFile LogoTab8SubAFooterFile { get; private set; }
		public StorageFile LogoTab8SubBRightFile { get; private set; }
		public StorageFile LogoTab8SubBFooterFile { get; private set; }
		public StorageFile LogoTab8SubCRightFile { get; private set; }
		public StorageFile LogoTab8SubCFooterFile { get; private set; }
		public StorageFile LogoTab8SubDRightFile { get; private set; }
		public StorageFile LogoTab8SubDFooterFile { get; private set; }

		public StorageFile LogoTab9SubARightFile { get; private set; }
		public StorageFile LogoTab9SubAFooterFile { get; private set; }
		public StorageFile LogoTab9SubBRightFile { get; private set; }
		public StorageFile LogoTab9SubBFooterFile { get; private set; }
		public StorageFile LogoTab9SubCRightFile { get; private set; }
		public StorageFile LogoTab9SubCFooterFile { get; private set; }

		public StorageFile LogoTab10SubARightFile { get; private set; }
		public StorageFile LogoTab10SubAFooterFile { get; private set; }
		public StorageFile LogoTab10SubBRightFile { get; private set; }
		public StorageFile LogoTab10SubBFooterFile { get; private set; }
		public StorageFile LogoTab10SubCRightFile { get; private set; }
		public StorageFile LogoTab10SubCFooterFile { get; private set; }
		public StorageFile LogoTab10SubDRightFile { get; private set; }
		public StorageFile LogoTab10SubDFooterFile { get; private set; }

		public StorageFile LogoTab11SubARightFile { get; private set; }
		public StorageFile LogoTab11SubAFooterFile { get; private set; }
		public StorageFile LogoTab11SubBRightFile { get; private set; }
		public StorageFile LogoTab11SubBFooterFile { get; private set; }
		public StorageFile LogoTab11SubCRightFile { get; private set; }
		public StorageFile LogoTab11SubCFooterFile { get; private set; }

		public async Task Load(StorageDirectory dataFolder)
		{
			var resourcesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge("resources"));

			LogoRibbonFile = new StorageFile(resourcesFolder.RelativePathParts.Merge("ribbon.png"));
			await LogoRibbonFile.Download();

			TitlesConfigFile = new StorageFile(resourcesFolder.RelativePathParts.Merge("settings.xml"));
			await TitlesConfigFile.Download();


			DataCoverFile = new StorageFile(dataFolder.RelativePathParts.Merge("01_cover.xml"));
			await DataCoverFile.Download();

			DataCNAFile = new StorageFile(dataFolder.RelativePathParts.Merge("02_cna.xml"));
			await DataCNAFile.Download();

			DataFishingFile = new StorageFile(dataFolder.RelativePathParts.Merge("03_fishing.xml"));
			await DataFishingFile.Download();

			DataCustomerFile = new StorageFile(dataFolder.RelativePathParts.Merge("04_customer.xml"));
			await DataCustomerFile.Download();

			DataShareFile = new StorageFile(dataFolder.RelativePathParts.Merge("05_share.xml"));
			await DataShareFile.Download();

			DataROIFile = new StorageFile(dataFolder.RelativePathParts.Merge("06_roi.xml"));
			await DataROIFile.Download();

			DataMarketFile = new StorageFile(dataFolder.RelativePathParts.Merge("07_market.xml"));
			await DataMarketFile.Download();

			DataVideoFile = new StorageFile(dataFolder.RelativePathParts.Merge("08_video.xml"));
			await DataVideoFile.Download();

			DataAudienceFile = new StorageFile(dataFolder.RelativePathParts.Merge("09_audience.xml"));
			await DataAudienceFile.Download();

			DataSolutionFile = new StorageFile(dataFolder.RelativePathParts.Merge("10_solution.xml"));
			await DataSolutionFile.Download();

			DataClosersFile = new StorageFile(dataFolder.RelativePathParts.Merge("11_closers.xml"));
			await DataClosersFile.Download();


			var imageResourceFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge("responsive_images"));

			#region Tab 1
			LogoTab1SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("1a_right.png"));
			await LogoTab1SubARightFile.Download();
			LogoTab1SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("1a_footer.png"));
			await LogoTab1SubAFooterFile.Download();

			LogoTab1SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("1b_right.png"));
			await LogoTab1SubBRightFile.Download();
			LogoTab1SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("1b_footer.png"));
			await LogoTab1SubBFooterFile.Download();
			#endregion

			#region Tab 2
			LogoTab2SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("2a_right.png"));
			await LogoTab2SubARightFile.Download();
			LogoTab2SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("2a_footer.png"));
			await LogoTab2SubAFooterFile.Download();

			LogoTab2SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("2b_right.png"));
			await LogoTab2SubBRightFile.Download();
			LogoTab2SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("2b_footer.png"));
			await LogoTab2SubBFooterFile.Download();
			#endregion

			#region Tab 3
			LogoTab3SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("3a_right.png"));
			await LogoTab3SubARightFile.Download();
			LogoTab3SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("3a_footer.png"));
			await LogoTab3SubAFooterFile.Download();

			LogoTab3SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("3b_right.png"));
			await LogoTab3SubBRightFile.Download();
			LogoTab3SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("3b_footer.png"));
			await LogoTab3SubBFooterFile.Download();

			LogoTab3SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("3c_right.png"));
			await LogoTab3SubCRightFile.Download();
			LogoTab3SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("3c_footer.png"));
			await LogoTab3SubCFooterFile.Download();
			#endregion

			#region Tab 4
			LogoTab4SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("4a_right.png"));
			await LogoTab4SubARightFile.Download();
			LogoTab4SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("4a_footer.png"));
			await LogoTab4SubAFooterFile.Download();

			LogoTab4SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("4b_right.png"));
			await LogoTab4SubBRightFile.Download();
			LogoTab4SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("4b_footer.png"));
			await LogoTab4SubBFooterFile.Download();

			LogoTab4SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("4c_right.png"));
			await LogoTab4SubCRightFile.Download();
			LogoTab4SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("4c_footer.png"));
			await LogoTab4SubCFooterFile.Download();
			#endregion

			#region Tab 5
			LogoTab5SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5a_right.png"));
			await LogoTab5SubARightFile.Download();
			LogoTab5SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5a_footer.png"));
			await LogoTab5SubAFooterFile.Download();

			LogoTab5SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5b_right.png"));
			await LogoTab5SubBRightFile.Download();
			LogoTab5SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5b_footer.png"));
			await LogoTab5SubBFooterFile.Download();

			LogoTab5SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5c_right.png"));
			await LogoTab5SubCRightFile.Download();
			LogoTab5SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5c_footer.png"));
			await LogoTab5SubCFooterFile.Download();

			LogoTab5SubDRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5d_right.png"));
			await LogoTab5SubDRightFile.Download();
			LogoTab5SubDFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5d_footer.png"));
			await LogoTab5SubDFooterFile.Download();

			LogoTab5SubERightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5e_right.png"));
			await LogoTab5SubERightFile.Download();
			LogoTab5SubEFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("5e_footer.png"));
			await LogoTab5SubEFooterFile.Download();
			#endregion

			#region Tab 6
			LogoTab6SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6a_right.png"));
			await LogoTab6SubARightFile.Download();
			LogoTab6SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6a_footer.png"));
			await LogoTab6SubAFooterFile.Download();

			LogoTab6SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6b_right.png"));
			await LogoTab6SubBRightFile.Download();
			LogoTab6SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6b_footer.png"));
			await LogoTab6SubBFooterFile.Download();

			LogoTab6SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6c_right.png"));
			await LogoTab6SubCRightFile.Download();
			LogoTab6SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6c_footer.png"));
			await LogoTab6SubCFooterFile.Download();

			LogoTab6SubDRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6d_right.png"));
			await LogoTab6SubDRightFile.Download();
			LogoTab6SubDFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("6d_footer.png"));
			await LogoTab6SubDFooterFile.Download();
			#endregion

			#region Tab 7
			LogoTab7SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("7a_right.png"));
			await LogoTab7SubARightFile.Download();
			LogoTab7SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("7a_footer.png"));
			await LogoTab7SubAFooterFile.Download();

			LogoTab7SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("7b_right.png"));
			await LogoTab7SubBRightFile.Download();
			LogoTab7SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("7b_footer.png"));
			await LogoTab7SubBFooterFile.Download();

			LogoTab7SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("7c_right.png"));
			await LogoTab7SubCRightFile.Download();
			LogoTab7SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("7c_footer.png"));
			await LogoTab7SubCFooterFile.Download();
			#endregion

			#region Tab 8
			LogoTab8SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8a_right.png"));
			await LogoTab8SubARightFile.Download();
			LogoTab8SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8a_footer.png"));
			await LogoTab8SubAFooterFile.Download();

			LogoTab8SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8b_right.png"));
			await LogoTab8SubBRightFile.Download();
			LogoTab8SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8b_footer.png"));
			await LogoTab8SubBFooterFile.Download();

			LogoTab8SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8c_right.png"));
			await LogoTab8SubCRightFile.Download();
			LogoTab8SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8c_footer.png"));
			await LogoTab8SubCFooterFile.Download();

			LogoTab8SubDRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8d_right.png"));
			await LogoTab8SubDRightFile.Download();
			LogoTab8SubDFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("8d_footer.png"));
			await LogoTab8SubDFooterFile.Download();
			#endregion

			#region Tab 9
			LogoTab9SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("9a_right.png"));
			await LogoTab9SubARightFile.Download();
			LogoTab9SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("9a_footer.png"));
			await LogoTab9SubAFooterFile.Download();

			LogoTab9SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("9b_right.png"));
			await LogoTab9SubBRightFile.Download();
			LogoTab9SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("9b_footer.png"));
			await LogoTab9SubBFooterFile.Download();

			LogoTab9SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("9c_right.png"));
			await LogoTab9SubCRightFile.Download();
			LogoTab9SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("9c_footer.png"));
			await LogoTab9SubCFooterFile.Download();
			#endregion

			#region Tab 10
			LogoTab10SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10a_right.png"));
			await LogoTab10SubARightFile.Download();
			LogoTab10SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10a_footer.png"));
			await LogoTab10SubAFooterFile.Download();

			LogoTab10SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10b_right.png"));
			await LogoTab10SubBRightFile.Download();
			LogoTab10SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10b_footer.png"));
			await LogoTab10SubBFooterFile.Download();

			LogoTab10SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10c_right.png"));
			await LogoTab10SubCRightFile.Download();
			LogoTab10SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10c_footer.png"));
			await LogoTab10SubCFooterFile.Download();

			LogoTab10SubDRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10d_right.png"));
			await LogoTab10SubDRightFile.Download();
			LogoTab10SubDFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("10d_footer.png"));
			await LogoTab10SubDFooterFile.Download();
			#endregion

			#region Tab 11
			LogoTab11SubARightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("11a_right.png"));
			await LogoTab11SubARightFile.Download();
			LogoTab11SubAFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("11a_footer.png"));
			await LogoTab11SubAFooterFile.Download();

			LogoTab11SubBRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("11b_right.png"));
			await LogoTab11SubBRightFile.Download();
			LogoTab11SubBFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("11b_footer.png"));
			await LogoTab11SubBFooterFile.Download();

			LogoTab11SubCRightFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("11c_right.png"));
			await LogoTab11SubCRightFile.Download();
			LogoTab11SubCFooterFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("11c_footer.png"));
			await LogoTab11SubCFooterFile.Download();
			#endregion
		}
	}
}
