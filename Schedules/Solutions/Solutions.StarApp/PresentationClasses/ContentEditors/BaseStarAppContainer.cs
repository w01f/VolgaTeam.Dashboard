using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Interfaces;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Audience;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Closers;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.CNA;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Cover;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Customer;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Fishing;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Market;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.ROI;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Share;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Solution;
using Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Video;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	//public abstract partial class BaseStarAppContainer : UserControl
	public abstract partial class BaseStarAppContainer : BaseSolutionEditor
	{
		private readonly List<XtraTabPage> _slides = new List<XtraTabPage>();

		public StarAppSolutionInfo StarInfo { get; }
		public StarAppContent EditedContent { get; protected set; }
		public abstract IStarAppSettingsContainer SettingsContainer { get; }
		public StarAppControl ActiveSlideContent => (xtraTabControl.SelectedTabPage as IStarAppTabPageContainer)?.ContentControl;
		public override SlideType SelectedSlideType => ActiveSlideContent?.SlideType ?? SlideType.Cleanslate;
		public override string HelpKey
		{
			get
			{
				switch (SelectedSlideType)
				{
					//case SlideType.Cleanslate:
					//	return "Home";
					default:
						return String.Empty;
				}
			}
		}

		protected BaseStarAppContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo)
		{
			StarInfo = (StarAppSolutionInfo)solutionInfo;
			InitializeComponent();
		}

		#region GUI Processing

		public override void InitControl(bool showSplash)
		{
			if (showSplash)
			{
				FormProgress.SetTitle("Loading data...");
				FormProgress.ShowProgress();
				Application.DoEvents();
			}

			foreach (var tabInfo in StarInfo.TabsInfo)
			{
				Application.DoEvents();

				switch (tabInfo.TabType)
				{
					case StarTopTabType.Cleanslate:
						_slides.Add(new StarAppTabPageContainerControl<CleanslateControl>(this, tabInfo));
						break;
					case StarTopTabType.Cover:
						_slides.Add(new StarAppTabPageContainerControl<CoverControl>(this, tabInfo));
						break;
					case StarTopTabType.CNA:
						_slides.Add(new StarAppTabPageContainerControl<CNAControl>(this, tabInfo));
						break;
					case StarTopTabType.Fishing:
						_slides.Add(new StarAppTabPageContainerControl<FishingControl>(this, tabInfo));
						break;
					case StarTopTabType.Customer:
						_slides.Add(new StarAppTabPageContainerControl<CustomerControl>(this, tabInfo));
						break;
					case StarTopTabType.Share:
						_slides.Add(new StarAppTabPageContainerControl<ShareControl>(this, tabInfo));
						break;
					case StarTopTabType.ROI:
						_slides.Add(new StarAppTabPageContainerControl<ROIControl>(this, tabInfo));
						break;
					case StarTopTabType.Market:
						_slides.Add(new StarAppTabPageContainerControl<MarketControl>(this, tabInfo));
						break;
					case StarTopTabType.Video:
						_slides.Add(new StarAppTabPageContainerControl<VideoControl>(this, tabInfo));
						break;
					case StarTopTabType.Audience:
						_slides.Add(new StarAppTabPageContainerControl<AudienceControl>(this, tabInfo));
						break;
					case StarTopTabType.Solution:
						_slides.Add(new StarAppTabPageContainerControl<SolutionControl>(this, tabInfo));
						break;
					case StarTopTabType.Closers:
						_slides.Add(new StarAppTabPageContainerControl<ClosersControl>(this, tabInfo));
						break;
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}

			xtraTabControl.TabPages.AddRange(_slides.OfType<XtraTabPage>().ToArray());
			Application.DoEvents();

			xtraTabControl.SelectedPageChanging += OnSelectedSlideChanging;
			xtraTabControl.SelectedPageChanged += OnSelectedSlideChanged;

			if (showSplash)
			{
				FormProgress.CloseProgress();
				Application.DoEvents();
			}
		}

		public override void ShowEditor(bool showSplash)
		{
			ShowHomeSlide(showSplash);
			base.ShowEditor(showSplash);
		}

		public override void ShowHomeSlide(bool showSplash)
		{
			xtraTabControl.SelectedTabPage = _slides.FirstOrDefault();
			LoadTabPage(xtraTabControl.SelectedTabPage as IStarAppTabPageContainer, showSplash);
			RaiseSlideTypeChanged();
			RaiseOutputStatuesChanged();
		}

		private void LoadTabPage(IStarAppTabPageContainer tabPageContainer, bool showSplash)
		{
			if (tabPageContainer == null) return;
			if (tabPageContainer.ContentControl != null) return;

			xtraTabControl.SelectedPageChanging -= OnSelectedSlideChanging;
			xtraTabControl.Selecting += OnTabPageSelecting;

			if (showSplash)
			{
				FormProgress.ShowProgress("Loading data...", () =>
				{
					tabPageContainer.LoadContent();
					tabPageContainer.ContentControl?.LoadData();
				});
			}
			else
			{
				tabPageContainer.LoadContent();
				tabPageContainer.ContentControl?.LoadData();
			}

			xtraTabControl.Selecting -= OnTabPageSelecting;
			xtraTabControl.SelectedTabPage = (XtraTabPage)tabPageContainer;
			xtraTabControl.SelectedPageChanging += OnSelectedSlideChanging;
		}

		private void OnTabPageSelecting(Object sender, TabPageCancelEventArgs e)
		{
			e.Cancel = true;
		}

		public void AssignCloseActiveEditorsOnOutsideClick(Control control)
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

		private void OnSelectedSlideChanging(object sender, TabPageChangingEventArgs e)
		{
			((IStarAppTabPageContainer)e.PrevPage)?.ContentControl?.ApplyChanges();

			var tabPageContainer = e.Page as IStarAppTabPageContainer;
			LoadTabPage(tabPageContainer, true);
		}

		private void OnSelectedSlideChanged(Object sender, TabPageChangedEventArgs e)
		{
			RaiseSlideTypeChanged();
			RaiseOutputStatuesChanged();
		}
		#endregion

		#region Data Processing
		public override void LoadData()
		{
			_slides
				.OfType<IStarAppTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(s => s.LoadData());
		}

		public override void ApplyChanges()
		{
			_slides
				.OfType<IStarAppTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(s => s.ApplyChanges());
		}
		#endregion

		#region Output Processing
		public override bool ReadyForOutput => ActiveSlideContent?.ReadyForOutput ?? false;
		public override bool MultipleSlidesAllowed => ActiveSlideContent?.MultipleSlidesAllowed ?? false;

		public abstract Theme GetSelectedTheme(SlideType slideType);

		protected IList<OutputItem> GetOutputItems(bool onlyCurrentSlide)
		{
			var selectedOutputItems = new List<OutputItem>();

			var availableOutputGroups = new List<OutputGroup>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Slides...");
			FormProgress.ShowProgress(MainForm);
			if (onlyCurrentSlide)
			{
				if (ActiveSlideContent != null)
					availableOutputGroups.Add(ActiveSlideContent.GetOutputGroup());
			}
			else
			{
				var allSlides = _slides
					.OfType<IStarAppTabPageContainer>()
					.ToList();

				foreach (var tabPageContainer in allSlides.Where(slide => slide.ContentControl == null).ToList())
				{
					tabPageContainer.LoadContent();
					if (tabPageContainer.ContentControl is MultiTabControl multiTabControl)
					{
						multiTabControl.LoadAllTabPages();
						Application.DoEvents();
					}
				}

				var contentControls = allSlides
					.Select(container => container.ContentControl)
					.ToList();
				foreach (var contentControl in contentControls)
				{
					var outputGroup = contentControl.GetOutputGroup();
					if (outputGroup.Items.Any())
						availableOutputGroups.Add(outputGroup);
					Application.DoEvents();
				}
			}
			FormProgress.CloseProgress();

			if (!availableOutputGroups.Any())
				return selectedOutputItems;

			using (var form = new FormPreview(
				MainForm,
				PowerPointProcessor))
			{
				form.LoadGroups(availableOutputGroups);
				if (form.ShowDialog() == DialogResult.OK)
					selectedOutputItems.AddRange(form.GetSelectedItems());
			}

			return selectedOutputItems;
		}
		#endregion
	}
}
