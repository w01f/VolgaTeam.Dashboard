using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Closers
{
	class ClosersPartAConvertor : BaseStarAppConvertor, IClosersConvertor
	{
		protected override string SourceSheetName => "11a";
		protected override string OutputFileName => "CP11A.xml";
		protected override string OutputRootNodeName => "CP11A";

		public ClosersPartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP11AHeader", "CP11AHeader"),
				new StartAppDictionaryConfiguration("CP11ASubheader1", "CP11ASubheader1"),
			};
		}
	}
}
