using System;
using System.Collections.Generic;
using Asa.Business.Online.Common;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class DigitalLegend
	{
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

		public RequestDigitalInfoEventArgs RequestOptions
		{
			get { return new RequestDigitalInfoEventArgs(null, ShowWebsites, ShowProduct, ShowDimensions, ShowDates, ShowImpressions, ShowCPM, ShowInvestment); }
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
}
