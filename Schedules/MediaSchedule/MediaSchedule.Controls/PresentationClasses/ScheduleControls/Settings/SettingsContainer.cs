using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public partial class SettingsContainer : UserControl
	{
		private readonly List<ISettingsControl> _settingsControls = new List<ISettingsControl>();
		private readonly List<SectionEditorSettingsRelation> _sectionEditorSettings = new List<SectionEditorSettingsRelation>();
		private ProgramScheduleContent _editedContent;
		private ScheduleSection _editedSection;

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
			_sectionEditorSettings.AddRange(new[]
			{
				new SectionEditorSettingsRelation
				{
					EditorType = SectionEditorType.Schedule,
					SettingsTypes = new []
					{
						ScheduleSettingsType.Columns,
						ScheduleSettingsType.Totals,
						ScheduleSettingsType.Colors,
					}
				} ,
				new SectionEditorSettingsRelation
				{
					EditorType = SectionEditorType.DigitalInfo,
					SettingsTypes = new []
					{
						ScheduleSettingsType.DigitalInfo,
					}
				},
				new SectionEditorSettingsRelation
				{
					EditorType = SectionEditorType.CustomSummary,
					SettingsTypes = new []
					{
						ScheduleSettingsType.CustomSummary,
					}
				},
			});
		}

		private void InitSettingsControls()
		{
			_settingsControls.AddRange(new ISettingsControl[]
				{
					new SectionColumnSettingsControl(),
					new SectionTotalsSettingsControl(),
					new ColorSettingsControl(),
					new SectionDigitalInfoSettingsControl(),
					new CustomSummaryInfoControl(),
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
		public void LoadContent(ProgramScheduleContent editedContent)
		{
			_editedContent = editedContent;
			_settingsControls.OfType<IContentSettingsControl>().ToList().ForEach(c => c.LoadContentData(_editedContent));
		}

		public void LoadSection(ScheduleSection editedSection)
		{
			_editedSection = editedSection;
			_settingsControls.OfType<ISectionSettingsControl>().ToList().ForEach(c => c.LoadSectionData(_editedSection));
		}

		public void UpdateSettingsAccordingSelectedSectionEditor(SectionEditorType editorType)
		{
			var selectedTabPage = xtraTabControlOptions.SelectedTabPage;
			xtraTabControlOptions.TabPages.Clear();
			var contentRelation = _sectionEditorSettings.FirstOrDefault(r => r.EditorType == editorType);
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

		public void UpdateSettingsAccordingDataChanges(SectionEditorType editorType)
		{
			switch (editorType)
			{
				case SectionEditorType.Schedule:
					_settingsControls.OfType<SectionColumnSettingsControl>().First().UpdateUniversalSettingsToggleVisibility();
					break;
				case SectionEditorType.CustomSummary:
					_settingsControls.OfType<CustomSummaryInfoControl>().First().UpdateTotals();
					break;
			}
		}

		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			SettingsChanged?.Invoke(this, args);
		}

		private void OnSettingsChanged(Object sender, SettingsChangedEventArgs e)
		{
			RaiseSettingsChanged(e);
			if (e.ChangedSettingsType == ScheduleSettingsType.Columns)
				_settingsControls.OfType<SectionTotalsSettingsControl>().First().UpdateQuarterState();
		}
		#endregion
	}
}
