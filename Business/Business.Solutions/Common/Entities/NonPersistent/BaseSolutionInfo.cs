using System;
using System.Xml;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Entities.NonPersistent;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public abstract class BaseSolutionInfo
	{
		protected ArchiveDirectory DataFolder { get; private set; }

		public string Id { get; private set; }
		public SolutionType Type { get; protected set; }
		public string ToggleTitle { get; protected set; }
		public int Order { get; private set; }

		public virtual void LoadData(StorageDirectory holderAppDataFolder)
		{
			DataFolder = new ArchiveDirectory(holderAppDataFolder.RelativePathParts.Merge(Id));
			AsyncHelper.RunSync(() => DataFolder.Download());
		}

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
				default:
					throw new ArgumentOutOfRangeException("Solution Type is undefined");
			}

			solutionInfo.Id = configNode.SelectSingleNode("Id")?.InnerText;
			solutionInfo.ToggleTitle = configNode.SelectSingleNode("Title")?.InnerText;
			solutionInfo.Order = Int32.Parse(configNode.SelectSingleNode("Order")?.InnerText ?? "0");

			return solutionInfo;
		}
	}
}
