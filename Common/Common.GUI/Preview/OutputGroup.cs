using System.Collections.Generic;

namespace Asa.Common.GUI.Preview
{
	public class OutputGroup
	{
		public string Name { get; set; }
		public bool IsCurrent { get; set; }
		public IList<OutputItem> Items { get; set; }

		public OutputGroup()
		{
			Items = new List<OutputItem>();
		}
	}
}
