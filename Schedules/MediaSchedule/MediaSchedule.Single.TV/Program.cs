﻿using System;
using System.Threading;
using System.Windows.Forms;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Single.TV
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			var force = args != null && args.Length > 0 && args[0].ToLower().Equals("force");

			bool firstInstance;
			var mutex = new Mutex(false, "Local\\TVSellerApplication", out firstInstance);
			if (firstInstance || force)
			{
				AppDomain.CurrentDomain.AssemblyResolve += SharedAssemblyHelper.OnAssemblyResolve;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				AppManager.Instance.RunApplication(MediaDataType.TVSchedule);
			}
			else
			{
				Utilities.ActivatePowerPoint(BusinessObjects.Instance.PowerPointManager.Processor.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
			GC.KeepAlive(mutex);
		}
	}
}
