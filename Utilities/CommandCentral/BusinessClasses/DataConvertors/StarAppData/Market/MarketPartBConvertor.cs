using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Market
{
	class MarketPartBConvertor : BaseStarAppConvertor, IMarketConvertor
	{
		protected override string SourceSheetName => "07b";
		protected override string OutputFileName => "CP07B.xml";
		protected override string OutputRootNodeName => "CP07B";

		public MarketPartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP07BHeader", "CP07BHeader"),
				new StartAppDictionaryConfiguration("CP07BSubHeader1", "CP07BSubheader1"),
				new StartAppDictionaryConfiguration("CP07BSubHeader2", "CP07BSubheader2"),
			};
		}
	}
}
