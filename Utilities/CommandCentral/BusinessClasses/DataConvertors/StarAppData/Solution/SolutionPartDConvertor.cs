using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Solution
{
	class SolutionPartDConvertor : BaseStarAppConvertor, ISolutionConvertor
	{
		protected override string SourceSheetName => "10d";
		protected override string OutputFileName => "CP10D.xml";
		protected override string OutputRootNodeName => "CP10D";

		public SolutionPartDConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP10DHeader", "CP10DHeader"),
				new StartAppDictionaryConfiguration("CP10DSubHeader1", "CP10DSubheader1"),
			};
		}
	}
}
