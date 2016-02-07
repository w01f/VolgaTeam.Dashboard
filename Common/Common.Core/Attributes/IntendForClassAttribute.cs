using System;

namespace Asa.Common.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class IntendForClassAttribute : Attribute
	{
		public Type BusinessObjectClass { get; private set; }

		public IntendForClassAttribute(Type businessObjectClass)
		{
			BusinessObjectClass = businessObjectClass;
		}
	}
}
