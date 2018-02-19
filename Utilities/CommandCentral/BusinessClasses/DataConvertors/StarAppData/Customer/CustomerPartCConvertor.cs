using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Customer
{
	class CustomerPartCConvertor : BaseStarAppConvertor, ICustomerConvertor
	{
		protected override string SourceSheetName => "04c";
		protected override string OutputFileName => "CP04C.xml";
		protected override string OutputRootNodeName => "CP04C";

		public CustomerPartCConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP04CHeader", "CP04CHeader"),
				new StartAppDictionaryConfiguration("CP04CSubheader1", "CP04CSubheader1"),
				new StartAppDictionaryConfiguration("CP04CSubheader2", "CP04CSubheader2"),
				new StartAppDictionaryConfiguration("CP04CSubheader3", "CP04CSubheader3"),
			};
		}
	}
}
