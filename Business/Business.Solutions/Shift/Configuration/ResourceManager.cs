using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ResourceManager
	{
		public StorageFile SettingsFile { get; private set; }

		#region Cleanslate
		public StorageFile LogoCleanslateHeaderFile { get; private set; }
		public StorageFile LogoCleanslateSplashFile { get; private set; }
		#endregion

		#region Tab 1
		public StorageFile LogoTab1SubARightFile { get; private set; }
		public StorageFile LogoTab1SubAFooterFile { get; private set; }
		public StorageFile LogoTab1SubBRightFile { get; private set; }
		public StorageFile LogoTab1SubBFooterFile { get; private set; }
		public StorageFile LogoTab1SubCRightFile { get; private set; }
		public StorageFile LogoTab1SubCFooterFile { get; private set; }
		public StorageFile LogoTab1SubDRightFile { get; private set; }
		public StorageFile LogoTab1SubDFooterFile { get; private set; }
		public StorageFile LogoTab1SubERightFile { get; private set; }
		public StorageFile LogoTab1SubEFooterFile { get; private set; }
		public StorageFile LogoTab1SubFRightFile { get; private set; }
		public StorageFile LogoTab1SubFFooterFile { get; private set; }
		public StorageFile LogoTab1SubGRightFile { get; private set; }
		public StorageFile LogoTab1SubGFooterFile { get; private set; }
		public StorageFile LogoTab1SubHRightFile { get; private set; }
		public StorageFile LogoTab1SubHFooterFile { get; private set; }
		public StorageFile LogoTab1SubIRightFile { get; private set; }
		public StorageFile LogoTab1SubIFooterFile { get; private set; }
		public StorageFile LogoTab1SubJRightFile { get; private set; }
		public StorageFile LogoTab1SubJFooterFile { get; private set; }
		public StorageFile LogoTab1SubURightFile { get; private set; }
		public StorageFile LogoTab1SubUFooterFile { get; private set; }
		public StorageFile LogoTab1SubVRightFile { get; private set; }
		public StorageFile LogoTab1SubVFooterFile { get; private set; }
		public StorageFile LogoTab1SubWRightFile { get; private set; }
		public StorageFile LogoTab1SubWFooterFile { get; private set; }

		public StorageDirectory Tab1PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 2
		public StorageFile LogoTab2SubARightFile { get; private set; }
		public StorageFile LogoTab2SubAFooterFile { get; private set; }
		public StorageFile LogoTab2SubBRightFile { get; private set; }
		public StorageFile LogoTab2SubBFooterFile { get; private set; }
		public StorageFile LogoTab2SubCRightFile { get; private set; }
		public StorageFile LogoTab2SubCFooterFile { get; private set; }
		public StorageFile LogoTab2SubDRightFile { get; private set; }
		public StorageFile LogoTab2SubDFooterFile { get; private set; }
		public StorageFile LogoTab2SubERightFile { get; private set; }
		public StorageFile LogoTab2SubEFooterFile { get; private set; }
		public StorageFile LogoTab2SubFRightFile { get; private set; }
		public StorageFile LogoTab2SubFFooterFile { get; private set; }
		public StorageFile LogoTab2SubGRightFile { get; private set; }
		public StorageFile LogoTab2SubGFooterFile { get; private set; }
		public StorageFile LogoTab2SubHRightFile { get; private set; }
		public StorageFile LogoTab2SubHFooterFile { get; private set; }
		public StorageFile LogoTab2SubIRightFile { get; private set; }
		public StorageFile LogoTab2SubIFooterFile { get; private set; }
		public StorageFile LogoTab2SubJRightFile { get; private set; }
		public StorageFile LogoTab2SubJFooterFile { get; private set; }
		public StorageFile LogoTab2SubURightFile { get; private set; }
		public StorageFile LogoTab2SubUFooterFile { get; private set; }
		public StorageFile LogoTab2SubVRightFile { get; private set; }
		public StorageFile LogoTab2SubVFooterFile { get; private set; }
		public StorageFile LogoTab2SubWRightFile { get; private set; }
		public StorageFile LogoTab2SubWFooterFile { get; private set; }

		public StorageDirectory Tab2PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 3
		public StorageFile LogoTab3SubARightFile { get; private set; }
		public StorageFile LogoTab3SubAFooterFile { get; private set; }
		public StorageFile LogoTab3SubBRightFile { get; private set; }
		public StorageFile LogoTab3SubBFooterFile { get; private set; }
		public StorageFile LogoTab3SubCRightFile { get; private set; }
		public StorageFile LogoTab3SubCFooterFile { get; private set; }
		public StorageFile LogoTab3SubDRightFile { get; private set; }
		public StorageFile LogoTab3SubDFooterFile { get; private set; }
		public StorageFile LogoTab3SubERightFile { get; private set; }
		public StorageFile LogoTab3SubEFooterFile { get; private set; }
		public StorageFile LogoTab3SubFRightFile { get; private set; }
		public StorageFile LogoTab3SubFFooterFile { get; private set; }
		public StorageFile LogoTab3SubGRightFile { get; private set; }
		public StorageFile LogoTab3SubGFooterFile { get; private set; }
		public StorageFile LogoTab3SubHRightFile { get; private set; }
		public StorageFile LogoTab3SubHFooterFile { get; private set; }
		public StorageFile LogoTab3SubIRightFile { get; private set; }
		public StorageFile LogoTab3SubIFooterFile { get; private set; }
		public StorageFile LogoTab3SubJRightFile { get; private set; }
		public StorageFile LogoTab3SubJFooterFile { get; private set; }
		public StorageFile LogoTab3SubURightFile { get; private set; }
		public StorageFile LogoTab3SubUFooterFile { get; private set; }
		public StorageFile LogoTab3SubVRightFile { get; private set; }
		public StorageFile LogoTab3SubVFooterFile { get; private set; }
		public StorageFile LogoTab3SubWRightFile { get; private set; }
		public StorageFile LogoTab3SubWFooterFile { get; private set; }

		public StorageDirectory Tab3PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 4
		public StorageFile LogoTab4SubARightFile { get; private set; }
		public StorageFile LogoTab4SubAFooterFile { get; private set; }
		public StorageFile LogoTab4SubBRightFile { get; private set; }
		public StorageFile LogoTab4SubBFooterFile { get; private set; }
		public StorageFile LogoTab4SubCRightFile { get; private set; }
		public StorageFile LogoTab4SubCFooterFile { get; private set; }
		public StorageFile LogoTab4SubDRightFile { get; private set; }
		public StorageFile LogoTab4SubDFooterFile { get; private set; }
		public StorageFile LogoTab4SubERightFile { get; private set; }
		public StorageFile LogoTab4SubEFooterFile { get; private set; }
		public StorageFile LogoTab4SubFRightFile { get; private set; }
		public StorageFile LogoTab4SubFFooterFile { get; private set; }
		public StorageFile LogoTab4SubGRightFile { get; private set; }
		public StorageFile LogoTab4SubGFooterFile { get; private set; }
		public StorageFile LogoTab4SubHRightFile { get; private set; }
		public StorageFile LogoTab4SubHFooterFile { get; private set; }
		public StorageFile LogoTab4SubIRightFile { get; private set; }
		public StorageFile LogoTab4SubIFooterFile { get; private set; }
		public StorageFile LogoTab4SubJRightFile { get; private set; }
		public StorageFile LogoTab4SubJFooterFile { get; private set; }
		public StorageFile LogoTab4SubURightFile { get; private set; }
		public StorageFile LogoTab4SubUFooterFile { get; private set; }
		public StorageFile LogoTab4SubVRightFile { get; private set; }
		public StorageFile LogoTab4SubVFooterFile { get; private set; }
		public StorageFile LogoTab4SubWRightFile { get; private set; }
		public StorageFile LogoTab4SubWFooterFile { get; private set; }

		public StorageDirectory Tab4PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 5
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
		public StorageFile LogoTab5SubFRightFile { get; private set; }
		public StorageFile LogoTab5SubFFooterFile { get; private set; }
		public StorageFile LogoTab5SubGRightFile { get; private set; }
		public StorageFile LogoTab5SubGFooterFile { get; private set; }
		public StorageFile LogoTab5SubHRightFile { get; private set; }
		public StorageFile LogoTab5SubHFooterFile { get; private set; }
		public StorageFile LogoTab5SubIRightFile { get; private set; }
		public StorageFile LogoTab5SubIFooterFile { get; private set; }
		public StorageFile LogoTab5SubJRightFile { get; private set; }
		public StorageFile LogoTab5SubJFooterFile { get; private set; }
		public StorageFile LogoTab5SubURightFile { get; private set; }
		public StorageFile LogoTab5SubUFooterFile { get; private set; }
		public StorageFile LogoTab5SubVRightFile { get; private set; }
		public StorageFile LogoTab5SubVFooterFile { get; private set; }
		public StorageFile LogoTab5SubWRightFile { get; private set; }
		public StorageFile LogoTab5SubWFooterFile { get; private set; }

		public StorageDirectory Tab5PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 6
		public StorageFile LogoTab6SubARightFile { get; private set; }
		public StorageFile LogoTab6SubAFooterFile { get; private set; }
		public StorageFile LogoTab6SubBRightFile { get; private set; }
		public StorageFile LogoTab6SubBFooterFile { get; private set; }
		public StorageFile LogoTab6SubCRightFile { get; private set; }
		public StorageFile LogoTab6SubCFooterFile { get; private set; }
		public StorageFile LogoTab6SubDRightFile { get; private set; }
		public StorageFile LogoTab6SubDFooterFile { get; private set; }
		public StorageFile LogoTab6SubERightFile { get; private set; }
		public StorageFile LogoTab6SubEFooterFile { get; private set; }
		public StorageFile LogoTab6SubFRightFile { get; private set; }
		public StorageFile LogoTab6SubFFooterFile { get; private set; }
		public StorageFile LogoTab6SubGRightFile { get; private set; }
		public StorageFile LogoTab6SubGFooterFile { get; private set; }
		public StorageFile LogoTab6SubHRightFile { get; private set; }
		public StorageFile LogoTab6SubHFooterFile { get; private set; }
		public StorageFile LogoTab6SubIRightFile { get; private set; }
		public StorageFile LogoTab6SubIFooterFile { get; private set; }
		public StorageFile LogoTab6SubJRightFile { get; private set; }
		public StorageFile LogoTab6SubJFooterFile { get; private set; }
		public StorageFile LogoTab6SubURightFile { get; private set; }
		public StorageFile LogoTab6SubUFooterFile { get; private set; }
		public StorageFile LogoTab6SubVRightFile { get; private set; }
		public StorageFile LogoTab6SubVFooterFile { get; private set; }
		public StorageFile LogoTab6SubWRightFile { get; private set; }
		public StorageFile LogoTab6SubWFooterFile { get; private set; }

		public StorageDirectory Tab6PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 7
		public StorageFile LogoTab7SubARightFile { get; private set; }
		public StorageFile LogoTab7SubAFooterFile { get; private set; }
		public StorageFile LogoTab7SubBRightFile { get; private set; }
		public StorageFile LogoTab7SubBFooterFile { get; private set; }
		public StorageFile LogoTab7SubCRightFile { get; private set; }
		public StorageFile LogoTab7SubCFooterFile { get; private set; }
		public StorageFile LogoTab7SubDRightFile { get; private set; }
		public StorageFile LogoTab7SubDFooterFile { get; private set; }
		public StorageFile LogoTab7SubERightFile { get; private set; }
		public StorageFile LogoTab7SubEFooterFile { get; private set; }
		public StorageFile LogoTab7SubFRightFile { get; private set; }
		public StorageFile LogoTab7SubFFooterFile { get; private set; }
		public StorageFile LogoTab7SubGRightFile { get; private set; }
		public StorageFile LogoTab7SubGFooterFile { get; private set; }
		public StorageFile LogoTab7SubHRightFile { get; private set; }
		public StorageFile LogoTab7SubHFooterFile { get; private set; }
		public StorageFile LogoTab7SubIRightFile { get; private set; }
		public StorageFile LogoTab7SubIFooterFile { get; private set; }
		public StorageFile LogoTab7SubJRightFile { get; private set; }
		public StorageFile LogoTab7SubJFooterFile { get; private set; }
		public StorageFile LogoTab7SubURightFile { get; private set; }
		public StorageFile LogoTab7SubUFooterFile { get; private set; }
		public StorageFile LogoTab7SubVRightFile { get; private set; }
		public StorageFile LogoTab7SubVFooterFile { get; private set; }
		public StorageFile LogoTab7SubWRightFile { get; private set; }
		public StorageFile LogoTab7SubWFooterFile { get; private set; }

		public StorageDirectory Tab7PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 8
		public StorageFile LogoTab8SubARightFile { get; private set; }
		public StorageFile LogoTab8SubAFooterFile { get; private set; }
		public StorageFile LogoTab8SubBRightFile { get; private set; }
		public StorageFile LogoTab8SubBFooterFile { get; private set; }
		public StorageFile LogoTab8SubCRightFile { get; private set; }
		public StorageFile LogoTab8SubCFooterFile { get; private set; }
		public StorageFile LogoTab8SubDRightFile { get; private set; }
		public StorageFile LogoTab8SubDFooterFile { get; private set; }
		public StorageFile LogoTab8SubERightFile { get; private set; }
		public StorageFile LogoTab8SubEFooterFile { get; private set; }
		public StorageFile LogoTab8SubFRightFile { get; private set; }
		public StorageFile LogoTab8SubFFooterFile { get; private set; }
		public StorageFile LogoTab8SubGRightFile { get; private set; }
		public StorageFile LogoTab8SubGFooterFile { get; private set; }
		public StorageFile LogoTab8SubHRightFile { get; private set; }
		public StorageFile LogoTab8SubHFooterFile { get; private set; }
		public StorageFile LogoTab8SubIRightFile { get; private set; }
		public StorageFile LogoTab8SubIFooterFile { get; private set; }
		public StorageFile LogoTab8SubJRightFile { get; private set; }
		public StorageFile LogoTab8SubJFooterFile { get; private set; }
		public StorageFile LogoTab8SubURightFile { get; private set; }
		public StorageFile LogoTab8SubUFooterFile { get; private set; }
		public StorageFile LogoTab8SubVRightFile { get; private set; }
		public StorageFile LogoTab8SubVFooterFile { get; private set; }
		public StorageFile LogoTab8SubWRightFile { get; private set; }
		public StorageFile LogoTab8SubWFooterFile { get; private set; }

		public StorageDirectory Tab8PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 9
		public StorageFile LogoTab9SubARightFile { get; private set; }
		public StorageFile LogoTab9SubAFooterFile { get; private set; }
		public StorageFile LogoTab9SubBRightFile { get; private set; }
		public StorageFile LogoTab9SubBFooterFile { get; private set; }
		public StorageFile LogoTab9SubCRightFile { get; private set; }
		public StorageFile LogoTab9SubCFooterFile { get; private set; }
		public StorageFile LogoTab9SubDRightFile { get; private set; }
		public StorageFile LogoTab9SubDFooterFile { get; private set; }
		public StorageFile LogoTab9SubERightFile { get; private set; }
		public StorageFile LogoTab9SubEFooterFile { get; private set; }
		public StorageFile LogoTab9SubFRightFile { get; private set; }
		public StorageFile LogoTab9SubFFooterFile { get; private set; }
		public StorageFile LogoTab9SubGRightFile { get; private set; }
		public StorageFile LogoTab9SubGFooterFile { get; private set; }
		public StorageFile LogoTab9SubHRightFile { get; private set; }
		public StorageFile LogoTab9SubHFooterFile { get; private set; }
		public StorageFile LogoTab9SubIRightFile { get; private set; }
		public StorageFile LogoTab9SubIFooterFile { get; private set; }
		public StorageFile LogoTab9SubJRightFile { get; private set; }
		public StorageFile LogoTab9SubJFooterFile { get; private set; }
		public StorageFile LogoTab9SubURightFile { get; private set; }
		public StorageFile LogoTab9SubUFooterFile { get; private set; }
		public StorageFile LogoTab9SubVRightFile { get; private set; }
		public StorageFile LogoTab9SubVFooterFile { get; private set; }
		public StorageFile LogoTab9SubWRightFile { get; private set; }
		public StorageFile LogoTab9SubWFooterFile { get; private set; }

		public StorageDirectory Tab9PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 10
		public StorageFile LogoTab10SubARightFile { get; private set; }
		public StorageFile LogoTab10SubAFooterFile { get; private set; }
		public StorageFile LogoTab10SubBRightFile { get; private set; }
		public StorageFile LogoTab10SubBFooterFile { get; private set; }
		public StorageFile LogoTab10SubCRightFile { get; private set; }
		public StorageFile LogoTab10SubCFooterFile { get; private set; }
		public StorageFile LogoTab10SubDRightFile { get; private set; }
		public StorageFile LogoTab10SubDFooterFile { get; private set; }
		public StorageFile LogoTab10SubERightFile { get; private set; }
		public StorageFile LogoTab10SubEFooterFile { get; private set; }
		public StorageFile LogoTab10SubFRightFile { get; private set; }
		public StorageFile LogoTab10SubFFooterFile { get; private set; }
		public StorageFile LogoTab10SubGRightFile { get; private set; }
		public StorageFile LogoTab10SubGFooterFile { get; private set; }
		public StorageFile LogoTab10SubHRightFile { get; private set; }
		public StorageFile LogoTab10SubHFooterFile { get; private set; }
		public StorageFile LogoTab10SubIRightFile { get; private set; }
		public StorageFile LogoTab10SubIFooterFile { get; private set; }
		public StorageFile LogoTab10SubJRightFile { get; private set; }
		public StorageFile LogoTab10SubJFooterFile { get; private set; }
		public StorageFile LogoTab10SubURightFile { get; private set; }
		public StorageFile LogoTab10SubUFooterFile { get; private set; }
		public StorageFile LogoTab10SubVRightFile { get; private set; }
		public StorageFile LogoTab10SubVFooterFile { get; private set; }
		public StorageFile LogoTab10SubWRightFile { get; private set; }
		public StorageFile LogoTab10SubWFooterFile { get; private set; }

		public StorageDirectory Tab10PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 11
		public StorageFile LogoTab11SubARightFile { get; private set; }
		public StorageFile LogoTab11SubAFooterFile { get; private set; }
		public StorageFile LogoTab11SubBRightFile { get; private set; }
		public StorageFile LogoTab11SubBFooterFile { get; private set; }
		public StorageFile LogoTab11SubCRightFile { get; private set; }
		public StorageFile LogoTab11SubCFooterFile { get; private set; }
		public StorageFile LogoTab11SubDRightFile { get; private set; }
		public StorageFile LogoTab11SubDFooterFile { get; private set; }
		public StorageFile LogoTab11SubERightFile { get; private set; }
		public StorageFile LogoTab11SubEFooterFile { get; private set; }
		public StorageFile LogoTab11SubFRightFile { get; private set; }
		public StorageFile LogoTab11SubFFooterFile { get; private set; }
		public StorageFile LogoTab11SubGRightFile { get; private set; }
		public StorageFile LogoTab11SubGFooterFile { get; private set; }
		public StorageFile LogoTab11SubHRightFile { get; private set; }
		public StorageFile LogoTab11SubHFooterFile { get; private set; }
		public StorageFile LogoTab11SubIRightFile { get; private set; }
		public StorageFile LogoTab11SubIFooterFile { get; private set; }
		public StorageFile LogoTab11SubJRightFile { get; private set; }
		public StorageFile LogoTab11SubJFooterFile { get; private set; }
		public StorageFile LogoTab11SubURightFile { get; private set; }
		public StorageFile LogoTab11SubUFooterFile { get; private set; }
		public StorageFile LogoTab11SubVRightFile { get; private set; }
		public StorageFile LogoTab11SubVFooterFile { get; private set; }
		public StorageFile LogoTab11SubWRightFile { get; private set; }
		public StorageFile LogoTab11SubWFooterFile { get; private set; }

		public StorageDirectory Tab11PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartWSlidesFolder { get; private set; }
		#endregion

		public void Init(StorageDirectory dataFolder)
		{
			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			#region Cleanslate
			LogoCleanslateHeaderFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_shift", "design_branding", "tab_1_header.png" }));
			LogoCleanslateSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_shift", "design_branding", "tab_1.png" }));
			#endregion

			#region Tab 1
			LogoTab1SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_a", "design_branding", "1a_right.png" }));
			LogoTab1SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_a", "design_branding", "1a_footer.png" }));
			LogoTab1SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_b", "design_branding", "1b_right.png" }));
			LogoTab1SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_b", "design_branding", "1b_footer.png" }));
			LogoTab1SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_c", "design_branding", "1c_right.png" }));
			LogoTab1SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_c", "design_branding", "1c_footer.png" }));
			LogoTab1SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_d", "design_branding", "1d_right.png" }));
			LogoTab1SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_d", "design_branding", "1d_footer.png" }));
			LogoTab1SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_e", "design_branding", "1e_right.png" }));
			LogoTab1SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_e", "design_branding", "1e_footer.png" }));
			LogoTab1SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_f", "design_branding", "1f_right.png" }));
			LogoTab1SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_f", "design_branding", "1f_footer.png" }));
			LogoTab1SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_g", "design_branding", "1g_right.png" }));
			LogoTab1SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_g", "design_branding", "1g_footer.png" }));
			LogoTab1SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_h", "design_branding", "1h_right.png" }));
			LogoTab1SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_h", "design_branding", "1h_footer.png" }));
			LogoTab1SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_i", "design_branding", "1i_right.png" }));
			LogoTab1SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_i", "design_branding", "1i_footer.png" }));
			LogoTab1SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_j", "design_branding", "1j_right.png" }));
			LogoTab1SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_j", "design_branding", "1j_footer.png" }));
			LogoTab1SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_u", "design_branding", "1u_right.png" }));
			LogoTab1SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_u", "design_branding", "1u_footer.png" }));
			LogoTab1SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_v", "design_branding", "1v_right.png" }));
			LogoTab1SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_v", "design_branding", "1v_footer.png" }));
			LogoTab1SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_w", "design_branding", "1w_right.png" }));
			LogoTab1SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_w", "design_branding", "1w_footer.png" }));

			Tab1PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_u" }));
			Tab1PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_v" }));
			Tab1PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_starters", "subtab_w" }));
			#endregion

			#region Tab 2
			LogoTab2SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_a", "design_branding", "2a_right.png" }));
			LogoTab2SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_a", "design_branding", "2a_footer.png" }));
			LogoTab2SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_b", "design_branding", "2b_right.png" }));
			LogoTab2SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_b", "design_branding", "2b_footer.png" }));
			LogoTab2SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_c", "design_branding", "2c_right.png" }));
			LogoTab2SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_c", "design_branding", "2c_footer.png" }));
			LogoTab2SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_d", "design_branding", "2d_right.png" }));
			LogoTab2SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_d", "design_branding", "2d_footer.png" }));
			LogoTab2SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_e", "design_branding", "2e_right.png" }));
			LogoTab2SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_e", "design_branding", "2e_footer.png" }));
			LogoTab2SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_f", "design_branding", "2f_right.png" }));
			LogoTab2SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_f", "design_branding", "2f_footer.png" }));
			LogoTab2SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_g", "design_branding", "2g_right.png" }));
			LogoTab2SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_g", "design_branding", "2g_footer.png" }));
			LogoTab2SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_h", "design_branding", "2h_right.png" }));
			LogoTab2SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_h", "design_branding", "2h_footer.png" }));
			LogoTab2SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_i", "design_branding", "2i_right.png" }));
			LogoTab2SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_i", "design_branding", "2i_footer.png" }));
			LogoTab2SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_j", "design_branding", "2j_right.png" }));
			LogoTab2SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_j", "design_branding", "2j_footer.png" }));
			LogoTab2SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_u", "design_branding", "2u_right.png" }));
			LogoTab2SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_u", "design_branding", "2u_footer.png" }));
			LogoTab2SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_v", "design_branding", "2v_right.png" }));
			LogoTab2SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_v", "design_branding", "2v_footer.png" }));
			LogoTab2SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_w", "design_branding", "2w_right.png" }));
			LogoTab2SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_w", "design_branding", "2w_footer.png" }));

			Tab2PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_u" }));
			Tab2PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_v" }));
			Tab2PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_goals", "subtab_w" }));
			#endregion

			#region Tab 3
			LogoTab3SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_a", "design_branding", "3a_right.png" }));
			LogoTab3SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_a", "design_branding", "3a_footer.png" }));
			LogoTab3SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_b", "design_branding", "3b_right.png" }));
			LogoTab3SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_b", "design_branding", "3b_footer.png" }));
			LogoTab3SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_c", "design_branding", "3c_right.png" }));
			LogoTab3SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_c", "design_branding", "3c_footer.png" }));
			LogoTab3SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_d", "design_branding", "3d_right.png" }));
			LogoTab3SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_d", "design_branding", "3d_footer.png" }));
			LogoTab3SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_e", "design_branding", "3e_right.png" }));
			LogoTab3SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_e", "design_branding", "3e_footer.png" }));
			LogoTab3SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_f", "design_branding", "3f_right.png" }));
			LogoTab3SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_f", "design_branding", "3f_footer.png" }));
			LogoTab3SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_g", "design_branding", "3g_right.png" }));
			LogoTab3SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_g", "design_branding", "3g_footer.png" }));
			LogoTab3SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_h", "design_branding", "3h_right.png" }));
			LogoTab3SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_h", "design_branding", "3h_footer.png" }));
			LogoTab3SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_i", "design_branding", "3i_right.png" }));
			LogoTab3SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_i", "design_branding", "3i_footer.png" }));
			LogoTab3SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_j", "design_branding", "3j_right.png" }));
			LogoTab3SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_j", "design_branding", "3j_footer.png" }));
			LogoTab3SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_u", "design_branding", "3u_right.png" }));
			LogoTab3SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_u", "design_branding", "3u_footer.png" }));
			LogoTab3SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_v", "design_branding", "3v_right.png" }));
			LogoTab3SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_v", "design_branding", "3v_footer.png" }));
			LogoTab3SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_w", "design_branding", "3w_right.png" }));
			LogoTab3SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_w", "design_branding", "3w_footer.png" }));

			Tab3PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_u" }));
			Tab3PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_v" }));
			Tab3PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_market_opportunity", "subtab_w" }));
			#endregion

			#region Tab 4
			LogoTab4SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_a", "design_branding", "4a_right.png" }));
			LogoTab4SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_a", "design_branding", "4a_footer.png" }));
			LogoTab4SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_b", "design_branding", "4b_right.png" }));
			LogoTab4SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_b", "design_branding", "4b_footer.png" }));
			LogoTab4SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_c", "design_branding", "4c_right.png" }));
			LogoTab4SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_c", "design_branding", "4c_footer.png" }));
			LogoTab4SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_d", "design_branding", "4d_right.png" }));
			LogoTab4SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_d", "design_branding", "4d_footer.png" }));
			LogoTab4SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_e", "design_branding", "4e_right.png" }));
			LogoTab4SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_e", "design_branding", "4e_footer.png" }));
			LogoTab4SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_f", "design_branding", "4f_right.png" }));
			LogoTab4SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_f", "design_branding", "4f_footer.png" }));
			LogoTab4SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_g", "design_branding", "4g_right.png" }));
			LogoTab4SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_g", "design_branding", "4g_footer.png" }));
			LogoTab4SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_h", "design_branding", "4h_right.png" }));
			LogoTab4SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_h", "design_branding", "4h_footer.png" }));
			LogoTab4SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_i", "design_branding", "4i_right.png" }));
			LogoTab4SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_i", "design_branding", "4i_footer.png" }));
			LogoTab4SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_j", "design_branding", "4j_right.png" }));
			LogoTab4SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_j", "design_branding", "4j_footer.png" }));
			LogoTab4SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_u", "design_branding", "4u_right.png" }));
			LogoTab4SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_u", "design_branding", "4u_footer.png" }));
			LogoTab4SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_v", "design_branding", "4v_right.png" }));
			LogoTab4SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_v", "design_branding", "4v_footer.png" }));
			LogoTab4SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_w", "design_branding", "4w_right.png" }));
			LogoTab4SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_w", "design_branding", "4w_footer.png" }));

			Tab4PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_u" }));
			Tab4PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_v" }));
			Tab4PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_needs_solutions", "subtab_w" }));
			#endregion

			#region Tab 5
			LogoTab5SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_a", "design_branding", "5a_right.png" }));
			LogoTab5SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_a", "design_branding", "5a_footer.png" }));
			LogoTab5SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_b", "design_branding", "5b_right.png" }));
			LogoTab5SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_b", "design_branding", "5b_footer.png" }));
			LogoTab5SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_c", "design_branding", "5c_right.png" }));
			LogoTab5SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_c", "design_branding", "5c_footer.png" }));
			LogoTab5SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_d", "design_branding", "5d_right.png" }));
			LogoTab5SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_d", "design_branding", "5d_footer.png" }));
			LogoTab5SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_e", "design_branding", "5e_right.png" }));
			LogoTab5SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_e", "design_branding", "5e_footer.png" }));
			LogoTab5SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_f", "design_branding", "5f_right.png" }));
			LogoTab5SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_f", "design_branding", "5f_footer.png" }));
			LogoTab5SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_g", "design_branding", "5g_right.png" }));
			LogoTab5SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_g", "design_branding", "5g_footer.png" }));
			LogoTab5SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_h", "design_branding", "5h_right.png" }));
			LogoTab5SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_h", "design_branding", "5h_footer.png" }));
			LogoTab5SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_i", "design_branding", "5i_right.png" }));
			LogoTab5SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_i", "design_branding", "5i_footer.png" }));
			LogoTab5SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_j", "design_branding", "5j_right.png" }));
			LogoTab5SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_j", "design_branding", "5j_footer.png" }));
			LogoTab5SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_u", "design_branding", "5u_right.png" }));
			LogoTab5SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_u", "design_branding", "5u_footer.png" }));
			LogoTab5SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_v", "design_branding", "5v_right.png" }));
			LogoTab5SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_v", "design_branding", "5v_footer.png" }));
			LogoTab5SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_w", "design_branding", "5w_right.png" }));
			LogoTab5SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_w", "design_branding", "5w_footer.png" }));

			Tab5PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_u" }));
			Tab5PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_v" }));
			Tab5PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_cbc", "subtab_w" }));
			#endregion

			#region Tab 6
			LogoTab6SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_a", "design_branding", "6a_right.png" }));
			LogoTab6SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_a", "design_branding", "6a_footer.png" }));
			LogoTab6SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_b", "design_branding", "6b_right.png" }));
			LogoTab6SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_b", "design_branding", "6b_footer.png" }));
			LogoTab6SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_c", "design_branding", "6c_right.png" }));
			LogoTab6SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_c", "design_branding", "6c_footer.png" }));
			LogoTab6SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_d", "design_branding", "6d_right.png" }));
			LogoTab6SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_d", "design_branding", "6d_footer.png" }));
			LogoTab6SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_e", "design_branding", "6e_right.png" }));
			LogoTab6SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_e", "design_branding", "6e_footer.png" }));
			LogoTab6SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_f", "design_branding", "6f_right.png" }));
			LogoTab6SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_f", "design_branding", "6f_footer.png" }));
			LogoTab6SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_g", "design_branding", "6g_right.png" }));
			LogoTab6SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_g", "design_branding", "6g_footer.png" }));
			LogoTab6SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_h", "design_branding", "6h_right.png" }));
			LogoTab6SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_h", "design_branding", "6h_footer.png" }));
			LogoTab6SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_i", "design_branding", "6i_right.png" }));
			LogoTab6SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_i", "design_branding", "6i_footer.png" }));
			LogoTab6SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_j", "design_branding", "6j_right.png" }));
			LogoTab6SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_j", "design_branding", "6j_footer.png" }));
			LogoTab6SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_u", "design_branding", "6u_right.png" }));
			LogoTab6SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_u", "design_branding", "6u_footer.png" }));
			LogoTab6SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_v", "design_branding", "6v_right.png" }));
			LogoTab6SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_v", "design_branding", "6v_footer.png" }));
			LogoTab6SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_w", "design_branding", "6w_right.png" }));
			LogoTab6SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_w", "design_branding", "6w_footer.png" }));

			Tab6PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_u" }));
			Tab6PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_v" }));
			Tab6PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_integrated_solution", "subtab_w" }));
			#endregion

			#region Tab 7
			LogoTab7SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_a", "design_branding", "7a_right.png" }));
			LogoTab7SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_a", "design_branding", "7a_footer.png" }));
			LogoTab7SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_b", "design_branding", "7b_right.png" }));
			LogoTab7SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_b", "design_branding", "7b_footer.png" }));
			LogoTab7SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_c", "design_branding", "7c_right.png" }));
			LogoTab7SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_c", "design_branding", "7c_footer.png" }));
			LogoTab7SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_d", "design_branding", "7d_right.png" }));
			LogoTab7SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_d", "design_branding", "7d_footer.png" }));
			LogoTab7SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_e", "design_branding", "7e_right.png" }));
			LogoTab7SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_e", "design_branding", "7e_footer.png" }));
			LogoTab7SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_f", "design_branding", "7f_right.png" }));
			LogoTab7SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_f", "design_branding", "7f_footer.png" }));
			LogoTab7SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_g", "design_branding", "7g_right.png" }));
			LogoTab7SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_g", "design_branding", "7g_footer.png" }));
			LogoTab7SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_h", "design_branding", "7h_right.png" }));
			LogoTab7SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_h", "design_branding", "7h_footer.png" }));
			LogoTab7SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_i", "design_branding", "7i_right.png" }));
			LogoTab7SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_i", "design_branding", "7i_footer.png" }));
			LogoTab7SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_j", "design_branding", "7j_right.png" }));
			LogoTab7SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_j", "design_branding", "7j_footer.png" }));
			LogoTab7SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_u", "design_branding", "7u_right.png" }));
			LogoTab7SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_u", "design_branding", "7u_footer.png" }));
			LogoTab7SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_v", "design_branding", "7v_right.png" }));
			LogoTab7SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_v", "design_branding", "7v_footer.png" }));
			LogoTab7SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_w", "design_branding", "7w_right.png" }));
			LogoTab7SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_w", "design_branding", "7w_footer.png" }));

			Tab7PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_u" }));
			Tab7PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_v" }));
			Tab7PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_investment", "subtab_w" }));
			#endregion

			#region Tab 8
			LogoTab8SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_a", "design_branding", "8a_right.png" }));
			LogoTab8SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_a", "design_branding", "8a_footer.png" }));
			LogoTab8SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_b", "design_branding", "8b_right.png" }));
			LogoTab8SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_b", "design_branding", "8b_footer.png" }));
			LogoTab8SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_c", "design_branding", "8c_right.png" }));
			LogoTab8SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_c", "design_branding", "8c_footer.png" }));
			LogoTab8SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_d", "design_branding", "8d_right.png" }));
			LogoTab8SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_d", "design_branding", "8d_footer.png" }));
			LogoTab8SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_e", "design_branding", "8e_right.png" }));
			LogoTab8SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_e", "design_branding", "8e_footer.png" }));
			LogoTab8SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_f", "design_branding", "8f_right.png" }));
			LogoTab8SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_f", "design_branding", "8f_footer.png" }));
			LogoTab8SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_g", "design_branding", "8g_right.png" }));
			LogoTab8SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_g", "design_branding", "8g_footer.png" }));
			LogoTab8SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_h", "design_branding", "8h_right.png" }));
			LogoTab8SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_h", "design_branding", "8h_footer.png" }));
			LogoTab8SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_i", "design_branding", "8i_right.png" }));
			LogoTab8SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_i", "design_branding", "8i_footer.png" }));
			LogoTab8SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_j", "design_branding", "8j_right.png" }));
			LogoTab8SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_j", "design_branding", "8j_footer.png" }));
			LogoTab8SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_u", "design_branding", "8u_right.png" }));
			LogoTab8SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_u", "design_branding", "8u_footer.png" }));
			LogoTab8SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_v", "design_branding", "8v_right.png" }));
			LogoTab8SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_v", "design_branding", "8v_footer.png" }));
			LogoTab8SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_w", "design_branding", "8w_right.png" }));
			LogoTab8SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_w", "design_branding", "8w_footer.png" }));

			Tab8PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_u" }));
			Tab8PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_v" }));
			Tab8PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_closers", "subtab_w" }));
			#endregion

			#region Tab 9
			LogoTab9SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_a", "design_branding", "9a_right.png" }));
			LogoTab9SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_a", "design_branding", "9a_footer.png" }));
			LogoTab9SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_b", "design_branding", "9b_right.png" }));
			LogoTab9SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_b", "design_branding", "9b_footer.png" }));
			LogoTab9SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_c", "design_branding", "9c_right.png" }));
			LogoTab9SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_c", "design_branding", "9c_footer.png" }));
			LogoTab9SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_d", "design_branding", "9d_right.png" }));
			LogoTab9SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_d", "design_branding", "9d_footer.png" }));
			LogoTab9SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_e", "design_branding", "9e_right.png" }));
			LogoTab9SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_e", "design_branding", "9e_footer.png" }));
			LogoTab9SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_f", "design_branding", "9f_right.png" }));
			LogoTab9SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_f", "design_branding", "9f_footer.png" }));
			LogoTab9SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_g", "design_branding", "9g_right.png" }));
			LogoTab9SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_g", "design_branding", "9g_footer.png" }));
			LogoTab9SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_h", "design_branding", "9h_right.png" }));
			LogoTab9SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_h", "design_branding", "9h_footer.png" }));
			LogoTab9SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_i", "design_branding", "9i_right.png" }));
			LogoTab9SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_i", "design_branding", "9i_footer.png" }));
			LogoTab9SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_j", "design_branding", "9j_right.png" }));
			LogoTab9SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_j", "design_branding", "9j_footer.png" }));
			LogoTab9SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_u", "design_branding", "9u_right.png" }));
			LogoTab9SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_u", "design_branding", "9u_footer.png" }));
			LogoTab9SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_v", "design_branding", "9v_right.png" }));
			LogoTab9SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_v", "design_branding", "9v_footer.png" }));
			LogoTab9SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_w", "design_branding", "9w_right.png" }));
			LogoTab9SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_w", "design_branding", "9w_footer.png" }));

			Tab9PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_u" }));
			Tab9PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_v" }));
			Tab9PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_relationship_next_steps", "subtab_w" }));
			#endregion

			#region Tab 10
			LogoTab10SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_a", "design_branding", "10a_right.png" }));
			LogoTab10SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_a", "design_branding", "10a_footer.png" }));
			LogoTab10SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_b", "design_branding", "10b_right.png" }));
			LogoTab10SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_b", "design_branding", "10b_footer.png" }));
			LogoTab10SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_c", "design_branding", "10c_right.png" }));
			LogoTab10SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_c", "design_branding", "10c_footer.png" }));
			LogoTab10SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_d", "design_branding", "10d_right.png" }));
			LogoTab10SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_d", "design_branding", "10d_footer.png" }));
			LogoTab10SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_e", "design_branding", "10e_right.png" }));
			LogoTab10SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_e", "design_branding", "10e_footer.png" }));
			LogoTab10SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_f", "design_branding", "10f_right.png" }));
			LogoTab10SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_f", "design_branding", "10f_footer.png" }));
			LogoTab10SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_g", "design_branding", "10g_right.png" }));
			LogoTab10SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_g", "design_branding", "10g_footer.png" }));
			LogoTab10SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_h", "design_branding", "10h_right.png" }));
			LogoTab10SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_h", "design_branding", "10h_footer.png" }));
			LogoTab10SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_i", "design_branding", "10i_right.png" }));
			LogoTab10SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_i", "design_branding", "10i_footer.png" }));
			LogoTab10SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_j", "design_branding", "10j_right.png" }));
			LogoTab10SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_j", "design_branding", "10j_footer.png" }));
			LogoTab10SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_u", "design_branding", "10u_right.png" }));
			LogoTab10SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_u", "design_branding", "10u_footer.png" }));
			LogoTab10SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_v", "design_branding", "10v_right.png" }));
			LogoTab10SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_v", "design_branding", "10v_footer.png" }));
			LogoTab10SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_w", "design_branding", "10w_right.png" }));
			LogoTab10SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_w", "design_branding", "10w_footer.png" }));

			Tab10PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_u" }));
			Tab10PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_v" }));
			Tab10PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_agreement_contract", "subtab_w" }));
			#endregion

			#region Tab 11
			LogoTab11SubARightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_a", "design_branding", "11a_right.png" }));
			LogoTab11SubAFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_a", "design_branding", "11a_footer.png" }));
			LogoTab11SubBRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_b", "design_branding", "11b_right.png" }));
			LogoTab11SubBFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_b", "design_branding", "11b_footer.png" }));
			LogoTab11SubCRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_c", "design_branding", "11c_right.png" }));
			LogoTab11SubCFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_c", "design_branding", "11c_footer.png" }));
			LogoTab11SubDRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_d", "design_branding", "11d_right.png" }));
			LogoTab11SubDFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_d", "design_branding", "11d_footer.png" }));
			LogoTab11SubERightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_e", "design_branding", "11e_right.png" }));
			LogoTab11SubEFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_e", "design_branding", "11e_footer.png" }));
			LogoTab11SubFRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_f", "design_branding", "11f_right.png" }));
			LogoTab11SubFFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_f", "design_branding", "11f_footer.png" }));
			LogoTab11SubGRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_g", "design_branding", "11g_right.png" }));
			LogoTab11SubGFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_g", "design_branding", "11g_footer.png" }));
			LogoTab11SubHRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_h", "design_branding", "11h_right.png" }));
			LogoTab11SubHFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_h", "design_branding", "11h_footer.png" }));
			LogoTab11SubIRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_i", "design_branding", "11i_right.png" }));
			LogoTab11SubIFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_i", "design_branding", "11i_footer.png" }));
			LogoTab11SubJRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_j", "design_branding", "11j_right.png" }));
			LogoTab11SubJFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_j", "design_branding", "11j_footer.png" }));
			LogoTab11SubURightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_u", "design_branding", "11u_right.png" }));
			LogoTab11SubUFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_u", "design_branding", "11u_footer.png" }));
			LogoTab11SubVRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_v", "design_branding", "11v_right.png" }));
			LogoTab11SubVFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_v", "design_branding", "11v_footer.png" }));
			LogoTab11SubWRightFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_w", "design_branding", "11w_right.png" }));
			LogoTab11SubWFooterFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_w", "design_branding", "11w_footer.png" }));

			Tab11PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_u" }));
			Tab11PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_v" }));
			Tab11PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_support_materials", "subtab_w" }));
			#endregion
		}
	}
}
