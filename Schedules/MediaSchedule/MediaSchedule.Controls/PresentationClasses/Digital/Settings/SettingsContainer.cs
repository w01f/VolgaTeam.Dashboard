using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.PresentationClasses.Digital.ContentEditors;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	public partial class SettingsContainer : UserControl
	{
		private readonly List<ISettingsControl> _settingsControls = new List<ISettingsControl>();
		private readonly List<DigitalEditorSettingsRelation> _sectionEditorSettings = new List<DigitalEditorSettingsRelation>();
		private DigitalProductsContent _editedContent;

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
				new DigitalEditorSettingsRelation
				{
					SectionType = DigitalSectionType.List,
					SettingsTypes = new []
					{
						DigitalSettingsType.ProductList
					}
				} ,
				new DigitalEditorSettingsRelation
				{
					SectionType = DigitalSectionType.Products,
					SettingsTypes = new DigitalSettingsType[] {}
				} ,
				new DigitalEditorSettingsRelation
				{
					SectionType = DigitalSectionType.Summary,
					SettingsTypes = new DigitalSettingsType[] {}
				} ,
				new DigitalEditorSettingsRelation
				{
					SectionType = DigitalSectionType.ProductPackage,
					SettingsTypes = new []
					{
						DigitalSettingsType.ProductPackage
					}
				},
				new DigitalEditorSettingsRelation
				{
					SectionType = DigitalSectionType.StandalonePackage,
					SettingsTypes = new []
					{
						DigitalSettingsType.StandalonePackage
					}
				}
			});
		}

		private void InitSettingsControls()
		{
			_settingsControls.AddRange(new ISettingsControl[]
				{
					new DigitalListSettingsControl(),
					new DigitalProductPackageSettingsControl(),
					new DigitalStandalonePackageSettingsControl(),
				});
			foreach (var settingsDataControl in _settingsControls.OfType<ISettingsControl>())
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
		public void LoadContent(DigitalProductsContent editedContent)
		{
			_editedContent = editedContent;
			_settingsControls.OfType<ISettingsControl>().ToList().ForEach(c => c.LoadContentData(_editedContent));
		}

		public void UpdateSettingsAccordingSelectedSectionEditor(DigitalSectionType sectionType)
		{
			var selectedTabPage = xtraTabControlOptions.SelectedTabPage;
			xtraTabControlOptions.TabPages.Clear();
			var contentRelation = _sectionEditorSettings.FirstOrDefault(r => r.SectionType == sectionType);
			if (contentRelation != null)
			{
				xtraTabControlOptions.TabPages.AddRange(_settingsControls
					.Where(sc => contentRelation.SettingsTypes.Contains(sc.SettingsType))
					.OrderBy(sc => sc.Order)
					.OfType<XtraTabPage>()
					.Where(tp => tp.PageVisible)
					.ToArray());
				if (selectedTabPage != null && xtraTabControlOptions.TabPages.Contains(selectedTabPage))
					xtraTabControlOptions.SelectedTabPage = selectedTabPage;
			}
			SettingsControlsUpdated?.Invoke(this, EventArgs.Empty);
		}

		public void UpdateSettingsAccordingDataChanges(DigitalSectionType sectionType)
		{
			switch (sectionType)
			{
				case DigitalSectionType.ProductPackage:
					_settingsControls.OfType<DigitalProductPackageSettingsControl>().First().LoadContentData(_editedContent);
					break;
			}
		}

		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			SettingsChanged?.Invoke(this, args);
		}

		private void OnSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			RaiseSettingsChanged(e);
		}
		#endregion
	}
}
