using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Common.PresentationClasses
{
	public class SolutionToggle : ButtonX
	{
		public const int ButtonHeight = 40;
		public const int ButtonPadding = 70;
		public const int ButtonMaxWidth = 350;

		public BaseSolutionInfo SolutionInfo { get; }

		private SolutionToggle(BaseSolutionInfo solutionInfo, int buttonWidth)
		{
			SolutionInfo = solutionInfo;

			if (File.Exists(SolutionInfo.ToggleImagePath))
			{
				ImagePosition = eImagePosition.Top;

				buttonWidth = (Int32)(buttonWidth / Utilities.GetScaleFactor(CreateGraphics().DpiX).Width);
				var originalImage = Image.FromFile(SolutionInfo.ToggleImagePath);
				if (originalImage.Width > buttonWidth)
				{
					Image = originalImage.Resize(new Size(buttonWidth, originalImage.Height));
					originalImage.Dispose();
				}
				else
					Image = originalImage;
				Height = (Int32)(Image.Height * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
			}
			else
			{
				Text = SolutionInfo.ToggleTitle;
				UseMnemonic = false;
				Height = (Int32)(ButtonHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
			}

			AccessibleRole = AccessibleRole.PushButton;
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			ColorTable = eButtonColor.OrangeWithBackground;
			Font = new Font("Arial", 9.75F);
			Style = eDotNetBarStyle.StyleManagerControlled;
			TextColor = Color.Black;

			Enabled = solutionInfo.Enabled;
		}

		public static SolutionToggle Create(BaseSolutionInfo solutionInfo, int buttonWidth)
		{
			return new SolutionToggle(solutionInfo, buttonWidth);
		}
	}
}
