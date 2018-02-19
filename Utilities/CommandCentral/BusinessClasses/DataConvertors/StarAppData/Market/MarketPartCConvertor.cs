using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Market
{
	class MarketPartCConvertor : BaseStarAppConvertor, IMarketConvertor
	{
		protected override string SourceSheetName => "07c";
		protected override string OutputFileName => "CP07C.xml";
		protected override string OutputRootNodeName => "CP07C";

		public MarketPartCConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP07CHeader", "CP07CHeader"),
				new StartAppDictionaryConfiguration("CP07CCombo1", "CP07CCombo1"),
			};
		}
	}
}
