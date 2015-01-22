using System.Collections.Generic;

namespace CommandCentral.Entities.Newspaper
{
	internal class MechanicalType
	{
		public MechanicalType()
		{
			Name = string.Empty;
			Items = new List<MechanicalItem>();
		}

		public string Name { get; set; }
		public List<MechanicalItem> Items { get; set; }
	}
}