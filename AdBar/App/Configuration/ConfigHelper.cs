using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Asa.Bar.App.Configuration
{
	static class ConfigHelper
	{
		public static string GetTextFromFile(string textFile)
		{
			if (!File.Exists(textFile)) return String.Empty;
			try
			{
				return File.ReadAllText(textFile);
			}
			catch
			{
				return String.Empty;
			}
		}

		public static String GetValueRegex(String expression, String input)
		{
			try
			{
				return new Regex(expression, RegexOptions.IgnoreCase).Match(input).Groups[1].Value;
			}
			catch
			{
			}
			return String.Empty;
		}

		public static List<string> GetValuesRegex(string expression, string input)
		{
			var list = new List<string>();
			try
			{
				list.AddRange(from Match m in new Regex(expression, RegexOptions.IgnoreCase).Matches(input) select m.Groups[1].Value);
			}
			catch
			{
			}
			return list;
		}
	}
}