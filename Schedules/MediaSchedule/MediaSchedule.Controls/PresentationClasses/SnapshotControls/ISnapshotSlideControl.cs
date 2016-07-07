using System.Collections.Generic;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls
{
	public interface ISnapshotSlideControl
	{
		bool ReadyForOutput { get; }
		string SlideName { get; }
		SlideType SlideType { get; }
		string TemplateFilePath { get; }
		string TotalRowValue { get; }
		string[][] Logos { get; set; }
		ContractSettings ContractSettings { get; }
		List<Dictionary<string, string>> ReplacementsList { get; }
		PreviewGroup GetPreviewGroup();
		void Output();
		void Release();
	}
}
