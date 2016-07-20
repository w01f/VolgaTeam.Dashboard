using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.Dashboard.Properties;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	//public abstract partial class BaseDashboardContainer : UserControl
	public abstract partial class BaseDashboardContainer : BaseSolutionEditor
	{
		private readonly List<DashboardSlideControl> _slides = new List<DashboardSlideControl>();
		private DashboardSlideControl ActiveSlide => xtraTabControl.SelectedTabPage as DashboardSlideControl;

		public DashboardSolutionInfo DashboardInfo { get; }
		public DashboardContent EditedContent { get; protected set; }
		public override SolutionType SolutionType => SolutionType.Dashboard;
		public override SlideType SelectedSlideType => ActiveSlide.SlideType;
		public override Image HomeLogo => Resources.RibbonLogo;
		public override string HelpKey
		{
			get
			{
				switch (SelectedSlideType)
				{
					case SlideType.Cleanslate:
						return "Home";
					case SlideType.Cover:
						return "Cover";
					case SlideType.LeadoffStatement:
						return "Intro";
					case SlideType.ClientGoals:
						return "Needs";
					case SlideType.TargetCustomers:
						return "Target";
					case SlideType.SimpleSummary:
						return "Closing";
					default:
						return String.Empty;
				}
			}
		}

		protected BaseDashboardContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo)
		{
			DashboardInfo = (DashboardSolutionInfo)solutionInfo;
			InitializeComponent();
		}

		#region GUI Processing
		public override void InitControl()
		{
			_slides.Add(new CleanslateControl(this));
			_slides.Add(new CoverControl(this));
			_slides.Add(new LeadoffStatementControl(this));
			_slides.Add(new ClientGoalsControl(this));
			_slides.Add(new TargetCustomersControl(this));
			_slides.Add(new SimpleSummaryControl(this));

			xtraTabControl.TabPages.AddRange(_slides.OfType<XtraTabPage>().ToArray());
			xtraTabControl.SelectedTabPage = _slides.FirstOrDefault();
			xtraTabControl.SelectedPageChanged += OnSelectedSlideChanged;
		}

		private void OnSelectedSlideChanged(Object sender, TabPageChangedEventArgs e)
		{
			var prevSlide =  e.PrevPage as DashboardSlideControl;
			prevSlide?.ApplyChanges();

			RaiseSlideTypeChanged();
			RaiseOutputStatuesChanged();
		}

		public override void ShowEditor()
		{
			ShowHomeSlide();
			base.ShowEditor();
		}

		public override void ShowHomeSlide()
		{
			xtraTabControl.SelectedTabPage = _slides.FirstOrDefault();
		}
		#endregion

		#region Data Processing
		public override void LoadData()
		{
			_slides.ForEach(s => s.LoadData());
		}

		public override void ApplyChanges()
		{
			_slides.ForEach(s => s.ApplyChanges());
		}
		#endregion

		#region Output Processing
		protected override bool ReadyForOutput => ActiveSlide?.ReadyForOutput ?? false;
		public abstract Theme GetSelectedTheme(SlideType simpleSummary);

		public IList<DashboardSlideControl> GetOutputSlides()
		{
			var selectedSlides = new List<DashboardSlideControl>();
			using (var form = new FormSelectOutputItems())
			{
				form.Text = "Select Slides";
				foreach (var slideControl in _slides.Where(s => s.ReadyForOutput))
				{
					var item = new CheckedListBoxItem(slideControl, slideControl.SlideName);
					form.checkedListBoxControlOutputItems.Items.Add(item);
					if (slideControl == ActiveSlide)
						form.buttonXSelectCurrent.Tag = item;
				}
				form.checkedListBoxControlOutputItems.CheckAll();
				if (form.ShowDialog() == DialogResult.OK)
					selectedSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
						OfType<CheckedListBoxItem>().
						Where(ci => ci.CheckState == CheckState.Checked).
						Select(ci => ci.Value).
						OfType<DashboardSlideControl>());
			}
			return selectedSlides;
		}
		#endregion
	}
}
