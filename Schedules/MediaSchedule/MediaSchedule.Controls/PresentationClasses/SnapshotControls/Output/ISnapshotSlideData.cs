using System.Collections.Generic;
using Asa.Common.Core.Objects.Output;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public interface ISnapshotSlideData
	{
		string TemplateFilePath { get; }
		string TotalRowValue { get; }
		string[][] Logos { get; set; }
		ContractSettings ContractSettings { get; }
		List<Dictionary<string, string>> ReplacementsList { get; }
	}
}
