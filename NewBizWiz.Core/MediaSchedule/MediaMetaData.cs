using System;
using Asa.Core.Common;

namespace Asa.Core.MediaSchedule
{
	public enum MediaDataType
	{
		TVSchedule = 0,
		RadioSchedule,
		TVPackage,
		RadioPackage
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

		public AppTypeEnum AppType
		{
			get
			{
				switch (DataType)
				{
					case MediaDataType.TVSchedule:
						return AppTypeEnum.TVSchedule;
					case MediaDataType.RadioSchedule:
						return AppTypeEnum.RadioSchedule;
				}
				return AppTypeEnum.None;
			}
		}

		public string DataTypeString
		{
			get
			{
				switch (DataType)
				{
					case MediaDataType.TVSchedule:
						return "TV";
					case MediaDataType.RadioSchedule:
						return "Radio";
				}
				return String.Empty;
			}
		}

		private MediaMetaData()
		{
		}

		public void Init(MediaDataType dataType)
		{
			DataType = dataType;

			SettingsManager = new MediaSettingsManager();

			switch (dataType)
			{
				case MediaDataType.TVSchedule:
					ListManager = new TVListManager();
					break;
				case MediaDataType.TVPackage:
					//ListManager = new T();
					break;
				case MediaDataType.RadioSchedule:
					ListManager = new RadioListManager();
					break;
				case MediaDataType.RadioPackage:
					//ListManager = new TVListManager();
					break;
			}
		}
	}
}
