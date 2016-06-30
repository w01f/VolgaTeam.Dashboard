using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Common;

namespace Asa.Business.Media.Entities.NonPersistent.Option
{
	public class OptionsContent : MediaScheduleContent
	{
		public List<OptionSet> Options { get; private set; }
		public OptionSummary OptionsSummary { get; private set; }

		public OptionsContent()
		{
			Options = new List<OptionSet>();
			OptionsSummary = new OptionSummary(this);
		}

		public override void Dispose()
		{
			Options.ForEach(o => o.Dispose());
			Options.Clear();

			OptionsSummary.Dispose();
			OptionsSummary = null;

			base.Dispose();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();

			foreach (var optionSet in Options)
			{
				optionSet.Parent = this;
				optionSet.AfterCreate();
			}

			RebuildOptionSetIndexes();
		}

		public void ChangeOptionSetPosition(int position, int newPosition)
		{
			if (position < 0 || position >= Options.Count) return;
			var optionSet = Options[position];
			optionSet.Index = newPosition - 0.5;
			RebuildOptionSetIndexes();
		}

		public void RebuildOptionSetIndexes()
		{
			var i = 0;
			foreach (var optionSet in Options.OrderBy(o => o.Index))
			{
				optionSet.Index = i;
				i++;
			}
			Options.Sort((x, y) => x.Index.CompareTo(y.Index));
		}
	}
}
