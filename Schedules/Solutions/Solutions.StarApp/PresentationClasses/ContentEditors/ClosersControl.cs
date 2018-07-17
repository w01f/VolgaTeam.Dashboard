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
	public sealed partial class ClosersControl : StarAppControl, IMultiTabsControl
	{
		private readonly List<XtraTabPage> _tabPages = new List<XtraTabPage>();

		public override SlideType SlideType => SlideType.StarAppClosers;

		public ClosersControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab11SubATitle))
				_tabPages.Add(new ClosersTabPageContainerControl<ClosersTabAControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab11SubBTitle))
				_tabPages.Add(new ClosersTabPageContainerControl<ClosersTabBControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab11SubCTitle))
				_tabPages.Add(new ClosersTabPageContainerControl<ClosersTabCControl>(this));

			xtraTabControl.TabPages.AddRange(_tabPages.OfType<XtraTabPage>().ToArray());

			var defaultPage = _tabPages.FirstOrDefault() as IClosersTabPageContainer;
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
				.OfType<IClosersTabPageContainer>()
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
					SlideContainer.EditedContent.ClosersState.TabA.SlideHeader = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.ClosersState.TabB.SlideHeader = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.ClosersState.TabC.SlideHeader = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;
					break;
			}

			_tabPages
				.OfType<IClosersTabPageContainer>()
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab11SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab11SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClosersState.TabA.SlideHeader ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab11SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab11SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClosersState.TabB.SlideHeader ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab11SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab11SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClosersState.TabC.SlideHeader ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
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
				.OfType<IClosersTabPageContainer>()
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
			((IClosersTabPageContainer)e.PrevPage)?.ContentControl?.ApplyChanges();

			var tabPageContainer = e.Page as IClosersTabPageContainer;
			if (tabPageContainer?.ContentControl != null) return;
			xtraTabControl.Enabled = false;
			FormProgress.SetTitle("Loading data...");
			FormProgress.ShowProgress();
			Application.DoEvents();
			tabPageContainer?.LoadContent();
			tabPageContainer?.ContentControl?.LoadData();
			xtraTabControl.Enabled = true;
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
			.OfType<IClosersTabPageContainer>()
			.Where(container => container.ContentControl != null)
			.Select(container => container.ContentControl)
			.Any(contentControl => contentControl.ReadyForOutput);

		public override OutputGroup GetOutputGroup()
		{
			LoadAllTabPages();

			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab11Title,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = _tabPages
					.OfType<IClosersTabPageContainer>()
					.Where(tabContainer => tabContainer.ContentControl.ReadyForOutput)
					.Select(tabContainer => tabContainer.ContentControl.GetOutputItem())
					.ToList()
			};
		}
		#endregion
	}
}