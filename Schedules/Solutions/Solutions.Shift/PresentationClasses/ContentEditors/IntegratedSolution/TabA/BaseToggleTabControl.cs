using System;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	//public class BaseToggleTabControl : UserControl
	public class BaseToggleTabControl : XtraTabPage
	{
		protected bool _allowHandleEvents;
		protected bool _dataChanged;
		protected ProductItemControl Container { get; }
		protected BaseShiftContainer SlideContainer => Container.SlideContainer;

		public bool Initialized { get; protected set; }

		public BaseToggleTabControl()
		{
		}

		public BaseToggleTabControl(ProductItemControl container)
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
			if (!_allowHandleEvents) return;
			_dataChanged = true;
			Container.RaiseEditValueChanged();
		}
	}
}
