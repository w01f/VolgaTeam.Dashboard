using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class MultiTabControl : BaseShiftControl
	{
		public ShiftChildTabsContainer TabContainerInfo => (ShiftChildTabsContainer)TabInfo;

		public MultiTabControl()
		{
			InitializeComponent();
		}

		public MultiTabControl(BaseShiftContainer slideContainer, ShiftChildTabsContainer tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();
		}

		public override void InitControls()
		{
			xtraTabControl.TabPages.AddRange(GetChildTabPages().OfType<XtraTabPage>().ToArray());

			var defaultPage = xtraTabControl.TabPages.FirstOrDefault() as IChildTabPageContainer;
			LoadTabPage(defaultPage, false);
			LoadChildTabData();

			xtraTabControl.SelectedPageChanging += OnSelectedTabPageChanging;
			xtraTabControl.SelectedPageChanged += OnSelectedTabPageChanged;
			xtraTabControl.MouseWheel += OnTabControlMouseWheel;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemOutputToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOutputToggle.MaxSize, scaleFactor);
			layoutControlItemOutputToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemOutputToggle.MinSize, scaleFactor);
			emptySpaceItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemSlideHeader.MaxSize, scaleFactor);
			emptySpaceItemSlideHeader.MinSize = RectangleHelper.ScaleSize(emptySpaceItemSlideHeader.MinSize, scaleFactor);
			simpleLabelItemSlideDescriptionLeft.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSlideDescriptionLeft.MaxSize, scaleFactor);
			simpleLabelItemSlideDescriptionLeft.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSlideDescriptionLeft.MinSize, scaleFactor);
			simpleLabelItemSlideDescriptionRight.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSlideDescriptionRight.MaxSize, scaleFactor);
			simpleLabelItemSlideDescriptionRight.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSlideDescriptionRight.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
			Resize += OnResize;
		}

		public override void LoadData()
		{
			xtraTabControl.TabPages
				.OfType<IChildTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(control =>
				{
					control.LoadData();
					control.TabPageContainer.FormatSlideHeader();
				});

			LoadChildTabData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
			selectedContentControl?.ApplyChanges();

			_dataChanged = false;
		}

		protected virtual IList<IChildTabPageContainer> GetChildTabPages()
		{
			throw new NotImplementedException();
		}

		private void LoadChildTabData()
		{
			_allowToSave = false;

			var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
			if (selectedContentControl != null)
			{
				pictureEditLogoRight.Image = selectedContentControl.TabInfo.RightLogo;
				pictureEditLogoFooter.Image = selectedContentControl.TabInfo.FooterLogo;

				toggleSwitchOutput.IsOn = selectedContentControl.TabInfo.IsRegularChildTab && selectedContentControl.GetOutputEnableState();

				if (selectedContentControl.TabInfo.IsRegularChildTab)
				{
					layoutControlItemOutputToggle.Visibility = LayoutVisibility.Always;
					simpleLabelItemSlideDescriptionLeft.Visibility = LayoutVisibility.Never;
					simpleLabelItemSlideDescriptionRight.Visibility = LayoutVisibility.Never;
				}
				else
					layoutControlItemOutputToggle.Visibility = LayoutVisibility.Never;

				selectedContentControl.RaiseSlideDescriptionChanged();
			}

			_allowToSave = true;
		}

		public void RaiseDataChanged()
		{
			_dataChanged = true;
			SlideContainer.RaiseDataChanged();
		}

		public void LoadAllTabPages()
		{
			foreach (var tabPageContainer in xtraTabControl.TabPages
				.OfType<IChildTabPageContainer>()
				.Where(tabPage => tabPage.TabInfo.IsRegularChildTab && tabPage.OutputEnabled)
				.ToList())
			{
				if (tabPageContainer.ContentControl == null)
					tabPageContainer.LoadContent();
				tabPageContainer.ContentControl.LoadData();
				tabPageContainer.ContentControl.SlideDescriptionChanged += OnSlideDescriptionChanged;
				tabPageContainer.FormatSlideHeader();
			}
		}

		private void LoadTabPage(IChildTabPageContainer tabPageContainer, bool showSplash)
		{
			if (tabPageContainer == null) return;
			if (tabPageContainer.ContentControl != null) return;

			xtraTabControl.Selecting += OnTabPageSelecting;
			if (showSplash)
			{
				FormProgress.ShowProgress("Loading data...", () =>
				{
					tabPageContainer.LoadContent();
					if (tabPageContainer.ContentControl != null)
					{
						tabPageContainer.ContentControl.LoadData();
						tabPageContainer.ContentControl.SlideDescriptionChanged += OnSlideDescriptionChanged;
					}
					tabPageContainer.FormatSlideHeader();
				});
			}
			else
			{
				tabPageContainer.LoadContent();
				if (tabPageContainer.ContentControl != null)
				{
					tabPageContainer.ContentControl.LoadData();
					tabPageContainer.ContentControl.SlideDescriptionChanged += OnSlideDescriptionChanged;
				}
				tabPageContainer.FormatSlideHeader();
			}
			xtraTabControl.Selecting -= OnTabPageSelecting;
		}

		private void OnSlideDescriptionChanged(Object sender, SlideDescriptionChangedEventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(e.SlideDescription.LeftText))
			{
				simpleLabelItemSlideDescriptionLeft.Visibility = LayoutVisibility.Always;
				simpleLabelItemSlideDescriptionLeft.Text =
					String.Format("<size=11><color=gray>{0}</color></size>", e.SlideDescription.LeftText);
			}
			else
				simpleLabelItemSlideDescriptionLeft.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrWhiteSpace(e.SlideDescription.RightText))
			{
				simpleLabelItemSlideDescriptionRight.Visibility = LayoutVisibility.Always;
				simpleLabelItemSlideDescriptionRight.Text =
					String.Format("<size=11><color=gray>{0}</color></size>", e.SlideDescription.RightText);
			}
			else
				simpleLabelItemSlideDescriptionRight.Visibility = LayoutVisibility.Never;
		}

		private void OnOutputToggled(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
			if (selectedContentControl == null) return;
			selectedContentControl.ApplyOutputEnableState(toggleSwitchOutput.IsOn);
			RaiseDataChanged();
		}

		private void OnSelectedTabPageChanging(object sender, TabPageChangingEventArgs e)
		{
			if (_allowToSave)
				ApplyChanges();

			var tabPageContainer = e.Page as IChildTabPageContainer;
			LoadTabPage(tabPageContainer, true);
		}

		private void OnSelectedTabPageChanged(Object sender, TabPageChangedEventArgs e)
		{
			LoadChildTabData();
			SlideContainer.RaiseOutputStatuesChanged();
			SlideContainer.RaiseSlideTypeChanged();
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

		private void OnResize(object sender, EventArgs e)
		{
			panelLogoRight.Visible = panelLogoBottom.Visible = Width > 1000;
		}

		#region Output Staff
		public override SlideType SlideType => (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl?.SlideType ?? SlideType.ShiftCleanslate;

		public override bool MultipleSlidesAllowed
		{
			get
			{
				var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
				return selectedContentControl != null && selectedContentControl.MultipleSlidesAllowed;
			}
		}

		public override bool ReadyForOutput
		{
			get
			{
				var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
				return selectedContentControl != null && (!selectedContentControl.TabInfo.IsRegularChildTab || selectedContentControl.GetOutputEnableState()) && selectedContentControl.ReadyForOutput;
			}
		}

		public override OutputGroup GetOutputGroup()
		{
			var outputItems = new List<OutputItem>();

			if (MultipleSlidesAllowed)
			{
				LoadAllTabPages();
				outputItems.AddRange(xtraTabControl.TabPages
					.OfType<IChildTabPageContainer>()
					.Where(tabContainer => tabContainer.TabInfo.IsRegularChildTab && tabContainer.OutputEnabled && tabContainer.ContentControl != null && tabContainer.ContentControl.ReadyForOutput && tabContainer.ContentControl.MultipleSlidesAllowed)
					.SelectMany(tabContainer => tabContainer.ContentControl.GetOutputItems())
					.Where(outputItem => outputItem != null));
			}
			else
			{
				var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
				outputItems.AddRange(selectedContentControl?.GetOutputItems());
			}

			return new OutputGroup
			{
				Name = MultipleSlidesAllowed ? TabInfo.Title : outputItems.FirstOrDefault()?.Name,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = outputItems
			};
		}
		#endregion
	}
}