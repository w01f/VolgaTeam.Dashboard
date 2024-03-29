﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.BarItems
{
	class TabGroup
	{
		private const string GroupConfigFileName = "settings.xml";
		private readonly string _rootPath;

		public TabGroupType Type;
		public string Name { get; set; }
		public string Tag { get; set; }
		public bool Visible { get; set; }
		public bool UserGranted { get; private set; }
		public List<TabGroupItem> Items { get; set; }


		public TabGroup(string path)
		{
			_rootPath = path;
			Items = new List<TabGroupItem>();
			Init();
			if (Visible && UserGranted)
				LoadItems();
		}

		private void Init()
		{
			var configContent = ConfigHelper.GetTextFromFile(Path.Combine(_rootPath, GroupConfigFileName));
			Name = ConfigHelper.GetValueRegex("<groupname>(.*)</groupname>", configContent);
			Tag = ConfigHelper.GetValueRegex("<content>(.*)</content>", configContent);
			Visible = ConfigHelper.GetValueRegex("<Visible>(.*)</Visible>", configContent) != "false";
			UserGranted = !configContent.ToLower().Contains("approvedusers") ||
						  ConfigHelper.GetValuesRegex("<user>(.*?)</user>", configContent)
							  .Any(user => user.Equals(Environment.UserName, StringComparison.OrdinalIgnoreCase));
			switch (Tag)
			{
				case "shortbutton":
					Type = TabGroupType.ShortButton;
					break;
				case "longbutton":
					Type = TabGroupType.LongButton;
					break;
				case "browsertoggle":
					Type = TabGroupType.BrowserPanel;
					break;
				case "settings":
					Type = TabGroupType.SettingsPanel;
					break;
				default:
					Type = TabGroupType.CustomControls;
					break;
			}
		}

		private void LoadItems()
		{
			foreach (var configFile in Directory
				.GetFiles(_rootPath, "*.xml")
				.Where(filePath => !GroupConfigFileName.Equals(Path.GetFileName(filePath), StringComparison.OrdinalIgnoreCase)))
			{
				Items.Add(TabGroupItem.Create(configFile));
			}
		}
	}
}
