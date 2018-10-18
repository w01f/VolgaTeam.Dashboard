using System;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Common.Configuration
{
	public abstract class BaseSolutionInfo
	{
		protected bool _contentLoaded;
		protected StorageDirectory DataFolder { get; private set; }
		protected StorageDirectory RemoteResourceFolder { get; private set; }

		public string Id { get; private set; }
		public SolutionType Type { get; protected set; }
		public string ToggleTitle { get; protected set; }
		public string ToggleImagePath { get; protected set; }
		public int Order { get; private set; }
		public bool Enabled { get; protected set; }

		protected BaseSolutionInfo()
		{
			Enabled = true;
		}

		public virtual void LoadToggleData(StorageDirectory holderAppDataFolder)
		{
			DataFolder = new StorageDirectory(holderAppDataFolder.RelativePathParts.Merge(Id));
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated ||
			   FileStorageManager.Instance.UseLocalMode)
				return;

			AsyncHelper.RunSync(async () =>
			{
				var templateVersionFile = new StorageFile(DataFolder.RelativePathParts.Merge("version.txt"));
				if (!await templateVersionFile.Exists(true) || await templateVersionFile.IsOutOfDate())
				{
					await DataFolder.Allocate(false);
					if (await DataFolder.Exists(true))
					{
						var templateFiles = (await DataFolder.GetRemoteFiles()).ToList();
						foreach (var templateFile in templateFiles)
						{
							if (templateFile.Name.Contains(".rar"))
							{
								var archiveFolder = new ArchiveDirectory(DataFolder.RelativePathParts.Merge(templateFile.NameOnly));
								await archiveFolder.Download();
							}
							else
								await templateFile.Download();
						}
					}
				}
			});
		}

		public abstract void LoadContentData();

		public static BaseSolutionInfo CreateFromConfig(XmlNode configNode)
		{
			var solutionTypeNode = configNode.SelectSingleNode("Type");
			if (solutionTypeNode == null)
				throw new ArgumentNullException("Solution type is not defined");

			BaseSolutionInfo solutionInfo;
			switch (solutionTypeNode.InnerText)
			{
				case "6ms":
					solutionInfo = new DashboardSolutionInfo();
					break;
				case "starapp":
					solutionInfo = new StarAppSolutionInfo();
					break;
				case "shift_1":
					solutionInfo = new ShiftSolutionInfo();
					break;
				default:
					throw new ArgumentOutOfRangeException("Solution Type is undefined");
			}

			solutionInfo.Id = configNode.SelectSingleNode("Id")?.InnerText;
			solutionInfo.Order = Int32.Parse(configNode.SelectSingleNode("Order")?.InnerText ?? "0");

			solutionInfo.ToggleTitle = configNode.SelectSingleNode("Title")?.InnerText;

			return solutionInfo;
		}
	}
}
