using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Agenda;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.CBC;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Cover;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Goals;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Intro;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Market;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Partnership;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public abstract partial class BaseShiftContainer : UserControl
	public abstract partial class BaseShiftContainer : BaseSolutionEditor
	{
		private readonly List<XtraTabPage> _slides = new List<XtraTabPage>();

		public ShiftSolutionInfo ShiftInfo { get; }
		public ShiftContent EditedContent { get; protected set; }
		public abstract IShiftSettingsContainer SettingsContainer { get; }
		public BaseShiftControl ActiveSlideContent => (xtraTabControl.SelectedTabPage as IShiftTabPageContainer)?.ContentControl;
		public override SlideType SelectedSlideType => ActiveSlideContent?.SlideType ?? SlideType.ShiftCleanslate;
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

		protected BaseShiftContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo)
		{
			ShiftInfo = (ShiftSolutionInfo)solutionInfo;
			InitializeComponent();
		}

		#region GUI Processing
		public override void InitControl(bool showSplash)
		{
			if (ResourceManager.SolutionToggleSwitchSkinElement != null)
			{
				var element = SkinManager.GetSkinElement(SkinProductId.Editors, UserLookAndFeel.Default, "ToggleSwitch");
				element.Image.SetImage(ResourceManager.SolutionToggleSwitchSkinElement, Color.Transparent);
				LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
			}

			if (showSplash)
			{
				FormProgress.ShowProgress("Loading data...", () =>
				{
					ShiftInfo.LoadContentData();
					BeginInvoke(new MethodInvoker(() =>
					{
						Application.DoEvents();
						InitControlInner();
					}));
				}, false);
			}
			else
			{
				ShiftInfo.LoadContentData();
				InitControlInner();
			}
		}

		private void InitControlInner()
		{
			foreach (var tabInfo in ShiftInfo.TabsInfo)
				switch (tabInfo.TabType)
				{
					case ShiftTopTabType.Cleanslate:
						_slides.Add(new ShiftTabPageContainerControl<CleanslateControl>(this, tabInfo));
						break;
					case ShiftTopTabType.Cover:
						_slides.Add(new ShiftTabPageContainerControl<CoverControl>(this, tabInfo));
						break;
					case ShiftTopTabType.Intro:
						_slides.Add(new ShiftTabPageContainerControl<IntroControl>(this, tabInfo));
						break;
					case ShiftTopTabType.Agenda:
						_slides.Add(new ShiftTabPageContainerControl<AgendaControl>(this, tabInfo));
						break;
					case ShiftTopTabType.Goals:
						_slides.Add(new ShiftTabPageContainerControl<GoalsControl>(this, tabInfo));
						break;
					case ShiftTopTabType.Market:
						_slides.Add(new ShiftTabPageContainerControl<MarketControl>(this, tabInfo));
						break;
					case ShiftTopTabType.Partnership:
						_slides.Add(new ShiftTabPageContainerControl<PartnershipControl>(this, tabInfo));
						break;
					case ShiftTopTabType.NeedsSolutions:
						_slides.Add(new ShiftTabPageContainerControl<NeedsSolutionsControl>(this, tabInfo));
						break;
					case ShiftTopTabType.CBC:
						_slides.Add(new ShiftTabPageContainerControl<CBCControl>(this, tabInfo));
						break;
					case ShiftTopTabType.IntegratedSolution:
						_slides.Add(new ShiftTabPageContainerControl<IntegratedSolutionControl>(this, tabInfo));
						break;
					default:
						_slides.Add(new ShiftTabPageContainerControl<CommonTopTabControl>(this, tabInfo));
						break;
				}


			xtraTabControl.TabPages.AddRange(_slides.OfType<XtraTabPage>().ToArray());
			Application.DoEvents();

			xtraTabControl.SelectedTabPage = _slides.FirstOrDefault();
			LoadTabPage(xtraTabControl.SelectedTabPage as IShiftTabPageContainer, false);

			xtraTabControl.SelectedPageChanging += OnSelectedSlideChanging;
			xtraTabControl.SelectedPageChanged += OnSelectedSlideChanged;
			xtraTabControl.MouseWheel += OnTabControlMouseWheel;
		}

		public override void ShowEditor(bool showSplash)
		{
			ShowHomeSlide(showSplash);
			base.ShowEditor(showSplash);
		}

		public override void ShowHomeSlide(bool showSplash)
		{
			RaiseSlideTypeChanged();
			RaiseOutputStatuesChanged();
		}

		private void LoadTabPage(IShiftTabPageContainer tabPageContainer, bool showSplash)
		{
			if (tabPageContainer == null) return;
			if (tabPageContainer.ContentControl != null) return;

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
			((IShiftTabPageContainer)e.PrevPage)?.ContentControl?.ApplyChanges();

			var tabPageContainer = e.Page as IShiftTabPageContainer;
			LoadTabPage(tabPageContainer, true);
		}

		private void OnSelectedSlideChanged(Object sender, TabPageChangedEventArgs e)
		{
			RaiseSlideTypeChanged();
			RaiseOutputStatuesChanged();
		}

		private void OnTabPageSelecting(Object sender, TabPageCancelEventArgs e)
		{
			e.Cancel = true;
		}


		private void OnTabControlMouseWheel(object sender, MouseEventArgs e)
		{
			var point = new Point(e.X, e.Y);
			var hitInfo = xtraTabControl.CalcHitInfo(point);
			if (hitInfo?.Page == null)
				return;

			var currentPageIndex = xtraTabControl.TabPages.IndexOf(hitInfo.Page);

			if (e.Delta < 0 && currentPageIndex < xtraTabControl.TabPages.Count - 1)
			{
				var nextPageIndex = currentPageIndex + 1;
				var moveToPageIndex = nextPageIndex;
				do
				{
					xtraTabControl.MakePageVisible(xtraTabControl.TabPages[moveToPageIndex]);
					hitInfo = xtraTabControl.CalcHitInfo(point);
					if (hitInfo?.Page == null)
						break;
					currentPageIndex = xtraTabControl.TabPages.IndexOf(hitInfo.Page);
					moveToPageIndex++;

				} while (nextPageIndex > currentPageIndex && moveToPageIndex <= xtraTabControl.TabPages.Count - 1);
			}
			else if (currentPageIndex > 0)
			{
				var nextPageIndex = currentPageIndex - 1;
				var moveToPageIndex = nextPageIndex;
				do
				{
					xtraTabControl.MakePageVisible(xtraTabControl.TabPages[moveToPageIndex]);
					hitInfo = xtraTabControl.CalcHitInfo(point);
					if (hitInfo?.Page == null)
						break;
					currentPageIndex = xtraTabControl.TabPages.IndexOf(hitInfo.Page);
					moveToPageIndex--;

				} while (nextPageIndex < currentPageIndex && moveToPageIndex >= 0);
			}
		}
		#endregion

		#region Data Processing
		public override void LoadData()
		{
			_slides
				.OfType<IShiftTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(s => s.LoadData());
		}

		public override void ApplyChanges()
		{
			_slides
				.OfType<IShiftTabPageContainer>()
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
					.OfType<IShiftTabPageContainer>()
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

			if (!availableOutputGroups.SelectMany(group => group.Items).Any())
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
