using System;

namespace CommandCentral.BusinessClasses.Common
{
	class ConversionException : Exception
	{
		public string SourceFilePath { get; set; }
	}
}
