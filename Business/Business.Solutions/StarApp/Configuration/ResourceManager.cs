﻿using Asa.Common.Core.Extensions;
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

		public void Init(StorageDirectory dataFolder)
		{
			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			DataCoverPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "data", "1a.xml" }));

			DataCNAPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "data", "2a.xml" }));
			DataCNAPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "data", "2b.xml" }));

			DataFishingPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "data", "3a.xml" }));
			DataFishingPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "data", "3b.xml" }));
			DataFishingPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "data", "3c.xml" }));

			DataCustomerPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "data", "4a.xml" }));
			DataCustomerPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "data", "4b.xml" }));
			DataCustomerPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "data", "4c.xml" }));

			DataSharePartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "data", "5a.xml" }));
			DataSharePartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "data", "5b.xml" }));
			DataSharePartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "data", "5c.xml" }));
			DataSharePartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "data", "5d.xml" }));
			DataSharePartEFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "data", "5e.xml" }));

			DataROIPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "data", "6a.xml" }));
			DataROIPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "data", "6b.xml" }));
			DataROIPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "data", "6c.xml" }));
			DataROIPartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "data", "6d.xml" }));

			DataMarketPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "data", "7a.xml" }));
			DataMarketPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "data", "7b.xml" }));
			DataMarketPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "data", "7c.xml" }));

			DataVideoPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "data", "8a.xml" }));
			DataVideoPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "data", "8b.xml" }));
			DataVideoPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "data", "8c.xml" }));
			DataVideoPartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "data", "8d.xml" }));

			DataAudiencePartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "data", "9a.xml" }));
			DataAudiencePartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "data", "9b.xml" }));
			DataAudiencePartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "data", "9c.xml" }));

			DataSolutionPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "data", "10a.xml" }));
			DataSolutionPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "data", "10b.xml" }));
			DataSolutionPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "data", "10c.xml" }));
			DataSolutionPartDFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "data", "10d.xml" }));

			DataClosersPartAFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "data", "11a.xml" }));
			DataClosersPartBFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "data", "11b.xml" }));
			DataClosersPartCFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "data", "11c.xml" }));

			#region Tab 1
			LogoTab1SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "design_branding", "1a_right.png" }));
			LogoTab1SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "design_branding", "1a_footer.png" }));
			#endregion

			#region Tab 2
			LogoTab2SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "design_branding", "2a_right.png" }));
			LogoTab2SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "design_branding", "2a_footer.png" }));

			LogoTab2SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "design_branding", "2b_right.png" }));
			LogoTab2SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "design_branding", "2b_footer.png" }));
			#endregion

			#region Tab 3
			LogoTab3SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "design_branding", "3a_right.png" }));
			LogoTab3SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "design_branding", "3a_footer.png" }));

			LogoTab3SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "design_branding", "3b_right.png" }));
			LogoTab3SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "design_branding", "3b_footer.png" }));

			LogoTab3SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "design_branding", "3c_right.png" }));
			LogoTab3SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "design_branding", "3c_footer.png" }));
			#endregion

			#region Tab 4
			LogoTab4SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "design_branding", "4a_right.png" }));
			LogoTab4SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "design_branding", "4a_footer.png" }));

			LogoTab4SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "design_branding", "4b_right.png" }));
			LogoTab4SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "design_branding", "4b_footer.png" }));

			LogoTab4SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "design_branding", "4c_right.png" }));
			LogoTab4SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "design_branding", "4c_footer.png" }));
			#endregion

			#region Tab 5
			LogoTab5SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "design_branding", "5a_right.png" }));
			LogoTab5SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "design_branding", "5a_footer.png" }));

			LogoTab5SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "design_branding", "5b_right.png" }));
			LogoTab5SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "design_branding", "5b_footer.png" }));

			LogoTab5SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "design_branding", "5c_right.png" }));
			LogoTab5SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "design_branding", "5c_footer.png" }));

			LogoTab5SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "design_branding", "5d_right.png" }));
			LogoTab5SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "design_branding", "5d_footer.png" }));

			LogoTab5SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "design_branding", "5e_right.png" }));
			LogoTab5SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "design_branding", "5e_footer.png" }));
			#endregion

			#region Tab 6
			LogoTab6SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "design_branding", "6a_right.png" }));
			LogoTab6SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "design_branding", "6a_footer.png" }));

			LogoTab6SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "design_branding", "6b_right.png" }));
			LogoTab6SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "design_branding", "6b_footer.png" }));

			LogoTab6SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "design_branding", "6c_right.png" }));
			LogoTab6SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "design_branding", "6c_footer.png" }));

			LogoTab6SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "design_branding", "6d_right.png" }));
			LogoTab6SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "design_branding", "6d_footer.png" }));
			#endregion

			#region Tab 7
			LogoTab7SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "design_branding", "7a_right.png" }));
			LogoTab7SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "design_branding", "7a_footer.png" }));

			LogoTab7SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "design_branding", "7b_right.png" }));
			LogoTab7SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "design_branding", "7b_footer.png" }));

			LogoTab7SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "design_branding", "7c_right.png" }));
			LogoTab7SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "design_branding", "7c_footer.png" }));
			#endregion

			#region Tab 8
			LogoTab8SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "design_branding", "8a_right.png" }));
			LogoTab8SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "design_branding", "8a_footer.png" }));

			LogoTab8SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "design_branding", "8b_right.png" }));
			LogoTab8SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "design_branding", "8b_footer.png" }));

			LogoTab8SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "design_branding", "8c_right.png" }));
			LogoTab8SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "design_branding", "8c_footer.png" }));

			LogoTab8SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "design_branding", "8d_right.png" }));
			LogoTab8SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "design_branding", "8d_footer.png" }));
			#endregion

			#region Tab 9
			LogoTab9SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "design_branding", "9a_right.png" }));
			LogoTab9SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "design_branding", "9a_footer.png" }));

			LogoTab9SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "design_branding", "9b_right.png" }));
			LogoTab9SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "design_branding", "9b_footer.png" }));

			LogoTab9SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "design_branding", "9c_right.png" }));
			LogoTab9SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "design_branding", "9c_footer.png" }));
			#endregion

			#region Tab 10
			LogoTab10SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "design_branding", "10a_right.png" }));
			LogoTab10SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "design_branding", "10a_footer.png" }));

			LogoTab10SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "design_branding", "10b_right.png" }));
			LogoTab10SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "design_branding", "10b_footer.png" }));

			LogoTab10SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "design_branding", "10c_right.png" }));
			LogoTab10SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "design_branding", "10c_footer.png" }));

			LogoTab10SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "design_branding", "10d_right.png" }));
			LogoTab10SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "design_branding", "10d_footer.png" }));
			#endregion

			#region Tab 11
			LogoTab11SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "design_branding", "11a_right.png" }));
			LogoTab11SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "design_branding", "11a_footer.png" }));

			LogoTab11SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "design_branding", "11b_right.png" }));
			LogoTab11SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "design_branding", "11b_footer.png" }));

			LogoTab11SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "design_branding", "11c_right.png" }));
			LogoTab11SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "design_branding", "11c_footer.png" }));
			#endregion
		}
	}
}
