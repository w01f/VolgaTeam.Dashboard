using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ResourceManager
	{
		public StorageFile SettingsFile { get; private set; }

		public StorageFile DataUsersFile { get; private set; }
		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataTargetCustomersFile { get; private set; }

		#region Cleanslate
		public StorageFile LogoCleanslateHeaderFile { get; private set; }
		public StorageFile LogoCleanslateSplashFile { get; private set; }
		#endregion

		#region Tab 1
		public StorageFile DataCoverPartAFile { get; private set; }

		public StorageFile LogoTab1SubARightFile { get; private set; }
		public StorageFile LogoTab1SubAFooterFile { get; private set; }
		public StorageFile LogoTab1SubURightFile { get; private set; }
		public StorageFile LogoTab1SubUFooterFile { get; private set; }
		public StorageFile LogoTab1SubVRightFile { get; private set; }
		public StorageFile LogoTab1SubVFooterFile { get; private set; }
		public StorageFile LogoTab1SubWRightFile { get; private set; }
		public StorageFile LogoTab1SubWFooterFile { get; private set; }

		public StorageFile ClipartTab1SubA1File { get; private set; }

		public StorageDirectory Tab1PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 2
		public StorageFile DataCNAPartAFile { get; private set; }
		public StorageFile DataCNAPartBFile { get; private set; }

		public StorageFile LogoTab2SubARightFile { get; private set; }
		public StorageFile LogoTab2SubAFooterFile { get; private set; }
		public StorageFile LogoTab2SubBRightFile { get; private set; }
		public StorageFile LogoTab2SubBFooterFile { get; private set; }
		public StorageFile LogoTab2SubURightFile { get; private set; }
		public StorageFile LogoTab2SubUFooterFile { get; private set; }
		public StorageFile LogoTab2SubVRightFile { get; private set; }
		public StorageFile LogoTab2SubVFooterFile { get; private set; }
		public StorageFile LogoTab2SubWRightFile { get; private set; }
		public StorageFile LogoTab2SubWFooterFile { get; private set; }

		public StorageFile ClipartTab2SubA1File { get; private set; }
		public StorageFile ClipartTab2SubB1File { get; private set; }
		public StorageFile ClipartTab2SubB2File { get; private set; }

		public StorageDirectory Tab2PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 3
		public StorageFile DataFishingPartAFile { get; private set; }
		public StorageFile DataFishingPartBFile { get; private set; }
		public StorageFile DataFishingPartCFile { get; private set; }

		public StorageFile LogoTab3SubARightFile { get; private set; }
		public StorageFile LogoTab3SubAFooterFile { get; private set; }
		public StorageFile LogoTab3SubBRightFile { get; private set; }
		public StorageFile LogoTab3SubBFooterFile { get; private set; }
		public StorageFile LogoTab3SubCRightFile { get; private set; }
		public StorageFile LogoTab3SubCFooterFile { get; private set; }
		public StorageFile LogoTab3SubURightFile { get; private set; }
		public StorageFile LogoTab3SubUFooterFile { get; private set; }
		public StorageFile LogoTab3SubVRightFile { get; private set; }
		public StorageFile LogoTab3SubVFooterFile { get; private set; }
		public StorageFile LogoTab3SubWRightFile { get; private set; }
		public StorageFile LogoTab3SubWFooterFile { get; private set; }

		public StorageFile ClipartTab3SubA1File { get; private set; }
		public StorageFile ClipartTab3SubB1File { get; private set; }
		public StorageFile ClipartTab3SubB2File { get; private set; }

		public StorageDirectory Tab3PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 4
		public StorageFile DataCustomerPartAFile { get; private set; }
		public StorageFile DataCustomerPartBFile { get; private set; }
		public StorageFile DataCustomerPartCFile { get; private set; }

		public StorageFile LogoTab4SubARightFile { get; private set; }
		public StorageFile LogoTab4SubAFooterFile { get; private set; }
		public StorageFile LogoTab4SubBRightFile { get; private set; }
		public StorageFile LogoTab4SubBFooterFile { get; private set; }
		public StorageFile LogoTab4SubCRightFile { get; private set; }
		public StorageFile LogoTab4SubCFooterFile { get; private set; }
		public StorageFile LogoTab4SubURightFile { get; private set; }
		public StorageFile LogoTab4SubUFooterFile { get; private set; }
		public StorageFile LogoTab4SubVRightFile { get; private set; }
		public StorageFile LogoTab4SubVFooterFile { get; private set; }
		public StorageFile LogoTab4SubWRightFile { get; private set; }
		public StorageFile LogoTab4SubWFooterFile { get; private set; }

		public StorageFile ClipartTab4SubA1File { get; private set; }
		public StorageFile ClipartTab4SubA2File { get; private set; }
		public StorageFile ClipartTab4SubB1File { get; private set; }
		public StorageFile ClipartTab4SubB2File { get; private set; }

		public StorageDirectory Tab4PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 5
		public StorageFile DataSharePartAFile { get; private set; }
		public StorageFile DataSharePartBFile { get; private set; }
		public StorageFile DataSharePartCFile { get; private set; }
		public StorageFile DataSharePartDFile { get; private set; }
		public StorageFile DataSharePartEFile { get; private set; }

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
		public StorageFile LogoTab5SubURightFile { get; private set; }
		public StorageFile LogoTab5SubUFooterFile { get; private set; }
		public StorageFile LogoTab5SubVRightFile { get; private set; }
		public StorageFile LogoTab5SubVFooterFile { get; private set; }
		public StorageFile LogoTab5SubWRightFile { get; private set; }
		public StorageFile LogoTab5SubWFooterFile { get; private set; }

		public StorageFile ClipartTab5SubA1File { get; private set; }
		public StorageFile ClipartTab5SubA2File { get; private set; }
		public StorageFile ClipartTab5SubA3File { get; private set; }

		public StorageFile ClipartTab5SubB1File { get; private set; }
		public StorageFile ClipartTab5SubB2File { get; private set; }
		public StorageFile ClipartTab5SubB3File { get; private set; }

		public StorageFile ClipartTab5SubC1File { get; private set; }
		public StorageFile ClipartTab5SubC2File { get; private set; }
		public StorageFile ClipartTab5SubC3File { get; private set; }

		public StorageFile ClipartTab5SubD1File { get; private set; }
		public StorageFile ClipartTab5SubD2File { get; private set; }
		public StorageFile ClipartTab5SubD3File { get; private set; }

		public StorageFile ClipartTab5SubE1File { get; private set; }
		public StorageFile ClipartTab5SubE2File { get; private set; }
		public StorageFile ClipartTab5SubE3File { get; private set; }

		public StorageDirectory Tab5PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 6
		public StorageFile DataROIPartAFile { get; private set; }
		public StorageFile DataROIPartBFile { get; private set; }
		public StorageFile DataROIPartCFile { get; private set; }
		public StorageFile DataROIPartDFile { get; private set; }

		public StorageFile LogoTab6SubARightFile { get; private set; }
		public StorageFile LogoTab6SubAFooterFile { get; private set; }
		public StorageFile LogoTab6SubBRightFile { get; private set; }
		public StorageFile LogoTab6SubBFooterFile { get; private set; }
		public StorageFile LogoTab6SubCRightFile { get; private set; }
		public StorageFile LogoTab6SubCFooterFile { get; private set; }
		public StorageFile LogoTab6SubDRightFile { get; private set; }
		public StorageFile LogoTab6SubDFooterFile { get; private set; }
		public StorageFile LogoTab6SubURightFile { get; private set; }
		public StorageFile LogoTab6SubUFooterFile { get; private set; }
		public StorageFile LogoTab6SubVRightFile { get; private set; }
		public StorageFile LogoTab6SubVFooterFile { get; private set; }
		public StorageFile LogoTab6SubWRightFile { get; private set; }
		public StorageFile LogoTab6SubWFooterFile { get; private set; }

		public StorageFile ClipartTab6SubA1File { get; private set; }
		public StorageFile ClipartTab6SubA2File { get; private set; }
		public StorageFile ClipartTab6SubA3File { get; private set; }

		public StorageFile ClipartTab6SubB1File { get; private set; }
		public StorageFile ClipartTab6SubB2File { get; private set; }
		public StorageFile ClipartTab6SubB3File { get; private set; }

		public StorageFile ClipartTab6SubC1File { get; private set; }
		public StorageFile ClipartTab6SubC2File { get; private set; }
		public StorageFile ClipartTab6SubC3File { get; private set; }

		public StorageFile ClipartTab6SubD1File { get; private set; }
		public StorageFile ClipartTab6SubD2File { get; private set; }
		public StorageFile ClipartTab6SubD3File { get; private set; }

		public StorageDirectory Tab6PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 7
		public StorageFile DataMarketPartAFile { get; private set; }
		public StorageFile DataMarketPartBFile { get; private set; }
		public StorageFile DataMarketPartCFile { get; private set; }

		public StorageFile LogoTab7SubARightFile { get; private set; }
		public StorageFile LogoTab7SubAFooterFile { get; private set; }
		public StorageFile LogoTab7SubBRightFile { get; private set; }
		public StorageFile LogoTab7SubBFooterFile { get; private set; }
		public StorageFile LogoTab7SubCRightFile { get; private set; }
		public StorageFile LogoTab7SubCFooterFile { get; private set; }
		public StorageFile LogoTab7SubURightFile { get; private set; }
		public StorageFile LogoTab7SubUFooterFile { get; private set; }
		public StorageFile LogoTab7SubVRightFile { get; private set; }
		public StorageFile LogoTab7SubVFooterFile { get; private set; }
		public StorageFile LogoTab7SubWRightFile { get; private set; }
		public StorageFile LogoTab7SubWFooterFile { get; private set; }

		public StorageFile ClipartTab7SubA1File { get; private set; }

		public StorageFile ClipartTab7SubB1File { get; private set; }
		public StorageFile ClipartTab7SubB2File { get; private set; }
		public StorageFile ClipartTab7SubB3File { get; private set; }
		public StorageFile ClipartTab7SubB4File { get; private set; }
		public StorageFile ClipartTab7SubB5File { get; private set; }

		public StorageFile ClipartTab7SubC1File { get; private set; }
		public StorageFile ClipartTab7SubC2File { get; private set; }
		public StorageFile ClipartTab7SubC3File { get; private set; }
		public StorageFile ClipartTab7SubC4File { get; private set; }

		public StorageDirectory Tab7PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 8
		public StorageFile DataVideoPartAFile { get; private set; }
		public StorageFile DataVideoPartBFile { get; private set; }
		public StorageFile DataVideoPartCFile { get; private set; }
		public StorageFile DataVideoPartDFile { get; private set; }

		public StorageFile LogoTab8SubARightFile { get; private set; }
		public StorageFile LogoTab8SubAFooterFile { get; private set; }
		public StorageFile LogoTab8SubBRightFile { get; private set; }
		public StorageFile LogoTab8SubBFooterFile { get; private set; }
		public StorageFile LogoTab8SubCRightFile { get; private set; }
		public StorageFile LogoTab8SubCFooterFile { get; private set; }
		public StorageFile LogoTab8SubDRightFile { get; private set; }
		public StorageFile LogoTab8SubDFooterFile { get; private set; }
		public StorageFile LogoTab8SubURightFile { get; private set; }
		public StorageFile LogoTab8SubUFooterFile { get; private set; }
		public StorageFile LogoTab8SubVRightFile { get; private set; }
		public StorageFile LogoTab8SubVFooterFile { get; private set; }
		public StorageFile LogoTab8SubWRightFile { get; private set; }
		public StorageFile LogoTab8SubWFooterFile { get; private set; }

		public StorageFile ClipartTab8SubA1File { get; private set; }
		public StorageFile ClipartTab8SubB1File { get; private set; }
		public StorageFile ClipartTab8SubC1File { get; private set; }
		public StorageFile ClipartTab8SubD1File { get; private set; }

		public StorageDirectory Tab8PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 9
		public StorageFile DataAudiencePartAFile { get; private set; }
		public StorageFile DataAudiencePartBFile { get; private set; }
		public StorageFile DataAudiencePartCFile { get; private set; }

		public StorageFile LogoTab9SubARightFile { get; private set; }
		public StorageFile LogoTab9SubAFooterFile { get; private set; }
		public StorageFile LogoTab9SubBRightFile { get; private set; }
		public StorageFile LogoTab9SubBFooterFile { get; private set; }
		public StorageFile LogoTab9SubCRightFile { get; private set; }
		public StorageFile LogoTab9SubCFooterFile { get; private set; }
		public StorageFile LogoTab9SubURightFile { get; private set; }
		public StorageFile LogoTab9SubUFooterFile { get; private set; }
		public StorageFile LogoTab9SubVRightFile { get; private set; }
		public StorageFile LogoTab9SubVFooterFile { get; private set; }
		public StorageFile LogoTab9SubWRightFile { get; private set; }
		public StorageFile LogoTab9SubWFooterFile { get; private set; }

		public StorageFile ClipartTab9SubA1File { get; private set; }
		public StorageFile ClipartTab9SubA2File { get; private set; }

		public StorageFile ClipartTab9SubB1File { get; private set; }
		public StorageFile ClipartTab9SubB2File { get; private set; }
		public StorageFile ClipartTab9SubB3File { get; private set; }

		public StorageFile ClipartTab9SubC1File { get; private set; }
		public StorageFile ClipartTab9SubC2File { get; private set; }
		public StorageFile ClipartTab9SubC3File { get; private set; }
		public StorageFile ClipartTab9SubC4File { get; private set; }

		public StorageDirectory Tab9PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 10
		public StorageFile DataSolutionPartAFile { get; private set; }
		public StorageFile DataSolutionPartBFile { get; private set; }
		public StorageFile DataSolutionPartCFile { get; private set; }
		public StorageFile DataSolutionPartDFile { get; private set; }

		public StorageFile LogoTab10SubARightFile { get; private set; }
		public StorageFile LogoTab10SubAFooterFile { get; private set; }
		public StorageFile LogoTab10SubBRightFile { get; private set; }
		public StorageFile LogoTab10SubBFooterFile { get; private set; }
		public StorageFile LogoTab10SubCRightFile { get; private set; }
		public StorageFile LogoTab10SubCFooterFile { get; private set; }
		public StorageFile LogoTab10SubDRightFile { get; private set; }
		public StorageFile LogoTab10SubDFooterFile { get; private set; }
		public StorageFile LogoTab10SubURightFile { get; private set; }
		public StorageFile LogoTab10SubUFooterFile { get; private set; }
		public StorageFile LogoTab10SubVRightFile { get; private set; }
		public StorageFile LogoTab10SubVFooterFile { get; private set; }
		public StorageFile LogoTab10SubWRightFile { get; private set; }
		public StorageFile LogoTab10SubWFooterFile { get; private set; }

		public StorageFile ClipartTab10SubA1File { get; private set; }
		public StorageFile ClipartTab10SubB1File { get; private set; }
		public StorageFile ClipartTab10SubB2File { get; private set; }
		public StorageFile ClipartTab10SubB3File { get; private set; }
		public StorageFile ClipartTab10SubC1File { get; private set; }
		public StorageFile ClipartTab10SubC2File { get; private set; }
		public StorageFile ClipartTab10SubD1File { get; private set; }

		public StorageDirectory Tab10PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 11
		public StorageFile DataClosersPartAFile { get; private set; }
		public StorageFile DataClosersPartBFile { get; private set; }
		public StorageFile DataClosersPartCFile { get; private set; }

		public StorageFile LogoTab11SubARightFile { get; private set; }
		public StorageFile LogoTab11SubAFooterFile { get; private set; }
		public StorageFile LogoTab11SubBRightFile { get; private set; }
		public StorageFile LogoTab11SubBFooterFile { get; private set; }
		public StorageFile LogoTab11SubCRightFile { get; private set; }
		public StorageFile LogoTab11SubCFooterFile { get; private set; }
		public StorageFile LogoTab11SubURightFile { get; private set; }
		public StorageFile LogoTab11SubUFooterFile { get; private set; }
		public StorageFile LogoTab11SubVRightFile { get; private set; }
		public StorageFile LogoTab11SubVFooterFile { get; private set; }
		public StorageFile LogoTab11SubWRightFile { get; private set; }
		public StorageFile LogoTab11SubWFooterFile { get; private set; }

		public StorageFile ClipartTab11SubA1File { get; private set; }
		public StorageFile ClipartTab11SubA2File { get; private set; }
		public StorageFile ClipartTab11SubB1File { get; private set; }
		public StorageFile ClipartTab11SubB2File { get; private set; }
		public StorageFile ClipartTab11SubC1File { get; private set; }
		public StorageFile ClipartTab11SubC2File { get; private set; }

		public StorageDirectory Tab11PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartWSlidesFolder { get; private set; }
		#endregion

		public void Init(StorageDirectory dataFolder)
		{
			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			DataUsersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Users.xml"));
			DataClientGoalsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			DataTargetCustomersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Target Customer.xml"));

			#region Cleanslate
			LogoCleanslateHeaderFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_star", "design_branding", "tab_1_header.png" }));
			LogoCleanslateSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_star", "design_branding", "tab_1.png" }));
			#endregion

			#region Tab 1
			DataCoverPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP01A.xml"));

			LogoTab1SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "design_branding", "1a_right.png" }));
			LogoTab1SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "design_branding", "1a_footer.png" }));
			LogoTab1SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_u", "design_branding", "1u_right.png" }));
			LogoTab1SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_u", "design_branding", "1u_footer.png" }));
			LogoTab1SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_v", "design_branding", "1v_right.png" }));
			LogoTab1SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_v", "design_branding", "1v_footer.png" }));
			LogoTab1SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_w", "design_branding", "1w_right.png" }));
			LogoTab1SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_w", "design_branding", "1w_footer.png" }));

			ClipartTab1SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_a", "placeholders", "CP01AClipart1.png" }));

			Tab1PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_u" }));
			Tab1PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_v" }));
			Tab1PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_w" }));
			#endregion

			#region Tab 2
			DataCNAPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP02A.xml"));
			DataCNAPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP02B.xml"));

			LogoTab2SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "design_branding", "2a_right.png" }));
			LogoTab2SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "design_branding", "2a_footer.png" }));

			LogoTab2SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "design_branding", "2b_right.png" }));
			LogoTab2SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "design_branding", "2b_footer.png" }));
			LogoTab2SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_u", "design_branding", "2u_right.png" }));
			LogoTab2SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_u", "design_branding", "2u_footer.png" }));
			LogoTab2SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_v", "design_branding", "2v_right.png" }));
			LogoTab2SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_v", "design_branding", "2v_footer.png" }));
			LogoTab2SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_w", "design_branding", "2w_right.png" }));
			LogoTab2SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_w", "design_branding", "2w_footer.png" }));

			ClipartTab2SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_a", "placeholders", "CP02AClipart1.png" }));

			ClipartTab2SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "placeholders", "CP02BClipart1.png" }));
			ClipartTab2SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_b", "placeholders", "CP02BClipart2.png" }));

			Tab2PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_u" }));
			Tab2PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_v" }));
			Tab2PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_w" }));
			#endregion

			#region Tab 3
			DataFishingPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03A.xml"));
			DataFishingPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03B.xml"));
			DataFishingPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03C.xml"));

			LogoTab3SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "design_branding", "3a_right.png" }));
			LogoTab3SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "design_branding", "3a_footer.png" }));
			LogoTab3SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "design_branding", "3b_right.png" }));
			LogoTab3SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "design_branding", "3b_footer.png" }));
			LogoTab3SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "design_branding", "3c_right.png" }));
			LogoTab3SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_c", "design_branding", "3c_footer.png" }));
			LogoTab3SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_u", "design_branding", "3u_right.png" }));
			LogoTab3SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_u", "design_branding", "3u_footer.png" }));
			LogoTab3SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_v", "design_branding", "3v_right.png" }));
			LogoTab3SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_v", "design_branding", "3v_footer.png" }));
			LogoTab3SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_w", "design_branding", "3w_right.png" }));
			LogoTab3SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_w", "design_branding", "3w_footer.png" }));

			ClipartTab3SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_a", "placeholders", "CP03AClipart1.png" }));

			ClipartTab3SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "placeholders", "CP03BClipart1.png" }));
			ClipartTab3SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_b", "placeholders", "CP03BClipart2.png" }));

			Tab3PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_u" }));
			Tab3PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_v" }));
			Tab3PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_w" }));
			#endregion

			#region Tab 4
			DataCustomerPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04A.xml"));
			DataCustomerPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04B.xml"));
			DataCustomerPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04C.xml"));

			LogoTab4SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "design_branding", "4a_right.png" }));
			LogoTab4SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "design_branding", "4a_footer.png" }));
			LogoTab4SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "design_branding", "4b_right.png" }));
			LogoTab4SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "design_branding", "4b_footer.png" }));
			LogoTab4SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "design_branding", "4c_right.png" }));
			LogoTab4SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_c", "design_branding", "4c_footer.png" }));
			LogoTab4SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_u", "design_branding", "4u_right.png" }));
			LogoTab4SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_u", "design_branding", "4u_footer.png" }));
			LogoTab4SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_v", "design_branding", "4v_right.png" }));
			LogoTab4SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_v", "design_branding", "4v_footer.png" }));
			LogoTab4SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_w", "design_branding", "4w_right.png" }));
			LogoTab4SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_w", "design_branding", "4w_footer.png" }));

			ClipartTab4SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "placeholders", "CP04AClipart1.png" }));
			ClipartTab4SubA2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_a", "placeholders", "CP04AClipart2.png" }));

			ClipartTab4SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "placeholders", "CP04BClipart1.png" }));
			ClipartTab4SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_b", "placeholders", "CP04BClipart2.png" }));

			Tab4PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_u" }));
			Tab4PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_v" }));
			Tab4PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_w" }));
			#endregion

			#region Tab 5
			DataSharePartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05A.xml"));
			DataSharePartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05B.xml"));
			DataSharePartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05C.xml"));
			DataSharePartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05D.xml"));
			DataSharePartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05E.xml"));

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
			LogoTab5SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_u", "design_branding", "5u_right.png" }));
			LogoTab5SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_u", "design_branding", "5u_footer.png" }));
			LogoTab5SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_v", "design_branding", "5v_right.png" }));
			LogoTab5SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_v", "design_branding", "5v_footer.png" }));
			LogoTab5SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_w", "design_branding", "5w_right.png" }));
			LogoTab5SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_w", "design_branding", "5w_footer.png" }));

			ClipartTab5SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "placeholders", "CP05AClipart1.png" }));
			ClipartTab5SubA2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "placeholders", "CP05AClipart2.png" }));
			ClipartTab5SubA3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_a", "placeholders", "CP05AClipart3.png" }));

			ClipartTab5SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "placeholders", "CP05BClipart1.png" }));
			ClipartTab5SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "placeholders", "CP05BClipart2.png" }));
			ClipartTab5SubB3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_b", "placeholders", "CP05BClipart3.png" }));

			ClipartTab5SubC1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "placeholders", "CP05CClipart1.png" }));
			ClipartTab5SubC2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "placeholders", "CP05CClipart2.png" }));
			ClipartTab5SubC3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_c", "placeholders", "CP05CClipart3.png" }));

			ClipartTab5SubD1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "placeholders", "CP05DClipart1.png" }));
			ClipartTab5SubD2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "placeholders", "CP05DClipart2.png" }));
			ClipartTab5SubD3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_d", "placeholders", "CP05DClipart3.png" }));

			ClipartTab5SubE1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "placeholders", "CP05EClipart1.png" }));
			ClipartTab5SubE2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "placeholders", "CP05EClipart2.png" }));
			ClipartTab5SubE3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_e", "placeholders", "CP05EClipart3.png" }));

			Tab5PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_u" }));
			Tab5PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_v" }));
			Tab5PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_w" }));
			#endregion

			#region Tab 6
			DataROIPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06A.xml"));
			DataROIPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06B.xml"));
			DataROIPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06C.xml"));
			DataROIPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06D.xml"));

			LogoTab6SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "design_branding", "6a_right.png" }));
			LogoTab6SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "design_branding", "6a_footer.png" }));
			LogoTab6SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "design_branding", "6b_right.png" }));
			LogoTab6SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "design_branding", "6b_footer.png" }));
			LogoTab6SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "design_branding", "6c_right.png" }));
			LogoTab6SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "design_branding", "6c_footer.png" }));
			LogoTab6SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "design_branding", "6d_right.png" }));
			LogoTab6SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "design_branding", "6d_footer.png" }));
			LogoTab6SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_u", "design_branding", "6u_right.png" }));
			LogoTab6SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_u", "design_branding", "6u_footer.png" }));
			LogoTab6SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_v", "design_branding", "6v_right.png" }));
			LogoTab6SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_v", "design_branding", "6v_footer.png" }));
			LogoTab6SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_w", "design_branding", "6w_right.png" }));
			LogoTab6SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_w", "design_branding", "6w_footer.png" }));

			ClipartTab6SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "placeholders", "CP06AClipart1.png" }));
			ClipartTab6SubA2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "placeholders", "CP06AClipart2.png" }));
			ClipartTab6SubA3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_a", "placeholders", "CP06AClipart3.png" }));

			ClipartTab6SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "placeholders", "CP06BClipart1.png" }));
			ClipartTab6SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "placeholders", "CP06BClipart2.png" }));
			ClipartTab6SubB3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_b", "placeholders", "CP06BClipart3.png" }));

			ClipartTab6SubC1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "placeholders", "CP06CClipart1.png" }));
			ClipartTab6SubC2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "placeholders", "CP06CClipart2.png" }));
			ClipartTab6SubC3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_c", "placeholders", "CP06CClipart3.png" }));

			ClipartTab6SubD1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "placeholders", "CP06DClipart1.png" }));
			ClipartTab6SubD2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "placeholders", "CP06DClipart2.png" }));
			ClipartTab6SubD3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_d", "placeholders", "CP06DClipart3.png" }));

			Tab6PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_u" }));
			Tab6PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_v" }));
			Tab6PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_w" }));
			#endregion

			#region Tab 7
			DataMarketPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07A.xml"));
			DataMarketPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07B.xml"));
			DataMarketPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07C.xml"));

			LogoTab7SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "design_branding", "7a_right.png" }));
			LogoTab7SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "design_branding", "7a_footer.png" }));
			LogoTab7SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "design_branding", "7b_right.png" }));
			LogoTab7SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "design_branding", "7b_footer.png" }));
			LogoTab7SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "design_branding", "7c_right.png" }));
			LogoTab7SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "design_branding", "7c_footer.png" }));
			LogoTab7SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_u", "design_branding", "7u_right.png" }));
			LogoTab7SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_u", "design_branding", "7u_footer.png" }));
			LogoTab7SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_v", "design_branding", "7v_right.png" }));
			LogoTab7SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_v", "design_branding", "7v_footer.png" }));
			LogoTab7SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_w", "design_branding", "7w_right.png" }));
			LogoTab7SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_w", "design_branding", "7w_footer.png" }));

			ClipartTab7SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_a", "placeholders", "CP07AClipart1.png" }));

			ClipartTab7SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "placeholders", "CP07BClipart1.png" }));
			ClipartTab7SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "placeholders", "CP07BClipart2.png" }));
			ClipartTab7SubB3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "placeholders", "CP07BClipart3.png" }));
			ClipartTab7SubB4File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "placeholders", "CP07BClipart4.png" }));
			ClipartTab7SubB5File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_b", "placeholders", "CP07BClipart5.png" }));

			ClipartTab7SubC1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "placeholders", "CP07CClipart1.png" }));
			ClipartTab7SubC2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "placeholders", "CP07CClipart2.png" }));
			ClipartTab7SubC3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "placeholders", "CP07CClipart3.png" }));
			ClipartTab7SubC4File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_c", "placeholders", "CP07CClipart4.png" }));

			Tab7PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_u" }));
			Tab7PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_v" }));
			Tab7PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_w" }));
			#endregion

			#region Tab 8
			DataVideoPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08A.xml"));
			DataVideoPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08B.xml"));
			DataVideoPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08C.xml"));
			DataVideoPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08D.xml"));

			LogoTab8SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "design_branding", "8a_right.png" }));
			LogoTab8SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "design_branding", "8a_footer.png" }));
			LogoTab8SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "design_branding", "8b_right.png" }));
			LogoTab8SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "design_branding", "8b_footer.png" }));
			LogoTab8SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "design_branding", "8c_right.png" }));
			LogoTab8SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "design_branding", "8c_footer.png" }));
			LogoTab8SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "design_branding", "8d_right.png" }));
			LogoTab8SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "design_branding", "8d_footer.png" }));
			LogoTab8SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_u", "design_branding", "8u_right.png" }));
			LogoTab8SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_u", "design_branding", "8u_footer.png" }));
			LogoTab8SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_v", "design_branding", "8v_right.png" }));
			LogoTab8SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_v", "design_branding", "8v_footer.png" }));
			LogoTab8SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_w", "design_branding", "8w_right.png" }));
			LogoTab8SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_w", "design_branding", "8w_footer.png" }));

			ClipartTab8SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_a", "placeholders", "CP08AClipart1.png" }));

			ClipartTab8SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_b", "placeholders", "CP08BClipart1.png" }));

			ClipartTab8SubC1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_c", "placeholders", "CP08CClipart1.png" }));

			ClipartTab8SubD1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_d", "placeholders", "CP08DClipart1.png" }));

			Tab8PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_u" }));
			Tab8PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_v" }));
			Tab8PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_w" }));
			#endregion

			#region Tab 9
			DataAudiencePartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09A.xml"));
			DataAudiencePartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09B.xml"));
			DataAudiencePartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09C.xml"));

			LogoTab9SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "design_branding", "9a_right.png" }));
			LogoTab9SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "design_branding", "9a_footer.png" }));
			LogoTab9SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "design_branding", "9b_right.png" }));
			LogoTab9SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "design_branding", "9b_footer.png" }));
			LogoTab9SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "design_branding", "9c_right.png" }));
			LogoTab9SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "design_branding", "9c_footer.png" }));
			LogoTab9SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_u", "design_branding", "9u_right.png" }));
			LogoTab9SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_u", "design_branding", "9u_footer.png" }));
			LogoTab9SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_v", "design_branding", "9v_right.png" }));
			LogoTab9SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_v", "design_branding", "9v_footer.png" }));
			LogoTab9SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_w", "design_branding", "9w_right.png" }));
			LogoTab9SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_w", "design_branding", "9w_footer.png" }));

			ClipartTab9SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "placeholders", "CP09AClipart1.png" }));
			ClipartTab9SubA2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_a", "placeholders", "CP09AClipart2.png" }));

			ClipartTab9SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "placeholders", "CP09BClipart1.png" }));
			ClipartTab9SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "placeholders", "CP09BClipart2.png" }));
			ClipartTab9SubB3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_b", "placeholders", "CP09BClipart3.png" }));

			ClipartTab9SubC1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "placeholders", "CP09CClipart1.png" }));
			ClipartTab9SubC2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "placeholders", "CP09CClipart2.png" }));
			ClipartTab9SubC3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "placeholders", "CP09CClipart3.png" }));
			ClipartTab9SubC4File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_c", "placeholders", "CP09CClipart4.png" }));

			Tab9PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_u" }));
			Tab9PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_v" }));
			Tab9PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_w" }));
			#endregion

			#region Tab 10
			DataSolutionPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10A.xml"));
			DataSolutionPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10B.xml"));
			DataSolutionPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10C.xml"));
			DataSolutionPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10D.xml"));

			LogoTab10SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "design_branding", "10a_right.png" }));
			LogoTab10SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "design_branding", "10a_footer.png" }));
			LogoTab10SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "design_branding", "10b_right.png" }));
			LogoTab10SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "design_branding", "10b_footer.png" }));
			LogoTab10SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "design_branding", "10c_right.png" }));
			LogoTab10SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "design_branding", "10c_footer.png" }));
			LogoTab10SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "design_branding", "10d_right.png" }));
			LogoTab10SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "design_branding", "10d_footer.png" }));
			LogoTab10SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_u", "design_branding", "10u_right.png" }));
			LogoTab10SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_u", "design_branding", "10u_footer.png" }));
			LogoTab10SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_v", "design_branding", "10v_right.png" }));
			LogoTab10SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_v", "design_branding", "10v_footer.png" }));
			LogoTab10SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_w", "design_branding", "10w_right.png" }));
			LogoTab10SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_w", "design_branding", "10w_footer.png" }));

			ClipartTab10SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_a", "placeholders", "CP10AClipart1.png" }));

			ClipartTab10SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "placeholders", "CP10BClipart1.png" }));
			ClipartTab10SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "placeholders", "CP10BClipart2.png" }));
			ClipartTab10SubB3File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_b", "placeholders", "CP10BClipart3.png" }));

			ClipartTab10SubC1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "placeholders", "CP10CClipart1.png" }));
			ClipartTab10SubC2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_c", "placeholders", "CP10CClipart2.png" }));

			ClipartTab10SubD1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_d", "placeholders", "CP10DClipart1.png" }));

			Tab10PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_u" }));
			Tab10PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_v" }));
			Tab10PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_w" }));
			#endregion

			#region Tab 11
			DataClosersPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11A.xml"));
			DataClosersPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11B.xml"));
			DataClosersPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11C.xml"));

			LogoTab11SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "design_branding", "11a_right.png" }));
			LogoTab11SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "design_branding", "11a_footer.png" }));
			LogoTab11SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "design_branding", "11b_right.png" }));
			LogoTab11SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "design_branding", "11b_footer.png" }));
			LogoTab11SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "design_branding", "11c_right.png" }));
			LogoTab11SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "design_branding", "11c_footer.png" }));
			LogoTab11SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_u", "design_branding", "11u_right.png" }));
			LogoTab11SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_u", "design_branding", "11u_footer.png" }));
			LogoTab11SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_v", "design_branding", "11v_right.png" }));
			LogoTab11SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_v", "design_branding", "11v_footer.png" }));
			LogoTab11SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_w", "design_branding", "11w_right.png" }));
			LogoTab11SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_w", "design_branding", "11w_footer.png" }));

			ClipartTab11SubA1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "place_holders", "CP11AClipart1.png" }));
			ClipartTab11SubA2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_a", "place_holders", "CP11AClipart2.png" }));

			ClipartTab11SubB1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "place_holders", "CP11BClipart1.png" }));
			ClipartTab11SubB2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_b", "place_holders", "CP11BClipart2.png" }));

			ClipartTab11SubC1File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "place_holders", "CP11CClipart1.png" }));
			ClipartTab11SubC2File = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_c", "place_holders", "CP11CClipart2.png" }));

			Tab11PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_u" }));
			Tab11PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_v" }));
			Tab11PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_w" }));
			#endregion
		}
	}
}
