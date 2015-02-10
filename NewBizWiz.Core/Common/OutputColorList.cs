using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Core.Common
{
	public class OutputColorList
	{
		public List<ColorFolder> Items { get; private set; }

		public OutputColorList(string path)
		{
			Items = new List<ColorFolder>();
			LoadColors(path);
		}

		private void LoadColors(string colorFolderPath)
		{
			Items.Clear();
			if (!Directory.Exists(colorFolderPath)) return;
			foreach (var directory in Directory.GetDirectories(colorFolderPath))
			{
				var colorFolder = new ColorFolder();
				colorFolder.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Path.GetFileName(directory));
				var imagePath = Path.Combine(directory, "image.png");
				if (File.Exists(imagePath))
					colorFolder.Logo = new Bitmap(imagePath);
				var colorSchemaPath = Path.Combine(directory, "color_scheme.txt");
				colorFolder.Schema = File.Exists(colorSchemaPath) ? 
					ColorSchema.Parse(File.ReadAllText(colorSchemaPath)) : 
					new ColorSchema();
				Items.Add(colorFolder);
			}
		}
	}

	public class ColorFolder
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
		public ColorSchema Schema { get; set; }
	}
}
