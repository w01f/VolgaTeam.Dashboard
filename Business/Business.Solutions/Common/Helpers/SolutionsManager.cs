using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Common.Helpers
{
	public class SolutionsManager
	{
		public List<BaseSolutionInfo> Solutions { get; }

		public SolutionsManager()
		{
			Solutions = new List<BaseSolutionInfo>();
		}

		public void LoadSolutions(StorageFile solutionsConfigurationFile)
		{
			Solutions.Clear();
			if(!solutionsConfigurationFile.ExistsLocal())
				return;
			var document = new XmlDocument();
			document.Load(solutionsConfigurationFile.LocalPath);
			foreach (var solutionNode in document.SelectNodes(@"//Root/Solution").OfType<XmlNode>())
			{
				var solution = BaseSolutionInfo.CreateFromConfig(solutionNode);
				Solutions.Add(solution);
			}

			Solutions.Sort((x,y)=>x.Order.CompareTo(y.Order));
		}

		public void LoadSolutionData(StorageDirectory holderAppDataFolder)
		{
			Solutions.ForEach(s => s.LoadData(holderAppDataFolder));
		}
	}
}
