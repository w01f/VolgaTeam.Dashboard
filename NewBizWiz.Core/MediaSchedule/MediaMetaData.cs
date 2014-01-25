using System;

namespace NewBizWiz.Core.MediaSchedule
{
	public enum MediaDataType
	{
		TV = 0,
		Radio
	}

	public class MediaMetaData
	{
		private static MediaMetaData _instance;

		public MediaDataType DataType { get; private set; }
		public IMediaSettingsManager SettingsManager { get; private set; }
		public MediaListManager ListManager { get; private set; }

		public static MediaMetaData Instance
		{
			get
			{
				if (_instance == null)
					_instance = new MediaMetaData();
				return _instance;
			}
		}

		public string DataTypeString
		{
			get
			{
				switch (DataType)
				{
					case MediaDataType.TV:
						return "TV";
					case MediaDataType.Radio:
						return "Radio";
				}
				return String.Empty;
			}
		}

		private MediaMetaData()
		{
		}

		public void Init<TSettingsManager, TListManager>(MediaDataType dataType)
			where TSettingsManager : IMediaSettingsManager
			where TListManager : MediaListManager
		{
			DataType = dataType;
			SettingsManager = (TSettingsManager)Activator.CreateInstance(typeof(TSettingsManager));
			ListManager = (TListManager)Activator.CreateInstance(typeof(TListManager));
		}
	}
}
