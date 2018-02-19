using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Video
{
	class VideoPartCConvertor : BaseStarAppConvertor, IVideoConvertor
	{
		protected override string SourceSheetName => "08c";
		protected override string OutputFileName => "CP08C.xml";
		protected override string OutputRootNodeName => "CP08C";

		public VideoPartCConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP08CHeader", "CP08CHeader"),
				new StartAppDictionaryConfiguration("CP08CSubHeader1", "CP08CSubheader1"),
			};
		}
	}
}
