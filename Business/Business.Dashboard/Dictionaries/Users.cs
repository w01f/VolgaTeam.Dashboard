using System.Collections.Generic;
using System.Xml;
using Asa.Business.Dashboard.Configuration;

namespace Asa.Business.Dashboard.Dictionaries
{
	public class Users
	{
		private readonly List<User> _users = new List<User>();

		public Users()
		{
			Load();
		}

		private void Load()
		{
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DataUsersFile.LocalPath);

			var node = document.SelectSingleNode(@"/Users");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.Name.Equals("User"))
				{
					var user = new User();
					foreach (XmlAttribute attribute in childNode.Attributes)
					{
						switch (attribute.Name)
						{
							case "Station":
								user.Station = attribute.Value;
								break;
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
							case "IsAdmin":
								bool tempBool;
								bool.TryParse(attribute.Value, out tempBool);
								user.IsAdmin = tempBool;
								break;
						}
					}
					_users.Add(user);
				}
			}
		}

		public User[] GetUsersByStation(string stationName)
		{
			return _users.ToArray();
		}
	}
}
