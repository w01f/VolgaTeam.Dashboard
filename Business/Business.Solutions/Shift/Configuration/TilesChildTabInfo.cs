using System;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class TilesChildTabInfo : ShiftChildTabInfo
	{
		private const string ConfigFileName = "Config.xml";

		private StorageDirectory _sourceDirectory;

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
							_sourceDirectory = _resourceManager.Tab1PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab1PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab1PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Intro:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab2PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab2PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab2PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Agenda:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab3PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab3PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab3PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Goals:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab4PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab4PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab4PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Market:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab5PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab5PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab5PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Partnership:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab6PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab6PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab6PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NeedsSolutions:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab7PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab7PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab7PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.CBC:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab8PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab8PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab8PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.IntegratedSolution:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab9PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab9PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab9PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Investment:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab10PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab10PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab10PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.NextSteps:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab11PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab11PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab11PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Contract:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab12PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab12PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab12PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SupportMaterials:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab13PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab13PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab13PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.SpecBuilder:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab14PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab14PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab14PartZTilesFolder;
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					break;
				case ShiftTopTabType.Approach:
					switch (TabType)
					{
						case ShiftChildTabType.X:
							_sourceDirectory = _resourceManager.Tab15PartXTilesFolder;
							break;
						case ShiftChildTabType.Y:
							_sourceDirectory = _resourceManager.Tab15PartYTilesFolder;
							break;
						case ShiftChildTabType.Z:
							_sourceDirectory = _resourceManager.Tab15PartZTilesFolder;
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
			var configFileName = Path.Combine(_sourceDirectory.LocalPath, ConfigFileName);

			if (!File.Exists(configFileName))
				return;

			Tiles = TileConfiguration.FromFile(configFileName);
		}
	}
}
