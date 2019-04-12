using System;
using System.Drawing;
using Asa.Business.Solutions.Common.Configuration;

namespace Asa.Solutions.Common.PresentationClasses
{
	public interface ISolutionToggle
	{
		bool Checked { get; set; }
		Color? SelectedColor { get; set; }
		Color? HoverColor { get; set; }
		BaseSolutionInfo SolutionInfo { get; }
		event EventHandler Click;
		event EventHandler CheckedChanged;
	}
}
