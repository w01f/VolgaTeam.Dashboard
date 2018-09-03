using System;
using System.IO;
using Asa.Business.Solutions.Shift.Enums;
using Newtonsoft.Json;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class LayoutItem
	{
		private const string FilePrefix = "layout_";

		public string FilePath { get; private set; }
		public ProductLayoutType LayoutType { get; private set; }
		public int LayoutIndex { get; private set; }

		[JsonConstructor]
		private LayoutItem() { }

		private LayoutItem(string filePath)
		{
			FilePath = filePath;
			LayoutType = ProductLayoutType.Undefined;
			LayoutIndex = -1;
		}

		public static LayoutItem FromFile(string filePath)
		{
			var layoutItem = new LayoutItem(filePath);

			var fileName = Path.GetFileNameWithoutExtension(filePath);
			if (fileName.Contains(FilePrefix))
			{
				layoutItem.LayoutIndex = Int32.Parse(fileName.Replace(FilePrefix, String.Empty));
				layoutItem.LayoutType = layoutItem.LayoutIndex > 6 ?
					ProductLayoutType.Left :
					ProductLayoutType.Right;
			}

			return layoutItem;
		}
	}

}
