using System.IO;
using System.Linq;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration.Contract.TabD
{
	public class UserData
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string ImageFilePath { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public static UserData FromXml(XmlNode node, StorageDirectory resourceFolder)
		{
			var userData = new UserData();
			if (node != null)
			{
				var attributes = node.Attributes?.OfType<XmlAttribute>().ToArray() ?? new XmlAttribute[] { };
				foreach (var attribute in attributes)
				{
					switch (attribute.Name)
					{
						case "Name":
							userData.Name = attribute.Value;
							break;
						case "Title":
							userData.Description = attribute.Value;
							break;
						case "Phone":
							userData.Phone = attribute.Value;
							break;
						case "Email":
							userData.Email = attribute.Value;
							break;
						case "Clipart1":
							userData.ImageFilePath = Path.Combine(resourceFolder.LocalPath, attribute.Value);
							break;
					}
				}
			}
			return userData;
		}
	}
}
