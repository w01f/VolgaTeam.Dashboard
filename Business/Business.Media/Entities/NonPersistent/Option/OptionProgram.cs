using System;
using System.Drawing;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Option
{
	public class OptionProgram : IJsonCloneable<OptionProgram>
	{
		private string _name;

		public OptionSet Parent { get; set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public string Station { get; set; }
		public ImageSource Logo { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public string Length { get; set; }
		public decimal? Rate { get; set; }
		public int? Spot { get; set; }

		#region Calculated Properties
		[JsonIgnore]
		public string Name
		{
			get { return _name; }
			set
			{
				string oldValue = _name;
				_name = value;
				if (string.IsNullOrEmpty(oldValue))
					ApplyDefaultValues();
			}
		}

		public Image SmallLogo
		{
			get { return Logo != null ? Logo.TinyImage : null; }
		}

		public decimal? Cost
		{
			get { return Rate * Spot; }
		}
		#endregion

		[JsonConstructor]
		private OptionProgram() { }

		public OptionProgram(OptionSet parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Station = Parent.Parent.ScheduleSettings.Stations.Count(x => x.Available) == 1 ?
				Parent.Parent.ScheduleSettings.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() :
				null;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault();
		}

		public void Dispose()
		{
			Logo.Dispose();
			Logo = null;

			Parent = null;
		}

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.FirstOrDefault(x => x.Name.Equals(_name));
			if (source == null) return;
			Day = source.Day;
			Time = source.Time;
		}

		public void AfterClone(OptionProgram source, bool fullClone = true)
		{
			Parent = source.Parent;
			UniqueID = Guid.NewGuid();
		}
	}
}
