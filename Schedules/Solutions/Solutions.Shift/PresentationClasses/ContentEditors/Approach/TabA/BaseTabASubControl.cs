using System;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach.TabA
{
	//public class BaseTabASubControl : UserControl
	public class BaseTabASubControl : XtraTabPage
	{
		protected bool _allowToSave;

		protected ApproachTabAControl Container { get; }
		protected BaseShiftContainer SlideContainer => Container.SlideContainer;

		public BaseTabASubControl()
		{
		}

		public BaseTabASubControl(ApproachTabAControl container)
		{
			Container = container;
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

		protected void RaiseEditValueChanged()
		{
			if (!_allowToSave) return;
			Container.RaiseEditValueChanged();
		}
	}
}
