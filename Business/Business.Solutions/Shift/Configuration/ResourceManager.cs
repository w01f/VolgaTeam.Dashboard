using System.Linq;
using System.Reflection;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Resources.Solutions.Shift;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ResourceManager
	{
		private StorageFile _graphicResourcesFile;

		public StorageFile SettingsFile { get; private set; }

		public StorageDirectory LocalDataFolder { get; private set; }
		public StorageDirectory RemoteResourcesFolder { get; private set; }

		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataHHIFile { get; private set; }
		public StorageFile DataDemoFile { get; private set; }
		public StorageFile DataGeographyFile { get; private set; }
		public StorageFile DataNeedsCommonFile { get; private set; }
		public StorageFile DataSolutionsCommonFile { get; private set; }
		public StorageFile DataApproachesCommonFile { get; private set; }
		public StorageFile DataCBCCommonFile { get; private set; }
		public StorageFile DataProofOfPerformanceCommonFile { get; private set; }
		public StorageFile DataNextStepsCommonFile { get; private set; }
		public StorageFile DataAgreementCommonFile { get; private set; }

		public IShiftGraphicResources GraphicResources { get; private set; }

		#region Tab 1
		public StorageFile DataCoverPartAFile { get; private set; }
		public StorageFile DataCoverPartBFile { get; private set; }
		public StorageFile DataCoverPartCFile { get; private set; }
		public StorageFile DataCoverPartDFile { get; private set; }
		public StorageFile DataCoverPartEFile { get; private set; }

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
		public StorageFile DataIntroPartAFile { get; private set; }
		public StorageFile DataIntroPartBFile { get; private set; }
		public StorageFile DataIntroPartCFile { get; private set; }
		public StorageFile DataIntroPartDFile { get; private set; }

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
		public StorageFile DataAgendaPartAFile { get; private set; }
		public StorageFile DataAgendaPartBFile { get; private set; }
		public StorageFile DataAgendaPartCFile { get; private set; }
		public StorageFile DataAgendaPartDFile { get; private set; }
		public StorageFile DataAgendaPartEFile { get; private set; }

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
		public StorageFile DataGoalsPartAFile { get; private set; }
		public StorageFile DataGoalsPartBFile { get; private set; }
		public StorageFile DataGoalsPartCFile { get; private set; }
		public StorageFile DataGoalsPartDFile { get; private set; }

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
		public StorageFile DataMarketPartAFile { get; private set; }
		public StorageFile DataMarketPartBFile { get; private set; }
		public StorageFile DataMarketPartCFile { get; private set; }
		public StorageFile DataMarketPartDFile { get; private set; }
		public StorageFile DataMarketPartEFile { get; private set; }

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
		public StorageFile DataPartnershipPartAFile { get; private set; }
		public StorageFile DataPartnershipPartBFile { get; private set; }
		public StorageFile DataPartnershipPartCFile { get; private set; }
		public StorageFile DataPartnershipPartDFile { get; private set; }

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
		public StorageFile DataNeedsSolutionsPartAFile { get; private set; }
		public StorageFile DataNeedsSolutionsPartBFile { get; private set; }
		public StorageFile DataNeedsSolutionsPartCFile { get; private set; }
		public StorageFile DataNeedsSolutionsPartDFile { get; private set; }
		public StorageFile DataNeedsSolutionsPartEFile { get; private set; }
		public StorageFile DataNeedsSolutionsPartFFile { get; private set; }

		public StorageDirectory ClipartTab7SubAFolder { get; private set; }
		public StorageDirectory ClipartTab7SubCFolder { get; private set; }
		public StorageDirectory ClipartTab7SubEFolder { get; private set; }
		public StorageDirectory ClipartTab7SubFFolder { get; private set; }

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
		public StorageFile DataCBCPartAFile { get; private set; }
		public StorageFile DataCBCPartBFile { get; private set; }
		public StorageFile DataCBCPartCFile { get; private set; }
		public StorageFile DataCBCPartDFile { get; private set; }
		public StorageFile DataCBCPartEFile { get; private set; }
		public StorageFile DataCBCPartFFile { get; private set; }

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
		public StorageFile DataIntegratedSolutionOutputConditionsFile { get; private set; }

		public StorageDirectory ClipartTab9SharedFolder { get; private set; }

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
		public StorageFile DataInvestmentPartAFile { get; private set; }
		public StorageFile DataInvestmentPartBFile { get; private set; }
		public StorageFile DataInvestmentPartCFile { get; private set; }
		public StorageFile DataInvestmentPartDFile { get; private set; }
		public StorageFile DataInvestmentPartEFile { get; private set; }
		public StorageFile DataInvestmentPartFFile { get; private set; }

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
		public StorageFile DataNextStepsPartAFile { get; private set; }
		public StorageFile DataNextStepsPartBFile { get; private set; }
		public StorageFile DataNextStepsPartCFile { get; private set; }
		public StorageFile DataNextStepsPartDFile { get; private set; }
		public StorageFile DataNextStepsPartEFile { get; private set; }
		public StorageFile DataNextStepsPartFFile { get; private set; }
		public StorageFile DataNextStepsPartGFile { get; private set; }
		public StorageFile DataNextStepsPartHFile { get; private set; }
		public StorageFile DataNextStepsPartIFile { get; private set; }

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

		#region Tab 12
		public StorageFile DataContractPartAFile { get; private set; }
		public StorageFile DataContractPartBFile { get; private set; }
		public StorageFile DataContractPartCFile { get; private set; }
		public StorageFile DataContractPartDFile { get; private set; }

		public StorageDirectory ClipartTab15DUsersFolder { get; private set; }

		public string[] Tab12PartKSlidesRelativePath { get; private set; }
		public string[] Tab12PartLSlidesRelativePath { get; private set; }
		public string[] Tab12PartMSlidesRelativePath { get; private set; }
		public string[] Tab12PartNSlidesRelativePath { get; private set; }
		public string[] Tab12PartOSlidesRelativePath { get; private set; }
		public string[] Tab12PartUSlidesRelativePath { get; private set; }
		public string[] Tab12PartVSlidesRelativePath { get; private set; }
		public string[] Tab12PartWSlidesRelativePath { get; private set; }
		public string[] Tab12PartXTilesRelativePath { get; private set; }
		public string[] Tab12PartYTilesRelativePath { get; private set; }
		public string[] Tab12PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 13
		public string[] Tab13PartKSlidesRelativePath { get; private set; }
		public string[] Tab13PartLSlidesRelativePath { get; private set; }
		public string[] Tab13PartMSlidesRelativePath { get; private set; }
		public string[] Tab13PartNSlidesRelativePath { get; private set; }
		public string[] Tab13PartOSlidesRelativePath { get; private set; }
		public string[] Tab13PartUSlidesRelativePath { get; private set; }
		public string[] Tab13PartVSlidesRelativePath { get; private set; }
		public string[] Tab13PartWSlidesRelativePath { get; private set; }
		public string[] Tab13PartXTilesRelativePath { get; private set; }
		public string[] Tab13PartYTilesRelativePath { get; private set; }
		public string[] Tab13PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 14
		public string[] Tab14PartKSlidesRelativePath { get; private set; }
		public string[] Tab14PartLSlidesRelativePath { get; private set; }
		public string[] Tab14PartMSlidesRelativePath { get; private set; }
		public string[] Tab14PartNSlidesRelativePath { get; private set; }
		public string[] Tab14PartOSlidesRelativePath { get; private set; }
		public string[] Tab14PartUSlidesRelativePath { get; private set; }
		public string[] Tab14PartVSlidesRelativePath { get; private set; }
		public string[] Tab14PartWSlidesRelativePath { get; private set; }
		public string[] Tab14PartXTilesRelativePath { get; private set; }
		public string[] Tab14PartYTilesRelativePath { get; private set; }
		public string[] Tab14PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 15
		public StorageFile DataApproachPartAFile { get; private set; }
		public StorageFile DataApproachPartBFile { get; private set; }
		public StorageFile DataApproachPartCFile { get; private set; }

		public StorageDirectory ClipartTab15SubAFolder { get; private set; }
		public StorageDirectory ClipartTab15SubCFolder { get; private set; }

		public string[] Tab15PartKSlidesRelativePath { get; private set; }
		public string[] Tab15PartLSlidesRelativePath { get; private set; }
		public string[] Tab15PartMSlidesRelativePath { get; private set; }
		public string[] Tab15PartNSlidesRelativePath { get; private set; }
		public string[] Tab15PartOSlidesRelativePath { get; private set; }
		public string[] Tab15PartUSlidesRelativePath { get; private set; }
		public string[] Tab15PartVSlidesRelativePath { get; private set; }
		public string[] Tab15PartWSlidesRelativePath { get; private set; }
		public string[] Tab15PartXTilesRelativePath { get; private set; }
		public string[] Tab15PartYTilesRelativePath { get; private set; }
		public string[] Tab15PartZTilesRelativePath { get; private set; }
		#endregion

		#region Tab 16
		public StorageFile DataROIPartAFile { get; private set; }
		public StorageFile DataROIPartBFile { get; private set; }
		public StorageFile DataROIPartCFile { get; private set; }
		public StorageFile DataROIPartDFile { get; private set; }
		public StorageFile DataROIPartEFile { get; private set; }

		public string[] Tab16PartKSlidesRelativePath { get; private set; }
		public string[] Tab16PartLSlidesRelativePath { get; private set; }
		public string[] Tab16PartMSlidesRelativePath { get; private set; }
		public string[] Tab16PartNSlidesRelativePath { get; private set; }
		public string[] Tab16PartOSlidesRelativePath { get; private set; }
		public string[] Tab16PartUSlidesRelativePath { get; private set; }
		public string[] Tab16PartVSlidesRelativePath { get; private set; }
		public string[] Tab16PartWSlidesRelativePath { get; private set; }
		public string[] Tab16PartXTilesRelativePath { get; private set; }
		public string[] Tab16PartYTilesRelativePath { get; private set; }
		public string[] Tab16PartZTilesRelativePath { get; private set; }
		#endregion

		public void Init(StorageDirectory dataFolder)
		{
			_graphicResourcesFile = new StorageFile(dataFolder.RelativePathParts.Merge("Shift.Resources.dll"));

			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			LocalDataFolder = dataFolder;
			RemoteResourcesFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Solution Templates",
				dataFolder.RelativePathParts.Last()
			});

			DataClientGoalsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			DataHHIFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Income.xml"));
			DataDemoFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Age.xml"));
			DataGeographyFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Residence.xml"));
			DataNeedsCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Common Needs.xml"));
			DataSolutionsCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Product List.xml"));
			DataApproachesCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Approach List.xml"));
			DataCBCCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT CBC.xml"));
			DataProofOfPerformanceCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Proof of Performance.xml"));
			DataNextStepsCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Next Steps.xml"));
			DataAgreementCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Agreement List.xml"));

			#region Tab 1
			DataCoverPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01A.xml"));
			DataCoverPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01B.xml"));
			DataCoverPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01C.xml"));
			DataCoverPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01D.xml"));
			DataCoverPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01E.xml"));

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
			DataIntroPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02A.xml"));
			DataIntroPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02B.xml"));
			DataIntroPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02C.xml"));
			DataIntroPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02D.xml"));

			Tab2PartKSlidesRelativePath = new[] { "02_intro", "subtab_k" };
			Tab2PartLSlidesRelativePath = new[] { "02_intro", "subtab_l" };
			Tab2PartMSlidesRelativePath = new[] { "02_intro", "subtab_m" };
			Tab2PartNSlidesRelativePath = new[] { "02_intro", "subtab_n" };
			Tab2PartOSlidesRelativePath = new[] { "02_intro", "subtab_o" };
			Tab2PartUSlidesRelativePath = new[] { "02_intro", "subtab_u" };
			Tab2PartVSlidesRelativePath = new[] { "02_intro", "subtab_v" };
			Tab2PartWSlidesRelativePath = new[] { "02_intro", "subtab_w" };
			Tab2PartXTilesRelativePath = new[] { "02_intro", "subtab_x" };
			Tab2PartYTilesRelativePath = new[] { "02_intro", "subtab_y" };
			Tab2PartZTilesRelativePath = new[] { "02_intro", "subtab_z" };
			#endregion

			#region Tab 3
			DataAgendaPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03A.xml"));
			DataAgendaPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03B.xml"));
			DataAgendaPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03C.xml"));
			DataAgendaPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03D.xml"));
			DataAgendaPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03E.xml"));

			Tab3PartKSlidesRelativePath = new[] { "03_agenda", "subtab_k" };
			Tab3PartLSlidesRelativePath = new[] { "03_agenda", "subtab_l" };
			Tab3PartMSlidesRelativePath = new[] { "03_agenda", "subtab_m" };
			Tab3PartNSlidesRelativePath = new[] { "03_agenda", "subtab_n" };
			Tab3PartOSlidesRelativePath = new[] { "03_agenda", "subtab_o" };
			Tab3PartUSlidesRelativePath = new[] { "03_agenda", "subtab_u" };
			Tab3PartVSlidesRelativePath = new[] { "03_agenda", "subtab_v" };
			Tab3PartWSlidesRelativePath = new[] { "03_agenda", "subtab_w" };
			Tab3PartXTilesRelativePath = new[] { "03_agenda", "subtab_x" };
			Tab3PartYTilesRelativePath = new[] { "03_agenda", "subtab_y" };
			Tab3PartZTilesRelativePath = new[] { "03_agenda", "subtab_z" };
			#endregion

			#region Tab 4
			DataGoalsPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04A.xml"));
			DataGoalsPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04B.xml"));
			DataGoalsPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04C.xml"));
			DataGoalsPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04D.xml"));

			Tab4PartKSlidesRelativePath = new[] { "04_goals", "subtab_k" };
			Tab4PartLSlidesRelativePath = new[] { "04_goals", "subtab_l" };
			Tab4PartMSlidesRelativePath = new[] { "04_goals", "subtab_m" };
			Tab4PartNSlidesRelativePath = new[] { "04_goals", "subtab_n" };
			Tab4PartOSlidesRelativePath = new[] { "04_goals", "subtab_o" };
			Tab4PartUSlidesRelativePath = new[] { "04_goals", "subtab_u" };
			Tab4PartVSlidesRelativePath = new[] { "04_goals", "subtab_v" };
			Tab4PartWSlidesRelativePath = new[] { "04_goals", "subtab_w" };
			Tab4PartXTilesRelativePath = new[] { "04_goals", "subtab_x" };
			Tab4PartYTilesRelativePath = new[] { "04_goals", "subtab_y" };
			Tab4PartZTilesRelativePath = new[] { "04_goals", "subtab_z" };
			#endregion

			#region Tab 5
			DataMarketPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05A.xml"));
			DataMarketPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05B.xml"));
			DataMarketPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05C.xml"));
			DataMarketPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05D.xml"));
			DataMarketPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05E.xml"));

			Tab5PartKSlidesRelativePath = new[] { "05_market_opportunity", "subtab_k" };
			Tab5PartLSlidesRelativePath = new[] { "05_market_opportunity", "subtab_l" };
			Tab5PartMSlidesRelativePath = new[] { "05_market_opportunity", "subtab_m" };
			Tab5PartNSlidesRelativePath = new[] { "05_market_opportunity", "subtab_n" };
			Tab5PartOSlidesRelativePath = new[] { "05_market_opportunity", "subtab_o" };
			Tab5PartUSlidesRelativePath = new[] { "05_market_opportunity", "subtab_u" };
			Tab5PartVSlidesRelativePath = new[] { "05_market_opportunity", "subtab_v" };
			Tab5PartWSlidesRelativePath = new[] { "05_market_opportunity", "subtab_w" };
			Tab5PartXTilesRelativePath = new[] { "05_market_opportunity", "subtab_x" };
			Tab5PartYTilesRelativePath = new[] { "05_market_opportunity", "subtab_y" };
			Tab5PartZTilesRelativePath = new[] { "05_market_opportunity", "subtab_z" };
			#endregion

			#region Tab 6
			DataPartnershipPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06A.xml"));
			DataPartnershipPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06B.xml"));
			DataPartnershipPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06C.xml"));
			DataPartnershipPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06D.xml"));

			Tab6PartKSlidesRelativePath = new[] { "06_our_partnership", "subtab_k" };
			Tab6PartLSlidesRelativePath = new[] { "06_our_partnership", "subtab_l" };
			Tab6PartMSlidesRelativePath = new[] { "06_our_partnership", "subtab_m" };
			Tab6PartNSlidesRelativePath = new[] { "06_our_partnership", "subtab_n" };
			Tab6PartOSlidesRelativePath = new[] { "06_our_partnership", "subtab_o" };
			Tab6PartUSlidesRelativePath = new[] { "06_our_partnership", "subtab_u" };
			Tab6PartVSlidesRelativePath = new[] { "06_our_partnership", "subtab_v" };
			Tab6PartWSlidesRelativePath = new[] { "06_our_partnership", "subtab_w" };
			Tab6PartXTilesRelativePath = new[] { "06_our_partnership", "subtab_x" };
			Tab6PartYTilesRelativePath = new[] { "06_our_partnership", "subtab_y" };
			Tab6PartZTilesRelativePath = new[] { "06_our_partnership", "subtab_z" };
			#endregion

			#region Tab 7
			DataNeedsSolutionsPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT07A.xml"));
			DataNeedsSolutionsPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT07B.xml"));
			DataNeedsSolutionsPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT07C.xml"));
			DataNeedsSolutionsPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT07D.xml"));
			DataNeedsSolutionsPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT07E.xml"));
			DataNeedsSolutionsPartFFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT07F.xml"));

			ClipartTab7SubAFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "shared", "needs" }));
			ClipartTab7SubCFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "shared", "benefits" }));
			ClipartTab7SubEFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "shared", "needs" }));
			ClipartTab7SubFFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "shared", "benefits" }));

			Tab7PartKSlidesRelativePath = new[] { "07_needs_solutions", "subtab_k" };
			Tab7PartLSlidesRelativePath = new[] { "07_needs_solutions", "subtab_l" };
			Tab7PartMSlidesRelativePath = new[] { "07_needs_solutions", "subtab_m" };
			Tab7PartNSlidesRelativePath = new[] { "07_needs_solutions", "subtab_n" };
			Tab7PartOSlidesRelativePath = new[] { "07_needs_solutions", "subtab_o" };
			Tab7PartUSlidesRelativePath = new[] { "07_needs_solutions", "subtab_u" };
			Tab7PartVSlidesRelativePath = new[] { "07_needs_solutions", "subtab_v" };
			Tab7PartWSlidesRelativePath = new[] { "07_needs_solutions", "subtab_w" };
			Tab7PartXTilesRelativePath = new[] { "07_needs_solutions", "subtab_x" };
			Tab7PartYTilesRelativePath = new[] { "07_needs_solutions", "subtab_y" };
			Tab7PartZTilesRelativePath = new[] { "07_needs_solutions", "subtab_z" };
			#endregion

			#region Tab 8
			DataCBCPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08A.xml"));
			DataCBCPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08B.xml"));
			DataCBCPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08C.xml"));
			DataCBCPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08D.xml"));
			DataCBCPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08E.xml"));
			DataCBCPartFFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08F.xml"));

			Tab8PartKSlidesRelativePath = new[] { "08_cbc", "subtab_k" };
			Tab8PartLSlidesRelativePath = new[] { "08_cbc", "subtab_l" };
			Tab8PartMSlidesRelativePath = new[] { "08_cbc", "subtab_m" };
			Tab8PartNSlidesRelativePath = new[] { "08_cbc", "subtab_n" };
			Tab8PartOSlidesRelativePath = new[] { "08_cbc", "subtab_o" };
			Tab8PartUSlidesRelativePath = new[] { "08_cbc", "subtab_u" };
			Tab8PartVSlidesRelativePath = new[] { "08_cbc", "subtab_v" };
			Tab8PartWSlidesRelativePath = new[] { "08_cbc", "subtab_w" };
			Tab8PartXTilesRelativePath = new[] { "08_cbc", "subtab_x" };
			Tab8PartYTilesRelativePath = new[] { "08_cbc", "subtab_y" };
			Tab8PartZTilesRelativePath = new[] { "08_cbc", "subtab_z" };
			#endregion

			#region Tab 9
			DataIntegratedSolutionOutputConditionsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT10 Slide Output Rules.xml"));

			ClipartTab9SharedFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "shared" }));

			Tab9PartKSlidesRelativePath = new[] { "10_integrated_solution", "subtab_k" };
			Tab9PartLSlidesRelativePath = new[] { "10_integrated_solution", "subtab_l" };
			Tab9PartMSlidesRelativePath = new[] { "10_integrated_solution", "subtab_m" };
			Tab9PartNSlidesRelativePath = new[] { "10_integrated_solution", "subtab_n" };
			Tab9PartOSlidesRelativePath = new[] { "10_integrated_solution", "subtab_o" };
			Tab9PartUSlidesRelativePath = new[] { "10_integrated_solution", "subtab_u" };
			Tab9PartVSlidesRelativePath = new[] { "10_integrated_solution", "subtab_v" };
			Tab9PartWSlidesRelativePath = new[] { "10_integrated_solution", "subtab_w" };
			Tab9PartXTilesRelativePath = new[] { "10_integrated_solution", "subtab_x" };
			Tab9PartYTilesRelativePath = new[] { "10_integrated_solution", "subtab_y" };
			Tab9PartZTilesRelativePath = new[] { "10_integrated_solution", "subtab_z" };
			#endregion

			#region Tab 10
			DataInvestmentPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT12A.xml"));
			DataInvestmentPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT12B.xml"));
			DataInvestmentPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT12C.xml"));
			DataInvestmentPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT12D.xml"));
			DataInvestmentPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT12E.xml"));
			DataInvestmentPartFFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT12F.xml"));

			Tab10PartKSlidesRelativePath = new[] { "12_investment", "subtab_k" };
			Tab10PartLSlidesRelativePath = new[] { "12_investment", "subtab_l" };
			Tab10PartMSlidesRelativePath = new[] { "12_investment", "subtab_m" };
			Tab10PartNSlidesRelativePath = new[] { "12_investment", "subtab_n" };
			Tab10PartOSlidesRelativePath = new[] { "12_investment", "subtab_o" };
			Tab10PartUSlidesRelativePath = new[] { "12_investment", "subtab_u" };
			Tab10PartVSlidesRelativePath = new[] { "12_investment", "subtab_v" };
			Tab10PartWSlidesRelativePath = new[] { "12_investment", "subtab_w" };
			Tab10PartXTilesRelativePath = new[] { "12_investment", "subtab_x" };
			Tab10PartYTilesRelativePath = new[] { "12_investment", "subtab_y" };
			Tab10PartZTilesRelativePath = new[] { "12_investment", "subtab_z" };
			#endregion

			#region Tab 11
			DataNextStepsPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14A.xml"));
			DataNextStepsPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14B.xml"));
			DataNextStepsPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14C.xml"));
			DataNextStepsPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14D.xml"));
			DataNextStepsPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14E.xml"));
			DataNextStepsPartFFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14F.xml"));
			DataNextStepsPartGFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14G.xml"));
			DataNextStepsPartHFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14H.xml"));
			DataNextStepsPartIFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT14I.xml"));

			Tab11PartKSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_k" };
			Tab11PartLSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_l" };
			Tab11PartMSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_m" };
			Tab11PartNSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_n" };
			Tab11PartOSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_o" };
			Tab11PartUSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_u" };
			Tab11PartVSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_v" };
			Tab11PartWSlidesRelativePath = new[] { "14_relationship_next_steps", "subtab_w" };
			Tab11PartXTilesRelativePath = new[] { "14_relationship_next_steps", "subtab_x" };
			Tab11PartYTilesRelativePath = new[] { "14_relationship_next_steps", "subtab_y" };
			Tab11PartZTilesRelativePath = new[] { "14_relationship_next_steps", "subtab_z" };
			#endregion

			#region Tab 12
			DataContractPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT15A.xml"));
			DataContractPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT15B.xml"));
			DataContractPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT15C.xml"));
			DataContractPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT15D.xml"));

			ClipartTab15DUsersFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_d", "Users" }));

			Tab12PartKSlidesRelativePath = new[] { "15_agreement_contract", "subtab_k" };
			Tab12PartLSlidesRelativePath = new[] { "15_agreement_contract", "subtab_l" };
			Tab12PartMSlidesRelativePath = new[] { "15_agreement_contract", "subtab_m" };
			Tab12PartNSlidesRelativePath = new[] { "15_agreement_contract", "subtab_n" };
			Tab12PartOSlidesRelativePath = new[] { "15_agreement_contract", "subtab_o" };
			Tab12PartUSlidesRelativePath = new[] { "15_agreement_contract", "subtab_u" };
			Tab12PartVSlidesRelativePath = new[] { "15_agreement_contract", "subtab_v" };
			Tab12PartWSlidesRelativePath = new[] { "15_agreement_contract", "subtab_w" };
			Tab12PartXTilesRelativePath = new[] { "15_agreement_contract", "subtab_x" };
			Tab12PartYTilesRelativePath = new[] { "15_agreement_contract", "subtab_y" };
			Tab12PartZTilesRelativePath = new[] { "15_agreement_contract", "subtab_z" };
			#endregion

			#region Tab 13
			Tab13PartKSlidesRelativePath = new[] { "16_support_materials", "subtab_k" };
			Tab13PartLSlidesRelativePath = new[] { "16_support_materials", "subtab_l" };
			Tab13PartMSlidesRelativePath = new[] { "16_support_materials", "subtab_m" };
			Tab13PartNSlidesRelativePath = new[] { "16_support_materials", "subtab_n" };
			Tab13PartOSlidesRelativePath = new[] { "16_support_materials", "subtab_o" };
			Tab13PartUSlidesRelativePath = new[] { "16_support_materials", "subtab_u" };
			Tab13PartVSlidesRelativePath = new[] { "16_support_materials", "subtab_v" };
			Tab13PartWSlidesRelativePath = new[] { "16_support_materials", "subtab_w" };
			Tab13PartXTilesRelativePath = new[] { "16_support_materials", "subtab_x" };
			Tab13PartYTilesRelativePath = new[] { "16_support_materials", "subtab_y" };
			Tab13PartZTilesRelativePath = new[] { "16_support_materials", "subtab_z" };
			#endregion

			#region Tab 14
			Tab14PartKSlidesRelativePath = new[] { "11_spec_builder", "subtab_k" };
			Tab14PartLSlidesRelativePath = new[] { "11_spec_builder", "subtab_l" };
			Tab14PartMSlidesRelativePath = new[] { "11_spec_builder", "subtab_m" };
			Tab14PartNSlidesRelativePath = new[] { "11_spec_builder", "subtab_n" };
			Tab14PartOSlidesRelativePath = new[] { "11_spec_builder", "subtab_o" };
			Tab14PartUSlidesRelativePath = new[] { "11_spec_builder", "subtab_u" };
			Tab14PartVSlidesRelativePath = new[] { "11_spec_builder", "subtab_v" };
			Tab14PartWSlidesRelativePath = new[] { "11_spec_builder", "subtab_w" };
			Tab14PartXTilesRelativePath = new[] { "11_spec_builder", "subtab_x" };
			Tab14PartYTilesRelativePath = new[] { "11_spec_builder", "subtab_y" };
			Tab14PartZTilesRelativePath = new[] { "11_spec_builder", "subtab_z" };
			#endregion

			#region Tab 15
			DataApproachPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09A.xml"));
			DataApproachPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09B.xml"));
			DataApproachPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09C.xml"));

			ClipartTab15SubAFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "shared", "approach" }));
			ClipartTab15SubCFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "shared", "approach" }));

			Tab15PartKSlidesRelativePath = new[] { "09_our_approach", "subtab_k" };
			Tab15PartLSlidesRelativePath = new[] { "09_our_approach", "subtab_l" };
			Tab15PartMSlidesRelativePath = new[] { "09_our_approach", "subtab_m" };
			Tab15PartNSlidesRelativePath = new[] { "09_our_approach", "subtab_n" };
			Tab15PartOSlidesRelativePath = new[] { "09_our_approach", "subtab_o" };
			Tab15PartUSlidesRelativePath = new[] { "09_our_approach", "subtab_u" };
			Tab15PartVSlidesRelativePath = new[] { "09_our_approach", "subtab_v" };
			Tab15PartWSlidesRelativePath = new[] { "09_our_approach", "subtab_w" };
			Tab15PartXTilesRelativePath = new[] { "09_our_approach", "subtab_x" };
			Tab15PartYTilesRelativePath = new[] { "09_our_approach", "subtab_y" };
			Tab15PartZTilesRelativePath = new[] { "09_our_approach", "subtab_z" };
			#endregion

			#region Tab 16
			DataROIPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06A.xml"));
			DataROIPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06B.xml"));
			DataROIPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06C.xml"));
			DataROIPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06D.xml"));
			DataROIPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT13E.xml"));

			Tab16PartKSlidesRelativePath = new[] { "13_roi", "subtab_k" };
			Tab16PartLSlidesRelativePath = new[] { "13_roi", "subtab_l" };
			Tab16PartMSlidesRelativePath = new[] { "13_roi", "subtab_m" };
			Tab16PartNSlidesRelativePath = new[] { "13_roi", "subtab_n" };
			Tab16PartOSlidesRelativePath = new[] { "13_roi", "subtab_o" };
			Tab16PartUSlidesRelativePath = new[] { "13_roi", "subtab_u" };
			Tab16PartVSlidesRelativePath = new[] { "13_roi", "subtab_v" };
			Tab16PartWSlidesRelativePath = new[] { "13_roi", "subtab_w" };
			Tab16PartXTilesRelativePath = new[] { "13_roi", "subtab_x" };
			Tab16PartYTilesRelativePath = new[] { "13_roi", "subtab_y" };
			Tab16PartZTilesRelativePath = new[] { "13_roi", "subtab_z" };
			#endregion
		}

		public void LoadGraphicResources()
		{
			if (GraphicResources != null) return;
			if (!_graphicResourcesFile.ExistsLocal()) return;
			var assembly = Assembly.LoadFile(_graphicResourcesFile.LocalPath);
			GraphicResources = assembly.CreateInstance("Asa.Solutions.Shift.Resources.ResourceContainer") as IShiftGraphicResources;
		}

		public void ReleaseGraphicResources()
		{
			GraphicResources = null;
		}
	}
}
