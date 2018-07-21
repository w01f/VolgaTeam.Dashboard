using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class ROIControl : StarAppControl, IMultiTabsControl
	{
		private readonly List<XtraTabPage> _tabPages = new List<XtraTabPage>();

		public override SlideType SlideType => SlideType.StarAppROI;

		public ROIControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab6SubATitle))
				_tabPages.Add(new ROITabPageContainerControl<ROITabAControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab6SubBTitle))
				_tabPages.Add(new ROITabPageContainerControl<ROITabBControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab6SubCTitle))
				_tabPages.Add(new ROITabPageContainerControl<ROITabCControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab6SubDTitle))
				_tabPages.Add(new ROITabPageContainerControl<ROITabDControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab6SubUTitle))
				_tabPages.Add(new ROITabPageContainerControl<ROITabUControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab6SubVTitle))
				_tabPages.Add(new ROITabPageContainerControl<ROITabVControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab6SubWTitle))
				_tabPages.Add(new ROITabPageContainerControl<ROITabWControl>(this));

			xtraTabControl.TabPages.AddRange(_tabPages.OfType<XtraTabPage>().ToArray());

			var defaultPage = _tabPages.FirstOrDefault() as IROITabPageContainer;
			defaultPage?.LoadContent();
			Application.DoEvents();
			xtraTabControl.SelectedTabPage = _tabPages.FirstOrDefault();

			xtraTabControl.SelectedPageChanged += OnSelectedTabPageChanged;
			xtraTabControl.SelectedPageChanging += OnSelectedTabPageChanging;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_tabPages
				.OfType<IROITabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(control => control.LoadData());

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var selectedTab = (xtraTabControl.SelectedTabPage as IROITabPageContainer)?.ContentControl;

			if (selectedTab is ROITabAControl)
			{
				SlideContainer.EditedContent.ROIState.TabA.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}
			else if (selectedTab is ROITabBControl)
			{
				SlideContainer.EditedContent.ROIState.TabB.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}
			else if (selectedTab is ROITabCControl)
			{
				SlideContainer.EditedContent.ROIState.TabC.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}
			else if (selectedTab is ROITabDControl)
			{
				SlideContainer.EditedContent.ROIState.TabD.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}

			_tabPages
				.OfType<IROITabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(control => control.ApplyChanges());

			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;

			var selectedTab = (xtraTabControl.SelectedTabPage as IROITabPageContainer)?.ContentControl;

			if (selectedTab is ROITabAControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubAFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabA.SlideHeader ??
													SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ROITabBControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubBFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabB.SlideHeader ??
													SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ROITabCControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubCFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabC.SlideHeader ??
													SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ROITabDControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubDRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubDFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabD.SlideHeader ??
													SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ROITabUControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubUFooterLogo;
			}
			else if (selectedTab is ROITabVControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubVFooterLogo;
			}
			else if (selectedTab is ROITabWControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubWFooterLogo;
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
			foreach (var tabPageContainer in _tabPages
				.OfType<IROITabPageContainer>()
				.ToList())
			{
				if (tabPageContainer.ContentControl == null)
					tabPageContainer.LoadContent();
				tabPageContainer.ContentControl?.LoadData();
			}
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			RaiseDataChanged();
		}

		private void OnSelectedTabPageChanging(object sender, TabPageChangingEventArgs e)
		{
			if (_allowToSave)
				ApplyChanges();

			var tabPageContainer = e.Page as IROITabPageContainer;
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

			xtraTabControl.TabPages
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = true);

			FormProgress.CloseProgress();
			Application.DoEvents();
		}

		private void OnSelectedTabPageChanged(object sender, TabPageChangedEventArgs e)
		{
			LoadPartData();
		}

		private void OnResize(object sender, EventArgs e)
		{
			panelLogoRight.Visible = panelLogoBottom.Visible = Width > 1000;
		}

		#region Output Staff

		public override bool ReadyForOutput => _tabPages
			.OfType<IROITabPageContainer>()
			.Where(container => container.ContentControl != null)
			.Select(container => container.ContentControl)
			.Any(contentControl => contentControl.ReadyForOutput);

		public override OutputGroup GetOutputGroup()
		{
			LoadAllTabPages();

			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab6Title,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = _tabPages
					.OfType<IROITabPageContainer>()
					.Where(tabContainer => tabContainer.ContentControl.ReadyForOutput)
					.Select(tabContainer => tabContainer.ContentControl.GetOutputItem())
					.Where(outputItem => outputItem != null)
					.ToList()
			};
		}
		#endregion
	}
}