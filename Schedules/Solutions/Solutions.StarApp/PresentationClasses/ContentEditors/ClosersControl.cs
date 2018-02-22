using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
	public sealed partial class ClosersControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		private readonly List<XtraTabPage> _tabPages = new List<XtraTabPage>();

		public override SlideType SlideType => SlideType.StarAppClosers;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab11Title;

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
			SlideContainer.EditedContent.ClosersState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			_tabPages
				.OfType<IClosersTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(control => control.ApplyChanges());

			SlideContainer.SettingsContainer.SaveSettings();
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
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ClosersState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab11SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab11SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ClosersState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab11SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab11SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.FirstOrDefault(h =>
							String.Equals(h.Value, SlideContainer.EditedContent.ClosersState.SlideHeader,
								StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.ClosersConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
			}
			_allowToSave = true;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSelectedTabPageChanging(object sender, TabPageChangingEventArgs e)
		{
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
			if (_allowToSave)
				ApplyChanges();
			LoadPartData();
		}

		private void OnResize(object sender, EventArgs e)
		{
			panelLogoRight.Visible = panelLogoBottom.Visible = Width > 1000;
		}

		#region Output Staff

		public override bool ReadyForOutput => false;

		public override void GenerateOutput()
		{
			//SolutionDashboardPowerPointHelper.Instance.AppendCover(this);
		}

		public override PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//SolutionDashboardPowerPointHelper.Instance.PrepareCover(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}