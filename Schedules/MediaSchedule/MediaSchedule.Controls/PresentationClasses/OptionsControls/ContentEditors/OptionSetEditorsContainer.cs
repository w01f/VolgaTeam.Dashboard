﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Output;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Settings;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	//public partial class OptionSetEditorsContainer : UserControl
	public partial class OptionSetEditorsContainer :
		XtraTabPage,
		IOptionContentEditorControl,
		IOutputContainer
	{
		public OptionSet OptionSetData { get; private set; }

		private OptionScheduleEditorControl _optionsControl;
		private OptionSetDigitalInfoControl _digitalInfoControl;

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> SelectedEditorChanged;

		public IOptionSetEditorControl ActiveEditor => (IOptionSetEditorControl)xtraTabControl.SelectedTabPage;
		public IOptionSetCollectionEditorControl ActiveItemCollection => ActiveEditor as IOptionSetCollectionEditorControl;

		public OptionSetEditorsContainer()
		{
			InitializeComponent();
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				labelControlCollectionItemsInfo.Font = font;
			}
		}

		public void InitControls()
		{
			_optionsControl = new OptionScheduleEditorControl();
			_optionsControl.DataChanged += OnOptionSetDataChanged;

			_digitalInfoControl = new OptionSetDigitalInfoControl(this);

			xtraTabControl.TabPages.AddRange(new XtraTabPage[]
			{
				_optionsControl,
				_digitalInfoControl,
			});

			_optionsControl.InitControls();
			_digitalInfoControl.InitControls();

			xtraTabControl.SelectedPageChanged += OnSelectedOptionsSetEditorChanged;
		}

		public void Release()
		{
			_optionsControl.Release();
			_optionsControl = null;

			_digitalInfoControl.Release();
			_digitalInfoControl = null;

			DataChanged = null;
			SelectedEditorChanged = null;
			OptionSetData = null;
		}

		#region Section Data Management
		public void LoadData(OptionSet optionSet)
		{
			OptionSetData = optionSet;
			Text = OptionSetData.Name;

			_optionsControl.LoadData(OptionSetData);
			_digitalInfoControl.LoadData();
			UpdateCollectionItemsInfo();
		}

		public void SaveData()
		{
			ActiveEditor?.SaveData();
		}

		public void UpdateAccordingSettings(SettingsChangedEventArgs eventArgs)
		{
			switch (eventArgs.ChangedSettingsType)
			{
				case OptionSettingsType.Schedule:
				case OptionSettingsType.AdvancedColumns:
					_optionsControl.UpdateView();
					break;
				case OptionSettingsType.DigitalInfo:
					_digitalInfoControl.UpdateGridView();
					break;
			}
		}

		private void UpdateCollectionItemsInfo()
		{
			switch (ActiveEditor?.EditorType)
			{
				case OptionEditorType.Schedule:
					labelControlCollectionItemsInfo.Visible = true;
					labelControlCollectionItemsInfo.Text = String.Format("<color=gray>Total Programs: {0}</color>", OptionSetData.Programs.Count);
					break;
				case OptionEditorType.DigitalInfo:
					labelControlCollectionItemsInfo.Visible = true;
					if (OptionSetData.DigitalInfo.Records.Count < BaseDigitalInfoOutputModel.MaxDigitalProducts)
						labelControlCollectionItemsInfo.Text = String.Format("<color=gray>DIGITAL Marketing Products: {0}</color>", OptionSetData.DigitalInfo.Records.Count);
					else
						labelControlCollectionItemsInfo.Text = "<color=red>Maximum DIGITAL Marketing Products: <b><u>6</u></b></color>";
					break;
				default:
					labelControlCollectionItemsInfo.Visible = false;
					break;
			}
		}

		public void RaiseDataChanged()
		{
			UpdateCollectionItemsInfo();
			DataChanged?.Invoke(ActiveEditor, EventArgs.Empty);
		}

		private void OnOptionSetDataChanged(object sender, EventArgs e)
		{
			RaiseDataChanged();
		}

		private void OnSelectedOptionsSetEditorChanged(object sender, TabPageChangedEventArgs e)
		{
			var previuseEditor = e.PrevPage as IOptionSetEditorControl;
			previuseEditor?.SaveData();
			UpdateCollectionItemsInfo();
			SelectedEditorChanged?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region Output Stuff
		public string OutputName => OptionSetData.Name;
		public OutputGroup GetOutputGroup()
		{
			return new OutputGroup(this)
			{
				Name = OutputName,
				IsCurrent = TabControl.SelectedTabPage == this,
				Configurations = _optionsControl.GetOutputConfigurations().Union(_digitalInfoControl.GetOutputConfigurations()).ToArray()
			};
		}

		public void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			foreach (var configuration in configurations)
			{
				switch (configuration.OutputType)
				{
					case OptionSetOutputType.Program:
					case OptionSetOutputType.ProgramAndDigital:
						_optionsControl.GenerateOutput(GetSelectedTheme(), configuration.OutputType == OptionSetOutputType.ProgramAndDigital);
						break;
					case OptionSetOutputType.Digital:
						_digitalInfoControl.GenerateOutput();
						break;
				}
			}
		}

		public IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations)
		{
			var previewGroups = new List<PreviewGroup>();

			foreach (var configuration in configurations)
			{
				switch (configuration.OutputType)
				{
					case OptionSetOutputType.Program:
					case OptionSetOutputType.ProgramAndDigital:
						previewGroups.Add(_optionsControl.GeneratePreview(GetSelectedTheme(), configuration.OutputType == OptionSetOutputType.ProgramAndDigital));
						break;
					case OptionSetOutputType.Digital:
						previewGroups.Add(_digitalInfoControl.GeneratePreview(String.Format("{0} ({1})", OptionSetData.Name, _digitalInfoControl.Text)));
						break;
				}
			}
			return previewGroups;
		}

		private Theme GetSelectedTheme()
		{
			var slideType = MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVOptions : SlideType.RadioOptions;
			return BusinessObjects.Instance.ThemeManager.GetThemes(slideType).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(slideType)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(slideType)));
		}
		#endregion
	}
}