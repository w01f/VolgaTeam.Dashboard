using System.Collections.Generic;
using Asa.CommonGUI.Preview;
using Asa.Core.Common;

namespace Asa.MediaSchedule.Controls.PresentationClasses.SnapshotControls
{
	public interface ISnapshotSlide
	{
		bool ReadyForOutput { get; }
		string SlideName { get; }
		string TemplateFilePath { get; }
		string TotalRowValue { get; }
		string[][] Logos { get; set; }
		ContractSettings ContractSettings { get; }
		List<Dictionary<string, string>> ReplacementsList { get; }
		PreviewGroup GetPreviewGroup(Theme selectedTheme);
		void Output(Theme selectedTheme);
	}
}
