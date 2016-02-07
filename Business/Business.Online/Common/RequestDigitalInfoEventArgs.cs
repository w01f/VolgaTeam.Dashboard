using System;

namespace Asa.Business.Online.Common
{
	public class RequestDigitalInfoEventArgs : EventArgs
	{
		public bool ShowWebsites { get; private set; }
		public bool ShowProduct { get; private set; }
		public bool ShowDimensions { get; private set; }
		public bool ShowDates { get; private set; }
		public bool ShowImpressions { get; private set; }
		public bool ShowCPM { get; private set; }
		public bool ShowInvestment { get; private set; }
		public string Separator { get; set; }
		public object Editor { get; private set; }

		public RequestDigitalInfoEventArgs(object editor, bool showWebsites, bool showProduct, bool showDimensions, bool showDates, bool showImpressions, bool showCPM, bool showInvestmenst, string separator = "")
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
	}
}
