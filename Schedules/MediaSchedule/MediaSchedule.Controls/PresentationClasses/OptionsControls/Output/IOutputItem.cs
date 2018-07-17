using System.Collections.Generic;
using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public interface IOutputItem
	{
		IList<OutputItem> GetOutputItems();
	}
}
