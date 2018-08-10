using System;
using System.Collections.Generic;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Solutions.Common.PresentationClasses.Output
{
	public class OutputDataPackage
	{
		public string TemplateName { get; set; }
		public Dictionary<string, ClipartObject> ClipartItems { get; }
		public Dictionary<string, Dictionary<string, decimal>> ChartItems { get; }
		public Dictionary<string, string> TextItems { get; set; }
		public Theme Theme { get; set; }
		public bool AddAsFirtsPage { get; set; }

		public OutputDataPackage()
		{
			ClipartItems = new Dictionary<String, ClipartObject>();
			ChartItems = new Dictionary<String, Dictionary<string, decimal>>();
			TextItems = new Dictionary<string, string>();
		}
	}
}
