﻿using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	//public partial class ColorSettingsControl : UserControl, ISettingsControl
	public partial class ColorSettingsControl : XtraTabPage, ISettingsControl
	{
		public int Order => 2;
		public bool IsAvailable => BusinessObjects.Instance.OutputManager.OptionsColors.Items.Count > 1;
		public ButtonInfo BarButton { get; }
		public OptionSettingsType SettingsType => OptionSettingsType.Colors;

		public ColorSettingsControl()
		{
			InitializeComponent();
			Text = "Slide Style";
			BusinessObjects.Instance.OutputManager.ColorCollectionChanged += (o, e) =>
				{
					LoadData();
				};
			BusinessObjects.Instance.OutputManager.SelectedColorChanged += (o, e) =>
			{
				LoadData();
			};
			BarButton = new ButtonInfo
			{
				Logo = BusinessObjects.Instance.ImageResourcesManager.OptionsRetractableBarColorsImage ?? Properties.Resources.SectionSettingsStyle,
				Tooltip = "Open Slide Style",
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));

			LoadData();
		}

		private void LoadData()
		{
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.OptionsColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			MediaMetaData.Instance.SettingsManager.SelectedColor = outputColorSelector.SelectedColor ??
				BusinessObjects.Instance.OutputManager.OptionsColors.Items.Select(ci => ci.Name).FirstOrDefault();
			MediaMetaData.Instance.SettingsManager.SaveSettings();
			BusinessObjects.Instance.OutputManager.RaiseSelectedColorChanged();
		}
	}
}
