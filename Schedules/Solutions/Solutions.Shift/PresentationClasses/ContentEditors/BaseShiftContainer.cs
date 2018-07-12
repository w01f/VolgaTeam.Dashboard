using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using Asa.Solutions.Shift.PresentationClasses.Output;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public abstract partial class BaseStarAppContainer : UserControl
	public abstract partial class BaseShiftContainer : BaseSolutionEditor
	{
		private readonly List<XtraTabPage> _slides = new List<XtraTabPage>();

		public ShiftSolutionInfo ShiftInfo { get; }
		public ShiftContent EditedContent { get; protected set; }
		public abstract IShiftSettingsContainer SettingsContainer { get; }
		public BaseShiftControl ActiveSlideContent => (xtraTabControl.SelectedTabPage as IShiftTabPageContainer)?.ContentControl;
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

		protected BaseShiftContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo)
		{
			ShiftInfo = (ShiftSolutionInfo)solutionInfo;
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

			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab0Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<CleanslateControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab1Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<StartersControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab2Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<CNAControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab3Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<MarketControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab4Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<NeedsSolutionsControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab5Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<CBCControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab6Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<IntegratedSolutionControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab7Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<InvestmentControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab8Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<ClosersControl>(this));
				Application.DoEvents();
			}
			if (!String.IsNullOrEmpty(ShiftInfo.Titles.Tab9Title))
			{
				_slides.Add(new ShiftTabPageContainerControl<SupportMaterialsControl>(this));
				Application.DoEvents();
			}

			xtraTabControl.TabPages.AddRange(_slides.OfType<XtraTabPage>().ToArray());
			Application.DoEvents();

			var defaultPage = _slides.FirstOrDefault() as IShiftTabPageContainer;
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
			((IShiftTabPageContainer)e.PrevPage)?.ContentControl?.ApplyChanges();

			var tabPageContainer = e.Page as IShiftTabPageContainer;
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
				.OfType<IShiftTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(s => s.LoadData());
		}

		public override void ApplyChanges()
		{
			_slides
				.OfType<IShiftTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.ToList()
				.ForEach(s => s.ApplyChanges());
		}
		#endregion

		#region Output Processing
		public override bool ReadyForOutput => ActiveSlideContent?.ReadyForOutput ?? false;

		public abstract Theme GetSelectedTheme(SlideType slideType);

		private List<ShiftOutputType> _selectedOutputItems;
		public List<ShiftOutputType> SelectedOutputItems
		{
			get
			{
				if (_selectedOutputItems == null)
				{
					_selectedOutputItems = new List<ShiftOutputType>();
					if (!String.IsNullOrWhiteSpace(SettingsContainer.SelectedShiftOutputItemsEncoded))
						_selectedOutputItems.AddRange(SettingsContainer.SelectedShiftOutputItemsEncoded.Split(';').Select(item => (ShiftOutputType)Int32.Parse(item)));
				}
				return _selectedOutputItems;
			}
		}


		protected IList<OutputGroup> GetOutputConfiguration()
		{
			var outputGroups = new List<OutputGroup>();

			var allSlides = _slides
				.OfType<IShiftTabPageContainer>()
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
							SettingsContainer.SelectedShiftOutputItemsEncoded =
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
