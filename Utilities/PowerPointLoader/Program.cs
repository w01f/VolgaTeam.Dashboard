﻿using System;
using NewBizWiz.Core.Common;
using NewBizWiz.PowerPointLoader.InteropClasses;

namespace NewBizWiz.PowerPointLoader
{
	internal static class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			MasterWizardManager.Instance.SetMasterWizard();
			LoaderPowerPointHelper.Instance.Connect();
			Utilities.Instance.ActivatePowerPoint(LoaderPowerPointHelper.Instance.PowerPointObject);
		}
	}
}