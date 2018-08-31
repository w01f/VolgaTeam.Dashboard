using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
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
			Solutions.ForEach(s => s.LoadToggleData(holderAppDataFolder));
			Solutions.FirstOrDefault()?.LoadContentData();

			if (FileStorageManager.Instance.DataState != DataActualityState.Updated)
			{
				var excessFolders = Directory.GetDirectories(holderAppDataFolder.LocalPath)
					.Where(path => !Solutions.Any(solutionInfo =>
						String.Equals(Path.GetFileName(path), solutionInfo.Id, StringComparison.OrdinalIgnoreCase)))
					.ToList();
				foreach (var excessFolder in excessFolders)
				{
					try
					{
						Utilities.DeleteFolder(excessFolder);
					}
					catch
					{
					}
				}
				var excessArchives = Directory.GetFiles(holderAppDataFolder.LocalPath,"*.rar")
					.Where(path => !Solutions.Any(solutionInfo =>
						String.Equals(Path.GetFileNameWithoutExtension(path), solutionInfo.Id, StringComparison.OrdinalIgnoreCase)))
					.ToList();
				foreach (var path in excessArchives)
				{
					try
					{
						File.Delete(path);
					}
					catch
					{
					}
				}
			}
		}
	}
}
