using System;
using Asa.Business.Media.Configuration;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public class OutputConfiguration
	{
		public SnapshotOutputType OutputType { get; }

		public string DisplayName
		{
			get
			{
				switch (OutputType)
				{
					case SnapshotOutputType.Program:
						return MediaMetaData.Instance.DataTypeString;
					case SnapshotOutputType.Digital:
						return "Digital";
					case SnapshotOutputType.ProgramAndDigital:
						return String.Format("{0} + Digital", MediaMetaData.Instance.DataTypeString);
					case SnapshotOutputType.Summary:
						return "Summary";
					case SnapshotOutputType.DigitalStrategy:
						return "Digital Strategies";
					default:
						throw new ArgumentOutOfRangeException("Undefined output type");
				}
			}
		}

		public OutputConfiguration(SnapshotOutputType outputType)
		{
			OutputType = outputType;
		}
	}
}
