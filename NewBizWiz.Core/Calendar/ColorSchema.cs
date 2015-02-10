using System;
using System.Drawing;
using System.Linq;

namespace NewBizWiz.Core.Calendar
{
	public class ColorSchema
	{
		public Color ActiveBackColor { get; set; }
		public Color ActiveForeColor { get; set; }
		public Color ActiveBodyColor { get; set; }
		public Color InactiveBackColor { get; set; }
		public Color InactiveForeColor { get; set; }
		public Color InactiveBodyColor { get; set; }
		public Color HeaderBackColor { get; set; }
		public Color HeaderForeColor { get; set; }
		public Color LineColor { get; set; }

		public ColorSchema()
		{
			ActiveBackColor = Color.White;
			ActiveForeColor = Color.Black;
			ActiveBodyColor = Color.White;
			InactiveBackColor = Color.LightGray;
			InactiveForeColor = Color.Black;
			InactiveBodyColor = Color.White;
			HeaderBackColor = Color.White;
			HeaderForeColor = Color.Black;
			LineColor = Color.DarkGray;
		}

		public static ColorSchema Parse(string content)
		{
			var result = new ColorSchema();
			var headerName = String.Empty;
			foreach (var line in content.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
			{
				if (String.IsNullOrEmpty(line)) continue;
				if (line.StartsWith("*"))
					headerName = line.Substring(1).Trim();
				else
				{
					var colorParts = line.Split(new[] { "," }, StringSplitOptions.None)
						.Select(part =>
						{
							int temp;
							if (Int32.TryParse(part, out temp))
								return temp;
							return -1;
						})
						.Where(part => part != -1)
						.ToArray();
					if (colorParts.Length != 3) continue;
					var color = Color.FromArgb(colorParts[0], colorParts[1], colorParts[2]);
					switch (headerName)
					{
						case "Day Names":
							result.HeaderForeColor = color;
							break;
						case "Dead Day Number Fill":
							result.InactiveBackColor = color;
							break;
						case "Dead Day Number Text":
							result.InactiveForeColor = color;
							break;
						case "Dead Day Square Fill":
							result.InactiveBodyColor = color;
							break;
						case "Active Day Number Text":
							result.ActiveForeColor = color;
							break;
						case "Lines":
							result.LineColor = color;
							break;
					}
				}
			}
			return result;
		}
	}
}
