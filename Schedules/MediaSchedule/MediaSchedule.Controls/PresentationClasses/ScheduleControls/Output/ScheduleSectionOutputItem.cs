using System;
using Asa.Business.Media.Configuration;
using Asa.Common.GUI.OutputSelector;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public class ScheduleSectionOutputItem : ISlideItem
	{
		public ScheduleSectionOutputType OutputType { get; set; }
		public bool IsCurrent { get; set; }
		public int SlidesCount { get; set; }
		public bool SelectedForOutput { get; set; } = true;

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

		public ISlideItem[] SlideItems
		{
			get => new ISlideItem[] { };
			set { }
		}
	}
}
