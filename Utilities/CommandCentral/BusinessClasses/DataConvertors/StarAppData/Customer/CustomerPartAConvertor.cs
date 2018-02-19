using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Customer
{
	class CustomerPartAConvertor : BaseStarAppConvertor, ICustomerConvertor
	{
		protected override string SourceSheetName => "04a";
		protected override string OutputFileName => "CP04A.xml";
		protected override string OutputRootNodeName => "CP04A";

		public CustomerPartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP04AHeader", "CP04AHeader"),
			};
		}
	}
}
