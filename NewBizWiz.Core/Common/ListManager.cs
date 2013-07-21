using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		private const string AdvertisersFileName = @"Advertisers.xml";
		private const string DecisionMakersFileName = @"DecisionMakers.xml";
		private string LocalListFolder { get; set; }

		private ListManager()
		{
			Advertisers = new List<string>();
			DecisionMakers = new List<string>();
			Statuses = new List<string>();

			LocalListFolder = Path.Combine(String.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + SettingsManager.Instance.AppID.ToString(), @"User_lists");
			if (!Directory.Exists(LocalListFolder))
				Directory.CreateDirectory(LocalListFolder);

			LoadAdvertisers();
			LoadDecisionMakers();
		}

		public static ListManager Instance
		{
			get { return _instance; }
		}
		public List<string> Advertisers { get; set; }
		public List<string> DecisionMakers { get; set; }
		public List<string> Statuses { get; set; }

		private void LoadAdvertisers()
		{
			Advertisers.Clear();
			string listPath = Path.Combine(LocalListFolder, AdvertisersFileName);
			if (File.Exists(listPath))
			{
				var document = new XmlDocument();
				document.Load(listPath);

				XmlNode node = document.SelectSingleNode(@"/Advertisers");
				if (node != null)
				{
					foreach (XmlNode childeNode in node.ChildNodes)
					{
						if (!Advertisers.Contains(childeNode.InnerText))
							Advertisers.Add(childeNode.InnerText);
					}
				}
			}
		}

		private void LoadDecisionMakers()
		{
			DecisionMakers.Clear();
			string listPath = Path.Combine(LocalListFolder, DecisionMakersFileName);
			if (File.Exists(listPath))
			{
				var document = new XmlDocument();
				document.Load(listPath);

				XmlNode node = document.SelectSingleNode(@"/DecisionMakers");
				if (node != null)
				{
					foreach (XmlNode childeNode in node.ChildNodes)
					{
						if (!DecisionMakers.Contains(childeNode.InnerText))
							DecisionMakers.Add(childeNode.InnerText);
					}
				}
			}
		}

		public void SaveAdvertisers()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Advertisers>");
			foreach (string advertiser in Advertisers)
				xml.AppendLine(@"<Advertiser>" + advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
			xml.AppendLine(@"</Advertisers>");

			string userConfigurationPath = Path.Combine(LocalListFolder, AdvertisersFileName);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void SaveDecisionMakers()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<DecisionMakers>");
			foreach (string decisionMaker in DecisionMakers)
				xml.AppendLine(@"<DecisionMaker>" + decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			xml.AppendLine(@"</DecisionMakers>");

			string userConfigurationPath = Path.Combine(LocalListFolder, DecisionMakersFileName);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}

	public class NameCodePair
	{
		public NameCodePair()
		{
			Name = string.Empty;
			Code = string.Empty;
		}

		public string Name { get; set; }
		public string Code { get; set; }

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<NameCodePair ");
			xml.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Code = \"" + Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.AppendLine(@"/>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "Code":
						Code = attribute.Value;
						break;
				}
		}
	}
}
