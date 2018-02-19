using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Fishing
{
	class FishingPartBConvertor : BaseStarAppConvertor, IFishingConvertor
	{
		protected override string SourceSheetName => "03b";
		protected override string OutputFileName => "CP03B.xml";
		protected override string OutputRootNodeName => "CP03B";

		public FishingPartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP03BHeader", "CP03BHeader"),
			};
		}
	}
}
