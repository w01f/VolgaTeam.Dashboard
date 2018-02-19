using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData
{
	class StarAppCoverConvertor : BaseStarAppConvertor
	{
		protected override string SourceSheetName => "01a";
		protected override string OutputFileName => "CP01A.xml";
		protected override string OutputRootNodeName => "CP01A";

		public StarAppCoverConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP01AHeader", "CP01AHeader"),
				new StartAppDictionaryConfiguration("CP01ASubheader1", "CP01ASubheader1")
			};
		}
	}
}
