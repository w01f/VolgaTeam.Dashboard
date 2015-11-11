﻿using System.Collections.Generic;
using System.IO;
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

			var tabDirectoryPath = Path.Combine(ResourceManager.Instance.DataFolder.LocalPath, Id);
			if (Directory.Exists(tabDirectoryPath))
				foreach (var p in Directory.GetDirectories(tabDirectoryPath))
					Groups.Add(new TabGroup(p));
		}
	}
}
