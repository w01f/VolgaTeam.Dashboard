using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Market
{
	class MarketPartAConvertor : BaseStarAppConvertor, IMarketConvertor
	{
		protected override string SourceSheetName => "07a";
		protected override string OutputFileName => "CP07A.xml";
		protected override string OutputRootNodeName => "CP07A";

		public MarketPartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP07AHeader", "CP07AHeader"),
				new StartAppDictionaryConfiguration("CP07ASubHeader1", "CP07ASubheader1"),
			};
		}
	}
}
