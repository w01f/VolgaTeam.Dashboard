using System.Linq;
using System.Reflection;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Resources.Solutions.StarApp;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ResourceManager
	{
		private StorageFile _graphicResourcesFile;

		public StorageFile SettingsFile { get; private set; }

		public StorageDirectory LocalDataFolder { get; private set; }
		public StorageDirectory RemoteResourcesFolder { get; private set; }

		public StorageFile DataUsersFile { get; private set; }
		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataTargetCustomersFile { get; private set; }

		public IStarAppGraphicResources GraphicResources { get; private set; }

		#region Tab 1
		public StorageFile DataCoverPartAFile { get; private set; }

		public string[] Tab1PartKSlidesRelativePath { get; private set; }
		public string[] Tab1PartLSlidesRelativePath { get; private set; }
		public string[] Tab1PartMSlidesRelativePath { get; private set; }
		public string[] Tab1PartNSlidesRelativePath { get; private set; }
		public string[] Tab1PartOSlidesRelativePath { get; private set; }
		public string[] Tab1PartUSlidesRelativePath { get; private set; }
		public string[] Tab1PartVSlidesRelativePath { get; private set; }
		public string[] Tab1PartWSlidesRelativePath { get; private set; }
		public string[] Tab1PartXTilesRelativePath { get; private set; }
		public string[] Tab1PartYTilesRelativePath { get; private set; }
		public string[] Tab1PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 2
		public StorageFile DataCNAPartAFile { get; private set; }
		public StorageFile DataCNAPartBFile { get; private set; }

		public string[] Tab2PartKSlidesRelativePath { get; private set; }
		public string[] Tab2PartLSlidesRelativePath { get; private set; }
		public string[] Tab2PartMSlidesRelativePath { get; private set; }
		public string[] Tab2PartNSlidesRelativePath { get; private set; }
		public string[] Tab2PartOSlidesRelativePath { get; private set; }
		public string[] Tab2PartUSlidesRelativePath { get; private set; }
		public string[] Tab2PartVSlidesRelativePath { get; private set; }
		public string[] Tab2PartWSlidesRelativePath { get; private set; }
		public string[] Tab2PartXTilesRelativePath { get; private set; }
		public string[] Tab2PartYTilesRelativePath { get; private set; }
		public string[] Tab2PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 3
		public StorageFile DataFishingPartAFile { get; private set; }
		public StorageFile DataFishingPartBFile { get; private set; }
		public StorageFile DataFishingPartCFile { get; private set; }

		public string[] Tab3PartKSlidesRelativePath { get; private set; }
		public string[] Tab3PartLSlidesRelativePath { get; private set; }
		public string[] Tab3PartMSlidesRelativePath { get; private set; }
		public string[] Tab3PartNSlidesRelativePath { get; private set; }
		public string[] Tab3PartOSlidesRelativePath { get; private set; }
		public string[] Tab3PartUSlidesRelativePath { get; private set; }
		public string[] Tab3PartVSlidesRelativePath { get; private set; }
		public string[] Tab3PartWSlidesRelativePath { get; private set; }
		public string[] Tab3PartXTilesRelativePath { get; private set; }
		public string[] Tab3PartYTilesRelativePath { get; private set; }
		public string[] Tab3PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 4
		public StorageFile DataCustomerPartAFile { get; private set; }
		public StorageFile DataCustomerPartBFile { get; private set; }
		public StorageFile DataCustomerPartCFile { get; private set; }

		public string[] Tab4PartKSlidesRelativePath { get; private set; }
		public string[] Tab4PartLSlidesRelativePath { get; private set; }
		public string[] Tab4PartMSlidesRelativePath { get; private set; }
		public string[] Tab4PartNSlidesRelativePath { get; private set; }
		public string[] Tab4PartOSlidesRelativePath { get; private set; }
		public string[] Tab4PartUSlidesRelativePath { get; private set; }
		public string[] Tab4PartVSlidesRelativePath { get; private set; }
		public string[] Tab4PartWSlidesRelativePath { get; private set; }
		public string[] Tab4PartXTilesRelativePath { get; private set; }
		public string[] Tab4PartYTilesRelativePath { get; private set; }
		public string[] Tab4PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 5
		public StorageFile DataSharePartAFile { get; private set; }
		public StorageFile DataSharePartBFile { get; private set; }
		public StorageFile DataSharePartCFile { get; private set; }
		public StorageFile DataSharePartDFile { get; private set; }
		public StorageFile DataSharePartEFile { get; private set; }

		public string[] Tab5PartKSlidesRelativePath { get; private set; }
		public string[] Tab5PartLSlidesRelativePath { get; private set; }
		public string[] Tab5PartMSlidesRelativePath { get; private set; }
		public string[] Tab5PartNSlidesRelativePath { get; private set; }
		public string[] Tab5PartOSlidesRelativePath { get; private set; }
		public string[] Tab5PartUSlidesRelativePath { get; private set; }
		public string[] Tab5PartVSlidesRelativePath { get; private set; }
		public string[] Tab5PartWSlidesRelativePath { get; private set; }
		public string[] Tab5PartXTilesRelativePath { get; private set; }
		public string[] Tab5PartYTilesRelativePath { get; private set; }
		public string[] Tab5PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 6
		public StorageFile DataROIPartAFile { get; private set; }
		public StorageFile DataROIPartBFile { get; private set; }
		public StorageFile DataROIPartCFile { get; private set; }
		public StorageFile DataROIPartDFile { get; private set; }

		public string[] Tab6PartKSlidesRelativePath { get; private set; }
		public string[] Tab6PartLSlidesRelativePath { get; private set; }
		public string[] Tab6PartMSlidesRelativePath { get; private set; }
		public string[] Tab6PartNSlidesRelativePath { get; private set; }
		public string[] Tab6PartOSlidesRelativePath { get; private set; }
		public string[] Tab6PartUSlidesRelativePath { get; private set; }
		public string[] Tab6PartVSlidesRelativePath { get; private set; }
		public string[] Tab6PartWSlidesRelativePath { get; private set; }
		public string[] Tab6PartXTilesRelativePath { get; private set; }
		public string[] Tab6PartYTilesRelativePath { get; private set; }
		public string[] Tab6PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 7
		public StorageFile DataMarketPartAFile { get; private set; }
		public StorageFile DataMarketPartBFile { get; private set; }
		public StorageFile DataMarketPartCFile { get; private set; }

		public string[] Tab7PartKSlidesRelativePath { get; private set; }
		public string[] Tab7PartLSlidesRelativePath { get; private set; }
		public string[] Tab7PartMSlidesRelativePath { get; private set; }
		public string[] Tab7PartNSlidesRelativePath { get; private set; }
		public string[] Tab7PartOSlidesRelativePath { get; private set; }
		public string[] Tab7PartUSlidesRelativePath { get; private set; }
		public string[] Tab7PartVSlidesRelativePath { get; private set; }
		public string[] Tab7PartWSlidesRelativePath { get; private set; }
		public string[] Tab7PartXTilesRelativePath { get; private set; }
		public string[] Tab7PartYTilesRelativePath { get; private set; }
		public string[] Tab7PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 8
		public StorageFile DataVideoPartAFile { get; private set; }
		public StorageFile DataVideoPartBFile { get; private set; }
		public StorageFile DataVideoPartCFile { get; private set; }
		public StorageFile DataVideoPartDFile { get; private set; }

		public string[] Tab8PartKSlidesRelativePath { get; private set; }
		public string[] Tab8PartLSlidesRelativePath { get; private set; }
		public string[] Tab8PartMSlidesRelativePath { get; private set; }
		public string[] Tab8PartNSlidesRelativePath { get; private set; }
		public string[] Tab8PartOSlidesRelativePath { get; private set; }
		public string[] Tab8PartUSlidesRelativePath { get; private set; }
		public string[] Tab8PartVSlidesRelativePath { get; private set; }
		public string[] Tab8PartWSlidesRelativePath { get; private set; }
		public string[] Tab8PartXTilesRelativePath { get; private set; }
		public string[] Tab8PartYTilesRelativePath { get; private set; }
		public string[] Tab8PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 9
		public StorageFile DataAudiencePartAFile { get; private set; }
		public StorageFile DataAudiencePartBFile { get; private set; }
		public StorageFile DataAudiencePartCFile { get; private set; }

		public string[] Tab9PartKSlidesRelativePath { get; private set; }
		public string[] Tab9PartLSlidesRelativePath { get; private set; }
		public string[] Tab9PartMSlidesRelativePath { get; private set; }
		public string[] Tab9PartNSlidesRelativePath { get; private set; }
		public string[] Tab9PartOSlidesRelativePath { get; private set; }
		public string[] Tab9PartUSlidesRelativePath { get; private set; }
		public string[] Tab9PartVSlidesRelativePath { get; private set; }
		public string[] Tab9PartWSlidesRelativePath { get; private set; }
		public string[] Tab9PartXTilesRelativePath { get; private set; }
		public string[] Tab9PartYTilesRelativePath { get; private set; }
		public string[] Tab9PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 10
		public StorageFile DataSolutionPartAFile { get; private set; }
		public StorageFile DataSolutionPartBFile { get; private set; }
		public StorageFile DataSolutionPartCFile { get; private set; }
		public StorageFile DataSolutionPartDFile { get; private set; }

		public string[] Tab10PartKSlidesRelativePath { get; private set; }
		public string[] Tab10PartLSlidesRelativePath { get; private set; }
		public string[] Tab10PartMSlidesRelativePath { get; private set; }
		public string[] Tab10PartNSlidesRelativePath { get; private set; }
		public string[] Tab10PartOSlidesRelativePath { get; private set; }
		public string[] Tab10PartUSlidesRelativePath { get; private set; }
		public string[] Tab10PartVSlidesRelativePath { get; private set; }
		public string[] Tab10PartWSlidesRelativePath { get; private set; }
		public string[] Tab10PartXTilesRelativePath { get; private set; }
		public string[] Tab10PartYTilesRelativePath { get; private set; }
		public string[] Tab10PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 11
		public StorageFile DataClosersPartAFile { get; private set; }
		public StorageFile DataClosersPartBFile { get; private set; }
		public StorageFile DataClosersPartCFile { get; private set; }

		public string[] Tab11PartKSlidesRelativePath { get; private set; }
		public string[] Tab11PartLSlidesRelativePath { get; private set; }
		public string[] Tab11PartMSlidesRelativePath { get; private set; }
		public string[] Tab11PartNSlidesRelativePath { get; private set; }
		public string[] Tab11PartOSlidesRelativePath { get; private set; }
		public string[] Tab11PartUSlidesRelativePath { get; private set; }
		public string[] Tab11PartVSlidesRelativePath { get; private set; }
		public string[] Tab11PartWSlidesRelativePath { get; private set; }
		public string[] Tab11PartXTilesRelativePath { get; private set; }
		public string[] Tab11PartYTilesRelativePath { get; private set; }
		public string[] Tab11PartZTilesRelativePath { get; private set; }
		#endregion

		public void Init(StorageDirectory dataFolder)
		{
			_graphicResourcesFile = new StorageFile(dataFolder.RelativePathParts.Merge("StarApp.Resources.dll"));

			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			LocalDataFolder = dataFolder;
			RemoteResourcesFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Solution Templates",
				dataFolder.RelativePathParts.Last()
			});

			DataUsersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Users.xml"));
			DataClientGoalsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			DataTargetCustomersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Target Customer.xml"));

			#region Tab 1
			DataCoverPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP01A.xml"));

			Tab1PartKSlidesRelativePath = new[] { "01_cover", "subtab_k" };
			Tab1PartLSlidesRelativePath = new[] { "01_cover", "subtab_l" };
			Tab1PartMSlidesRelativePath = new[] { "01_cover", "subtab_m" };
			Tab1PartNSlidesRelativePath = new[] { "01_cover", "subtab_n" };
			Tab1PartOSlidesRelativePath = new[] { "01_cover", "subtab_o" };
			Tab1PartUSlidesRelativePath = new[] { "01_cover", "subtab_u" };
			Tab1PartVSlidesRelativePath = new[] { "01_cover", "subtab_v" };
			Tab1PartWSlidesRelativePath = new[] { "01_cover", "subtab_w" };
			Tab1PartXTilesRelativePath = new[] { "01_cover", "subtab_x" };
			Tab1PartYTilesRelativePath = new[] { "01_cover", "subtab_y" };
			Tab1PartZTilesRelativePath = new[] { "01_cover", "subtab_z" };
			#endregion

			#region Tab 2
			DataCNAPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP02A.xml"));
			DataCNAPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP02B.xml"));

			Tab2PartKSlidesRelativePath = new[] { "02_cna", "subtab_k" };
			Tab2PartLSlidesRelativePath = new[] { "02_cna", "subtab_l" };
			Tab2PartMSlidesRelativePath = new[] { "02_cna", "subtab_m" };
			Tab2PartNSlidesRelativePath = new[] { "02_cna", "subtab_n" };
			Tab2PartOSlidesRelativePath = new[] { "02_cna", "subtab_o" };
			Tab2PartUSlidesRelativePath = new[] { "02_cna", "subtab_u" };
			Tab2PartVSlidesRelativePath = new[] { "02_cna", "subtab_v" };
			Tab2PartWSlidesRelativePath = new[] { "02_cna", "subtab_w" };
			Tab2PartXTilesRelativePath = new[] { "02_cna", "subtab_x" };
			Tab2PartYTilesRelativePath = new[] { "02_cna", "subtab_y" };
			Tab2PartZTilesRelativePath = new[] { "02_cna", "subtab_z" };
			#endregion

			#region Tab 3
			DataFishingPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03A.xml"));
			DataFishingPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03B.xml"));
			DataFishingPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03C.xml"));

			Tab3PartKSlidesRelativePath = new[] { "03_fishing", "subtab_k" };
			Tab3PartLSlidesRelativePath = new[] { "03_fishing", "subtab_l" };
			Tab3PartMSlidesRelativePath = new[] { "03_fishing", "subtab_m" };
			Tab3PartNSlidesRelativePath = new[] { "03_fishing", "subtab_n" };
			Tab3PartOSlidesRelativePath = new[] { "03_fishing", "subtab_o" };
			Tab3PartUSlidesRelativePath = new[] { "03_fishing", "subtab_u" };
			Tab3PartVSlidesRelativePath = new[] { "03_fishing", "subtab_v" };
			Tab3PartWSlidesRelativePath = new[] { "03_fishing", "subtab_w" };
			Tab3PartXTilesRelativePath = new[] { "03_fishing", "subtab_x" };
			Tab3PartYTilesRelativePath = new[] { "03_fishing", "subtab_y" };
			Tab3PartZTilesRelativePath = new[] { "03_fishing", "subtab_z" };
			#endregion

			#region Tab 4
			DataCustomerPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04A.xml"));
			DataCustomerPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04B.xml"));
			DataCustomerPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04C.xml"));

			Tab4PartKSlidesRelativePath = new[] { "04_customer", "subtab_k" };
			Tab4PartLSlidesRelativePath = new[] { "04_customer", "subtab_l" };
			Tab4PartMSlidesRelativePath = new[] { "04_customer", "subtab_m" };
			Tab4PartNSlidesRelativePath = new[] { "04_customer", "subtab_n" };
			Tab4PartOSlidesRelativePath = new[] { "04_customer", "subtab_o" };
			Tab4PartUSlidesRelativePath = new[] { "04_customer", "subtab_u" };
			Tab4PartVSlidesRelativePath = new[] { "04_customer", "subtab_v" };
			Tab4PartWSlidesRelativePath = new[] { "04_customer", "subtab_w" };
			Tab4PartXTilesRelativePath = new[] { "04_customer", "subtab_x" };
			Tab4PartYTilesRelativePath = new[] { "04_customer", "subtab_y" };
			Tab4PartZTilesRelativePath = new[] { "04_customer", "subtab_z" };
			#endregion

			#region Tab 5
			DataSharePartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05A.xml"));
			DataSharePartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05B.xml"));
			DataSharePartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05C.xml"));
			DataSharePartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05D.xml"));
			DataSharePartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05E.xml"));

			Tab5PartKSlidesRelativePath = new[] { "05_share", "subtab_k" };
			Tab5PartLSlidesRelativePath = new[] { "05_share", "subtab_l" };
			Tab5PartMSlidesRelativePath = new[] { "05_share", "subtab_m" };
			Tab5PartNSlidesRelativePath = new[] { "05_share", "subtab_n" };
			Tab5PartOSlidesRelativePath = new[] { "05_share", "subtab_o" };
			Tab5PartUSlidesRelativePath = new[] { "05_share", "subtab_u" };
			Tab5PartVSlidesRelativePath = new[] { "05_share", "subtab_v" };
			Tab5PartWSlidesRelativePath = new[] { "05_share", "subtab_w" };
			Tab5PartXTilesRelativePath = new[] { "05_share", "subtab_x" };
			Tab5PartYTilesRelativePath = new[] { "05_share", "subtab_y" };
			Tab5PartZTilesRelativePath = new[] { "05_share", "subtab_z" };
			#endregion

			#region Tab 6
			DataROIPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06A.xml"));
			DataROIPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06B.xml"));
			DataROIPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06C.xml"));
			DataROIPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06D.xml"));

			Tab6PartKSlidesRelativePath = new[] { "06_roi", "subtab_k" };
			Tab6PartLSlidesRelativePath = new[] { "06_roi", "subtab_l" };
			Tab6PartMSlidesRelativePath = new[] { "06_roi", "subtab_m" };
			Tab6PartNSlidesRelativePath = new[] { "06_roi", "subtab_n" };
			Tab6PartOSlidesRelativePath = new[] { "06_roi", "subtab_o" };
			Tab6PartUSlidesRelativePath = new[] { "06_roi", "subtab_u" };
			Tab6PartVSlidesRelativePath = new[] { "06_roi", "subtab_v" };
			Tab6PartWSlidesRelativePath = new[] { "06_roi", "subtab_w" };
			Tab6PartXTilesRelativePath = new[] { "06_roi", "subtab_x" };
			Tab6PartYTilesRelativePath = new[] { "06_roi", "subtab_y" };
			Tab6PartZTilesRelativePath = new[] { "06_roi", "subtab_z" };
			#endregion

			#region Tab 7
			DataMarketPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07A.xml"));
			DataMarketPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07B.xml"));
			DataMarketPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07C.xml"));

			Tab7PartKSlidesRelativePath = new[] { "01_cover", "subtab_k" };
			Tab7PartLSlidesRelativePath = new[] { "01_cover", "subtab_l" };
			Tab7PartMSlidesRelativePath = new[] { "01_cover", "subtab_m" };
			Tab7PartNSlidesRelativePath = new[] { "01_cover", "subtab_n" };
			Tab7PartOSlidesRelativePath = new[] { "01_cover", "subtab_o" };
			Tab7PartUSlidesRelativePath = new[] { "07_market", "subtab_u" };
			Tab7PartVSlidesRelativePath = new[] { "07_market", "subtab_v" };
			Tab7PartWSlidesRelativePath = new[] { "07_market", "subtab_w" };
			Tab7PartXTilesRelativePath = new[] { "07_market", "subtab_x" };
			Tab7PartYTilesRelativePath = new[] { "07_market", "subtab_y" };
			Tab7PartZTilesRelativePath = new[] { "07_market", "subtab_z" };
			#endregion

			#region Tab 8
			DataVideoPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08A.xml"));
			DataVideoPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08B.xml"));
			DataVideoPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08C.xml"));
			DataVideoPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08D.xml"));

			Tab8PartKSlidesRelativePath = new[] { "08_video", "subtab_k" };
			Tab8PartLSlidesRelativePath = new[] { "08_video", "subtab_l" };
			Tab8PartMSlidesRelativePath = new[] { "08_video", "subtab_m" };
			Tab8PartNSlidesRelativePath = new[] { "08_video", "subtab_n" };
			Tab8PartOSlidesRelativePath = new[] { "08_video", "subtab_o" };
			Tab8PartUSlidesRelativePath = new[] { "08_video", "subtab_u" };
			Tab8PartVSlidesRelativePath = new[] { "08_video", "subtab_v" };
			Tab8PartWSlidesRelativePath = new[] { "08_video", "subtab_w" };
			Tab8PartXTilesRelativePath = new[] { "08_video", "subtab_x" };
			Tab8PartYTilesRelativePath = new[] { "08_video", "subtab_y" };
			Tab8PartZTilesRelativePath = new[] { "08_video", "subtab_z" };
			#endregion

			#region Tab 9
			DataAudiencePartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09A.xml"));
			DataAudiencePartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09B.xml"));
			DataAudiencePartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09C.xml"));

			Tab9PartKSlidesRelativePath = new[] { "09_audience", "subtab_k" };
			Tab9PartLSlidesRelativePath = new[] { "09_audience", "subtab_l" };
			Tab9PartMSlidesRelativePath = new[] { "09_audience", "subtab_m" };
			Tab9PartNSlidesRelativePath = new[] { "09_audience", "subtab_n" };
			Tab9PartOSlidesRelativePath = new[] { "09_audience", "subtab_o" };
			Tab9PartUSlidesRelativePath = new[] { "09_audience", "subtab_u" };
			Tab9PartVSlidesRelativePath = new[] { "09_audience", "subtab_v" };
			Tab9PartWSlidesRelativePath = new[] { "09_audience", "subtab_w" };
			Tab9PartXTilesRelativePath = new[] { "09_audience", "subtab_x" };
			Tab9PartYTilesRelativePath = new[] { "09_audience", "subtab_y" };
			Tab9PartZTilesRelativePath = new[] { "09_audience", "subtab_z" };
			#endregion

			#region Tab 10
			DataSolutionPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10A.xml"));
			DataSolutionPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10B.xml"));
			DataSolutionPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10C.xml"));
			DataSolutionPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10D.xml"));

			Tab10PartKSlidesRelativePath = new[] { "10_solution", "subtab_k" };
			Tab10PartLSlidesRelativePath = new[] { "10_solution", "subtab_l" };
			Tab10PartMSlidesRelativePath = new[] { "10_solution", "subtab_m" };
			Tab10PartNSlidesRelativePath = new[] { "10_solution", "subtab_n" };
			Tab10PartOSlidesRelativePath = new[] { "10_solution", "subtab_o" };
			Tab10PartUSlidesRelativePath = new[] { "10_solution", "subtab_u" };
			Tab10PartVSlidesRelativePath = new[] { "10_solution", "subtab_v" };
			Tab10PartWSlidesRelativePath = new[] { "10_solution", "subtab_w" };
			Tab10PartXTilesRelativePath = new[] { "10_solution", "subtab_x" };
			Tab10PartYTilesRelativePath = new[] { "10_solution", "subtab_y" };
			Tab10PartZTilesRelativePath = new[] { "10_solution", "subtab_z" };
			#endregion

			#region Tab 11
			DataClosersPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11A.xml"));
			DataClosersPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11B.xml"));
			DataClosersPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11C.xml"));

			Tab11PartKSlidesRelativePath = new[] { "11_closers", "subtab_k" };
			Tab11PartLSlidesRelativePath = new[] { "11_closers", "subtab_l" };
			Tab11PartMSlidesRelativePath = new[] { "11_closers", "subtab_m" };
			Tab11PartNSlidesRelativePath = new[] { "11_closers", "subtab_n" };
			Tab11PartOSlidesRelativePath = new[] { "11_closers", "subtab_o" };
			Tab11PartUSlidesRelativePath = new[] { "11_closers", "subtab_u" };
			Tab11PartVSlidesRelativePath = new[] { "11_closers", "subtab_v" };
			Tab11PartWSlidesRelativePath = new[] { "11_closers", "subtab_w" };
			Tab11PartXTilesRelativePath = new[] { "11_closers", "subtab_x" };
			Tab11PartYTilesRelativePath = new[] { "11_closers", "subtab_y" };
			Tab11PartZTilesRelativePath = new[] { "11_closers", "subtab_z" };
			#endregion
		}

		public void LoadGraphicResources()
		{
			if (GraphicResources != null) return;
			if (!_graphicResourcesFile.ExistsLocal()) return;
			var assembly = Assembly.LoadFile(_graphicResourcesFile.LocalPath);
			GraphicResources = assembly.CreateInstance("Asa.Solutions.StarApp.Resources.ResourceContainer") as IStarAppGraphicResources;
		}

		public void ReleaseGraphicResources()
		{
			GraphicResources = null;
		}
	}
}
