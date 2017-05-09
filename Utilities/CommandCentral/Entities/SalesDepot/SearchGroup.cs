using System.Collections.Generic;

namespace CommandCentral.Entities.SalesDepot
{
	internal class SearchGroup
	{
		public SearchGroup()
		{
			Name = string.Empty;
			Description = string.Empty;
			Tags = new List<string>();
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public string TopGroupName { get; set; }
		public string TopGroupIcon { get; set; }
		public List<string> Tags { get; set; }
	}
}