using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class SlidesChildTabInfo : ShiftChildTabInfo
	{
		public ShiftTopTabType TopTabType { get; }

		public SolutionSlideManager Slides { get; }

		public SlidesChildTabInfo(ShiftChildTabType tabType, ShiftTopTabType topTabType) : base(tabType)
		{
			TopTabType = topTabType;
			Slides = new SolutionSlideManager();
			EnableOutput = false;
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			var thumbnailWidth = Int32.Parse(configNode.SelectSingleNode("./ThumbnailSize/Width")?.InnerText ?? "0");
			var thumbnailHeight = Int32.Parse(configNode.SelectSingleNode("./ThumbnailSize/Height")?.InnerText ?? "0");
			Slides.InitThumbnailSize(new Size(thumbnailWidth, thumbnailHeight));

			StorageDirectory sourceDirectory;
			var tabId = configNode.SelectSingleNode("./Type")?.InnerText?.ToLower();
			switch (TopTabType)
			{
				case ShiftTopTabType.Cover:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab1PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab1SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab1PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab1SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab1PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab1SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Intro:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab2PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab2SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab2PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab2SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab2PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab2SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Agenda:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab3PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab3SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab3PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab3SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab3PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab3SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Goals:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab4PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab4SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab4PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab4SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab4PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab4SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Market:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab5PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab5SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab5PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab5SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab5PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab5SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Partnership:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab6PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab6SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab6PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab6SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab6PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab6SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab7PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab7SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab7PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab7SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab7PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab7SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.CBC:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab8PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab8SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab8PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab8SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab8PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab8SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab9PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab9SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab9PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab9SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab9PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab9SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Investment:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab10PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab10SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab10PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab10SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab10PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab10SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NextSteps:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab11PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab11SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab11PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab11SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab11PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab11SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Contract:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab12PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab12SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab12SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab12SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab12SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab12PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab12SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab12SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab12SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab12SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab12PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab12SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab12SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab12SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab12SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SupportMaterials:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab13PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab13SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab13SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab13SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab13SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab13PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab13SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab13SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab13SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab13SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab13PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab13SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab13SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab13SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab13SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SpecBuilder:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab14PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab14SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab14SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab14SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab14SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab14PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab14SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab14SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab14SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab14SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab14PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab14SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab14SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab14SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab14SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Approach:
					switch (tabId)
					{
						case "u":
							sourceDirectory = resourceManager.Tab15PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab15SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab15SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab15SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab15SubUFooterFile.LocalPath)
								: null;
							break;
						case "v":
							sourceDirectory = resourceManager.Tab15PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab15SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab15SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab15SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab15SubVFooterFile.LocalPath)
								: null;
							break;
						case "w":
							sourceDirectory = resourceManager.Tab15PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab15SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab15SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab15SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab15SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Shift tab type is not defined");
			}
			Slides.LoadSlides(sourceDirectory);
		}
	}
}
