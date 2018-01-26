using System;
using System.Collections.Generic;
using System.Xml;

namespace AdBar.PatchUpdater
{
	class PatchConfig
	{
		public string TargetFileName { get; private set; }
		public List<string> ExistedFileNames { get; }

		public PatchConfig()
		{
			ExistedFileNames = new List<String>();
		}

		public static PatchConfig FromXml(XmlNode configNode)
		{
			var instance = new PatchConfig();
			foreach (XmlNode childNode in configNode.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "OldName":
						instance.ExistedFileNames.Add(childNode.InnerText);
						break;
					case "NewName":
						instance.TargetFileName = childNode.InnerText;
						break;
				}
			}
			return instance;
		}
	}
}
