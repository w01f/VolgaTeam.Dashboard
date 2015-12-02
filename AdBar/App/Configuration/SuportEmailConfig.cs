using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asa.Bar.App.Configuration
{
	class SuportEmailConfig
	{
		public List<string> Emails { get; private set; }

		public SuportEmailConfig()
		{
			Emails = new List<string>();
		}

		public void Load()
		{
			var configPath = Path.Combine(Path.GetDirectoryName(typeof(SuportEmailConfig).Assembly.Location), "mailto.txt");
			if (!File.Exists(configPath))
				throw new FileNotFoundException("File mailto.txt not found");
			Emails.AddRange(File.ReadAllLines(configPath).Select(line => line.Trim()));
		}
	}
}
