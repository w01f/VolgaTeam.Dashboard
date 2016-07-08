using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	//public partial class SnapshotEditorsContainer : UserControl
	public partial class SnapshotEditorsContainer :
		XtraTabPage,
		ISnapshotContentEditorControl,
		IOutputContainer
	{
		public Snapshot SnapshotData { get; private set; }

		private SnapshotScheduleEditorControl _snapshotControl;
		private SnapshotDigitalInfoControl _digitalInfoControl;

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> SelectedEditorChanged;

		public ISnapshotEditorControl ActiveEditor => (ISnapshotEditorControl)xtraTabControl.SelectedTabPage;
		public ISnapshotCollectionEditorControl ActiveItemCollection => ActiveEditor as ISnapshotCollectionEditorControl;

		public SnapshotEditorsContainer()
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
			_snapshotControl = new SnapshotScheduleEditorControl();
			_snapshotControl.DataChanged += OnSnapshotDataChanged;

			_digitalInfoControl = new SnapshotDigitalInfoControl(this);

			xtraTabControl.TabPages.AddRange(new XtraTabPage[]
			{
				_snapshotControl,
				_digitalInfoControl,
			});

			_snapshotControl.InitControls();
			_digitalInfoControl.InitControls();

			xtraTabControl.SelectedPageChanged += OnSelectedSnapshotsSetEditorChanged;
		}

		public void Release()
		{
			_snapshotControl.Release();
			_snapshotControl = null;

			_digitalInfoControl.Release();
			_digitalInfoControl = null;

			DataChanged = null;
			SelectedEditorChanged = null;
			SnapshotData = null;
		}

		#region Section Data Management
		public void LoadData(Snapshot snapshot)
		{
			SnapshotData = snapshot;
			Text = SnapshotData.Name;

			_snapshotControl.LoadData(SnapshotData);
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
				case SnapshotSettingsType.Schedule:
				case SnapshotSettingsType.AdvancedColumns:
					_snapshotControl.UpdateView();
					break;
				case SnapshotSettingsType.DigitalInfo:
					_digitalInfoControl.UpdateGridView();
					break;
			}
		}

		private void UpdateCollectionItemsInfo()
		{
			switch (ActiveEditor?.EditorType)
			{
				case SnapshotEditorType.Schedule:
					labelControlCollectionItemsInfo.Visible = true;
					labelControlCollectionItemsInfo.Text = String.Format("<color=gray>Total Programs: {0}</color>", SnapshotData.Programs.Count);
					break;
				case SnapshotEditorType.DigitalInfo:
					labelControlCollectionItemsInfo.Visible = true;
					if (SnapshotData.DigitalInfo.Records.Count < BaseDigitalInfoOneSheetOutputModel.MaxRecords)
						labelControlCollectionItemsInfo.Text = String.Format("<color=gray>DIGITAL Marketing Products: {0}</color>", SnapshotData.DigitalInfo.Records.Count);
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

		private void OnSnapshotDataChanged(object sender, EventArgs e)
		{
			RaiseDataChanged();
		}

		private void OnSelectedSnapshotsSetEditorChanged(object sender, TabPageChangedEventArgs e)
		{
			var previuseEditor = e.PrevPage as ISnapshotEditorControl;
			previuseEditor?.SaveData();
			UpdateCollectionItemsInfo();
			SelectedEditorChanged?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region Output Stuff
		public string OutputName => SnapshotData.Name;
		public OutputGroup GetOutputGroup()
		{
			return new OutputGroup(this)
			{
				Name = OutputName,
				IsCurrent = TabControl.SelectedTabPage == this,
				Configurations = _snapshotControl.GetOutputConfigurations().Union(_digitalInfoControl.GetOutputConfigurations()).ToArray()
			};
		}

		public void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			foreach (var configuration in configurations)
			{
				switch (configuration.OutputType)
				{
					case SnapshotOutputType.Program:
					case SnapshotOutputType.ProgramAndDigital:
						_snapshotControl.GenerateOutput(configuration.OutputType == SnapshotOutputType.ProgramAndDigital);
						break;
					case SnapshotOutputType.Digital:
						_digitalInfoControl.GenerateOneSheetOutput();
						break;
					case SnapshotOutputType.DigitalStrategy:
						_digitalInfoControl.GenerateStrategyOutput();
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
					case SnapshotOutputType.Program:
					case SnapshotOutputType.ProgramAndDigital:
						previewGroups.Add(_snapshotControl.GeneratePreview(configuration.OutputType == SnapshotOutputType.ProgramAndDigital));
						break;
					case SnapshotOutputType.Digital:
						previewGroups.Add(_digitalInfoControl.GenerateOneSheetPreview(String.Format("{0} ({1})", SnapshotData.Name, _digitalInfoControl.Text)));
						break;
					case SnapshotOutputType.DigitalStrategy:
						previewGroups.Add(_digitalInfoControl.GenerateStrategyPreview(String.Format("{0} ({1})", SnapshotData.Name, "Digital Strategies")));
						break;
				}
			}
			return previewGroups;
		}
		#endregion
	}
}
