using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Objects.Output
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

			foreach (var folder in colorListFolder.GetLocalFolders())
			{
				var files = folder.GetLocalFiles().ToList();

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
}
