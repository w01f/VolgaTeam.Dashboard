using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class SlidesChildTabInfo : ShiftChildTabInfo
	{
		private string[] _sourceDirectoryRelativePath;

		public SolutionSlideManager Slides { get; }

		public SlidesChildTabInfo(ShiftChildTabType tabType, ShiftTopTabType topTabType) : base(tabType, topTabType)
		{
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
				case ShiftTopTabType.Cover:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Intro:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Agenda:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Goals:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Market:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Partnership:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.CBC:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Investment:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NextSteps:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Contract:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SupportMaterials:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SpecBuilder:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Approach:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.ROI:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartKSlidesRelativePath;
							break;
						case ShiftChildTabType.L:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartLSlidesRelativePath;
							break;
						case ShiftChildTabType.M:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartMSlidesRelativePath;
							break;
						case ShiftChildTabType.N:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartNSlidesRelativePath;
							break;
						case ShiftChildTabType.O:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartOSlidesRelativePath;
							break;
						case ShiftChildTabType.U:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartUSlidesRelativePath;
							break;
						case ShiftChildTabType.V:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartVSlidesRelativePath;
							break;
						case ShiftChildTabType.W:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartWSlidesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Shift tab type is not defined");
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

