using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Video
{
	class VideoPartBConvertor : BaseStarAppConvertor, IVideoConvertor
	{
		protected override string SourceSheetName => "08b";
		protected override string OutputFileName => "CP08B.xml";
		protected override string OutputRootNodeName => "CP08B";

		public VideoPartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP08BHeader", "CP08BHeader"),
				new StartAppDictionaryConfiguration("CP08BSubHeader1", "CP08BSubheader1"),
			};
		}
	}
}
