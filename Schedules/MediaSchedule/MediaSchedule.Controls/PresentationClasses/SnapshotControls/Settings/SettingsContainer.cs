using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	public partial class SettingsContainer : UserControl
	{
		private readonly List<ISettingsControl> _settingsControls = new List<ISettingsControl>();
		private readonly List<SnapshotEditorSettingsRelation> _snapshotEditorSettings = new List<SnapshotEditorSettingsRelation>();
		private SnapshotContent _editedContent;

		public event EventHandler<SettingsChangedEventArgs> SettingsChanged;
		public event EventHandler<EventArgs> SettingsControlsUpdated;

		public SettingsContainer()
		{
			InitializeComponent();
		}

		#region Controls Management
		public void InitControl()
		{
			InitSettingsControls();
			InitContentEditorSettingsRelations();
		}

		private void InitContentEditorSettingsRelations()
		{
			_snapshotEditorSettings.AddRange(new[]
			{
				new SnapshotEditorSettingsRelation
				{
					EditorType = SnapshotEditorType.Schedule,
					SettingsTypes = new []
					{
						SnapshotSettingsType.Schedule,
						SnapshotSettingsType.Colors,
						SnapshotSettingsType.Calendar
					}
				} ,
				new SnapshotEditorSettingsRelation
				{
					EditorType = SnapshotEditorType.DigitalInfo,
					SettingsTypes = new []
					{
						SnapshotSettingsType.DigitalInfo
					}
				},
				new SnapshotEditorSettingsRelation
				{
					EditorType = SnapshotEditorType.Summary,
					SettingsTypes = new []
					{
						SnapshotSettingsType.Summary,
						SnapshotSettingsType.Colors
					}
				},
			});
		}

		private void InitSettingsControls()
		{
			_settingsControls.AddRange(new ISettingsControl[]
				{
					new SnapshotColumnSettingsControl(),
					new SnapshotDigitalInfoSettingsControl(),
					new SummaryColumnSettingsControl(),
					new ActiveWeeksSettingsControl(),
					new ColorSettingsControl(),
				});
			foreach (var settingsDataControl in _settingsControls.OfType<ISettingsDataControl>())
				settingsDataControl.DataChanged += OnSettingsChanged;
		}

		public IEnumerable<ButtonInfo> GetSettingsButtons()
		{
			return xtraTabControlOptions.TabPages
				.OfType<ISettingsControl>()
				.Select(sc => sc.BarButton)
				.ToList();
		}
		#endregion

		#region Data Management
		public void LoadContent(SnapshotContent editedContent)
		{
			_editedContent = editedContent;
			_settingsControls.OfType<IContentSettingsControl>().ToList().ForEach(c => c.LoadContentData(_editedContent));
		}

		public void LoadSnapshot(Snapshot editedSnapshot)
		{
			_settingsControls.OfType<ISnapshotSettingsControl>().ToList().ForEach(c => c.LoadSnapshotData(editedSnapshot));
		}

		public void UpdateSettingsAccordingSelectedEditor(SnapshotEditorType editorType)
		{
			var selectedTabPage = xtraTabControlOptions.SelectedTabPage;
			xtraTabControlOptions.TabPages.Clear();
			var contentRelation = _snapshotEditorSettings.FirstOrDefault(r => r.EditorType == editorType);
			if (contentRelation != null)
			{
				xtraTabControlOptions.TabPages.AddRange(_settingsControls
					.Where(sc => sc.IsAvailable && contentRelation.SettingsTypes.Contains(sc.SettingsType))
					.OrderBy(sc => sc.Order)
					.OfType<XtraTabPage>()
					.Where(tp => tp.PageVisible)
					.ToArray());
				if (selectedTabPage != null && xtraTabControlOptions.TabPages.Contains(selectedTabPage))
					xtraTabControlOptions.SelectedTabPage = selectedTabPage;
			}

			SettingsControlsUpdated?.Invoke(this, EventArgs.Empty);
		}

		public void UpdateSettingsAccordingDataChanges(SnapshotEditorType editorType)
		{
			switch (editorType)
			{
				case SnapshotEditorType.Schedule:
					_settingsControls.OfType<SnapshotColumnSettingsControl>().First().UpdateUniversalSettingsToggleVisibility();
					break;
			}
		}

		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			if (args.ChangedSettingsType == SnapshotSettingsType.Schedule)
				_settingsControls.OfType<SummaryColumnSettingsControl>().First().LoadContentData(_editedContent);
			SettingsChanged?.Invoke(this, args);
		}

		private void OnSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			RaiseSettingsChanged(e);
		}
		#endregion
	}
}
