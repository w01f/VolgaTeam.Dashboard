using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Video
{
	class VideoPartAConvertor : BaseStarAppConvertor, IVideoConvertor
	{
		protected override string SourceSheetName => "08a";
		protected override string OutputFileName => "CP08A.xml";
		protected override string OutputRootNodeName => "CP08A";

		public VideoPartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP08AHeader", "CP08AHeader"),
				new StartAppDictionaryConfiguration("CP08ASubHeader1", "CP08ASubheader1"),
			};
		}
	}
}
