using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ResourceManager
	{
		public StorageFile SettingsFile { get; private set; }

		public StorageFile DataCoverPartAFile { get; private set; }

		public StorageFile DataCNAPartAFile { get; private set; }
		public StorageFile DataCNAPartBFile { get; private set; }

		public StorageFile DataFishingPartAFile { get; private set; }
		public StorageFile DataFishingPartBFile { get; private set; }
		public StorageFile DataFishingPartCFile { get; private set; }

		public StorageFile DataCustomerPartAFile { get; private set; }
		public StorageFile DataCustomerPartBFile { get; private set; }
		public StorageFile DataCustomerPartCFile { get; private set; }

		public StorageFile DataSharePartAFile { get; private set; }
		public StorageFile DataSharePartBFile { get; private set; }
		public StorageFile DataSharePartCFile { get; private set; }
		public StorageFile DataSharePartDFile { get; private set; }
		public StorageFile DataSharePartEFile { get; private set; }

		public StorageFile DataROIPartAFile { get; private set; }
		public StorageFile DataROIPartBFile { get; private set; }
		public StorageFile DataROIPartCFile { get; private set; }
		public StorageFile DataROIPartDFile { get; private set; }

		public StorageFile DataMarketPartAFile { get; private set; }
		public StorageFile DataMarketPartBFile { get; private set; }
		public StorageFile DataMarketPartCFile { get; private set; }

		public StorageFile DataVideoPartAFile { get; private set; }
		public StorageFile DataVideoPartBFile { get; private set; }
		public StorageFile DataVideoPartCFile { get; private set; }
		public StorageFile DataVideoPartDFile { get; private set; }

		public StorageFile DataAudiencePartAFile { get; private set; }
		public StorageFile DataAudiencePartBFile { get; private set; }
		public StorageFile DataAudiencePartCFile { get; private set; }

		public StorageFile DataSolutionPartAFile { get; private set; }
		public StorageFile DataSolutionPartBFile { get; private set; }
		public StorageFile DataSolutionPartCFile { get; private set; }
		public StorageFile DataSolutionPartDFile { get; private set; }

		public StorageFile DataClosersPartAFile { get; private set; }
		public StorageFile DataClosersPartBFile { get; private set; }
		public StorageFile DataClosersPartCFile { get; private set; }

		public StorageFile LogoTab1SubARightFile { get; private set; }
		public StorageFile LogoTab1SubAFooterFile { get; private set; }

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
			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));
			await SettingsFile.Download();

			DataCoverPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "data", "1a.xml" }));
			await DataCoverPartAFile.Download();

			DataCNAPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "data", "2a.xml" }));
			await DataCNAPartAFile.Download();
			DataCNAPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "data", "2b.xml" }));
			await DataCNAPartBFile.Download();

			DataFishingPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "data", "3a.xml" }));
			await DataFishingPartAFile.Download();
			DataFishingPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "data", "3b.xml" }));
			await DataFishingPartBFile.Download();
			DataFishingPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "data", "3c.xml" }));
			await DataFishingPartCFile.Download();

			DataCustomerPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "data", "4a.xml" }));
			await DataCustomerPartAFile.Download();
			DataCustomerPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "data", "4b.xml" }));
			await DataCustomerPartBFile.Download();
			DataCustomerPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "data", "4c.xml" }));
			await DataCustomerPartCFile.Download();

			DataSharePartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "data", "5a.xml" }));
			await DataSharePartAFile.Download();
			DataSharePartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "data", "5b.xml" }));
			await DataSharePartBFile.Download();
			DataSharePartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "data", "5c.xml" }));
			await DataSharePartCFile.Download();
			DataSharePartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "data", "5d.xml" }));
			await DataSharePartDFile.Download();
			DataSharePartEFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "data", "5e.xml" }));
			await DataSharePartEFile.Download();

			DataROIPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "data", "6a.xml" }));
			await DataROIPartAFile.Download();
			DataROIPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "data", "6b.xml" }));
			await DataROIPartBFile.Download();
			DataROIPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "data", "6c.xml" }));
			await DataROIPartCFile.Download();
			DataROIPartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "data", "6d.xml" }));
			await DataROIPartDFile.Download();

			DataMarketPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "data", "7a.xml" }));
			await DataMarketPartAFile.Download();
			DataMarketPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "data", "7b.xml" }));
			await DataMarketPartBFile.Download();
			DataMarketPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "data", "7c.xml" }));
			await DataMarketPartCFile.Download();

			DataVideoPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "data", "8a.xml" }));
			await DataVideoPartAFile.Download();
			DataVideoPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "data", "8b.xml" }));
			await DataVideoPartBFile.Download();
			DataVideoPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "data", "8c.xml" }));
			await DataVideoPartCFile.Download();
			DataVideoPartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "data", "8d.xml" }));
			await DataVideoPartDFile.Download();

			DataAudiencePartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "data", "9a.xml" }));
			await DataAudiencePartAFile.Download();
			DataAudiencePartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "data", "9b.xml" }));
			await DataAudiencePartBFile.Download();
			DataAudiencePartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "data", "9c.xml" }));
			await DataAudiencePartCFile.Download();

			DataSolutionPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "data", "10a.xml" }));
			await DataSolutionPartAFile.Download();
			DataSolutionPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "data", "10b.xml" }));
			await DataSolutionPartBFile.Download();
			DataSolutionPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "data", "10c.xml" }));
			await DataSolutionPartCFile.Download();
			DataSolutionPartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "data", "10d.xml" }));
			await DataSolutionPartDFile.Download();

			DataClosersPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "data", "11a.xml" }));
			await DataClosersPartAFile.Download();
			DataClosersPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "data", "11b.xml" }));
			await DataClosersPartBFile.Download();
			DataClosersPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "data", "11c.xml" }));
			await DataClosersPartCFile.Download();


			#region Tab 1
			LogoTab1SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "design_branding", "1a_right.png" }));
			await LogoTab1SubARightFile.Download();
			LogoTab1SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "design_branding", "1a_footer.png" }));
			await LogoTab1SubAFooterFile.Download();
			#endregion

			#region Tab 2
			LogoTab2SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "design_branding", "2a_right.png" }));
			await LogoTab2SubARightFile.Download();
			LogoTab2SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "design_branding", "2a_footer.png" }));
			await LogoTab2SubAFooterFile.Download();

			LogoTab2SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "design_branding", "2b_right.png" }));
			await LogoTab2SubBRightFile.Download();
			LogoTab2SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "design_branding", "2b_footer.png" }));
			await LogoTab2SubBFooterFile.Download();
			#endregion

			#region Tab 3
			LogoTab3SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "design_branding", "3a_right.png" }));
			await LogoTab3SubARightFile.Download();
			LogoTab3SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "design_branding", "3a_footer.png" }));
			await LogoTab3SubAFooterFile.Download();

			LogoTab3SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "design_branding", "3b_right.png" }));
			await LogoTab3SubBRightFile.Download();
			LogoTab3SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "design_branding", "3b_footer.png" }));
			await LogoTab3SubBFooterFile.Download();

			LogoTab3SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "design_branding", "3c_right.png" }));
			await LogoTab3SubCRightFile.Download();
			LogoTab3SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "design_branding", "3c_footer.png" }));
			await LogoTab3SubCFooterFile.Download();
			#endregion

			#region Tab 4
			LogoTab4SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "design_branding", "4a_right.png" }));
			await LogoTab4SubARightFile.Download();
			LogoTab4SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "design_branding", "4a_footer.png" }));
			await LogoTab4SubAFooterFile.Download();

			LogoTab4SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "design_branding", "4b_right.png" }));
			await LogoTab4SubBRightFile.Download();
			LogoTab4SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "design_branding", "4b_footer.png" }));
			await LogoTab4SubBFooterFile.Download();

			LogoTab4SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "design_branding", "4c_right.png" }));
			await LogoTab4SubCRightFile.Download();
			LogoTab4SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "design_branding", "4c_footer.png" }));
			await LogoTab4SubCFooterFile.Download();
			#endregion

			#region Tab 5
			LogoTab5SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "design_branding", "5a_right.png" }));
			await LogoTab5SubARightFile.Download();
			LogoTab5SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "design_branding", "5a_footer.png" }));
			await LogoTab5SubAFooterFile.Download();

			LogoTab5SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "design_branding", "5b_right.png" }));
			await LogoTab5SubBRightFile.Download();
			LogoTab5SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "design_branding", "5b_footer.png" }));
			await LogoTab5SubBFooterFile.Download();

			LogoTab5SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "design_branding", "5c_right.png" }));
			await LogoTab5SubCRightFile.Download();
			LogoTab5SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "design_branding", "5c_footer.png" }));
			await LogoTab5SubCFooterFile.Download();

			LogoTab5SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "design_branding", "5d_right.png" }));
			await LogoTab5SubDRightFile.Download();
			LogoTab5SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "design_branding", "5d_footer.png" }));
			await LogoTab5SubDFooterFile.Download();

			LogoTab5SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "design_branding", "5e_right.png" }));
			await LogoTab5SubERightFile.Download();
			LogoTab5SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "design_branding", "5e_footer.png" }));
			await LogoTab5SubEFooterFile.Download();
			#endregion

			#region Tab 6
			LogoTab6SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "design_branding", "6a_right.png" }));
			await LogoTab6SubARightFile.Download();
			LogoTab6SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "design_branding", "6a_footer.png" }));
			await LogoTab6SubAFooterFile.Download();

			LogoTab6SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "design_branding", "6b_right.png" }));
			await LogoTab6SubBRightFile.Download();
			LogoTab6SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "design_branding", "6b_footer.png" }));
			await LogoTab6SubBFooterFile.Download();

			LogoTab6SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "design_branding", "6c_right.png" }));
			await LogoTab6SubCRightFile.Download();
			LogoTab6SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "design_branding", "6c_footer.png" }));
			await LogoTab6SubCFooterFile.Download();

			LogoTab6SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "design_branding", "6d_right.png" }));
			await LogoTab6SubDRightFile.Download();
			LogoTab6SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "design_branding", "6d_footer.png" }));
			await LogoTab6SubDFooterFile.Download();
			#endregion

			#region Tab 7
			LogoTab7SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "design_branding", "7a_right.png" }));
			await LogoTab7SubARightFile.Download();
			LogoTab7SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "design_branding", "7a_footer.png" }));
			await LogoTab7SubAFooterFile.Download();

			LogoTab7SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "design_branding", "7b_right.png" }));
			await LogoTab7SubBRightFile.Download();
			LogoTab7SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "design_branding", "7b_footer.png" }));
			await LogoTab7SubBFooterFile.Download();

			LogoTab7SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "design_branding", "7c_right.png" }));
			await LogoTab7SubCRightFile.Download();
			LogoTab7SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "design_branding", "7c_footer.png" }));
			await LogoTab7SubCFooterFile.Download();
			#endregion

			#region Tab 8
			LogoTab8SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "design_branding", "8a_right.png" }));
			await LogoTab8SubARightFile.Download();
			LogoTab8SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "design_branding", "8a_footer.png" }));
			await LogoTab8SubAFooterFile.Download();

			LogoTab8SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "design_branding", "8b_right.png" }));
			await LogoTab8SubBRightFile.Download();
			LogoTab8SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "design_branding", "8b_footer.png" }));
			await LogoTab8SubBFooterFile.Download();

			LogoTab8SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "design_branding", "8c_right.png" }));
			await LogoTab8SubCRightFile.Download();
			LogoTab8SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "design_branding", "8c_footer.png" }));
			await LogoTab8SubCFooterFile.Download();

			LogoTab8SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "design_branding", "8d_right.png" }));
			await LogoTab8SubDRightFile.Download();
			LogoTab8SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "design_branding", "8d_footer.png" }));
			await LogoTab8SubDFooterFile.Download();
			#endregion

			#region Tab 9
			LogoTab9SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "design_branding", "9a_right.png" }));
			await LogoTab9SubARightFile.Download();
			LogoTab9SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "design_branding", "9a_footer.png" }));
			await LogoTab9SubAFooterFile.Download();

			LogoTab9SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "design_branding", "9b_right.png" }));
			await LogoTab9SubBRightFile.Download();
			LogoTab9SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "design_branding", "9b_footer.png" }));
			await LogoTab9SubBFooterFile.Download();

			LogoTab9SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "design_branding", "9c_right.png" }));
			await LogoTab9SubCRightFile.Download();
			LogoTab9SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "design_branding", "9c_footer.png" }));
			await LogoTab9SubCFooterFile.Download();
			#endregion

			#region Tab 10
			LogoTab10SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "design_branding", "10a_right.png" }));
			await LogoTab10SubARightFile.Download();
			LogoTab10SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "design_branding", "10a_footer.png" }));
			await LogoTab10SubAFooterFile.Download();

			LogoTab10SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "design_branding", "10b_right.png" }));
			await LogoTab10SubBRightFile.Download();
			LogoTab10SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "design_branding", "10b_footer.png" }));
			await LogoTab10SubBFooterFile.Download();

			LogoTab10SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "design_branding", "10c_right.png" }));
			await LogoTab10SubCRightFile.Download();
			LogoTab10SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "design_branding", "10c_footer.png" }));
			await LogoTab10SubCFooterFile.Download();

			LogoTab10SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "design_branding", "10d_right.png" }));
			await LogoTab10SubDRightFile.Download();
			LogoTab10SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "design_branding", "10d_footer.png" }));
			await LogoTab10SubDFooterFile.Download();
			#endregion

			#region Tab 11
			LogoTab11SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "design_branding", "11a_right.png" }));
			await LogoTab11SubARightFile.Download();
			LogoTab11SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "design_branding", "11a_footer.png" }));
			await LogoTab11SubAFooterFile.Download();

			LogoTab11SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "design_branding", "11b_right.png" }));
			await LogoTab11SubBRightFile.Download();
			LogoTab11SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "design_branding", "11b_footer.png" }));
			await LogoTab11SubBFooterFile.Download();

			LogoTab11SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "design_branding", "11c_right.png" }));
			await LogoTab11SubCRightFile.Download();
			LogoTab11SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "design_branding", "11c_footer.png" }));
			await LogoTab11SubCFooterFile.Download();
			#endregion
		}
	}
}
