using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Customer
{
	class CustomerPartBConvertor : BaseStarAppConvertor, ICustomerConvertor
	{
		protected override string SourceSheetName => "04b";
		protected override string OutputFileName => "CP04B.xml";
		protected override string OutputRootNodeName => "CP04B";

		public CustomerPartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP04BHeader", "CP04BHeader"),
				new StartAppDictionaryConfiguration("CP04BSubheader1", "CP04BSubheader1"),
				new StartAppDictionaryConfiguration("CP04BSubheader2", "CP04BSubheader2"),
			};
		}
	}
}
