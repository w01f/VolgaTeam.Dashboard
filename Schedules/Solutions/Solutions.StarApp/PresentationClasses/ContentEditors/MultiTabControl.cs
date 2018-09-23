using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class MultiTabControl : StarAppControl
	{
		public StarChildTabsContainer TabContainerInfo => (StarChildTabsContainer)TabInfo;

		public MultiTabControl()
		{
			InitializeComponent();
		}

		public MultiTabControl(BaseStarAppContainer slideContainer, StarChildTabsContainer tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();
		}

		public override void InitControls()
		{
			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			xtraTabControl.TabPages.AddRange(GetChildTabPages().OfType<XtraTabPage>().ToArray());

			var defaultPage = xtraTabControl.TabPages.FirstOrDefault() as IChildTabPageContainer;
			LoadTabPage(defaultPage, false);
			LoadChildTabData();

			xtraTabControl.SelectedPageChanged += OnSelectedTabPageChanged;
			xtraTabControl.SelectedPageChanging += OnSelectedTabPageChanging;
			xtraTabControl.MouseWheel += OnTabControlMouseWheel;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemOutputToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOutputToggle.MaxSize, scaleFactor);
			layoutControlItemOutputToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemOutputToggle.MinSize, scaleFactor);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			emptySpaceItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemSlideHeader.MaxSize, scaleFactor);
			emptySpaceItemSlideHeader.MinSize = RectangleHelper.ScaleSize(emptySpaceItemSlideHeader.MinSize, scaleFactor);

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
			if (selectedContentControl != null)
			{
				selectedContentControl.ApplyChanges();
				if (selectedContentControl.TabInfo.IsRegularChildTab)
					selectedContentControl.ApplySlideHeaderValue(comboBoxEditSlideHeader.EditValue as ListDataItem ??
						new ListDataItem
						{
							Value = comboBoxEditSlideHeader.EditValue as String
						});
			}

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
					layoutControlItemSlideHeader.Visibility = LayoutVisibility.Always;
					layoutControlItemOutputToggle.Visibility = LayoutVisibility.Always;

					var slideHaederTabInfo = (StarTabWithHeaderInfo)selectedContentControl.TabInfo;
					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(slideHaederTabInfo.HeadersItems
						.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = selectedContentControl.GetSlideHeaderValue();
					comboBoxEditSlideHeader.Properties.NullText =
						slideHaederTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
				}
				else
				{
					layoutControlItemSlideHeader.Visibility = LayoutVisibility.Never;
					layoutControlItemOutputToggle.Visibility = LayoutVisibility.Never;
				}
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
				tabPageContainer.ContentControl?.LoadData();
				tabPageContainer.FormatSlideHeader();
			}
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			RaiseDataChanged();
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
					tabPageContainer?.LoadContent();
					tabPageContainer?.ContentControl?.LoadData();
					tabPageContainer.FormatSlideHeader();
				});
			}
			else
			{
				tabPageContainer.LoadContent();
				tabPageContainer.ContentControl?.LoadData();
				tabPageContainer.FormatSlideHeader();
			}
			xtraTabControl.Selecting -= OnTabPageSelecting;
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
					.Select(tabContainer => tabContainer.ContentControl.GetOutputItem())
					.Where(outputItem => outputItem != null));
			}
			else
			{
				var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
				var outputItem = selectedContentControl?.GetOutputItem();
				if (outputItem != null)
					outputItems.Add(outputItem);
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