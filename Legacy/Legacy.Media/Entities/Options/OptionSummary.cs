using System;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Options
{
	public class OptionSummary
	{
		public SpotType SpotType { get; set; }
		public bool ApplySettingsForAll { get; set; }

		public ContractSettings ContractSettings { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCampaign { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowTotalPeriods { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowTallySpots { get; set; }
		public bool ShowTallyCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UseDecimalRates { get; set; }
		#endregion

		public OptionSummary()
		{
			ApplySettingsForAll = false;
			ContractSettings = new ContractSettings();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;

			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "SpotType":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SpotType = (SpotType)temp;
						}
						break;
					case "ApplySettingsForAll":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplySettingsForAll = tempBool;
						break;
					case "ContractSettings":
						ContractSettings.Deserialize(childNode);
						break;

					#region Options
					case "ShowLineId":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLineId = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowCampaign":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCampaign = tempBool;
						break;
					case "ShowComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowComments = tempBool;
						break;
					case "ShowSpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpots = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowTotalPeriods":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalPeriods = tempBool;
						break;
					case "ShowTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalCost = tempBool;
						break;
					case "ShowTallySpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTallySpots = tempBool;
						break;
					case "ShowTallyCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTallyCost = tempBool;
						break;
					case "ShowSpotsX":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpotsX = tempBool;
						break;
					case "UseDecimalRates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseDecimalRates = tempBool;
						break;
						#endregion
				}
		}
	}
}
