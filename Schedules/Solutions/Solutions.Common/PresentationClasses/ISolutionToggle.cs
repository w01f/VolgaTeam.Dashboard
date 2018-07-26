using System;
using System.Drawing;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

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
