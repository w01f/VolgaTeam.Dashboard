using System.Collections.Generic;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls
{
	public interface IOptionsSlideControl
	{
		bool ReadyForOutput { get; }
		string SlideName { get; }
		string TemplateFilePath { get; }
		string[][] Logos { get; set; }
		float[] ColumnWidths { get; set; }
		ContractSettings ContractSettings { get; }
		List<Dictionary<string, string>> ReplacementsList { get; }
		PreviewGroup GetPreviewGroup(Theme selectedTheme);
		void Output(Theme selectedTheme);
		void Release();
	}
}
