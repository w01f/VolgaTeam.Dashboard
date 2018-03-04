using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData.Closers
{
	class ClosersPartBConvertor : BaseStarAppConvertor, IClosersConvertor
	{
		protected override string SourceSheetName => "11b";
		protected override string OutputFileName => "CP11B.xml";
		protected override string OutputRootNodeName => "CP11B";

		public ClosersPartBConvertor(string sourceFilePath) : base(sourceFilePath) { }

		protected override IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations()
		{
			return new[]
			{
				new StartAppDictionaryConfiguration("CP11BHeader", "CP11BHeader"),
				new StartAppDictionaryConfiguration("CP11BCombo0", "CP11BCombo0"),
				new StartAppDictionaryConfiguration("CP11BCombo1", "CP11BCombo1"),
				new StartAppDictionaryConfiguration("CP11BSubheader1", "CP11BSubheader1"),
				new StartAppDictionaryConfiguration("CP11BCombo2", "CP11BCombo2"),
				new StartAppDictionaryConfiguration("CP11BSubheader2", "CP11BSubheader2"),
				new StartAppDictionaryConfiguration("CP11BCombo3", "CP11BCombo3"),
				new StartAppDictionaryConfiguration("CP11BSubheader3", "CP11BSubheader3")
			};
		}
	}
}
