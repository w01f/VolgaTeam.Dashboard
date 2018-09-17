using System;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class TilesChildTabInfo : StarChildTabInfo
	{
		private const string ConfigFileName = "Config.xml";

		private StorageDirectory _sourceDirectory;

		public override StarChildTabType TabType { get; }

		public TileConfiguration Tiles { get; private set; }

		public TilesChildTabInfo(StarTopTabType topTabType, StarChildTabType tabType) : base(topTabType)
		{
			TabType = tabType;
			EnableOutput = false;
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			switch (TopTabType)
			{
				case StarTopTabType.Cover:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab1PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab1PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab1PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab2PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab2PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab2PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab3PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab3PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab3PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab4PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab4PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab4PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab5PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab5PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab5PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab6PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab6PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab6PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab7PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab7PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab7PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab8PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab8PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab8PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab9PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab9PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab9PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab10PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab10PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab10PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectory = _resourceManager.Tab11PartXTilesFolder;
							break;
						case StarChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab11PartYTilesFolder;
							break;
						case StarChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab11PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}

		public void LoadTiles()
		{
			var configFileName = Path.Combine(_sourceDirectory.LocalPath, ConfigFileName);

			if (!File.Exists(configFileName))
				return;

			Tiles = TileConfiguration.FromFile(configFileName);
		}
	}
}
