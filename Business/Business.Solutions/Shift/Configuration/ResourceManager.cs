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

		public StorageDirectory Tab1PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab1PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 2
		public StorageFile DataIntroPartAFile { get; private set; }
		public StorageFile DataIntroPartBFile { get; private set; }
		public StorageFile DataIntroPartCFile { get; private set; }
		public StorageFile DataIntroPartDFile { get; private set; }

		public StorageDirectory Tab2PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab2PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 3
		public StorageFile DataAgendaPartAFile { get; private set; }
		public StorageFile DataAgendaPartBFile { get; private set; }
		public StorageFile DataAgendaPartCFile { get; private set; }
		public StorageFile DataAgendaPartDFile { get; private set; }
		public StorageFile DataAgendaPartEFile { get; private set; }

		public StorageDirectory Tab3PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab3PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 4
		public StorageFile DataGoalsPartAFile { get; private set; }
		public StorageFile DataGoalsPartBFile { get; private set; }
		public StorageFile DataGoalsPartCFile { get; private set; }
		public StorageFile DataGoalsPartDFile { get; private set; }

		public StorageDirectory Tab4PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab4PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 5
		public StorageFile DataMarketPartAFile { get; private set; }
		public StorageFile DataMarketPartBFile { get; private set; }
		public StorageFile DataMarketPartCFile { get; private set; }
		public StorageFile DataMarketPartDFile { get; private set; }
		public StorageFile DataMarketPartEFile { get; private set; }

		public StorageDirectory Tab5PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab5PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 6
		public StorageFile DataPartnershipPartAFile { get; private set; }
		public StorageFile DataPartnershipPartBFile { get; private set; }
		public StorageFile DataPartnershipPartCFile { get; private set; }
		public StorageFile DataPartnershipPartDFile { get; private set; }

		public StorageDirectory Tab6PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab6PartWSlidesFolder { get; private set; }
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

		public StorageDirectory Tab7PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab7PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 8
		public StorageFile DataCBCPartAFile { get; private set; }
		public StorageFile DataCBCPartBFile { get; private set; }
		public StorageFile DataCBCPartCFile { get; private set; }
		public StorageFile DataCBCPartDFile { get; private set; }
		public StorageFile DataCBCPartEFile { get; private set; }
		public StorageFile DataCBCPartFFile { get; private set; }

		public StorageDirectory Tab8PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab8PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 9
		public StorageFile DataIntegratedSolutionOutputConditionsFile { get; private set; }

		public StorageDirectory ClipartTab9SharedFolder { get; private set; }

		public StorageDirectory Tab9PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab9PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 10
		public StorageDirectory Tab10PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab10PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 11
		public StorageDirectory Tab11PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab11PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 12
		public StorageDirectory Tab12PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab12PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 13
		public StorageDirectory Tab13PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab13PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 14
		public StorageDirectory Tab14PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab14PartWSlidesFolder { get; private set; }
		#endregion

		#region Tab 15
		public StorageFile DataApproachPartAFile { get; private set; }
		public StorageFile DataApproachPartBFile { get; private set; }
		public StorageFile DataApproachPartCFile { get; private set; }

		public StorageDirectory ClipartTab15SubAFolder { get; private set; }
		public StorageDirectory ClipartTab15SubCFolder { get; private set; }

		public StorageDirectory Tab15PartUSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartVSlidesFolder { get; private set; }
		public StorageDirectory Tab15PartWSlidesFolder { get; private set; }
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

			Tab1PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_u" }));
			Tab1PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_v" }));
			Tab1PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "subtab_w" }));
			#endregion

			#region Tab 2
			DataIntroPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02A.xml"));
			DataIntroPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02B.xml"));
			DataIntroPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02C.xml"));
			DataIntroPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT02D.xml"));

			Tab2PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_u" }));
			Tab2PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_v" }));
			Tab2PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "subtab_w" }));
			#endregion

			#region Tab 3
			DataAgendaPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03A.xml"));
			DataAgendaPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03B.xml"));
			DataAgendaPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03C.xml"));
			DataAgendaPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03D.xml"));
			DataAgendaPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT03E.xml"));

			Tab3PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_u" }));
			Tab3PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_v" }));
			Tab3PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "03_agenda", "subtab_w" }));
			#endregion

			#region Tab 4
			DataGoalsPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04A.xml"));
			DataGoalsPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04B.xml"));
			DataGoalsPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04C.xml"));
			DataGoalsPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT04D.xml"));

			Tab4PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_u" }));
			Tab4PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_v" }));
			Tab4PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "04_goals", "subtab_w" }));
			#endregion

			#region Tab 5
			DataMarketPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05A.xml"));
			DataMarketPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05B.xml"));
			DataMarketPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05C.xml"));
			DataMarketPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05D.xml"));
			DataMarketPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT05E.xml"));

			Tab5PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_u" }));
			Tab5PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_v" }));
			Tab5PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "05_market_opportunity", "subtab_w" }));
			#endregion

			#region Tab 6
			DataPartnershipPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06A.xml"));
			DataPartnershipPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06B.xml"));
			DataPartnershipPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06C.xml"));
			DataPartnershipPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT06D.xml"));

			Tab6PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_u" }));
			Tab6PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_v" }));
			Tab6PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "06_our_partnership", "subtab_w" }));
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

			Tab7PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_u" }));
			Tab7PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_v" }));
			Tab7PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "07_needs_solutions", "subtab_w" }));
			#endregion

			#region Tab 8
			DataCBCPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08A.xml"));
			DataCBCPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08B.xml"));
			DataCBCPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08C.xml"));
			DataCBCPartDFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08D.xml"));
			DataCBCPartEFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08E.xml"));
			DataCBCPartFFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT08F.xml"));

			Tab8PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_u" }));
			Tab8PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_v" }));
			Tab8PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "08_cbc", "subtab_w" }));
			#endregion

			#region Tab 9
			DataIntegratedSolutionOutputConditionsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT10 Slide Output Rules.xml"));

			ClipartTab9SharedFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "shared" }));

			Tab9PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_u" }));
			Tab9PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_v" }));
			Tab9PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "10_integrated_solution", "subtab_w" }));
			#endregion

			#region Tab 10
			Tab10PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_u" }));
			Tab10PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_v" }));
			Tab10PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "12_investment", "subtab_w" }));
			#endregion

			#region Tab 11
			Tab11PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_relationship_next_steps", "subtab_u" }));
			Tab11PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_relationship_next_steps", "subtab_v" }));
			Tab11PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "13_relationship_next_steps", "subtab_w" }));
			#endregion

			#region Tab 12
			Tab12PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_agreement_contract", "subtab_u" }));
			Tab12PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_agreement_contract", "subtab_v" }));
			Tab12PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "14_agreement_contract", "subtab_w" }));
			#endregion

			#region Tab 13
			Tab13PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_support_materials", "subtab_u" }));
			Tab13PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_support_materials", "subtab_v" }));
			Tab13PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "15_support_materials", "subtab_w" }));
			#endregion

			#region Tab 14
			Tab14PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_u" }));
			Tab14PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_v" }));
			Tab14PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "11_spec_builder", "subtab_w" }));
			#endregion

			#region Tab 15
			DataApproachPartAFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09A.xml"));
			DataApproachPartBFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09B.xml"));
			DataApproachPartCFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("SHIFT09C.xml"));

			ClipartTab15SubAFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "shared", "approach" }));
			ClipartTab15SubCFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "shared", "approach" }));

			Tab15PartUSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_u" }));
			Tab15PartVSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_v" }));
			Tab15PartWSlidesFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge(new[] { "09_our_approach", "subtab_w" }));
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
