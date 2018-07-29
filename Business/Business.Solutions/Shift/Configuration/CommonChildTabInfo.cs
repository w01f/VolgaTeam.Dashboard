using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class CommonChildTabInfo : ShiftChildTabInfo
	{
		private readonly ShiftTopTabType _topTabInfo;

		public CommonChildTabInfo(ShiftChildTabType tabType, ShiftTopTabType topTabInfo) : base(tabType)
		{
			_topTabInfo = topTabInfo;
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			var tabId = configNode.SelectSingleNode("./Type")?.InnerText?.ToLower();
			switch (_topTabInfo)
			{
				case ShiftTopTabType.Starters:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab1SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab1SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab1SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab1SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab1SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab1SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab1SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab1SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab1SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab1SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Goals:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab2SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab2SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab2SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab2SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab2SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab2SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab2SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab2SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab2SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab2SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Market:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab3SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab3SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab3SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab3SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab3SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab3SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab3SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab3SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab3SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab3SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Partnership:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab4SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab4SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab4SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab4SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab4SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab4SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab4SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab4SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab4SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab4SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab5SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab5SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab5SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab5SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab5SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab5SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab5SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab5SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab5SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab5SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.CBC:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab6SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab6SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab6SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab6SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab6SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab6SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab6SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab6SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab6SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab6SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab7SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab7SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab7SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab7SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab7SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab7SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab7SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab7SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab7SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab7SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab7SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Investment:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab8SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab8SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab8SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab8SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab8SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab8SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab8SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab8SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab8SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab8SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NextSteps:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab9SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab9SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab9SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab9SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab9SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab9SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab9SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab9SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab9SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab9SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Contract:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab10SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab10SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab10SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab10SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab10SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab10SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab10SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab10SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab10SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab10SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SupportMaterials:
					switch (tabId)
					{
						case "a":
							RightLogo = resourceManager.LogoTab11SubARightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubARightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubAFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubAFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubABackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubABackgroundFile.LocalPath)
								: null;
							break;
						case "b":
							RightLogo = resourceManager.LogoTab11SubBRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubBRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubBFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubBFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubBBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubBBackgroundFile.LocalPath)
								: null;
							break;
						case "c":
							RightLogo = resourceManager.LogoTab11SubCRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubCRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubCFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubCFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubCBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubCBackgroundFile.LocalPath)
								: null;
							break;
						case "d":
							RightLogo = resourceManager.LogoTab11SubDRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubDRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubDFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubDFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubDBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubDBackgroundFile.LocalPath)
								: null;
							break;
						case "e":
							RightLogo = resourceManager.LogoTab11SubERightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubERightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubEFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubEFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubEBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubEBackgroundFile.LocalPath)
								: null;
							break;
						case "f":
							RightLogo = resourceManager.LogoTab11SubFRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubFRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubFFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubFFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubFBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubFBackgroundFile.LocalPath)
								: null;
							break;
						case "g":
							RightLogo = resourceManager.LogoTab11SubGRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubGRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubGFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubGFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubGBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubGBackgroundFile.LocalPath)
								: null;
							break;
						case "h":
							RightLogo = resourceManager.LogoTab11SubHRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubHRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubHFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubHFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubHBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubHBackgroundFile.LocalPath)
								: null;
							break;
						case "i":
							RightLogo = resourceManager.LogoTab11SubIRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubIRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubIFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubIFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubIBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubIBackgroundFile.LocalPath)
								: null;
							break;
						case "j":
							RightLogo = resourceManager.LogoTab11SubJRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubJRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubJFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubJFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubJBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubJBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Shift tab type is not defined");
			}
		}
	}
}
