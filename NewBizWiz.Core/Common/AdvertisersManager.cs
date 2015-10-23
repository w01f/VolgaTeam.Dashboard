using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Asa.Core.Interop;

namespace Asa.Core.Common
{
	public class AdvertisersManager
	{
		private const string AdvertisersFileName = @"Advertisers.xml";

		private StorageFile _contentFile;

		public List<string> Items { get; private set; }

		public event EventHandler<EventArgs> ListChanged;

		protected virtual void OnListChanged()
		{
			var handler = ListChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		public AdvertisersManager()
		{
			Items = new List<string>();
		}

		public void Load()
		{
			Items.Clear();

			_contentFile = new StorageFile(ResourceManager.Instance.UserListsFolder.RelativePathParts.Merge(AdvertisersFileName));
			_contentFile.AllocateParentFolder();

			if (!_contentFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(_contentFile.LocalPath);

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

			using (var sw = new StreamWriter(_contentFile.LocalPath, false))
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
