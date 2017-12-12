using Asa.Browser.Controls.BusinessClasses.Objects;

namespace Asa.Media.Controls.PresentationClasses.Browser
{
	public interface IMediaSite
	{
		SiteSettings SiteSettings { get; }

		void CopyUrl();
		void EmailUrl();
	}
}
