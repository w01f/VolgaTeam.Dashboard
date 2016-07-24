using System;
using System.Text;
using System.Xml;

namespace Asa.Common.Core.Objects.Output
{
	public class ContractSettings
	{
		public bool ShowSignatureLine { get; set; }
		public bool ShowDisclaimer { get; set; }
		public DateTime? RateExpirationDate { get; set; }

		public bool IsConfigured => ShowSignatureLine || ShowDisclaimer || RateExpirationDate.HasValue;

		public string TemplateName
		{
			get
			{
				if (ShowSignatureLine && ShowDisclaimer && RateExpirationDate.HasValue)
					return "ca_re_ds.pptx";
				if (ShowSignatureLine && ShowDisclaimer)
					return "ca_ds_only.pptx";
				if (ShowSignatureLine && RateExpirationDate.HasValue)
					return "ca_re_only.pptx";
				if (ShowDisclaimer && RateExpirationDate.HasValue)
					return "re_ds_only.pptx";
				if (ShowSignatureLine)
					return "ca_only.pptx";
				if (ShowDisclaimer)
					return "ds_only.pptx";
				if (RateExpirationDate.HasValue)
					return "re_only.pptx";
				return String.Empty;
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowSignatureLine>" + ShowSignatureLine + @"</ShowSignatureLine>");
			result.AppendLine(@"<ShowDisclaimer>" + ShowDisclaimer + @"</ShowDisclaimer>");
			if (RateExpirationDate.HasValue)
				result.AppendLine(@"<RateExpirationDate>" + RateExpirationDate.Value + @"</RateExpirationDate>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowSignatureLine":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSignatureLine = temp;
						}
						break;
					case "ShowDisclaimer":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDisclaimer = temp;
						}
						break;
					case "RateExpirationDate":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								RateExpirationDate = temp;
						}
						break;
				}
			}
		}
	}
}
