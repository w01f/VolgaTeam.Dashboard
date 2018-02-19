using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.CNA
{
	class CNAPartAConvertor : BaseStarAppConvertor, ICNAConvertor
	{
		protected override string SourceSheetName => "02a";
		protected override string OutputFileName => "CP02A.xml";
		protected override string OutputRootNodeName => "CP02A";

		public CNAPartAConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP02AHeader", "CP02AHeader"),
				new StartAppDictionaryConfiguration("CP02ASubheader1", "CP02ASubheader1"),
				new StartAppDictionaryConfiguration("CP02ASubheader2", "CP02ASubheader2"),
			};
		}
	}
}
