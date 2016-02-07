using System;

namespace Asa.Common.Core.Objects.Output
{
	public class ContractSettings
	{
		public bool ShowSignatureLine { get; set; }
		public bool ShowDisclaimer { get; set; }
		public DateTime? RateExpirationDate { get; set; }

		public bool IsConfigured
		{
			get { return ShowSignatureLine || ShowDisclaimer || RateExpirationDate.HasValue; }
		}

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
	}
}
