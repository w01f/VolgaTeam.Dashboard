using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Video
{
	class VideoPartDConvertor : BaseStarAppConvertor, IVideoConvertor
	{
		protected override string SourceSheetName => "08d";
		protected override string OutputFileName => "CP08D.xml";
		protected override string OutputRootNodeName => "CP08D";

		public VideoPartDConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP08DHeader", "CP08DHeader"),
				new StartAppDictionaryConfiguration("CP08DSubHeader1", "CP08DSubheader1"),
			};
		}
	}
}
