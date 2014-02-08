using System;
using System.IO;

namespace NewBizWiz.CommonGUI.Preview
{
	public class PreviewGroup
	{
		public string Name { get; set; }
		public string PresentationSourcePath { get; set; }

		public string ImageSourcePath
		{
			get { return PresentationSourcePath.Replace(Path.GetExtension(PresentationSourcePath), String.Empty); }
		}

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
