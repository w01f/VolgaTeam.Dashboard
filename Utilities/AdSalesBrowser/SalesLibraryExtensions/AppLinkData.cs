using System.Collections.Generic;
using System.Linq;
using EO.WebBrowser;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	class AppLinkData : LinkData
	{
		private string _secondPath;

		public override LinkDataType DataType => LinkDataType.App;

		public override void Load(object[] data)
		{
			base.Load(data);
			_secondPath = (from object item in (JSArray)data[3] select item.ToString()).FirstOrDefault();
		}

		public IEnumerable<string> GetExecutablePaths()
		{
			return new[] { OriginalFileUrl, _secondPath };
		}
	}
}
