using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CommandCentral.BusinessClasses.Common
{
	public static class ConvertorExtensions
	{
		public static IList<string> GetExternalOutputLines(string outputFilePath, string rootNodeName, IList<string> ignoreNodes)
		{
			var resultLines = new List<string>();
			if (File.Exists(outputFilePath))
			{
				var document = new XmlDocument();
				document.Load(outputFilePath);
				var rootNode = document.SelectSingleNode(String.Format(@"//{0}", rootNodeName));
				foreach (var childNode in rootNode.ChildNodes.OfType<XmlNode>().ToList())
				{
					if (ignoreNodes.Any(nodeName => String.Equals(childNode.Name, nodeName, StringComparison.OrdinalIgnoreCase)))
						continue;
					resultLines.Add(childNode.OuterXml);
				}
			}
			return resultLines;
		}
	}
}
