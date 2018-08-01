using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class SlidesChildTabInfo : StarChildTabInfo
	{
		public StarTopTabType TopTabType { get; }
		public override StarChildTabType TabType { get; }

		public SolutionSlideManager Slides { get; }

		public SlidesChildTabInfo(StarTopTabType topTabType, StarChildTabType tabType)
		{
			TopTabType = topTabType;
			TabType = tabType;
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
			switch (TopTabType)
			{
				case StarTopTabType.Cover:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab1PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab1SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab1PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab1SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab1PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab1SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab1SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab1SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab1SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab2PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab2SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab2PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab2SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab2PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab2SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab2SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab2SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab2SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab3PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab3SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab3PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab3SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab3PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab3SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab3SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab3SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab3SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab4PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab4SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab4PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab4SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab4PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab4SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab4SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab4SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab4SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab5PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab5SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab5PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab5SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab5PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab5SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab5SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab5SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab5SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab6PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab6SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab6PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab6SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab6PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab6SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab6SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab6SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab6SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab7PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab7SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubUFooterFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab7PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab7SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubVFooterFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab7PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab7SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab7SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab7SubWFooterFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab8PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab8SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab8PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab8SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab8PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab8SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab8SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab8SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab8SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab9PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab9SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab9PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab9SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab9PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab9SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab9SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab9SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab9SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab10PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab10SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab10PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab10SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab10PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab10SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab10SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab10SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab10SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (TabType)
					{
						case StarChildTabType.U:
							sourceDirectory = resourceManager.Tab11PartUSlidesFolder;

							RightLogo = resourceManager.LogoTab11SubURightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubURightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubUFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubUFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubUBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubUBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab11PartVSlidesFolder;

							RightLogo = resourceManager.LogoTab11SubVRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubVRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubVFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubVFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubVBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubVBackgroundFile.LocalPath)
								: null;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab11PartWSlidesFolder;

							RightLogo = resourceManager.LogoTab11SubWRightFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubWRightFile.LocalPath)
								: null;
							FooterLogo = resourceManager.LogoTab11SubWFooterFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubWFooterFile.LocalPath)
								: null;
							BackgroundLogo = resourceManager.LogoTab11SubWBackgroundFile.ExistsLocal()
								? Image.FromFile(resourceManager.LogoTab11SubWBackgroundFile.LocalPath)
								: null;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
			Slides.LoadSlides(sourceDirectory);
		}
	}
}
