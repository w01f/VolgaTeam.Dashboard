using System.Collections.Generic;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.Core.Common;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.OptionsControls
{
	public interface IOptionsSlide
	{
		bool ReadyForOutput { get; }
		string SlideName { get; }
		string TemplateFileName { get; }
		string[][] Logos { get; set; }
		float[] ColumnWidths { get; set; }
		ContractSettings ContractSettings { get; }
		List<Dictionary<string, string>> ReplacementsList { get; }
		PreviewGroup GetPreviewGroup(Theme selectedTheme);
		void Output(Theme selectedTheme);
	}
}
