﻿using System;
using Asa.Business.Media.Enums;

namespace Asa.Business.Media.Entities.NonPersistent.Schedule
{
	public class Demo
	{
		public string Source { get; set; }
		public DemoType DemoType { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public string DisplayString => String.Format("{0} {1}", DemoType == DemoType.Rtg ? "Rtg" : "(000s)", Name);

		public Demo()
		{
			Name = String.Empty;
			Source = String.Empty;
			Value = String.Empty;
		}

		public override string ToString()
		{
			return DisplayString;
		}
	}
}
