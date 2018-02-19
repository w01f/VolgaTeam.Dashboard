using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Solution
{
	class SolutionPartCConvertor : BaseStarAppConvertor, ISolutionConvertor
	{
		protected override string SourceSheetName => "10c";
		protected override string OutputFileName => "CP10C.xml";
		protected override string OutputRootNodeName => "CP10C";

		public SolutionPartCConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP10CHeader", "CP10CHeader"),
				new StartAppDictionaryConfiguration("CP010CSubheader1", "CP10CSubheader1"),
				new StartAppDictionaryConfiguration("CP10CSubheader2", "CP10CSubheader2"),
			};
		}
	}
}
