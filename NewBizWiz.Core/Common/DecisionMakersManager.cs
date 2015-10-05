using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.Core.Common
{
	public class DecisionMakersManager
	{
		private const string DecisionMakersFileName = @"DecisionMakers.xml";

		private StorageFile _contentFile;

		public List<string> Items { get; private set; }

		public event EventHandler<EventArgs> ListChanged;

		protected virtual void OnListChanged()
		{
			var handler = ListChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		public DecisionMakersManager()
		{
			Items = new List<string>();
		}

		public void Load()
		{
			Items.Clear();

			_contentFile = new StorageFile(AppProfileManager.Instance.UserListsFolder.RelativePathParts.Merge(DecisionMakersFileName));
			_contentFile.AllocateParentFolder();

			if (!_contentFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(_contentFile.LocalPath);

			var node = document.SelectSingleNode(@"/DecisionMakers");
			if (node == null) return;
			foreach (XmlNode childeNode in node.ChildNodes)
			{
				if (!Items.Contains(childeNode.InnerText))
					Items.Add(childeNode.InnerText);
			}
		}

		public async Task Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<DecisionMakers>");
			foreach (string decisionMaker in Items)
				xml.AppendLine(@"<DecisionMaker>" + decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			xml.AppendLine(@"</DecisionMakers>");

			using (var sw = new StreamWriter(_contentFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
			await _contentFile.Upload();
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
