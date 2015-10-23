using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DevExpress.XtraEditors;

namespace Asa.Core.OnlineSchedule
{
	public class DigitalLegend
	{
		public DigitalLegend()
		{
			Enabled = false;
			ShowWebsites = true;
			ShowProduct = true;
			ShowDimensions = false;
			ShowDates = false;
			ShowImpressions = false;
			ShowCPM = false;
			ShowInvestment = false;
		}

		public bool Enabled { get; set; }
		public bool AllowEdit { get; set; }
		public bool ApplyForAll { get; set; }
		public bool OutputOnlyOnce { get; set; }

		public bool ShowWebsites { get; set; }
		public bool ShowProduct { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowDates { get; set; }
		public bool ShowImpressions { get; set; }
		public bool ShowCPM { get; set; }
		public bool ShowInvestment { get; set; }
		public string Info1 { get; set; }
		public string Info2 { get; set; }
		public string Info3 { get; set; }
		public decimal? Total { get; set; }
		public decimal? Monthly { get; set; }

		public string CompiledInfo
		{
			get
			{
				if (!String.IsNullOrEmpty(Info1))
					return Info1;
				if (!String.IsNullOrEmpty(Info2))
					return Info2;
				if (!String.IsNullOrEmpty(Info3))
					return Info3;
				return String.Empty;
			}
		}

		public RequestDigitalInfoEventArgs RequestOptions
		{
			get { return new RequestDigitalInfoEventArgs(null, ShowWebsites, ShowProduct, ShowDimensions, ShowDates, ShowImpressions, ShowCPM, ShowInvestment); }
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<Enabled>" + Enabled + @"</Enabled>");
			result.AppendLine(@"<AllowEdit>" + AllowEdit + @"</AllowEdit>");
			result.AppendLine(@"<ApplyForAll>" + ApplyForAll + @"</ApplyForAll>");
			result.AppendLine(@"<OutputOnlyOnce>" + OutputOnlyOnce + @"</OutputOnlyOnce>");

			result.AppendLine(@"<ShowWebsites>" + ShowWebsites + @"</ShowWebsites>");
			result.AppendLine(@"<ShowProduct>" + ShowProduct + @"</ShowProduct>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions + @"</ShowDimensions>");
			result.AppendLine(@"<ShowDates>" + ShowDates + @"</ShowDates>");
			result.AppendLine(@"<ShowImpressions>" + ShowImpressions + @"</ShowImpressions>");
			result.AppendLine(@"<ShowCPM>" + ShowCPM + @"</ShowCPM>");
			result.AppendLine(@"<ShowInvestment>" + ShowInvestment + @"</ShowInvestment>");
			if (!String.IsNullOrEmpty(Info1))
				result.AppendLine(@"<Info1>" + Info1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Info1>");
			if (!String.IsNullOrEmpty(Info2))
				result.AppendLine(@"<Info2>" + Info2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Info2>");
			if (!String.IsNullOrEmpty(Info3))
				result.AppendLine(@"<Info3>" + Info3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Info3>");
			if (Total.HasValue)
				result.AppendLine(@"<Total>" + Total + @"</Total>");
			if (Monthly.HasValue)
				result.AppendLine(@"<Monthly>" + Total + @"</Monthly>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "Enabled":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Enabled = tempBool;
						break;
					case "AllowEdit":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							AllowEdit = tempBool;
						break;
					case "ApplyForAll":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAll = tempBool;
						break;
					case "OutputOnlyOnce":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							OutputOnlyOnce = tempBool;
						break;

					case "ShowWebsites":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowWebsites = tempBool;
						break;
					case "ShowProduct":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowProduct = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDates = tempBool;
						break;
					case "ShowImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowImpressions = tempBool;
						break;
					case "ShowCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCPM = tempBool;
						break;
					case "ShowInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInvestment = tempBool;
						break;
					case "Info1":
						Info1 = childNode.InnerText;
						break;
					case "Info2":
						Info2 = childNode.InnerText;
						break;
					case "Info3":
						Info3 = childNode.InnerText;
						break;
					case "Total":
						decimal total;
						if (Decimal.TryParse(childNode.InnerText, out total))
							Total = total;
						break;
					case "Monthly":
						decimal monthly;
						if (Decimal.TryParse(childNode.InnerText, out monthly))
							Monthly = monthly;
						break;
				}
			}
		}

		public DigitalLegend Clone()
		{
			var result = new DigitalLegend();
			result.Enabled = Enabled;
			result.AllowEdit = AllowEdit;
			result.ApplyForAll = ApplyForAll;
			result.OutputOnlyOnce = OutputOnlyOnce;

			result.ShowWebsites = ShowWebsites;
			result.ShowProduct = ShowProduct;
			result.ShowDimensions = ShowDimensions;
			result.ShowDates = ShowDates;
			result.ShowImpressions = ShowImpressions;
			result.ShowCPM = ShowCPM;
			result.ShowInvestment = ShowInvestment;
			result.Info1 = Info1;
			result.Info2 = Info2;
			result.Info3 = Info3;
			result.Total = Total;
			result.Monthly = Monthly;

			return result;
		}

		public string GetAdditionalData(string separator = "")
		{
			separator = String.IsNullOrEmpty(separator) ? Environment.NewLine : separator;
			var result = new List<string>();
			if (Total.HasValue)
				result.Add(String.Format("[Total Digital Investment: {0}]", Total.Value.ToString("$#,##0")));
			if (Monthly.HasValue)
				result.Add(String.Format("[Monthly Digital Investment: {0}]", Monthly.Value.ToString("$#,##0")));
			return String.Join(separator, result);
		}
	}

	public class RequestDigitalInfoEventArgs : EventArgs
	{
		public RequestDigitalInfoEventArgs(BaseEdit editor, bool showWebsites, bool showProduct, bool showDimensions, bool showDates, bool showImpressions, bool showCPM, bool showInvestmenst, string separator = "")
		{
			Editor = editor;
			ShowWebsites = showWebsites;
			ShowProduct = showProduct;
			ShowDimensions = showDimensions;
			ShowDates = showDates;
			ShowImpressions = showImpressions;
			ShowCPM = showCPM;
			ShowInvestment = showInvestmenst;
			Separator = !String.IsNullOrEmpty(separator) ? separator : Environment.NewLine;
		}

		public bool ShowWebsites { get; private set; }
		public bool ShowProduct { get; private set; }
		public bool ShowDimensions { get; private set; }
		public bool ShowDates { get; private set; }
		public bool ShowImpressions { get; private set; }
		public bool ShowCPM { get; private set; }
		public bool ShowInvestment { get; private set; }
		public string Separator { get; set; }

		public BaseEdit Editor { get; private set; }
	}
}