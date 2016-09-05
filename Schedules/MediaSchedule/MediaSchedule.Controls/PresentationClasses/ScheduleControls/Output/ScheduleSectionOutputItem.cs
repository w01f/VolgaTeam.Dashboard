using System;
using Asa.Business.Media.Configuration;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public class ScheduleSectionOutputItem
	{
		public ScheduleSectionOutputType OutputType { get; set; }
		public int SlidesCount { get; set; }

		public string DisplayName
		{
			get
			{
				switch (OutputType)
				{
					case ScheduleSectionOutputType.Program:
						return MediaMetaData.Instance.DataTypeString;
					case ScheduleSectionOutputType.DigitalOneSheet:
						return "Digital";
					case ScheduleSectionOutputType.ProgramAndDigital:
						return String.Format("{0} + Digital", MediaMetaData.Instance.DataTypeString);
					case ScheduleSectionOutputType.Summary:
						return "Summary";
					case ScheduleSectionOutputType.DigitalStrategy:
						return "Digital Strategies";
					default:
						throw new ArgumentOutOfRangeException("Undefined output type");
				}
			}
		}
	}
}
