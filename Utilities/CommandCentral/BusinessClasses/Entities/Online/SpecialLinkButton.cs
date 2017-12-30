using System;
using System.Collections.Generic;
using System.Drawing;

namespace CommandCentral.BusinessClasses.Entities.Online
{
	public class SpecialLinkButton
	{
		public string Name { get; set; }
		public string Tooltip { get; set; }
		public string Type { get; set; }
		public Image Image { get; set; }
		public List<string> Paths { get; }

		public SpecialLinkButton()
		{
			Name = String.Empty;
			Tooltip = String.Empty;
			Type = String.Empty;
			Paths = new List<string>();
		}
	}
}
