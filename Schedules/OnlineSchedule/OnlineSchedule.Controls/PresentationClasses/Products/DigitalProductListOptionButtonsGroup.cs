using DevComponents.DotNetBar;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public class DigitalProductListOptionButtonsGroup
	{
		public ButtonItem ToggleDimensions { get; set; }
		public ButtonItem ToggleStrategy { get; set; }
		public ButtonItem ToggleLocation { get; set; }
		public ButtonItem ToggleRichMedia { get; set; }
		public ButtonItem ToggleTargeting { get; set; }

		public ButtonItem[] Toggles => new[]
		{
			ToggleDimensions,
			ToggleLocation,
			ToggleRichMedia,
			ToggleStrategy,
			ToggleTargeting
		};
	}
}
