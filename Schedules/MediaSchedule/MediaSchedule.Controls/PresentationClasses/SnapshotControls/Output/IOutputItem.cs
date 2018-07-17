using System.Collections.Generic;
using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public interface IOutputItem
	{
		IList<OutputItem> GetOutputItems();
	}
}
