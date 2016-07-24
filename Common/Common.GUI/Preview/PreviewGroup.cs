using System;
using System.IO;

namespace Asa.Common.GUI.Preview
{
	public class PreviewGroup
	{
		public string Name { get; set; }
		public string PresentationSourcePath { get; set; }

		public bool InsertOnTop { get; set; }

		public string ImageSourcePath => PresentationSourcePath.Replace(Path.GetExtension(PresentationSourcePath), String.Empty);

		public void ClearAssets()
		{
			try
			{
				File.Delete(PresentationSourcePath);
				Directory.Delete(ImageSourcePath, true);
			}
			catch { }
		}
	}
}
