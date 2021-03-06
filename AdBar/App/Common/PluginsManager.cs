﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Asa.Bar.App.Configuration;
using Asa.Bar.Plugins.Core;

namespace Asa.Bar.App.Common
{
	public class PluginsManager
	{
		private readonly string _root;

		public List<IAdBarControl> Controls { get; private set; }

		private static PluginsManager _instance;
		public static PluginsManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new PluginsManager();
				return _instance;
			}
		}

		private PluginsManager()
		{
			_root = Path.Combine(ResourceManager.Instance.AppRootFolderPath, "plugins");
			Controls = new List<IAdBarControl>();
			Load();
		}

		private void Load()
		{
			if (!Directory.Exists(_root)) return;
			var pluginType = typeof(IAdBarControl);
			var assemblyFiles = Directory.GetFiles(_root, "AdBar.Plugins.*.dll", SearchOption.AllDirectories);
			foreach (var assemblyFile in assemblyFiles)
			{
				try
				{
					var assembly = Assembly.LoadFrom(assemblyFile);
					if (assembly == null) continue;
					foreach (var type in assembly.GetTypes())
					{
						if (type.IsInterface || !pluginType.IsAssignableFrom(type)) continue;
						var plugin = Activator.CreateInstance(type) as IAdBarControl;
						if (plugin == null) continue;
						plugin.StateChanged += (o, e) => UpdateControls(o as IAdBarControl, e.StateParameters);
						Controls.Add(plugin);
					}
				}
				catch { }
			}
		}

		private void UpdateControls(IAdBarControl raisedBy, object[] stateParameters)
		{
			Controls.ForEach(c => c.UpdateControl(raisedBy, stateParameters));
		}
	}
}
