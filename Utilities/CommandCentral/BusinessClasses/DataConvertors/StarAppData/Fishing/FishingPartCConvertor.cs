using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Fishing
{
	class FishingPartCConvertor : BaseStarAppConvertor, IFishingConvertor
	{
		protected override string SourceSheetName => "03c";
		protected override string OutputFileName => "CP03C.xml";
		protected override string OutputRootNodeName => "CP03C";

		public FishingPartCConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP03CHeader", "CP03CHeader"),
				new StartAppDictionaryConfiguration("CP03CSubheader1", "CP03CSubheader1"),
				new StartAppDictionaryConfiguration("CP03CSubheader2", "CP03CSubheader2"),
				new StartAppDictionaryConfiguration("CP03CSubheader3", "CP03CSubheader3"),
			};
		}
	}
}
