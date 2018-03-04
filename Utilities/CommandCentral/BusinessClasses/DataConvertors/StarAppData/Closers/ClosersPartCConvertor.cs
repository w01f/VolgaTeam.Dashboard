using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Closers
{
	class ClosersPartCConvertor : BaseStarAppConvertor, IClosersConvertor
	{
		protected override string SourceSheetName => "11c";
		protected override string OutputFileName => "CP11C.xml";
		protected override string OutputRootNodeName => "CP11C";

		public ClosersPartCConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP11CHeader", "CP11CHeader"),
				new StartAppDictionaryConfiguration("CP11CSubheader1", "CP11CSubheader1"),
				new StartAppDictionaryConfiguration("CP11CSubheader2", "CP11CSubheader2"),
			};
		}
	}
}
