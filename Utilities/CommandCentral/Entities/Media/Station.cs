using System.Drawing;

namespace CommandCentral.Entities.Media
{
	internal class Station
	{
		public Station()
		{
			Name = string.Empty;
		}

		public string Name { get; set; }
		public Image Logo { get; set; }
	}
}