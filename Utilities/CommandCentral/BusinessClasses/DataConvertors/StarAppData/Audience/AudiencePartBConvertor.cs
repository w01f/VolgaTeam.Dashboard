using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Audience
{
	class AudiencePartBConvertor : BaseStarAppConvertor, IAudienceConvertor
	{
		protected override string SourceSheetName => "09b";
		protected override string OutputFileName => "CP09B.xml";
		protected override string OutputRootNodeName => "CP09B";

		public AudiencePartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP09BHeader", "CP09BHeader"),
				new StartAppDictionaryConfiguration("CP09BSubheader1", "CP09BSubheader1"),
				new StartAppDictionaryConfiguration("CP09BSubheader2", "CP09BSubheader2"),
				new StartAppDictionaryConfiguration("CP09BSubheader3", "CP09BSubheader3"),
				new StartAppDictionaryConfiguration("CP09BSubheader4", "CP09BSubheader4"),
				new StartAppDictionaryConfiguration("CP09BSubheader5", "CP09BSubheader5"),
				new StartAppDictionaryConfiguration("CP09BSubheader6", "CP09BSubheader6"),
			};
		}
	}
}
