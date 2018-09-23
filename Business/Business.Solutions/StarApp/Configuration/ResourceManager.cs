using System.Reflection;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Resources.Solutions.StarApp;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ResourceManager
	{
		private StorageFile _graphicResourcesFile;

		public StorageFile SettingsFile { get; private set; }

		public StorageFile DataUsersFile { get; private set; }
		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataTargetCustomersFile { get; private set; }

		public IStarAppGraphicResources GraphicResources { get; private set; }

		#region Tab 1
		public StorageFile DataCoverPartAFile { get; private set; }

		public StorageDirectory Tab1PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartXTilesFolder { get; private set; }
		public StorageDirectory Tab1PartYTilesFolder { get; private set; }
		public StorageDirectory Tab1PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 2
		public StorageFile DataCNAPartAFile { get; private set; }
		public StorageFile DataCNAPartBFile { get; private set; }

		public StorageDirectory Tab2PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartXTilesFolder { get; private set; }
		public StorageDirectory Tab2PartYTilesFolder { get; private set; }
		public StorageDirectory Tab2PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 3
		public StorageFile DataFishingPartAFile { get; private set; }
		public StorageFile DataFishingPartBFile { get; private set; }
		public StorageFile DataFishingPartCFile { get; private set; }

		public StorageDirectory Tab3PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartXTilesFolder { get; private set; }
		public StorageDirectory Tab3PartYTilesFolder { get; private set; }
		public StorageDirectory Tab3PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 4
		public StorageFile DataCustomerPartAFile { get; private set; }
		public StorageFile DataCustomerPartBFile { get; private set; }
		public StorageFile DataCustomerPartCFile { get; private set; }

		public StorageDirectory Tab4PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartXTilesFolder { get; private set; }
		public StorageDirectory Tab4PartYTilesFolder { get; private set; }
		public StorageDirectory Tab4PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 5
		public StorageFile DataSharePartAFile { get; private set; }
		public StorageFile DataSharePartBFile { get; private set; }
		public StorageFile DataSharePartCFile { get; private set; }
		public StorageFile DataSharePartDFile { get; private set; }
		public StorageFile DataSharePartEFile { get; private set; }

		public StorageDirectory Tab5PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartXTilesFolder { get; private set; }
		public StorageDirectory Tab5PartYTilesFolder { get; private set; }
		public StorageDirectory Tab5PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 6
		public StorageFile DataROIPartAFile { get; private set; }
		public StorageFile DataROIPartBFile { get; private set; }
		public StorageFile DataROIPartCFile { get; private set; }
		public StorageFile DataROIPartDFile { get; private set; }

		public StorageDirectory Tab6PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartXTilesFolder { get; private set; }
		public StorageDirectory Tab6PartYTilesFolder { get; private set; }
		public StorageDirectory Tab6PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 7
		public StorageFile DataMarketPartAFile { get; private set; }
		public StorageFile DataMarketPartBFile { get; private set; }
		public StorageFile DataMarketPartCFile { get; private set; }

		public StorageDirectory Tab7PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartXTilesFolder { get; private set; }
		public StorageDirectory Tab7PartYTilesFolder { get; private set; }
		public StorageDirectory Tab7PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 8
		public StorageFile DataVideoPartAFile { get; private set; }
		public StorageFile DataVideoPartBFile { get; private set; }
		public StorageFile DataVideoPartCFile { get; private set; }
		public StorageFile DataVideoPartDFile { get; private set; }

		public StorageDirectory Tab8PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartXTilesFolder { get; private set; }
		public StorageDirectory Tab8PartYTilesFolder { get; private set; }
		public StorageDirectory Tab8PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 9
		public StorageFile DataAudiencePartAFile { get; private set; }
		public StorageFile DataAudiencePartBFile { get; private set; }
		public StorageFile DataAudiencePartCFile { get; private set; }

		public StorageDirectory Tab9PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartXTilesFolder { get; private set; }
		public StorageDirectory Tab9PartYTilesFolder { get; private set; }
		public StorageDirectory Tab9PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 10
		public StorageFile DataSolutionPartAFile { get; private set; }
		public StorageFile DataSolutionPartBFile { get; private set; }
		public StorageFile DataSolutionPartCFile { get; private set; }
		public StorageFile DataSolutionPartDFile { get; private set; }

		public StorageDirectory Tab10PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartXTilesFolder { get; private set; }
		public StorageDirectory Tab10PartYTilesFolder { get; private set; }
		public StorageDirectory Tab10PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 11
		public StorageFile DataClosersPartAFile { get; private set; }
		public StorageFile DataClosersPartBFile { get; private set; }
		public StorageFile DataClosersPartCFile { get; private set; }

		public StorageDirectory Tab11PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartXTilesFolder { get; private set; }
		public StorageDirectory Tab11PartYTilesFolder { get; private set; }
		public StorageDirectory Tab11PartZTilesFolder { get; private set; }
		#endregion

		public void Init(StorageDirectory dataFolder)
		{
			_graphicResourcesFile = new StorageFile(dataFolder.RelativePathParts.Merge("StarApp.Resources.dll"));

			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			DataUsersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Users.xml"));
			DataClientGoalsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			DataTargetCustomersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Target Customer.xml"));

			#region Tab 1
			DataCoverPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP01A.xml"));

			Tab1PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_k" }));
			Tab1PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_l" }));
			Tab1PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_m" }));
			Tab1PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_n" }));
			Tab1PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_o" }));
			Tab1PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_u" }));
			Tab1PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_v" }));
			Tab1PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_w" }));
			Tab1PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_x" }));
			Tab1PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_y" }));
			Tab1PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_z" }));
			#endregion

			#region Tab 2
			DataCNAPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP02A.xml"));
			DataCNAPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP02B.xml"));

			Tab2PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_k" }));
			Tab2PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_l" }));
			Tab2PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_m" }));
			Tab2PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_n" }));
			Tab2PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_o" }));
			Tab2PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_u" }));
			Tab2PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_v" }));
			Tab2PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_w" }));
			Tab2PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_x" }));
			Tab2PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_y" }));
			Tab2PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_cna", "subtab_z" }));
			#endregion

			#region Tab 3
			DataFishingPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03A.xml"));
			DataFishingPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03B.xml"));
			DataFishingPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP03C.xml"));

			Tab3PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_k" }));
			Tab3PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_l" }));
			Tab3PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_m" }));
			Tab3PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_n" }));
			Tab3PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_o" }));
			Tab3PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_u" }));
			Tab3PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_v" }));
			Tab3PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_w" }));
			Tab3PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_x" }));
			Tab3PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_y" }));
			Tab3PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_fishing", "subtab_z" }));
			#endregion

			#region Tab 4
			DataCustomerPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04A.xml"));
			DataCustomerPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04B.xml"));
			DataCustomerPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP04C.xml"));

			Tab4PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_k" }));
			Tab4PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_l" }));
			Tab4PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_m" }));
			Tab4PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_n" }));
			Tab4PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_o" }));
			Tab4PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_u" }));
			Tab4PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_v" }));
			Tab4PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_w" }));
			Tab4PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_x" }));
			Tab4PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_y" }));
			Tab4PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_customer", "subtab_z" }));
			#endregion

			#region Tab 5
			DataSharePartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05A.xml"));
			DataSharePartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05B.xml"));
			DataSharePartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05C.xml"));
			DataSharePartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05D.xml"));
			DataSharePartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP05E.xml"));

			Tab5PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_k" }));
			Tab5PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_l" }));
			Tab5PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_m" }));
			Tab5PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_n" }));
			Tab5PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_o" }));
			Tab5PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_u" }));
			Tab5PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_v" }));
			Tab5PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_w" }));
			Tab5PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_x" }));
			Tab5PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_y" }));
			Tab5PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_share", "subtab_z" }));
			#endregion

			#region Tab 6
			DataROIPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06A.xml"));
			DataROIPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06B.xml"));
			DataROIPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06C.xml"));
			DataROIPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06D.xml"));

			Tab6PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_k" }));
			Tab6PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_l" }));
			Tab6PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_m" }));
			Tab6PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_n" }));
			Tab6PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_o" }));
			Tab6PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_u" }));
			Tab6PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_v" }));
			Tab6PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_w" }));
			Tab6PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_x" }));
			Tab6PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_y" }));
			Tab6PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_roi", "subtab_z" }));
			#endregion

			#region Tab 7
			DataMarketPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07A.xml"));
			DataMarketPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07B.xml"));
			DataMarketPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP07C.xml"));

			Tab7PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_k" }));
			Tab7PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_l" }));
			Tab7PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_m" }));
			Tab7PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_n" }));
			Tab7PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_o" }));
			Tab7PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_u" }));
			Tab7PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_v" }));
			Tab7PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_w" }));
			Tab7PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_x" }));
			Tab7PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_y" }));
			Tab7PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_market", "subtab_z" }));
			#endregion

			#region Tab 8
			DataVideoPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08A.xml"));
			DataVideoPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08B.xml"));
			DataVideoPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08C.xml"));
			DataVideoPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP08D.xml"));

			Tab8PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_k" }));
			Tab8PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_l" }));
			Tab8PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_m" }));
			Tab8PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_n" }));
			Tab8PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_o" }));
			Tab8PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_u" }));
			Tab8PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_v" }));
			Tab8PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_w" }));
			Tab8PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_x" }));
			Tab8PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_y" }));
			Tab8PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_video", "subtab_z" }));
			#endregion

			#region Tab 9
			DataAudiencePartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09A.xml"));
			DataAudiencePartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09B.xml"));
			DataAudiencePartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP09C.xml"));

			Tab9PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_k" }));
			Tab9PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_l" }));
			Tab9PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_m" }));
			Tab9PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_n" }));
			Tab9PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_o" }));
			Tab9PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_u" }));
			Tab9PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_v" }));
			Tab9PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_w" }));
			Tab9PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_x" }));
			Tab9PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_y" }));
			Tab9PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_audience", "subtab_z" }));
			#endregion

			#region Tab 10
			DataSolutionPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10A.xml"));
			DataSolutionPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10B.xml"));
			DataSolutionPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10C.xml"));
			DataSolutionPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP10D.xml"));

			Tab10PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_k" }));
			Tab10PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_l" }));
			Tab10PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_m" }));
			Tab10PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_n" }));
			Tab10PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_o" }));
			Tab10PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_u" }));
			Tab10PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_v" }));
			Tab10PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_w" }));
			Tab10PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_x" }));
			Tab10PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_y" }));
			Tab10PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_solution", "subtab_z" }));
			#endregion

			#region Tab 11
			DataClosersPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11A.xml"));
			DataClosersPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11B.xml"));
			DataClosersPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP11C.xml"));

			Tab11PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_k" }));
			Tab11PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_l" }));
			Tab11PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_m" }));
			Tab11PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_n" }));
			Tab11PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_o" }));
			Tab11PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_u" }));
			Tab11PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_v" }));
			Tab11PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_w" }));
			Tab11PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_x" }));
			Tab11PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_y" }));
			Tab11PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_closers", "subtab_z" }));
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
