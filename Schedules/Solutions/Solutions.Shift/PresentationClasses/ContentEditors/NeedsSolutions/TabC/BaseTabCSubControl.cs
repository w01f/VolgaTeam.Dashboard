using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabC
{
	//public class BaseTabCSubControl : UserControl
	public class BaseTabCSubControl : XtraTabPage
	{
		protected bool _allowToSave;

		protected NeedsSolutionsTabCControl Container { get; }
		protected BaseShiftContainer SlideContainer => Container.SlideContainer;

		public BaseTabCSubControl()
		{
		}

		public BaseTabCSubControl(NeedsSolutionsTabCControl container)
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
