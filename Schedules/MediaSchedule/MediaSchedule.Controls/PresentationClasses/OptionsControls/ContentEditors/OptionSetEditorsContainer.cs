using System;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Output;
using Asa.Media.Controls.PresentationClasses.OptionsControls.Settings;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors
{
	//public partial class OptionSetEditorsContainer : UserControl,
	public partial class OptionSetEditorsContainer : XtraTabPage,
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

			simpleLabelItemCollectionItemsInfo.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemCollectionItemsInfo.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemCollectionItemsInfo.MinSize = RectangleHelper.ScaleSize(simpleLabelItemCollectionItemsInfo.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
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

		public void ShowEditor(OptionEditorType editorType)
		{
			var editorToShow = xtraTabControl.TabPages
				.OfType<IOptionSetEditorControl>()
				.First(e => e.EditorType == editorType);
			xtraTabControl.SelectedTabPage = (XtraTabPage)editorToShow;
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
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Always;
					simpleLabelItemCollectionItemsInfo.Text = String.Format("<color=gray>Total Programs: {0}</color>", OptionSetData.Programs.Count);
					break;
				case OptionEditorType.DigitalInfo:
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Always;
					if (OptionSetData.DigitalInfo.Records.Count < BaseDigitalInfoOneSheetOutputModel.MaxRecords)
						simpleLabelItemCollectionItemsInfo.Text = String.Format("<color=gray>DIGITAL Marketing Products: {0}</color>", OptionSetData.DigitalInfo.Records.Count);
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
			return new OutputGroup
			{
				Name = OutputName,
				IsCurrent = TabControl.SelectedTabPage == this,
				Items = _optionsControl.GetOutputItems().Union(_digitalInfoControl.GetOutputItems()).ToArray()
			};
		}
		#endregion
	}
}
