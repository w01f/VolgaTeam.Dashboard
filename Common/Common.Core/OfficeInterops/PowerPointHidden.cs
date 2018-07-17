using System;
using System.Threading;

namespace Asa.Common.Core.OfficeInterops
{
	public class PowerPointHidden : PowerPointProcessor, IDisposable
	{
		public void Dispose()
		{
			if (_isFirstLaunch)
				Close();
			Disconnect();
		}

		public bool DoTimeLimitedAction(Action action, int timeLimit = 120)
		{
			var actionDone = false;
			var actionIteroped = false;

			System.Threading.Tasks.Task.Run(() =>
			{
				Thread.Sleep(timeLimit * 1000);
				if (!actionDone)
				{
					Close();
					actionIteroped = true;
				}
			});

			try
			{
				action();
			}
			catch { }

			actionDone = true;

			return actionIteroped;
		}
	}
}
