﻿using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class CoverLists
	{
		public List<SlideHeader> Headers { get; set; }

		public CoverLists()
		{
			Headers = new List<SlideHeader>();
		}

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var node = document.SelectSingleNode(@"/Cover");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						var header = SlideHeader.FromXml(childNode);
						if (!String.IsNullOrEmpty(header.Value))
							Headers.Add(header);
						break;
				}
			}
		}
	}
}
