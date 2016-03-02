﻿using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;

namespace AdSalesBrowser
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			AppDomain.CurrentDomain.AssemblyResolve += SharedAssemblyHelper.OnAssemblyResolve;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(FormMain.Instance);
		}
	}
}
