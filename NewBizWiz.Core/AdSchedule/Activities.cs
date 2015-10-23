using System.Xml.Linq;
using Asa.Core.Common;

namespace Asa.Core.AdSchedule
{
	public class PublicationOutputActivity : OutputActivity
	{
		public string Publication { get; private set; }

		public PublicationOutputActivity(string slideName, string advertiser, string publication, decimal dollarValue)
			: base(slideName, advertiser, dollarValue)
		{
			Publication = publication;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Publication", Publication));
			return element;
		}
	}
}
