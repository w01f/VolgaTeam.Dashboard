using System;
using Asa.Business.Media.Configuration;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public class OutputConfiguration
	{
		public OptionSetOutputType OutputType { get; }

		public string DisplayName
		{
			get
			{
				switch (OutputType)
				{
					case OptionSetOutputType.Program:
						return MediaMetaData.Instance.DataTypeString;
					case OptionSetOutputType.Digital:
						return "Digital";
					case OptionSetOutputType.ProgramAndDigital:
						return String.Format("{0} + Digital", MediaMetaData.Instance.DataTypeString);
					case OptionSetOutputType.Summary:
						return "Summary";
					case OptionSetOutputType.DigitalStrategy:
						return "Digital Strategies";
					default:
						throw new ArgumentOutOfRangeException("Undefined output type");
				}
			}
		}

		public OutputConfiguration(OptionSetOutputType outputType)
		{
			OutputType = outputType;
		}
	}
}
