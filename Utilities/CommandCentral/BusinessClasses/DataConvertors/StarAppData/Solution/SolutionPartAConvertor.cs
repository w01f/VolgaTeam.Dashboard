using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Solution
{
	class SolutionPartAConvertor : BaseStarAppConvertor, ISolutionConvertor
	{
		protected override string SourceSheetName => "10a";
		protected override string OutputFileName => "CP10A.xml";
		protected override string OutputRootNodeName => "CP10A";

		public SolutionPartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP10AHeader", "CP10AHeader"),
				new StartAppDictionaryConfiguration("CP10ASubHeader1", "CP10ASubheader1"),
			};
		}
	}
}
