using System;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class TilesChildTabInfo : StarChildTabInfo
	{
		private const string ConfigFileName = "Config.xml";

		private string[] _sourceDirectoryRelativePath;

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
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.CNA:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Fishing:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Customer:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Share:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.ROI:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Market:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Video:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Audience:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Solution:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					break;
				case StarTopTabType.Closers:
					switch (TabType)
					{
						case StarChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartXTilesRelativePath;
							break;
						case StarChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartYTilesRelativePath;
							break;
						case StarChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartZTilesRelativePath;
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
			var configFileName = Path.Combine(
				new StorageFile(_resourceManager.LocalDataFolder.RelativePathParts.Merge(_sourceDirectoryRelativePath)).LocalPath,
				ConfigFileName);
			if (!File.Exists(configFileName))
				return;

			Tiles = TileConfiguration.FromFile(configFileName);
		}
	}
}
