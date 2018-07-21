using System;
using System.Drawing;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class StarAppSolutionInfo : BaseSolutionInfo
	{
		public TitlesConfiguration Titles { get; } = new TitlesConfiguration();

		public Users UsersList { get; }
		public ClientGoalsLists ClientGoalsLists { get; }
		public TargetCustomersLists TargetCustomersLists { get; }

		#region Cleanslate
		public Image CleanslateHeaderLogo { get; private set; }
		public Image CleanslateSplashLogo { get; private set; }
		#endregion

		#region Tab 1
		public CoverConfiguration CoverConfiguration { get; }

		public Image Tab1SubARightLogo { get; private set; }
		public Image Tab1SubAFooterLogo { get; private set; }
		public Image Tab1SubURightLogo { get; private set; }
		public Image Tab1SubUFooterLogo { get; private set; }
		public Image Tab1SubVRightLogo { get; private set; }
		public Image Tab1SubVFooterLogo { get; private set; }
		public Image Tab1SubWRightLogo { get; private set; }
		public Image Tab1SubWFooterLogo { get; private set; }

		public Image Tab1SubAClipart1Image { get; private set; }
		#endregion

		#region Tab 2
		public CNAConfiguration CNAConfiguration { get; }

		public Image Tab2SubARightLogo { get; private set; }
		public Image Tab2SubAFooterLogo { get; private set; }
		public Image Tab2SubBRightLogo { get; private set; }
		public Image Tab2SubBFooterLogo { get; private set; }
		public Image Tab2SubURightLogo { get; private set; }
		public Image Tab2SubUFooterLogo { get; private set; }
		public Image Tab2SubVRightLogo { get; private set; }
		public Image Tab2SubVFooterLogo { get; private set; }
		public Image Tab2SubWRightLogo { get; private set; }
		public Image Tab2SubWFooterLogo { get; private set; }

		public Image Tab2SubAClipart1Image { get; private set; }
		public Image Tab2SubBClipart1Image { get; private set; }
		public Image Tab2SubBClipart2Image { get; private set; }
		#endregion

		#region Tab 3
		public FishingConfiguration FishingConfiguration { get; }

		public Image Tab3SubARightLogo { get; private set; }
		public Image Tab3SubAFooterLogo { get; private set; }
		public Image Tab3SubBRightLogo { get; private set; }
		public Image Tab3SubBFooterLogo { get; private set; }
		public Image Tab3SubCRightLogo { get; private set; }
		public Image Tab3SubCFooterLogo { get; private set; }
		public Image Tab3SubURightLogo { get; private set; }
		public Image Tab3SubUFooterLogo { get; private set; }
		public Image Tab3SubVRightLogo { get; private set; }
		public Image Tab3SubVFooterLogo { get; private set; }
		public Image Tab3SubWRightLogo { get; private set; }
		public Image Tab3SubWFooterLogo { get; private set; }

		public Image Tab3SubAClipart1Image { get; private set; }
		public Image Tab3SubBClipart1Image { get; private set; }
		public Image Tab3SubBClipart2Image { get; private set; }
		#endregion

		#region Tab 4
		public CustomerConfiguration CustomerConfiguration { get; }

		public Image Tab4SubARightLogo { get; private set; }
		public Image Tab4SubAFooterLogo { get; private set; }
		public Image Tab4SubBRightLogo { get; private set; }
		public Image Tab4SubBFooterLogo { get; private set; }
		public Image Tab4SubCRightLogo { get; private set; }
		public Image Tab4SubCFooterLogo { get; private set; }
		public Image Tab4SubURightLogo { get; private set; }
		public Image Tab4SubUFooterLogo { get; private set; }
		public Image Tab4SubVRightLogo { get; private set; }
		public Image Tab4SubVFooterLogo { get; private set; }
		public Image Tab4SubWRightLogo { get; private set; }
		public Image Tab4SubWFooterLogo { get; private set; }

		public Image Tab4SubAClipart1Image { get; private set; }
		public Image Tab4SubAClipart2Image { get; private set; }
		public Image Tab4SubBClipart1Image { get; private set; }
		public Image Tab4SubBClipart2Image { get; private set; }
		#endregion

		#region Tab 5
		public ShareConfiguration ShareConfiguration { get; }

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
		public Image Tab5SubURightLogo { get; private set; }
		public Image Tab5SubUFooterLogo { get; private set; }
		public Image Tab5SubVRightLogo { get; private set; }
		public Image Tab5SubVFooterLogo { get; private set; }
		public Image Tab5SubWRightLogo { get; private set; }
		public Image Tab5SubWFooterLogo { get; private set; }

		public Image Tab5SubAClipart1Image { get; private set; }
		public Image Tab5SubAClipart2Image { get; private set; }
		public Image Tab5SubAClipart3Image { get; private set; }

		public Image Tab5SubBClipart1Image { get; private set; }
		public Image Tab5SubBClipart2Image { get; private set; }
		public Image Tab5SubBClipart3Image { get; private set; }

		public Image Tab5SubCClipart1Image { get; private set; }
		public Image Tab5SubCClipart2Image { get; private set; }
		public Image Tab5SubCClipart3Image { get; private set; }

		public Image Tab5SubDClipart1Image { get; private set; }
		public Image Tab5SubDClipart2Image { get; private set; }
		public Image Tab5SubDClipart3Image { get; private set; }

		public Image Tab5SubEClipart1Image { get; private set; }
		public Image Tab5SubEClipart2Image { get; private set; }
		public Image Tab5SubEClipart3Image { get; private set; }
		#endregion

		#region Tab 6
		public ROIConfiguration ROIConfiguration { get; }

		public Image Tab6SubARightLogo { get; private set; }
		public Image Tab6SubAFooterLogo { get; private set; }
		public Image Tab6SubBRightLogo { get; private set; }
		public Image Tab6SubBFooterLogo { get; private set; }
		public Image Tab6SubCRightLogo { get; private set; }
		public Image Tab6SubCFooterLogo { get; private set; }
		public Image Tab6SubDRightLogo { get; private set; }
		public Image Tab6SubDFooterLogo { get; private set; }
		public Image Tab6SubURightLogo { get; private set; }
		public Image Tab6SubUFooterLogo { get; private set; }
		public Image Tab6SubVRightLogo { get; private set; }
		public Image Tab6SubVFooterLogo { get; private set; }
		public Image Tab6SubWRightLogo { get; private set; }
		public Image Tab6SubWFooterLogo { get; private set; }

		public Image Tab6SubAClipart1Image { get; private set; }
		public Image Tab6SubAClipart2Image { get; private set; }
		public Image Tab6SubAClipart3Image { get; private set; }

		public Image Tab6SubBClipart1Image { get; private set; }
		public Image Tab6SubBClipart2Image { get; private set; }
		public Image Tab6SubBClipart3Image { get; private set; }

		public Image Tab6SubCClipart1Image { get; private set; }
		public Image Tab6SubCClipart2Image { get; private set; }
		public Image Tab6SubCClipart3Image { get; private set; }

		public Image Tab6SubDClipart1Image { get; private set; }
		public Image Tab6SubDClipart2Image { get; private set; }
		public Image Tab6SubDClipart3Image { get; private set; }
		#endregion

		#region Tab 7
		public MarketConfiguration MarketConfiguration { get; }

		public Image Tab7SubARightLogo { get; private set; }
		public Image Tab7SubAFooterLogo { get; private set; }
		public Image Tab7SubBRightLogo { get; private set; }
		public Image Tab7SubBFooterLogo { get; private set; }
		public Image Tab7SubCRightLogo { get; private set; }
		public Image Tab7SubCFooterLogo { get; private set; }
		public Image Tab7SubURightLogo { get; private set; }
		public Image Tab7SubUFooterLogo { get; private set; }
		public Image Tab7SubVRightLogo { get; private set; }
		public Image Tab7SubVFooterLogo { get; private set; }
		public Image Tab7SubWRightLogo { get; private set; }
		public Image Tab7SubWFooterLogo { get; private set; }

		public Image Tab7SubAClipart1Image { get; private set; }

		public Image Tab7SubBClipart1Image { get; private set; }
		public Image Tab7SubBClipart2Image { get; private set; }
		public Image Tab7SubBClipart3Image { get; private set; }
		public Image Tab7SubBClipart4Image { get; private set; }
		public Image Tab7SubBClipart5Image { get; private set; }

		public Image Tab7SubCClipart1Image { get; private set; }
		public Image Tab7SubCClipart2Image { get; private set; }
		public Image Tab7SubCClipart3Image { get; private set; }
		public Image Tab7SubCClipart4Image { get; private set; }
		#endregion

		#region Tab 8
		public VideoConfiguration VideoConfiguration { get; }

		public Image Tab8SubARightLogo { get; private set; }
		public Image Tab8SubAFooterLogo { get; private set; }
		public Image Tab8SubBRightLogo { get; private set; }
		public Image Tab8SubBFooterLogo { get; private set; }
		public Image Tab8SubCRightLogo { get; private set; }
		public Image Tab8SubCFooterLogo { get; private set; }
		public Image Tab8SubDRightLogo { get; private set; }
		public Image Tab8SubDFooterLogo { get; private set; }
		public Image Tab8SubURightLogo { get; private set; }
		public Image Tab8SubUFooterLogo { get; private set; }
		public Image Tab8SubVRightLogo { get; private set; }
		public Image Tab8SubVFooterLogo { get; private set; }
		public Image Tab8SubWRightLogo { get; private set; }
		public Image Tab8SubWFooterLogo { get; private set; }

		public Image Tab8SubAClipart1Image { get; private set; }
		public Image Tab8SubBClipart1Image { get; private set; }
		public Image Tab8SubCClipart1Image { get; private set; }
		public Image Tab8SubDClipart1Image { get; private set; }
		#endregion

		#region Tab 9
		public AudienceConfiguration AudienceConfiguration { get; }

		public Image Tab9SubARightLogo { get; private set; }
		public Image Tab9SubAFooterLogo { get; private set; }
		public Image Tab9SubBRightLogo { get; private set; }
		public Image Tab9SubBFooterLogo { get; private set; }
		public Image Tab9SubCRightLogo { get; private set; }
		public Image Tab9SubCFooterLogo { get; private set; }
		public Image Tab9SubURightLogo { get; private set; }
		public Image Tab9SubUFooterLogo { get; private set; }
		public Image Tab9SubVRightLogo { get; private set; }
		public Image Tab9SubVFooterLogo { get; private set; }
		public Image Tab9SubWRightLogo { get; private set; }
		public Image Tab9SubWFooterLogo { get; private set; }

		public Image Tab9SubAClipart1Image { get; private set; }
		public Image Tab9SubAClipart2Image { get; private set; }

		public Image Tab9SubBClipart1Image { get; private set; }
		public Image Tab9SubBClipart2Image { get; private set; }
		public Image Tab9SubBClipart3Image { get; private set; }

		public Image Tab9SubCClipart1Image { get; private set; }
		public Image Tab9SubCClipart2Image { get; private set; }
		public Image Tab9SubCClipart3Image { get; private set; }
		public Image Tab9SubCClipart4Image { get; private set; }
		#endregion

		#region Tab 10
		public SolutionConfiguration SolutionConfiguration { get; }

		public Image Tab10SubARightLogo { get; private set; }
		public Image Tab10SubAFooterLogo { get; private set; }
		public Image Tab10SubBRightLogo { get; private set; }
		public Image Tab10SubBFooterLogo { get; private set; }
		public Image Tab10SubCRightLogo { get; private set; }
		public Image Tab10SubCFooterLogo { get; private set; }
		public Image Tab10SubDRightLogo { get; private set; }
		public Image Tab10SubDFooterLogo { get; private set; }
		public Image Tab10SubURightLogo { get; private set; }
		public Image Tab10SubUFooterLogo { get; private set; }
		public Image Tab10SubVRightLogo { get; private set; }
		public Image Tab10SubVFooterLogo { get; private set; }
		public Image Tab10SubWRightLogo { get; private set; }
		public Image Tab10SubWFooterLogo { get; private set; }

		public Image Tab10SubAClipart1Image { get; private set; }

		public Image Tab10SubBClipart1Image { get; private set; }
		public Image Tab10SubBClipart2Image { get; private set; }
		public Image Tab10SubBClipart3Image { get; private set; }

		public Image Tab10SubCClipart1Image { get; private set; }
		public Image Tab10SubCClipart2Image { get; private set; }

		public Image Tab10SubDClipart1Image { get; private set; }
		#endregion

		#region Tab 11
		public ClosersConfiguration ClosersConfiguration { get; }

		public Image Tab11SubARightLogo { get; private set; }
		public Image Tab11SubAFooterLogo { get; private set; }
		public Image Tab11SubBRightLogo { get; private set; }
		public Image Tab11SubBFooterLogo { get; private set; }
		public Image Tab11SubCRightLogo { get; private set; }
		public Image Tab11SubCFooterLogo { get; private set; }
		public Image Tab11SubURightLogo { get; private set; }
		public Image Tab11SubUFooterLogo { get; private set; }
		public Image Tab11SubVRightLogo { get; private set; }
		public Image Tab11SubVFooterLogo { get; private set; }
		public Image Tab11SubWRightLogo { get; private set; }
		public Image Tab11SubWFooterLogo { get; private set; }

		public Image Tab11SubAClipart1Image { get; private set; }
		public Image Tab11SubAClipart2Image { get; private set; }

		public Image Tab11SubBClipart1Image { get; private set; }
		public Image Tab11SubBClipart2Image { get; private set; }

		public Image Tab11SubCClipart1Image { get; private set; }
		public Image Tab11SubCClipart2Image { get; private set; }
		#endregion

		public StarAppSolutionInfo()
		{
			Type = SolutionType.StarApp;

			UsersList = new Users();
			ClientGoalsLists = new ClientGoalsLists();
			TargetCustomersLists = new TargetCustomersLists();

			CoverConfiguration = new CoverConfiguration();
			CNAConfiguration = new CNAConfiguration();
			FishingConfiguration = new FishingConfiguration();
			CustomerConfiguration = new CustomerConfiguration();
			ShareConfiguration = new ShareConfiguration();
			ROIConfiguration = new ROIConfiguration();
			MarketConfiguration = new MarketConfiguration();
			VideoConfiguration = new VideoConfiguration();
			AudienceConfiguration = new AudienceConfiguration();
			SolutionConfiguration = new SolutionConfiguration();
			ClosersConfiguration = new ClosersConfiguration();
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

			UsersList.Load(resourceManager.DataUsersFile);
			ClientGoalsLists.Load(resourceManager.DataClientGoalsFile);
			TargetCustomersLists.Load(resourceManager.DataTargetCustomersFile);

			#region Cleanslate
			CleanslateHeaderLogo = resourceManager.LogoCleanslateHeaderFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateHeaderFile.LocalPath)
				: null;
			CleanslateSplashLogo = resourceManager.LogoCleanslateSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateSplashFile.LocalPath)
				: null;
			#endregion

			#region Tab 1
			CoverConfiguration.Load(resourceManager);

			Tab1SubARightLogo = resourceManager.LogoTab1SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubARightFile.LocalPath)
				: null;
			Tab1SubAFooterLogo = resourceManager.LogoTab1SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubAFooterFile.LocalPath)
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

			Tab1SubAClipart1Image = resourceManager.ClipartTab1SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab1SubA1File.LocalPath)
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

			Tab2SubAClipart1Image = resourceManager.ClipartTab2SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubA1File.LocalPath)
				: null;

			Tab2SubBClipart1Image = resourceManager.ClipartTab2SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubB1File.LocalPath)
				: null;
			Tab2SubBClipart2Image = resourceManager.ClipartTab2SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubB2File.LocalPath)
				: null;
			#endregion

			#region Tab 3
			FishingConfiguration.Load(resourceManager);

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

			Tab3SubAClipart1Image = resourceManager.ClipartTab3SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubA1File.LocalPath)
				: null;

			Tab3SubBClipart1Image = resourceManager.ClipartTab3SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubB1File.LocalPath)
				: null;
			Tab3SubBClipart2Image = resourceManager.ClipartTab3SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubB2File.LocalPath)
				: null;
			#endregion

			#region Tab 4
			CustomerConfiguration.Load(resourceManager);

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

			Tab4SubAClipart1Image = resourceManager.ClipartTab4SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubA1File.LocalPath)
				: null;
			Tab4SubAClipart2Image = resourceManager.ClipartTab4SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubA2File.LocalPath)
				: null;

			Tab4SubBClipart1Image = resourceManager.ClipartTab4SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubB1File.LocalPath)
				: null;
			Tab4SubBClipart2Image = resourceManager.ClipartTab4SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubB2File.LocalPath)
				: null;
			#endregion

			#region Tab 5
			ShareConfiguration.Load(resourceManager);

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

			Tab5SubAClipart1Image = resourceManager.ClipartTab5SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA1File.LocalPath)
				: null;
			Tab5SubAClipart2Image = resourceManager.ClipartTab5SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA2File.LocalPath)
				: null;
			Tab5SubAClipart3Image = resourceManager.ClipartTab5SubA3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA3File.LocalPath)
				: null;

			Tab5SubBClipart1Image = resourceManager.ClipartTab5SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubB1File.LocalPath)
				: null;
			Tab5SubBClipart2Image = resourceManager.ClipartTab5SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubB2File.LocalPath)
				: null;
			Tab5SubBClipart3Image = resourceManager.ClipartTab5SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubB3File.LocalPath)
				: null;

			Tab5SubCClipart1Image = resourceManager.ClipartTab5SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubC1File.LocalPath)
				: null;
			Tab5SubCClipart2Image = resourceManager.ClipartTab5SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubC2File.LocalPath)
				: null;
			Tab5SubCClipart3Image = resourceManager.ClipartTab5SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubC3File.LocalPath)
				: null;

			Tab5SubDClipart1Image = resourceManager.ClipartTab5SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubD1File.LocalPath)
				: null;
			Tab5SubDClipart2Image = resourceManager.ClipartTab5SubD2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubD2File.LocalPath)
				: null;
			Tab5SubDClipart3Image = resourceManager.ClipartTab5SubD3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubD3File.LocalPath)
				: null;

			Tab5SubEClipart1Image = resourceManager.ClipartTab5SubE1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubE1File.LocalPath)
				: null;
			Tab5SubEClipart2Image = resourceManager.ClipartTab5SubE2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubE2File.LocalPath)
				: null;
			Tab5SubEClipart3Image = resourceManager.ClipartTab5SubE3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubE3File.LocalPath)
				: null;
			#endregion

			#region Tab 6
			ROIConfiguration.Load(resourceManager);

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

			Tab6SubAClipart1Image = resourceManager.ClipartTab6SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubA1File.LocalPath)
				: null;
			Tab6SubAClipart2Image = resourceManager.ClipartTab6SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubA2File.LocalPath)
				: null;
			Tab6SubAClipart3Image = resourceManager.ClipartTab6SubA3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubA3File.LocalPath)
				: null;

			Tab6SubBClipart1Image = resourceManager.ClipartTab6SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubB1File.LocalPath)
				: null;
			Tab6SubBClipart2Image = resourceManager.ClipartTab6SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubB2File.LocalPath)
				: null;
			Tab6SubBClipart3Image = resourceManager.ClipartTab6SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubB3File.LocalPath)
				: null;

			Tab6SubCClipart1Image = resourceManager.ClipartTab6SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubC1File.LocalPath)
				: null;
			Tab6SubCClipart2Image = resourceManager.ClipartTab6SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubC2File.LocalPath)
				: null;
			Tab6SubCClipart3Image = resourceManager.ClipartTab6SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubC3File.LocalPath)
				: null;

			Tab6SubDClipart1Image = resourceManager.ClipartTab6SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubD1File.LocalPath)
				: null;
			Tab6SubDClipart2Image = resourceManager.ClipartTab6SubD2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubD2File.LocalPath)
				: null;
			Tab6SubDClipart3Image = resourceManager.ClipartTab6SubD3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubD3File.LocalPath)
				: null;
			#endregion

			#region Tab 7
			MarketConfiguration.Load(resourceManager);

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

			Tab7SubAClipart1Image = resourceManager.ClipartTab7SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubA1File.LocalPath)
				: null;

			Tab7SubBClipart1Image = resourceManager.ClipartTab7SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB1File.LocalPath)
				: null;
			Tab7SubBClipart2Image = resourceManager.ClipartTab7SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB2File.LocalPath)
				: null;
			Tab7SubBClipart3Image = resourceManager.ClipartTab7SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB3File.LocalPath)
				: null;
			Tab7SubBClipart4Image = resourceManager.ClipartTab7SubB4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB4File.LocalPath)
				: null;
			Tab7SubBClipart5Image = resourceManager.ClipartTab7SubB5File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB5File.LocalPath)
				: null;

			Tab7SubCClipart1Image = resourceManager.ClipartTab7SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC1File.LocalPath)
				: null;
			Tab7SubCClipart2Image = resourceManager.ClipartTab7SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC2File.LocalPath)
				: null;
			Tab7SubCClipart3Image = resourceManager.ClipartTab7SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC3File.LocalPath)
				: null;
			Tab7SubCClipart4Image = resourceManager.ClipartTab7SubC4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC4File.LocalPath)
				: null;
			#endregion

			#region Tab 8
			VideoConfiguration.Load(resourceManager);

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

			Tab8SubAClipart1Image = resourceManager.ClipartTab8SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubA1File.LocalPath)
				: null;
			Tab8SubBClipart1Image = resourceManager.ClipartTab8SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubB1File.LocalPath)
				: null;
			Tab8SubCClipart1Image = resourceManager.ClipartTab8SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubC1File.LocalPath)
				: null;
			Tab8SubDClipart1Image = resourceManager.ClipartTab8SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubD1File.LocalPath)
				: null;
			#endregion

			#region Tab 9
			AudienceConfiguration.Load(resourceManager);

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

			Tab9SubAClipart1Image = resourceManager.ClipartTab9SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubA1File.LocalPath)
				: null;
			Tab9SubAClipart2Image = resourceManager.ClipartTab9SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubA2File.LocalPath)
				: null;

			Tab9SubBClipart1Image = resourceManager.ClipartTab9SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubB1File.LocalPath)
				: null;
			Tab9SubBClipart2Image = resourceManager.ClipartTab9SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubB2File.LocalPath)
				: null;
			Tab9SubBClipart3Image = resourceManager.ClipartTab9SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubB3File.LocalPath)
				: null;

			Tab9SubCClipart1Image = resourceManager.ClipartTab9SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC1File.LocalPath)
				: null;
			Tab9SubCClipart2Image = resourceManager.ClipartTab9SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC2File.LocalPath)
				: null;
			Tab9SubCClipart3Image = resourceManager.ClipartTab9SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC3File.LocalPath)
				: null;
			Tab9SubCClipart4Image = resourceManager.ClipartTab9SubC4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC4File.LocalPath)
				: null;
			#endregion

			#region Tab 10
			SolutionConfiguration.Load(resourceManager);

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

			Tab10SubAClipart1Image = resourceManager.ClipartTab10SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubA1File.LocalPath)
				: null;

			Tab10SubBClipart1Image = resourceManager.ClipartTab10SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubB1File.LocalPath)
				: null;
			Tab10SubBClipart2Image = resourceManager.ClipartTab10SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubB2File.LocalPath)
				: null;
			Tab10SubBClipart3Image = resourceManager.ClipartTab10SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubB3File.LocalPath)
				: null;

			Tab10SubCClipart1Image = resourceManager.ClipartTab10SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubC1File.LocalPath)
				: null;
			Tab10SubCClipart2Image = resourceManager.ClipartTab10SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubC2File.LocalPath)
				: null;

			Tab10SubDClipart1Image = resourceManager.ClipartTab10SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubD1File.LocalPath)
				: null;
			#endregion

			#region Tab 11
			ClosersConfiguration.Load(resourceManager);

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

			Tab11SubAClipart1Image = resourceManager.ClipartTab11SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubA1File.LocalPath)
				: null;
			Tab11SubAClipart2Image = resourceManager.ClipartTab11SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubA2File.LocalPath)
				: null;

			Tab11SubBClipart1Image = resourceManager.ClipartTab11SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubB1File.LocalPath)
				: null;
			Tab11SubBClipart2Image = resourceManager.ClipartTab11SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubB2File.LocalPath)
				: null;

			Tab11SubCClipart1Image = resourceManager.ClipartTab11SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubC1File.LocalPath)
				: null;
			Tab11SubCClipart2Image = resourceManager.ClipartTab11SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubC2File.LocalPath)
				: null;
			#endregion
		}
	}
}
