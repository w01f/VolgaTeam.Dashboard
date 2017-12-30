using System.Collections.Generic;
using System.IO;
using Asa.Common.Core.Json;
using CommandCentral.BusinessClasses.Common;
using Newtonsoft.Json;

namespace CommandCentral.Configuraion
{
	public class Settings
	{
		public List<OutputFolder> OutputFolders { get; }

		public Settings()
		{
			OutputFolders = new List<OutputFolder>();
		}

		public static Settings Load(string settingsFilePath)
		{
			if (!File.Exists(settingsFilePath))
				return new Settings();
			return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsFilePath), new DefaultSerializeSettings());
		}

		public static void Save(Settings instance, string settingsFilePath)
		{
			File.WriteAllText(settingsFilePath, JsonConvert.SerializeObject(instance, new DefaultSerializeSettings()));
		}
	}
}
