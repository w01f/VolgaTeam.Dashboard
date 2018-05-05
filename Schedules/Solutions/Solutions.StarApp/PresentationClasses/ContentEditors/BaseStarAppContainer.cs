using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	//public abstract partial class BaseStarAppContainer : UserControl
	public abstract partial class BaseStarAppContainer : BaseSolutionEditor
	{
		private readonly List<XtraTabPage> _slides = new List<XtraTabPage>();

		public StarAppSolutionInfo StarInfo { get; }
		public StarAppContent EditedContent { get; protected set; }
		public abstract IStarAppSettingsContainer SettingsContainer { get; }
		public StarAppControl ActiveSlideContent => (xtraTabControl.SelectedTabPage as IStarAppTabPageContainer)?.ContentControl;
		public override SlideType SelectedSlideType => ActiveSlideContent?.SlideType ?? SlideType.Cleanslate;
		public abstract Form MainForm { get; }
		public abstract Color? AccentColor { get; }
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

		public override void InitControl(bool showSplash)
		{
			if (showSplash)
			{
				FormProgress.SetTitle("Loading data...");
				FormProgress.ShowProgress();
				Application.DoEvents();
			}

			_slides.Add(new StarAppTabPageContainerControl<CleanslateControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<CoverControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<CNAControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<FishingControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<CustomerControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<ShareControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<ROIControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<MarketControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<VideoControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<AudienceControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<SolutionControl>(this));
			Application.DoEvents();
			_slides.Add(new StarAppTabPageContainerControl<ClosersControl>(this));
			Application.DoEvents();

			xtraTabControl.TabPages.AddRange(_slides.OfType<XtraTabPage>().ToArray());
			Application.DoEvents();

			var defaultPage = _slides.FirstOrDefault() as IStarAppTabPageContainer;
			defaultPage?.LoadContent();
			Application.DoEvents();
			xtraTabControl.SelectedTabPage = _slides.FirstOrDefault();

			xtraTabControl.SelectedPageChanged += OnSelectedSlideChanged;
			xtraTabControl.SelectedPageChanging += OnSelectedSlideChanging;

			if (showSplash)
			{
				FormProgress.CloseProgress();
				Application.DoEvents();
			}
		}

		private void OnSelectedSlideChanging(object sender, TabPageChangingEventArgs e)
		{
			((IStarAppTabPageContainer)e.PrevPage)?.ContentControl?.ApplyChanges();

			var tabPageContainer = e.Page as IStarAppTabPageContainer;
			if (tabPageContainer?.ContentControl != null) return;
			FormProgress.SetTitle("Loading data...");
			FormProgress.ShowProgress();
			Application.DoEvents();
			tabPageContainer?.LoadContent();
			tabPageContainer?.ContentControl?.LoadData();
			FormProgress.CloseProgress();
			Application.DoEvents();
		}

		private void OnSelectedSlideChanged(object sender, TabPageChangedEventArgs e)
		{
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
			OnSelectedSlideChanging(xtraTabControl, new TabPageChangingEventArgs(null, xtraTabControl.SelectedTabPage));
		}

		public void AssignCloseActiveEditorsOnOutsideClick(Control control)
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
			_slides
				.OfType<IStarAppTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(s => s.LoadData());
		}

		public override void ApplyChanges()
		{
			_slides
				.OfType<IStarAppTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(s => s.ApplyChanges());
		}
		#endregion

		#region Output Processing
		public override bool ReadyForOutput => ActiveSlideContent?.ReadyForOutput ?? false;

		public abstract Theme GetSelectedTheme(SlideType slideType);

		private List<StarAppOutputType> _selectedOutputItems;
		public List<StarAppOutputType> SelectedOutputItems
		{
			get
			{
				if (_selectedOutputItems == null)
				{
					_selectedOutputItems = new List<StarAppOutputType>();
					if (!String.IsNullOrWhiteSpace(SettingsContainer.SelectedStarOutputItemsEncoded))
						_selectedOutputItems.AddRange(SettingsContainer.SelectedStarOutputItemsEncoded.Split(';').Select(item => (StarAppOutputType)Int32.Parse(item)));
				}
				return _selectedOutputItems;
			}
		}


		protected IList<OutputGroup> GetOutputConfiguration()
		{
			var outputGroups = new List<OutputGroup>();

			var allSlides = _slides
				.OfType<IStarAppTabPageContainer>()
				.ToList();

			var allSlidesLoaded = allSlides.All(slide => slide.ContentControl != null);

			var availableOutputGroups = allSlides
				.Where(slide => slide.ContentControl != null)
				.Select(container => container.ContentControl)
				.Where(control => control.ReadyForOutput)
				.Select(control => control.GetOutputGroup())
				.ToList();

			if (SelectedOutputItems.Any())
				foreach (var outputGroup in availableOutputGroups)
					foreach (var configuration in outputGroup.Configurations)
						configuration.SelectedForOutput = SelectedOutputItems.Contains(configuration.OutputType);

			if (availableOutputGroups.Any())
			{
				FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
				FormProgress.ShowProgress();
				var previewGroup = availableOutputGroups
					.Where(group => group.Configurations.Any(configuration => configuration.IsCurrent))
					.SelectMany(g => g.OutputContainer.GeneratePreview(g.Configurations.Where(configuration => configuration.IsCurrent).ToList()))
					.First();
				Utilities.ActivateForm(MainForm.Handle, MainForm.WindowState == FormWindowState.Maximized, false);
				FormProgress.CloseProgress();

				using (var form = new FormConfigureOutput(availableOutputGroups, previewGroup, allSlidesLoaded))
				{
					if (!allSlidesLoaded)
					{
						form.LoadAllRequested += (o, e) =>
						{
							FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Slides...");
							FormProgress.ShowProgress(form);

							foreach (var tabPageContainer in allSlides.Where(slide => slide.ContentControl == null).ToList())
							{
								tabPageContainer.LoadContent();
								if (tabPageContainer.ContentControl is IMultiTabsControl multiTabsControl)
									multiTabsControl.LoadAllTabPages();
							}

							Utilities.ActivateForm(form.Handle, false, false);
							FormProgress.CloseProgress();

							var outputItems = allSlides
								.Where(slide => slide.ContentControl != null)
								.Select(container => container.ContentControl)
								.Where(control => control.ReadyForOutput)
								.Select(control => control.GetOutputGroup())
								.ToList();
							if (SelectedOutputItems.Any())
								foreach (var outputGroup in outputItems)
									foreach (var configuration in outputGroup.Configurations)
										configuration.SelectedForOutput = SelectedOutputItems.Contains(configuration.OutputType);

							e.OutputItems.AddRange(outputItems);
						};
					}

					form.hyperLinkEditAddSingleSlide.Text = String.Format("<color={1}>{0}</color>", form.hyperLinkEditAddSingleSlide.Text, AccentColor.HasValue
						? AccentColor.Value.ToHex()
						: "blue");
					form.hyperLinkEditLoadAll.Text = String.Format("<color={1}>{0}</color>", form.hyperLinkEditLoadAll.Text, allSlidesLoaded ? "gray" : AccentColor.HasValue
						? AccentColor.Value.ToHex()
						: "blue");
					form.hyperLinkEditSelectAll.Text = String.Format("<color={1}>{0}</color>", form.hyperLinkEditSelectAll.Text, AccentColor.HasValue
						? AccentColor.Value.ToHex()
						: "blue");
					form.hyperLinkEditClearAll.Text = String.Format("<color={1}>{0}</color>", form.hyperLinkEditClearAll.Text, AccentColor.HasValue
						? AccentColor.Value.ToHex()
						: "blue");

					if (form.ShowDialog() == DialogResult.OK)
					{
						var result = form.GetSelectedItems();
						outputGroups.AddRange(result.Item1);

						if (result.Item2)
						{
							SelectedOutputItems.Clear();
							SelectedOutputItems.AddRange(result.Item1.SelectMany(group => group.Configurations).Select(configuration => configuration.OutputType));
							SettingsContainer.SelectedStarOutputItemsEncoded =
								String.Join(";", SelectedOutputItems.Select(item => (Int32)item).ToArray());
							SettingsContainer.SaveSettings();
						}
					}
					else
						availableOutputGroups.ForEach(g => g.Dispose());
				}
			}
			return outputGroups;
		}
		#endregion
	}
}
