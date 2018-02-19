using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Audience
{
	class AudiencePartCConvertor : BaseStarAppConvertor, IAudienceConvertor
	{
		protected override string SourceSheetName => "09c";
		protected override string OutputFileName => "CP09C.xml";
		protected override string OutputRootNodeName => "CP09C";

		public AudiencePartCConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP09CHeader", "CP09CHeader"),
				new StartAppDictionaryConfiguration("CP09CCombo1", "CP09CCombo1"),
			};
		}
	}
}
