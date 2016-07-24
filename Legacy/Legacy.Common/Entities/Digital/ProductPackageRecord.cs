using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Digital
{
	public class ProductPackageRecord
	{
		private string _category;
		private string _subCategory;
		private string _name;
		private string _info;
		private string _comments;
		private decimal? _rate;
		private decimal? _investment;
		private decimal? _impressions;
		private decimal? _cpm;

		public bool UseFormula { get; set; }

		public string Category => _category;

		public string SubCategory => _subCategory;

		public string Name => _name;

		public string Info => _info;

		public string Comments => _comments;

		public decimal? Rate => _rate;

		public decimal? Investment => _investment;

		public decimal? Impressions => _impressions;

		public decimal? CPM => _cpm;

		public void Deserialize(XmlNode node)
		{
			decimal tempDecimal;

			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "Category":
						_category = childNode.InnerText;
						break;
					case "SubCategory":
						_subCategory = childNode.InnerText;
						break;
					case "Name":
						_name = childNode.InnerText;
						break;
					case "Info":
						_info = childNode.InnerText;
						break;
					case "Comments":
						_comments = childNode.InnerText;
						break;
					case "Rate":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_rate = tempDecimal;
						break;
					case "Investment":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_investment = tempDecimal;
						break;
					case "Impressions":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_impressions = tempDecimal;
						break;
					case "CPM":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_cpm = tempDecimal;
						break;
					case "UseFormula":
						bool tempBool;
						if (Boolean.TryParse(childNode.InnerText, out tempBool))
							UseFormula = tempBool;
						break;
				}
		}
	}
}
