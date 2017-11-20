using System;
using System.Collections.Generic;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.PresentationClasses.Browser;
using Asa.Media.Controls.PresentationClasses.Calendar;
using Asa.Media.Controls.PresentationClasses.Digital.ContentEditors;
using Asa.Media.Controls.PresentationClasses.Gallery;
using Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors;
using Asa.Media.Controls.PresentationClasses.RateCard;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors;
using Asa.Media.Controls.PresentationClasses.SettingsControls;
using Asa.Media.Controls.PresentationClasses.Slides;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors;
using Asa.Media.Controls.PresentationClasses.Solutions;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using Asa.Schedules.Common.Controls.ContentEditors.Objects;
using DevComponents.DotNetBar;
using DevExpress.XtraLayout;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class ContentController : IContentController<MediaScheduleChangeInfo>
	{
		private readonly ContentTabStateManager _tabStateManager;
		public IContentControl ActiveControl { get; set; }
		public List<IContentControl> ContentControls { get; }
		public List<ContentEditorRelation> EditorRelations { get; }

		public event EventHandler<EventArgs> RibbonTabsStateChanged;

		public RibbonControl ContentRibbon => Controller.Instance.Ribbon;
		public LayoutControlItem MainPanel => Controller.Instance.MainPanel;
		public LayoutControlItem EmptyPanel => Controller.Instance.EmptyPanel;

		public IContentEditControl<MediaScheduleChangeInfo> ActiveEditor
			=> ActiveControl as IContentEditControl<MediaScheduleChangeInfo>;

		public IOutputControl ActiveOutputControl => ActiveControl as IOutputControl;
		public IThemeManagementControl ActiveThemeManagementControl => ActiveControl as IThemeManagementControl;

		public ContentController()
		{
			_tabStateManager = new ContentTabStateManager(this);
			ContentControls = new List<IContentControl>();
			EditorRelations = new List<ContentEditorRelation>();
			EditorRelations.AddRange(new[]{
				new ContentEditorRelation
				{
					Target = ContentEditManager<MediaScheduleChangeInfo>.DefaultContentIdentifier,
					Destrination = new[]
					{
						ContentIdentifiers.ScheduleSettings,
						ContentIdentifiers.ProgramSchedule,
						ContentIdentifiers.DigitalProducts,
						ContentIdentifiers.Snapshots,
						ContentIdentifiers.Options,
						ContentIdentifiers.Solutions,
						ContentIdentifiers.BroadcastCalendar,
						ContentIdentifiers.CustomCalendar
					}
				},
				new ContentEditorRelation
				{
					Target = ContentIdentifiers.ScheduleSettings,
					Destrination = new[]
					{
						ContentIdentifiers.ProgramSchedule,
						ContentIdentifiers.DigitalProducts,
						ContentIdentifiers.Snapshots,
						ContentIdentifiers.Options,
						ContentIdentifiers.Solutions,
						ContentIdentifiers.BroadcastCalendar,
						ContentIdentifiers.CustomCalendar
					}
				},
				new ContentEditorRelation
				{
					Target = ContentIdentifiers.ProgramSchedule,
					Destrination = new[]
					{
						ContentIdentifiers.BroadcastCalendar,
						ContentIdentifiers.Snapshots,
						ContentIdentifiers.Options,
					}
				},
				new ContentEditorRelation
				{
					Target = ContentIdentifiers.Snapshots,
					Destrination = new[]
					{
						ContentIdentifiers.BroadcastCalendar,
					}
				},
				new ContentEditorRelation
				{
					Target = ContentIdentifiers.DigitalProducts,
					Destrination = new[]
					{
						ContentIdentifiers.ScheduleSettings,
					}
				},
			});
		}

		public void Init()
		{
			foreach (var tabPageConfig in BusinessObjects.Instance.TabPageManager.TabPageSettings)
			{
				var contentEditControl = CreateContentEditor(tabPageConfig.Id);
				if (contentEditControl == null) continue;
				contentEditControl.TabPage.Text = tabPageConfig.Name;
				contentEditControl.TabPage.Visible = true;
				ContentControls.Add(contentEditControl);
			}
			ContentControls.ForEach(c => c.InitMetaData());
			ContentEditManager<MediaScheduleChangeInfo>.Init(this);
			BusinessObjects.Instance.ScheduleManager.ScheduleOpened +=
				ContentEditManager<MediaScheduleChangeInfo>.OnScheduleOpened;
			BusinessObjects.Instance.ThemeManager.ThemesChanged +=
				ContentEditManager<MediaScheduleChangeInfo>.OnThemeChanged;
			ContentEditManager<MediaScheduleChangeInfo>.ScheduleSaving +=
				(o, e) => BusinessObjects.Instance.ScheduleManager.ActiveSchedule.Save();
			ContentEditManager<MediaScheduleChangeInfo>.ScheduleSavingAs +=
				(o, e) => BusinessObjects.Instance.ScheduleManager.SaveScheduleAs(e.Name);
			ContentEditManager<MediaScheduleChangeInfo>.ScheduleSavingTemplate += OnSaveTemplate;
			_tabStateManager.Init();
		}

		public void UpdateTabsSate()
		{
			_tabStateManager.UpdateTabState();
			RibbonTabsStateChanged?.Invoke(this, EventArgs.Empty);
		}

		private static IContentControl CreateContentEditor(string id)
		{
			switch (id)
			{
				case ContentIdentifiers.ScheduleSettings:
					return new HomeControl();
				case ContentIdentifiers.ProgramSchedule:
					return new ScheduleContainer();
				case ContentIdentifiers.DigitalProducts:
					return new DigitalEditorsContainer();
				case ContentIdentifiers.Snapshots:
					return new SnapshotContentEditorsContainer();
				case ContentIdentifiers.Options:
					return new OptionsContentEditorsContainer();
				case ContentIdentifiers.BroadcastCalendar:
					return new BroadcastCalendarControl();
				case ContentIdentifiers.CustomCalendar:
					return new CustomCalendarControl();
				case ContentIdentifiers.Solutions:
					return new MediaSolutionsContainer();
				case ContentIdentifiers.Slides:
					return new MediaSlidesControl();
				case ContentIdentifiers.Gallery1:
					return new MediaGallery1Control();
				case ContentIdentifiers.Gallery2:
					return new MediaGallery2Control();
				case ContentIdentifiers.RateCard:
					return new MediaRateCardControl();
				case ContentIdentifiers.Browser:
					return new BrowserContentControl();
				default:
					return null;
			}
		}

		public void OnSaveSchedule(object sender, EventArgs e)
		{
			ContentEditManager<MediaScheduleChangeInfo>.SaveSchedule();
		}

		public void OnSaveAsSchedule(object sender, EventArgs e)
		{
			ContentEditManager<MediaScheduleChangeInfo>.SaveScheduleAs();
		}

		public void OnGetHelp(object sender, EventArgs e)
		{
			ActiveControl.GetHelp();
		}

		public void RaiseThemeChanged()
		{
			ContentEditManager<MediaScheduleChangeInfo>.RaiseThemeChanged(ActiveThemeManagementControl);
		}

		public void OnEditOutputSettings(object sender, EventArgs e)
		{
			ContentEditManager<MediaScheduleChangeInfo>.EditOutputSettings();
		}

		public void OnOutputPowerPoint(object sender, EventArgs e)
		{
			if (Controller.Instance.CheckPowerPointRunning())
				ContentEditManager<MediaScheduleChangeInfo>.OutputPowerPoint();
		}

		public void OnOutputPdf(object sender, EventArgs e)
		{
			if (Controller.Instance.CheckPowerPointRunning())
				ContentEditManager<MediaScheduleChangeInfo>.OutputPdf();
		}

		public void OnPreview(object sender, EventArgs e)
		{
			if (Controller.Instance.CheckPowerPointRunning())
				ContentEditManager<MediaScheduleChangeInfo>.Preview();
		}

		public void OnEmail(object sender, EventArgs e)
		{
			if (Controller.Instance.CheckPowerPointRunning())
				ContentEditManager<MediaScheduleChangeInfo>.Email();
		}

		public void OnSaveTemplate(object sender, ScheduleSavingEventArgs e)
		{
			FormProgress.ShowProgress("Saving Your Schedule Template…...", () =>
			{
				AsyncHelper.RunSync(() => BusinessObjects.Instance.ScheduleTemplatesManager.SaveTemplate(BusinessObjects.Instance.ScheduleManager.ActiveSchedule.GetTemplate(e.Name)));
			}, false);
		}
	}
}
