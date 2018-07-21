using System;
using System.Drawing;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public class ShiftSolutionInfo : BaseSolutionInfo
	{
		public TitlesConfiguration Titles { get; } = new TitlesConfiguration();

		#region Cleanslate
		public Image CleanslateHeaderLogo { get; private set; }
		public Image CleanslateSplashLogo { get; private set; }
		#endregion

		#region Tab 1
		public StartersConfiguration StartersConfiguration { get; }

		public Image Tab1SubARightLogo { get; private set; }
		public Image Tab1SubAFooterLogo { get; private set; }
		public Image Tab1SubBRightLogo { get; private set; }
		public Image Tab1SubBFooterLogo { get; private set; }
		public Image Tab1SubCRightLogo { get; private set; }
		public Image Tab1SubCFooterLogo { get; private set; }
		public Image Tab1SubDRightLogo { get; private set; }
		public Image Tab1SubDFooterLogo { get; private set; }
		public Image Tab1SubERightLogo { get; private set; }
		public Image Tab1SubEFooterLogo { get; private set; }
		public Image Tab1SubFRightLogo { get; private set; }
		public Image Tab1SubFFooterLogo { get; private set; }
		public Image Tab1SubGRightLogo { get; private set; }
		public Image Tab1SubGFooterLogo { get; private set; }
		public Image Tab1SubHRightLogo { get; private set; }
		public Image Tab1SubHFooterLogo { get; private set; }
		public Image Tab1SubIRightLogo { get; private set; }
		public Image Tab1SubIFooterLogo { get; private set; }
		public Image Tab1SubJRightLogo { get; private set; }
		public Image Tab1SubJFooterLogo { get; private set; }
		public Image Tab1SubURightLogo { get; private set; }
		public Image Tab1SubUFooterLogo { get; private set; }
		public Image Tab1SubVRightLogo { get; private set; }
		public Image Tab1SubVFooterLogo { get; private set; }
		public Image Tab1SubWRightLogo { get; private set; }
		public Image Tab1SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 2
		public CNAConfiguration CNAConfiguration { get; }

		public Image Tab2SubARightLogo { get; private set; }
		public Image Tab2SubAFooterLogo { get; private set; }
		public Image Tab2SubBRightLogo { get; private set; }
		public Image Tab2SubBFooterLogo { get; private set; }
		public Image Tab2SubCRightLogo { get; private set; }
		public Image Tab2SubCFooterLogo { get; private set; }
		public Image Tab2SubDRightLogo { get; private set; }
		public Image Tab2SubDFooterLogo { get; private set; }
		public Image Tab2SubERightLogo { get; private set; }
		public Image Tab2SubEFooterLogo { get; private set; }
		public Image Tab2SubFRightLogo { get; private set; }
		public Image Tab2SubFFooterLogo { get; private set; }
		public Image Tab2SubGRightLogo { get; private set; }
		public Image Tab2SubGFooterLogo { get; private set; }
		public Image Tab2SubHRightLogo { get; private set; }
		public Image Tab2SubHFooterLogo { get; private set; }
		public Image Tab2SubIRightLogo { get; private set; }
		public Image Tab2SubIFooterLogo { get; private set; }
		public Image Tab2SubJRightLogo { get; private set; }
		public Image Tab2SubJFooterLogo { get; private set; }
		public Image Tab2SubURightLogo { get; private set; }
		public Image Tab2SubUFooterLogo { get; private set; }
		public Image Tab2SubVRightLogo { get; private set; }
		public Image Tab2SubVFooterLogo { get; private set; }
		public Image Tab2SubWRightLogo { get; private set; }
		public Image Tab2SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 3
		public MarketConfiguration MarketConfiguration { get; }

		public Image Tab3SubARightLogo { get; private set; }
		public Image Tab3SubAFooterLogo { get; private set; }
		public Image Tab3SubBRightLogo { get; private set; }
		public Image Tab3SubBFooterLogo { get; private set; }
		public Image Tab3SubCRightLogo { get; private set; }
		public Image Tab3SubCFooterLogo { get; private set; }
		public Image Tab3SubDRightLogo { get; private set; }
		public Image Tab3SubDFooterLogo { get; private set; }
		public Image Tab3SubERightLogo { get; private set; }
		public Image Tab3SubEFooterLogo { get; private set; }
		public Image Tab3SubFRightLogo { get; private set; }
		public Image Tab3SubFFooterLogo { get; private set; }
		public Image Tab3SubGRightLogo { get; private set; }
		public Image Tab3SubGFooterLogo { get; private set; }
		public Image Tab3SubHRightLogo { get; private set; }
		public Image Tab3SubHFooterLogo { get; private set; }
		public Image Tab3SubIRightLogo { get; private set; }
		public Image Tab3SubIFooterLogo { get; private set; }
		public Image Tab3SubJRightLogo { get; private set; }
		public Image Tab3SubJFooterLogo { get; private set; }
		public Image Tab3SubURightLogo { get; private set; }
		public Image Tab3SubUFooterLogo { get; private set; }
		public Image Tab3SubVRightLogo { get; private set; }
		public Image Tab3SubVFooterLogo { get; private set; }
		public Image Tab3SubWRightLogo { get; private set; }
		public Image Tab3SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 4
		public NeedsSolutionsConfiguration NeedsSolutionsConfiguration { get; }

		public Image Tab4SubARightLogo { get; private set; }
		public Image Tab4SubAFooterLogo { get; private set; }
		public Image Tab4SubBRightLogo { get; private set; }
		public Image Tab4SubBFooterLogo { get; private set; }
		public Image Tab4SubCRightLogo { get; private set; }
		public Image Tab4SubCFooterLogo { get; private set; }
		public Image Tab4SubDRightLogo { get; private set; }
		public Image Tab4SubDFooterLogo { get; private set; }
		public Image Tab4SubERightLogo { get; private set; }
		public Image Tab4SubEFooterLogo { get; private set; }
		public Image Tab4SubFRightLogo { get; private set; }
		public Image Tab4SubFFooterLogo { get; private set; }
		public Image Tab4SubGRightLogo { get; private set; }
		public Image Tab4SubGFooterLogo { get; private set; }
		public Image Tab4SubHRightLogo { get; private set; }
		public Image Tab4SubHFooterLogo { get; private set; }
		public Image Tab4SubIRightLogo { get; private set; }
		public Image Tab4SubIFooterLogo { get; private set; }
		public Image Tab4SubJRightLogo { get; private set; }
		public Image Tab4SubJFooterLogo { get; private set; }
		public Image Tab4SubURightLogo { get; private set; }
		public Image Tab4SubUFooterLogo { get; private set; }
		public Image Tab4SubVRightLogo { get; private set; }
		public Image Tab4SubVFooterLogo { get; private set; }
		public Image Tab4SubWRightLogo { get; private set; }
		public Image Tab4SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 5
		public CBCConfiguration CBCConfiguration { get; }

		public Image Tab5SubARightLogo { get; private set; }
		public Image Tab5SubAFooterLogo { get; private set; }
		public Image Tab5SubBRightLogo { get; private set; }
		public Image Tab5SubBFooterLogo { get; private set; }
		public Image Tab5SubCRightLogo { get; private set; }
		public Image Tab5SubCFooterLogo { get; private set; }
		public Image Tab5SubDRightLogo { get; private set; }
		public Image Tab5SubDFooterLogo { get; private set; }
		public Image Tab5SubERightLogo { get; private set; }
		public Image Tab5SubEFooterLogo { get; private set; }
		public Image Tab5SubFRightLogo { get; private set; }
		public Image Tab5SubFFooterLogo { get; private set; }
		public Image Tab5SubGRightLogo { get; private set; }
		public Image Tab5SubGFooterLogo { get; private set; }
		public Image Tab5SubHRightLogo { get; private set; }
		public Image Tab5SubHFooterLogo { get; private set; }
		public Image Tab5SubIRightLogo { get; private set; }
		public Image Tab5SubIFooterLogo { get; private set; }
		public Image Tab5SubJRightLogo { get; private set; }
		public Image Tab5SubJFooterLogo { get; private set; }
		public Image Tab5SubURightLogo { get; private set; }
		public Image Tab5SubUFooterLogo { get; private set; }
		public Image Tab5SubVRightLogo { get; private set; }
		public Image Tab5SubVFooterLogo { get; private set; }
		public Image Tab5SubWRightLogo { get; private set; }
		public Image Tab5SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 6
		public IntegratedSolutionConfiguration IntegratedSolutionConfiguration { get; }

		public Image Tab6SubARightLogo { get; private set; }
		public Image Tab6SubAFooterLogo { get; private set; }
		public Image Tab6SubBRightLogo { get; private set; }
		public Image Tab6SubBFooterLogo { get; private set; }
		public Image Tab6SubCRightLogo { get; private set; }
		public Image Tab6SubCFooterLogo { get; private set; }
		public Image Tab6SubDRightLogo { get; private set; }
		public Image Tab6SubDFooterLogo { get; private set; }
		public Image Tab6SubERightLogo { get; private set; }
		public Image Tab6SubEFooterLogo { get; private set; }
		public Image Tab6SubFRightLogo { get; private set; }
		public Image Tab6SubFFooterLogo { get; private set; }
		public Image Tab6SubGRightLogo { get; private set; }
		public Image Tab6SubGFooterLogo { get; private set; }
		public Image Tab6SubHRightLogo { get; private set; }
		public Image Tab6SubHFooterLogo { get; private set; }
		public Image Tab6SubIRightLogo { get; private set; }
		public Image Tab6SubIFooterLogo { get; private set; }
		public Image Tab6SubJRightLogo { get; private set; }
		public Image Tab6SubJFooterLogo { get; private set; }
		public Image Tab6SubURightLogo { get; private set; }
		public Image Tab6SubUFooterLogo { get; private set; }
		public Image Tab6SubVRightLogo { get; private set; }
		public Image Tab6SubVFooterLogo { get; private set; }
		public Image Tab6SubWRightLogo { get; private set; }
		public Image Tab6SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 7
		public InvestmentConfiguration InvestmentConfiguration { get; }

		public Image Tab7SubARightLogo { get; private set; }
		public Image Tab7SubAFooterLogo { get; private set; }
		public Image Tab7SubBRightLogo { get; private set; }
		public Image Tab7SubBFooterLogo { get; private set; }
		public Image Tab7SubCRightLogo { get; private set; }
		public Image Tab7SubCFooterLogo { get; private set; }
		public Image Tab7SubDRightLogo { get; private set; }
		public Image Tab7SubDFooterLogo { get; private set; }
		public Image Tab7SubERightLogo { get; private set; }
		public Image Tab7SubEFooterLogo { get; private set; }
		public Image Tab7SubFRightLogo { get; private set; }
		public Image Tab7SubFFooterLogo { get; private set; }
		public Image Tab7SubGRightLogo { get; private set; }
		public Image Tab7SubGFooterLogo { get; private set; }
		public Image Tab7SubHRightLogo { get; private set; }
		public Image Tab7SubHFooterLogo { get; private set; }
		public Image Tab7SubIRightLogo { get; private set; }
		public Image Tab7SubIFooterLogo { get; private set; }
		public Image Tab7SubJRightLogo { get; private set; }
		public Image Tab7SubJFooterLogo { get; private set; }
		public Image Tab7SubURightLogo { get; private set; }
		public Image Tab7SubUFooterLogo { get; private set; }
		public Image Tab7SubVRightLogo { get; private set; }
		public Image Tab7SubVFooterLogo { get; private set; }
		public Image Tab7SubWRightLogo { get; private set; }
		public Image Tab7SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 8
		public ClosersConfiguration ClosersConfiguration { get; }

		public Image Tab8SubARightLogo { get; private set; }
		public Image Tab8SubAFooterLogo { get; private set; }
		public Image Tab8SubBRightLogo { get; private set; }
		public Image Tab8SubBFooterLogo { get; private set; }
		public Image Tab8SubCRightLogo { get; private set; }
		public Image Tab8SubCFooterLogo { get; private set; }
		public Image Tab8SubDRightLogo { get; private set; }
		public Image Tab8SubDFooterLogo { get; private set; }
		public Image Tab8SubERightLogo { get; private set; }
		public Image Tab8SubEFooterLogo { get; private set; }
		public Image Tab8SubFRightLogo { get; private set; }
		public Image Tab8SubFFooterLogo { get; private set; }
		public Image Tab8SubGRightLogo { get; private set; }
		public Image Tab8SubGFooterLogo { get; private set; }
		public Image Tab8SubHRightLogo { get; private set; }
		public Image Tab8SubHFooterLogo { get; private set; }
		public Image Tab8SubIRightLogo { get; private set; }
		public Image Tab8SubIFooterLogo { get; private set; }
		public Image Tab8SubJRightLogo { get; private set; }
		public Image Tab8SubJFooterLogo { get; private set; }
		public Image Tab8SubURightLogo { get; private set; }
		public Image Tab8SubUFooterLogo { get; private set; }
		public Image Tab8SubVRightLogo { get; private set; }
		public Image Tab8SubVFooterLogo { get; private set; }
		public Image Tab8SubWRightLogo { get; private set; }
		public Image Tab8SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 9
		public NextStepsConfiguration NextStepsConfiguration { get; }

		public Image Tab9SubARightLogo { get; private set; }
		public Image Tab9SubAFooterLogo { get; private set; }
		public Image Tab9SubBRightLogo { get; private set; }
		public Image Tab9SubBFooterLogo { get; private set; }
		public Image Tab9SubCRightLogo { get; private set; }
		public Image Tab9SubCFooterLogo { get; private set; }
		public Image Tab9SubDRightLogo { get; private set; }
		public Image Tab9SubDFooterLogo { get; private set; }
		public Image Tab9SubERightLogo { get; private set; }
		public Image Tab9SubEFooterLogo { get; private set; }
		public Image Tab9SubFRightLogo { get; private set; }
		public Image Tab9SubFFooterLogo { get; private set; }
		public Image Tab9SubGRightLogo { get; private set; }
		public Image Tab9SubGFooterLogo { get; private set; }
		public Image Tab9SubHRightLogo { get; private set; }
		public Image Tab9SubHFooterLogo { get; private set; }
		public Image Tab9SubIRightLogo { get; private set; }
		public Image Tab9SubIFooterLogo { get; private set; }
		public Image Tab9SubJRightLogo { get; private set; }
		public Image Tab9SubJFooterLogo { get; private set; }
		public Image Tab9SubURightLogo { get; private set; }
		public Image Tab9SubUFooterLogo { get; private set; }
		public Image Tab9SubVRightLogo { get; private set; }
		public Image Tab9SubVFooterLogo { get; private set; }
		public Image Tab9SubWRightLogo { get; private set; }
		public Image Tab9SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 10
		public ContractConfiguration ContractConfiguration { get; }

		public Image Tab10SubARightLogo { get; private set; }
		public Image Tab10SubAFooterLogo { get; private set; }
		public Image Tab10SubBRightLogo { get; private set; }
		public Image Tab10SubBFooterLogo { get; private set; }
		public Image Tab10SubCRightLogo { get; private set; }
		public Image Tab10SubCFooterLogo { get; private set; }
		public Image Tab10SubDRightLogo { get; private set; }
		public Image Tab10SubDFooterLogo { get; private set; }
		public Image Tab10SubERightLogo { get; private set; }
		public Image Tab10SubEFooterLogo { get; private set; }
		public Image Tab10SubFRightLogo { get; private set; }
		public Image Tab10SubFFooterLogo { get; private set; }
		public Image Tab10SubGRightLogo { get; private set; }
		public Image Tab10SubGFooterLogo { get; private set; }
		public Image Tab10SubHRightLogo { get; private set; }
		public Image Tab10SubHFooterLogo { get; private set; }
		public Image Tab10SubIRightLogo { get; private set; }
		public Image Tab10SubIFooterLogo { get; private set; }
		public Image Tab10SubJRightLogo { get; private set; }
		public Image Tab10SubJFooterLogo { get; private set; }
		public Image Tab10SubURightLogo { get; private set; }
		public Image Tab10SubUFooterLogo { get; private set; }
		public Image Tab10SubVRightLogo { get; private set; }
		public Image Tab10SubVFooterLogo { get; private set; }
		public Image Tab10SubWRightLogo { get; private set; }
		public Image Tab10SubWFooterLogo { get; private set; }
		#endregion

		#region Tab 11
		public SupportMaterialsConfiguration SupportMaterialsConfiguration { get; }

		public Image Tab11SubARightLogo { get; private set; }
		public Image Tab11SubAFooterLogo { get; private set; }
		public Image Tab11SubBRightLogo { get; private set; }
		public Image Tab11SubBFooterLogo { get; private set; }
		public Image Tab11SubCRightLogo { get; private set; }
		public Image Tab11SubCFooterLogo { get; private set; }
		public Image Tab11SubDRightLogo { get; private set; }
		public Image Tab11SubDFooterLogo { get; private set; }
		public Image Tab11SubERightLogo { get; private set; }
		public Image Tab11SubEFooterLogo { get; private set; }
		public Image Tab11SubFRightLogo { get; private set; }
		public Image Tab11SubFFooterLogo { get; private set; }
		public Image Tab11SubGRightLogo { get; private set; }
		public Image Tab11SubGFooterLogo { get; private set; }
		public Image Tab11SubHRightLogo { get; private set; }
		public Image Tab11SubHFooterLogo { get; private set; }
		public Image Tab11SubIRightLogo { get; private set; }
		public Image Tab11SubIFooterLogo { get; private set; }
		public Image Tab11SubJRightLogo { get; private set; }
		public Image Tab11SubJFooterLogo { get; private set; }
		public Image Tab11SubURightLogo { get; private set; }
		public Image Tab11SubUFooterLogo { get; private set; }
		public Image Tab11SubVRightLogo { get; private set; }
		public Image Tab11SubVFooterLogo { get; private set; }
		public Image Tab11SubWRightLogo { get; private set; }
		public Image Tab11SubWFooterLogo { get; private set; }
		#endregion

		public ShiftSolutionInfo()
		{
			Type = SolutionType.Shift;

			StartersConfiguration = new StartersConfiguration();
			CNAConfiguration = new CNAConfiguration();
			MarketConfiguration = new MarketConfiguration();
			NeedsSolutionsConfiguration = new NeedsSolutionsConfiguration();
			CBCConfiguration = new CBCConfiguration();
			IntegratedSolutionConfiguration = new IntegratedSolutionConfiguration();
			InvestmentConfiguration = new InvestmentConfiguration();
			ClosersConfiguration = new ClosersConfiguration();
			NextStepsConfiguration = new NextStepsConfiguration();
			ContractConfiguration = new ContractConfiguration();
			SupportMaterialsConfiguration = new SupportMaterialsConfiguration();
		}

		public override void LoadData(StorageDirectory holderAppDataFolder)
		{
			base.LoadData(holderAppDataFolder);

			var resourceManager = new ResourceManager();

			resourceManager.Init(DataFolder);

			var document = new XmlDocument();
			if (resourceManager.SettingsFile.ExistsLocal())
			{
				document.Load(resourceManager.SettingsFile.LocalPath);

				Enabled = !Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonDisabled")?.InnerText ?? "false");

				var useImage = Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonImage")?.InnerText ?? "false");
				if (useImage)
				{
					foreach (var extension in new[] { ".svg", ".png" })
					{
						ToggleImagePath = Path.Combine(DataFolder.LocalPath, String.Format("{0}{1}", Id.ToLower(), extension));
						if (File.Exists(ToggleImagePath))
							break;
					}
				}

				ToggleTitle = document.SelectSingleNode(@"//Settings/RightPanelButton")?.InnerText ?? ToggleTitle;
			}

			Titles.Load(resourceManager.SettingsFile);

			#region Cleanslate
			CleanslateHeaderLogo = resourceManager.LogoCleanslateHeaderFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateHeaderFile.LocalPath)
				: null;
			CleanslateSplashLogo = resourceManager.LogoCleanslateSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateSplashFile.LocalPath)
				: null;
			#endregion

			#region Tab 1
			StartersConfiguration.Load(resourceManager);

			Tab1SubARightLogo = resourceManager.LogoTab1SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubARightFile.LocalPath)
				: null;
			Tab1SubAFooterLogo = resourceManager.LogoTab1SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubAFooterFile.LocalPath)
				: null;
			Tab1SubBRightLogo = resourceManager.LogoTab1SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubBRightFile.LocalPath)
				: null;
			Tab1SubBFooterLogo = resourceManager.LogoTab1SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubBFooterFile.LocalPath)
				: null;
			Tab1SubCRightLogo = resourceManager.LogoTab1SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubCRightFile.LocalPath)
				: null;
			Tab1SubCFooterLogo = resourceManager.LogoTab1SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubCFooterFile.LocalPath)
				: null;
			Tab1SubDRightLogo = resourceManager.LogoTab1SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubDRightFile.LocalPath)
				: null;
			Tab1SubDFooterLogo = resourceManager.LogoTab1SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubDFooterFile.LocalPath)
				: null;
			Tab1SubERightLogo = resourceManager.LogoTab1SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubERightFile.LocalPath)
				: null;
			Tab1SubEFooterLogo = resourceManager.LogoTab1SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubEFooterFile.LocalPath)
				: null;
			Tab1SubFRightLogo = resourceManager.LogoTab1SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubFRightFile.LocalPath)
				: null;
			Tab1SubFFooterLogo = resourceManager.LogoTab1SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubFFooterFile.LocalPath)
				: null;
			Tab1SubGRightLogo = resourceManager.LogoTab1SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubGRightFile.LocalPath)
				: null;
			Tab1SubGFooterLogo = resourceManager.LogoTab1SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubGFooterFile.LocalPath)
				: null;
			Tab1SubHRightLogo = resourceManager.LogoTab1SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubHRightFile.LocalPath)
				: null;
			Tab1SubHFooterLogo = resourceManager.LogoTab1SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubHFooterFile.LocalPath)
				: null;
			Tab1SubIRightLogo = resourceManager.LogoTab1SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubIRightFile.LocalPath)
				: null;
			Tab1SubIFooterLogo = resourceManager.LogoTab1SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubIFooterFile.LocalPath)
				: null;
			Tab1SubJRightLogo = resourceManager.LogoTab1SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubJRightFile.LocalPath)
				: null;
			Tab1SubJFooterLogo = resourceManager.LogoTab1SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubJFooterFile.LocalPath)
				: null;
			Tab1SubURightLogo = resourceManager.LogoTab1SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubURightFile.LocalPath)
				: null;
			Tab1SubUFooterLogo = resourceManager.LogoTab1SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubUFooterFile.LocalPath)
				: null;
			Tab1SubVRightLogo = resourceManager.LogoTab1SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubVRightFile.LocalPath)
				: null;
			Tab1SubVFooterLogo = resourceManager.LogoTab1SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubVFooterFile.LocalPath)
				: null;
			Tab1SubWRightLogo = resourceManager.LogoTab1SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubWRightFile.LocalPath)
				: null;
			Tab1SubWFooterLogo = resourceManager.LogoTab1SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 2
			CNAConfiguration.Load(resourceManager);

			Tab2SubARightLogo = resourceManager.LogoTab2SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubARightFile.LocalPath)
				: null;
			Tab2SubAFooterLogo = resourceManager.LogoTab2SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubAFooterFile.LocalPath)
				: null;
			Tab2SubBRightLogo = resourceManager.LogoTab2SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubBRightFile.LocalPath)
				: null;
			Tab2SubBFooterLogo = resourceManager.LogoTab2SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubBFooterFile.LocalPath)
				: null;
			Tab2SubCRightLogo = resourceManager.LogoTab2SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubCRightFile.LocalPath)
				: null;
			Tab2SubCFooterLogo = resourceManager.LogoTab2SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubCFooterFile.LocalPath)
				: null;
			Tab2SubDRightLogo = resourceManager.LogoTab2SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubDRightFile.LocalPath)
				: null;
			Tab2SubDFooterLogo = resourceManager.LogoTab2SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubDFooterFile.LocalPath)
				: null;
			Tab2SubERightLogo = resourceManager.LogoTab2SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubERightFile.LocalPath)
				: null;
			Tab2SubEFooterLogo = resourceManager.LogoTab2SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubEFooterFile.LocalPath)
				: null;
			Tab2SubFRightLogo = resourceManager.LogoTab2SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubFRightFile.LocalPath)
				: null;
			Tab2SubFFooterLogo = resourceManager.LogoTab2SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubFFooterFile.LocalPath)
				: null;
			Tab2SubGRightLogo = resourceManager.LogoTab2SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubGRightFile.LocalPath)
				: null;
			Tab2SubGFooterLogo = resourceManager.LogoTab2SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubGFooterFile.LocalPath)
				: null;
			Tab2SubHRightLogo = resourceManager.LogoTab2SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubHRightFile.LocalPath)
				: null;
			Tab2SubHFooterLogo = resourceManager.LogoTab2SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubHFooterFile.LocalPath)
				: null;
			Tab2SubIRightLogo = resourceManager.LogoTab2SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubIRightFile.LocalPath)
				: null;
			Tab2SubIFooterLogo = resourceManager.LogoTab2SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubIFooterFile.LocalPath)
				: null;
			Tab2SubJRightLogo = resourceManager.LogoTab2SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubJRightFile.LocalPath)
				: null;
			Tab2SubJFooterLogo = resourceManager.LogoTab2SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubJFooterFile.LocalPath)
				: null;
			Tab2SubURightLogo = resourceManager.LogoTab2SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubURightFile.LocalPath)
				: null;
			Tab2SubUFooterLogo = resourceManager.LogoTab2SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubUFooterFile.LocalPath)
				: null;
			Tab2SubVRightLogo = resourceManager.LogoTab2SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubVRightFile.LocalPath)
				: null;
			Tab2SubVFooterLogo = resourceManager.LogoTab2SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubVFooterFile.LocalPath)
				: null;
			Tab2SubWRightLogo = resourceManager.LogoTab2SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubWRightFile.LocalPath)
				: null;
			Tab2SubWFooterLogo = resourceManager.LogoTab2SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 3
			MarketConfiguration.Load(resourceManager);

			Tab3SubARightLogo = resourceManager.LogoTab3SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubARightFile.LocalPath)
				: null;
			Tab3SubAFooterLogo = resourceManager.LogoTab3SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubAFooterFile.LocalPath)
				: null;
			Tab3SubBRightLogo = resourceManager.LogoTab3SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubBRightFile.LocalPath)
				: null;
			Tab3SubBFooterLogo = resourceManager.LogoTab3SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubBFooterFile.LocalPath)
				: null;
			Tab3SubCRightLogo = resourceManager.LogoTab3SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCRightFile.LocalPath)
				: null;
			Tab3SubCFooterLogo = resourceManager.LogoTab3SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCFooterFile.LocalPath)
				: null;
			Tab3SubDRightLogo = resourceManager.LogoTab3SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubDRightFile.LocalPath)
				: null;
			Tab3SubDFooterLogo = resourceManager.LogoTab3SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubDFooterFile.LocalPath)
				: null;
			Tab3SubERightLogo = resourceManager.LogoTab3SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubERightFile.LocalPath)
				: null;
			Tab3SubEFooterLogo = resourceManager.LogoTab3SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubEFooterFile.LocalPath)
				: null;
			Tab3SubFRightLogo = resourceManager.LogoTab3SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubFRightFile.LocalPath)
				: null;
			Tab3SubFFooterLogo = resourceManager.LogoTab3SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubFFooterFile.LocalPath)
				: null;
			Tab3SubGRightLogo = resourceManager.LogoTab3SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubGRightFile.LocalPath)
				: null;
			Tab3SubGFooterLogo = resourceManager.LogoTab3SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubGFooterFile.LocalPath)
				: null;
			Tab3SubHRightLogo = resourceManager.LogoTab3SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubHRightFile.LocalPath)
				: null;
			Tab3SubHFooterLogo = resourceManager.LogoTab3SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubHFooterFile.LocalPath)
				: null;
			Tab3SubIRightLogo = resourceManager.LogoTab3SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubIRightFile.LocalPath)
				: null;
			Tab3SubIFooterLogo = resourceManager.LogoTab3SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubIFooterFile.LocalPath)
				: null;
			Tab3SubJRightLogo = resourceManager.LogoTab3SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubJRightFile.LocalPath)
				: null;
			Tab3SubJFooterLogo = resourceManager.LogoTab3SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubJFooterFile.LocalPath)
				: null;
			Tab3SubURightLogo = resourceManager.LogoTab3SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubURightFile.LocalPath)
				: null;
			Tab3SubUFooterLogo = resourceManager.LogoTab3SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubUFooterFile.LocalPath)
				: null;
			Tab3SubVRightLogo = resourceManager.LogoTab3SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubVRightFile.LocalPath)
				: null;
			Tab3SubVFooterLogo = resourceManager.LogoTab3SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubVFooterFile.LocalPath)
				: null;
			Tab3SubWRightLogo = resourceManager.LogoTab3SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubWRightFile.LocalPath)
				: null;
			Tab3SubWFooterLogo = resourceManager.LogoTab3SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 4
			NeedsSolutionsConfiguration.Load(resourceManager);

			Tab4SubARightLogo = resourceManager.LogoTab4SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubARightFile.LocalPath)
				: null;
			Tab4SubAFooterLogo = resourceManager.LogoTab4SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubAFooterFile.LocalPath)
				: null;
			Tab4SubBRightLogo = resourceManager.LogoTab4SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBRightFile.LocalPath)
				: null;
			Tab4SubBFooterLogo = resourceManager.LogoTab4SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBFooterFile.LocalPath)
				: null;
			Tab4SubCRightLogo = resourceManager.LogoTab4SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubCRightFile.LocalPath)
				: null;
			Tab4SubCFooterLogo = resourceManager.LogoTab4SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubCFooterFile.LocalPath)
				: null;
			Tab4SubDRightLogo = resourceManager.LogoTab4SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubDRightFile.LocalPath)
				: null;
			Tab4SubDFooterLogo = resourceManager.LogoTab4SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubDFooterFile.LocalPath)
				: null;
			Tab4SubERightLogo = resourceManager.LogoTab4SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubERightFile.LocalPath)
				: null;
			Tab4SubEFooterLogo = resourceManager.LogoTab4SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubEFooterFile.LocalPath)
				: null;
			Tab4SubFRightLogo = resourceManager.LogoTab4SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubFRightFile.LocalPath)
				: null;
			Tab4SubFFooterLogo = resourceManager.LogoTab4SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubFFooterFile.LocalPath)
				: null;
			Tab4SubGRightLogo = resourceManager.LogoTab4SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubGRightFile.LocalPath)
				: null;
			Tab4SubGFooterLogo = resourceManager.LogoTab4SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubGFooterFile.LocalPath)
				: null;
			Tab4SubHRightLogo = resourceManager.LogoTab4SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubHRightFile.LocalPath)
				: null;
			Tab4SubHFooterLogo = resourceManager.LogoTab4SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubHFooterFile.LocalPath)
				: null;
			Tab4SubIRightLogo = resourceManager.LogoTab4SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubIRightFile.LocalPath)
				: null;
			Tab4SubIFooterLogo = resourceManager.LogoTab4SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubIFooterFile.LocalPath)
				: null;
			Tab4SubJRightLogo = resourceManager.LogoTab4SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubJRightFile.LocalPath)
				: null;
			Tab4SubJFooterLogo = resourceManager.LogoTab4SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubJFooterFile.LocalPath)
				: null;
			Tab4SubURightLogo = resourceManager.LogoTab4SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubURightFile.LocalPath)
				: null;
			Tab4SubUFooterLogo = resourceManager.LogoTab4SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubUFooterFile.LocalPath)
				: null;
			Tab4SubVRightLogo = resourceManager.LogoTab4SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubVRightFile.LocalPath)
				: null;
			Tab4SubVFooterLogo = resourceManager.LogoTab4SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubVFooterFile.LocalPath)
				: null;
			Tab4SubWRightLogo = resourceManager.LogoTab4SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubWRightFile.LocalPath)
				: null;
			Tab4SubWFooterLogo = resourceManager.LogoTab4SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 5
			CBCConfiguration.Load(resourceManager);

			Tab5SubARightLogo = resourceManager.LogoTab5SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubARightFile.LocalPath)
				: null;
			Tab5SubAFooterLogo = resourceManager.LogoTab5SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubAFooterFile.LocalPath)
				: null;
			Tab5SubBRightLogo = resourceManager.LogoTab5SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubBRightFile.LocalPath)
				: null;
			Tab5SubBFooterLogo = resourceManager.LogoTab5SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubBFooterFile.LocalPath)
				: null;
			Tab5SubCRightLogo = resourceManager.LogoTab5SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubCRightFile.LocalPath)
				: null;
			Tab5SubCFooterLogo = resourceManager.LogoTab5SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubCFooterFile.LocalPath)
				: null;
			Tab5SubDRightLogo = resourceManager.LogoTab5SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubDRightFile.LocalPath)
				: null;
			Tab5SubDFooterLogo = resourceManager.LogoTab5SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubDFooterFile.LocalPath)
				: null;
			Tab5SubERightLogo = resourceManager.LogoTab5SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubERightFile.LocalPath)
				: null;
			Tab5SubEFooterLogo = resourceManager.LogoTab5SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubEFooterFile.LocalPath)
				: null;
			Tab5SubFRightLogo = resourceManager.LogoTab5SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubFRightFile.LocalPath)
				: null;
			Tab5SubFFooterLogo = resourceManager.LogoTab5SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubFFooterFile.LocalPath)
				: null;
			Tab5SubGRightLogo = resourceManager.LogoTab5SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubGRightFile.LocalPath)
				: null;
			Tab5SubGFooterLogo = resourceManager.LogoTab5SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubGFooterFile.LocalPath)
				: null;
			Tab5SubHRightLogo = resourceManager.LogoTab5SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubHRightFile.LocalPath)
				: null;
			Tab5SubHFooterLogo = resourceManager.LogoTab5SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubHFooterFile.LocalPath)
				: null;
			Tab5SubIRightLogo = resourceManager.LogoTab5SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubIRightFile.LocalPath)
				: null;
			Tab5SubIFooterLogo = resourceManager.LogoTab5SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubIFooterFile.LocalPath)
				: null;
			Tab5SubJRightLogo = resourceManager.LogoTab5SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubJRightFile.LocalPath)
				: null;
			Tab5SubJFooterLogo = resourceManager.LogoTab5SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubJFooterFile.LocalPath)
				: null;
			Tab5SubURightLogo = resourceManager.LogoTab5SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubURightFile.LocalPath)
				: null;
			Tab5SubUFooterLogo = resourceManager.LogoTab5SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubUFooterFile.LocalPath)
				: null;
			Tab5SubVRightLogo = resourceManager.LogoTab5SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubVRightFile.LocalPath)
				: null;
			Tab5SubVFooterLogo = resourceManager.LogoTab5SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubVFooterFile.LocalPath)
				: null;
			Tab5SubWRightLogo = resourceManager.LogoTab5SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubWRightFile.LocalPath)
				: null;
			Tab5SubWFooterLogo = resourceManager.LogoTab5SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 6
			IntegratedSolutionConfiguration.Load(resourceManager);

			Tab6SubARightLogo = resourceManager.LogoTab6SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubARightFile.LocalPath)
				: null;
			Tab6SubAFooterLogo = resourceManager.LogoTab6SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubAFooterFile.LocalPath)
				: null;
			Tab6SubBRightLogo = resourceManager.LogoTab6SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubBRightFile.LocalPath)
				: null;
			Tab6SubBFooterLogo = resourceManager.LogoTab6SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubBFooterFile.LocalPath)
				: null;
			Tab6SubCRightLogo = resourceManager.LogoTab6SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubCRightFile.LocalPath)
				: null;
			Tab6SubCFooterLogo = resourceManager.LogoTab6SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubCFooterFile.LocalPath)
				: null;
			Tab6SubDRightLogo = resourceManager.LogoTab6SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubDRightFile.LocalPath)
				: null;
			Tab6SubDFooterLogo = resourceManager.LogoTab6SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubDFooterFile.LocalPath)
				: null;
			Tab6SubERightLogo = resourceManager.LogoTab6SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubERightFile.LocalPath)
				: null;
			Tab6SubEFooterLogo = resourceManager.LogoTab6SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubEFooterFile.LocalPath)
				: null;
			Tab6SubFRightLogo = resourceManager.LogoTab6SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubFRightFile.LocalPath)
				: null;
			Tab6SubFFooterLogo = resourceManager.LogoTab6SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubFFooterFile.LocalPath)
				: null;
			Tab6SubGRightLogo = resourceManager.LogoTab6SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubGRightFile.LocalPath)
				: null;
			Tab6SubGFooterLogo = resourceManager.LogoTab6SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubGFooterFile.LocalPath)
				: null;
			Tab6SubHRightLogo = resourceManager.LogoTab6SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubHRightFile.LocalPath)
				: null;
			Tab6SubHFooterLogo = resourceManager.LogoTab6SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubHFooterFile.LocalPath)
				: null;
			Tab6SubIRightLogo = resourceManager.LogoTab6SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubIRightFile.LocalPath)
				: null;
			Tab6SubIFooterLogo = resourceManager.LogoTab6SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubIFooterFile.LocalPath)
				: null;
			Tab6SubJRightLogo = resourceManager.LogoTab6SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubJRightFile.LocalPath)
				: null;
			Tab6SubJFooterLogo = resourceManager.LogoTab6SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubJFooterFile.LocalPath)
				: null;
			Tab6SubURightLogo = resourceManager.LogoTab6SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubURightFile.LocalPath)
				: null;
			Tab6SubUFooterLogo = resourceManager.LogoTab6SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubUFooterFile.LocalPath)
				: null;
			Tab6SubVRightLogo = resourceManager.LogoTab6SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubVRightFile.LocalPath)
				: null;
			Tab6SubVFooterLogo = resourceManager.LogoTab6SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubVFooterFile.LocalPath)
				: null;
			Tab6SubWRightLogo = resourceManager.LogoTab6SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubWRightFile.LocalPath)
				: null;
			Tab6SubWFooterLogo = resourceManager.LogoTab6SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 7
			InvestmentConfiguration.Load(resourceManager);

			Tab7SubARightLogo = resourceManager.LogoTab7SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubARightFile.LocalPath)
				: null;
			Tab7SubAFooterLogo = resourceManager.LogoTab7SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubAFooterFile.LocalPath)
				: null;
			Tab7SubBRightLogo = resourceManager.LogoTab7SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBRightFile.LocalPath)
				: null;
			Tab7SubBFooterLogo = resourceManager.LogoTab7SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBFooterFile.LocalPath)
				: null;
			Tab7SubCRightLogo = resourceManager.LogoTab7SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubCRightFile.LocalPath)
				: null;
			Tab7SubCFooterLogo = resourceManager.LogoTab7SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubCFooterFile.LocalPath)
				: null;
			Tab7SubDRightLogo = resourceManager.LogoTab7SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubDRightFile.LocalPath)
				: null;
			Tab7SubDFooterLogo = resourceManager.LogoTab7SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubDFooterFile.LocalPath)
				: null;
			Tab7SubERightLogo = resourceManager.LogoTab7SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubERightFile.LocalPath)
				: null;
			Tab7SubEFooterLogo = resourceManager.LogoTab7SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubEFooterFile.LocalPath)
				: null;
			Tab7SubFRightLogo = resourceManager.LogoTab7SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubFRightFile.LocalPath)
				: null;
			Tab7SubFFooterLogo = resourceManager.LogoTab7SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubFFooterFile.LocalPath)
				: null;
			Tab7SubGRightLogo = resourceManager.LogoTab7SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubGRightFile.LocalPath)
				: null;
			Tab7SubGFooterLogo = resourceManager.LogoTab7SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubGFooterFile.LocalPath)
				: null;
			Tab7SubHRightLogo = resourceManager.LogoTab7SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubHRightFile.LocalPath)
				: null;
			Tab7SubHFooterLogo = resourceManager.LogoTab7SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubHFooterFile.LocalPath)
				: null;
			Tab7SubIRightLogo = resourceManager.LogoTab7SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubIRightFile.LocalPath)
				: null;
			Tab7SubIFooterLogo = resourceManager.LogoTab7SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubIFooterFile.LocalPath)
				: null;
			Tab7SubJRightLogo = resourceManager.LogoTab7SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubJRightFile.LocalPath)
				: null;
			Tab7SubJFooterLogo = resourceManager.LogoTab7SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubJFooterFile.LocalPath)
				: null;
			Tab7SubURightLogo = resourceManager.LogoTab7SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubURightFile.LocalPath)
				: null;
			Tab7SubUFooterLogo = resourceManager.LogoTab7SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubUFooterFile.LocalPath)
				: null;
			Tab7SubVRightLogo = resourceManager.LogoTab7SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubVRightFile.LocalPath)
				: null;
			Tab7SubVFooterLogo = resourceManager.LogoTab7SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubVFooterFile.LocalPath)
				: null;
			Tab7SubWRightLogo = resourceManager.LogoTab7SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubWRightFile.LocalPath)
				: null;
			Tab7SubWFooterLogo = resourceManager.LogoTab7SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 8
			ClosersConfiguration.Load(resourceManager);

			Tab8SubARightLogo = resourceManager.LogoTab8SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubARightFile.LocalPath)
				: null;
			Tab8SubAFooterLogo = resourceManager.LogoTab8SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubAFooterFile.LocalPath)
				: null;
			Tab8SubBRightLogo = resourceManager.LogoTab8SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubBRightFile.LocalPath)
				: null;
			Tab8SubBFooterLogo = resourceManager.LogoTab8SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubBFooterFile.LocalPath)
				: null;
			Tab8SubCRightLogo = resourceManager.LogoTab8SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubCRightFile.LocalPath)
				: null;
			Tab8SubCFooterLogo = resourceManager.LogoTab8SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubCFooterFile.LocalPath)
				: null;
			Tab8SubDRightLogo = resourceManager.LogoTab8SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubDRightFile.LocalPath)
				: null;
			Tab8SubDFooterLogo = resourceManager.LogoTab8SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubDFooterFile.LocalPath)
				: null;
			Tab8SubERightLogo = resourceManager.LogoTab8SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubERightFile.LocalPath)
				: null;
			Tab8SubEFooterLogo = resourceManager.LogoTab8SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubEFooterFile.LocalPath)
				: null;
			Tab8SubFRightLogo = resourceManager.LogoTab8SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubFRightFile.LocalPath)
				: null;
			Tab8SubFFooterLogo = resourceManager.LogoTab8SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubFFooterFile.LocalPath)
				: null;
			Tab8SubGRightLogo = resourceManager.LogoTab8SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubGRightFile.LocalPath)
				: null;
			Tab8SubGFooterLogo = resourceManager.LogoTab8SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubGFooterFile.LocalPath)
				: null;
			Tab8SubHRightLogo = resourceManager.LogoTab8SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubHRightFile.LocalPath)
				: null;
			Tab8SubHFooterLogo = resourceManager.LogoTab8SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubHFooterFile.LocalPath)
				: null;
			Tab8SubIRightLogo = resourceManager.LogoTab8SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubIRightFile.LocalPath)
				: null;
			Tab8SubIFooterLogo = resourceManager.LogoTab8SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubIFooterFile.LocalPath)
				: null;
			Tab8SubJRightLogo = resourceManager.LogoTab8SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubJRightFile.LocalPath)
				: null;
			Tab8SubJFooterLogo = resourceManager.LogoTab8SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubJFooterFile.LocalPath)
				: null;
			Tab8SubURightLogo = resourceManager.LogoTab8SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubURightFile.LocalPath)
				: null;
			Tab8SubUFooterLogo = resourceManager.LogoTab8SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubUFooterFile.LocalPath)
				: null;
			Tab8SubVRightLogo = resourceManager.LogoTab8SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubVRightFile.LocalPath)
				: null;
			Tab8SubVFooterLogo = resourceManager.LogoTab8SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubVFooterFile.LocalPath)
				: null;
			Tab8SubWRightLogo = resourceManager.LogoTab8SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubWRightFile.LocalPath)
				: null;
			Tab8SubWFooterLogo = resourceManager.LogoTab8SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 9
			NextStepsConfiguration.Load(resourceManager);

			Tab9SubARightLogo = resourceManager.LogoTab9SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubARightFile.LocalPath)
				: null;
			Tab9SubAFooterLogo = resourceManager.LogoTab9SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubAFooterFile.LocalPath)
				: null;
			Tab9SubBRightLogo = resourceManager.LogoTab9SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubBRightFile.LocalPath)
				: null;
			Tab9SubBFooterLogo = resourceManager.LogoTab9SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubBFooterFile.LocalPath)
				: null;
			Tab9SubCRightLogo = resourceManager.LogoTab9SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCRightFile.LocalPath)
				: null;
			Tab9SubCFooterLogo = resourceManager.LogoTab9SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCFooterFile.LocalPath)
				: null;
			Tab9SubDRightLogo = resourceManager.LogoTab9SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubDRightFile.LocalPath)
				: null;
			Tab9SubDFooterLogo = resourceManager.LogoTab9SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubDFooterFile.LocalPath)
				: null;
			Tab9SubERightLogo = resourceManager.LogoTab9SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubERightFile.LocalPath)
				: null;
			Tab9SubEFooterLogo = resourceManager.LogoTab9SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubEFooterFile.LocalPath)
				: null;
			Tab9SubFRightLogo = resourceManager.LogoTab9SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubFRightFile.LocalPath)
				: null;
			Tab9SubFFooterLogo = resourceManager.LogoTab9SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubFFooterFile.LocalPath)
				: null;
			Tab9SubGRightLogo = resourceManager.LogoTab9SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubGRightFile.LocalPath)
				: null;
			Tab9SubGFooterLogo = resourceManager.LogoTab9SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubGFooterFile.LocalPath)
				: null;
			Tab9SubHRightLogo = resourceManager.LogoTab9SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubHRightFile.LocalPath)
				: null;
			Tab9SubHFooterLogo = resourceManager.LogoTab9SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubHFooterFile.LocalPath)
				: null;
			Tab9SubIRightLogo = resourceManager.LogoTab9SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubIRightFile.LocalPath)
				: null;
			Tab9SubIFooterLogo = resourceManager.LogoTab9SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubIFooterFile.LocalPath)
				: null;
			Tab9SubJRightLogo = resourceManager.LogoTab9SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubJRightFile.LocalPath)
				: null;
			Tab9SubJFooterLogo = resourceManager.LogoTab9SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubJFooterFile.LocalPath)
				: null;
			Tab9SubURightLogo = resourceManager.LogoTab9SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubURightFile.LocalPath)
				: null;
			Tab9SubUFooterLogo = resourceManager.LogoTab9SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubUFooterFile.LocalPath)
				: null;
			Tab9SubVRightLogo = resourceManager.LogoTab9SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubVRightFile.LocalPath)
				: null;
			Tab9SubVFooterLogo = resourceManager.LogoTab9SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubVFooterFile.LocalPath)
				: null;
			Tab9SubWRightLogo = resourceManager.LogoTab9SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubWRightFile.LocalPath)
				: null;
			Tab9SubWFooterLogo = resourceManager.LogoTab9SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 10
			ContractConfiguration.Load(resourceManager);

			Tab10SubARightLogo = resourceManager.LogoTab10SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubARightFile.LocalPath)
				: null;
			Tab10SubAFooterLogo = resourceManager.LogoTab10SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubAFooterFile.LocalPath)
				: null;
			Tab10SubBRightLogo = resourceManager.LogoTab10SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubBRightFile.LocalPath)
				: null;
			Tab10SubBFooterLogo = resourceManager.LogoTab10SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubBFooterFile.LocalPath)
				: null;
			Tab10SubCRightLogo = resourceManager.LogoTab10SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubCRightFile.LocalPath)
				: null;
			Tab10SubCFooterLogo = resourceManager.LogoTab10SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubCFooterFile.LocalPath)
				: null;
			Tab10SubDRightLogo = resourceManager.LogoTab10SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubDRightFile.LocalPath)
				: null;
			Tab10SubDFooterLogo = resourceManager.LogoTab10SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubDFooterFile.LocalPath)
				: null;
			Tab10SubERightLogo = resourceManager.LogoTab10SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubERightFile.LocalPath)
				: null;
			Tab10SubEFooterLogo = resourceManager.LogoTab10SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubEFooterFile.LocalPath)
				: null;
			Tab10SubFRightLogo = resourceManager.LogoTab10SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubFRightFile.LocalPath)
				: null;
			Tab10SubFFooterLogo = resourceManager.LogoTab10SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubFFooterFile.LocalPath)
				: null;
			Tab10SubGRightLogo = resourceManager.LogoTab10SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubGRightFile.LocalPath)
				: null;
			Tab10SubGFooterLogo = resourceManager.LogoTab10SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubGFooterFile.LocalPath)
				: null;
			Tab10SubHRightLogo = resourceManager.LogoTab10SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubHRightFile.LocalPath)
				: null;
			Tab10SubHFooterLogo = resourceManager.LogoTab10SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubHFooterFile.LocalPath)
				: null;
			Tab10SubIRightLogo = resourceManager.LogoTab10SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubIRightFile.LocalPath)
				: null;
			Tab10SubIFooterLogo = resourceManager.LogoTab10SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubIFooterFile.LocalPath)
				: null;
			Tab10SubJRightLogo = resourceManager.LogoTab10SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubJRightFile.LocalPath)
				: null;
			Tab10SubJFooterLogo = resourceManager.LogoTab10SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubJFooterFile.LocalPath)
				: null;
			Tab10SubURightLogo = resourceManager.LogoTab10SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubURightFile.LocalPath)
				: null;
			Tab10SubUFooterLogo = resourceManager.LogoTab10SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubUFooterFile.LocalPath)
				: null;
			Tab10SubVRightLogo = resourceManager.LogoTab10SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubVRightFile.LocalPath)
				: null;
			Tab10SubVFooterLogo = resourceManager.LogoTab10SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubVFooterFile.LocalPath)
				: null;
			Tab10SubWRightLogo = resourceManager.LogoTab10SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubWRightFile.LocalPath)
				: null;
			Tab10SubWFooterLogo = resourceManager.LogoTab10SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubWFooterFile.LocalPath)
				: null;
			#endregion

			#region Tab 11
			SupportMaterialsConfiguration.Load(resourceManager);

			Tab11SubARightLogo = resourceManager.LogoTab11SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubARightFile.LocalPath)
				: null;
			Tab11SubAFooterLogo = resourceManager.LogoTab11SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubAFooterFile.LocalPath)
				: null;
			Tab11SubBRightLogo = resourceManager.LogoTab11SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubBRightFile.LocalPath)
				: null;
			Tab11SubBFooterLogo = resourceManager.LogoTab11SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubBFooterFile.LocalPath)
				: null;
			Tab11SubCRightLogo = resourceManager.LogoTab11SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubCRightFile.LocalPath)
				: null;
			Tab11SubCFooterLogo = resourceManager.LogoTab11SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubCFooterFile.LocalPath)
				: null;
			Tab11SubDRightLogo = resourceManager.LogoTab11SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubDRightFile.LocalPath)
				: null;
			Tab11SubDFooterLogo = resourceManager.LogoTab11SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubDFooterFile.LocalPath)
				: null;
			Tab11SubERightLogo = resourceManager.LogoTab11SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubERightFile.LocalPath)
				: null;
			Tab11SubEFooterLogo = resourceManager.LogoTab11SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubEFooterFile.LocalPath)
				: null;
			Tab11SubFRightLogo = resourceManager.LogoTab11SubFRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubFRightFile.LocalPath)
				: null;
			Tab11SubFFooterLogo = resourceManager.LogoTab11SubFFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubFFooterFile.LocalPath)
				: null;
			Tab11SubGRightLogo = resourceManager.LogoTab11SubGRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubGRightFile.LocalPath)
				: null;
			Tab11SubGFooterLogo = resourceManager.LogoTab11SubGFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubGFooterFile.LocalPath)
				: null;
			Tab11SubHRightLogo = resourceManager.LogoTab11SubHRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubHRightFile.LocalPath)
				: null;
			Tab11SubHFooterLogo = resourceManager.LogoTab11SubHFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubHFooterFile.LocalPath)
				: null;
			Tab11SubIRightLogo = resourceManager.LogoTab11SubIRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubIRightFile.LocalPath)
				: null;
			Tab11SubIFooterLogo = resourceManager.LogoTab11SubIFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubIFooterFile.LocalPath)
				: null;
			Tab11SubJRightLogo = resourceManager.LogoTab11SubJRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubJRightFile.LocalPath)
				: null;
			Tab11SubJFooterLogo = resourceManager.LogoTab11SubJFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubJFooterFile.LocalPath)
				: null;
			Tab11SubURightLogo = resourceManager.LogoTab11SubURightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubURightFile.LocalPath)
				: null;
			Tab11SubUFooterLogo = resourceManager.LogoTab11SubUFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubUFooterFile.LocalPath)
				: null;
			Tab11SubVRightLogo = resourceManager.LogoTab11SubVRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubVRightFile.LocalPath)
				: null;
			Tab11SubVFooterLogo = resourceManager.LogoTab11SubVFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubVFooterFile.LocalPath)
				: null;
			Tab11SubWRightLogo = resourceManager.LogoTab11SubWRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubWRightFile.LocalPath)
				: null;
			Tab11SubWFooterLogo = resourceManager.LogoTab11SubWFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubWFooterFile.LocalPath)
				: null;
			#endregion
		}
	}
}
