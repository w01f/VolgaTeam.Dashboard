﻿using System;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Common.Helpers;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Themes;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Solutions.Common.PresentationClasses;
using DevComponents.DotNetBar;

namespace Asa.Media.Controls.PresentationClasses.Solutions
{
	class MediaSolutionsContainer : BaseSolutionContainerControl<MediaScheduleChangeInfo>
	{
		public override string Identifier => ContentIdentifiers.Solutions;
		public override RibbonTabItem TabPage => Controller.Instance.TabSolutions;
		protected override SolutionsManager SolutionManager => BusinessObjects.Instance.SolutionsManager;
		public override RibbonPanel PanelSolutions => Controller.Instance.SolutionsPanel;
		public override ButtonItem ButtonPowerPoint => Controller.Instance.SolutionsPowerPoint;
		public override ButtonItem ButtonPdf => Controller.Instance.MenuOutputPdfButton;
		public override ButtonItem ButtonPreview => Controller.Instance.SolutionsPreview;
		public override ButtonItem ButtonEmail => Controller.Instance.MenuEmailButton;

		protected override void UpdateEditedContet()
		{
			base.UpdateEditedContet();

			UpdateMenuOutputButtons();
		}

		protected override ISolutionEditor CreateSolutionEditor(BaseSolutionInfo solutionInfo)
		{
			switch (solutionInfo.Type)
			{
				case SolutionType.Dashboard:
					return new MediaDashboardContainer(solutionInfo);
				case SolutionType.StarApp:
					return new MediaStarAppContainer(solutionInfo);
				default:
					throw new NotImplementedException("Solution type is not implemented");
			}
		}

		protected override void LoadThemes(SlideType slideType)
		{
			base.LoadThemes();
			FormThemeSelector.Link(
				Controller.Instance.SolutionsTheme,
				Controller.Instance.FormMain,
				BusinessObjects.Instance.ThemeManager.GetThemes(slideType), 
				MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(slideType),
				MediaMetaData.Instance.SettingsManager,
				(theme, applyForAllSlideTypes) =>
				{
					MediaMetaData.Instance.SettingsManager.SetSelectedTheme(ActiveSolutionEditor.SelectedSlideType, theme.Name, applyForAllSlideTypes);
					MediaMetaData.Instance.SettingsManager.SaveSettings();
					if (applyForAllSlideTypes)
						Controller.Instance.ContentController.RaiseThemeChanged();
				}
			);
			Controller.Instance.SolutionsThemeBar.RecalcLayout();
			Controller.Instance.SolutionsPanel.PerformLayout();
		}

		protected override void LoadThemes()
		{
			if (ActiveSolutionEditor == null) return;
			LoadThemes(ActiveSolutionEditor.SelectedSlideType);
		}

		protected override void UpdateMenuOutputButtons()
		{
			Controller.Instance.MenuOutputPdfButton.Enabled = Controller.Instance.MenuEmailButton.Enabled = ActiveSolutionEditor.ReadyForOutput;
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink(ActiveSolutionEditor?.HelpKey);
		}
	}
}
