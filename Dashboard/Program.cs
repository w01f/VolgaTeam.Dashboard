﻿using System;
using System.Threading;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;

namespace Asa.Dashboard
{
	internal static class Program
	{
		private static Mutex _mutex;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			bool firstInstance;
			const string uniqueIdentifier = "Local\\NewBizWizApplication";
			_mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				AppDomain.CurrentDomain.AssemblyResolve += SharedAssemblyHelper.OnAssemblyResolve;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				AppManager.Instance.RunForm();
			}
			else
			{
				Utilities.ActivatePowerPoint(AppManager.Instance.PowerPointManager.Processor.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}