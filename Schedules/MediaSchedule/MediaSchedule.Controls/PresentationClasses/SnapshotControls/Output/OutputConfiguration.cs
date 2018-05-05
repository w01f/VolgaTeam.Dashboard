using System;
using Asa.Business.Media.Configuration;
using Asa.Common.GUI.OutputSelector;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public class OutputConfiguration: ISlideItem
	{
		public SnapshotOutputType OutputType { get; }
		public bool IsCurrent { get; set; }
		public int SlidesCount { get; }
		public bool SelectedForOutput { get; set; } = true;

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

		public ISlideItem[] SlideItems
		{
			get => new ISlideItem[] { };
			set { }
		}

		public OutputConfiguration(SnapshotOutputType outputType, int slidesCount)
		{
			OutputType = outputType;
			SlidesCount = slidesCount;
		}
	}
}
