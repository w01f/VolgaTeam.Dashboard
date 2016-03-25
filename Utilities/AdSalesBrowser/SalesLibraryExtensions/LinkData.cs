using System;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	abstract class LinkData
	{
		public abstract LinkDataType DataType { get; }
		public string OriginalFileName { get; private set; }
		public string OriginalFileUrl { get; private set; }
		
		public virtual void Load(object[] data)
		{
			OriginalFileName = data[1] as String;
			OriginalFileUrl = data[2] as String;
		}

		public abstract void SwitchCurrentPart(int partIndex);

		public abstract string GetPartFileUrl();
		public abstract string GetPartFileName();
	}
}
