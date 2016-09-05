using System.Linq;
using EO.WebBrowser;

namespace AdSalesBrowser.SalesLibraryExtensions.LinkViewContent
{
	class VideoContent : ViewContent
	{
		private string _mp4Url;

		public override LinkContentType ContentType => LinkContentType.Video;

		public override void Load(object[] data)
		{
			base.Load(data);
			_mp4Url = (from object item in (JSArray)data[3] select item.ToString()).FirstOrDefault();
		}

		public string GetMp4Url()
		{
			return _mp4Url;
		}
	}
}
