using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asa.Core.Calendar;

namespace Asa.Core.Common
{
	public class OutputColorList
	{
		public List<ColorFolder> Items { get; private set; }

		public OutputColorList()
		{
			Items = new List<ColorFolder>();
		}

		public void Load(StorageDirectory colorListFolder)
		{
			Items.Clear();

			if (!colorListFolder.ExistsLocal()) return;

			foreach (var folder in colorListFolder.GetFolders())
			{
				var files = folder.GetFiles().ToList();

				var colorFolder = new ColorFolder();
				colorFolder.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(folder.Name);

				var imageFile = files.FirstOrDefault(file => file.Name == "image.png");
				if (imageFile != null)
					colorFolder.Logo = new Bitmap(imageFile.LocalPath);

				var schemaFile = files.FirstOrDefault(file => file.Name == "color_scheme.txt");
				colorFolder.Schema = schemaFile != null ?
				ColorSchema.Parse(File.ReadAllText(schemaFile.LocalPath)) :
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
