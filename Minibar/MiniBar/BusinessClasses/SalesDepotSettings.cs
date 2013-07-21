using System;
using System.IO;
using System.Xml;

namespace NewBizWiz.MiniBar.BusinessClasses
{
	public class SalesDepotSettings
	{
		private const string DefaultUserName = "Default";
		private readonly string _approvedLibrariesFile;
		private readonly string _settingsPath;

		public SalesDepotSettings()
		{
			_settingsPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\SDSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_approvedLibrariesFile = string.Format(@"{0}\newlocaldirect.com\Sales Depot\ApprovedLibraries.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ExecutablePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\SalesDepot.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			RemoteRootPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Remote Libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LocalLogoPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\sdbutton.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WebLogoPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\webbutton.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LocalIconPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\sdicon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WebIconPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\webicon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LocalAppName = "Sales Depot";
			WebAppName = "iSalesDepot";
			GroupName = "Launch Sales Depot";
			ShowLocalButton = true;

			Load();
			LoadApprovedLibraries();
		}

		public string ExecutablePath { get; set; }
		public string Url { get; set; }
		public string RemoteRootPath { get; set; }
		public string LocalLogoPath { get; set; }
		public string WebLogoPath { get; set; }
		public string LocalIconPath { get; set; }
		public string WebIconPath { get; set; }

		public string LocalAppName { get; set; }
		public string WebAppName { get; set; }
		public string GroupName { get; set; }

		public bool ShowLocalButton { get; set; }
		public bool ShowWebButton { get; set; }
		public bool UseRemoteSalesDepot { get; set; }

		private void Load()
		{
			if (!File.Exists(_settingsPath)) return;
			var document = new XmlDocument();
			document.Load(_settingsPath);

			XmlNode node = document.SelectSingleNode(@"/Root/LocalName");
			if (node != null)
				LocalAppName = node.InnerText;
			node = document.SelectSingleNode(@"/Root/WebName");
			if (node != null)
				WebAppName = node.InnerText;
			node = document.SelectSingleNode(@"/Root/GroupName");
			if (node != null)
				GroupName = node.InnerText;
			node = document.SelectSingleNode(@"/Root/Url");
			if (node != null)
				Url = node.InnerText;

			bool temp = false;
			node = document.SelectSingleNode(@"/Root/LocalButton");
			if (node != null && Boolean.TryParse(node.InnerText, out temp))
				ShowLocalButton = temp;

			node = document.SelectSingleNode(@"/Root/WebButton");
			if (node != null && Boolean.TryParse(node.InnerText, out temp))
				ShowWebButton = temp;
		}

		private void LoadApprovedLibraries()
		{
			UseRemoteSalesDepot = true;
			bool defaultUseRemoteSalesDepot = false;
			bool userExisted = false;
			if (File.Exists(_approvedLibrariesFile))
			{
				var document = new XmlDocument();
				document.Load(_approvedLibrariesFile);

				XmlNode node = document.SelectSingleNode(@"/ApprovedLibraries");
				if (node != null)
					foreach (XmlNode userNode in node.ChildNodes)
						if (userNode.Name.Equals("User"))
						{
							string userName = string.Empty;
							bool useRemoteLibraries = false;
							foreach (XmlAttribute attribute in userNode.Attributes)
							{
								switch (attribute.Name)
								{
									case "Name":
										userName = attribute.Value;
										break;
									case "UseRemoteLibraries":
										bool.TryParse(attribute.Value, out useRemoteLibraries);
										break;
								}
							}
							if (userName.Equals(Environment.UserName))
							{
								userExisted = true;
								UseRemoteSalesDepot = useRemoteLibraries;
								break;
							}
							if (userName.Equals(DefaultUserName))
								defaultUseRemoteSalesDepot = useRemoteLibraries;
						}
				if (!userExisted)
					UseRemoteSalesDepot = defaultUseRemoteSalesDepot;
			}
		}
	}
}