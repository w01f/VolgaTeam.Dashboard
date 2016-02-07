using System;
using System.IO;
using System.Xml.Serialization;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Helpers
{
	public static class SettingsSerializeHelper
	{
		public static TSettings Load<TSettings>(StorageFile settingsFile) where TSettings : class
		{
			var defaultSettings = Activator.CreateInstance<TSettings>();
			if (settingsFile.ExistsLocal())
			{
				try
				{
					using (var stream = File.OpenRead(settingsFile.LocalPath))
					{
						var bf = new XmlSerializer(typeof(TSettings));
						return (TSettings)bf.Deserialize(stream);
					}
				}
				catch
				{
				}
			}
			return defaultSettings;
		}

		public static void Save<TSettings>(this TSettings target, StorageFile settingsFile) where TSettings : class
		{
			try
			{
				using (var stream = File.CreateText(settingsFile.LocalPath))
				{
					var bf = new XmlSerializer(typeof(TSettings));
					bf.Serialize(stream, target);
				}
			}
			catch
			{
			}
		}
	}
}
