using System.Collections.Generic;
using System.Drawing;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarChildTabInfo : StarTabInfo
	{
		public abstract StarChildTabType TabType { get; }
		public Image RightLogo { get; protected set; }
		public Image FooterLogo { get; protected set; }
		public Image BackgroundLogo { get; protected set; }
		public List<ListDataItem> HeadersItems { get; set; }

		protected StarChildTabInfo()
		{
			HeadersItems = new List<ListDataItem>();
		}
	}
}
