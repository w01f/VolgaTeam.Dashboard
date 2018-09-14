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
		public override StarChildTabType TabType { get; }

		public SolutionSlideManager Slides { get; }

		public SlidesChildTabInfo(StarTopTabType topTabType, StarChildTabType tabType) : base(topTabType)
		{
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab1PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab1PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab2PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab2PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab3PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab3PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab4PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab4PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab5PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab5PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab6PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab6PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab7PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab7PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab8PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab8PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab9PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab9PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab10PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab10PartWSlidesFolder;
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
							break;
						case StarChildTabType.V:
							sourceDirectory = resourceManager.Tab11PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							sourceDirectory = resourceManager.Tab11PartWSlidesFolder;
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
