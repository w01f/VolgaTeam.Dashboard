using System;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class TilesChildTabInfo : ShiftChildTabInfo
	{
		private const string ConfigFileName = "Config.xml";

		private string[] _sourceDirectoryRelativePath;

		public TileConfiguration Tiles { get; private set; }

		public TilesChildTabInfo(ShiftChildTabType tabType, ShiftTopTabType topTabType) : base(tabType, topTabType)
		{
			EnableOutput = false;
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			switch (TopTabType)
			{
				case ShiftTopTabType.Cover:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab1PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Intro:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab2PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Agenda:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab3PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Goals:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab4PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Market:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab5PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Partnership:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab6PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab7PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.CBC:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab8PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab9PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Investment:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab10PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NextSteps:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab11PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Contract:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab12PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SupportMaterials:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab13PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SpecBuilder:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab14PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Approach:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab15PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.ROI:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartXTilesRelativePath;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartYTilesRelativePath;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectoryRelativePath = _resourceManager.Tab16PartZTilesRelativePath;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("Shift tab type is not defined");
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
