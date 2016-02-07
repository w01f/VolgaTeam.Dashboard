using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Common.Core.Attributes;

namespace Asa.Common.Core.Helpers
{
	public static class ReflectionHelper
	{
		private static readonly Dictionary<string, Type> _sTypesDictionary = new Dictionary<string, Type>();

		private static object FindControlInTypes(Type baseType, Type intendedClass, IEnumerable<Type> assemblyTypes, object[] parameters)
		{
			var lKey = baseType.FullName + intendedClass.FullName;
			if (_sTypesDictionary.ContainsKey(lKey))
				return Activator.CreateInstance(_sTypesDictionary[lKey], parameters);

			foreach (var type in assemblyTypes)
			{
				if (type != baseType && !type.IsSubclassOf(baseType) && (!baseType.IsInterface || type.GetInterface(baseType.Name) == null)) continue;
				var attrs = type.GetCustomAttributes(typeof(IntendForClassAttribute), false);
				foreach (IntendForClassAttribute attr in attrs)
				{
					if (attr.BusinessObjectClass != intendedClass && !intendedClass.IsSubclassOf(attr.BusinessObjectClass)) continue;
					_sTypesDictionary.Add(lKey, type);
					return Activator.CreateInstance(type, parameters);
				}
			}
			return null;
		}

		public static object GetControlInstance(Type baseType, Type intendedClass, params object[] parameters)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var assemblyTypes = new List<Type>();
			foreach (var assembly in assemblies)
			{
				assemblyTypes.Clear();
				try
				{
					assemblyTypes.AddRange(assembly.GetTypes());
				}
				catch { continue; }
				if (!assemblyTypes.Any()) continue;
				var targetObject = FindControlInTypes(baseType, intendedClass, assemblyTypes, parameters);
				if (targetObject == null) continue;
				return targetObject;
			}
			return null;
		}
	}
}
