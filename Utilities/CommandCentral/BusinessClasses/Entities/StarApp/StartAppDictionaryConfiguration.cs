using System.Collections.Generic;
using CommandCentral.BusinessClasses.Entities.Common;

namespace CommandCentral.BusinessClasses.Entities.StarApp
{
	class StartAppDictionaryConfiguration
	{
		public string InputTagName { get; }
		public string OutputNodeName { get; }
		public List<ListDataItem> ListItems { get; }

		public StartAppDictionaryConfiguration(string inputTagName, string outputNodeName)
		{
			InputTagName = inputTagName;
			OutputNodeName = outputNodeName;
			ListItems=new List<ListDataItem>();
		}
	}
}
