using System;
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
		public string Info { get; set; }
		public string CompiledInfo
		{
			get
			{
				if (!String.IsNullOrEmpty(Info))
					return Info;
				return String.Empty;
			}
		}

		public RequestDigitalInfoEventArgs RequestOptions =>
		new RequestDigitalInfoEventArgs(
			null,
			ShowWebsites,
			ShowProduct,
			ShowDimensions,
			ShowDates,
			ShowImpressions,
			ShowCPM,
			ShowInvestment);

		public DigitalLegend()
		{
			Enabled = false;
			ResetDefaults();
		}

		public void ResetDefaults()
		{
			ShowWebsites = false;
			ShowProduct = true;
			ShowDimensions = false;
			ShowDates = false;
			ShowImpressions = false;
			ShowCPM = false;
			ShowInvestment = false;
		}
	}
}
