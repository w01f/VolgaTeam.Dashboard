using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.StarApp.Configuration.Audience;
using Asa.Business.Solutions.StarApp.Configuration.Cleanslate;
using Asa.Business.Solutions.StarApp.Configuration.Closers;
using Asa.Business.Solutions.StarApp.Configuration.CNA;
using Asa.Business.Solutions.StarApp.Configuration.Cover;
using Asa.Business.Solutions.StarApp.Configuration.Customer;
using Asa.Business.Solutions.StarApp.Configuration.Fishing;
using Asa.Business.Solutions.StarApp.Configuration.Market;
using Asa.Business.Solutions.StarApp.Configuration.ROI;
using Asa.Business.Solutions.StarApp.Configuration.Share;
using Asa.Business.Solutions.StarApp.Configuration.Solution;
using Asa.Business.Solutions.StarApp.Configuration.Video;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class StarAppSolutionInfo : BaseSolutionInfo
	{
		private ResourceManager _resourceManager;

		public List<StarTopTabInfo> TabsInfo { get; }

		public Users UsersList { get; }
		public ClientGoalsLists ClientGoalsLists { get; }
		public TargetCustomersLists TargetCustomersLists { get; }

		public StarAppSolutionInfo()
		{
			Type = SolutionType.StarApp;

			TabsInfo = new List<StarTopTabInfo>();

			UsersList = new Users();
			ClientGoalsLists = new ClientGoalsLists();
			TargetCustomersLists = new TargetCustomersLists();
		}

		public override void LoadToggleData(StorageDirectory holderAppDataFolder)
		{
			base.LoadToggleData(holderAppDataFolder);

			_resourceManager = new ResourceManager();

			_resourceManager.Init(DataFolder);

			if (_resourceManager.SettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(_resourceManager.SettingsFile.LocalPath);

				Enabled = !Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonDisabled")?.InnerText ?? "false");

				var useImage = Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonImage")?.InnerText ?? "false");
				if (useImage)
				{
					foreach (var extension in new[] { ".svg", ".png" })
					{
						ToggleImagePath = Path.Combine(DataFolder.LocalPath, String.Format("{0}{1}", Id.ToLower(), extension));
						if (File.Exists(ToggleImagePath))
							break;
					}
				}

				ToggleTitle = document.SelectSingleNode(@"//Settings/RightPanelButton")?.InnerText ?? ToggleTitle;
			}
		}

		public override void LoadContentData()
		{
			_resourceManager.LoadGraphicResources();

			if (_contentLoaded) return;

			if (_resourceManager.SettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(_resourceManager.SettingsFile.LocalPath);

				foreach (var tabConfigNode in document.SelectNodes(@"//Settings/Tab")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				{
					var tabId = tabConfigNode.SelectSingleNode("Type")?.InnerText?.ToLower();

					StarTopTabInfo tabInfo;
					switch (tabId)
					{
						case "default":
							tabInfo = new CleanslateTabInfo();
							break;
						case "cover":
							tabInfo = new CoverTabInfo();
							break;
						case "cna":
							tabInfo = new CNATabInfo();
							break;
						case "fishing":
							tabInfo = new FishingTabInfo();
							break;
						case "customer":
							tabInfo = new CustomerTabInfo();
							break;
						case "share":
							tabInfo = new ShareTabInfo();
							break;
						case "roi":
							tabInfo = new ROITabInfo();
							break;
						case "market":
							tabInfo = new MarketTabInfo();
							break;
						case "video":
							tabInfo = new VideoTabInfo();
							break;
						case "audience":
							tabInfo = new AudienceTabInfo();
							break;
						case "solution":
							tabInfo = new SolutionTabInfo();
							break;
						case "closers":
							tabInfo = new ClosersTabInfo();
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					tabInfo.LoadData(tabConfigNode, _resourceManager);
					TabsInfo.Add(tabInfo);
				}
			}

			UsersList.Load(_resourceManager.DataUsersFile);
			ClientGoalsLists.Load(_resourceManager.DataClientGoalsFile);
			TargetCustomersLists.LoadCombinedData(_resourceManager.DataTargetCustomersFile);

			_contentLoaded = true;
		}

		public void ReleaseContentData()
		{
			_resourceManager.ReleaseGraphicResources();
		}
	}
}
