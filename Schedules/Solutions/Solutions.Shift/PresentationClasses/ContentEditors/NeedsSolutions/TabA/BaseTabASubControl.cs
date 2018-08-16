using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabA
{
	//public class BaseTabASubControl : UserControl
	public class BaseTabASubControl : XtraTabPage
	{
		protected bool _allowToSave;

		protected NeedsSolutionsTabAControl Container { get; }
		protected BaseShiftContainer SlideContainer => Container.SlideContainer;

		public BaseTabASubControl()
		{
		}

		public BaseTabASubControl(NeedsSolutionsTabAControl container)
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
