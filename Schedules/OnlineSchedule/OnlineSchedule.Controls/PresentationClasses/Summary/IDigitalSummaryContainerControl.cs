using System;
using System.Collections.Generic;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Online.Controls.PresentationClasses.Summary
{
	public interface IDigitalSummaryContainerControl
	{
		List<Dictionary<string,string>> OutputReplacementsLists { get; set; }
		Theme SelectedTheme { get; }
		void PopulateReplacementsList();
	}
}
