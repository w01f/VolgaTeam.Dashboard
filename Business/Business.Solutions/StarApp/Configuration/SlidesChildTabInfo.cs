using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class SlidesChildTabInfo : StarChildTabInfo
	{
		private string[] _sourceDirectoryRelativePath;
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
							_sourceDirectoryRelativePath = resourceManager.Tab1PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab1PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab1PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab1PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab1PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab1PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab1PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab1PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab2PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab3PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab4PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab5PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab6PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab7PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab8PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab9PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab10PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (TabType)
					{
						case StarChildTabType.K:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartKSlidesRelativePath;
							break;
						case StarChildTabType.L:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartLSlidesRelativePath;
							break;
						case StarChildTabType.M:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartMSlidesRelativePath;
							break;
						case StarChildTabType.N:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartNSlidesRelativePath;
							break;
						case StarChildTabType.O:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartOSlidesRelativePath;
							break;
						case StarChildTabType.U:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartUSlidesRelativePath;
							break;
						case StarChildTabType.V:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartVSlidesRelativePath;
							break;
						case StarChildTabType.W:
							_sourceDirectoryRelativePath = resourceManager.Tab11PartWSlidesRelativePath;
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
			Slides.LoadSlides(
				new StorageDirectory(_resourceManager.LocalDataFolder.RelativePathParts.Merge(_sourceDirectoryRelativePath)),
				new StorageDirectory(_resourceManager.RemoteResourcesFolder.RelativePathParts.Merge(_sourceDirectoryRelativePath)));
		}
	}
}
