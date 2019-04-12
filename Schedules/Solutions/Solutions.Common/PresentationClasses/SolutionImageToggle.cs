using System;
using System.Drawing;
using System.IO;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.GUI.Common;
using DevExpress.Utils.Svg;

namespace Asa.Solutions.Common.PresentationClasses
{
	public class SolutionImageToggle : ImageToggleButton, ISolutionToggle
	{
		public BaseSolutionInfo SolutionInfo { get; }

		public SolutionImageToggle(BaseSolutionInfo solutionInfo, int buttonWidth)
		{
			SolutionInfo = solutionInfo;

			Int32 imageHeight;
			if (String.Equals(Path.GetExtension(SolutionInfo.ToggleImagePath), ".svg", StringComparison.OrdinalIgnoreCase))
			{
				var svgBitmap = SvgBitmap.FromFile(SolutionInfo.ToggleImagePath);
				imageHeight = (Int32)(svgBitmap.SvgImage.Height * (buttonWidth / svgBitmap.SvgImage.Width));
				Image = svgBitmap.Render(null, 1.0D);
			}
			else
			{
				var pngImage = Image.FromFile(SolutionInfo.ToggleImagePath);
				imageHeight = (Int32)(pngImage.Height * (buttonWidth / pngImage.Width));
				Image = pngImage;
			}

			Height = imageHeight;

			Enabled = solutionInfo.Enabled;
		}
	}
}
