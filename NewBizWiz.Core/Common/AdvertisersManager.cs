using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.Core.Common
{
	public class AdvertisersManager
	{
		private const string AdvertisersFileName = @"Advertisers.xml";
		private readonly string _localListFolder;
		public List<string> Items { get; private set; }

		public event EventHandler<EventArgs> ListChanged;

		protected virtual void OnListChanged()
		{
			var handler = ListChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		public AdvertisersManager(string localListFolder)
		{
			_localListFolder = localListFolder;
			Items = new List<string>();
			Load();
		}

		private void Load()
		{
			Items.Clear();
			var listPath = Path.Combine(_localListFolder, AdvertisersFileName);
			if (!File.Exists(listPath)) return;
			var document = new XmlDocument();
			document.Load(listPath);

			var node = document.SelectSingleNode(@"/Advertisers");
			if (node == null) return;
			foreach (XmlNode childeNode in node.ChildNodes)
			{
				if (!Items.Contains(childeNode.InnerText))
					Items.Add(childeNode.InnerText);
			}
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Advertisers>");
			foreach (string advertiser in Items)
				xml.AppendLine(@"<Advertiser>" + advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
			xml.AppendLine(@"</Advertisers>");

			string userConfigurationPath = Path.Combine(_localListFolder, AdvertisersFileName);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void Add(string item)
		{
			if (String.IsNullOrEmpty(item) || Items.Contains(item)) return;
			Items.Add(item);
			Items.Sort(WinAPIHelper.StrCmpLogicalW);
			OnListChanged();
		}

		public void AddRange(IEnumerable<string> items)
		{
			foreach (var item in items)
				Add(item);
		}

		public void Clear()
		{
			Items.Clear();
		}
	}
}
