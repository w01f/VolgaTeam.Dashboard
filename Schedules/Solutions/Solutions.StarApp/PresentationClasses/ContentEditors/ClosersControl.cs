using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
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
	public sealed partial class ClosersControl : StarAppControl
	{
		private readonly List<XtraTabPage> _tabPages = new List<XtraTabPage>();

		public override SlideType SlideType => SlideType.StarAppClosers;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab11Title;

		public ClosersControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll();

			_tabPages.Add(new ClosersTabPageContainerControl<ClosersTabAControl>(this));
			_tabPages.Add(new ClosersTabPageContainerControl<ClosersTabBControl>(this));
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
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.ClosersState.TabB.SlideHeader = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.ClosersState.TabC.SlideHeader = SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
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
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClosersState.TabA.SlideHeader ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab11SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab11SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClosersState.TabB.SlideHeader ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab11SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab11SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClosersState.TabC.SlideHeader ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
			}
			_allowToSave = true;
		}

		public void RaiseDataChanged()
		{
			_dataChanged = true;
			SlideContainer.RaiseDataChanged();
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

		public override bool ReadyForOutput => true;

		public override OutputGroup GetOutputGroup()
		{
			var outputConfigurations = new List<OutputConfiguration>();

			foreach (var closersControl in _tabPages
				.OfType<IClosersTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList())
			{
				outputConfigurations.Add(new OutputConfiguration(
					closersControl.OutputType,
					closersControl.OutputName,
					closersControl.SlidesCount));
			}

			return new OutputGroup(this)
			{
				Name = OutputName,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Configurations = outputConfigurations.ToArray()
			};
		}

		public override void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			foreach (var configuration in configurations)
			{
				var tabPage = _tabPages
					.OfType<IClosersTabPageContainer>()
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
					.OfType<IClosersTabPageContainer>()
					.Select(container => container.ContentControl)
					.First(contentControl => contentControl.OutputType == configuration.OutputType);
				previewGroups.Add(tabPage.GeneratePreview());
			}

			return previewGroups;
		}
		#endregion
	}
}