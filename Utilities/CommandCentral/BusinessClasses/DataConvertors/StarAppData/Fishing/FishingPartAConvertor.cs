using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Fishing
{
	class FishingPartAConvertor : BaseStarAppConvertor, IFishingConvertor
	{
		protected override string SourceSheetName => "03a";
		protected override string OutputFileName => "CP03A.xml";
		protected override string OutputRootNodeName => "CP03A";

		public FishingPartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP03AHeader", "CP03AHeader"),
				new StartAppDictionaryConfiguration("CP03ASubheader1", "CP03ASubheader1"),
				new StartAppDictionaryConfiguration("CP03ASubheader2", "CP03ASubheader2"),
			};
		}
	}
}
