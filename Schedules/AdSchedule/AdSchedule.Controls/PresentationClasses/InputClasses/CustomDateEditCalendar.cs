using System;
using DevExpress.XtraEditors.Controls;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
	public partial class CustomDateEditCalendar : DateEditCalendar
	{
		public CustomDateEditCalendar() : base(null, DateTime.Now)
		{
			InitializeComponent();
		}
	}
}