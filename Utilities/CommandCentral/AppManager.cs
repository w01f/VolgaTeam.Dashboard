using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using CommandCentral.Configuraion;

namespace CommandCentral
{
	public class AppManager
	{
		public static AppManager Instance { get; } = new AppManager();
		public ResourceManager AppResources { get; } = new ResourceManager();
		public Settings AppSettings { get; private set; }

		private AppManager() { }

		public void Run()
		{
			AppSettings = Settings.Load(AppResources.SettingsFilePath);

			var mainForm = new FormMain();
			mainForm.Closed += OnMainFormClosed;

			PopupMessageHelper.Instance.Title = mainForm.Text;

			Application.Run(mainForm);
		}

		private void OnMainFormClosed(object sender, EventArgs e)
		{
			Settings.Save(AppSettings, AppResources.SettingsFilePath);
		}
	}
}
