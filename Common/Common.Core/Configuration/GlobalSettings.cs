using System.IO;
using System.Reflection;

namespace Asa.Common.Core.Configuration
{
	public static class GlobalSettings
	{
		public static string ApplicationRootPath { get; private set; }

		static GlobalSettings()
		{
			ApplicationRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		}
	}
}
