using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
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
			defaultPage?.LoadContent();
			Application.DoEvents();
			xtraTabControl.SelectedTabPage = xtraTabControl.TabPages.FirstOrDefault();

			xtraTabControl.SelectedPageChanged += OnSelectedTabPageChanged;
			xtraTabControl.SelectedPageChanging += OnSelectedTabPageChanging;

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
				.ForEach(control => control.LoadData());

			LoadChildTabaData();
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

		private void LoadChildTabaData()
		{
			_allowToSave = false;

			var selectedContentControl = (xtraTabControl.SelectedTabPage as IChildTabPageContainer)?.ContentControl;
			if (selectedContentControl == null) return;

			pictureEditLogoRight.Image = selectedContentControl.TabInfo.RightLogo;
			pictureEditLogoFooter.Image = selectedContentControl.TabInfo.FooterLogo;

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
				.Where(tabPage => tabPage.TabInfo.TabType != ShiftChildTabType.U && tabPage.TabInfo.TabType != ShiftChildTabType.V && tabPage.TabInfo.TabType != ShiftChildTabType.W)
				.ToList())
			{
				if (tabPageContainer.ContentControl == null)
					tabPageContainer.LoadContent();
				tabPageContainer.ContentControl?.LoadData();
			}
		}

		private void OnSelectedTabPageChanging(object sender, TabPageChangingEventArgs e)
		{
			if (_allowToSave)
				ApplyChanges();

			var tabPageContainer = e.Page as IChildTabPageContainer;
			if (tabPageContainer?.ContentControl != null) return;

			xtraTabControl.TabPages
				.Where(tabPage => tabPage != e.Page)
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = false);

			FormProgress.SetTitle("Loading data...");
			FormProgress.ShowProgress();
			Application.DoEvents();

			tabPageContainer?.LoadContent();
			tabPageContainer?.ContentControl?.LoadData();

			FormProgress.CloseProgress();
			Application.DoEvents();

			xtraTabControl.TabPages
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = true);
			Application.DoEvents();
		}

		private void OnSelectedTabPageChanged(object sender, TabPageChangedEventArgs e)
		{
			LoadChildTabaData();
			SlideContainer.RaiseOutputStatuesChanged();
			SlideContainer.RaiseSlideTypeChanged();
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
				return selectedContentControl != null && selectedContentControl.ReadyForOutput;
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
					.Where(tabContainer => tabContainer.ContentControl != null && tabContainer.ContentControl.ReadyForOutput && tabContainer.ContentControl.MultipleSlidesAllowed)
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