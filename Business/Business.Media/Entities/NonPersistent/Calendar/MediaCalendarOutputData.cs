using System.Linq;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class MediaCalendarOutputData : CalendarOutputData
	{
		[JsonConstructor]
		private MediaCalendarOutputData() { }

		public MediaCalendarOutputData(CalendarMonth parent)
			: base(parent)
		{
			ApplyForAllCustomComment = false;
			ShowLogo = false;
			Logo = MediaMetaData.Instance.ListManager.Images.SelectMany(g => g.Images)
					.OrderByDescending(i => i.IsDefault)
					.ThenBy(i => i.Name)
					.FirstOrDefault()
					?.Clone<ImageSource, ImageSource>();
		}
	}
}
