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
		private StorageDirectory _sourceDirectory;

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
							_sourceDirectory = _resourceManager.Tab1PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab1PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab1PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab1PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab1PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab1PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab1PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab1PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Intro:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab2PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab2PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab2PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab2PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab2PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab2PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab2PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab2PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Agenda:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab3PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab3PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab3PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab3PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab3PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab3PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab3PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab3PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Goals:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab4PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab4PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab4PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab4PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab4PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab4PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab4PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab4PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Market:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab5PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab5PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab5PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab5PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab5PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab5PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab5PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab5PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Partnership:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab6PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab6PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab6PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab6PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab6PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab6PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab6PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab6PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab7PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab7PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab7PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab7PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab7PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab7PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab7PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab7PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.CBC:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab8PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab8PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab8PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab8PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab8PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab8PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab8PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab8PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab9PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab9PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab9PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab9PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab9PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab9PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab9PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab9PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Investment:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab10PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab10PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab10PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab10PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab10PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab10PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab10PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab10PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NextSteps:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab11PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab11PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab11PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab11PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab11PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab11PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab11PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab11PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Contract:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab12PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab12PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab12PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab12PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab12PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab12PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab12PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab12PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SupportMaterials:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab13PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab13PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab13PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab13PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab13PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab13PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab13PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab13PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SpecBuilder:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab14PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab14PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab14PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab14PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab14PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab14PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab14PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab14PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Approach:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab15PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab15PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab15PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab15PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab15PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab15PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab15PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab15PartWSlidesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.ROI:
					switch (TabType)
					{
						case ShiftChildTabType.K:
							_sourceDirectory = _resourceManager.Tab16PartKSlidesFolder;
							break;
						case ShiftChildTabType.L:
							_sourceDirectory = _resourceManager.Tab16PartLSlidesFolder;
							break;
						case ShiftChildTabType.M:
							_sourceDirectory = _resourceManager.Tab16PartMSlidesFolder;
							break;
						case ShiftChildTabType.N:
							_sourceDirectory = _resourceManager.Tab16PartNSlidesFolder;
							break;
						case ShiftChildTabType.O:
							_sourceDirectory = _resourceManager.Tab16PartOSlidesFolder;
							break;
						case ShiftChildTabType.U:
							_sourceDirectory = _resourceManager.Tab16PartUSlidesFolder;
							break;
						case ShiftChildTabType.V:
							_sourceDirectory = _resourceManager.Tab16PartVSlidesFolder;
							break;
						case ShiftChildTabType.W:
							_sourceDirectory = _resourceManager.Tab16PartWSlidesFolder;
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
			Slides.LoadSlides(_sourceDirectory);
		}
	}
}
