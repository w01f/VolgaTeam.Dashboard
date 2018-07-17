using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	//public partial class SnapshotEditorsContainer : UserControl,
	public partial class SnapshotEditorsContainer : XtraTabPage,
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

			simpleLabelItemCollectionItemsInfo.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemCollectionItemsInfo.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemCollectionItemsInfo.MinSize = RectangleHelper.ScaleSize(simpleLabelItemCollectionItemsInfo.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
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

		public void ShowEditor(SnapshotEditorType editorType)
		{
			var editorToShow = xtraTabControl.TabPages
				.OfType<ISnapshotEditorControl>()
				.First(e => e.EditorType == editorType);
			xtraTabControl.SelectedTabPage = (XtraTabPage)editorToShow;
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
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Always;
					simpleLabelItemCollectionItemsInfo.Text = String.Format("<color=gray>Total Programs: {0}</color>", SnapshotData.Programs.Count);
					break;
				case SnapshotEditorType.DigitalInfo:
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Always;
					if (SnapshotData.DigitalInfo.Records.Count < BaseDigitalInfoOneSheetOutputModel.MaxRecords)
						simpleLabelItemCollectionItemsInfo.Text = String.Format("<color=gray>DIGITAL Marketing Products: {0}</color>", SnapshotData.DigitalInfo.Records.Count);
					else
						simpleLabelItemCollectionItemsInfo.Text = "<color=red>Maximum DIGITAL Marketing Products: <b><u>6</u></b></color>";
					break;
				default:
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Never;
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
			return new OutputGroup
			{
				Name = OutputName,
				IsCurrent = TabControl.SelectedTabPage == this,
				Items = _snapshotControl.GetOutputItems().Union(_digitalInfoControl.GetOutputItems()).ToArray()
			};
		}
		#endregion
	}
}
