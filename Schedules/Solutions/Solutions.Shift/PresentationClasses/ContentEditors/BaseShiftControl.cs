using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.Shift.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class BaseShiftControl : UserControl, IShiftSlideContainer, IScheduleResourceHolder
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		public BaseShiftContainer SlideContainer { get; }

		public virtual SlideType SlideType { get; }
		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType);
		public BaseScheduleResourceContainer ResourceContainer => SlideContainer.EditedContent.ScheduleResources;

		protected BaseShiftControl()
		{
			InitializeComponent();
		}

		protected BaseShiftControl(BaseShiftContainer slideContainer) : this()
		{
			SlideContainer = slideContainer;
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}
		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

		public virtual bool ReadyForOutput { get; }

		public virtual OutputGroup GetOutputGroup()
		{
			throw new NotImplementedException();
		}
	}
}
