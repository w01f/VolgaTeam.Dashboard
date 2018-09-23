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
		private StorageDirectory _sourceDirectory;
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

			switch (TopTabType)
			{
				case StarTopTabType.Cover:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab1PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab1PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab1PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab1PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab1PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab1PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab1PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab1PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab2PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab2PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab2PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab2PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab2PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab2PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab2PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab2PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab3PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab3PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab3PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab3PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab3PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab3PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab3PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab3PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab4PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab4PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab4PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab4PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab4PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab4PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab4PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab4PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab5PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab5PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab5PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab5PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab5PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab5PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab5PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab5PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab6PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab6PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab6PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab6PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab6PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab6PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab6PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab6PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab7PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab7PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab7PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab7PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab7PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab7PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab7PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab7PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab8PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab8PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab8PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab8PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab8PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab8PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab8PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab8PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab9PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab9PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab9PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab9PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab9PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab9PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab9PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab9PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab10PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab10PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab10PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab10PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab10PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab10PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab10PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab10PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectory = resourceManager.Tab11PartKSlidesFolder;
							break;
						case StarChildTabType.L:
							_sourceDirectory = resourceManager.Tab11PartLSlidesFolder;
							break;
						case StarChildTabType.M:
							_sourceDirectory = resourceManager.Tab11PartMSlidesFolder;
							break;
						case StarChildTabType.N:
							_sourceDirectory = resourceManager.Tab11PartNSlidesFolder;
							break;
						case StarChildTabType.O:
							_sourceDirectory = resourceManager.Tab11PartOSlidesFolder;
							break;
						case StarChildTabType.U:
							_sourceDirectory = resourceManager.Tab11PartUSlidesFolder;
							break;
						case StarChildTabType.V:
							_sourceDirectory = resourceManager.Tab11PartVSlidesFolder;
							break;
						case StarChildTabType.W:
							_sourceDirectory = resourceManager.Tab11PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}

		public void LoadSlides()
		{
			Slides.LoadSlides(_sourceDirectory);
		}
	}
}
