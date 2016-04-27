using System;
using System.IO;
using System.Linq;
using EO.WebBrowser;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	class PowerPointData : ContentLinkData
	{
		private int _currentPartIndex;
		private string[] _partFileUrls;

		private double _slideWidth;
		private double _slideHeight;

		public override LinkDataType DataType => LinkDataType.PowerPoint;

		public override void Load(object[] data)
		{
			base.Load(data);
			_partFileUrls = (from object item in (JSArray)data[3] select item.ToString()).ToArray();

			double temp = 0;
			if (double.TryParse(data[4].ToString(), out temp))
				_slideWidth = temp;
			if (double.TryParse(data[5].ToString(), out temp))
				_slideHeight = temp;
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

		public bool IsFitToInsert(SlideSettings currentSlideSettings)
		{
			var currentWidth = (Int32)Math.Round(currentSlideSettings.SizeWidth);
			var slideWidth = (Int32)Math.Round(_slideWidth);
			return slideWidth == 0 || currentWidth == slideWidth;
		}
	}
}
