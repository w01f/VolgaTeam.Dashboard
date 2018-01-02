using System;
using System.Windows.Forms;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabBaseControl : UserControl
	{
		protected bool _allowToSave;

		protected ROIControl ROIContentContainer { get; }

		public ROITabBaseControl()
		{
			InitializeComponent();
		}

		public ROITabBaseControl(ROIControl shareContentContainer) : this()
		{
			ROIContentContainer = shareContentContainer;
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
