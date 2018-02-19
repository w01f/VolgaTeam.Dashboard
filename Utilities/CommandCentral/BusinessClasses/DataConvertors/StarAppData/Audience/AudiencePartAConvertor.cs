using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Audience
{
	class AudiencePartAConvertor : BaseStarAppConvertor, IAudienceConvertor
	{
		protected override string SourceSheetName => "09a";
		protected override string OutputFileName => "CP09A.xml";
		protected override string OutputRootNodeName => "CP09A";

		public AudiencePartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP09AHeader", "CP09AHeader"),
				new StartAppDictionaryConfiguration("CP09ASubheader1", "CP09ASubheader1"),
				new StartAppDictionaryConfiguration("CP09ASubheader2", "CP09ASubheader2")
			};
		}
	}
}
