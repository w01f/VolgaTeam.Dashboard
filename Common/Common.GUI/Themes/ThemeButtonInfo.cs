using System;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Common.GUI.Themes
{
	class ThemeButtonInfo
	{
		public Theme CurrentTheme { get; set; }
		public Action ClickHandler { get; set; }

	}
}
