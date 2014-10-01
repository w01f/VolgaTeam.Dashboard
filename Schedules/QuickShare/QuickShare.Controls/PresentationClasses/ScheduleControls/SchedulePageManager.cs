using System.Collections.Generic;
using System.Linq;
using NewBizWiz.Core.QuickShare;

namespace NewBizWiz.QuickShare.Controls.PresentationClasses.ScheduleControls
{
	public class SchedulePageManager
	{
		private readonly Package _localPackage;
		public List<SchedulePage> SchedulePages { get;private set; }

		public SchedulePageManager(Package package)
		{
			_localPackage = package;
			SchedulePages = new List<SchedulePage>();
		}
		public void RebuildTabPages()
		{
			Controller.Instance.Ribbon.SuspendLayout();
			ClearRibbon();
			foreach (var schedule in _localPackage.Schedules)
			{
				var schedulePage = SchedulePages.FirstOrDefault(sp => sp.Schedule.Id == schedule.Id);
				if (schedulePage != null) continue;
				schedulePage = new SchedulePage(schedule);
				SchedulePages.Add(schedulePage);
			}
			var obsoletePages = SchedulePages.Where(sp => _localPackage.Schedules.All(s => s.Id != sp.Schedule.Id)).ToList();
			obsoletePages.ForEach(sp => sp.Dispose());
			foreach (var page in obsoletePages)
				SchedulePages.Remove(page);
			Controller.Instance.Ribbon.Controls.AddRange(SchedulePages.Select(sp => sp.Panel).ToArray());
			Controller.Instance.Ribbon.Items.AddRange(SchedulePages.Select(sp => sp.Tab).ToArray());
			Controller.Instance.Ribbon.ResumeLayout();
			SchedulePages.ForEach(sp => sp.Update());
		}

		private void ClearRibbon()
		{
			var panels = SchedulePages.Select(sp => sp.Panel).ToList();
			foreach (var panel in panels)
				Controller.Instance.Ribbon.Controls.Remove(panel);
			var tabPages = SchedulePages.Select(sp => sp.Tab).ToList();
			foreach (var tabItem in tabPages)
				Controller.Instance.Ribbon.Items.Remove(tabItem);
		}

		public void Dispose()
		{
			ClearRibbon();
			SchedulePages.ForEach(sp => sp.Dispose());
			SchedulePages.Clear();
		}
	}
}
