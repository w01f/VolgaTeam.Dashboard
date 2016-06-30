using System.Linq;
using Asa.Common.Core.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class StandalonePackageRecord : BasePackageRecord, IJsonCloneable<StandalonePackageRecord>
	{
		public StandaloneDigitalPackage Parent { get; private set; }
		public decimal Index { get; set; }
		public override string Category { get; set; }
		public override string SubCategory { get; set; }
		public override string Name { get; set; }
		public override string Info { get; set; }
		public override string Comments { get; set; }
		public override decimal? Rate { get; set; }
		public override decimal? Investment { get; set; }
		public override decimal? Impressions { get; set; }
		public override decimal? CPM { get; set; }

		[JsonConstructor]
		private StandalonePackageRecord() { }

		public StandalonePackageRecord(StandaloneDigitalPackage parent) : base(parent)
		{
			Parent = parent;
			Index = parent.Items.Any() ? parent.Items.Max(s => s.Index) + 1 : 1;
		}

		public override void Dispose()
		{
			base.Dispose();
			Parent = null;
		}

		public void AfterClone(StandalonePackageRecord source, bool fullClone = true)
		{
			Parent = source.Parent;
		}
	}
}
