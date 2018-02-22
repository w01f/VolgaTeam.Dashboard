using System;
using System.Windows.Forms;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabBaseControl : UserControl
	{
		protected bool _allowToSave;

		protected ClosersControl ClosersContentContainer { get; }

		public ClosersTabBaseControl()
		{
			InitializeComponent();
		}

		public ClosersTabBaseControl(ClosersControl closersContentContainer) : this()
		{
			ClosersContentContainer = closersContentContainer;
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}
	}
}
