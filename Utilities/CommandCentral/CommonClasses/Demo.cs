using System;

namespace CommandCentral.CommonClasses
{
	public enum DemoType
	{
		Rtg = 0,
		Imp
	}

	public class Demo
	{
		public Demo()
		{
			Name = String.Empty;
			Source = String.Empty;
			Value = String.Empty;
		}

		public string Source { get; set; }
		public DemoType DemoType { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
}
