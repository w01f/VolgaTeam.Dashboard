﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	//public abstract partial class BaseDashboardContainer : UserControl
	public abstract partial class BaseDashboardContainer : BaseSolutionEditor
	{
		private readonly List<DashboardSlideControl> _slides = new List<DashboardSlideControl>();
		private DashboardSlideControl ActiveSlide => xtraTabControl.SelectedTabPage as DashboardSlideControl;

		public DashboardSolutionInfo DashboardInfo { get; }
		public DashboardContent EditedContent { get; protected set; }
		public abstract IDashboardSettingsContainer SettingsContainer { get; }
		public override SlideType SelectedSlideType => ActiveSlide.SlideType;
		public abstract Color? AccentColor { get; }
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

			foreach (var slideControl in _slides)
				AssignCloseActiveEditorsOnOutsideClick(slideControl);
		}

		private void OnSelectedSlideChanged(object sender, TabPageChangedEventArgs e)
		{
			var prevSlide = e.PrevPage as DashboardSlideControl;
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

		private void AssignCloseActiveEditorsOnOutsideClick(Control control)
		{
			if (!(control is BaseEdit ||
				control is CheckedListBoxControl))
			{
				control.Click += CloseActiveEditorsOnOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsOnOutsideClick(childControl);
			}
		}

		protected void CloseActiveEditorsOnOutSideClick(object sender, EventArgs e)
		{
			xtraTabControl.Focus();
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
		public override bool ReadyForOutput => ActiveSlide?.ReadyForOutput ?? false;
		public abstract Theme GetSelectedTheme(SlideType slideType);

		public IList<DashboardSlideInfo> GetOutputSlides()
		{
			var selectedSlideInfos = new List<DashboardSlideInfo>();
			using (var form = new FormSelectOutputItems())
			{
				form.Text = "Slide Output Options";
				foreach (var slideInfo in _slides
					.Where(s => s.ReadyForOutput).OfType<IDashboardSlide>()
					.SelectMany(slide => slide.GetSlideInfo())) 
				{
					var item = new CheckedListBoxItem(slideInfo, slideInfo.SlideName, ActiveSlide.SlideType == SlideType.Cleanslate || slideInfo.SlideContainer == ActiveSlide ? CheckState.Checked : CheckState.Unchecked);
					form.checkedListBoxControlOutputItems.Items.Add(item);
					if (slideInfo.SlideContainer == ActiveSlide)
						form.buttonXSelectCurrent.Tag = item;
				}
				if (form.ShowDialog() == DialogResult.OK)
					selectedSlideInfos.AddRange(form.checkedListBoxControlOutputItems.Items.
						OfType<CheckedListBoxItem>().
						Where(ci => ci.CheckState == CheckState.Checked).
						Select(ci => ci.Value).
						OfType<DashboardSlideInfo>());
			}
			return selectedSlideInfos;
		}
		#endregion
	}
}