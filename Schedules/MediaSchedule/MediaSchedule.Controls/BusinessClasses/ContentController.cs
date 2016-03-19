using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Helpers;
using Asa.Common.GUI.ContentEditors.Interfaces;
using Asa.Common.GUI.ContentEditors.Objects;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.PresentationClasses.Calendar;
using Asa.Media.Controls.PresentationClasses.Digital;
using Asa.Media.Controls.PresentationClasses.Gallery;
using Asa.Media.Controls.PresentationClasses.OptionsControls;
using Asa.Media.Controls.PresentationClasses.RateCard;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors;
using Asa.Media.Controls.PresentationClasses.SettingsControls;
using Asa.Media.Controls.PresentationClasses.SnapshotControls;
using DevComponents.DotNetBar;

namespace Asa.Media.Controls.BusinessClasses
{
	public class ContentController : IContentController<MediaScheduleChangeInfo>
	{
		private readonly ContentTabStateManager _tabStateManager;
		public IContentControl ActiveControl { get; set; }
		public List<IContentControl> ContentControls { get; }
		public List<ContentEditorRelation> EditorRelations { get; }

		public event EventHandler<EventArgs> RibbonTabsStateChanged;

		public RibbonControl ContentRibbon
		{
			get { return Controller.Instance.Ribbon; }
		}
		public Panel MainPanel
		{
			get { return Controller.Instance.MainPanel; }
		}
		public Panel EmptyPanel
		{
			get { return Controller.Instance.EmptyPanel; }
		}

		public IContentEditControl<MediaScheduleChangeInfo> ActiveEditor
		{
			get { return ActiveControl as IContentEditControl<MediaScheduleChangeInfo>; }
			set { ActiveControl = value; }
		}

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
						ContentIdentifiers.DigitalPackages,
						ContentIdentifiers.Snapshots,
						ContentIdentifiers.Options,
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
						ContentIdentifiers.DigitalPackages,
						ContentIdentifiers.Snapshots,
						ContentIdentifiers.Options,
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
						ContentIdentifiers.DigitalPackages,
					}
				},
				new ContentEditorRelation
				{
					Target = ContentIdentifiers.DigitalPackages,
					Destrination = new[]
					{
						ContentIdentifiers.ScheduleSettings,
						ContentIdentifiers.DigitalProducts,
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
					return new DigitalProductContainerControl();
				case ContentIdentifiers.DigitalPackages:
					return new MediaWebPackageControl();
				case ContentIdentifiers.Snapshots:
					return new SnapshotContainer();
				case ContentIdentifiers.Options:
					return new OptionsContainer();
				case ContentIdentifiers.BroadcastCalendar:
					return new BroadcastCalendarControl();
				case ContentIdentifiers.CustomCalendar:
					return new CustomCalendarControl();
				case ContentIdentifiers.Gallery1:
					return new MediaGallery1Control();
				case ContentIdentifiers.Gallery2:
					return new MediaGallery2Control();
				case ContentIdentifiers.RateCard:
					return new MediaRateCardControl();
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
