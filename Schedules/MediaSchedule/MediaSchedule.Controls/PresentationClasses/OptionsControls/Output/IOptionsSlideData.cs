using System;
using System.Collections.Generic;
using Asa.Common.Core.Objects.Output;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public interface IOptionsSlideData
	{
		string TemplateFilePath { get; }
		float[] ColumnWidths { get; }
		string[][] Logos { get; }
		List<Dictionary<string, string>> ReplacementsList { get; }
		ContractSettings ContractSettings { get;}
	}
}
