using Asa.Browser.Controls.BusinessClasses.Objects;

namespace Asa.Browser.Controls.Controls
{
	public interface ISiteContainer
	{
		SiteSettings SiteSettings { get; }
		string CurrentUrl { get; }

		void CopyUrl();
		void EmailUrl();

		void UpdateExtensionsState();

		void UpdateYouTubeState();

		void UpdateNavigationButtons();
		void NavigateBack();
		void NavigateForward();
		void RefreshPage();
	}
}
