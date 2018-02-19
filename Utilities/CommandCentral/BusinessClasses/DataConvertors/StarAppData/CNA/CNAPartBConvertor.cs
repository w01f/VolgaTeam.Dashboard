using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.CNA
{
	class CNAPartBConvertor : BaseStarAppConvertor, ICNAConvertor
	{
		protected override string SourceSheetName => "02b";
		protected override string OutputFileName => "CP02B.xml";
		protected override string OutputRootNodeName => "CP02B";

		public CNAPartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP02BHeader", "CP02BHeader"),
			};
		}
	}
}
