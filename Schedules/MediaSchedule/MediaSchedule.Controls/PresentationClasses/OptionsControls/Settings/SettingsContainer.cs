using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	public partial class SettingsContainer : UserControl
	{
		private readonly List<ISettingsControl> _settingsControls = new List<ISettingsControl>();
		private readonly List<OptionEditorSettingsRelation> _optionEditorSettings = new List<OptionEditorSettingsRelation>();
		private OptionsContent _editedContent;

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
			_optionEditorSettings.AddRange(new[]
			{
				new OptionEditorSettingsRelation
				{
					EditorType = OptionEditorType.Schedule,
					SettingsTypes = new []
					{
						OptionSettingsType.Schedule,
						OptionSettingsType.Colors,
					}
				} ,
				new OptionEditorSettingsRelation
				{
					EditorType = OptionEditorType.DigitalInfo,
					SettingsTypes = new []
					{
						OptionSettingsType.DigitalInfo,
					}
				},
				new OptionEditorSettingsRelation
				{
					EditorType = OptionEditorType.Summary,
					SettingsTypes = new []
					{
						OptionSettingsType.Summary,
						OptionSettingsType.Colors,
					}
				},
			});
		}

		private void InitSettingsControls()
		{
			_settingsControls.AddRange(new ISettingsControl[]
				{
					new OptionSetColumnSettingsControl(),
					new OptionsDigitalInfoSettingsControl(),
					new SummaryColumnSettingsControl(),
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
		public void LoadContent(OptionsContent editedContent)
		{
			_editedContent = editedContent;
			_settingsControls.OfType<IContentSettingsControl>().ToList().ForEach(c => c.LoadContentData(_editedContent));
		}

		public void LoadOptionSet(OptionSet editedOptionSet)
		{
			_settingsControls.OfType<IOptionSetSettingsControl>().ToList().ForEach(c => c.LoadOptionsSetData(editedOptionSet));
		}

		public void UpdateSettingsAccordingSelectedEditor(OptionEditorType editorType)
		{
			var selectedTabPage = xtraTabControlOptions.SelectedTabPage;
			xtraTabControlOptions.TabPages.Clear();
			var contentRelation = _optionEditorSettings.FirstOrDefault(r => r.EditorType == editorType);
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

		public void UpdateSettingsAccordingDataChanges(OptionEditorType editorType)
		{
			switch (editorType)
			{
				case OptionEditorType.Schedule:
					_settingsControls.OfType<OptionSetColumnSettingsControl>().First().UpdateUniversalSettingsToggleVisibility();
					break;
			}
		}

		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			if (args.ChangedSettingsType == OptionSettingsType.Schedule)
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
