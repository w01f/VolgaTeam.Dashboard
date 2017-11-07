using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	//public abstract partial class BaseStarAppContainer : UserControl
	public abstract partial class BaseStarAppContainer : BaseSolutionEditor
	{
		private readonly List<XtraTabPage> _slides = new List<XtraTabPage>();
		private StarAppControl ActiveSlide => xtraTabControl.SelectedTabPage as StarAppControl;

		public StarAppSolutionInfo StarInfo { get; }
		public StarAppContent EditedContent { get; protected set; }
		public abstract IStarAppSettingsContainer SettingsContainer { get; }
		public override SolutionType SolutionType => SolutionType.StarApp;
		public override SlideType SelectedSlideType => ActiveSlide?.SlideType ?? SlideType.Cleanslate;
		public override Image HomeLogo => StarInfo.RibbonLogo;
		public override string HomeText => StarInfo.Titles.RibbonTitle;
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
		public override void InitControl()
		{
			var homePage = new XtraTabPage();
			homePage.Text = StarInfo.Titles.Tab0Title;
			_slides.Add(homePage);

			_slides.Add(new CoverControl(this));
			_slides.Add(new CNAControl(this));
			_slides.Add(new FishingControl(this));
			_slides.Add(new CustomerControl(this));
			_slides.Add(new ShareControl(this));
			_slides.Add(new ROIControl(this));
			_slides.Add(new MarketControl(this));
			_slides.Add(new VideoControl(this));
			_slides.Add(new AudienceControl(this));
			_slides.Add(new SolutionControl(this));
			_slides.Add(new ClosersControl(this));

			xtraTabControl.TabPages.AddRange(_slides.OfType<XtraTabPage>().ToArray());
			xtraTabControl.SelectedTabPage = _slides.FirstOrDefault();
			xtraTabControl.SelectedPageChanged += OnSelectedSlideChanged;

			foreach (var slideControl in _slides)
				AssignCloseActiveEditorsOnOutsideClick(slideControl);
		}

		private void OnSelectedSlideChanged(object sender, TabPageChangedEventArgs e)
		{
			var prevSlide = e.PrevPage as StarAppControl;
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
			_slides.OfType<StarAppControl>().ToList().ForEach(s => s.LoadData());
		}

		public override void ApplyChanges()
		{
			_slides.OfType<StarAppControl>().ToList().ForEach(s => s.ApplyChanges());
		}
		#endregion

		#region Output Processing

		protected override bool ReadyForOutput => ActiveSlide?.ReadyForOutput ?? false;
		public abstract Theme GetSelectedTheme(SlideType slideType);

		public IList<IStarAppSlide> GetOutputSlides()
		{
			var selectedSlides = new List<IStarAppSlide>();
			using (var form = new FormSelectOutputItems())
			{
				form.Text = "Slide Output Options";
				foreach (var slideControl in _slides.OfType<StarAppControl>().ToList().Where(s => s.ReadyForOutput))
				{
					var item = new CheckedListBoxItem(slideControl, slideControl.SlideName, ActiveSlide.SlideType == SlideType.Cleanslate || slideControl == ActiveSlide ? CheckState.Checked : CheckState.Unchecked);
					form.checkedListBoxControlOutputItems.Items.Add(item);
					if (slideControl == ActiveSlide)
						form.buttonXSelectCurrent.Tag = item;
				}
				if (form.ShowDialog() == DialogResult.OK)
					selectedSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
						OfType<CheckedListBoxItem>().
						Where(ci => ci.CheckState == CheckState.Checked).
						Select(ci => ci.Value).
						OfType<IStarAppSlide>());
			}
			return selectedSlides;
		}
		#endregion
	}
}
