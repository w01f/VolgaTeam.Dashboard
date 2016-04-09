using System;
using System.IO;
using System.Linq;
using EO.WebBrowser;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	class VideoData : ContentLinkData
	{
		private string _mp4Url;

		public override LinkDataType DataType => LinkDataType.Video;

		public override void Load(object[] data)
		{
			base.Load(data);
			_mp4Url = (from object item in (JSArray)data[3] select item.ToString()).FirstOrDefault();
		}

		public override void SwitchCurrentPart(int partIndex){}

		public override string GetPartFileUrl()
		{
			return _mp4Url;
		}

		public override string GetPartFileName()
		{
			return String.Format("{0}.mp4",Path.GetFileNameWithoutExtension(OriginalFileName));
		}
	}
}
