using System;
using System.Reflection;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Resources.Solutions.Shift;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ResourceManager
	{
		private StorageFile _graphicResourcesFile;

		public StorageFile SettingsFile { get; private set; }

		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataHHIFile { get; private set; }
		public StorageFile DataDemoFile { get; private set; }
		public StorageFile DataGeographyFile { get; private set; }
		public StorageFile DataNeedsCommonFile { get; private set; }
		public StorageFile DataSolutionsCommonFile { get; private set; }
		public StorageFile DataApproachesCommonFile { get; private set; }
		public StorageFile DataCBCCommonFile { get; private set; }

		public IShiftGraphicResources GraphicResources { get; private set; }

		#region Tab 1
		public StorageFile DataCoverPartAFile { get; private set; }
		public StorageFile DataCoverPartBFile { get; private set; }
		public StorageFile DataCoverPartCFile { get; private set; }
		public StorageFile DataCoverPartDFile { get; private set; }
		public StorageFile DataCoverPartEFile { get; private set; }

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
		public StorageFile DataIntroPartAFile { get; private set; }
		public StorageFile DataIntroPartBFile { get; private set; }
		public StorageFile DataIntroPartCFile { get; private set; }
		public StorageFile DataIntroPartDFile { get; private set; }

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
		public StorageFile DataAgendaPartAFile { get; private set; }
		public StorageFile DataAgendaPartBFile { get; private set; }
		public StorageFile DataAgendaPartCFile { get; private set; }
		public StorageFile DataAgendaPartDFile { get; private set; }
		public StorageFile DataAgendaPartEFile { get; private set; }

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
		public StorageFile DataGoalsPartAFile { get; private set; }
		public StorageFile DataGoalsPartBFile { get; private set; }
		public StorageFile DataGoalsPartCFile { get; private set; }
		public StorageFile DataGoalsPartDFile { get; private set; }

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
		public StorageFile DataMarketPartAFile { get; private set; }
		public StorageFile DataMarketPartBFile { get; private set; }
		public StorageFile DataMarketPartCFile { get; private set; }
		public StorageFile DataMarketPartDFile { get; private set; }
		public StorageFile DataMarketPartEFile { get; private set; }

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
		public StorageFile DataPartnershipPartAFile { get; private set; }
		public StorageFile DataPartnershipPartBFile { get; private set; }
		public StorageFile DataPartnershipPartCFile { get; private set; }
		public StorageFile DataPartnershipPartDFile { get; private set; }

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
		public StorageFile DataCBCPartAFile { get; private set; }
		public StorageFile DataCBCPartBFile { get; private set; }
		public StorageFile DataCBCPartCFile { get; private set; }
		public StorageFile DataCBCPartDFile { get; private set; }
		public StorageFile DataCBCPartEFile { get; private set; }
		public StorageFile DataCBCPartFFile { get; private set; }

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
		public StorageFile DataIntegratedSolutionOutputConditionsFile { get; private set; }

		public StorageDirectory ClipartTab9SharedFolder { get; private set; }

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

		#region Tab 12
		public StorageDirectory Tab12PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartXTilesFolder { get; private set; }
		public StorageDirectory Tab12PartYTilesFolder { get; private set; }
		public StorageDirectory Tab12PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 13
		public StorageDirectory Tab13PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartXTilesFolder { get; private set; }
		public StorageDirectory Tab13PartYTilesFolder { get; private set; }
		public StorageDirectory Tab13PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 14
		public StorageDirectory Tab14PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartXTilesFolder { get; private set; }
		public StorageDirectory Tab14PartYTilesFolder { get; private set; }
		public StorageDirectory Tab14PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 15
		public StorageFile DataApproachPartAFile { get; private set; }
		public StorageFile DataApproachPartBFile { get; private set; }
		public StorageFile DataApproachPartCFile { get; private set; }

		public StorageDirectory ClipartTab15SubAFolder { get; private set; }
		public StorageDirectory ClipartTab15SubCFolder { get; private set; }

		public StorageDirectory Tab15PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartXTilesFolder { get; private set; }
		public StorageDirectory Tab15PartYTilesFolder { get; private set; }
		public StorageDirectory Tab15PartZTilesFolder { get; private set; }
		#endregion

		#region Tab 16
		public StorageFile DataROIPartAFile { get; private set; }
		public StorageFile DataROIPartBFile { get; private set; }
		public StorageFile DataROIPartCFile { get; private set; }
		public StorageFile DataROIPartDFile { get; private set; }

		public StorageDirectory Tab16PartKSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartLSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartMSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartNSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartOSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartWSlidesFolder { get; private set; }
		public StorageDirectory Tab16PartXTilesFolder { get; private set; }
		public StorageDirectory Tab16PartYTilesFolder { get; private set; }
		public StorageDirectory Tab16PartZTilesFolder { get; private set; }
		#endregion

		public void Init(StorageDirectory dataFolder)
		{
			_graphicResourcesFile = new StorageFile(dataFolder.RelativePathParts.Merge("Shift.Resources.dll"));

			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			DataClientGoalsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			DataHHIFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Income.xml"));
			DataDemoFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Age.xml"));
			DataGeographyFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Residence.xml"));
			DataNeedsCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Common Needs.xml"));
			DataSolutionsCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Product List.xml"));
			DataApproachesCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT Approach List.xml"));
			DataCBCCommonFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT CBC.xml"));

			#region Tab 1
			DataCoverPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01A.xml"));
			DataCoverPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01B.xml"));
			DataCoverPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01C.xml"));
			DataCoverPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01D.xml"));
			DataCoverPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT01E.xml"));

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
			DataIntroPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02A.xml"));
			DataIntroPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02B.xml"));
			DataIntroPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02C.xml"));
			DataIntroPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02D.xml"));

			Tab2PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_k" }));
			Tab2PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_l" }));
			Tab2PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_m" }));
			Tab2PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_n" }));
			Tab2PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_o" }));
			Tab2PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_u" }));
			Tab2PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_v" }));
			Tab2PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_w" }));
			Tab2PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_x" }));
			Tab2PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_y" }));
			Tab2PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_z" }));
			#endregion

			#region Tab 3
			DataAgendaPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03A.xml"));
			DataAgendaPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03B.xml"));
			DataAgendaPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03C.xml"));
			DataAgendaPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03D.xml"));
			DataAgendaPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03E.xml"));

			Tab3PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_k" }));
			Tab3PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_l" }));
			Tab3PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_m" }));
			Tab3PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_n" }));
			Tab3PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_o" }));
			Tab3PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_u" }));
			Tab3PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_v" }));
			Tab3PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_w" }));
			Tab3PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_x" }));
			Tab3PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_y" }));
			Tab3PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_z" }));
			#endregion

			#region Tab 4
			DataGoalsPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04A.xml"));
			DataGoalsPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04B.xml"));
			DataGoalsPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04C.xml"));
			DataGoalsPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04D.xml"));

			Tab4PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_k" }));
			Tab4PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_l" }));
			Tab4PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_m" }));
			Tab4PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_n" }));
			Tab4PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_o" }));
			Tab4PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_u" }));
			Tab4PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_v" }));
			Tab4PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_w" }));
			Tab4PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_x" }));
			Tab4PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_y" }));
			Tab4PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_z" }));
			#endregion

			#region Tab 5
			DataMarketPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05A.xml"));
			DataMarketPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05B.xml"));
			DataMarketPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05C.xml"));
			DataMarketPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05D.xml"));
			DataMarketPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05E.xml"));

			Tab5PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_k" }));
			Tab5PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_l" }));
			Tab5PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_m" }));
			Tab5PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_n" }));
			Tab5PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_o" }));
			Tab5PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_u" }));
			Tab5PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_v" }));
			Tab5PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_w" }));
			Tab5PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_x" }));
			Tab5PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_y" }));
			Tab5PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_z" }));
			#endregion

			#region Tab 6
			DataPartnershipPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06A.xml"));
			DataPartnershipPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06B.xml"));
			DataPartnershipPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06C.xml"));
			DataPartnershipPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06D.xml"));

			Tab6PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_k" }));
			Tab6PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_l" }));
			Tab6PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_m" }));
			Tab6PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_n" }));
			Tab6PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_o" }));
			Tab6PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_u" }));
			Tab6PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_v" }));
			Tab6PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_w" }));
			Tab6PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_x" }));
			Tab6PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_y" }));
			Tab6PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_z" }));
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

			Tab7PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_k" }));
			Tab7PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_l" }));
			Tab7PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_m" }));
			Tab7PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_n" }));
			Tab7PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_o" }));
			Tab7PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_u" }));
			Tab7PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_v" }));
			Tab7PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_w" }));
			Tab7PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_x" }));
			Tab7PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_y" }));
			Tab7PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_z" }));
			#endregion

			#region Tab 8
			DataCBCPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08A.xml"));
			DataCBCPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08B.xml"));
			DataCBCPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08C.xml"));
			DataCBCPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08D.xml"));
			DataCBCPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08E.xml"));
			DataCBCPartFFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08F.xml"));

			Tab8PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_k" }));
			Tab8PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_l" }));
			Tab8PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_m" }));
			Tab8PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_n" }));
			Tab8PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_o" }));
			Tab8PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_u" }));
			Tab8PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_v" }));
			Tab8PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_w" }));
			Tab8PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_x" }));
			Tab8PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_y" }));
			Tab8PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_z" }));
			#endregion

			#region Tab 9
			DataIntegratedSolutionOutputConditionsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT10 Slide Output Rules.xml"));

			ClipartTab9SharedFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "shared" }));

			Tab9PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_k" }));
			Tab9PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_l" }));
			Tab9PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_m" }));
			Tab9PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_n" }));
			Tab9PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_o" }));
			Tab9PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_u" }));
			Tab9PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_v" }));
			Tab9PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_w" }));
			Tab9PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_x" }));
			Tab9PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_y" }));
			Tab9PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_z" }));
			#endregion

			#region Tab 10
			Tab10PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_k" }));
			Tab10PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_l" }));
			Tab10PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_m" }));
			Tab10PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_n" }));
			Tab10PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_o" }));
			Tab10PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_u" }));
			Tab10PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_v" }));
			Tab10PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_w" }));
			Tab10PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_x" }));
			Tab10PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_y" }));
			Tab10PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_z" }));
			#endregion

			#region Tab 11
			Tab11PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_k" }));
			Tab11PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_l" }));
			Tab11PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_m" }));
			Tab11PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_n" }));
			Tab11PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_o" }));
			Tab11PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_u" }));
			Tab11PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_v" }));
			Tab11PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_w" }));
			Tab11PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_x" }));
			Tab11PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_y" }));
			Tab11PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_relationship_next_steps", "subtab_z" }));
			#endregion

			#region Tab 12
			Tab12PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_k" }));
			Tab12PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_l" }));
			Tab12PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_m" }));
			Tab12PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_n" }));
			Tab12PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_o" }));
			Tab12PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_u" }));
			Tab12PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_v" }));
			Tab12PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_w" }));
			Tab12PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_x" }));
			Tab12PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_y" }));
			Tab12PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_agreement_contract", "subtab_z" }));
			#endregion

			#region Tab 13
			Tab13PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_k" }));
			Tab13PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_l" }));
			Tab13PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_m" }));
			Tab13PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_n" }));
			Tab13PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_o" }));
			Tab13PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_u" }));
			Tab13PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_v" }));
			Tab13PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_w" }));
			Tab13PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_x" }));
			Tab13PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_y" }));
			Tab13PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "16_support_materials", "subtab_z" }));
			#endregion

			#region Tab 14
			Tab14PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_k" }));
			Tab14PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_l" }));
			Tab14PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_m" }));
			Tab14PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_n" }));
			Tab14PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_o" }));
			Tab14PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_u" }));
			Tab14PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_v" }));
			Tab14PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_w" }));
			Tab14PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_x" }));
			Tab14PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_y" }));
			Tab14PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_z" }));
			#endregion

			#region Tab 15
			DataApproachPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09A.xml"));
			DataApproachPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09B.xml"));
			DataApproachPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09C.xml"));

			ClipartTab15SubAFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "shared", "approach" }));
			ClipartTab15SubCFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "shared", "approach" }));

			Tab15PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_k" }));
			Tab15PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_l" }));
			Tab15PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_m" }));
			Tab15PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_n" }));
			Tab15PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_o" }));
			Tab15PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_u" }));
			Tab15PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_v" }));
			Tab15PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_w" }));
			Tab15PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_x" }));
			Tab15PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_y" }));
			Tab15PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_z" }));
			#endregion

			#region Tab 16
			DataROIPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06A.xml"));
			DataROIPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06B.xml"));
			DataROIPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06C.xml"));
			DataROIPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("CP06D.xml"));

			Tab16PartKSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_k" }));
			Tab16PartLSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_l" }));
			Tab16PartMSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_m" }));
			Tab16PartNSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_n" }));
			Tab16PartOSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_o" }));
			Tab16PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_u" }));
			Tab16PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_v" }));
			Tab16PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_w" }));
			Tab16PartXTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_x" }));
			Tab16PartYTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_y" }));
			Tab16PartZTilesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_roi", "subtab_z" }));
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
