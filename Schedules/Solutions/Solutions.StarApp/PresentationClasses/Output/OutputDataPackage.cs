using System;
using System.Collections.Generic;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public class OutputDataPackage
	{
		public string TemplateName { get; set; }
		public Dictionary<string, ClipartObject> ClipartItems { get; }
		public Dictionary<string, string> TextItems { get; set; }
		public Theme Theme { get; set; }
		public bool AddAsFirtsPage { get; set; }

		public OutputDataPackage()
		{
			ClipartItems = new Dictionary<String, ClipartObject>();
			TextItems = new Dictionary<string, string>();
		}
	}
}
