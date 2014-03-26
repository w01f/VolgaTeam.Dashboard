using System.Xml.Linq;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.Dashboard
{
	public class SectionActivity : UserActivity
	{
		public string SectionName { get; private set; }

		public SectionActivity(string sectionName)
			: base("Section Selected")
		{
			SectionName = sectionName;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Section", SectionName));
			return element;
		}
	}
}
