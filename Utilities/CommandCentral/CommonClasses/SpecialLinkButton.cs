using System;
using System.Collections.Generic;
using System.Drawing;

namespace CommandCentral.CommonClasses
{
	public class SpecialLinkButton
	{
		public string Name { get; set; }
		public string Tooltip { get; set; }
		public string Type { get; set; }
		public Image Image { get; set; }
		public List<string> Paths { get; private set; }

		public SpecialLinkButton()
		{
			Name = String.Empty;
			Tooltip = String.Empty;
			Type = String.Empty;
			Paths = new List<string>();
		}
	}
}
