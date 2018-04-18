﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses;
using DevExpress.XtraTab;
using Asa.Solutions.StarApp.PresentationClasses.Output;
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
		public override void InitControl()
		{
			FormProgress.SetTitle("Loading data...");
			FormProgress.ShowProgress();
			Application.DoEvents();
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

			FormProgress.CloseProgress();
			Application.DoEvents();
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

		protected IList<OutputGroup> GetOutputConfiguration()
		{
			var outputGroups = new List<OutputGroup>();

			var availableOutputGroups = _slides
				.OfType<IStarAppTabPageContainer>()
				.Where(container => container.ContentControl != null)
				.Select(container => container.ContentControl)
				.Where(control => control.ReadyForOutput)
				.Select(control => control.GetOutputGroup())
				.ToList();

			if (availableOutputGroups.Any())
			{
				using (var form = new FormConfigureOutput(availableOutputGroups))
				{

					if (form.ShowDialog() == DialogResult.OK)
						outputGroups.AddRange(availableOutputGroups);
					else
						availableOutputGroups.ForEach(g => g.Dispose());
				}
			}
			return outputGroups;
		}
		#endregion
	}
}
