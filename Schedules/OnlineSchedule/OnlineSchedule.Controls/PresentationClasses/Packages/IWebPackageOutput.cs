using System.Collections.Generic;
using Asa.Business.Online.Configuration;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Online.Controls.PresentationClasses.Packages
{
	public interface IWebPackageOutput
	{
		int RowsPerSlide { get; }
		Theme SelectedTheme { get; }
		DigitalPackageSettings PackageSettings { get; }
		List<Dictionary<string, string>> OutputReplacementsLists { get; }
	}
}
