using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using DevExpress.XtraEditors;
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
		public abstract Form MainForm { get; }
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
			var availableSlideInfos = _slides
				.Where(s => s.ReadyForOutput).OfType<IDashboardSlide>()
				.SelectMany(slide => slide.GetSlideInfo())
				.ToList();

			if (availableSlideInfos.Any())
			{
				FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
				FormProgress.ShowProgress();
				var previewGroup = availableSlideInfos
									   .Where(slideInfo => slideInfo.IsCurrent)
									   .Select(slideInfo => slideInfo.SlideContainer.GeneratePreview(slideInfo))
									   .FirstOrDefault() ?? availableSlideInfos.First().SlideContainer.GeneratePreview(availableSlideInfos.First());
				Utilities.ActivateForm(MainForm.Handle, MainForm.WindowState == FormWindowState.Maximized, false);
				FormProgress.CloseProgress();

				using (var form = new FormConfigureOutput(availableSlideInfos, previewGroup))
				{
					form.hyperLinkEditAddSingleSlide.Text = String.Format("<color={1}>{0}</color>", form.hyperLinkEditAddSingleSlide.Text, AccentColor.HasValue
						? AccentColor.Value.ToHex()
						: "blue");
					form.hyperLinkEditSelectAll.Text = String.Format("<color={1}>{0}</color>", form.hyperLinkEditSelectAll.Text, AccentColor.HasValue
						? AccentColor.Value.ToHex()
						: "blue");
					form.hyperLinkEditClearAll.Text = String.Format("<color={1}>{0}</color>", form.hyperLinkEditClearAll.Text, AccentColor.HasValue
						? AccentColor.Value.ToHex()
						: "blue");

					if (form.ShowDialog() == DialogResult.OK)
						selectedSlideInfos.AddRange(form.GetSelectedSlideItems());
				}
			}

			return selectedSlideInfos;
		}
		#endregion
	}
}
