using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Asa.Business.Online.Enums;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class ProductInfo : IJsonCloneable<ProductInfo>
	{
		public ProductInfoType Type { get; set; }
		public bool Selected { get; set; }
		public string Group { get; set; }
		public List<string> Phrases { get; private set; }
		public string UserValue { get; set; }

		public string Key
		{
			get { return JoinedPhrases; }
		}

		public string JoinedPhrases
		{
			get { return String.Join("  -  ", Phrases); }
		}

		public string EditValue
		{
			get { return String.IsNullOrEmpty(UserValue) ? JoinedPhrases : UserValue; }
			set { UserValue = value != JoinedPhrases ? value : null; }
		}

		public ProductInfo()
		{
			Phrases = new List<string>();
		}

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Type>" + (Int32)Type + @"</Type>");
			xml.AppendLine(@"<Selected>" + Selected + @"</Selected>");
			if (!String.IsNullOrEmpty(Group))
				xml.AppendLine(@"<Group>" + Group.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Group>");
			foreach (var phrase in Phrases)
			{
				xml.AppendLine(@"<Phrase>" + phrase.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Phrase>");
			}
			if (!String.IsNullOrEmpty(UserValue))
				xml.AppendLine(@"<UserValue>" + UserValue.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</UserValue>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "Type":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								Type = (ProductInfoType)temp;
						}
						break;
					case "Selected":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								Selected = temp;
						}
						break;
					case "Group":
						Group = childNode.InnerText;
						break;
					case "Phrase":
						Phrases.Add(childNode.InnerText);
						break;
					case "UserValue":
						UserValue = childNode.InnerText;
						break;
				}
		}

		public void ApplyValues(ProductInfo source)
		{
			if (source != null)
			{
				Selected = source.Selected;
				UserValue = source.UserValue;
			}
		}

		public void AfterClone(ProductInfo source, bool fullClone = true) { }
	}
}
