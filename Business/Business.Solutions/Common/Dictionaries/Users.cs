using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Common.Dictionaries
{
	public class Users
	{
		private readonly List<User> _users = new List<User>();

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var userNodes = document.SelectNodes(@"//Users/User");
			if (userNodes == null) return;
			foreach (var userNode in userNodes.OfType<XmlNode>())
			{
				var user = new User();
				foreach (XmlAttribute attribute in userNode.Attributes)
				{
					switch (attribute.Name)
					{
						case "FirstName":
							user.FirstName = attribute.Value;
							break;
						case "LastName":
							user.LastName = attribute.Value;
							break;
						case "Phone":
							user.Phone = attribute.Value;
							break;
						case "Email":
							user.Email = attribute.Value;
							break;
						case "Title":
							user.Title = attribute.Value;
							break;
						case "IsAdmin":
							bool.TryParse(attribute.Value, out var tempBool);
							user.IsAdmin = tempBool;
							break;
					}
				}

				var groupNodes = userNode.SelectNodes("./Group");
				if (groupNodes != null)
					user.Groups.AddRange(groupNodes.OfType<XmlNode>().Select(node => node.InnerText));

				_users.Add(user);
			}
		}

		public User[] GetUsersByStation(string stationName)
		{
			return _users.ToArray();
		}
	}
}
