using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewBizWiz.Core.Common
{
	public class GalleryManager
	{
		private string _urlScreenshots;
		private string _urlAdSpecs;
		private bool _isConfigured;

		public bool AutoLoad { get; private set; }

		public GalleryManager(string settingPath)
		{
			Init(settingPath);
		}

		private void Init(string settingsPath)
		{
			if (!File.Exists(settingsPath)) return;
			var settingsDoc = XDocument.Load(settingsPath);
			var node = settingsDoc.Descendants("Screenshots").FirstOrDefault();
			_urlScreenshots = node != null ? node.Value : null;
			node = settingsDoc.Descendants("AdSpecs").FirstOrDefault();
			_urlAdSpecs = node != null ? node.Value : null;
			node = settingsDoc.Descendants("AutoLoad").FirstOrDefault();
			bool temp;
			AutoLoad = node != null && Boolean.TryParse(node.Value, out temp) && temp;

			if (String.IsNullOrEmpty(_urlScreenshots) || String.IsNullOrEmpty(_urlAdSpecs)) return;
			_isConfigured = true;
			if (!_urlScreenshots.EndsWith("/")) _urlScreenshots += "/";
			if (!_urlAdSpecs.EndsWith("/")) _urlAdSpecs += "/";
		}

		public IEnumerable<SnapshotCollection> GetSnapshots()
		{
			return GetSnapshots(_urlScreenshots);
		}

		public IEnumerable<SnapshotCollection> GetAdSpecs()
		{
			return GetSnapshots(_urlAdSpecs);
		}

		private IEnumerable<SnapshotCollection> GetSnapshots(string url)
		{
			var result = new List<SnapshotCollection>();
			if (!_isConfigured) return result;
			foreach (var item in (IEnumerable)JsonConvert.DeserializeObject(new WebClient().DownloadString(url + "contents.php")))
			{
				var i = 0;
				JProperty tmp = null;
				foreach (var m in (IEnumerable)item)
				{
					switch (i)
					{
						case 0:
							tmp = (m as JProperty); i++;
							break;

						case 1:
							var s = new SnapshotCollection(tmp != null ? (String)tmp.Value : String.Empty);

							if (m != null)
								foreach (var property in ((IEnumerable)m).OfType<JArray>())
								{
									foreach (var ss in property.Select(prop => (String)prop).Where(ss => !String.IsNullOrEmpty(ss)))
										s.Screenshots.Add(new SnapshotItem(ss.Replace(s.Name, String.Empty), String.Format("{0}{1}/{2}/", url, s.Name, ss)));
									break;
								}

							if (s.Screenshots.Any())
								result.Add(s);
							break;
					}
					if (i > 1)
						break; // Next
				}
			}
			return result;
		}
	}

	public class SnapshotCollection
	{
		public string Name { get; private set; }

		public SnapshotCollection(string name)
		{
			Name = name;
			Screenshots = new List<SnapshotItem>();
		}

		public override String ToString()
		{
			return Name;
		}

		public List<SnapshotItem> Screenshots { get; private set; }
	}

	public class SnapshotItem
	{
		public string Name { get; private set; }
		public string Url { get; private set; }

		public SnapshotItem(string name, string url)
		{
			Url = url;
			Name = name.Trim().TrimStart(new[] { '-' }).Replace(" - ", "-").Replace("-", " - ");
		}

		public override String ToString()
		{
			return Name;
		}
	}
}
