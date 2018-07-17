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
		#endregion

		#region Tab 2
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
		#endregion

		#region Tab 3
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
		#endregion

		#region Tab 4
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
		#endregion

		#region Tab 5
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
		#endregion

		#region Tab 6
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
		#endregion

		#region Tab 7
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
		#endregion

		#region Tab 8
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
		#endregion

		#region Tab 9
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
		#endregion

		#region Tab 10
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
		#endregion

		#region Tab 11
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
		#endregion

		public ShiftSolutionInfo()
		{
			Type = SolutionType.Shift;
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
			#endregion

			#region Tab 2
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
			#endregion

			#region Tab 3
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
			#endregion

			#region Tab 4
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
			#endregion

			#region Tab 5
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
			#endregion

			#region Tab 6
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
			#endregion

			#region Tab 7
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
			#endregion

			#region Tab 8
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
			#endregion

			#region Tab 9
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
			#endregion

			#region Tab 10
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
			#endregion

			#region Tab 11
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
			#endregion
		}
	}
}
