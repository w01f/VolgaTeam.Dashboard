using System;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public class CommonChildTabControl : ChildTabBaseControl
	{
		private CommonChildTabInfo CustomTabInfo => (CommonChildTabInfo)TabInfo;

		public CommonChildTabControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo) { }

		public override void LoadData()
		{
			_allowToSave = false;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			_dataChanged = false;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override Boolean ReadyForOutput => false;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			return outputDataPackage;
		}
		#endregion
	}
}
