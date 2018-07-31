using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Solutions.Shift.Configuration;
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
		public ShiftTopTabInfo TabInfo { get; }

		public virtual SlideType SlideType { get; }
		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType);
		public BaseScheduleResourceContainer ResourceContainer => SlideContainer.EditedContent.ScheduleResources;

		protected BaseShiftControl()
		{
			InitializeComponent();
		}

		protected BaseShiftControl(BaseShiftContainer slideContainer, ShiftTopTabInfo tabInfo) : this()
		{
			SlideContainer = slideContainer;
			TabInfo = tabInfo;
		}

		public virtual void InitControls()
		{
			throw new NotImplementedException();
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}
		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

		public virtual bool MultipleSlidesAllowed => false;
		public virtual bool ReadyForOutput => false;

		public virtual OutputGroup GetOutputGroup()
		{
			return new OutputGroup()
			{
				Name = TabInfo.Title,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = new List<OutputItem>()
			};
		}
	}
}
