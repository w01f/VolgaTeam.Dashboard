using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Solution
{
	class SolutionPartBConvertor : BaseStarAppConvertor, ISolutionConvertor
	{
		protected override string SourceSheetName => "10b";
		protected override string OutputFileName => "CP10B.xml";
		protected override string OutputRootNodeName => "CP10B";

		public SolutionPartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP10BHeader", "CP10BHeader"),
				new StartAppDictionaryConfiguration("CP10BSubHeader1", "CP10BSubheader1"),
			};
		}
	}
}
