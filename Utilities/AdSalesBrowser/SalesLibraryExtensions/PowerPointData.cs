using System;
using System.IO;
using System.Linq;
using EO.WebBrowser;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	class PowerPointData : LinkData
	{
		private int _currentPartIndex;
		private string[] _partFileUrls;

		public override LinkDataType DataType => LinkDataType.PowerPoint;

		public override void Load(object[] data)
		{
			base.Load(data);
			_partFileUrls = (from object item in (JSArray)data[3] select item.ToString()).ToArray();
		}

		public override void SwitchCurrentPart(int partIndex)
		{
			_currentPartIndex = partIndex;
		}

		public override string GetPartFileUrl()
		{
			return _partFileUrls[_currentPartIndex];
		}

		public override string GetPartFileName()
		{
			return String.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(OriginalFileName), (_currentPartIndex + 1), Path.GetExtension(OriginalFileName));
		}
	}
}
