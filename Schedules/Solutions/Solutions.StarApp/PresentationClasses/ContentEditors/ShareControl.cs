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
	public sealed partial class ShareControl : StarAppControl, IMultiTabsControl
	{
		private readonly List<XtraTabPage> _tabPages = new List<XtraTabPage>();
		public override SlideType SlideType => SlideType.StarAppShare;

		public ShareControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubATitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabAControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubBTitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabBControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubCTitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabCControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubDTitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabDControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubETitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabEControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubUTitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabUControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubVTitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabVControl>(this));
			if (!String.IsNullOrWhiteSpace(SlideContainer.StarInfo.Titles.Tab5SubWTitle))
				_tabPages.Add(new ShareTabPageContainerControl<ShareTabWControl>(this));

			xtraTabControl.TabPages.AddRange(_tabPages.OfType<XtraTabPage>().ToArray());

			var defaultPage = _tabPages.FirstOrDefault() as IShareTabPageContainer;
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
				.OfType<IShareTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(control => control.LoadData());

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var selectedTab = (xtraTabControl.SelectedTabPage as IShareTabPageContainer)?.ContentControl;

			if (selectedTab is ShareTabAControl)
			{
				SlideContainer.EditedContent.ShareState.TabA.SlideHeader = SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}
			else if (selectedTab is ShareTabBControl)
			{
				SlideContainer.EditedContent.ShareState.TabB.SlideHeader = SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}
			else if (selectedTab is ShareTabCControl)
			{
				SlideContainer.EditedContent.ShareState.TabC.SlideHeader = SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}
			else if (selectedTab is ShareTabDControl)
			{
				SlideContainer.EditedContent.ShareState.TabD.SlideHeader = SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}
			else if (selectedTab is ShareTabEControl)
			{
				SlideContainer.EditedContent.ShareState.TabE.SlideHeader = SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;
			}

			_tabPages
				.OfType<IShareTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(control => control.ApplyChanges());

			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;

			var selectedTab = (xtraTabControl.SelectedTabPage as IShareTabPageContainer)?.ContentControl;

			if (selectedTab is ShareTabAControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubAFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ShareState.TabA.SlideHeader ??
													SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ShareConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ShareTabBControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubBFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ShareState.TabB.SlideHeader ??
													SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ShareConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ShareTabCControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubCFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ShareState.TabC.SlideHeader ??
													SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ShareConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ShareTabDControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubDRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubDFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ShareState.TabD.SlideHeader ??
													SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ShareConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ShareTabEControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubERightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubEFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ShareState.TabE.SlideHeader ??
													SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.ShareConfiguration.HeadersPartEItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (selectedTab is ShareTabUControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubUFooterLogo;
			}
			else if (selectedTab is ShareTabVControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubVFooterLogo;
			}
			else if (selectedTab is ShareTabWControl)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubWFooterLogo;
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
				.OfType<IShareTabPageContainer>()
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

			var tabPageContainer = e.Page as IShareTabPageContainer;
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
			.OfType<IShareTabPageContainer>()
			.Where(container => container.ContentControl != null)
			.Select(container => container.ContentControl)
			.Any(contentControl => contentControl.ReadyForOutput);

		public override OutputGroup GetOutputGroup()
		{
			LoadAllTabPages();

			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab5Title,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = _tabPages
					.OfType<IShareTabPageContainer>()
					.Where(tabContainer => tabContainer.ContentControl.ReadyForOutput)
					.Select(tabContainer => tabContainer.ContentControl.GetOutputItem())
					.Where(outputItem => outputItem != null)
					.ToList()
			};
		}
		#endregion
	}
}