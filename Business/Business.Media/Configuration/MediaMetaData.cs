using System;
using Asa.Business.Media.Dictionaries;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Asa.Common.Core.Enums;

namespace Asa.Business.Media.Configuration
{
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

		private MediaMetaData() { }

		public void Init(MediaDataType dataType)
		{
			DataType = dataType;

			SettingsManager = new MediaSettingsManager();

			switch (dataType)
			{
				case MediaDataType.TVSchedule:
					ListManager = new TVListManager();
					break;
				case MediaDataType.RadioSchedule:
					ListManager = new RadioListManager();
					break;
			}
		}
	}
}
