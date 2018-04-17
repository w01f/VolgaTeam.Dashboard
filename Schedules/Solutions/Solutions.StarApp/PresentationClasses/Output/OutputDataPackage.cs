using System.Collections.Generic;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public class OutputDataPackage
	{
		public string TemplateName { get; set; }
		public Dictionary<string, OutputImageInfo> ClipartItems { get; }
		public Dictionary<string, string> TextItems { get; set; }
		public Theme Theme { get; set; }
		public bool AddAsFirtsPage { get; set; }

		public OutputDataPackage()
		{
			ClipartItems = new Dictionary<string, OutputImageInfo>();
			TextItems = new Dictionary<string, string>();
		}
	}
}
