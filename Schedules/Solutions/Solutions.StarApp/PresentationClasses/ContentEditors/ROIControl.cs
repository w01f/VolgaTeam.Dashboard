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
using Asa.Solutions.StarApp.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class ROIControl : StarAppControl, IMultiTabsControl
	{
		private readonly List<XtraTabPage> _tabPages = new List<XtraTabPage>();

		public override SlideType SlideType => SlideType.StarAppROI;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab6Title;

		public ROIControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			_tabPages.Add(new ROITabPageContainerControl<ROITabAControl>(this));
			_tabPages.Add(new ROITabPageContainerControl<ROITabBControl>(this));
			_tabPages.Add(new ROITabPageContainerControl<ROITabCControl>(this));
			_tabPages.Add(new ROITabPageContainerControl<ROITabDControl>(this));

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

			switch (xtraTabControl.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.ROIState.TabA.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.ROIState.TabB.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.ROIState.TabC.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;
					break;
				case 3:
					SlideContainer.EditedContent.ROIState.TabD.SlideHeader = SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;
					break;
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
			switch (xtraTabControl.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabA.SlideHeader ??
						SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabB.SlideHeader ??
						SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabC.SlideHeader ??
						SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab6SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab6SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ROIState.TabD.SlideHeader ??
						SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ROIConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
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
			((IROITabPageContainer)e.PrevPage)?.ContentControl?.ApplyChanges();

			var tabPageContainer = e.Page as IROITabPageContainer;
			if (tabPageContainer?.ContentControl != null) return;
			FormProgress.SetTitle("Loading data...");
			FormProgress.ShowProgress();
			Application.DoEvents();
			tabPageContainer?.LoadContent();
			tabPageContainer?.ContentControl?.LoadData();
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
			var outputConfigurations = new List<OutputConfiguration>();

			foreach (var tabPage in _tabPages)
			{
				var roiControl = ((IROITabPageContainer)tabPage).ContentControl;
				if (roiControl == null || !roiControl.ReadyForOutput) continue;

				outputConfigurations.Add(new OutputConfiguration(
					roiControl.OutputType,
					roiControl.OutputName,
					roiControl.SlidesCount,
					SlideContainer.ActiveSlideContent == this && xtraTabControl.SelectedTabPage == tabPage));
			}

			return new OutputGroup(this)
			{
				DisplayName = OutputName,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Configurations = outputConfigurations.ToArray()
			};
		}

		public override void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			foreach (var configuration in configurations)
			{
				var tabPage = _tabPages
					.OfType<IROITabPageContainer>()
					.Where(container => container.ContentControl != null)
					.Select(container => container.ContentControl)
					.First(contentControl => contentControl.OutputType == configuration.OutputType);
				tabPage.GenerateOutput();
			}
		}

		public override IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations)
		{
			var previewGroups = new List<PreviewGroup>();

			foreach (var configuration in configurations)
			{
				var tabPage = _tabPages
					.OfType<IROITabPageContainer>()
					.Where(container => container.ContentControl != null)
					.Select(container => container.ContentControl)
					.First(contentControl => contentControl.OutputType == configuration.OutputType);
				previewGroups.Add(tabPage.GeneratePreview());
			}

			return previewGroups;
		}
		#endregion
	}
}