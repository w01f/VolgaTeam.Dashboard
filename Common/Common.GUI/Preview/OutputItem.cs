using System;
using System.IO;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Microsoft.Office.Interop.PowerPoint;

namespace Asa.Common.GUI.Preview
{
	public class OutputItem
	{
		public string Name { get; set; }
		public string PresentationSourcePath { get; set; }
		public int SlidesCount { get; set; }
		public bool IsCurrent { get; set; }
		public Action<PowerPointProcessor, Presentation> SlideGeneratingAction { get; set; }
		public Action<PowerPointProcessor, string> PreviewGeneratingAction { get; set; }

		public bool InsertOnTop { get; set; }

		public bool Enabled { get; set; }

		public string ImageSourcePath => PresentationSourcePath.Replace(Path.GetExtension(PresentationSourcePath), String.Empty);

		public OutputItem()
		{
			Enabled = true;
		}

		public void ClearAssets()
		{
			try
			{
				File.Delete(PresentationSourcePath);
				Utilities.DeleteFolder(ImageSourcePath);
			}
			catch { }
		}
	}
}
