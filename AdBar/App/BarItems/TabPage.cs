using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.BarItems
{
	class TabPage
	{
		private readonly string _configContent;

		public string Id { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public bool Visible { get; set; }
		public bool UserGranted { get; private set; }

		public List<TabGroup> Groups { get; set; }

		public TabPage(string configContent)
		{
			_configContent = configContent;
			Groups = new List<TabGroup>();
			Init();
		}

		private void Init()
		{
			Id = ConfigHelper.GetValueRegex("<Id>(.*)</Id>", _configContent);
			Name = ConfigHelper.GetValueRegex("<Name>(.*)</Name>", _configContent);
			Enabled = ConfigHelper.GetValueRegex("<Enabled>(.*)</Enabled>", _configContent) != "false";
			Visible = ConfigHelper.GetValueRegex("<Visible>(.*)</Visible>", _configContent) != "false";
			UserGranted = !_configContent.ToLower().Contains("approvedusers") ||
						  ConfigHelper.GetValuesRegex("<user>(.*?)</user>", _configContent)
							  .Any(user => user.Equals(Environment.UserName, StringComparison.OrdinalIgnoreCase));

			if (Visible && UserGranted)
			{
				var tabDirectoryPath = Path.Combine(ResourceManager.Instance.DataFolder.LocalPath, Id);
				if (Directory.Exists(tabDirectoryPath))
					foreach (var p in Directory.GetDirectories(tabDirectoryPath))
					{
						var tabGroup = new TabGroup(p);
						if (tabGroup.Visible && tabGroup.UserGranted)
							Groups.Add(tabGroup);
					}
			}
		}
	}
}
