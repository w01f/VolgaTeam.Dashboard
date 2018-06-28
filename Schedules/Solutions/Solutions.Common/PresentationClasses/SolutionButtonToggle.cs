using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Common.PresentationClasses
{
	public class SolutionButtonToggle : ButtonX, ISolutionToggle
	{
		public Color? SelectedColor { get; set; }
		public Color? HoverColor { get; set; }
		public BaseSolutionInfo SolutionInfo { get; }

		public SolutionButtonToggle(BaseSolutionInfo solutionInfo)
		{
			SolutionInfo = solutionInfo;

			Text = SolutionInfo.ToggleTitle;
			UseMnemonic = false;
			Height = (Int32)(SolutionToggleHelper.ButtonHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);

			Tooltip = SolutionInfo.ToggleTitle;
			AccessibleRole = AccessibleRole.PushButton;
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			ColorTable = eButtonColor.OrangeWithBackground;
			Font = new Font("Arial", 9.75F);
			Style = eDotNetBarStyle.StyleManagerControlled;
			TextColor = Color.Black;

			Enabled = solutionInfo.Enabled;
		}
	}
}
