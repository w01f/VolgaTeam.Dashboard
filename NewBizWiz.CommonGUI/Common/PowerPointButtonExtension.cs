using System;
using DevComponents.DotNetBar;

namespace NewBizWiz.CommonGUI.Common
{
	public static class PowerPointButtonExtension
	{
		public static void AddEventHandler(this ButtonItem button, Func<bool> checkFunction, Action<object, EventArgs> targetAction)
		{
			button.Click += (sender, args) =>
			{
				if (!checkFunction()) return;
				targetAction(sender, args);
			};
		}

		public static void AddEventHandler(this ButtonItem button, Func<bool> checkFunction, Action targetAction)
		{
			button.Click += (sender, args) =>
			{
				if (!checkFunction()) return;
				targetAction();
			};
		}
	}
}
