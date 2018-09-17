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
