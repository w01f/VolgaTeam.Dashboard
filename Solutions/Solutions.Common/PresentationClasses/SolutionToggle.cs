using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Common.PresentationClasses
{
	public class SolutionToggle : ButtonX
	{
		public const int ButtonHeight = 40;
		public const int ButtonPadding = 40;

		public BaseSolutionInfo SolutionInfo { get; }

		private SolutionToggle(BaseSolutionInfo solutionInfo)
		{
			SolutionInfo = solutionInfo;
			Text = SolutionInfo.Title;
			UseMnemonic = false;
			Height = ButtonHeight;

			AccessibleRole = AccessibleRole.PushButton;
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			ColorTable = eButtonColor.OrangeWithBackground;
			Font = new System.Drawing.Font("Arial", 9.75F);
			Style = eDotNetBarStyle.StyleManagerControlled;
			TextColor = System.Drawing.Color.Black;
		}

		public static SolutionToggle Create(BaseSolutionInfo solutionInfo)
		{
			return new SolutionToggle(solutionInfo);
		}
	}
}
