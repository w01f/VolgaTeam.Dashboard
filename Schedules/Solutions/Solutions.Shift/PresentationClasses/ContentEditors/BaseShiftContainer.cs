using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public abstract partial class BaseStarAppContainer : UserControl
	public abstract partial class BaseShiftContainer : BaseSolutionEditor
	{
		private readonly List<XtraTabPage> _slides = new List<XtraTabPage>();

		public ShiftSolutionInfo ShiftInfo { get; }
		public ShiftContent EditedContent { get; protected set; }
		public abstract IShiftSettingsContainer SettingsContainer { get; }
		public BaseShiftControl ActiveSlideContent => (xtraTabControl.SelectedTabPage as IShiftTabPageContainer)?.ContentControl;
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

		protected BaseShiftContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo)
		{
			ShiftInfo = (ShiftSolutionInfo)solutionInfo;
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

			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab0Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<CleanslateControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab1Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<StartersControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab2Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<CNAControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab3Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<MarketControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab4Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<NeedsSolutionsControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab5Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<CBCControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab6Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<IntegratedSolutionControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab7Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<InvestmentControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab8Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<ClosersControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab9Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<NextStepsControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab10Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<ContractControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab11Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<SupportMaterialsControl>(this));
				Application.DoEvents();
			}

			xtraTabControl.TabPages.AddRange(_slides.OfType<XtraTabPage>().ToArray());
			Application.DoEvents();

			var defaultPage = _slides.FirstOrDefault() as IShiftTabPageContainer;
			defaultPage?.LoadContent();
			Application.DoEvents();
			xtraTabControl.SelectedTabPage = _slides.FirstOrDefault();

			xtraTabControl.SelectedPageChanged += OnSelectedSlideChanged;
			xtraTabControl.SelectedPageChanging += OnSelectedSlideChanging;

			if (showSplash)
			{
				FormProgress.CloseProgress();
				Application.DoEvents();
			}
		}

		private void OnSelectedSlideChanging(object sender, TabPageChangingEventArgs e)
		{
			((IShiftTabPageContainer)e.PrevPage)?.ContentControl?.ApplyChanges();

			var tabPageContainer = e.Page as IShiftTabPageContainer;
			if (tabPageContainer?.ContentControl != null) return;

			FormProgress.SetTitle("Loading data...");
			FormProgress.ShowProgress();
			Application.DoEvents();

			xtraTabControl.TabPages
				.Where(tabPage => tabPage != e.Page)
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = false);
			tabPageContainer?.LoadContent();
			tabPageContainer?.ContentControl?.LoadData();
			xtraTabControl.TabPages
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = true);

			FormProgress.CloseProgress();
			Application.DoEvents();
		}

		private void OnSelectedSlideChanged(object sender, TabPageChangedEventArgs e)
		{
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
			OnSelectedSlideChanging(xtraTabControl, new TabPageChangingEventArgs(null, xtraTabControl.SelectedTabPage));
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
					if (tabPageContainer.ContentControl is IMultiTabsControl multiTabsControl)
					{
						multiTabsControl.LoadAllTabPages();
						Application.DoEvents();
					}
				}

				var contentControls = allSlides
					.Select(container => container.ContentControl)
					.Where(control => control.ReadyForOutput)
					.ToList();
				foreach (var contentControl in contentControls)
				{
					availableOutputGroups.Add(contentControl.GetOutputGroup());
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
