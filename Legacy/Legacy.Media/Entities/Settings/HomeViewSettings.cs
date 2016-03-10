using System.Xml;

namespace Asa.Legacy.Media.Entities.Settings
{
	public class HomeViewSettings
	{
		public HomeViewSettings()
		{
			EnableAccountNumber = true;
			EnablePrintDelivery = true;
			EnablePrintReadership = true;
			EnablePrintLogo = true;
			EnablePrintCode = true;
			EnableDigitalDimensions = true;
			EnableDigitalStrategy = true;
			EnableDigitalLocation = true;
			EnableDigitalTargeting = true;
			EnableDigitalRichMedia = true;

			ShowAccountNumber = false;
			ShowPrintDelivery = false;
			ShowPrintReadership = false;
			ShowPrintLogo = true;
			ShowPrintCode = true;
			ShowDigitalDimensions = true;
			ShowDigitalStrategy = true;
			ShowDigitalLocation = true;
			ShowDigitalTargeting = true;
			ShowDigitalRichMedia = true;
		}

		public bool EnableAccountNumber { get; set; }
		public bool EnablePrintDelivery { get; set; }
		public bool EnablePrintReadership { get; set; }
		public bool EnablePrintLogo { get; set; }
		public bool EnablePrintCode { get; set; }
		public bool EnableDigitalDimensions { get; set; }
		public bool EnableDigitalStrategy { get; set; }
		public bool EnableDigitalLocation { get; set; }
		public bool EnableDigitalTargeting { get; set; }
		public bool EnableDigitalRichMedia { get; set; }

		public bool ShowAccountNumber { get; set; }
		public bool ShowPrintDelivery { get; set; }
		public bool ShowPrintReadership { get; set; }
		public bool ShowPrintLogo { get; set; }
		public bool ShowPrintCode { get; set; }
		public bool ShowDigitalDimensions { get; set; }
		public bool ShowDigitalStrategy { get; set; }
		public bool ShowDigitalLocation { get; set; }
		public bool ShowDigitalTargeting { get; set; }
		public bool ShowDigitalRichMedia { get; set; }

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableAccountNumber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAccountNumber = tempBool;
						break;
					case "EnableCode":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePrintCode = tempBool;
						break;
					case "EnableDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePrintDelivery = tempBool;
						break;
					case "EnableLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePrintLogo = tempBool;
						break;
					case "EnableReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePrintReadership = tempBool;
						break;
					case "EnableDigitalDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalDimensions = tempBool;
						break;
					case "EnableDigitalStrategy":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalStrategy = tempBool;
						break;
					case "EnableDigitalLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalLocation = tempBool;
						break;
					case "EnableDigitalTargeting":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalTargeting = tempBool;
						break;
					case "EnableDigitalRichMedia":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalRichMedia = tempBool;
						break;

					case "ShowAccountNumber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAccountNumber = tempBool;
						break;
					case "ShowCode":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintCode = tempBool;
						break;
					case "ShowDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintDelivery = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintLogo = tempBool;
						break;
					case "ShowReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintReadership = tempBool;
						break;
					case "ShowDigitalDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalDimensions = tempBool;
						break;
					case "ShowDigitalStrategy":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalStrategy = tempBool;
						break;
					case "ShowDigitalLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalLocation = tempBool;
						break;
					case "ShowDigitalTargeting":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalTargeting = tempBool;
						break;
					case "ShowDigitalRichMedia":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalRichMedia = tempBool;
						break;
				}
			}

			ShowAccountNumber &= EnableAccountNumber;
			ShowPrintDelivery &= EnablePrintDelivery;
			ShowPrintReadership &= EnablePrintReadership;
			ShowPrintLogo &= EnablePrintLogo;
			ShowPrintCode &= EnablePrintCode;
			ShowDigitalDimensions &= EnableDigitalDimensions;
			ShowDigitalStrategy &= EnableDigitalStrategy;
			ShowDigitalLocation &= EnableDigitalLocation;
			ShowDigitalTargeting &= EnableDigitalTargeting;
			ShowDigitalRichMedia &= EnableDigitalRichMedia;
		}
	}
}
