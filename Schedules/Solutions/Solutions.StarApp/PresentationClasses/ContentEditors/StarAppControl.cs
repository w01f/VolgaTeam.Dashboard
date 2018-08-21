using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class StarAppControl : UserControl, IStarAppSlideContainer, IScheduleResourceHolder
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		public BaseStarAppContainer SlideContainer { get; }
		public StarTopTabInfo TabInfo { get; }

		public virtual SlideType SlideType => SlideType.None;
		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType);
		public BaseScheduleResourceContainer ResourceContainer => SlideContainer.EditedContent.ScheduleResources;

		protected StarAppControl()
		{
			InitializeComponent();
		}

		protected StarAppControl(BaseStarAppContainer slideContainer, StarTopTabInfo tabInfo) : this()
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
