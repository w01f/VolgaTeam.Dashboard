﻿using Asa.Business.Solutions.Common.Enums;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public abstract class ClipartObject
	{
		public string Name { get; set; }
		public bool OutputBackground { get; set; }
		public abstract ClipartObjectType Type { get; }
		public abstract ClipartObject Clone();
	}
}
