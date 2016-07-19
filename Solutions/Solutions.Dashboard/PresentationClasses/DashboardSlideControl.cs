using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class DashboardSlideControl : UserControl
	{
		protected BaseDashboardContainer SlideContainer { get; }

		public virtual SlideType SlideType { get; }
		public virtual string SlideName { get; }
		public virtual bool ReadyForOutput { get; }
		public bool IsActive { get; set; }

		public event EventHandler<DashboardSlideChangedEventArgs> SelectedSlideChanged;

		public DashboardSlideControl()
		{
			InitializeComponent();
		}

		protected DashboardSlideControl(BaseDashboardContainer slideContainer)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SlideContainer = slideContainer;
			comboBoxEditSlideHeader.EnableSelectAll();
			if (CreateGraphics().DpiX > 96)
			{
			}
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

		public virtual void UpdateSelectedSlide(SlideType slideType)
		{
			foreach (var button in pnSlideSelector.Controls.OfType<ButtonX>())
				button.Checked = false;
			switch (slideType)
			{
				case SlideType.Cover:
					buttonXCover.Checked = true;
					break;
				case SlideType.LeadoffStatement:
					buttonXLeadoff.Checked = true;
					break;
				case SlideType.ClientGoals:
					buttonXClientGoals.Checked = true;
					break;
				case SlideType.TargetCustomers:
					buttonXTargetCustomers.Checked = true;
					break;
				case SlideType.SimpleSummary:
					buttonXSummary.Checked = true;
					break;
			}
		}

		private void OnSelectSlideType(object sender, EventArgs e)
		{
			var slideType = SlideType.None;
			if (sender == buttonXCover)
				slideType = SlideType.Cover;
			else if (sender == buttonXLeadoff)
				slideType = SlideType.LeadoffStatement;
			else if (sender == buttonXClientGoals)
				slideType = SlideType.ClientGoals;
			else if (sender == buttonXTargetCustomers)
				slideType = SlideType.TargetCustomers;
			else if (sender == buttonXSummary)
				slideType = SlideType.SimpleSummary;
			SelectedSlideChanged?.Invoke(this, new DashboardSlideChangedEventArgs { SlideType = slideType });
		}

		public virtual void GenerateOutput()
		{
			throw new NotImplementedException();
		}

		public virtual PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
		}
	}
}
